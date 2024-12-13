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
            //Trailer Weather
            Variants_1_GolemPlains.Setup();
            //
            Variants_1_BlackBeach.Setup();
            //Aurora Borealis
            Variants_1_SnowyForest.Setup();

            //FUCK you sets your verdant falls on fire.

            //2
            //Overflowing Tar
            Variants_2_Goolake.Setup();
            //Overflowing Rain + Fog
            Variants_2_FoggySwamp.Setup();
            //Eclipse but like more eclipsed idk
            Variants_2_AncientLoft.Setup();
            //Blessed? Fall Tree too
            Variants_2_LemurianTemple.Setup();

            //3
            //DayTime like SnowyForest? Ice replacing Water?
            Variants_3_FrozenWall.Setup();
            //Eclipse Weather Test / Dusk
            Variants_3_WispGraveyard.Setup();
            //Sulfur Blue Fire
            Variants_3_Sulfur.Setup();

            //4
            //Red Plane-like
            Variants_4_DampCaveSimpleAbyss.Setup();
            //
            Variants_4_ShipGraveyard.Setup();
            //Copy Golden Dieback
            Variants_4_RootJungle.Setup();

            //5
            //Hostile Paradise? Use that somewhere
            Variants_5_SkyMeadow.Setup();
            //Molten Gold River
            Variants_5_HelminthRoost.Setup();

            //6
            //The Spooky
            Variants_6_Meridian.Setup();
            Variants_6_Moon.Setup();
            //

            if (WConfig.Stage_1_LakesVillageNight.Value)
            {
                LakesNightVillageNight.EditDccs();
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


            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneBlackbeach_Eclipse.asset").WaitForCompletion();
            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneBlackbeach.asset").WaitForCompletion();
            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneDampcave.asset").WaitForCompletion();
            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneEclipseClose.asset").WaitForCompletion();
            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneEclipseStandard.asset").WaitForCompletion();
            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneGoldshores.asset").WaitForCompletion();
            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneGolemplains.asset").WaitForCompletion();
            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneMoonFoggy.asset").WaitForCompletion();
            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneWispGraveyardSoot.asset").WaitForCompletion();
            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneRootJungleClear.asset").WaitForCompletion();
            Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneRootJungleRain.asset").WaitForCompletion();


        }


        public static int FindSpawnCard(DirectorCard[] insert, string LookingFor)
        {
            for (int i = 0; i < insert.Length; i++)
            {
                if (insert[i].spawnCard.name.EndsWith(LookingFor))
                {
                    //Debug.Log("Found " + LookingFor);
                    return i;
                }
            }
            Debug.LogWarning("Couldn't find " + LookingFor);
            return -1;
        }



        private static void ServerAuthManager_HandleSetClientAuth(ServerAuthManager.orig_HandleSetClientAuth orig, NetworkMessage netMsg)
        {
        }

    }
}