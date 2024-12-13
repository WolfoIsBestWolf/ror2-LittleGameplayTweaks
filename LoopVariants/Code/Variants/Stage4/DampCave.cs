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
    public class Variants_4_DampCaveSimpleAbyss
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
        public static Material matDCPortalCard;

        public static PostProcessProfile ppSceneDampcaveHot;
        public static PostProcessProfile ppSceneDampcaveInTunnels;

        //Chains
        public static Material matTrimSheetLemurianMetalLight;



        public static void Setup()
        {
            /*
            Texture2D texWhiteSandSimple = new Texture2D(1024, 1024, TextureFormat.DXT5, true);
            texWhiteSandSimple.LoadImage(Properties.Resources.texWhiteSandSimple, false);

            Texture2D texDCGrass = new Texture2D(512, 512, TextureFormat.DXT5, true);
            texDCGrass.LoadImage(Properties.Resources.texDCGrass, false);

            Texture2D texDCGravelDiffuse = new Texture2D(512, 512, TextureFormat.DXT5, true);
            texDCGravelDiffuse.LoadImage(Properties.Resources.texDCGravelDiffuse, false);

            Texture2D texDCRockSide = new Texture2D(256, 256, TextureFormat.DXT5, true);
            texDCRockSide.LoadImage(Properties.Resources.texDCRockSide, false);


            Texture2D spmDCFern1_Atlas = new Texture2D(64, 256, TextureFormat.DXT5, true);
            spmDCFern1_Atlas.LoadImage(Properties.Resources.spmDCFern1_Atlas, false);
            */


            Texture2D texMalachite = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/Damp/texMalachite.png");
            Texture2D spmDCFern1_Atlas = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/Damp/spmDCFern1_Atlas.png");
            Texture2D texDCRockSide = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/Damp/texDCRockSide.png");
            Texture2D texDCGravelDiffuse = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/Damp/texDCGravelDiffuse.png");
            Texture2D texDCGrass = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/Damp/texDCGrass.png");
            Texture2D texWhiteSandSimple = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/Damp/texWhiteSandSimple.png");
         


            //matEliteHauntedOverlay

            matDCCrystal = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCCrystal.mat").WaitForCompletion());
            matDCCrystal.color = Color.red;  //1 0.852 0.2783 1
            //_FlowHeightRamp //texRampMageFire
            //_FresnelRamp //texRampDroneFire

            matDCCrystalPebble = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCCrystalPebble.mat").WaitForCompletion());
            matDCCrystalPebble.color = Color.red;


            //matDCCoral = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCCoral.mat").WaitForCompletion());
            //matDCCoralActive = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCCoralActive.mat").WaitForCompletion());
            //matDCHeatvent1 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCHeatvent1.mat").WaitForCompletion());
            matDCHeatvent1 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/voidstage/matVoidOverhang.mat").WaitForCompletion());
            
            //matDCMagmaFlow = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCMagmaFlow.mat").WaitForCompletion());
            matDCStalagmite = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCStalagmite.mat").WaitForCompletion());
            
            matDCChainsSmall = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCChainsSmall.mat").WaitForCompletion());

            Fronds_0_LOD0 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/Fronds_0_LOD0.mat").WaitForCompletion());
            Fronds_0_LOD1 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/Fronds_0_LOD1.mat").WaitForCompletion());
            Fronds_0_LOD0.mainTexture = spmDCFern1_Atlas;
            Fronds_0_LOD1.mainTexture = spmDCFern1_Atlas;
            Fronds_0_LOD0.color = new Color(1.5f, 1f, 1.25f, 1f);
            Fronds_0_LOD1.color = new Color(1.5f, 1f, 1.25f, 1f);


            matTrimSheetLemurianMetalLight = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/Common/TrimSheets/matTrimSheetLemurianMetalLight.mat").WaitForCompletion());
            matTrimSheetLemurianMetalLight.SetFloat("_SnowBias", 1);

            matDCTerrainFloor = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCTerrainFloor.mat").WaitForCompletion());
            matDCTerrainGiantColumns = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCTerrainGiantColumns.mat").WaitForCompletion());
            matDCTerrainWalls = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCTerrainWalls.mat").WaitForCompletion());
            matDCTerrainSmallColumn = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcavesimple/matDCTerrainSmallColumn.mat").WaitForCompletion());

            matDCBoulder = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcavesimple/matDCBoulder.mat").WaitForCompletion());
            matDCPebble = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcavesimple/matDCPebble.mat").WaitForCompletion());

            matDCPortalCard = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/dampcave/matDCPortalCard.mat").WaitForCompletion());
            matDCPortalCard.SetColor("_TintColor", new Color(0.4f, 0.1f, 0.2f, 1f )); //0.4151 0.1872 0 1


            //Crystal Decors
           
           
            


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
            RampFog new_RampFog = (RampFog)Object.Instantiate(ppSceneDampcaveHot.settings[0]);
            Bloom new_Bloom = (Bloom)Object.Instantiate(ppSceneDampcaveHot.settings[1]);

            new_RampFog.fogColorEnd.value = new Color(0.7f, 0.3f, 0.4f, 1.2f)*0.9f;//0.8962 0.6807 0.334 1
            new_RampFog.fogColorMid.value = new Color(0.5f, 0.1f, 0.2f, 0.7f);//0.6588 0.2196 0.3229 0.4471
            new_RampFog.fogColorStart.value = new Color(0.46f, 0.22f, 0.36f, 0);//0.3882 0.2196 0.4627 0
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



            matDCTerrainFloor.SetTexture("_BlueChannelTex", texDCGravelDiffuse);
            //matDCTerrainFloor.SetTexture("_GreenChannelTex", texDCGrass);
            matDCTerrainFloor.SetTexture("_RedChannelSideTex", texDCRockSide);
            matDCTerrainFloor.SetTexture("_RedChannelTopTex", texDCRockSide);

            matDCTerrainGiantColumns.SetTexture("_BlueChannelTex", texDCGravelDiffuse);
            //matDCTerrainGiantColumns.SetTexture("_GreenChannelTex", texDCGrass);
            matDCTerrainGiantColumns.SetTexture("_RedChannelSideTex", texDCRockSide);
            matDCTerrainGiantColumns.SetTexture("_RedChannelTopTex", texDCRockSide);

            matDCTerrainSmallColumn.SetTexture("_BlueChannelTex", texWhiteSandSimple); //texWhiteSandSimple
            //matDCTerrainSmallColumn.SetTexture("_GreenChannelTex", texDCGrass);
            matDCTerrainSmallColumn.SetTexture("_RedChannelSideTex", texDCRockSide);
            matDCTerrainSmallColumn.SetTexture("_RedChannelTopTex", texDCRockSide);

            matDCTerrainWalls.SetTexture("_BlueChannelTex", texWhiteSandSimple); //texWhiteSandSimple
            //matDCTerrainWalls.SetTexture("_GreenChannelTex", texDCGrass);
            matDCTerrainWalls.SetTexture("_RedChannelSideTex", texDCRockSide);
            matDCTerrainWalls.SetTexture("_RedChannelTopTex", texDCRockSide);



            matDCTerrainFloor.SetTexture("_GreenChannelTex", texMalachite);
            matDCTerrainFloor.SetTextureScale("_GreenChannelTex", new Vector2(0.9f, 0.9f));
            matDCTerrainWalls.SetTexture("_GreenChannelTex", texMalachite);
            matDCTerrainWalls.SetTextureScale("_GreenChannelTex", new Vector2(0.2f, 0.2f));
            matDCTerrainSmallColumn.SetTexture("_GreenChannelTex", texMalachite);
            matDCTerrainSmallColumn.SetTextureScale("_GreenChannelTex", new Vector2(0.2f, 0.2f));
       
            matDCTerrainGiantColumns.SetTexture("_GreenChannelTex", texMalachite);
            matDCTerrainGiantColumns.SetTextureScale("_GreenChannelTex", new Vector2(0.2f, 0.2f));

 
            matDCTerrainFloor.SetFloat("_GreenChannelSmoothness", 0.5f);
            matDCTerrainWalls.SetFloat("_GreenChannelSmoothness", 1f);
            matDCTerrainSmallColumn.SetFloat("_GreenChannelSmoothness", 1f);
            matDCTerrainGiantColumns.SetFloat("_GreenChannelSmoothness", 1f);

            matDCTerrainFloor.SetFloat("_NormalStrength", 0.12f);
            matDCTerrainWalls.SetFloat("_NormalStrength", 0.12f);
            matDCTerrainSmallColumn.SetFloat("_NormalStrength", 0.12f);
            matDCTerrainGiantColumns.SetFloat("_NormalStrength", 0.12f);
            matDCTerrainWalls.SetFloat("_TextureFactor", 0.05f);
             
            matDCTerrainSmallColumn.SetFloat("_RedChannelBias", 4f);  //1.36f
            matDCTerrainGiantColumns.SetFloat("_RedChannelBias", 1f); //2

            matDCTerrainFloor.SetFloat("_GreenChannelBias", 1f); //0.83f
            matDCTerrainGiantColumns.SetFloat("_GreenChannelBias", 0f); //0.23f
           
            matDCTerrainSmallColumn.shaderKeywords = matDCTerrainWalls.shaderKeywords;
            
            //matDCTerrainFloor.SetTexture("_RedChannelSideTex", texMalachite);
            //matDCTerrainFloor.SetTextureScale("_RedChannelSideTex", new Vector2(0.05f, 0.05f));
            /*matDCTerrainSmallColumn.SetTexture("_RedChannelSideTex", texMalachite);
            matDCTerrainSmallColumn.SetTextureScale("_RedChannelSideTex", new Vector2(0.05f, 0.05f));
            matDCTerrainGiantColumns.SetTexture("_RedChannelSideTex", texMalachite);
            matDCTerrainGiantColumns.SetTextureScale("_RedChannelSideTex", new Vector2(0.05f, 0.05f));*/
            //matDCTerrainWalls.SetTexture("_RedChannelSideTex", texMalachite);
            //matDCTerrainWalls.SetTextureScale("_RedChannelSideTex", new Vector2(0.05f, 0.05f));
        }

        public static void LoopWeather()
        {
            GameObject Weather = GameObject.Find("/HOLDER: Lighting, PP, Wind, Misc");
            Light Sun = Weather.transform.GetChild(0).GetComponent<Light>();
            Sun.color = new Color(1, 0.55f, 0.65f); //0.8974 0.898 0.3961 1 //Barley does anything
            Sun.intensity = 0.6f; //0.5f
            Sun.transform.localEulerAngles = new Vector3(60, 200, 0);
            Sun.shadowStrength = 0.5f;

            //41.2134 222.6395 0

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


            #region PortalCards
            GameObject PortalCards = GameObject.Find("/HOLDER: Portal Cards");

            PortalCards.transform.GetChild(0).GetComponent<MeshRenderer>().material = matDCPortalCard;
            PortalCards.transform.GetChild(1).GetComponent<MeshRenderer>().material = matDCPortalCard;

            #endregion

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
            //Reconsider maybe
            /*GameObject Crystals = GameObject.Find("/HOLDER: Crystals");
            lightsList = Crystals.GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = CrystalLight;
            }*/
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
                        case "matTrimSheetLemurianMetalLight":
                            renderer.sharedMaterial = matTrimSheetLemurianMetalLight;
                            
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

        public static void AddVariantMonsters(DirectorCardCategorySelection dccs)
        {
            if (dccs == null || !LoopVariantsMain.AddMonsters)
            {
                return;
            }
            DirectorCard cscVoidBarnacle = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidBarnacle/cscVoidBarnacle.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            }; 
            DirectorCard cscVoidBarnacleNoCast = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidBarnacle/cscVoidBarnacleNoCast.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscNullifier = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Nullifier/cscNullifier.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscVoidJailer = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscVoidMegaCrab = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscImp = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImp"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard cscImpBoss = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImpBoss"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            int num = Main_Variants.FindSpawnCard(dccs.categories[0].cards, "parent");
            if (num != -1)
            {
                dccs.categories[0].cards[num] = cscVoidMegaCrab;
            }
            else
            {
                dccs.AddCard(0, cscVoidMegaCrab);
            }
            num = Main_Variants.FindSpawnCard(dccs.categories[1].cards, "Parent");
            if (num != -1)
            {
                dccs.AddCard(1, cscVoidJailer);
            }
            else
            {
                dccs.AddCard(1, cscVoidJailer);
            }

            dccs.AddCard(0, cscImpBoss);
            dccs.AddCard(2, cscImp);
            dccs.AddCard(1, cscNullifier);
            dccs.AddCard(2, cscVoidBarnacle);
            dccs.AddCard(2, cscVoidBarnacleNoCast);
        }
    }
}