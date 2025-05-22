using RoR2;
using RoR2.ExpansionManagement;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LittleGameplayFeatures
{
    public class NewFamilyEvents
    {
        public static FamilyDirectorCardCategorySelection dccsClayFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
        public static FamilyDirectorCardCategorySelection dccsRoboBallFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
        public static FamilyDirectorCardCategorySelection dccsRoboBall2Family = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
        public static FamilyDirectorCardCategorySelection dccsVerminFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
        public static FamilyDirectorCardCategorySelection dccsVerminFamilySnowy = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
        public static FamilyDirectorCardCategorySelection dccsWormsFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();


        public static void Families()
        {
            if (WConfig.cfgNewFamilies.Value == false)
            {
                return;
            }
            //DccsPool dpGolemplainsMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/golemplains/dpGolemplainsMonsters.asset").WaitForCompletion();
            //DccsPool dpBlackBeachMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/blackbeach/dpBlackBeachMonsters.asset").WaitForCompletion();
            DccsPool dpSnowyForestMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/snowyforest/dpSnowyForestMonsters.asset").WaitForCompletion();
            //DccsPool dpVillageNightMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/villagenight/dpVillageNightMonsters.asset").WaitForCompletion();
            DccsPool dpLakesnightMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/lakesnight/dpLakesnightMonsters.asset").WaitForCompletion();

            DccsPool dpGooLakeMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/goolake/dpGooLakeMonsters.asset").WaitForCompletion();
            //DccsPool dpFoggySwampMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/foggyswamp/dpFoggySwampMonsters.asset").WaitForCompletion();
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

            //DccsPool dpSkyMeadowMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/skymeadow/dpSkyMeadowMonsters.asset").WaitForCompletion();
            DccsPool dpHelminthRoostMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/helminthroost/dpHelminthRoostMonsters.asset").WaitForCompletion();


            //
            FamilyDirectorCardCategorySelection dccsBeetleFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsBeetleFamily.asset").WaitForCompletion();


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

            dccsClayFamily.AddCategory("Champions", 3);
            dccsClayFamily.AddCategory("Minibosses", 6);
            dccsClayFamily.AddCard(0, DC_ClayBoss);
            dccsClayFamily.AddCard(1, DC_ClayTemp);
            dccsClayFamily.AddCard(1, DC_ClayGrenadier);
            dccsClayFamily.name = "dccsClayFamily";
            dccsClayFamily.minimumStageCompletion = dccsBeetleFamily.minimumStageCompletion;
            dccsClayFamily.maximumStageCompletion = 1000000;
            dccsClayFamily.selectionChatString = "FAMILY_CLAY";

            dccsRoboBallFamily.AddCategory("Champions", 4);
            dccsRoboBallFamily.AddCategory("Minibosses", 4);
            dccsRoboBallFamily.AddCategory("Basic Monsters", 4);
            dccsRoboBallFamily.AddCard(0, DC_RoboBallBoss);
            dccsRoboBallFamily.AddCard(1, DC_RoboBallMini);
            dccsRoboBallFamily.AddCard(2, DC_VultureNoCeling);
            dccsRoboBallFamily.name = "dccsRoboBallFamily";
            dccsRoboBallFamily.minimumStageCompletion = dccsBeetleFamily.minimumStageCompletion;
            dccsRoboBallFamily.maximumStageCompletion = 1000000;
            dccsRoboBallFamily.selectionChatString = "FAMILY_ROBOBALL";

            dccsVerminFamily.AddCategory("Basic Monsters", 6);
            dccsVerminFamily.AddCard(0, DC_BlindPest);
            dccsVerminFamily.AddCard(0, DC_BlindVermin);
            dccsVerminFamily.name = "dccsVerminFamily";
            dccsVerminFamily.minimumStageCompletion = dccsBeetleFamily.minimumStageCompletion;
            dccsVerminFamily.maximumStageCompletion = 11;
            dccsVerminFamily.selectionChatString = "FAMILY_PESTS";

            dccsVerminFamilySnowy.AddCategory("Basic Monsters", 6);
            dccsVerminFamilySnowy.AddCard(0, DC_BlindPestSnowy);
            dccsVerminFamilySnowy.AddCard(0, DC_BlindVerminSnowy);
            dccsVerminFamilySnowy.name = "dccsVerminSnowyFamily";
            dccsVerminFamilySnowy.minimumStageCompletion = dccsBeetleFamily.minimumStageCompletion;
            dccsVerminFamilySnowy.maximumStageCompletion = 11;
            dccsVerminFamilySnowy.selectionChatString = "FAMILY_PESTS";
            //<style=cWorldEvent>[WARNING] You hear squeaks and chirps around you..</style>

            dccsWormsFamily.AddCategory("Champions", 5);
            dccsWormsFamily.AddCard(0, DC_MagmaWormWithElite);
            dccsWormsFamily.AddCard(0, DC_ElectricWormWithElite);
            dccsWormsFamily.AddCategory("Miniboss", 5);
            dccsWormsFamily.AddCard(1, DC_Scorchling);
            dccsWormsFamily.minimumStageCompletion = 2;
            dccsWormsFamily.maximumStageCompletion = 1000000;
            dccsWormsFamily.name = "dccsWormsFamily";
            dccsWormsFamily.selectionChatString = "FAMILY_WORMS";




            //Family Event Changes
            //0 is Normal
            //1 is Family
            //2 is Void
            ExpansionDef DLC1 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC1/Common/DLC1.asset").WaitForCompletion();
            ExpansionDef DLC2 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC2/Common/DLC2.asset").WaitForCompletion();

            ExpansionDef[] ExpansionNone = new ExpansionDef[0];
            ExpansionDef[] ExpansionDLC1 = { DLC1 };
            ExpansionDef[] ExpansionDLC2 = { DLC2 };

            DccsPool.ConditionalPoolEntry FamilyVermin = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsVerminFamily, requiredExpansions = ExpansionDLC1 };
            DccsPool.ConditionalPoolEntry FamilyVerminSnow = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsVerminFamilySnowy, requiredExpansions = ExpansionDLC1 };
            DccsPool.ConditionalPoolEntry FamilyClay = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsClayFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyRobo = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsRoboBallFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyWorms = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsWormsFamily, requiredExpansions = ExpansionDLC2 };


            AddFamily(dpSnowyForestMonsters, FamilyVerminSnow);
            AddFamily(dpLakesnightMonsters, FamilyRobo);

            AddFamily(dpGooLakeMonsters, FamilyClay);
            AddFamily(dpAncientLoftMonsters, FamilyClay);
            AddFamily(dpAncientLoftMonsters, FamilyVermin);
            AddFamily(dpAncientLoftMonsters, FamilyWorms);
            AddFamily(dpLemurianTempleMonsters, FamilyClay);

            AddFamily(dpFrozenWallMonsters, FamilyRobo);
            AddFamily(dpWispGraveyardMonsters, FamilyClay);
            AddFamily(dpWispGraveyardMonsters, FamilyWorms);
            AddFamily(dpSulfurPoolsMonsters, FamilyWorms);
            AddFamily(dpHabitatMonsters, FamilyRobo);
            AddFamily(dpHabitatfallMonsters, FamilyRobo);

            AddFamily(dpDampCaveMonsters, FamilyWorms);
            AddFamily(dpShipgraveyardMonsters, FamilyRobo);
            AddFamily(dpRootJungleMonsters, FamilyVermin);
            AddFamily(dpRootJungleMonsters, FamilyClay);

            //AddFamily(dpSkyMeadowMonsters, FamilyWorms);
            AddFamily(dpHelminthRoostMonsters, FamilyWorms);
        }

        public static void AddFamily(DccsPool pool, DccsPool.ConditionalPoolEntry family)
        {
            HG.ArrayUtils.ArrayAppend(ref pool.poolCategories[1].includedIfConditionsMet, family);
        }


        public static void ModSupport()
        {
            CharacterSpawnCard[] CSCList = Object.FindObjectsOfType(typeof(CharacterSpawnCard)) as CharacterSpawnCard[];
            for (var i = 0; i < CSCList.Length; i++)
            {
                //Debug.LogWarning(CSCList[i]);
                switch (CSCList[i].name)
                {
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
                }
            }
        }

    }


}