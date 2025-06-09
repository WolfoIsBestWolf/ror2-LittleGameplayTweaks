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
            
            On.RoR2.TeleporterInteraction.Start += MorePortals;

            On.RoR2.Run.AdvanceStage += Run_AdvanceStage;

            On.RoR2.ScriptedCombatEncounter.BeginEncounter += FinalBossLevelLimit;

            //AdvanceStage onyl runs on Server so clients would desync-ish

            Run.onRunDestroyGlobal += Run_onRunDestroyGlobal;
            Run.onRunStartGlobal += Run_onRunStartGlobal;
            
            Stage.onStageStartGlobal += Stage_onStageStartGlobal;

            ChatMessageBase.chatMessageTypeToIndex.Add(typeof(SyncLevelStuffWithHost), (byte)ChatMessageBase.chatMessageIndexToType.Count);
            ChatMessageBase.chatMessageIndexToType.Add(typeof(SyncLevelStuffWithHost));

        }

        public class SyncLevelStuffWithHost : Chat.SimpleChatMessage
        {
            public bool config;
            public override string ConstructChatString()
            {         
                if (config == false)
                {
                    return null;
                }
                if (added == true)
                {
                    return null;
                }
                if (Run.instance is InfiniteTowerRun)
                {
                    //Doesnt use run ambient level
                    return null;
                }
                if (Run.instance.participatingPlayerCount == 1)
                {
                    //Singleplayer who cares
                    return null;
                }
                if (NetworkModCompatibilityHelper._networkModList.Length != 0)
                {
                    //Probably sharing modpack
                    return null;
                }
                added = true;
                Debug.Log("Adding fix for Clients without mod");
                IL.RoR2.CharacterBody.RecalculateStats += ReplaceUseAmbienWithLevelBonus;
                return null;
            }
            public override void Serialize(NetworkWriter writer)
            {
                base.Serialize(writer);
                writer.Write(config);
            }
            public override void Deserialize(NetworkReader reader)
            {
                base.Deserialize(reader);
                config = reader.ReadBoolean();
            }


        }

        public static int stageClearCount = 0;
        private static void Stage_onStageStartGlobal(Stage self)
        {
            if (!WConfig.cfgAddLevelCapPerStage.Value)
            {
                return;
            }
            if (Run.instance.loopClearCount == 0)
            {
                return;
            }
            //Doesn't increase on Moon2 this way
            if (self.sceneDef.stageOrder < 6)
            {
                //Ensure level cap and Stage count match
                for (int i = stageClearCount; stageClearCount <= Run.instance.stageClearCount; stageClearCount++)
                {
                    int loopCount = stageClearCount / 5;
                    if (loopCount > 0)
                    {
                        Run.ambientLevelCap += 20;
                    }
                }
            }
        }
        private static bool added = false;



        private static void Run_onRunStartGlobal(Run run)
        {
            levelBonus = (int)RoR2Content.Items.LevelBonus.itemIndex;
            if (NetworkServer.active)
            {
                Chat.SendBroadcastChat(new SyncLevelStuffWithHost
                {
                    config = WConfig.cfgAddLevelCapPerStage.Value
                });
            }
        }
        private static void Run_onRunDestroyGlobal(Run obj)
        {
            Run.ambientLevelCap = storedRunAmbientLevelCap;
            if (added)
            {
                IL.RoR2.CharacterBody.RecalculateStats -= ReplaceUseAmbienWithLevelBonus;
            }
        }
        public static int storedRunAmbientLevelCap = 99;
       
        private static void Run_AdvanceStage(On.RoR2.Run.orig_AdvanceStage orig, Run self, SceneDef nextScene)
        {
            orig(self, nextScene);
            if (WConfig.cfgAddLevelCapPerStage.Value == false)
            {
                return;
            }
            if (self.loopClearCount > 0)
            {
                SceneDef sceneDefForCurrentScene = SceneCatalog.GetSceneDefForCurrentScene();
                if ((sceneDefForCurrentScene.sceneType == SceneType.Stage || sceneDefForCurrentScene.sceneType == SceneType.UntimedStage) && !sceneDefForCurrentScene.preventStageAdvanceCounter)
                {
                    Run.ambientLevelCap += 20 * self.loopClearCount;
                }
            }
        }

        

        private static void MorePortals(On.RoR2.TeleporterInteraction.orig_Start orig, TeleporterInteraction self)
        {
            if (Run.instance)
            {
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
            }
            orig(self);
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
            //If level > 99, still set level to 99 and do the rest with LevelBonus
            //This way it's synced for people who do not have the mod.
            //Jank, but what can you do I guess.
 
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
                    if (levels > 99)
                    {
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
            if (inv && inv.itemStacks[levelBonus] != count)
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
                if (WConfig.LevelMaximumFinalBoss.Value && Run.instance.ambientLevelFloor > 99)
                {
                    for (int i = 0; i < self.combatSquad.membersList.Count; i++)
                    {
                        var t = self.combatSquad.membersList[i].inventory;
                        t.RemoveItem(RoR2Content.Items.UseAmbientLevel);
                        t.RemoveItem(RoR2Content.Items.LevelBonus, t.GetItemCount(RoR2Content.Items.LevelBonus));
                        t.GiveItem(RoR2Content.Items.LevelBonus, 99);
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
                //Debug.Log("Loop TP multiplying HP by " + mult);
                int hp = 10 + inv.GetItemCount(RoR2Content.Items.BoostHp);
                hp = (int)(hp * mult) - 10;
                inv.GiveItem(RoR2Content.Items.BoostHp, hp);
            }


        }


    }
}