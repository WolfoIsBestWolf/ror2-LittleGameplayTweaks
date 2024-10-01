using MonoMod.Cil;
using R2API;
using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
    public class ChangesInteractables
    {
        public static InteractableSpawnCard RedMultiShopISC = ScriptableObject.CreateInstance<InteractableSpawnCard>();
        public static DirectorCard ADTrippleRed = new DirectorCard { };
        public static InteractableSpawnCard iscShrineGoldFake = Object.Instantiate(LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscShrineGoldshoresAccess"));
        public static readonly GameObject MiliMutliShopMain = R2API.PrefabAPI.InstantiateClone(Resources.Load<GameObject>("prefabs/networkedobjects/chest/TripleShopLarge"), "TripleShopRed", true);
        public static readonly GameObject MiliMutliShopTerminal = R2API.PrefabAPI.InstantiateClone(Resources.Load<GameObject>("prefabs/networkedobjects/chest/MultiShopLargeTerminal"), "MultiShopRedTerminal", true);
        private static Material MatMiliMultiShop;
        //
        //
        //public static GameObject RedToWhiteSoup = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/LunarCauldron, RedToWhite Variant");
        //public static bool RedSoupBought = false;
        //
        public static void Start()
        {
            RedMultiShopMaker();
            FakeGoldShrine();
            StupidPriceChanger();
            Faster();

            On.RoR2.TimedChestController.PreStartClient += TimedChestController_PreStartClient;

            BasicPickupDropTable dtLockbox = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/TreasureCache/dtLockbox.asset").WaitForCompletion();
            dtLockbox.canDropBeReplaced = false;

            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Chest1StealthedVariant/Chest1StealthedVariant.prefab").WaitForCompletion().GetComponent<RoR2.ChestBehavior>().dropTable = dtLockbox;
            Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/TreasureCacheVoid/dtVoidLockbox.asset").WaitForCompletion().canDropBeReplaced = false;
            Addressables.LoadAssetAsync<FreeChestDropTable>(key: "RoR2/DLC1/FreeChest/dtFreeChest.asset").WaitForCompletion().canDropBeReplaced = false;


            GameObject DeepVoidPortalBattery = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/DeepVoidPortalBattery/DeepVoidPortalBattery.prefab").WaitForCompletion();
            DeepVoidPortalBattery.GetComponent<HoldoutZoneController>().baseChargeDuration = WConfig.FasterDeepVoidSignal.Value;
            DeepVoidPortalBattery.GetComponent<HoldoutZoneController>().baseRadius += 5;
            //Maybe Reduce the time by like 10 seconds idk

            GameObject Teleporter1 = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Teleporters/Teleporter1");
            GameObject Teleporter2 = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Teleporters/LunarTeleporter Variant");

            //Logger.LogMessage(YellowPercentage.Value * 100 + "% Yellow Percentage");
            Teleporter1.GetComponent<BossGroup>().bossDropChance = WConfig.YellowPercentage.Value / 100;
            Teleporter2.GetComponent<BossGroup>().bossDropChance = WConfig.YellowPercentage.Value / 100;

            Teleporter1.GetComponent<TeleporterInteraction>().baseShopSpawnChance = WConfig.ShopChancePercentage.Value / 100; ;
            //Teleporter2.GetComponent<TeleporterInteraction>().baseShopSpawnChance = WConfig.ShopChancePercentage.Value / 100; ;

            if(WConfig.VoidPortalChance.Value)
            {
                Teleporter1.GetComponent<PortalSpawner>().minStagesCleared = 4;
                Teleporter2.GetComponent<PortalSpawner>().spawnChance = 0.2f;

                Teleporter2.GetComponent<PortalSpawner>().minStagesCleared = 4;
                Teleporter2.GetComponent<PortalSpawner>().spawnChance = 1f;
            }

            if (WConfig.InteractablesCombatShrineHP.Value == false)
            {
                GameObject ShrineCombat = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShrineCombat/ShrineCombat.prefab").WaitForCompletion();
                ShrineCombat.GetComponent<CombatSquad>().grantBonusHealthInMultiplayer = false;
                ShrineCombat = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShrineCombat/ShrineCombatSandy Variant.prefab").WaitForCompletion();
                ShrineCombat.GetComponent<CombatSquad>().grantBonusHealthInMultiplayer = false;
                ShrineCombat = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShrineCombat/ShrineCombatSnowy Variant.prefab").WaitForCompletion();
                ShrineCombat.GetComponent<CombatSquad>().grantBonusHealthInMultiplayer = false;
            }

            //This is kinda dumb
            On.EntityStates.LunarTeleporter.LunarTeleporterBaseState.FixedUpdate += LunarTeleporterBaseState_FixedUpdate;
        }

        private static void TimedChestController_PreStartClient(On.RoR2.TimedChestController.orig_PreStartClient orig, TimedChestController self)
        {
            orig(self);
            if (Run.instance.stageClearCount > 3)
            {
                float newTime = self.lockTime / 2 * Run.instance.stageClearCount;
                self.lockTime = newTime;
            }          
        }

        public static void LunarTeleporterBaseState_FixedUpdate(On.EntityStates.LunarTeleporter.LunarTeleporterBaseState.orig_FixedUpdate orig, EntityStates.LunarTeleporter.LunarTeleporterBaseState self)
        {
            self.fixedAge += Time.fixedDeltaTime;
            if (TeleporterInteraction.instance)
            {
                if (TeleporterInteraction.instance.isInFinalSequence)
                {
                    self.genericInteraction.Networkinteractability = Interactability.Disabled;
                    return;
                }
                self.genericInteraction.Networkinteractability = self.preferredInteractability;
            }
        }

        public static void Faster()
        {
            if (WConfig.FasterPrinter.Value == true)
            {
                IL.RoR2.PurchaseInteraction.CreateItemTakenOrb += (ILContext il) =>
                {
                    ILCursor c = new ILCursor(il);
                    if (c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdcR4(1.5f)))
                    {
                        c.Next.Operand = 0.75f;
                        //Debug.Log("IL Found: PurchaseInteraction.CreateItemTakenOrb");
                    }
                    else
                    {
                        Debug.LogWarning("IL Failed: PurchaseInteraction.CreateItemTakenOrb");
                    }
                };

                RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/chest/Duplicator").GetComponent<RoR2.EntityLogic.DelayedEvent>().enabled = false;
                RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/chest/DuplicatorLarge").GetComponent<RoR2.EntityLogic.DelayedEvent>().enabled = false;
                RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/chest/DuplicatorMilitary").GetComponent<RoR2.EntityLogic.DelayedEvent>().enabled = false;
                RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/chest/DuplicatorWild").GetComponent<RoR2.EntityLogic.DelayedEvent>().enabled = false;

                On.EntityStates.Duplicator.Duplicating.DropDroplet += (orig, self) =>
                {
                    orig(self);
                    if (NetworkServer.active)
                    {
                        self.outer.GetComponent<PurchaseInteraction>().Networkavailable = true;
                    }
                };

                EntityStateConfiguration Duplicating = RoR2.LegacyResourcesAPI.Load<EntityStateConfiguration>("EntityStateConfigurations/EntityStates.Duplicator.Duplicating");

                Duplicating.serializedFieldsCollection.serializedFields[0].fieldValue.stringValue = "0.6";
                Duplicating.serializedFieldsCollection.serializedFields[1].fieldValue.stringValue = "1.25";
            }

            if (WConfig.FasterScrapper.Value == true)
            {
                IL.RoR2.ScrapperController.CreateItemTakenOrb += (ILContext il) =>
                {
                    ILCursor c = new ILCursor(il);
                    if (c.TryGotoNext(MoveType.Before,
                        x => x.MatchLdcR4(1.5f)))
                    {
                        c.Next.Operand = 0.75f;
                        //Debug.Log("IL Found: ScrapperController.CreateItemTakenOrb");
                    }
                    else
                    {
                        Debug.LogWarning("IL Failed: ScrapperController.CreateItemTakenOrb");
                    }
                };

                EntityStateConfiguration Scrapping = RoR2.LegacyResourcesAPI.Load<EntityStateConfiguration>("EntityStateConfigurations/EntityStates.Scrapper.Scrapping");
                EntityStateConfiguration ScrappingToIdle = RoR2.LegacyResourcesAPI.Load<EntityStateConfiguration>("EntityStateConfigurations/EntityStates.Scrapper.ScrappingToIdle");
                EntityStateConfiguration WaitToBeginScrapping = RoR2.LegacyResourcesAPI.Load<EntityStateConfiguration>("EntityStateConfigurations/EntityStates.Scrapper.WaitToBeginScrapping");

                Scrapping.serializedFieldsCollection.serializedFields[2].fieldValue.stringValue = "1";
                ScrappingToIdle.serializedFieldsCollection.serializedFields[2].fieldValue.stringValue = "0.4";
                WaitToBeginScrapping.serializedFieldsCollection.serializedFields[0].fieldValue.stringValue = "0.9";

            }




            if (WConfig.FasterShrines.Value == true)
            {
                On.RoR2.ShrineBloodBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    if (NetworkServer.active)
                    {
                        self.refreshTimer = 1;
                    }
                };
                On.RoR2.ShrineBossBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    if (NetworkServer.active)
                    {
                        self.refreshTimer = 1;
                    }
                };
                On.RoR2.ShrineChanceBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    if (NetworkServer.active)
                    {
                        self.refreshTimer = 1;
                    }
                };
                On.RoR2.ShrineCombatBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    if (NetworkServer.active)
                    {
                        self.refreshTimer = 1;
                    }
                };
                On.RoR2.ShrineHealingBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    if (NetworkServer.active)
                    {
                        self.refreshTimer = 1;
                    }
                };
                On.RoR2.ShrineRestackBehavior.AddShrineStack += (orig, self, activator) =>
                {
                    orig(self, activator);
                    if (NetworkServer.active)
                    {
                        self.refreshTimer = 1;
                    }
                };
            }

        }

        public static void StupidPriceChanger()
        {
            if (WConfig.InteractableHealingShrine.Value)
            {
                RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineHealing").GetComponent<RoR2.PurchaseInteraction>().cost = 10;
                RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineHealing").GetComponent<RoR2.ShrineHealingBehavior>().costMultiplierPerPurchase = 1.4f;
                RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineHealing").GetComponent<RoR2.ShrineHealingBehavior>().maxPurchaseCount += 1;
            }
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BrokenDrones/MegaDroneBroken").GetComponent<RoR2.PurchaseInteraction>().cost = WConfig.MegaDroneCost.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BrokenDrones/Turret1Broken").GetComponent<RoR2.PurchaseInteraction>().cost = WConfig.TurretDroneCost.Value;

            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/LunarCauldron, RedToWhite Variant").GetComponent<ShopTerminalBehavior>().dropVelocity = new Vector3(5, 10, 5);


            if (WConfig.InteractableFastHalcyShrine.Value)
            {
                RoR2.LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/InteractableSpawnCard/iscShrineHalcyon").prefab.GetComponent<HalcyoniteShrineInteractable>().goldDrainValue = 3;
            }
            if (WConfig.InteractableFastHalcyShrine.Value)
            {
                RoR2.LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/InteractableSpawnCard/iscShrineHalcyon").prefab.GetComponent<HalcyoniteShrineInteractable>().monsterCredit = 80;
            }

            if (WConfig.InteractableNoLunarCost.Value == true)
            {
                On.RoR2.PurchaseInteraction.Awake += (orig, self) =>
                {
                    orig(self);
                    if (self.costType == CostTypeIndex.LunarCoin)
                    {
                        self.cost = 0;
                    }
                };
            }

            if (WConfig.InteractableBloodShrineScaleWithTime.Value == true)
            {
                On.RoR2.ShrineBloodBehavior.AddShrineStack += SetShrineBloodAmount;
            }
            if (WConfig.InteractableBloodShrineLessCost.Value == true)
            {
 
                On.RoR2.ShrineBloodBehavior.AddShrineStack += (orig, self, interactor) =>
                {
                    orig(self, interactor);
                    //self.costMultiplierPerPurchase = 1;
                    if (self.purchaseCount == 1)
                    {
                        //self.purchaseInteraction.cost = 70;
                        self.costMultiplierPerPurchase = 1.75f;
                    }
                    else if (self.purchaseCount == 2)
                    {
                        //self.purchaseInteraction.cost = 90;
                        self.costMultiplierPerPurchase = 1.93f;
                    }
                };
            }
            if (WConfig.InteractableRedSoupAmount.Value != 0)
            {
                On.RoR2.ShopTerminalBehavior.DropPickup += RedToWhiteSoupMore;
            }

        }

        private static void RedToWhiteSoupMore(On.RoR2.ShopTerminalBehavior.orig_DropPickup orig, ShopTerminalBehavior self)
        {
            if (self.name.StartsWith("LunarCauldron, RedToWhite"))
            {
                if (!self.GetComponent<PurchaseInteraction>().available)
                {
                    for (int i = 0; i < WConfig.InteractableRedSoupAmount.Value; i++)
                    {
                        PickupDropletController.CreatePickupDroplet(self.pickupIndex, (self.dropTransform ? self.dropTransform : self.transform).position, self.transform.TransformVector(self.dropVelocity));
                    };
                }
                if (!self.hasBeenPurchased)
                {

                }
            }
            orig(self);
        }

        private static void SetShrineBloodAmount(On.RoR2.ShrineBloodBehavior.orig_AddShrineStack orig, ShrineBloodBehavior self, Interactor interactor)
        {
            CharacterBody component = interactor.GetComponent<CharacterBody>();

            if(Stage.instance && component)
            {
                int newGold = 50 + self.purchaseCount * 25;
                float difficultyScaledCost = Run.instance.GetDifficultyScaledCost(newGold, Stage.instance.entryDifficultyCoefficient);
                //_entryDifficultyCoefficient

                float HealthForDiv = component.baseMaxHealth + component.levelMaxHealth * (component.level - 1);

                self.goldToPaidHpRatio = difficultyScaledCost / HealthForDiv / self.purchaseInteraction.cost * 100;
            }
            orig(self, interactor);
        }

        internal static void FakeGoldShrine()
        {
            iscShrineGoldFake.name = "iscShrineGoldFake";
            iscShrineGoldFake.maxSpawnsPerStage = -1;
            GameObject FakeGoldShrine = PrefabAPI.InstantiateClone(iscShrineGoldFake.prefab, "iscShrineGoldFake", true);
            FakeGoldShrine.name = "FakeGoldShrine";

            GameObject.Destroy(FakeGoldShrine.GetComponent<RoR2.Hologram.HologramProjector>());
            GameObject.Destroy(FakeGoldShrine.GetComponent<PurchaseAvailabilityIndicator>());
            GameObject.Destroy(FakeGoldShrine.GetComponent<PortalStatueBehavior>());
            GameObject.Destroy(FakeGoldShrine.GetComponent<PurchaseInteraction>());
            GameObject.Destroy(FakeGoldShrine.transform.GetChild(3).gameObject);
            GameObject.Destroy(FakeGoldShrine.transform.GetChild(2).gameObject);
            GameObject.Destroy(FakeGoldShrine.transform.GetChild(1).gameObject);

            iscShrineGoldFake.prefab = FakeGoldShrine;
        }

        internal static void RedMultiShopMaker()
        {

            Texture2D TexBlackMultiShopT = new Texture2D(64, 64, TextureFormat.DXT1, false)
            {
                filterMode = FilterMode.Bilinear
            };
            TexBlackMultiShopT.LoadImage(Properties.Resources.texRedMultiChestPodDiffuse, false);

            MiliMutliShopMain.GetComponentInChildren<RandomizeSplatBias>().enabled = false;
            MiliMutliShopTerminal.GetComponentInChildren<RandomizeSplatBias>().enabled = false;

            MeshRenderer tempshopmesh = MiliMutliShopMain.GetComponentInChildren<MeshRenderer>();
            SkinnedMeshRenderer tempterminalmesh = MiliMutliShopTerminal.GetComponentInChildren<SkinnedMeshRenderer>();
            SkinnedMeshRenderer tempprintermesh = Resources.Load<GameObject>("prefabs/networkedobjects/chest/DuplicatorMilitary").GetComponentInChildren<SkinnedMeshRenderer>();

            MatMiliMultiShop = Object.Instantiate(tempprintermesh.material);
            MatMiliMultiShop.mainTexture = TexBlackMultiShopT;
            //MatMiliMultiShop.shaderKeywords = new string[0];

            tempshopmesh.material = MatMiliMultiShop;
            tempshopmesh.sharedMaterial = MatMiliMultiShop;
            tempterminalmesh.material = MatMiliMultiShop;
            tempterminalmesh.sharedMaterial = MatMiliMultiShop;

            GameObject laserturbineog = Resources.Load<ItemDef>("itemdefs/LaserTurbine").pickupModelPrefab.gameObject;
            Transform laserturbineprefab = laserturbineog.transform.GetChild(0).GetChild(1);

            Transform temp01 = Object.Instantiate(laserturbineprefab.transform, MiliMutliShopTerminal.transform.GetChild(0));
            Transform temp02 = Object.Instantiate(laserturbineprefab.transform, MiliMutliShopTerminal.transform.GetChild(0));

            temp01.localScale = new Vector3(1.8f, 1.8f, 1.6f);
            temp02.localScale = new Vector3(1.8f, 1.8f, 1.6f);

            temp01.localPosition = new Vector3(-0.0029f, 0.5176f, 0.0073f);
            temp02.localPosition = new Vector3(-0.0029f, 5.9676f, 0.0073f);

            temp01.rotation = new Quaternion(180f, 0f, 0f, 180f);
            temp02.rotation = new Quaternion(180f, 0f, 0f, 180f);


            Renderer disc1renderer = temp01.GetComponent<MeshRenderer>();
            Renderer disc2renderer = temp02.GetComponent<MeshRenderer>();
            disc1renderer.material = Object.Instantiate(disc1renderer.material);
            disc1renderer.material.SetColor("_EmColor", new Color(1f, 0f, 0f, 0f));
            disc2renderer.material = disc1renderer.material;

            Renderer[] temprenderers0 = MiliMutliShopTerminal.GetComponent<DitherModel>().renderers;
            temprenderers0 = temprenderers0.Add(disc1renderer, disc2renderer);
            MiliMutliShopTerminal.GetComponent<DitherModel>().renderers = temprenderers0;
            //
            //
            MultiShopController mutlishopcontroller = MiliMutliShopMain.GetComponent<MultiShopController>();
            mutlishopcontroller.baseCost = 450;
            mutlishopcontroller.itemTier = ItemTier.Tier3;
            mutlishopcontroller.terminalPrefab = MiliMutliShopTerminal;

            PurchaseInteraction purchaseint = MiliMutliShopTerminal.GetComponent<PurchaseInteraction>();
            purchaseint.cost = 500;
            purchaseint.displayNameToken = "Mili-Multishop Terminal";

            ShopTerminalBehavior shopbehavior = MiliMutliShopTerminal.GetComponent<ShopTerminalBehavior>();
            shopbehavior.itemTier = ItemTier.Tier3;
            shopbehavior.dropTable = Addressables.LoadAssetAsync<PickupDropTable>(key: "RoR2/Base/GoldChest/dtGoldChest.asset").WaitForCompletion();
            //



            GameObject TripleShopLarge = Resources.Load<GameObject>("prefabs/networkedobjects/chest/TripleShopLarge");
            TripleShopLarge.transform.GetChild(0).localPosition = new Vector3(0, 6, 0);
            MiliMutliShopMain.transform.GetChild(0).localPosition = new Vector3(0, 6, 0);


            //RedMultiShopISC = 
            RedMultiShopISC.name = "iscTripleShopRed";
            RedMultiShopISC.prefab = MiliMutliShopMain;
            RedMultiShopISC.sendOverNetwork = true;
            RedMultiShopISC.hullSize = HullClassification.Human;
            RedMultiShopISC.nodeGraphType = MapNodeGroup.GraphType.Ground;
            RedMultiShopISC.requiredFlags = NodeFlags.None;
            RedMultiShopISC.forbiddenFlags = NodeFlags.NoChestSpawn;
            RedMultiShopISC.directorCreditCost = 50;
            RedMultiShopISC.occupyPosition = false;
            RedMultiShopISC.eliteRules = SpawnCard.EliteRules.Default;
            RedMultiShopISC.orientToFloor = false;
            RedMultiShopISC.slightlyRandomizeOrientation = false;
            RedMultiShopISC.skipSpawnWhenSacrificeArtifactEnabled = true;

            ADTrippleRed.spawnCard = ChangesInteractables.RedMultiShopISC;
                ADTrippleRed.selectionWeight = 2;
            ADTrippleRed.minimumStageCompletions = 4;
        }


        public static void ModSupport()
        {
            GameObject tempGoldChest = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/GoldChest/GoldChest.prefab").WaitForCompletion();
            if (tempGoldChest.GetComponent<PingInfoProvider>() != null)
            {
                MiliMutliShopTerminal.AddComponent<PingInfoProvider>().pingIconOverride = tempGoldChest.GetComponent<PingInfoProvider>().pingIconOverride;
            }
            //
            GameObject tempGoldShrine = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShrineGoldshoresAccess/ShrineGoldshoresAccess.prefab").WaitForCompletion();
            if (tempGoldShrine.GetComponent<PingInfoProvider>() != null)
            {
                iscShrineGoldFake.prefab.AddComponent<PingInfoProvider>().pingIconOverride = tempGoldShrine.GetComponent<PingInfoProvider>().pingIconOverride;
            }
        }

    }
}