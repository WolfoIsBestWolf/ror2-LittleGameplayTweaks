using R2API;
using RoR2;
using RoR2.UI;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace LittleGameplayFeatures
{
    public class Prism_Run
    {
        public static GameObject runObject;

        public static void Start()
        {
            GameObject weeklyRunObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/gamemodes/WeeklyRun");
            WeeklyRun weeklyRun = weeklyRunObject.GetComponent<WeeklyRun>();

            GameObject runObjectTemp = new GameObject("xPrismRun");
            runObjectTemp.SetActive(false);
            runObjectTemp.AddComponent<NetworkIdentity>();
            runObjectTemp.AddComponent<NetworkRuleBook>();
            runObjectTemp.AddComponent<PrismRun>();
            //runObjectTemp.AddComponent<RunArtifactManager>(); //Added when Run is added
            runObjectTemp.AddComponent<TeamManager>();
            runObjectTemp.AddComponent<RunCameraManager>();

            runObject = PrefabAPI.InstantiateClone(runObjectTemp, "xPrismRun", true);
            runObject.SetActive(true);
            runObject.AddComponent<GameModeInfo>().buttonHoverDescription = "GAMEMODE_PRISM_RUN_DESC";


            PrismRun prismRun = runObject.GetComponent<PrismRun>();
            prismRun.nameToken = "GAMEMODE_PRISM_RUN_NAME";
            prismRun.userPickable = true;
            prismRun.crystalSpawnCard = weeklyRun.crystalSpawnCard;
            prismRun.equipmentBarrelSpawnCard = weeklyRun.equipmentBarrelSpawnCard;

            prismRun.gameOverPrefab = weeklyRun.gameOverPrefab;
            prismRun.lobbyBackgroundPrefab = weeklyRun.lobbyBackgroundPrefab;
            prismRun.rebirthDropTable = weeklyRun.rebirthDropTable;
            prismRun.startingSceneGroup = weeklyRun.startingSceneGroup;
            prismRun.uiPrefab = weeklyRun.uiPrefab;



            ContentAddition.AddGameMode(Prism_Run.runObject);

            On.RoR2.RunReport.PlayerInfo.Generate += PrismMentionOnDeathScreen;

            On.RoR2.Stage.Start += PrismRun_AddTeleporterLock;

            On.RoR2.UI.ObjectivePanelController.GetObjectiveSources += ObjectivePanelController_GetObjectiveSources;


        }

        private static void ObjectivePanelController_GetObjectiveSources(On.RoR2.UI.ObjectivePanelController.orig_GetObjectiveSources orig, RoR2.UI.ObjectivePanelController self, CharacterMaster master, List<RoR2.UI.ObjectivePanelController.ObjectiveSourceDescriptor> output)
        {
            orig(self, master, output);
            PrismRun prismRun = Run.instance as PrismRun;
            if (prismRun && prismRun.crystalsToKill > prismRun.crystalsKilled_)
            {
                output.Add(new ObjectivePanelController.ObjectiveSourceDescriptor
                {
                    source = Run.instance,
                    master = master,
                    objectiveType = typeof(PrismDestroyCrystals),
                });
            }
        }
        private class PrismDestroyCrystals : ObjectivePanelController.ObjectiveTracker
        {
            private PrismRun run;
            public PrismDestroyCrystals()
            {
                baseToken = "OBJECTIVE_WEEKLYRUN_DESTROY_CRYSTALS";
            }
            public override string GenerateString()
            {
                PrismRun run = Run.instance as PrismRun;
                return string.Format(Language.GetString(this.baseToken), run.crystalsKilled_, run.crystalsToKill);
            }
            public override bool IsDirty()
            {
                return true;
            }
        }

        public static System.Collections.IEnumerator PrismRun_AddTeleporterLock(On.RoR2.Stage.orig_Start orig, Stage self)
        {
            if (Run.instance && Run.instance is PrismRun)
            {
                if (TeleporterInteraction.instance)
                {
                    //Doesn't run in Weekly normally because its mid scene director
                    if (TeleporterInteraction.instance)
                    {
                        ChildLocator component2 = TeleporterInteraction.instance.GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>();
                        if (component2)
                        {
                            component2.FindChild("TimeCrystalProps").gameObject.SetActive(true);
                            component2.FindChild("TimeCrystalBeaconBlocker").gameObject.SetActive(true);
                        }
                    }
                    TeleporterInteraction.instance.GetComponent<HoldoutZoneController>().baseRadius = 500;
                }
            }
            return orig(self);
        }

        public static RunReport.PlayerInfo PrismMentionOnDeathScreen(On.RoR2.RunReport.PlayerInfo.orig_Generate orig, PlayerCharacterMasterController playerCharacterMasterController, GameEndingDef gameEnding)
        {
            if (Run.instance && Run.instance is PrismRun)
            {
                string type = "GAMEMODE_PRISM_RUN_NAME";
                string win = "PRISM_DEATH";
                if (gameEnding.isWin)
                {
                    win = "PRISM_WIN";
                }
                RunReport.PlayerInfo temp = orig(playerCharacterMasterController, gameEnding);
                temp.finalMessageToken = Language.GetStringFormatted(win, Language.GetString(type));
                return temp;
            }
            return orig(playerCharacterMasterController, gameEnding);
        }

        public static void LateRunningMethod()
        {
            //Proper Map Normal Icon
            Texture2D texRuleMapIsNormal = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texRuleMapIsNormal.LoadImage(Properties.Resources.texRuleMapIsNormal, true);
            texRuleMapIsNormal.wrapMode = TextureWrapMode.Clamp;
            Sprite texRuleMapIsNormalS = Sprite.Create(texRuleMapIsNormal, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));

            RuleChoiceDef mapDef = RuleCatalog.FindChoiceDef("Misc.StageOrder.Normal");
            if (mapDef != null)
            {
                mapDef.sprite = texRuleMapIsNormalS;
            }

            //All Boss Affixes for the Weekly
            List<EquipmentIndex> list = new List<EquipmentIndex>();
            for (var i = 0; i < EliteCatalog.eliteDefs.Length; i++)
            {
                EquipmentDef eqDef = EliteCatalog.eliteDefs[i].eliteEquipmentDef;
                //Debug.Log(EliteCatalog.eliteDefs[i]+ " | "+eqDef);
                if (eqDef && eqDef.dropOnDeathChance > 0)
                {
                    list.Add(eqDef.equipmentIndex);
                }
            }
            list.Remove(JunkContent.Equipment.EliteGoldEquipment.equipmentIndex);
            list.Add(DLC1Content.Equipment.EliteVoidEquipment.equipmentIndex);
            PrismRun prismRun = runObject.GetComponent<PrismRun>();
            if (list.Count == 0)
            {
                prismRun.bossAffixes = new EquipmentIndex[]
                {
                    RoR2Content.Equipment.AffixRed.equipmentIndex,
                    RoR2Content.Equipment.AffixBlue.equipmentIndex,
                    RoR2Content.Equipment.AffixWhite.equipmentIndex,
                    RoR2Content.Equipment.AffixLunar.equipmentIndex,
                    DLC1Content.Elites.Earth.eliteEquipmentDef.equipmentIndex,
                    DLC2Content.Equipment.EliteAurelioniteEquipment.equipmentIndex
                };
            }
            else
            {
                prismRun.bossAffixes = list.ToArray();
            }

        }

    }


    public class PrismRun : Run
    {
        public EquipmentIndex[] bossAffixes = System.Array.Empty<EquipmentIndex>();
        public EquipmentIndex bossAffix = EquipmentIndex.None;

        public SpawnCard crystalSpawnCard;
        public int baseCrystalToSpawn = 5;
        public int baseCrystalsToKill = 3;
        public int crystalToSpawn = 5;
        public int crystalsToKill = 3;

        public int baseCrystalRewardValue = 30;
        public uint crystalRewardValue = 30;

        [SyncVar]
        public int crystalsKilled_;
        public int CrystalsKilled
        {
            get
            {
                return crystalsKilled_;
            }
            [param: In]
            set
            {
                base.SetSyncVar<int>(value, ref crystalsKilled_, 128U);
            }
        }

        public EquipmentIndex AffixToUse()
        {
            if (SceneInfo.instance)
            {
                if (SceneInfo.instance.sceneDef.stageOrder > 5)
                {
                    return bossRewardRng.NextElementUniform<EquipmentIndex>(bossAffixes);
                }
            }
            return bossAffix;
        }

        public override void AdvanceStage(SceneDef nextScene)
        {
            Debug.Log("Prism Run | Advance Stage");
            base.AdvanceStage(nextScene);
            if (NetworkServer.active)
            {

            }
            bossAffix = bossRewardRng.NextElementUniform<EquipmentIndex>(bossAffixes);
            crystalActiveList.Clear();
            crystalsKilled_ = 0;
            CrystalsKilled = 0;
            crystalRewardValue = (uint)GetDifficultyScaledCost(baseCrystalRewardValue);
            int kill = baseCrystalsToKill;
            int crystal = baseCrystalToSpawn;
            if (nextScene.stageOrder == 4)
            {
                kill += 1;
                crystal += 1;
            }
            else if (nextScene.stageOrder == 5)
            {
                kill += 2;
                crystal += 3;
            }
            if (participatingPlayerCount > 2)
            {
                kill += 1;
                crystal += 1;
            }
            crystalToSpawn = crystal;
            crystalsToKill = kill;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (TeleporterInteraction.instance)
            {
                bool shouldLock = crystalsToKill > crystalsKilled_;
                if (shouldLock != TeleporterInteraction.instance._locked)
                {
                    if (shouldLock)
                    {
                        TeleporterInteraction.instance._locked = true;
                        return;
                    }
                    else
                    {
                        TeleporterInteraction.instance.locked = false;
                        ChildLocator component = TeleporterInteraction.instance.GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>();
                        if (component)
                        {
                            Transform transform = component.FindChild("TimeCrystalBeaconBlocker");
                            EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/TimeCrystalDeath"), new EffectData
                            {
                                origin = transform.transform.position
                            }, false);
                            transform.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        private List<OnDestroyCallback> crystalActiveList = new List<OnDestroyCallback>();
        public override void OnServerTeleporterPlaced(SceneDirector sceneDirector, GameObject teleporter)
        {
            base.OnServerTeleporterPlaced(sceneDirector, teleporter);
            DirectorPlacementRule directorPlacementRule = new DirectorPlacementRule();
            directorPlacementRule.placementMode = DirectorPlacementRule.PlacementMode.Random;
            int num = 0;
            while (num < crystalToSpawn)
            {
                GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(this.crystalSpawnCard, directorPlacementRule, this.stageRng));
                if (gameObject)
                {
                    DeathRewards component3 = gameObject.GetComponent<DeathRewards>();
                    if (component3)
                    {
                        component3.goldReward = crystalRewardValue;
                    }
                }
                crystalActiveList.Add(OnDestroyCallback.AddCallback(gameObject, delegate (OnDestroyCallback component)
                {
                    if (crystalActiveList.Contains(component))
                    {
                        CrystalsKilled++;
                        crystalActiveList.Remove(component);
                    }
                }));

                num++;
            }
            if (TeleporterInteraction.instance)
            {
                ChildLocator component2 = TeleporterInteraction.instance.GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>();
                if (component2)
                {
                    component2.FindChild("TimeCrystalProps").gameObject.SetActive(true);
                    component2.FindChild("TimeCrystalBeaconBlocker").gameObject.SetActive(true);
                }
            }
        }

        public override void OnServerBossAdded(BossGroup bossGroup, CharacterMaster characterMaster)
        {
            base.OnServerBossAdded(bossGroup, characterMaster);
            string stage = SceneInfo.instance.sceneDef.baseSceneName;
            if (stage == "limbo" || stage.StartsWith("artifact"))
            {
                return;
            }
            if (this.stageClearCount >= 3) //4 onwards
            {
                if (characterMaster.inventory.GetEquipmentIndex() == EquipmentIndex.None)
                {
                    characterMaster.inventory.SetEquipmentIndex(AffixToUse());
                }
                //Final Bosses get less stats so they're not absurd
                if (characterMaster.inventory.GetItemCount(RoR2Content.Items.AdaptiveArmor) > 0)
                {
                    EquipmentIndex equip = characterMaster.inventory.currentEquipmentIndex;
                    if (equip == RoR2Content.Equipment.AffixBlue.equipmentIndex || equip == RoR2Content.Equipment.AffixRed.equipmentIndex)
                    {
                        characterMaster.inventory.RemoveItem(RoR2Content.Items.BoostDamage, (int)(characterMaster.inventory.GetItemCount(RoR2Content.Items.BoostDamage) * 0.35f));
                    }
                    else if (equip == DLC1Content.Equipment.EliteVoidEquipment.equipmentIndex)
                    {
                        characterMaster.inventory.RemoveItem(RoR2Content.Items.BoostHp, 5);
                        characterMaster.inventory.RemoveItem(RoR2Content.Items.BoostDamage, (int)(characterMaster.inventory.GetItemCount(RoR2Content.Items.BoostDamage) * 0.60f));
                    }
                }
                else
                {
                    characterMaster.inventory.GiveItem(RoR2Content.Items.BoostHp, 5);
                    characterMaster.inventory.GiveItem(RoR2Content.Items.BoostDamage, 1);
                }
            }
        }
        public override void OnServerBossDefeated(BossGroup bossGroup)
        {
            base.OnServerBossDefeated(bossGroup);
            if (bossGroup.GetComponent<TeleporterInteraction>() != null)
            {
                TeleporterInteraction.instance.holdoutZoneController.FullyChargeHoldoutZone();
            }
        }


        public override GameObject GetTeleportEffectPrefab(GameObject objectToTeleport)
        {
            return LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/TeleportOutCrystalBoom");
        }
        public override void OverrideRuleChoices(RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, ulong runSeed)
        {
            base.OverrideRuleChoices(mustInclude, mustExclude, base.seed);
            base.ForceChoice(mustInclude, mustExclude, "Misc.StartingMoney.50");
        }



        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            bool flag = base.OnSerialize(writer, forceAll);
            if (forceAll)
            {
                writer.WritePackedIndex32(crystalsKilled_);
                return true;
            }
            bool flag2 = false;
            if ((base.syncVarDirtyBits & 128U) != 0U)
            {
                if (!flag2)
                {
                    writer.WritePackedUInt32(base.syncVarDirtyBits);
                    flag2 = true;
                }
                writer.WritePackedIndex32(this.crystalsKilled_);
            }
            if (!flag2)
            {
                writer.WritePackedUInt32(base.syncVarDirtyBits);
            }
            return flag2 || flag;
        }
        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            base.OnDeserialize(reader, initialState);
            if (initialState)
            {
                this.crystalsKilled_ = reader.ReadPackedIndex32();
                return;
            }
            int num = (int)reader.ReadPackedUInt32();
            if ((num & 128) != 0)
            {
                this.crystalsKilled_ = reader.ReadPackedIndex32();
            }
        }

        public override bool ShouldUpdateRunStopwatch()
        {
            if (WConfig.cfgPrismTimerAlwaysRun.Value)
            {
                return base.livingPlayerCount > 0;
            }
            return base.ShouldUpdateRunStopwatch();
        }
        public override bool ShouldAllowNonChampionBossSpawn()
        {
            return true; //Allows Hordes on stage 1 ig. Is that needed?
        }
        public int equipmentBarrelCount = 1;
        public float equipmentBarrelRadius = 20f;
        public SpawnCard equipmentBarrelSpawnCard;
        public override void OnPlayerSpawnPointsPlaced(SceneDirector sceneDirector)
        {
            //Spawn shit ass guranteed equipment barrel;
            if (this.stageClearCount == 0)
            {
                SpawnPoint spawnPoint = SpawnPoint.readOnlyInstancesList[0];
                if (spawnPoint)
                {
                    float num = 360f / this.equipmentBarrelCount;
                    int num2 = 0;
                    while ((long)num2 < (long)((ulong)this.equipmentBarrelCount))
                    {
                        Vector3 b = Quaternion.AngleAxis(num * (float)num2, Vector3.up) * (Vector3.forward * this.equipmentBarrelRadius);
                        DirectorPlacementRule directorPlacementRule = new DirectorPlacementRule();
                        directorPlacementRule.minDistance = 0f;
                        directorPlacementRule.maxDistance = 3f;
                        directorPlacementRule.placementMode = DirectorPlacementRule.PlacementMode.NearestNode;
                        directorPlacementRule.position = spawnPoint.transform.position + b;
                        DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(this.equipmentBarrelSpawnCard, directorPlacementRule, this.stageRng));
                        num2++;
                    }
                }
            }
        }

    }
}
