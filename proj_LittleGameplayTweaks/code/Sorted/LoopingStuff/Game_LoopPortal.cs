using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.Networking;
 
namespace LittleGameplayTweaks
{
    /*
    public class LoopPortalStuff
    {


        public static void CallLate()
        {
             
            On.EntityStates.Missions.BrotherEncounter.EncounterFinished.OnEnter += LoopPortal_Moon;
            On.EntityStates.Missions.LunarScavengerEncounter.FadeOut.OnEnter += LoopPortal_MS;

            On.EntityStates.LunarTeleporter.IdleToActive.OnEnter += IdleToActive_OnEnter;
            On.EntityStates.LunarTeleporter.IdleToActive.OnExit += IdleToActive_OnExit;
            On.EntityStates.LunarTeleporter.Idle.OnEnter += Idle_OnEnter;
 
            Addressables.LoadAssetAsync<GameObject>(key: "34d770816ffbf0d468728c48853fd5f6").WaitForCompletion().AddComponent<LoopPortal>();
 
        }

        /*private void SceneDirector_PlaceTeleporter(On.RoR2.SceneDirector.orig_PlaceTeleporter orig, SceneDirector self)
        {
            if (WConfig.cfgLunarTeleporterAlways.Value)
            {
                if (Run.instance && Run.instance.loopClearCount > 0)
                {
                    if (self.teleporterSpawnCard)
                    {
                        self.teleporterSpawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Teleporters/iscLunarTeleporter.asset").WaitForCompletion();
                    }
                }
            }
            orig(self);
        }   
 
        private static void Idle_OnEnter(On.EntityStates.LunarTeleporter.Idle.orig_OnEnter orig, EntityStates.LunarTeleporter.Idle self)
        {
            orig(self);
            if (Run.instance && Run.instance.eventFlags.Contains("KilledBrother"))
            {
               self.preferredInteractability = Interactability.Disabled;
            }         
        }
        private static void IdleToActive_OnEnter(On.EntityStates.LunarTeleporter.IdleToActive.orig_OnEnter orig, EntityStates.LunarTeleporter.IdleToActive self)
        {
            if (Run.instance && Run.instance.eventFlags.Contains("KilledBrother"))
            {
                self.outer.SetNextState(new EntityStates.LunarTeleporter.ActiveToIdle());
                return;
            }
            orig(self);
        }
        private static void IdleToActive_OnExit(On.EntityStates.LunarTeleporter.IdleToActive.orig_OnExit orig, EntityStates.LunarTeleporter.IdleToActive self)
        {
            if (Run.instance && Run.instance.eventFlags.Contains("KilledBrother"))
            {
                return;
            }
            orig(self);
        }

        private static void LoopPortal_Moon(On.EntityStates.Missions.BrotherEncounter.EncounterFinished.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.EncounterFinished self)
        {
            orig(self); 
            Run.instance.eventFlags.Add("KilledBrother");
            SpawnLoopPortal(0);
         
        }
 
        private static void LoopPortal_MS(On.EntityStates.Missions.LunarScavengerEncounter.FadeOut.orig_OnEnter orig, EntityStates.Missions.LunarScavengerEncounter.FadeOut self)
        {
            orig(self);
            SpawnLoopPortal(1);
        }

        public static void SpawnLoopPortal(int version)
        {
            if (!NetworkServer.active)
            { 
                return; 
            }
            if (!WConfig.cfgMoreLoop.Value)
            {
                return;
            }
            GameObject portalObject = null;

            switch (version)
            {
                case 0:
                    //portalObject = Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>(key: "161f0fdda19f1eb4389d36a5f336cb84").WaitForCompletion(), new Vector3(1114.95f, -283.011f, 1191.11f), Quaternion.Euler(new Vector3(0f, 222f, 0f)));
                    portalObject = Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>(key: "34d770816ffbf0d468728c48853fd5f6").WaitForCompletion(), new Vector3(1117.95f, -283.011f, 1194.11f), Quaternion.Euler(new Vector3(0f, 275f, 0f)));
                    portalObject.GetComponentInChildren<LightIntensityCurve>().maxIntensity = 1f;
                   
                    break;
                case 1: //MS Limbo
                    //portalObject = Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>(key: "eadfcaf9ea3275e49858ed19f874db5a").WaitForCompletion(), new Vector3(60.1877f, -3.4472f, -3.8198f), Quaternion.Euler(new Vector3(0f, 312.045f, 0f)));
                    portalObject = Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>(key: "34d770816ffbf0d468728c48853fd5f6").WaitForCompletion(), new Vector3(60f, -3.5f, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                   
                    break;
                case 2:
                    //34d770816ffbf0d468728c48853fd5f6
                    break;

            }
            NetworkServer.Spawn(portalObject);
        }
  
 
    }
 

    public class LoopPortal: MonoBehaviour
    {
        public void OnEnable()
        {
            if (SceneInfo.instance)
            {
                string a = SceneInfo.instance.sceneDef.cachedName;
                string context = string.Empty;
                if (a == "moon2")
                {
                    context = "ACHIEVEMENT_LOOPONCE_DESCRIPTION";
                }
                else if (a == "limbo")
                {
                    context = "ACHIEVEMENT_REPEATEDLYDUPLICATEITEMS_NAME";
                }
                else
                {
                    return;
                }

                //This isn't networked so like blegh

                Object.Destroy(this.GetComponent<GenericObjectiveProvider>());

                GenericDisplayNameProvider genericDisplayNameProvider = this.GetComponent<GenericDisplayNameProvider>();
                genericDisplayNameProvider.displayToken = "ACHIEVEMENT_LOOPONCE_NAME";
                GenericInteraction genericInteraction = this.GetComponent<GenericInteraction>();
                genericInteraction.contextToken = context;

                SceneExitController sceneExitController = this.GetComponent<SceneExitController>();
                sceneExitController.useRunNextStageScene = true;
         
            }
        }
    }

    */

}