using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants_1_SnowyForest : Variant_Base
    {
        public static void Setup()
        {
        }

        public static void LoopWeather()
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
            Sun.color = new Color(0.6667f, 0.9373f, 0.8f, 1f);//0.6667 0.9373 0.9373 1
            Sun.intensity = 0.7f; //0.4f
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


            newAmbient.ambientGroundColor = new Color(0.1407f, 0.2235f, 0.1392f, 0.5f) * 1.4f; //0.1407 0.2235 0.1392 0.5
            newAmbient.ambientEquatorColor = new Color(0.0521f, 0.0804f, 0.049f, 0.5f) * 1.4f; //0.0521 0.0804 0.049 0.5
            newAmbient.ambientSkyColor = new Color(0.2796f, 0.3443f, 0.2745f, 0.5f) * 1.4f; //0.2796 0.2745 0.3443 0.5
            /*
            //Based on VillageNight
            newAmbient.ambientSkyColor = new Color(0.652f, 0.782f, 0.741f, 0.8f); //0.6519 0.7788 0.7441 0.8
            newAmbient.ambientEquatorColor = new Color(0.078f, 0.133f, 0.124f, 0.8f); //0.0781 0.1296 0.1272 0.8
            newAmbient.ambientGroundColor = new Color(0.223f, 0.36f, 0.352f, 0.8f); //0.2231 0.3562 0.3562 0.8 
            */
            newAmbient.ambientIntensity = 1.2f;

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

        public static void AddVariantMonsters(DirectorCardCategorySelection dccs)
        {
            if (ShouldAddLoopEnemies(dccs) == false)
            {
                return;
            }
            DirectorCard Loop_GreaterWisp = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/GreaterWisp/cscGreaterWisp.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            dccs.AddCard(1, Loop_GreaterWisp);
        }
    }
}