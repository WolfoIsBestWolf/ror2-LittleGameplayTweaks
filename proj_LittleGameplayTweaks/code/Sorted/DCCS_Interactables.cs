using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using WolfoFixes;

namespace LittleGameplayTweaks
{  
    public class DCCS_Interactables
    {
        public static bool VanillaVoidsInstalled = false;
        public static void Start()
        {
            VanillaVoidsInstalled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Zenithrium.vanillaVoid");
            ;
            if (WConfig.cfgCredits_Interactables.Value == true)
            {

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/DeepVoidPortalBattery/iscDeepVoidPortalBattery.asset").WaitForCompletion().directorCreditCost = 0; //Idk if this cost matters but it should be 0

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/DuplicatorWild/iscDuplicatorWild.asset").WaitForCompletion().directorCreditCost = 15; //10 Default
                
                Addressables.LoadAssetAsync<SpawnCard>(key: "81e6491f830f9c143bb5954640a383b1").WaitForCompletion().directorCreditCost = 5; //10 iscBrokenTurret1 <3

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidTriple/iscVoidTriple.asset").WaitForCompletion().directorCreditCost = 30; //iscVoidTriple

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Chest1StealthedVariant/iscChest1Stealthed.asset").WaitForCompletion().directorCreditCost = 4; //iscChest1Stealthed

                Addressables.LoadAssetAsync<SpawnCard>(key: "49eb4eedc03a0e746a643c3b6051bfc4").WaitForCompletion().directorCreditCost = 10; //iscVoidCoinBarrel
 
                Addressables.LoadAssetAsync<SpawnCard>(key: "d21f2d3075f064e4081a41a368c505b1").WaitForCompletion().directorCreditCost = 15; //iscLunarChest
 
                Addressables.LoadAssetAsync<SpawnCard>(key: "36f38da2d5e04e44abb4f5ed9788bad9").WaitForCompletion().directorCreditCost = 25; //iscBrokenEmergencyDrone
                Addressables.LoadAssetAsync<SpawnCard>(key: "1439f6d216991ee469049c5ab7aff52e").WaitForCompletion().directorCreditCost = 35; //iscBrokenMegaDrone
                Addressables.LoadAssetAsync<SpawnCard>(key: "caab08f30f159b54f92e7d42b4b1d717").WaitForCompletion().directorCreditCost = 4; //iscShrineHealing
 
                //Order
                Addressables.LoadAssetAsync<SpawnCard>(key: "ba9d25d63bbcef34a9077c08a6d6df95").WaitForCompletion().directorCreditCost = 3; //iscShrineRestack
                Addressables.LoadAssetAsync<SpawnCard>(key: "3547e84f7f2c8064ba91cc54e517f5b9").WaitForCompletion().directorCreditCost = 3; //iscShrineRestack
                Addressables.LoadAssetAsync<SpawnCard>(key: "0e981358f6bf4de4e83e30286ad5df75").WaitForCompletion().directorCreditCost = 3; //iscShrineRestack
                //Blood
                Addressables.LoadAssetAsync<SpawnCard>(key: "a6d01afb758a15940bf09deb9db44067").WaitForCompletion().directorCreditCost = 15;//iscShrineBlood
                Addressables.LoadAssetAsync<SpawnCard>(key: "94a96af94cc91294fab616f523ce58b5").WaitForCompletion().directorCreditCost = 15;//iscShrineBlood
                Addressables.LoadAssetAsync<SpawnCard>(key: "3b3c5b543ce972e4d963cdfeafdc955f").WaitForCompletion().directorCreditCost = 15;//iscShrineBlood
                //Combat
                Addressables.LoadAssetAsync<SpawnCard>(key: "f48d76e496db90b44ac25782ce35528a").WaitForCompletion().directorCreditCost = 15;//iscShrineCombat
                Addressables.LoadAssetAsync<SpawnCard>(key: "7a94cc7c833cfec4eab51b41cd999619").WaitForCompletion().directorCreditCost = 15;//iscShrineCombat
                Addressables.LoadAssetAsync<SpawnCard>(key: "06bead17ed54dac49bfe3d01181c9466").WaitForCompletion().directorCreditCost = 15;//iscShrineCombat
                //Shaping
                Addressables.LoadAssetAsync<SpawnCard>(key: "9427ecf1e2e9c184ea39c9c30788aeab").WaitForCompletion().directorCreditCost = 40; //iscShrineColossusAccess
           

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
          
            SceneDirector.onGenerateInteractableCardSelection += SceneDirector_onGenerateInteractableCardSelection;
            On.RoR2.SceneDirector.GenerateInteractableCardSelection += SceneDirector_GenerateInteractableCardSelection;
        }

        private static WeightedSelection<DirectorCard> SceneDirector_GenerateInteractableCardSelection(On.RoR2.SceneDirector.orig_GenerateInteractableCardSelection orig, SceneDirector self)
        {   
            if (self.interactableCredit == 0)
            {
                return orig(self);
            }
            if (WConfig.cfgShrineBossMult.Value != 1)
            {
                var selection = orig(self);
                for (int i = 0; i < selection.Count; i++)
                {
                    //Debug.Log(selection.choices[i].value.spawnCard);
                    if (selection.choices[i].value.spawnCard.prefab.GetComponent<ShrineBossBehavior>())
                    {
                        float mult = 1+((WConfig.cfgShrineBossMult.Value - 1) / Run.instance.participatingPlayerCount);
                        selection.choices[i].weight *= mult;
                    }
                }
                return selection;
            }
            return orig(self);
        }

        public static void InteractableDCCS_Changes(DirectorCardCategorySelection dccs)
        {
            //Debug.Log("LGT_DCCS");
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
            try
            {
                DirectorCardCategorySelection.Category category;
                if (chestIndex != -1)
                {
                    category = dccs.categories[chestIndex];
                    for (int card = 0; card < category.cards.Length; card++)
                    {
                        if (category.cards[card].spawnCard.name.StartsWith("iscCategoryChest2"))
                        {
                            if (SceneInfo.instance.sceneDef.baseSceneName != "helminthroost")
                            {
                                category.cards[card].selectionWeight = 9;
                            }
                            
                        }
                        else if (category.cards[card].spawnCard.name == "iscCategoryChestDamage")
                        {
                            MatchCategoryChestsToLarge(category, card);
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
                    for (int card = 0; card < category.cards.Length; card++)
                    {
                        if (category.cards[card].spawnCard.name.EndsWith("Large"))
                        {
                            //Intended for Solo idk bout changing it specifically in solo ig
                            category.cards[card].selectionWeight = 7;
                        }
                        else if (category.cards[card].spawnCard.name.EndsWith("Military"))
                        {
                            category.cards[card].selectionWeight = 2;
                            category.cards[card].minimumStageCompletions = 3;
                        }
                        /*else if (category.cards[card].spawnCard.name.EndsWith("Wild"))
                        {
                            category.cards[card].minimumStageCompletions = 2;
                        }*/
                    }
                }
                if (rareIndex != -1)
                { 
                    //Slightly more Rare for fun
                    category = dccs.categories[rareIndex];
                    dccs.categories[rareIndex].selectionWeight += 0.1f;
                }
                if (voidIndex != -1)
                {
                    //Slightly more void for fun
                    //But good chunk more for VV
                    if (VanillaVoidsInstalled)
                    {
                        dccs.categories[voidIndex].selectionWeight +=1f;
                    }
                    /*else
                    {
                        dccs.categories[voidIndex].selectionWeight += 0.5f;
                    }*/     
                }
                if (stormIndex != -1)
                {
                    category = dccs.categories[stormIndex];
                    for (int card = 0; card < category.cards.Length; card++)
                    {
                        if (category.cards[card].spawnCard.name.EndsWith("ess"))
                        {
                            //Revive Shrine not on Stage 2
                            category.cards[card].minimumStageCompletions = 2;
                            break;
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("REPORT THIS");
                Debug.LogWarning(e);
            }
        }

        public static void MatchCategoryChestsToLarge(DirectorCardCategorySelection.Category dccs, int card)
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



        }
       

        public static void ClassicStageInfo_RebuildCards(On.RoR2.ClassicStageInfo.orig_RebuildCards orig, ClassicStageInfo self, DirectorCardCategorySelection forcedMonsterCategory, DirectorCardCategorySelection forcedInteractableCategory)
        {
            orig(self, forcedMonsterCategory, forcedInteractableCategory);
            if (NetworkServer.active && forcedInteractableCategory == null && self.sceneDirectorInteractibleCredits > 0)
            {
                DoThing = true;
            }
            else
            {
                DoThing = false;
            }
        }
        public static bool DoThing = false;
        private static void SceneDirector_onGenerateInteractableCardSelection(SceneDirector scene, DirectorCardCategorySelection dccs)
        {
            if (DoThing)
            {
                InteractableDCCS_Changes(dccs);
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


        public static void Interactables_Other()
        {


            DirectorCardCategorySelection dccsInfiniteTowerInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dccsInfiniteTowerInteractables.asset").WaitForCompletion();
            dccsInfiniteTowerInteractables.categories[2].selectionWeight += 0.1f;

            DirectorCardCategorySelection dccsGoldshoresInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goldshores/dccsGoldshoresInteractables.asset").WaitForCompletion();
            DirectorCard ADShrineCleanse1 = new DirectorCard
            {
                //spawnCardReference = new AssetReferenceT<SpawnCard>("RoR2/Base/ShrineCleanse/iscShrineCleanse.asset"),
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                selectionWeight = 5,
            };
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
                cards = new DirectorCard[] { AdShrineCombat, ShrineBlood, ADShrineCleanse1, ADShrineHealing , ADBARREL }
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
                dccsGolemplainsInteractables.AddCard(3, loopTC280);
            }
 
            if (ConfigStages.Stage_1_Roost.Value)
            {
                dccsBlackBeachInteractables.AddCard(2, new DirectorCard //Cleanse10
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    minimumStageCompletions = 2,
                });
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
                dccsLakesInteractables_DLC2.categories[0].cards[0].selectionWeight = 1; //Cleansing Shrine

                DCCS.MultWholeCateory(dccsLakesInteractables, 0, 10);
                int chests = dccsLakesInteractablesDLC1.AddCategory("Chests", 45);
                dccsLakesInteractablesDLC1.AddCard(chests, iscCategoryChest2Utility);

                dccsLakesnightInteractables_DLC2.AddCard(3, loopTC280);
                DCCS.MultWholeCateory(dccsLakesnightInteractables_DLC2, 0, 10);
                chests = dccsLakesnightInteractables_DLC1.AddCategory("Chests", 45);
                dccsLakesnightInteractables_DLC1.AddCard(chests, iscCategoryChest2Utility);
          
            }
            if (ConfigStages.Stage_1_Village.Value)
            {
                dccsVillageInteractables_DLC2.AddCard(2, new DirectorCard //Default uses x10 weights
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion(),
                    selectionWeight = 10,
                });
               
                DCCS.MultWholeCateory(dccsVillageInteractables_DLC2, 0, 10);
                int chests = dccsVillageInteractablesDLC1.AddCategory("Chests", 45);
               
                dccsVillageInteractablesDLC1.AddCard(chests, iscCategoryChest2Healing);

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
 
            #endregion

            if (ConfigStages.Stage_2_Swamp.Value)
            {
                dccsFoggySwampInteractables.categories[2].cards[3].selectionWeight = 13;
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
                int chests = dccsLemurianTempleInteractables_DLC1.AddCategory("Chests", 45);
                dccsLemurianTempleInteractables_DLC1.AddCard(chests, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                    selectionWeight = 3,
                });
                dccsLemurianTempleInteractables_DLC1.AddCard(chests, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Healing.asset").WaitForCompletion(),
                    selectionWeight = 3,
                });
                dccsLemurianTempleInteractables_DLC1.AddCard(chests, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Utility.asset").WaitForCompletion(),
                    selectionWeight = 3,
                });
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
 
            #endregion

            if (ConfigStages.Stage_3_Wisp.Value)
            {
                dccsWispGraveyardInteractables.categories[2].cards[3].selectionWeight = 14; //Cleanse more?
            }
            if (ConfigStages.Stage_3_Sulfur.Value)
            {
                dccsSulfurPoolsInteractablesDLC1.AddCard(2, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBoss/iscShrineBoss.asset").WaitForCompletion(),
                    selectionWeight = 15,
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
                int chests = dccsHabitatInteractables_DLC1.AddCategory("Chests", 45);
                dccsHabitatInteractables_DLC1.AddCard(chests, iscCategoryChest2Damage);

                dccsHabitatInteractables_DLC2.categories[3].selectionWeight = 5;
                dccsHabitatInteractables_DLC2.AddCard(3, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenEmergencyDrone"),
                    selectionWeight = 2,
                });
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
                dccsDampCaveInteractables.categories[3].cards[3].selectionWeight = 7; //Flame Drone
                DCCS.RemoveCard(dccsDampCaveInteractables, 4, 0);//Gunner Turret Removal
            }
            if (ConfigStages.Stage_4_Ship.Value)
            {
                dccsShipgraveyardInteractables.AddCard(3, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenMegaDrone"),
                    selectionWeight = 1,
                });
                /*dccsShipgraveyardInteractables.AddCard(1, new DirectorCard
                {
                    spawnCardReference = new AssetReferenceT<SpawnCard>("5e1909bcf4fbea34992ef53b26515678"),
                    selectionWeight = 10,
                });*/
                DCCS.RemoveCard(dccsShipgraveyardInteractables, 4, 0);//Gunner Turret Removal
                dccsShipgraveyardInteractables.categories[2].cards[3].selectionWeight = 12; //Cleanse more
                dccsShipgraveyardInteractables.categories[3].cards[0].selectionWeight = 4; //Missile
                dccsShipgraveyardInteractables.categories[3].cards[1].selectionWeight = 4; //Heal
                dccsShipgraveyardInteractables.categories[3].cards[2].selectionWeight = 3; //Eq
                
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
 
            if (ConfigStages.Stage_5_Sky.Value)
            {
                //Emergency stage 5
                dccsSkyMeadowInteractables.categories[3].cards[1].spawnCardReference = new AssetReferenceT<SpawnCard>("36f38da2d5e04e44abb4f5ed9788bad9");

                DCCS.RemoveCard(dccsSkyMeadowInteractables, 4, 0); //Gunner Turret Removal
            }
             if (ConfigStages.Stage_5_Helminth.Value)
            {
                dccsHelminthRoostInteractables_DLC2.categories[3].cards[1].spawnCardReference = new AssetReferenceT<SpawnCard>("36f38da2d5e04e44abb4f5ed9788bad9");

                DCCS.RemoveCard(dccsHelminthRoostInteractables_DLC2, 7, 0); //Gunner Turret Removal
            }
        }

 
    }
}