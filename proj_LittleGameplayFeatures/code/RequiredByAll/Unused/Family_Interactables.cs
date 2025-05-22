using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MiscContent
{
    public class InteractableFamilyEvents
    {
        public static FamilyDirectorCardCategorySelection dccsDamageInteractablesFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
        public static FamilyDirectorCardCategorySelection dccsHealingInteractablesFamily;
        public static FamilyDirectorCardCategorySelection dccsUtilityInteractablesFamily;

        public static void Start()
        {
            //On.RoR2.DccsPool.GenerateWeightedSelection += DccsPool_GenerateWeightedSelection;

            //Chests
            DirectorCard ADChest1 = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscChest1"),
                selectionWeight = 120,
            };
            DirectorCard ADChest2 = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscChest2"),
                selectionWeight = 20,
            };
            DirectorCard ADEquipmentBarrel = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscEquipmentBarrel"),
                selectionWeight = 20,
            };
            DirectorCard ADTripleShop = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscTripleShop"),
                selectionWeight = 20,
            };
            DirectorCard ADLunarChest = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscLunarChest"),
                selectionWeight = 10,
            };

            DirectorCard ADTripleShopLarge = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscTripleShopLarge"),
                selectionWeight = 40,
            };
            DirectorCard ADCasinoChest = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscCasinoChest"),
                selectionWeight = 40,
            };
            DirectorCard ADTripleShopEquipment = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscTripleShopEquipment"),
                selectionWeight = 20,
            };


            //DLC Chests
            DirectorCard ADCategoryChestDamage = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscCategoryChestDamage"),
                selectionWeight = 240,
            };
            DirectorCard ADCategoryChestHealing = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscCategoryChestHealing"),
                selectionWeight = 240,
            };
            DirectorCard ADCategoryChestUtility = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscCategoryChestUtility"),
                selectionWeight = 240,
            };

            DirectorCard ADCategoryChest2Damage = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                selectionWeight = 40,
            };
            DirectorCard ADCategoryChest2Healing = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Healing.asset").WaitForCompletion(),
                selectionWeight = 40,
            };
            DirectorCard ADCategoryChest2Utility = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Utility.asset").WaitForCompletion(),
                selectionWeight = 40,
            };



            //ChestsEnd
            //Barrel
            DirectorCard ADBarrel1 = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBarrel1"),
                selectionWeight = 10,
            };


            //BarrelEnd
            //Shrines
            DirectorCard ADShrineCombat = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineCombat"),
                selectionWeight = 30,
            };
            DirectorCard ADShrineBoss = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineBoss"),
                selectionWeight = 20,
            };
            //
            DirectorCard ADShrineBlood = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineBlood"),
                selectionWeight = 30,
            };
            DirectorCard ADShrineHealing = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineHealing"),
                selectionWeight = 20,
            };
            DirectorCard ADShrineCleanse = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineCleanse"),
                selectionWeight = 10,
            };
            //
            DirectorCard ADShrineChance = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineChance"),
                selectionWeight = 40,
            };
            DirectorCard ADShrineRestack = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineRestack"),
                selectionWeight = 10,
            };
            //
            //SAND
            /*DirectorCard ADShrineCombat_SAND = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineCombat"),
                selectionWeight = 30,
            };
            DirectorCard ADShrineBoss_SAND = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineBoss"),
                selectionWeight = 20,
            };
            //
            DirectorCard ADShrineBlood_SAND = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineBlood"),
                selectionWeight = 30,
            };
            DirectorCard ADShrineCleanse_SAND = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineCleanse"),
                selectionWeight = 10,
            };
            //
            DirectorCard ADShrineChance_SAND = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineChance"),
                selectionWeight = 40,
            };
            DirectorCard ADShrineRestack_SAND = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineRestack"),
                selectionWeight = 10,
            };*/


            //ShrinesEnd
            //Drones
            DirectorCard ADBrokenDrone1 = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenDrone1"),
                selectionWeight = 21,
            };
            DirectorCard ADBrokenDrone2 = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenDrone2"),
                selectionWeight = 21,
            };
            DirectorCard ADBrokenEmergencyDrone = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenEmergencyDrone"),
                selectionWeight = 14,
            };
            DirectorCard ADBrokenEquipmentDrone = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenEquipmentDrone"),
                selectionWeight = 14,
            };
            DirectorCard ADBrokenFlameDrone = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenFlameDrone"),
                selectionWeight = 14,
            };
            DirectorCard ADBrokenMegaDrone = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenMegaDrone"),
                selectionWeight = 3,
                minimumStageCompletions = 2,
            };
            DirectorCard ADBrokenMissileDrone = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenMissileDrone"),
                selectionWeight = 14,
            };
            DirectorCard ADBrokenTurret1 = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenTurret1"),
                selectionWeight = 14,
            };
            //DronesEnd
            //Rare
            DirectorCard ADGoldChest = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscGoldChest"),
                selectionWeight = 3,
                minimumStageCompletions = 4,
            };
            DirectorCard ADShrineGoldshoresAccess = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineGoldshoresAccess"),
                selectionWeight = 2,
                minimumStageCompletions = 2,
            };
            DirectorCard ADChest1Stealthed = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscChest1Stealthed"),
                selectionWeight = 6,
            };



            DirectorCard ADVoidChest = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidChest/iscVoidChest.asset").WaitForCompletion(),
                selectionWeight = 15,
            };
            DirectorCard ADVoidCamp = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidCamp/iscVoidCamp.asset").WaitForCompletion(),
                selectionWeight = 7,
                minimumStageCompletions = 1,
            };
            DirectorCard AdShrineHalcyonite = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/iscShrineHalcyonite.asset").WaitForCompletion(),
                selectionWeight = 2,
                minimumStageCompletions = 1
            };
            DirectorCard ADShrineShaping = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/iscShrineColossusAccess.asset").WaitForCompletion(),
                selectionWeight = 2,
                minimumStageCompletions = 3,
            };
            //RareEnd
            //Duplicators
            DirectorCard ADDuplicator = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscDuplicator"),
                selectionWeight = 30,
            };
            DirectorCard ADDuplicatorLarge = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscDuplicatorLarge"),
                selectionWeight = 7,
                minimumStageCompletions = 1,
            };
            DirectorCard ADDuplicatorMilitary = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscDuplicatorMilitary"),
                selectionWeight = 2,
                minimumStageCompletions = 3,
            };
            DirectorCard ADDuplicatorWild = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscDuplicatorWild"),
                selectionWeight = 2,
                minimumStageCompletions = 2,
            };
            DirectorCard ADScrapper = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscScrapper"),
                selectionWeight = 12,
            };




            dccsDamageInteractablesFamily.name = "dccsDamageInteractablesFamily";

            dccsDamageInteractablesFamily.AddCategory("Chests", 45); //0
            dccsDamageInteractablesFamily.AddCategory("Barrels", 10); //1
            dccsDamageInteractablesFamily.AddCategory("Shrines", 15); //2
            dccsDamageInteractablesFamily.AddCategory("Drones", 21); //3
            dccsDamageInteractablesFamily.AddCategory("Misc", 0); //4
            dccsDamageInteractablesFamily.AddCategory("Rare", 0.4f); //5
            dccsDamageInteractablesFamily.AddCategory("Duplicator", 8f); //6
            dccsDamageInteractablesFamily.AddCategory("Void Stuff", 1f); //7
            dccsDamageInteractablesFamily.AddCategory("Storm Stuff", 1f); //8

            dccsDamageInteractablesFamily.AddCard(0, ADChest1);
            dccsDamageInteractablesFamily.AddCard(0, ADChest2);
            dccsDamageInteractablesFamily.AddCard(0, ADTripleShop);
            dccsDamageInteractablesFamily.AddCard(0, ADEquipmentBarrel);

            dccsDamageInteractablesFamily.AddCard(3, ADBarrel1);

            dccsDamageInteractablesFamily.AddCard(5, ADChest1Stealthed);
            dccsDamageInteractablesFamily.AddCard(5, ADGoldChest);
            dccsDamageInteractablesFamily.AddCard(5, ADShrineGoldshoresAccess);

            dccsDamageInteractablesFamily.AddCard(6, ADDuplicator);
            dccsDamageInteractablesFamily.AddCard(6, ADDuplicatorLarge);
            dccsDamageInteractablesFamily.AddCard(6, ADDuplicatorMilitary);
            dccsDamageInteractablesFamily.AddCard(6, ADDuplicatorWild);
            dccsDamageInteractablesFamily.AddCard(6, ADScrapper);

            dccsDamageInteractablesFamily.AddCard(7, ADVoidChest);
            dccsDamageInteractablesFamily.AddCard(7, ADVoidCamp);
            dccsDamageInteractablesFamily.AddCard(8, ADShrineShaping);
            dccsDamageInteractablesFamily.AddCard(8, AdShrineHalcyonite);

            dccsHealingInteractablesFamily = Object.Instantiate<FamilyDirectorCardCategorySelection>(dccsDamageInteractablesFamily);
            dccsUtilityInteractablesFamily = Object.Instantiate<FamilyDirectorCardCategorySelection>(dccsDamageInteractablesFamily);
            dccsHealingInteractablesFamily.name = "dccsHealingInteractablesFamily";
            dccsUtilityInteractablesFamily.name = "dccsUtilityInteractablesFamily";

            dccsDamageInteractablesFamily.AddCard(0, ADCategoryChestDamage);
            dccsDamageInteractablesFamily.AddCard(0, ADCategoryChest2Damage);

            dccsHealingInteractablesFamily.AddCard(0, ADCategoryChestHealing);
            dccsHealingInteractablesFamily.AddCard(0, ADCategoryChest2Healing);
            dccsHealingInteractablesFamily.AddCard(0, ADTripleShopLarge);
            dccsHealingInteractablesFamily.AddCard(0, ADLunarChest);

            dccsUtilityInteractablesFamily.AddCard(0, ADCategoryChestUtility);
            dccsUtilityInteractablesFamily.AddCard(0, ADCategoryChest2Utility);
            dccsUtilityInteractablesFamily.AddCard(0, ADTripleShopEquipment);
            dccsUtilityInteractablesFamily.AddCard(0, ADCasinoChest);

            dccsDamageInteractablesFamily.AddCard(2, ADShrineBoss);
            dccsDamageInteractablesFamily.AddCard(2, ADShrineCombat);

            dccsHealingInteractablesFamily.AddCard(2, ADShrineBlood);
            dccsHealingInteractablesFamily.AddCard(2, ADShrineHealing);
            dccsHealingInteractablesFamily.AddCard(2, ADShrineCleanse);

            dccsUtilityInteractablesFamily.AddCard(2, ADShrineChance);
            dccsUtilityInteractablesFamily.AddCard(2, ADShrineRestack);


            dccsDamageInteractablesFamily.AddCard(3, ADBrokenDrone1);
            dccsDamageInteractablesFamily.AddCard(3, ADBrokenMissileDrone);
            dccsDamageInteractablesFamily.AddCard(3, ADBrokenFlameDrone);
            dccsDamageInteractablesFamily.AddCard(3, ADBrokenMegaDrone);

            dccsHealingInteractablesFamily.AddCard(3, ADBrokenDrone2);
            dccsHealingInteractablesFamily.AddCard(3, ADBrokenEmergencyDrone);

            dccsUtilityInteractablesFamily.AddCard(3, ADBrokenEquipmentDrone);
            dccsUtilityInteractablesFamily.AddCard(3, ADBrokenTurret1);

            Add();
        }

        public static WeightedSelection<DirectorCardCategorySelection> DccsPool_GenerateWeightedSelection(On.RoR2.DccsPool.orig_GenerateWeightedSelection orig, DccsPool self)
        {
            var a = orig(self);

            for (int i = 0; i < a.choices.Length; i++)
            {
                Debug.Log(a.choices[i].value + " : " + a.choices[i].weight);
            }


            return a;
        }

        public static void Add()
        {
            DccsPool dpGolemplainsInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/golemplains/dpGolemplainsInteractables.asset").WaitForCompletion();
            DccsPool dpBlackBeachInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/blackbeach/dpBlackBeachInteractables.asset").WaitForCompletion();
            DccsPool dpSnowyForestInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/snowyforest/dpSnowyForestInteractables.asset").WaitForCompletion();
            DccsPool dpLakesInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/lakes/dpLakesInteractables.asset").WaitForCompletion();
            DccsPool dpLakesnightInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/lakesnight/dpLakesnightInteractables.asset").WaitForCompletion();
            DccsPool dpVillageInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/village/dpVillageInteractables.asset").WaitForCompletion();
            //DccsPool dpGolemplainsMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/golemplains/dpGolemplainsMonsters.asset").WaitForCompletion();

            DccsPool dpGooLakeInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/goolake/dpGooLakeInteractables.asset").WaitForCompletion();
            DccsPool dpFoggySwampInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/foggyswamp/dpFoggySwampInteractables.asset").WaitForCompletion();
            DccsPool dpAncientLoftInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/ancientloft/dpAncientLoftInteractables.asset").WaitForCompletion();
            DccsPool dpLemurianTempleInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/lemuriantemple/dpLemurianTempleInteractables.asset").WaitForCompletion();

            DccsPool dpFrozenWallInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/frozenwall/dpFrozenWallInteractables.asset").WaitForCompletion();
            DccsPool dpWispGraveyardInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/wispgraveyard/dpWispGraveyardInteractables.asset").WaitForCompletion();
            DccsPool dpSulfurPoolsInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/sulfurpools/dpSulfurPoolsInteractables.asset").WaitForCompletion();
            DccsPool dpHabitatInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/habitat/dpHabitatInteractables.asset").WaitForCompletion();
            DccsPool dpHabitatfallInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/habitatfall/dpHabitatfallInteractables.asset").WaitForCompletion();

            DccsPool dpDampCaveInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/dampcave/dpDampCaveInteractables.asset").WaitForCompletion();
            DccsPool dpShipgraveyardInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/shipgraveyard/dpShipgraveyardInteractables.asset").WaitForCompletion();
            DccsPool dpRootJungleInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/rootjungle/dpRootJungleInteractables.asset").WaitForCompletion();

            DccsPool dpSkyMeadowInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/skymeadow/dpSkyMeadowInteractables.asset").WaitForCompletion();
            DccsPool dpHelminthRoostInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC2/helminthroost/dpHelminthRoostInteractables.asset").WaitForCompletion();


            DccsPool.Category ExtraCategory = new DccsPool.Category
            {
                categoryWeight = 0.005f,
                name = "Family",
                alwaysIncluded = new DccsPool.PoolEntry[] {
            new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsDamageInteractablesFamily, requiredExpansions = null },
            new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsHealingInteractablesFamily, requiredExpansions = null },
            new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsUtilityInteractablesFamily, requiredExpansions = null }
            },
                includedIfNoConditionsMet = new DccsPool.PoolEntry[0]
            ,
                includedIfConditionsMet = new DccsPool.ConditionalPoolEntry[0],
            };

            HG.ArrayUtils.ArrayAppend(ref dpGolemplainsInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpBlackBeachInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpSnowyForestInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpLakesInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpLakesnightInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpVillageInteractables.poolCategories, ExtraCategory);

            HG.ArrayUtils.ArrayAppend(ref dpGooLakeInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpFoggySwampInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpAncientLoftInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpLemurianTempleInteractables.poolCategories, ExtraCategory);

            HG.ArrayUtils.ArrayAppend(ref dpFrozenWallInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpWispGraveyardInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpSulfurPoolsInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpHabitatInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpHabitatfallInteractables.poolCategories, ExtraCategory);

            HG.ArrayUtils.ArrayAppend(ref dpDampCaveInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpShipgraveyardInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpRootJungleInteractables.poolCategories, ExtraCategory);

            HG.ArrayUtils.ArrayAppend(ref dpSkyMeadowInteractables.poolCategories, ExtraCategory);
            HG.ArrayUtils.ArrayAppend(ref dpHelminthRoostInteractables.poolCategories, ExtraCategory);


        }
    }

}