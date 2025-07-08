using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System.Collections.ObjectModel;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
 
         
            On.EntityStates.Missions.BrotherEncounter.EncounterFinished.OnEnter += LoopPortal_Moon;
            On.EntityStates.Missions.LunarScavengerEncounter.FadeOut.OnEnter += LoopPortal_MS;

            On.EntityStates.LunarTeleporter.IdleToActive.OnEnter += IdleToActive_OnEnter;
            On.EntityStates.LunarTeleporter.IdleToActive.OnExit += IdleToActive_OnExit;
            On.EntityStates.LunarTeleporter.Idle.OnEnter += Idle_OnEnter;

            LoopingLevel.CallLate();

            Addressables.LoadAssetAsync<GameObject>(key: "34d770816ffbf0d468728c48853fd5f6").WaitForCompletion().AddComponent<LoopPortal>();
        }

        private static void MoreCredits(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.Before,
                x => x.MatchStfld("RoR2.CombatDirector", "monsterCredit")))
            {
                c.EmitDelegate<System.Func<float, float>>((credits) =>
                {
                    Debug.Log("TP Credits: "+credits);
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

        private static void Idle_OnEnter(On.EntityStates.LunarTeleporter.Idle.orig_OnEnter orig, EntityStates.LunarTeleporter.Idle self)
        {
            orig(self);
            if (Run.instance && Run.instance.eventFlags.Contains("KilledBrother"))
            {
               self.preferredInteractability = Interactability.Disabled;
            }         
        }
        private static void IdleToActive_OnEnter(On.EntityStates.LunarTeleporter.IdleToActive.orig_OnEnter orig, EntityStates.LunarTeleporter.IdleToActive self)
        {
            if (Run.instance && Run.instance.eventFlags.Contains("KilledBrother"))
            {
                self.outer.SetNextState(new EntityStates.LunarTeleporter.ActiveToIdle());
                return;
            }
            orig(self);
        }
        private static void IdleToActive_OnExit(On.EntityStates.LunarTeleporter.IdleToActive.orig_OnExit orig, EntityStates.LunarTeleporter.IdleToActive self)
        {
            if (Run.instance && Run.instance.eventFlags.Contains("KilledBrother"))
            {
                return;
            }
            orig(self);
        }

        private static void LoopPortal_Moon(On.EntityStates.Missions.BrotherEncounter.EncounterFinished.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.EncounterFinished self)
        {
            orig(self); 
            Run.instance.eventFlags.Add("KilledBrother");
            SpawnLoopPortal(0);
         
        }
 
        private static void LoopPortal_MS(On.EntityStates.Missions.LunarScavengerEncounter.FadeOut.orig_OnEnter orig, EntityStates.Missions.LunarScavengerEncounter.FadeOut self)
        {
            orig(self);
            SpawnLoopPortal(1);
        }

        public static void SpawnLoopPortal(int version)
        {
            if (!NetworkServer.active)
            { 
                return; 
            }
            if (!WConfig.cfgMoreLoop.Value)
            {
                return;
            }
            GameObject portalObject = null;

            switch (version)
            {
                case 0:
                    //portalObject = Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>(key: "161f0fdda19f1eb4389d36a5f336cb84").WaitForCompletion(), new Vector3(1114.95f, -283.011f, 1191.11f), Quaternion.Euler(new Vector3(0f, 222f, 0f)));
                    portalObject = Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>(key: "34d770816ffbf0d468728c48853fd5f6").WaitForCompletion(), new Vector3(1117.95f, -283.011f, 1194.11f), Quaternion.Euler(new Vector3(0f, 275f, 0f)));
                    portalObject.GetComponentInChildren<LightIntensityCurve>().maxIntensity = 1f;
                   
                    break;
                case 1: //MS Limbo
                    //portalObject = Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>(key: "eadfcaf9ea3275e49858ed19f874db5a").WaitForCompletion(), new Vector3(60.1877f, -3.4472f, -3.8198f), Quaternion.Euler(new Vector3(0f, 312.045f, 0f)));
                    portalObject = Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>(key: "34d770816ffbf0d468728c48853fd5f6").WaitForCompletion(), new Vector3(60f, -3.5f, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                   
                    break;
                case 2:
                    //34d770816ffbf0d468728c48853fd5f6
                    break;

            }
            NetworkServer.Spawn(portalObject);
        }
 

        private static void MorePortals(On.RoR2.TeleporterInteraction.orig_Start orig, TeleporterInteraction self)
        {
            bool stage5 = SceneCatalog.mostRecentSceneDef.stageOrder == 5;
            if (Run.instance)
            {
                int stageClearCount = Run.instance.stageClearCount;
                if (WConfig.VoidPortalStage5.Value)
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
            if (WConfig.CelestialStage10.Value)
            {
                if (NetworkServer.active)
                {
                    if (stage5 && Run.instance.stageClearCount > 7 && !Run.instance.GetEventFlag("NoMysterySpace"))
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


    public class LoopPortal: MonoBehaviour
    {
        public void OnEnable()
        {
            if (SceneInfo.instance)
            {
                string a = SceneInfo.instance.sceneDef.cachedName;
                string context = string.Empty;
                if (a == "moon2")
                {
                    context = "ACHIEVEMENT_LOOPONCE_DESCRIPTION";
                }
                else if (a == "limbo")
                {
                    context = "ACHIEVEMENT_REPEATEDLYDUPLICATEITEMS_NAME";
                }
                else
                {
                    return;
                }

                //This isn't networked so like blegh

                Object.Destroy(this.GetComponent<GenericObjectiveProvider>());

                GenericDisplayNameProvider genericDisplayNameProvider = this.GetComponent<GenericDisplayNameProvider>();
                genericDisplayNameProvider.displayToken = "ACHIEVEMENT_LOOPONCE_NAME";
                GenericInteraction genericInteraction = this.GetComponent<GenericInteraction>();
                genericInteraction.contextToken = context;

                SceneExitController sceneExitController = this.GetComponent<SceneExitController>();
                sceneExitController.useRunNextStageScene = true;
         
            }
        }
    }

}