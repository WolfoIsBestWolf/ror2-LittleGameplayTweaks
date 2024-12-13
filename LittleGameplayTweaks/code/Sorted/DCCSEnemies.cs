using RoR2;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LittleGameplayTweaks
{
    public class DCCSEnemies
    {
        public static void Start()
        {
            if (WConfig.DCCSEnemyNewFamilies.Value == true)
            {
                FamilyEvents.Families();
            }
            if (WConfig.SulfurPoolsSkin.Value == true)
            {
                SulfurPoolsBeetle();
            }

            //I should do this better
            if (WConfig.cscGreaterWispCredits.Value)
            {
                Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/GreaterWisp/cscGreaterWisp.asset").WaitForCompletion().directorCreditCost -= 40;
            }
        }

        public static void SulfurPoolsBeetle()
        {
            DirectorCardCategorySelection dccsSulfurPoolsMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsMonstersDLC1.asset").WaitForCompletion();

            int num = -1;
            num = FindSpawnCard(dccsSulfurPoolsMonstersDLC1.categories[2].cards, "Beetle");
            if (num > -1)
            {
                dccsSulfurPoolsMonstersDLC1.categories[2].cards[num].spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Beetle/cscBeetleSulfur.asset").WaitForCompletion();
            }
            num = FindSpawnCard(dccsSulfurPoolsMonstersDLC1.categories[1].cards, "BeetleGuard");
            if (num > -1)
            {
                dccsSulfurPoolsMonstersDLC1.categories[1].cards[num].spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Beetle/cscBeetleGuardSulfur.asset").WaitForCompletion();
            }
            num = FindSpawnCard(dccsSulfurPoolsMonstersDLC1.categories[0].cards, "BeetleQueen");
            if (num > -1)
            {
                dccsSulfurPoolsMonstersDLC1.categories[0].cards[num].spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Beetle/cscBeetleQueenSulfur.asset").WaitForCompletion();
            }
            On.EntityStates.BeetleQueenMonster.SummonEggs.OnEnter += FixSulfurPoolsBeetleQueen;
        }


       

        public static void EnemiesPreLoop_NoDLC()
        {
            DirectorCardCategorySelection dccsFrozenWallMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleMonsters.asset").WaitForCompletion();

            DirectorCard DC_Grovetender = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),

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

            if (ConfigStages.Stage_3_Frozen.Value)
            {
                dccsFrozenWallMonsters.categories[0].cards[0] = DC_RoboBallBoss; //Clay Dunestrider replaced by Solus Unit
            }
            if (ConfigStages.Stage_4_Root_Jungle.Value)
            {
                dccsRootJungleMonsters.categories[0].cards[1] = DC_Grovetender; //Titan replaced by Grovetender
            }

        }

        public static void EnemiesPreLoop_DLC1()
        {
            //DirectorCardCategorySelection dccsGolemplainsMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsMonstersDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsBlackBeachMonstersDLC = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachMonstersDLC.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSnowyForestMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/snowyforest/dccsSnowyForestMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsGooLakeMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeMonstersDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsFoggySwampMonstersDLC = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampMonstersDLC.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsAncientLoftMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/ancientloft/dccsAncientLoftMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFrozenWallMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSulfurPoolsMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsMonstersDLC1.asset").WaitForCompletion();

            //DirectorCardCategorySelection dccsDampCaveMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSkyMeadowMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowMonstersDLC1.asset").WaitForCompletion();

            DirectorCard DC_MagmaWorm = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DC_ElectricWorm = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DC_Grovetender = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),
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
            DirectorCard DC_HermitCrab = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            };
            DirectorCard DC_AcidLarva = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscAcidLarva"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            };

            DirectorCard DC_ClayGrenadier = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscScorchling = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/Scorchling/cscScorchling.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            int num = -1;


            if (ConfigStages.Stage_1_Snow.Value)
            {
                num = FindSpawnCard(dccsSnowyForestMonstersDLC1.categories[1].cards, "GreaterWisp");
                if (num > -1)
                {
                    dccsSnowyForestMonstersDLC1.categories[1].cards[num].spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBison");
                    //Bison replaces Greater Wisp
                }
                num = FindSpawnCard(dccsSnowyForestMonstersDLC1.categories[2].cards, "cscVerminSnowy");
                if (num > -1)
                {
                    dccsSnowyForestMonstersDLC1.categories[2].cards[num].minimumStageCompletions = 0;
                    //Pre Loop Vermin
                }
                /*num = FindSpawnCard(dccsSnowyForestMonstersDLC1.categories[2].cards, "LesserWisp");
                if (num > -1)
                {
                    dccsSnowyForestMonstersDLC1.categories[2].cards = dccsSnowyForestMonstersDLC1.categories[2].cards.Remove(dccsSnowyForestMonstersDLC1.categories[2].cards[num]); //Remove Wisp
                }*/
            }


            if (ConfigStages.Stage_2_Goolake.Value)
            {
                num = FindSpawnCard(dccsGooLakeMonstersDLC1.categories[1].cards, "ClayGrenadier");
                if (num > -1)
                {
                    dccsGooLakeMonstersDLC1.categories[1].cards[num].minimumStageCompletions = 0;
                    //Pre Loop Clay Grenadier
                }
            }
            if (ConfigStages.Stage_3_Frozen.Value)
            {
                dccsFrozenWallMonstersDLC1.categories[0].cards[0] = DC_RoboBallBoss; //Clay Dunestrider replaced by Solus Unit
            }
            if (ConfigStages.Stage_3_Wisp.Value)
            {
                dccsWispGraveyardMonstersDLC1.AddCard(1, DC_ClayGrenadier);
            }
            if (ConfigStages.Stage_3_Sulfur.Value)
            {
                num = FindSpawnCard(dccsSulfurPoolsMonstersDLC1.categories[0].cards, "MegaConstruct");
                if (num > -1)
                {
                    dccsSulfurPoolsMonstersDLC1.categories[0].cards[num].selectionWeight++;
                    //More Lamp because there's so many bosses here
                }

                dccsSulfurPoolsMonstersDLC1.AddCard(0, DC_MagmaWorm); //Keep Beetle Queen but still add this dude
                dccsSulfurPoolsMonstersDLC1.AddCard(0, DC_ElectricWorm); //Keep Beetle Queen but still add this dude
                dccsSulfurPoolsMonstersDLC1.AddCard(1, DC_HermitCrab); //Seems fitting for another trash mob
                dccsSulfurPoolsMonstersDLC1.AddCard(1, DC_AcidLarva); //Seems fitting for another trash mob
                dccsSulfurPoolsMonstersDLC1.AddCard(1, cscScorchling); 
            }
            if (ConfigStages.Stage_4_Root_Jungle.Value)
            {
                num = FindSpawnCard(dccsRootJungleMonstersDLC1.categories[0].cards, "TitanBlackBeach");
                if (num > -1)
                {
                    dccsRootJungleMonstersDLC1.categories[0].cards[num].spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper");
                    dccsRootJungleMonstersDLC1.categories[0].cards[num].selectionWeight++;
                    //Replacing Titan with Grovetender
                }
                num = FindSpawnCard(dccsRootJungleMonstersDLC1.categories[1].cards, "Golem");
                if (num > -1)
                {
                    dccsRootJungleMonstersDLC1.categories[1].cards[num].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Golem/cscGolemNature.asset").WaitForCompletion();
                    //Replacing Golem with skinned Golem
                }
            }
            if (ConfigStages.Stage_4_Ship.Value)
            {
                dccsShipgraveyardMonstersDLC1.AddCard(0, DC_ElectricWorm); //Magma Worm so Elec Worm
            }
            if (ConfigStages.Stage_5_Sky.Value)
            {
                dccsSkyMeadowMonstersDLC1.AddCard(0, DC_MagmaWorm); //They removed him with the DLC but Elec Worm is still there
            }



        }

        public static void EnemiesPreLoop_DLC2()
        {
            //DirectorCardCategorySelection dccsGolemplainsMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGolemplainsMonstersDLC2.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsBlackBeachMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsBlackBeachMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesMonstersDLC2.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsLakesnightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightMonsters.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsVillageMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageMonsters_DLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsVillageNightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillageNightMonsters.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsGooLakeMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGooLakeMonstersDLC2.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsFoggySwampMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFoggySwampMonstersDLC2.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsAncientLoftMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsAncientLoftMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLemurianTempleMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lemuriantemple/dccsLemurianTempleMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFrozenWallMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFrozenWallMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsWispGraveyardMonstersDLC2.asset").WaitForCompletion();
            //DOES_NOT_EXIST//DirectorCardCategorySelection dccsSulfurPoolsMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/sulfurpools/dccsSulfurPoolsMonstersDLC2.asset").WaitForCompletion();
            
            
            DirectorCardCategorySelection dccsHabitatMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitat/dccsHabitatMonsters_DLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatfallMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitatfall/dccsHabitatfallMonsters_DLC1.asset").WaitForCompletion();

            //DirectorCardCategorySelection dccsDampCaveMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsDampCaveMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShipgraveyardMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsRootJungleMonstersDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSkyMeadowMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSkyMeadowMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHelminthRoostMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/helminthroost/dccsHelminthRoostMonsters.asset").WaitForCompletion();


            DirectorCard cscBison = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBison"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_Grandparent = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Grandparent/cscGrandparent.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_BlindPest = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/FlyingVermin/cscFlyingVermin.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_Mushroom = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/MiniMushroom/cscMiniMushroom.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_MagmaWorm = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DC_ElectricWorm = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DC_Grovetender = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_Grovetender1 = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),
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
            DirectorCard DC_HermitCrab = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            };
            DirectorCard DC_AcidLarva = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscAcidLarva"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };

            DirectorCard DC_ClayGrenadier = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscScorchling = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/Scorchling/cscScorchling.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            int num = -1;

            //dccsGolemplainsMonstersDLC2

            //Snowy Forest
            if (ConfigStages.Stage_1_Snow.Value)
            {
                DirectorCardCategorySelection dccsSnowyForestMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSnowyForestMonstersDLC2.asset").WaitForCompletion();
                num = FindSpawnCard(dccsSnowyForestMonstersDLC2.categories[1].cards, "GreaterWisp");
                if (num > -1)
                {
                    dccsSnowyForestMonstersDLC2.categories[1].cards[num].spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBison");
                    dccsSnowyForestMonstersDLC2.categories[1].cards[num].selectionWeight++;
                    //Bison replaces Greater Wisp
                }
                num = FindSpawnCard(dccsSnowyForestMonstersDLC2.categories[2].cards, "VerminSnowy");
                if (num > -1)
                {
                    dccsSnowyForestMonstersDLC2.categories[2].cards[num].minimumStageCompletions = 0;
                    //Pre Loop Vermin
                }
                /*num = FindSpawnCard(dccsSnowyForestMonstersDLC2.categories[2].cards, "LesserWisp");
                if (num > -1)
                {
                    dccsSnowyForestMonstersDLC2.categories[2].cards = dccsSnowyForestMonstersDLC2.categories[2].cards.Remove(dccsSnowyForestMonstersDLC2.categories[2].cards[num]); //Remove Wisp
                }*/
            }

            if (ConfigStages.Stage_1_Lake.Value)
            {
                if (dccsLakesMonstersDLC2.categories[2].cards.Length >= 6)
                {
                    DirectorCard cscJellyfish = new DirectorCard
                    {
                        spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Jellyfish/cscJellyfish.asset").WaitForCompletion(),
                        selectionWeight = 2,
                        preventOverhead = true,
                        minimumStageCompletions = 0,
                        spawnDistance = DirectorCore.MonsterSpawnDistance.Far
                    };

                    dccsLakesMonstersDLC2.categories[2].cards[0].selectionWeight--;
                    dccsLakesMonstersDLC2.categories[2].cards[1] = cscJellyfish;
                    dccsLakesMonstersDLC2.categories[2].cards[2].selectionWeight--;
                    dccsLakesMonstersDLC2.categories[2].cards[3].selectionWeight++;
                    dccsLakesMonstersDLC2.categories[2].cards[4].selectionWeight++;
                    dccsLakesMonstersDLC2.categories[2].cards[5].selectionWeight++;
                }
                else
                {
                    Debug.LogWarning("dccsLakes too short");
                }
            }
            if (ConfigStages.Stage_2_Goolake.Value)
            {
                dccsGooLakeMonstersDLC2.AddCard(1, DC_ClayGrenadier);
            }
            if (ConfigStages.Stage_3_Frozen.Value)
            {
                dccsFrozenWallMonstersDLC2.categories[0].cards[0] = DC_RoboBallBoss; //Clay Dunestrider replaced by Solus Unit
            }
            if (ConfigStages.Stage_3_Wisp.Value)
            {
                dccsWispGraveyardMonstersDLC2.AddCard(1, DC_ClayGrenadier);
            }
            if (ConfigStages.Stage_3_Sulfur.Value)
            {
                DirectorCardCategorySelection dccsSulfurPoolsMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsMonstersDLC1.asset").WaitForCompletion();
                num = FindSpawnCard(dccsSulfurPoolsMonstersDLC1.categories[0].cards, "MegaConstruct");
                if (num > -1)
                {
                    dccsSulfurPoolsMonstersDLC1.categories[0].cards[num].selectionWeight++;
                    //More Lamp because there's so many bosses here
                }

                dccsSulfurPoolsMonstersDLC1.AddCard(0, DC_MagmaWorm); //Keep Beetle Queen but still add this dude
                dccsSulfurPoolsMonstersDLC1.AddCard(0, DC_ElectricWorm); //Keep Beetle Queen but still add this dude
                dccsSulfurPoolsMonstersDLC1.AddCard(1, DC_HermitCrab); //Seems fitting for another trash mob
                dccsSulfurPoolsMonstersDLC1.AddCard(1, DC_AcidLarva); //Seems fitting for another trash mob
                dccsSulfurPoolsMonstersDLC1.AddCard(1, cscScorchling);
            }
            if (ConfigStages.Stage_4_Root_Jungle.Value)
            {
                num = FindSpawnCard(dccsRootJungleMonstersDLC2.categories[0].cards, "TitanBlackBeach");
                if (num > -1)
                {
                    dccsRootJungleMonstersDLC2.categories[0].cards[num].spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper");
                    dccsRootJungleMonstersDLC2.categories[0].cards[num].selectionWeight++;
                    //Replacing Titan with Grovetender
                }
                num = FindSpawnCard(dccsRootJungleMonstersDLC2.categories[1].cards, "Golem");
                if (num > -1)
                {
                    dccsRootJungleMonstersDLC2.categories[1].cards[num].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Golem/cscGolemNature.asset").WaitForCompletion();
                    //Replacing Golem with skinned Golem
                }
            }
            if (ConfigStages.Stage_4_Ship.Value)
            {
                dccsShipgraveyardMonstersDLC2.AddCard(0, DC_ElectricWorm); //Magma Worm so Elec Worm
            }
            if (ConfigStages.Stage_5_Sky.Value)
            {
                dccsSkyMeadowMonstersDLC2.AddCard(0, DC_MagmaWorm); //They removed him with the DLC but Elec Worm is still there
            }


            if (ConfigStages.Stage_2_Temple.Value)
            {
                dccsLemurianTempleMonstersDLC1.AddCard(0, DC_Grovetender1);

                num = FindSpawnCard(dccsLemurianTempleMonstersDLC1.categories[1].cards, "FlyingVermin");
                if (num > -1)
                {
                    dccsLemurianTempleMonstersDLC1.categories[1].cards = dccsLemurianTempleMonstersDLC1.categories[1].cards.Remove(dccsLemurianTempleMonstersDLC1.categories[1].cards[num]);
                }
                dccsLemurianTempleMonstersDLC1.AddCard(1, cscBison);
                dccsLemurianTempleMonstersDLC1.categories[1].selectionWeight = 4;

                if (dccsLemurianTempleMonstersDLC1.categories[2].cards.Length >= 5)
                {
                    dccsLemurianTempleMonstersDLC1.categories[2].cards[3].selectionWeight++; //Wurm
                    dccsLemurianTempleMonstersDLC1.categories[2].cards[4].selectionWeight++; //Vermin
                }
            }
            if (ConfigStages.Stage_3_Tree.Value)
            {
                dccsHabitatMonsters_DLC1.AddCard(2, DC_BlindPest);
                dccsHabitatfallMonsters_DLC1.AddCard(2, DC_BlindPest);

                dccsHabitatMonsters_DLC1.categories[0].cards[2] = DC_Grovetender1;
                dccsHabitatfallMonsters_DLC1.AddCard(0, DC_Grovetender1);

                dccsHabitatfallMonsters_DLC1.AddCard(2, DC_Mushroom);
            }
            if (ConfigStages.Stage_5_Helminth.Value)
            {
                num = FindSpawnCard(dccsHelminthRoostMonsters.categories[1].cards, "corchling");
                if (num > -1)
                {
                    dccsHelminthRoostMonsters.categories[1].cards[num].selectionWeight++;
                }

                dccsHelminthRoostMonsters.AddCard(0, DC_Grandparent);
            }








        }
        //
        //
        //
        public static void EnemiesPostLoop_DLC1()
        {
            //DirectorCardCategorySelection dccsGolemplainsMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsBlackBeachMonstersDLC = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachMonstersDLC.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSnowyForestMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/snowyforest/dccsSnowyForestMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsGooLakeMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFoggySwampMonstersDLC = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampMonstersDLC.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsAncientLoftMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/ancientloft/dccsAncientLoftMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFrozenWallMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSulfurPoolsMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsDampCaveMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSkyMeadowMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowMonstersDLC1.asset").WaitForCompletion();

            DirectorCard LoopVulture = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopImp = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImp"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard LoopImpBoss = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImpBoss"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopMiniMushroom = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMiniMushroom"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            DirectorCard LoopElderLemurian = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopBlindPest = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVermin"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };



            DirectorCard LoopParent = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscParent"),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopGrandparent = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 2,
                preventOverhead = false,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            DirectorCard LoopAcidLarva = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscAcidLarva"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard LoopLunarExploder = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarGolem = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarWisp = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopRoboBallMini = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            /*DirectorCard DoubleLoopVoidBarnacle = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidBarnacle/cscVoidBarnacle.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 8,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };*/
            DirectorCard DoubleLoopVoidReaver = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Nullifier/cscNullifier.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DoubleLoopVoidJailer = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DoubleLoopVoidDevestator = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopGrovetender = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Gravekeeper/cscGravekeeper.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard SimuLoopMinorConstruct = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/MajorAndMinorConstruct/cscMinorConstruct.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopMegaConstruct = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/MajorAndMinorConstruct/cscMegaConstruct.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };




            //Golem Plains already has Lamps, Lamp Boss and Hermit Crab
            if (ConfigStages.Stage_1_Roost.Value)
            {
                dccsBlackBeachMonstersDLC.AddCard(2, LoopVulture);
            }
            if (ConfigStages.Stage_1_Snow.Value)
            {
                dccsSnowyForestMonstersDLC1.AddCard(0, LoopImpBoss); //Loop Imps
                dccsSnowyForestMonstersDLC1.AddCard(2, LoopImp); //I don't remember why maybe because the other snow area has Imps?
            }

            if (ConfigStages.Stage_2_Goolake.Value)
            {
                //Goolake already has Templar

            }
            if (ConfigStages.Stage_2_Swamp.Value)
            {
                dccsFoggySwampMonstersDLC.AddCard(2, LoopAcidLarva);
                dccsFoggySwampMonstersDLC.AddCard(2, LoopMiniMushroom);  //Mushroom
            }
            if (ConfigStages.Stage_2_Ancient.Value)
            {
                dccsAncientLoftMonstersDLC1.AddCard(0, LoopGrovetender);
                dccsAncientLoftMonstersDLC1.AddCard(1, LoopElderLemurian); //Loop Elder Lemurian
            }
            if (ConfigStages.Stage_3_Frozen.Value)
            {
                dccsFrozenWallMonstersDLC1.AddCard(2, LoopVulture); //Like the Big Ball leads them to it so they can eat more People and Iron
                dccsFrozenWallMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);
            }
            if (ConfigStages.Stage_3_Wisp.Value)
            {
                int num = FindSpawnCard(dccsWispGraveyardMonstersDLC1.categories[2].cards, "Vulture");
                if (num > -1)
                {
                    dccsWispGraveyardMonstersDLC1.categories[2].cards[num].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/Vermin/cscVermin.asset").WaitForCompletion();
                    dccsWispGraveyardMonstersDLC1.categories[2].cards[num].selectionWeight = 3;
                }
            }
            if (ConfigStages.Stage_3_Sulfur.Value)
            {
                dccsSulfurPoolsMonstersDLC1.AddCard(0, LoopGrandparent); //Loop Parents
                dccsSulfurPoolsMonstersDLC1.AddCard(2, LoopParent); //Idk Yellow on Yellow action or smth
                dccsSulfurPoolsMonstersDLC1.AddCard(2, LoopLunarExploder);
            }
            if (ConfigStages.Stage_4_Damp_Abyss.Value)
            {
                dccsDampCaveMonstersDLC1.AddCard(0, LoopGrandparent); //Loop Parents
                dccsDampCaveMonstersDLC1.AddCard(1, LoopParent); //Yeah I got nothin I just wanted at least 3 Parents
                dccsDampCaveMonstersDLC1.AddCard(0, DoubleLoopVoidJailer);
            }
            if (ConfigStages.Stage_4_Ship.Value)
            {
                dccsShipgraveyardMonstersDLC1.AddCard(1, LoopRoboBallMini); //Sure
                dccsShipgraveyardMonstersDLC1.AddCard(0, SimuLoopMegaConstruct); //Lamps look good I think
                dccsShipgraveyardMonstersDLC1.AddCard(2, SimuLoopMinorConstruct); //
                dccsShipgraveyardMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);
            }
            if (ConfigStages.Stage_4_Root_Jungle.Value)
            {
                dccsRootJungleMonstersDLC1.AddCard(2, LoopBlindPest); //Hell yeah
                dccsRootJungleMonstersDLC1.AddCard(1, DoubleLoopVoidJailer);
            }
            if (ConfigStages.Stage_5_Sky.Value)
            {
                dccsSkyMeadowMonstersDLC1.AddCard(2, LoopLunarWisp); //If you loop you wouldn't really see these enemies so I think seeing them more here is nice
                dccsSkyMeadowMonstersDLC1.AddCard(2, LoopLunarGolem);
                dccsSkyMeadowMonstersDLC1.AddCard(2, LoopLunarExploder);
                dccsSkyMeadowMonstersDLC1.AddCard(0, DoubleLoopVoidDevestator);
            }


        }

        public static void EnemiesPostLoop_DLC2()
        {
            DirectorCardCategorySelection dccsGolemplainsMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGolemplainsMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsBlackBeachMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsBlackBeachMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSnowyForestMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSnowyForestMonstersDLC2.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsLakesMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesnightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightMonsters.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsVillageMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageMonsters_DLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillageNightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillageNightMonsters.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsGooLakeMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGooLakeMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFoggySwampMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFoggySwampMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsAncientLoftMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsAncientLoftMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLemurianTempleMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lemuriantemple/dccsLemurianTempleMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFrozenWallMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFrozenWallMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsWispGraveyardMonstersDLC2.asset").WaitForCompletion();
            //DOES_NOT_EXIST//DirectorCardCategorySelection dccsSulfurPoolsMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/sulfurpools/dccsSulfurPoolsMonstersDLC2.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsHabitatMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitat/dccsHabitatMonsters_DLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsHabitatfallMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitatfall/dccsHabitatfallMonsters_DLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsDampCaveMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsDampCaveMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShipgraveyardMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsRootJungleMonstersDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSkyMeadowMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSkyMeadowMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHelminthRoostMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/helminthroost/dccsHelminthRoostMonsters.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsMeridianMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/meridian/dccsMeridianMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsArenaMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/arena/dccsArenaMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVoidCampMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/VoidCamp/dccsVoidCampMonsters.asset").WaitForCompletion();

            DirectorCard cscBell = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Bell/cscBell.asset").WaitForCompletion(),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscHalcy = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/Halcyonite/cscHalcyonite.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscHalcy2 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/Halcyonite/cscHalcyonite.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 4,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscHalcy6 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/Halcyonite/cscHalcyonite.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 6,
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
            DirectorCard DC_ClayBoss = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBoss"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopVulture = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopVulture2 = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard LoopImp = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImp"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard LoopImpBoss = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImpBoss"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopMiniMushroom = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMiniMushroom"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            DirectorCard LoopElderLemurian = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser"),

                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopBlindPest = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVermin"),
                preventOverhead = true,
                selectionWeight = 3,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopPest = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVermin"),
                preventOverhead = true,
                selectionWeight = 3,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            DirectorCard DC_Child = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/Child/cscChild.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopParent = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscParent"),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopGrandparent = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 2,
                preventOverhead = false,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            DirectorCard LoopAcidLarva = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscAcidLarva"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard LoopLunarExploder = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarGolem = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarWisp = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopRoboBallMini = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DoubleLoopVoidBarnacle = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidBarnacle/cscVoidBarnacle.asset").WaitForCompletion(),
                selectionWeight = 3,
                preventOverhead = true,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DoubleLoopVoidReaver = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Nullifier/cscNullifier.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DoubleLoopVoidJailer = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard Damp_VoidJailer2 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion(),
                selectionWeight = 3,
                preventOverhead = true,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DoubleLoopVoidDevestator = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                selectionWeight = 3,
                preventOverhead = true,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard Damp_VoidDevestator = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                selectionWeight = 3,
                preventOverhead = true,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DoubleLoopVoidDevestator2 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                selectionWeight = 6,
                preventOverhead = true,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopGrovetender = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Gravekeeper/cscGravekeeper.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard SimuLoopMinorConstruct = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/MajorAndMinorConstruct/cscMinorConstruct.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopMegaConstruct = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/MajorAndMinorConstruct/cscMegaConstruct.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscRoboBallBoss = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/RoboBallBoss/cscRoboBallBoss.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscJellyfish = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Jellyfish/cscJellyfish.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard Loop_GreaterWisp = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/GreaterWisp/cscGreaterWisp.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_HermitCrab = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            };
            int num = -1;


            if (ConfigStages.Stage_1_Golem.Value)
            {
                num = FindSpawnCard(dccsGolemplainsMonstersDLC2.categories[0].cards, "MegaConstruct");
                if (num > -1)
                {
                    dccsGolemplainsMonstersDLC2.categories[2].cards[num].selectionWeight++;
                }
                else
                {
                    dccsGolemplainsMonstersDLC2.AddCard(0, SimuLoopMegaConstruct);

                }
                num = FindSpawnCard(dccsGolemplainsMonstersDLC2.categories[2].cards, "MinorConstruct");
                if (num > -1)
                {
                    dccsGolemplainsMonstersDLC2.categories[2].cards[num].selectionWeight++;
                }
                else
                {
                    dccsGolemplainsMonstersDLC2.AddCard(2, SimuLoopMinorConstruct);
                }
            }
            if (ConfigStages.Stage_1_Roost.Value)
            {
                dccsBlackBeachMonstersDLC2.AddCard(2, LoopVulture);
                //dccsBlackBeachMonstersDLC2.AddCard(0, LoopGrovetender);
            }

            if (ConfigStages.Stage_1_Snow.Value)
            {
                dccsSnowyForestMonstersDLC2.AddCard(0, LoopImpBoss); //Loop Imps
                dccsSnowyForestMonstersDLC2.AddCard(1, Loop_GreaterWisp); //Loop Imps
                dccsSnowyForestMonstersDLC2.AddCard(2, LoopImp); //I don't remember why maybe because the other snow area has Imps?
            }
            if (ConfigStages.Stage_1_Village.Value)
            {
                dccsVillageNightMonsters.AddCard(0, cscHalcy);
                dccsVillageNightMonsters.AddCard(1, LoopLunarGolem);
            }
            if (ConfigStages.Stage_2_Goolake.Value)
            {
                dccsGooLakeMonstersDLC2.AddCard(0, DC_ClayBoss);
                if (dccsGooLakeMonstersDLC2.categories[1].cards.Length >= 3)
                {
                    dccsGooLakeMonstersDLC2.categories[1].cards[2].selectionWeight++;
                    dccsGooLakeMonstersDLC2.categories[1].cards[3].selectionWeight++;
                }
                dccsGooLakeMonstersDLC2.AddCard(1, DC_HermitCrab);
            }
            if (ConfigStages.Stage_2_Swamp.Value)
            {
                dccsFoggySwampMonstersDLC2.AddCard(2, LoopAcidLarva); //Simu has Acid Larva here but I don't see how this fits
                dccsFoggySwampMonstersDLC2.AddCard(2, LoopMiniMushroom);  //Mushroom
            }
            if (ConfigStages.Stage_2_Ancient.Value)
            {
                dccsAncientLoftMonstersDLC2.AddCard(0, LoopGrovetender);
                dccsAncientLoftMonstersDLC2.AddCard(2, LoopElderLemurian); //Loop Elder Lemurian
                
            }
            if (ConfigStages.Stage_2_Temple.Value)
            {
                dccsLemurianTempleMonstersDLC1.AddCard(1, cscBell); //Loop Elder Lemurian
            }
            if (ConfigStages.Stage_3_Frozen.Value)
            {
                num = FindSpawnCard(dccsFrozenWallMonstersDLC2.categories[1].cards, "Scorchling");
                if (num > -1)
                {
                    dccsFrozenWallMonstersDLC2.categories[0].cards[num].minimumStageCompletions = 500;
                    dccsFrozenWallMonstersDLC2.categories[0].cards[num].selectionWeight--;
                }
                dccsFrozenWallMonstersDLC2.AddCard(1, DoubleLoopVoidReaver);
                dccsFrozenWallMonstersDLC2.AddCard(1, LoopRoboBallMini);
                dccsFrozenWallMonstersDLC2.AddCard(2, LoopVulture2); //Like the Big Ball leads them to it so they can eat more People and Iron
            }
            if (ConfigStages.Stage_3_Wisp.Value)
            {
                //dccsWispGraveyardMonstersDLC2.AddCard(1, LoopLunarWisp);
                //dccsWispGraveyardMonstersDLC2.AddCard(2, DC_Child);
                //ccsWispGraveyardMonstersDLC2.AddCard(2, Loop_GreaterWisp);
                dccsWispGraveyardMonstersDLC2.AddCard(2, LoopPest);
                dccsWispGraveyardMonstersDLC2.AddCard(2, LoopVulture);
            }
            /*if (ConfigStages.Stage_3_Sulfur.Value)
            {
                //DOES NOT EXIST IN DLC2
            }*/
            if (ConfigStages.Stage_3_Sulfur.Value)
            {
                DirectorCardCategorySelection dccsSulfurPoolsMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsMonstersDLC1.asset").WaitForCompletion();
                dccsSulfurPoolsMonstersDLC1.AddCard(0, LoopGrandparent); //Loop Parents
                dccsSulfurPoolsMonstersDLC1.AddCard(2, LoopParent); //Idk Yellow on Yellow action or smth
                //dccsSulfurPoolsMonstersDLC1.AddCard(2, LoopLunarExploder);
            }
            if (ConfigStages.Stage_4_Damp_Abyss.Value)
            {
                if (dccsDampCaveMonstersDLC2.categories[0].cards.Length >= 5)
                {
                    dccsDampCaveMonstersDLC2.categories[0].cards[4].minimumStageCompletions = 100000;
                }
                /*dccsDampCaveMonstersDLC2.AddCard(1, DoubleLoopVoidReaver);
                dccsDampCaveMonstersDLC2.AddCard(1, Damp_VoidJailer2);
                dccsDampCaveMonstersDLC2.AddCard(0, Damp_VoidDevestator);
                dccsDampCaveMonstersDLC2.AddCard(2, DoubleLoopVoidBarnacle);*/

                dccsDampCaveMonstersDLC2.AddCard(0, LoopGrandparent); //Loop Parents
                dccsDampCaveMonstersDLC2.AddCard(1, LoopParent); //Yeah I got nothin I just wanted at least 3 Parents
                dccsDampCaveMonstersDLC2.AddCard(0, DoubleLoopVoidJailer);
            }
            if (ConfigStages.Stage_4_Ship.Value)
            {
                dccsShipgraveyardMonstersDLC2.AddCard(1, DoubleLoopVoidReaver);
                dccsShipgraveyardMonstersDLC2.AddCard(1, LoopRoboBallMini); //Sure
                dccsShipgraveyardMonstersDLC2.AddCard(0, SimuLoopMegaConstruct); //Lamps look good I think
                dccsShipgraveyardMonstersDLC2.AddCard(2, SimuLoopMinorConstruct); //
            }
            if (ConfigStages.Stage_4_Root_Jungle.Value)
            {
                if (dccsRootJungleMonstersDLC2.categories[0].cards.Length >= 4)
                {
                    dccsRootJungleMonstersDLC2.categories[0].cards[3].minimumStageCompletions = 100000;
                }

                //dccsRootJungleMonstersDLC2.AddCard(0, DoubleLoopVoidJailer);
                dccsRootJungleMonstersDLC2.AddCard(2, LoopBlindPest); //Hell yeah}
            }
            if (ConfigStages.Stage_5_Sky.Value)
            {
                dccsSkyMeadowMonstersDLC2.AddCard(0, DoubleLoopVoidReaver);
                dccsSkyMeadowMonstersDLC2.AddCard(2, LoopLunarWisp); //If you loop you wouldn't really see these enemies so I think seeing them more here is nice
                dccsSkyMeadowMonstersDLC2.AddCard(2, LoopLunarGolem);
                dccsSkyMeadowMonstersDLC2.AddCard(2, LoopLunarExploder);
            }
            //Dlc2
            if (ConfigStages.Stage_1_Lake.Value)
            {
                dccsLakesnightMonsters.AddCard(0, cscRoboBallBoss);
                dccsLakesnightMonsters.AddCard(1, LoopParent);
                dccsLakesnightMonsters.categories[2].cards[1] = cscJellyfish;
            }

            if (ConfigStages.Stage_5_Helminth.Value)
            {
                dccsHelminthRoostMonsters.AddCard(0, DoubleLoopVoidJailer);
                dccsHelminthRoostMonsters.AddCard(2, LoopLunarWisp);
                dccsHelminthRoostMonsters.AddCard(2, LoopLunarGolem);
                dccsHelminthRoostMonsters.AddCard(2, LoopLunarExploder);
                dccsHelminthRoostMonsters.AddCard(1, cscHalcy6);
            }
            if (ConfigStages.Stage_F_Meridian.Value)
            {
                dccsMeridianMonsters.AddCard(1, LoopLunarWisp);
                dccsMeridianMonsters.AddCard(1, LoopLunarGolem);
                dccsMeridianMonsters.AddCard(2, LoopLunarExploder);
            }
            if (ConfigStages.Stage_X_Arena_Void.Value)
            {
                dccsArenaMonstersDLC1.AddCard(1, LoopLunarWisp);
                dccsArenaMonstersDLC1.AddCard(1, LoopLunarGolem);
                dccsArenaMonstersDLC1.AddCard(1, LoopLunarExploder);
            }
            /*if (ConfigStages.Stage_X_Arena_Void.Value)
            {
                
            }*/
            dccsVoidCampMonsters.AddCard(0, DoubleLoopVoidDevestator);
            dccsVoidCampMonsters.AddCard(0, DoubleLoopVoidDevestator2);
            //

            if (WConfig.InteractableHalcyShrineHalcy.Value)
            {
                DirectorCardCategorySelection dccsShrineHalcyoniteActivationMonsterWave = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShrineHalcyoniteActivationMonsterWave.asset").WaitForCompletion();
                dccsShrineHalcyoniteActivationMonsterWave.AddCard(0, cscHalcy);
                dccsShrineHalcyoniteActivationMonsterWave.AddCard(0, cscHalcy2);
            }
           

            
        }
        //
        //
      
        //RiskyMod broke my spawn pool changes maybe other mods do too so I guess we'll just do this
        public static int FindSpawnCard(DirectorCard[] insert, string LookingFor)
        {
            for (int i = 0; i < insert.Length; i++)
            {
                if (insert[i].spawnCard.name.EndsWith(LookingFor))
                {
                    //Debug.Log("Found " + LookingFor);
                    return i;
                }
            }
            Debug.LogWarning("Couldn't find " + LookingFor);
            return -1;
        }

      

        private static void FixSulfurPoolsBeetleQueen(On.EntityStates.BeetleQueenMonster.SummonEggs.orig_OnEnter orig, EntityStates.BeetleQueenMonster.SummonEggs self)
        {
            orig(self);
            if (self.characterBody.skinIndex > 0)
            {
                EntityStates.BeetleQueenMonster.SummonEggs.spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Beetle/cscBeetleGuardSulfur.asset").WaitForCompletion();
            }
            else
            {
                EntityStates.BeetleQueenMonster.SummonEggs.spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBeetleGuard");
            }

        }

    }

    public class ForcedVoidTeamCSC : CharacterSpawnCard
    {
        public override void Spawn(Vector3 position, Quaternion rotation, DirectorSpawnRequest directorSpawnRequest, ref SpawnCard.SpawnResult result)
        {
            directorSpawnRequest.teamIndexOverride = TeamIndex.Void;
            base.Spawn(position, rotation, directorSpawnRequest, ref result);
        }
    }
}