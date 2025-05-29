using R2API;
using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LittleGameplayFeatures
{
    public class TripleShopLegendary
    {

        public static InteractableSpawnCard RedMultiShopISC = ScriptableObject.CreateInstance<InteractableSpawnCard>();
        public static GameObject MiliMutliShopTerminal;


        public static void Start()
        {
            RedMultiShopMaker();
            if (WConfig.cfgRedMultiShop.Value == false)
            {
                return;
            }
    
            SceneDirector.onGenerateInteractableCardSelection += SceneDirector_onGenerateInteractableCardSelection;
        }

        private static void SceneDirector_onGenerateInteractableCardSelection(SceneDirector arg1, DirectorCardCategorySelection dccs)
        {
            //Debug log does not work in here (???)
            int rareIndex = dccs.FindCategoryIndexByName("Rare");
            if (rareIndex != -1)
            {
                if (dccs.categories[rareIndex].cards.Length == 1)
                {
                    //Kith check ig
                    return;
                }
                DirectorCard TrippleRed = new DirectorCard
                {
                    spawnCard = RedMultiShopISC,
                    selectionWeight = 2,
                    minimumStageCompletions = (Run.instance is InfiniteTowerRun) ? 2 : 4,
                };
                dccs.AddCard(rareIndex, TrippleRed);

               /* DirectorCard TrippleRed2 = new DirectorCard
                {
                    spawnCard = RedMultiShopISC,
                    selectionWeight = 2222,
                    minimumStageCompletions = (Run.instance is InfiniteTowerRun) ? 2 : 4,
                }; dccs.AddCard(rareIndex, TrippleRed2);
                dccs.categories[rareIndex].selectionWeight = 222222;*/
            }
        }

        internal static void RedMultiShopMaker()
        {
            GameObject MiliMutliShopMain = R2API.PrefabAPI.InstantiateClone(Resources.Load<GameObject>("prefabs/networkedobjects/chest/TripleShopLarge"), "TripleShopRed", true);
            MiliMutliShopTerminal = R2API.PrefabAPI.InstantiateClone(Resources.Load<GameObject>("prefabs/networkedobjects/chest/MultiShopLargeTerminal"), "MultiShopRedTerminal", true);


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

            Material MatMiliMultiShop = Object.Instantiate(tempprintermesh.material);
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
            temprenderers0 = HG.ArrayUtils.Join(temprenderers0, new Renderer[] { disc1renderer, disc2renderer });
            MiliMutliShopTerminal.GetComponent<DitherModel>().renderers = temprenderers0;
            //
            //
            MultiShopController mutlishopcontroller = MiliMutliShopMain.GetComponent<MultiShopController>();
            mutlishopcontroller.baseCost = 450;
            mutlishopcontroller.itemTier = ItemTier.Tier3;
            mutlishopcontroller.terminalPrefab = MiliMutliShopTerminal;

            PurchaseInteraction purchaseint = MiliMutliShopTerminal.GetComponent<PurchaseInteraction>();
            purchaseint.cost = 500;
            purchaseint.displayNameToken = "MULTISHOP_LEGENDARY_TERMINAL_NAME";

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


        }



        public static void AddPingIcon()
        {
            GameObject tempGoldChest = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/GoldChest/GoldChest.prefab").WaitForCompletion();
            if (tempGoldChest.GetComponent<PingInfoProvider>() != null)
            {
                MiliMutliShopTerminal.AddComponent<PingInfoProvider>().pingIconOverride = tempGoldChest.GetComponent<PingInfoProvider>().pingIconOverride;
                MiliMutliShopTerminal = null;
            }
        }



    }
}