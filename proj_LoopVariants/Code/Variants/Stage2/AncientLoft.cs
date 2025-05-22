using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LoopVariants
{
    public class Variants_2_AncientLoft : Variant_Base
    {
        public static Material matAncientLoft_SunCloud;
        public static Material matAncientLoft_CloudFloor;
        public static Material matAncientLoftDeepFog;
        public static Material matAncientLoft_Cloud;
        public static Material matAncientLoft_Water;
        public static Material matAncientLoftEndlessHole;
        public static Material matEclipseMoon;


        public static void Setup()
        {

            matAncientLoft_CloudFloor = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/ancientloft/matAncientLoft_CloudFloor.mat").WaitForCompletion());
            matAncientLoft_CloudFloor.SetColor("_TintColor", new Color(0.03f, 0.07f, 0.16f, 1f)); //0.1604 0.0604 0.025 1

            matAncientLoftDeepFog = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/ancientloft/matAncientLoftDeepFog.mat").WaitForCompletion());
            matAncientLoftDeepFog.SetColor("_TintColor", new Color(0.03f, 0.05f, 0.07f, 1f)); //0.1226 0.0746 0.0318 1

            matAncientLoft_Cloud = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/ancientloft/matAncientLoft_Cloud.mat").WaitForCompletion());
            matAncientLoft_Cloud.SetColor("_TintColor", new Color(0.07f, 0.18f, 0.32f, 1f)); //0.3774 0.1821 0.0765 1

            matAncientLoft_SunCloud = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/ancientloft/matAncientLoft_SunCloud.mat").WaitForCompletion());
            matAncientLoft_SunCloud.SetTexture("_RemapTex", matAncientLoft_CloudFloor.GetTexture("_RemapTex"));//  texRampArtifactShellSoft
            matAncientLoft_SunCloud.SetColor("_TintColor", new Color(0f, 0.4f, 0.5f, 0.4f)); //0.5566 0 0.014 1

            matAncientLoft_Water = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/ancientloft/matAncientLoft_Water.mat").WaitForCompletion());
            matAncientLoft_Water.SetTexture("_Cube", null);

            matAncientLoftEndlessHole = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/ancientloft/matAncientLoftEndlessHole.mat").WaitForCompletion());
            matAncientLoftEndlessHole.SetColor("_TintColor", new Color(0.03f, 0.05f, 0.08f, 1f)); //0.0943 0.0459 0.012 1

            matEclipseMoon = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/eclipseworld/matEclipseMoon.mat").WaitForCompletion());

        }

        public static void LoopWeather()
        {
            #region Weather

            GameObject AL_Sun = GameObject.Find("/HOLDER: Terrain/AL_Sun");
            AL_Sun.SetActive(false);

            GameObject Weather = GameObject.Find("/Weather, AncientLoft");
            Weather.transform.GetChild(1).GetComponent<Light>().enabled = false;
            Weather.transform.GetChild(2).gameObject.SetActive(false);


            GameObject EclipseWeatherOG = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/eclipseworld/Weather, Eclipse.prefab").WaitForCompletion();
            GameObject Eclipse = GameObject.Instantiate(EclipseWeatherOG.transform.GetChild(3).GetChild(2).gameObject);
            GameObject EclipseWeather = GameObject.Instantiate(EclipseWeatherOG);

            Light Sun = EclipseWeather.transform.GetChild(1).GetComponent<Light>();
            Sun.shadowStrength = 0.5f;

            SetAmbientLight Ambient = EclipseWeather.transform.GetChild(2).GetComponent<SetAmbientLight>();
            Ambient.ambientIntensity = 0.6f;
            Ambient.ApplyLighting();


            Eclipse.gameObject.SetActive(true);
            Eclipse.transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 0);
            //Eclipse.transform.GetChild(0).localScale *= 1.04f;
            Eclipse.transform.localPosition = new Vector3(-1800f, 360f, 310f);//13.8 34.7 1.6
            Eclipse.transform.localEulerAngles = new Vector3(0f, 100f, 0);//2.5 331.288 6.351
            Eclipse.transform.localScale = new Vector3(420f, 420f, 420);//10.7663 10.7663 10.7663

            //LightCausticsIntensity
            Eclipse.GetComponent<MeshRenderer>().material = matEclipseMoon;


            GameObject.Instantiate(Weather.transform.GetChild(1).gameObject);


            #endregion
            GameObject Water = GameObject.Find("/HOLDER: Water");
            Water.transform.GetChild(1).GetComponent<MeshRenderer>().material = matAncientLoft_Water;

            GameObject Holes = GameObject.Find("/HOLDER: Terrain/Endless Hole Illusion Card");
            Holes.GetComponent<MeshRenderer>().material = matAncientLoftEndlessHole;
            Holes.transform.GetChild(0).GetComponent<MeshRenderer>().material = matAncientLoftEndlessHole;
            Holes.transform.GetChild(1).GetComponent<MeshRenderer>().material = matAncientLoftEndlessHole;
            Holes.transform.GetChild(2).GetComponent<MeshRenderer>().material = matAncientLoftEndlessHole;

            GameObject SkyboxFog = GameObject.Find("/HOLDER: Cards");

            Transform cloud = SkyboxFog.transform.GetChild(3);
            Vector3 vec3 = cloud.localScale;
            vec3.z *= -1f;
            cloud.localScale = vec3;

            SkyboxFog.transform.GetChild(4).gameObject.SetActive(false);
            SkyboxFog.transform.GetChild(5).gameObject.SetActive(false);


            MeshRenderer[] rendererList = SkyboxFog.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in rendererList)
            {
                switch (renderer.material.name)
                {
                    case "matAncientLoft_SunCloud (Instance)":
                        renderer.material = matAncientLoft_SunCloud;
                        break;
                    case "matAncientLoft_CloudFloor (Instance)":
                        renderer.material = matAncientLoft_CloudFloor;
                        break;
                    case "matAncientLoftDeepFog (Instance)":
                        renderer.material = matAncientLoftDeepFog;
                        break;
                }
            }

            Weather.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().material = matAncientLoft_Cloud;
        }

        public static void AddVariantMonsters(DirectorCardCategorySelection dccs)
        {
            if (ShouldAddLoopEnemies(dccs) == false)
            {
                return;
            }
            DirectorCard LoopLunarExploder = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarGolem = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarWisp = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            dccs.AddCard(2, LoopLunarExploder);
            dccs.AddCard(0, LoopLunarGolem);
            dccs.AddCard(0, LoopLunarWisp);
        }
    }
}