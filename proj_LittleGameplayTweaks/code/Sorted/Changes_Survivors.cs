using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using RoR2;
using RoR2.CharacterAI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class Changes_Survivors
    {
        public static void Start()
        {
            On.RoR2.CharacterMaster.GetDeployableSameSlotLimit += Captain3Beacon;
 

            //Hold down button to fire multiple
            Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>(key: "RoR2/Base/Commando/CommandoBodyFireFMJ.asset").WaitForCompletion().mustKeyPress = false;
            Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>(key: "RoR2/Base/Croco/CrocoSpit.asset").WaitForCompletion().mustKeyPress = false;
            Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>(key: "RoR2/Base/LunarSkillReplacements/LunarSecondaryReplacement.asset").WaitForCompletion().mustKeyPress = true;

            //Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>(key: "RoR2/Base/Mage/MageBodyNovaBomb.asset").WaitForCompletion().mustKeyPress = false;
            //Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>(key: "RoR2/Base/Mage/MageBodyIceBomb.asset").WaitForCompletion().mustKeyPress = false;
            //Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>(key: "RoR2/DLC2/Seeker/SeekerBodySoulSpiral.asset").WaitForCompletion().mustKeyPress = false;
            //Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>(key: "RoR2/DLC2/Chef/ChefIceBox.asset").WaitForCompletion().mustKeyPress = false;
            //Addressables.LoadAssetAsync<RoR2.Skills.SkillDef>(key: "RoR2/DLC2/FalseSon/FalseSonLunarStake.asset").WaitForCompletion().mustKeyPress = false;


            //This should like Barley Change anything but still make it configs
            /*On.EntityStates.Merc.Weapon.GroundLight2.OnEnter += (orig, self) =>
            {
                if (self.isComboFinisher == true)
                {
                    self.ignoreAttackSpeed = true;
                }
                orig(self);
            };*/
            On.EntityStates.Merc.Uppercut.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.duration < 0.15f)
                {
                    self.duration = 0.15f;
                }
            };


            //Huntress multiple Ballistas at once
            On.EntityStates.Huntress.AimArrowSnipe.OnEnter += Lysate_DoubleBallista;


            if (WConfig.CharactersCaptainKeepInHiddemRealm.Value == true)
            {
                SceneCatalog.availability.CallWhenAvailable(NoOrbitalStrikeBlocking);
            }
  
            //Walkers Sprinting more
            AISkillDriver[] skilllist = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/EngiWalkerTurretMaster").GetComponents<AISkillDriver>();
            skilllist[0].shouldSprint = true;
            if (skilllist[0].minDistance == 110)
            {
                skilllist[0].minDistance = 45;
            }

            //Equipment Drone fire Equipment even if no enemies
            LegacyResourcesAPI.Load<GameObject>("Prefabs/charactermasters/EquipmentDroneMaster").AddComponent<FireEquipmentAlways>();

          
        }

     

        private static void Lysate_DoubleBallista(On.EntityStates.Huntress.AimArrowSnipe.orig_OnEnter orig, EntityStates.Huntress.AimArrowSnipe self)
        {
            orig(self);
            if (WConfig.CharactersHuntressLysateCell.Value == true)
            {
                //Debug.LogWarning(self.skillLocator.special.finalRechargeInterval);
                //Debug.LogWarning(self.skillLocator.special.stock);

                int specialStock = self.skillLocator.special.stock;
                int baseMaxStock = self.primarySkillSlot.skillDef.baseMaxStock;
                if (specialStock > 1)
                {
                    specialStock = 1;
                }
                else if (self.skillLocator.special.finalRechargeInterval == 0.5)
                {
                    specialStock--;
                }

                int stock = baseMaxStock * (specialStock + 1);
                if (specialStock >= 0)
                {
                    self.skillLocator.special.DeductStock(specialStock);
                }
                // if (stock < 3) { stock= 3; }
                self.skillLocator.primary.stock = stock;
            }
        }

        private static int Captain3Beacon(On.RoR2.CharacterMaster.orig_GetDeployableSameSlotLimit orig, CharacterMaster self, DeployableSlot slot)
        {
            if (slot == DeployableSlot.CaptainSupplyDrop)
            {
                if (self.inventory.GetItemCount(DLC1Content.Items.EquipmentMagazineVoid) > 0)
                {
                    return orig(self, slot) + 1;
                }
            }
            return orig(self, slot);
        }


        public static void NoOrbitalStrikeBlocking()
        {
            for (int i = 0; i < SceneCatalog.allSceneDefs.Length; i++)
            {
                //Debug.LogWarning(SceneCatalog.allSceneDefs[i] + " " +  SceneCatalog.allSceneDefs[i].stageOrder);
                SceneCatalog.allSceneDefs[i].blockOrbitalSkills = false;
            }
            SceneDef bazaar = SceneCatalog.FindSceneDef("bazaar");
            bazaar.blockOrbitalSkills = true;
        }



        public static void MarriedLemurianBandActivator(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
                    x => x.MatchLdsfld("EntityStates.LemurianBruiserMonster.FireMegaFireball", "damageCoefficient")))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<float, EntityStates.LemurianBruiserMonster.FireMegaFireball, float>>((damageCoeff, entityState) =>
                {
                    if (entityState.characterBody.inventory.GetItemCount(RoR2Content.Items.Clover) >= 20)
                    {
                        //entityState.characterBody.ClearTimedBuffs(RoR2Content.Buffs.ElementalRingsCooldown);
                        return 4f;
                    }
                    return damageCoeff;
                });
                //Debug.Log("IL Found: Buff Married Lemurians");
            }
            else
            {
                Debug.LogWarning("IL Failed: Buff Married Lemurians");
            }
        }

       
    }

    public class FireEquipmentAlways : MonoBehaviour
    {
        public bool ShouldFireNearPlayer(EquipmentIndex index)
        {
            EquipmentDef def = EquipmentCatalog.GetEquipmentDef(index);
            if (def == RoR2Content.Equipment.Tonic ||
                //def == RoR2Content.Equipment.CommandMissile ||
                def == RoR2Content.Equipment.Fruit ||
                //def == RoR2Content.Equipment.DroneBackup ||
                def == RoR2Content.Equipment.Jetpack ||
                def == RoR2Content.Equipment.PassiveHealing ||
                def == RoR2Content.Equipment.Scanner ||
                def == RoR2Content.Equipment.Gateway ||
                def == RoR2Content.Equipment.Cleanse ||
                def == RoR2Content.Equipment.GainArmor ||
                def == RoR2Content.Equipment.Recycle ||
                def == DLC1Content.Equipment.GummyClone ||
                def == DLC1Content.Equipment.VendingMachine ||
                def == DLC1Content.Equipment.BossHunterConsumed ||
                def == DLC2Content.Equipment.HealAndReviveConsumed ||
                def == RoR2Content.Equipment.Gateway
                )
            {
                return true;
            }
            ;
            return false;
        }
        public bool ShouldFireAlways(EquipmentIndex index)
        {
            EquipmentDef def = EquipmentCatalog.GetEquipmentDef(index);
            if (def == RoR2Content.Equipment.Tonic ||
                def == RoR2Content.Equipment.CommandMissile ||
                def == RoR2Content.Equipment.Fruit ||
                //def == RoR2Content.Equipment.DroneBackup ||
                //def == RoR2Content.Equipment.Jetpack ||
                def == RoR2Content.Equipment.PassiveHealing ||
                def == RoR2Content.Equipment.Scanner ||
                def == RoR2Content.Equipment.Gateway ||
                def == RoR2Content.Equipment.Cleanse ||
                def == RoR2Content.Equipment.GainArmor ||
                //def == RoR2Content.Equipment.Recycle ||
                def == RoR2Content.Equipment.TeamWarCry ||
                //def == DLC1Content.Equipment.GummyClone ||
                def == DLC1Content.Equipment.VendingMachine ||
                //def == DLC1Content.Equipment.BossHunterConsumed ||
                //def == DLC2Content.Equipment.EliteAurelioniteEquipment ||
                // def == DLC2Content.Equipment.HealAndRevive ||
                def == DLC2Content.Equipment.HealAndReviveConsumed// ||
                //def == RoR2Content.Equipment.Gateway
                )
            {
                return true;
            };
            return false;
        }
        public void Start()
        {
            if (!WConfig.cfgEquipmentDroneThing.Value)
            {
                return;
            }
            EquipmentIndex eq = GetComponent<Inventory>().currentEquipmentIndex;
            if (ShouldFireAlways(eq))
            {

                //0 HardLeashToLeader
                //1 SoftLeashAttack
                //2 SoftLeashToLeader
                //3 StrafeNearbyEnemies
                //4 ChaseFarEnemies
                //5 FleeLeader
                //6 IdleNearLeaderWhenNoEnemies
                //7 ReturnToLeaderWhenNoEnemies
                //8 ChaseDownRandomEnemiesIfLeaderIsDead

                Debug.Log("Equipment Drone fire always");
                AISkillDriver[] skilllist = this.GetComponents<AISkillDriver>();


                skilllist[6].shouldFireEquipment = true;

                /*for (int i = 0; i < skilllist.Length; i++)
                {
                    if (skilllist[i].maxDistance < 40)
                    {
                        skilllist[i].shouldFireEquipment = true;
                    }
                 
                }*/
            }

        }
    }

}