using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using On.RoR2.Networking;

namespace LoopVariants
{
    public class Main_Variants
    {
        

        public static void Start()
        {

            //1
            //Sunset
            LanguageAPI.Add("MAP_GOLEMPLAINS_TITLE_LOOP", "Sunset Prairie"); //Titanic Plains
            LanguageAPI.Add("MAP_GOLEMPLAINS_SUBTITLE_LOOP", "Grounds Alpha"); //Ground Zero

            //LanguageAPI.Add("MAP_BLACKBEACH_TITLE_LOOP", "Distant Roost"); //Stormy?
            //LanguageAPI.Add("MAP_BLACKBEACH_SUBTITLE_LOOP", "Ground Zero"); //Ground Zero

            //Aurorare Borelasis
            LanguageAPI.Add("MAP_SNOWYFOREST_TITLE_LOOP", "Aurora Woods"); //Siphoned Forest
            LanguageAPI.Add("MAP_SNOWYFOREST_SUBTITLE_LOOP", "Astral Light"); //Ground Zero
            //
            //2
            //Tar Flood
            LanguageAPI.Add("MAP_GOOLAKE_TITLE_LOOP", "Abandoned Tarpit"); //Abandoned Aqueduct
            LanguageAPI.Add("MAP_GOOLAKE_SUBTITLE_LOOP", "Flowing Endlessly"); //Origin of Tar

            //Rainy? Foggy? Smth with Nkuhana?
            LanguageAPI.Add("MAP_FOGGYSWAMP_TITLE_LOOP", "Wetland Moor");  //Wetland Aspect
            LanguageAPI.Add("MAP_FOGGYSWAMP_SUBTITLE_LOOP", "Clouded Minds"); //Rehabilitation Zone

            //Provi fan club tea time
            LanguageAPI.Add("MAP_ANCIENTLOFT_TITLE_LOOP", "Aphelian Sanctuary"); 
            LanguageAPI.Add("MAP_ANCIENTLOFT_SUBTITLE_LOOP", "Cleansing Center");
            //
            //3

            LanguageAPI.Add("MAP_FROZENWALL_TITLE_LOOP", "Rallypoint Delta");
            LanguageAPI.Add("MAP_FROZENWALL_SUBTITLE_LOOP", "Failed Rescue"); //"'Contact Light' Survivor Camp"

            //Dusk
            LanguageAPI.Add("MAP_WISPGRAVEYARD_TITLE_LOOP", "Blighted Acres"); //Scorched Acres
            LanguageAPI.Add("MAP_WISPGRAVEYARD_SUBTITLE_LOOP", "Protective Garden"); //Wisp Installation

            //Sulfur Blue Fire? Sulfur Red?
            LanguageAPI.Add("MAP_SULFURPOOLS_TITLE_LOOP", "Sulfur Flames");  //"Sulfur Pools"
            LanguageAPI.Add("MAP_SULFURPOOLS_SUBTITLE_LOOP", "Blazing Blue"); //"Pungent Spring"
            //LanguageAPI.Add("MAP_SULFURPOOLS_SUBTITLE_LOOP", "Api Biru");
            //LanguageAPI.Add("MAP_SULFURPOOLS_SUBTITLE_LOOP", "Ijen\u2019s Wrath");

            //
            //4
            //Crimson Abyss? Crimson Depths? Red Plane?
            LanguageAPI.Add("MAP_DAMPCAVE_TITLE_LOOP", "Scarlet Abyss"); //Abyssal Depths
            LanguageAPI.Add("MAP_DAMPCAVE_SUBTITLE_LOOP", "Dimensional Activity"); //Tectonic Relics

            LanguageAPI.Add("MAP_SHIPGRAVEYARD_TITLE_LOOP", "Siren's Call");//Siren's Call
            LanguageAPI.Add("MAP_SHIPGRAVEYARD_SUBTITLE_LOOP", "Ship Graveyard");//Ship Graveyard

            //Copy Golden Dieback
            LanguageAPI.Add("MAP_ROOTJUNGLE_TITLE_LOOP", "Renewing Grove");//"Sundered Grove"
            LanguageAPI.Add("MAP_ROOTJUNGLE_SUBTITLE_LOOP", "Fruits of Labor");//"Dormant Locus"

            //
            //5
            //Hostile Paradise? Use that somewhere
            LanguageAPI.Add("MAP_SKYMEADOW_TITLE_LOOP", "Sky Meadow");// "Sky Meadow"
            LanguageAPI.Add("MAP_SKYMEADOW_SUBTITLE_LOOP", "Sprite Fields"); //"Sprite Fields"

            //Molten Gold River
            LanguageAPI.Add("MAP_HELMINTHROOST_TITLE_LOOP", "Helminth Forge");  //"Helminth Hatchery")
            LanguageAPI.Add("MAP_HELMINTHROOST_SUBTITLE_LOOP", "Another\u2019s Reproof"); //"A Brother\u2019s Respite"

            //6
            LanguageAPI.Add("MAP_MOON_TITLE_LOOP" , "Cessation"); //The Spooky
            LanguageAPI.Add("MAP_MOON_SUBTITLE_LOOP", "Dark Side of the moon");

            //
            //
            Variants_1_GolemPlains.Setup();
            Variants_1_BlackBeach.Setup();
            Variants_1_SnowyForest.Setup();
            //
            Variants_2_Goolake.Setup();
            Variants_2_FoggySwamp.Setup();
            Variants_2_AncientLoft.Setup();
            Variants_2_LemurianTemple.Setup();
            //
            Variants_3_FrozenWall.Setup();
            Variants_3_WispGraveyard.Setup();
            Variants_3_Sulfur.Setup();
            //
            Variants_4_DampCaveSimpleAbyss.Setup();
            Variants_4_ShipGraveyard.Setup();
            Variants_4_RootJungle.Setup();
            //
            Variants_5_SkyMeadow.Setup();
            Variants_5_HelminthRoost.Setup();
            //
            Variants_6_Moon.Setup();
            //
         
            if (WConfig.Stage_1_LakesVillageNight.Value)
            {
                Nerf_OfficialStage1LoopSpawnpools();
            }

            SceneDef lakes = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC2/lakes/lakes.asset").WaitForCompletion();
            SceneDef village = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC2/village/village.asset").WaitForCompletion();
            SceneDef habitat = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC2/habitat/habitat.asset").WaitForCompletion();
            LoopVariantsMain.loopSceneDefToNon.Add("lakesnight", lakes);
            LoopVariantsMain.loopSceneDefToNon.Add("villagenight", village);
            LoopVariantsMain.loopSceneDefToNon.Add("habitatfall", habitat);

            if (WConfig.MultiplayerTesting.Value)
            {
                On.RoR2.Networking.ServerAuthManager.HandleSetClientAuth += ServerAuthManager_HandleSetClientAuth;
                UnusedStages();
            }  
        }

        

        public static void Nerf_OfficialStage1LoopSpawnpools()
        {
            DirectorCardCategorySelection dccsLakesnightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillageNightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillageNightMonsters.asset").WaitForCompletion();

            try
            {
                dccsLakesnightMonsters.categories[0].cards[1].minimumStageCompletions = 1; //Grandparent
                dccsLakesnightMonsters.categories[0].cards[4].minimumStageCompletions = 1; //Imp Boss
                dccsLakesnightMonsters.categories[1].cards[2].minimumStageCompletions = 1; //Elder Lemurian
                dccsLakesnightMonsters.categories[1].cards[3].minimumStageCompletions = 1; //Void Reaver
                //dccsLakesnightMonsters.categories[2].cards[2].minimumStageCompletions = 1; //Imp

                dccsVillageNightMonsters.categories[1].cards[2].minimumStageCompletions = 1; //Elder Lemurian
                //dccsVillageNightMonsters.categories[2].cards[1].minimumStageCompletions = 1; //Jellyfish
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Some dude edited LakesVillageNight");
                Debug.LogWarning(e);
            }

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




        private static void ServerAuthManager_HandleSetClientAuth(ServerAuthManager.orig_HandleSetClientAuth orig, NetworkMessage netMsg)
        {
        }

    }
}