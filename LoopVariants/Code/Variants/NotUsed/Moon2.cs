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
    public class Variants_6_Moon
    {
        public static void Setup()
        {
        }

        public static void LoopWeather()
        {
             //Spawn in modified Umbra PP Vol with more blue more consistent lighting
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