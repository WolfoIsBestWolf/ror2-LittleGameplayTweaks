using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using RoR2.ExpansionManagement;
using HG;

namespace LittleGameplayTweaks
{
    public class DCCSInteractables
    {
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


        }

       
        public static void DCCSThings_NoDLC()
        {
            DirectorCardCategorySelection dccsGolemplainsInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsBlackBeachInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachInteractables.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsGooLakeInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFoggySwampInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampInteractables.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFrozenWallInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardInteractables.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsDampCaveInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleInteractables.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSkyMeadowInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowInteractables.asset").WaitForCompletion();

            //DirectorCardCategorySelection dccsArtifactWorldInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/artifactworld/dccsArtifactWorldInteractables.asset").WaitForCompletion();
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


 
            //Remove Gunner Turret from Stage 4/5
 
            dccsDampCaveInteractables.categories[4].cards = dccsDampCaveInteractables.categories[4].cards.Remove(dccsDampCaveInteractables.categories[4].cards[0]);
            dccsShipgraveyardInteractables.categories[4].cards = dccsShipgraveyardInteractables.categories[4].cards.Remove(dccsShipgraveyardInteractables.categories[4].cards[0]);
            dccsRootJungleInteractables.categories[4].cards = dccsRootJungleInteractables.categories[4].cards.Remove(dccsRootJungleInteractables.categories[4].cards[0]);
            dccsSkyMeadowInteractables.categories[4].cards = dccsSkyMeadowInteractables.categories[4].cards.Remove(dccsSkyMeadowInteractables.categories[4].cards[0]);

            //Goldshores Random Interactables Test

            DirectorCardCategorySelection.Category GoldShoreShrines = new DirectorCardCategorySelection.Category
            {
                name = "Shrines",
                selectionWeight = 10,
                cards = new DirectorCard[] { AdShrineCombat, ADShrineChance, AdShrineGold, ADShrineCleanse1 }
            };
            dccsGoldshoresInteractables.categories = new DirectorCardCategorySelection.Category[] { GoldShoreShrines };




            //Other Stage Interactable Changes
            DirectorCardCategorySelection[] allinteractables = new DirectorCardCategorySelection[] {
                dccsGolemplainsInteractables, dccsBlackBeachInteractables,
                dccsGooLakeInteractables, dccsFoggySwampInteractables,
                dccsFrozenWallInteractables, dccsWispGraveyardInteractables,
                dccsDampCaveInteractables, dccsShipgraveyardInteractables, dccsRootJungleInteractables,
                dccsSkyMeadowInteractables };
            DoToAllDCCS(allinteractables);

        }

        public static void DCCSThings_DLC1()
        {
            DirectorCardCategorySelection dccsGolemplainsInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsBlackBeachInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSnowyForestInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/snowyforest/dccsSnowyForestInteractablesDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsGooLakeInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFoggySwampInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsAncientLoftInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/ancientloft/dccsAncientLoftInteractablesDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFrozenWallInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSulfurPoolsInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsInteractablesDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsDampCaveInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleInteractablesDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSkyMeadowInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowInteractablesDLC1.asset").WaitForCompletion();

            //DirectorCardCategorySelection dccsArtifactWorldInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/artifactworld/dccsArtifactWorldInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsGoldshoresInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goldshores/dccsGoldshoresInteractablesDLC1.asset").WaitForCompletion();


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
            DirectorCard ADShrineBoss10 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBoss/iscShrineBoss.asset").WaitForCompletion(),
                selectionWeight = 10,
            };
            DirectorCard ADBrokenMegaDrone = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenMegaDrone"),
                selectionWeight = 4,
                minimumStageCompletions = 4,
            };
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


            //

            //CategoryChest shenanigans
            if (WConfig.DCCSCategoryChest.Value)
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

            }


            if (ConfigStages.Stage_1_Roost.Value)
            {
                dccsBlackBeachInteractablesDLC1.categories[7].selectionWeight = 5;
            }
            if (ConfigStages.Stage_1_Snow.Value)
            {
                dccsSnowyForestInteractablesDLC1.AddCard(3, ADBrokenMegaDrone);
            }
            if (ConfigStages.Stage_3_Sulfur.Value)
            {
                dccsSulfurPoolsInteractablesDLC1.categories[2].cards = dccsSulfurPoolsInteractablesDLC1.categories[2].cards.Remove(dccsSulfurPoolsInteractablesDLC1.categories[2].cards[3], dccsSulfurPoolsInteractablesDLC1.categories[2].cards[2]);
                dccsSulfurPoolsInteractablesDLC1.AddCard(2, ADShrineBoss10);
            }

            //Remove Gunner Turret from Stage 4/5
            if (ConfigStages.Stage_4_Damp_Abyss.Value)
            {
                dccsDampCaveInteractablesDLC1.categories[4].cards = dccsDampCaveInteractablesDLC1.categories[4].cards.Remove(dccsDampCaveInteractablesDLC1.categories[4].cards[0]);
            }
            if (ConfigStages.Stage_4_Ship.Value)
            {
                dccsShipgraveyardInteractablesDLC1.categories[4].cards = dccsShipgraveyardInteractablesDLC1.categories[4].cards.Remove(dccsShipgraveyardInteractablesDLC1.categories[4].cards[0]);
            }
            if (ConfigStages.Stage_4_Root_Jungle.Value)
            {
                dccsRootJungleInteractablesDLC1.categories[4].cards = dccsRootJungleInteractablesDLC1.categories[4].cards.Remove(dccsRootJungleInteractablesDLC1.categories[4].cards[0]);
            }
            if (ConfigStages.Stage_5_Sky.Value)
            {
                dccsSkyMeadowInteractablesDLC1.categories[4].cards = dccsSkyMeadowInteractablesDLC1.categories[4].cards.Remove(dccsSkyMeadowInteractablesDLC1.categories[4].cards[0]);
            }


            //Goldshores Random Interactables Test
            DirectorCardCategorySelection.Category GoldShoreShrines = new DirectorCardCategorySelection.Category
            {
                name = "Shrines",
                selectionWeight = 10,
                cards = new DirectorCard[] { AdShrineCombat, ADShrineChance, AdShrineGold, ADShrineCleanse1 }
            };
            dccsGoldshoresInteractablesDLC1.categories = new DirectorCardCategorySelection.Category[] { GoldShoreShrines };




            //Other Stage Interactable Changes
            DirectorCardCategorySelection[] allinteractables = new DirectorCardCategorySelection[] {
                dccsGolemplainsInteractablesDLC1, dccsBlackBeachInteractablesDLC1, dccsSnowyForestInteractablesDLC1,
                dccsGooLakeInteractablesDLC1, dccsFoggySwampInteractablesDLC1, dccsAncientLoftInteractablesDLC1,
                dccsFrozenWallInteractablesDLC1, dccsWispGraveyardInteractablesDLC1, dccsSulfurPoolsInteractablesDLC1,
                dccsDampCaveInteractablesDLC1, dccsShipgraveyardInteractablesDLC1, dccsRootJungleInteractablesDLC1,
                dccsSkyMeadowInteractablesDLC1 };

            DoToAllDCCS(allinteractables);
          
        }

        public static void DCCSThings_DLC2()
        {
            //0 Chest
            //1 Barrel
            //2 Shrine
            //3 Drone
            //4 Misc / Turret
            //5 Rare
            //6 Duplicator
            //7 Void Stuff
            //8 Storm Stuff


            DirectorCardCategorySelection dccsGolemplainsInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGolemplainsInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsBlackBeachInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsBlackBeachInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSnowyForestInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSnowyForestInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesnightInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillageInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillagenightInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillagenightInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsGooLakeInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGooLakeInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFoggySwampInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFoggySwampInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsAncientLoftInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsAncientLoftInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLemurianTempleInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lemuriantemple/dccsLemurianTempleInteractables.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFrozenWallInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFrozenWallInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsWispGraveyardInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSulfurPoolsInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSulfurPoolsInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitat/dccsHabitatInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatfallInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitatfall/dccsHabitatfallInteractables.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsDampCaveInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsDampCaveInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShipgraveyardInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsRootJungleInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSkyMeadowInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSkyMeadowInteractablesDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHelminthRoostInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/helminthroost/dccsHelminthRoostInteractables.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsArenaInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/arena/dccsArenaInteractablesDLC1.asset").WaitForCompletion();


            DirectorCard ShrineOrder_SAND = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSandy.asset").WaitForCompletion(),
                selectionWeight = 1,
            };
            DirectorCard ShrineOrder_DEFAULT10 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion(),
                selectionWeight = 10,
            };
            DirectorCard ShrineOrder_SNOW = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSnowy.asset").WaitForCompletion(),
                selectionWeight = 1,
            };
            DirectorCard ADShrineCleanse4 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                selectionWeight = 4,
                minimumStageCompletions = 1,
            };
            DirectorCard ADShrineCleanse15 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                selectionWeight = 15,
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
            DirectorCard ADBrokenMegaDrone = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenMegaDrone"),
                selectionWeight = 4,
                minimumStageCompletions = 4,
            };
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



            if (ConfigStages.Stage_1_Roost.Value)
            {
                dccsBlackBeachInteractablesDLC2.categories[7].selectionWeight = 5;
                dccsBlackBeachInteractablesDLC2.AddCard(2, ADShrineCleanse4);
            }
            if (ConfigStages.Stage_1_Snow.Value)
            {
                dccsSnowyForestInteractablesDLC2.AddCard(3, ADBrokenMegaDrone);
                dccsSnowyForestInteractablesDLC2.AddCard(5, ShrineOrder_SNOW);
            }
            if (ConfigStages.Stage_1_Lake.Value)
            {
                dccsLakesInteractablesDLC2.AddCard(2, ADShrineCleanse4);
                dccsLakesnightInteractables.AddCard(2, ADShrineCleanse4);
            }
            if (ConfigStages.Stage_1_Village.Value)
            {
                dccsVillageInteractablesDLC1.AddCard(2, ShrineOrder_DEFAULT10);
                dccsVillagenightInteractablesDLC2.AddCard(2, ShrineOrder_DEFAULT10);
                dccsVillagenightInteractablesDLC2.AddCard(3, ADBrokenMegaDrone);
            }
            if (ConfigStages.Stage_2_Ancient.Value)
            {
                dccsAncientLoftInteractablesDLC2.AddCard(4, ShrineOrder_SAND);
            }
            if (ConfigStages.Stage_2_Temple.Value)
            {
                dccsLemurianTempleInteractables.AddCard(2, ShrineOrder_SAND);
            }
            if (ConfigStages.Stage_X_Arena_Void.Value)
            {
                dccsArenaInteractablesDLC1.categories[2].cards[0].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion();
            }
            if (ConfigStages.Stage_3_Sulfur.Value)
            {
                dccsSulfurPoolsInteractablesDLC2.categories[2].cards = dccsSulfurPoolsInteractablesDLC2.categories[2].cards.Remove(dccsSulfurPoolsInteractablesDLC2.categories[2].cards[3]);
                dccsSulfurPoolsInteractablesDLC2.AddCard(2, ADShrineBoss10);
                dccsSulfurPoolsInteractablesDLC2.AddCard(3, ADBrokenEmergencyDrone);
            }
            if (ConfigStages.Stage_3_Tree.Value)
            {
                dccsHabitatInteractables.categories[2].cards[0] = ADShrineCleanse15;
                dccsHabitatfallInteractables.categories[2].cards[0] = ADShrineCleanse15;
            }
            //Remove Gunner Turret from Stage 4/5
            if (ConfigStages.Stage_4_Damp_Abyss.Value)
            {
                dccsDampCaveInteractablesDLC2.categories[4].cards = dccsDampCaveInteractablesDLC2.categories[4].cards.Remove(dccsDampCaveInteractablesDLC2.categories[4].cards[0]);
            }
            if (ConfigStages.Stage_4_Ship.Value)
            {
                dccsShipgraveyardInteractablesDLC2.categories[4].cards = dccsShipgraveyardInteractablesDLC2.categories[4].cards.Remove(dccsShipgraveyardInteractablesDLC2.categories[4].cards[0]);
            }
            if (ConfigStages.Stage_4_Root_Jungle.Value)
            {
                dccsRootJungleInteractablesDLC2.categories[4].cards = dccsRootJungleInteractablesDLC2.categories[4].cards.Remove(dccsRootJungleInteractablesDLC2.categories[4].cards[0]);
            }
            if (ConfigStages.Stage_5_Sky.Value)
            {
                dccsSkyMeadowInteractablesDLC2.categories[4].cards = dccsSkyMeadowInteractablesDLC2.categories[4].cards.Remove(dccsSkyMeadowInteractablesDLC2.categories[4].cards[0]);
            }
            if (ConfigStages.Stage_5_Helminth.Value)
            {
                dccsHelminthRoostInteractables.categories[4].cards = dccsHelminthRoostInteractables.categories[4].cards.Remove(dccsHelminthRoostInteractables.categories[4].cards[0]);
            }


            if (WConfig.DCCSCategoryChest.Value)
            {
                dccsGolemplainsInteractablesDLC2.categories[0].cards = dccsGolemplainsInteractablesDLC2.categories[0].cards.Remove(dccsGolemplainsInteractablesDLC2.categories[0].cards[7], dccsGolemplainsInteractablesDLC2.categories[0].cards[5]);
                dccsBlackBeachInteractablesDLC2.categories[0].cards = dccsBlackBeachInteractablesDLC2.categories[0].cards.Remove(dccsBlackBeachInteractablesDLC2.categories[0].cards[6], dccsBlackBeachInteractablesDLC2.categories[0].cards[5]);
                dccsSnowyForestInteractablesDLC2.categories[0].cards = dccsSnowyForestInteractablesDLC2.categories[0].cards.Remove(dccsSnowyForestInteractablesDLC2.categories[0].cards[7], dccsSnowyForestInteractablesDLC2.categories[0].cards[6]);

                dccsGooLakeInteractablesDLC2.categories[0].cards = dccsGooLakeInteractablesDLC2.categories[0].cards.Remove(dccsGooLakeInteractablesDLC2.categories[0].cards[8], dccsGooLakeInteractablesDLC2.categories[0].cards[7]);
                dccsFoggySwampInteractablesDLC2.categories[0].cards = dccsFoggySwampInteractablesDLC2.categories[0].cards.Remove(dccsFoggySwampInteractablesDLC2.categories[0].cards[8], dccsFoggySwampInteractablesDLC2.categories[0].cards[6]);
                dccsAncientLoftInteractablesDLC2.categories[0].cards = dccsAncientLoftInteractablesDLC2.categories[0].cards.Remove(dccsAncientLoftInteractablesDLC2.categories[0].cards[6], dccsAncientLoftInteractablesDLC2.categories[0].cards[5]);

                dccsFrozenWallInteractablesDLC2.categories[0].cards = dccsFrozenWallInteractablesDLC2.categories[0].cards.Remove(dccsFrozenWallInteractablesDLC2.categories[0].cards[6], dccsFrozenWallInteractablesDLC2.categories[0].cards[5]);
                dccsWispGraveyardInteractablesDLC2.categories[0].cards = dccsWispGraveyardInteractablesDLC2.categories[0].cards.Remove(dccsWispGraveyardInteractablesDLC2.categories[0].cards[6], dccsWispGraveyardInteractablesDLC2.categories[0].cards[5]);
                dccsSulfurPoolsInteractablesDLC2.categories[0].cards = dccsSulfurPoolsInteractablesDLC2.categories[0].cards.Remove(dccsSulfurPoolsInteractablesDLC2.categories[0].cards[7], dccsSulfurPoolsInteractablesDLC2.categories[0].cards[5]);

                dccsDampCaveInteractablesDLC2.categories[0].cards = dccsDampCaveInteractablesDLC2.categories[0].cards.Remove(dccsDampCaveInteractablesDLC2.categories[0].cards[7], dccsDampCaveInteractablesDLC2.categories[0].cards[6]);
                dccsShipgraveyardInteractablesDLC2.categories[0].cards = dccsShipgraveyardInteractablesDLC2.categories[0].cards.Remove(dccsShipgraveyardInteractablesDLC2.categories[0].cards[7], dccsShipgraveyardInteractablesDLC2.categories[0].cards[5]);
                dccsRootJungleInteractablesDLC2.categories[0].cards = dccsRootJungleInteractablesDLC2.categories[0].cards.Remove(dccsRootJungleInteractablesDLC2.categories[0].cards[6], dccsRootJungleInteractablesDLC2.categories[0].cards[5]);

            }

            if (WConfig.DccsAddCategoryToSots.Value)
            {
                DirectorCardCategorySelection[] mult10Needed = new DirectorCardCategorySelection[] {
                dccsLakesInteractablesDLC2, dccsLakesnightInteractables,
                dccsVillageInteractablesDLC1,
                dccsLemurianTempleInteractables,
                dccsHabitatfallInteractables,dccsHabitatInteractables,
                dccsHelminthRoostInteractables
            };
                for (int dccs = 0; mult10Needed.Length > dccs; dccs++)
                {
                    var cat = mult10Needed[dccs].categories[0];
                    for (int card = 0; cat.cards.Length > card; card++)
                    {
                        cat.cards[card].selectionWeight *= 10;
                    }
                }

                //Mult 10
                dccsLakesInteractablesDLC2.AddCard(0, iscCategoryChest2Healing);
                dccsLakesInteractablesDLC2.AddCard(0, iscCategoryChest2Utility);
                //Mult 10
                dccsLakesnightInteractables.AddCard(0, iscCategoryChest2Healing);
                dccsLakesnightInteractables.AddCard(0, iscCategoryChest2Utility);

                //Mult 10
                dccsVillageInteractablesDLC1.AddCard(0, iscCategoryChest2Damage);
                dccsVillageInteractablesDLC1.AddCard(0, iscCategoryChest2Utility);
                //No Need
                dccsVillagenightInteractablesDLC2.AddCard(0, iscCategoryChest2Damage);
                dccsVillagenightInteractablesDLC2.AddCard(0, iscCategoryChest2Utility);

                //Mult 10
                dccsLemurianTempleInteractables.AddCard(0, iscCategoryChest2Damage);
                dccsLemurianTempleInteractables.AddCard(0, iscCategoryChest2Healing);
                dccsLemurianTempleInteractables.AddCard(0, iscCategoryChest2Utility);

                //Mult 10
                dccsHabitatInteractables.AddCard(0, iscCategoryChest2Damage);
                dccsHabitatInteractables.AddCard(0, iscCategoryChest2Healing);
                //Mult 10
                dccsHabitatfallInteractables.AddCard(0, iscCategoryChest2Damage);
                dccsHabitatfallInteractables.AddCard(0, iscCategoryChest2Healing);

                //Mult 10
                dccsHelminthRoostInteractables.AddCard(0, iscCategoryChest2Damage);
                dccsHelminthRoostInteractables.AddCard(0, iscCategoryChest2Healing);
                dccsHelminthRoostInteractables.AddCard(0, iscCategoryChest2Utility);

            }



            //Other Stage Interactable Changes
            DirectorCardCategorySelection[] allinteractables = new DirectorCardCategorySelection[] {
                dccsGolemplainsInteractablesDLC2, dccsBlackBeachInteractablesDLC2, dccsSnowyForestInteractablesDLC2,
                dccsGooLakeInteractablesDLC2, dccsFoggySwampInteractablesDLC2, dccsAncientLoftInteractablesDLC2,
                dccsFrozenWallInteractablesDLC2, dccsWispGraveyardInteractablesDLC2, dccsSulfurPoolsInteractablesDLC2,
                dccsDampCaveInteractablesDLC2, dccsShipgraveyardInteractablesDLC2, dccsRootJungleInteractablesDLC2,
                dccsSkyMeadowInteractablesDLC2,
                dccsLakesInteractablesDLC2, dccsLakesnightInteractables, dccsVillageInteractablesDLC1, dccsVillagenightInteractablesDLC2,
                dccsLemurianTempleInteractables,
                dccsHabitatfallInteractables,dccsHabitatInteractables,
                dccsHelminthRoostInteractables
            };

            DoToAllDCCS(allinteractables);

        }


        public static void DoToAllDCCS(DirectorCardCategorySelection[] allinteractables)
        {
            DirectorCardCategorySelection.Category category;

            for (int dccs = 0; allinteractables.Length > dccs; dccs++)
            {
                var direct = allinteractables[dccs];
                #region Chests (Always 0)
                category = allinteractables[dccs].categories[0];
                for (int card = 0; card < category.cards.Length; card++)
                {
                    if (category.cards[card].spawnCard.name.EndsWith("oryChest2"))
                    {
                        category.cards[card].selectionWeight *= 3;
                    }
                }
                #endregion
                #region Shrines (Always 2)
                category = allinteractables[dccs].categories[2];
                for (int card = 0; card < category.cards.Length; card++)
                {
                    if (category.cards[card].spawnCard.name.EndsWith("Cleanse"))
                    {
                        if (category.cards[card].selectionWeight == 3)
                        {
                            category.cards[card].selectionWeight = 14;
                        }
                    }
                    else if (category.cards[card].spawnCard.name.StartsWith("iscShrineBoss"))
                    {
                        //Bro RoRR has way more mountains this should help
                        category.cards[card].selectionWeight = (int)(category.cards[card].selectionWeight * WConfig.InteractablesMountainMultiplier.Value);
                    }
                }      
                #endregion
                #region Drone (Always 3)
                category = allinteractables[dccs].categories[3];
                for (int card = 0; card < category.cards.Length; card++)
                {
                    if (category.cards[card].spawnCard.name.EndsWith("EquipmentDrone"))
                    {
                        category.cards[card].selectionWeight = 4;
                    }
                }
                #endregion
                #region Rare (4 or 5)
                int foundIndex = allinteractables[dccs].FindCategoryIndexByName("Rare");
                if (foundIndex != -1)
                {
                    category = allinteractables[dccs].categories[foundIndex];
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
                        allinteractables[dccs].AddCard(foundIndex, ChangesInteractables.ADTrippleRed);
                    }
                }
                #endregion
                #region Duplicator (5 or 6)
                foundIndex = allinteractables[dccs].FindCategoryIndexByName("Duplicator");
                if (foundIndex != -1)
                {
                    category = allinteractables[dccs].categories[foundIndex];
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
                #region Void (6,7,8,9)
                foundIndex = allinteractables[dccs].FindCategoryIndexByName("Void Stuff");
                if (foundIndex != -1)
                {
                    allinteractables[dccs].categories[foundIndex].selectionWeight += 0.5f;
                }
                #endregion
                //Storm Stuff might be Seekers of the Storm stuff, and either at 6,7,8,9
                for (int cat = 6; allinteractables[dccs].categories.Length > cat; cat++)
                {
                    category = allinteractables[dccs].categories[cat];
                    if (category.name.StartsWith("Storm"))
                    {
                        category.cards[1].minimumStageCompletions = 2; //Shrine Revive always at 1
                    }
                }
            }

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