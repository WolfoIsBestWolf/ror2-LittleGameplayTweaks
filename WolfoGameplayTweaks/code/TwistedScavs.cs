using EntityStates;
using R2API;
using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class TwistedScavs
    {
        public static GameObject ScavLunarWSpeedBody = R2API.PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ScavLunar1Body"), "ScavLunarWSpeedBody", true);
        public static GameObject ScavLunarWSpeedMaster = R2API.PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavLunar1Master"), "ScavLunarWSpeedMaster", true);

        public static GameObject ScavLunarWTankBody = R2API.PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ScavLunar1Body"), "ScavLunarWTankBody", true);
        public static GameObject ScavLunarWTankMaster = R2API.PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavLunar1Master"), "ScavLunarWTankMaster", true);

        public static GameObject ScavLunarWGoomanBody = R2API.PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ScavLunar1Body"), "ScavLunarWGoomanBody", true);
        public static GameObject ScavLunarWGoomanMaster = R2API.PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavLunar1Master"), "ScavLunarWGoomanMaster", true);


        public static void Start()
        {
            MultiCharacterSpawnCard cscScavLunar = LegacyResourcesAPI.Load<MultiCharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScavLunar");

            ContentAddition.AddBody(ScavLunarWSpeedBody);
            ContentAddition.AddMaster(ScavLunarWSpeedMaster);

            ContentAddition.AddBody(ScavLunarWTankBody);
            ContentAddition.AddMaster(ScavLunarWTankMaster);

            ContentAddition.AddBody(ScavLunarWGoomanBody);
            ContentAddition.AddMaster(ScavLunarWGoomanMaster);


            ScavLunarWSpeedMaster.GetComponent<CharacterMaster>().bodyPrefab = ScavLunarWSpeedBody;
            ScavLunarWTankMaster.GetComponent<CharacterMaster>().bodyPrefab = ScavLunarWTankBody;
            ScavLunarWGoomanMaster.GetComponent<CharacterMaster>().bodyPrefab = ScavLunarWGoomanBody;

            CharacterBody SpeedBody = ScavLunarWSpeedBody.GetComponent<CharacterBody>();
            CharacterBody TankBody = ScavLunarWTankBody.GetComponent<CharacterBody>();
            CharacterBody GoomanBody = ScavLunarWGoomanBody.GetComponent<CharacterBody>();


            SpeedBody.baseNameToken = "Feefee the Nimble";
            SpeedBody.baseMoveSpeed *= 2f;
            TankBody.baseNameToken = "Dobdob the Stagnant";
            TankBody.baseMaxHealth *= 0.4f; //Gets Bear
            TankBody.levelMaxHealth *= 0.4f;
            TankBody.baseAttackSpeed *= 1.6f; //He gets half cooldowns
            TankBody.baseMoveSpeed *= 0.8f;
            GoomanBody.baseNameToken = "Quabquab the Lonesome";


            ScavLunarWSpeedMaster.GetComponent<GivePickupsOnStart>().equipmentString = "FireBallDash";
            ScavLunarWSpeedMaster.GetComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("Hoof"), count = 10, },
                new GivePickupsOnStart.ItemInfo { itemString = ("Syringe"), count = 3, },
                new GivePickupsOnStart.ItemInfo { itemString = ("WarCryOnMultiKill"), count = 2, },
                new GivePickupsOnStart.ItemInfo { itemString = ("EnergizedOnEquipmentUse"), count = 4, },
                new GivePickupsOnStart.ItemInfo { itemString = ("AttackSpeedOnCrit"), count = 2, },
                new GivePickupsOnStart.ItemInfo { itemString = ("SprintOutOfCombat"), count = 4, },
                new GivePickupsOnStart.ItemInfo { itemString = ("Phasing"), count = 3, },
                new GivePickupsOnStart.ItemInfo { itemString = ("AlienHead"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarBadLuck"), count = 1, },
                //Decoration
                new GivePickupsOnStart.ItemInfo { itemString = ("SprintBonus"), count = 5, },
                new GivePickupsOnStart.ItemInfo { itemString = ("AttackSpeedAndMoveSpeed"), count = 2, },
                new GivePickupsOnStart.ItemInfo { itemString = ("FallBoots"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarTrinket"), count = 1, },
            };

            ScavLunarWTankMaster.GetComponent<GivePickupsOnStart>().equipmentString = "CrippleWard";
            ScavLunarWTankMaster.GetComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("Bear"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("OutOfCombatArmor"), count = 3, },
                new GivePickupsOnStart.ItemInfo { itemString = ("SecondarySkillMagazine"), count = 3, },
                new GivePickupsOnStart.ItemInfo { itemString = ("ArmorPlate"), count = 3, },
                new GivePickupsOnStart.ItemInfo { itemString = ("SlowOnHit"), count = 3, },
                new GivePickupsOnStart.ItemInfo { itemString = ("IceRing"), count = 2, },
                new GivePickupsOnStart.ItemInfo { itemString = ("ImmuneToDebuff"), count = 2, },
                new GivePickupsOnStart.ItemInfo { itemString = ("HalfAttackSpeedHalfCooldowns"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("HalfSpeedDoubleHealth"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarSecondaryReplacement"), count = 1, },
                //Decorations     
                new GivePickupsOnStart.ItemInfo { itemString = ("SprintArmor"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("BarrierOnOverHeal"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("LunarTrinket"), count = 1, },
            };

            ScavLunarWGoomanMaster.GetComponent<RoR2.CharacterAI.AISkillDriver>().maxUserHealthFraction = 0.9f;
            ScavLunarWGoomanMaster.GetComponent<GivePickupsOnStart>().equipmentString = "GummyClone";
            ScavLunarWGoomanMaster.GetComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("BoostEquipmentRecharge"), count = 28, },
                new GivePickupsOnStart.ItemInfo { itemString = ("RegeneratingScrap"), count = 5, },
                new GivePickupsOnStart.ItemInfo { itemString = ("PermanentDebuffOnHit"), count = 2, },
                new GivePickupsOnStart.ItemInfo { itemString = ("RandomDamageZone"), count = 1, },
                //Decorations
                new GivePickupsOnStart.ItemInfo { itemString = ("GhostOnKill"), count = 1, },
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

        }




    }

}