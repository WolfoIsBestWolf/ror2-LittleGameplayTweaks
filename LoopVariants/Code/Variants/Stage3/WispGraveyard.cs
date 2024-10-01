using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants_3_WispGraveyard
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
            EclipseWeather.transform.GetChild(4).gameObject.SetActive(true);

            /*SetAmbientLight Lighting = EclipseWeather.transform.GetChild(2).GetComponent<SetAmbientLight>();
            Lighting.setSkyboxMaterial = false;
            Lighting.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            Lighting.ApplyLighting();*/

        }
    }
}