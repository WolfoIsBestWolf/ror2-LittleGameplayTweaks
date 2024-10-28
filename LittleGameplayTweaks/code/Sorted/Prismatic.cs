using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittleGameplayTweaks
{
    public class Prismatic
    {
        static readonly System.Random random = new System.Random();

        public static EquipmentIndex[] newBossAffixes = Array.Empty<EquipmentIndex>();

        public static RuleDef RuleDefPrismatic;
        public static List<RuleDef> ChangedDefaultDefs = new List<RuleDef>();
        public static List<int> ChangedDefaultIndex = new List<int>();

        public static void Start()
        {
            Texture2D texPrismRuleOn = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texPrismRuleOn.LoadImage(Properties.Resources.texPrismRuleOn, true);
            texPrismRuleOn.wrapMode = TextureWrapMode.Clamp;
            Sprite texPrismRuleOnS = Sprite.Create(texPrismRuleOn, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));

            RiskOfOptions.ModSettingsManager.SetModIcon(texPrismRuleOnS);

            Texture2D texPrismRuleOff = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texPrismRuleOff.LoadImage(Properties.Resources.texPrismRuleOff, true);
            texPrismRuleOn.wrapMode = TextureWrapMode.Clamp;
            Sprite texPrismRuleOffS = Sprite.Create(texPrismRuleOff, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));


            RuleDefPrismatic = new RuleDef("Misc.PrismaticTrialExtended", "End Prismatic Trial Early");
            RuleChoiceDef ruleChoiceDef5 = RuleDefPrismatic.AddChoice("Endless", true, true);
            ruleChoiceDef5.tooltipNameToken = "RULE_PRISMATIC_NEW_NAME";
            ruleChoiceDef5.tooltipBodyToken = "RULE_PRISMATIC_NEW_DESC";
            ruleChoiceDef5.tooltipNameColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.VoidCoin);
            ruleChoiceDef5.onlyShowInGameBrowserIfNonDefault = true;
            ruleChoiceDef5.sprite = texPrismRuleOnS;
            ruleChoiceDef5.selectionUISound = "Play_UI_artifactSelect";
            RuleDefPrismatic.MakeNewestChoiceDefault();
            RuleChoiceDef ruleChoiceDef4 = RuleDefPrismatic.AddChoice("Stage2", false, true);
            ruleChoiceDef4.tooltipNameToken = "RULE_PRISMATIC_OLD_NAME";
            ruleChoiceDef4.tooltipBodyToken = "RULE_PRISMATIC_OLD_DESC";
            ruleChoiceDef4.tooltipNameColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.VoidCoin);
            ruleChoiceDef4.onlyShowInGameBrowserIfNonDefault = true;
            ruleChoiceDef4.sprite = texPrismRuleOffS;
            ruleChoiceDef4.selectionUISound = "Play_UI_artifactSelect";


            RuleCatalog.availability.CallWhenAvailable(LateRunningMethod);
            //
            GameEndingDef Ending = RoR2.LegacyResourcesAPI.Load<GameEndingDef>("GameEndingDefs/PrismaticTrialEnding");
            Ending.backgroundColor = new Color(0.7f, 0.3f, 0.7f, 0.615f);
            Ending.foregroundColor = new Color(0.9f, 0.6f, 0.9f, 0.833f);
            Ending.endingTextToken = "ACHIEVEMENT_COMPLETEPRISMATICTRIAL_NAME";
            //

            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/gamemodes/WeeklyRun").GetComponent<WeeklyRun>().userPickable = true;
            //RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/gamemodes/WeeklyRun").GetComponent<WeeklyRun>().crystalCount = WConfig.PrismaticTrialsCrystalsTotal.Value;
            //RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/gamemodes/WeeklyRun").GetComponent<WeeklyRun>().crystalsRequiredToKill = WConfig.PrismaticTrialsCrystalsNeeded.Value;

            //
            //Circumventing run ending early
            On.RoR2.WeeklyRun.AdvanceStage += WeeklyRun_AdvanceStage;

            //Always gets set to only Overloading/Blazing at the start of the run so you can't set it in the prefab
            On.RoR2.WeeklyRun.Start += (orig, self) =>
            {
                orig(self);
                if (self.ruleBook.GetRuleChoice(RuleDefPrismatic).localName.Equals("Endless"))
                {
                    self.bossAffixes = newBossAffixes;
                }
            };

            //Vengance, maybe other things still instantly charge the teleporter
            On.RoR2.WeeklyRun.OnServerBossDefeated += (orig, self, bossGroup) =>
            {
                if (bossGroup.name.StartsWith("Teleporter1") || bossGroup.name.StartsWith("LunarTeleporter"))
                {
                    orig(self, bossGroup);
                }
                else
                {
                    //Pretty sure this calls the main method, which then gets overwrriten by this again which causes and endless loop
                    //Idk what to do as a replacement for base.
                    //(self as Run).OnServerBossDefeated(bossGroup);
                }
            };


            //All Bosses on stage 2 and beyond are set to be Elites
            On.RoR2.WeeklyRun.OnServerBossAdded += (orig, self, bossGroup, characterMaster) =>
            {
                if (bossGroup.name.StartsWith("ScavLunar"))
                {
                    return;
                }
                orig(self, bossGroup, characterMaster);

                if (self.ruleBook.GetRuleChoice(RuleDefPrismatic).localName.Equals("Endless"))
                {
                    if (SceneInfo.instance.sceneDef.baseSceneName == "artifactworld")
                    {
                        characterMaster.inventory.SetEquipmentIndex(EquipmentIndex.None);
                    }

                    if (characterMaster.inventory.GetItemCount(RoR2Content.Items.AdaptiveArmor) > 0)
                    {
                        EquipmentIndex equip = characterMaster.inventory.currentEquipmentIndex;
                        if (equip == RoR2Content.Equipment.AffixBlue.equipmentIndex || equip == RoR2Content.Equipment.AffixRed.equipmentIndex)
                        {
                            characterMaster.inventory.RemoveItem(RoR2Content.Items.BoostDamage, (int)(characterMaster.inventory.GetItemCount(RoR2Content.Items.BoostDamage) * 0.35f));
                        }
                        else if (equip == DLC1Content.Equipment.EliteVoidEquipment.equipmentIndex)
                        {
                            characterMaster.inventory.RemoveItem(RoR2Content.Items.BoostHp, 5);
                            characterMaster.inventory.RemoveItem(RoR2Content.Items.BoostDamage, (int)(characterMaster.inventory.GetItemCount(RoR2Content.Items.BoostDamage) * 0.60f));
                        }
                    }

                    //With Rule only force fake Elite bosses after a certain stage
                    if (self.stageClearCount < 5)
                    {
                        if (characterMaster.inventory.GetItemCount(RoR2Content.Items.BoostHp) == 5)
                        {
                            if (self.stageClearCount < 3)
                            {
                                characterMaster.inventory.SetEquipmentIndex(EquipmentIndex.None);
                            }
                            characterMaster.inventory.RemoveItem(RoR2Content.Items.BoostHp, 5);
                            characterMaster.inventory.RemoveItem(RoR2Content.Items.BoostDamage, 1);
                        }
                    }
                    else
                    {
                        /*
                        //Elite Bosses would start spawning anyways we don't gotta force it, it's hard enough
                        float health = (self.stageClearCount - 3) * 6.67f - 5;
                        float damage = (self.stageClearCount - 3) * 6.67f - 1;
                        damage /= 4;

                        characterMaster.inventory.GiveItem(RoR2Content.Items.BoostHp, (int)health);
                        characterMaster.inventory.GiveItem(RoR2Content.Items.BoostDamage, (int)damage);
                        Debug.Log("Prismatic Trial Boss : BoostHP" + characterMaster.inventory.GetItemCount(RoR2Content.Items.BoostHp) + "  BoostDmg:"+characterMaster.inventory.GetItemCount(RoR2Content.Items.BoostDamage));
                        */
                    }
                }
            };

            //Move Modifier Category up
            On.RoR2.UI.RuleBookViewer.Start += RuleBookViewer_Start;

            //Change Death Message Top Right to mention the game mode
            On.RoR2.RunReport.PlayerInfo.Generate += PlayerInfo_Generate;


            //LanguageAPI.Add("TITLE_WEEKLY", "Prismatic Trial", "en");
            //LanguageAPI.Add("GAMEMODE_WEEKLY_RUN_NAME", "Prismatic\nTrial", "en");
            LanguageAPI.Add("GAMEMODE_WEEKLY_RUN_NAME", "Prismatic", "en");
            LanguageAPI.Add("GAMEMODE_WEEKLY_RUN_NAME", "Prismatisch", "de");
            //Rewrite these
            LanguageAPI.Add("TITLE_WEEKLY_DESC", "Play a Prismatic Trial, a different take on the normal run", "en");
            LanguageAPI.Add("WEEKLY_RUN_DESCRIPTION", "<style=cWorldEvent>'LittleGameplayTweaks'</style> changes <style=cWorldEvent>Prismatic Trials</style> to serve more as an alternative game mode.\n\n" +
                                                        "To beat the <style=cWorldEvent>Prismatic Trial</style>, you must break <style=cWorldEvent>Time Crystals</style> before activating the teleporter. Crystals will reward 30$ scaling with time. Teleporters fully charge upon defeating the Boss.\n\n" +
                                                        "These Prismatic Trials will have normal stage order and won't end after stage 2. All bosses will be given a random elite aspect starting at Stage 4.\n\n" +
                                                        "You can still choose random stage order, Artifacts and for the run to end after Stage 2 for achievement hunting.", "en");
            //
            //Rule Manipulators
            On.RoR2.Run.ForceChoice_RuleChoiceMask_RuleChoiceMask_RuleChoiceDef += Run_ForceChoice_RuleChoiceMask_RuleChoiceMask_RuleChoiceDef;
            On.RoR2.WeeklyRun.OverrideRuleChoices += (orig, self, inc, exc, rng) =>
            {
                self.ForceChoice(inc, exc, "Misc.PrismaticTrialExtended.Stage2");
                orig(self, inc, exc, rng);
            };
            On.RoR2.PreGameController.Awake += (orig, self) =>
            {
                orig(self);
                if (ChangedDefaultDefs.Count > 0)
                {
                    for (int i = 0; i < ChangedDefaultDefs.Count; i++)
                    {
                        ChangedDefaultDefs[i].defaultChoiceIndex = ChangedDefaultIndex[i];
                    }
                    ChangedDefaultDefs.Clear();
                    ChangedDefaultIndex.Clear();
                }
            };
            //
            //
            //Removing networking stuff
            On.RoR2.UI.WeeklyRunScreenController.OnEnable += (orig, self) =>
            {
                Debug.Log("Disabled WeeklyRunScreenController.OnEnable");
                UnityEngine.Object.Destroy(self.leaderboard.gameObject.transform.parent.parent.gameObject);
                self.enabled = false;
            };
            On.RoR2.WeeklyRun.GetCurrentSeedCycle += (orig) =>
            {
                Debug.Log("WeeklyRun.GetCurrentSeedCycle");
                return (uint)random.Next();
            };
            On.RoR2.WeeklyRun.ClientSubmitLeaderboardScore += (orig, self, runReport) =>
            {
                Debug.Log("Disabled WeeklyRun.ClientSubmitLeaderboardScore");
            };
            On.RoR2.DisableIfGameModded.OnEnable += (orig, self) =>
            {
                orig(self);
                self.enabled = false;
                self.gameObject.SetActive(true);
                Debug.Log("Disabled DisableIfGameModded.OnEnable");
            };
            //

            //Crystal List being empty tho idk
            //Objective gets cleared presumably because the list is fucked
            On.RoR2.SceneDirector.Start += AdjustCrystalStuff;
            On.RoR2.Stage.Start += FixClientsLate;
            On.RoR2.WeeklyRun.OnServerTeleporterPlaced += (orig, self, sceneDirector, teleporter) =>
            {
                orig(self, sceneDirector, teleporter);
                self.crystalActiveList = new List<OnDestroyCallback>();
            };
        }

        private static void WeeklyRun_AdvanceStage(On.RoR2.WeeklyRun.orig_AdvanceStage orig, WeeklyRun self, SceneDef nextScene)
        {
            bool PrismaticTrialExtender = false;
            if (self.ruleBook.GetRuleChoice(RuleDefPrismatic).localName.Equals("Endless") && self.stageClearCount == 1 && SceneInfo.instance.countsAsStage)
            {
                self.stageClearCount = 0;
                PrismaticTrialExtender = true;
            }
            orig(self, nextScene);
            if (PrismaticTrialExtender == true)
            {
                self.stageClearCount = 2;
                PrismaticTrialExtender = false;
            }

            if (self.ruleBook.GetRuleChoice(RuleDefPrismatic).localName.Equals("Endless"))
            {
                //self.bossAffixes[0] = bossAffixes[random.Next(bossAffixes.Length)];
                int kill = 3;// + self.participatingPlayerCount - 1;
                int crystal = 5;// + self.participatingPlayerCount - 1;
                if (nextScene.stageOrder == 4)
                {
                    kill += 1;
                    crystal += 1;
                }
                else if (nextScene.stageOrder == 5)
                {
                    kill += 2;
                    crystal += 3;
                }
                self.crystalCount = (uint)crystal;
                self.crystalsRequiredToKill = (uint)kill;

            }
            self.crystalRewardValue = (uint)self.GetDifficultyScaledCost(30);
        }

        private static System.Collections.IEnumerator FixClientsLate(On.RoR2.Stage.orig_Start orig, Stage self)
        {
            var RETURN = orig(self);   
            if (Run.instance && Run.instance.GetComponent<WeeklyRun>())
            {
                if (TeleporterInteraction.instance)
                {
                    //TP instance seems to always work for the particles so idk what the issue may be with some of the other stuff
                    ChildLocator component2 = TeleporterInteraction.instance.GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>();
                    if (component2)
                    {
                        component2.FindChild("TimeCrystalProps").gameObject.SetActive(true);
                        component2.FindChild("TimeCrystalBeaconBlocker").gameObject.SetActive(true);
                    }
                    TeleporterInteraction.instance.GetComponent<HoldoutZoneController>().baseRadius = 500;
                }
            }
            return RETURN;
        }

        private static void AdjustCrystalStuff(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            if (Run.instance && Run.instance.GetComponent<WeeklyRun>())
            {
                WeeklyRun run = Run.instance.GetComponent<WeeklyRun>();
                //Debug.Log(SceneInfo.instance);
                if (run.ruleBook.GetRuleChoice(RuleDefPrismatic).localName.Equals("Endless"))
                {
                    int kill = 3;// + self.participatingPlayerCount - 1;
                    int crystal = 5;// + self.participatingPlayerCount - 1;
                    if (SceneInfo.instance.sceneDef.stageOrder == 4)
                    {
                        kill += 1;
                        crystal += 1;
                    }
                    else if (SceneInfo.instance.sceneDef.stageOrder == 5)
                    {
                        kill += 2;
                        crystal += 3;
                    }
                    run.crystalCount = (uint)crystal;
                    run.crystalsRequiredToKill = (uint)kill;
                }
                run.crystalRewardValue = (uint)run.GetDifficultyScaledCost(30);
            }
            orig(self);
        }


        private static void RuleBookViewer_Start(On.RoR2.UI.RuleBookViewer.orig_Start orig, RoR2.UI.RuleBookViewer self)
        {
            orig(self);
            //Debug.Log("RuleBookViewer_Start" + PreGameController.instance);
            RoR2.UI.RuleCategoryController ruleCategoryController = self.categoryElementAllocator.elements[5];
            ruleCategoryController.gameObject.transform.SetSiblingIndex(2);
        }



        private static RunReport.PlayerInfo PlayerInfo_Generate(On.RoR2.RunReport.PlayerInfo.orig_Generate orig, PlayerCharacterMasterController playerCharacterMasterController, GameEndingDef gameEnding)
        {
            RunReport.PlayerInfo temp = orig(playerCharacterMasterController, gameEnding);

            if (Run.instance && Run.instance is WeeklyRun)
            {
                if (gameEnding.isWin)
                {
                    temp.finalMessageToken = "Prismatic Trial  :  Prismatically aligned";
                }
                else
                {
                    temp.finalMessageToken = "Prismatic Trial  :  Prismatic loss";
                }
            }
            return temp;
        }



        private static void Run_ForceChoice_RuleChoiceMask_RuleChoiceMask_RuleChoiceDef(On.RoR2.Run.orig_ForceChoice_RuleChoiceMask_RuleChoiceMask_RuleChoiceDef orig, Run self, RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, RuleChoiceDef choiceDef)
        {
            //Debug.LogWarning("Run_ForceChoice "+ choiceDef.globalName);
            if (self.name.StartsWith("Week"))
            {
                if (choiceDef.globalName.StartsWith("Difficulty") || choiceDef.globalName.StartsWith("Misc.Prism") || choiceDef.globalName.StartsWith("Misc.Stage") || choiceDef.globalName.StartsWith("Artifacts"))
                {
                    //Difficulty would need to be set here
                    if (choiceDef.globalName.StartsWith("Difficulty"))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            mustInclude[choiceDef.ruleDef.choices[i].globalIndex] = true;
                            mustExclude[choiceDef.ruleDef.choices[i].globalIndex] = false;
                        }
                        mustInclude[choiceDef.globalIndex] = true;
                        mustExclude[choiceDef.globalIndex] = false;
                    }
                    else
                    {
                        foreach (RuleChoiceDef ruleChoiceDef in choiceDef.ruleDef.choices)
                        {
                            mustInclude[ruleChoiceDef.globalIndex] = true;
                            mustExclude[ruleChoiceDef.globalIndex] = false;
                        }
                        mustInclude[choiceDef.globalIndex] = true;
                        mustExclude[choiceDef.globalIndex] = false;
                    }

                    if (!ChangedDefaultDefs.Contains(choiceDef.ruleDef) && choiceDef.localName != "Off")
                    {
                        if (choiceDef.ruleDef.defaultChoiceIndex != choiceDef.localIndex)
                        {
                            /* ChangedDefaultDefs.Add(choiceDef.ruleDef);
                             ChangedDefaultIndex.Add(choiceDef.ruleDef.defaultChoiceIndex);
                             Debug.LogWarning("Run_ForceChoice " + choiceDef.ruleDef.globalName);
                             choiceDef.ruleDef.defaultChoiceIndex = choiceDef.localIndex;*/

                        }
                    }
                }
                else
                {
                    orig(self, mustInclude, mustExclude, choiceDef);
                }
            }
            else
            {
                orig(self, mustInclude, mustExclude, choiceDef);
            }
        }



        public static void LateRunningMethod()
        {
            RuleCatalog.AddRule(RuleDefPrismatic); //Automatically adds to last Category which is Misc

            //They definitely did not ever care to add support for Modded Rules
            for (int k = 0; k < RuleCatalog.allRuleDefs.Count; k++)
            {
                RuleDef ruleDef5 = RuleCatalog.allRuleDefs[k];
                if (ruleDef5 == RuleDefPrismatic)
                {
                    ruleDef5.globalIndex = k;
                    for (int j = 0; j < ruleDef5.choices.Count; j++)
                    {
                        RuleChoiceDef ruleChoiceDef10 = ruleDef5.choices[j];
                        ruleChoiceDef10.localIndex = j;
                        ruleChoiceDef10.globalIndex = RuleCatalog.allChoicesDefs.Count;
                        RuleCatalog.allChoicesDefs.Add(ruleChoiceDef10);
                    }
                }
            }

            //Debug.Log(RuleDefPrismatic.category.displayToken);

            //Proper Map Normal Icon
            Texture2D texRuleMapIsNormal = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texRuleMapIsNormal.LoadImage(Properties.Resources.texRuleMapIsNormal, true);
            //texRuleMapIsNormal.filterMode = FilterMode.Bilinear;
            texRuleMapIsNormal.wrapMode = TextureWrapMode.Clamp;
            Sprite texRuleMapIsNormalS = Sprite.Create(texRuleMapIsNormal, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));

            RuleChoiceDef mapDef = RuleCatalog.FindChoiceDef("Misc.StageOrder.Normal");
            if (mapDef != null)
            {
                mapDef.sprite = texRuleMapIsNormalS;
            }

            //
            //All Boss Affixes for the Weekly
            newBossAffixes = Array.Empty<EquipmentIndex>();
            for (var i = 0; i < EliteCatalog.eliteDefs.Length; i++)
            {
                /*if (EliteCatalog.eliteDefs[i].name.EndsWith("Gold") || EliteCatalog.eliteDefs[i].name.EndsWith("Echo") || EliteCatalog.eliteDefs[i].name.EndsWith("SecretSpeed"))
                {
                }*/
                EquipmentDef tempEliteEquip = EliteCatalog.eliteDefs[i].eliteEquipmentDef;
                if (tempEliteEquip != null)
                {
                    if (tempEliteEquip.dropOnDeathChance == 0 || tempEliteEquip.name.Contains("Gold") || tempEliteEquip.name.EndsWith("Echo") || tempEliteEquip.name.Contains("Yellow"))
                    {
                    }
                    else
                    {
                        newBossAffixes = newBossAffixes.Add(tempEliteEquip.equipmentIndex);
                    }
                }
            }
            if (newBossAffixes.Length == 0)
            {
                newBossAffixes = new EquipmentIndex[]
                {
                RoR2Content.Equipment.AffixRed.equipmentIndex,
                RoR2Content.Equipment.AffixBlue.equipmentIndex,
                RoR2Content.Equipment.AffixWhite.equipmentIndex,
                RoR2Content.Equipment.AffixLunar.equipmentIndex,
                DLC1Content.Elites.Earth.eliteEquipmentDef.equipmentIndex,
                DLC2Content.Equipment.EliteAurelioniteEquipment.equipmentIndex
                };
            }
            //
            //
            /*Debug.LogWarning("ALL RULE CHOICES");
            for (int i = 0; i < RuleCatalog.allChoicesDefs.Count; i++)
            {
                Debug.LogWarning(RuleCatalog.allChoicesDefs[i].globalName);
            }*/


            GameObject TimeCrystalBody = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TimeCrystalBody");
            TimeCrystalBody.AddComponent<FixCrystalsClient>();
        }



        public class FixCrystalsClient : MonoBehaviour
        {
            public void Start()
            {
                if (Run.instance)
                {
                    WeeklyRun run = Run.instance.GetComponent<WeeklyRun>();  
                    if (run)
                    {
                        run.crystalActiveList.Add(OnDestroyCallback.AddCallback(base.gameObject, delegate (OnDestroyCallback component)
                        {
                            run.crystalActiveList.Remove(component);
                        }));
                    }
                }
            }
        }
    }

}
