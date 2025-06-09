using HarmonyLib;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class Changes_Stages
    {
        public static void Start()
        {


            On.RoR2.ClassicStageInfo.Start += MoreSceneCredits;
            On.RoR2.ClassicStageInfo.Start += RunsAlways_ClassicStageInfo_Start;


 
            //Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/voidstage/voidstage.asset").WaitForCompletion().sceneType = Arena.sceneType;
            Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/voidraid/voidraid.asset").WaitForCompletion().sceneType = SceneType.Intermission;

            On.RoR2.ArenaMissionController.OnStartServer += ArenaMissionController_OnEnable;
            On.EntityStates.Missions.Arena.NullWard.WardOnAndReady.OnEnter += WardOnAndReady_OnEnter;
            On.EntityStates.Missions.Arena.NullWard.Active.OnEnter += Active_OnEnter;



            if (ConfigStages.Stage_F_Moon.Value)
            {
                On.RoR2.EscapeSequenceController.OnEnable += EscapeSequenceController_OnEnable;
            }

            On.RoR2.BazaarController.SetUpSeerStations += ThirdSeerNew;
            On.RoR2.SeerStationController.SetRunNextStageToTarget += BazaarDisableAllSeers;
        }

        private static void BazaarDisableAllSeers(On.RoR2.SeerStationController.orig_SetRunNextStageToTarget orig, SeerStationController self)
        {
            orig(self);
            if (self.transform.parent != null)
            {
                SeerStationController[] seers = self.transform.parent.GetComponentsInChildren<SeerStationController>(false);
                for (int i = 0; i < seers.Length; i++)
                {
                    seers[i].GetComponent<PurchaseInteraction>().SetAvailable(false);
                }
            }

          
        }

        public static void ThirdSeerNew(On.RoR2.BazaarController.orig_SetUpSeerStations orig, BazaarController self)
        {
            GameObject newSeerObject = null;
            SeerStationController newSeer = null;
            bool doIt = WConfig.ThirdLunarSeer.Value && NetworkServer.active;
            if (doIt)
            {
                SeerStationController oldSeer = self.seerStations[0].GetComponent<SeerStationController>();
 
                newSeerObject = GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/bazaar/SeerStation.prefab").WaitForCompletion(), self.seerStations[0].gameObject.transform.parent);
                newSeerObject.transform.localPosition = new Vector3(-45.9807f, -15.22f, 9.5654f);
                newSeerObject.transform.localRotation = new Quaternion(0f, 0.7772f, 0f, 0.6293f);
                newSeer = newSeerObject.GetComponent<SeerStationController>();
                newSeer.explicitTargetSceneExitController = oldSeer.explicitTargetSceneExitController;
                HG.ArrayUtils.ArrayAppend(ref self.seerStations, newSeer); //Let the game take care of it itself
                NetworkServer.Spawn(newSeerObject);

            }
            orig(self);
            if (doIt)
            {
                //Nvm game can't do it
                //If game overwrites seer with Gilded, doesn't use third seer properly
                if (newSeer.targetSceneDefIndex == -1)
                {
                    List<int> list = new List<int>();
                    if (Run.instance.nextStageScene != null)
                    {
                        int stageOrder = Run.instance.nextStageScene.stageOrder;
                        foreach (SceneDef sceneDef in SceneCatalog.allSceneDefs)
                        {
                            if (sceneDef.stageOrder == stageOrder && (sceneDef.requiredExpansion == null || Run.instance.IsExpansionEnabled(sceneDef.requiredExpansion)) && self.IsUnlockedBeforeLooping(sceneDef))
                            {
                                list.Add((int)sceneDef.sceneDefIndex);
                            }
                        }
                    }
                    foreach (SeerStationController seerStation in self.seerStations)
                    {
                        list.Remove(seerStation.targetSceneDefIndex);
                    }
                    if (list.Count > 0)
                    {
                        Util.ShuffleList(list, self.rng);
                        newSeer.SetTargetSceneDefIndex(list[0]);
                        newSeer.GetComponent<PurchaseInteraction>().SetAvailable(true);
                    }
                }
                

            }
        }

        public static void EscapeSequenceController_OnEnable(On.RoR2.EscapeSequenceController.orig_OnEnable orig, EscapeSequenceController self)
        {
            orig(self);
            if (Run.instance.stageClearCount > 7)
            {
                self.gameObject.transform.GetChild(0).GetChild(1).GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsVoidFamily.asset").WaitForCompletion();
            }
        }


        public static void RunsAlways_ClassicStageInfo_Start(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            if (Run.instance && SceneInfo.instance)
            {
                //self.sceneDirectorInteractibleCredits += 550;

                switch (SceneInfo.instance.sceneDef.cachedName)
                {
                    case "goolake":
                        BuffDef SlowTar = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/Base/Common/bdClayGoo.asset").WaitForCompletion();
                        GameObject MiscProps = GameObject.Find("/HOLDER: Misc Props");
                        GameObject WaterFall = GameObject.Find("/HOLDER: GameplaySpace/mdlGlDam/GL_AqueductPartial/GL_Waterfall");
                        if (MiscProps)
                        {
                            GameObject GooPlaneOld1 = MiscProps.transform.GetChild(2).gameObject;
                            GameObject GooPlaneOld2 = MiscProps.transform.GetChild(3).gameObject;
                            if (WaterFall)
                            {
                                WaterFall.transform.GetChild(8).GetComponent<DebuffZone>().buffType = SlowTar;
                                GooPlaneOld1.GetComponentInChildren<DebuffZone>().buffType = SlowTar;
                                GooPlaneOld2.GetComponentInChildren<DebuffZone>().buffType = SlowTar;
                            }
                        }
                        //UnityEngine.RenderSettings.defaultReflectionMode
                        break;
                }
            }
            orig(self);
        }



        public static void Active_OnEnter(On.EntityStates.Missions.Arena.NullWard.Active.orig_OnEnter orig, EntityStates.Missions.Arena.NullWard.Active self)
        {
            orig(self);
            if (WConfig.RegenArenaCells.Value)
            {
                BuffWard[] ward = self.gameObject.GetComponents<BuffWard>();
                if (ward.Length == 2)
                {
                    ward[0].enabled = false;
                    ward[1].enabled = false;
                }
            }
        }

        public static void WardOnAndReady_OnEnter(On.EntityStates.Missions.Arena.NullWard.WardOnAndReady.orig_OnEnter orig, EntityStates.Missions.Arena.NullWard.WardOnAndReady self)
        {
            orig(self);
            if (WConfig.RegenArenaCells.Value)
            {
                BuffWard[] ward = self.gameObject.GetComponents<BuffWard>();
                if (ward.Length == 2)
                {
                    ward[0].enabled = true;
                    ward[1].enabled = true;
                }
            }
        }


        public static void ArenaMissionController_OnEnable(On.RoR2.ArenaMissionController.orig_OnStartServer orig, ArenaMissionController self)
        {
            orig(self);
            for (int i = 0; i < self.nullWards.Length; i++)
            {
                if (WConfig.FasterArenaCells.Value)
                {
                    if (i < 4)
                    {
                        self.nullWards[i].GetComponent<HoldoutZoneController>().baseChargeDuration = 30;
                    }
                    else if (i < 8)
                    {
                        self.nullWards[i].GetComponent<HoldoutZoneController>().baseChargeDuration = 45;
                    }
                    else
                    {
                    }
                }
                if (WConfig.RegenArenaCells.Value)
                {
                    BuffWard buffWard = self.nullWards[i].AddComponent<BuffWard>();
                    buffWard.enabled = false;
                    buffWard.radius = 5;
                    buffWard.buffDuration = 1.3f;
                    buffWard.buffDef = DLC1Content.Buffs.MushroomVoidActive;
                    buffWard = self.nullWards[i].AddComponent<BuffWard>();
                    buffWard.enabled = false;
                    buffWard.radius = 5;
                    buffWard.buffDuration = 1.3f;
                    buffWard.buffDef = RoR2Content.Buffs.CrocoRegen;
                }

            }
        }

        public static void MoreSceneCredits(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            //Idk why we do this prior to Start but whatever
            if (WConfig.cfgCredits_Stages.Value)
            {
                if (Run.instance && SceneInfo.instance)
                {
                    switch (SceneInfo.instance.sceneDef.baseSceneName)
                    {
                        case "golemplains":
                            self.sceneDirectorInteractibleCredits += 20; //Rather buff these two a bit than nerf Snowy Ig we'll see
                            break;
                        case "blackbeach":

                            self.sceneDirectorInteractibleCredits += 20; //
                            break;
                        case "goolake":
                            self.sceneDirectorInteractibleCredits = 280;
                            HG.ArrayUtils.ArrayAppend(ref self.bonusInteractibleCreditObjects,
                               new ClassicStageInfo.BonusInteractibleCreditObject
                               {
                                   points = Run.instance.participatingPlayerCount == 1 ? -40 : -60,
                                   objectThatGrantsPointsIfEnabled = RoR2.Run.instance.gameObject
                               });
                            break;
                        case "sulfurpools":
                            if (ConfigStages.Stage_3_Sulfur.Value)
                            {
                                self.sceneDirectorInteractibleCredits += 20; //Hell stage
                            }
                            break;
                        case "rootjungle":
                            HG.ArrayUtils.ArrayAppend(ref self.bonusInteractibleCreditObjects,
                            new ClassicStageInfo.BonusInteractibleCreditObject
                            {
                                points = 50,
                                objectThatGrantsPointsIfEnabled = RoR2.Run.instance.gameObject
                            });      
                            break;
                        case "shipgraveyard":
                            HG.ArrayUtils.ArrayAppend(ref self.bonusInteractibleCreditObjects,
                            new ClassicStageInfo.BonusInteractibleCreditObject
                            {
                                points = 50,
                                objectThatGrantsPointsIfEnabled = RoR2.Run.instance.gameObject
                            });
                            break;
                        case "goldshores":
                            if (WConfig.cfgGoldShoresCredits.Value)
                            {
                                self.sceneDirectorInteractibleCredits += 75; //For Fun ig
                            }
                            break;
                    }
                }
            }
            orig(self);
        }

    }
}