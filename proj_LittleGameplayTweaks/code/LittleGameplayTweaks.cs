using BepInEx;
using R2API.Utils;
using RoR2;
using System;
 
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace LittleGameplayTweaks
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("Wolfo.LittleGameplayTweaks", "LittleGameplayTweaks", "3.6.2")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]

    public class LittleGameplayTweaks : BaseUnityPlugin
    {
        public static readonly System.Random random = new System.Random();

        public void Awake()
        {
            //Assets.Init(Info);
            WConfig.InitConfig();
            ConfigStages.InitConfig();

            //ChangesItems.Start();
            Changes_Monsters.Start();
            Changes_Survivors.Start();
            Changes_Interactables.Start();
            Changes_Stages.Start();

            DCCS_Monsters.Start();
            DCCS_Interactables.Start();
            DCCS_Family.Start();

            Eclipse.Start();
            GameModeCatalog.availability.CallWhenAvailable(LateMethod);

            On.RoR2.SceneDirector.Start += GameplayQoL_SceneDirector_Start;
            On.RoR2.SceneDirector.PlaceTeleporter += SceneDirector_PlaceTeleporter;
            On.RoR2.ClassicStageInfo.Awake += RollForScavBoss;


        
            //On.RoR2.CharacterBody.OnSkillActivated += CharacterBody_OnSkillActivated;

        }

        private void SceneDirector_PlaceTeleporter(On.RoR2.SceneDirector.orig_PlaceTeleporter orig, SceneDirector self)
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

        public void Start()
        {
            WConfig.RiskConfig();
        }
   
        internal static void LateMethod()
        {
            ConfigStages.RiskConfig();

            Looping.CallLate();
            Changes_Monsters.CallLate();

            EquipmentBonusRate(null, null);
 
            WConfig.EclipseDifficultyAlways_SettingChanged(null, null);

            if (WConfig.cfgMendingCoreBuff.Value)
            {
                EntityStates.AffixEarthHealer.Heal.healCoefficient *= 2;
            }
 
        }


        public static void GameplayQoL_SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
           
            orig(self);
            switch (SceneInfo.instance.sceneDef.baseSceneName)
            {
                case "blackbeach":
                    //Guaranteeing one Newt
                    GameObject tempobj = GameObject.Find("/HOLDER: Preplaced Objects");
                    if (tempobj != null)
                    {
                        UnlockableGranter[] portalstatuelist = tempobj.GetComponentsInChildren<RoR2.UnlockableGranter>(true);
                        int unavailable = 0;
                        for (var i = 0; i < portalstatuelist.Length; i++)
                        {
                            if (portalstatuelist[i].gameObject.activeSelf == false)
                            {
                                unavailable++;
                            }
                            if (unavailable == 3)
                            {
                                portalstatuelist[1].gameObject.SetActive(true);
                            }
                        }
                        portalstatuelist = null;
                        GC.Collect();
                    }
                    break;
                case "goolake":
                    if (WConfig.cfgElderLemurianBands.Value == true)
                    {
                        GameObject RingEventController = GameObject.Find("/HOLDER: Secret Ring Area Content/ApproxCenter/RingEventController/");
                        CharacterMaster master1 = RingEventController.transform.GetChild(1).GetComponent<CharacterMaster>();
                        CharacterMaster master2 = RingEventController.transform.GetChild(2).GetComponent<CharacterMaster>();
                        master1.inventory.GiveItem(RoR2Content.Items.UseAmbientLevel);
                        master2.inventory.GiveItem(RoR2Content.Items.UseAmbientLevel);
                        master1.inventory.GiveItem(RoR2Content.Items.CutHp,1);
                        master2.inventory.GiveItem(RoR2Content.Items.CutHp,1);
                        master1.onBodyStart += AquaductEldersStats;
                        master2.onBodyStart += AquaductEldersStats;
                    }
                    break;
            };
        }

        private static void AquaductEldersStats(CharacterBody body)
        {
            Debug.Log("Aquaduct Elder Lemurian Start");
            //*0.8 because we increase the damage to proc bands
            //Lower damage in general because holy fuck elder lemurian with bands
            body.baseDamage *= 0.45f; 
            body.levelDamage *= 0.45f;


        }

        public static void RollForScavBoss(On.RoR2.ClassicStageInfo.orig_Awake orig, global::RoR2.ClassicStageInfo self)
        {
            orig(self);
            if (!NetworkServer.active)
            {
                return;
            }
            if (WConfig.cfgScavBoss.Value)
            {
                //2 in 5 chance
                if (self.rng != null && self.rng.nextNormalizedFloat > 0.6f)
                {
                    LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScav").forbiddenAsBoss = false;
                }
                else
                {
                    LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScav").forbiddenAsBoss = true;
                }
            }
        }

        public static void EquipmentBonusRate(object sender, System.EventArgs e)
        {
            for (int i = 0; i < EliteCatalog.eliteDefs.Length; i++)
            {
                //Debug.LogWarning(EliteCatalog.eliteDefs[i].eliteEquipmentDef);
                if (EliteCatalog.eliteDefs[i].eliteEquipmentDef)
                {
                    if (EliteCatalog.eliteDefs[i].eliteEquipmentDef.dropOnDeathChance != 0)
                    {
                        EliteCatalog.eliteDefs[i].eliteEquipmentDef.dropOnDeathChance = 1f / (float)WConfig.cfgAspectDropRate.Value;
                    }
                }
                
            }
        }

    }


}