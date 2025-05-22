using BepInEx;
using MonoMod.Cil;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LoopVariants
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("Wolfo.LoopVariants", "WolfosLoopVariants", "1.4.1")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]


    public class LoopVariantsMain : BaseUnityPlugin
    {
        public static ExpansionDef DLC2 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC2/Common/DLC2.asset").WaitForCompletion();

        public static Dictionary<string, SceneDef> loopSceneDefToNon = new Dictionary<string, SceneDef>();

        //public static List<string> ExistingVariants = new List<string>() { "wispgraveyard", "golemplains", "goolake", "dampcavesimple", "snowyforest", "helminthroost", "foggyswamp", "rootjungle", "sulfurpools", "lemuriantemple", "ancientloft" };
        public static List<string> DisabledVariants = new List<string>();

        public static bool AddMonsters_ = false;
        public static bool AddMonsters
        {
            get
            {
                return AddMonsters_ && EnableNewContent;
            }
            set
            {
                AddMonsters_ = value;
            }
        }
        public static bool GameplayChanges
        {
            get
            {
                return WConfig.cfgGameplayChanges.Value && EnableNewContent;
            }
        }
        public static bool EnableNewContent = false;
        public static bool HostHasMod_ = false;
        public static bool HostHasMod
        {
            get
            {
                return NetworkServer.active || HostHasMod_;
            }
            set
            {
                HostHasMod_ = value;
            }
        }

        public static bool ShouldRollForBool
        {
            get
            {
                //If Host doesn't have mod, Clients roll for themselves
                if (HostHasMod == false && NetworkServer.active == false)
                {
                    return true;
                }
                return NetworkServer.active;
            }
        }


        public void Awake()
        {
            WConfig.InitConfig();
            EnableNewContent = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Wolfo.RequiredByAllCheck");

            Files.Init(Info);



            Main_Variants.Start();
            OfficialVariant.Awake();
            RemoveVariantNames();

            //Add component and roll for first stage
            On.RoR2.Run.Start += AddRoll_StartOfRun;

            //Advances Roll and applies stuff
            On.RoR2.UI.AssignStageToken.Start += ApplyLoopNameChanges;
            On.RoR2.ClassicStageInfo.Start += RollForWeather;
            IL.RoR2.ClassicStageInfo.RebuildCards += ApplyWeatherWithDCCS;


            ChatMessageBase.chatMessageTypeToIndex.Add(typeof(SendSyncLoopWeather), (byte)ChatMessageBase.chatMessageIndexToType.Count);
            ChatMessageBase.chatMessageIndexToType.Add(typeof(SendSyncLoopWeather));
            ChatMessageBase.chatMessageTypeToIndex.Add(typeof(HostHasModAlert), (byte)ChatMessageBase.chatMessageIndexToType.Count);
            ChatMessageBase.chatMessageIndexToType.Add(typeof(HostHasModAlert));
            WConfig.Monster_Additions_SettingChanged(null, null);


        }



        private void RollForWeather(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            try
            {
                ChooseIfNextStageLoop(true);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            orig(self);

        }



        public static bool RollForLoopWeather(SyncLoopWeather weather)
        {
            bool useLoopChance = false;
            bool useLoop2Chance = false;



            if (WConfig.Alternate_Chances.Value)
            {
                if (loopClearCountPlusOne % 2 == 1)
                {
                    useLoopChance = true;
                }
            }
            else if (WConfig.Chance_Loop_2.Value > 0 && loopClearCountPlusOne > 1)
            {
                useLoop2Chance = true;
            }
            else
            {
                useLoopChance = loopClearCountPlusOne > 0;
            }
            if (Run.instance.ruleBook.stageOrder == StageOrder.Random)
            {
                useLoopChance = Util.CheckRoll(50, null);
                useLoop2Chance = false;
            }
            else if (weather)
            {
                if (Run.instance.loopClearCount > 1)
                {
                    if (WConfig.Chance_Loop_2.Value >= 100)
                    {
                        weather.CurrentStage_LoopVariant = true;
                    }
                    else if (WConfig.Chance_Loop_2.Value == 0)
                    {
                        weather.CurrentStage_LoopVariant = false;
                    }
                }
                else if (Run.instance.loopClearCount == 1)
                {
                    if (WConfig.Chance_Loop.Value >= 100)
                    {
                        weather.CurrentStage_LoopVariant = true;
                    }
                    else if (WConfig.Chance_Loop.Value == 0)
                    {
                        weather.CurrentStage_LoopVariant = false;
                    }
                }
                else
                {
                    if (WConfig.Chance_PreLoop.Value >= 100)
                    {
                        weather.CurrentStage_LoopVariant = true;
                    }
                    else if (WConfig.Chance_PreLoop.Value == 0)
                    {
                        weather.CurrentStage_LoopVariant = false;
                    }
                }
            }

            /*
            Debug.Log(useLoopChance);
            Debug.Log(useLoop2Chance);
            Debug.Log(Run.instance.stageClearCount);
            Debug.Log(loopClearCountPlusOne);
            Debug.Log(weather.CurrentStage_LoopVariant);
            */


            if (useLoop2Chance)
            {
                return Util.CheckRoll(WConfig.Chance_Loop_2.Value, null);
            }
            else if (useLoopChance)
            {
                return Util.CheckRoll(WConfig.Chance_Loop.Value, null);
            }
            else
            {
                return Util.CheckRoll(WConfig.Chance_PreLoop.Value, null);
            }
        }


        private void ApplyWeatherWithDCCS(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.Before,
            x => x.MatchStfld("RoR2.ClassicStageInfo", "modifiableMonsterCategories")))
            {
                Debug.Log(c);
                c.EmitDelegate<Func<DirectorCardCategorySelection, DirectorCardCategorySelection>>((dccs) =>
                {
                    ApplyLoopWeatherChanges(dccs);
                    return dccs;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: AddVariantExclusiveMonsters");
            }
        }



        private static void ApplyLoopWeatherChanges(DirectorCardCategorySelection dccs)
        {
            if (!Run.instance)
            {
                return;
            }
            SyncLoopWeather weather = Run.instance.GetComponent<SyncLoopWeather>();
            if (!weather)
            {
                Debug.LogWarning("No Weather Data");
                return;
            }
            if (weather.Applied_To_Stage)
            {
                Debug.Log("Already applied weather this stage");
                return;
            }
            Debug.Log("Loop weather for curr " + weather.CurrentStage_LoopVariant);
            Debug.Log("Loop weather for next " + weather.NextStage_LoopVariant);

            //Kin works fine
            //Force monsters work fines
            //Disso would would fine, but might as well not do it for optimization
            //Family does not work fine, have to filter.


            if (weather.CurrentStage_LoopVariant)
            {
                Debug.Log("Applying Weather for : " + SceneCatalog.mostRecentSceneDef.baseSceneName + " \n" + dccs);
                weather.Applied_To_Stage = true;
                try
                {
                    switch (SceneInfo.instance.sceneDef.baseSceneName)
                    {
                        case "golemplains":
                            if (WConfig.Stage_1_Golem.Value)
                            {
                                Variants_1_GolemPlains.LoopWeather();
                            }
                            break;
                        case "blackbeach":
                            if (WConfig.WIP.Value)
                            {
                                Variants_1_BlackBeach.LoopWeather();
                            }
                            break;
                        case "snowyforest":
                            if (WConfig.Stage_1_Snow.Value)
                            {
                                Variants_1_SnowyForest.LoopWeather();
                                if (WConfig.Enemy_1_Snow.Value)
                                {
                                    Variants_1_SnowyForest.AddVariantMonsters(dccs);
                                }
                            }
                            break;
                        case "lakesnight":
                            if (WConfig.Stage_1_LakesVillageNight.Value)
                            {
                                LakesNightVillageNight.LakesNight_AddStage1FriendlyMonsters(dccs);
                            }
                            break;
                        case "villagenight":
                            if (WConfig.Stage_1_LakesVillageNight.Value)
                            {
                                LakesNightVillageNight.VillageNight_AddStage1FriendlyMonsters(dccs);
                            }
                            if (WConfig.Stage_1_VillageNight_Credits.Value)
                            {
                                if (loopClearCountPlusOne < 1)
                                {
                                    ClassicStageInfo.instance.sceneDirectorInteractibleCredits -= 40;
                                }
                            }
                            break;
                        case "goolake":
                            if (WConfig.Stage_2_Goolake.Value)
                            {
                                Variants_2_Goolake.LoopWeather();
                            }
                            break;
                        case "foggyswamp":
                            if (WConfig.Stage_2_Swamp.Value)
                            {
                                Variants_2_FoggySwamp.LoopWeather();
                            }
                            break;
                        case "ancientloft":
                            if (WConfig.Stage_2_Ancient.Value)
                            {
                                Variants_2_AncientLoft.LoopWeather();
                                if (WConfig.Enemy_2_Ancient.Value)
                                {
                                    Variants_2_AncientLoft.AddVariantMonsters(dccs);
                                }
                            }
                            break;
                        case "lemuriantemple":
                            if (WConfig.Stage_2_Temple.Value)
                            {
                                Variants_2_LemurianTemple.LoopWeather();
                            }
                            break;
                        case "frozenwall":
                            if (WConfig.WIP.Value)
                            {
                                Variants_3_FrozenWall.LoopWeather();
                            }
                            break;
                        case "wispgraveyard":
                            if (WConfig.Stage_3_Wisp.Value)
                            {
                                Variants_3_WispGraveyard.LoopWeather();
                                if (WConfig.Enemy_3_Wisp.Value)
                                {
                                    Variants_3_WispGraveyard.AddVariantMonsters(dccs);
                                }
                            }
                            break;
                        case "sulfurpools":
                            if (WConfig.Stage_3_Sulfur.Value)
                            {
                                Variants_3_Sulfur.LoopWeather();
                            }
                            break;
                        case "dampcavesimple":
                            if (WConfig.Stage_4_Damp_Abyss.Value)
                            {
                                Variants_4_DampCaveSimpleAbyss.LoopWeather();
                                if (WConfig.Enemy_4_Damp_Abyss.Value)
                                {
                                    Variants_4_DampCaveSimpleAbyss.AddVariantMonsters(dccs);
                                }
                            }
                            break;
                        case "shipgraveyard":
                            if (WConfig.WIP.Value)
                            {
                                Variants_4_ShipGraveyard.LoopWeather();
                            }
                            break;
                        case "rootjungle":
                            if (WConfig.Stage_4_Root_Jungle.Value)
                            {
                                Variants_4_RootJungle.LoopWeather();
                                if (WConfig.Enemy_4_Root_Jungle.Value)
                                {
                                    Variants_4_RootJungle.AddVariantMonsters(dccs);
                                }
                            }
                            break;
                        case "skymeadow":
                            if (WConfig.WIP.Value)
                            {
                                Variants_5_SkyMeadow.LoopWeather();
                            }
                            break;
                        case "helminthroost":
                            if (WConfig.Stage_5_Helminth.Value)
                            {
                                Variants_5_HelminthRoost.LoopWeather();
                                if (WConfig.Enemy_5_Helminth.Value)
                                {
                                    Variants_5_HelminthRoost.AddVariantMonsters(dccs);
                                }
                            }
                            break;
                        case "moon2":
                            if (WConfig.WIP.Value)
                            {
                                Variants_6_Moon.LoopWeather();
                            }
                            break;
                        case "meridian":
                            if (WConfig.Stage_6_Meridian.Value)
                            {
                                Variants_6_Meridian.LoopWeather();
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning("LoopVariants Error: " + e);
                }

            }


        }




        private void AddRoll_StartOfRun(On.RoR2.Run.orig_Start orig, Run self)
        {
            if (!self.gameObject.GetComponent<SyncLoopWeather>())
            {
                self.gameObject.AddComponent<SyncLoopWeather>();
            }
            if (ShouldRollForBool)
            {
                ChooseIfNextStageLoop(false);
                //ChooseIfNextStageLoop(true);
            }
            orig(self);
        }

        public static void ChooseIfNextStageLoop(bool send)
        {

            if (ShouldRollForBool)
            {
                if (!Run.instance)
                {
                    Debug.LogWarning("Called without a Run Instance");
                    return;
                }

                SyncLoopWeather weather = Run.instance.GetComponent<SyncLoopWeather>();
                if (!weather)
                {
                    Debug.LogWarning("SyncLoopWeather wasn't added when the run started, How?");
                    weather = Run.instance.gameObject.AddComponent<SyncLoopWeather>();
                }

                weather.Applied_To_Stage = false;

                //bool Current_Use_Loop = false;
                //Current_Use_Loop = weather.NextStage_LoopVariant;
                weather.CurrentStage_LoopVariant = weather.NextStage_LoopVariant;

                bool Next = RollForLoopWeather(weather);


                //While this gets called before a run
                //The time it takes to send, and recieve the message is too long
                //To replace Stage 1 Verdant with Viscious
                weather.NextStage_LoopVariant = Next;
                if (send)
                {
                    RoR2.Chat.SendBroadcastChat(new SendSyncLoopWeather
                    {
                        CURRENT = weather.CurrentStage_LoopVariant,
                        NEXT = Next
                    });
                }

            }


        }

        public static void RemoveVariantNames()
        {

            if (!WConfig.Stage_1_Golem.Value)
            {
                DisabledVariants.Add("golemplains");
            }
            if (WConfig.Stage_1_Roost != null && !WConfig.Stage_1_Roost.Value)
            {
                DisabledVariants.Add("blackbeach");
            }
            if (!WConfig.Stage_1_Snow.Value)
            {
                DisabledVariants.Add("snowyforest");
            }
            //
            if (!WConfig.Stage_2_Goolake.Value)
            {
                DisabledVariants.Add("goolake");
            }
            if (!WConfig.Stage_2_Swamp.Value)
            {
                DisabledVariants.Add("foggyswamp");
            }
            if (!WConfig.Stage_2_Ancient.Value)
            {
                DisabledVariants.Add("ancientloft");
            }
            if (!WConfig.Stage_2_Temple.Value)
            {
                DisabledVariants.Add("lemuriantemple");
            }
            //
            if (WConfig.Stage_3_Frozen != null && !WConfig.Stage_3_Frozen.Value)
            {
                DisabledVariants.Add("frozenwall");
            }
            if (!WConfig.Stage_3_Wisp.Value)
            {
                DisabledVariants.Add("wispgraveyard");
            }
            if (!WConfig.Stage_3_Sulfur.Value)
            {
                DisabledVariants.Add("sulfurpools");
            }
            //
            if (!WConfig.Stage_4_Damp_Abyss.Value)
            {
                DisabledVariants.Add("dampcavesimple");
            }
            if (WConfig.Stage_4_Ship != null && !WConfig.Stage_4_Ship.Value)
            {
                DisabledVariants.Add("shipgraveyard");
            }
            if (!WConfig.Stage_4_Root_Jungle.Value)
            {
                DisabledVariants.Add("rootjungle");
            }
            //
            if (WConfig.Stage_5_Sky != null && !WConfig.Stage_5_Sky.Value)
            {
                DisabledVariants.Add("skymeadow");
            }
            if (!WConfig.Stage_5_Helminth.Value)
            {
                DisabledVariants.Add("helminthroost");
            }
            //
            if (WConfig.Stage_6_Commencement != null && !WConfig.Stage_6_Commencement.Value)
            {
                DisabledVariants.Add("moon2");
            }
            if (!WConfig.Stage_6_Meridian.Value)
            {
                DisabledVariants.Add("meridian");
            }
        }


        private static void ApplyLoopNameChanges(On.RoR2.UI.AssignStageToken.orig_Start orig, RoR2.UI.AssignStageToken self)
        {
            orig(self);
            if (WConfig.Name_Changes.Value)
            {
                if (Run.instance)
                {
                    SyncLoopWeather weather = Run.instance.GetComponent<SyncLoopWeather>();
                    if (weather)
                    {
                        if (weather.CurrentStage_LoopVariant)
                        {
                            SceneDef mostRecentSceneDef = SceneCatalog.mostRecentSceneDef;
                            //If token exists
                            Language language2 = Language.FindLanguageByName("en");
                            if (language2.stringsByToken.ContainsKey(mostRecentSceneDef.nameToken + "_LOOP"))
                            {
                                if (!DisabledVariants.Contains(mostRecentSceneDef.baseSceneName))
                                {
                                    self.titleText.SetText(Language.GetString(mostRecentSceneDef.nameToken + "_LOOP"), true);
                                    self.subtitleText.SetText(Language.GetString(mostRecentSceneDef.subtitleToken + "_LOOP"), true);
                                }
                            }
                        }
                    }
                }
            }
        }


        public static int loopClearCountPlusOne
        {
            get
            {
                if (!Run.instance)
                {
                    return 0;
                }
                return (Run.instance.stageClearCount + 1) / Run.stagesPerLoop;
            }
        }

        public class HostHasModAlert : ChatMessageBase
        {
            public override string ConstructChatString()
            {
                HostHasMod = true;
                Debug.Log("WolfoLoopWeather Host recognizer | Host has mod");
                return null;
            }
        }

        public class SendSyncLoopWeather : ChatMessageBase
        {
            public override string ConstructChatString()
            {
                SyncLoopWeather weather = Run.instance.GetComponent<SyncLoopWeather>();
                if (weather)
                {
                    weather.CurrentStage_LoopVariant = CURRENT;
                    weather.NextStage_LoopVariant = NEXT;
                    if (HostHasMod && !NetworkServer.active)
                    {
                        weather.Applied_To_Stage = false;
                        ApplyLoopWeatherChanges(null);
                    }
                }
                else
                {
                    Debug.LogWarning("Sent SendSyncLoopWeather without SyncLoopWeather");
                }
                return null;
            }

            public bool CURRENT;
            public bool NEXT;


            public override void Serialize(NetworkWriter writer)
            {
                base.Serialize(writer);
                writer.Write(CURRENT);
                writer.Write(NEXT);
            }

            public override void Deserialize(NetworkReader reader)
            {
                base.Deserialize(reader);
                CURRENT = reader.ReadBoolean();
                NEXT = reader.ReadBoolean();
            }

        }

        public class SyncLoopWeather : MonoBehaviour
        {
            public bool Applied_To_Stage;
            public bool CurrentStage_LoopVariant;
            public bool NextStage_LoopVariant;
        }
    }

}