using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.Projectile;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
 
namespace LittleGameplayTweaks
{
    public class Changes_Monsters
    {
        public static BasicPickupDropTable DropTableForBossScav = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        
        public static void CallLate()
        {
            GameObject UrchinTurretMaster = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ElitePoison/UrchinTurretMaster.prefab").WaitForCompletion();
            UrchinTurretMaster.AddComponent<GivePickupsOnStart>().itemDefInfos = new GivePickupsOnStart.ItemDefInfo[]
            {
                new GivePickupsOnStart.ItemDefInfo
                {
                    itemDef = RoR2Content.Items.UseAmbientLevel,
                    count = 1,
                }
            };
           /* GivePickupsOnStart.ItemDefInfo[] teleportWhenOob = new GivePickupsOnStart.ItemDefInfo[]
            {
                new GivePickupsOnStart.ItemDefInfo
                {
                    itemDef = RoR2Content.Items.TeleportWhenOob,
                    count = 1,
                }
            };
            GameObject MagmaWormMaster = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/MagmaWorm/MagmaWormMaster.prefab").WaitForCompletion();
            MagmaWormMaster.AddComponent<GivePickupsOnStart>().itemDefInfos = teleportWhenOob;
            GameObject ElectricWormMaster = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ElectricWorm/ElectricWormMaster.prefab").WaitForCompletion();
            ElectricWormMaster.AddComponent<GivePickupsOnStart>().itemDefInfos = teleportWhenOob;   */



        }

        public static void Start()
        {
            Enemies();
            if (WConfig.cfgElderLemurianBands.Value)
            {
                IL.EntityStates.LemurianBruiserMonster.FireMegaFireball.FixedUpdate += MarriedLemurianBandActivator;
 
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
 
            GameObject BeadProjectileTrackingBomb = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Elites/EliteBead/BeadProjectileTrackingBomb.prefab").WaitForCompletion();
            if (WConfig.cfgTwistedBuff.Value)
            {
                On.RoR2.AffixBeadAttachment.Initialize += AffixBeadAttachment_Initialize;
 
                GameObject.Destroy(BeadProjectileTrackingBomb.transform.GetChild(0).GetComponent<HurtBoxGroup>());
                GameObject.Destroy(BeadProjectileTrackingBomb.transform.GetChild(0).GetChild(0).GetComponent<HurtBox>());

                //BeadProjectileTrackingBomb.GetComponent<ProjectileImpactExplosion>().blastRadius++;
                //BeadProjectileTrackingBomb.GetComponent<SphereCollider>().radius = 1.5f;
            }
            if (WConfig.cfgTwistedBuffEpic.Value)
            {
                ProjectileDamage d = BeadProjectileTrackingBomb.GetComponent<ProjectileDamage>();
                // d.damageType.damageTypeExtended |= DamageTypeExtended.ApplyBuffPermanently;
                d.damageType.damageTypeExtended |= DamageTypeExtended.DisableAllSkills;
                d.damageType.damageType &= ~DamageType.LunarRuin;
                d.damageType.damageTypeExtended &= ~DamageTypeExtended.ApplyBuffPermanently;

            }
            if (WConfig.cfgBrotherHurtFix.Value)
            {
                GameObject BrotherBody = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Brother/BrotherBody.prefab").WaitForCompletion();
                BrotherBody.GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;
 
                On.EntityStates.BrotherMonster.SpellChannelState.OnEnter += SpellChannelState_OnEnter;
                On.EntityStates.BrotherMonster.SpellChannelExitState.OnExit += SpellChannelExitState_OnExit;
            }

            On.RoR2.VoidRaidGauntletController.Start += VoidRaidGauntletController_Start;
            On.RoR2.ScriptedCombatEncounter.BeginEncounter += ScalingChanges;

            if (WConfig.cfgXIEliteFix.Value)
            {
                On.RoR2.NetworkedBodySpawnSlot.OnSpawnedServer += NetworkedBodySpawnSlot_OnSpawnedServer;
            }
          
            KillableProjectileScaling();


            //Barnacle Nullify on hit
            //Addressables.LoadAssetAsync<GameObject>(key: "3432ca070cbe99347896361003887b2d").WaitForCompletion().GetComponent<ProjectileDamage>().damageType.damageType |= DamageType.Nullify;

        }

        private static void AffixBeadAttachment_Initialize(On.RoR2.AffixBeadAttachment.orig_Initialize orig, AffixBeadAttachment self)
        {
            orig(self);
            self.trackedBodies.Add(self.networkedBodyAttachment.attachedBody.gameObject);
            self.damageCooldown = 0.1f; //Due to the more rapid fire nature
            self.cooldownAfterFiring = 0.1f; //0 cooldown
            self.fireDelayTimer = 2f; //But longer windup
            self.damageHitCountTotal = 10; //Just shoot them out a lot but easy to dodge?
        }
 

        public static void KillableProjectileScaling()
        {
            //Armor instead of Health because that doesnt need to be synced to client ig.
            CharacterBody body = null;
            body = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Vagrant/VagrantTrackingBomb.prefab").WaitForCompletion().GetComponent<CharacterBody>();
            body.bodyFlags |= CharacterBody.BodyFlags.UsesAmbientLevel;
            body.levelMaxHealth = 0;
            body.levelArmor = 30;
            body = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/LunarWisp/LunarWispTrackingBomb.prefab").WaitForCompletion().GetComponent<CharacterBody>();
            body.bodyFlags |= CharacterBody.BodyFlags.UsesAmbientLevel;
            body.levelMaxHealth = 0;
            body.levelArmor = 30;
            body = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Gravekeeper/GravekeeperTrackingFireball.prefab").WaitForCompletion().GetComponent<CharacterBody>();
            body.bodyFlags |= CharacterBody.BodyFlags.UsesAmbientLevel;
            body.levelMaxHealth = 0;
            body.levelArmor = 30;
            body = Addressables.LoadAssetAsync<GameObject>(key: "de83659161b919844b1309bc9aaa3c71").WaitForCompletion().GetComponent<CharacterBody>();
            body.bodyFlags |= CharacterBody.BodyFlags.UsesAmbientLevel;
            body.levelMaxHealth = 0;
            body.levelArmor = 30;
            body = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Scorchling/ScorchlingBombProjectile.prefab").WaitForCompletion().GetComponent<CharacterBody>();
            body.bodyFlags |= CharacterBody.BodyFlags.UsesAmbientLevel;
            body.levelMaxHealth = 0;
            body.levelArmor = 30;

      
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
            float multHp = 0;
            float multDmg = 0;
            //Reduction not overall
            switch (phase)
            {
                case 1:
                    multHp = 0.4f;
                    multDmg = 0.2f;
                    break;
                case 2:
                    multHp = 0.2f;
                    multDmg = 0.2f;
                    break;
                case 3:
                    multHp = 0f;
                    multDmg = 0.2f;
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
                    if (self.name.StartsWith("ScavLunar"))
                    {
                        if (WConfig.cfgScavTwistedScaling.Value)
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
                        for (int i = 0; i < self.combatSquad.membersList.Count; i++)
                        {
                            Inventory inv = self.combatSquad.membersList[i].inventory;
                            inv.RemoveItem(RoR2Content.Items.BoostDamage, (int)(0.2f*inv.GetItemCount(RoR2Content.Items.BoostDamage)));
                        }
                    }
                }
        }


        private static void SpellChannelState_OnEnter(On.EntityStates.BrotherMonster.SpellChannelState.orig_OnEnter orig, EntityStates.BrotherMonster.SpellChannelState self)
        {
            orig(self);
            self.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
            self.characterBody.AddBuff(RoR2Content.Buffs.Immune);
        }

        private static void SpellChannelExitState_OnExit(On.EntityStates.BrotherMonster.SpellChannelExitState.orig_OnExit orig, EntityStates.BrotherMonster.SpellChannelExitState self)
        {
            orig(self);
            self.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
            self.characterBody.RemoveBuff(RoR2Content.Buffs.Immune);
        }

 

        public static void AffixBeadAttachment_OnEnable(On.RoR2.AffixBeadAttachment.orig_OnEnable orig, AffixBeadAttachment self)
        {
            orig(self);
            self.trackedBodies.Add(self.networkedBodyAttachment.attachedBody.gameObject);
            self.damageCooldown = 0.05f;
            self.cooldownAfterFiring = 0.1f; //0 cooldown
            self.fireDelayTimer = 1f; //Shorter windup
            //self.damageHitCountTotal = 10;
        }




        public static void Enemies()
        {
            if (WConfig.cfgMendingCoreBuff.Value)
            {
                GameObject AffixEarthHealer = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/EliteEarth/AffixEarthHealerBody.prefab").WaitForCompletion();
                CharacterBody AffixEarthHealerBody = AffixEarthHealer.GetComponent<CharacterBody>();
                AffixEarthHealerBody.baseArmor = 500;
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
            var searchItem = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavMaster").GetComponents<RoR2.CharacterAI.AISkillDriver>()[1];
            searchItem.maxUserHealthFraction = 1f;
            searchItem.minUserHealthFraction = 0.4f;
            searchItem.selectionRequiresOnGround = false;
            searchItem.maxDistance = 300;
            searchItem.minDistance = 60;
            searchItem.minTargetHealthFraction = 0.4f;

                       //
             //XI
            //var skilllist = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/MajorAndMinorConstruct/MegaConstructMaster.prefab").WaitForCompletion().GetComponents<RoR2.CharacterAI.AISkillDriver>();
            //0 SpawnMinorConstructs
            //1 Shield
            //2 FollowFast
            //3 ShootStep
            //4 FollowStep
            //5 StrafeStep
            //6 FleeStep
            //7 StopStep
            /*if (skilllist.Length > 6)
            {
                skilllist[6].enabled = false;
            }*/
 
            if (WConfig.cfgOverloadingWorm.Value)
            {

                //Electric Worm Tweaks

                GameObject ElectricWormBody = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ElectricWorm/ElectricWormBody.prefab").WaitForCompletion();
                //ElectricWormBody.GetComponent<CharacterBody>().baseMoveSpeed = 40;
                WormBodyPositions2 elecWorm = ElectricWormBody.GetComponent<WormBodyPositions2>();
                //elecWorm.meatballCount = 5;
                elecWorm.blastAttackForce *= 2;
                elecWorm.meatballForce *= 2;

                GameObject ElectricOrbProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ElectricWorm/ElectricOrbProjectile.prefab").WaitForCompletion();
                ProjectileImpactExplosion elecOrb = ElectricOrbProjectile.GetComponent<ProjectileImpactExplosion>();
                elecOrb.useChildRotation = false;
                elecOrb.childrenCount = 2;
                elecOrb.childrenDamageCoefficient = 0.5f;
                elecOrb.minPitchDegrees = 90;
                elecOrb.rangePitchDegrees = 90;
                elecOrb.rangeYawDegrees = 360;
                GameObject ElectricWormSeekerProjectile = elecOrb.childrenProjectilePrefab;
                ElectricWormSeekerProjectile.GetComponent<ProjectileSimple>().desiredForwardSpeed = 30;
                ProjectileImpactExplosion wormProj2 = ElectricWormSeekerProjectile.GetComponent<ProjectileImpactExplosion>();
                wormProj2.childrenCount = 1;
                wormProj2.childrenDamageCoefficient = 0.5f;
                wormProj2.fireChildren = true;
                wormProj2.childrenProjectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/LightningStake");

            }


            Addressables.LoadAssetAsync<GameObject>(key: "746b53f076ca9af4d89f67c981d2bbf9").WaitForCompletion().GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath; //ScavLunar
            Addressables.LoadAssetAsync<GameObject>(key: "a0a8fa4272069874b9e538c59bbda5ed").WaitForCompletion().GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath; //ScavLunar
            Addressables.LoadAssetAsync<GameObject>(key: "7dfb4548829852a49a4b2840046787ed").WaitForCompletion().GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath; //ScavLunar
            Addressables.LoadAssetAsync<GameObject>(key: "769510dc6be546b40aa3aca3cf93945c").WaitForCompletion().GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath; //ScavLunar
            //Addressables.LoadAssetAsync<CharacterBody>(key: "18809d9c5863c864c8d8ca0d2309556d").WaitForCompletion().bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath; //ScavLunar
            Addressables.LoadAssetAsync<GameObject>(key: "097b0e271757ce24581d4a8983d2c941").WaitForCompletion().GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath; //VoidMegaCrab
            Addressables.LoadAssetAsync<GameObject>(key: "285347cf04a9df04b9dada8fed09832f").WaitForCompletion().GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath; //VoidMegaCrab

            Addressables.LoadAssetAsync<GameObject>(key: "097b0e271757ce24581d4a8983d2c941").WaitForCompletion().GetComponent<SetStateOnHurt>().canBeFrozen = false; //VoidMegaCrab

        }

        private static void AffixEarthHealthInvulThing(On.EntityStates.AffixEarthHealer.Chargeup.orig_OnEnter orig, EntityStates.AffixEarthHealer.Chargeup self)
        {
            orig(self);
            if (NetworkServer.active)
            {
                if (self.characterBody && self.characterBody.inventory)
                {
                    //This way, we can have levelMaxHealth, without Desyncing the client.
                    self.characterBody.inventory.GiveItem(RoR2Content.Items.BoostHp, 3 * Run.instance.ambientLevelFloor);
                    self.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 0.5f);
                }
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
                            /*DeathRewards deathreward = tempbod.GetComponent<DeathRewards>();
                            if (deathreward)
                            {
                                deathreward.bossDropTable = DropTableForBossScav;
                            }*/
                            PickupDef pickupDef = PickupCatalog.GetPickupDef(DropTableForBossScav.GenerateDrop(ScavengerItemGranter.rng));
                            inventory.GiveItem(pickupDef.itemIndex, bossItemCount);
                        }
                    }
                }
            }
            orig(self);
            Inventory inv = self.GetComponent<Inventory>();
            EquipmentIndex e = inv.currentEquipmentIndex;
            if (e == RoR2Content.Equipment.Recycle.equipmentIndex||
                e == RoR2Content.Equipment.Gateway.equipmentIndex||
                e == DLC1Content.Equipment.MultiShopCard.equipmentIndex)
            {
                inv.GiveRandomEquipment(ScavengerItemGranter.rng);
            }

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


    }
}