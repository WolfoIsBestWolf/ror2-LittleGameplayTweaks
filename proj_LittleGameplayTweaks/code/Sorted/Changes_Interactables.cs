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

        //
        //public static GameObject RedToWhiteSoup = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/LunarCauldron, RedToWhite Variant");
        //public static bool RedSoupBought = false;
        //
        public static void Start()
        {

            StupidPriceChanger();
            Faster();

            On.RoR2.TimedChestController.PreStartClient += TimedChestController_PreStartClient;

            BasicPickupDropTable dtLockbox = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/TreasureCache/dtLockbox.asset").WaitForCompletion();
            dtLockbox.canDropBeReplaced = false;
            BasicPickupDropTable dtChanceDoll = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC2/Items/ExtraShrineItem/dtChanceDoll.asset").WaitForCompletion();
            dtChanceDoll.canDropBeReplaced = false;

            Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/TreasureCacheVoid/dtVoidLockbox.asset").WaitForCompletion().canDropBeReplaced = false;
            Addressables.LoadAssetAsync<FreeChestDropTable>(key: "RoR2/DLC1/FreeChest/dtFreeChest.asset").WaitForCompletion().canDropBeReplaced = false;


            if (WConfig.cfgVoidStagePillar.Value)
            {
                GameObject DeepVoidPortalBattery = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/DeepVoidPortalBattery/DeepVoidPortalBattery.prefab").WaitForCompletion();
                DeepVoidPortalBattery.GetComponent<HoldoutZoneController>().baseChargeDuration = 45;
                DeepVoidPortalBattery.GetComponent<HoldoutZoneController>().baseRadius = 26;

            }
            BasicPickupDropTable dtStealthedChest = ScriptableObject.CreateInstance<BasicPickupDropTable>();
            dtStealthedChest.tier1Weight = 0.5f;
            dtStealthedChest.tier2Weight = 0.5f;
            dtStealthedChest.tier3Weight = 0.2f;
            dtStealthedChest.bossWeight = 0.02f;
            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Chest1StealthedVariant/Chest1StealthedVariant.prefab").WaitForCompletion().GetComponent<ChestBehavior>().dropTable = dtStealthedChest;
 
            if (WConfig.InteractablesCombatShrineHP.Value == false)
            {
                GameObject ShrineCombat = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShrineCombat/ShrineCombat.prefab").WaitForCompletion();
                ShrineCombat.GetComponent<CombatSquad>().grantBonusHealthInMultiplayer = false;
                ShrineCombat = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShrineCombat/ShrineCombatSandy Variant.prefab").WaitForCompletion();
                ShrineCombat.GetComponent<CombatSquad>().grantBonusHealthInMultiplayer = false;
                ShrineCombat = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShrineCombat/ShrineCombatSnowy Variant.prefab").WaitForCompletion();
                ShrineCombat.GetComponent<CombatSquad>().grantBonusHealthInMultiplayer = false;

            }


            On.RoR2.HalcyoniteShrineInteractable.Awake += HalcyoniteShrine_ApplyNumbers;
            IL.RoR2.HalcyoniteShrineInteractable.DrainConditionMet += HalcyoniteShrine_NerfStats;

            IL.RoR2.ShrineBloodBehavior.AddShrineStack += ShrineBloodBehavior_GoldAmount;
 
        }

        private static void ShrineBloodBehavior_GoldAmount(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.Before,
                x => x.MatchStloc(1)))
            {
                c.Emit(OpCodes.Ldloc_0);
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<System.Func<uint, CharacterBody, ShrineBloodBehavior, uint>>((money, body, self) =>
                {
                    if (WConfig.cfgShrineBloodGold.Value)
                    {
                        float moneyMult = body.healthComponent.fullCombinedHealth / 100f / (0.7f + body.level * 0.3f);

                        //25, 40, 60;
                        int baseMoney = 25;
                        if (self.purchaseCount == 1)
                        {
                            baseMoney = 40;
                        }
                        else if (self.purchaseCount == 2)
                        {
                            baseMoney = 60;
                        }
                        baseMoney = Run.instance.GetDifficultyScaledCost(baseMoney, Stage.instance.entryDifficultyCoefficient);
                        return (uint)(baseMoney * moneyMult);
                    }
                    return money;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: PurchaseInteraction.ShrineBloodBehavior_GoldAmount");
            }
        }

        private static void HalcyoniteShrine_NerfStats(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
            x => x.MatchLdcR4(100f)))
            {
                c.EmitDelegate<System.Func<float, float>>((damageCoeff) =>
                {
                    if (WConfig.cfgHalcyon_NerfStats.Value)
                    {
                        return 120f;
                    }
                    return damageCoeff;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: Buff Married Lemurians");
            }
        }

        private static void HalcyoniteShrine_ApplyNumbers(On.RoR2.HalcyoniteShrineInteractable.orig_Awake orig, HalcyoniteShrineInteractable self)
        {
            if (NetworkServer.active)
            {
                if (WConfig.cfgHalcyon_FastDrain.Value)
                {
                    self.goldDrainValue = 2;
                }
                if (WConfig.cfgHalcyon_LessMonsterCredits.Value)
                {
                    self.monsterCredit = 80;
                }
                if (WConfig.cfgHalcyon_ForcedSots.Value)
                {
                    self.halcyoniteDropTableTier2 = self.halcyoniteDropTableTier3;
                }
                self.purchaseInteraction.Networkcost = self.goldDrainValue;
            }
            orig(self);

            self.combatDirector.combatSquad.onMemberAddedServer += MakeHalcyoniteBoss;
            self.activationDirector.combatSquad.grantBonusHealthInMultiplayer = WConfig.cfgHalcyon_ScaleHPMult.Value;
        }

        private static void MakeHalcyoniteBoss(CharacterMaster obj)
        {
            obj.isBoss = true;
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
            if (WConfig.FasterPrinter.Value > WConfig.Client.Off)
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
                if (WConfig.FasterPrinter.Value > WConfig.Client.Match)
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
                else
                {
                    /* EntityStateConfiguration Duplicating = LegacyResourcesAPI.Load<EntityStateConfiguration>("EntityStateConfigurations/EntityStates.Duplicator.Duplicating");
                     Duplicating.serializedFieldsCollection.serializedFields[0].fieldValue.stringValue = "1.4";
                     Duplicating.serializedFieldsCollection.serializedFields[1].fieldValue.stringValue = "1.23";*/
                }

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
            if (WConfig.InteractableHealingShrine.Value)
            {
                LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineHealing").GetComponent<RoR2.PurchaseInteraction>().cost = 10;
                LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineHealing").GetComponent<RoR2.ShrineHealingBehavior>().costMultiplierPerPurchase = 1.4f;
                LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineHealing").GetComponent<RoR2.ShrineHealingBehavior>().maxPurchaseCount += 1;
            }
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


            On.RoR2.ShrineBloodBehavior.AddShrineStack += ShrineBloodChanges;
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

        public static void ShrineBloodChanges(On.RoR2.ShrineBloodBehavior.orig_AddShrineStack orig, ShrineBloodBehavior self, Interactor interactor)
        {
            /*if (WConfig.cfgShrineBloodGold.Value == true)
            {
                CharacterBody component = interactor.GetComponent<CharacterBody>();
                if (Stage.instance && component)
                {
                    //int newGold = 50 + self.purchaseCount * 25;
                    int newGold = 35 + self.purchaseCount * 35;
                    float difficultyScaledCost = Run.instance.GetDifficultyScaledCost(newGold, Stage.instance.entryDifficultyCoefficient);
                    //_entryDifficultyCoefficient

                    float HealthForDiv = component.baseMaxHealth + component.levelMaxHealth * (component.level - 1);

                    self.goldToPaidHpRatio = difficultyScaledCost / HealthForDiv / self.purchaseInteraction.cost * 100;
                }
            }*/
            orig(self, interactor);
            if (WConfig.InteractableBloodShrineLessCost.Value == true)
            {
                if (self.purchaseCount == 1)
                {
                    //self.purchaseInteraction.cost = 70;
                    self.costMultiplierPerPurchase = 1.75f;
                }
                else if (self.purchaseCount == 2)
                {
                    //self.purchaseInteraction.cost = 90;
                    self.costMultiplierPerPurchase = 1.93f;
                }
            }
        }


    }
}