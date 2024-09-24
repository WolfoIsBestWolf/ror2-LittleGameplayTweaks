using BepInEx;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace LoopVariants
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("Wolfo.LoopVariants", "WolfosLoopVariants", "1.0.0")]
    //[R2APISubmoduleDependency(nameof(ContentAddition), nameof(LanguageAPI), nameof(PrefabAPI), nameof(ItemAPI), nameof(LoadoutAPI), nameof(EliteAPI))]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]


    public class LoopVariantsMain : BaseUnityPlugin
    {
        public static ExpansionDef DLC2 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC2/Common/DLC2.asset").WaitForCompletion();

        //public static string[] LoopVariants = new string[] { "wispgraveyard", "golemplains", "golemplains2" };
        public static List<string> ExistingVariants = new List<string>() { "wispgraveyard", "golemplains", "goolake", "dampcavesimple", "snowyforest", "helminthroost" };
        public System.Random random = new System.Random();
        public static bool WillNextStangeUseLoopVariant = false;
        public static bool IsPreviousStageLoopVariant = false;
        public Dictionary<string, SceneDef> loopSceneDefToNon = new Dictionary<string, SceneDef>();

        public void Awake()
        {
            WConfig.InitConfig();

            bool stageAesth = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("HIFU.StageAesthetic");
            if (WConfig.cfgLoopWeather.Value)
            {
                Variants.Start();


                On.RoR2.Stage.PreStartClient += LoopWeatherChanges;
                On.RoR2.UI.AssignStageToken.Start += LoopNameChanges;

                On.RoR2.Run.PickNextStageScene += Run_PickNextStageScene;
                On.RoR2.SceneExitController.IfLoopedUseValidLoopStage += SceneExitController_IfLoopedUseValidLoopStage;

                //On.RoR2.DirectorCard.IsAvailable += DirectorCard_IsAvailable;
            }

        }

        private bool DirectorCard_IsAvailable(On.RoR2.DirectorCard.orig_IsAvailable orig, DirectorCard self)
        {
            if (WillNextStangeUseLoopVariant)
            {
                self.minimumStageCompletions = 0;
            }
            return orig(self);
        }

        private void Run_PickNextStageScene(On.RoR2.Run.orig_PickNextStageScene orig, Run self, WeightedSelection<SceneDef> choices)
        {
            orig(self, choices);

            if (self.nextStageScene)
            {
                IsPreviousStageLoopVariant = WillNextStangeUseLoopVariant;
                if (Run.instance.stageClearCount >= 4)
                {
                    WillNextStangeUseLoopVariant = RoR2.Util.CheckRoll(WConfig.Chance_Loop.Value, null);
                }
                else
                {
                    WillNextStangeUseLoopVariant = RoR2.Util.CheckRoll(WConfig.Chance_PreLoop.Value, null);
                }
                Debug.Log("Loop weather for curr " + IsPreviousStageLoopVariant);
                Debug.Log("Loop weather for next " + WillNextStangeUseLoopVariant);


                if (WillNextStangeUseLoopVariant)
                {
                    if (WConfig.ApplyLoopToVanilla.Value)
                    {
                        if (self.nextStageScene.loopedSceneDef)
                        {
                            self.nextStageScene = self.nextStageScene.loopedSceneDef;
                        }
                    }
                    else
                    {
                        //Idk
                    }
                }
            }
        }

        private static void LoopNameChanges(On.RoR2.UI.AssignStageToken.orig_Start orig, RoR2.UI.AssignStageToken self)
        {
            orig(self);
            if (Run.instance)
            {
                if (IsPreviousStageLoopVariant)
                {
                    SceneDef mostRecentSceneDef = SceneCatalog.mostRecentSceneDef;
                    if (ExistingVariants.Contains(mostRecentSceneDef.baseSceneName))
                    {
                        Debug.Log(mostRecentSceneDef.baseSceneName + "_LOOP");

                        self.titleText.SetText(Language.GetString(mostRecentSceneDef.nameToken + "_LOOP"), true);
                        self.subtitleText.SetText(Language.GetString(mostRecentSceneDef.subtitleToken + "_LOOP"), true);
                    }
                }
            }
        }

        private static void LoopWeatherChanges(On.RoR2.Stage.orig_PreStartClient orig, Stage self)
        {
            orig(self);
            if (WillNextStangeUseLoopVariant)
            {
                //Should just work lol?
                try
                {
                    switch (SceneInfo.instance.sceneDef.baseSceneName)
                    {
                        case "golemplains":
                            Variants.Loop_1_Golem();
                            break;
                        case "blackbeach":
                            //Loop_1_Roost();
                            break;
                        case "snowyforest":
                            Variants.Loop_1_Snowy();
                            break;
                        case "goolake":
                            Variants.Loop_2_Goo();
                            break;
                        case "frozenwall":
                            //Variants.Loop_3_Frozen();
                            break;
                        case "wispgraveyard":
                            Variants.Loop_3_Wisp();
                            break;
                        case "dampcavesimple":
                            VariantsDampCave.Loop_4_Damp();
                            break;
                        case "rootjungle":
                            //Variants.Loop_4_Jungle();
                            break;
                        case "helminthroost":
                            VariantsHelminthRoost.Loop_5_Roost();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning("LoopVariants Error: " + e);
                }

            }
        }

        private static SceneDef SceneExitController_IfLoopedUseValidLoopStage(On.RoR2.SceneExitController.orig_IfLoopedUseValidLoopStage orig, SceneExitController self, SceneDef sceneDef)
        {
            return orig(self, sceneDef);
        }
    }

}