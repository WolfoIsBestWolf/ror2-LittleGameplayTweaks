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
    public class Variants_5_HelminthRoost
    {


        public static Material matHRLava;
        public static Material matHRTerrain;
        public static Material matHRTerrainLava;
        public static Material matHRTerrainOuter;
        public static Material matHRWalls;
        public static Material matHRWorm;
        public static Material matHRCrystal;



        public static void Setup()
        {
            matHRLava = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/helminthroost/Assets/matHRLava.mat").WaitForCompletion());
            matHRTerrain = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/helminthroost/Assets/matHRTerrain.mat").WaitForCompletion());
            matHRTerrainLava = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/helminthroost/Assets/matHRTerrainLava.mat").WaitForCompletion());
            matHRTerrainOuter = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/helminthroost/Assets/matHRTerrainOuter.mat").WaitForCompletion());
            matHRWalls = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/helminthroost/Assets/matHRWalls.mat").WaitForCompletion());
            matHRWorm = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/helminthroost/Assets/matHRWorm.mat").WaitForCompletion());
            matHRCrystal = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/helminthroost/Assets/matHRCrystal.mat").WaitForCompletion());

            Texture2D rampGold = Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/DLC1/Common/ColorRamps/texRampStrongerBurn.png").WaitForCompletion();

            Texture2D texRampMagmaWorm = new Texture2D(256, 8, TextureFormat.DXT1, false);
            texRampMagmaWorm.LoadImage(Properties.Resources.Gold_texRampMagmaWorm, true);
            texRampMagmaWorm.filterMode = FilterMode.Bilinear;
            texRampMagmaWorm.wrapMode = TextureWrapMode.Repeat;
            Texture2D texRampCaptainAirstrike = new Texture2D(128, 16, TextureFormat.DXT1, false);
            texRampCaptainAirstrike.LoadImage(Properties.Resources.Gold_texRampCaptainAirstrike, true);
            texRampCaptainAirstrike.filterMode = FilterMode.Bilinear;
            texRampCaptainAirstrike.wrapMode = TextureWrapMode.Repeat;
            Texture2D texSPGroundRed_FORLAVA = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPGroundRed_FORLAVA.LoadImage(Properties.Resources.Gold_texSPGroundRed, true);
            texSPGroundRed_FORLAVA.filterMode = FilterMode.Bilinear;
            texSPGroundRed_FORLAVA.wrapMode = TextureWrapMode.Repeat;

            //matHRCrystal Blue inside of Caves
            //matHRWalls used for Buildings

            //Color NewLava = new Color(0.5f, 0.5f, 0f, 1); //0.9623 0.3237 0 1
            Color NewLava = new Color(1f, 1f, 0f, 0.5f); //0.9623 0.3237 0 1
            matHRLava.color = NewLava;
            matHRLava.SetColor("_FlashColor", new Color(-0.1f, -0.1f, -0.1f, 1f));

            //matHRLava.SetTexture("_GreenChannelTex", texSPGroundRed_FORLAVA); //texSPGroundRed
            //matHRLava.SetTexture("_FlowHeightRamp", texRampMagmaWorm); //texRampMagmaWorm
            //matHRLava.SetTexture("_FresnelRamp", texRampCaptainAirstrike); //texRampCaptainAirstrike
            matHRLava.SetFloat("_FlowHeightPower", 7f);

            matHRTerrainLava.color = new Color(0.9f, 0.9f, 0f, 1);
            matHRTerrainLava.SetColor("_FlashColor", new Color(-0.1f, -0.1f, -0.1f, 1f));

            //_FlowNormalStrength 0.34 //Lava movement
            //_FlowHeightPower: 8.53 //Brightness?

            //
            //matHRTerrain
            //_BlueChannelTex texHRTerrainAsh
            //_GreenChannelTex texHRLavaDiffuse
            //_RedChannelSideTex texHRTerrainVerticaliDiffuse
            //_RedChannelTopTex texHRTerrainHorizontalDiffuse

            //matHRTerrainLava
            //_GreenChannelTex texSPGroundRed

            //matHRLava
            //matHRTerrainLava
            //_GreenChannelTex texSPGroundRed
            //_FresnelRamp texRampCaptainAirstrike

            //lava
            //_FresnelRamp texRampCaptainAirstrike

            Texture2D texHRLavaDiffuse = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texHRLavaDiffuse.LoadImage(Properties.Resources.texHRLavaDiffuse, true);
            texHRLavaDiffuse.filterMode = FilterMode.Bilinear;
            texHRLavaDiffuse.wrapMode = TextureWrapMode.Repeat;

            matHRTerrain.SetTexture("_GreenChannelTex", texHRLavaDiffuse);
            matHRWorm.SetTexture("_GreenChannelTex", texHRLavaDiffuse);

            matHRCrystal.SetTexture("_FlowHeightRamp", rampGold); //texRampBombOrb


            //texRampMinorConstructElectric
            //texRampStrongerBurn
        }


        public static void LoopWeather()
        {
            GameObject Weather = GameObject.Find("/HOLDER: Lighting");
            PostProcessVolume pp = Weather.GetComponentInChildren<PostProcessVolume>();
            RampFog rampFog = (RampFog)pp.profile.settings[0];
            //rampFog.fogColorEnd.value = new Color();
            rampFog.fogOne.value = 0.5f;
            pp.profile.settings[0] = rampFog;
            pp.priority++;


            Weather.transform.GetChild(5).GetChild(1).GetComponent<Light>().intensity = 0.2f;


            GameObject HRCrystals = GameObject.Find("/HOLDER: Art/Props/HRCrystals");
            HRCrystals.GetComponent<MeshRenderer>().sharedMaterials = new Material[] { matHRCrystal, matHRCrystal };

            MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer renderer in meshList)
            {
                //var meshBase = renderer.gameObject;
                //var meshParent = meshBase.transform.parent;
                if (renderer.sharedMaterial)
                {
                    //Debug.Log(renderer.sharedMaterial);
                    switch (renderer.sharedMaterial.name)
                    {
                        case "matHRLava":
                            renderer.sharedMaterial = matHRLava;
                            break;
                        case "matHRTerrain":
                            renderer.sharedMaterial = matHRTerrain;
                            break;
                        case "matHRTerrainLava":
                            renderer.sharedMaterial = matHRTerrainLava;
                            break;
                        case "matHRTerrainOuter":
                            renderer.sharedMaterial = matHRTerrainOuter;
                            break;
                        case "matHRCrystal":
                            renderer.sharedMaterial = matHRCrystal;
                            break;
                        case "matHRWorm":
                            renderer.sharedMaterial = matHRWorm;
                            break;

                    }

                }
            }



        }
     }
}