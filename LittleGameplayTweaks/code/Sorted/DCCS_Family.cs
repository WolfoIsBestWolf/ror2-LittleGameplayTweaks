using RoR2;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LittleGameplayTweaks
{
    public class FamilyEvents
    {
        public static FamilyDirectorCardCategorySelection dccsClayFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
 
 
        public static void Families()
        {
            DccsPool dpGolemplainsMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/golemplains/dpGolemplainsMonsters.asset").WaitForCompletion();
            DccsPool dpBlackBeachMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/blackbeach/dpBlackBeachMonsters.asset").WaitForCompletion();
            DccsPool dpSnowyForestMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/snowyforest/dpSnowyForestMonsters.asset").WaitForCompletion();
            DccsPool dpVillageNightMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/villagenight/dpVillageNightMonsters.asset").WaitForCompletion();

            DccsPool dpGooLakeMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/goolake/dpGooLakeMonsters.asset").WaitForCompletion();
            DccsPool dpFoggySwampMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/foggyswamp/dpFoggySwampMonsters.asset").WaitForCompletion();
            DccsPool dpAncientLoftMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/ancientloft/dpAncientLoftMonsters.asset").WaitForCompletion();
            DccsPool dpLemurianTempleMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/lemuriantemple/dpLemurianTempleMonsters.asset").WaitForCompletion();

            DccsPool dpFrozenWallMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/frozenwall/dpFrozenWallMonsters.asset").WaitForCompletion();
            DccsPool dpWispGraveyardMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/wispgraveyard/dpWispGraveyardMonsters.asset").WaitForCompletion();
            DccsPool dpSulfurPoolsMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/sulfurpools/dpSulfurPoolsMonsters.asset").WaitForCompletion();

            DccsPool dpDampCaveMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/dampcave/dpDampCaveMonsters.asset").WaitForCompletion();
            DccsPool dpShipgraveyardMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/shipgraveyard/dpShipgraveyardMonsters.asset").WaitForCompletion();
            DccsPool dpRootJungleMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/rootjungle/dpRootJungleMonsters.asset").WaitForCompletion();

            DccsPool dpSkyMeadowMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/skymeadow/dpSkyMeadowMonsters.asset").WaitForCompletion();
            DccsPool dpHelminthRoostMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/helminthroost/dpHelminthRoostMonsters.asset").WaitForCompletion();

            DccsPool dpMoonMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/moon/dpMoonMonsters.asset").WaitForCompletion();
            DccsPool dpArtifactWorldMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/artifactworld/dpArtifactWorldMonsters.asset").WaitForCompletion();
            DccsPool dpVoidStageMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/voidstage/dpVoidStageMonsters.asset").WaitForCompletion();
            //
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
            FamilyDirectorCardCategorySelection dccsRoboBallFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
            FamilyDirectorCardCategorySelection dccsVerminFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
            FamilyDirectorCardCategorySelection dccsVerminFamilySnowy = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
            FamilyDirectorCardCategorySelection dccsWormsFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();



 
            DirectorCard DC_ClayBoss = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBoss"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_ClayTemp = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBruiser"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_ClayGrenadier = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DC_RoboBallBoss = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_RoboBallMini = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            CharacterSpawnCard cscVultureNoCeiling = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Vulture/cscVulture.asset").WaitForCompletion());
            cscVultureNoCeiling.name = "cscVultureNoCeiling";
            cscVultureNoCeiling.requiredFlags = NodeFlags.None;

            DirectorCard DC_VultureNoCeling = new DirectorCard
            {
                spawnCard = cscVultureNoCeiling,
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DC_VultureNoCeling.spawnCard = cscVultureNoCeiling;
            //Debug.LogWarning(DSVultureNoCeling.spawnCard);


            DirectorCard DC_Grandparent = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 1,
                preventOverhead = false,
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

            DirectorCard DC_BlindPest = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVermin"),
                preventOverhead = true,
                selectionWeight = 10,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_BlindVermin = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVermin"),
                preventOverhead = false,
                selectionWeight = 10,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_BlindPestSnowy = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVerminSnowy"),
                preventOverhead = true,
                selectionWeight = 10,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_BlindVerminSnowy = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVerminSnowy"),
                preventOverhead = false,
                selectionWeight = 10,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DC_MagmaWormWithElite = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DC_ElectricWormWithElite = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };

            DirectorCard DC_Scorchling = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/Scorchling/cscScorchling.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 1,
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

            int familyMin = WConfig.FamiliesStage1.Value ? 0 : 1;



            dccsGolemFamilyAbyssal.categories[0].cards[0].spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscTitanDampCave");
            dccsGolemFamilyAbyssal.name = "dccsGolemFamilyAbyssal";

            dccsClayFamily.AddCategory("Champions", 3);
            dccsClayFamily.AddCategory("Minibosses", 6);
            dccsClayFamily.AddCard(0, DC_ClayBoss);
            dccsClayFamily.AddCard(1, DC_ClayTemp);
            dccsClayFamily.AddCard(1, DC_ClayGrenadier);
            dccsClayFamily.name = "dccsClayFamily";
            dccsClayFamily.minimumStageCompletion = familyMin;
            dccsClayFamily.maximumStageCompletion = 1000000;
            dccsClayFamily.selectionChatString = "FAMILY_CLAY";

            dccsRoboBallFamily.AddCategory("Champions", 4);
            dccsRoboBallFamily.AddCategory("Minibosses", 4);
            dccsRoboBallFamily.AddCategory("Basic Monsters", 4);
            dccsRoboBallFamily.AddCard(0, DC_RoboBallBoss);
            dccsRoboBallFamily.AddCard(1, DC_RoboBallMini);
            dccsRoboBallFamily.AddCard(2, DC_VultureNoCeling);
            dccsRoboBallFamily.name = "dccsRoboBallFamily";
            dccsRoboBallFamily.minimumStageCompletion = familyMin;
            dccsRoboBallFamily.maximumStageCompletion = 1000000;
            dccsRoboBallFamily.selectionChatString = "FAMILY_ROBOBALL";

            dccsVerminFamily.AddCategory("Basic Monsters", 6);
            dccsVerminFamily.AddCard(0, DC_BlindPest);
            dccsVerminFamily.AddCard(0, DC_BlindVermin);
            dccsVerminFamily.name = "dccsVerminFamily";
            dccsVerminFamily.minimumStageCompletion = familyMin;
            dccsVerminFamily.maximumStageCompletion = 9;
            dccsVerminFamily.selectionChatString = "FAMILY_PESTS";

            dccsVerminFamilySnowy.AddCategory("Basic Monsters", 6);
            dccsVerminFamilySnowy.AddCard(0, DC_BlindPestSnowy);
            dccsVerminFamilySnowy.AddCard(0, DC_BlindVerminSnowy);
            dccsVerminFamilySnowy.name = "dccsVerminSnowyFamily";
            dccsVerminFamilySnowy.minimumStageCompletion = familyMin;
            dccsVerminFamilySnowy.maximumStageCompletion = 9;
            dccsVerminFamilySnowy.selectionChatString = "FAMILY_PESTS";
            //<style=cWorldEvent>[WARNING] You hear squeaks and chirps around you..</style>

            dccsWormsFamily.AddCategory("Champions", 5);
            dccsWormsFamily.AddCard(0, DC_MagmaWormWithElite);
            dccsWormsFamily.AddCard(0, DC_ElectricWormWithElite);
            dccsWormsFamily.AddCategory("Miniboss", 5);
            dccsWormsFamily.AddCard(1, DC_Scorchling);
            dccsWormsFamily.minimumStageCompletion = 7;
            dccsWormsFamily.maximumStageCompletion = 1000000;
            dccsWormsFamily.name = "dccsWormsFamily";
            dccsWormsFamily.selectionChatString = "FAMILY_WORMS";


            dccsBeetleFamily.minimumStageCompletion = familyMin;
            dccsLemurianFamily.minimumStageCompletion = familyMin;
            dccsJellyfishFamily.minimumStageCompletion = familyMin;
            dccsGupFamily.minimumStageCompletion = familyMin;     
            dccsImpFamily.minimumStageCompletion = familyMin;
            dccsWispFamily.minimumStageCompletion = familyMin;
            dccsConstructFamily.minimumStageCompletion = familyMin;

            dccsLemurianFamily.maximumStageCompletion = 14;
            dccsLunarFamily.minimumStageCompletion = 4;
            dccsLunarFamily.maximumStageCompletion = 1000000;
            dccsVoidFamily.minimumStageCompletion = 4;

            dccsParentFamily.AddCategory("Champions", 4);
            dccsParentFamily.AddCard(1, DC_Grandparent);
            dccsParentFamily.AddCategory("Basic", 6);
            dccsParentFamily.AddCard(2, DC_Child);
            dccsParentFamily.selectionChatString = "FAMILY_PARENT";

            dccsGupFamily.categories[0].cards[0].selectionWeight = 3;
            dccsGupFamily.AddCard(0, DC_Geep);
            dccsGupFamily.AddCard(0, DC_Gip);

            //LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGeepBody").directorCreditCost = 60;
            LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody").directorCreditCost = 25;


            //Family Event Changes
            //0 is Normal
            //1 is Family
            //2 is Void
            RoR2.ExpansionManagement.ExpansionDef[] ExpansionNone = { };
            //RoR2.ExpansionManagement.ExpansionDef[] ExpansionDLC1 = { };
            DccsPool.PoolEntry[] NoPoolEntries = { };


            DccsPool.ConditionalPoolEntry FamilyExtraVermin = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsVerminFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraBeetle = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsBeetleFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraClay = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsClayFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraGolemAbyssal = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsGolemFamilyAbyssal, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraParent = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsParentFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraLunar = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsLunarFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraImp = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsImpFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraVoid = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsVoidFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraWorms = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsWormsFamily, requiredExpansions = ExpansionNone };
            DccsPool.Category CategoryFamilyArtifactWorld = new DccsPool.Category { categoryWeight = 0.02f, name = "Family", alwaysIncluded = NoPoolEntries, includedIfNoConditionsMet = NoPoolEntries };
            DccsPool.Category CategoryFamilyMoon2 = new DccsPool.Category { categoryWeight = 0.01f, name = "Family", alwaysIncluded = NoPoolEntries, includedIfNoConditionsMet = NoPoolEntries };

            DccsPool.ConditionalPoolEntry FamilyExtraLarva = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsAcidLarvaFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraMushroom = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsMushroomFamily, requiredExpansions = ExpansionNone };



            dpGolemplainsMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsConstructFamily;

            dpSnowyForestMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsVerminFamilySnowy;
            dpSnowyForestMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsImpFamily;
            //
            dpGooLakeMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsClayFamily;

            dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet = dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet.Remove(dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet[0]);
            dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet = dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraMushroom, FamilyExtraLarva);

            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet = dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraVermin, FamilyExtraWorms);
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[0].dccs = dccsClayFamily;
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsLemurianFamily;
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet = dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet.Remove(dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[1], dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[2]);
            //
            dpFrozenWallMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsRoboBallFamily;

            dpWispGraveyardMonsters.poolCategories[1].includedIfConditionsMet = dpWispGraveyardMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraClay, FamilyExtraBeetle, FamilyExtraWorms);

           dpSulfurPoolsMonsters.poolCategories[1].includedIfConditionsMet = dpSulfurPoolsMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraParent, FamilyExtraLarva);
            //
            //Wouldn't we need to replace the normal Golem Family here or is there just no Golem Family
            dpDampCaveMonsters.poolCategories[1].includedIfConditionsMet = dpDampCaveMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraGolemAbyssal, FamilyExtraParent, FamilyExtraWorms);

            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet = dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet.Remove(dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[4]);
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsRoboBallFamily;
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsConstructFamily;
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet = dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraLarva);

            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet = dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraVermin);
            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsClayFamily;
            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsJellyfishFamily;
            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet = dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraMushroom, FamilyExtraLarva);


            dpSkyMeadowMonsters.poolCategories[1].includedIfConditionsMet = dpSkyMeadowMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraParent, FamilyExtraWorms);
            dpHelminthRoostMonsters.poolCategories[1].includedIfConditionsMet = dpHelminthRoostMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraParent, FamilyExtraWorms);

            //

            dpArtifactWorldMonsters.poolCategories = dpArtifactWorldMonsters.poolCategories.Add(CategoryFamilyArtifactWorld);
            dpArtifactWorldMonsters.poolCategories[1].includedIfConditionsMet = dpArtifactWorldMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraLunar, FamilyExtraImp);

            dpMoonMonsters.poolCategories = dpMoonMonsters.poolCategories.Add(CategoryFamilyMoon2);
            dpMoonMonsters.poolCategories[1].includedIfConditionsMet = dpMoonMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraVoid);



            dpVillageNightMonsters.poolCategories[2].alwaysIncluded = dpVillageNightMonsters.poolCategories[2].alwaysIncluded.Add(FamilyExtraLunar);
            dpAncientLoftMonsters.poolCategories[2].alwaysIncluded = dpAncientLoftMonsters.poolCategories[2].alwaysIncluded.Add(FamilyExtraLunar);
            dpLemurianTempleMonsters.poolCategories[2].alwaysIncluded = dpLemurianTempleMonsters.poolCategories[2].alwaysIncluded.Add(FamilyExtraLunar);
            dpWispGraveyardMonsters.poolCategories[2].alwaysIncluded = dpWispGraveyardMonsters.poolCategories[2].alwaysIncluded.Add(FamilyExtraLunar);
            dpSkyMeadowMonsters.poolCategories[2].alwaysIncluded = dpSkyMeadowMonsters.poolCategories[2].alwaysIncluded.Add(FamilyExtraLunar);
            dpHelminthRoostMonsters.poolCategories[2].alwaysIncluded = dpHelminthRoostMonsters.poolCategories[2].alwaysIncluded.Add(FamilyExtraLunar);


            DccsPool[] AllDccsPools = {
            dpGolemplainsMonsters, dpBlackBeachMonsters, dpSnowyForestMonsters,
            dpGooLakeMonsters,dpFoggySwampMonsters,dpAncientLoftMonsters,
            dpFrozenWallMonsters,dpWispGraveyardMonsters,dpSulfurPoolsMonsters,
            dpDampCaveMonsters,dpShipgraveyardMonsters,dpRootJungleMonsters,
            dpSkyMeadowMonsters};

            for (int i = 0; i < AllDccsPools.Length; i++)
            {
                AllDccsPools[i].poolCategories[0].categoryWeight = 1 - WConfig.DCCSEnemyFamilyChance.Value / 100f;
                AllDccsPools[i].poolCategories[1].categoryWeight = WConfig.DCCSEnemyFamilyChance.Value / 100f;
                /*Debug.Log("");
                Debug.Log(AllDccsPools[i]);
                for (int tw = 0; tw < AllDccsPools[i].poolCategories[1].includedIfConditionsMet.Length; tw++)
                {
                    Debug.Log(AllDccsPools[i].poolCategories[1].includedIfConditionsMet[tw].dccs);
                }*/
            }

        }
 
        public static void ModSupport()
        {
            FamilyDirectorCardCategorySelection dccsConstructFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsConstructFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsLemurianFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLemurianFamily.asset").WaitForCompletion();

            CharacterSpawnCard[] CSCList = Object.FindObjectsOfType(typeof(CharacterSpawnCard)) as CharacterSpawnCard[];
            for (var i = 0; i < CSCList.Length; i++)
            {
                //Debug.LogWarning(CSCList[i]);
                switch (CSCList[i].name)
                {
                    case "cscSigmaConstruct":
                        if (CSCList[i].directorCreditCost > 0)
                        {
                            DirectorCard DC__Sigma = new DirectorCard
                            {
                                spawnCard = CSCList[i],
                                selectionWeight = 1,
                                minimumStageCompletions = 0,
                                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                            };
                            dccsConstructFamily.AddCategory("Minibosses", 1);
                            dccsConstructFamily.AddCard(2, DC__Sigma);
                        }
                        break;
                    case "cscClayMan":
                        DirectorCard DC_ClayMan = new DirectorCard
                        {
                            spawnCard = CSCList[i],
                            selectionWeight = 1,
                            preventOverhead = false,
                            minimumStageCompletions = 0,
                            spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                        };
                        dccsClayFamily.AddCategory("Basic Monsters", 1);
                        dccsClayFamily.AddCard(2, DC_ClayMan);
                        break;
                    case "cscArchWisp":
                        break;
                    case "cscAncientWisp":
                        break;
                    case "cscDireseeker":
                        DirectorCard DC_Direseeker = new DirectorCard
                        {
                            spawnCard = CSCList[i],
                            selectionWeight = 1,
                            preventOverhead = false,
                            minimumStageCompletions = 0,
                            spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                        };
                        dccsLemurianFamily.AddCategory("Champions", 2);
                        dccsLemurianFamily.AddCard(2, DC_Direseeker);
                        break;
                }
            }
        }

        
    }

 
}