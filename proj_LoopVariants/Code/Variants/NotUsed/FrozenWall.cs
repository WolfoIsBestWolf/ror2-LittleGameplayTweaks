using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants_3_FrozenWall
    {
        public static PostProcessProfile ppFrozenWallDay;

        public static void Setup()
        {
            ppFrozenWallDay = Object.Instantiate(Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneFrozenwallNight.asset").WaitForCompletion());
            PostProcessProfile ppSceneSnowyForest = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/DLC1/snowyforest/ppSceneSnowyForest.asset").WaitForCompletion();
            ppFrozenWallDay = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/DLC1/snowyforest/ppSceneSnowyForest.asset").WaitForCompletion();

            //ppFrozenWallDay.settings[0] = ppSceneSnowyForest.settings[0];
        }

        public static void LoopWeather()
        {
            PostProcessVolume PPVol = SceneInfo.instance.gameObject.GetComponent<PostProcessVolume>();
            PPVol.profile = ppFrozenWallDay;

            GameObject Skybox = GameObject.Find("/HOLDER: Skybox");
            Transform Water = Skybox.transform.GetChild(0);


        }

        public static void AddVariantMonsters(DirectorCardCategorySelection dccs)
        {
            if (dccs == null || !LoopVariantsMain.AddMonsters)
            {
                return;
            }
        }
    }
}