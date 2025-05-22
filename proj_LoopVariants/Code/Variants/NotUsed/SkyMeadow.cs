using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LoopVariants
{
    public class Variants_5_SkyMeadow : Variant_Base
    {
        public static void Setup()
        {
        }

        public static void LoopWeather()
        {
            GameObject Weather = GameObject.Find("/HOLDER: Weather Set 1");
            Weather.SetActive(false);

            GameObject WeatherNight = GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/lakesnight/Weather, Lakesnight.prefab").WaitForCompletion());


            Light Sun = WeatherNight.transform.GetChild(1).GetComponent<Light>();
            Sun.color = new Color(0.9149f, 0.5961f, 0.9333f, 1);//0.5961 0.9149 0.9333 1
            Sun.shadowStrength *= 0.5f;



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