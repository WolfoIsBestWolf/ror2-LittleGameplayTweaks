using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

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



    }
}