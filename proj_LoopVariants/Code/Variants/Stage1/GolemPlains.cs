using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants_1_GolemPlains : Variant_Base
    {
        public static void Setup()
        {
        }

        public static void LoopWeather()
        {

            GameObject Weather = GameObject.Find("/Weather, Golemplains");

            PostProcessVolume process = Weather.transform.GetChild(2).gameObject.GetComponent<PostProcessVolume>();
            PostProcessProfile TrailerWeather = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneGolemplainsTrailer.asset").WaitForCompletion();
            process.profile = TrailerWeather;
            process.sharedProfile = TrailerWeather;

            //
            Light TheSun = Weather.transform.GetChild(1).gameObject.GetComponent<Light>();
            TheSun.color = new Color(1f, 0.5329f, 0f, 1);
            TheSun.intensity = 1.14f;
            if (SceneInfo.instance.sceneDef.cachedName.EndsWith("2"))
            {
                TheSun.shadowStrength *= 0.4f;
                TheSun.gameObject.transform.localEulerAngles = new Vector3(54f, 80f, 150f);
            }
            else
            {
                TheSun.shadowStrength *= 0.6f;
                TheSun.gameObject.transform.localEulerAngles = new Vector3(42.6938f, 140.2757f, 150.3377f);

            }
            //
            SetAmbientLight newAmbient = Weather.AddComponent<SetAmbientLight>();
            newAmbient.setAmbientLightColor = true;
            newAmbient.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            newAmbient.ambientSkyColor = new Color(0.4191f, 0.305f, 0.7264f, 0f);
            newAmbient.ambientEquatorColor = new Color(0.114f, 0.125f, 0.133f, 1f);
            newAmbient.ambientGroundColor = new Color(0.047f, 0.043f, 0.035f, 1f);
            newAmbient.ambientIntensity = 1f;

            newAmbient.setSkyboxMaterial = true;
            newAmbient.skyboxMaterial = Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/bazaar/matSkybox4.mat").WaitForCompletion();
            newAmbient.ApplyLighting();

        }



    }
}