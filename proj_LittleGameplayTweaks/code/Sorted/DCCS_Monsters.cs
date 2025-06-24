using MonoMod.Cil;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LittleGameplayTweaks
{
    public class DCCS_Monsters
    {

        public static void Start()
        {
            //I should do this better
            if (WConfig.cfgCredits_Monsters.Value)
            {
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Grandparent/cscGrandparent.asset").WaitForCompletion().directorCreditCost = 1000; //1035 would match 600|2100 800|2800 credit|hp ratio. No reason why he should cost extra extra
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscParent").directorCreditCost = 80; //100, Overpriced because was nerfed but not adjusted
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/Child/cscChild.asset").WaitForCompletion().directorCreditCost = 25; //Go forth my children (bro they cannot hit people)
 
               
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier").directorCreditCost = 250; //300, he just sucks dude.
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidBarnacle/cscVoidBarnacle.asset").WaitForCompletion().directorCreditCost = 30; //The floor version only costs 30 credits?
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody").directorCreditCost = 20; //40, Geep is 40
                Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/GreaterWisp/cscGreaterWisp.asset").WaitForCompletion().directorCreditCost = 180; //200, Overpriced because was nerfed but not adjusted

            }

            if (WConfig.cfgDccsMonster.Value)
            {
                Enemies_Stage1();
                Enemies_Stage2();
                Enemies_Stage3();
                Enemies_Stage4();
                Enemies_Stage5();
                Enemies_MiscPools();
            }

            IL.RoR2.ClassicStageInfo.RebuildCards += DCCS_Monsters_ChangesMidRun;

        }

        public static void DCCS_Monsters_ChangesMidRun(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (c.TryGotoNext(MoveType.Before,
            x => x.MatchStfld("RoR2.ClassicStageInfo", "modifiableMonsterCategories")))
            {
                c.EmitDelegate<System.Func<DirectorCardCategorySelection, DirectorCardCategorySelection>>((dccs) =>
                {
                    DCCS_MonsterChanges(dccs);
                    return dccs;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: SotV_EnemyRemovals");
            }
        }

        public static void DCCS_MonsterChanges(DirectorCardCategorySelection dccs)
        {
            if (dccs == null)
            {
                return;
            }
            if (WConfig.cfgEarlyScavs.Value)
            {
                int specialIndex = dccs.FindCategoryIndexByName("Special");
                if (specialIndex > -1 && dccs.categories[specialIndex].cards.Length > 0)
                {
                    dccs.categories[specialIndex].cards[0].minimumStageCompletions = 3;
                }
            }
        }




        public static void Enemies_Stage1()
        {
            #region DCCS
            //DirectorCardCategorySelection dccsGolemplainsMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsGolemplainsMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsMonstersDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsGolemplainsMonstersDLC2Only = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGolemplainsMonstersDLC2Only.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsBlackBeachMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachMonsters.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsBlackBeachMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsBlackBeachMonstersDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSnowyForestMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/snowyforest/dccsSnowyForestMonstersDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsSnowyForestMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSnowyForestMonstersDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsLakesMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesMonsters.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsLakesMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesMonstersDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsLakesMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesnightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightMonsters.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsLakesnightMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightMonsters_DLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsVillageMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillageNightMonsters_Additional = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillageNightMonsters_Additional.asset").WaitForCompletion();

            #endregion
            #region Cards
            DirectorCard cscJellyfish = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Jellyfish/cscJellyfish.asset").WaitForCompletion(),
                selectionWeight = 2,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };

            DirectorCard cscBison = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBison"),
                selectionWeight = 1,
                preventOverhead = false,
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
            DirectorCard LoopRoboBallBoss = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),
                selectionWeight = 2,
                minimumStageCompletions = 3,
            };

            DirectorCard cscHalcy = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/Halcyonite/cscHalcyonite.asset").WaitForCompletion(),
                selectionWeight = 1,
            };
            DirectorCard LoopLunarExploder = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                selectionWeight = 1,
                minimumStageCompletions = 4,
            };
            DirectorCard LoopLunarGolem = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 1,
                minimumStageCompletions = 4,
            };
            DirectorCard LoopLunarWisp = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 4,
            };
            DirectorCard LoopVulture = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                selectionWeight = 3,
                minimumStageCompletions = 3,
            };
            #endregion
            #region Additions
            int num = -1;
            if (ConfigStages.Stage_1_Golem.Value)
            {
                num = DCCS.FindSpawnCard(dccsGolemplainsMonstersDLC1.categories[0].cards, "cscMegaConstruct");
                if (num > -1)
                {
                    dccsGolemplainsMonstersDLC1.categories[0].cards[num].selectionWeight++;
                }
            }
            if (ConfigStages.Stage_1_Roost.Value)
            {
                dccsBlackBeachMonsters.AddCard(2, LoopVulture);
                //dccsBlackBeachMonsters.AddCard(0, LoopGrovetender);
            }
            if (ConfigStages.Stage_1_Snow.Value)
            {
                num = DCCS.FindSpawnCard(dccsSnowyForestMonstersDLC1.categories[1].cards, "cscGreaterWisp");
                if (num > -1)
                {
                    dccsSnowyForestMonstersDLC1.categories[1].cards[num] = cscBison;
                }

                DCCS.MultWholeCateory(dccsSnowyForestMonstersDLC1, 2, 2); //Make Vermin and Pest rarer

                num = DCCS.FindSpawnCard(dccsSnowyForestMonstersDLC1.categories[2].cards, "cscVerminSnowy");
                if (num > -1)
                {
                    dccsSnowyForestMonstersDLC1.categories[2].cards[2].selectionWeight--;
                    dccsSnowyForestMonstersDLC1.categories[2].cards[num].selectionWeight--;
                    dccsSnowyForestMonstersDLC1.categories[2].cards[num].minimumStageCompletions = 0;//Pre Loop Vermin
                }


                dccsSnowyForestMonstersDLC1.AddCard(1, cscBison);

                dccsSnowyForestMonstersDLC1.AddCard(0, LoopImpBoss); //Loop Imps
                dccsSnowyForestMonstersDLC1.AddCard(2, LoopImp); //I don't remember why maybe because the other snow area has Imps?
            }

            if (ConfigStages.Stage_1_Lake.Value)
            {
                dccsLakesMonsters.categories[2].cards[0].selectionWeight--; //Less Beetles
                dccsLakesMonsters.categories[2].cards[2].selectionWeight--; //Less Lemurians

                dccsLakesMonsters.AddCard(0, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Grandparent/cscGrandparent.asset").WaitForCompletion(),
                    selectionWeight = 1,
                });

                dccsLakesMonsters.AddCard(2, cscJellyfish);
                dccsLakesMonsters.AddCard(0, LoopRoboBallBoss);
                dccsLakesnightMonsters.AddCard(2, cscJellyfish);
                dccsLakesnightMonsters.AddCard(0, LoopRoboBallBoss);
                dccsLakesnightMonsters.AddCard(2, LoopVulture);
            }
            if (ConfigStages.Stage_1_Village.Value)
            {
                dccsVillageMonsters.AddCard(0, cscHalcy);
                dccsVillageMonsters.AddCard(0, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Grandparent/cscGrandparent.asset").WaitForCompletion(),
                    selectionWeight = 1,
                });
                dccsVillageMonsters.categories[2].cards[3].spawnDistance = 0; //Why Child spawn far away

                dccsVillageNightMonsters_Additional.AddCard(1, LoopLunarExploder);
                dccsVillageNightMonsters_Additional.AddCard(1, LoopLunarGolem);
                dccsVillageNightMonsters_Additional.AddCard(1, LoopLunarWisp);
            }

            #endregion
        }

        public static void Enemies_Stage2()
        {
            #region DCCS
            DirectorCardCategorySelection dccsGooLakeMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsGooLakeMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeMonstersDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsGooLakeMonstersDLC2Only = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGooLakeMonstersDLC2Only.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFoggySwampMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFoggySwampMonstersDLC = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampMonstersDLC.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsFoggySwampMonstersDLC2Only = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFoggySwampMonstersDLC2Only.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsAncientLoftMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/ancientloft/dccsAncientLoftMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsLemurianTempleMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lemuriantemple/dccsLemurianTempleMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLemurianTempleMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lemuriantemple/dccsLemurianTempleMonstersDLC1.asset").WaitForCompletion();
            #endregion
            #region Cards

            DirectorCard cscClayGrenadier = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopClayBoss = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBoss"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopHermitCrab = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            };
            DirectorCard LoopAcidLarva = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscAcidLarva"),
                selectionWeight = 1,
                preventOverhead = true,
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
            DirectorCard LoopGrovetender = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Gravekeeper/cscGravekeeper.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
 
            DirectorCard LoopBison = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBison"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
 
            #endregion
            #region Additions
            int num = -1;
            if (ConfigStages.Stage_2_Goolake.Value)
            {
                dccsGooLakeMonstersDLC1.AddCard(0, cscClayGrenadier); //PreLoop
                dccsGooLakeMonsters.AddCard(0, LoopClayBoss);
                dccsGooLakeMonsters.AddCard(1, LoopHermitCrab);
                dccsGooLakeMonsters.categories[1].cards[2].selectionWeight++;
            }
            if (ConfigStages.Stage_2_Swamp.Value)
            {
                dccsFoggySwampMonstersDLC.AddCard(0, LoopAcidLarva);
                dccsFoggySwampMonsters.AddCard(2, LoopMiniMushroom);  //Mushroom
            }
            if (ConfigStages.Stage_2_Ancient.Value)
            {
                dccsAncientLoftMonstersDLC1.AddCard(0, LoopGrovetender);
                dccsAncientLoftMonstersDLC1.AddCard(1, LoopElderLemurian); //Loop Elder Lemurian
            }

            if (ConfigStages.Stage_2_Temple.Value)
            {
                dccsLemurianTempleMonsters.AddCard(0, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),
                    preventOverhead = false,
                    selectionWeight = 1,
                    minimumStageCompletions = 0,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                });
                dccsLemurianTempleMonsters.AddCard(1, LoopBison); //Loop Bisons
                dccsLemurianTempleMonsters.AddCard(1, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Bell/cscBell.asset").WaitForCompletion(),
                    preventOverhead = true,
                    selectionWeight = 1,
                }); //Bell

                dccsLemurianTempleMonsters.categories[1].selectionWeight = 3; //More Elder
                dccsLemurianTempleMonsters.categories[2].cards[3].selectionWeight++; //Wurm
                DCCS.RemoveCard(dccsLemurianTempleMonsters, 0, 1); //Beetle Queen
                DCCS.RemoveCard(dccsLemurianTempleMonsters, 2, 2); //Beetle
                DCCS.RemoveCard(dccsLemurianTempleMonstersDLC1, 0, 0); //Blind Pest
            }
            #endregion
        }

        public static void Enemies_Stage3()
        {
            #region DCCS
            DirectorCardCategorySelection dccsFrozenWallMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallMonsters.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsFrozenWallMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallMonstersDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsFrozenWallMonstersDLC2Only = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFrozenWallMonstersDLC2Only.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsWispGraveyardMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardMonsters.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsWispGraveyardMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsWispGraveyardMonstersDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSulfurPoolsMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsMonstersDLC1.asset").WaitForCompletion();

            //DirectorCardCategorySelection dccsHabitatMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitat/dccsHabitatMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitat/dccsHabitatMonsters_DLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsHabitatfallMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitatfall/dccsHabitatfallMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatfallMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitatfall/dccsHabitatfallMonsters_DLC1.asset").WaitForCompletion();
            #endregion
            #region Cards
     
            DirectorCard cscHermitCrab = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
                selectionWeight = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            };

            DirectorCard cscAcidLarva = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscAcidLarva"),
                selectionWeight = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
 
            DirectorCard cscBlindPest = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/FlyingVermin/cscFlyingVermin.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
          
            #endregion
            #region Additions
            if (ConfigStages.Stage_3_Frozen.Value)
            {
                dccsFrozenWallMonsters.categories[0].cards[0] = new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),
                    preventOverhead = false,
                    selectionWeight = 1,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                }; //Clay Dunestrider replaced by Solus Unit
                dccsFrozenWallMonsters.AddCard(1, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
                    preventOverhead = true,
                    selectionWeight = 1,
                    minimumStageCompletions = 4,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                });
                dccsFrozenWallMonsters.AddCard(2, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 4,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                }); //Like the Big Ball leads them to it so they can eat more People and Iron                                               
            }

            if (ConfigStages.Stage_3_Wisp.Value)
            {
                dccsWispGraveyardMonsters.AddCard(1, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                    preventOverhead = false,
                    selectionWeight = 1,
                    minimumStageCompletions = 0,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                });
                dccsWispGraveyardMonsters.AddCard(2, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVermin"),
                    preventOverhead = true,
                    selectionWeight = 4, //The Rats Are Here
                    minimumStageCompletions = 5,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                });
            }

            if (ConfigStages.Stage_3_Sulfur.Value)
            {
                dccsSulfurPoolsMonstersDLC1.categories[0].cards[1].selectionWeight++; //More Mega


                dccsSulfurPoolsMonstersDLC1.categories[2].selectionWeight++;
                dccsSulfurPoolsMonstersDLC1.AddCard(2, cscHermitCrab); //Seems fitting for another trash mob
                dccsSulfurPoolsMonstersDLC1.AddCard(2, cscAcidLarva); //Seems fitting for another trash mob

                dccsSulfurPoolsMonstersDLC1.categories[2].cards[0].selectionWeight = 2;
                dccsSulfurPoolsMonstersDLC1.categories[2].cards[1].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;


                dccsSulfurPoolsMonstersDLC1.AddCard(0, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm"),
                    selectionWeight = 1,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Far
                }); //Worms fit the Sulfur theme I think
                dccsSulfurPoolsMonstersDLC1.AddCard(0, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                    selectionWeight = 1,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Far
                });


                dccsSulfurPoolsMonstersDLC1.AddCard(0, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                    selectionWeight = 1,
                    minimumStageCompletions = 4,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                }); //Loop Parents
                dccsSulfurPoolsMonstersDLC1.AddCard(1, new DirectorCard
                {
                    spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscParent"),
                    selectionWeight = 1,
                    minimumStageCompletions = 4,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                }); //They look nice here
 
            }

            if (ConfigStages.Stage_3_Tree.Value)
            {
                dccsHabitatMonsters_DLC1.AddCard(2, cscBlindPest);
                dccsHabitatfallMonsters_DLC1.AddCard(2, cscBlindPest); //Flying enemy more fitting here than Temple
                dccsHabitatfallMonsters_DLC1.AddCard(2, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/MiniMushroom/cscMiniMushroom.asset").WaitForCompletion(),
                    preventOverhead = false,
                    selectionWeight = 2,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                }); //Mushroom biome but no mushroom enemy??
            }
            #endregion
        }

        public static void Enemies_Stage4()
        {
            #region DCCS
            DirectorCardCategorySelection dccsDampCaveMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsDampCaveMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveMonstersDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsDampCaveMonstersDLC2Only = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsDampCaveMonstersDLC2Only.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsShipgraveyardMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardMonstersDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsShipgraveyardMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShipgraveyardMonstersDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsRootJungleMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsRootJungleMonstersDLC2.asset").WaitForCompletion();

            #endregion
            #region Cards
            DirectorCard LoopParent = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscParent"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopGrandparent = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopVoidJailer = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscElectricWorm = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard LoopRoboBallMini = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopMinorConstruct = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/MajorAndMinorConstruct/cscMinorConstruct.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopMegaConstruct = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/MajorAndMinorConstruct/cscMegaConstruct.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscGrovetender = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
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
            #endregion
            #region Adds
            if (ConfigStages.Stage_4_Damp_Abyss.Value)
            {
                dccsDampCaveMonsters.AddCard(0, LoopGrandparent); //Loop Parents, idk they look cool there.
                dccsDampCaveMonsters.AddCard(1, LoopParent);
                dccsDampCaveMonstersDLC1.AddCard(0, LoopVoidJailer);
            }
            if (ConfigStages.Stage_4_Ship.Value)
            {
                dccsShipgraveyardMonsters.AddCard(0, cscElectricWorm); //Magma Worm so Elec Worm

                dccsShipgraveyardMonsters.AddCard(1, LoopRoboBallMini); //Sure
                dccsShipgraveyardMonstersDLC1.AddCard(0, LoopMegaConstruct); //Lamps look good I think
                dccsShipgraveyardMonstersDLC1.AddCard(0, LoopMinorConstruct);
            }

            if (ConfigStages.Stage_4_Root_Jungle.Value)
            {
                dccsRootJungleMonsters.categories[0].cards[1] = cscGrovetender; //Titan replaced by Grovetender
                dccsRootJungleMonsters.categories[1].cards[0].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Golem/cscGolemNature.asset").WaitForCompletion();   //Skinned Golem

                dccsRootJungleMonstersDLC1.AddCard(1, LoopBlindPest); //Log says they like Warm and Jungle biomes, proceeds to add them 2 Snow Biomes good one Hopoo.
                dccsRootJungleMonstersDLC1.AddCard(0, LoopVoidJailer);

                DCCS.RemoveCard(dccsRootJungleMonstersDLC2, 0, 0);
            }

            #endregion
        }

        public static void Enemies_Stage5()
        {
            #region DCCS
            DirectorCardCategorySelection dccsSkyMeadowMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowMonsters.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsSkyMeadowMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowMonstersDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsSkyMeadowMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSkyMeadowMonstersDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsHelminthRoostMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/helminthroost/dccsHelminthRoostMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHelminthRoostMonstersDLC2Only = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/helminthroost/dccsHelminthRoostMonstersDLC2Only.asset").WaitForCompletion();

            #endregion
            #region Cards
            DirectorCard LoopLunarExploder = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarGolem = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarWisp = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscGrandparent = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Grandparent/cscGrandparent.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopVoidDevestator = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard Halcy = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/Halcyonite/cscHalcyonite.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            #endregion
            #region Additions

            if (ConfigStages.Stage_5_Sky.Value)
            {
                dccsSkyMeadowMonsters.AddCard(2, LoopLunarWisp); //If you loop you wouldn't really see these enemies so I think seeing them more here is nice
                dccsSkyMeadowMonsters.AddCard(2, LoopLunarGolem);
                dccsSkyMeadowMonsters.AddCard(2, LoopLunarExploder);
                dccsSkyMeadowMonsters.AddCard(0, LoopVoidDevestator);
            }

            if (ConfigStages.Stage_5_Helminth.Value)
            {
                dccsHelminthRoostMonstersDLC2Only.categories[0].cards[0].selectionWeight++; //More Wurm
                dccsHelminthRoostMonsters.AddCard(0, cscGrandparent); //Why they didn't put him here 
                dccsHelminthRoostMonsters.categories[1].cards[0] = Halcy; //Replace Greater Wisp
                //dccsHelminthRoostMonsters.AddCard(2, LoopLunarWisp);
                //dccsHelminthRoostMonsters.AddCard(2, LoopLunarGolem);
                //dccsHelminthRoostMonsters.AddCard(2, LoopLunarExploder);

            }

            #endregion
        }
        public static void Enemies_MiscPools()
        {
            #region DCCS
            DirectorCardCategorySelection dccsMeridianMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/meridian/dccsMeridianMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsArenaMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/arena/dccsArenaMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVoidCampMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/VoidCamp/dccsVoidCampMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsGoldshoresMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goldshores/dccsGoldshoresMonsters.asset").WaitForCompletion();

            #endregion
            #region Cards
            DirectorCard LoopLunarExploder = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarGolem = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarWisp = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscParent = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Parent/cscParent.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscBell = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Bell/cscBell.asset").WaitForCompletion(),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscGrandparent = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Grandparent/cscGrandparent.asset").WaitForCompletion(),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscGravekeeper = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Gravekeeper/cscGravekeeper.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
          
            DirectorCard ShrineMoreHalcy = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/Halcyonite/cscHalcyonite.asset").WaitForCompletion(),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
          
            #endregion
            #region Additions
            if (ConfigStages.Stage_X_GoldShores.Value)
            {

                dccsGoldshoresMonsters.AddCard(0, cscGrandparent);
                dccsGoldshoresMonsters.AddCard(0, cscGravekeeper);
                dccsGoldshoresMonsters.AddCard(1, cscBell);
                dccsGoldshoresMonsters.AddCard(1, cscParent);

            }

            if (ConfigStages.Stage_F_Meridian.Value)
            {
                dccsMeridianMonsters.AddCard(1, LoopLunarWisp);
                dccsMeridianMonsters.AddCard(1, LoopLunarGolem);
                dccsMeridianMonsters.AddCard(2, LoopLunarExploder);
            }
            if (ConfigStages.Stage_X_Arena_Void.Value)
            {
                /*dccsArenaMonsters.AddCard(1, LoopLunarWisp);
                dccsArenaMonsters.AddCard(1, LoopLunarGolem);
                dccsArenaMonsters.AddCard(1, LoopLunarExploder);*/
            }

            //More common Devestators because they're kinda rare.
            //Change to replace Jailer on Stage 5, chance to replace anything on loops
            dccsVoidCampMonsters.categories[0].selectionWeight++;
            dccsVoidCampMonsters.categories[1].selectionWeight++;
            dccsVoidCampMonsters.categories[2].cards[1].selectionWeight = 6;
            dccsVoidCampMonsters.AddCard(1, new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                selectionWeight = 1,
                minimumStageCompletions = 4,
            }); //More Devastators stage 5
            dccsVoidCampMonsters.AddCard(2, new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                selectionWeight = 1,
                minimumStageCompletions = 7,
            }); //More Devastators late loop
            dccsVoidCampMonsters.AddCard(2, new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("5786a7412d08c49478e91947083bafdd"),
                selectionWeight = 2,
                minimumStageCompletions = 7,
            }); //More Jailers

            if (WConfig.cfgHalcyon_Spawnpool.Value)
            {
                DirectorCardCategorySelection dccsShrineHalcyoniteActivationMonsterWave = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShrineHalcyoniteActivationMonsterWave.asset").WaitForCompletion();
                dccsShrineHalcyoniteActivationMonsterWave.AddCard(0, ShrineMoreHalcy = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/Halcyonite/cscHalcyonite.asset").WaitForCompletion(),
                    selectionWeight = 2,
                    minimumStageCompletions = 3,
                }); //Halcy instead of Stone Golem sometimes
                dccsShrineHalcyoniteActivationMonsterWave.AddCard(0, new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/Halcyonite/cscHalcyonite.asset").WaitForCompletion(),
                    selectionWeight = 4,
                    minimumStageCompletions = 7,
                }); //More common on loops

                dccsShrineHalcyoniteActivationMonsterWave.AddCard(0, new DirectorCard
                {
                    spawnCardReference = new AssetReferenceT<SpawnCard>("5d95c9377c88031459b0e9d3c41c69c8"),
                    selectionWeight = 1,
                    minimumStageCompletions = 3,
                }); //Rare Stone Titan
            }
            #endregion
        }











        public static void FixSulfurPoolsBeetleQueen(On.EntityStates.BeetleQueenMonster.SummonEggs.orig_OnEnter orig, EntityStates.BeetleQueenMonster.SummonEggs self)
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