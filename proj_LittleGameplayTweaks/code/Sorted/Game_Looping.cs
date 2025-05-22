using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System.Collections.ObjectModel;
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

            WConfig.Tier2_SettingChanged(null, null);
            WConfig.LevelMaximum_SettingChanged(null, null);



            On.RoR2.TeleporterInteraction.Start += MorePortals;

            On.RoR2.ScriptedCombatEncounter.BeginEncounter += FinalBossLevelLimit;

            IL.RoR2.CharacterBody.RecalculateStats += ReplaceUseAmbienWithLevelBonus;

            Run.onRunStartGlobal += Add;
            Run.onRunDestroyGlobal += Remove;

            On.RoR2.LevelUpEffectManager.OnCharacterLevelUp += Looping.DisableLevelSound_P;
            On.RoR2.LevelUpEffectManager.OnRunAmbientLevelUp += Looping.DisableLevelSound_E;
        }

        private static void MorePortals(On.RoR2.TeleporterInteraction.orig_Start orig, TeleporterInteraction self)
        {
            if (!Run.instance)
            {
                return;
            }
            int stageClearCount = Run.instance.stageClearCount;
            if (WConfig.VoidPortalStage5.Value)
            {
                PortalSpawner voidLocust = self.GetComponent<PortalSpawner>();
                if (voidLocust)
                {
                    voidLocust.minStagesCleared = 4;
                    if (stageClearCount % Run.stagesPerLoop == 4)
                    {
                        voidLocust.spawnChance = 1f;
                    }
                    else
                    {
                        voidLocust.spawnChance = 0.2f;
                    }
                }
            }

            orig(self);
            //Should run before anything else because Awake -> OnEnable -> Start?
            if (WConfig.CelestialStage10.Value)
            {
                if (NetworkServer.active)
                {
                    if (stageClearCount % Run.stagesPerLoop == 4 && stageClearCount > Run.stagesPerLoop && !Run.instance.GetEventFlag("NoMysterySpace"))
                    {
                        self.shouldAttemptToSpawnMSPortal = true;
                    }
                }
            }

            if (self.bossGroup)
            {
                self.bossGroup.bossDropChance = WConfig.cfgBossItemChance.Value / 100f;
            }
        }

        public static void ChangeMidRun()
        {
            if (!Run.instance)
            {
                return;
            }
            ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
            int i = 0;
            int count = readOnlyInstancesList.Count;
            while (i < count)
            {
                CharacterBody characterBody = readOnlyInstancesList[i];
                Inventory inventory = characterBody.inventory;
                if (inventory && inventory.GetItemCount(RoR2Content.Items.UseAmbientLevel) > 0)
                {
                    SetLevelBonus(inventory, 0);
                    characterBody.MarkAllStatsDirty();
                }
                i++;
            }
        }

        private static void Remove(Run run)
        {
            if (!(run is InfiniteTowerRun))
            {
                IL.RoR2.CharacterBody.RecalculateStats -= ReplaceUseAmbienWithLevelBonus;
            }
        }

        private static void Add(Run run)
        {
            if (!(run is InfiniteTowerRun))
            {
                IL.RoR2.CharacterBody.RecalculateStats += ReplaceUseAmbienWithLevelBonus;
            }
        }

        public static void ReplaceUseAmbienWithLevelBonus(ILContext il)
        {
            //We need to remove the useambientlevel itself;
            //Because else it's still not synced;
            //Because they'd have both useambient and the + levels
            //Unless we only do it for levels above 99
            //Which I guess would work?

            //99 + LevelBonus would work for NonModOwners (They'd question why enemies get tankier probably but stats would not desync)
            //99 + LevelBonus would work for HostHas 999 and Client has 99
            //99 + LevelBonus would work if HostHas 99 and Client has 999 because only Host decides, they'd have wrong Hud but can't fix that


            //Host could probably just send out message of run ambient cap
            ILCursor c = new ILCursor(il);
            c.TryGotoNext(MoveType.Before,
            x => x.MatchCall("RoR2.CharacterBody", "set_level"));
            if (c.TryGotoNext(MoveType.Before,
            x => x.MatchCall("RoR2.CharacterBody", "set_level")
            ))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<System.Func<float, CharacterBody, float>>((levels, body) =>
                {
                    //If level above 99,
                    //Say 99, give rest as level bonus?
                    if (levels > 99)
                    {
                        //Levels would only be above 99 if config is enabled so we dont need to check for it
                        if (NetworkServer.active)
                        {
                            SetLevelBonus(body.inventory, (int)levels - 99);
                        }
                        return 99;
                    }
                    return levels;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed : ReplaceUseAmbienWithLevelBonus");
            }
        }

        public static int levelBonus = -1;
        public static void SetLevelBonus(Inventory inv, int count)
        {
            //Setting Run ambient level to 999 desyncs, whether it's host to client or client to host or people with mod or not
            //But inventory is networked, and LevelBonus is networked, so, I guess
            if (inv.itemStacks[levelBonus] != count)
            {
                inv.itemStacks[levelBonus] = count;
                inv.SetDirtyBit(1U);
            }
        }

        public static void FinalBossLevelLimit(On.RoR2.ScriptedCombatEncounter.orig_BeginEncounter orig, ScriptedCombatEncounter self)
        {
            orig(self);
            if (self.grantUniqueBonusScaling)
            {
                if (Run.instance.ambientLevelFloor > WConfig.LevelMaximumFinalBoss.Value)
                {
                    for (int i = 0; i < self.combatSquad.membersList.Count; i++)
                    {
                        //Set value to a flat, whatever it is
                        var t = self.combatSquad.membersList[i].inventory;
                        t.RemoveItem(RoR2Content.Items.UseAmbientLevel);
                        t.RemoveItem(RoR2Content.Items.LevelBonus, t.GetItemCount(RoR2Content.Items.LevelBonus));
                        t.GiveItem(RoR2Content.Items.LevelBonus, WConfig.LevelMaximumFinalBoss.Value - 1);
                    }
                }
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


        public static void DisableLevelSound_E(On.RoR2.LevelUpEffectManager.orig_OnRunAmbientLevelUp orig, Run run)
        {
            if (run.ambientLevel < 200)
            {
                orig(run);
            }
        }

        public static void DisableLevelSound_P(On.RoR2.LevelUpEffectManager.orig_OnCharacterLevelUp orig, CharacterBody characterBody)
        {
            if (characterBody.level < 200)
            {
                orig(characterBody);
            }
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
                            float loop = Run.instance.loopClearCount * 1f;
                            self.bossDirector.monsterCredit += (float)((int)(200f * loop * Mathf.Pow(Run.instance.compensatedDifficultyCoefficient, 0.5f) * (float)(1 + self.shrineBonusStacks)));
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
                float mult = 1f + Run.instance.loopClearCount * 0.5f;
                Debug.Log("Loop TP multiplying HP by " + mult);
                int hp = 10 + inv.GetItemCount(RoR2Content.Items.BoostHp);
                hp = (int)(hp * mult) - 10;
                inv.GiveItem(RoR2Content.Items.BoostHp, hp);
            }


        }


    }
}