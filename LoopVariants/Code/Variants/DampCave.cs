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
    public class VariantsDampCave
    {


        //public static Material matDCCoral;
        //public static Material matDCCoralActive;
        //public static Material matDCMagmaFlow;    
        //public static Material matDCPortalCard;
        //public static Material matDCSkybox;
        public static Material matDCStalagmite;
        public static Material matDCPebble;

        public static Material matDCBoulder;
        public static Material matDCChainsSmall;

        public static Material matDCHeatvent1;
        public static Material Fronds_0_LOD0;
        public static Material Fronds_0_LOD1;

        public static Material matDCCrystal;
        public static Material matDCCrystalPebble;

        public static Material matDCTerrainFloor;
        public static Material matDCTerrainGiantColumns;
        public static Material matDCTerrainSmallColumn;
        public static Material matDCTerrainWalls;

        public static PostProcessProfile ppSceneDampcaveHot;
        public static PostProcessProfile ppSceneDampcaveInTunnels;

 
        public static void Setup()
        {
            matDCCrystal = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCCrystal.mat").WaitForCompletion());
            matDCCrystalPebble = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCCrystalPebble.mat").WaitForCompletion());
            //matDCCoral = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCCoral.mat").WaitForCompletion());
            //matDCCoralActive = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCCoralActive.mat").WaitForCompletion());
            //matDCHeatvent1 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCHeatvent1.mat").WaitForCompletion());
            matDCHeatvent1 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/voidstage/matVoidOverhang.mat").WaitForCompletion());
            //matDCMagmaFlow = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCMagmaFlow.mat").WaitForCompletion());
            matDCStalagmite = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCStalagmite.mat").WaitForCompletion());
            matDCChainsSmall = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCChainsSmall.mat").WaitForCompletion());

            Fronds_0_LOD0 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/Fronds_0_LOD0.mat").WaitForCompletion());
            Fronds_0_LOD1 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/Fronds_0_LOD1.mat").WaitForCompletion());


            matDCTerrainFloor = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCTerrainFloor.mat").WaitForCompletion());
            matDCTerrainGiantColumns = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCTerrainGiantColumns.mat").WaitForCompletion());
            matDCTerrainWalls = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCTerrainWalls.mat").WaitForCompletion());

            matDCTerrainSmallColumn = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcavesimple/matDCTerrainSmallColumn.mat").WaitForCompletion());
            matDCBoulder = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcavesimple/matDCBoulder.mat").WaitForCompletion());
            matDCPebble = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcavesimple/matDCPebble.mat").WaitForCompletion());


            //Crystal Decors
            matDCCrystal.color = Color.red;  //1 0.852 0.2783 1
            matDCCrystalPebble.color = Color.red;
            //_FlowHeightRamp //texRampMageFire
            //_FresnelRamp //texRampDroneFire


            //matDCTerrainFloor
            //matDCTerrainWalls
            //matDCTerrainSmallColumn
            //matDCTerrainGiantColumns  
            //matDCBoulder
            //matDCSkybox

            //
            PostProcessProfile original = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneDampcaveHot.asset").WaitForCompletion();
            PostProcessProfile original2 = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneDampcaveInTunnels.asset").WaitForCompletion();
            ppSceneDampcaveHot = Object.Instantiate(original);
            ppSceneDampcaveInTunnels = Object.Instantiate(original2);
            RampFog new_RampFog = (RampFog)Object.Instantiate(ppSceneDampcaveHot.settings[0]);
            Bloom new_Bloom = (Bloom)Object.Instantiate(ppSceneDampcaveHot.settings[1]);

            new_RampFog.fogColorEnd.value = new Color(0.7f, 0.3f, 0.4f, 1f)*0.9f;//0.8962 0.6807 0.334 1
            new_RampFog.fogColorMid.value = new Color(0.5f, 0.1f, 0.2f, 0.6f);//0.6588 0.2196 0.3229 0.4471
            //new_RampFog.fogColorStart = new Color();//0.3882 0.2196 0.4627 0
            new_RampFog.fogOne.value = 0.12f;//0.159

            new_Bloom.intensity.overrideState = true;
            new_Bloom.intensity.value = 2;

            ppSceneDampcaveHot.settings[0] = new_RampFog;
            ppSceneDampcaveHot.settings[1] = new_Bloom;
            //
            //
            //
            //Tunnel
            ppSceneDampcaveInTunnels = Object.Instantiate(original2);
            new_RampFog = (RampFog)Object.Instantiate(ppSceneDampcaveInTunnels.settings[0]);
            new_Bloom = (Bloom)Object.Instantiate(ppSceneDampcaveInTunnels.settings[2]);

            new_RampFog.fogColorEnd.value = new Color(0.4f, 0.15f, 0.26f, 1f);//0.8962 0.6807 0.334 1
            new_RampFog.fogColorMid.value = new Color(0.5f, 0.1f, 0.4f, 0.4471f);//0.6588 0.2196 0.3229 0.4471
            //new_RampFog.fogColorStart = new Color();//0.3882 0.2196 0.4627 0
            //new_RampFog.fogOne.value = 0.070f;//0.077

            new_Bloom.intensity.overrideState = true;
            new_Bloom.intensity.value = 2;

            ppSceneDampcaveInTunnels.settings[0] = new_RampFog;
            ppSceneDampcaveInTunnels.settings[2] = new_Bloom;
            //
            //
            Texture2D texWhiteSandSimple = new Texture2D(1024,1024, TextureFormat.DXT5, false);
            texWhiteSandSimple.LoadImage(Properties.Resources.texWhiteSandSimple, true);
            texWhiteSandSimple.filterMode = FilterMode.Bilinear;
            texWhiteSandSimple.wrapMode = TextureWrapMode.Repeat;


            Texture2D texDCGrass = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texDCGrass.LoadImage(Properties.Resources.texDCGrass, true);
            texDCGrass.filterMode = FilterMode.Bilinear;
            texDCGrass.wrapMode = TextureWrapMode.Repeat;

            Texture2D texDCGravelDiffuse = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texDCGravelDiffuse.LoadImage(Properties.Resources.texDCGravelDiffuse, true);
            texDCGravelDiffuse.filterMode = FilterMode.Bilinear;
            texDCGravelDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texDCRockSide = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texDCRockSide.LoadImage(Properties.Resources.texDCRockSide, true);
            texDCRockSide.filterMode = FilterMode.Bilinear;
            texDCRockSide.wrapMode = TextureWrapMode.Repeat;

            Texture2D spmDCFern1_Atlas = new Texture2D(64, 256, TextureFormat.DXT5, false);
            spmDCFern1_Atlas.LoadImage(Properties.Resources.spmDCFern1_Atlas, true);
            spmDCFern1_Atlas.filterMode = FilterMode.Bilinear;
            spmDCFern1_Atlas.wrapMode = TextureWrapMode.Repeat;


            matDCTerrainFloor.SetTexture("_BlueChannelTex", texDCGravelDiffuse);
            matDCTerrainFloor.SetTexture("_GreenChannelTex", texDCGrass);
            matDCTerrainFloor.SetTexture("_RedChannelSideTex", texDCRockSide);
            matDCTerrainFloor.SetTexture("_RedChannelTopTex", texDCRockSide);

            matDCTerrainGiantColumns.SetTexture("_BlueChannelTex", texDCGravelDiffuse);
            matDCTerrainGiantColumns.SetTexture("_GreenChannelTex", texDCGrass);
            matDCTerrainGiantColumns.SetTexture("_RedChannelSideTex", texDCRockSide);
            matDCTerrainGiantColumns.SetTexture("_RedChannelTopTex", texDCRockSide);

            matDCTerrainSmallColumn.SetTexture("_BlueChannelTex", texWhiteSandSimple); //texWhiteSandSimple
            matDCTerrainSmallColumn.SetTexture("_GreenChannelTex", texDCGrass);
            matDCTerrainSmallColumn.SetTexture("_RedChannelSideTex", texDCRockSide);
            matDCTerrainSmallColumn.SetTexture("_RedChannelTopTex", texDCRockSide);

            matDCTerrainWalls.SetTexture("_BlueChannelTex", texWhiteSandSimple); //texWhiteSandSimple
            matDCTerrainWalls.SetTexture("_GreenChannelTex", texDCGrass);
            matDCTerrainWalls.SetTexture("_RedChannelSideTex", texDCRockSide);
            matDCTerrainWalls.SetTexture("_RedChannelTopTex", texDCRockSide);

            Fronds_0_LOD0.mainTexture = spmDCFern1_Atlas;
            Fronds_0_LOD1.mainTexture = spmDCFern1_Atlas;


        }

        public static void Loop_4_Damp()
        {
            GameObject Weather = GameObject.Find("/HOLDER: Lighting, PP, Wind, Misc");
            Light Sun = Weather.transform.GetChild(0).GetComponent<Light>();
            Sun.color = new Color(1, 0.55f, 0.65f); //0.8974 0.898 0.3961 1 //Barley does anything
            Sun.intensity = 0.6f; //0.5f


            SetAmbientLight newAmbient = Weather.AddComponent<SetAmbientLight>();
            newAmbient.setAmbientLightColor = true;
            newAmbient.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            newAmbient.ambientSkyColor = new Color(0.58f, 0.3233f, 0.4279f, 0f) * 1.1f; //0.58 0.3233 0.4279 0
            newAmbient.ambientGroundColor = new Color(0.4528f, 0.3178f, 0.1687f, 1f) * 1.1f; //0.4528 0.3178 0.1687 1
            newAmbient.ambientEquatorColor = new Color(0.114f, 0.125f, 0.133f, 1f)*1.1f; //0.114 0.125 0.133 1
            newAmbient.ambientIntensity = 1;
            newAmbient.ApplyLighting();


            SceneInfo.instance.GetComponent<PostProcessVolume>().sharedProfile = ppSceneDampcaveHot;
            Weather.transform.Find("DCPPInTunnels").GetComponent<PostProcessVolume>().sharedProfile = ppSceneDampcaveInTunnels;



            #region Lights
            Color LampColorNew = new Color(0.5f, 0f, 0.1f);
            Color LanternColorNew = new Color(0.8f, 0.1f, 0.3f);
            Color CoralColorNew = new Color(0.8113f, 0.4784f, 0.7601f, 1);
            Color CrystalLight = new Color(0.7f, 0.25f, 0.45f, 1); //0.8302 0.7849 0.231 1

            GameObject Lamps = GameObject.Find("/HOLDER: Lemmy Lights");
            Light[] lightsList = Lamps.GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = LampColorNew;
            }
            //
            GameObject Crystals = GameObject.Find("/HOLDER: Crystals");
            lightsList = Lamps.GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = CrystalLight;
            }
            //
            GameObject Gameplay = GameObject.Find("/HOLDER: Gameplay Space");
            Transform Heroes = Gameplay.transform.GetChild(0);
 

            lightsList = Gameplay.transform.GetChild(1).GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = LanternColorNew;
            }
            lightsList = Gameplay.transform.GetChild(4).GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = LanternColorNew;
            }
            //
            lightsList = Heroes.transform.GetChild(8).GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = LanternColorNew;
            }
            lightsList = Heroes.transform.GetChild(10).GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = LanternColorNew;
            }
            lightsList = Heroes.transform.GetChild(11).GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = LanternColorNew;
            }
            lightsList = Heroes.transform.GetChild(12).GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = LanternColorNew;
            }
            GameObject Coral = GameObject.Find("/HOLDER: Coral and Vents");
            lightsList = Coral.GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = CoralColorNew;
            }
            #endregion

            MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer renderer in meshList)
            {
                //var meshBase = renderer.gameObject;
                //var meshParent = meshBase.transform.parent;
                if (renderer.sharedMaterial)
                {
                    //Debug.Log(renderer.sharedMaterial);
                    switch(renderer.sharedMaterial.name)
                    {
                        case "matDCCrystal":
                            renderer.sharedMaterial = matDCCrystal;
                            break;
                        case "matDCCrystalPebble":
                            renderer.sharedMaterial = matDCCrystalPebble;
                            break;
                        case "matDCTerrainFloor":
                            renderer.sharedMaterial = matDCTerrainFloor;
                            break;
                        case "matDCTerrainGiantColumns":
                            renderer.sharedMaterial = matDCTerrainGiantColumns;
                            break;
                        case "matDCTerrainSmallColumn":
                            renderer.sharedMaterial = matDCTerrainSmallColumn;
                            break;
                        case "matDCTerrainWalls":
                            renderer.sharedMaterial = matDCTerrainWalls;
                            break;
                        case "matDCHeatvent1":
                            renderer.sharedMaterial = matDCHeatvent1;
                            break;
                        case "Fronds_0_LOD0":
                            renderer.sharedMaterial = Fronds_0_LOD0;
                            break;
                        case "Fronds_0_LOD1":
                            renderer.sharedMaterial = Fronds_0_LOD1;
                            break;
                    }

                }
            }


            #region Geyser
            /*//Geyser
            GameObject GeyserHolderDamp = GameObject.Find("/HOLDER: Geyser");
            Material MatLavaGeyser = Object.Instantiate(GeyserHolderDamp.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetComponent<ParticleSystemRenderer>().material);
            Material MatLavaGeyser2 = Object.Instantiate(MatLavaGeyser);
            MatLavaGeyser.SetColor("_EmissionColor", new Color(1.2f, 0.5f, 1f));
            MatLavaGeyser2.SetColor("_EmissionColor", new Color(1f, 0.4f, 0.9f));

            for (int i = 0; i < GeyserHolderDamp.transform.childCount; i++)
            {
                Transform LoopParticles = GeyserHolderDamp.transform.GetChild(i).GetChild(2).GetChild(0);
                LoopParticles.GetChild(1).GetComponent<ParticleSystemRenderer>().material = MatLavaGeyser;
                LoopParticles.GetChild(2).GetComponent<ParticleSystemRenderer>().material = MatLavaGeyser2;
                LoopParticles.GetChild(3).GetComponent<ParticleSystemRenderer>().material = MatLavaGeyser2;
            }*/
            #endregion

            //iMP EYE DECAL??
        }
        
    }
}