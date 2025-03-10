using BepInEx;
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
using HG;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace SotsSpawnPoolFixer
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("Wolfo.DLCSpawnPoolFixer", "DLCSpawnPoolFixer", "1.2.0")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]


    public class SotsSpawnPoolFix : BaseUnityPlugin
    {

        public void Awake()
        {
            FixSotsSpawnpools();

            SceneCollection sgStage1 = Addressables.LoadAssetAsync<SceneCollection>(key: "RoR2/Base/SceneGroups/sgStage1.asset").WaitForCompletion();
            sgStage1._sceneEntries[0].weight = 0.75f;
            sgStage1._sceneEntries[1].weight = 0.75f;
            sgStage1._sceneEntries[2].weight = 0.75f;
            sgStage1._sceneEntries[3].weight = 0.75f;

            SceneCollection loopSgStage1 = Addressables.LoadAssetAsync<SceneCollection>(key: "RoR2/Base/SceneGroups/loopSgStage1.asset").WaitForCompletion();
            loopSgStage1._sceneEntries[0].weight = 0.75f;
            loopSgStage1._sceneEntries[1].weight = 0.75f;
            loopSgStage1._sceneEntries[2].weight = 0.75f;
            loopSgStage1._sceneEntries[3].weight = 0.75f;

            RoR2.SceneDirector.onGenerateInteractableCardSelection += FixWrongRadarTowers;
        }

        private void FixWrongRadarTowers(SceneDirector scene, DirectorCardCategorySelection dccs)
        {
            string name = SceneInfo.instance.sceneDef.cachedName;
            if (name.StartsWith("villag") || name.StartsWith("habitat") || name.StartsWith("helm"))
            {
                UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef("Logs.Stages."+name);
                Debug.Log("Trying to fix Radar Scanner for " + unlockableDef);
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