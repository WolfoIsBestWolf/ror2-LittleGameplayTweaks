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
            //StupidPriceChanger();
            Faster();
 
            /*if (WConfig.cfgVoidTripleAllTier.Value == true)
            {
                Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidTriple/VoidTriple.prefab").WaitForCompletion().GetComponent<RoR2.OptionChestBehavior>().dropTable = WolfoLibrary.Shared.dtAllTier;
            }*/

     
      
            if (WConfig.VoidSeedsMore.Value)
            {
                GameObject VoidCamp = Addressables.LoadAssetAsync<GameObject>(key: "e515327d3d5e0144488357748ce1e899").WaitForCompletion();
                VoidCamp.transform.GetChild(0).GetComponent<CampDirector>().baseMonsterCredit = 75;
                VoidCamp.transform.GetChild(1).GetComponent<CampDirector>().baseMonsterCredit = 75;
            }
            On.RoR2.CampDirector.CalculateCredits += VoidSeedLoopCredits;
            if (WConfig.VoidCradlesMore.Value)
            {
                GameObject VoidChest = Addressables.LoadAssetAsync<GameObject>(key: "e82b1a3fea19dfd439109683ce4a14b7").WaitForCompletion();
                ScriptedCombatEncounter infestors = VoidChest.GetComponent<ScriptedCombatEncounter>();
                infestors.spawns[0].cullChance = 30;
                infestors.spawns[1].cullChance = 30;
                infestors.spawns[2].cullChance = 50;
                infestors.spawns[3].cullChance = 50;
                 
                GivePickupsOnStart voidInfestorMaster = Addressables.LoadAssetAsync<GameObject>(key: "741e2f9222e19bd4185f43aff65ea213").WaitForCompletion().GetComponent<GivePickupsOnStart>();
                if (voidInfestorMaster.itemInfos.Length > 0)
                {
                    voidInfestorMaster.itemInfos[0].count = 100;
                }
            }
           
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
  
            

            /*if (WConfig.InteractableNoLunarCost.Value == true)
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
 
            On.RoR2.ShopTerminalBehavior.DropPickup += RedToWhiteSoupMore;*/

        }

        /*public static void RedToWhiteSoupMore(On.RoR2.ShopTerminalBehavior.orig_DropPickup orig, ShopTerminalBehavior self)
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
        }*/
 

    }
}