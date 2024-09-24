using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants
    {

        public static void Start()
        {
 
            //1
            LanguageAPI.Add("MAP_GOLEMPLAINS_TITLE_LOOP", "Sunset Prairie"); //Sunset
            LanguageAPI.Add("MAP_GOLEMPLAINS_SUBTITLE_LOOP", "Grounds Alpha");

            LanguageAPI.Add("MAP_BLACKBEACH_TITLE_LOOP", "Distant Roost"); //Stormy?
            LanguageAPI.Add("MAP_BLACKBEACH_SUBTITLE_LOOP", "Ground Zero");

            LanguageAPI.Add("MAP_SNOWYFOREST_TITLE_LOOP", "Aurora Woods"); //Aurorare Borelasis?
            LanguageAPI.Add("MAP_SNOWYFOREST_SUBTITLE_LOOP", "Astral Light");
            //
            //2
            LanguageAPI.Add("MAP_GOOLAKE_TITLE_LOOP", "Abandoned Tarpit"); //Tar Flood?
            LanguageAPI.Add("MAP_GOOLAKE_SUBTITLE_LOOP", "Flowing Endlessly");

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

            LanguageAPI.Add("MAP_SULFURPOOLS_TITLE_LOOP", "Sulfur Pools");  //Sulfur Blue Fire? Sulfur Red?
            LanguageAPI.Add("MAP_SULFURPOOLS_SUBTITLE_LOOP", "Pungent Spring");
            //
            //4
            //Crimson Abyss? Crimson Depths?
            LanguageAPI.Add("MAP_DAMPCAVE_TITLE_LOOP", "Scarlet Abyss"); //Red Plane?
            LanguageAPI.Add("MAP_DAMPCAVE_SUBTITLE_LOOP", "Dimensional Activity");

            LanguageAPI.Add("MAP_SHIPGRAVEYARD_TITLE_LOOP", "Siren's Call");
            LanguageAPI.Add("MAP_SHIPGRAVEYARD_SUBTITLE_LOOP", "Ship Graveyard");

            LanguageAPI.Add("MAP_ROOTJUNGLE_TITLE_LOOP", "Sundered Grove"); //Copy Golden Dieback
            LanguageAPI.Add("MAP_ROOTJUNGLE_SUBTITLE_LOOP", "Dormant Locus");
            //
            //5
            LanguageAPI.Add("MAP_SKYMEADOW_TITLE_LOOP", "Sky Meadow");
            LanguageAPI.Add("MAP_SKYMEADOW_SUBTITLE_LOOP", "Sprite Fields"); //Hostile Paradise? Use that somewhere

            //LanguageAPI.Add("MAP_HELMINTHROOST_TITLE_LOOP", "Helminth Hatchery"); //Molten Gold River
            //LanguageAPI.Add("MAP_HELMINTHROOST_SUBTITLE_LOOP", "A Brother\u2019s Respite");

            LanguageAPI.Add("MAP_HELMINTHROOST_TITLE_LOOP", "Helminth Forge"); //Molten Gold River
            //LanguageAPI.Add("MAP_HELMINTHROOST_SUBTITLE_LOOP", "Another\u2019s Design");
            LanguageAPI.Add("MAP_HELMINTHROOST_SUBTITLE_LOOP", "Another\u2019s Reproof");

            //6
            LanguageAPI.Add("MAP_MOON_TITLE_LOOP" , "Cessation"); //The Spooky
            LanguageAPI.Add("MAP_MOON_SUBTITLE_LOOP", "Dark Side of the moon");

            //
            //
            SetupMisc();

            VariantsDampCave.Setup();
            VariantsHelminthRoost.Setup();

            //LoadStuff();
            //UnusedStages();
        }


        public static void SetupMisc()
        {
            //PostProcessProfile original = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppLocalGoo.asse").WaitForCompletion();


        }

        public static void LoadStuff()
        {
            Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/Common/Skyboxes/matSkybox1.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/Junk/slice1/matSkybox2.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/bazaar/matSkybox4.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/Common/Skyboxes/matSkyboxFoggy.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/arena/matSkyboxArena.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/artifactworld/matSkyboxArtifactWorld.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/golemplains/matSkyboxGolemplainsFoggy.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/snowyforest/matSkyboxSF.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/goolake/matSkyboxGoolake.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/ancientloft/matSkyboxAncientLoft.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSkyboxSP.mat").WaitForCompletion(); //SulfurPools
            Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/frozenwall/matSkyboxFrozenwallNight.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/moon2/matSkyboxMoon.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/goldshores/matSkyboxGoldshores.mat").WaitForCompletion();

            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneGolemplainsTrailer.asset").WaitForCompletion();
        
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


        public static void Loop_1_Golem()
        {
            GameObject Weather = GameObject.Find("/Weather, Golemplains");

            PostProcessVolume process = Weather.transform.GetChild(2).gameObject.GetComponent<PostProcessVolume>();
            PostProcessProfile TrailerWeather = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneGolemplainsTrailer.asset").WaitForCompletion();
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
            //PPSnowy
            //fogEnd 0.5815 0.593 0.6887 1
            //fogMid 0.4085 0.547 0.6415 1
            //fogStart 0.6316 0.7228 0.787 0
            //fogOne 1
            //fogPower 0.7
            //fogZero -0.004
            //skybox 0


            //0.149 0.2858 0.3396 1
            //0.794


            GameObject Weather = GameObject.Find("/HOLDER: Skybox");

            Light Sun = Weather.transform.GetChild(1).GetComponent<Light>();
            Sun.color = new Color(0.6667f, 0.9373f, 0.8f,1f);//0.6667 0.9373 0.9373 1
            Sun.intensity = 0.8f; //0.4f
            //0.2796 0.2745 0.3443 0.5
           
            PostProcessVolume process = SceneInfo.instance.gameObject.GetComponent<PostProcessVolume>();
            PostProcessProfile ORIGINAL = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/DLC2/villagenight/ppSceneLVnight.asset").WaitForCompletion();
            PostProcessProfile VillageNightPP = GameObject.Instantiate(ORIGINAL);
            RampFog new_RampFog = (RampFog)Object.Instantiate(VillageNightPP.settings[0]);
 
            //new_RampFog.fogColorMid.value = new_RampFog.fogColorMid.value.AlphaMultiplied(0.8f);
            new_RampFog.fogColorStart.value = new_RampFog.fogColorStart.value.AlphaMultiplied(0.8f);
            new_RampFog.fogIntensity.value *= 1.2f;

            //0.231

            new_RampFog.skyboxStrength.value *= 0.33f;

            VillageNightPP.settings[0] = new_RampFog;

            process.profile = VillageNightPP;
            process.sharedProfile = VillageNightPP;



            process = Weather.transform.GetChild(2).GetComponent<PostProcessVolume>();
            process.enabled = false;

            //0.231 

            Transform OGAura = Weather.transform.GetChild(8);

            SetAmbientLight newAmbient = SceneInfo.instance.gameObject.AddComponent<SetAmbientLight>();
            newAmbient.setAmbientLightColor = true;
            newAmbient.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            
            
            newAmbient.ambientGroundColor = new Color(0.1407f, 0.2235f, 0.1392f, 0.5f)*1.4f; //0.1407 0.2235 0.1392 0.5
            newAmbient.ambientEquatorColor = new Color(0.0521f, 0.0804f, 0.049f, 0.5f) * 1.4f; //0.0521 0.0804 0.049 0.5
            newAmbient.ambientSkyColor = new Color(0.2796f, 0.3443f, 0.2745f, 0.5f) * 1.4f; //0.2796 0.2745 0.3443 0.5
            /*
            //Based on VillageNight
            newAmbient.ambientSkyColor = new Color(0.652f, 0.782f, 0.741f, 0.8f); //0.6519 0.7788 0.7441 0.8
            newAmbient.ambientEquatorColor = new Color(0.078f, 0.133f, 0.124f, 0.8f); //0.0781 0.1296 0.1272 0.8
            newAmbient.ambientGroundColor = new Color(0.223f, 0.36f, 0.352f, 0.8f); //0.2231 0.3562 0.3562 0.8 
            */
            newAmbient.ambientIntensity = 1;

            newAmbient.setSkyboxMaterial = true;
            newAmbient.skyboxMaterial = Addressables.LoadAssetAsync<Material>(key: "RoR2/Junk/slice1/matSkybox2.mat").WaitForCompletion();
            newAmbient.ApplyLighting();


            /*Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/meridian/matEventClearedVFX1.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/meridian/matEventClearedVFX2.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/meridian/matEventClearedVFX3.mat").WaitForCompletion();
            Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/meridian/matEventClearedVFX4.mat").WaitForCompletion();*/
            GameObject AuraraOg = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/meridian/EventClearedVFX.prefab").WaitForCompletion();
            Mesh AuraraMesh = Addressables.LoadAssetAsync<Mesh>(key: "RoR2/DLC1/snowyforest/mdlSnowyForestAurora.fbx").WaitForCompletion();

            GameObject Aurara_New = GameObject.Instantiate(AuraraOg);
            Aurara_New.transform.GetChild(0).gameObject.SetActive(false);
            Aurara_New.transform.GetChild(1).GetComponent<MeshFilter>().mesh = AuraraMesh;
            Aurara_New.transform.GetChild(2).GetComponent<MeshFilter>().mesh = AuraraMesh;
            Aurara_New.transform.GetChild(3).GetComponent<MeshFilter>().mesh = AuraraMesh;

            //Main

            Aurara_New.transform.localScale = new Vector3(0.22f, 0.22f, 0.22f);
            //Aurara_New.transform.localPosition = new Vector3(-41f, 50f, -155.4f);
            Aurara_New.transform.localPosition = new Vector3(-106.9092f, 50f, -141.4909f);
            Aurara_New.transform.localEulerAngles = new Vector3(0f, 15f, 0f);

            GameObject Aurara_New_2 = GameObject.Instantiate(Aurara_New);
            GameObject Aurara_New_3 = GameObject.Instantiate(Aurara_New);
            Aurara_New_2.transform.SetParent(Aurara_New.transform);
            Aurara_New_3.transform.SetParent(Aurara_New.transform);

            Aurara_New_2.transform.localPosition = new Vector3(4f, -4f, 4f);
            Aurara_New_2.transform.localScale = new Vector3(1f, 1f, 1f);
            Aurara_New_2.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

          
            Aurara_New_3.transform.localPosition = new Vector3(-4f, 4f, -4f);
            Aurara_New_3.transform.localScale = new Vector3(1f, 1f, 1f);
            Aurara_New_3.transform.localEulerAngles = new Vector3(0f, 0f, 0f);


            Aurara_New = GameObject.Instantiate(Aurara_New);
            Aurara_New.transform.localScale = new Vector3(-0.22f, 0.22f, -0.22f);
            //Aurara_New.transform.localPosition = new Vector3(4f, 60f, 0f);
            Aurara_New.transform.localPosition = new Vector3(-204.8727f, 50f, -160.3091f);
            Aurara_New.transform.localEulerAngles = new Vector3(0f, 26f, 0f);

 
            //
            /*
            // Decor
            Aurara_New = GameObject.Instantiate(AuraraOg);
            Aurara_New.transform.localScale = new Vector3(1f, 1f, 1f);
            Aurara_New.transform.localPosition = new Vector3(240f, 240f, 240f);
            Aurara_New.transform.localEulerAngles = new Vector3(0f, 90f, 0f);

            Aurara_New = GameObject.Instantiate(AuraraOg);
            Aurara_New.transform.localScale = new Vector3(-1f, 1f, -1f);
            Aurara_New.transform.localPosition = new Vector3(240f, 240f, 240f);
            Aurara_New.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            */

        }

        public static void Loop_2_Goo()
        {
            //0.4941 0.4471 0.4078 1
            //0.2431 0.2392 0.2745 1


            //GameObject Sun = Weather.transform.GetChild(0).gameObject;
            //Sun.SetActive(false);

            /*SetAmbientLight newAmbient = Weather.AddComponent<SetAmbientLight>();
            newAmbient.setSkyboxMaterial = true;
            newAmbient.skyboxMaterial = Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/bazaar/matSkybox4.mat").WaitForCompletion();
            newAmbient.ApplyLighting();*/
            //
            //
 
            //GameObject tempobj = GameObject.Find("/HOLDER: Misc Props/GooPlane, High");
            GameObject MiscProps = GameObject.Find("/HOLDER: Misc Props");
            GameObject WaterFall = GameObject.Find("/HOLDER: GameplaySpace/mdlGlDam/GL_AqueductPartial/GL_Waterfall");

            BuffDef SlowTar = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/Base/Common/bdClayGoo.asset").WaitForCompletion();

            WaterFall.transform.localPosition = new Vector3(-0.36f, -1f, 0f);
            WaterFall.transform.localScale = new Vector3(1f, 0.9f, 1f);
            WaterFall.transform.GetChild(8).GetComponent<DebuffZone>().buffType = SlowTar;
            WaterFall.transform.GetChild(8).GetComponent<DebuffZone>().buffDuration = 3;
 
            GameObject GooPlaneOldWaterFall = MiscProps.transform.GetChild(2).gameObject;
            GameObject GooPlaneOldWateringHole = MiscProps.transform.GetChild(3).gameObject;

            DebuffZone debuffZone = GooPlaneOldWateringHole.GetComponentInChildren<DebuffZone>();
            debuffZone.buffType = null;
            DebuffZoneFixed debuffZoneReal = debuffZone.gameObject.AddComponent<DebuffZoneFixed>();
            debuffZoneReal.interval = 1;
            debuffZoneReal.buffApplicationEffectPrefab = debuffZone.buffApplicationEffectPrefab;
            debuffZoneReal.buffApplicationSoundString = debuffZone.buffApplicationSoundString;
            debuffZoneReal.buffDuration = 4;
            debuffZoneReal.buffType = SlowTar;

            debuffZone = GooPlaneOldWaterFall.GetComponentInChildren<DebuffZone>();
            debuffZone.buffType = null;
            debuffZoneReal = debuffZone.gameObject.AddComponent<DebuffZoneFixed>();
            debuffZoneReal.interval = 1;
            debuffZoneReal.buffApplicationEffectPrefab = debuffZone.buffApplicationEffectPrefab;
            debuffZoneReal.buffApplicationSoundString = debuffZone.buffApplicationSoundString;
            debuffZoneReal.buffDuration = 4;
            debuffZoneReal.buffType = SlowTar;

            GooPlaneOldWaterFall.transform.localPosition = new Vector3(107.6f, -122.7f, 50.3f);
            GooPlaneOldWaterFall.transform.localScale = new Vector3(7.5579f, 1f,7.8565f);
            GooPlaneOldWaterFall.transform.GetChild(0).localScale = new Vector3(10f, 100f, 10);
            //GooPlaneOldWaterFall.transform.GetChild(0).GetChild(0).localScale = new Vector3(1f, 50f, 1f);


            GooPlaneOldWateringHole.transform.localPosition = new Vector3(164.4f, -83.01f, -221.2f);
            GooPlaneOldWateringHole.transform.localScale = new Vector3(7.467f, 1f, 7.9853f);
            GooPlaneOldWateringHole.transform.GetChild(1).localScale = new Vector3(10f, 100f, 10f);
            //GooPlaneOld2.transform.GetChild(1).GetChild(0).localScale = new Vector3(1f, 50f, 1f);

            GameObject GooPlaneRiver = Object.Instantiate(GooPlaneOldWateringHole, MiscProps.transform.parent);
            GameObject GooPlaneDecor = Object.Instantiate(GooPlaneOldWaterFall, MiscProps.transform.parent);

            GooPlaneRiver.transform.localPosition = new Vector3(270f, -134.4f, 160f);
            GooPlaneRiver.transform.localScale = new Vector3(30f, 1f, 30f);
            GooPlaneRiver.transform.localEulerAngles = new Vector3(0f, 326.1636f, 0f);
            GooPlaneRiver.transform.GetChild(1).localPosition = new Vector3(0f, -0.2f, 0f);
            GooPlaneRiver.name = "GooPlane CentralRiver";
            //GooPlaneRiver.GetComponent<ParticleSystem>()

            GooPlaneDecor.transform.localPosition = new Vector3(360f, -106f, -260f);
            GooPlaneDecor.transform.localScale = new Vector3(15, 1f, 10f);
            GooPlaneDecor.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            GooPlaneDecor.name = "GooPlane Decor";




            //Add some sort of DECAL decor of tar around the rims and edges of the lake and junk

            GameObject VoidDecalOriginal = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidCamp/VoidCamp.prefab").WaitForCompletion().transform.GetChild(4).gameObject;
            Material matClayBossGooDecal = Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/Clay/matClayBossGooDecal.mat").WaitForCompletion();


            GameObject TarDecal = GameObject.Instantiate(VoidDecalOriginal);
            TarDecal.GetComponent<ThreeEyedGames.Decal>().Material = matClayBossGooDecal;
            TarDecal.transform.localPosition = new Vector3(265.3105f, -123.385f, 225.0929f);
            TarDecal.transform.localEulerAngles = new Vector3(0f, 45f, 0f);
            TarDecal.transform.localScale = new Vector3(150f, 40f, 67f);

            TarDecal = GameObject.Instantiate(VoidDecalOriginal);
            TarDecal.GetComponent<ThreeEyedGames.Decal>().Material = matClayBossGooDecal;
            TarDecal.GetComponent<ThreeEyedGames.Decal>().Fade = 2f;
            TarDecal.transform.localPosition = new Vector3(136.7616f, - 135.4982f, 218.1272f);
            //VoidDecal.transform.localEulerAngles = new Vector3(0f, 243.8361f, 340.6543f);
            TarDecal.transform.localRotation = new Quaternion(0.1426f, -0.8367f, -0.0888f, 0.5212f);
            TarDecal.transform.localScale = new Vector3(70.9044f, 86.1103f, 76.1418f);



            #region LIGHTING
            GameObject ApproxCenter = GameObject.Find("HOLDER: Secret Ring Area Content/ApproxCenter");
            GameObject GLUndergroundPPVolume = ApproxCenter.transform.GetChild(4).gameObject;

            GameObject Weather = GameObject.Find("/Weather, Goolake");

            PostProcessProfile original = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneGoolakeInTunnels.asset").WaitForCompletion();
            PostProcessProfile newFog = Object.Instantiate(original);
            RampFog new_RampFog = (RampFog)Object.Instantiate(newFog.settings[0]);

            new_RampFog.fogColorEnd.value = new Color(0.7f, 1f, 0.213f, 1f); //1 0.913 0.2123 1
            new_RampFog.fogColorMid.value = new Color(0.8896f, 0.9151f, 0.3842f, 0.2745f); //0.9151 0.8896 0.3842 0.2745
            //new_RampFog.fogColorStart.value = new Color(0,0,0,0.1f); //0.2803 0.4519 0.566 0
            new_RampFog.fogColorEnd.overrideState = true;

            newFog.settings[0] = new_RampFog;
        
            PostProcessVolume PPWorld = Weather.transform.GetChild(1).GetComponent<PostProcessVolume>();
            PPWorld.sharedProfile = newFog;
            PPWorld.profile = newFog;
            PPWorld.priority = 1;

            Weather.SetActive(false);
            Weather.SetActive(true);


            SceneWeatherController SunReal = SceneInfo.instance.GetComponent<SceneWeatherController>();

            SunReal.initialWeatherParams.sunColor = new Color(0.5f, 0.6f, 0.5f);
            SunReal.initialWeatherParams.sunIntensity = 1f;

            HookLightingIntoPostProcessVolume HookLighting = GLUndergroundPPVolume.GetComponent<HookLightingIntoPostProcessVolume>();
            HookLighting.defaultAmbientColor = new Color(0.4f, 0.5f, 0.45f, 1);
            HookLighting.overrideAmbientColor = new Color(0.213f, 0.24f, 0.333f, 1);
            #endregion
            #region SECRET
            if (Run.instance.IsExpansionEnabled(LoopVariantsMain.DLC2))
            { 
                GameObject Secret = ApproxCenter.transform.GetChild(0).gameObject;

                Secret.transform.GetChild(1).GetComponent<StartEvent>().enabled = false;
                Secret.transform.GetChild(2).GetComponent<StartEvent>().enabled = false;

                Inventory inventory = Secret.transform.GetChild(1).GetComponent<Inventory>();
                inventory.GiveItem(RoR2Content.Items.FireRing);
                inventory.GiveItem(RoR2Content.Items.Clover, 20);
                inventory.GiveItem(RoR2Content.Items.BoostHp, 20);
                inventory.GiveEquipmentString("EliteAurelioniteEquipment");
                inventory = Secret.transform.GetChild(2).GetComponent<Inventory>();
                inventory.GiveItem(RoR2Content.Items.IceRing);
                inventory.GiveItem(RoR2Content.Items.Clover, 20);
                inventory.GiveItem(RoR2Content.Items.BoostHp, 20);
                inventory.GiveEquipmentString("EliteBeadEquipment");
            }
            #endregion

        }
        public static void Loop_2_Swamp()
        {

        }
        public static void Loop_2_Loft()
        {

        }
        
        public static void Loop_3_Frozen()
        {/*
            GameObject TimedChestsHolders = GameObject.Find("/HOLDER: Timed Chests");

            int newTime = Run.instance.stageClearCount * 300;
            GameObject TimedChest = TimedChestsHolders.transform.GetChild(0).GetChild(0).gameObject;
            TimedChest.GetComponent<TimedChestController>().lockTime = newTime;
            TimedChest = TimedChestsHolders.transform.GetChild(1).GetChild(0).gameObject;
            TimedChest.GetComponent<TimedChestController>().lockTime = newTime;
            TimedChest = TimedChestsHolders.transform.GetChild(2).GetChild(0).gameObject;
            TimedChest.GetComponent<TimedChestController>().lockTime = newTime;
            //TimedChest.GetComponentInChildren<RoR2.GenericPickupController>();*/

        }
        public static void Loop_3_Wisp()
        {
            GameObject Weather = GameObject.Find("/Weather, Wispgraveyard");
            Weather.transform.GetChild(2).GetComponent<SetAmbientLight>().ambientIntensity = 1.1f;
            Weather.transform.GetChild(2).GetComponent<SetAmbientLight>().ApplyLighting();
            Weather.SetActive(false);
            //GameObject.Find("/Weather, Eclipse").SetActive(true); //Can not find inactive Objects

            GameObject EclipseWeather = GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/eclipseworld/Weather, Eclipse.prefab").WaitForCompletion());
            EclipseWeather.transform.GetChild(0).GetComponent<UnityEngine.ReflectionProbe>().bakedTexture = Addressables.LoadAssetAsync<Cubemap>(key: "RoR2/Base/wispgraveyard/ReflectionProbe-2.exr").WaitForCompletion();
            EclipseWeather.transform.GetChild(4).gameObject.SetActive(true);

            /*SetAmbientLight Lighting = EclipseWeather.transform.GetChild(2).GetComponent<SetAmbientLight>();
            Lighting.setSkyboxMaterial = false;
            Lighting.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            Lighting.ApplyLighting();*/


        }
        public static void Loop_3_Sulfur()
        {

        }
        
        public static void Loop_4_Ship()
        {

        }
        public static void Loop_4_Jungle()
        {
            GameObject Weather = GameObject.Find("/HOLDER: Weather Set 1");

            Light TheSun = Weather.transform.GetChild(0).gameObject.GetComponent<Light>();
            TheSun.color = new Color(0.8863f, 0.7255f, 0.5647f, 1);
            TheSun.intensity = 1f;

            SetAmbientLight newAmbient = Weather.AddComponent<SetAmbientLight>();
            newAmbient.setAmbientLightColor = true;
            newAmbient.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            newAmbient.ambientSkyColor = new Color(0.5101f, 0.2622f, 0.3133f, 0.8f);        
            newAmbient.ambientGroundColor = new Color(0.3562f, 0.2231f,0.2512f, 0.8f);
            newAmbient.ambientEquatorColor = new Color(0.1272f, 0.0799f, 0.0799f, 0.8f);
            newAmbient.ambientIntensity = 1;

            newAmbient.setSkyboxMaterial = true;
            newAmbient.skyboxMaterial = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallSkybox.mat").WaitForCompletion();
            newAmbient.ApplyLighting();

            Weather.transform.GetChild(3).GetComponent<PostProcessVolume>().profile = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/DLC2/habitatfall/ppSceneBHFall.asset").WaitForCompletion();

            Material matBHFallGrassGlow = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallGrassGlow.mat").WaitForCompletion());
            Material matBHFallDistantTree = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDistantTree.mat").WaitForCompletion());
            Material matBHFallShurb = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShurb.mat").WaitForCompletion());
            Material matBHFallClouds = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallClouds.mat").WaitForCompletion());
            Material matBHFallDistantMountainClouds = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDistantMountainClouds.mat").WaitForCompletion());
            Material matBHFallDomeTrim = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDomeTrim.mat").WaitForCompletion());
            Material matBHFallEnvfxLeaves = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallEnvfxLeaves.mat").WaitForCompletion());
            Material matBHFallFlower = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallFlower.mat").WaitForCompletion());
            Material matBHFallGodray = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallGodray.mat").WaitForCompletion());
            Material matBHFallHiveBase = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallHiveBase.mat").WaitForCompletion());
            Material matBHFallHiveBubble = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallHiveBubble.mat").WaitForCompletion());
            Material matBHFallHiveDecal = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallHiveDecal.mat").WaitForCompletion());
            Material matBHFallPebble = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallPebble.mat").WaitForCompletion());
            Material matBHFallPlatformSimple = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallPlatformSimple.mat").WaitForCompletion());
            Material matBHFallPlatformTerrain = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallPlatformTerrain.mat").WaitForCompletion());
            Material matBHFallShroomDrips = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShroomDrips.mat").WaitForCompletion());
            Material matBHFallShroomPath = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShroomPath.mat").WaitForCompletion());
            Material matBHFallShroomTunnel = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShroomTunnel.mat").WaitForCompletion());
            Material matBHFallSilhouette = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallSilhouette.mat").WaitForCompletion());
            Material matBHFallStatueBeam = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallStatueBeam.mat").WaitForCompletion());
            Material matBHFallStatueCrystals = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallStatueCrystals.mat").WaitForCompletion());
            Material matBHFallTempleTrim = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallTempleTrim.mat").WaitForCompletion());
            Material matBHFallTerrainVines = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallTerrainVines.mat").WaitForCompletion());
           



            MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer renderer in meshList)
            {
                //var meshBase = renderer.gameObject;
                //var meshParent = meshBase.transform.parent;
                if (renderer.sharedMaterial)
                {
                    //Debug.Log(renderer.sharedMaterial);
                    switch (renderer.sharedMaterial.name)
                    {
                        case "Material_2_LOD0":
                        case "Material_2_LOD1":
                        case "Material_2_LOD2":
                            renderer.materials[1] = matBHFallEnvfxLeaves;
                            break;
                        case "RJShroomFoliage_LOD0":
                            break;
                        case "RJTowerTreeFoliage":
                            renderer.sharedMaterial = matBHFallEnvfxLeaves;
                            break;
                        case "RJTreeBigFoliage":
                            break;
                        case "RJTreeBigFoliage_LOD0":
                            break;
                        case "matRootJungleSpore":
                            break;
                        case "matCliff1VeryMossy":
                            break;
                        case "matRJFogFloor":
                            break;
                        case "matRJLeaf_Blue":
                            renderer.sharedMaterial = matBHFallEnvfxLeaves;
                            break;
                        case "matRJLeaf_Green":
                            renderer.sharedMaterial = matBHFallPebble;
                            break;
                        case "matRJMossPatch1":
                            renderer.sharedMaterial = matBHFallShroomTunnel;
                            break;
                        case "matRJMossPatch2":
                            renderer.sharedMaterial = matBHFallShroomTunnel;
                            break;
                        case "matRJMossPatchLarge":
                            renderer.sharedMaterial = matBHFallShroomTunnel;
                            break;
                        case "matRJMossPatchTriplanar":
                            break;
                        case "matRJPebble":
                            renderer.sharedMaterial = matBHFallEnvfxLeaves;
                            break;
                        case "matRJRock":
                            renderer.sharedMaterial = matBHFallPebble;
                            break;
                        case "matRJSandstone":
                            renderer.sharedMaterial = matBHFallPebble;
                            break;
                        case "matRJShroomBig":
                            renderer.sharedMaterial = matBHFallShroomTunnel;
                            break;
                        case "matRJShroomBounce":
                            break;
                        case "matRJShroomShelf":
                            renderer.sharedMaterial = matBHFallShroomPath;
                            break;
                        case "matRJShroomSmall":
                            renderer.sharedMaterial = matBHFallShroomDrips;
                            break;
                        case "matRJTemple":
                            renderer.sharedMaterial = matBHFallTempleTrim;
                            break;
                        case "matRJTerrain":
                            renderer.sharedMaterial = matBHFallPlatformTerrain;
                            break;
                        case "matRJTerrain2":
                            renderer.sharedMaterial = matBHFallPlatformTerrain;
                            break;
                        case "matRJTree":
                            renderer.sharedMaterial = matBHFallTerrainVines;
                            break;
                        case "matRJTriangle":
                            renderer.sharedMaterial = matBHFallPlatformSimple;
                            break;

                    }

                }
            }
        }

        public static void Loop_5_Sky()
        {

        }



        public class DebuffZoneFixed : MonoBehaviour
        {
            private void Awake()
            {
            }

            private void OnTriggerEnter(Collider other)
            {
                if (NetworkServer.active)
                {
                    if (!this.buffType)
                    {
                        return;
                    }
                    CharacterBody component = other.GetComponent<CharacterBody>();
                    if (component)
                    {
                        component.AddTimedBuff(this.buffType.buffIndex, this.buffDuration);
                        Util.PlaySound(this.buffApplicationSoundString, component.gameObject);
                        if (this.buffApplicationEffectPrefab)
                        {
                            EffectManager.SpawnEffect(this.buffApplicationEffectPrefab, new EffectData
                            {
                                origin = component.mainHurtBox.transform.position,
                                scale = component.radius
                            }, true);
                        }
                    }
                }
            }

            private void OnTriggerStay(Collider other)
            {
                if (NetworkServer.active)
                {
                    if (!this.buffType)
                    {
                        return;
                    }
                    this.buffTimer -= Time.fixedDeltaTime;
                    if (this.buffTimer <= 0f)
                    {
                        this.buffTimer = this.interval;
                        CharacterBody component = other.GetComponent<CharacterBody>();
                        if (component)
                        {
                            component.AddTimedBuff(this.buffType.buffIndex, this.buffDuration);
                        }
                    }
                }
            }
            public float buffTimer;
            public float interval;

            [Tooltip("The buff type to grant")]
            public BuffDef buffType;

            [Tooltip("The buff duration")]
            public float buffDuration;

            public string buffApplicationSoundString;

            public GameObject buffApplicationEffectPrefab;
        }
    }
}