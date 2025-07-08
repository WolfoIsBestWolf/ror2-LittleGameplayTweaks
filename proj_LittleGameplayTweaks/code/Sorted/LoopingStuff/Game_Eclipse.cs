using HG;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class Eclipse
    {
        public static void Start()
        {
            On.RoR2.EclipseRun.Start += EclipseRun_Start;
            On.EntityStates.Interactables.MSObelisk.EndingGame.DoFinalAction += EndingGame_DoFinalAction;

            On.RoR2.EclipseRun.OverrideRuleChoices += EclipseRun_OverrideRuleChoices;
            IL.RoR2.EclipseRun.OverrideRuleChoices += EclipseRun_DontRemoveArtifacts;

         
             
            IL.RoR2.EclipseRun.OnClientGameOver += LevelUpIfDyingToVoidling;
            if (WConfig.cfgE1Unnerf.Value)
            {
                IL.RoR2.CharacterMaster.OnBodyStart += E1UnnerfHappiestMask;
            }
           
        }

        private static void E1UnnerfHappiestMask(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.TryGotoNext(MoveType.Before,
            x => x.MatchCallvirt("RoR2.Run", "get_selectedDifficulty"),
             x => x.MatchLdcI4(3));
            c.Index--;
            if (c.TryGotoNext(MoveType.After,
            x => x.MatchCallvirt("RoR2.Run", "get_selectedDifficulty")
            ))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<int, CharacterMaster, int>>((value, self) =>
                {
                    if (self.inventory)
                    {
                        if (self.inventory.GetItemCount(RoR2Content.Items.HealthDecay) > 0)
                        {
                            return 1;
                        }
                    }
                    return value;
                });
            }
            else
            {
                Debug.LogWarning("IL FAILED : E1UnnerfHappiestMask");
            }
        }

        private static void LevelUpIfDyingToVoidling(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
            x => x.MatchLdfld("RoR2.GameEndingDef", "isWin")))
            {
                c.EmitDelegate<Func<bool, bool>>((value) =>
                {
                    if (value == false && Run.instance)
                    {
                        //If you die on Commencement dont count
                        if (Run.instance.eventFlags.Contains("KilledBrother") && SceneCatalog.mostRecentSceneDef.cachedName != "moon2")
                        {
                            Debug.Log("Counting death as Win because killed Mithrix this run");
                            return true;
                        }
                    }
                    return value;
                });
            }
            else
            {
                Debug.LogWarning("IL FAILED : EclipseRun_OnClientGameOver");
            }
        }

 
  
        public static void EclipseRun_DontRemoveArtifacts(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
            x => x.MatchCall("RoR2.ArtifactCatalog", "get_artifactCount")))
            {
                c.EmitDelegate<Func<int, int>>((damageCoeff) =>
                {
                    if (WConfig.EclipseAllowChoiceArtifacts.Value >= WConfig.EclipseArtifact.Blacklist)
                    {
                        return 0;
                    }
                    return damageCoeff;
                });
            }
            else
            {
                Debug.LogWarning("IL FAILED : EclipseRun_DontRemoveArtifacts");
            }
        }

        public static void ReverseForceChoice(ArtifactDef artifact, RuleChoiceMask mustInclude, RuleChoiceMask mustExclude)
        {
            if (artifact)
            {
                RuleDef ruleDef = RuleCatalog.FindRuleDef("Artifacts." + artifact.cachedName);
                if (ruleDef != null)
                {
                    foreach (RuleChoiceDef ruleChoiceDef in ruleDef.choices)
                    {
                        mustInclude[ruleChoiceDef.globalIndex] = false;
                        mustExclude[ruleChoiceDef.globalIndex] = false;
                    }
                }
            }
        }
        public static void ForceChoiceOff(ArtifactDef artifact, RuleChoiceMask mustInclude, RuleChoiceMask mustExclude)
        {
            if (artifact)
            {
                RuleDef ruleDef = RuleCatalog.FindRuleDef("Artifacts." + artifact.cachedName);
                RuleChoiceDef choiceDef = ruleDef.FindChoice("Off");
                if (choiceDef != null)
                {
                    foreach (RuleChoiceDef ruleChoiceDef in choiceDef.ruleDef.choices)
                    {
                        mustInclude[ruleChoiceDef.globalIndex] = false;
                        mustExclude[ruleChoiceDef.globalIndex] = true;
                    }
                    mustInclude[choiceDef.globalIndex] = true;
                    mustExclude[choiceDef.globalIndex] = false;
                }
            }
        }


        public static void EclipseRun_OverrideRuleChoices(On.RoR2.EclipseRun.orig_OverrideRuleChoices orig, EclipseRun self, RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, ulong runSeed)
        {
            orig(self, mustInclude, mustExclude, runSeed);

            if (WConfig.EclipseAllowTwisted.Value)
            {
                self.ForceChoice(mustInclude, mustExclude, "Items." + RoR2Content.Items.LunarTrinket.name + ".Off");
            }

            if (WConfig.EclipseAllowChoiceArtifacts.Value != WConfig.EclipseArtifact.Off)
            {
                /*for (int j = 0; j < ArtifactCatalog.artifactCount; j++)
                {
                    Debug.Log(ArtifactCatalog.artifactDefs[j].cachedName);
                }*/
                if (WConfig.EclipseAllowChoiceArtifacts.Value == WConfig.EclipseArtifact.Blacklist)
                {
                    ForceChoiceOff(RoR2Content.Artifacts.Command, mustInclude, mustExclude);
                    ForceChoiceOff(RoR2Content.Artifacts.RandomSurvivorOnRespawn, mustInclude, mustExclude);
                    ForceChoiceOff(RoR2Content.Artifacts.Glass, mustInclude, mustExclude);
                    ForceChoiceOff(CU8Content.Artifacts.Delusion, mustInclude, mustExclude);
                    ForceChoiceOff(DLC2Content.Artifacts.Rebirth, mustInclude, mustExclude);
                    ForceChoiceOff(ArtifactCatalog.FindArtifactDef("CLASSICITEMSRETURNS_ARTIFACT_CLOVER"), mustInclude, mustExclude);
                    ForceChoiceOff(ArtifactCatalog.FindArtifactDef("ARTIFACT_SPOILS"), mustInclude, mustExclude);
                    ForceChoiceOff(ArtifactCatalog.FindArtifactDef("ARTIFACT_ZETDROPIFACT"), mustInclude, mustExclude);
                    ForceChoiceOff(ArtifactCatalog.FindArtifactDef("ZZT_CommandTrueCommand"), mustInclude, mustExclude);

                }
            }
        }

        public static void EndingGame_DoFinalAction(On.EntityStates.Interactables.MSObelisk.EndingGame.orig_DoFinalAction orig, EntityStates.Interactables.MSObelisk.EndingGame self)
        {
            if (Run.instance && Run.instance.GetComponent<EclipseRun>())
            {
                self.outer.SetNextState(new EntityStates.Interactables.MSObelisk.TransitionToNextStage());
                return;
            }
            orig(self);
        }

        public static void EclipseRun_Start(On.RoR2.EclipseRun.orig_Start orig, EclipseRun self)
        {
            orig(self);
            if (NetworkServer.active)
            {
                if (WConfig.EclipseAllowArtifactWorld.Value)
                {
                    self.ResetEventFlag("NoArtifactWorld");
                }
                if (WConfig.EclipseAllowTwisted.Value)
                {
                    self.ResetEventFlag("NoMysterySpace");
                }
                if (WConfig.EclipseAllowVoid.Value)
                {
                    self.ResetEventFlag("NoVoidStage");
                }
            }
        }
    }

}
