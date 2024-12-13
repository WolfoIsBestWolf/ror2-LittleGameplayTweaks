using BepInEx;
using MonoMod.Cil;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

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

        public static bool AddMonsters = false;

        public void Awake()
        {
            WConfig.InitConfig();

            //bool stageAesth = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("HIFU.StageAesthetic");
            if (WConfig.cfgLoopWeather.Value)
            {
                Files.Init(Info);

                ChatMessageBase.chatMessageTypeToIndex.Add(typeof(SendSyncLoopWeather), (byte)ChatMessageBase.chatMessageIndexToType.Count);
                ChatMessageBase.chatMessageIndexToType.Add(typeof(SendSyncLoopWeather));

                Main_Variants.Start();
                RemoveVariantNames();

                //Add component and roll for first stage
                On.RoR2.Run.Start += AddRoll_StartOfRun;

                //Advances Roll and applies stuff
                //On.RoR2.Stage.Start += ApplyLoopWeatherChanges;
                //SceneDirector.onPrePopulateSceneServer += ApplyAndRollOnStage;
                On.RoR2.UI.AssignStageToken.Start += ApplyLoopNameChanges;
                On.RoR2.ClassicStageInfo.Start += RollForWeather;
                IL.RoR2.ClassicStageInfo.RebuildCards += ApplyWeatherWithDCCS;

                On.RoR2.Run.PickNextStageScene += Official_Variants_MainPath;
                On.RoR2.SceneExitController.IfLoopedUseValidLoopStage += Official_Variants_AltPath;
                On.RoR2.BazaarController.IsUnlockedBeforeLooping += Official_Variants_Bazaar;


             
            }
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

        public void Start()
        {
            if (WConfig.Monster_Additions.Value == WConfig.EnumEnemyAdds.Always)
            {
                AddMonsters = true;
            }
            else if (WConfig.Monster_Additions.Value == WConfig.EnumEnemyAdds.LittleGameplayTweaks)
            {
                AddMonsters = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Wolfo.LittleGameplayTweaks");
            }
            else if (WConfig.Monster_Additions.Value == WConfig.EnumEnemyAdds.Never)
            {
                AddMonsters = false;
            }
            Debug.Log("Variant Monsters : " + AddMonsters);
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
            else if(WConfig.Chance_Loop_2.Value > 0 && loopClearCountPlusOne > 1)
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
                    else if(WConfig.Chance_Loop_2.Value == 0)
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
 
        private void ApplyAndRollOnStage(SceneDirector obj)
        {
            if (NetworkServer.active)
            {
                ChooseIfNextStageLoop(true);
                ApplyLoopWeatherChanges(null);
            }
        }

        private bool Official_Variants_Bazaar(On.RoR2.BazaarController.orig_IsUnlockedBeforeLooping orig, BazaarController self, SceneDef sceneDef)
        {

            if (Run.instance)
            {
                SyncLoopWeather weather = Run.instance.GetComponent<SyncLoopWeather>();

                if (Run.instance.ruleBook.stageOrder == StageOrder.Normal)
                {
                    if (weather.NextStage_LoopVariant) //When In lobby gets rolled.
                    {
                        if (Run.instance.stageClearCount < 5 && WConfig.LoopVariant_OnPreLoop_Vanilla.Value)
                        {
                            return true;
                        }
                    }
                    else //Next stage NOT loop
                    {
                        //If Looped and Chance Failed, use pre Loop if config enables it.
                        if (Run.instance.stageClearCount > 4 && WConfig.PreLoopVariant_PostLoop_Vanilla.Value)
                        {
                            return false;
                        }
                    }
                }


            }

            return orig(self, sceneDef);
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
            if (RunArtifactManager.instance && RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.mixEnemyArtifactDef))
            {
                dccs = null;
            }
            if (dccs && dccs is FamilyDirectorCardCategorySelection)
            {
                dccs = null;
            }
            if (weather.CurrentStage_LoopVariant)
            {
                Debug.Log("Applying Weather for : " + SceneCatalog.mostRecentSceneDef.baseSceneName + " \n"+ dccs);
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
                                if(loopClearCountPlusOne < 1)
                                {
                                    ClassicStageInfo.instance.sceneDirectorInteractibleCredits -= 30;
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


        private void Official_Variants_MainPath(On.RoR2.Run.orig_PickNextStageScene orig, Run self, WeightedSelection<SceneDef> choices)
        {
            orig(self, choices);


            SyncLoopWeather weather = Run.instance.GetComponent<SyncLoopWeather>();
            if (!weather)
            {
                Debug.LogWarning("SyncLoopWeather wasn't added when the run started, How?");
                return;
            }
            if (!NetworkServer.active)
            {
                //This doesn't run on clients anyways
                return;
            }
            //Both variants already in random stage order
            if (self.ruleBook.stageOrder == StageOrder.Normal)
            {
                if (weather.NextStage_LoopVariant) //When In lobby gets rolled.
                {
                    if (self.stageClearCount < 5 && WConfig.LoopVariant_OnPreLoop_Vanilla.Value)
                    {
                        if (self.nextStageScene && self.nextStageScene.loopedSceneDef)
                        {
                            if (self.IsExpansionEnabled(self.nextStageScene.loopedSceneDef.requiredExpansion))
                            {
                                Debug.Log("Replacing " + self.nextStageScene + " with " + self.nextStageScene.loopedSceneDef);
                                self.nextStageScene = self.nextStageScene.loopedSceneDef;
                            }
                        }
                    }
                }
                else //Next stage NOT loop
                {
                    //If Looped and Chance Failed, use pre Loop if config enables it.
                    if (self.stageClearCount > 4 && WConfig.PreLoopVariant_PostLoop_Vanilla.Value)
                    {
                        if (loopSceneDefToNon.ContainsKey(self.nextStageScene.baseSceneName))
                        {
                            SceneDef preLoop = loopSceneDefToNon[self.nextStageScene.baseSceneName];
                            if (preLoop != null)
                            {
                                Debug.Log("Replacing " + self.nextStageScene + " with " + preLoop);
                                self.nextStageScene = preLoop;
                            }
                        }
                    }
                }
            }


        }

        private SceneDef Official_Variants_AltPath(On.RoR2.SceneExitController.orig_IfLoopedUseValidLoopStage orig, SceneExitController self, SceneDef sceneDef)
        {
            if (NetworkServer.active)
            {
                SyncLoopWeather weather = Run.instance.GetComponent<SyncLoopWeather>();
                if (Run.instance.ruleBook.stageOrder == StageOrder.Normal)
                {
                    if (weather.NextStage_LoopVariant) //When In lobby gets rolled.
                    {
                        if (Run.instance.stageClearCount < 5 && WConfig.LoopVariant_OnPreLoop_Vanilla.Value)
                        {
                            if (sceneDef.loopedSceneDef)
                            {
                                Debug.Log("Replacing " + sceneDef + " with " + sceneDef.loopedSceneDef);
                                return sceneDef.loopedSceneDef;
                            }
                        }
                    }
                    else //Next stage NOT loop
                    {
                        //If Looped and Chance Failed, use pre Loop if config enables it.
                        if (Run.instance.stageClearCount > 4 && WConfig.PreLoopVariant_PostLoop_Vanilla.Value)
                        {
                            if (loopSceneDefToNon.ContainsKey(sceneDef.baseSceneName))
                            {
                                Debug.Log("Replacing " + sceneDef.loopedSceneDef + " with " + sceneDef);
                                return sceneDef;
                            }
                        }
                    }
                }
            }


            return orig(self, sceneDef);
        }

        private void AddRoll_StartOfRun(On.RoR2.Run.orig_Start orig, Run self)
        {
            if (!self.gameObject.GetComponent<SyncLoopWeather>())
            {
                self.gameObject.AddComponent<SyncLoopWeather>();
            }
            if (NetworkServer.active)
            {
                ChooseIfNextStageLoop(false);
                //ChooseIfNextStageLoop(true);
            }
            orig(self);
        }

        public static void ChooseIfNextStageLoop(bool send)
        {
            //Ideally chosen before PickNextScene runs so we can replace destination SceneDefs
            if (NetworkServer.active)
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
                return (Run.instance.stageClearCount+1) / Run.stagesPerLoop;
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
                    if (!NetworkServer.active)
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