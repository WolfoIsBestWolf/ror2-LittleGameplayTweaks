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
    public class Variants_4_ShipGraveyard
    {
        public static void Setup()
        {
        }

        public static void LoopWeather()
        {
            GameObject Weather = GameObject.Find("/Weather, Shipgraveyard");

            Light Sun = Weather.transform.GetChild(0).GetComponent<Light>();
            Sun.color = new Color(0.9f, 0.62f, 0.62f,1f);//0.6233 0.8623 0.8868 1

             

            PostProcessVolume PPVol = Weather.transform.GetChild(2).GetComponent<PostProcessVolume>();

            RampFog rampFog = (RampFog)Object.Instantiate(PPVol.profile.settings[0]);
            PPVol.profile.settings[0] = rampFog;
            rampFog.fogColorEnd.value = new Color(0.63f, 0.46f, 0.46f, 1f);//0.425 0.566 0.505 1
            rampFog.fogColorMid.value = new Color(0.48f, 0.29f, 0.29f, 0.51f);//0.215 0.37 0.44 0.51
            rampFog.fogColorStart.value = new Color(0.45f, 0.38f, 0.38f, 0f);//0.375 0.40 0.425 0

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