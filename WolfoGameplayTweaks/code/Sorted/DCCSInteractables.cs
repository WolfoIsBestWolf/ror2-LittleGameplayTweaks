using R2API;
using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LittleGameplayTweaks
{
    public class DCCSInteractables
    {
  
        public static void Start()
        {
            if (WConfig.DCCSInteractableChanges.Value)
            {
                DCCSThingsNoDLC();
                DCCSThingsDLC1();
            }
            if (WConfig.DCCSInteractablesStageCredits.Value)
            {
                On.RoR2.ClassicStageInfo.RebuildCards += MoreSceneCredits;
            }
            if (WConfig.DCCSInteractableCostChanges.Value == true)
            {
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/DuplicatorWild/iscDuplicatorWild.asset").WaitForCompletion().directorCreditCost = 15; //10 Default

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidTriple/iscVoidTriple.asset").WaitForCompletion().directorCreditCost = 25; //40 default
                
                //Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidCoinBarrel/iscVoidCoinBarrel.asset").WaitForCompletion().directorCreditCost = 15; //15 default
                RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscLunarChest").directorCreditCost = 15; //25 default
                RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscLunarChest").requiredFlags = 0;
                RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscBrokenMegaDrone").directorCreditCost = 25;
               

                RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscShrineHealing").directorCreditCost = 5;

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion().directorCreditCost = 5; //30 default (hopoo gaems)
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSandy.asset").WaitForCompletion().directorCreditCost = 5;
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSnowy.asset").WaitForCompletion().directorCreditCost = 5;

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBlood.asset").WaitForCompletion().directorCreditCost -= 5; //20 default
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBloodSandy.asset").WaitForCompletion().directorCreditCost -= 5;
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBloodSnowy.asset").WaitForCompletion().directorCreditCost -= 5;

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombat.asset").WaitForCompletion().directorCreditCost -= 5; //20 default
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombatSandy.asset").WaitForCompletion().directorCreditCost -= 5;
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombatSnowy.asset").WaitForCompletion().directorCreditCost -= 5;
            }
        }

        public static void DCCSThingsNoDLC()
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

            DirectorCardCategorySelection dccsArtifactWorldInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/artifactworld/dccsArtifactWorldInteractables.asset").WaitForCompletion();
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
            DirectorCard ADTrippleRed = new DirectorCard
            {
                spawnCard = ChangesInteractables.RedMultiShopISC,
                selectionWeight = 2,
                minimumStageCompletions = 4,
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

            //Doesn't work because it doesn't get unloaded resources
            //DirectorCardCategorySelection[] allinteractables = Resources.GetBuiltinResource<DirectorCardCategorySelection>();

            for (int dccs = 0; allinteractables.Length > dccs; dccs++)
            {
                //Debug.Log(allinteractables[dccs]);
                for (int cat = 0; allinteractables[dccs].categories.Length > cat; cat++)
                {
                    if (allinteractables[dccs].categories[cat].name.Equals("Chests"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (!allinteractables[dccs].name.Equals("dccsSkyMeadowInteractables"))
                            {
                                if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.StartsWith("iscCategoryChest"))
                                {
                                    allinteractables[dccs].categories[cat].cards[card].selectionWeight *= 3;
                                }
                            }
                        }
                    }
                    else if (allinteractables[dccs].categories[cat].name.Equals("Shrines"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.Equals("iscShrineCleanse"))
                            {
                                if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 3)
                                {
                                    allinteractables[dccs].categories[cat].cards[card].selectionWeight = 14;
                                }
                            }
                            else if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.StartsWith("iscShrineBoss"))
                            {
                                //Bro RoRR has way more mountains this should help
                                allinteractables[dccs].categories[cat].cards[card].selectionWeight = (int)(allinteractables[dccs].categories[cat].cards[card].selectionWeight * WConfig.InteractablesMountainMultiplier.Value);
                            }
                        }
                    }
                    else if (allinteractables[dccs].categories[cat].name.Equals("Drones"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.Equals("iscBrokenEquipmentDrone"))
                            {
                                allinteractables[dccs].categories[cat].cards[card].selectionWeight = 3;
                            }
                        }
                    }
                    else if (allinteractables[dccs].categories[cat].name.Equals("Rare"))
                    {
                        allinteractables[dccs].categories[cat].selectionWeight += 0.1f;
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.Equals("iscRadarTower"))
                            {
                                allinteractables[dccs].categories[cat].cards[card].selectionWeight = 20;
                            }
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.Equals("iscGoldChest"))
                            {
                                allinteractables[dccs].AddCard(cat, ADTrippleRed);
                            }
                        }
                    }
                    else if (allinteractables[dccs].categories[cat].name.Equals("Duplicator"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            switch (allinteractables[dccs].categories[cat].cards[card].spawnCard.name)
                            {
                                case "iscDuplicator":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 30)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].selectionWeight = 27;
                                    }
                                    break;
                                case "iscDuplicatorLarge":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 6)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].selectionWeight = 7;
                                    }
                                    break;
                                case "iscDuplicatorMilitary":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 1)
                                    {
                                        if (dccs < 6)
                                        {
                                            allinteractables[dccs].categories[cat].cards[card].selectionWeight = 2;
                                        }
                                        else
                                        {
                                            allinteractables[dccs].categories[cat].cards[card].selectionWeight = 3;
                                        }
                                    }
                                    if (allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions == 4)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions = 3;
                                    }
                                    break;
                                case "iscDuplicatorWild":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 2)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].selectionWeight = 3;
                                    }
                                    else if (allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions == 1)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions = 2;
                                    }
                                    break;
                            }
                        }
                    }

                }
            }

        }

        public static void DCCSThingsDLC1()
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

            DirectorCardCategorySelection dccsArtifactWorldInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/artifactworld/dccsArtifactWorldInteractablesDLC1.asset").WaitForCompletion();
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
            DirectorCard ADTrippleRed = new DirectorCard
            {
                spawnCard = ChangesInteractables.RedMultiShopISC,
                selectionWeight = 2,
                minimumStageCompletions = 1
        };

            DirectorCardCategorySelection dccsInfiniteTowerInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dccsInfiniteTowerInteractables.asset").WaitForCompletion();
            dccsInfiniteTowerInteractables.AddCard(2, ADTrippleRed);
            dccsInfiniteTowerInteractables.categories[2].selectionWeight += 0.1f;
            //

            ADTrippleRed.minimumStageCompletions = 3;
            //CategoryChest shenanigans

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


            //dccsBlackBeachInteractablesDLC1.categories[2].selectionWeight = 7;
            //dccsBlackBeachInteractablesDLC1.categories[2].cards[2].selectionWeight = 2;
            dccsBlackBeachInteractablesDLC1.categories[7].selectionWeight = 5;

            //dccsDampCaveInteractablesDLC1.categories[2].cards[0].selectionWeight *= 2;
            //dccsDampCaveInteractablesDLC1.categories[2].cards[1].selectionWeight *= 3;
            //dccsDampCaveInteractablesDLC1.categories[2].cards[2].selectionWeight *= 2;

            dccsSnowyForestInteractablesDLC1.AddCard(3, ADBrokenMegaDrone);

            dccsSulfurPoolsInteractablesDLC1.categories[2].cards = dccsSulfurPoolsInteractablesDLC1.categories[2].cards.Remove(dccsSulfurPoolsInteractablesDLC1.categories[2].cards[3], dccsSulfurPoolsInteractablesDLC1.categories[2].cards[2]);
            dccsSulfurPoolsInteractablesDLC1.AddCard(2, ADShrineBoss10);

            dccsArtifactWorldInteractablesDLC1.categories[2].cards[1] = ADShrineCleanse1;

            //Remove Gunner Turret from Stage 4/5
            dccsDampCaveInteractablesDLC1.categories[4].cards = dccsDampCaveInteractablesDLC1.categories[4].cards.Remove(dccsDampCaveInteractablesDLC1.categories[4].cards[0]);
            dccsShipgraveyardInteractablesDLC1.categories[4].cards = dccsShipgraveyardInteractablesDLC1.categories[4].cards.Remove(dccsShipgraveyardInteractablesDLC1.categories[4].cards[0]);
            dccsRootJungleInteractablesDLC1.categories[4].cards = dccsRootJungleInteractablesDLC1.categories[4].cards.Remove(dccsRootJungleInteractablesDLC1.categories[4].cards[0]);
            dccsSkyMeadowInteractablesDLC1.categories[4].cards = dccsSkyMeadowInteractablesDLC1.categories[4].cards.Remove(dccsSkyMeadowInteractablesDLC1.categories[4].cards[0]);

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
                dccsSkyMeadowInteractablesDLC1, dccsArtifactWorldInteractablesDLC1 };

            //Doesn't work because it doesn't get unloaded resources
            //DirectorCardCategorySelection[] allinteractables = Resources.GetBuiltinResource<DirectorCardCategorySelection>();

            for (int dccs = 0; allinteractables.Length > dccs; dccs++)
            {
                //Debug.Log(allinteractables[dccs]);
                for (int cat = 0; allinteractables[dccs].categories.Length > cat; cat++)
                {
                    if (allinteractables[dccs].categories[cat].name.Equals("Chests"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (!allinteractables[dccs].name.Equals("dccsSkyMeadowInteractablesDLC1"))
                            {
                                if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.StartsWith("iscCategoryChest"))
                                {
                                    allinteractables[dccs].categories[cat].cards[card].selectionWeight *= 3;
                                }
                            }
                        }
                    }
                    else if (allinteractables[dccs].categories[cat].name.Equals("Shrines"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.Equals("iscShrineCleanse"))
                            {
                                if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 3)
                                {
                                    allinteractables[dccs].categories[cat].cards[card].selectionWeight = 14;
                                }
                            }
                            else if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.StartsWith("iscShrineBoss"))
                            {
                                //Bro RoRR has way more mountains this should help
                                allinteractables[dccs].categories[cat].cards[card].selectionWeight = (int)(allinteractables[dccs].categories[cat].cards[card].selectionWeight * WConfig.InteractablesMountainMultiplier.Value);
                            }
                        }
                    }
                    else if (allinteractables[dccs].categories[cat].name.Equals("Drones"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.Equals("iscBrokenEquipmentDrone"))
                            {
                                allinteractables[dccs].categories[cat].cards[card].selectionWeight = 4;
                            }
                        }
                    }
                    else if (allinteractables[dccs].categories[cat].name.Equals("Rare"))
                    {
                        allinteractables[dccs].categories[cat].selectionWeight += 0.1f;
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.Equals("iscRadarTower"))
                            {
                                allinteractables[dccs].categories[cat].cards[card].selectionWeight = 20;
                            }
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.Equals("iscGoldChest"))
                            {
                                allinteractables[dccs].AddCard(cat, ADTrippleRed);
                            }
                        }
                    }
                    else if (allinteractables[dccs].categories[cat].name.Equals("Duplicator"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            switch (allinteractables[dccs].categories[cat].cards[card].spawnCard.name)
                            {
                                case "iscDuplicator":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 30)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].selectionWeight = 27;
                                    }
                                    break;
                                case "iscDuplicatorLarge":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 6)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].selectionWeight = 7;
                                    }
                                    break;
                                case "iscDuplicatorMilitary":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 1)
                                    {
                                        if (dccs < 6)
                                        {
                                            allinteractables[dccs].categories[cat].cards[card].selectionWeight = 2;
                                        }
                                        else
                                        {
                                            allinteractables[dccs].categories[cat].cards[card].selectionWeight = 3;
                                        }
                                    }
                                    if (allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions == 4)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions = 3;
                                    }
                                    break;
                                case "iscDuplicatorWild":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 2)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].selectionWeight = 3;
                                    }
                                    else if (allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions == 1)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions = 2;
                                    }
                                    break;
                            }
                        }
                    }

                }
            }

        }


        public static void MoreSceneCredits(On.RoR2.ClassicStageInfo.orig_RebuildCards orig, ClassicStageInfo self)
        {
            if (Run.instance && SceneInfo.instance)
            {
                switch (SceneInfo.instance.sceneDef.baseSceneName)
                {
                    case "golemplains":
                        self.sceneDirectorInteractibleCredits += 20; //Rather buff these two a bit than nerf Snowy Ig we'll see
                        break;
                    case "blackbeach":
                        self.sceneDirectorInteractibleCredits += 20; //
                        break;
                    case "snowyforest":
                        //self.sceneDirectorInteractibleCredits -= 20; //It deserves it right idk
                        break;
                    case "foggyswamp":
                        //self.sceneDirectorInteractibleCredits += 30;
                        break;
                    case "goolake":
                        self.sceneDirectorInteractibleCredits += 20; //Has 60 less due to the Lemurians a bit too harsh imo
                        break;
                    case "rootjungle":
                        self.sceneDirectorInteractibleCredits += 40; //Depths sometimes has 560 so raising the other two by 40 seems fine
                        break;
                    case "shipgraveyard":
                        self.sceneDirectorInteractibleCredits += 40; //Depths has 400 or 560
                        break;
                    case "goldshores":
                        self.sceneDirectorInteractibleCredits += 114; //For Fun ig
                        break;
                    case "artifactworld":
                        self.sceneDirectorInteractibleCredits += 40; //For Fun ig
                        break;
                }
            }
            orig(self);
        }

    }
}