using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
 
namespace LittleGameplayTweaks
{  
    public class DCCS_Credits
    {
        public static void Start()
        {


            //I should do this better
            if (WConfig.cfgCredits_Monsters.Value)
            {
                //Lower cost of various mobs by ~10% so they show up more.
                //Most of these are not really that strong or oppressive and mod makes the game lil easier.

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Grandparent/cscGrandparent.asset").WaitForCompletion().directorCreditCost = 1000; //1035 would match 600|2100 800|2800 credit|hp ratio. No reason why he should cost extra extra
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscParent").directorCreditCost = 80; //100, Overpriced because was nerfed but not adjusted
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC2/Child/cscChild.asset").WaitForCompletion().directorCreditCost = 30; //Pretty bad at hitting people and slightly nerfed in mod without Invul

                Addressables.LoadAssetAsync<SpawnCard>(key: "a19470701f77bd945bef064b8890c14b").WaitForCompletion().directorCreditCost = 130; //cscClayGrenadier  //Match Elder Lem
                Addressables.LoadAssetAsync<SpawnCard>(key: "04a333710013a3e449cbbb494f96145f").WaitForCompletion().directorCreditCost = 130; //cscClayBruiser //Match Elder Lem

                //Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/GreaterWisp/cscGreaterWisp.asset").WaitForCompletion().directorCreditCost = 180; //200, A bit pricy but he's strong

                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier").directorCreditCost = 260; //300, he just sucks dude.
                                                                                                                                     //Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidBarnacle/cscVoidBarnacle.asset").WaitForCompletion().directorCreditCost = 30; //The floor version only costs 30 credits?

            }

            if (WConfig.cfgCredits_Interactables.Value == true)
            {

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/DeepVoidPortalBattery/iscDeepVoidPortalBattery.asset").WaitForCompletion().directorCreditCost = 0; //Idk if this cost matters but it should be 0

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/DuplicatorWild/iscDuplicatorWild.asset").WaitForCompletion().directorCreditCost = 15; //10 Default
 
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidTriple/iscVoidTriple.asset").WaitForCompletion().directorCreditCost = 30; //White Multishop costs 20 why the fuck does this cost 40
 
                //Void Barrel is the VoidSeed filler
                //Addressables.LoadAssetAsync<SpawnCard>(key: "49eb4eedc03a0e746a643c3b6051bfc4").WaitForCompletion().directorCreditCost = 10; //15 iscVoidCoinBarrel
 
                //Lunar Chests often dont give you anything useful so they shouldnt be that costly
                Addressables.LoadAssetAsync<SpawnCard>(key: "d21f2d3075f064e4081a41a368c505b1").WaitForCompletion().directorCreditCost = 20; //25 iscLunarChest
 
                //Healing Turret ass shrine
                Addressables.LoadAssetAsync<SpawnCard>(key: "caab08f30f159b54f92e7d42b4b1d717").WaitForCompletion().directorCreditCost = 10; //15 iscShrineHealing

                //Order
                //Costing 30 is insulting, bro forgot they could just make it rare instead of expensive.
                Addressables.LoadAssetAsync<SpawnCard>(key: "ba9d25d63bbcef34a9077c08a6d6df95").WaitForCompletion().directorCreditCost = 5; //iscShrineRestack
                Addressables.LoadAssetAsync<SpawnCard>(key: "3547e84f7f2c8064ba91cc54e517f5b9").WaitForCompletion().directorCreditCost = 5; //iscShrineRestack
                Addressables.LoadAssetAsync<SpawnCard>(key: "0e981358f6bf4de4e83e30286ad5df75").WaitForCompletion().directorCreditCost = 5; //iscShrineRestack
                Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "ba9d25d63bbcef34a9077c08a6d6df95").WaitForCompletion().maxSpawnsPerStage = 1; //iscShrineRestack
                Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "3547e84f7f2c8064ba91cc54e517f5b9").WaitForCompletion().maxSpawnsPerStage = 1; //iscShrineRestack
                Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "0e981358f6bf4de4e83e30286ad5df75").WaitForCompletion().maxSpawnsPerStage = 1; //iscShrineRestack
                
                //Blood, doesn't give much money
                Addressables.LoadAssetAsync<SpawnCard>(key: "a6d01afb758a15940bf09deb9db44067").WaitForCompletion().directorCreditCost = 15;//iscShrineBlood
                Addressables.LoadAssetAsync<SpawnCard>(key: "94a96af94cc91294fab616f523ce58b5").WaitForCompletion().directorCreditCost = 15;//iscShrineBlood
                Addressables.LoadAssetAsync<SpawnCard>(key: "3b3c5b543ce972e4d963cdfeafdc955f").WaitForCompletion().directorCreditCost = 15;//iscShrineBlood
 

            }
 
        }

    
     
 
 
    }
}