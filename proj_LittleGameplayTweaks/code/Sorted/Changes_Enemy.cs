using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.CharacterAI;
using RoR2.Projectile;
using System;
using System.ComponentModel;
using System.Reflection;
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
            GivePickupsOnStart.ItemDefInfo[] teleportWhenOob = new GivePickupsOnStart.ItemDefInfo[]
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
            ElectricWormMaster.AddComponent<GivePickupsOnStart>().itemDefInfos = teleportWhenOob;
 
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
                //ItemTag.EquipmentRelated,
            };
 
            GameObject BeadProjectileTrackingBomb = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Elites/EliteBead/BeadProjectileTrackingBomb.prefab").WaitForCompletion();
            if (WConfig.cfgTwistedBuff.Value)
            {
                On.RoR2.AffixBeadAttachment.Initialize += AffixBeadAttachment_Initialize;
 
                GameObject.Destroy(BeadProjectileTrackingBomb.transform.GetChild(0).GetComponent<HurtBoxGroup>());
                GameObject.Destroy(BeadProjectileTrackingBomb.transform.GetChild(0).GetChild(0).GetComponent<HurtBox>());
            }
  
 
 
            On.RoR2.ScriptedCombatEncounter.BeginEncounter += ScalingChanges;
 
            KillableProjectileScaling();


            /*IL.RoR2.ChildMonsterController.RegisterTeleport += ChildAndScorchling_ArmorNotInvul;
            IL.ScorchlingController.Breach += ChildAndScorchling_ArmorNotInvul;
            IL.ScorchlingController.Burrow += ChildAndScorchling_ArmorNotInvul;*/
        }

        private static void ChildAndScorchling_ArmorNotInvul(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
            x => x.MatchLdsfld("RoR2.RoR2Content/Buffs", "HiddenInvincibility")))
            {
                //Wish I knew how to do this proper but whatevs
                c.EmitDelegate<Func<BuffDef, BuffDef>>((_) =>
                {
                    return RoR2Content.Buffs.ArmorBoost;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: Scorchling_ArmorNotInvul");
            }
            if (c.TryGotoNext(MoveType.After,
          x => x.MatchLdsfld("RoR2.RoR2Content/Buffs", "HiddenInvincibility")))
            {
                //Wish I knew how to do this proper but whatevs
                c.EmitDelegate<Func<BuffDef, BuffDef>>((_) =>
                {
                    return RoR2Content.Buffs.ArmorBoost;
                });
            }
        }

 

        private static void AffixBeadAttachment_Initialize(On.RoR2.AffixBeadAttachment.orig_Initialize orig, AffixBeadAttachment self)
        {
            orig(self);
            self.trackedBodies.Add(self.networkedBodyAttachment.attachedBody.gameObject);
            //self.damageCooldown = 0.1f; //Due to the more rapid fire nature
            self.cooldownAfterFiring = 1f; //0 cooldown
            //self.fireDelayTimer = 2f; //But longer windup
            //self.damageHitCountTotal = 10; //Just shoot them out a lot but easy to dodge?
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

    

       

        private static void VoidRaidGauntletController_Start(On.RoR2.VoidRaidGauntletController.orig_Start orig, VoidRaidGauntletController self)
        {
            orig(self);
            if (!NetworkServer.active)
            {
                return;
            }
            if (self.phaseEncounters.Length == 3)
            {
                /*if (WConfig.cfgVoidlingNerf.Value)
                {
                    self.phaseEncounters[0].onBeginEncounter += VoidlingPhase1Nerf;
                    self.phaseEncounters[1].onBeginEncounter += VoidlingPhase2Nerf;
                    self.phaseEncounters[2].onBeginEncounter += VoidlingPhase3Nerf;
                }*/
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
                                inv.RemoveItemPermanent(RoR2Content.Items.BoostHp, inv.GetItemCountPermanent(RoR2Content.Items.BoostHp));
                                inv.GiveItemPermanent(RoR2Content.Items.BoostHp, Mathf.RoundToInt((extraHealth - 1f) * 10f));
                            }
                        }
                    }
                }
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
                AffixEarthHealerBody.baseArmor = 100;
                AffixEarthHealerBody.levelArmor = 60; //Imitates Health Per Level well enough
                //But Health Per Level wouldnt be client side.
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



 
            //Scav search item more reliably, stop searching when about to die
            var searchItem = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavMaster").GetComponents<RoR2.CharacterAI.AISkillDriver>()[1];
            searchItem.maxUserHealthFraction = 0.999f;
            searchItem.minUserHealthFraction = 0.4f;
            searchItem.minTargetHealthFraction = 0.4f;
            searchItem.selectionRequiresOnGround = false;
            /*searchItem.maxDistance = 300;
            searchItem.minDistance = 60;*/

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
 
            //Devestator immune to freeze consistency
            Addressables.LoadAssetAsync<GameObject>(key: "097b0e271757ce24581d4a8983d2c941").WaitForCompletion().GetComponent<SetStateOnHurt>().canBeFrozen = false; //VoidMegaCrab


            //ExtractorStuff
            //On.RoR2.ItemStealController.ExtractorUnitItemStealFilter += ItemStealController_ExtractorUnitItemStealFilter;
            Addressables.LoadAssetAsync<GameObject>(key: "645c6efa053511c488c3993881e2884a").WaitForCompletion().GetComponent<BaseAI>().ignoreFliers = true;


        }

        private static bool ItemStealController_ExtractorUnitItemStealFilter(On.RoR2.ItemStealController.orig_ExtractorUnitItemStealFilter orig, ItemIndex itemIndex)
        {
            return orig(itemIndex) && !ItemStealController.ExtractorUnitItemShareFilter(itemIndex);
        }

        private static void AffixEarthHealthInvulThing(On.EntityStates.AffixEarthHealer.Chargeup.orig_OnEnter orig, EntityStates.AffixEarthHealer.Chargeup self)
        {
            orig(self);
            if (NetworkServer.active)
            {
                //Brief Invul to not just get fucked by Willo Wisp
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
                                //bossItemCount = 2;
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
                                //bossItemCount = 2;
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
                            inventory.GiveItemPermanent(pickupDef.itemIndex, bossItemCount);
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
                        return 4f;
                    }
                    return damageCoeff;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: Buff Married Lemurians");
            }
        }


    }
}