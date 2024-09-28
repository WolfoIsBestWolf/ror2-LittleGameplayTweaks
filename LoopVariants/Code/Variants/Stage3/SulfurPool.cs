using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants_3_Sulfur
    {


        //public static Material matDCCoral;
        //public static Material matDCCoralActive;
        //public static Material matDCMagmaFlow;    
        //public static Material matDCPortalCard;
        //public static Material matDCSkybox;
        public static Material matSPPortalCard;
        public static Material matSPMoss;

    
     
 
       
        public static Material matSPDistantVolcanoCloud;

        //Foliage
        public static Material matSPGrass;
        public static Material matSPGrassEmi;
        public static Material matSPTallGrass;
        public static Material matSPVine;

        public static Material matSPCoralEmi;
        public static Material matSPCoralString;

        public static Material matSPCrystal;

        //Most Ground
        public static Material matSPGround;       
        public static Material matSPSphere;

        public static Material matSPMountain;
        public static Material matSPMountainDistant;

        public static Material matSPProps;
        public static Material matSPEnvSmoke; //Fog from heatvents
        public static Material matSPEnvSmokeScreen; //Fog on screen
        public static Material matSPEnvPoolSmoke;  


        public static Material matSPCloseLowFog;
        public static Material matSPDistantLowFog;
        public static Material matSPDistantMountainClouds;

        public static Material matSPBgWater;
        public static Material matSPWaterBlue;
        public static Material matSPWaterGreen;
        public static Material matSPWaterRed;
        public static Material matSPWaterYellow;

        public static Material matSkyboxSP;

        public static Material matHRLava;


        public static PostProcessProfile ppSceneSulfurPools;
        public static PostProcessProfile ppSceneSulfurPoolsCave;

        private static void SulfurPodHelfire(On.EntityStates.Destructible.SulfurPodDeath.orig_Explode orig, EntityStates.Destructible.SulfurPodDeath self)
        {
            bool shouldBeFire = false;

            if (Run.instance)
            {
                SceneDef mostRecentSceneDef = SceneCatalog.mostRecentSceneDef;
                if (mostRecentSceneDef && mostRecentSceneDef.baseSceneName.StartsWith("sulfur"))
                {
                    LoopVariantsMain.SyncLoopWeather weather = Run.instance.GetComponent<LoopVariantsMain.SyncLoopWeather>();
                    if (weather && weather.CurrentStage_LoopVariant)
                    {
                        shouldBeFire = true;

                    }
                }

            }

            if (!shouldBeFire)
            {
                orig(self);
            }
            else if (shouldBeFire)
            {
                if (self.hasExploded)
                {
                    return;
                }
                self.hasExploded = true;
                if (EntityStates.Destructible.SulfurPodDeath.explosionEffectPrefab)
                {
                    EffectManager.SpawnEffect(EntityStates.Destructible.SulfurPodDeath.explosionEffectPrefab, new EffectData
                    {
                        origin = self.transform.position,
                        scale = EntityStates.Destructible.SulfurPodDeath.explosionRadius,
                        rotation = Quaternion.identity
                    }, true);
                }
                self.DestroyModel();
                if (NetworkServer.active)
                {
                    SphereSearch sphereSearch = new SphereSearch();
                    List<HurtBox> list = HG.CollectionPool<HurtBox, List<HurtBox>>.RentCollection();
                    sphereSearch.mask = LayerIndex.entityPrecise.mask;
                    sphereSearch.origin = self.gameObject.transform.position;
                    sphereSearch.radius = EntityStates.Destructible.SulfurPodDeath.explosionRadius;
                    sphereSearch.queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
                    sphereSearch.RefreshCandidates();
                    sphereSearch.FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(TeamIndex.None));
                    sphereSearch.OrderCandidatesByDistance();
                    sphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
                    sphereSearch.GetHurtBoxes(list);
                    sphereSearch.ClearCandidates();
                    for (int i = 0; i < list.Count; i++)
                    {
                        HurtBox hurtBox = list[i];
                        if (hurtBox && hurtBox.healthComponent && hurtBox.healthComponent.alive)
                        {
                            CharacterBody body = hurtBox.healthComponent.body;
                            float baseDamage = self.damageStat * EntityStates.Destructible.SulfurPodDeath.explosionDamageCoefficient * Run.instance.teamlessDamageCoefficient;

                            InflictDotInfo inflictDotInfo = new InflictDotInfo
                            {
                                attackerObject = self.gameObject,
                                victimObject = body.gameObject,
                                totalDamage = new float?(baseDamage),
                                damageMultiplier = 0.75f,
                                dotIndex = DotController.DotIndex.Helfire,
                                maxStacksFromAttacker = new uint?(1U)
                            };
                            DotController.InflictDot(ref inflictDotInfo);

                        }
                    }
                    new BlastAttack
                    {
                        attacker = self.gameObject,
                        damageColorIndex = DamageColorIndex.Poison,
                        baseDamage = self.damageStat * EntityStates.Destructible.SulfurPodDeath.explosionDamageCoefficient * Run.instance.teamlessDamageCoefficient,
                        radius = EntityStates.Destructible.SulfurPodDeath.explosionRadius,
                        falloffModel = BlastAttack.FalloffModel.None,
                        procCoefficient = EntityStates.Destructible.SulfurPodDeath.explosionProcCoefficient,
                        teamIndex = TeamIndex.None,
                        damageType = DamageType.Generic,
                        position = self.transform.position,
                        baseForce = EntityStates.Destructible.SulfurPodDeath.explosionForce,
                        attackerFiltering = AttackerFiltering.NeverHitSelf
                    }.Fire();

                    self.DestroyBodyAsapServer();
                }
            }

        }


        public static void Setup()
        {
            if (WConfig.Stage_3_Sulfur.Value)
            {
                On.EntityStates.Destructible.SulfurPodDeath.Explode += SulfurPodHelfire;
            }


            Texture2D texSPGroundRed = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPGroundRed.LoadImage(Properties.Resources.texSPGroundRed, true);
            texSPGroundRed.filterMode = FilterMode.Bilinear;
            texSPGroundRed.wrapMode = TextureWrapMode.Repeat;


            Texture2D texSPGroundRed_FORLAVA = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPGroundRed_FORLAVA.LoadImage(Properties.Resources.texSPGroundRed_FORLAVA, true);
            texSPGroundRed_FORLAVA.filterMode = FilterMode.Bilinear;
            texSPGroundRed_FORLAVA.wrapMode = TextureWrapMode.Repeat;


            Texture2D texSPGroundDIFVein = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPGroundDIFVein.LoadImage(Properties.Resources.texSPGroundDIFVein, true);
            texSPGroundDIFVein.filterMode = FilterMode.Bilinear;
            texSPGroundDIFVein.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSPGroundDIFMain = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPGroundDIFMain.LoadImage(Properties.Resources.texSPGroundDIFMain, true);
            texSPGroundDIFMain.filterMode = FilterMode.Bilinear;
            texSPGroundDIFMain.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSPGroundDIFPale = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPGroundDIFPale.LoadImage(Properties.Resources.texSPGroundDIFPale, true);
            texSPGroundDIFPale.filterMode = FilterMode.Bilinear;
            texSPGroundDIFPale.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSPSpheremoss = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPSpheremoss.LoadImage(Properties.Resources.texSPSpheremoss, true);
            texSPSpheremoss.filterMode = FilterMode.Bilinear;
            texSPSpheremoss.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSPSphereRock = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texSPSphereRock.LoadImage(Properties.Resources.texSPSphereRock, true);
            texSPSphereRock.filterMode = FilterMode.Bilinear;
            texSPSphereRock.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRampMagmaWorm = new Texture2D(256, 8, TextureFormat.DXT1, false);
            texRampMagmaWorm.LoadImage(Properties.Resources.texRampMagmaWorm, true);
            texRampMagmaWorm.filterMode = FilterMode.Bilinear;
            texRampMagmaWorm.wrapMode = TextureWrapMode.Repeat;
            Texture2D texRampCaptainAirstrike = new Texture2D(128, 16, TextureFormat.DXT1, false);
            texRampCaptainAirstrike.LoadImage(Properties.Resources.texRampCaptainAirstrike, true);
            texRampCaptainAirstrike.filterMode = FilterMode.Bilinear;
            texRampCaptainAirstrike.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRampTeleporterSoft = new Texture2D(128, 16, TextureFormat.DXT1, false);
            texRampTeleporterSoft.LoadImage(Properties.Resources.texRampTeleporterSoft, true);
            texRampTeleporterSoft.filterMode = FilterMode.Bilinear;
            texRampTeleporterSoft.wrapMode = TextureWrapMode.Repeat;
            Texture2D texRampArtifactShellSoft = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampArtifactShellSoft.LoadImage(Properties.Resources.texRampArtifactShellSoft, true);
            texRampArtifactShellSoft.filterMode = FilterMode.Bilinear;
            texRampArtifactShellSoft.wrapMode = TextureWrapMode.Clamp;


            Texture2D texRampLightning = Object.Instantiate(Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/Base/Common/ColorRamps/texRampLightning.png").WaitForCompletion());


            matHRLava = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/helminthroost/Assets/matHRLava.mat").WaitForCompletion());
            matHRLava.color = new Color(0f, 0f, 1f, 1f); //0.9623 0.3237 0 1
            //matHRLava.SetTexture("_BlueChannelTex", texFSLichen); //texFSLichen
            matHRLava.SetTexture("_GreenChannelTex", texSPGroundRed_FORLAVA); //texSPGroundRed
            matHRLava.SetTexture("_FlowHeightRamp", texRampMagmaWorm); //texRampMagmaWorm
            matHRLava.SetTexture("_FresnelRamp", texRampCaptainAirstrike); //texRampCaptainAirstrike

            matSPGround = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPGround.mat").WaitForCompletion());
            matSPGround.SetTexture("_BlueChannelTex", texSPGroundDIFPale); //texSPGroundDIFPale
            matSPGround.SetTexture("_GreenChannelTex", texSPGroundDIFVein); //texSPGroundDIFVein
            matSPGround.SetTexture("_RedChannelTopTex", texSPGroundDIFMain); //texSPGroundDIFMain
            matSPGround.SetTexture("_RedChannelSideTex", texSPGroundRed); //texSPGroundRed

            matSPMountain = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPMountain.mat").WaitForCompletion());
            matSPMountain.SetTexture("_BlueChannelTex", texSPGroundDIFPale); //texSPGroundDIFPale
            matSPMountain.SetTexture("_GreenChannelTex", texSPGroundDIFVein); //texSPGroundDIFVein
            matSPMountain.SetTexture("_RedChannelTopTex", texSPGroundDIFMain); //texSPGroundDIFMain
            matSPMountain.SetTexture("_RedChannelSideTex", texSPGroundRed); //texSPGroundRed


            //Red Under Sphere
            matSPProps = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPProps.mat").WaitForCompletion());
            matSPProps.color = new Color(); //0.9245 0.7196 0.7196 1
            matSPProps.SetTexture("_BlueChannelTex", texSPGroundDIFMain); //texSPGroundDIFMain
            //matSPProps.SetTexture("_GreenChannelTex", texFocusedConvergenceOrb); //texFocusedConvergenceOrb
            matSPProps.SetTexture("_RedChannelTopTex", texSPGroundRed); //texSPGroundRed
            matSPProps.SetTexture("_RedChannelSideTex", texSPGroundRed); //texSPGroundRed


            matSPSphere = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPSphere.mat").WaitForCompletion());
            matSPSphere.SetTexture("_BlueChannelTex", texSPGroundDIFPale); //texSPGroundDIFPale
            matSPSphere.SetTexture("_GreenChannelTex", texSPSpheremoss); //texSPSpheremoss
            matSPSphere.SetTexture("_RedChannelTopTex", texSPSphereRock); //texSPSphereRock
            matSPSphere.SetTexture("_RedChannelSideTex", texSPGroundRed); //texSPGroundRed

            //Rocks inside of sphers or smth
            matSPCrystal = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPCrystal.mat").WaitForCompletion());
            matSPCrystal.color = new Color(0, 0.0732f, 0.6038f, 1f); //0.6038 0 0.0732 1
            matSPCrystal.mainTexture = texSPSphereRock; //texSPSphereRock
            matSPCrystal.SetTexture("_FlowHeightRamp", texRampTeleporterSoft); //texRampTeleporterSoft
            matSPCrystal.SetTexture("_FresnelRamp", texRampArtifactShellSoft); //texRampArtifactShellSoft


            #region Fog / Gas Clouds
            //All 4 share a yellow main color but it just doesn't do anything
            //Also have EM colors that don't do shit

            Color newGas = new Color();

            matSPCloseLowFog = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPCloseLowFog.mat").WaitForCompletion());
            matSPCloseLowFog.SetColor("_TintColor", new Color(0f, 0.06f, 0.18f, 1f));//

            matSPDistantLowFog = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPDistantLowFog.mat").WaitForCompletion());
            matSPDistantLowFog.SetColor("_TintColor", new Color(0.166f, 0.333f, 0.9f, 1f)); // 0.2075 0.1986 0.001 1

 
            matSPDistantMountainClouds = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPDistantMountainClouds.mat").WaitForCompletion());
            matSPDistantMountainClouds.SetColor("_TintColor", new Color(0.166f, 0.333f, 0.9f, 1f));//

            matSPDistantVolcanoCloud = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPDistantVolcanoCloud.mat").WaitForCompletion());
            matSPDistantVolcanoCloud.SetColor("_TintColor", new Color(0, 0.1f, 1f , 1f)); // {r: 0.49056602, g: 0.2592463, b: 0, a: 1}

            #endregion

            #region PP Vol
            PostProcessProfile original = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/DLC1/sulfurpools/ppSceneSulfurPools.asset").WaitForCompletion();
            PostProcessProfile original_Cave = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/DLC1/sulfurpools/ppSceneSulfurPoolsCave.asset").WaitForCompletion();
            ppSceneSulfurPools = Object.Instantiate(original);
            ppSceneSulfurPoolsCave = Object.Instantiate(original_Cave);
 
            //PP - WORLD
            RampFog rampFog = Object.Instantiate((RampFog)ppSceneSulfurPools.settings[0]);
            //rampFog.fogColorEnd.value = new Color(0.4902f, 0.3671f, 0.1647f, 0.949f); //0.4902 0.3671 0.1647 0.949
            rampFog.fogColorEnd.value = new Color(0.31f, 0.367f, 0.48f, 0.949f); //0.4902 0.3671 0.1647 0.949
            rampFog.fogColorMid.value = new Color(0.18f, 0.17f, 0.31f, 0.3569f); //0.3458 0.3765 0.1255 0.3569
            rampFog.fogColorStart.value = new Color(0.3243f, 0.3412f, 0.1791f, 0); //0.3243 0.3412 0.1791 0
            rampFog.fogIntensity.value = 1; //1
            rampFog.fogOne.value = 0.11f; //0.11
            rampFog.fogPower.value = 1.7f; //1.7
            rampFog.fogZero.value = -0.12f; //-0.12
            rampFog.skyboxStrength.value = 0.175f;//0.175
            ppSceneSulfurPools.settings[0] = rampFog;


            //PP - CAVE
            rampFog = Object.Instantiate((RampFog)ppSceneSulfurPoolsCave.settings[0]);
            rampFog.fogColorEnd.value = new Color(0.2f, 0.2f, 0.4f,  1f); //0.3312 0.3962 0.1962 1
            rampFog.fogColorMid.value = new Color(0.10f, 0.11f, 0.18f, 1); //0.1792 0.1095 0.104 1
            rampFog.fogColorStart.value = new Color(0.27f, 0.277f, 0.38f, 0); //0.3774 0.2723 0.2723 0
            /*rampFog.fogIntensity.value = 1; //1
            rampFog.fogOne.value = 0.09f; //0.09
            rampFog.fogPower.value = 7f; //7
            rampFog.fogZero.value = -0.36f; //-0.36
            rampFog.skyboxStrength.value = 0f; //0
            */
            ppSceneSulfurPoolsCave.settings[0] = rampFog;
            #endregion

            matSkyboxSP = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSkyboxSP.mat").WaitForCompletion());

            Texture2D texSkyboxSPFAKE = new Texture2D(3072, 2048, TextureFormat.RGBA32, false);
            texSkyboxSPFAKE.LoadImage(Properties.Resources.texSkyboxSP, false);
            texSkyboxSPFAKE.filterMode = FilterMode.Bilinear;
            texSkyboxSPFAKE.wrapMode = TextureWrapMode.Clamp;

       


            Cubemap texSkyboxSP = new Cubemap(1024, TextureFormat.RGBA32, false);
            texSkyboxSP.filterMode = FilterMode.Bilinear;
            texSkyboxSP.wrapMode = TextureWrapMode.Clamp;

            /*Debug.Log(texSkyboxSPFAKE.isReadable);
            Debug.Log(texSkyboxSP.isReadable);
            Debug.Log(texSkyboxSPFAKE.GetPixels(0, 0, 1024, 1024));
            Debug.Log(texSkyboxSPFAKE.GetPixels(1024, 0, 1024, 1024));
            Debug.Log(texSkyboxSPFAKE.GetPixels(2048, 0, 1024, 1024));
            Debug.Log(texSkyboxSPFAKE.GetPixels(0, 1024, 1024, 1024));
            Debug.Log(texSkyboxSPFAKE.GetPixels(1024, 1024, 1024, 1024));
            Debug.Log(texSkyboxSPFAKE.GetPixels(2048, 1024, 1024, 1024));*/

            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(0, 0, 1024, 1024), CubemapFace.PositiveX);

            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(1024, 0, 1024, 1024), CubemapFace.PositiveZ);

            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(2048, 0, 1024, 1024), CubemapFace.PositiveY);

            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(0, 1024, 1024, 1024), CubemapFace.NegativeY);

            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(1024, 1024, 1024, 1024), CubemapFace.NegativeX);

            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(2048, 1024, 1024, 1024), CubemapFace.NegativeZ);
            texSkyboxSP.Apply();
            matSkyboxSP.SetTexture("_Tex", texSkyboxSP); //texSkyboxSP




            //Foliage

            matSPGrass = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPGrass.mat").WaitForCompletion());
            matSPGrassEmi = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPGrassEmi.mat").WaitForCompletion());

            Texture2D texSPGrass = new Texture2D(128, 256, TextureFormat.DXT5, false);
            texSPGrass.LoadImage(Properties.Resources.texSPGrass, false);
            texSPGrass.filterMode = FilterMode.Bilinear;
            texSPGrass.wrapMode = TextureWrapMode.Clamp;

            matSPGrass.mainTexture = texSPGrass;
            matSPGrassEmi.mainTexture = texSPGrass;
            //
            matSPTallGrass = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPTallGrass.mat").WaitForCompletion());
            
            Texture2D texSPTallGrass = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texSPTallGrass.LoadImage(Properties.Resources.texSPTallGrass, false);
            texSPTallGrass.filterMode = FilterMode.Bilinear;
            texSPTallGrass.wrapMode = TextureWrapMode.Clamp;

            matSPTallGrass.mainTexture = texSPTallGrass;
            //
            matSPVine = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPVine.mat").WaitForCompletion());
            
            Texture2D spmSPVine = new Texture2D(128, 512, TextureFormat.DXT5, false);
            spmSPVine.LoadImage(Properties.Resources.spmSPVine, false);
            spmSPVine.filterMode = FilterMode.Bilinear;
            spmSPVine.wrapMode = TextureWrapMode.Clamp;

            matSPVine.mainTexture = spmSPVine;
            //

            matSPCoralEmi = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPCoralEmi.mat").WaitForCompletion());
            matSPCoralEmi.SetTexture("_BlueChannelTex", texSPGroundDIFMain); //texSPGroundDIFMain
            matSPCoralEmi.SetTexture("_GreenChannelTex", texSPGroundRed); //texSPGroundRed

            //

            Color NewSmoke = new Color(0.0426f, 0.2448f, 0.8208f, 1f);

            matSPEnvSmoke = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPEnvSmoke.mat").WaitForCompletion());
            matSPEnvSmoke.SetColor("_TintColor", NewSmoke); //0.8208 0.2448 0.0426 1
            matSPEnvSmokeScreen = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPEnvSmokeScreen.mat").WaitForCompletion());
            matSPEnvSmokeScreen.SetColor("_TintColor", NewSmoke);
            matSPEnvPoolSmoke = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPEnvPoolSmoke.mat").WaitForCompletion());
            matSPEnvPoolSmoke.SetColor("_TintColor", NewSmoke);

            //
        }


        public static void LoopWeather()
        {
            GameObject Weather = GameObject.Find("/HOLDER: Skybox");

            PostProcessVolume PP = Weather.transform.GetChild(3).GetComponent<PostProcessVolume>();
            PostProcessVolume PP_CAVE = Weather.transform.GetChild(5).GetComponent<PostProcessVolume>();
            PP.profile = ppSceneSulfurPools;
            PP.sharedProfile = ppSceneSulfurPools;
            PP_CAVE.profile = ppSceneSulfurPoolsCave;
            PP_CAVE.sharedProfile = ppSceneSulfurPoolsCave;


            SetAmbientLight Ambient = Weather.transform.GetChild(3).GetComponent<SetAmbientLight>();
  

            //Do Portal Cards

            //Ambient Light
            Ambient.ambientSkyColor = new Color(0.45f, 0.45f, 0.6f, 1);//0.6431 0.5449 0.3569 1
            /*//Ambient.ambientGroundColor = new Color();// These don't do anything
            //Ambient.ambientEquatorColor = new Color();//*/
 
            Ambient.skyboxMaterial = matSkyboxSP;
            Ambient.ApplyLighting();







            //Sun Light
            Light Sun = Weather.transform.GetChild(2).GetComponent<Light>();
            Sun.color = new Color(0.6f, 0.4f, 1f, 1); //0.8252 0.9151 0.5655 1
            Sun.intensity = 1f; //0.6f



            #region Gameplay

            GameObject SulfurPods = GameObject.Find("/HOLDER: Skybox");

            #endregion
            #region Waters

            GameObject MainTerrain = GameObject.Find("mdlSPTerrain");

            Transform BigWater = MainTerrain.transform.GetChild(0);
            BigWater.GetComponent<MeshRenderer>().material = matHRLava;


            float corners = 200;
            float height = -300;



            GameObject Light_HOLDER = new GameObject("LavaLight_Holder");
            Light_HOLDER.transform.position = new Vector3(0, height, 0);

            GameObject lavaLight_Object = new GameObject("LavaLight1");
            lavaLight_Object.transform.SetParent(Light_HOLDER.transform);
            lavaLight_Object.transform.localPosition = new Vector3(corners, 0, corners);
            lavaLight_Object.transform.localEulerAngles = new Vector3(270, 0, 0);
            Light lavaLight = lavaLight_Object.AddComponent<Light>();
            lavaLight.color = new Color(0,0.5f,1f);
            lavaLight.type = LightType.Point;
            lavaLight.intensity = 3f;
            lavaLight.spotAngle = 90;
            lavaLight.innerSpotAngle = 90;
 
            lavaLight.range = 4000f;


            //200 , 75
            //50 , -300
            //-200, -100
            //0, 250


            lavaLight_Object = new GameObject("LavaLight2");
            lavaLight_Object.transform.SetParent(Light_HOLDER.transform);
            lavaLight_Object.transform.localPosition = new Vector3(-corners, 0f, corners);

            lavaLight_Object = new GameObject("LavaLight3");
            lavaLight_Object.transform.SetParent(Light_HOLDER.transform);
            lavaLight_Object.transform.localPosition = new Vector3(corners, 0f, -corners/2);

            lavaLight_Object = new GameObject("LavaLight4");
            lavaLight_Object.transform.SetParent(Light_HOLDER.transform);
            lavaLight_Object.transform.localPosition = new Vector3(-corners, 0f, -corners);


            //Add some sort of light from below ig


            Transform meshSPDistantLowFog = MainTerrain.transform.GetChild(3); //matSPDistantLowFog   
            meshSPDistantLowFog.GetComponent<MeshRenderer>().material = matSPDistantLowFog;
            Transform meshSPDistantMountainClouds = MainTerrain.transform.GetChild(5); //matSPDistantMountainClouds 
            meshSPDistantMountainClouds.GetComponent<MeshRenderer>().material = matSPDistantMountainClouds;
            Transform meshSPDistantVolcanoCloud = MainTerrain.transform.GetChild(6); //matSPDistantVolcanoCloud
            meshSPDistantVolcanoCloud.GetComponent<MeshRenderer>().material = matSPDistantVolcanoCloud;
            Transform meshSPNearWaterFog = MainTerrain.transform.GetChild(12); //matSPDistantLowFog
            meshSPNearWaterFog.GetComponent<MeshRenderer>().material = matSPDistantLowFog;
            Transform meshSPTerrainBaseFog = MainTerrain.transform.GetChild(14); //matSPCloseLowFog
            meshSPTerrainBaseFog.GetComponent<MeshRenderer>().material = matSPCloseLowFog;



            Transform Water_Blue_Side = MainTerrain.transform.GetChild(16);
            Transform Water_Green_Up = MainTerrain.transform.GetChild(17);
            Transform Water_Red_Center = MainTerrain.transform.GetChild(18);
            Transform Water_Yellow_Down = MainTerrain.transform.GetChild(19);

            #endregion


            GameObject Props = GameObject.Find("HOLDER: Props");

            #region Gas
            //Gas from Heatvents
            Transform Heatvents = Props.transform.GetChild(2);

            ParticleSystemRenderer[] particleList = Heatvents.GetComponentsInChildren<ParticleSystemRenderer>();
            foreach (ParticleSystemRenderer renderer in particleList)
            {
                renderer.material = matSPEnvSmoke;
            }

            Weather.transform.GetChild(10).GetChild(1).GetComponent<ParticleSystemRenderer>().material = matSPEnvSmokeScreen;
            Weather.transform.GetChild(11).GetChild(0).GetComponent<ParticleSystemRenderer>().material = matSPEnvPoolSmoke;
            Weather.transform.GetChild(11).GetChild(1).GetComponent<ParticleSystemRenderer>().material = matSPEnvPoolSmoke;
            Weather.transform.GetChild(11).GetChild(2).GetComponent<ParticleSystemRenderer>().material = matSPEnvPoolSmoke;

            #endregion



            //Has some lights in it
            Transform SPCoral = Props.transform.GetChild(1);



            //Blue Fire Ideas maybe idk
            //RoR2/Base/moon/Platform_Column_Straight.prefab
            //matLunarExploderDeathDecal
            //matLunarGolemExplosion
            //matLunarGolemExplosion

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
                        case "matSPGround":
                            renderer.sharedMaterial = matSPGround;
                            break;
                        case "matSPProps":
                            renderer.sharedMaterial = matSPProps;
                            break;
                        case "matSPSphere":
                            renderer.sharedMaterial = matSPSphere;
                            break;
                        case "matSPCrystal":
                            renderer.sharedMaterial = matSPCrystal;
                            break;
                        case "matSPMountain":
                            renderer.sharedMaterial = matSPMountain;
                            break;
                        case "matSPGrass":
                            renderer.sharedMaterial = matSPGrass;
                            break;
                        case "matSPGrassEmi":
                            renderer.sharedMaterial = matSPGrassEmi;
                            break;
                        case "matSPTallGrass":
                            renderer.sharedMaterial = matSPTallGrass;
                            break;
                        case "matSPVine":
                            renderer.sharedMaterial = matSPVine;
                            break;
                        case "matSPCoralEmi":
                            renderer.sharedMaterial = matSPCoralEmi;
                            break;

                            

                    }

                }
            }
        }

    }
}