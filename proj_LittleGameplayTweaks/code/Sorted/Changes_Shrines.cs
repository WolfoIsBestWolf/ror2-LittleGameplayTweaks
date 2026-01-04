using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class Changes_Shrines
    {

        public static void Start()
        {
            //On.RoR2.ShrineBloodBehavior.AddShrineStack += ShrineBloodChanges;
            if (WConfig.Shrine_Healing.Value)
            {
                LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineHealing").GetComponent<RoR2.PurchaseInteraction>().cost = 20;
                LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineHealing").GetComponent<RoR2.ShrineHealingBehavior>().costMultiplierPerPurchase = 1f;
                //LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineHealing").GetComponent<RoR2.ShrineHealingBehavior>().maxPurchaseCount += 1;
            }
            /* if (WConfig.Shrine_Combat.Value == false)
             {
                 GameObject ShrineCombat = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShrineCombat/ShrineCombat.prefab").WaitForCompletion();
                 ShrineCombat.GetComponent<CombatSquad>().grantBonusHealthInMultiplayer = false;
                 ShrineCombat = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShrineCombat/ShrineCombatSandy Variant.prefab").WaitForCompletion();
                 ShrineCombat.GetComponent<CombatSquad>().grantBonusHealthInMultiplayer = false;
                 ShrineCombat = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShrineCombat/ShrineCombatSnowy Variant.prefab").WaitForCompletion();
                 ShrineCombat.GetComponent<CombatSquad>().grantBonusHealthInMultiplayer = false;

             }*/

            //Fix this being gone? //Is this a fix??
            var dtShrineHalcyon1 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "bb3b51f04206e3242af6981db3c402a7").WaitForCompletion();
            dtShrineHalcyon1.requiredItemTags = System.Array.Empty<ItemTag>();
            Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "e291748f54c927a47ad44789d295c39f").WaitForCompletion().bannedItemTags = new ItemTag[] { ItemTag.HalcyoniteShrine };

            On.RoR2.HalcyoniteShrineInteractable.Awake += HalcyoniteShrine_ApplyNumbers;
            //IL.RoR2.HalcyoniteShrineInteractable.DrainConditionMet += Halcyon_OptionalNerfStats;
            //IL.RoR2.HalcyoniteShrineInteractable.DropRewards += Halcyon_RemoveForcedSots;

            IL.RoR2.ShrineBloodBehavior.AddShrineStack += BloodBehavior_GoldAmount;
            /*if (WConfig.Shrine_Blood_NoBreak.Value)
            {
                IL.RoR2.HealthComponent.UpdateLastHitTime += BloodShrine_DontBreakElixir;
                IL.RoR2.HealthComponent.TakeDamageProcess += BloodShrine_DontBreakTransmitter;
            }*/


        }

        private static void BloodShrine_DontBreakTransmitter(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
             x => x.MatchLdfld("RoR2.HealthComponent/ItemCounts", "unstableTransmitter")))
            {
                c.Emit(OpCodes.Ldarg_1);
                c.EmitDelegate<System.Func<int, DamageInfo, int>>((item, info) =>
                {
                    if (info.attacker && info.attacker.GetComponent<PurchaseInteraction>())
                    {
                        return 0;
                    }
                    return item;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: BloodShrineDontBreakElixir");
            }
        }

        /*private static void Halcyon_RemoveForcedSots(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            bool a = c.TryGotoNext(MoveType.Before,
            x => x.MatchLdfld("RoR2.HalcyoniteShrineInteractable", "rng"),
            x => x.MatchCall("RoR2.PickupPickerController", "GenerateOptionsFromDropTablePlusForcedStorm"),
            x => x.MatchStfld("RoR2.GenericPickupController/CreatePickupInfo", "pickerOptions"));
            if (a && c.TryGotoNext(MoveType.Before,
            x => x.MatchStfld("RoR2.GenericPickupController/CreatePickupInfo", "pickerOptions")))
            {

                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<System.Func<PickupPickerController.Option[], HalcyoniteShrineInteractable, PickupPickerController.Option[]>>((options, self) =>
                {
                    if (WConfig.cfgHalcyon_NoForcedSots.Value)
                    {
                        return PickupPickerController.GenerateOptionsFromDropTable(self.rewardOptionCount, self.halcyoniteDropTableTier1, self.rng); ;
                    }
                    return options;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: Halcyon_RemoveForcedSots");
            }
        }*/

        private static void BloodShrine_DontBreakElixir(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
            x => x.MatchLdfld("RoR2.HealthComponent/ItemCounts", "healingPotion")))
            {
                c.Emit(OpCodes.Ldarg_S, (byte)4);
                c.EmitDelegate<System.Func<int, GameObject, int>>((item, attacker) =>
                {
                    if (item != 0 && attacker && attacker.GetComponent<PurchaseInteraction>())
                    {
                        return 0;
                    }
                    return item;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: BloodShrineDontBreakElixir");
            }
            if (c.TryGotoNext(MoveType.After,
                x => x.MatchLdfld("RoR2.HealthComponent/ItemCounts", "fragileDamageBonus")))
            {
                c.Emit(OpCodes.Ldarg_S, (byte)4);
                c.EmitDelegate<System.Func<int, GameObject, int>>((item, attacker) =>
                {
                    if (item != 0 && attacker && attacker.GetComponent<PurchaseInteraction>())
                    {
                        return 0;
                    }
                    return item;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: BloodShrineDontBreakWatch");
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

        private static void BloodBehavior_GoldAmount(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.Before,
                x => x.MatchStloc(1)))
            {
                c.EmitDelegate<System.Func<uint, uint>>((money) =>
                {
                    if (WConfig.Shrine_Blood_Gold.Value)
                    {
                        return (uint)(money * Mathf.Pow(Run.instance.difficultyCoefficient, 1.05f));
                        /*float moneyMult = body.healthComponent.fullCombinedHealth / 100f / (0.7f + body.level * 0.3f);
                        if (moneyMult < 1)
                        {
                            moneyMult = 1;
                        }
                        //25, 40, 60;
                        int baseMoney = 25;
                        if (self.purchaseCount == 1)
                        {
                            baseMoney = 42;
                        }
                        else if (self.purchaseCount == 2)
                        {
                            baseMoney = 60;
                        }
                        baseMoney = Run.instance.GetDifficultyScaledCost(baseMoney, Stage.instance.entryDifficultyCoefficient);
                        return (uint)(baseMoney * moneyMult);*/
                    }
                    return money;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: PurchaseInteraction.ShrineBloodBehavior_GoldAmount");
            }
        }

        /*private static void Halcyon_OptionalNerfStats(ILContext il)
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
        }*/

        private static void HalcyoniteShrine_ApplyNumbers(On.RoR2.HalcyoniteShrineInteractable.orig_Awake orig, HalcyoniteShrineInteractable self)
        {
            if (NetworkServer.active)
            {
                if (WConfig.cfgHalcyon_FastDrain.Value)
                {
                    self.goldDrainValue = 2;
                }
                self.purchaseInteraction.Networkcost = self.goldDrainValue;
            }
            orig(self);

            //self.combatDirector.combatSquad.onMemberAddedServer += MakeHalcyoniteBoss;
            self.activationDirector.combatSquad.grantBonusHealthInMultiplayer = WConfig.cfgHalcyon_ScaleHPMult.Value;
        }

        private static void MakeHalcyoniteBoss(CharacterMaster obj)
        {
            obj.isBoss = true;
        }


        /*public static void ShrineBloodChanges(On.RoR2.ShrineBloodBehavior.orig_AddShrineStack orig, ShrineBloodBehavior self, Interactor interactor)
        {
            orig(self, interactor);
            if (WConfig.Shrine_Blood_NoBreak.Value == true)
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
        }*/


    }
}