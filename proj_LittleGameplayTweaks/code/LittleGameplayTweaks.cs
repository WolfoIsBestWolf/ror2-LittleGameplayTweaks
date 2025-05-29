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
    [BepInPlugin("Wolfo.LittleGameplayTweaks", "LittleGameplayTweaks", "3.5.0")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]

    public class LittleGameplayTweaks : BaseUnityPlugin
    {
        static readonly System.Random random = new System.Random();

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
            On.RoR2.ClassicStageInfo.Awake += ClassicStageInfoMethod;


            GameObject PickupDroplet = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Common/PickupDroplet.prefab").WaitForCompletion();
            PickupDroplet.AddComponent<ItemOrbPhysicsChanger>();
            On.RoR2.MapZone.TryZoneStart += PickupTeleportHook;

            //On.RoR2.CharacterBody.OnSkillActivated += CharacterBody_OnSkillActivated;

        }

        public void Start()
        {
            WConfig.RiskConfig();

        }



        public class ItemOrbPhysicsChanger : MonoBehaviour
        {
            public Rigidbody body;

            public void Awake()
            {
                body = this.GetComponent<Rigidbody>();
            }

            public void FixedUpdate()
            {
                if (body && body.velocity.y < -30)
                {
                    this.gameObject.layer = 8;
                    Destroy(this);
                }
            }
        }


        internal static void LateMethod()
        {
            ConfigStages.RiskConfig();

            Looping.CallLate();

            EquipmentBonusRate(null, null);


            HG.ArrayUtils.ArrayAppend(ref RoR2Content.Items.RoboBallBuddy.tags, ItemTag.AIBlacklist);
            HG.ArrayUtils.ArrayAppend(ref DLC1Content.Items.MinorConstructOnKill.tags, ItemTag.AIBlacklist);
            HG.ArrayUtils.ArrayAppend(ref RoR2Content.Items.CaptainDefenseMatrix.tags, ItemTag.CannotSteal);

   
            WConfig.EclipseDifficultyAlways_SettingChanged(null, null);

            if (WConfig.cfgMendingCoreBuff.Value)
            {
                EntityStates.AffixEarthHealer.Heal.healCoefficient *= 2;
            }
            Looping.storedRunAmbientLevelCap = Run.ambientLevelCap;
        }


        public static void GameplayQoL_SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            if (WConfig.cfgLunarTeleporterAlways.Value)
            {
                if (Run.instance.loopClearCount > 0)
                {
                    if (self.teleporterSpawnCard)
                    {
                        self.teleporterSpawnCard = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/Base/Teleporters/iscLunarTeleporter.asset").WaitForCompletion();
                    }
                }
            }
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
                        master1.inventory.GiveItem(RoR2Content.Items.CutHp,3);
                        master2.inventory.GiveItem(RoR2Content.Items.CutHp,3);
                        master1.onBodyStart += AquaductEldersStats;
                        master2.onBodyStart += AquaductEldersStats;
                    }
                    break;
            };
        }

        private static void AquaductEldersStats(CharacterBody body)
        {
            Debug.Log("Aquaduct Elder Lemurian Start");
            body.baseDamage /= 3.2f;
            body.levelDamage /= 3.2f;
 
        }

        public static void ClassicStageInfoMethod(On.RoR2.ClassicStageInfo.orig_Awake orig, global::RoR2.ClassicStageInfo self)
        {
            orig(self);
            if (!NetworkServer.active)
            {
                return;
            }
            if (WConfig.cfgScavBoss.Value)
            {
                Changes_Monsters.ScavBossItem = Changes_Monsters.DropTableForBossScav.GenerateDrop(self.rng);
                if (Run.instance && Run.instance.stageClearCount > 5)
                {
                    if (self.rng != null && self.rng.nextBool)
                    {
                        LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScav").forbiddenAsBoss = false;
                    }
                    else
                    {
                        LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScav").forbiddenAsBoss = true;
                    }
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

        public static bool ColliderPickup(Collider collider)
        {
            if (collider.GetComponent<PickupDropletController>()) return true;
            if (collider.GetComponent<GenericPickupController>()) return true;
            if (collider.GetComponent<PickupPickerController>()) return true;
            return false;
        }

        public static void PickupTeleportHook(On.RoR2.MapZone.orig_TryZoneStart orig, MapZone self, Collider collider)
        {
            orig(self, collider);
            if (self.zoneType == MapZone.ZoneType.OutOfBounds)
            {
                if (ColliderPickup(collider))
                {

                    InfiniteTowerRun itRun = Run.instance.GetComponent<InfiniteTowerRun>();
                    if (itRun && itRun.safeWardController)
                    {
                        Debug.Log("it tp item back");
                        TeleportHelper.TeleportGameObject(collider.gameObject, itRun.safeWardController.transform.position);
                    }
                    else
                    {
                        SpawnCard spawnCard = ScriptableObject.CreateInstance<SpawnCard>();
                        spawnCard.hullSize = HullClassification.Human;
                        spawnCard.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Ground;
                        spawnCard.prefab = LegacyResourcesAPI.Load<GameObject>("SpawnCards/HelperPrefab");

                        DirectorPlacementRule placementRule = new DirectorPlacementRule
                        {
                            placementMode = DirectorPlacementRule.PlacementMode.NearestNode,
                            position = collider.transform.position
                        };

                        GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, placementRule, RoR2Application.rng));
                        if (gameObject)
                        {
                            Debug.Log("tp item back");
                            TeleportHelper.TeleportGameObject(collider.gameObject, gameObject.transform.position);
                            UnityEngine.Object.Destroy(gameObject);
                        }
                        UnityEngine.Object.Destroy(spawnCard);
                    }
                }
            }
        }

    }


}