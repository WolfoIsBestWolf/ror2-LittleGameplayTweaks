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
        public static List<string> ExistingVariants = new List<string>() { "wispgraveyard", "golemplains", "goolake", "dampcavesimple", "snowyforest", "helminthroost", "foggyswamp" };


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

            //LanguageAPI.Add("MAP_FOGGYSWAMP_TITLE_LOOP", "Wetland Aspect"); 
            //LanguageAPI.Add("MAP_FOGGYSWAMP_SUBTITLE_LOOP", "Rehabilitation Zone");
            LanguageAPI.Add("MAP_FOGGYSWAMP_TITLE_LOOP", "Wetland Moor"); //Rainy? Foggy? Smth with Nkuhana?
            LanguageAPI.Add("MAP_FOGGYSWAMP_SUBTITLE_LOOP", "Clouded Minds");


            LanguageAPI.Add("MAP_ANCIENTLOFT_TITLE_LOOP", "Aphelian Sanctuary"); //Provi fan club tea time
            LanguageAPI.Add("MAP_ANCIENTLOFT_SUBTITLE_LOOP", "Cleansing Center");
            //
            //3
            LanguageAPI.Add("MAP_FROZENWALL_TITLE_LOOP", "Rallypoint Delta");
            //LanguageAPI.Add("MAP_FROZENWALL_SUBTITLE_LOOP", "'Contact Light' Survivor Camp");
            LanguageAPI.Add("MAP_FROZENWALL_SUBTITLE_LOOP", "Failed Rescue");

            LanguageAPI.Add("MAP_WISPGRAVEYARD_TITLE_LOOP", "Blighted Acres"); //Dusk
            LanguageAPI.Add("MAP_WISPGRAVEYARD_SUBTITLE_LOOP", "Protective Garden");

            /* LanguageAPI.Add("MAP_SULFURPOOLS_TITLE_LOOP", "Sulfur Pools");  //Sulfur Blue Fire? Sulfur Red?
            LanguageAPI.Add("MAP_SULFURPOOLS_SUBTITLE_LOOP", "Pungent Spring");*/

            LanguageAPI.Add("MAP_SULFURPOOLS_TITLE_LOOP", "Sulfur Flames");  //Sulfur Blue Fire? Sulfur Red?
            LanguageAPI.Add("MAP_SULFURPOOLS_SUBTITLE_LOOP", "Blazing Blue");

            //
            //4
            //Crimson Abyss? Crimson Depths?
            LanguageAPI.Add("MAP_DAMPCAVE_TITLE_LOOP", "Scarlet Abyss"); //Red Plane?
            LanguageAPI.Add("MAP_DAMPCAVE_SUBTITLE_LOOP", "Dimensional Activity");

            LanguageAPI.Add("MAP_SHIPGRAVEYARD_TITLE_LOOP", "Siren's Call");
            LanguageAPI.Add("MAP_SHIPGRAVEYARD_SUBTITLE_LOOP", "Ship Graveyard");

            /*LanguageAPI.Add("MAP_ROOTJUNGLE_TITLE_LOOP", "Sundered Grove"); //Copy Golden Dieback
            LanguageAPI.Add("MAP_ROOTJUNGLE_SUBTITLE_LOOP", "Dormant Locus");*/

            LanguageAPI.Add("MAP_ROOTJUNGLE_TITLE_LOOP", "Renewing Grove"); //Copy Golden Dieback
            LanguageAPI.Add("MAP_ROOTJUNGLE_SUBTITLE_LOOP", "Living Anew");

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
            if (WConfig.MultiplayerTesting.Value)
            {
                On.RoR2.Networking.ServerAuthManager.HandleSetClientAuth += ServerAuthManager_HandleSetClientAuth;
                UnusedStages();
            }  
        }

        public static void RemoveVariantNames()
        {

            if (!WConfig.Stage_1_Golem.Value)
            {
                ExistingVariants.Remove("golemplains");
            }
            if (!WConfig.Stage_1_Snow.Value)
            {
                ExistingVariants.Remove("snowyforest");
            }
            //
            if (!WConfig.Stage_2_Goolake.Value)
            {
                ExistingVariants.Remove("goolake");
            }
            if (!WConfig.Stage_2_Swamp.Value)
            {
                ExistingVariants.Remove("foggyswamp");
            }
            //
            if (!WConfig.Stage_3_Wisp.Value)
            {
                ExistingVariants.Remove("wispgraveyard");
            }
            if (!WConfig.Stage_3_Sulfur.Value)
            {
                ExistingVariants.Remove("sulfurpools");
            }
            //
            if (!WConfig.Stage_4_Damp_Abyss.Value)
            {
                ExistingVariants.Remove("dampcavesimple");
            }
            if (!WConfig.Stage_4_Root_Jungle.Value)
            {
                ExistingVariants.Remove("rootjungle");
            }
            //
            if (!WConfig.Stage_5_Helminth.Value)
            {
                ExistingVariants.Remove("helminthroost");
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