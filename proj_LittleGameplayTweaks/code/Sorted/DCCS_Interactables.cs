using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class DCCS_Interactables
    {
        public static bool VanillaVoidsInstalled = false;
        public static void Start()
        {
            DCCS_Interactables.VanillaVoidsInstalled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Zenithrium.vanillaVoid");

            if (WConfig.cfgDccsInteractables.Value)
            {
                Interactables_Stage1();
                Interactables_Stage2();
                Interactables_Stage3();
                Interactables_Stage4();
                Interactables_Stage5();
                FixWrongCategoryName();
                On.RoR2.ClassicStageInfo.RebuildCards += ClassicStageInfo_RebuildCards;
                On.RoR2.SceneDirector.GenerateInteractableCardSelection += SceneDirector_GenerateInteractableCardSelection;
            }
            Interactables_Other();

        }



        /*public static void MatchCategoryChestsToLarge(DirectorCardCategorySelection.Category dccs, int card)
        {
            if (WConfig.cfgMatchCategory.Value == WConfig.MatchCategory.NoChange)
            {
                return;
            }
            if (WConfig.cfgMatchCategory.Value == WConfig.MatchCategory.RandomOneSmall)
            {
                DCCS.MatchCategoryChests(dccs, card, LittleGameplayTweaks.random.Next(0, 3));
                return;
            }
            string scene = SceneInfo.instance.sceneDef.baseSceneName;
            switch (scene)
            {
                case "snowyforest": //1
                case "goolake": //2
                case "wispgraveyard": //3
                case "habitat":
                case "habitatfall":
                case "dampcavesimple": //4
                    DCCS.MatchCategoryChests(dccs, card, (int)DCCS.Category.Damage);
                    break;
                case "golemplains":
                case "village":
                case "villagenight":
                case "foggyswamp":
                case "sulfurpools":
                case "shipgraveyard":
                    DCCS.MatchCategoryChests(dccs, card, (int)DCCS.Category.Healing);
                    break;
                case "blackbeach":
                case "lakes":
                case "lakesnight":
                case "ancientloft":
                case "frozenwall":
                case "rootjungle":
                    DCCS.MatchCategoryChests(dccs, card, (int)DCCS.Category.Utility);
                    break;
            }
 
        }*/


        public static void ClassicStageInfo_RebuildCards(On.RoR2.ClassicStageInfo.orig_RebuildCards orig, ClassicStageInfo self, DirectorCardCategorySelection forcedMonsterCategory, DirectorCardCategorySelection forcedInteractableCategory)
        {
            orig(self, forcedMonsterCategory, forcedInteractableCategory);
            if (NetworkServer.active && forcedInteractableCategory == null && self.sceneDirectorInteractibleCredits > 0)
            {
                DoThing = true;
            }
        }
        public static bool DoThing = false;
        private static WeightedSelection<DirectorCard> SceneDirector_GenerateInteractableCardSelection(On.RoR2.SceneDirector.orig_GenerateInteractableCardSelection orig, SceneDirector self)
        {
            if (DoThing)
            {
                DCCS_ApplyAsNeeded.InteractableDCCS_Changes(ClassicStageInfo.instance.interactableCategories);
                DoThing = false;
            }
            return orig(self);
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


        public static void FixWrongCategoryName()
        {
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "2751f6d6bca27a44a9e45d87c5bbee1c").WaitForCompletion().categories[7].name = "Storm Stuff"; //dccsVillageNightInteractables
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "38cab42b47b4a5f47a53cca09406c7e8").WaitForCompletion().categories[0].name = "Storm Stuff"; //dccsSnowyForestInteractablesDLC2
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "dcf6865317893a5418f29485be05eee1").WaitForCompletion().categories[0].name = "Storm Stuff"; //dccsBlackBeachInteractablesDLC2
            Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "b64fd8e8ca9b6c24faddc10ee7aeb476").WaitForCompletion().categories[0].name = "Storm Stuff"; //dccsGolemplainsInteractablesDLC2

        }

        public static void Interactables_Other()
        {

            DirectorCardCategorySelection dccsGoldshoresInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goldshores/dccsGoldshoresInteractables.asset").WaitForCompletion();
            /* DirectorCard ADShrineCleanse1 = new DirectorCard
             {
                 //spawnCardReference = new AssetReferenceT<SpawnCard>("RoR2/Base/ShrineCleanse/iscShrineCleanse.asset"),
                 spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                 selectionWeight = 5,
             };*/
            DirectorCard AdShrineCombat = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombat.asset").WaitForCompletion(),
                selectionWeight = 10,
            };
            DirectorCard ShrineBlood = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "a6d01afb758a15940bf09deb9db44067").WaitForCompletion(),
                selectionWeight = 5,
            };
            DirectorCard ADShrineHealing = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscShrineHealing"),
                selectionWeight = 5,
            };
            DirectorCard ADBARREL = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscBarrel1"),
                selectionWeight = 1,
            };

            DirectorCardCategorySelection.Category GoldShoreShrines = new DirectorCardCategorySelection.Category
            {
                name = "Shrines",
                selectionWeight = 10,
                cards = new DirectorCard[] { AdShrineCombat, ShrineBlood, ADShrineHealing, ADBARREL }
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
            DirectorCardCategorySelection dccsGolemplainsInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsBlackBeachInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSnowyForestInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/snowyforest/dccsSnowyForestInteractablesDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsLakesInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesnightInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightInteractables_DLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsVillageInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageInteractables_DLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillageNightInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "2751f6d6bca27a44a9e45d87c5bbee1c").WaitForCompletion();
            #endregion
            #region Cards
            /*DirectorCard iscCategoryChest2Damage = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                selectionWeight = 3,
            };*/
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
            DirectorCard loopTC280 = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenMegaDrone"),
                selectionWeight = 3,
                minimumStageCompletions = 4,
            };

            #endregion
            #region Adds
            if (ConfigStages.Stage_1_Golem.Value)
            {

            }

            if (ConfigStages.Stage_1_Roost.Value)
            {
                //Loop Cleanse Pool was a reference to a weird thing 1.0 SotS had
                /*dccsBlackBeachInteractables.AddCard(2, new DirectorCard //Cleanse10
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    minimumStageCompletions = 2,
                });*/
            }
            if (ConfigStages.Stage_1_Snow.Value)
            {
                dccsSnowyForestInteractablesDLC1.AddCard(3, loopTC280);
                dccsSnowyForestInteractablesDLC1.AddCard(5, new DirectorCard //Snow Order Rare
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSnowy.asset").WaitForCompletion(),
                    selectionWeight = 4,
                });
            }
            if (ConfigStages.Stage_1_Lake.Value)
            {
                //dccsLakesInteractables_DLC2.categories[0].cards[0].selectionWeight = 1; //Cleansing Shrine


                //Missing Large Category Chest
                DCCS.MultWholeCateory(dccsLakesInteractables, 0, 10);
                dccsLakesInteractables.AddCard(0, iscCategoryChest2Utility);
                DCCS.MultWholeCateory(dccsLakesnightInteractables_DLC2, 0, 10);
                dccsLakesnightInteractables_DLC2.AddCard(0, iscCategoryChest2Utility);

            }
            if (ConfigStages.Stage_1_Village.Value)
            {
                dccsVillageInteractables_DLC2.AddCard(2, new DirectorCard //Default uses x10 weights
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion(),
                    selectionWeight = 8,
                });

                DCCS.MultWholeCateory(dccsVillageInteractables_DLC2, 0, 10);
                dccsVillageInteractables_DLC2.AddCard(0, iscCategoryChest2Healing);

                dccsVillageNightInteractables.AddCard(2, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion(),
                    selectionWeight = 1,
                });
                dccsVillageNightInteractables.AddCard(2, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                    selectionWeight = 2,
                    minimumStageCompletions = 2,
                });
                dccsVillageNightInteractables.AddCard(2, loopTC280);
            }


            #endregion

        }
        public static void Interactables_Stage2()
        {
            #region DCCS
            //DirectorCardCategorySelection dccsGooLakeInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFoggySwampInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsAncientLoftInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/ancientloft/dccsAncientLoftInteractablesDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsLemurianTempleInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lemuriantemple/dccsLemurianTempleInteractables_DLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsNestInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "94dce4a2db8e76c4ca8c3f480ce3c602").WaitForCompletion();
            #endregion
            #region Cards

            #endregion

            if (ConfigStages.Stage_2_Swamp.Value)
            {
                dccsFoggySwampInteractables.categories[2].cards[3].selectionWeight = 10; //Cleanse
            }
            if (ConfigStages.Stage_2_Ancient.Value)
            {
                //dccsAncientLoftInteractablesDLC1.categories[3].cards[1].selectionWeight = 4;
                dccsAncientLoftInteractablesDLC1.AddCard(4, new DirectorCard //Order Sand Rare
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSandy.asset").WaitForCompletion(),
                    selectionWeight = 1,
                }); //RARE
            }
            if (ConfigStages.Stage_2_Temple.Value)
            {
                dccsLemurianTempleInteractables_DLC2.AddCard(2, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSandy.asset").WaitForCompletion(),
                    selectionWeight = 1,
                });

                DCCS.MultWholeCateory(dccsLemurianTempleInteractables_DLC2, 0, 10);
                dccsLemurianTempleInteractables_DLC2.AddCard(0, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Healing.asset").WaitForCompletion(),
                    selectionWeight = 3,
                });

            }
            if (ConfigStages.Stage_2_Nest.Value)
            {
                //Loop TC280
                dccsNestInteractables.AddCard(3, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenMegaDrone"),
                    selectionWeight = 3,
                    minimumStageCompletions = 4,
                });

            }

        }
        public static void Interactables_Stage3()
        {
            #region DCCS
            DirectorCardCategorySelection dccsFrozenWallInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallInteractables.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFrozenWallInteractablesDLC3 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "012378d89cb7b834d81c37c8a9fb12ec").WaitForCompletion();

            DirectorCardCategorySelection dccsWispGraveyardInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardInteractables.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSulfurPoolsInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsInteractablesDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsHabitatInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitat/dccsHabitatInteractables_DLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatFallInteractables_DLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "73da5a1bb52082b4cbf796ba3d457120").WaitForCompletion();
            DirectorCardCategorySelection dccsIronalluviumInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "9ff74dc74272e2743ace2444fa941b70").WaitForCompletion();
            DirectorCardCategorySelection dccsIronalluvium2Interactables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "0e043005f4848ef43a54354cc4332eda").WaitForCompletion();
            #endregion
            #region Cards


            #endregion

            if (ConfigStages.Stage_3_Frozen.Value)
            {
                //Add Drone Combiner
                dccsFrozenWallInteractables.AddCard(0, new DirectorCard
                {
                    spawnCardReference = new AssetReferenceT<SpawnCard>("2eaec01927ea16245822dcb50080cba3"),
                    selectionWeight = 10,
                });

                //The addition of really common Drone Shops made TC280 literally 3x as rare.
                dccsFrozenWallInteractablesDLC3.categories[1].selectionWeight = 2;
            }
            if (ConfigStages.Stage_3_Wisp.Value)
            {
                dccsWispGraveyardInteractables.categories[2].cards[3].selectionWeight = 10; //3 
            }
            if (ConfigStages.Stage_3_Sulfur.Value)
            {
                dccsSulfurPoolsInteractablesDLC1.AddCard(2, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBoss/iscShrineBoss.asset").WaitForCompletion(),
                    selectionWeight = 10,
                });

                dccsSulfurPoolsInteractablesDLC1.AddCard(3, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenEmergencyDrone"),
                    selectionWeight = 3,
                });
            }
            if (ConfigStages.Stage_3_Tree.Value)
            {
                DCCS.MultWholeCateory(dccsHabitatInteractables_DLC2, 0, 10);
                DCCS.MultWholeCateory(dccsHabitatFallInteractables_DLC2, 0, 10);
                dccsHabitatInteractables_DLC2.AddCard(0, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                    selectionWeight = 3,
                });
                dccsHabitatInteractables_DLC2.AddCard(3, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenEmergencyDrone"),
                    selectionWeight = 2,
                });
                dccsHabitatFallInteractables_DLC2.AddCard(0, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                    selectionWeight = 3,
                });
                dccsHabitatFallInteractables_DLC2.AddCard(3, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenEmergencyDrone"),
                    selectionWeight = 2,
                });
            }
            if (ConfigStages.Stage_3_Iron.Value)
            {
                //Add Drone Combiner
                dccsIronalluviumInteractables.AddCard(0, new DirectorCard
                {
                    spawnCardReference = new AssetReferenceT<SpawnCard>("2eaec01927ea16245822dcb50080cba3"),
                    selectionWeight = 10,
                });
                dccsIronalluvium2Interactables.AddCard(0, new DirectorCard
                {
                    spawnCardReference = new AssetReferenceT<SpawnCard>("2eaec01927ea16245822dcb50080cba3"),
                    selectionWeight = 10,
                });
            }
        }
        public static void Interactables_Stage4()
        {
            #region DCCS
            DirectorCardCategorySelection dccsDampCaveInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveInteractables.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsShipgraveyardInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardInteractables.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsRootJungleInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleInteractables.asset").WaitForCompletion();
            #endregion

            //Remove Gunner Turret from Stage 4/5
            if (ConfigStages.Stage_4_Damp_Abyss.Value)
            {
                dccsDampCaveInteractables.categories[3].cards[3].selectionWeight = 7; //Flame Drone
            }
            if (ConfigStages.Stage_4_Ship.Value)
            {
                dccsShipgraveyardInteractables.AddCard(3, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenMegaDrone"),
                    selectionWeight = 1,
                    minimumStageCompletions = 3,
                });

                dccsShipgraveyardInteractables.categories[2].cards[3].selectionWeight = 10; //Cleanse more
                dccsShipgraveyardInteractables.categories[3].cards[0].selectionWeight = 3; //Missile
                dccsShipgraveyardInteractables.categories[3].cards[1].selectionWeight = 4; //Heal
                dccsShipgraveyardInteractables.categories[3].cards[2].selectionWeight = 3; //Eq

            }
            if (ConfigStages.Stage_4_Root_Jungle.Value)
            {

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

            if (ConfigStages.Stage_5_Sky.Value)
            {
                //Add Drone Scrapper
                dccsSkyMeadowInteractables.AddCard(6, new DirectorCard
                {
                    spawnCardReference = new AssetReferenceT<SpawnCard>("d7e78d150bd132744934165e6471f5f6"),
                    selectionWeight = 8,
                });

                //Replace Healing Drone with Emergency Drone
                dccsSkyMeadowInteractables.categories[3].cards[1].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "36f38da2d5e04e44abb4f5ed9788bad9").WaitForCompletion();
            }
            if (ConfigStages.Stage_5_Helminth.Value)
            {
                //Replace Healing Drone with Emergency Drone
                //dccsHelminthRoostInteractables_DLC2.categories[3].cards[1].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "36f38da2d5e04e44abb4f5ed9788bad9").WaitForCompletion();

                //Add Drone Scrapper
                dccsHelminthRoostInteractables_DLC2.AddCard(5, new DirectorCard
                {
                    spawnCardReference = new AssetReferenceT<SpawnCard>("d7e78d150bd132744934165e6471f5f6"),
                    selectionWeight = 8,
                });
            }
        }


    }
}