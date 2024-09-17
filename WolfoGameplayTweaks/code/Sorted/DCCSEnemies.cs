using RoR2;
using R2API;
using RoR2.ExpansionManagement;
using RoR2.Navigation;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class DCCSEnemies
    {
        public static FamilyDirectorCardCategorySelection dccsClayFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();

        public static void Start()
        {
            if (WConfig.DCCSEnemyChanges.Value == true)
            {
                EnemiesPreLoop_NoDLC();
                EnemiesPreLoop_DLC1();
                EnemiesPreLoop_DLC2();
            }
            if (WConfig.DCCSEnemyChangesLooping.Value == true)
            {
                EnemiesPostLoop_DLC1();
                EnemiesPostLoop_DLC2();
            }
            if (WConfig.DCCSEnemyNewFamilies.Value == true)
            {
                Families();
            }

            On.RoR2.DccsPool.AreConditionsMet += DccsPool_AreConditionsMet;
        }


        private static bool DccsPool_AreConditionsMet(On.RoR2.DccsPool.orig_AreConditionsMet orig, DccsPool self, DccsPool.ConditionalPoolEntry entry)
        {
            if (entry.requiredExpansions.Length == 1)
            {
                if (entry.requiredExpansions[0].name.Equals("DLC2"))
                {
                    entry.weight = 50000;
                }
                else
                {
                    entry.weight = 500;
                }           
            }
            else if (entry.requiredExpansions.Length == 2)
            {
                entry.weight = 5000000;
            }
            return orig(self, entry);
        }

        public static void EnemiesPreLoop_NoDLC()
        {
            DirectorCardCategorySelection dccsFrozenWallMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleMonsters.asset").WaitForCompletion();

            DirectorCard DC_Grovetender = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_RoboBallBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            dccsFrozenWallMonsters.categories[0].cards[0] = DC_RoboBallBoss; //Clay Dunestrider replaced by Solus Unit

            dccsRootJungleMonsters.categories[0].cards[1] = DC_Grovetender; //Titan replaced by Grovetender
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

            DirectorCardCategorySelection dccsArtifactWorldMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/artifactworld/dccsArtifactWorldMonstersDLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsGoldshoresMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goldshores/dccsGoldshoresMonstersDLC1.asset").WaitForCompletion();
 
            DirectorCard DC_MagmaWorm = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DC_ElectricWorm = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DC_Grovetender = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_RoboBallBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_HermitCrab = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
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
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            int num = -1;

            //dccsGolemplainsMonstersDLC1

            //Snowy Forest
            num = FindSpawnCard(dccsSnowyForestMonstersDLC1.categories[1].cards, "Nullifier");
            if (num > -1)
            {
                dccsSnowyForestMonstersDLC1.categories[1].cards = dccsSnowyForestMonstersDLC1.categories[1].cards.Remove(dccsSnowyForestMonstersDLC1.categories[1].cards[num]);  
                //Remove Reaver
            }
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

            //
            //Goolake
            num = FindSpawnCard(dccsGooLakeMonstersDLC1.categories[1].cards, "ClayGrenadier");
            if (num > -1)
            {
                dccsGooLakeMonstersDLC1.categories[1].cards[num].minimumStageCompletions = 0;
                //Pre Loop Clay Grenadier
            }
            //
            dccsFrozenWallMonstersDLC1.categories[0].cards[0] = DC_RoboBallBoss; //Clay Dunestrider replaced by Solus Unit
            //
            dccsWispGraveyardMonstersDLC1.AddCard(1, DC_ClayGrenadier);

            //
            //SulfurPools
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


            On.EntityStates.BeetleQueenMonster.SummonEggs.OnEnter += FixSulfurPoolsBeetleQueen;

            //
            //Sundered Grove
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
            //
            dccsShipgraveyardMonstersDLC1.AddCard(0, DC_ElectricWorm); //Magma Worm so Elec Worm

            dccsSkyMeadowMonstersDLC1.AddCard(0, DC_MagmaWorm); //They removed him with the DLC but Elec Worm is still there

            dccsArtifactWorldMonstersDLC1.AddCard(0, DC_Grovetender);
            dccsArtifactWorldMonstersDLC1.AddCard(0, DC_MagmaWorm);
            dccsArtifactWorldMonstersDLC1.AddCard(1, DC_ClayGrenadier);
            //dccsArtifactWorldMonstersDLC1.AddCard(2, DC_BlindVermin);
        }

        public static void EnemiesPreLoop_DLC2()
        {
            DirectorCardCategorySelection dccsGolemplainsMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGolemplainsMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsBlackBeachMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsBlackBeachMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSnowyForestMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSnowyForestMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesMonstersDLC2.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsLakesnightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillageMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageMonsters_DLC1.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsVillageNightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillageNightMonsters.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsGooLakeMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGooLakeMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFoggySwampMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFoggySwampMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsAncientLoftMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsAncientLoftMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLemurianTempleMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lemuriantemple/dccsLemurianTempleMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFrozenWallMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFrozenWallMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsWispGraveyardMonstersDLC2.asset").WaitForCompletion();
            //DOES_NOT_EXIST//DirectorCardCategorySelection dccsSulfurPoolsMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/sulfurpools/dccsSulfurPoolsMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitat/dccsHabitatMonsters_DLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatfallMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitatfall/dccsHabitatfallMonsters_DLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsDampCaveMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsDampCaveMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShipgraveyardMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsRootJungleMonstersDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSkyMeadowMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSkyMeadowMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHelminthRoostMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/helminthroost/dccsHelminthRoostMonsters.asset").WaitForCompletion();


            DirectorCard cscBison = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBison"),
                selectionWeight = 2,
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
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DC_ElectricWorm = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DC_Grovetender = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_Grovetender1 = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_RoboBallBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_HermitCrab = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
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
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            int num = -1;

            //dccsGolemplainsMonstersDLC2

            //Snowy Forest
            num = FindSpawnCard(dccsSnowyForestMonstersDLC2.categories[1].cards, "Nullifier");
            if (num > -1)
            {
                dccsSnowyForestMonstersDLC2.categories[1].cards = dccsSnowyForestMonstersDLC2.categories[1].cards.Remove(dccsSnowyForestMonstersDLC2.categories[1].cards[num]);
                //Remove Reaver
            }
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


            num = FindSpawnCard(dccsLakesMonstersDLC2.categories[2].cards, "LesserWisp");
            if (num > -1)
            {
                DirectorCard cscJellyfish = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Jellyfish/cscJellyfish.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 0,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Far
                };

                dccsLakesMonstersDLC2.categories[2].cards[num] = cscJellyfish;
            }
            //
            //Goolake
            num = FindSpawnCard(dccsGooLakeMonstersDLC2.categories[1].cards, "ClayGrenadier");
            if (num > -1)
            {
                dccsGooLakeMonstersDLC2.categories[1].cards[num].minimumStageCompletions = 0;
                //Pre Loop Clay Grenadier
            }
            //
            dccsFrozenWallMonstersDLC2.categories[0].cards[0] = DC_RoboBallBoss; //Clay Dunestrider replaced by Solus Unit
            //
            dccsWispGraveyardMonstersDLC2.AddCard(1, DC_ClayGrenadier);


            //
            //Sundered Grove
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
            //
            dccsShipgraveyardMonstersDLC2.AddCard(0, DC_ElectricWorm); //Magma Worm so Elec Worm

            dccsSkyMeadowMonstersDLC2.AddCard(0, DC_MagmaWorm); //They removed him with the DLC but Elec Worm is still there





            //DLC2 Stuff
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

            dccsHabitatMonsters_DLC1.AddCard(2, DC_BlindPest);
            dccsHabitatfallMonsters_DLC1.AddCard(2, DC_BlindPest);

            dccsHabitatMonsters_DLC1.AddCard(0, DC_Grovetender1);

            dccsHabitatMonsters_DLC1.categories[0].cards[2] = DC_Grovetender1;
            dccsHabitatfallMonsters_DLC1.AddCard(0, DC_Grovetender1);

            num = FindSpawnCard(dccsHelminthRoostMonsters.categories[1].cards, "corchling");
            if (num > -1)
            {
                dccsHelminthRoostMonsters.categories[1].cards[num].selectionWeight++;
            }



            dccsHabitatfallMonsters_DLC1.AddCard(2, DC_Mushroom);
            dccsHelminthRoostMonsters.AddCard(0, DC_Grandparent); 

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
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopImp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImp"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard LoopImpBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImpBoss"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopMiniMushroom = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMiniMushroom"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            DirectorCard LoopElderLemurian = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopBlindPest = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVermin"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };



            DirectorCard LoopParent = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscParent"),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopGrandparent = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 2,
                preventOverhead = false,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            DirectorCard LoopAcidLarva = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscAcidLarva"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard LoopLunarExploder = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarGolem = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarWisp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopRoboBallMini = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
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
            dccsBlackBeachMonstersDLC.AddCard(2, LoopVulture);

            dccsSnowyForestMonstersDLC1.AddCard(0, LoopImpBoss); //Loop Imps
            dccsSnowyForestMonstersDLC1.AddCard(2, LoopImp); //I don't remember why maybe because the other snow area has Imps?


            //Goolake already has Templar
            dccsGooLakeMonstersDLC1.AddCard(2, LoopAcidLarva); //Simu has Acid Larva here but I don't see how this fits

            dccsFoggySwampMonstersDLC.AddCard(2, LoopMiniMushroom);  //Mushroom

            dccsAncientLoftMonstersDLC1.AddCard(0, LoopGrovetender); 
            dccsAncientLoftMonstersDLC1.AddCard(1, LoopElderLemurian); //Loop Elder Lemurian
            //
            dccsFrozenWallMonstersDLC1.AddCard(2, LoopVulture); //Like the Big Ball leads them to it so they can eat more People and Iron

            int num = FindSpawnCard(dccsWispGraveyardMonstersDLC1.categories[2].cards, "Vulture");
            if (num > -1)
            {
                dccsWispGraveyardMonstersDLC1.categories[2].cards[num].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/Vermin/cscVermin.asset").WaitForCompletion();
                dccsWispGraveyardMonstersDLC1.categories[2].cards[num].selectionWeight = 3;
                //Idk why we replace Vultures with Rats
            }

            dccsSulfurPoolsMonstersDLC1.AddCard(0, LoopGrandparent); //Loop Parents
            dccsSulfurPoolsMonstersDLC1.AddCard(2, LoopParent); //Idk Yellow on Yellow action or smth
            //
            dccsDampCaveMonstersDLC1.AddCard(0, LoopGrandparent); //Loop Parents
            dccsDampCaveMonstersDLC1.AddCard(1, LoopParent); //Yeah I got nothin I just wanted at least 3 Parents

            dccsShipgraveyardMonstersDLC1.AddCard(1, LoopRoboBallMini); //Sure
            dccsShipgraveyardMonstersDLC1.AddCard(0, SimuLoopMegaConstruct); //Lamps look good I think
            dccsShipgraveyardMonstersDLC1.AddCard(2, SimuLoopMinorConstruct); //

            dccsRootJungleMonstersDLC1.AddCard(2, LoopBlindPest); //Hell yeah

            dccsSkyMeadowMonstersDLC1.AddCard(2, LoopLunarWisp); //If you loop you wouldn't really see these enemies so I think seeing them more here is nice
            dccsSkyMeadowMonstersDLC1.AddCard(2, LoopLunarGolem);
            dccsSkyMeadowMonstersDLC1.AddCard(2, LoopLunarExploder);


            //VoidStuff
            //dccsGolemplainsMonstersDLC1.AddCard(1, Void);
            //dccsBlackBeachMonstersDLC.AddCard(2, DoubleLoopVoidBarnacle);
            dccsSnowyForestMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);

            //dccsGooLakeMonstersDLC1.AddCard(1, DoubleLoopVoidJailer);
            dccsFoggySwampMonstersDLC.AddCard(1, DoubleLoopVoidJailer);
            //dccsAncientLoftMonstersDLC1.AddCard(0, DoubleLoopVoidDevestator); 
            //
            dccsFrozenWallMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);
            //dccsWispGraveyardMonstersDLC1.AddCard(1, DoubleLoopVoidJailer);
            //dccsSulfurPoolsMonstersDLC1.AddCard(0, DoubleLoopVoidDevestator);

            dccsDampCaveMonstersDLC1.AddCard(0, DoubleLoopVoidJailer);
            dccsShipgraveyardMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);
            dccsRootJungleMonstersDLC1.AddCard(1, DoubleLoopVoidJailer);

            dccsSkyMeadowMonstersDLC1.AddCard(0, DoubleLoopVoidDevestator);
            //dccsSkyMeadowMonstersDLC1.AddCard(1, DoubleLoopVoidJailer);
            //dccsSkyMeadowMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);

        }

        public static void EnemiesPostLoop_DLC2()
        {
            DirectorCardCategorySelection dccsGolemplainsMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGolemplainsMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsBlackBeachMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsBlackBeachMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsSnowyForestMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSnowyForestMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakes/dccsLakesMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesnightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillageMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/village/dccsVillageMonsters_DLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillageNightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillageNightMonsters.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsGooLakeMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsGooLakeMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsFoggySwampMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFoggySwampMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsAncientLoftMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsAncientLoftMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLemurianTempleMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lemuriantemple/dccsLemurianTempleMonstersDLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsFrozenWallMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsFrozenWallMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsWispGraveyardMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsWispGraveyardMonstersDLC2.asset").WaitForCompletion();
            //DOES_NOT_EXIST//DirectorCardCategorySelection dccsSulfurPoolsMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/sulfurpools/dccsSulfurPoolsMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitat/dccsHabitatMonsters_DLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHabitatfallMonsters_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/habitatfall/dccsHabitatfallMonsters_DLC1.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsDampCaveMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsDampCaveMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsShipgraveyardMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShipgraveyardMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsRootJungleMonstersDLC2.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsSkyMeadowMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsSkyMeadowMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsHelminthRoostMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/helminthroost/dccsHelminthRoostMonsters.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsArenaMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/arena/dccsArenaMonstersDLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVoidCampMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/VoidCamp/dccsVoidCampMonsters.asset").WaitForCompletion();





            DirectorCard LoopVulture = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard LoopImp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImp"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard LoopImpBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImpBoss"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopMiniMushroom = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMiniMushroom"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            DirectorCard LoopElderLemurian = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopBlindPest = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVermin"),
                preventOverhead = true,
                selectionWeight = 3,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopPest = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVermin"),
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
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscParent"),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopGrandparent = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 2,
                preventOverhead = false,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            DirectorCard LoopAcidLarva = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscAcidLarva"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard LoopLunarExploder = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarGolem = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarWisp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopRoboBallMini = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            /*DirectorCard DoubleLoopVoidBarnacle = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/VoidBarnacle/cscVoidBarnacle.asset").WaitForCompletion(),
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
            DirectorCard DoubleLoopVoidDevestator = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                selectionWeight = 3,
                preventOverhead = true,
                minimumStageCompletions = 3,
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
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard Loop_GreaterWisp = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/GreaterWisp/cscGreaterWisp.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_HermitCrab = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            };


            int num = FindSpawnCard(dccsGolemplainsMonstersDLC2.categories[0].cards, "MegaConstruct");
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

            //Golem Plains already has Lamps, Lamp Boss and Hermit Crab
            dccsBlackBeachMonstersDLC2.AddCard(2, LoopVulture);
            //dccsBlackBeachMonstersDLC2.AddCard(0, LoopGrovetender);

            dccsSnowyForestMonstersDLC2.AddCard(0, LoopImpBoss); //Loop Imps
            dccsSnowyForestMonstersDLC2.AddCard(2, LoopImp); //I don't remember why maybe because the other snow area has Imps?


            //Goolake already has Templar
            dccsGooLakeMonstersDLC2.AddCard(1, DC_HermitCrab); 

            dccsFoggySwampMonstersDLC2.AddCard(2, LoopAcidLarva); //Simu has Acid Larva here but I don't see how this fits
            dccsFoggySwampMonstersDLC2.AddCard(2, LoopMiniMushroom);  //Mushroom

            dccsAncientLoftMonstersDLC2.AddCard(0, LoopGrovetender);
            dccsAncientLoftMonstersDLC2.AddCard(1, LoopElderLemurian); //Loop Elder Lemurian
            //
            dccsFrozenWallMonstersDLC2.AddCard(2, LoopVulture); //Like the Big Ball leads them to it so they can eat more People and Iron

            dccsWispGraveyardMonstersDLC2.AddCard(2, DC_Child);
            dccsWispGraveyardMonstersDLC2.AddCard(2, LoopPest);
            num = FindSpawnCard(dccsWispGraveyardMonstersDLC2.categories[2].cards, "Vulture"); //Vultures are removed in Sots
            /*if (num > -1)
            {
                dccsWispGraveyardMonstersDLC2.categories[2].cards[num].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/Vermin/cscVermin.asset").WaitForCompletion();
                dccsWispGraveyardMonstersDLC2.categories[2].cards[num].selectionWeight = 3;
                //Idk why we replace Vultures with Rats
            }*/

            //
            dccsDampCaveMonstersDLC2.AddCard(0, LoopGrandparent); //Loop Parents
            dccsDampCaveMonstersDLC2.AddCard(1, LoopParent); //Yeah I got nothin I just wanted at least 3 Parents
            dccsDampCaveMonstersDLC2.AddCard(1, DC_Child); //Yeah I got nothin I just wanted at least 3 Parents

            dccsShipgraveyardMonstersDLC2.AddCard(1, LoopRoboBallMini); //Sure
            dccsShipgraveyardMonstersDLC2.AddCard(0, SimuLoopMegaConstruct); //Lamps look good I think
            dccsShipgraveyardMonstersDLC2.AddCard(2, SimuLoopMinorConstruct); //

            dccsRootJungleMonstersDLC2.AddCard(2, LoopBlindPest); //Hell yeah

            dccsSkyMeadowMonstersDLC2.AddCard(2, LoopLunarWisp); //If you loop you wouldn't really see these enemies so I think seeing them more here is nice
            dccsSkyMeadowMonstersDLC2.AddCard(2, LoopLunarGolem);
            dccsSkyMeadowMonstersDLC2.AddCard(2, LoopLunarExploder);

            //Dlc2
            dccsLakesnightMonsters.AddCard(0, cscRoboBallBoss);
            dccsLakesnightMonsters.AddCard(1, LoopParent);
            dccsLakesnightMonsters.categories[2].cards[1] = cscJellyfish;

            dccsHelminthRoostMonsters.AddCard(2, LoopLunarWisp); 
            dccsHelminthRoostMonsters.AddCard(2, LoopLunarGolem);
            dccsHelminthRoostMonsters.AddCard(2, LoopLunarExploder);

            dccsArenaMonstersDLC1.AddCard(1, LoopLunarWisp);
            dccsArenaMonstersDLC1.AddCard(1, LoopLunarGolem);
            dccsArenaMonstersDLC1.AddCard(1, LoopLunarExploder);


            //VoidStuff
            //dccsGolemplainsMonstersDLC2.AddCard(1, Void);
            //dccsBlackBeachMonstersDLC.AddCard(2, DoubleLoopVoidBarnacle);
            dccsSnowyForestMonstersDLC2.AddCard(1, DoubleLoopVoidReaver);

            //dccsGooLakeMonstersDLC2.AddCard(1, DoubleLoopVoidJailer);
            dccsFoggySwampMonstersDLC2.AddCard(1, DoubleLoopVoidJailer);
            //dccsAncientLoftMonstersDLC2.AddCard(0, DoubleLoopVoidDevestator); 
            //
            dccsFrozenWallMonstersDLC2.AddCard(0, DoubleLoopVoidReaver);
            //dccsWispGraveyardMonstersDLC2.AddCard(1, DoubleLoopVoidJailer);
            //dccsSulfurPoolsMonstersDLC2.AddCard(0, DoubleLoopVoidDevestator);

            dccsDampCaveMonstersDLC2.AddCard(0, DoubleLoopVoidJailer);
            dccsShipgraveyardMonstersDLC2.AddCard(1, DoubleLoopVoidReaver);
            dccsRootJungleMonstersDLC2.AddCard(0, DoubleLoopVoidJailer);

            dccsSkyMeadowMonstersDLC2.AddCard(0, DoubleLoopVoidReaver);
            dccsHelminthRoostMonsters.AddCard(0, DoubleLoopVoidJailer);

            dccsVoidCampMonsters.AddCard(0, DoubleLoopVoidDevestator);
            dccsVoidCampMonsters.AddCard(0, DoubleLoopVoidDevestator2);


        }
        //
        //
        public static void Families()
        {
            DccsPool dpGolemplainsMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/golemplains/dpGolemplainsMonsters.asset").WaitForCompletion();
            DccsPool dpBlackBeachMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/blackbeach/dpBlackBeachMonsters.asset").WaitForCompletion();
            DccsPool dpSnowyForestMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/snowyforest/dpSnowyForestMonsters.asset").WaitForCompletion();

            DccsPool dpGooLakeMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/goolake/dpGooLakeMonsters.asset").WaitForCompletion();
            DccsPool dpFoggySwampMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/foggyswamp/dpFoggySwampMonsters.asset").WaitForCompletion();
            DccsPool dpAncientLoftMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/ancientloft/dpAncientLoftMonsters.asset").WaitForCompletion();

            DccsPool dpFrozenWallMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/frozenwall/dpFrozenWallMonsters.asset").WaitForCompletion();
            DccsPool dpWispGraveyardMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/wispgraveyard/dpWispGraveyardMonsters.asset").WaitForCompletion();
            DccsPool dpSulfurPoolsMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/sulfurpools/dpSulfurPoolsMonsters.asset").WaitForCompletion();

            DccsPool dpDampCaveMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/dampcave/dpDampCaveMonsters.asset").WaitForCompletion();
            DccsPool dpShipgraveyardMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/shipgraveyard/dpShipgraveyardMonsters.asset").WaitForCompletion();
            DccsPool dpRootJungleMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/rootjungle/dpRootJungleMonsters.asset").WaitForCompletion();

            DccsPool dpSkyMeadowMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/skymeadow/dpSkyMeadowMonsters.asset").WaitForCompletion();

            DccsPool dpMoonMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/moon/dpMoonMonsters.asset").WaitForCompletion();
            DccsPool dpArtifactWorldMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/artifactworld/dpArtifactWorldMonsters.asset").WaitForCompletion();
            DccsPool dpVoidStageMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/voidstage/dpVoidStageMonsters.asset").WaitForCompletion();
            //
            //
            FamilyDirectorCardCategorySelection dccsBeetleFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsBeetleFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsGolemFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGolemFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsGolemFamilyAbyssal = UnityEngine.Object.Instantiate(dccsGolemFamily);
            FamilyDirectorCardCategorySelection dccsGupFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGupFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsImpFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsImpFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsJellyfishFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsJellyfishFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsLemurianFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLemurianFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsLunarFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsParentFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsParentFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsConstructFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsConstructFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsWispFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsWispFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsVoidFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsVoidFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsMushroomFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsMushroomFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsAcidLarvaFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsAcidLarvaFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsRoboBallFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
            FamilyDirectorCardCategorySelection dccsVerminFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
            FamilyDirectorCardCategorySelection dccsVerminFamilySnowy = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
            FamilyDirectorCardCategorySelection dccsWormsFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();



            DirectorCard DC_TitanDampCaves = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscTitanDampCave"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DC_ClayBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBoss"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_ClayTemp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBruiser"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_ClayGrenadier = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DC_RoboBallBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_RoboBallMini = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
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
            //Debug.LogWarning(DSVultureNoCeling.spawnCard);


            DirectorCard DC_Grandparent = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DC_Geep = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGeepBody"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DC_Gip = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DC_BlindPest = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVermin"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_BlindVermin = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVermin"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_BlindPestSnowy = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVerminSnowy"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DC_BlindVerminSnowy = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVerminSnowy"),
                preventOverhead = false,
                selectionWeight = 1,
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

            dccsGolemFamilyAbyssal.categories[0].cards[0] = DC_TitanDampCaves;
            dccsGolemFamilyAbyssal.name = "dccsGolemFamilyAbyssal";

            dccsClayFamily.AddCategory("Champions", 3);
            dccsClayFamily.AddCategory("Minibosses", 6);
            dccsClayFamily.AddCard(0, DC_ClayBoss);
            dccsClayFamily.AddCard(1, DC_ClayTemp);
            dccsClayFamily.AddCard(1, DC_ClayGrenadier);
            dccsClayFamily.name = "dccsClayFamily";
            dccsClayFamily.minimumStageCompletion = 0;
            dccsClayFamily.maximumStageCompletion = 1000000;
            dccsClayFamily.selectionChatString = "<style=cWorldEvent>[WARNING] You feel parasitic influences..</style>";

            dccsRoboBallFamily.AddCategory("Champions", 4);
            dccsRoboBallFamily.AddCategory("Minibosses", 4);
            dccsRoboBallFamily.AddCategory("Basic Monsters", 4);
            dccsRoboBallFamily.AddCard(0, DC_RoboBallBoss);
            dccsRoboBallFamily.AddCard(1, DC_RoboBallMini);
            dccsRoboBallFamily.AddCard(2, DC_VultureNoCeling);
            dccsRoboBallFamily.name = "dccsRoboBallFamily";
            dccsRoboBallFamily.minimumStageCompletion = 0;
            dccsRoboBallFamily.maximumStageCompletion = 1000000;
            dccsRoboBallFamily.selectionChatString = "<style=cWorldEvent>[WARNING] You hear the whirring of wings and machinery..</style>";

            dccsVerminFamily.AddCategory("Basic Monsters", 6);
            dccsVerminFamily.AddCard(0, DC_BlindPest);
            dccsVerminFamily.AddCard(0, DC_BlindVermin);
            dccsVerminFamily.name = "dccsVerminFamily";
            dccsVerminFamily.minimumStageCompletion = 0;
            dccsVerminFamily.maximumStageCompletion = 9;
            dccsVerminFamily.selectionChatString = "<style=cWorldEvent>[WARNING] You have invaded rampant breeding grounds..</style>";

            dccsVerminFamilySnowy.AddCategory("Basic Monsters", 6);
            dccsVerminFamilySnowy.AddCard(0, DC_BlindPestSnowy);
            dccsVerminFamilySnowy.AddCard(0, DC_BlindVerminSnowy);
            dccsVerminFamilySnowy.name = "dccsVerminSnowyFamily";
            dccsVerminFamilySnowy.minimumStageCompletion = 0;
            dccsVerminFamilySnowy.maximumStageCompletion = 9;
            dccsVerminFamilySnowy.selectionChatString = "<style=cWorldEvent>[WARNING] You have invaded rampant breeding grounds..</style>";
            //<style=cWorldEvent>[WARNING] You hear squeaks and chirps around you..</style>

            dccsWormsFamily.AddCategory("Champions", 5);
            dccsWormsFamily.AddCard(0, DC_MagmaWormWithElite);
            dccsWormsFamily.AddCard(0, DC_ElectricWormWithElite);
            dccsWormsFamily.AddCategory("Miniboss", 5);
            dccsWormsFamily.AddCard(1, DC_Scorchling);
            dccsWormsFamily.minimumStageCompletion = 7;
            dccsWormsFamily.maximumStageCompletion = 1000000;
            dccsWormsFamily.name = "dccsWormsFamily";
            dccsWormsFamily.selectionChatString = "<style=cWorldEvent>[WARNING] His brother loved worms..</style>";



            dccsLemurianFamily.minimumStageCompletion = 0;
            dccsJellyfishFamily.minimumStageCompletion = 0;
            dccsGupFamily.minimumStageCompletion = 0;
            dccsBeetleFamily.minimumStageCompletion = 0;
            dccsImpFamily.minimumStageCompletion = 0;
            dccsWispFamily.minimumStageCompletion = 0;
            dccsConstructFamily.minimumStageCompletion = 0;

            dccsLemurianFamily.maximumStageCompletion = 14;
            dccsLunarFamily.minimumStageCompletion = 3;
            dccsLunarFamily.maximumStageCompletion = 1000000;
            dccsVoidFamily.minimumStageCompletion = 4;

            dccsParentFamily.AddCategory("Champions", 4);
            dccsParentFamily.AddCard(1, DC_Grandparent);
            dccsParentFamily.AddCategory("Basic", 6);
            dccsParentFamily.AddCard(2, DC_Child);
            dccsParentFamily.selectionChatString = "<style=cWorldEvent>[WARNING] A familial bond is being interrupted..</style>";

            dccsGupFamily.categories[0].cards[0].selectionWeight = 3;
            dccsGupFamily.AddCard(0, DC_Geep);
            dccsGupFamily.AddCard(0, DC_Gip);

            //RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGeepBody").directorCreditCost = 60;
            RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody").directorCreditCost = 25;


            //Family Event Changes
            //0 is Normal
            //1 is Family
            //2 is Void
            RoR2.ExpansionManagement.ExpansionDef[] ExpansionNone = { };
            //RoR2.ExpansionManagement.ExpansionDef[] ExpansionDLC1 = { };
            DccsPool.PoolEntry[] NoPoolEntries = { };


            DccsPool.ConditionalPoolEntry FamilyExtraVermin = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsVerminFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraBeetle = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsBeetleFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraClay = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsClayFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraGolemAbyssal = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsGolemFamilyAbyssal, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraParent = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsParentFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraLunar = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsLunarFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraImp = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsImpFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraVoid = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsVoidFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraWorms = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsWormsFamily, requiredExpansions = ExpansionNone };
            DccsPool.Category CategoryFamilyArtifactWorld = new DccsPool.Category { categoryWeight = 0.02f, name = "Family", alwaysIncluded = NoPoolEntries, includedIfNoConditionsMet = NoPoolEntries };
            DccsPool.Category CategoryFamilyMoon2 = new DccsPool.Category { categoryWeight = 0.02f, name = "Family", alwaysIncluded = NoPoolEntries, includedIfNoConditionsMet = NoPoolEntries };

            DccsPool.ConditionalPoolEntry FamilyExtraLarva = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsAcidLarvaFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraMushroom = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsMushroomFamily, requiredExpansions = ExpansionNone };



            dpGolemplainsMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsConstructFamily;

            dpSnowyForestMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsVerminFamilySnowy;
            dpSnowyForestMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsImpFamily;
            //
            dpGooLakeMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsClayFamily;
           
            dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet = dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet.Remove(dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet[0]);
            dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet = dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraMushroom, FamilyExtraLarva);

            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet = dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraVermin, FamilyExtraLunar, FamilyExtraWorms);
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[0].dccs = dccsClayFamily;
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsLemurianFamily;
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet = dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet.Remove(dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[1], dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[2]);
            //
            dpFrozenWallMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsRoboBallFamily;

            dpWispGraveyardMonsters.poolCategories[1].includedIfConditionsMet = dpWispGraveyardMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraClay, FamilyExtraBeetle, FamilyExtraLunar, FamilyExtraWorms);

            dpSulfurPoolsMonsters.poolCategories[1].includedIfConditionsMet = dpSulfurPoolsMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraParent, FamilyExtraLarva);
            //
            //Wouldn't we need to replace the normal Golem Family here or is there just no Golem Family
            dpDampCaveMonsters.poolCategories[1].includedIfConditionsMet = dpDampCaveMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraGolemAbyssal, FamilyExtraParent, FamilyExtraWorms);

            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet = dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet.Remove(dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[4]);
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsRoboBallFamily;
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsConstructFamily;
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet = dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraLarva);

            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet = dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraVermin);
            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsClayFamily;
            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsJellyfishFamily;
            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet = dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraMushroom, FamilyExtraLarva);

            dpSkyMeadowMonsters.poolCategories[1].includedIfConditionsMet[0].weight = 2;
            dpSkyMeadowMonsters.poolCategories[1].includedIfConditionsMet = dpSkyMeadowMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraParent, FamilyExtraWorms);
            //

            dpArtifactWorldMonsters.poolCategories = dpArtifactWorldMonsters.poolCategories.Add(CategoryFamilyArtifactWorld);
            dpArtifactWorldMonsters.poolCategories[1].includedIfConditionsMet = dpArtifactWorldMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraLunar, FamilyExtraImp);

            dpMoonMonsters.poolCategories = dpMoonMonsters.poolCategories.Add(CategoryFamilyMoon2);
            dpMoonMonsters.poolCategories[1].includedIfConditionsMet = dpMoonMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraVoid);

            
            
            DccsPool[] AllDccsPools = { 
            dpGolemplainsMonsters, dpBlackBeachMonsters, dpSnowyForestMonsters,
            dpGooLakeMonsters,dpFoggySwampMonsters,dpAncientLoftMonsters,
            dpFrozenWallMonsters,dpWispGraveyardMonsters,dpSulfurPoolsMonsters,
            dpDampCaveMonsters,dpShipgraveyardMonsters,dpRootJungleMonsters,
            dpSkyMeadowMonsters,dpMoonMonsters,dpArtifactWorldMonsters,dpVoidStageMonsters};

            for (int i = 0; i < AllDccsPools.Length; i++)
            {
                AllDccsPools[i].poolCategories[0].categoryWeight = 1 - WConfig.DCCSEnemyFamilyChance.Value / 100f;
                AllDccsPools[i].poolCategories[1].categoryWeight = WConfig.DCCSEnemyFamilyChance.Value / 100f;
                /*Debug.Log("");
                Debug.Log(AllDccsPools[i]);
                for (int tw = 0; tw < AllDccsPools[i].poolCategories[1].includedIfConditionsMet.Length; tw++)
                {
                    Debug.Log(AllDccsPools[i].poolCategories[1].includedIfConditionsMet[tw].dccs);
                }*/
            }
            
        }

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

        public static void ModSupport()
        {
            FamilyDirectorCardCategorySelection dccsConstructFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsConstructFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsLemurianFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLemurianFamily.asset").WaitForCompletion();

            CharacterSpawnCard[] CSCList = Object.FindObjectsOfType(typeof(CharacterSpawnCard)) as CharacterSpawnCard[];
            for (var i = 0; i < CSCList.Length; i++)
            {
                //Debug.LogWarning(CSCList[i]);
                switch (CSCList[i].name)
                {
                    case "cscSigmaConstruct":
                        if (CSCList[i].directorCreditCost > 0)
                        {
                            DirectorCard DC__Sigma = new DirectorCard
                            {
                                spawnCard = CSCList[i],
                                selectionWeight = 1,
                                minimumStageCompletions = 0,
                                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                            };
                            dccsConstructFamily.AddCategory("Minibosses", 1);
                            dccsConstructFamily.AddCard(2, DC__Sigma);
                        }
                        break;
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
                    case "cscArchWisp":
                        break;
                    case "cscAncientWisp":
                        break;
                    case "cscDireseeker":
                        DirectorCard DC_Direseeker = new DirectorCard
                        {
                            spawnCard = CSCList[i],
                            selectionWeight = 1,
                            preventOverhead = false,
                            minimumStageCompletions = 0,
                            spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                        };
                        dccsLemurianFamily.AddCategory("Champions", 1);
                        dccsLemurianFamily.AddCard(2, DC_Direseeker);
                        break;
                }
            }
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
}