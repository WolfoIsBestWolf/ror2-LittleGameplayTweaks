using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using static LoopVariants.LoopVariantsMain;

namespace LoopVariants
{
    public class OfficialVariant
    {
        public static void Awake()
        {
            On.RoR2.Run.PickNextStageScene += Official_Variants_MainPath;
            On.RoR2.SceneExitController.IfLoopedUseValidLoopStage += Official_Variants_AltPath;
            On.RoR2.BazaarController.IsUnlockedBeforeLooping += Official_Variants_Bazaar;
        }

        public static bool Official_Variants_Bazaar(On.RoR2.BazaarController.orig_IsUnlockedBeforeLooping orig, BazaarController self, SceneDef sceneDef)
        {
            if (Run.instance)
            {
                SyncLoopWeather weather = Run.instance.GetComponent<SyncLoopWeather>();
                if (Run.instance.ruleBook.stageOrder == StageOrder.Normal)
                {
                    //If is loop Variant
                    //And decided next should be loop, just allow ig?
                    if (sceneDef.isLockedBeforeLooping)
                    {
                        if (weather.NextStage_LoopVariant)
                        {
                            if (WConfig.LoopVariant_OnPreLoop_Vanilla.Value)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return orig(self, sceneDef);
        }


        public static void Official_Variants_MainPath(On.RoR2.Run.orig_PickNextStageScene orig, Run self, WeightedSelection<SceneDef> choices)
        {
            orig(self, choices);


            SyncLoopWeather weather = Run.instance.GetComponent<SyncLoopWeather>();
            if (!weather)
            {
                Debug.LogWarning("SyncLoopWeather wasn't added when the run started, How?");
                return;
            }
            if (!NetworkServer.active)
            {
                //This doesn't run on clients anyways
                return;
            }
            //Both variants already in random stage order
            if (self.ruleBook.stageOrder == StageOrder.Normal)
            {
                if (weather.NextStage_LoopVariant) //When In lobby gets rolled.
                {
                    if (self.stageClearCount < 5 && WConfig.LoopVariant_OnPreLoop_Vanilla.Value)
                    {
                        if (self.nextStageScene && self.nextStageScene.loopedSceneDef)
                        {
                            if (self.IsExpansionEnabled(self.nextStageScene.loopedSceneDef.requiredExpansion))
                            {
                                Debug.Log("Replacing " + self.nextStageScene + " with " + self.nextStageScene.loopedSceneDef);
                                self.nextStageScene = self.nextStageScene.loopedSceneDef;
                            }
                        }
                    }
                }
                else //Next stage NOT loop
                {
                    //If Looped and Chance Failed, use pre Loop if config enables it.
                    if (self.stageClearCount > 4 && WConfig.PreLoopVariant_PostLoop_Vanilla.Value)
                    {
                        if (loopSceneDefToNon.ContainsKey(self.nextStageScene.baseSceneName))
                        {
                            SceneDef preLoop = loopSceneDefToNon[self.nextStageScene.baseSceneName];
                            if (preLoop != null)
                            {
                                Debug.Log("Replacing " + self.nextStageScene + " with " + preLoop);
                                self.nextStageScene = preLoop;
                            }
                        }
                    }
                }
            }


        }

        public static SceneDef Official_Variants_AltPath(On.RoR2.SceneExitController.orig_IfLoopedUseValidLoopStage orig, SceneExitController self, SceneDef sceneDef)
        {
            if (NetworkServer.active)
            {
                SyncLoopWeather weather = Run.instance.GetComponent<SyncLoopWeather>();
                if (Run.instance.ruleBook.stageOrder == StageOrder.Normal)
                {
                    if (weather.NextStage_LoopVariant) //When In lobby gets rolled.
                    {
                        if (Run.instance.stageClearCount < 5 && WConfig.LoopVariant_OnPreLoop_Vanilla.Value)
                        {
                            if (sceneDef.loopedSceneDef)
                            {
                                Debug.Log("Replacing " + sceneDef + " with " + sceneDef.loopedSceneDef);
                                return sceneDef.loopedSceneDef;
                            }
                        }
                    }
                    else //Next stage NOT loop
                    {
                        //If Looped and Chance Failed, use pre Loop if config enables it.
                        if (Run.instance.stageClearCount > 4 && WConfig.PreLoopVariant_PostLoop_Vanilla.Value)
                        {
                            if (loopSceneDefToNon.ContainsKey(sceneDef.baseSceneName))
                            {
                                Debug.Log("Replacing " + sceneDef.loopedSceneDef + " with " + sceneDef);
                                return sceneDef;
                            }
                        }
                    }
                }
            }


            return orig(self, sceneDef);
        }


    }
}