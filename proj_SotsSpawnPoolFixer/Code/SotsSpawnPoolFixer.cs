using BepInEx;
using HG;
using MonoMod.Cil;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using RoR2.Navigation;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace SpawnPoolFixer
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("Wolfo.DLCSpawnPoolFixer", "DLCSpawnPoolFixer", "1.3.0")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class SotsSpawnPoolFix : BaseUnityPlugin
    {
        public static ExpansionDef DLC1 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC1/Common/DLC1.asset").WaitForCompletion();
        public void Awake()
        {
            WConfig.InitConfig();
            FixSotsSpawnpools();

            SceneCollection sgStage1 = Addressables.LoadAssetAsync<SceneCollection>(key: "RoR2/Base/SceneGroups/sgStage1.asset").WaitForCompletion();
            sgStage1._sceneEntries[0].weight = WConfig.cfgStage1Weight.Value;
            sgStage1._sceneEntries[1].weight = WConfig.cfgStage1Weight.Value;
            sgStage1._sceneEntries[2].weight = WConfig.cfgStage1Weight.Value;
            sgStage1._sceneEntries[3].weight = WConfig.cfgStage1Weight.Value;

            SceneCollection loopSgStage1 = Addressables.LoadAssetAsync<SceneCollection>(key: "RoR2/Base/SceneGroups/loopSgStage1.asset").WaitForCompletion();
            loopSgStage1._sceneEntries[0].weight = WConfig.cfgStage1Weight.Value;
            loopSgStage1._sceneEntries[1].weight = WConfig.cfgStage1Weight.Value;
            loopSgStage1._sceneEntries[2].weight = WConfig.cfgStage1Weight.Value;
            loopSgStage1._sceneEntries[3].weight = WConfig.cfgStage1Weight.Value;

            On.RoR2.ClassicStageInfo.Start += FixWrongDccsPool;
            SceneDirector.onGenerateInteractableCardSelection += FixWrongRadarTowers;



            IL.RoR2.ClassicStageInfo.RebuildCards += SotV_EnemyRemovals;

            On.RoR2.BazaarController.IsUnlockedBeforeLooping += NoPreLoopPostLoop;
            On.RoR2.DCCSBlender.GetBlendedDCCS += DCCSBlender_GetBlendedDCCS;


            //Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/LunarChest/iscLunarChest.asset").WaitForCompletion().requiredFlags = NodeFlags.None;
            //Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/iscShrineHalcyonite.asset").WaitForCompletion().requiredFlags = NodeFlags.TeleporterOK;
            //Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/iscShrineHalcyoniteTier1.asset").WaitForCompletion().requiredFlags = NodeFlags.TeleporterOK;

            //All NodeGraphs with 0 NoCeiling Ground spots
            NodeGraph ancientloft_GroundNodeGraph = Addressables.LoadAssetAsync<NodeGraph>(key: "RoR2/ancientloft_GroundNodeGraph.asset").WaitForCompletion();
            //NodeGraph AncientLoft = Addressables.LoadAssetAsync<NodeGraph>(key: "RoR2/DLC1/ancientloft/ancientloft_GroundNodeGraph.asset").WaitForCompletion(); //Good one, Hopoo Games
            NodeGraph itancientloft_GroundNodeGraph = Addressables.LoadAssetAsync<NodeGraph>(key: "RoR2/DLC1/itancientloft/itancientloft_GroundNodeGraph.asset").WaitForCompletion();
            NodeGraph itmoon_GroundNodeGraph = Addressables.LoadAssetAsync<NodeGraph>(key: "RoR2/DLC1/itmoon/itmoon_GroundNodeGraph.asset").WaitForCompletion();
            NodeGraph voidraid_GroundNodeGraph = Addressables.LoadAssetAsync<NodeGraph>(key: "RoR2/DLC1/voidraid/voidraid_GroundNodeGraph.asset").WaitForCompletion();

            for (int i = 0; i < ancientloft_GroundNodeGraph.nodes.Length; i++)
            {
                ancientloft_GroundNodeGraph.nodes[i].flags |= NodeFlags.NoCeiling;
            }
            for (int i = 0; i < itancientloft_GroundNodeGraph.nodes.Length; i++)
            {
                itancientloft_GroundNodeGraph.nodes[i].flags |= NodeFlags.NoCeiling;
            }
            for (int i = 0; i < itmoon_GroundNodeGraph.nodes.Length; i++)
            {
                itmoon_GroundNodeGraph.nodes[i].flags |= NodeFlags.NoCeiling;
            }
            for (int i = 0; i < voidraid_GroundNodeGraph.nodes.Length; i++)
            {
                voidraid_GroundNodeGraph.nodes[i].flags |= NodeFlags.NoCeiling;
            }
        }

        private static DirectorCardCategorySelection DCCSBlender_GetBlendedDCCS(On.RoR2.DCCSBlender.orig_GetBlendedDCCS orig, DccsPool.Category dccsPoolCategory, ref Xoroshiro128Plus rng, ClassicStageInfo stageInfo, int contentSourceMixLimit, List<RoR2.ExpansionManagement.ExpansionDef> acceptableExpansionList)
        {
            return orig(dccsPoolCategory, ref rng, stageInfo, contentSourceMixLimit, null);
        }


        private bool NoPreLoopPostLoop(On.RoR2.BazaarController.orig_IsUnlockedBeforeLooping orig, BazaarController self, SceneDef sceneDef)
        {
            if (WConfig.cfgLoopSeers.Value == false)
            {
                if (sceneDef.loopedSceneDef && Run.instance.stageClearCount >= 4)
                {
                    return false;
                }
            }
            return orig(self, sceneDef);
        }


        private void SotV_EnemyRemovals(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (c.TryGotoNext(MoveType.Before,
            x => x.MatchStfld("RoR2.ClassicStageInfo", "modifiableMonsterCategories")))
            {
                c.EmitDelegate<Func<DirectorCardCategorySelection, DirectorCardCategorySelection>>((dccs) =>
                {
                    RemoveMonsterBasedOnSotVReplacement(dccs);
                    return dccs;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: SotV_EnemyRemovals");
            }
        }


        public static void RemoveMonsterBasedOnSotVReplacement(DirectorCardCategorySelection dccs)
        {
            if (!WConfig.cfgSotV_EnemyRemovals.Value)
            {
                return;
            }
            if (Run.instance && Run.instance.IsExpansionEnabled(DLC1))
            {
                string scene = SceneInfo.instance.sceneDef.cachedName;
                switch (scene)
                {
                    case "golemplains":
                    case "golemplains2":
                        //They remove my guy jellyfish? wtf
                        RemoveCard(dccs, 2, "fish"); //-> Alpha
                        break;
                    case "frozenwall":
                        RemoveCard(dccs, 2, "Lemurian"); //-> Vermin
                        RemoveCard(dccs, 2, "Wisp"); //-> Pest
                        //Reaver
                        break;
                    case "dampcavesimple":
                        RemoveCard(dccs, 1, "GreaterWisp"); //-> Gup
                        break;
                    case "shipgraveyard":
                        RemoveCard(dccs, 2, "Beetle"); //-> Larva
                        //Reaver
                        break;
                    case "rootjungle":
                        RemoveCard(dccs, 1, "LemurianBruiser"); //-> Gup
                        RemoveCard(dccs, 2, "Lemurian"); //-> Larva
                        break;
                    case "skymeadow":
                        if (WConfig.WORMREMOVAL.Value)
                        {
                            RemoveCard(dccs, 0, "MagmaWorm"); //-> XI
                        }
                        //Reaver
                        break;
                }
            }
        }


        public static void RemoveCard(DirectorCardCategorySelection dccs, int cat, string card)
        {
            Debug.Log("Attempting to remove " + card + " from " + SceneInfo.instance.sceneDef);
            //Because categories get shifted around in DCCS Blender, better to just find them
            string a = "";
            switch (cat)
            {
                case 2:
                    a = "Basic Monsters";
                    break;
                case 1:
                    a = "Minibosses";
                    break;
                case 0:
                    a = "Champions";
                    break;

            }
            cat = dccs.FindCategoryIndexByName(a);
            if (cat == -1)
            {
                return;
            }
            int c = FindSpawnCard(dccs.categories[cat].cards, card);
            if (c == -1)
            {
                return;
            }
            ArrayUtils.ArrayRemoveAtAndResize(ref dccs.categories[cat].cards, c);
        }


        public static int FindSpawnCard(DirectorCard[] insert, string LookingFor)
        {
            for (int i = 0; i < insert.Length; i++)
            {
                if (insert[i].spawnCard.name.EndsWith(LookingFor))
                {
                    //Debug.Log("Found " + LookingFor);
                    return i;
                }
            }
            Debug.LogWarning("Couldn't find " + LookingFor);
            return -1;
        }

        private static void FixWrongDccsPool(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            if (Run.instance && SceneInfo.instance)
            {
                if (SceneInfo.instance.sceneDef.cachedName.Equals("villagenight"))
                {
                    self.interactableDccsPool = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/village/dpVillageInteractables.asset").WaitForCompletion();
                }
            }
            orig(self);
        }



        private void FixWrongRadarTowers(SceneDirector scene, DirectorCardCategorySelection dccs)
        {
            string name = SceneInfo.instance.sceneDef.cachedName;
            if (name.StartsWith("villag") || name.StartsWith("habitat") || name.StartsWith("helm"))
            {
                UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef("Logs.Stages." + name);
                Debug.Log("Automatic Radar Scanner fix for " + unlockableDef);
                if (!unlockableDef)
                {
                    Debug.Log("No unlockableDef");
                    return;
                }
                int rare = dccs.FindCategoryIndexByName("Rare");
                if (rare > 0)
                {
                    for (int i = 0; i < dccs.categories[rare].cards.Length; i++)
                    {
                        if (dccs.categories[rare].cards[i].forbiddenUnlockableDef)
                        {
                            dccs.categories[rare].cards[i].forbiddenUnlockableDef = unlockableDef;
                        }
                    }
                }
            }
        }




        public static void FixSotsSpawnpools()
        {
            DccsPool dpArtifactWorld02Monsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/artifactworld02/dpArtifactWorld02Monsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsArtifactWorld02Monsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/artifactworld02/dccsArtifactWorld02Monsters_DLC1.asset").WaitForCompletion();

            dpArtifactWorld02Monsters.poolCategories[0].includedIfConditionsMet[0].dccs = dccsArtifactWorld02Monsters_DLC1;
            //
            DirectorCardCategorySelection dccsVillageInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageInteractables_DLC2.asset").WaitForCompletion();
            dccsVillageInteractables_DLC2.categories[5].cards[3].minimumStageCompletions = 1;
        }
    }
}