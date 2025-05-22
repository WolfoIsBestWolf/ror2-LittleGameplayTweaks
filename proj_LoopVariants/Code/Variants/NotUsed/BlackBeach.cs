using RoR2;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants_1_BlackBeach
    {
        public static void Setup()
        {
        }

        public static void LoopWeather()
        {
            SceneInfo.instance.GetComponent<PostProcessVolume>().profile = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneShipgraveyard.asset").WaitForCompletion();
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