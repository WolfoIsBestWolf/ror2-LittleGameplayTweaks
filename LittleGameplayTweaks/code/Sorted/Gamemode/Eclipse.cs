using R2API;
using RoR2;
using System;
using System.Collections.Generic;
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

        }

        public static void ReverseForceChoice(ArtifactDef artifact, RuleChoiceMask mustInclude, RuleChoiceMask mustExclude)
        {
            if (artifact)
            {
                RuleDef ruleDef = RuleCatalog.FindRuleDef("Artifacts." + artifact.cachedName);
                Debug.Log(ruleDef);
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



        private static void EclipseRun_OverrideRuleChoices(On.RoR2.EclipseRun.orig_OverrideRuleChoices orig, EclipseRun self, RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, ulong runSeed)
        {
            orig(self, mustInclude, mustExclude, runSeed);

            if (WConfig.EclipseAllowChoiceArtifacts.Value)
            {
                //Does this check if it's unlocked??
                ReverseForceChoice(RoR2Content.Artifacts.Bomb, mustInclude, mustExclude);
                ReverseForceChoice(RoR2Content.Artifacts.EliteOnly, mustInclude, mustExclude);
                ReverseForceChoice(RoR2Content.Artifacts.FriendlyFire, mustInclude, mustExclude);
                ReverseForceChoice(RoR2Content.Artifacts.MixEnemy, mustInclude, mustExclude);

                ReverseForceChoice(RoR2Content.Artifacts.MonsterTeamGainsItems, mustInclude, mustExclude);
                ReverseForceChoice(RoR2Content.Artifacts.ShadowClone, mustInclude, mustExclude);
                ReverseForceChoice(RoR2Content.Artifacts.TeamDeath, mustInclude, mustExclude);
                ReverseForceChoice(RoR2Content.Artifacts.WispOnDeath, mustInclude, mustExclude);

                ReverseForceChoice(ArtifactCatalog.FindArtifactDef("SingleEliteType"), mustInclude, mustExclude);
                ReverseForceChoice(ArtifactCatalog.FindArtifactDef("RandomLoadoutOnRespawn"), mustInclude, mustExclude);

                //Weirded options
                /*
                ReverseForceChoice(RoR2Content.Artifacts.Enigma, mustInclude, mustExclude);
                ReverseForceChoice(CU8Content.Artifacts.Devotion, mustInclude, mustExclude);
                ReverseForceChoice(RoR2Content.Artifacts.Swarms, mustInclude, mustExclude);
                ReverseForceChoice(RoR2Content.Artifacts.Glass, mustInclude, mustExclude);

                ReverseForceChoice(ArtifactCatalog.FindArtifactDef("SingleInteractablePerCategory"), mustInclude, mustExclude);
                ReverseForceChoice(ArtifactCatalog.FindArtifactDef("SingleItemPerTier"), mustInclude, mustExclude);
                ReverseForceChoice(ArtifactCatalog.FindArtifactDef("StatsOnLowHealth"), mustInclude, mustExclude);
                ReverseForceChoice(ArtifactCatalog.FindArtifactDef("MixInteractable"), mustInclude, mustExclude);
                ReverseForceChoice(ArtifactCatalog.FindArtifactDef("RerollItemsAndEquipments"), mustInclude, mustExclude);
                */
            }



            //ReverseForceChoice(RoR2Content.Artifacts.TeamDeath, mustInclude, mustExclude);
            //ReverseForceChoice(RoR2Content.Artifacts.TeamDeath, mustInclude, mustExclude);




        }

        private static void EndingGame_DoFinalAction(On.EntityStates.Interactables.MSObelisk.EndingGame.orig_DoFinalAction orig, EntityStates.Interactables.MSObelisk.EndingGame self)
        {
            if (Run.instance && Run.instance.GetComponent<EclipseRun>())
            {
                self.outer.SetNextState(new EntityStates.Interactables.MSObelisk.TransitionToNextStage());
                return;
            }
            orig(self);
        }

        private static void EclipseRun_Start(On.RoR2.EclipseRun.orig_Start orig, EclipseRun self)
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
