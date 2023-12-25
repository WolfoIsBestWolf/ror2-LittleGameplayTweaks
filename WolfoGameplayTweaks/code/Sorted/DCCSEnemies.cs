using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Navigation;
//using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
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
                EnemiesPreLoopNoDLC();
                EnemiesPreLoopDLC1();
            }
            if (WConfig.DCCSEnemyChangesLooping.Value == true)
            {
                EnemiesPostLoop();
            }
            if (WConfig.DCCSEnemyNewFamilies.Value == true)
            {
                Families();
            }
            //This doesn't work anymore
            ClassicStageInfo.monsterFamilyChance = WConfig.DCCSEnemyFamilyChance.Value/100f;
        }

        public static void EnemiesPreLoopNoDLC()
        {
            DirectorCardCategorySelection dccsFrozenWallMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsRootJungleMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleMonsters.asset").WaitForCompletion();

            DirectorCard DSGrovetender = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSRoboBallBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            dccsFrozenWallMonstersDLC1.categories[0].cards[0] = DSRoboBallBoss; //Clay Dunestrider replaced by Solus Unit

            dccsRootJungleMonstersDLC1.categories[0].cards[1] = DSGrovetender; //Titan replaced by Grovetender
        }


        public static void EnemiesPreLoopDLC1()
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


            DirectorCard DSBison = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBison"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSMagmaWorm = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DSElectricWorm = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DSGrovetender = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSRoboBallBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSHermitCrab = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSAcidLarva = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscAcidLarva"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSClayGrenadier = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            //dccsGolemplainsMonstersDLC1

            dccsSnowyForestMonstersDLC1.categories[1].cards = dccsSnowyForestMonstersDLC1.categories[1].cards.Remove(dccsSnowyForestMonstersDLC1.categories[1].cards[2]); //Remove Reaver
            dccsSnowyForestMonstersDLC1.categories[1].cards[1] = DSBison; //Bison replaces Greater Wisp
            dccsSnowyForestMonstersDLC1.categories[2].cards[3].minimumStageCompletions = 0; //Pre Loop Vermin
            dccsSnowyForestMonstersDLC1.categories[2].cards = dccsSnowyForestMonstersDLC1.categories[2].cards.Remove(dccsSnowyForestMonstersDLC1.categories[2].cards[1]); //Remove Wisp

            dccsGooLakeMonstersDLC1.categories[1].cards[3].minimumStageCompletions = 0; //Pre Loop Clay Grenadier

            dccsFrozenWallMonstersDLC1.categories[0].cards[0] = DSRoboBallBoss; //Clay Dunestrider replaced by Solus Unit

            dccsWispGraveyardMonstersDLC1.AddCard(1, DSClayGrenadier);

            //dccsSulfurPoolsMonstersDLC1.categories[0].cards[2] = DSMagmaWorm; //Beetle Queen replaced by Magma Worm
            dccsSulfurPoolsMonstersDLC1.categories[2].cards[2] = DSAcidLarva; //Beetle replaced by Acid Larva, Too many beetles and ugly green thing fits into ugly green place
            dccsSulfurPoolsMonstersDLC1.categories[0].cards[1].selectionWeight = 2;
            dccsSulfurPoolsMonstersDLC1.AddCard(0, DSMagmaWorm); //Keep Beetle Queen but still add this dude
            dccsSulfurPoolsMonstersDLC1.AddCard(0, DSElectricWorm); //Keep Beetle Queen but still add this dude
            dccsSulfurPoolsMonstersDLC1.AddCard(1, DSHermitCrab); //Seems fitting for another trash mob

            dccsRootJungleMonstersDLC1.categories[0].cards[1] = DSGrovetender; //Titan replaced by Grovetender
            dccsRootJungleMonstersDLC1.categories[1].cards[0].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Golem/cscGolemNature.asset").WaitForCompletion();

            dccsShipgraveyardMonstersDLC1.AddCard(0, DSElectricWorm); //Magma Worm so Elec Worm

            dccsSkyMeadowMonstersDLC1.AddCard(0, DSMagmaWorm); //They removed him with the DLC but Elec Worm is still there

            dccsArtifactWorldMonstersDLC1.AddCard(0, DSGrovetender);
            dccsArtifactWorldMonstersDLC1.AddCard(0, DSMagmaWorm);
            dccsArtifactWorldMonstersDLC1.AddCard(1, DSClayGrenadier);
            //dccsArtifactWorldMonstersDLC1.AddCard(2, DSBlindVermin);





        }
        
        public static void EnemiesPostLoop()
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
                selectionWeight = 1,
                minimumStageCompletions = 4,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopGrandparent = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 1,
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
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarGolem = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarWisp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 7,
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

            dccsWispGraveyardMonstersDLC1.categories[2].cards[3].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/Vermin/cscVermin.asset").WaitForCompletion(); 
            //Idk why we replace Vultures with Rats 

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

            dccsDampCaveMonstersDLC1.AddCard(0, DoubleLoopVoidDevestator);
            dccsShipgraveyardMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);
            dccsRootJungleMonstersDLC1.AddCard(1, DoubleLoopVoidJailer);

            dccsSkyMeadowMonstersDLC1.AddCard(0, DoubleLoopVoidDevestator);
            //dccsSkyMeadowMonstersDLC1.AddCard(1, DoubleLoopVoidJailer);
            //dccsSkyMeadowMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);

        }
        

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
            //FamilyDirectorCardCategorySelection dccsGolemFamilySimu = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGolemFamily.asset").WaitForCompletion();
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
            //FamilyDirectorCardCategorySelection dccsMushroomFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsMushroomFamily.asset").WaitForCompletion();
            //FamilyDirectorCardCategorySelection dccsAcidLarvaFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsAcidLarvaFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsRoboBallFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
            FamilyDirectorCardCategorySelection dccsVerminFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
            FamilyDirectorCardCategorySelection dccsVerminFamilySnowy = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
            FamilyDirectorCardCategorySelection dccsWormsFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();



        DirectorCard DSTitanDampCaves = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscTitanDampCave"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DSClayBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBoss"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSClayTemp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBruiser"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSClayGrenadier = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DSRoboBallBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSRoboBallMini = new DirectorCard
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
            DirectorCard DSVultureNoCeling = new DirectorCard
            {
                spawnCard = cscVultureNoCeiling,
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DSVultureNoCeling.spawnCard = cscVultureNoCeiling;
            //Debug.LogWarning(DSVultureNoCeling.spawnCard);


            DirectorCard DSGrandparent = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DSGeep = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGeepBody"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DSGip = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DSBlindPest = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVermin"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSBlindVermin = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVermin"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSBlindPestSnowy = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVerminSnowy"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSBlindVerminSnowy = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVerminSnowy"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            CharacterSpawnCard cscMagmaWormElite = Object.Instantiate(RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm"));
            CharacterSpawnCard cscElectricWormElite = Object.Instantiate(RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"));

            cscMagmaWormElite.name = "cscMagmaWormElite";
            cscMagmaWormElite.noElites = false;
            cscElectricWormElite.name = "cscElectricWormElite";
            cscElectricWormElite.noElites = false;

            DirectorCard DSMagmaWormWithElite = new DirectorCard
            {
                spawnCard = cscMagmaWormElite,
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DSElectricWormWithElite = new DirectorCard
            {
                spawnCard = cscElectricWormElite,
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };



            dccsGolemFamilyAbyssal.categories[0].cards[0] = DSTitanDampCaves;
            dccsGolemFamilyAbyssal.name = "dccsGolemFamilyAbyssal";



            dccsClayFamily.AddCategory("Champions", 3);
            dccsClayFamily.AddCategory("Minibosses", 6);
            dccsClayFamily.AddCard(0, DSClayBoss);
            dccsClayFamily.AddCard(1, DSClayTemp);
            dccsClayFamily.AddCard(1, DSClayGrenadier);
            dccsClayFamily.name = "dccsClayFamily";
            dccsClayFamily.minimumStageCompletion = 0;
            dccsClayFamily.maximumStageCompletion = 1000000;
            dccsClayFamily.selectionChatString = "<style=cWorldEvent>[WARNING] You feel parasitic influences..</style>";

            dccsRoboBallFamily.AddCategory("Champions", 4);
            dccsRoboBallFamily.AddCategory("Minibosses", 4);
            dccsRoboBallFamily.AddCategory("Basic Monsters", 4);
            dccsRoboBallFamily.AddCard(0, DSRoboBallBoss);
            dccsRoboBallFamily.AddCard(1, DSRoboBallMini);
            dccsRoboBallFamily.AddCard(2, DSVultureNoCeling);
            dccsRoboBallFamily.name = "dccsRoboBallFamily";
            dccsRoboBallFamily.minimumStageCompletion = 0;
            dccsRoboBallFamily.maximumStageCompletion = 1000000;
            dccsRoboBallFamily.selectionChatString = "<style=cWorldEvent>[WARNING] You hear the whirring of wings and machinery..</style>";

            dccsVerminFamily.AddCategory("Basic Monsters", 6);
            dccsVerminFamily.AddCard(0, DSBlindPest);
            dccsVerminFamily.AddCard(0, DSBlindVermin);
            dccsVerminFamily.name = "dccsVerminFamily";
            dccsVerminFamily.minimumStageCompletion = 0;
            dccsVerminFamily.maximumStageCompletion = 9;
            dccsVerminFamily.selectionChatString = "<style=cWorldEvent>[WARNING] You have invaded rampant breeding grounds..</style>";

            dccsVerminFamilySnowy.AddCategory("Basic Monsters", 6);
            dccsVerminFamilySnowy.AddCard(0, DSBlindPestSnowy);
            dccsVerminFamilySnowy.AddCard(0, DSBlindVerminSnowy);
            dccsVerminFamilySnowy.name = "dccsVerminSnowyFamily";
            dccsVerminFamilySnowy.minimumStageCompletion = 0;
            dccsVerminFamilySnowy.maximumStageCompletion = 9;
            dccsVerminFamilySnowy.selectionChatString = "<style=cWorldEvent>[WARNING] You have invaded rampant breeding grounds..</style>";
            //<style=cWorldEvent>[WARNING] You hear squeaks and chirps around you..</style>

            dccsWormsFamily.AddCategory("Champions", 5);
            dccsWormsFamily.AddCard(0, DSMagmaWormWithElite);
            dccsWormsFamily.AddCard(0, DSElectricWormWithElite);
            dccsWormsFamily.minimumStageCompletion = 9;
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
            dccsParentFamily.AddCard(1, DSGrandparent);
            dccsParentFamily.selectionChatString = "<style=cWorldEvent>[WARNING] A familial bond is being interrupted..</style>";

            dccsGupFamily.categories[0].cards[0].selectionWeight = 3;
            dccsGupFamily.AddCard(0, DSGeep);
            dccsGupFamily.AddCard(0, DSGip);

            RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGeepBody").directorCreditCost = 60;
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




            dpGolemplainsMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsConstructFamily;

            dpSnowyForestMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsVerminFamilySnowy;
            dpSnowyForestMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsImpFamily;
            //
            dpGooLakeMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsClayFamily;
           
            dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet = dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet.Remove(dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet[0]);

            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet = dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraVermin, FamilyExtraLunar, FamilyExtraWorms);
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[0].dccs = dccsClayFamily;
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsLemurianFamily;
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet = dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet.Remove(dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[1], dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[2]);
            //
            dpFrozenWallMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsRoboBallFamily;

            dpWispGraveyardMonsters.poolCategories[1].includedIfConditionsMet = dpWispGraveyardMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraClay, FamilyExtraBeetle, FamilyExtraLunar, FamilyExtraWorms);

            dpSulfurPoolsMonsters.poolCategories[1].includedIfConditionsMet = dpSulfurPoolsMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraParent);
            //
            //Wouldn't we need to replace the normal Golem Family here or is there just no Golem Family
            dpDampCaveMonsters.poolCategories[1].includedIfConditionsMet = dpDampCaveMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraGolemAbyssal, FamilyExtraParent, FamilyExtraWorms);

            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet = dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet.Remove(dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[4]);
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsRoboBallFamily;
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsConstructFamily;

            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet = dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraVermin);
            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsClayFamily;
            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsJellyfishFamily;

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
                            DirectorCard DS_Sigma = new DirectorCard
                            {
                                spawnCard = CSCList[i],
                                selectionWeight = 1,
                                minimumStageCompletions = 0,
                                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                            };
                            dccsConstructFamily.AddCategory("Minibosses", 1);
                            dccsConstructFamily.AddCard(2, DS_Sigma);
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
                        dccsClayFamily.AddCategory("Basic Monsters", 6);
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
    }
}