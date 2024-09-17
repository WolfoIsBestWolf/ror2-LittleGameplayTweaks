using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LittleGameplayTweaks
{
    public class LoopStageVariants
    {
        public static ExpansionDef DLC2 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC2/Common/DLC2.asset").WaitForCompletion();

        //public static string[] LoopVariants = new string[] { "wispgraveyard", "golemplains", "golemplains2" };
        public static List<string> LoopVariants = new List<string>() { "wispgraveyard", "golemplains" };


        public static void Start()
        {

            //1
            LanguageAPI.Add("MAP_GOLEMPLAINS_TITLE_LOOP", "Sunset Prairie"); //Sunset
            LanguageAPI.Add("MAP_GOLEMPLAINS_SUBTITLE_LOOP", "Grounds Alpha");

            LanguageAPI.Add("MAP_BLACKBEACH_TITLE_LOOP", "Distant Roost"); //Stormy?
            LanguageAPI.Add("MAP_BLACKBEACH_SUBTITLE_LOOP", "Ground Zero");

            LanguageAPI.Add("MAP_SNOWYFOREST_TITLE_LOOP", "Aurora Woods"); //Aurorare Borelasis?
            LanguageAPI.Add("MAP_SNOWYFOREST_SUBTITLE_LOOP", "A saviours light");
            //
            //2
            LanguageAPI.Add("MAP_GOOLAKE_TITLE_LOOP", "Tar Pit"); //Tar Flood?
            LanguageAPI.Add("MAP_GOOLAKE_SUBTITLE_LOOP", "Writhing Restlessly");

            LanguageAPI.Add("MAP_FOGGYSWAMP_TITLE_LOOP", "Wetland Aspect"); 
            LanguageAPI.Add("MAP_FOGGYSWAMP_SUBTITLE_LOOP", "Rehabilitation Zone");

            LanguageAPI.Add("MAP_ANCIENTLOFT_TITLE_LOOP", "Aphelian Sanctuary"); //Provi fan club tea time
            LanguageAPI.Add("MAP_ANCIENTLOFT_SUBTITLE_LOOP", "Cleansing Center");
            //
            //3
            LanguageAPI.Add("MAP_FROZENWALL_TITLE_LOOP", "Rallypoint Delta");
            //LanguageAPI.Add("MAP_FROZENWALL_SUBTITLE_LOOP", "'Contact Light' Survivor Camp");
            LanguageAPI.Add("MAP_FROZENWALL_SUBTITLE_LOOP", "Failed Rescue");

            LanguageAPI.Add("MAP_WISPGRAVEYARD_TITLE_LOOP", "Blighted Acres"); //Dusk
            LanguageAPI.Add("MAP_WISPGRAVEYARD_SUBTITLE_LOOP", "Protective Garden");

            LanguageAPI.Add("MAP_SULFURPOOLS_TITLE_LOOP", "Sulfur Pools");  //Sulfur Blue Fire? Idk
            LanguageAPI.Add("MAP_SULFURPOOLS_SUBTITLE_LOOP", "Pungent Spring");
            //
            //4
            LanguageAPI.Add("MAP_DAMPCAVE_TITLE_LOOP", "Scarlet Abyss"); //Red Plane?
            LanguageAPI.Add("MAP_DAMPCAVE_SUBTITLE_LOOP", "Tectonic Relics");

            LanguageAPI.Add("MAP_SHIPGRAVEYARD_TITLE_LOOP", "Siren's Call");
            LanguageAPI.Add("MAP_SHIPGRAVEYARD_SUBTITLE_LOOP", "Ship Graveyard");

            LanguageAPI.Add("MAP_ROOTJUNGLE_TITLE_LOOP", "Sundered Grove");
            LanguageAPI.Add("MAP_ROOTJUNGLE_SUBTITLE_LOOP", "Dormant Locus");
            //
            //5
            LanguageAPI.Add("MAP_SKYMEADOW_TITLE_LOOP", "Sky Meadow");
            LanguageAPI.Add("MAP_SKYMEADOW_SUBTITLE_LOOP", "Sprite Fields"); //Hostile Paradise? Use that somewhere

            LanguageAPI.Add("MAP_HELMINTHROOST_TITLE_LOOP", "Helminth Hatchery");
            LanguageAPI.Add("MAP_HELMINTHROOST_SUBTITLE_LOOP", "A Brother\u2019s Respite");

            LanguageAPI.Add("MAP_MOON_TITLE_LOOP" , "Cessation"); //The Spooky
            LanguageAPI.Add("MAP_MOON_SUBTITLE_LOOP", "Dark Side of the moon");

            //
            //
            On.RoR2.Stage.PreStartClient += LoopWeatherChanges;
            On.RoR2.UI.AssignStageToken.Start += LoopNameChanges;


            //UnusedStages();
        }

        public static void UnusedStages()
        {
            SceneDef newScenedDef = ScriptableObject.CreateInstance<SceneDef>();
            newScenedDef.sceneAddress = new AssetReferenceScene("31b8f728a914c9a4faa3df76d1bc0c0e");
            newScenedDef.cachedName = "golemplains_trailer";
            newScenedDef.nameToken = "Trailer Plains";
            newScenedDef.shouldIncludeInLogbook = false;
            R2API.ContentAddition.AddSceneDef(newScenedDef);

            newScenedDef = ScriptableObject.CreateInstance<SceneDef>();
            newScenedDef.sceneAddress = new AssetReferenceScene("b30e537b28c514b4a99227ab56a3e1d3");
            newScenedDef.cachedName = "ItemLogBookPositionalOffsets";
            newScenedDef.nameToken = "ItemLogBookPositionalOffsets";
            newScenedDef.shouldIncludeInLogbook = false;
            R2API.ContentAddition.AddSceneDef(newScenedDef);

            newScenedDef = ScriptableObject.CreateInstance<SceneDef>();
            newScenedDef.sceneAddress = new AssetReferenceScene("722873b571c73734c8572658dbb8f0db");
            newScenedDef.cachedName = "renderitem";
            newScenedDef.nameToken = "renderitem";
            newScenedDef.shouldIncludeInLogbook = false;
            R2API.ContentAddition.AddSceneDef(newScenedDef);

            newScenedDef = ScriptableObject.CreateInstance<SceneDef>();
            newScenedDef.sceneAddress = new AssetReferenceScene("835344f0a7461cc4b8909469b31a3ccc");
            newScenedDef.cachedName = "slice2";
            newScenedDef.nameToken = "slice2";
            newScenedDef.shouldIncludeInLogbook = false;
            R2API.ContentAddition.AddSceneDef(newScenedDef);

            newScenedDef = ScriptableObject.CreateInstance<SceneDef>();
            newScenedDef.sceneAddress = new AssetReferenceScene("5d8ea33392b43b94daac86dcf06740ab");
            newScenedDef.cachedName = "space";
            newScenedDef.nameToken = "space";
            newScenedDef.shouldIncludeInLogbook = false;
            R2API.ContentAddition.AddSceneDef(newScenedDef);

            newScenedDef = ScriptableObject.CreateInstance<SceneDef>();
            newScenedDef.sceneAddress = new AssetReferenceScene("db3348f5ee64faa48b2c14c3c52d5186");
            newScenedDef.cachedName = "stage1";
            newScenedDef.nameToken = "stage1";
            newScenedDef.shouldIncludeInLogbook = false;
            R2API.ContentAddition.AddSceneDef(newScenedDef);
        }

        private static void LoopNameChanges(On.RoR2.UI.AssignStageToken.orig_Start orig, RoR2.UI.AssignStageToken self)
        {
            orig(self);
            if (Run.instance && Run.instance.stageClearCount > 4)
            {
                if (Run.instance.IsExpansionEnabled(DLC2))
                {
                    SceneDef mostRecentSceneDef = SceneCatalog.mostRecentSceneDef;
                    if (LoopVariants.Contains(mostRecentSceneDef.baseSceneName))
                    {
                        Debug.Log(mostRecentSceneDef.baseSceneName + "LOOP");

                        self.titleText.SetText(Language.GetString(mostRecentSceneDef.nameToken + "_LOOP"), true);
                        self.subtitleText.SetText(Language.GetString(mostRecentSceneDef.subtitleToken + "_LOOP"), true);
                    }
                }
            }
        }

        private static void LoopWeatherChanges(On.RoR2.Stage.orig_PreStartClient orig, Stage self)
        {
            orig(self);
            if (Run.instance.stageClearCount > 4)
            {
                if (Run.instance.IsExpansionEnabled(DLC2))
                {
                    //Debug.Log(SceneInfo.instance.sceneDef);

                    switch (SceneInfo.instance.sceneDef.baseSceneName)
                    {
                        case "golemplains":
                            Loop_1_Golem();
                            break;
                        case "blackbeach":
                            Loop_1_Roost();
                            break;
                        case "snowyforest":
                            Loop_1_Snowy();
                            break;
                        case "goolake":
                            Loop_2_Goo();
                            break;
                        case "wispgraveyard":
                            Loop_3_Wisp();
                            break;
                        case "dampcavesimple":
                            Loop_4_Damp();
                            break;
                    }
                }
            }
        }


        public static void Loop_1_Golem()
        {
            GameObject Weather = GameObject.Find("/Weather, Golemplains");

            UnityEngine.Rendering.PostProcessing.PostProcessVolume process = Weather.transform.GetChild(2).gameObject.GetComponent<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
            UnityEngine.Rendering.PostProcessing.PostProcessProfile TrailerWeather = Addressables.LoadAssetAsync<UnityEngine.Rendering.PostProcessing.PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneGolemplainsTrailer.asset").WaitForCompletion();
            process.profile = TrailerWeather;
            process.sharedProfile = TrailerWeather;

            Light TheSun = Weather.transform.GetChild(1).gameObject.GetComponent<Light>();
            TheSun.color = new Color(1f, 0.5329f, 0f, 1);
            TheSun.intensity = 1.14f;

            SetAmbientLight newAmbient = Weather.AddComponent<SetAmbientLight>();
            newAmbient.setAmbientLightColor = true;
            newAmbient.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            newAmbient.ambientSkyColor = new Color(0.4191f, 0.305f, 0.7264f, 0f);
            newAmbient.ambientEquatorColor = new Color(0.114f, 0.125f, 0.133f, 1f);
            newAmbient.ambientGroundColor = new Color(0.047f, 0.043f, 0.035f, 1f);
            newAmbient.ambientIntensity = 1;

            newAmbient.setSkyboxMaterial = true;
            newAmbient.skyboxMaterial = Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/bazaar/matSkybox4.mat").WaitForCompletion();
            newAmbient.ApplyLighting();
        }
        public static void Loop_1_Roost()
        {

        }
        public static void Loop_1_Snowy()
        {

        }

        public static void Loop_2_Goo()
        {

        }
        public static void Loop_2_Swamp()
        {

        }
        public static void Loop_2_Loft()
        {

        }
        public static void Loop_3_Snow()
        {

        }
        public static void Loop_3_Wisp()
        {
            GameObject.Find("/Weather, Wispgraveyard").SetActive(false);
            //GameObject.Find("/Weather, Eclipse").SetActive(true); //Can not find inactive Objects

            GameObject EclipseWeather = GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/eclipseworld/Weather, Eclipse.prefab").WaitForCompletion());
            EclipseWeather.transform.GetChild(0).GetComponent<UnityEngine.ReflectionProbe>().bakedTexture = Addressables.LoadAssetAsync<Cubemap>(key: "RoR2/Base/wispgraveyard/ReflectionProbe-2.exr").WaitForCompletion();
            EclipseWeather.transform.GetChild(4).gameObject.SetActive(true);
        }
        public static void Loop_3_Sulfur()
        {

        }
        public static void Loop_4_Damp()
        {

        }
        public static void Loop_4_Ship()
        {

        }
        public static void Loop_4_Jungle()
        {

        }
        public static void Loop_5_Sky()
        {

        }
        public static void Loop_5_Roost()
        {

        }

    }
}