using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.Projectile;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class Changes_Monsters
    {
        public static BasicPickupDropTable DropTableForBossScav = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static PickupIndex ScavBossItem = PickupIndex.none;

        public static void Start()
        {
            Enemies();
            if (WConfig.cfgElderLemurianBands.Value)
            {
                IL.EntityStates.LemurianBruiserMonster.FireMegaFireball.FixedUpdate += MarriedLemurianBandActivator;
                On.RoR2.Util.GetBestBodyName += MarriedLemurianNameHook; //Maybe a little excessive idk

            }


            DropTableForBossScav.name = "dtScavRandomBoss";
            DropTableForBossScav.tier1Weight = 0;
            DropTableForBossScav.tier2Weight = 0;
            DropTableForBossScav.tier3Weight = 0;
            DropTableForBossScav.bossWeight = 1;
            DropTableForBossScav.bannedItemTags = new ItemTag[]
            {
                ItemTag.AIBlacklist,
                ItemTag.SprintRelated,
                ItemTag.CannotCopy,
            };


            GameObject BeadProjectileTrackingBomb = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Elites/EliteBead/BeadProjectileTrackingBomb.prefab").WaitForCompletion();
            if (WConfig.cfgTwistedBuff.Value)
            {
                On.RoR2.AffixBeadAttachment.OnEnable += AffixBeadAttachment_OnEnable;
                GameObject.Destroy(BeadProjectileTrackingBomb.transform.GetChild(0).GetComponent<HurtBoxGroup>());
                GameObject.Destroy(BeadProjectileTrackingBomb.transform.GetChild(0).GetChild(0).GetComponent<HurtBox>());

                BeadProjectileTrackingBomb.GetComponent<SphereCollider>().radius = 2;
            }
            if (WConfig.cfgTwistedBuffEpic.Value)
            {
                ProjectileDamage d = BeadProjectileTrackingBomb.GetComponent<ProjectileDamage>();
                // d.damageType.damageTypeExtended |= DamageTypeExtended.ApplyBuffPermanently;
                d.damageType.damageTypeExtended |= DamageTypeExtended.DisableAllSkills;
                d.damageType.damageTypeExtended &= ~DamageTypeExtended.ApplyBuffPermanently;

            }
            if (WConfig.cfgBrotherHurtFix.Value)
            {
                GameObject BrotherBody = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Brother/BrotherBody.prefab").WaitForCompletion();
                BrotherBody.GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;
                GameObject BrotherHurtBody = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Brother/BrotherHurtBody.prefab").WaitForCompletion();
                HurtBoxGroup component = BrotherHurtBody.GetComponentInChildren<HurtBoxGroup>();
                if (component)
                {
                    component.hurtBoxesDeactivatorCounter = 1;
                }
                //Seems to work entirely fine without any editing

                On.EntityStates.BrotherMonster.SpellChannelEnterState.OnEnter += SpellChannelEnterState_OnEnter;
                On.EntityStates.BrotherMonster.SpellChannelState.OnEnter += SpellChannelState_OnEnter;
                On.EntityStates.BrotherMonster.SpellChannelExitState.OnExit += SpellChannelExitState_OnExit;
            }

            On.RoR2.VoidRaidGauntletController.Start += VoidRaidGauntletController_Start;
            On.RoR2.ScriptedCombatEncounter.BeginEncounter += ScalingChanges;

            if (WConfig.cfgXIEliteFix.Value)
            {
                On.RoR2.NetworkedBodySpawnSlot.OnSpawnedServer += NetworkedBodySpawnSlot_OnSpawnedServer;
            }


            On.RoR2.CombatDirector.HalcyoniteShrineActivation += CombatDirector_HalcyoniteShrineActivation;
        }

        private static void CombatDirector_HalcyoniteShrineActivation(On.RoR2.CombatDirector.orig_HalcyoniteShrineActivation orig, CombatDirector self, float monsterCredit, DirectorCard chosenDirectorCard, int difficultyLevel, Transform shrineTransform)
        {
            orig(self, monsterCredit, chosenDirectorCard, difficultyLevel, shrineTransform);

        }

        private static void NetworkedBodySpawnSlot_OnSpawnedServer(On.RoR2.NetworkedBodySpawnSlot.orig_OnSpawnedServer orig, NetworkedBodySpawnSlot self, GameObject ownerBodyObject, SpawnCard.SpawnResult spawnResult, Action<MasterSpawnSlotController.ISlot, SpawnCard.SpawnResult> callback)
        {
            orig(self, ownerBodyObject, spawnResult, callback);

            if (spawnResult.success)
            {
                CharacterBody ownerBody = ownerBodyObject.GetComponent<CharacterBody>();
                Inventory spawnsInventory = spawnResult.spawnedInstance.GetComponent<Inventory>();
                if (ownerBody && spawnsInventory != null)
                {
                    spawnsInventory.SetEquipmentIndex(ownerBody.inventory.currentEquipmentIndex);
                }
            }
        }

        private static void VoidRaidGauntletController_Start(On.RoR2.VoidRaidGauntletController.orig_Start orig, VoidRaidGauntletController self)
        {
            orig(self);
            if (!NetworkServer.active)
            {
                return;
            }
            if (self.phaseEncounters.Length == 3)
            {
                if (WConfig.cfgVoidlingNerf.Value)
                {
                    self.phaseEncounters[0].onBeginEncounter += VoidlingPhase1Nerf;
                    self.phaseEncounters[1].onBeginEncounter += VoidlingPhase2Nerf;
                    self.phaseEncounters[2].onBeginEncounter += VoidlingPhase3Nerf;
                }
            }
        }

        private static void VoidlingPhase1Nerf(ScriptedCombatEncounter obj)
        {
            VoidlingScalingNerf(obj, 1);
        }
        private static void VoidlingPhase2Nerf(ScriptedCombatEncounter obj)
        {
            VoidlingScalingNerf(obj, 2);
        }
        private static void VoidlingPhase3Nerf(ScriptedCombatEncounter obj)
        {
            VoidlingScalingNerf(obj, 3);
        }
        public static void VoidlingScalingNerf(ScriptedCombatEncounter encounter, int phase)
        {
            if (!WConfig.cfgVoidlingNerf.Value)
            {
                return;
            }
            float multHp = 1;
            float multDmg = 1;
            //Reduction not overall
            switch (phase)
            {
                case 1:
                    multHp -= 0.6f;
                    multDmg -= 0.5f;
                    break;
                case 2:
                    multHp -= 0.8f;
                    multDmg -= 0.6f;
                    break;
                case 3:
                    multHp -= 0.9f;
                    multDmg -= 0.6f;
                    break;
            }
            Debug.Log("Voidling Nerf Phase " + phase);
            for (int i = 0; i < encounter.combatSquad.membersList.Count; i++)
            {
                Inventory inv = encounter.combatSquad.membersList[i].inventory;
                Debug.Log("Pre  Health: " + inv.GetItemCount(RoR2Content.Items.BoostHp) + " Damage: " + inv.GetItemCount(RoR2Content.Items.BoostDamage));
                inv.RemoveItem(RoR2Content.Items.BoostHp, (int)(multHp * (float)inv.GetItemCount(RoR2Content.Items.BoostHp)));
                inv.RemoveItem(RoR2Content.Items.BoostDamage, (int)(multDmg * (float)inv.GetItemCount(RoR2Content.Items.BoostDamage)));
                Debug.Log("Post Health: " + inv.GetItemCount(RoR2Content.Items.BoostHp) + " Damage: " + inv.GetItemCount(RoR2Content.Items.BoostDamage));
            }
        }

        public static void ScalingChanges(On.RoR2.ScriptedCombatEncounter.orig_BeginEncounter orig, ScriptedCombatEncounter self)
        {
            orig(self);
            if (!SceneInfo.instance)
                if (Run.instance.livingPlayerCount == 0)
                {
                    if (WConfig.cfgScavTwistedScaling.Value && self.name.StartsWith("ScavLunar"))
                    {
                        float extraHealth = 1f;
                        extraHealth += Run.instance.difficultyCoefficient / 2.5f;
                        float playerScaling = Mathf.Max(1, Run.instance.participatingPlayerCount * 0.9f);
                        extraHealth *= (Mathf.Pow((float)playerScaling, 0.5f));
                        Debug.Log("Replacing previous HP bonus with: currentBoostHpCoefficient=" + extraHealth);
                        for (int i = 0; i < self.combatSquad.membersList.Count; i++)
                        {
                            Inventory inv = self.combatSquad.membersList[i].inventory;
                            inv.RemoveItem(RoR2Content.Items.BoostHp, inv.GetItemCount(RoR2Content.Items.BoostHp));
                            inv.GiveItem(RoR2Content.Items.BoostHp, Mathf.RoundToInt((extraHealth - 1f) * 10f));
                        }
                    }
                }
        }


        private static void SpellChannelState_OnEnter(On.EntityStates.BrotherMonster.SpellChannelState.orig_OnEnter orig, EntityStates.BrotherMonster.SpellChannelState self)
        {
            orig(self);
            self.characterBody.AddBuff(RoR2Content.Buffs.Immune);
        }

        private static void SpellChannelExitState_OnExit(On.EntityStates.BrotherMonster.SpellChannelExitState.orig_OnExit orig, EntityStates.BrotherMonster.SpellChannelExitState self)
        {
            orig(self);
            self.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
            self.characterBody.RemoveBuff(RoR2Content.Buffs.Immune);
        }

        private static void SpellChannelEnterState_OnEnter(On.EntityStates.BrotherMonster.SpellChannelEnterState.orig_OnEnter orig, EntityStates.BrotherMonster.SpellChannelEnterState self)
        {
            orig(self);
            self.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
        }

        public static void AffixBeadAttachment_OnEnable(On.RoR2.AffixBeadAttachment.orig_OnEnable orig, AffixBeadAttachment self)
        {
            orig(self);
            self.cooldownAfterFiring = 2;
            self.damageHitCountTotal = 9;
        }




        public static void Enemies()
        {
            if (WConfig.cfgMendingCoreBuff.Value)
            {
                GameObject AffixEarthHealer = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/EliteEarth/AffixEarthHealerBody.prefab").WaitForCompletion();
                CharacterBody AffixEarthHealerBody = AffixEarthHealer.GetComponent<CharacterBody>();
                AffixEarthHealerBody.baseArmor += 400;
                AffixEarthHealerBody.baseDamage = 25;
                AffixEarthHealerBody.levelDamage = 5;

                HurtBox hurtbox = AffixEarthHealer.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<HurtBox>();
                hurtbox.isSniperTarget = false;
                hurtbox.isBullseye = false;

                On.EntityStates.AffixEarthHealer.Chargeup.OnEnter += AffixEarthHealthInvulThing;

            }


            //Scav
            On.RoR2.ScavengerItemGranter.Start += GiveScavMoreItems;
            LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScav").requiredFlags = RoR2.Navigation.NodeFlags.None;

            On.EntityStates.ScavMonster.FindItem.PickupIsNonBlacklistedItem += (orig, self, pickupIndex) =>
            {
                bool tempBool = orig(self, pickupIndex);
                if (tempBool == true)
                {
                    PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
                    ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);
                    return !(itemDef == null) && itemDef.DoesNotContainTag(ItemTag.SprintRelated) && itemDef.DoesNotContainTag(ItemTag.OnStageBeginEffect);
                }
                return tempBool;
            };



            //Lunar Scavs not picking up more items I think Idk If they even do that to begin with
            LegacyResourcesAPI.Load<GameObject>("Prefabs/charactermasters/ScavLunar1Master").GetComponents<RoR2.CharacterAI.AISkillDriver>()[1].maxTargetHealthFraction = -1;
            LegacyResourcesAPI.Load<GameObject>("Prefabs/charactermasters/ScavLunar2Master").GetComponents<RoR2.CharacterAI.AISkillDriver>()[1].maxTargetHealthFraction = -1;
            LegacyResourcesAPI.Load<GameObject>("Prefabs/charactermasters/ScavLunar3Master").GetComponents<RoR2.CharacterAI.AISkillDriver>()[1].maxTargetHealthFraction = -1;
            LegacyResourcesAPI.Load<GameObject>("Prefabs/charactermasters/ScavLunar4Master").GetComponents<RoR2.CharacterAI.AISkillDriver>()[1].maxTargetHealthFraction = -1;

            //Scav search item more reliably, stop searching when about to die
            RoR2.CharacterAI.AISkillDriver[] skilllist = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavMaster").GetComponents<RoR2.CharacterAI.AISkillDriver>();
            skilllist[1].maxUserHealthFraction = 1f;
            skilllist[1].minUserHealthFraction = 0.35f;
            skilllist[1].selectionRequiresOnGround = false;
            skilllist[1].maxDistance = 1000;
            skilllist[1].minDistance = 60;




            //
            //XI
            skilllist = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/MajorAndMinorConstruct/MegaConstructMaster.prefab").WaitForCompletion().GetComponents<RoR2.CharacterAI.AISkillDriver>();
            //0 SpawnMinorConstructs
            //1 Shield
            //2 FollowFast
            //3 ShootStep
            //4 FollowStep
            //5 StrafeStep
            //6 FleeStep
            //7 StopStep
            if (skilllist.Length > 6)
            {
                skilllist[6].enabled = false;
            }



        }

        private static void AffixEarthHealthInvulThing(On.EntityStates.AffixEarthHealer.Chargeup.orig_OnEnter orig, EntityStates.AffixEarthHealer.Chargeup self)
        {
            orig(self);
            if (self.characterBody)
            {
                //This way, we can have levelMaxHealth, without Desyncing the client.
                self.characterBody.inventory.GiveItem(RoR2Content.Items.BoostHp, 3 * Run.instance.ambientLevelFloor);
                self.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 0.4f);
            }
        }

        public static void GiveScavMoreItems(On.RoR2.ScavengerItemGranter.orig_Start orig, ScavengerItemGranter self)
        {
            if (!self.GetComponent<CharacterMaster>())
            {
                orig(self);
                return;
            }

            if (NetworkServer.active)
            {
                if (WConfig.cfgScavMoreItemsElites.Value == true)
                {
                    CharacterBody tempbod = self.GetComponent<CharacterMaster>().GetBody();
                    Inventory inventory = self.GetComponent<Inventory>();
                    //They are made Elite beforehand but dont count as Elite
                    bool isBoss = tempbod && tempbod.isBoss;
                    bool hasEliteEquip = inventory.currentEquipmentIndex != EquipmentIndex.None && inventory.currentEquipmentState.equipmentDef.passiveBuffDef && inventory.currentEquipmentState.equipmentDef.passiveBuffDef.isElite;

                    if (self.stackRollDataList.Length == 3)
                    {
                        int bossItemCount = 1;
                        if (hasEliteEquip)
                        {
                            //Vanilla is 3x3W 2x2G 1x1R
                            //B  3x3 2x2 1x2
                            //T1 4x4 2x3 2x2
                            //T2 5x6 3x4 2x3
                            bool isElite = inventory.GetItemCount(RoR2Content.Items.BoostHp) >= inventory.currentEquipmentState.equipmentDef.passiveBuffDef.eliteDef.healthBoostCoefficient * 10 - 10;
                            bool isT2Elite = isElite && inventory.currentEquipmentState.equipmentDef.passiveBuffDef.eliteDef.healthBoostCoefficient > 10;
                            if (RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.EliteOnly))
                            {
                                isElite = isT2Elite;
                                isT2Elite = false;
                            }
                            if (isT2Elite)
                            {
                                bossItemCount = 2;
                                //By the time you see Tier2 Elite Scav, go fuck yourself
                                //White 10 * 8
                                self.stackRollDataList[0].stacks = 5;
                                self.stackRollDataList[0].numRolls = 6;
                                //Green 8 * 5
                                self.stackRollDataList[1].stacks = 4;
                                self.stackRollDataList[1].numRolls = 5;
                                //Red 5 * 2
                                self.stackRollDataList[2].stacks = 2;
                                self.stackRollDataList[2].numRolls = 3;
                            }
                            else if (isElite)
                            {
                                bossItemCount = 2;
                                //White 5 * 5
                                self.stackRollDataList[0].stacks = 4;
                                self.stackRollDataList[0].numRolls = 4;
                                //Green 3 * 3
                                self.stackRollDataList[1].stacks = 2;
                                self.stackRollDataList[1].numRolls = 3;
                                //Red   2 * 2
                                self.stackRollDataList[2].stacks = 2;
                                self.stackRollDataList[2].numRolls = 2;
                            }
                        }
                        if (isBoss)
                        {
                            DeathRewards deathreward = tempbod.GetComponent<DeathRewards>();
                            if (deathreward)
                            {
                                deathreward.bossDropTable = DropTableForBossScav;
                            }
                            ItemDef tempdef = ItemCatalog.GetItemDef(ScavBossItem.pickupDef.itemIndex);
                            if (tempdef.DoesNotContainTag(ItemTag.AIBlacklist) && tempdef.DoesNotContainTag(ItemTag.SprintRelated))
                            {
                                Debug.Log("Giving Boss Scav " + tempdef);
                                inventory.GiveItem(tempdef, bossItemCount);
                            }
                            else
                            {
                                Debug.Log(tempdef + " is Blacklisted for Scavs, resorting to ShinyPearl");
                                inventory.GiveItem(RoR2Content.Items.ShinyPearl, bossItemCount);
                            }
                        }
                    }
                }
            }
            orig(self);
        }


        public static void MarriedLemurianBandActivator(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
                    x => x.MatchLdsfld("EntityStates.LemurianBruiserMonster.FireMegaFireball", "damageCoefficient")))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<float, EntityStates.LemurianBruiserMonster.FireMegaFireball, float>>((damageCoeff, entityState) =>
                {
                    if (entityState.characterBody.inventory.GetItemCount(RoR2Content.Items.Clover) >= 20)
                    {
                        //entityState.characterBody.ClearTimedBuffs(RoR2Content.Buffs.ElementalRingsCooldown);
                        return 4f;
                    }
                    return damageCoeff;
                });
                //Debug.Log("IL Found: Buff Married Lemurians");
            }
            else
            {
                Debug.LogWarning("IL Failed: Buff Married Lemurians");
            }
        }

        public static BodyIndex LemurianBruiser = BodyIndex.None;
        public static string MarriedLemurianNameHook(On.RoR2.Util.orig_GetBestBodyName orig, GameObject bodyObject)
        {
            if (bodyObject)
            {
                CharacterBody characterBody = bodyObject.GetComponent<CharacterBody>();
                if (characterBody && characterBody.bodyIndex == LemurianBruiser)
                {
                    if (characterBody.inventory.GetItemCount(RoR2Content.Items.Clover) >= 20)
                    {
                        if (characterBody.inventory.GetItemCount(RoR2Content.Items.FireRing) > 0)
                        {
                            return "Kjaro";
                        }
                        else if (characterBody.inventory.GetItemCount(RoR2Content.Items.IceRing) > 0)
                        {
                            return "Runald";
                        }
                    }
                }
            }
            return orig(bodyObject);
        }


    }
}