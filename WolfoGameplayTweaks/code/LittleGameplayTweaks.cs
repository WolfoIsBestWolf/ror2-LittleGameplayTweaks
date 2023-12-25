using BepInEx;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
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
    [BepInPlugin("com.Wolfo.LittleGameplayTweaks", "LittleGameplayTweaks", "2.5.0")]
    //[R2APISubmoduleDependency(nameof(ContentAddition), nameof(LanguageAPI), nameof(PrefabAPI), nameof(ItemAPI), nameof(LoadoutAPI), nameof(EliteAPI))]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]


    public class LittleGameplayTweaks : BaseUnityPlugin
    {
        static readonly System.Random random = new System.Random();
        //public static ItemDef NoDamageFromVoidFog = ScriptableObject.CreateInstance<ItemDef>();
 
        public static List<ExplicitPickupDropTable> AllScavCompatibleBossItems = new List<ExplicitPickupDropTable>();


        public void Awake()
        {
            WConfig.InitConfig();

            DCCSEnemies.Start();
            DCCSInteractables.Start();

            ChangesItems.Start();
            ChangesCharacters.Start();
            ChangesInteractables.Start();

            Prismatic.Start();
            TwistedScavs.Start();

            if (WConfig.SkinsWolfoConfig.Value == true)
            {
                SkinStuff.WolfoSkins();
            }
            if (WConfig.SkinsGreenMando.Value == true)
            {
                R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/skinCommandoMarine.asset").WaitForCompletion());
            }

            GameModeCatalog.availability.CallWhenAvailable(ModSupport);
            On.RoR2.UI.MainMenu.MainMenuController.Start += OneTimeOnlyLateRunner;
            

            On.RoR2.SceneDirector.Start += GameplayQoL_SceneDirector_Start;
            On.RoR2.ClassicStageInfo.Awake += ClassicStageInfoMethod;

            SceneDef rootjungle = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/Base/rootjungle/rootjungle.asset").WaitForCompletion();
            MusicTrackDef MusicSulfurPoolsBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muBossfightDLC1_12.asset").WaitForCompletion();
            rootjungle.bossTrack = MusicSulfurPoolsBoss;
            /*SceneDef wispgraveyard = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/Base/wispgraveyard/wispgraveyard.asset").WaitForCompletion();
             SceneDef ancientloft = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/ancientloft/ancientloft.asset").WaitForCompletion();
             SceneDef goolake = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/Base/goolake/goolake.asset").WaitForCompletion();*/

            //wispgraveyard.mainTrack = ancientloft.mainTrack;
            //goolake.bossTrack = dampcavesimple.bossTrack;


            //Still Ripped
            Physics.IgnoreLayerCollision(15, 13, false); //How much does this actually like affect
            Physics.IgnoreLayerCollision(10, 13, false); //For Simulacrum
            On.RoR2.MapZone.TryZoneStart += PickupTeleportHook;
        }




        internal static void ModSupport()
        {
            DCCSEnemies.ModSupport();
            ChangesInteractables.ModSupport();
            ChangesItems.ItemsLate();
            GameModeCatalog.FindGameModePrefabComponent("WeeklyRun").startingScenes = GameModeCatalog.FindGameModePrefabComponent("ClassicRun").startingScenes;

            RoR2Content.Items.BonusGoldPackOnKill.tags = RoR2Content.Items.BonusGoldPackOnKill.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.Infusion.tags = RoR2Content.Items.Infusion.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.GoldOnHit.tags = RoR2Content.Items.GoldOnHit.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.RegeneratingScrap.tags = DLC1Content.Items.RegeneratingScrap.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.ShockNearby.tags = RoR2Content.Items.ShockNearby.tags.Add(ItemTag.Count);
            DLC1Content.Items.CritDamage.tags = DLC1Content.Items.CritDamage.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.DroneWeapons.tags = DLC1Content.Items.DroneWeapons.tags.Add(ItemTag.AIBlacklist);

            RoR2Content.Items.RoboBallBuddy.tags = RoR2Content.Items.RoboBallBuddy.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.MinorConstructOnKill.tags = DLC1Content.Items.MinorConstructOnKill.tags.Add(ItemTag.AIBlacklist);

            RoR2Content.Items.CaptainDefenseMatrix.tags = RoR2Content.Items.CaptainDefenseMatrix.tags.Add(ItemTag.CannotSteal);
            DLC1Content.Items.BearVoid.tags = DLC1Content.Items.BearVoid.tags.Add(ItemTag.BrotherBlacklist);

            if (WConfig.CharactersEngineerWarbanner.Value)
            {
                RoR2Content.Items.WardOnLevel.tags = RoR2Content.Items.WardOnLevel.tags.Remove(ItemTag.CannotCopy);
                RoR2Content.Items.TPHealingNova.tags = RoR2Content.Items.TPHealingNova.tags.Remove(ItemTag.CannotCopy);
                RoR2Content.Items.TonicAffliction.tags = RoR2Content.Items.TonicAffliction.tags.Add(ItemTag.CannotCopy);
            }

            if (WConfig.EclipseDifficultyAlways.Value == true)
            {
                RuleChoiceDef tempR = RuleCatalog.FindChoiceDef("Difficulty.Eclipse8");
                if (tempR != null)
                {
                    tempR.excludeByDefault = false;
                }
            }

            /*for (int i = 0; i < SceneCatalog.allSceneDefs.Length; i++)
            {
                Debug.Log(SceneCatalog.allSceneDefs[i].baseSceneName +"  "+ SceneCatalog.allSceneDefs[i].mainTrack +"  "+ SceneCatalog.allSceneDefs[i].bossTrack);
            }*/
        }


        public static void GameplayQoL_SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            if (!SceneInfo.instance) { orig(self); return; }

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
                    Inventory[] InventoryList = UnityEngine.Object.FindObjectsOfType(typeof(RoR2.Inventory)) as RoR2.Inventory[];
                    for (var i = 0; i < InventoryList.Length; i++)
                    {
                        if (NetworkServer.active)
                        {
                            //Debug.LogWarning(InventoryList[i]); ////DISABLE THIS
                            if (InventoryList[i].name.StartsWith("LemurianBruiserFireMaster") || InventoryList[i].name.StartsWith("LemurianBruiserIceMaster"))
                            {
                                InventoryList[i].GiveItem(RoR2Content.Items.UseAmbientLevel);
                                InventoryList[i].GiveItem(ChangesCharacters.MarriageLemurianIdentifier);
                            }
                        }
                    }
                    break;
                case "bazaar":
                    SeerStationController[] seerList = UnityEngine.Object.FindObjectsOfType(typeof(SeerStationController)) as SeerStationController[];
                    int instant = 10;
                    if (WConfig.ThirdLunarSeer.Value == true)
                    {
                        instant = 0;
                    }

                    SceneDef nextStageScene = Run.instance.nextStageScene;
                    List<SceneDef> nextscenelist = new List<SceneDef>();
                    if (nextStageScene != null)
                    {
                        int stageOrder = nextStageScene.stageOrder;
                        foreach (SceneDef sceneDef in SceneCatalog.allSceneDefs)
                        {
                            if (sceneDef.stageOrder == stageOrder && (sceneDef.requiredExpansion == null || Run.instance.IsExpansionEnabled(sceneDef.requiredExpansion)) && !sceneDef.baseSceneName.EndsWith("2"))
                            {
                                nextscenelist.Add(sceneDef);
                            }
                        }
                    }

                    for (var i = 0; i < seerList.Length; i++)
                    {
                        if (seerList[i].name.StartsWith("SeerStation"))
                        {
                            if (NetworkServer.active)
                            {
                                if (nextscenelist.Count > 0)
                                {
                                    SceneDef sceneDefForRemoval = SceneCatalog.GetSceneDef((SceneIndex)seerList[i].gameObject.GetComponent<SeerStationController>().NetworktargetSceneDefIndex);
                                    if (sceneDefForRemoval)
                                    {
                                        if (sceneDefForRemoval.cachedName.StartsWith("blackbeach") || sceneDefForRemoval.cachedName.StartsWith("golemplains"))
                                        {
                                            nextscenelist.Remove(SceneCatalog.FindSceneDef(sceneDefForRemoval.baseSceneName));
                                            nextscenelist.Remove(SceneCatalog.FindSceneDef(sceneDefForRemoval.baseSceneName + "2"));
                                        }
                                        else
                                        {
                                            nextscenelist.Remove(sceneDefForRemoval);
                                        }
                                    }
                                }

                                if (instant == 1)
                                {
                                    instant = 2;
                                    GameObject newseer = GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/bazaar/SeerStation.prefab").WaitForCompletion(), seerList[i].gameObject.transform.parent);
                                    NetworkServer.Spawn(newseer);
                                    newseer.transform.localPosition = new Vector3(10f, -0.81f, 4f);
                                    newseer.transform.localRotation = new Quaternion(0f, 0.3827f, 0f, -0.9239f);
                                    if (nextscenelist.Count > 0)
                                    {
                                        newseer.GetComponent<SeerStationController>().SetTargetScene(nextscenelist[random.Next(nextscenelist.Count)]);
                                        newseer.GetComponent<SeerStationController>().OnStartClient();
                                    }
                                    else
                                    {
                                        newseer.GetComponent<PurchaseInteraction>().SetAvailable(false);
                                        newseer.GetComponent<SeerStationController>().OnStartClient();
                                    }
                                }
                                instant++;
                            }
                        }
                    }
                    break;
                case "moon2":
                    ShopTerminalBehavior[] shopList = UnityEngine.Object.FindObjectsOfType(typeof(ShopTerminalBehavior)) as ShopTerminalBehavior[];
                    int cauldronthing = 0;
                    Transform tempcauldronparent = null;
                    Transform tempcauldron1 = null;
                    for (var i = 0; i < shopList.Length; i++)
                    {
                        //Debug.LogWarning(highlightlist[i]); ////DISABLE THIS
                        if (shopList[i].name.StartsWith("LunarCauldron,"))
                        {
                            if (cauldronthing == 0)
                            {
                                tempcauldron1 = shopList[i].gameObject.transform;
                                tempcauldronparent = shopList[i].gameObject.transform.parent;
                            };
                            cauldronthing++;
                            if (shopList[i].name.StartsWith("LunarCauldron, RedToWhite"))
                            {
                                cauldronthing = 10;
                                shopList[i].gameObject.GetComponent<ShopTerminalBehavior>().dropVelocity = new Vector3(5, 10, 5);
                            };
                            if (cauldronthing == 5 && tempcauldronparent != null)
                            {
                                if (WConfig.GuaranteedRedToWhite.Value == true)
                                {
                                    //GameObject RedToWhiteSoup = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/LunarCauldron, RedToWhite Variant");
                                    //Surely I could just activate a Soup
                                    GameObject newSoup = UnityEngine.Object.Instantiate<GameObject>(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/LunarCauldron, RedToWhite Variant"), tempcauldron1.position, tempcauldron1.rotation);
                                    NetworkServer.Spawn(newSoup);
                                    tempcauldron1.gameObject.SetActive(false);
                                    Debug.Log("No White Soup, making one");
                                }
                                else
                                {
                                    Debug.Log("No White Soup");
                                }
                            };
                            //RedToWhiteSoup
                        }
                    }
                    break;
            };
            orig(self);
        }



        public void OneTimeOnlyLateRunner(On.RoR2.UI.MainMenu.MainMenuController.orig_Start orig, RoR2.UI.MainMenu.MainMenuController self)
        {
            orig(self);


            DLC1Content.Items.RegeneratingScrapConsumed.canRemove = false;

            ExplicitPickupDropTable[] ExplicitPickupDropTableList = Resources.FindObjectsOfTypeAll(typeof(ExplicitPickupDropTable)) as ExplicitPickupDropTable[];
            //Debug.LogWarning(ExplicitPickupDropTableList.Length);
            for (int i = 0; i < ExplicitPickupDropTableList.Length; i++)
            {
                //Debug.LogWarning(ExplicitPickupDropTableList[i]);
                ExplicitPickupDropTableList[i].canDropBeReplaced = false;

                if (ExplicitPickupDropTableList[i].name.StartsWith("dtBoss"))
                {
                    foreach (RoR2.ExplicitPickupDropTable.PickupDefEntry entry in ExplicitPickupDropTableList[i].pickupEntries)
                    {
                        ItemDef tempitemdef = (entry.pickupDef as ItemDef);
                        if (tempitemdef && tempitemdef.tier == ItemTier.Boss && tempitemdef.DoesNotContainTag(ItemTag.WorldUnique))
                        {
                            AllScavCompatibleBossItems.Add(ExplicitPickupDropTableList[i]);
                            //Debug.LogWarning(ExplicitPickupDropTableList[i]);
                        }
                    }
                }


            }


            bool dlc1 = RoR2.EntitlementManagement.EntitlementManager.localUserEntitlementTracker.AnyUserHasEntitlement(LegacyResourcesAPI.Load<RoR2.EntitlementManagement.EntitlementDef>("EntitlementDefs/entitlementDLC1"));
            if (dlc1 && WConfig.cfgScavNewTwisted.Value)
            {
                
                MultiCharacterSpawnCard cscScavLunar = LegacyResourcesAPI.Load<MultiCharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScavLunar");
                cscScavLunar.masterPrefabs = cscScavLunar.masterPrefabs.Add(TwistedScavs.ScavLunarWGoomanMaster, TwistedScavs.ScavLunarWSpeedMaster, TwistedScavs.ScavLunarWTankMaster);
            }

            On.RoR2.UI.MainMenu.MainMenuController.Start -= OneTimeOnlyLateRunner;
        }




        public static void ClassicStageInfoMethod(On.RoR2.ClassicStageInfo.orig_Awake orig, global::RoR2.ClassicStageInfo self)
        {
            orig(self);
            ChangesCharacters.DropTableForBossScav = AllScavCompatibleBossItems[WRect.random.Next(AllScavCompatibleBossItems.Count)];
        }


        private static bool ColliderPickup(Collider collider)
        {
            if (collider.GetComponent<PickupDropletController>()) return true;
            if (collider.GetComponent<GenericPickupController>()) return true;
            if (collider.GetComponent<PickupPickerController>()) return true;
            return false;
        }

        private static void PickupTeleportHook(On.RoR2.MapZone.orig_TryZoneStart orig, MapZone self, Collider collider)
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