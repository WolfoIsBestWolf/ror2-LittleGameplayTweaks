using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;

namespace LittleGameplayTweaks
{
    public class ChangesCharacters
    {
        public static ItemDef MarriageLemurianIdentifier = ScriptableObject.CreateInstance<ItemDef>();
        public static BasicPickupDropTable DropTableForBossScav = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static PickupIndex ScavBossItem = PickupIndex.none;

        public static void Start()
        {
            Enemies();
            Characters();

            if (!WConfig.disableNewContent.Value)
            {
                MarriedEldersBands();
            }

            if (WConfig.LevelMaximumFinalBoss.Value > 98)
            {
                On.RoR2.ScriptedCombatEncounter.BeginEncounter += VoidlingLevelLimit;
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
            };


            On.RoR2.AffixBeadAttachment.OnEnable += AffixBeadAttachment_OnEnable;
        }

        private static void AffixBeadAttachment_OnEnable(On.RoR2.AffixBeadAttachment.orig_OnEnable orig, AffixBeadAttachment self)
        {
            orig(self);
            self.cooldownAfterFiring = 2;
        }

        private static void VoidlingLevelLimit(On.RoR2.ScriptedCombatEncounter.orig_BeginEncounter orig, ScriptedCombatEncounter self)
        {
            orig(self);

            if (self.grantUniqueBonusScaling)
            {
                if (Run.instance.ambientLevelFloor > WConfig.LevelMaximumFinalBoss.Value)
                {
                    for (int i = 0; i < self.combatSquad.membersList.Count; i++)
                    {
                        self.combatSquad.membersList[i].inventory.RemoveItem(RoR2Content.Items.UseAmbientLevel);
                        self.combatSquad.membersList[i].inventory.GiveItem(RoR2Content.Items.LevelBonus, WConfig.LevelMaximumFinalBoss.Value);
                    }
                }
            }
        }


        public static void Enemies()
        {
            if (WConfig.cfgMendingCoreBuff.Value)
            {
                GameObject AffixEarthHealerMaster = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/EliteEarth/AffixEarthHealerMaster.prefab").WaitForCompletion();
                AffixEarthHealerMaster.AddComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[]
                {
                    new GivePickupsOnStart.ItemInfo{itemString = "UseAmbientLevel", count = 1 }
                };
                CharacterBody AffixEarthHealerBody = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/EliteEarth/AffixEarthHealerBody.prefab").WaitForCompletion().GetComponent<CharacterBody>();
                AffixEarthHealerBody.baseArmor += 300;
                AffixEarthHealerBody.baseMaxHealth *= 3;
                AffixEarthHealerBody.levelMaxHealth *= 3;
                AffixEarthHealerBody.baseDamage *= 0.5f;
                AffixEarthHealerBody.levelDamage += AffixEarthHealerBody.baseDamage;

                On.EntityStates.AffixEarthHealer.Chargeup.OnEnter += (orig, self) =>
                {
                    orig(self);
                    if (self.characterBody)
                    {
                        //self.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
                        self.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 2f);
                    }
                };
            }


            GameObject FalseSon = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSonBoss/FalseSonBossBody.prefab").WaitForCompletion();
            FalseSon.GetComponent<SetStateOnHurt>().canBeFrozen = true;
            FalseSon = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSonBoss/FalseSonBossBodyLunarShard.prefab").WaitForCompletion();
            FalseSon.GetComponent<SetStateOnHurt>().canBeFrozen = true;
            FalseSon = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSonBoss/FalseSonBossBodyBrokenLunarShard.prefab").WaitForCompletion();
            FalseSon.GetComponent<SetStateOnHurt>().canBeFrozen = true;

            On.RoR2.ScriptedCombatEncounter.BeginEncounter += FalseSonAdaptive;


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
            //
            /*if (WConfig.cfgVoidlingNerf.Value)
            {
                CharacterBody Voidling = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidRaidCrab/MiniVoidRaidCrabBodyPhase1.prefab").WaitForCompletion().GetComponent<CharacterBody>();
                Voidling.baseDamage *= 0.6f;
                Voidling.levelDamage *= 0.6f;
                Voidling.baseMaxHealth *= 0.8f;
                Voidling.levelMaxHealth *= 0.8f;
                Voidling = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidRaidCrab/MiniVoidRaidCrabBodyPhase2.prefab").WaitForCompletion().GetComponent<CharacterBody>();
                Voidling.baseDamage *= 0.6f;
                Voidling.levelDamage *= 0.6f;
                Voidling.baseMaxHealth *= 0.9f;
                Voidling.levelMaxHealth *= 0.9f;
                Voidling = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidRaidCrab/MiniVoidRaidCrabBodyPhase3.prefab").WaitForCompletion().GetComponent<CharacterBody>();
                Voidling.baseDamage *= 0.6f;
                Voidling.levelDamage *= 0.6f;
                //Voidling.baseMaxHealth *= 0.9f;
                //Voidling.levelMaxHealth *= 0.9f;
            }*/




            RoR2.CharacterAI.AISkillDriver[] skilllist = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavMaster").GetComponents<RoR2.CharacterAI.AISkillDriver>();
            skilllist[1].maxUserHealthFraction = 1f;
            skilllist[1].minUserHealthFraction = 0.3f;
            skilllist[1].selectionRequiresOnGround = false;
            skilllist[1].maxDistance = 1000;
            skilllist[1].minDistance = 60;


            //Walkers Sprinting more
            skilllist = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/EngiWalkerTurretMaster").GetComponents<RoR2.CharacterAI.AISkillDriver>();
            skilllist[0].shouldSprint = true;
            if (skilllist[0].minDistance == 110)
            {
                skilllist[0].minDistance = 50;
            }

            //Equipment Drone fire Equipment even if no enemies
            skilllist = LegacyResourcesAPI.Load<GameObject>("Prefabs/charactermasters/EquipmentDroneMaster").GetComponents<RoR2.CharacterAI.AISkillDriver>();
            skilllist[0].shouldFireEquipment = true;
            skilllist[2].shouldFireEquipment = true;
            skilllist[5].shouldFireEquipment = true;
            skilllist[6].shouldFireEquipment = true;

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

        private static void FalseSonAdaptive(On.RoR2.ScriptedCombatEncounter.orig_BeginEncounter orig, ScriptedCombatEncounter self)
        {
            orig(self);

            if (Run.instance.stageClearCount > 4)
            {
                if (self.spawns.Length > 0 && self.spawns[0].spawnCard.name.StartsWith("cscFalse"))
                {
                    for(int i = 0; i < self.combatSquad.membersList.Count; i++)
                    {
                        Inventory inventory = self.combatSquad.membersList[i].inventory;
                        if (inventory.GetItemCount(RoR2Content.Items.AdaptiveArmor) == 0)
                        {
                            inventory.GiveItem(RoR2Content.Items.AdaptiveArmor);
                        }
                    }
                }
            }

        }

        public static void Characters()
        {
            if(WConfig.BuffMegaDroneStats.Value)
            {
                GameObject MegaDroneBody = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/MegaDroneBody.prefab").WaitForCompletion();
                MegaDroneBody.GetComponent<CharacterBody>().baseArmor = 20;
                MegaDroneBody.GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.ResistantToAOE;
            }
           
            //Hold down button to fire multiple
            Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>(key: "RoR2/Base/Commando/CommandoBodyFireFMJ.asset").WaitForCompletion().mustKeyPress = false;
            Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>(key: "RoR2/Base/Croco/CrocoSpit.asset").WaitForCompletion().mustKeyPress = false;
            Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>(key: "RoR2/Base/LunarSkillReplacements/LunarSecondaryReplacement.asset").WaitForCompletion().mustKeyPress = true;

            //This should like Barley Change anything but still make it configs
            On.EntityStates.Merc.Weapon.GroundLight2.OnEnter += (orig, self) =>
            {
                if (self.isComboFinisher == true)
                {
                    self.ignoreAttackSpeed = true;
                }
                orig(self);
            };
            On.EntityStates.Merc.Uppercut.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.duration < 0.15f)
                {
                    self.duration = 0.15f;
                }
            };


            //Huntress multiple Ballistas at once
            if (WConfig.CharactersHuntressLysateCell.Value == true)
            {
                On.EntityStates.Huntress.AimArrowSnipe.OnEnter += (orig, self) =>
                {
                    orig(self);

                    //Debug.LogWarning(self.skillLocator.special.finalRechargeInterval);
                    //Debug.LogWarning(self.skillLocator.special.stock);

                    int specialStock = self.skillLocator.special.stock;
                    int baseMaxStock = self.primarySkillSlot.skillDef.baseMaxStock;
                    if (specialStock > 1)
                    {
                        specialStock = 1;
                    }
                    else if (self.skillLocator.special.finalRechargeInterval == 0.5)
                    {
                        specialStock--;
                    }

                    int stock = baseMaxStock * (specialStock + 1);
                    if (specialStock >= 0)
                    {
                        self.skillLocator.special.DeductStock(specialStock);
                    }
                    // if (stock < 3) { stock= 3; }
                    self.skillLocator.primary.stock = stock;
                };
            }


            if (WConfig.CharactersCaptainKeepInHiddemRealm.Value == true)
            {
                SceneCatalog.availability.CallWhenAvailable(NoOrbitalStrikeBlocking);
            }

            On.EntityStates.Croco.Spawn.OnEnter += (orig, self) =>
            {
                orig(self);
                if (NetworkServer.active)
                {
                    self.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
                }
            };
            On.EntityStates.Croco.Spawn.OnExit += (orig, self) =>
            {
                orig(self);
                if (NetworkServer.active)
                {
                    self.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
                    self.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 3f);
                }
            };

            if (WConfig.CharactersCommandoInvul.Value)
            {
                On.EntityStates.Commando.DodgeState.OnEnter += (orig, self) =>
                {
                    orig(self);
                    if (NetworkServer.active)
                    {
                        self.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
                    }
                };
                On.EntityStates.Commando.DodgeState.OnExit += (orig, self) =>
                {
                    orig(self);
                    if (NetworkServer.active)
                    {
                        self.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
                    }
                };
                LanguageAPI.Add("COMMANDO_UTILITY_DESCRIPTION", "<style=cIsUtility>Roll</style> a short distance. You <style=cIsUtility>cannot be hit</style> while rolling.");

            }

            if (WConfig.CharactersVoidFiendEquip.Value)
            {
                On.EntityStates.VoidSurvivor.CorruptMode.CorruptModeBase.OnEnter += (orig, self) =>
                {
                    orig(self);
                    if (self is EntityStates.VoidSurvivor.CorruptMode.CorruptMode)
                    {
                        self.characterBody.inventory.SetActiveEquipmentSlot(1);
                    }
                    else
                    {
                        self.characterBody.inventory.SetActiveEquipmentSlot(0);
                    }
                };
            }
            

        }



        private static void GiveScavMoreItems(On.RoR2.ScavengerItemGranter.orig_Start orig, ScavengerItemGranter self)
        {
            if (!self.GetComponent<CharacterMaster>())
            {
                orig(self);
                return;
            }

            if (WConfig.cfgScavMoreItemsElites.Value == true)
            {
                if (NetworkServer.active)
                {
                    CharacterBody tempbod = self.GetComponent<CharacterMaster>().GetBody();
                    Inventory inventory = self.GetComponent<Inventory>();
                    //They are made Elite beforehand but dont count as Elite

                    bool hasEliteEquip = inventory.currentEquipmentIndex != EquipmentIndex.None && inventory.currentEquipmentState.equipmentDef.passiveBuffDef && inventory.currentEquipmentState.equipmentDef.passiveBuffDef.isElite;

                    if (self.stackRollDataList.Length == 3)
                    {
                        if (hasEliteEquip)
                        {
                            bool isElite = inventory.GetItemCount(RoR2Content.Items.BoostHp) >= inventory.currentEquipmentState.equipmentDef.passiveBuffDef.eliteDef.healthBoostCoefficient * 10 - 10;
                            bool isT2Elite = isElite && inventory.currentEquipmentState.equipmentDef.passiveBuffDef.eliteDef.healthBoostCoefficient > 10;

                            if (RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.EliteOnly))
                            {
                                isElite = inventory.GetItemCount(RoR2Content.Items.BoostHp) > 80;
                                isT2Elite = false;
                            }

                            if (isT2Elite)
                            {
                                //By the time you see Tier2 Elite Scav, go fuck yourself
                                //White 10 * 8
                                self.stackRollDataList[0].stacks = 7;
                                self.stackRollDataList[0].numRolls = 7;
                                //Green 8 * 5
                                self.stackRollDataList[1].stacks = 5;
                                self.stackRollDataList[1].numRolls = 5;
                                //Red 5 * 2
                                self.stackRollDataList[2].stacks = 2;
                                self.stackRollDataList[2].numRolls = 3;
                            }
                            else if (isElite)
                            {
                                //White 5 * 5
                                self.stackRollDataList[0].stacks = 5;
                                self.stackRollDataList[0].numRolls = 5;
                                //Green 3 * 3
                                self.stackRollDataList[1].stacks = 3;
                                self.stackRollDataList[1].numRolls = 4;
                                //Red   2 * 2
                                self.stackRollDataList[2].stacks = 2;
                                self.stackRollDataList[2].numRolls = 2;
                            }
                            else if (tempbod && tempbod.isBoss)
                            {
                                self.stackRollDataList[2].numRolls += 1; //+1 Red
                            }
                        }
                    }
                }
            }

            orig(self);
            if (WConfig.cfgScavBossItem.Value)
            {
                CharacterBody tempbod = self.GetComponent<CharacterMaster>().GetBody();
                Inventory tempinv = self.GetComponent<Inventory>();
                //Debug.LogWarning(tempbod);
                if (tempbod && tempbod.isBoss)
                {
                    DeathRewards deathreward = tempbod.GetComponent<DeathRewards>();
                    if (deathreward)
                    {
                        deathreward.bossDropTable = DropTableForBossScav;
                    }
                    ItemDef tempdef = ItemCatalog.GetItemDef(ScavBossItem.pickupDef.itemIndex);
                    int ItemCount = 1;
                    if (tempinv.currentEquipmentIndex != EquipmentIndex.None && tempinv.currentEquipmentState.equipmentDef.passiveBuffDef && tempinv.currentEquipmentState.equipmentDef.passiveBuffDef.isElite)
                    {
                        ItemCount = 2;
                    }
                    else
                    {
                        tempinv.GiveItem(RoR2Content.Items.BoostHp, 5);
                    }
                    if (tempdef.DoesNotContainTag(ItemTag.AIBlacklist) && tempdef.DoesNotContainTag(ItemTag.SprintRelated))
                    {
                        Debug.Log("Giving Boss Scav " + tempdef);
                        tempinv.GiveItem(tempdef, ItemCount);
                    }
                    else
                    {
                        Debug.Log(tempdef + " is Blacklisted for Scavs, resorting to ShinyPearl");
                        tempinv.GiveItem(RoR2Content.Items.ShinyPearl, ItemCount);
                    }
                }
            }
        }

 

        public static void NoOrbitalStrikeBlocking()
        {
            for (int i = 0; i < SceneCatalog.allSceneDefs.Length; i++)
            {
                //Debug.LogWarning(SceneCatalog.allSceneDefs[i] + " " +  SceneCatalog.allSceneDefs[i].stageOrder);
                SceneCatalog.allSceneDefs[i].blockOrbitalSkills = false;
            }
        }

        public static void MarriedEldersBands()
        {
            IL.EntityStates.LemurianBruiserMonster.FireMegaFireball.FixedUpdate += MarriedLemurianBandActivator;
            On.RoR2.Util.GetBestBodyName += MarriedLemurianNameHook; //Maybe a little excessive idk

            ItemDef AACannon = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AACannon");
            MarriageLemurianIdentifier.name = "MarriageLemurianIdentifier";
            MarriageLemurianIdentifier.deprecatedTier = ItemTier.NoTier;
            MarriageLemurianIdentifier._itemTierDef = AACannon._itemTierDef;
            MarriageLemurianIdentifier.nameToken = "MarriageLemurianIdentifier";
            MarriageLemurianIdentifier.pickupToken = "MarriageLemurianIdentifier";
            MarriageLemurianIdentifier.descriptionToken = "MarriageLemurianIdentifier";
            MarriageLemurianIdentifier.hidden = false;
            MarriageLemurianIdentifier.canRemove = false;
            MarriageLemurianIdentifier.pickupIconSprite = AACannon.pickupIconSprite;
            MarriageLemurianIdentifier.pickupModelPrefab = AACannon.pickupModelPrefab;

            RecalculateStatsAPI.GetStatCoefficients += delegate (CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args)
            {
                bool flag = sender.inventory != null;
                if (flag)
                {
                    if (sender.inventory.GetItemCount(MarriageLemurianIdentifier) > 0)
                    {
                        args.damageMultAdd += -0.5f;
                        args.healthMultAdd += -0.5f;
                    }
                }
            };


            CustomItem customItem = new CustomItem(MarriageLemurianIdentifier, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);
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
                    if (entityState.characterBody.inventory.GetItemCount(MarriageLemurianIdentifier) == 1)
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

        public static string MarriedLemurianNameHook(On.RoR2.Util.orig_GetBestBodyName orig, GameObject bodyObject)
        {
            string temp = orig(bodyObject);

            if (bodyObject)
            {
                CharacterBody characterBody = bodyObject.GetComponent<CharacterBody>();
                if (characterBody && characterBody.inventory)
                {
                    if (characterBody.inventory.GetItemCount(MarriageLemurianIdentifier) > 0)
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
            return temp;
        }


    }
}