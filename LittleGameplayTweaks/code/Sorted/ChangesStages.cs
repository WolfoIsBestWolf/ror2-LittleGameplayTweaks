using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;

namespace LittleGameplayTweaks
{
    public class ChangesStages
    {
        public static void Start()
        {
            On.RoR2.ClassicStageInfo.Start += MoreSceneCredits;
           
            if (WConfig.cfgGoldShoresCredits.Value)
            {
                On.RoR2.ClassicStageInfo.Start += GoldShoresCredits;
            }
            On.RoR2.ClassicStageInfo.Start += RunsAlways_ClassicStageInfo_Start;


            if (WConfig.cfgVoidStagesNoTime.Value)
            {
                Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/Base/arena/arena.asset").WaitForCompletion().sceneType = SceneType.UntimedStage;
                Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/voidstage/voidstage.asset").WaitForCompletion().sceneType = SceneType.UntimedStage;
                Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/voidraid/voidraid.asset").WaitForCompletion().sceneType = SceneType.UntimedStage;
            }
            if (WConfig.RegenArenaCells.Value)
            {
                On.EntityStates.Missions.Arena.NullWard.WardOnAndReady.OnEnter += WardOnAndReady_OnEnter;
                On.EntityStates.Missions.Arena.NullWard.Active.OnEnter += Active_OnEnter;
            }
            On.RoR2.ArenaMissionController.OnStartServer += ArenaMissionController_OnEnable;

          
 

            GameObject DestinationPortal = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/PM DestinationPortal.prefab").WaitForCompletion();
            DestinationPortal.GetComponent<SceneExitController>().useRunNextStageScene = true;

            SceneDef meridian = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC2/meridian/meridian.asset").WaitForCompletion();
            meridian.destinationsGroup = Addressables.LoadAssetAsync<SceneCollection>(key: "RoR2/Base/SceneGroups/sgStage5.asset").WaitForCompletion();
            meridian.loopedDestinationsGroup = Addressables.LoadAssetAsync<SceneCollection>(key: "RoR2/Base/SceneGroups/loopSgStage5.asset").WaitForCompletion();

 
            if (ConfigStages.Stage_F_Moon.Value)
            {
                On.RoR2.EscapeSequenceController.OnEnable += EscapeSequenceController_OnEnable;
            }
        }

        private static void EscapeSequenceController_OnEnable(On.RoR2.EscapeSequenceController.orig_OnEnable orig, EscapeSequenceController self)
        {
            orig(self);
            if (Run.instance.stageClearCount > 7)
            {
                self.gameObject.transform.GetChild(0).GetChild(1).GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsVoidFamily.asset").WaitForCompletion();
            }
        }
 

        private static void RunsAlways_ClassicStageInfo_Start(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            if (Run.instance && SceneInfo.instance)
            {
                //self.sceneDirectorInteractibleCredits += 550;

                switch (SceneInfo.instance.sceneDef.cachedName)
                {
                    case "goolake":
                        BuffDef SlowTar = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/Base/Common/bdClayGoo.asset").WaitForCompletion();
                        GameObject MiscProps = GameObject.Find("/HOLDER: Misc Props"); 
                        GameObject WaterFall = GameObject.Find("/HOLDER: GameplaySpace/mdlGlDam/GL_AqueductPartial/GL_Waterfall");
                        if (MiscProps)
                        {
                            GameObject GooPlaneOld1 = MiscProps.transform.GetChild(2).gameObject;
                            GameObject GooPlaneOld2 = MiscProps.transform.GetChild(3).gameObject;
                            if (WaterFall)
                            {
                                WaterFall.transform.GetChild(8).GetComponent<DebuffZone>().buffType = SlowTar;
                                GooPlaneOld1.GetComponentInChildren<DebuffZone>().buffType = SlowTar;
                                GooPlaneOld2.GetComponentInChildren<DebuffZone>().buffType = SlowTar;
                            }
                        }     
                        //UnityEngine.RenderSettings.defaultReflectionMode

                        break;
                    case "meridian":
                        Run.instance.PickNextStageSceneFromCurrentSceneDestinations();
                        break;
                }
            }
            orig(self);
        }

 

        private static void Active_OnEnter(On.EntityStates.Missions.Arena.NullWard.Active.orig_OnEnter orig, EntityStates.Missions.Arena.NullWard.Active self)
        {
            orig(self);
            BuffWard[] ward = self.gameObject.GetComponents<BuffWard>();
            if (ward.Length == 2)
            {
                ward[0].enabled = false;
                ward[1].enabled = false;
            }
        }

        private static void WardOnAndReady_OnEnter(On.EntityStates.Missions.Arena.NullWard.WardOnAndReady.orig_OnEnter orig, EntityStates.Missions.Arena.NullWard.WardOnAndReady self)
        {
            orig(self);

            self.gameObject.transform.GetChild(3).GetChild(0).GetChild(0).localScale = new Vector3(1.5f, 3f, 1.5f);

            BuffWard[] ward = self.gameObject.GetComponents<BuffWard>();
            if (ward.Length == 2)
            {
                ward[0].enabled = true;
                ward[1].enabled = true;
            }
        }
 

        private static void ArenaMissionController_OnEnable(On.RoR2.ArenaMissionController.orig_OnStartServer orig, ArenaMissionController self)
        {
            orig(self);
            for (int i = 0; i < self.nullWards.Length; i++)
            {
                if (WConfig.FasterArenaCells.Value)
                {
                    if (i < 4)
                    {
                        self.nullWards[i].GetComponent<HoldoutZoneController>().baseChargeDuration = 30;
                    }
                    else if (i < 8)
                    {
                        self.nullWards[i].GetComponent<HoldoutZoneController>().baseChargeDuration = 45;
                    }
                    else
                    {
                    }
                }
                if (WConfig.RegenArenaCells.Value)
                {
                    BuffWard buffWard = self.nullWards[i].AddComponent<BuffWard>();
                    buffWard.enabled = false;
                    buffWard.radius = 5;
                    buffWard.buffDuration = 1.3f;
                    buffWard.buffDef = DLC1Content.Buffs.MushroomVoidActive;
                    buffWard = self.nullWards[i].AddComponent<BuffWard>();
                    buffWard.enabled = false;
                    buffWard.radius = 5;
                    buffWard.buffDuration = 1.3f;
                    buffWard.buffDef = RoR2Content.Buffs.CrocoRegen;
                }
               
            }
        }

        public static void MoreSceneCredits(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            if (WConfig.DCCSInteractablesStageCredits.Value)
            {
                if (Run.instance && SceneInfo.instance)
                {
                    switch (SceneInfo.instance.sceneDef.baseSceneName)
                    {
                        case "golemplains":
                            if (ConfigStages.Stage_1_Golem.Value)
                            { }
                            self.sceneDirectorInteractibleCredits += 20; //Rather buff these two a bit than nerf Snowy Ig we'll see
                            break;
                        case "blackbeach":
                            if (ConfigStages.Stage_1_Roost.Value)
                            {
                                self.sceneDirectorInteractibleCredits += 20; //
                            }
                            break;
                        case "goolake":
                            if (ConfigStages.Stage_2_Goolake.Value)
                            {
                                self.sceneDirectorInteractibleCredits += 20; //Has 60 less due to the Lemurians a bit too harsh imo
                            }
                            break;
                        case "sulfurpools":
                            if (ConfigStages.Stage_3_Sulfur.Value)
                            {
                                self.sceneDirectorInteractibleCredits += 20; //Hell stage
                            }
                            break;
                        case "rootjungle":
                            if (ConfigStages.Stage_4_Root_Jungle.Value)
                            {
                                self.sceneDirectorInteractibleCredits += 30; //Depths sometimes has 560 so raising the other two by 40 seems fine
                            }
                            break;
                        case "shipgraveyard":
                            if (ConfigStages.Stage_4_Ship.Value)
                            {
                                self.sceneDirectorInteractibleCredits += 30; //Depths has 400 or 560}
                            }
                            break;
                    }
                }
            }
            orig(self);
        }

        public static void GoldShoresCredits(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            if (Run.instance && SceneInfo.instance)
            {
                if (SceneInfo.instance.sceneDef.baseSceneName == "goldshores")
                {
                    self.sceneDirectorInteractibleCredits += 64; //For Fun ig
                }
            }
            orig(self);
        }
    }
}