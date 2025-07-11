using EntityStates.Scrapper;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class Changes_Interactables
    {
 
        public static void Start()
        {
            Changes_Shrines.Start();
            StupidPriceChanger();
            Faster();

            On.RoR2.TimedChestController.PreStartClient += TimedChestController_PreStartClient;

            Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/TreasureCache/dtLockbox.asset").WaitForCompletion().canDropBeReplaced = false;
            Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC2/Items/ExtraShrineItem/dtChanceDoll.asset").WaitForCompletion().canDropBeReplaced = false;
            Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/TreasureCacheVoid/dtVoidLockbox.asset").WaitForCompletion().canDropBeReplaced = false;
            Addressables.LoadAssetAsync<FreeChestDropTable>(key: "RoR2/DLC1/FreeChest/dtFreeChest.asset").WaitForCompletion().canDropBeReplaced = false;

            if (WConfig.cfgVoidTripleAllTier.Value == true)
            {
                Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidTriple/VoidTriple.prefab").WaitForCompletion().GetComponent<RoR2.OptionChestBehavior>().dropTable = WolfoFixes.Shared.dtAllTier;
            }

            if (WConfig.cfgVoidStagePillar.Value)
            {
                GameObject DeepVoidPortalBattery = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/DeepVoidPortalBattery/DeepVoidPortalBattery.prefab").WaitForCompletion();
                DeepVoidPortalBattery.GetComponent<HoldoutZoneController>().baseChargeDuration = 48;
                DeepVoidPortalBattery.GetComponent<HoldoutZoneController>().baseRadius = 26;

            }
            BasicPickupDropTable dtStealthedChest = ScriptableObject.CreateInstance<BasicPickupDropTable>();
            dtStealthedChest.tier1Weight = 0.8f;
            dtStealthedChest.tier2Weight = 0.3f;
            dtStealthedChest.tier3Weight = 0.1f;
            dtStealthedChest.bossWeight = 0.02f;
            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Chest1StealthedVariant/Chest1StealthedVariant.prefab").WaitForCompletion().GetComponent<ChestBehavior>().dropTable = dtStealthedChest;
  
            if (WConfig.VoidSeedsMore.Value)
            {
                GameObject VoidCamp = Addressables.LoadAssetAsync<GameObject>(key: "e515327d3d5e0144488357748ce1e899").WaitForCompletion();
                VoidCamp.transform.GetChild(0).GetComponent<CampDirector>().baseMonsterCredit = 75;
                //VoidCamp.transform.GetChild(0).GetComponent<CombatDirector>().eliteBias = 2;
                VoidCamp.transform.GetChild(1).GetComponent<CampDirector>().baseMonsterCredit = 75;
                //VoidCamp.transform.GetChild(1).GetComponent<CombatDirector>().eliteBias = 2;
            }
            On.RoR2.CampDirector.CalculateCredits += VoidSeedLoopCredits;
            if (WConfig.VoidCradlesMore.Value)
            {
                GameObject VoidChest = Addressables.LoadAssetAsync<GameObject>(key: "e82b1a3fea19dfd439109683ce4a14b7").WaitForCompletion();
                ScriptedCombatEncounter infestors = VoidChest.GetComponent<ScriptedCombatEncounter>();
                infestors.spawns[0].cullChance = 30;
                infestors.spawns[1].cullChance = 30;
                infestors.spawns[2].cullChance = 30;
                infestors.spawns[3].cullChance = 30;
                 
                GivePickupsOnStart voidInfestorMaster = Addressables.LoadAssetAsync<GameObject>(key: "741e2f9222e19bd4185f43aff65ea213").WaitForCompletion().GetComponent<GivePickupsOnStart>();
                if (voidInfestorMaster.itemInfos.Length > 0)
                {
                    voidInfestorMaster.itemInfos[0].count = 100;
                }
            }
            Addressables.LoadAssetAsync<GameObject>(key: "34d770816ffbf0d468728c48853fd5f6").WaitForCompletion().GetComponent<ConvertPlayerMoneyToExperience>().enabled = false;
        }
 
        private static void VoidSeedLoopCredits(On.RoR2.CampDirector.orig_CalculateCredits orig, CampDirector self)
        {
            //1,1,1.1,1.2,1.7
            if (WConfig.VoidSeedsScale.Value && self.combatDirector && self.combatDirector.teamIndex == TeamIndex.Void)
            {
                //0
                //1
                //2 0.9
                //3 1
                //4 1.1 
                //5 1.5
                float mult = 0.7f + Run.instance.stageClearCount * 0.1f + Run.instance.loopClearCount * 0.3f;
                if (mult > 1)
                {
                    self.baseMonsterCredit = (int)((float)self.baseMonsterCredit * mult);
                    self.monsterCreditPenaltyCoefficient /= mult;
                }
            }
 
            orig(self);
        }

      
        public static void TimedChestController_PreStartClient(On.RoR2.TimedChestController.orig_PreStartClient orig, TimedChestController self)
        {
            orig(self);
            if (Run.instance.stageClearCount > 3)
            {
                float newTime = self.lockTime / 2 * Run.instance.stageClearCount;
                self.lockTime = newTime;
            }
        }



        public static void Faster()
        {
            if (WConfig.FasterPrinter.Value == true)
            {
                LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/chest/Duplicator").GetComponent<RoR2.EntityLogic.DelayedEvent>().enabled = false;
                LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/chest/DuplicatorLarge").GetComponent<RoR2.EntityLogic.DelayedEvent>().enabled = false;
                LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/chest/DuplicatorMilitary").GetComponent<RoR2.EntityLogic.DelayedEvent>().enabled = false;
                LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/chest/DuplicatorWild").GetComponent<RoR2.EntityLogic.DelayedEvent>().enabled = false;

                On.EntityStates.Duplicator.Duplicating.DropDroplet += (orig, self) =>
                {
                    orig(self);
                    if (NetworkServer.active)
                    {
                        self.outer.GetComponent<PurchaseInteraction>().Networkavailable = true;
                    }
                };

                //Just 1 entity state so probably can't really work with it
                /*if (WConfig.FasterPrinter.Value > WConfig.Client.Match)
                {
                    IL.RoR2.PurchaseInteraction.CreateItemTakenOrb += (ILContext il) =>
                    {
                        ILCursor c = new ILCursor(il);
                        if (c.TryGotoNext(MoveType.Before,
                            x => x.MatchLdcR4(1.5f)))
                        {
                            c.Next.Operand = 0.8f;
                        }
                        else
                        {
                            Debug.LogWarning("IL Failed: PurchaseInteraction.CreateItemTakenOrb");
                        }
                    };

                    EntityStateConfiguration Duplicating = LegacyResourcesAPI.Load<EntityStateConfiguration>("EntityStateConfigurations/EntityStates.Duplicator.Duplicating");
                    Duplicating.serializedFieldsCollection.serializedFields[0].fieldValue.stringValue = "0.6"; //1.5
                    Duplicating.serializedFieldsCollection.serializedFields[1].fieldValue.stringValue = "1.25"; //1.33
                }
                */

            }

            if (WConfig.FasterScrapper.Value == true)
            {
                IL.RoR2.ScrapperController.CreateItemTakenOrb += (ILContext il) =>
                {
                    ILCursor c = new ILCursor(il);
                    if (c.TryGotoNext(MoveType.Before,
                    x => x.MatchLdcR4(1.5f)))
                    {
                        c.Next.Operand = 1f;
                    }
                    else
                    {
                        Debug.LogWarning("IL Failed: ScrapperController.CreateItemTakenOrb");
                    }
                };

                //Because this is multiple EntityStates leading into each other
                //Works fine Host -> xClient, looks weird xHost -> Client
                /*EntityStateConfiguration Scrapping = LegacyResourcesAPI.Load<EntityStateConfiguration>("EntityStateConfigurations/EntityStates.Scrapper.Scrapping");
                EntityStateConfiguration ScrappingToIdle = LegacyResourcesAPI.Load<EntityStateConfiguration>("EntityStateConfigurations/EntityStates.Scrapper.ScrappingToIdle");
                EntityStateConfiguration WaitToBeginScrapping = LegacyResourcesAPI.Load<EntityStateConfiguration>("EntityStateConfigurations/EntityStates.Scrapper.WaitToBeginScrapping");

                Scrapping.serializedFieldsCollection.serializedFields[2].fieldValue.stringValue = "1";
                ScrappingToIdle.serializedFieldsCollection.serializedFields[2].fieldValue.stringValue = "0.4";
                WaitToBeginScrapping.serializedFieldsCollection.serializedFields[0].fieldValue.stringValue = "0.9";
                */
                On.EntityStates.Scrapper.ScrapperBaseState.OnEnter += FasterScrapper_ClientSync;
            }

            if (WConfig.FasterShrines.Value == true)
            {
                //These all work fine on Host
                On.RoR2.ShrineBloodBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    self.refreshTimer = 1;
                };
                On.RoR2.ShrineBossBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    self.refreshTimer = 1;
                };
                On.RoR2.ShrineChanceBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    self.refreshTimer = 1;
                };
                On.RoR2.ShrineCombatBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    self.refreshTimer = 1;
                };
                On.RoR2.ShrineHealingBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    self.refreshTimer = 1;
                };
                On.RoR2.ShrineRestackBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    self.refreshTimer = 1;
                };
            }

        }

        private static void FasterScrapper_ClientSync(On.EntityStates.Scrapper.ScrapperBaseState.orig_OnEnter orig, EntityStates.Scrapper.ScrapperBaseState self)
        {
            orig(self);
            if (NetworkServer.active)
            {
                WaitToBeginScrapping.duration = 0.9f;
                ScrappingToIdle.duration = 0.4f;
                Scrapping.duration = 1f;
            }
        }

        public static void StupidPriceChanger()
        {
 
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BrokenDrones/MegaDroneBroken").GetComponent<RoR2.PurchaseInteraction>().cost = WConfig.MegaDroneCost.Value;
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BrokenDrones/Turret1Broken").GetComponent<RoR2.PurchaseInteraction>().cost = WConfig.TurretDroneCost.Value;

            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/LunarCauldron, RedToWhite Variant").GetComponent<ShopTerminalBehavior>().dropVelocity = new Vector3(5, 10, 5);


            if (WConfig.InteractableNoLunarCost.Value == true)
            {
                On.RoR2.PurchaseInteraction.Awake += (orig, self) =>
                {
                    orig(self);
                    if (self.costType == CostTypeIndex.LunarCoin)
                    {
                        self.cost = 0;
                    }
                };
            }
 
            On.RoR2.ShopTerminalBehavior.DropPickup += RedToWhiteSoupMore;

        }

        public static void RedToWhiteSoupMore(On.RoR2.ShopTerminalBehavior.orig_DropPickup orig, ShopTerminalBehavior self)
        {
            if (WConfig.InteractableRedSoupAmount.Value > 0)
            {
                if (self.name.StartsWith("LunarCauldron, RedToWhite"))
                {
                    if (!self.GetComponent<PurchaseInteraction>().available)
                    {
                        for (int i = 0; i < WConfig.InteractableRedSoupAmount.Value; i++)
                        {
                            PickupDropletController.CreatePickupDroplet(self.pickupIndex, (self.dropTransform ? self.dropTransform : self.transform).position, self.transform.TransformVector(self.dropVelocity));
                        };
                    }
                }
            }
            orig(self);
        }
 

    }
}