using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class Changes_Stages
    {
        public static void Start()
        {
            On.RoR2.ClassicStageInfo.Start += MoreSceneCredits;
            On.RoR2.ClassicStageInfo.Start += RunsAlways_ClassicStageInfo_Start;

            //Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/voidstage/voidstage.asset").WaitForCompletion().sceneType = Arena.sceneType;
            //SceneDef voidRaid = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/voidraid/voidraid.asset").WaitForCompletion();
            //voidRaid.sceneType = SceneType.UntimedStage; //Whatever
            //voidRaid.allowItemsToSpawnObjects = true;


            /*On.RoR2.ArenaMissionController.OnStartServer += ArenaMissionController_OnEnable;
            On.EntityStates.Missions.Arena.NullWard.WardOnAndReady.OnEnter += WardOnAndReady_OnEnter;
            On.EntityStates.Missions.Arena.NullWard.Active.OnEnter += Active_OnEnter;*/

            if (ConfigStages.Stage_F_Moon.Value)
            {
                On.RoR2.EscapeSequenceController.OnEnable += EscapeSequenceController_OnEnable;
            }

            On.RoR2.BazaarController.SetUpSeerStations += ThirdSeerNew;

            On.RoR2.HoldoutZoneController.Start += OnlyRequireOnePlayer;
        }

        private static void OnlyRequireOnePlayer(On.RoR2.HoldoutZoneController.orig_Start orig, HoldoutZoneController self)
        {
            orig(self);
            //Drop Ship
            //Arena cells
            string rootName = self.transform.root.name;
            if (rootName.StartsWith("Moon") || rootName.StartsWith("Aren"))
            {
                self.playerCountScaling = 0;
            }
            /*else if (self.name.EndsWith("Mass"))
            {
                if (WConfig.cfgMasstweak.Value)
                {
                    self.GetComponent<CombatDirector>().monsterCredit += 140; //20% less charge 20% more credits
                    self.baseChargeDuration = Mathf.Min(self.baseChargeDuration, 48);
                    self.baseRadius += 2;
                }
            }*/
        }

     

        public static void ThirdSeerNew(On.RoR2.BazaarController.orig_SetUpSeerStations orig, BazaarController self)
        {
            GameObject newSeerObject = null;
            SeerStationController newSeer = null;
            bool doIt = WConfig.ThirdLunarSeer.Value && NetworkServer.active && self.seerStations.Length == 2;
            if (doIt)
            {
                SeerStationController oldSeer = self.seerStations[0].GetComponent<SeerStationController>();

                newSeerObject = GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/bazaar/SeerStation.prefab").WaitForCompletion(), self.seerStations[0].gameObject.transform.parent);
                newSeerObject.transform.localPosition = new Vector3(-45.9807f, -15.22f, 9.5654f);
                newSeerObject.transform.localRotation = new Quaternion(0f, 0.7772f, 0f, 0.6293f);
                newSeer = newSeerObject.GetComponent<SeerStationController>();
                newSeer.explicitTargetSceneExitController = oldSeer.explicitTargetSceneExitController;
                HG.ArrayUtils.ArrayAppend(ref self.seerStations, newSeer); //Let the game take care of it itself
                NetworkServer.Spawn(newSeerObject);

            }
            orig(self);
        }

        public static void EscapeSequenceController_OnEnable(On.RoR2.EscapeSequenceController.orig_OnEnable orig, EscapeSequenceController self)
        {
            orig(self);
            self.gameObject.transform.GetChild(0).GetChild(1).GetComponent<CombatDirector>().monsterCards = DCCS_Family.dccsMoonVoids;
        }


        public static void RunsAlways_ClassicStageInfo_Start(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
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
                }
            }
            orig(self);
        }



        /*public static void Active_OnEnter(On.EntityStates.Missions.Arena.NullWard.Active.orig_OnEnter orig, EntityStates.Missions.Arena.NullWard.Active self)
        {
            orig(self);
            if (WConfig.RegenArenaCells.Value)
            {
                BuffWard[] ward = self.gameObject.GetComponents<BuffWard>();
                if (ward.Length == 2)
                {
                    ward[0].enabled = false;
                    ward[1].enabled = false;
                }
            }
        }*/

        /*public static void WardOnAndReady_OnEnter(On.EntityStates.Missions.Arena.NullWard.WardOnAndReady.orig_OnEnter orig, EntityStates.Missions.Arena.NullWard.WardOnAndReady self)
        {
            orig(self);
            if (WConfig.RegenArenaCells.Value)
            {
                BuffWard[] ward = self.gameObject.GetComponents<BuffWard>();
                if (ward.Length == 2)
                {
                    ward[0].enabled = true;
                    ward[1].enabled = true;
                }
            }
        }*/


        /*public static void ArenaMissionController_OnEnable(On.RoR2.ArenaMissionController.orig_OnStartServer orig, ArenaMissionController self)
        {
            orig(self);
            for (int i = 0; i < self.nullWards.Length; i++)
            {
                if (WConfig.FasterArenaCells.Value)
                {
                    if (i < 4)
                    {
                        self.nullWards[i].GetComponent<HoldoutZoneController>().baseChargeDuration = 40;
                    }
                    else if (i < 8)
                    {
                        self.nullWards[i].GetComponent<HoldoutZoneController>().baseChargeDuration = 50;
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
        }*/

        public static void MoreSceneCredits(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            //Idk why we do this prior to Start but whatever
            if (SceneInfo.instance)
            {
                SceneDef def = SceneInfo.instance.sceneDef;
                if (WConfig.cfgStageCredits_Interactables.Value)
                {
                    switch (def.baseSceneName)
                    {
                        case "lakesnight":
                            //self.sceneDirectorInteractibleCredits += 10;
                            break;
                        case "golemplains":
                            self.sceneDirectorInteractibleCredits += 20; //Rather buff these two a bit than nerf Snowy Ig we'll see
                            break;
                        case "blackbeach":
                            self.sceneDirectorInteractibleCredits += 20; //
                            break;
                        case "goolake":
                            //Flat -60 instead of having the -60 scale in multiplayer
                            self.sceneDirectorInteractibleCredits = 280;
                            HG.ArrayUtils.ArrayAppend(ref self.bonusInteractibleCreditObjects,
                               new ClassicStageInfo.BonusInteractibleCreditObject
                               {
                                   points = -60,
                                   objectThatGrantsPointsIfEnabled = RoR2.Run.instance.gameObject
                               });
                            break;
                        case "rootjungle":
                        case "shipgraveyard":
                            HG.ArrayUtils.ArrayAppend(ref self.bonusInteractibleCreditObjects,
                            new ClassicStageInfo.BonusInteractibleCreditObject
                            {
                                points = 30,
                                objectThatGrantsPointsIfEnabled = RoR2.Run.instance.gameObject
                            });
                            break;
                        case "goldshores":
                            if (WConfig.cfgGoldShoresCredits.Value)
                            {
                                self.sceneDirectorInteractibleCredits += 60; //For Fun ig
                            }
                            break;
                    }
                }
                if (self.sceneDirectorMonsterCredits > 0)
                {
                    //Debug.Log("SceneDirector Monster Credits Pre : " + self.sceneDirectorMonsterCredits);
                    if (WConfig.cfgStageCredits_Monsters.Value)
                    {
                        switch (def.stageOrder)
                        {
                            case 1:
                            case 2:
                            case 3:
                                self.sceneDirectorMonsterCredits = (int)(self.sceneDirectorMonsterCredits * 1.2f);
                                break;
                        }
                    }
                    if (WConfig.cfgMonsterCreditLoopScale.Value && def.stageOrder < 6)
                    {
                        self.sceneDirectorMonsterCredits += Run.instance.loopClearCount * 100;
                    }
                    //Debug.Log("SceneDirector Monster Credits Post: " + self.sceneDirectorMonsterCredits);

                }

            }
            orig(self);
        }

    }
}