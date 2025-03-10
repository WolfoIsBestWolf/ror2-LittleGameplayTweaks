using R2API;
using RoR2;
using System.Collections.Generic;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LittleGameplayTweaks
{
    public class DCCSInteractables
    {
        public static List<DirectorCardCategorySelection> allDCCS = new List<DirectorCardCategorySelection>();
        public static void Start()
        {
            if (WConfig.DCCSInteractableCostChanges.Value == true)
            {
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/DuplicatorWild/iscDuplicatorWild.asset").WaitForCompletion().directorCreditCost = 15; //10 Default

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidTriple/iscVoidTriple.asset").WaitForCompletion().directorCreditCost = 30; //40 default

                //Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidCoinBarrel/iscVoidCoinBarrel.asset").WaitForCompletion().directorCreditCost = 15; //15 default
                LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscLunarChest").directorCreditCost = 15; //25 default
                LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscLunarChest").requiredFlags = 0;
                LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscBrokenMegaDrone").directorCreditCost = 35;

                LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscShrineHealing").directorCreditCost = 5;

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion().directorCreditCost = 5; //30 default (hopoo gaems)
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSandy.asset").WaitForCompletion().directorCreditCost = 5;
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSnowy.asset").WaitForCompletion().directorCreditCost = 5;

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBlood.asset").WaitForCompletion().directorCreditCost = 15; //20 default
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBloodSandy.asset").WaitForCompletion().directorCreditCost = 15;
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBloodSnowy.asset").WaitForCompletion().directorCreditCost = 15;

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombat.asset").WaitForCompletion().directorCreditCost = 15; //20 default
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombatSandy.asset").WaitForCompletion().directorCreditCost = 15;
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombatSnowy.asset").WaitForCompletion().directorCreditCost = 15;

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/iscShrineColossusAccess.asset").WaitForCompletion().directorCreditCost = 40;

            }


            if (WConfig.DCCSInteractableChanges.Value)
            {
                Interactables_Stage1();
                Interactables_Stage2();
                Interactables_Stage3();
                Interactables_Stage4();
                Interactables_Stage5();
            }
            Interactables_Other();
        }

        public static void Interactables_Other()
        {
            //CategoryChest shenanigans
            /*if (WConfig.DCCSCategoryChest.Value)
            {
                dccsGolemplainsInteractablesDLC1.categories[0].cards = dccsGolemplainsInteractablesDLC1.categories[0].cards.Remove(dccsGolemplainsInteractablesDLC1.categories[0].cards[7], dccsGolemplainsInteractablesDLC1.categories[0].cards[5]);
                dccsBlackBeachInteractablesDLC1.categories[0].cards = dccsBlackBeachInteractablesDLC1.categories[0].cards.Remove(dccsBlackBeachInteractablesDLC1.categories[0].cards[6], dccsBlackBeachInteractablesDLC1.categories[0].cards[5]);
                dccsSnowyForestInteractablesDLC1.categories[0].cards = dccsSnowyForestInteractablesDLC1.categories[0].cards.Remove(dccsSnowyForestInteractablesDLC1.categories[0].cards[7], dccsSnowyForestInteractablesDLC1.categories[0].cards[6]);

                dccsGooLakeInteractablesDLC1.categories[0].cards = dccsGooLakeInteractablesDLC1.categories[0].cards.Remove(dccsGooLakeInteractablesDLC1.categories[0].cards[8], dccsGooLakeInteractablesDLC1.categories[0].cards[7]);
                dccsFoggySwampInteractablesDLC1.categories[0].cards = dccsFoggySwampInteractablesDLC1.categories[0].cards.Remove(dccsFoggySwampInteractablesDLC1.categories[0].cards[8], dccsFoggySwampInteractablesDLC1.categories[0].cards[6]);
                dccsAncientLoftInteractablesDLC1.categories[0].cards = dccsAncientLoftInteractablesDLC1.categories[0].cards.Remove(dccsAncientLoftInteractablesDLC1.categories[0].cards[6], dccsAncientLoftInteractablesDLC1.categories[0].cards[5]);

                dccsFrozenWallInteractablesDLC1.categories[0].cards = dccsFrozenWallInteractablesDLC1.categories[0].cards.Remove(dccsFrozenWallInteractablesDLC1.categories[0].cards[6], dccsFrozenWallInteractablesDLC1.categories[0].cards[5]);
                dccsWispGraveyardInteractablesDLC1.categories[0].cards = dccsWispGraveyardInteractablesDLC1.categories[0].cards.Remove(dccsWispGraveyardInteractablesDLC1.categories[0].cards[6], dccsWispGraveyardInteractablesDLC1.categories[0].cards[5]);
                dccsSulfurPoolsInteractablesDLC1.categories[0].cards = dccsSulfurPoolsInteractablesDLC1.categories[0].cards.Remove(dccsSulfurPoolsInteractablesDLC1.categories[0].cards[7], dccsSulfurPoolsInteractablesDLC1.categories[0].cards[5]);

                dccsDampCaveInteractablesDLC1.categories[0].cards = dccsDampCaveInteractablesDLC1.categories[0].cards.Remove(dccsDampCaveInteractablesDLC1.categories[0].cards[7], dccsDampCaveInteractablesDLC1.categories[0].cards[6]);
                dccsShipgraveyardInteractablesDLC1.categories[0].cards = dccsShipgraveyardInteractablesDLC1.categories[0].cards.Remove(dccsShipgraveyardInteractablesDLC1.categories[0].cards[7], dccsShipgraveyardInteractablesDLC1.categories[0].cards[5]);
                dccsRootJungleInteractablesDLC1.categories[0].cards = dccsRootJungleInteractablesDLC1.categories[0].cards.Remove(dccsRootJungleInteractablesDLC1.categories[0].cards[6], dccsRootJungleInteractablesDLC1.categories[0].cards[5]);

            }*/

            if (!WConfig.disableNewContent.Value)
            {
                DirectorCard ADTrippleRed = new DirectorCard
                {
                    spawnCard = ChangesInteractables.RedMultiShopISC,
                    selectionWeight = 2,
                    minimumStageCompletions = 2
                };
                DirectorCardCategorySelection dccsInfiniteTowerInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dccsInfiniteTowerInteractables.asset").WaitForCompletion();
                dccsInfiniteTowerInteractables.AddCard(2, ADTrippleRed);
                dccsInfiniteTowerInteractables.categories[2].selectionWeight += 0.1f;

            }



            DirectorCardCategorySelection dccsGoldshoresInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goldshores/dccsGoldshoresInteractables.asset").WaitForCompletion();


            DirectorCard ADShrineCleanse1 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                selectionWeight = 5,
            };
            DirectorCard AdShrineCombat = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombat.asset").WaitForCompletion(),
                selectionWeight = 20,
            };
            DirectorCard AdShrineGold = new DirectorCard
            {
                spawnCard = ChangesInteractables.iscShrineGoldFake,
                selectionWeight = 10,
            };
            DirectorCard ADShrineChance = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineChance/iscShrineChance.asset").WaitForCompletion(),
                selectionWeight = 5,
            };

            DirectorCardCategorySelection.Category GoldShoreShrines = new DirectorCardCategorySelection.Category
            {
                name = "Shrines",
                selectionWeight = 10,
                cards = new DirectorCard[] { AdShrineCombat, ADShrineChance, AdShrineGold, ADShrineCleanse1 }
            };
            dccsGoldshoresInteractables.categories = new DirectorCardCategorySelection.Category[] { GoldShoreShrines };


            DirectorCardCategorySelection dccsArenaInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/arena/dccsArenaInteractables.asset").WaitForCompletion();

            if (ConfigStages.Stage_X_Arena_Void.Value)
            {
                //Stealthed Chest replaced with Order Shrine
                dccsArenaInteractables.categories[2].cards[0].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion();
            }
        }


        public static void Interactables_Stage1()
        {
            #region DCCS
            DirectorCardCategorySelection dccsGolemplainsInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsGolemplainsInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsGolemplainsInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGolemplainsInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsBlackBeachInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsBlackBeachInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsBlackBeachInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsBlackBeachInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSnowyForestInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/snowyforest/dccsSnowyForestInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSnowyForestInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSnowyForestInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsLakesInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesInteractables_DLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsLakesnightInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightInteractables_DLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesnightInteractables_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightInteractables_DLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsVillageInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageInteractables_DLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillageInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillagenightInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillagenightInteractablesDLC2.asset").WaitForCompletion();
            #endregion
            #region Cards
            DirectorCard iscCategoryChest2Damage = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard iscCategoryChest2Healing = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Healing.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard iscCategoryChest2Utility = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Utility.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard ADBrokenMegaDrone = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenMegaDrone"),
                selectionWeight = 4,
                minimumStageCompletions = 4,
            };
            DirectorCard ShrineOrder_SNOW = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSnowy.asset").WaitForCompletion(),
                selectionWeight = 1,
            };
            DirectorCard LoopShrineCleanse = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                selectionWeight = 1,
                minimumStageCompletions = 2,
            };
            DirectorCard LoopShrineCleanse15 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                selectionWeight = 1,
                minimumStageCompletions = 15,
            };
            DirectorCard ShrineOrder_DEFAULT10 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion(),
                selectionWeight = 10,
            };
            #endregion
            #region Adds

            ChangesToBase(dccsGolemplainsInteractables);
            ChangesToSotV(dccsGolemplainsInteractablesDLC1);
            
            ChangesToBase(dccsBlackBeachInteractables);
            ChangesToSotV(dccsBlackBeachInteractablesDLC1);
            
            ChangesToBase(dccsSnowyForestInteractablesDLC1);
            ChangesToSotV(dccsSnowyForestInteractablesDLC1);

            ChangesToBase(dccsLakesInteractables);
            ChangesToSotV(dccsLakesInteractablesDLC1);

            ChangesToBase(dccsLakesnightInteractables_DLC2);
            ChangesToSotV(dccsLakesnightInteractables_DLC1);

            ChangesToBase(dccsVillageInteractables_DLC2);
            ChangesToSotV(dccsVillageInteractablesDLC1);

            if (ConfigStages.Stage_1_Roost.Value)
            {
                dccsBlackBeachInteractables.AddCard(2, LoopShrineCleanse);
                dccsBlackBeachInteractablesDLC1.categories[1].selectionWeight = 4.5f;
                
            }
            if (ConfigStages.Stage_1_Snow.Value)
            {
                dccsSnowyForestInteractablesDLC1.AddCard(3, ADBrokenMegaDrone);
                dccsSnowyForestInteractablesDLC1.AddCard(5, ShrineOrder_SNOW);
            }
            if (ConfigStages.Stage_1_Lake.Value)
            {
                dccsLakesInteractables_DLC2.categories[0].cards[0].minimumStageCompletions = 2;
                dccsLakesInteractables_DLC2.categories[0].cards[0].selectionWeight = 2; //Cleansing Shrine

                MultBy10(dccsLakesInteractables, 0);
                int chests = dccsLakesInteractablesDLC1.AddCategory("Chests", 45);
                dccsLakesInteractablesDLC1.AddCard(chests, iscCategoryChest2Healing);
                dccsLakesInteractablesDLC1.AddCard(chests, iscCategoryChest2Utility);

                MultBy10(dccsLakesnightInteractables_DLC2, 0);
                chests = dccsLakesnightInteractables_DLC1.AddCategory("Chests", 45);
                dccsLakesnightInteractables_DLC1.AddCard(chests, iscCategoryChest2Healing);
                dccsLakesnightInteractables_DLC1.AddCard(chests, iscCategoryChest2Utility);
            }
            if (ConfigStages.Stage_1_Village.Value)
            {
                dccsVillageInteractables_DLC2.AddCard(2, ShrineOrder_DEFAULT10);
                dccsVillageInteractables_DLC2.AddCard(2, LoopShrineCleanse15);
                dccsVillageInteractables_DLC2.AddCard(3, ADBrokenMegaDrone);

                MultBy10(dccsVillageInteractables_DLC2, 0);
                int chests = dccsVillageInteractablesDLC1.AddCategory("Chests", 45);
                dccsVillageInteractablesDLC1.AddCard(chests, iscCategoryChest2Damage);
                dccsVillageInteractablesDLC1.AddCard(chests, iscCategoryChest2Utility);
            }


            #endregion

        }
        public static void Interactables_Stage2()
        {
            #region DCCS
            DirectorCardCategorySelection dccsGooLakeInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsGooLakeInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsGooLakeInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGooLakeInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFoggySwampInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFoggySwampInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFoggySwampInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFoggySwampInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsAncientLoftInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/ancientloft/dccsAncientLoftInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsAncientLoftInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsAncientLoftInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsLemurianTempleInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lemuriantemple/dccsLemurianTempleInteractables_DLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLemurianTempleInteractables_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lemuriantemple/dccsLemurianTempleInteractables_DLC1.asset").WaitForCompletion();
            #endregion
            #region Cards
            DirectorCard iscCategoryChest2Damage = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard iscCategoryChest2Healing = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Healing.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard iscCategoryChest2Utility = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Utility.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard ShrineOrder_SAND = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSandy.asset").WaitForCompletion(),
                selectionWeight = 1,
            };
            #endregion
            ChangesToBase(dccsGooLakeInteractables);
            ChangesToSotV(dccsGooLakeInteractablesDLC1);
            ChangesToSotS(dccsGooLakeInteractablesDLC2);

            ChangesToBase(dccsFoggySwampInteractables);
            ChangesToSotV(dccsFoggySwampInteractablesDLC1);
            ChangesToSotS(dccsFoggySwampInteractablesDLC2);

            ChangesToBase(dccsAncientLoftInteractablesDLC1);
            ChangesToSotV(dccsAncientLoftInteractablesDLC1);
            ChangesToSotS(dccsAncientLoftInteractablesDLC2);

            ChangesToBase(dccsLemurianTempleInteractables_DLC2);
            ChangesToSotV(dccsLemurianTempleInteractables_DLC1);
            ChangesToSotS(dccsLemurianTempleInteractables_DLC2);

            if (ConfigStages.Stage_2_Swamp.Value)
            {
                dccsFoggySwampInteractables.categories[2].cards[3].selectionWeight = 14;
            }
            if (ConfigStages.Stage_2_Ancient.Value)
            {
                dccsAncientLoftInteractablesDLC1.AddCard(4, ShrineOrder_SAND); //RARE
            }
            if (ConfigStages.Stage_2_Temple.Value)
            {
                dccsLemurianTempleInteractables_DLC2.AddCard(2, ShrineOrder_SAND);

                MultBy10(dccsLemurianTempleInteractables_DLC2, 0);
                int chests = dccsLemurianTempleInteractables_DLC1.AddCategory("Chests", 45);
                dccsLemurianTempleInteractables_DLC1.AddCard(chests, iscCategoryChest2Damage);
                dccsLemurianTempleInteractables_DLC1.AddCard(chests, iscCategoryChest2Healing);
                dccsLemurianTempleInteractables_DLC1.AddCard(chests, iscCategoryChest2Utility);
            }
        
        }
        public static void Interactables_Stage3()
        {
            #region DCCS
            DirectorCardCategorySelection dccsFrozenWallInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFrozenWallInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFrozenWallInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFrozenWallInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsWispGraveyardInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsWispGraveyardInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSulfurPoolsInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSulfurPoolsInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSulfurPoolsInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsHabitatInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitat/dccsHabitatInteractables_DLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatInteractables_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitat/dccsHabitatInteractables_DLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsHabitatfallInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitatfall/dccsHabitatfallInteractables.asset").WaitForCompletion();
            #endregion
            #region Cards
            DirectorCard iscCategoryChest2Damage = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard iscCategoryChest2Healing = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Healing.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard ADShrineBoss10 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBoss/iscShrineBoss.asset").WaitForCompletion(),
                selectionWeight = 11,
            };
            DirectorCard ADBrokenEmergencyDrone = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenEmergencyDrone"),
                selectionWeight = 3,
            };
            #endregion

            ChangesToBase(dccsFrozenWallInteractables);
            ChangesToSotV(dccsFrozenWallInteractablesDLC1);

            ChangesToBase(dccsWispGraveyardInteractables);
            ChangesToSotV(dccsWispGraveyardInteractablesDLC1);

            ChangesToBase(dccsSulfurPoolsInteractablesDLC1);
            ChangesToSotV(dccsSulfurPoolsInteractablesDLC1);

            ChangesToBase(dccsHabitatInteractables_DLC2);
            ChangesToSotV(dccsHabitatInteractables_DLC1);
            MultBy10(dccsHabitatInteractables_DLC2, 0);

            if (ConfigStages.Stage_3_Wisp.Value)
            {
                dccsWispGraveyardInteractables.categories[2].cards[3].selectionWeight = 13;
            }
            if (ConfigStages.Stage_3_Sulfur.Value)
            {
                dccsSulfurPoolsInteractablesDLC1.AddCard(2, ADShrineBoss10);
                dccsSulfurPoolsInteractablesDLC1.AddCard(3, ADBrokenEmergencyDrone);
            }
            if (ConfigStages.Stage_3_Tree.Value)
            {
                int chests = dccsHabitatInteractables_DLC1.AddCategory("Chests", 45);
                dccsHabitatInteractables_DLC1.AddCard(chests, iscCategoryChest2Damage);
                dccsHabitatInteractables_DLC1.AddCard(chests, iscCategoryChest2Healing);
            }

        }
        public static void Interactables_Stage4()
        {
            #region DCCS
            DirectorCardCategorySelection dccsDampCaveInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsDampCaveInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsDampCaveInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsDampCaveInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsShipgraveyardInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShipgraveyardInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsRootJungleInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsRootJungleInteractablesDLC2.asset").WaitForCompletion();
            #endregion

            ChangesToBase(dccsDampCaveInteractables);
            ChangesToSotV(dccsDampCaveInteractablesDLC1);

            ChangesToBase(dccsShipgraveyardInteractables);
            ChangesToSotV(dccsShipgraveyardInteractablesDLC1);

            ChangesToBase(dccsRootJungleInteractables);
            ChangesToSotV(dccsRootJungleInteractablesDLC1);

            //Remove Gunner Turret from Stage 4/5
            if (ConfigStages.Stage_4_Damp_Abyss.Value)
            {
                dccsDampCaveInteractables.categories[4].cards = dccsDampCaveInteractables.categories[4].cards.Remove(dccsDampCaveInteractables.categories[4].cards[0]);
            }
            if (ConfigStages.Stage_4_Ship.Value)
            {
                dccsShipgraveyardInteractables.categories[2].cards[3].selectionWeight = 14;
                dccsShipgraveyardInteractables.categories[4].cards = dccsShipgraveyardInteractables.categories[4].cards.Remove(dccsShipgraveyardInteractables.categories[4].cards[0]);
            }
            if (ConfigStages.Stage_4_Root_Jungle.Value)
            {
                dccsRootJungleInteractables.categories[4].cards = dccsRootJungleInteractables.categories[4].cards.Remove(dccsRootJungleInteractables.categories[4].cards[0]);
            }
        }
        public static void Interactables_Stage5()
        {
            #region DCCS
            DirectorCardCategorySelection dccsSkyMeadowInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSkyMeadowInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSkyMeadowInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSkyMeadowInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsHelminthRoostInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/helminthroost/dccsHelminthRoostInteractables_DLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHelminthRoostInteractables_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/helminthroost/dccsHelminthRoostInteractables_DLC1.asset").WaitForCompletion();
            #endregion
            #region Cards
            DirectorCard iscCategoryChest2Damage = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                selectionWeight = 4,
            };
            DirectorCard iscCategoryChest2Healing = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Healing.asset").WaitForCompletion(),
                selectionWeight = 4,
            };
            DirectorCard iscCategoryChest2Utility = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Utility.asset").WaitForCompletion(),
                selectionWeight = 4,
            };
            #endregion

            ChangesToBase(dccsSkyMeadowInteractables);
            ChangesToSotV(dccsSkyMeadowInteractablesDLC1);
           
            ChangesToBase(dccsHelminthRoostInteractables_DLC2);
            ChangesToSotV(dccsHelminthRoostInteractables_DLC1);
    

            if (ConfigStages.Stage_5_Sky.Value)
            {
                dccsSkyMeadowInteractables.categories[4].cards = dccsSkyMeadowInteractables.categories[4].cards.Remove(dccsSkyMeadowInteractables.categories[4].cards[0]);
            }
            if (ConfigStages.Stage_5_Helminth.Value)
            {
                dccsHelminthRoostInteractables_DLC2.categories[7].cards = dccsHelminthRoostInteractables_DLC2.categories[7].cards.Remove(dccsHelminthRoostInteractables_DLC2.categories[7].cards[0]);
                
                MultBy10(dccsHelminthRoostInteractables_DLC2, 0);
                MultBy10(dccsHelminthRoostInteractables_DLC1, 0);
                dccsHelminthRoostInteractables_DLC1.AddCard(0, iscCategoryChest2Damage);
                dccsHelminthRoostInteractables_DLC1.AddCard(0, iscCategoryChest2Healing);
                dccsHelminthRoostInteractables_DLC1.AddCard(0, iscCategoryChest2Utility);
            }
        }


      
        
        public static void MultBy10(DirectorCardCategorySelection dccs, int category)
        {
            for (int i = 0; i < dccs.categories[category].cards.Length; i++)
            {
                dccs.categories[0].cards[i].selectionWeight *= 10;
            }
        }


        public static void ChangesToSotV(DirectorCardCategorySelection dccs)
        {
            for (int i = 0; i < dccs.categories[0].cards.Length; i++)
            {
                if (dccs.categories[0].cards[i].spawnCard.name.EndsWith("oryChest2"))
                {
                    dccs.categories[0].cards[i].selectionWeight *= 3;
                }
            }

            for (int i = dccs.categories.Length - 1; 0 < i; i--)
            {
                if (dccs.categories[i].name.StartsWith("Void"))
                {
                    dccs.categories[i].selectionWeight += 0.5f;
                    return;
                }
            }
        }
        public static void ChangesToSotS(DirectorCardCategorySelection dccs)
        {
            for (int i = dccs.categories.Length - 1; 0 < i; i--)
            {
                if (dccs.categories[i].name.StartsWith("Storm"))
                {
                    dccs.categories[i].cards[1].minimumStageCompletions = 2; //Shrine Revive always at 1
                    return;
                }
            }
        }

        public static void ChangesToBase(DirectorCardCategorySelection dccs)
        {

            DirectorCardCategorySelection.Category category;
            #region Shrines (Always 2)
            category = dccs.categories[2];
            for (int card = 0; card < category.cards.Length; card++)
            {
                if (category.cards[card].spawnCard.name.StartsWith("iscShrineBoss"))
                {
                    //Bro RoRR has way more mountains this should help
                    category.cards[card].selectionWeight = (int)(category.cards[card].selectionWeight * WConfig.InteractablesMountainMultiplier.Value);
                }
            }
            #endregion
            #region Drone (Always 3)
            category = dccs.categories[3];
            for (int card = 0; card < category.cards.Length; card++)
            {
                if (category.cards[card].spawnCard.name.EndsWith("EquipmentDrone"))
                {
                    category.cards[card].selectionWeight = 4;
                }
            }
            #endregion
            #region Rare (4 or 5)
            int foundIndex = dccs.FindCategoryIndexByName("Rare");
            if (foundIndex != -1)
            {
                category = dccs.categories[foundIndex];
                category.selectionWeight += 0.1f;
                for (int card = 0; card < category.cards.Length; card++)
                {
                    if (category.cards[card].spawnCard.name.EndsWith("RadarTower"))
                    {
                        category.cards[card].selectionWeight = 20;
                    }
                }
                if (!WConfig.disableNewContent.Value)
                {
                    dccs.AddCard(foundIndex, ChangesInteractables.ADTrippleRed);
                }
            }
            #endregion
            #region Duplicator (5 or 6)
            foundIndex = dccs.FindCategoryIndexByName("Duplicator");
            if (foundIndex != -1)
            {
                category = dccs.categories[foundIndex];
                category.cards[1].selectionWeight = 7; //Green Dupli Always at 1
                category.cards[2].selectionWeight = 2; //Mili Dupli Always at 2
                category.cards[2].minimumStageCompletions = 3; //Mili Dupli Always at 2
                for (int card = 3; card < category.cards.Length; card++)
                {
                    if (category.cards[card].spawnCard.name.EndsWith("Wild"))
                    {
                        category.cards[card].minimumStageCompletions = 2;
                    }
                }
            }
            #endregion
        }



        public class InteractableSkinner : MonoBehaviour
        {
            public Material replacement;


            public void Start()
            {

            }


        }
    }
}