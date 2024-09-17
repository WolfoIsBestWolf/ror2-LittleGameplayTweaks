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
        public static BuffDef HiddenRegen;

        public static void Start()
        {
            if (WConfig.DCCSInteractablesStageCredits.Value)
            {
                On.RoR2.ClassicStageInfo.Start += MoreSceneCredits;
            }
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
            if (WConfig.FasterArenaCells.Value)
            {
                On.RoR2.ArenaMissionController.OnStartServer += ArenaMissionController_OnEnable;

                On.EntityStates.Missions.Arena.NullWard.WardOnAndReady.OnEnter += WardOnAndReady_OnEnter;
                On.EntityStates.Missions.Arena.NullWard.Active.OnEnter += Active_OnEnter;
            }

            GameObject DestinationPortal = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/PM DestinationPortal.prefab").WaitForCompletion();
            DestinationPortal.GetComponent<SceneExitController>().useRunNextStageScene = true;

            SceneDef meridian = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC2/meridian/meridian.asset").WaitForCompletion();
            meridian.destinationsGroup = Addressables.LoadAssetAsync<SceneCollection>(key: "RoR2/Base/SceneGroups/sgStage5.asset").WaitForCompletion();
            meridian.loopedDestinationsGroup = Addressables.LoadAssetAsync<SceneCollection>(key: "RoR2/Base/SceneGroups/loopSgStage5.asset").WaitForCompletion();

            LanguageAPI.Add("PORTAL_DESTINATION_HELMINTH", "Reject being Reborn", "en");



            RecalculateStatsAPI.GetStatCoefficients += VoidArenaStupidThing;
            HiddenRegen = ScriptableObject.CreateInstance<BuffDef>();
            HiddenRegen.name = "W_HiddenRegen";
            HiddenRegen.isHidden = false;
            HiddenRegen.ignoreGrowthNectar = true;
            R2API.ContentAddition.AddBuffDef(HiddenRegen);
        }

        private static void RunsAlways_ClassicStageInfo_Start(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            if (Run.instance && SceneInfo.instance)
            {
                //self.sceneDirectorInteractibleCredits += 550;

                switch (SceneInfo.instance.sceneDef.cachedName)
                {
                    //DccsPool dpFrozenWallInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/frozenwall/dpFrozenWallInteractables.asset").WaitForCompletion();

                    case "foggyswamp":
                        self.interactableDccsPool = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/foggyswamp/dpFoggySwampInteractables.asset").WaitForCompletion();
                        self.monsterDccsPool = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/foggyswamp/dpFoggySwampMonsters.asset").WaitForCompletion();
                        break;
                    case "blackbeach2":
                        self.interactableDccsPool = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/blackbeach/dpBlackBeachInteractables.asset").WaitForCompletion();
                        self.monsterDccsPool = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/blackbeach/dpBlackBeachMonsters.asset").WaitForCompletion();
                        break;
                    case "habitatfall":
                        self.interactableDccsPool = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/habitatfall/dpHabitatfallInteractables.asset").WaitForCompletion();
                        self.monsterDccsPool = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/habitatfall/dpHabitatfallMonsters.asset").WaitForCompletion();
                        break;
                    case "villagenight":
                        self.interactableDccsPool = null;
                        self.interactableCategories = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillagenightInteractablesDLC2.asset").WaitForCompletion();
                        break;
                    case "meridian":
                        Run.instance.PickNextStageSceneFromCurrentSceneDestinations();
                        break;
                }
            }
            orig(self);
        }

        private static void VoidArenaStupidThing(CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args)
        {
            bool flag = sender.inventory != null;
            if (flag)
            {
                if (sender.HasBuff(HiddenRegen))
                {
                    args.regenMultAdd += 5;
                }
            }
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

                BuffWard buffWard = self.nullWards[i].AddComponent<BuffWard>();
                buffWard.enabled = false;
                buffWard.radius = 5;
                buffWard.buffDuration = 1.5f;
                buffWard.buffDef = DLC1Content.Buffs.MushroomVoidActive;
                buffWard = self.nullWards[i].AddComponent<BuffWard>();
                buffWard.enabled = false;
                buffWard.radius = 5;
                buffWard.buffDuration = 1.5f;
                buffWard.buffDef = HiddenRegen;
            }
        }

        public static void MoreSceneCredits(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            if (Run.instance && SceneInfo.instance)
            {
                //self.sceneDirectorInteractibleCredits += 550;

                switch (SceneInfo.instance.sceneDef.baseSceneName)
                {
                    case "golemplains":
                        self.sceneDirectorInteractibleCredits += 20; //Rather buff these two a bit than nerf Snowy Ig we'll see
                        break;
                    case "blackbeach":
                        self.sceneDirectorInteractibleCredits += 20; //
                        break;
                    case "goolake":
                        self.sceneDirectorInteractibleCredits += 20; //Has 60 less due to the Lemurians a bit too harsh imo
                        break;
                    case "sulfurpools":
                        self.sceneDirectorInteractibleCredits += 30; //Hell stage
                        break;
                    case "rootjungle":
                        self.sceneDirectorInteractibleCredits += 30; //Depths sometimes has 560 so raising the other two by 40 seems fine
                        break;
                    case "shipgraveyard":
                        self.sceneDirectorInteractibleCredits += 30; //Depths has 400 or 560
                        break;
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