using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System.Collections.ObjectModel;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class LoopingLevel
    {


        public static void CallLate()
        {
            Stage.onStageStartGlobal += Stage_onStageStartGlobal;
            
            Run.onRunDestroyGlobal += Run_onRunDestroyGlobal;
            Run.onRunStartGlobal += Run_onRunStartGlobal;

            IL.RoR2.CombatDirector.Spawn += CombatDirector_Spawn;
            
        }

        private static void CombatDirector_Spawn(ILContext il)
        {
            ILCursor c = new ILCursor(il);
 
            if (c.TryGotoNext(MoveType.Before,
                x => x.MatchCallvirt("RoR2.DirectorCore", "TrySpawnObject")))
            {
                c.EmitDelegate<System.Func<DirectorSpawnRequest, DirectorSpawnRequest>>((spawn) =>
                {
                    spawn.onSpawnedServer += A;
                    return spawn;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: PurchaseInteraction.ShrineBloodBehavior_GoldAmount");
            }
        }

        public static int LevelsToGive = 0;
        public static void A(SpawnCard.SpawnResult result)
        {
            if (LevelsToGive > 0 && result.success)
            {  
                Inventory component = result.spawnedInstance.GetComponent<Inventory>();
                component.GiveItem(RoR2Content.Items.LevelBonus, LevelsToGive);
            }
        }
 
         private static void Stage_onStageStartGlobal(Stage self)
        {
            if (!WConfig.cfgAddLevelCapPerStage.Value)
            {
                return;
            }
            if (Run.instance.loopClearCount == 0)
            {
                return;
            }
            //Doesn't increase on Moon2 this way
            if (self.sceneDef.stageOrder < 6)
            {
                LevelsToGive = (Run.instance.stageClearCount - 4)*10;
            }
        }




        private static void Run_onRunStartGlobal(Run run)
        {
            
        }
        private static void Run_onRunDestroyGlobal(Run obj)
        {
            LevelsToGive = 0;
        }
     
 

    }
}