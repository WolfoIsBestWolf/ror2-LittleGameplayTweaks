using RoR2;
using RoR2.Projectile;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants_3_Sulfur
    {
        public static SurfaceDef blueLava = ScriptableObject.CreateInstance<SurfaceDef>();

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

        public static Material matSulfurPodBody;
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
        public static Material matHREgg;

        public static GameObject LunarExploderDotZone = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/LunarExploder/LunarExploderProjectileDotZone.prefab").WaitForCompletion();

        public static PostProcessProfile ppSceneSulfurPools;
        public static PostProcessProfile ppSceneSulfurPoolsCave;

        private static void SulfurPodHelfire(On.EntityStates.Destructible.SulfurPodDeath.orig_Explode orig, EntityStates.Destructible.SulfurPodDeath self)
        {
            bool shouldBeFire = false;

            if (WConfig.cfgGameplayChanges.Value  && WConfig.Stage_3_Sulfur.Value && WConfig.Stage_3_Sulfur_Hellfire.Value)
            {
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
                self.teamComponent.teamIndex = TeamIndex.Monster;
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
                            float baseDamage = self.damageStat * 0.5f * EntityStates.Destructible.SulfurPodDeath.explosionDamageCoefficient * Run.instance.teamlessDamageCoefficient;

                            if (body.teamComponent && body.teamComponent.teamIndex == TeamIndex.Monster)
                            {
                                baseDamage *= 5;
                            }

                            InflictDotInfo inflictDotInfo = new InflictDotInfo
                            {
                                attackerObject = self.gameObject,
                                victimObject = body.gameObject,
                                totalDamage = new float?(baseDamage),
                                damageMultiplier = 1,
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
                        baseDamage = self.damageStat * 0.25f * EntityStates.Destructible.SulfurPodDeath.explosionDamageCoefficient * Run.instance.teamlessDamageCoefficient,
                        radius = EntityStates.Destructible.SulfurPodDeath.explosionRadius,
                        falloffModel = BlastAttack.FalloffModel.None,
                        procCoefficient = 0.5f,
                        damageType = DamageType.PoisonOnHit,
                        teamIndex = TeamIndex.Monster,
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
            On.EntityStates.Destructible.SulfurPodDeath.Explode += SulfurPodHelfire;
            blueLava.name = "sdLavaBlue";
            blueLava.damage = 69f;
            blueLava.isLava = true;
            blueLava.speedBoost = 5;
            blueLava.materialSwitchString = "dirt";
            blueLava.approximateColor = new Color(0, 0.5f, 1f);

            /*
            Texture2D texSPGroundRed = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPGroundRed.LoadImage(Properties.Resources.texSPGroundRed, false);
           
            Texture2D texSPGroundRed_FORLAVA = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPGroundRed_FORLAVA.LoadImage(Properties.Resources.texSPGroundRed_FORLAVA, false);
          
            Texture2D texSPGroundDIFVein = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPGroundDIFVein.LoadImage(Properties.Resources.texSPGroundDIFVein, false);
            
            Texture2D VeinEM = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            VeinEM.LoadImage(Properties.Resources.VeinEM, false);     

            Texture2D texSPGroundDIFMain = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPGroundDIFMain.LoadImage(Properties.Resources.texSPGroundDIFMain, false);
        
            Texture2D texSPGroundDIFPale = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPGroundDIFPale.LoadImage(Properties.Resources.texSPGroundDIFPale, false);
            
            Texture2D texSPSpheremoss = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texSPSpheremoss.LoadImage(Properties.Resources.texSPSpheremoss, false);
            
            Texture2D texSPSphereRock = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texSPSphereRock.LoadImage(Properties.Resources.texSPSphereRock, false);
            
            Texture2D texRampMagmaWorm = new Texture2D(256, 8, TextureFormat.DXT1, false);
            texRampMagmaWorm.LoadImage(Properties.Resources.texRampMagmaWorm, true);
            
            Texture2D texRampCaptainAirstrike = new Texture2D(128, 16, TextureFormat.DXT1, false);
            texRampCaptainAirstrike.LoadImage(Properties.Resources.texRampCaptainAirstrike, true);
            
            Texture2D texRampTeleporterSoft = new Texture2D(128, 16, TextureFormat.DXT1, false);
            texRampTeleporterSoft.LoadImage(Properties.Resources.texRampTeleporterSoft, true);
           
            Texture2D texRampArtifactShellSoft = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampArtifactShellSoft.LoadImage(Properties.Resources.texRampArtifactShellSoft, true);
            texRampArtifactShellSoft.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampGreaterWisp = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampGreaterWisp.LoadImage(Properties.Resources.texRampGreaterWisp, true);
            texRampGreaterWisp.wrapMode = TextureWrapMode.Clamp;

            Texture2D texSPGrass = new Texture2D(128, 256, TextureFormat.DXT5, false);
            texSPGrass.LoadImage(Properties.Resources.texSPGrass, false);
            texSPGrass.wrapMode = TextureWrapMode.Clamp;

            Texture2D texSPTallGrass = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texSPTallGrass.LoadImage(Properties.Resources.texSPTallGrass, false);
            texSPTallGrass.wrapMode = TextureWrapMode.Clamp;

            Texture2D spmSPVine = new Texture2D(128, 512, TextureFormat.DXT5, false);
            spmSPVine.LoadImage(Properties.Resources.spmSPVine, false);
            spmSPVine.wrapMode = TextureWrapMode.Clamp;

            */

            Texture2D spmSPVine = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/spmSPVine.png");
            Texture2D texRampArtifactShellSoft = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texRampArtifactShellSoft.png");
            Texture2D texRampCaptainAirstrike = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texRampCaptainAirstrike.png");
            Texture2D texRampGreaterWisp = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texRampGreaterWisp.png");
            Texture2D texRampMagmaWorm = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texRampMagmaWorm.png");
            Texture2D texRampTeleporterSoft = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texRampTeleporterSoft.png");
            Cubemap texSkyboxSP = Files.Bundle.LoadAsset<Cubemap>("Assets/LoopVariants/SulfurPools/texSkyboxSP.png");
            Texture2D texSPCoralEmi = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPCoralEmi.png");
            Texture2D texSPCoralString = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPCoralString.png");
            Texture2D texSPGrass = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPGrass.png");
            Texture2D texSPGrassEMI = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPGrassEMI.png");
            Texture2D texSPGroundDIFMain = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPGroundDIFMain.png");
            Texture2D texSPGroundDIFPale = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPGroundDIFPale.png");
            Texture2D texSPGroundDIFVein = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPGroundDIFVein.png");
            Texture2D texSPGroundRed = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPGroundRed.png");
            Texture2D texSPGroundRed_FORLAVA = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPGroundRed_FORLAVA.png");
            Texture2D texSPSpheremoss = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPSpheremoss.png");
            Texture2D texSPSphereRock = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPSphereRock.png");
            Texture2D texSPTallGrass = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/texSPTallGrass.png");
            Texture2D VeinEM = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/SulfurPools/VeinEM.png");
 
            Texture2D texRampLightning = Object.Instantiate(Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/Base/Common/ColorRamps/texRampLightning.png").WaitForCompletion());
          
            matHRLava = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/helminthroost/Assets/matHRLava.mat").WaitForCompletion());
            matHRLava.color = new Color(0f, 0f, 1f, 1f); //0.9623 0.3237 0 1
            //matHRLava.SetTexture("_BlueChannelTex", texFSLichen); //texFSLichen
            matHRLava.SetTexture("_GreenChannelTex", texSPGroundRed_FORLAVA); //texSPGroundRed
            matHRLava.SetTexture("_FlowHeightRamp", texRampMagmaWorm); //texRampMagmaWorm
            matHRLava.SetTexture("_FresnelRamp", texRampCaptainAirstrike); //texRampCaptainAirstrike
            matHRLava.name = "matHRLava_Blue";

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

            //Rocks inside of sphers or smth
            matSPCrystal = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPCrystal.mat").WaitForCompletion());
            matSPCrystal.color = new Color(0, 0.0732f, 0.6038f, 1f); //0.6038 0 0.0732 1
            matSPCrystal.mainTexture = texSPSphereRock; //texSPSphereRock
            //matSPCrystal.SetTexture("_FlowHeightRamp", texRampTeleporterSoft); //texRampTeleporterSoft
            //matSPCrystal.SetTexture("_FresnelRamp", texRampArtifactShellSoft); //texRampArtifactShellSoft
            matSPCrystal.SetColor("_EmColor", new Color(0, 0.2f, 0.8f, 1f)); ; //_EmColor: {r: 0.9056604, g: 0, b: 0, a: 1}



            matSulfurPodBody = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/SulfurPod/matSulfurPodBody.mat").WaitForCompletion());
            matSulfurPodBody.color = new Color(0f, 0f, 0f, 0f);//0.4417 0.5094 0.286 1
            matSulfurPodBody.SetTexture("_BlueChannelTex", texSPSphereRock);
            matSulfurPodBody.SetTexture("_GreenChannelTex", texSPGroundDIFPale);
            matSulfurPodBody.SetTexture("_FresnelRamp", texRampGreaterWisp);
            matSulfurPodBody.SetFloat("_FresnelBoost", 4f);
            //matSulfurPodBody.SetFloat("_FresnelPower", 0.7f);

            matSPSphere = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPSphere.mat").WaitForCompletion());
            matSPSphere.shader = matSPCrystal.shader;
            matSPSphere.mainTexture = texSPSphereRock;
            matSPSphere.SetTexture("_BlueChannelTex", texSPGroundDIFPale); //texSPGroundDIFPale
            matSPSphere.SetTexture("_GreenChannelTex", texSPSpheremoss); //texSPSpheremoss
            matSPSphere.SetTexture("_RedChannelTopTex", texSPSphereRock); //texSPSphereRock
            matSPSphere.SetTexture("_RedChannelSideTex", texSPGroundRed); //texSPGroundRed
            matSPSphere.SetTexture("_EmTex", VeinEM);
            matSPSphere.SetColor("_EmColor", new Color(1, 2f, 5f, 1f));



            #region Fog / Gas Clouds
            //All 4 share a yellow main color but it just doesn't do anything
            //Also have EM colors that don't do shit

            //Color newGas = new Color();

            matSPCloseLowFog = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPCloseLowFog.mat").WaitForCompletion());
            matSPCloseLowFog.SetColor("_TintColor", new Color(0f, 0.06f, 0.18f, 1f));//

            matSPDistantLowFog = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPDistantLowFog.mat").WaitForCompletion());
            matSPDistantLowFog.SetColor("_TintColor", new Color(0.1f, 0.2f, 0.55f, 1f)); // 0.2075 0.1986 0.001 1


            matSPDistantMountainClouds = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPDistantMountainClouds.mat").WaitForCompletion());
            matSPDistantMountainClouds.SetColor("_TintColor", new Color(0.1f, 0.2f, 0.55f, 1f));//

            matSPDistantVolcanoCloud = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPDistantVolcanoCloud.mat").WaitForCompletion());
            matSPDistantVolcanoCloud.SetColor("_TintColor", new Color(0, 0.1f, 1f, 1f)); // {r: 0.49056602, g: 0.2592463, b: 0, a: 1}


            matSPPortalCard = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPPortalCard.mat").WaitForCompletion());
            matSPPortalCard.SetColor("_TintColor", new Color(0, 0f, 0.1f, 1f)); //0.0943 0.0102 0.0124 1

            matSPWaterBlue = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPWaterBlue.mat").WaitForCompletion());
            matSPWaterBlue.color = Color.cyan;
            matSPWaterBlue.SetTexture("_Cube", null);
            matSPWaterBlue.SetColor("_TintColor", new Color(0, 0f, 0.1f, 1f)); //0.0943 0.0102 0.0124 1

            #endregion

            #region PP Vol
            PostProcessProfile original = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/DLC1/sulfurpools/ppSceneSulfurPoolsCave.asset").WaitForCompletion();
            PostProcessProfile original_Cave = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/DLC1/sulfurpools/ppSceneSulfurPoolsCave.asset").WaitForCompletion();
            ppSceneSulfurPools = Object.Instantiate(original);
            ppSceneSulfurPoolsCave = Object.Instantiate(original_Cave);

            //PP - WORLD
            RampFog rampFog = Object.Instantiate((RampFog)ppSceneSulfurPools.settings[0]);
            
            rampFog.fogColorEnd.value = new Color(0.2f, 0.2f, 0.4f, 1f); //0.3312 0.3962 0.1962 1
            rampFog.fogColorMid.value = new Color(0.10f, 0.11f, 0.18f, 0.7f); //0.1792 0.1095 0.104 1
            rampFog.fogColorStart.value = new Color(0.27f, 0.277f, 0.38f, 0); //0.3774 0.2723 0.2723 0
            rampFog.fogIntensity.value = 1; //1
            rampFog.fogOne.value = 0.1f; //0.09
            rampFog.fogPower.value = 1f; //7
            rampFog.fogZero.value = 0f; //-0.36
            rampFog.skyboxStrength.value = 0.1f; //0
            ppSceneSulfurPools.settings[0] = rampFog;


            //PP - CAVE
            rampFog = Object.Instantiate((RampFog)ppSceneSulfurPoolsCave.settings[0]);
            rampFog.fogColorEnd.value = new Color(0.2f, 0.2f, 0.4f, 1f); //0.3312 0.3962 0.1962 1
            rampFog.fogColorMid.value = new Color(0.10f, 0.11f, 0.18f, 1); //0.1792 0.1095 0.104 1
            rampFog.fogColorStart.value = new Color(0.27f, 0.277f, 0.38f, 0); //0.3774 0.2723 0.2723 0
 
            ppSceneSulfurPoolsCave.settings[0] = rampFog;
            #endregion

            matSkyboxSP = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSkyboxSP.mat").WaitForCompletion());

            /* Texture2D texSkyboxSPFAKE = new Texture2D(3072, 2048, TextureFormat.RGBA32, false);
            texSkyboxSPFAKE.LoadImage(Properties.Resources.texSkyboxSP, false);
            texSkyboxSPFAKE.wrapMode = TextureWrapMode.Clamp;*/




            /*Cubemap texSkyboxSP = new Cubemap(1024, TextureFormat.RGBA32, false);
            texSkyboxSP.wrapMode = TextureWrapMode.Clamp;
 
            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(0, 0, 1024, 1024), CubemapFace.PositiveX);

            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(1024, 0, 1024, 1024), CubemapFace.PositiveZ);

            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(2048, 0, 1024, 1024), CubemapFace.PositiveY);

            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(0, 1024, 1024, 1024), CubemapFace.NegativeY);

            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(1024, 1024, 1024, 1024), CubemapFace.NegativeX);

            texSkyboxSP.SetPixels(texSkyboxSPFAKE.GetPixels(2048, 1024, 1024, 1024), CubemapFace.NegativeZ);
            texSkyboxSP.Apply();*/
            matSkyboxSP.SetTexture("_Tex", texSkyboxSP); //texSkyboxSP




            //Foliage

            matSPGrass = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPGrass.mat").WaitForCompletion());
            matSPGrassEmi = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPGrassEmi.mat").WaitForCompletion());

            

            matSPGrass.mainTexture = texSPGrass;
            matSPGrassEmi.mainTexture = texSPGrass;
            //
            matSPTallGrass = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPTallGrass.mat").WaitForCompletion());

          

            matSPTallGrass.mainTexture = texSPTallGrass;
            //
            matSPVine = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/sulfurpools/matSPVine.mat").WaitForCompletion());
            

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

             
        }

        public static void LoopWeather()
        {
            GameObject Weather = GameObject.Find("/HOLDER: Skybox");
            GameObject MainTerrain = GameObject.Find("mdlSPTerrain");
            GameObject Props = GameObject.Find("HOLDER: Props");

            PostProcessVolume PP = Weather.transform.GetChild(3).GetComponent<PostProcessVolume>();
            PostProcessVolume PP_CAVE = Weather.transform.GetChild(5).GetComponent<PostProcessVolume>();
            PP.profile = ppSceneSulfurPools;
            PP.sharedProfile = ppSceneSulfurPools;
            PP_CAVE.profile = ppSceneSulfurPoolsCave;
            PP_CAVE.sharedProfile = ppSceneSulfurPoolsCave;


            SetAmbientLight Ambient = Weather.transform.GetChild(3).GetComponent<SetAmbientLight>();

            //Ambient Light
            Ambient.ambientSkyColor = new Color(0.45f, 0.45f, 0.6f, 1);//0.6431 0.5449 0.3569 1
            /*//Ambient.ambientGroundColor = new Color();// These don't do anything
            //Ambient.ambientEquatorColor = new Color();//*/

            Ambient.skyboxMaterial = matSkyboxSP;
            Ambient.ApplyLighting();


            //Sun Light
            Light Sun = Weather.transform.GetChild(2).GetComponent<Light>();
            Sun.color = new Color(0.05f, 0.25f, 0.45f, 1); //0.8252 0.9151 0.5655 1
            Sun.intensity = 1f; //0.6f

            #region LavaAtBottom
            Transform BigWater = MainTerrain.transform.GetChild(0);
            BigWater.GetComponent<MeshRenderer>().material = matHRLava;


            //float corners = 200;

            GameObject Light_HOLDER = new GameObject("LavaLight_Holder_Inner");
            Light_HOLDER.AddComponent<ApplyToAllLights>();
            Light_HOLDER.transform.position = new Vector3(0, -60f, 0);
            Light_HOLDER.transform.localScale = new Vector3(1f, 1f, 1f);

            GameObject lavaLight_Object = new GameObject("LavaLight1");         
            lavaLight_Object.transform.SetParent(Light_HOLDER.transform);
            lavaLight_Object.transform.localPosition = new Vector3(-40, 0, -260);
            lavaLight_Object.transform.localEulerAngles = new Vector3(270, 0, 0);
            Light lavaLight = lavaLight_Object.AddComponent<Light>();
            lavaLight.color = new Color(0, 0.5f, 1f);
            lavaLight.type = LightType.Point;
            lavaLight.intensity = 12f;
            //lavaLight.spotAngle = 90;
            //lavaLight.innerSpotAngle = 90;
            lavaLight.range = 200f;
            //lavaLight.shadows = LightShadows.Hard;


            GameObject lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(90, 0f, -220);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(190, 0f, -210);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(190, 0f, -90);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(95, 0f, -30);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(-5, 0f, -60);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(65, 0f, 25);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(250, 0f, 10);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(170, 0f, 100);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(100, 0f, 125);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(20, 0f, 151);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(-80, 0f, 200);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(-140, 0f, 150);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(-175, 0f, -70);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(-190, 0f, 30);

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(-80, 0f, -35);

            /*lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(-150, 0f, -125);*/

            lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(-120, 0f, -190);

            /*lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(-50, 0f, -225);*/

            /*lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
            lavaLight_Object2.transform.localPosition = new Vector3(10, 0f, -280);*/


            if (!WConfig.Stage_3_Sulfur_ExtraLights.Value)
            {
                ////OUTER
                Light_HOLDER = new GameObject("LavaLight_Holder_Outer");
                Light_HOLDER.transform.position = new Vector3(0, -110f, 0);
                Light_HOLDER.transform.localScale = new Vector3(1f, 1f, 1f);
                Light_HOLDER.AddComponent<ApplyToAllLights>();

                lavaLight_Object = new GameObject("LavaLight2");
                lavaLight_Object.transform.SetParent(Light_HOLDER.transform);
                lavaLight = lavaLight_Object.AddComponent<Light>();
                lavaLight.color = new Color(0, 0.5f, 1f);
                lavaLight.type = LightType.Point;
                lavaLight.intensity = 12f;
                lavaLight.range = 300f;
                //lavaLight.shadows = LightShadows.None;
                lavaLight_Object.transform.localEulerAngles = new Vector3(270, 0, 0);
                lavaLight_Object.transform.localPosition = new Vector3(-190, 0f, -290);
           

                /*lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(-225, 0, -430);*/
                /*lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(-190, 0f, -290);*/
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(-280, 0f, -180);
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(-360, 0f, -60);
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(-360, 0f, 100);
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(-340, 0f, 230);
                /*lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(-230, 0f, 270);*/
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(-110, 0f, 320);
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(0, 0f, 275);
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(140, 0f, 240);
                /*lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(160, 0f, 380);*/
                /*lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(300, 0f, 275);*/
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(350, 0f, 160);
                /*lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(420, 0f, 60);*/
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(380, 0f, 55);
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(370, 0f, -135);
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(360, 0f, -260);
                /*lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(320, 0f, -380);*/
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(200, 0f, -330);
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(30, 0f, -340);
                /*lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(20, 0f, -460);
                lavaLight_Object2 = Object.Instantiate(lavaLight_Object, Light_HOLDER.transform);
                lavaLight_Object2.transform.localPosition = new Vector3(-75, 0f, -600);*/
            }


            #endregion

            #region Waters

            Transform Water_Blue_Side = MainTerrain.transform.GetChild(16);
            Transform Water_Green_Up = MainTerrain.transform.GetChild(17);
            Transform Water_Red_Center = MainTerrain.transform.GetChild(18);
            Transform Water_Yellow_Down = MainTerrain.transform.GetChild(19);
            Water_Blue_Side.GetComponent<MeshRenderer>().material = matSPWaterBlue;
            Water_Green_Up.GetComponent<MeshRenderer>().material = matSPWaterBlue;
            Water_Red_Center.GetComponent<MeshRenderer>().material = matSPWaterBlue;
            Water_Yellow_Down.GetComponent<MeshRenderer>().material = matSPWaterBlue;




            #endregion

            #region Materials
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
                            renderer.sharedMaterial = matSulfurPodBody;
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

            #endregion

            #region Gas

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

            //Gas from Heatvents
            Transform Heatvents = Props.transform.GetChild(2);

            ParticleSystemRenderer[] particleList = Heatvents.GetComponentsInChildren<ParticleSystemRenderer>();
            foreach (ParticleSystemRenderer renderer in particleList)
            {
                renderer.material = matSPEnvSmoke;
            }

            //Portal Cards
            Weather.transform.GetChild(4).GetComponent<MeshRenderer>().material = matSPPortalCard;
            Weather.transform.GetChild(6).GetComponent<MeshRenderer>().material = matSPPortalCard;
            Weather.transform.GetChild(7).GetComponent<MeshRenderer>().material = matSPPortalCard;
            Weather.transform.GetChild(8).GetComponent<MeshRenderer>().material = matSPPortalCard;
            //
            Weather.transform.GetChild(9).GetComponent<ParticleSystemRenderer>().material = matSPEnvPoolSmoke;
            Weather.transform.GetChild(10).GetChild(1).GetComponent<ParticleSystemRenderer>().material = matSPEnvSmokeScreen;
            Weather.transform.GetChild(11).GetChild(0).GetComponent<ParticleSystemRenderer>().material = matSPEnvPoolSmoke;
            Weather.transform.GetChild(11).GetChild(1).GetComponent<ParticleSystemRenderer>().material = matSPEnvPoolSmoke;
            Weather.transform.GetChild(11).GetChild(2).GetComponent<ParticleSystemRenderer>().material = matSPEnvPoolSmoke;

            #endregion

            //Has some lights in it
            //Transform SPCoral = Props.transform.GetChild(1);

            #region LunarFlames
            /*
            GameObject LunarFlame = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/moon/Platform_Column_Straight.prefab").WaitForCompletion());

           
            Object.Destroy(LunarFlame.GetComponent<MeshCollider>());
            Object.Destroy(LunarFlame.GetComponent<MeshRenderer>());
            Object.Destroy(LunarFlame.GetComponent<MeshFilter>());
            Object.Destroy(LunarFlame.transform.GetChild(0).GetComponent<MeshCollider>());
            Object.Destroy(LunarFlame.transform.GetChild(0).GetComponent<MeshRenderer>());
            Object.Destroy(LunarFlame.transform.GetChild(0).GetComponent<MeshFilter>());
            LunarFlame.transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
            LunarFlame.transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0, 0);
            LunarFlame.transform.GetChild(0).GetChild(1).localPosition = new Vector3(0, 0, 0);
            LunarFlame.transform.GetChild(0).GetChild(1).GetComponent<LightScaleFromParent>().enabled = true;


            float og = LunarExploderDotZone.GetComponent<ProjectileDotZone>().lifetime;
            LunarExploderDotZone.GetComponent<ProjectileDotZone>().lifetime = 9999;
            LunarExploderDotZone.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false);
            //LunarExploderDotZone


            FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
            fireProjectileInfo.projectilePrefab = LunarExploderDotZone;
            fireProjectileInfo.position = new Vector3(0,0,0);
            fireProjectileInfo.owner = null;
            fireProjectileInfo.damage = RoR2.Run.instance.ambientLevel;
            ProjectileManager.instance.FireProjectile(fireProjectileInfo);
            ProjectileManager.instance.FireProjectile(fireProjectileInfo);
            ProjectileManager.instance.FireProjectile(fireProjectileInfo);
            ProjectileManager.instance.FireProjectile(fireProjectileInfo);
            ProjectileManager.instance.FireProjectile(fireProjectileInfo);
            ProjectileManager.instance.FireProjectile(fireProjectileInfo);
            ProjectileManager.instance.FireProjectile(fireProjectileInfo);
            ProjectileManager.instance.FireProjectile(fireProjectileInfo);


            //
            LunarExploderDotZone.GetComponent<ProjectileDotZone>().lifetime = og;
            LunarExploderDotZone.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
            */
            #endregion

        }


        public class ApplyToAllLights : MonoBehaviour
        {

            public LightType type;
            public LightShadows shadows;
            public float intensity;
            public Color colorNew;
            public float range;
            public float spotAngle;
            public float innerSpotAngle;
            public Vector3 newEulerAngles;

            public void ApplyFromChild()
            {
                Light light = this.gameObject.GetComponentInChildren<Light>();
                this.type = light.type;
                this.shadows = light.shadows;
                this.intensity = light.intensity;
                this.colorNew = light.color;
                this.range = light.range;
                this.spotAngle = light.spotAngle;
                this.innerSpotAngle = light.innerSpotAngle;
                this.newEulerAngles = light.transform.eulerAngles;
            }

            public void ApplyToAllChildren()
            {
                Light[] lightsList = this.gameObject.GetComponentsInChildren<Light>();
                foreach (Light light in lightsList)
                {
                    light.type = this.type;
                    light.shadows = this.shadows;
                    light.intensity = this.intensity;
                    light.color = this.colorNew;
                    light.range = this.range;
                    light.spotAngle = this.spotAngle;
                    light.innerSpotAngle = this.innerSpotAngle;
                    light.transform.eulerAngles = this.newEulerAngles;
                }

            }
        }
    }
    
}