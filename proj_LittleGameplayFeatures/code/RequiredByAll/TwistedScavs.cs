﻿using R2API;
using RoR2;
using RoR2.ExpansionManagement;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LittleGameplayFeatures
{
    public class TwistedScavs
    {
        public static GameObject ScavLunarWSpeedBody;
        public static GameObject ScavLunarWSpeedMaster;

        public static GameObject ScavLunarWTankBody;
        public static GameObject ScavLunarWTankMaster;

        public static GameObject ScavLunarWGoomanBody;
        public static GameObject ScavLunarWGoomanMaster;


        public static void Start()
        {
            ScavLunarWSpeedBody = R2API.PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ScavLunar1Body"), "ScavLunarWSpeedBody", true);
            ScavLunarWSpeedMaster = R2API.PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavLunar1Master"), "ScavLunarWSpeedMaster", true);

            ScavLunarWTankBody = R2API.PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ScavLunar1Body"), "ScavLunarWTankBody", true);
            ScavLunarWTankMaster = R2API.PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavLunar1Master"), "ScavLunarWTankMaster", true);

            ScavLunarWGoomanBody = R2API.PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ScavLunar1Body"), "ScavLunarWGoomanBody", true);
            ScavLunarWGoomanMaster = R2API.PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavLunar1Master"), "ScavLunarWGoomanMaster", true);

            ExpansionDef DLC1 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC1/Common/DLC1.asset").WaitForCompletion();


            CharacterBody TwipTwip = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ScavLunar3Body").GetComponent<CharacterBody>();
            TwipTwip.baseMaxHealth *= 0.8f;
            TwipTwip.levelMaxHealth *= 0.8f;

            MultiCharacterSpawnCard cscScavLunar = LegacyResourcesAPI.Load<MultiCharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScavLunar");

            cscScavLunar.masterPrefabs[0].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath;
            cscScavLunar.masterPrefabs[1].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath;
            cscScavLunar.masterPrefabs[2].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath;
            cscScavLunar.masterPrefabs[3].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath;

            ContentAddition.AddBody(ScavLunarWSpeedBody);
            ContentAddition.AddMaster(ScavLunarWSpeedMaster);

            ContentAddition.AddBody(ScavLunarWTankBody);
            ContentAddition.AddMaster(ScavLunarWTankMaster);

            ContentAddition.AddBody(ScavLunarWGoomanBody);
            ContentAddition.AddMaster(ScavLunarWGoomanMaster);

            ScavLunarWGoomanBody.AddComponent<ExpansionRequirementComponent>().requiredExpansion = DLC1;
            ScavLunarWTankBody.AddComponent<ExpansionRequirementComponent>().requiredExpansion = DLC1;
            ScavLunarWSpeedBody.AddComponent<ExpansionRequirementComponent>().requiredExpansion = DLC1;


            ScavLunarWSpeedMaster.GetComponent<CharacterMaster>().bodyPrefab = ScavLunarWSpeedBody;
            ScavLunarWTankMaster.GetComponent<CharacterMaster>().bodyPrefab = ScavLunarWTankBody;
            ScavLunarWGoomanMaster.GetComponent<CharacterMaster>().bodyPrefab = ScavLunarWGoomanBody;

            CharacterBody SpeedBody = ScavLunarWSpeedBody.GetComponent<CharacterBody>();
            CharacterBody TankBody = ScavLunarWTankBody.GetComponent<CharacterBody>();
            CharacterBody GoomanBody = ScavLunarWGoomanBody.GetComponent<CharacterBody>();


            SpeedBody.baseNameToken = "SCAVLUNAR_FAST_BODY_NAME";
            SpeedBody.baseMoveSpeed *= 1.75f;
            SpeedBody.baseDamage *= 0.75f;
            SpeedBody.levelDamage *= 0.75f;
            SpeedBody.baseJumpPower *= 3f;
            TankBody.baseNameToken = "SCAVLUNAR_SLOW_BODY_NAME";
            TankBody.baseMaxHealth *= 0.5f;
            TankBody.levelMaxHealth *= 0.5f;
            TankBody.baseAttackSpeed *= 1.65f; //He gets half cooldowns
            TankBody.baseMoveSpeed *= 0.8f;
            GoomanBody.baseNameToken = "SCAVLUNAR_GOOBO_BODY_NAME";
            GoomanBody.baseDamage *= 0.75f;
            GoomanBody.levelDamage *= 0.75f;


            cscScavLunar.masterPrefabs[0].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().baseDamage *= 0.75f;
            cscScavLunar.masterPrefabs[1].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().baseDamage *= 0.75f;
            cscScavLunar.masterPrefabs[2].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().baseDamage *= 0.75f;
            cscScavLunar.masterPrefabs[3].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().baseDamage *= 0.75f;
            cscScavLunar.masterPrefabs[0].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().levelDamage *= 0.75f;
            cscScavLunar.masterPrefabs[1].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().levelDamage *= 0.75f;
            cscScavLunar.masterPrefabs[2].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().levelDamage *= 0.75f;
            cscScavLunar.masterPrefabs[3].GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().levelDamage *= 0.75f;
            SpeedBody.baseDamage *= 0.75f;
            SpeedBody.levelDamage *= 0.75f;
            TankBody.baseDamage *= 0.75f;
            TankBody.levelDamage *= 0.75f;
            GoomanBody.baseDamage *= 0.75f;
            GoomanBody.levelDamage *= 0.75f;


            ScavLunarWSpeedMaster.GetComponent<GivePickupsOnStart>().equipmentString = "FireBallDash";
            ScavLunarWSpeedMaster.GetComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("Hoof"), count = 10, },
                new GivePickupsOnStart.ItemInfo { itemString = ("Syringe"), count = 3, },
                new GivePickupsOnStart.ItemInfo { itemString = ("WarCryOnMultiKill"), count = 2, },
                new GivePickupsOnStart.ItemInfo { itemString = ("EnergizedOnEquipmentUse"), count = 4, },
                new GivePickupsOnStart.ItemInfo { itemString = ("AttackSpeedOnCrit"), count = 2, },
                new GivePickupsOnStart.ItemInfo { itemString = ("SprintOutOfCombat"), count = 4, },
                new GivePickupsOnStart.ItemInfo { itemString = ("MoveSpeedOnKill"), count = 5, },
                new GivePickupsOnStart.ItemInfo { itemString = ("Phasing"), count = 3, },
                new GivePickupsOnStart.ItemInfo { itemString = ("AlienHead"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarBadLuck"), count = 1, },
                //Decoration
                new GivePickupsOnStart.ItemInfo { itemString = ("SprintBonus"), count = 5, },
                new GivePickupsOnStart.ItemInfo { itemString = ("AttackSpeedAndMoveSpeed"), count = 2, },
                new GivePickupsOnStart.ItemInfo { itemString = ("FallBoots"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarTrinket"), count = 1, },
            };

            ScavLunarWTankMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[2].maxUserHealthFraction = 1f;
            ScavLunarWTankMaster.GetComponent<GivePickupsOnStart>().equipmentString = "CrippleWard";
            ScavLunarWTankMaster.GetComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                //new GivePickupsOnStart.ItemInfo { itemString = ("Bear"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("OutOfCombatArmor"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("ArmorPlate"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("SlowOnHit"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("IceRing"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("ImmuneToDebuff"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("HalfAttackSpeedHalfCooldowns"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("HalfSpeedDoubleHealth"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("SecondarySkillMagazine"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarSecondaryReplacement"), count = 1, },
                //Decorations     
                 new GivePickupsOnStart.ItemInfo { itemString = ("BarrierOnKill"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("SprintArmor"), count = 1, },
                //new GivePickupsOnStart.ItemInfo { itemString = ("BarrierOnOverHeal"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarTrinket"), count = 1, },
            };


            ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[0].maxUserHealthFraction = 1f;
            //ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[1].shouldFireEquipment = true;
            ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[2].shouldFireEquipment = true;
            ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[2].maxUserHealthFraction = 1f;
            ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[3].shouldFireEquipment = true;
            ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[4].shouldFireEquipment = true;
            ScavLunarWGoomanMaster.GetComponent<GivePickupsOnStart>().equipmentString = "GummyClone";
            ScavLunarWGoomanMaster.GetComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("BoostEquipmentRecharge"), count = 10, },
                new GivePickupsOnStart.ItemInfo { itemString = ("EquipmentMagazine"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("PermanentDebuffOnHit"), count = 2, },
                new GivePickupsOnStart.ItemInfo { itemString = ("RandomDamageZone"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("Incubator"), count = 3, },
                new GivePickupsOnStart.ItemInfo { itemString = ("WardOnLevel"), count = 2, },
                new GivePickupsOnStart.ItemInfo { itemString = ("LevelBonus"), count = 1, },
                //Decorations
                //new GivePickupsOnStart.ItemInfo { itemString = ("RegeneratingScrap"), count = 5, },
                //new GivePickupsOnStart.ItemInfo { itemString = ("GhostOnKill"), count = 1, }, //Causes issues with FlatItemBuffs rework. Seemingly would be fine if ghosts dont get items
                new GivePickupsOnStart.ItemInfo { itemString = ("SprintWisp"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarTrinket"), count = 1, },
            };



            //KipKip
            cscScavLunar.masterPrefabs[0].AddComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarTrinket"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("FlatHealth"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("TPHealingNova"), count = 1, },
            };

            //WipWip
            cscScavLunar.masterPrefabs[1].AddComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarTrinket"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("DeathMark"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("Bandolier"), count = 1, },
            };

            //Twiptwip
            cscScavLunar.masterPrefabs[2].AddComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarTrinket"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("HeadHunter"), count = 1, },
            };

            //Guragura
            cscScavLunar.masterPrefabs[3].AddComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarTrinket"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("BonusGoldPackOnKill"), count = 10, },
            };

            On.RoR2.GivePickupsOnStart.Start += (orig, self) =>
            {
                orig(self);
                if (self.inventory)
                {
                    if (self.inventory.GetItemCount(DLC1Content.Items.GummyCloneIdentifier) > 0 || self.inventory.GetItemCount(RoR2Content.Items.Ghost) > 0)
                    {
                        self.inventory.SetEquipmentIndex(EquipmentIndex.None);
                    }
                };
            };

           //cscScavLunar.masterPrefabs = cscScavLunar.masterPrefabs.Add(TwistedScavs.ScavLunarWGoomanMaster, TwistedScavs.ScavLunarWSpeedMaster, TwistedScavs.ScavLunarWTankMaster);
            On.RoR2.LocalUserManager.AddUser += LocalUserManager_AddUser;
        }

        public static void LocalUserManager_AddUser(On.RoR2.LocalUserManager.orig_AddUser orig, Rewired.Player inputPlayer, UserProfile userProfile)
        {
            orig(inputPlayer, userProfile);
            On.RoR2.LocalUserManager.AddUser -= LocalUserManager_AddUser;
            if (!WConfig.cfgScavNewTwisted.Value)
            {
                return;
            }

            if (RoR2.EntitlementManagement.EntitlementManager.localUserEntitlementTracker != null)
            {

                bool dlc1 = RoR2.EntitlementManagement.EntitlementManager.localUserEntitlementTracker.AnyUserHasEntitlement(Addressables.LoadAssetAsync<RoR2.EntitlementManagement.EntitlementDef>(key: "RoR2/DLC1/Common/entitlementDLC1.asset").WaitForCompletion());
                bool dlc2 = RoR2.EntitlementManagement.EntitlementManager.localUserEntitlementTracker.AnyUserHasEntitlement(Addressables.LoadAssetAsync<RoR2.EntitlementManagement.EntitlementDef>(key: "RoR2/DLC1/Common/entitlementDLC2.asset").WaitForCompletion());

                Debug.Log(userProfile.name + " has DLC1 : " + dlc1);
                Debug.Log(userProfile.name + " has DLC2 : " + dlc2);
 
                if (dlc1 && dlc2)
                {
                    GameObject[] newA = new GameObject[]
                    {
                        ScavLunarWSpeedMaster,
                        ScavLunarWGoomanMaster,
                        ScavLunarWTankMaster
                    };
                    MultiCharacterSpawnCard cscScavLunar = LegacyResourcesAPI.Load<MultiCharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScavLunar");
                    cscScavLunar.masterPrefabs = HG.ArrayUtils.Join<GameObject>(cscScavLunar.masterPrefabs, newA);
                }
            }
        }


    }

}