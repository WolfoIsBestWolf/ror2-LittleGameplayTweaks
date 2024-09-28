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
    public class Variants_4_RootJungle
    {
         
 
        public static void Setup()
        {
            //ADD BIGFRUITNAME??

        }


        public static void LoopWeather()
        {
            GameObject Weather = GameObject.Find("/HOLDER: Weather Set 1");

            Light TheSun = Weather.transform.GetChild(0).gameObject.GetComponent<Light>();
            TheSun.color = new Color(0.8863f, 0.7255f, 0.5647f, 1);
            TheSun.intensity = 1f;

            SetAmbientLight newAmbient = Weather.AddComponent<SetAmbientLight>();
            newAmbient.setAmbientLightColor = true;
            newAmbient.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            newAmbient.ambientSkyColor = new Color(0.5101f, 0.2622f, 0.3133f, 0.8f);
            newAmbient.ambientGroundColor = new Color(0.3562f, 0.2231f, 0.2512f, 0.8f);
            newAmbient.ambientEquatorColor = new Color(0.1272f, 0.0799f, 0.0799f, 0.8f);
            newAmbient.ambientIntensity = 1;

            newAmbient.setSkyboxMaterial = true;
            newAmbient.skyboxMaterial = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallSkybox.mat").WaitForCompletion();
            newAmbient.ApplyLighting();

            Weather.transform.GetChild(3).GetComponent<PostProcessVolume>().profile = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/DLC2/habitatfall/ppSceneBHFall.asset").WaitForCompletion();

            Material matBHFallGrassGlow = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallGrassGlow.mat").WaitForCompletion());
            Material matBHFallDistantTree = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDistantTree.mat").WaitForCompletion());
            Material matBHFallShurb = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShurb.mat").WaitForCompletion());
            Material matBHFallClouds = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallClouds.mat").WaitForCompletion());
            Material matBHFallDistantMountainClouds = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDistantMountainClouds.mat").WaitForCompletion());
            Material matBHFallDomeTrim = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDomeTrim.mat").WaitForCompletion());
            Material matBHFallEnvfxLeaves = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallEnvfxLeaves.mat").WaitForCompletion());
            Material matBHFallFlower = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallFlower.mat").WaitForCompletion());
            Material matBHFallGodray = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallGodray.mat").WaitForCompletion());
            Material matBHFallHiveBase = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallHiveBase.mat").WaitForCompletion());
            Material matBHFallHiveBubble = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallHiveBubble.mat").WaitForCompletion());
            Material matBHFallHiveDecal = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallHiveDecal.mat").WaitForCompletion());
            Material matBHFallPebble = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallPebble.mat").WaitForCompletion());
            Material matBHFallPlatformSimple = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallPlatformSimple.mat").WaitForCompletion());
            Material matBHFallPlatformTerrain = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallPlatformTerrain.mat").WaitForCompletion());
            Material matBHFallShroomDrips = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShroomDrips.mat").WaitForCompletion());
            Material matBHFallShroomPath = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShroomPath.mat").WaitForCompletion());
            Material matBHFallShroomTunnel = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShroomTunnel.mat").WaitForCompletion());
            Material matBHFallSilhouette = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallSilhouette.mat").WaitForCompletion());
            Material matBHFallStatueBeam = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallStatueBeam.mat").WaitForCompletion());
            Material matBHFallStatueCrystals = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallStatueCrystals.mat").WaitForCompletion());
            Material matBHFallTempleTrim = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallTempleTrim.mat").WaitForCompletion());
            Material matBHFallTerrainVines = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallTerrainVines.mat").WaitForCompletion());




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
                        case "Material_2_LOD0":
                        case "Material_2_LOD1":
                        case "Material_2_LOD2":
                            renderer.materials[1] = matBHFallEnvfxLeaves;
                            break;
                        case "RJShroomFoliage_LOD0":
                            break;
                        case "RJTowerTreeFoliage":
                            renderer.sharedMaterial = matBHFallEnvfxLeaves;
                            break;
                        case "RJTreeBigFoliage":
                            break;
                        case "RJTreeBigFoliage_LOD0":
                            break;
                        case "matRootJungleSpore":
                            break;
                        case "matCliff1VeryMossy":
                            break;
                        case "matRJFogFloor":
                            break;
                        case "matRJLeaf_Blue":
                            renderer.sharedMaterial = matBHFallEnvfxLeaves;
                            break;
                        case "matRJLeaf_Green":
                            renderer.sharedMaterial = matBHFallPebble;
                            break;
                        case "matRJMossPatch1":
                            renderer.sharedMaterial = matBHFallShroomTunnel;
                            break;
                        case "matRJMossPatch2":
                            renderer.sharedMaterial = matBHFallShroomTunnel;
                            break;
                        case "matRJMossPatchLarge":
                            renderer.sharedMaterial = matBHFallShroomTunnel;
                            break;
                        case "matRJMossPatchTriplanar":
                            break;
                        case "matRJPebble":
                            renderer.sharedMaterial = matBHFallEnvfxLeaves;
                            break;
                        case "matRJRock":
                            renderer.sharedMaterial = matBHFallPebble;
                            break;
                        case "matRJSandstone":
                            renderer.sharedMaterial = matBHFallPebble;
                            break;
                        case "matRJShroomBig":
                            renderer.sharedMaterial = matBHFallShroomTunnel;
                            break;
                        case "matRJShroomBounce":
                            break;
                        case "matRJShroomShelf":
                            renderer.sharedMaterial = matBHFallShroomPath;
                            break;
                        case "matRJShroomSmall":
                            renderer.sharedMaterial = matBHFallShroomDrips;
                            break;
                        case "matRJTemple":
                            renderer.sharedMaterial = matBHFallTempleTrim;
                            break;
                        case "matRJTerrain":
                            renderer.sharedMaterial = matBHFallPlatformTerrain;
                            break;
                        case "matRJTerrain2":
                            renderer.sharedMaterial = matBHFallPlatformTerrain;
                            break;
                        case "matRJTree":
                            renderer.sharedMaterial = matBHFallTerrainVines;
                            break;
                        case "matRJTriangle":
                            renderer.sharedMaterial = matBHFallPlatformSimple;
                            break;

                    }

                }
            }
        }

    }
}