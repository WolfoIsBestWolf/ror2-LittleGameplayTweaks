using RoR2;
using RoR2.Navigation;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants_2_Goolake : Variant_Base
    {
        public static Material matGoolakeTerrain;
        public static Cubemap ReflectionProbe = Addressables.LoadAssetAsync<Cubemap>(key: "RoR2/Base/goolake/ReflectionProbe-0.exr").WaitForCompletion();

        public static void Setup()
        {
            matGoolakeTerrain = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/goolake/matGoolakeTerrain.mat").WaitForCompletion());

            //matGoolakeTerrain.color = new Color(0.6f, 0.64f, 0.6f);
            matGoolakeTerrain.SetFloat("_BlueChannelBias", 0.4f);
            //matGoolakeTerrain.SetFloat("_BlueChannelSmoothness", 0.4f);
        }

        public static void LoopWeather()
        {
            //0.4941 0.4471 0.4078 1
            //0.2431 0.2392 0.2745 1


            //GameObject Sun = Weather.transform.GetChild(0).gameObject;
            //Sun.SetActive(false);

            /*SetAmbientLight newAmbient = Weather.AddComponent<SetAmbientLight>();
            newAmbient.setSkyboxMaterial = true;
            newAmbient.skyboxMaterial = Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/bazaar/matSkybox4.mat").WaitForCompletion();
            newAmbient.ApplyLighting();*/
            //
            //
            GameObject GameplaySpace = GameObject.Find("/HOLDER: GameplaySpace");

            //GameplaySpace.transform.GetChild(1).GetComponentsInChildren<MeshRenderer>().material = matGoolakeTerrain;
            GameObject GL_Terrain = GameplaySpace.transform.GetChild(2).gameObject;
            GL_Terrain.GetComponent<MeshRenderer>().material = matGoolakeTerrain;


            //GameObject tempobj = GameObject.Find("/HOLDER: Misc Props/GooPlane, High");
            GameObject MiscProps = GameObject.Find("/HOLDER: Misc Props");
            GameObject WaterFall = GameObject.Find("/HOLDER: GameplaySpace/mdlGlDam/GL_AqueductPartial/GL_Waterfall");

            BuffDef SlowTar = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/Base/Common/bdClayGoo.asset").WaitForCompletion();

            WaterFall.transform.localPosition = new Vector3(-0.36f, -1f, 0f);
            WaterFall.transform.localScale = new Vector3(1f, 0.9f, 1f);
            WaterFall.transform.GetChild(8).GetComponent<DebuffZone>().buffType = SlowTar;
            WaterFall.transform.GetChild(8).GetComponent<DebuffZone>().buffDuration = 2;
            GameObject DecalOG = WaterFall.transform.GetChild(9).gameObject; //Decal


            GameObject GooPlaneOldWaterFall = MiscProps.transform.GetChild(2).gameObject;
            GameObject GooPlaneOldWateringHole = MiscProps.transform.GetChild(3).gameObject;

            DebuffZone debuffZone = GooPlaneOldWateringHole.GetComponentInChildren<DebuffZone>();
            debuffZone.buffType = null;
            DebuffZoneFixed debuffZoneReal = debuffZone.gameObject.AddComponent<DebuffZoneFixed>();
            debuffZoneReal.interval = 1;
            debuffZoneReal.buffApplicationEffectPrefab = debuffZone.buffApplicationEffectPrefab;
            debuffZoneReal.buffApplicationSoundString = debuffZone.buffApplicationSoundString;
            debuffZoneReal.buffDuration = 3;
            debuffZoneReal.buffType = SlowTar;

            debuffZone = GooPlaneOldWaterFall.GetComponentInChildren<DebuffZone>();
            debuffZone.buffType = null;
            debuffZoneReal = debuffZone.gameObject.AddComponent<DebuffZoneFixed>();
            debuffZoneReal.interval = 1;
            debuffZoneReal.buffApplicationEffectPrefab = debuffZone.buffApplicationEffectPrefab;
            debuffZoneReal.buffApplicationSoundString = debuffZone.buffApplicationSoundString;
            debuffZoneReal.buffDuration = 3;
            debuffZoneReal.buffType = SlowTar;

            GooPlaneOldWaterFall.transform.localPosition = new Vector3(107.6f, -122.7f, 50.3f);
            GooPlaneOldWaterFall.transform.localScale = new Vector3(7.5579f, 1f, 7.8565f);
            GooPlaneOldWaterFall.transform.GetChild(0).localScale = new Vector3(10f, 100f, 10);
            //GooPlaneOldWaterFall.transform.GetChild(0).GetChild(0).localScale = new Vector3(1f, 50f, 1f);


            GooPlaneOldWateringHole.transform.localPosition = new Vector3(164.4f, -83.01f, -221.2f);
            GooPlaneOldWateringHole.transform.localScale = new Vector3(7.467f, 1f, 7.9853f);
            GooPlaneOldWateringHole.transform.GetChild(1).localScale = new Vector3(10f, 100f, 10f);
            //GooPlaneOld2.transform.GetChild(1).GetChild(0).localScale = new Vector3(1f, 50f, 1f);


            GameObject GooPlaneDecor = Object.Instantiate(GooPlaneOldWaterFall, MiscProps.transform.parent);
            GooPlaneDecor.transform.localPosition = new Vector3(360f, -106f, -260f);
            GooPlaneDecor.transform.localScale = new Vector3(15, 1f, 10f);
            GooPlaneDecor.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            GooPlaneDecor.name = "GooPlane Decor";

            GameObject DecorTarHolder_River = new GameObject("TarDecalHolder_TarRiver");
            if (WConfig.Stage_2_Goolake_River.Value)
            {
                GameObject GooPlaneRiver = Object.Instantiate(GooPlaneOldWateringHole, MiscProps.transform.parent);

                GooPlaneRiver.transform.localPosition = new Vector3(270f, -134.4f, 160f);
                GooPlaneRiver.transform.localScale = new Vector3(30f, 1f, 30f);
                GooPlaneRiver.transform.localEulerAngles = new Vector3(0f, 326.1636f, 0f);
                GooPlaneRiver.transform.GetChild(1).localPosition = new Vector3(0f, -0.2f, 0f);
                GooPlaneRiver.name = "GooPlane CentralRiver";
                //GooPlaneRiver.GetComponent<ParticleSystem>()

                //Add some sort of DECAL decor of tar around the rims and edges of the lake and junk
                GameObject TarDecal = GameObject.Instantiate(DecalOG, DecorTarHolder_River.transform);
                TarDecal.GetComponent<ThreeEyedGames.Decal>().LimitTo = GL_Terrain;
                TarDecal.transform.localPosition = new Vector3(265.3105f, -123.385f, 225.0929f);
                TarDecal.transform.localEulerAngles = new Vector3(0f, 45f, 0f);
                TarDecal.transform.localScale = new Vector3(150f, 40f, 67f);


                TarDecal = GameObject.Instantiate(DecalOG, DecorTarHolder_River.transform);
                TarDecal.GetComponent<ThreeEyedGames.Decal>().Fade = 2f;
                TarDecal.transform.localPosition = new Vector3(136.7616f, -135.4982f, 218.1272f);
                //VoidDecal.transform.localEulerAngles = new Vector3(0f, 243.8361f, 340.6543f);
                TarDecal.transform.localRotation = new Quaternion(0.1426f, -0.8367f, -0.0888f, 0.5212f);
                TarDecal.transform.localScale = new Vector3(70.9044f, 86.1103f, 76.1418f);

                //
                //
                TarDecal = GameObject.Instantiate(DecalOG, DecorTarHolder_River.transform);
                TarDecal.GetComponent<ThreeEyedGames.Decal>().Fade = 4;
                TarDecal.transform.localPosition = new Vector3(240.8404f, -158.2065f, 33.9958f); //-156.0808 -46.5901 -289.8992
                TarDecal.transform.localScale = new Vector3(50f, 79.6727f, 30.3744f);
                TarDecal.transform.localEulerAngles = new Vector3(6.4184f, 343.1097f, 0f);


                TarDecal = GameObject.Instantiate(DecalOG, DecorTarHolder_River.transform);
                TarDecal.GetComponent<ThreeEyedGames.Decal>().Fade = 4;
                TarDecal.transform.localPosition = new Vector3(240.8404f, -158.2065f, 33.9958f); //-156.0808 -46.5901 -289.8992
                TarDecal.transform.localScale = new Vector3(50f, 79.6727f, 30.3744f);
                TarDecal.transform.localEulerAngles = new Vector3(6.4184f, 343.1097f, 0f);
            }


            #region LIGHTING
            GameObject ApproxCenter = GameObject.Find("HOLDER: Secret Ring Area Content/ApproxCenter");
            GameObject GLUndergroundPPVolume = ApproxCenter.transform.GetChild(4).gameObject;

            GameObject Weather = GameObject.Find("/Weather, Goolake");


            Light Sun = Weather.transform.GetChild(0).GetComponent<Light>();
            Sun.shadowStrength = 0.5f;

            ReflectionProbe reflect = Weather.AddComponent<ReflectionProbe>();
            reflect.bakedTexture = ReflectionProbe;
            reflect.resolution = 512;
            //reflect.bounds.extents = new Vector3(500, 500, 500);


            PostProcessProfile original = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneGoolakeInTunnels.asset").WaitForCompletion();
            PostProcessProfile newFog = Object.Instantiate(original);
            RampFog new_RampFog = (RampFog)Object.Instantiate(newFog.settings[0]);

            new_RampFog.fogColorEnd.value = new Color(0.7f, 1f, 0.213f, 1f); //1 0.913 0.2123 1
            new_RampFog.fogColorMid.value = new Color(0.8896f, 0.9151f, 0.3842f, 0.2745f); //0.9151 0.8896 0.3842 0.2745
            //new_RampFog.fogColorStart.value = new Color(0,0,0,0.1f); //0.2803 0.4519 0.566 0
            new_RampFog.fogColorEnd.overrideState = true;

            newFog.settings[0] = new_RampFog;

            PostProcessVolume PPWorld = Weather.transform.GetChild(1).GetComponent<PostProcessVolume>();
            PPWorld.sharedProfile = newFog;
            PPWorld.profile = newFog;
            PPWorld.priority = 1;

            Weather.SetActive(false);
            Weather.SetActive(true);


            SceneWeatherController SunReal = SceneInfo.instance.GetComponent<SceneWeatherController>();
            SunReal.initialWeatherParams.sunColor = new Color(0.5f, 0.6f, 0.5f); //0.4941 0.4471 0.4078 1
            SunReal.initialWeatherParams.sunIntensity = 1f;

            HookLightingIntoPostProcessVolume HookLighting = GLUndergroundPPVolume.GetComponent<HookLightingIntoPostProcessVolume>();
            HookLighting.defaultAmbientColor = new Color(0.4f, 0.55f, 0.45f, 1);
            HookLighting.overrideAmbientColor = new Color(0.22f, 0.35f, 0.24f, 1);
            #endregion

            //Tried to take/learn this from StageVariety but this shit does not work.
            List<NodeGraph.NodeIndex> dest = new List<NodeGraph.NodeIndex>();
            HullMask forbid = (HullMask)7;
            Bounds bounds = new Bounds
            {
                min = new Vector3(230, -120, -300),
                max = new Vector3(250, -30, -228),
            };
            NodeGraph groundNodegraph = SceneInfo.instance.groundNodes;
            groundNodegraph.blockMap.GetItemsInBounds(bounds, dest);
            foreach (NodeGraph.NodeIndex index in dest)
            {
                groundNodegraph.nodes[index.nodeIndex].forbiddenHulls = forbid;
            };


            NodeGraph airNodegraph = SceneInfo.instance.airNodes;
            airNodegraph.blockMap.GetItemsInBounds(bounds, dest);
            foreach (NodeGraph.NodeIndex index in dest)
            {
                airNodegraph.nodes[index.nodeIndex].forbiddenHulls = forbid;
            };
            #region MoreAquaducts
            Transform OriginalAquaduct = GameplaySpace.transform.GetChild(4).GetChild(0);
            GameObject SecondAquadcut = GameObject.Instantiate(OriginalAquaduct.gameObject); //Aquaduct

            SecondAquadcut.transform.localScale = OriginalAquaduct.lossyScale;
            SecondAquadcut.transform.localEulerAngles = new Vector3(0, 25, 180);
            SecondAquadcut.transform.localPosition = new Vector3(214f, -95.1249f, -248f);

            Transform WaterFall2 = SecondAquadcut.transform.GetChild(1);
            WaterFall2.localPosition = new Vector3(0, -3, 0);
            WaterFall2.localScale = new Vector3(1, 0.7f, 1);

            //Disable Bridge 
            Transform Gates = GameplaySpace.transform.Find("Gates");
            Gates.GetChild(0).gameObject.SetActive(false);
            Gates.GetChild(1).gameObject.SetActive(true);


            GameObject DecorAquaduct = GameObject.Instantiate(OriginalAquaduct.gameObject); //Aquaduct

            DecorAquaduct.transform.localScale = OriginalAquaduct.lossyScale;
            DecorAquaduct.transform.localEulerAngles = new Vector3(0, 207.3991f, 180);
            DecorAquaduct.transform.localPosition = new Vector3(-233.9893f, -40f, -228.5455f);

            Transform WaterFall3 = DecorAquaduct.transform.GetChild(1);
            WaterFall3.localPosition = new Vector3(-0.36f, -3, 0);
            WaterFall3.localScale = new Vector3(1, 0.7f, 1);

            //WaterFall3.GetChild(1).gameObject.SetActive(false);
            WaterFall3.GetChild(4).gameObject.SetActive(false);


            GameObject DecorAquaduct2 = GameObject.Instantiate(DecorAquaduct.gameObject); //Aquaduct

            DecorAquaduct2.transform.localScale = OriginalAquaduct.lossyScale;
            DecorAquaduct2.transform.localEulerAngles = new Vector3(0, 262.7f, 180);
            DecorAquaduct2.transform.localPosition = new Vector3(106.1051f, -95.3636f, 329.692f);
            //48.8322 -95.1248 345.5283
            //Transform WaterFall4 = DecorAquaduct2.transform.GetChild(1);
            //WaterFall4.localPosition = new Vector3(0, -3, 0);
            //WaterFall4.localScale = new Vector3(1, 0.7f, 1);



            GameObject DecorTarHolder_Distant = new GameObject("TarDecalHolder_DistantAquaduct");
            GameObject NEWDECAL = GameObject.Instantiate(DecalOG, DecorTarHolder_Distant.transform);
            NEWDECAL.transform.localPosition = new Vector3(-185.5541f, -60.0904f, -250.7442f);
            NEWDECAL.transform.localScale = new Vector3(70, 70, 70);

            NEWDECAL = GameObject.Instantiate(DecalOG, DecorTarHolder_Distant.transform);
            NEWDECAL.transform.localPosition = new Vector3(-156f, -47f, -290f); //-156.0808 -46.5901 -289.8992
            NEWDECAL.transform.localScale = new Vector3(153f, 150f, 91f);
            NEWDECAL.transform.localEulerAngles = new Vector3(0, 51, 0);

            NEWDECAL = GameObject.Instantiate(DecalOG, DecorTarHolder_Distant.transform);
            NEWDECAL.transform.localPosition = new Vector3(-73f, -96.6f, -343f); //-156.0808 -46.5901 -289.8992
            NEWDECAL.transform.localScale = new Vector3(153f, 45f, 66f);
            NEWDECAL.transform.localEulerAngles = new Vector3(0, 0, 0);
            #endregion


            //Just turned neon green randomly????
            GameObject RescueShip = GameObject.Find("/HOLDER: Stage Count Toggled Objects/RescueshipBroken/escapeship/DropshipArmature/ROOT/Base");
            if (RescueShip)
            {
                RescueShip.SetActive(false);
            }

            // IGuess check for StageVariety
            if (WConfig.cfgGameplayChanges.Value)
            {
                if (NetworkServer.active)
                {
                    #region SECRET
                    if (Run.instance.stageClearCount > 4 && Run.instance.IsExpansionEnabled(LoopVariantsMain.DLC2))
                    {
                        GameObject Secret = ApproxCenter.transform.GetChild(0).gameObject;

                        Secret.transform.GetChild(1).GetComponent<StartEvent>().enabled = false;
                        Secret.transform.GetChild(2).GetComponent<StartEvent>().enabled = false;

                        Inventory inventory = Secret.transform.GetChild(1).GetComponent<Inventory>();
                        inventory.GiveItem(RoR2Content.Items.FireRing);
                        inventory.GiveItem(RoR2Content.Items.Clover, 20);
                        inventory.GiveItem(RoR2Content.Items.BoostHp, 20);
                        inventory.GiveEquipmentString("EliteAurelioniteEquipment");
                        inventory = Secret.transform.GetChild(2).GetComponent<Inventory>();
                        inventory.GiveItem(RoR2Content.Items.IceRing);
                        inventory.GiveItem(RoR2Content.Items.Clover, 20);
                        inventory.GiveItem(RoR2Content.Items.BoostHp, 20);
                        inventory.GiveEquipmentString("EliteBeadEquipment");
                    }
                    #endregion


                }
            }

        }



        public class DebuffZoneFixed : MonoBehaviour
        {
            private void Awake()
            {
            }

            private void OnTriggerEnter(Collider other)
            {
                if (NetworkServer.active)
                {
                    if (!WConfig.cfgGameplayChanges.Value)
                    {
                        this.buffType = null;
                    }
                    if (!this.buffType)
                    {
                        return;
                    }
                    CharacterBody component = other.GetComponent<CharacterBody>();
                    if (component)
                    {
                        component.AddTimedBuff(this.buffType.buffIndex, this.buffDuration);
                        Util.PlaySound(this.buffApplicationSoundString, component.gameObject);
                        if (this.buffApplicationEffectPrefab)
                        {
                            EffectManager.SpawnEffect(this.buffApplicationEffectPrefab, new EffectData
                            {
                                origin = component.mainHurtBox.transform.position,
                                scale = component.radius
                            }, true);
                        }
                    }
                }
            }

            private void OnTriggerStay(Collider other)
            {
                if (NetworkServer.active)
                {
                    if (!this.buffType)
                    {
                        return;
                    }
                    this.buffTimer -= Time.fixedDeltaTime;
                    if (this.buffTimer <= 0f)
                    {
                        this.buffTimer = this.interval;
                        CharacterBody component = other.GetComponent<CharacterBody>();
                        if (component)
                        {
                            component.AddTimedBuff(this.buffType.buffIndex, this.buffDuration);
                        }
                    }
                }
            }

            public float buffTimer;
            public float interval;

            [Tooltip("The buff type to grant")]
            public BuffDef buffType;

            [Tooltip("The buff duration")]
            public float buffDuration;

            public string buffApplicationSoundString;

            public GameObject buffApplicationEffectPrefab;
        }
    }
}