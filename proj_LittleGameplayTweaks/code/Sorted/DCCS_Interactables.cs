using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class DCCS_Interactables
    {
        public static void Start()
        {
            if (WConfig.cfgCredits_Interactables.Value == true)
            {

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/DeepVoidPortalBattery/iscDeepVoidPortalBattery.asset").WaitForCompletion().directorCreditCost = 0; //Idk if this cost matters but it should be 0

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/DuplicatorWild/iscDuplicatorWild.asset").WaitForCompletion().directorCreditCost = 15; //10 Default

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidTriple/iscVoidTriple.asset").WaitForCompletion().directorCreditCost = 30; //40 default

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Chest1StealthedVariant/iscChest1Stealthed.asset").WaitForCompletion().directorCreditCost = 2; //10 default
                //Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/Base/Chest1StealthedVariant/iscChest1Stealthed.asset").WaitForCompletion().maxSpawnsPerStage = 2;

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidCoinBarrel/iscVoidCoinBarrel.asset").WaitForCompletion().directorCreditCost = 10; //15 default
                LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscLunarChest").directorCreditCost = 15; //25 default

                LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscBrokenEmergencyDrone").directorCreditCost -= 5;
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

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/iscShrineColossusAccess.asset").WaitForCompletion().directorCreditCost = 40; //Rez Shrine
                                                                                                                                                    //Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/iscShrineColossusAccess.asset").WaitForCompletion().requiredFlags = 0;

            }


            if (WConfig.cfgDccsInteractables.Value)
            {
                Interactables_Stage1();
                Interactables_Stage2();
                Interactables_Stage3();
                Interactables_Stage4();
                Interactables_Stage5();
            }
            Interactables_Other();

            //How can we optimize this big ass mess?
            //Increasing Void Weight when made?
            //Increase Storm weight wehen made?
            //rest? idk
            On.RoR2.ClassicStageInfo.RebuildCards += ClassicStageInfo_RebuildCards;
            On.RoR2.SceneDirector.GenerateInteractableCardSelection += DCCS_Interactables_ChangesMidRun;


        }


        public static void InteractableDCCS_Changes(DirectorCardCategorySelection dccs)
        {
            //Debug.Log("LTG_DCCS");
            if (dccs == null)
            {
                return;
            }

            //Blended DCCS have stuff mixed around often
            int chestIndex = dccs.FindCategoryIndexByName("Chests");
            //int shrineIndex = dccs.FindCategoryIndexByName("Shrines");
            //int droneIndex = dccs.FindCategoryIndexByName("Drones");
            int duplicatorIndex = dccs.FindCategoryIndexByName("Duplicator");
            int rareIndex = dccs.FindCategoryIndexByName("Rare");
            int voidIndex = dccs.FindCategoryIndexByName("Void Stuff");
            int stormIndex = dccs.FindCategoryIndexByName("Storm Stuff");

            //CompareOrdinal??

            DirectorCardCategorySelection.Category category;
            if (chestIndex != -1)
            {
                category = dccs.categories[chestIndex];
                for (int card = 0; card < category.cards.Length; card++)
                {
                    if (category.cards[card].spawnCard.name.StartsWith("iscCategoryChest2"))
                    {
                        category.cards[card].selectionWeight = 8;
                        break;
                    }
                }
            }
            /*if (shrineIndex != -1)
            {
                category = dccs.categories[shrineIndex];
            }
            if (droneIndex != -1)
            {
                category = dccs.categories[droneIndex];
            }*/
            if (duplicatorIndex != -1)
            {
                category = dccs.categories[duplicatorIndex];
                category.cards[1].selectionWeight = 7; //Green Dupli Always at 1
                category.cards[2].selectionWeight = 2; //Mili Dupli Always at 2
                category.cards[2].minimumStageCompletions = 3; //Mili Dupli Always at 2
                for (int card = 3; card < category.cards.Length; card++)
                {
                    //if (category.cards[card].spawnCard.name == "iscDuplicatorWild")
                    if (category.cards[card].spawnCard.name.EndsWith("Wild"))
                    {
                        category.cards[card].minimumStageCompletions = 2;
                        break;
                    }
                }
            }
            if (rareIndex != -1)
            {
                category = dccs.categories[rareIndex];
                dccs.categories[rareIndex].selectionWeight += 0.1f;
                for (int card = 0; card < category.cards.Length; card++)
                {
                    if (category.cards[card].spawnCard.name == "iscRadarTower")
                    {
                        category.cards[card].selectionWeight = 22;
                        break;
                    }
                }
            }
            if (voidIndex != -1)
            {
                dccs.categories[voidIndex].selectionWeight += 0.5f;
            }
            if (stormIndex != -1)
            {
                dccs.categories[stormIndex].cards[1].minimumStageCompletions = 2; //Shrine Revive always at 1
            }

        }


        public static void Mountain()
        {
            InteractableSpawnCard iscShrineBoss = Object.Instantiate(Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/Base/ShrineBoss/iscShrineBoss.asset").WaitForCompletion());
            InteractableSpawnCard iscShrineBossSandy = Object.Instantiate(Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/Base/ShrineBoss/iscShrineBossSandy.asset").WaitForCompletion());
            InteractableSpawnCard iscShrineBossSnowy = Object.Instantiate(Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/Base/ShrineBoss/iscShrineBossSnowy.asset").WaitForCompletion());
            iscShrineBoss.maxSpawnsPerStage = 1;
            iscShrineBossSandy.maxSpawnsPerStage = 1;
            iscShrineBossSnowy.maxSpawnsPerStage = 1;


        }


        public static void ClassicStageInfo_RebuildCards(On.RoR2.ClassicStageInfo.orig_RebuildCards orig, ClassicStageInfo self, DirectorCardCategorySelection forcedMonsterCategory, DirectorCardCategorySelection forcedInteractableCategory)
        {
            orig(self, forcedMonsterCategory, forcedInteractableCategory);
            if (forcedInteractableCategory == null)
            {
                DoThing = true;
            }
        }
        public static bool DoThing = false;
        public static WeightedSelection<DirectorCard> DCCS_Interactables_ChangesMidRun(On.RoR2.SceneDirector.orig_GenerateInteractableCardSelection orig, SceneDirector self)
        {
            if (NetworkServer.active && DoThing)
            {
                DoThing = false;
                if (self.interactableCredit > 0)
                {
                    if (ClassicStageInfo.instance && ClassicStageInfo.instance.interactableCategories)
                    {
                        InteractableDCCS_Changes(ClassicStageInfo.instance.interactableCategories);
                    }
                }

            }
            /*var temp = orig(self);
            for (int i = 0; i < temp.Count; i++)
            {
                Debug.Log(temp.choices[i].value.spawnCard + " | " + temp.choices[i].weight);
            }*/
            return orig(self);
        }



        public static void Interactables_Other()
        {


            DirectorCardCategorySelection dccsInfiniteTowerInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dccsInfiniteTowerInteractables.asset").WaitForCompletion();
            dccsInfiniteTowerInteractables.categories[2].selectionWeight += 0.1f;

            DirectorCardCategorySelection dccsGoldshoresInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goldshores/dccsGoldshoresInteractables.asset").WaitForCompletion();
            DirectorCard ADShrineCleanse1 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                selectionWeight = 5,
            };
            DirectorCard AdShrineCombat = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombat.asset").WaitForCompletion(),
                selectionWeight = 10,
            };
            DirectorCard ADShrineChance = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineChance/iscShrineChance.asset").WaitForCompletion(),
                selectionWeight = 5,
            };
            DirectorCard ADShrineHealing = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscShrineHealing"),
                selectionWeight = 5,
            };


            DirectorCardCategorySelection.Category GoldShoreShrines = new DirectorCardCategorySelection.Category
            {
                name = "Shrines",
                selectionWeight = 10,
                cards = new DirectorCard[] { AdShrineCombat, ADShrineChance, ADShrineCleanse1, ADShrineHealing }
            };
            dccsGoldshoresInteractables.categories = new DirectorCardCategorySelection.Category[] { GoldShoreShrines };



            if (ConfigStages.Stage_X_Arena_Void.Value)
            {
                //Stealthed Chest replaced with Order Shrine
                DirectorCardCategorySelection dccsArenaInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/arena/dccsArenaInteractables.asset").WaitForCompletion();
                dccsArenaInteractables.categories[2].cards[0].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion();
            }
        }


        public static void Interactables_Stage1()
        {
            #region DCCS
            //DirectorCardCategorySelection dccsGolemplainsInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsInteractables.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsGolemplainsInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsGolemplainsInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGolemplainsInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsBlackBeachInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachInteractables.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsBlackBeachInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsBlackBeachInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsBlackBeachInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSnowyForestInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/snowyforest/dccsSnowyForestInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsSnowyForestInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSnowyForestInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsLakesInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesInteractablesDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesInteractables_DLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsLakesnightInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightInteractables_DLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesnightInteractables_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightInteractables_DLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsVillageInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageInteractables_DLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillageInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsVillagenightInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillagenightInteractablesDLC2.asset").WaitForCompletion();
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
                selectionWeight = 4,
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
                selectionWeight = 15,
                minimumStageCompletions = 2,
            };
            DirectorCard ShrineOrder_DEFAULT10 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion(),
                selectionWeight = 10,
            };
            #endregion
            #region Adds

            if (ConfigStages.Stage_1_Roost.Value)
            {
                dccsBlackBeachInteractables.AddCard(2, LoopShrineCleanse);
            }
            if (ConfigStages.Stage_1_Snow.Value)
            {
                dccsSnowyForestInteractablesDLC1.AddCard(3, ADBrokenMegaDrone);
                dccsSnowyForestInteractablesDLC1.AddCard(5, ShrineOrder_SNOW);
            }
            if (ConfigStages.Stage_1_Lake.Value)
            {
                dccsLakesInteractables_DLC2.categories[0].cards[0].selectionWeight = 2; //Cleansing Shrine

                DCCS.MultWholeCateory(dccsLakesInteractables, 0, 10);
                int chests = dccsLakesInteractablesDLC1.AddCategory("Chests", 45);
                dccsLakesInteractablesDLC1.AddCard(chests, iscCategoryChest2Healing);
                dccsLakesInteractablesDLC1.AddCard(chests, iscCategoryChest2Utility);

                DCCS.MultWholeCateory(dccsLakesnightInteractables_DLC2, 0, 10);
                chests = dccsLakesnightInteractables_DLC1.AddCategory("Chests", 45);
                dccsLakesnightInteractables_DLC1.AddCard(chests, iscCategoryChest2Healing);
                dccsLakesnightInteractables_DLC1.AddCard(chests, iscCategoryChest2Utility);
            }
            if (ConfigStages.Stage_1_Village.Value)
            {
                dccsVillageInteractables_DLC2.AddCard(2, ShrineOrder_DEFAULT10);
                dccsVillageInteractables_DLC2.AddCard(2, LoopShrineCleanse15);
                dccsVillageInteractables_DLC2.AddCard(3, ADBrokenMegaDrone);

                DCCS.MultWholeCateory(dccsVillageInteractables_DLC2, 0, 10);
                int chests = dccsVillageInteractablesDLC1.AddCategory("Chests", 45);
                dccsVillageInteractablesDLC1.AddCard(chests, iscCategoryChest2Damage);
                dccsVillageInteractablesDLC1.AddCard(chests, iscCategoryChest2Utility);
            }


            #endregion

        }
        public static void Interactables_Stage2()
        {
            #region DCCS
            //DirectorCardCategorySelection dccsGooLakeInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeInteractables.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsGooLakeInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsGooLakeInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGooLakeInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFoggySwampInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampInteractables.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsFoggySwampInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsFoggySwampInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFoggySwampInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsAncientLoftInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/ancientloft/dccsAncientLoftInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsAncientLoftInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsAncientLoftInteractablesDLC2.asset").WaitForCompletion();

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
            DirectorCard ShrineOrder_SANDR = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSandy.asset").WaitForCompletion(),
                selectionWeight = 4,
            };
            #endregion

            if (ConfigStages.Stage_2_Swamp.Value)
            {
                dccsFoggySwampInteractables.categories[2].cards[3].selectionWeight = 13;
            }
            if (ConfigStages.Stage_2_Ancient.Value)
            {
                dccsAncientLoftInteractablesDLC1.categories[3].cards[1].selectionWeight = 4;
                dccsAncientLoftInteractablesDLC1.AddCard(4, ShrineOrder_SANDR); //RARE
            }
            if (ConfigStages.Stage_2_Temple.Value)
            {
                dccsLemurianTempleInteractables_DLC2.AddCard(2, ShrineOrder_SAND);

                DCCS.MultWholeCateory(dccsLemurianTempleInteractables_DLC2, 0, 10);
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
            //DirectorCardCategorySelection dccsFrozenWallInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsFrozenWallInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFrozenWallInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsWispGraveyardInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardInteractables.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsWispGraveyardInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsWispGraveyardInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsWispGraveyardInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSulfurPoolsInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsSulfurPoolsInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSulfurPoolsInteractablesDLC2.asset").WaitForCompletion();

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
                selectionWeight = 15,
            };
            DirectorCard ADBrokenEmergencyDrone = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenEmergencyDrone"),
                selectionWeight = 2,
            };
            #endregion

            if (ConfigStages.Stage_3_Wisp.Value)
            {
                dccsWispGraveyardInteractables.categories[2].cards[3].selectionWeight = 14; //Cleanse more?
            }
            if (ConfigStages.Stage_3_Sulfur.Value)
            {
                dccsSulfurPoolsInteractablesDLC1.AddCard(2, ADShrineBoss10);
                dccsSulfurPoolsInteractablesDLC1.AddCard(3, ADBrokenEmergencyDrone);
            }
            if (ConfigStages.Stage_3_Tree.Value)
            {
                DCCS.MultWholeCateory(dccsHabitatInteractables_DLC2, 0, 10);
                int chests = dccsHabitatInteractables_DLC1.AddCategory("Chests", 45);
                dccsHabitatInteractables_DLC1.AddCard(chests, iscCategoryChest2Damage);
                dccsHabitatInteractables_DLC1.AddCard(chests, iscCategoryChest2Healing);
            }

        }
        public static void Interactables_Stage4()
        {
            #region DCCS
            DirectorCardCategorySelection dccsDampCaveInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveInteractables.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsDampCaveInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsDampCaveInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsDampCaveInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsShipgraveyardInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardInteractables.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsShipgraveyardInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsShipgraveyardInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShipgraveyardInteractablesDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsRootJungleInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleInteractables.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsRootJungleInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsRootJungleInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsRootJungleInteractablesDLC2.asset").WaitForCompletion();
            #endregion

            //Remove Gunner Turret from Stage 4/5
            if (ConfigStages.Stage_4_Damp_Abyss.Value)
            {
                //dccsDampCaveInteractables.categories[3].cards[2].selectionWeight = 4; //Eq drone
                DCCS.RemoveCard(dccsDampCaveInteractables, 4, 0);//Gunner Turret Removal
            }
            if (ConfigStages.Stage_4_Ship.Value)
            {
                dccsShipgraveyardInteractables.categories[3].cards[2].selectionWeight = 5; //Eq drone
                dccsShipgraveyardInteractables.categories[2].cards[3].selectionWeight = 12; //Cleanse more
                DCCS.RemoveCard(dccsShipgraveyardInteractables, 4, 0);//Gunner Turret Removal
            }
            if (ConfigStages.Stage_4_Root_Jungle.Value)
            {
                DCCS.RemoveCard(dccsRootJungleInteractables, 4, 0);//Gunner Turret Removal
            }
        }
        public static void Interactables_Stage5()
        {
            #region DCCS
            DirectorCardCategorySelection dccsSkyMeadowInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowInteractables.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsSkyMeadowInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowInteractablesDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsSkyMeadowInteractablesDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSkyMeadowInteractablesDLC2.asset").WaitForCompletion();

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

            if (ConfigStages.Stage_5_Sky.Value)
            {
                DCCS.RemoveCard(dccsSkyMeadowInteractables, 4, 0); //Gunner Turret Removal
            }
            if (ConfigStages.Stage_5_Helminth.Value)
            {
                DCCS.RemoveCard(dccsHelminthRoostInteractables_DLC2, 7, 0); //Gunner Turret Removal

                DCCS.MultWholeCateory(dccsHelminthRoostInteractables_DLC2, 0, 10);
                DCCS.MultWholeCateory(dccsHelminthRoostInteractables_DLC1, 0, 10);
                dccsHelminthRoostInteractables_DLC1.AddCard(0, iscCategoryChest2Damage);
                dccsHelminthRoostInteractables_DLC1.AddCard(0, iscCategoryChest2Healing);
                dccsHelminthRoostInteractables_DLC1.AddCard(0, iscCategoryChest2Utility);
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