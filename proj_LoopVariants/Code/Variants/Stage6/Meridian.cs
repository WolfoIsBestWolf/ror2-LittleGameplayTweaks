using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LoopVariants
{
    public class Variants_6_Meridian : Variant_Base
    {
        public static Material matBHFallPlatformSimple;
        public static Material matBHFallTerrainVines;
        public static Material matBHFallDomeTrim;

        public static void Setup()
        {
            matBHFallPlatformSimple = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallPlatformSimple.mat").WaitForCompletion();
            matBHFallTerrainVines = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallTerrainVines.mat").WaitForCompletion();
            matBHFallDomeTrim = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDomeTrim.mat").WaitForCompletion();
        }

        public static void LoopWeather()
        {

            GameObject Terrain = GameObject.Find("/HOLDER: Art/Terrain");

            Transform BHFoliage = Terrain.transform.GetChild(5);


            BillboardRenderer[] billboardList = BHFoliage.GetComponentsInChildren<BillboardRenderer>();
            foreach (BillboardRenderer renderer in billboardList)
            {
                /*if (renderer.billboard.material.name.StartsWith("BHDistantTree_Billboard"))
                {
                    
                }*/
                renderer.billboard.material = Variants_2_LemurianTemple.matBHDistantTree_Billboard;
            }
            //Why does this one work lol
            MeshRenderer[] rendererList = BHFoliage.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in rendererList)
            {
                switch (renderer.material.name)
                {
                    case "matBHDistantTree (Instance)":
                        renderer.material = Variants_2_LemurianTemple.matBHDistantTree;
                        break;
                }
            }

            Transform T3Objects = Terrain.transform.GetChild(6);
            rendererList = T3Objects.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in rendererList)
            {
                switch (renderer.material.name)
                {
                    case "matBHPlatformSimple (Instance)":
                        renderer.material = matBHFallPlatformSimple;
                        break;
                    case "matBHFallTerrainVines (Instance)":
                        renderer.material = matBHFallTerrainVines;
                        break;
                    case "matBHDomeTrim (Instance)":
                        renderer.material = matBHFallDomeTrim;
                        break;
                }
            }
        }

    }
}