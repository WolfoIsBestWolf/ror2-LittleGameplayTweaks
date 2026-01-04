using MonoMod.Cil;
using RoR2;
//using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class Looping
    {


        public static void CallLate()
        {
            TeleporterInteraction.onTeleporterBeginChargingGlobal += MoreDifficultLoopTeleporters;
            IL.RoR2.TeleporterInteraction.ChargingState.OnEnter += MoreCredits;

            WConfig.Tier2_SettingChanged(null, null);

            On.RoR2.TeleporterInteraction.Start += MorePortals;

        }

        private static void MoreCredits(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.Before,
                x => x.MatchStfld("RoR2.CombatDirector", "monsterCredit")))
            {
                c.EmitDelegate<System.Func<float, float>>((credits) =>
                {
                    Debug.Log("TP Credits: " + credits);
                    if (Run.instance.loopClearCount > 0)
                    {
                        if (WConfig.cfgLoopDifficultTeleporters.Value)
                        {
                            credits *= (1f + (float)Run.instance.loopClearCount);
                            Debug.Log("TP Credits New: " + credits);
                        }
                    }
                    return credits;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: PurchaseInteraction.ShrineBloodBehavior_GoldAmount");
            }
        }

        private static void MorePortals(On.RoR2.TeleporterInteraction.orig_Start orig, TeleporterInteraction self)
        {
            bool stage5 = SceneCatalog.mostRecentSceneDef.stageOrder == 4;
            if (Run.instance)
            {
                int stageClearCount = Run.instance.stageClearCount;
                if (WConfig.VoidPortalStage9.Value)
                {
                    PortalSpawner voidLocust = self.GetComponent<PortalSpawner>();
                    if (voidLocust)
                    {
                        voidLocust.minStagesCleared = 4;
                        if (stage5)
                        {
                            voidLocust.spawnChance = 1f;
                        }
                        else
                        {
                            voidLocust.spawnChance = 0.2f;
                        }
                    }
                }
            }
            orig(self);
            /*if (WConfig.CelestialStage10.Value)
            {
                if (NetworkServer.active)
                {
                    if (stage5 && Run.instance.stageClearCount > 7 && !Run.instance.GetEventFlag("NoMysterySpace"))
                    {
                        self.shouldAttemptToSpawnMSPortal = true;
                    }
                }
            }*/
            if (self.bossGroup)
            {
                self.bossGroup.bossDropChance = WConfig.cfgBossItemChance.Value / 100f;
            }
        }

        public static int FindTier2Elite()
        {
            for (int i = 0; i < CombatDirector.eliteTiers.Length; i++)
            {
                for (int E = 0; E < CombatDirector.eliteTiers[i].eliteTypes.Length; E++)
                {
                    if (CombatDirector.eliteTiers[i].eliteTypes[E] == RoR2Content.Elites.Poison)
                    {
                        //Debug.Log("Tier2 Elite Tier at " + i);
                        return i;
                    }
                }
            }
            Debug.LogWarning("Could not find Tier2 Elite Tier");
            return 111; //Force out of bounds
        }

        public static void MoreDifficultLoopTeleporters(TeleporterInteraction self)
        {
            if (WConfig.cfgLoopDifficultTeleporters.Value)
            {
                if (NetworkServer.active)
                {
                    if (self.bossDirector)
                    {
                        if (Run.instance.loopClearCount > 0)
                        {
                            //Setting Elite Bias <1 can ressult in the director choosing an expensive elite
                            //Paying for it entirely, but then just not giving the elite
                            //float loop = Run.instance.loopClearCount * 1f;
                            //self.bossDirector.eliteBias = 0.2f;               
                            //self.bossDirector.monsterCredit += (float)((int)(200f * loop * Mathf.Pow(Run.instance.compensatedDifficultyCoefficient, 0.5f) * (float)(1 + self.shrineBonusStacks)));
                            self.bossDirector.onSpawnedServer.AddListener(new UnityAction<GameObject>(MultiplyBossHP));
                        }
                    }
                }
            }

        }

        public static void MultiplyBossHP(GameObject masterObject)
        {
            if (masterObject == null)
            {
                return;
            }
            Inventory inv = masterObject.GetComponent<Inventory>();
            if (inv == null)
            {
                return;
            }
            if (Run.instance.loopClearCount > 0)
            {

                int loopStages = Run.instance.stageClearCount - 4;
                float mult = Mathf.Pow(1.2f, loopStages);// +0.5f*Run.instance.loopClearCount;
                int baseHP = 10 + inv.GetItemCount(RoR2Content.Items.BoostHp);
                int bonusHP = (int)(baseHP * mult) - baseHP;
                Debug.Log("Loop TP multiplying HP by " + mult);
                inv.GiveItem(RoR2Content.Items.BoostHp, baseHP);
            }


        }


    }



}