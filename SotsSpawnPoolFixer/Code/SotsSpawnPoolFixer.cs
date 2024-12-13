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
    [BepInPlugin("Wolfo.DLCSpawnPoolFixer", "DLCSpawnPoolFixer", "1.1.1")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]


    public class SotsSpawnPoolFix : BaseUnityPlugin
    {

        public void Awake()
        {
            On.RoR2.ClassicStageInfo.Start += RunsAlways_ClassicStageInfo_Start;
            FixSotsSpawnpools();
            On.RoR2.DccsPool.AreConditionsMet += DccsPool_AreConditionsMet;


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

        }
 
        private static bool DccsPool_AreConditionsMet(On.RoR2.DccsPool.orig_AreConditionsMet orig, DccsPool self, DccsPool.ConditionalPoolEntry entry)
        {
            if (entry.dccs && entry.dccs.name.EndsWith("Standard"))
            {
                if (entry.requiredExpansions.Length == 1)
                {
                    if (entry.requiredExpansions[0].name.Equals("DLC2"))
                    {
                        entry.weight = 1000;
                    }
                    else
                    {
                        entry.weight = 1;
                    }
                }
                else if (entry.requiredExpansions.Length == 2)
                {
                    entry.weight = 1000000;
                }
            }
            return orig(self, entry);
        }

        private static void RunsAlways_ClassicStageInfo_Start(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            if (Run.instance && SceneInfo.instance)
            {
                //self.sceneDirectorInteractibleCredits += 550;
                if (SceneInfo.instance.sceneDef.cachedName.Equals("villagenight"))
                {
                    self.interactableDccsPool = null;
                    self.interactableCategories = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillagenightInteractablesDLC2.asset").WaitForCompletion();
                }
            }
            orig(self);
        }


        public static void FixSotsSpawnpools()
        {
            Debug.Log("Gearbox Games");
            ExpansionDef DLC2 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC2/Common/DLC2.asset").WaitForCompletion();
            ExpansionDef[] required = new ExpansionDef[] { DLC2 };

            
            DccsPool dpFrozenWallInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/frozenwall/dpFrozenWallInteractables.asset").WaitForCompletion();
            DccsPool dpWispGraveyardInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/wispgraveyard/dpWispGraveyardInteractables.asset").WaitForCompletion();
            DccsPool dpSulfurPoolsInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/sulfurpools/dpSulfurPoolsInteractables.asset").WaitForCompletion();
            DccsPool dpHabitatfallInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/habitatfall/dpHabitatfallInteractables.asset").WaitForCompletion();

            DccsPool dpDampCaveInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/dampcave/dpDampCaveInteractables.asset").WaitForCompletion();
            DccsPool dpShipgraveyardInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/shipgraveyard/dpShipgraveyardInteractables.asset").WaitForCompletion();
            DccsPool dpRootJungleInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/rootjungle/dpRootJungleInteractables.asset").WaitForCompletion();

            DccsPool dpSkyMeadowInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/skymeadow/dpSkyMeadowInteractables.asset").WaitForCompletion();

            

            DirectorCardCategorySelection dccsFrozenWallInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFrozenWallInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsWispGraveyardInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSulfurPoolsInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSulfurPoolsInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatfallInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitatfall/dccsHabitatfallInteractables.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsDampCaveInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsDampCaveInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShipgraveyardInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsRootJungleInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSkyMeadowInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSkyMeadowInteractablesDLC2.asset").WaitForCompletion();
            DccsPool dpArtifactWorld02Monsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/artifactworld02/dpArtifactWorld02Monsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsArtifactWorld02Monsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/artifactworld02/dccsArtifactWorld02Monsters_DLC1.asset").WaitForCompletion();



 ;
            //
            dpArtifactWorld02Monsters.poolCategories[0].includedIfConditionsMet[0].dccs = dccsArtifactWorld02Monsters_DLC1;
            //
            dpHabitatfallInteractables.poolCategories[0].includedIfConditionsMet[0].dccs = dccsHabitatfallInteractables;


            //

            DccsPool.ConditionalPoolEntry NEW_ENTRY = new DccsPool.ConditionalPoolEntry
            {
                dccs = dccsFrozenWallInteractablesDLC2,
                requiredExpansions = required,
                weight = 1,
            };
            ArrayUtils.ArrayAppend(ref dpFrozenWallInteractables.poolCategories[0].includedIfConditionsMet, NEW_ENTRY);

            NEW_ENTRY = new DccsPool.ConditionalPoolEntry
            {
                dccs = dccsWispGraveyardInteractablesDLC2,
                requiredExpansions = required,
                weight = 1,
            };
            ArrayUtils.ArrayAppend(ref dpWispGraveyardInteractables.poolCategories[0].includedIfConditionsMet, NEW_ENTRY);

            NEW_ENTRY = new DccsPool.ConditionalPoolEntry
            {
                dccs = dccsSulfurPoolsInteractablesDLC2,
                requiredExpansions = required,
                weight = 1,
            };
            ArrayUtils.ArrayAppend(ref dpSulfurPoolsInteractables.poolCategories[0].includedIfConditionsMet, NEW_ENTRY);

            NEW_ENTRY = new DccsPool.ConditionalPoolEntry
            {
                dccs = dccsDampCaveInteractablesDLC2,
                requiredExpansions = required,
                weight = 1,
            };
            ArrayUtils.ArrayAppend(ref dpDampCaveInteractables.poolCategories[0].includedIfConditionsMet, NEW_ENTRY);

            NEW_ENTRY = new DccsPool.ConditionalPoolEntry
            {
                dccs = dccsShipgraveyardInteractablesDLC2,
                requiredExpansions = required,
                weight = 1,
            };
            ArrayUtils.ArrayAppend(ref dpShipgraveyardInteractables.poolCategories[0].includedIfConditionsMet, NEW_ENTRY);

            NEW_ENTRY = new DccsPool.ConditionalPoolEntry
            {
                dccs = dccsRootJungleInteractablesDLC2,
                requiredExpansions = required,
                weight = 1,
            };
            ArrayUtils.ArrayAppend(ref dpRootJungleInteractables.poolCategories[0].includedIfConditionsMet, NEW_ENTRY);

            NEW_ENTRY = new DccsPool.ConditionalPoolEntry
            {
                dccs = dccsSkyMeadowInteractablesDLC2,
                requiredExpansions = required,
                weight = 1,
            };
            ArrayUtils.ArrayAppend(ref dpSkyMeadowInteractables.poolCategories[0].includedIfConditionsMet, NEW_ENTRY);



            DccsPool dpLakesMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/lakes/dpLakesMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesMonstersDLC2.asset").WaitForCompletion();

            NEW_ENTRY = new DccsPool.ConditionalPoolEntry
            {
                dccs = dccsLakesMonstersDLC2,
                requiredExpansions = required,
                weight = 1,
            };
            ArrayUtils.ArrayAppend(ref dpLakesMonsters.poolCategories[0].includedIfConditionsMet, NEW_ENTRY);


            DccsPool dpSnowyForestMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/snowyforest/dpSnowyForestMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSnowyForestMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSnowyForestMonstersDLC2.asset").WaitForCompletion();

            NEW_ENTRY = new DccsPool.ConditionalPoolEntry
            {
                dccs = dccsSnowyForestMonstersDLC2,
                requiredExpansions = required,
                weight = 1,
            };
            ArrayUtils.ArrayAppend(ref dpSnowyForestMonsters.poolCategories[0].includedIfConditionsMet, NEW_ENTRY);


            DccsPool dpGolemplainsMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/golemplains/dpGolemplainsMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsGolemplainsMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGolemplainsMonstersDLC2.asset").WaitForCompletion();

            NEW_ENTRY = new DccsPool.ConditionalPoolEntry
            {
                dccs = dccsGolemplainsMonstersDLC2,
                requiredExpansions = required,
                weight = 1,
            };
            ArrayUtils.ArrayAppend(ref dpGolemplainsMonsters.poolCategories[0].includedIfConditionsMet, NEW_ENTRY);

            DirectorCardCategorySelection dccsHabitatInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitat/dccsHabitatInteractables.asset").WaitForCompletion();
            dccsHabitatInteractables.categories[4].cards[0].forbiddenUnlockableDef = Addressables.LoadAssetAsync<UnlockableDef>(key: "RoR2/DLC2/habitat/Logs.Stages.habitat.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsVillageInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageInteractablesDLC1.asset").WaitForCompletion();
            dccsVillageInteractablesDLC1.categories[5].cards[3].minimumStageCompletions = 1;

            //

            DccsPool dpAncientLoftMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/ancientloft/dpAncientLoftMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsAncientLoftMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsAncientLoftMonstersDLC2.asset").WaitForCompletion();

            NEW_ENTRY = new DccsPool.ConditionalPoolEntry
            {
                dccs = dccsAncientLoftMonstersDLC2,
                requiredExpansions = required,
                weight = 1,
            };
            ArrayUtils.ArrayAppend(ref dpAncientLoftMonsters.poolCategories[0].includedIfConditionsMet, NEW_ENTRY);


        }
    }
    }