using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LoopVariants
{
    public class Variants_3_WispGraveyard : Variant_Base
    {
        public static void Setup()
        {
        }

        public static void LoopWeather()
        {
            GameObject Weather = GameObject.Find("/Weather, Wispgraveyard");
            Weather.transform.GetChild(2).GetComponent<SetAmbientLight>().ambientIntensity = 1.1f;
            Weather.transform.GetChild(2).GetComponent<SetAmbientLight>().ApplyLighting();
            Weather.SetActive(false);
            //GameObject.Find("/Weather, Eclipse").SetActive(true); //Can not find inactive Objects

            GameObject EclipseWeather = GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/eclipseworld/Weather, Eclipse.prefab").WaitForCompletion());
            EclipseWeather.transform.GetChild(0).GetComponent<UnityEngine.ReflectionProbe>().bakedTexture = Addressables.LoadAssetAsync<Cubemap>(key: "RoR2/Base/wispgraveyard/ReflectionProbe-2.exr").WaitForCompletion();
            EclipseWeather.transform.GetChild(1).GetComponent<UnityEngine.Light>().intensity = 0.7f;
            EclipseWeather.transform.GetChild(1).GetComponent<UnityEngine.Light>().shadowStrength = 0.5f;
            EclipseWeather.transform.GetChild(4).gameObject.SetActive(true);

            /*SetAmbientLight Lighting = EclipseWeather.transform.GetChild(2).GetComponent<SetAmbientLight>();
            Lighting.setSkyboxMaterial = false;
            Lighting.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            Lighting.ApplyLighting();*/

        }

        public static void AddVariantMonsters(DirectorCardCategorySelection dccs)
        {
            if (ShouldAddLoopEnemies(dccs) == false)
            {
                return;
            }

            DirectorCard DC_Child = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/Child/cscChild.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard Loop_GreaterWisp = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/GreaterWisp/cscGreaterWisp.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            dccs.AddCard(2, DC_Child);
            dccs.AddCard(1, Loop_GreaterWisp);

        }
    }
}