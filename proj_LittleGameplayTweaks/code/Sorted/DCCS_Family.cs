using RoR2;
using RoR2.ExpansionManagement;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LittleGameplayTweaks
{
    public class DCCS_Family
    {
        public static DirectorCardCategorySelection dccsMoonVoids;
        public static void Start()
        {
            On.RoR2.DccsPool.GenerateWeightedCategorySelection += DccsPool_GenerateWeightedCategorySelection;
            if (WConfig.cfgDccsFamily.Value == false)
            {
                return;
            }

            DccsPool dpGolemplainsMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/golemplains/dpGolemplainsMonsters.asset").WaitForCompletion();
            DccsPool dpBlackBeachMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/blackbeach/dpBlackBeachMonsters.asset").WaitForCompletion();
            DccsPool dpSnowyForestMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/snowyforest/dpSnowyForestMonsters.asset").WaitForCompletion();
            DccsPool dpLakesMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/lakes/dpLakesMonsters.asset").WaitForCompletion();
            DccsPool dpLakesnightMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/lakesnight/dpLakesnightMonsters.asset").WaitForCompletion();
            DccsPool dpVillageNightMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/villagenight/dpVillageNightMonsters.asset").WaitForCompletion();

            DccsPool dpGooLakeMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/goolake/dpGooLakeMonsters.asset").WaitForCompletion();
            DccsPool dpFoggySwampMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/foggyswamp/dpFoggySwampMonsters.asset").WaitForCompletion();
            DccsPool dpAncientLoftMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/ancientloft/dpAncientLoftMonsters.asset").WaitForCompletion();
            DccsPool dpLemurianTempleMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/lemuriantemple/dpLemurianTempleMonsters.asset").WaitForCompletion();

            DccsPool dpFrozenWallMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/frozenwall/dpFrozenWallMonsters.asset").WaitForCompletion();
            DccsPool dpWispGraveyardMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/wispgraveyard/dpWispGraveyardMonsters.asset").WaitForCompletion();
            DccsPool dpSulfurPoolsMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/sulfurpools/dpSulfurPoolsMonsters.asset").WaitForCompletion();
            DccsPool dpHabitatMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/habitat/dpHabitatMonsters.asset").WaitForCompletion();
            DccsPool dpHabitatfallMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/habitatfall/dpHabitatfallMonsters.asset").WaitForCompletion();

            DccsPool dpDampCaveMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/dampcave/dpDampCaveMonsters.asset").WaitForCompletion();
            DccsPool dpShipgraveyardMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/shipgraveyard/dpShipgraveyardMonsters.asset").WaitForCompletion();
            DccsPool dpRootJungleMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/rootjungle/dpRootJungleMonsters.asset").WaitForCompletion();

            DccsPool dpSkyMeadowMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/skymeadow/dpSkyMeadowMonsters.asset").WaitForCompletion();
            DccsPool dpHelminthRoostMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/helminthroost/dpHelminthRoostMonsters.asset").WaitForCompletion();

            DccsPool dpMoonMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/moon/dpMoonMonsters.asset").WaitForCompletion();
            DccsPool dpGoldshoresMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/goldshores/dpGoldshoresMonsters.asset").WaitForCompletion();
            DccsPool dpArtifactWorldMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/artifactworld/dpArtifactWorldMonsters.asset").WaitForCompletion();

            //
            FamilyDirectorCardCategorySelection dccsBeetleFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsBeetleFamily.asset").WaitForCompletion();
            //FamilyDirectorCardCategorySelection dccsBeetleSulfurFamily = UnityEngine.Object.Instantiate(dccsBeetleFamily);
            FamilyDirectorCardCategorySelection dccsGolemFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGolemFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsGolemFamilyAbyssal = UnityEngine.Object.Instantiate(dccsGolemFamily);
            FamilyDirectorCardCategorySelection dccsGupFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGupFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsImpFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsImpFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsJellyfishFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsJellyfishFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsLemurianFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLemurianFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsLunarFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsParentFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsParentFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsConstructFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsConstructFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsWispFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsWispFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsVoidFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsVoidFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsMushroomFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsMushroomFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsAcidLarvaFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsAcidLarvaFamily.asset").WaitForCompletion();

            dccsMoonVoids = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsMoonVoids.AddCategory("Champions", 2);
            dccsMoonVoids.AddCategory("Minibosses", 2);
            dccsMoonVoids.AddCategory("Basic Monsters", 4);
            dccsMoonVoids.AddCard(0, new DirectorCard
            {
                spawnCardReference = dccsVoidFamily.categories[0].cards[0].spawnCardReference,
                spawnCard = dccsVoidFamily.categories[0].cards[0].spawnCard,
                selectionWeight = 1,
                minimumStageCompletions = 7,
            });
            dccsMoonVoids.AddCard(1, new DirectorCard
            {
                spawnCardReference = dccsVoidFamily.categories[1].cards[0].spawnCardReference,
                spawnCard = dccsVoidFamily.categories[1].cards[0].spawnCard,
                selectionWeight = 1,
                minimumStageCompletions = 7,
            });
            dccsMoonVoids.AddCard(2, new DirectorCard
            {
                spawnCardReference = dccsVoidFamily.categories[2].cards[0].spawnCardReference,
                spawnCard = dccsVoidFamily.categories[2].cards[0].spawnCard,
                selectionWeight = 2,
            });
            dccsMoonVoids.AddCard(2, new DirectorCard
            {
                spawnCardReference = dccsVoidFamily.categories[2].cards[1].spawnCardReference,
                spawnCard = dccsVoidFamily.categories[2].cards[1].spawnCard,
                selectionWeight = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close,
            });
            dccsMoonVoids.name = "dccsMoonVoidMonstersEscape";
            
            FamilyDirectorCardCategorySelection dccsVoidFamilyLate = GameObject.Instantiate(dccsVoidFamily);
            dccsVoidFamilyLate.name = "dccsVoidFamilyLate";
            dccsVoidFamilyLate.minimumStageCompletion = 10;


            DirectorCard DC_Grandparent = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_Child = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/Child/cscChild.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_Geep = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGeepBody"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DC_Gip = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };



            int familyMin = WConfig.FamiliesStage1.Value ? 0 : 1;

            dccsGolemFamilyAbyssal.categories[0].cards[0].spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscTitanDampCave");
            dccsGolemFamilyAbyssal.name = "dccsGolemFamilyAbyssal";

            dccsBeetleFamily.minimumStageCompletion = familyMin;
            dccsLemurianFamily.minimumStageCompletion = familyMin;
            dccsJellyfishFamily.minimumStageCompletion = familyMin;
            dccsGupFamily.minimumStageCompletion = familyMin;
            dccsImpFamily.minimumStageCompletion = familyMin;
            dccsWispFamily.minimumStageCompletion = familyMin;

            dccsLemurianFamily.maximumStageCompletion = 14;
            dccsLunarFamily.minimumStageCompletion = 4;
            dccsLunarFamily.maximumStageCompletion = 1000000;
            dccsVoidFamily.minimumStageCompletion = 4;

            dccsParentFamily.AddCategory("Champions", 4);
            dccsParentFamily.AddCard(1, DC_Grandparent);
            dccsParentFamily.AddCategory("Basic", 6);
            dccsParentFamily.AddCard(2, DC_Child);

            dccsGupFamily.categories[0].cards[0].selectionWeight = 3;
            dccsGupFamily.AddCard(0, DC_Geep);
            dccsGupFamily.AddCard(0, DC_Gip);

            //Family Event Changes
            //0 is Normal
            //1 is Family
            //2 is Void
            ExpansionDef DLC1 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC1/Common/DLC1.asset").WaitForCompletion();
            ExpansionDef DLC2 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC2/Common/DLC2.asset").WaitForCompletion();

            ExpansionDef[] ExpansionDLC1 = { DLC1 };


            DccsPool.ConditionalPoolEntry FamilyBeetle = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsBeetleFamily };
            DccsPool.ConditionalPoolEntry FamilyLemurian = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsLemurianFamily };
            DccsPool.ConditionalPoolEntry FamilyJellyfish = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsJellyfishFamily };
            DccsPool.ConditionalPoolEntry FamilyGolemAbyssal = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsGolemFamilyAbyssal };
            DccsPool.ConditionalPoolEntry FamilyParent = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsParentFamily };
            DccsPool.ConditionalPoolEntry FamilyLunar = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsLunarFamily };
            DccsPool.ConditionalPoolEntry FamilyImp = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsImpFamily };
            DccsPool.ConditionalPoolEntry FamilyConstruct = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsConstructFamily, requiredExpansions = ExpansionDLC1 };
            DccsPool.ConditionalPoolEntry FamilyGup = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsGupFamily, requiredExpansions = ExpansionDLC1 };

            DccsPool.ConditionalPoolEntry FamilyVoidLate = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsVoidFamilyLate, requiredExpansions = ExpansionDLC1 };

            DccsPool.ConditionalPoolEntry FamilyLarva = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsAcidLarvaFamily, requiredExpansions = ExpansionDLC1 };
            DccsPool.ConditionalPoolEntry FamilyMushroom = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsMushroomFamily };

            //DccsPool.Category CategoryFamilyArtifactWorld = new DccsPool.Category { categoryWeight = 0.02f, name = "Family", alwaysIncluded = NoPoolEntries, includedIfNoConditionsMet = NoPoolEntries };
            DccsPool.Category CategoryFamilyMoon2 = new DccsPool.Category
            {
                categoryWeight = 0.02f,
                name = "Family",
                alwaysIncluded = new DccsPool.PoolEntry[0],
                includedIfNoConditionsMet = new DccsPool.PoolEntry[0],
                includedIfConditionsMet = new DccsPool.ConditionalPoolEntry[]
                {
                    FamilyVoidLate
                }
            };
            DccsPool.Category CategoryFamilyGoldshores = new DccsPool.Category
            {
                categoryWeight = 0.02f,
                name = "Family",
                alwaysIncluded = new DccsPool.PoolEntry[0],
                includedIfNoConditionsMet = new DccsPool.PoolEntry[0],
                includedIfConditionsMet = new DccsPool.ConditionalPoolEntry[]
                {
                    FamilyParent,
                    FamilyConstruct,
                    FamilyLunar,
                }
            };

            dpGolemplainsMonsters.poolCategories[1].includedIfConditionsMet[2] = FamilyConstruct;

            dpSnowyForestMonsters.poolCategories[1].includedIfConditionsMet[3] = FamilyImp;

            dpLakesMonsters.poolCategories[1].includedIfConditionsMet[0] = FamilyGup;
            dpLakesMonsters.poolCategories[1].includedIfConditionsMet[0] = FamilyGup;
            dpLakesnightMonsters.poolCategories[1].includedIfConditionsMet[3] = FamilyParent;
            dpLakesnightMonsters.poolCategories[1].includedIfConditionsMet[3] = FamilyParent;

            dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet[0] = FamilyMushroom;
            dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet[3] = FamilyLarva;

            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[3] = FamilyLemurian;
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[0].weight = 0;
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[2].weight = 0;


            dpFrozenWallMonsters.poolCategories[1].includedIfConditionsMet[2] = FamilyConstruct;
            AddFamily(dpWispGraveyardMonsters, FamilyBeetle);
            AddFamily(dpSulfurPoolsMonsters, FamilyParent);
            AddFamily(dpSulfurPoolsMonsters, FamilyLarva);

            AddFamily(dpHabitatMonsters, FamilyConstruct);

            AddFamily(dpDampCaveMonsters, FamilyGolemAbyssal);
            AddFamily(dpDampCaveMonsters, FamilyParent);

            AddFamily(dpDampCaveMonsters, FamilyParent);
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[2] = FamilyConstruct;
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[3] = FamilyConstruct;

            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet[1] = FamilyJellyfish;
            AddFamily(dpRootJungleMonsters, FamilyMushroom);

            AddFamily(dpSkyMeadowMonsters, FamilyParent);
            AddFamily(dpHelminthRoostMonsters, FamilyConstruct);

            //dpArtifactWorldMonsters.poolCategories = dpArtifactWorldMonsters.poolCategories.Add(CategoryFamilyArtifactWorld);
            //dpArtifactWorldMonsters.poolCategories[1].includedIfConditionsMet = dpArtifactWorldMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraLunar, FamilyExtraImp);

            HG.ArrayUtils.ArrayAppend(ref dpMoonMonsters.poolCategories, CategoryFamilyMoon2);
            HG.ArrayUtils.ArrayAppend(ref dpGoldshoresMonsters.poolCategories, CategoryFamilyGoldshores);

            AddInvasion(dpVillageNightMonsters, FamilyLunar);
            AddInvasion(dpAncientLoftMonsters, FamilyLunar);
            AddInvasion(dpLemurianTempleMonsters, FamilyLunar);
            AddInvasion(dpWispGraveyardMonsters, FamilyLunar);
            AddInvasion(dpHabitatfallMonsters, FamilyLunar);
            AddInvasion(dpSkyMeadowMonsters, FamilyLunar);
            AddInvasion(dpHelminthRoostMonsters, FamilyLunar);


        }

        public static WeightedSelection<DccsPool.Category> DccsPool_GenerateWeightedCategorySelection(On.RoR2.DccsPool.orig_GenerateWeightedCategorySelection orig, DccsPool self)
        {
            if (self.poolCategories.Length == 3 && self.poolCategories[1].name == "Family")
            {
                if (WConfig.cfgFamilyChance.Value != 2)
                {
                    self.poolCategories[0].categoryWeight = 1f - WConfig.cfgFamilyChance.Value / 100f;
                    self.poolCategories[1].categoryWeight = WConfig.cfgFamilyChance.Value / 100f;
                    self.poolCategories[2].categoryWeight = WConfig.cfgFamilyChance.Value / 100f;
                }
            }
            return orig(self);
        }

        public static void AddFamily(DccsPool pool, DccsPool.ConditionalPoolEntry family)
        {
            HG.ArrayUtils.ArrayAppend(ref pool.poolCategories[1].includedIfConditionsMet, family);
        }
        public static void AddInvasion(DccsPool pool, DccsPool.ConditionalPoolEntry invasion)
        {
            HG.ArrayUtils.ArrayAppend(ref pool.poolCategories[2].includedIfConditionsMet, invasion);
        }

    }


}