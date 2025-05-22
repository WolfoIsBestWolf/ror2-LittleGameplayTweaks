using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants_2_FoggySwamp : Variant_Base
    {
        public static void Setup()
        {
            //On.EntityStates.Destructible.AltarSkeletonDeath.Explode += AltarSkeletonDeath_Explode;

        }

        private static void AltarSkeletonDeath_Explode(On.EntityStates.Destructible.AltarSkeletonDeath.orig_Explode orig, EntityStates.Destructible.AltarSkeletonDeath self)
        {
            orig(self);
            //Add 500 malachites to the elite tier
            //Malachite Scav that drops smth 
            //Block healing for rest of stage idk

            //Spawn Vengence clone for every player but Malachite idk dude
        }

        public static void LoopWeather()
        {

            GameObject SunReal = GameObject.Find("/Directional Light (SUN)");
            SunReal.GetComponent<Light>().shadowStrength = 0.5f;

            //How the fuck do you make shadows less hard

            #region Higher Water

            GameObject Hero = GameObject.Find("/HOLDER: Hero Assets");

            Transform WaterMain = Hero.transform.GetChild(0);
            Transform WaterSmallNook = Hero.transform.GetChild(1);


            Material CleanWater = Object.Instantiate(WaterMain.GetComponent<MeshRenderer>().material);
            Material CleanWater2 = Object.Instantiate(CleanWater);
            CleanWater2.color = new Color(1, 1, 1, 0.8f);
            CleanWater.color = new Color(1, 1, 1, 0.8f);
            WaterMain.GetComponent<MeshRenderer>().material = CleanWater;
            WaterSmallNook.GetComponent<MeshRenderer>().material = CleanWater2;
            //For more consistency ig


            GameObject WaterForWaterFoot = GameObject.Instantiate(WaterMain.gameObject);
            Object.Destroy(WaterForWaterFoot.GetComponent<MeshRenderer>());
            //GameObject.Instantiate(WaterSmallNook.gameObject);


            /*
            PostProcessVolume processWater = WaterMain.transform.GetChild(0).GetComponent<PostProcessVolume>();
            Vignette vig = (Vignette)processWater.profile.settings[1];
            processWater.profile.settings[1] = vig;
            vig.center.value = new Vector2(5, 5);
            vig.color.value = new Color(0.95f, 1f, 1f, 1f);
            processWater = WaterSmallNook.transform.GetChild(0).GetComponent<PostProcessVolume>();
            processWater.profile.settings[1] = vig;*/


            Transform LeafOnWater = GameObject.Find("/HOLDER: Foliage/Cloverfield").transform;


            Tools.UpTransformByY(WaterMain, 1);
            Tools.UpTransformByY(LeafOnWater.GetChild(8), 1);
            Tools.UpTransformByY(LeafOnWater.GetChild(9), 1);
            Tools.UpTransformByY(LeafOnWater.GetChild(15), 1);
            Tools.UpTransformByY(LeafOnWater.GetChild(16), 1);
            Tools.UpTransformByY(LeafOnWater.GetChild(17), 1);
            Tools.UpTransformByY(LeafOnWater.GetChild(10), 1);
            Tools.UpTransformByY(LeafOnWater.GetChild(32), 1);
            Tools.UpTransformByY(LeafOnWater.GetChild(33), 1);
            Tools.UpTransformByY(LeafOnWater.GetChild(47), 1);
            Tools.UpTransformByY(LeafOnWater.GetChild(48), 1);
            Tools.UpTransformByY(LeafOnWater.GetChild(45), 1);


            //float defaultHeight = -154.4f;
            float desiredHeight = -149.5f;

            Tools.UpTransformByY(WaterSmallNook, 5.1f); //154?
            Tools.UpTransformByY(LeafOnWater.GetChild(63), 5.1f);
            Tools.UpTransformByY(LeafOnWater.GetChild(64), 5.1f);

            //
            GameObject Ruins = GameObject.Find("/HOLDER: Ruin Pieces");
            Transform WaterWall1 = Ruins.transform.Find("FSGiantRuinDoorCollision (3)");
            Transform WaterWall2 = Ruins.transform.Find("FSGiantRuinDoorCollision (1)");
            //-230.6
            //-189.6


            WaterWall1.localScale = new Vector3(1, 2, 1);
            WaterWall2.localScale = new Vector3(1, 2, 1);

            Tools.UpTransformByY(WaterWall1, -40.5f);
            Tools.UpTransformByY(WaterWall2, -40.5f);
            #endregion


            #region FloodWholeThingmaybe


            GameObject Trees = GameObject.Find("/HOLDER: Tree Trunks w Collision/");
            Transform TreesFallOver = Trees.transform.GetChild(20);

            TreesFallOver.localEulerAngles = new Vector3(0, 340, 90);
            TreesFallOver.localPosition = new Vector3(85f, -150.9878f, -156.2819f);

            //The one I wanted has nodes depending on it can't really be removed
            Transform TreeRandomWallig = Trees.transform.Find("FSTreeTrunkMediumCollision (28)");
            TreeRandomWallig.rotation = new Quaternion(-0.5998f, -0.5506f, 0.3745f, 0.4437f);
            TreeRandomWallig.localPosition = new Vector3(-87.9046f, -153.3757f, -209.8361f);
            TreeRandomWallig.localScale = new Vector3(1.54f, 1.62f, 1.54f);


            GameObject NewWaterHost = new GameObject("New Waters");
            NewWaterHost.transform.localPosition = new Vector3(0, desiredHeight, 0);


            GameObject NewWater1 = Object.Instantiate(WaterMain.gameObject, NewWaterHost.transform);
            NewWater1.GetComponent<MeshRenderer>().material = CleanWater2;
            NewWater1.transform.rotation = new Quaternion(-0.6996f, 0.1027f, -0.1027f, -0.6996f);
            NewWater1.transform.localPosition = new Vector3(-222.6811f, 0f, -379.4f);
            //
            GameObject NewWater2 = Object.Instantiate(NewWater1.gameObject, NewWaterHost.transform);
            NewWater2.transform.rotation = new Quaternion(-0.6996f, 0.1027f, -0.1027f, -0.6996f);
            NewWater2.transform.localPosition = new Vector3(-64.5721f, 0f, -150.2274f);
            NewWater2.transform.localScale = new Vector3(354.2f, 100f, 100f);
            //
            GameObject NewWater3 = Object.Instantiate(NewWater1.gameObject, NewWaterHost.transform);
            NewWater3.transform.localEulerAngles = new Vector3(90, 0, 0);
            NewWater3.transform.localPosition = new Vector3(-64.5721f, 0f, -114.2274f);
            NewWater3.transform.localScale = new Vector3(200f, 100f, 100f);

            #endregion

            //Higher Water?
            GameObject Altar = GameObject.Find("/HOLDER: Hidden Altar Stuff");
            HookLightingIntoPostProcessVolume AmbientLight = Altar.transform.GetChild(2).GetComponent<HookLightingIntoPostProcessVolume>();
            AmbientLight.defaultAmbientColor = new Color(0.2363f, 0.3038f, 0.42f, 1f) * 1.4f;//0.4763 0.3976 0.6338 1


            PostProcessVolume process = SceneInfo.instance.GetComponent<PostProcessVolume>();
            RampFog rampFog = (RampFog)process.profile.settings[0];

            //End for dark clouds?
            //Middle for the actual fog
            //rampFog.fogColorEnd.value = new Color(0.44f, 0.55f, 0.66f, 1f); //0.6934 0.7547 0.5803 1
            //rampFog.fogColorMid.value = new Color(0.5f, 0.6f, 0.7f, 1f); //0.9151 0.8896 0.3842 0.2745
            rampFog.fogColorEnd.value = new Color(0.3535f, 0.4074f, 0.4434f, 0.94f);
            rampFog.fogColorMid.value = new Color(0.3373f, 0.3793f, 0.4157f, 0.65f);
            rampFog.fogColorStart.value = new Color(0f, 0f, 0f, 0f); //0.4471 0.5087 0.5294 0
            rampFog.fogOne.value = 0.09f;
            rampFog.fogPower.value = 2f;
            process.profile.settings[0] = rampFog;

            Bloom bloom = (Bloom)Variants_4_DampCaveSimpleAbyss.ppSceneDampcaveHot.settings[1];
            bloom.color.value = new Color(1.2f, 1.2f, 1f, 1f);

            //0.3535 0.4074 0.4434 1


            GameObject HalcyShrine = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/ShrineHalcyonite.prefab").WaitForCompletion();

            GameObject Clouds = Object.Instantiate(HalcyShrine.transform.GetChild(7).gameObject);
            Clouds.transform.position = new Vector3(50, -150, -150);
            Clouds.transform.localScale = new Vector3(6, 6, 6);

            GameObject Rain = Object.Instantiate(HalcyShrine.transform.GetChild(8).gameObject); //Particle System
            Rain.SetActive(true);
            Rain.AddComponent<WeatherParticles>().lockPosition = true;


            HalcyShrine.transform.GetChild(9); //StormPPVolume

        }


    }
}