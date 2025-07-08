using R2API;
using Rewired.ControllerExtensions;
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
            GameObject ScavLunar1Master = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavLunar1Master");
            GameObject ScavLunar1Body = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ScavLunar1Body");

            var KipKip = ScavLunar1Master.GetComponent<GivePickupsOnStart>();
            KipKip.enabled = false;
            ScavLunarWSpeedBody = PrefabAPI.InstantiateClone(ScavLunar1Body, "ScavLunarWSpeedBody", true);
            ScavLunarWSpeedMaster =  PrefabAPI.InstantiateClone(ScavLunar1Master, "ScavLunarWSpeedMaster", true);

            ScavLunarWTankBody = R2API.PrefabAPI.InstantiateClone(ScavLunar1Body, "ScavLunarWTankBody", true);
            ScavLunarWTankMaster = R2API.PrefabAPI.InstantiateClone(ScavLunar1Master, "ScavLunarWTankMaster", true);

            ScavLunarWGoomanBody = R2API.PrefabAPI.InstantiateClone(ScavLunar1Body, "ScavLunarWGoomanBody", true);
            ScavLunarWGoomanMaster = R2API.PrefabAPI.InstantiateClone(ScavLunar1Master, "ScavLunarWGoomanMaster", true);
            KipKip.enabled = true;

            ExpansionDef DLC1 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC1/Common/DLC1.asset").WaitForCompletion();
 
            MultiCharacterSpawnCard cscScavLunar = LegacyResourcesAPI.Load<MultiCharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScavLunar");
 
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
            SpeedBody.baseDamage *= 0.6f;
            SpeedBody.levelDamage *= 0.6f;
            SpeedBody.baseJumpPower *= 3f;
            TankBody.baseNameToken = "SCAVLUNAR_SLOW_BODY_NAME";
            TankBody.baseMaxHealth *= 0.5f;
            TankBody.levelMaxHealth *= 0.5f;
            TankBody.baseAttackSpeed *= 1.65f; //He gets half cooldowns
            TankBody.baseMoveSpeed *= 0.8f;
            GoomanBody.baseNameToken = "SCAVLUNAR_GOOBO_BODY_NAME";
            GoomanBody.baseDamage *= 0.75f;
            GoomanBody.levelDamage *= 0.75f;


            On.RoR2.GivePickupsOnStart.Start += DontDuplicateOnGooboGhosts;
             
         //Really need a better way to add it than this
            On.RoR2.LocalUserManager.AddUser += LocalUserManager_AddUser;
        }

        private static void DontDuplicateOnGooboGhosts(On.RoR2.GivePickupsOnStart.orig_Start orig, GivePickupsOnStart self)
        {     
            self.inventory = self.GetComponent<Inventory>();
            if (self.inventory)
            {
                if (self.inventory.GetItemCount(DLC1Content.Items.GummyCloneIdentifier) > 0 || self.inventory.GetItemCount(RoR2Content.Items.Ghost) > 0)
                {
                    return;
                }
            };
            orig(self);
        }

        public static void CallLate()
        {

            var pickups = ScavLunarWSpeedMaster.AddComponent<GivePickupsOnStart>();
            pickups.equipmentDef = RoR2Content.Equipment.FireBallDash;
            pickups.itemDefInfos = new GivePickupsOnStart.ItemDefInfo[] {
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.Hoof, count = 2, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.Syringe, count = 2, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = DLC1Content.Items.AttackSpeedAndMoveSpeed, count = 4, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.WarCryOnMultiKill, count = 2, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.EnergizedOnEquipmentUse, count = 2, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.AttackSpeedOnCrit, count = 2, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.SprintOutOfCombat, count = 2, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = DLC1Content.Items.MoveSpeedOnKill, count = 2, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.Phasing, count = 2, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.AlienHead, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.LunarBadLuck, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.SprintBonus, count = 5, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.FallBoots, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.LunarTrinket, count = 1, },
            };

            var sack = ScavLunarWTankMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[2];
            sack.maxUserHealthFraction = float.PositiveInfinity;
            sack.buttonPressType = RoR2.CharacterAI.AISkillDriver.ButtonPressType.Hold;
            sack.noRepeat = true; //Somehow Hooks breaks his brain
            pickups = ScavLunarWTankMaster.AddComponent<GivePickupsOnStart>();
            pickups.equipmentDef = RoR2Content.Equipment.CrippleWard;
            pickups.itemDefInfos = new GivePickupsOnStart.ItemDefInfo[] {
                new GivePickupsOnStart.ItemDefInfo { itemDef = DLC1Content.Items.OutOfCombatArmor, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.ArmorPlate, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.SlowOnHit, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.IceRing, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = DLC1Content.Items.ImmuneToDebuff, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = DLC1Content.Items.HalfAttackSpeedHalfCooldowns, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = DLC1Content.Items.HalfSpeedDoubleHealth, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.LunarSecondaryReplacement, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.SprintArmor, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.LunarTrinket, count = 1, },
            };


            ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[0].maxUserHealthFraction = 1f;
            //ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[1].shouldFireEquipment = true;
            ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[2].shouldFireEquipment = true;
            ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[2].maxUserHealthFraction = 1f;
            ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[3].shouldFireEquipment = true;
            ScavLunarWGoomanMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>()[4].shouldFireEquipment = true;
            pickups = ScavLunarWGoomanMaster.AddComponent<GivePickupsOnStart>();
            pickups.equipmentDef = DLC1Content.Equipment.GummyClone;
            pickups.itemDefInfos = new GivePickupsOnStart.ItemDefInfo[] {
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.BoostEquipmentRecharge, count = 10, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.EquipmentMagazine, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = DLC1Content.Items.PermanentDebuffOnHit, count = 2, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.RandomDamageZone, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = JunkContent.Items.Incubator, count = 3, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.WardOnLevel, count = 2, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.LevelBonus, count = 1, },
                //new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.GhostOnKill, count = 1, }, //Causes issues with FlatItemBuffs rework. Seemingly would be fine if ghosts dont get items
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.SprintWisp, count = 1, },
                new GivePickupsOnStart.ItemDefInfo { itemDef = RoR2Content.Items.LunarTrinket, count = 1, },
            };

        }

        public static bool addedNewLunarScavs = false;
        public static void LocalUserManager_AddUser(On.RoR2.LocalUserManager.orig_AddUser orig, Rewired.Player inputPlayer, UserProfile userProfile)
        {
            orig(inputPlayer, userProfile);
             
            if (!WConfig.cfgScavNewTwisted.Value)
            {
                return;
            }
            if (addedNewLunarScavs)
            {
                return;
            }
            if (RoR2.EntitlementManagement.EntitlementManager.localUserEntitlementTracker != null)
            {
                bool dlc1 = RoR2.EntitlementManagement.EntitlementManager.localUserEntitlementTracker.AnyUserHasEntitlement(Addressables.LoadAssetAsync<RoR2.EntitlementManagement.EntitlementDef>(key: "RoR2/DLC1/Common/entitlementDLC1.asset").WaitForCompletion());
                bool dlc2 = RoR2.EntitlementManagement.EntitlementManager.localUserEntitlementTracker.AnyUserHasEntitlement(Addressables.LoadAssetAsync<RoR2.EntitlementManagement.EntitlementDef>(key: "RoR2/DLC1/Common/entitlementDLC2.asset").WaitForCompletion());

                //Debug.Log(userProfile.name + " has DLC1 : " + dlc1);
                //Debug.Log(userProfile.name + " has DLC2 : " + dlc2);
 
                if (dlc1 && dlc2)
                {
                    addedNewLunarScavs = true;
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