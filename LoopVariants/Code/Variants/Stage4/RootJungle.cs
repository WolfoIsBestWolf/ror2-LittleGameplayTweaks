using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using EntityStates.Fauna;

namespace LoopVariants
{
    public class Variants_4_RootJungle
    {

        public static Material RJDistantTreeFoliage;
        public static Material RJfern_LOD0;
        public static Material spmRJgrass1_LOD0;
        public static Material spmRJgrass2_LOD0;
        public static Material RJHangingMoss_LOD0;
        public static Material RJMossFoliage_LOD0;
        public static Material RJpinkshroom_LOD0;
        public static Material RJShroomFoliage_LOD0;
        public static Material RJTowerTreeFoliage;
        public static Material RJTreeBigFoliage;
        public static Material RJTreeBigFoliage_LOD0;
        public static Material matRootJungleSpore;

        public static Material matRJFogFloor;
        public static Material matRJLeaf_Blue;
        public static Material matRJLeaf_Green;
        public static Material matRJMossPatch1;
        public static Material matRJMossPatch2;
        public static Material matRJMossPatchLarge;
        public static Material matRJMossPatchTriplanar;
        public static Material matRJPebble;
        public static Material matRJRock;
        public static Material matRJSandstone;
        public static Material matRJShroomBig;
        public static Material matRJShroomBounce;
        public static Material matRJShroomShelf;
        public static Material matRJShroomSmall;
        public static Material matRJTemple;
        public static Material matRJTerrain;
        public static Material matRJTerrain2;
        public static Material matRJTree;
        public static Material matRJTreeLOD;
        public static Material matRJTriangle;

        //Not in the RJ folder but used anyways
 
        public static Material Material_2;

        public static Material Fronds_0; //Ferns

        public static GameObject Fruit;
        public static GameObject FruitFall;
        public static GameObject JellyfishDeath;


        public static void Setup()
        {
            On.EntityStates.Fauna.HabitatFruitDeathState.OnEnter += FixDumbFruit;
            JellyfishDeath = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Jellyfish/JellyfishDeath.prefab").WaitForCompletion();
            EntityStates.Fauna.HabitatFruitDeathState.deathSoundString = "Play_jellyfish_death";
            //EntityStates.Fauna.HabitatFruitDeathState.healPackCount = 2;
            EntityStates.Fauna.HabitatFruitDeathState.healPackMaxVelocity = 60;
            EntityStates.Fauna.HabitatFruitDeathState.fractionalHealing = 0.15f;
            EntityStates.Fauna.HabitatFruitDeathState.scale = 1;
            LanguageAPI.Add("MUSHROOMDRIPS_NAME", "Mushroom Nectar");

            Texture2D texRJMossPatch1Diffuse = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            texRJMossPatch1Diffuse.LoadImage(Properties.Resources.texRJMossPatch1Diffuse, false);
            texRJMossPatch1Diffuse.filterMode = FilterMode.Bilinear;
            texRJMossPatch1Diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJMossPatch2Diffuse = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            texRJMossPatch2Diffuse.LoadImage(Properties.Resources.texRJMossPatch2Diffuse, false);
            texRJMossPatch2Diffuse.filterMode = FilterMode.Bilinear;
            texRJMossPatch2Diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJLichenTerrain = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texRJLichenTerrain.LoadImage(Properties.Resources.texRJLichenTerrain, false);
            texRJLichenTerrain.filterMode = FilterMode.Bilinear;
            texRJLichenTerrain.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJShroomBounceDiffuse = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            texRJShroomBounceDiffuse.LoadImage(Properties.Resources.texRJShroomBounceDiffuse, false);
            texRJShroomBounceDiffuse.filterMode = FilterMode.Bilinear;
            texRJShroomBounceDiffuse.wrapMode = TextureWrapMode.Repeat;


            Texture2D texRJShroomBigDiffuse = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            texRJShroomBigDiffuse.LoadImage(Properties.Resources.texRJShroomBigDiffuse, false);
            texRJShroomBigDiffuse.filterMode = FilterMode.Bilinear;
            texRJShroomBigDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJShroomBigEmissive = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            texRJShroomBigEmissive.LoadImage(Properties.Resources.texRJShroomBigEmissive, false);
            texRJShroomBigEmissive.filterMode = FilterMode.Bilinear;
            texRJShroomBigEmissive.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJShroomShelfDiffuse = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            texRJShroomShelfDiffuse.LoadImage(Properties.Resources.texRJShroomShelfDiffuse, false);
            texRJShroomShelfDiffuse.filterMode = FilterMode.Bilinear;
            texRJShroomShelfDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJShroomShelfEmissive = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            texRJShroomShelfEmissive.LoadImage(Properties.Resources.texRJShroomShelfEmissive, false);
            texRJShroomShelfEmissive.filterMode = FilterMode.Bilinear;
            texRJShroomShelfEmissive.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJShroomSmallDiffuse = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            texRJShroomSmallDiffuse.LoadImage(Properties.Resources.texRJShroomSmallDiffuse, false);
            texRJShroomSmallDiffuse.filterMode = FilterMode.Bilinear;
            texRJShroomSmallDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJShroomSmallEmissive = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            texRJShroomSmallEmissive.LoadImage(Properties.Resources.texRJShroomSmallEmissive, false);
            texRJShroomSmallEmissive.filterMode = FilterMode.Bilinear;
            texRJShroomSmallEmissive.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRampRJMushroom = new Texture2D(16, 256, TextureFormat.DXT5, false);
            texRampRJMushroom.LoadImage(Properties.Resources.texRampRJMushroom, false);
            texRampRJMushroom.filterMode = FilterMode.Bilinear;
            texRampRJMushroom.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRJMoss = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texRJMoss.LoadImage(Properties.Resources.texRJMoss, false);
            texRJMoss.filterMode = FilterMode.Bilinear;
            texRJMoss.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJTree = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texRJTree.LoadImage(Properties.Resources.texRJTree, false);
            texRJTree.filterMode = FilterMode.Bilinear;
            texRJTree.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJTreeTop = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texRJTreeTop.LoadImage(Properties.Resources.texRJMoss, false);
            texRJTreeTop.filterMode = FilterMode.Bilinear;
            texRJTreeTop.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJTriangleDiffuse = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            texRJTriangleDiffuse.LoadImage(Properties.Resources.texRJTriangleDiffuse, false);
            texRJTriangleDiffuse.filterMode = FilterMode.Bilinear;
            texRJTriangleDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJGrassTerrain = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texRJGrassTerrain.LoadImage(Properties.Resources.texRJGrassTerrain, false);
            texRJGrassTerrain.filterMode = FilterMode.Bilinear;
            texRJGrassTerrain.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJGrassTerrain2 = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texRJGrassTerrain2.LoadImage(Properties.Resources.texRJGrassTerrain2, false);
            texRJGrassTerrain2.filterMode = FilterMode.Bilinear;
            texRJGrassTerrain2.wrapMode = TextureWrapMode.Repeat;


            Texture2D RJDistantTreeFoliage_Color = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            RJDistantTreeFoliage_Color.LoadImage(Properties.Resources.RJDistantTreeFoliage_Color, false);
            RJDistantTreeFoliage_Color.filterMode = FilterMode.Bilinear;
            RJDistantTreeFoliage_Color.wrapMode = TextureWrapMode.Repeat;

            Texture2D TEX_RJTowerTreeFoliage = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            TEX_RJTowerTreeFoliage.LoadImage(Properties.Resources.RJTowerTreeFoliage_Color, false);
            TEX_RJTowerTreeFoliage.filterMode = FilterMode.Bilinear;
            TEX_RJTowerTreeFoliage.wrapMode = TextureWrapMode.Repeat;


            Texture2D spmRJgrass1_Color = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            spmRJgrass1_Color.LoadImage(Properties.Resources.spmRJgrass1_Color, false);
            spmRJgrass1_Color.filterMode = FilterMode.Bilinear;
            spmRJgrass1_Color.wrapMode = TextureWrapMode.Repeat;

            Texture2D spmRJgrass1_Subsurface = new Texture2D(512, 512, TextureFormat.DXT1, false);
            spmRJgrass1_Subsurface.LoadImage(Properties.Resources.spmRJgrass1_Subsurface, false);
            spmRJgrass1_Subsurface.filterMode = FilterMode.Bilinear;
            spmRJgrass1_Subsurface.wrapMode = TextureWrapMode.Repeat;

            Texture2D spmRJgrass2_Color = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            spmRJgrass2_Color.LoadImage(Properties.Resources.spmRJgrass2_Color, false);
            spmRJgrass2_Color.filterMode = FilterMode.Bilinear;
            spmRJgrass2_Color.wrapMode = TextureWrapMode.Repeat;

            Texture2D spmRJgrass2_Subsurface = new Texture2D(512, 512, TextureFormat.DXT1, false);
            spmRJgrass2_Subsurface.LoadImage(Properties.Resources.spmRJgrass2_Subsurface, false);
            spmRJgrass2_Subsurface.filterMode = FilterMode.Bilinear;
            spmRJgrass2_Subsurface.wrapMode = TextureWrapMode.Repeat;

            Texture2D RJHangingMoss_Color = new Texture2D(128, 512, TextureFormat.DXT5, false);
            RJHangingMoss_Color.LoadImage(Properties.Resources.RJHangingMoss_Color, false);
            RJHangingMoss_Color.filterMode = FilterMode.Bilinear;
            RJHangingMoss_Color.wrapMode = TextureWrapMode.Repeat;

            Texture2D RJMossFoliage_Color = new Texture2D(512, 512, TextureFormat.DXT5, false);
            RJMossFoliage_Color.LoadImage(Properties.Resources.RJMossFoliage_Color, false);
            RJMossFoliage_Color.filterMode = FilterMode.Bilinear;
            RJMossFoliage_Color.wrapMode = TextureWrapMode.Repeat;

            Texture2D RJpinkshroom_Color = new Texture2D(512, 512, TextureFormat.DXT5, false);
            RJpinkshroom_Color.LoadImage(Properties.Resources.RJpinkshroom_Color, false);
            RJpinkshroom_Color.filterMode = FilterMode.Bilinear;
            RJpinkshroom_Color.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRJpinkshroomEmission = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texRJpinkshroomEmission.LoadImage(Properties.Resources.texRJpinkshroomEmission, false);
            texRJpinkshroomEmission.filterMode = FilterMode.Bilinear;
            texRJpinkshroomEmission.wrapMode = TextureWrapMode.Repeat;

            Texture2D RJShroomFoliage_Color = new Texture2D(512, 512, TextureFormat.DXT5, false);
            RJShroomFoliage_Color.LoadImage(Properties.Resources.RJShroomFoliage_Color, false);
            RJShroomFoliage_Color.filterMode = FilterMode.Bilinear;
            RJShroomFoliage_Color.wrapMode = TextureWrapMode.Repeat;

            Texture2D RJTreeBigFoliage_Color = new Texture2D(128, 512, TextureFormat.DXT5, false);
            RJTreeBigFoliage_Color.LoadImage(Properties.Resources.RJTreeBigFoliage_Color, false);
            RJTreeBigFoliage_Color.filterMode = FilterMode.Bilinear;
            RJTreeBigFoliage_Color.wrapMode = TextureWrapMode.Repeat;
            //
            //
            RJDistantTreeFoliage = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/RJDistantTreeFoliage.st").WaitForCompletion());
            RJDistantTreeFoliage.mainTexture = RJDistantTreeFoliage_Color;
            RJDistantTreeFoliage.color = Color.white; //0.6706 0.6706 0.6706 1

            RJfern_LOD0 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/RJfern_LOD0.mat").WaitForCompletion());
            
            spmRJgrass1_LOD0 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/spmRJgrass1_LOD0.mat").WaitForCompletion());
            spmRJgrass1_LOD0.mainTexture = spmRJgrass1_Color;
            spmRJgrass1_LOD0.SetTexture("_SubsurfaceTex", spmRJgrass1_Subsurface);
            spmRJgrass1_LOD0.color = Color.white;

            spmRJgrass2_LOD0 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/spmRJgrass2_LOD0.mat").WaitForCompletion());
            spmRJgrass2_LOD0.mainTexture = spmRJgrass2_Color;
            spmRJgrass2_LOD0.SetTexture("_SubsurfaceTex", spmRJgrass2_Subsurface);
            spmRJgrass2_LOD0.color = Color.white;

            RJHangingMoss_LOD0 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/RJHangingMoss_LOD0.mat").WaitForCompletion());
            RJHangingMoss_LOD0.mainTexture = RJHangingMoss_Color;
            RJHangingMoss_LOD0.color = Color.white;

            RJMossFoliage_LOD0 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/RJMossFoliage_LOD0.mat").WaitForCompletion());
            RJMossFoliage_LOD0.color = new Color(0.8679f, 0.8416f, 0.479f, 1); //0.479 0.8416 0.8679 1
            RJMossFoliage_LOD0.mainTexture = RJMossFoliage_Color;

            RJpinkshroom_LOD0 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/RJpinkshroom_LOD0.mat").WaitForCompletion());
            RJpinkshroom_LOD0.mainTexture = RJpinkshroom_Color;
            RJpinkshroom_LOD0.SetTexture("_EmissionTex", texRJpinkshroomEmission);
            //_EmissionTint

            RJShroomFoliage_LOD0 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/RJShroomFoliage_LOD0.mat").WaitForCompletion());
            RJShroomFoliage_LOD0.mainTexture = RJShroomFoliage_Color;

            RJTowerTreeFoliage = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/RJTowerTreeFoliage.st").WaitForCompletion());
            RJTowerTreeFoliage.mainTexture = TEX_RJTowerTreeFoliage;
            RJTowerTreeFoliage.color = new Color(1.34f, 1.34f, 1.34f, 1f); //0.6706 0.6706 0.6706 1

            RJTreeBigFoliage = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/RJTreeBigFoliage.st").WaitForCompletion());
            RJTreeBigFoliage_LOD0 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/RJTreeBigFoliage_LOD0.mat").WaitForCompletion());
            RJTreeBigFoliage.mainTexture = RJTreeBigFoliage_Color;

            matRootJungleSpore = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRootJungleSpore.mat").WaitForCompletion());
            matRootJungleSpore.SetColor("_TintColor", new Color(12f, 10.7f, 3.5f,1f)); //3.5137 6.7137 16 1

            matRJFogFloor = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJFogFloor.mat").WaitForCompletion());
            matRJFogFloor.SetColor("_TintColor", new Color(0.1132f, 0.1061f, 0.0294f, 1f)); //0.0294 0.1132 0.1061 1

            matRJLeaf_Blue = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJLeaf_Blue.mat").WaitForCompletion());
            matRJLeaf_Blue.SetColor("_EmissionColor", new Color(0.5f,0.1f,0.1f,1f)); //0.0382 0.3095 0.5841 1

            matRJLeaf_Green = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJLeaf_Green.mat").WaitForCompletion());
            matRJLeaf_Green.SetColor("_EmissionColor", new Color(0.5f, 0.2f, 0.2f, 1f)); //0.0382 0.3095 0.5841 1

            matRJMossPatch1 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJMossPatch1.mat").WaitForCompletion());
            matRJMossPatch1.mainTexture = texRJMossPatch1Diffuse;

            matRJMossPatch2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJMossPatch2.mat").WaitForCompletion());
            matRJMossPatch2.mainTexture = texRJMossPatch2Diffuse;
            matRJMossPatch2.SetTexture("_SnowTex", texRJLichenTerrain);

            matRJMossPatchLarge = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJMossPatchLarge.mat").WaitForCompletion());
            matRJMossPatchLarge.SetTexture("_GreenChannelTex", texRJLichenTerrain);
            matRJMossPatchLarge.SetTexture("_RedChannelTopTex", texRJGrassTerrain);
            matRJMossPatchLarge.SetTexture("_RedChannelSideTex", texRJGrassTerrain);

            matRJMossPatchTriplanar = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJMossPatchTriplanar.mat").WaitForCompletion());
            matRJMossPatchTriplanar.SetTexture("_GreenChannelTex", texRJLichenTerrain);
            matRJMossPatchTriplanar.SetTexture("_RedChannelTopTex", texRJGrassTerrain);

            matRJPebble = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJPebble.mat").WaitForCompletion());
            //NoTextures
            //0.4118 0.3804 0.3176 1
            
            matRJRock = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJRock.mat").WaitForCompletion());
            //matRJRock.SetTexture("_BlueChannelTex", texRJDirtDiffuse);
            matRJRock.SetTexture("_GreenChannelTex", texRJGrassTerrain);
            //matRJRock.SetTexture("_RedChannelTopTex", texGPRockSide);
            //matRJRock.SetTexture("_RedChannelSideTex", texBlackbeachBasicRockSide);
 

            matRJSandstone = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJSandstone.mat").WaitForCompletion());
            matRJSandstone.SetTexture("_BlueChannelTex", texRJLichenTerrain);
            matRJSandstone.SetTexture("_GreenChannelTex", texRJGrassTerrain2);
 
            matRJShroomBig = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJShroomBig.mat").WaitForCompletion());
            matRJShroomBig.mainTexture = texRJShroomBigDiffuse;
            matRJShroomBig.SetTexture("_BlueChannelTex", texRJMoss);
            matRJShroomBig.SetTexture("_FlowHeightRamp", texRampRJMushroom);
            matRJShroomBig.SetTexture("_FlowHeightmap", texRJShroomBigEmissive);
            matRJShroomBig.SetTexture("_GreenChannelTex", texRJLichenTerrain);
            matRJShroomBig.SetFloat("_FlowEmissionStrength", 0.4f);

            matRJShroomBounce = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJShroomBounce.mat").WaitForCompletion());
            matRJShroomBounce.mainTexture = texRJShroomBounceDiffuse;
            //matRJShroomBounce.SetTexture("_FresnelRamp", texRampHuntressSoft);
 
            matRJShroomShelf = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJShroomShelf.mat").WaitForCompletion());
            matRJShroomShelf.mainTexture = texRJShroomShelfDiffuse;
            matRJShroomShelf.SetTexture("_BlueChannelTex", texRJShroomShelfDiffuse);
            matRJShroomShelf.SetTexture("_FlowHeightRamp", texRampRJMushroom);
            matRJShroomShelf.SetTexture("_FlowHeightmap", texRJShroomShelfEmissive);
            matRJShroomShelf.SetTexture("_GreenChannelTex", texRJLichenTerrain);
            matRJShroomShelf.SetFloat("_FlowEmissionStrength", 0.3f);

            matRJShroomSmall = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJShroomSmall.mat").WaitForCompletion());
            matRJShroomSmall.mainTexture = texRJShroomSmallDiffuse;
            matRJShroomSmall.SetTexture("_BlueChannelTex", texRJMoss);
            matRJShroomSmall.SetTexture("_FlowHeightRamp", texRampRJMushroom);
            matRJShroomSmall.SetTexture("_FlowHeightmap", texRJShroomSmallEmissive);
            matRJShroomSmall.SetTexture("_GreenChannelTex", texRJLichenTerrain);
            //matRJShroomSmall.color = Color.white;

            matRJTemple = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJTemple.mat").WaitForCompletion());
            //Appears to not be used 

            matRJTerrain = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJTerrain.mat").WaitForCompletion());
            //matRJTerrain.SetTexture("_BlueChannelTex", texRJDirtDiffuse);
            matRJTerrain.SetTexture("_GreenChannelTex", texRJGrassTerrain);
            //matRJTerrain.SetTexture("_RedChannelTopTex", texGPRockSide);
            //matRJTerrain.SetTexture("_RedChannelSideTex", texBlackbeachBasicRockSide);

            matRJTerrain2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJTerrain2.mat").WaitForCompletion());
            matRJTerrain2.SetTexture("_GreenChannelTex", texRJGrassTerrain); //They are the same?

            matRJTree = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion());
            matRJTree.SetTexture("_BlueChannelTex", texRJGrassTerrain2);
            matRJTree.SetTexture("_GreenChannelTex", texRJMoss);
            matRJTree.SetTexture("_RedChannelTopTex", texRJTreeTop);
            matRJTree.SetTexture("_RedChannelSideTex", texRJTree);

            matRJTreeLOD = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJTreeLOD.mat").WaitForCompletion());
            matRJTreeLOD.SetTexture("_BlueChannelTex", texRJGrassTerrain2);
            matRJTreeLOD.SetTexture("_GreenChannelTex", texRJMoss);
            matRJTreeLOD.SetTexture("_RedChannelTopTex", texRJTreeTop);
            matRJTreeLOD.SetTexture("_RedChannelSideTex", texRJTree);

            matRJTriangle = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/rootjungle/matRJTriangle.mat").WaitForCompletion());
            matRJTriangle.mainTexture = texRJTriangleDiffuse;

            //Material_2; //Tree Branches
            //Fronds_0; //

            Fronds_0 = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/Common/Props/Fronds_0.mat").WaitForCompletion());
            Fronds_0.color = new Color(1.7f, 1f, 0f);


            Fruit = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/habitat/Assets/BHFruitSmall.prefab").WaitForCompletion();
            FruitFall = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/habitatfall/Assets/BHFallMushroomDrips.prefab").WaitForCompletion();

            FruitFall.transform.GetChild(0).gameObject.SetActive(false);
            FruitFall.transform.GetChild(1).GetChild(1).localPosition = new Vector3(0, -5, 0);
            Transform temp = FruitFall.transform.GetChild(1).GetChild(3).GetChild(0);
            temp.GetComponent<HurtBoxGroup>().hurtBoxes[0] = temp.GetComponentInChildren<HurtBox>();
            FruitFall.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

        }

        private static void FixDumbFruit(On.EntityStates.Fauna.HabitatFruitDeathState.orig_OnEnter orig, EntityStates.Fauna.HabitatFruitDeathState self)
        {

            if (NetworkServer.active)
            {
                Transform Fruit = self.gameObject.transform.GetChild(1).GetChild(3);
                EffectManager.SimpleImpactEffect(JellyfishDeath, Fruit.position, Vector3.up, true);
                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/HealPack"), Fruit.position, UnityEngine.Random.rotation);
                gameObject.GetComponent<TeamFilter>().teamIndex = TeamIndex.Player;
                gameObject.GetComponentInChildren<HealthPickup>().fractionalHealing = HabitatFruitDeathState.fractionalHealing;
                gameObject.transform.localScale = new Vector3(HabitatFruitDeathState.scale, HabitatFruitDeathState.scale, HabitatFruitDeathState.scale);
                gameObject.GetComponent<Rigidbody>().AddForce(UnityEngine.Random.insideUnitSphere * HabitatFruitDeathState.healPackMaxVelocity, ForceMode.VelocityChange);
                NetworkServer.Spawn(gameObject);
            }
            orig(self);
        }

        public static void LoopWeather()
        {
            FruitFall.transform.GetChild(0).gameObject.SetActive(false);
            FruitFall.transform.GetChild(1).GetChild(1).localPosition = new Vector3(0, -4, 0);
            FruitFall.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            GameObject Weather = GameObject.Find("/HOLDER: Weather Set 1");

            Light TheSun = Weather.transform.GetChild(0).gameObject.GetComponent<Light>();
            TheSun.color = new Color(0.8863f, 0.7255f, 0.5647f, 1);
            TheSun.intensity = 1f;

            SetAmbientLight newAmbient = Weather.AddComponent<SetAmbientLight>();
            newAmbient.setAmbientLightColor = true;
            newAmbient.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            newAmbient.ambientSkyColor = new Color(0.5101f, 0.2622f, 0.3133f, 0.8f);
            newAmbient.ambientGroundColor = new Color(0.3562f, 0.2231f, 0.2512f, 0.8f);
            newAmbient.ambientEquatorColor = new Color(0.1272f, 0.0799f, 0.0799f, 0.8f);
            newAmbient.ambientIntensity = 1;

            newAmbient.setSkyboxMaterial = true;
            newAmbient.skyboxMaterial = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallSkybox.mat").WaitForCompletion();
            newAmbient.ApplyLighting();

            Weather.transform.GetChild(3).GetComponent<PostProcessVolume>().profile = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/DLC2/habitatfall/ppSceneBHFall.asset").WaitForCompletion();


            Weather.transform.GetChild(2).GetComponent<ReflectionProbe>().bakedTexture = Addressables.LoadAssetAsync<Cubemap>(key: "RoR2/DLC2/habitatfall/ReflectionProbe-0.exr").WaitForCompletion();


            #region Fruity
            //forEach Vine, spawn big Fruit

            //RoR2/DLC2/habitat/Assets/BHFruitSmall.prefab
            //RoR2/DLC2/habitatfall/Assets/BHFallMushroomDrips.prefab

            #endregion





            #region Lights

            //forEach Mushroom, change light color
            Color MushLight  = new Color(1f,0.45f,0.5f,1f); //(0.118, 1.000, 0.756, 1.000)
            Color MushLightLow = new Color(0.886f, 0.38f, 0.4f, 1f);//RGBA(0.114, 0.886, 0.648, 1.000)
            

            //(0.429, 0.874, 1.000, 1.000) HOLLOW TREE
            //(0.886, 0.726, 0.565, 1.000) //This is the SUN
            //
            Light[] LightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in LightList)
            {
                if (light.color.r < 0.114f)
                {
                    light.color = MushLightLow;
                }
                else if (light.color.r < 0.44f)
                {
                    light.color = MushLight;
                }
            }
            #endregion



            #region Particles
            //forEach Mushroom, particle change colors ig

            GameObject Particles = GameObject.Find("/HOLDER: FX");

            GradientColorKey[] newSpore = new GradientColorKey[]
            {
                new GradientColorKey{color = new Color(1, 0.557f,0.4f,1f), time = 0},
                new GradientColorKey{color = new Color(1, 0.42f, 0.22f, 1f), time = 1},
            };

            ParticleSystem[] particleList = Particles.transform.GetChild(0).GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particle in particleList)
            {
                var A = particle.colorOverLifetime;
                var B = A.color;
                B.m_GradientMax.colorKeys = newSpore; //0.4 0.5569 1 1
                B.gradientMax.colorKeys = newSpore; //0.4 0.5569 1 1
                B.gradient.colorKeys = newSpore; //0.4 0.5569 1 1
                A.color = B;
                //Debug.Log(particle.colorOverLifetime.color.gradient.colorKeys[0].color);
                //Debug.Log(particle.colorOverLifetime.color.gradient.colorKeys[1].color);
            }
            ParticleSystemRenderer[] particleList2 = Particles.transform.GetChild(0).GetComponentsInChildren<ParticleSystemRenderer>();
            foreach (ParticleSystemRenderer particle in particleList2)
            {
                particle.sharedMaterial = matRootJungleSpore;
            }
            Particles.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<ParticleSystemRenderer>().sharedMaterial = matRJLeaf_Green;
            Particles.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<ParticleSystemRenderer>().sharedMaterial = matRJLeaf_Blue;

            #endregion

            #region Materials

            //Tree Folliage has 2 material renderers ig I need to keep stuff like that In mind

            //Bounce Shrooms use SkinnedMeshRenderer that is just not the same apparently
            SkinnedMeshRenderer[] skinMeshList = Object.FindObjectsOfType(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer[];
            foreach (SkinnedMeshRenderer renderer in skinMeshList)
            {
                if (renderer.sharedMaterial)
                {
                    switch (renderer.sharedMaterial.name)
                    {
                        case "matRJShroomBounce":
                            renderer.sharedMaterial = matRJShroomBounce;
                            break;
                    }
                }
            }

            MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer renderer in meshList)
            {
                //var meshBase = renderer.gameObject;
                //var meshParent = meshBase.transform.parent;
                if (renderer.sharedMaterial)
                {
                    if (renderer.sharedMaterials.Length > 1)
                    {
                        //Debug.Log(renderer.sharedMaterials[0]);
                        //Debug.Log(renderer.sharedMaterials[1]);

                        //RJTowerTreeFoliage_LOD1
                        //RJDistantTreeFoliage
                        if (renderer.materials[1].name.StartsWith("RJDistant"))
                        {
                            renderer.materials = new Material[]{
                                renderer.material,
                                RJDistantTreeFoliage
                            };
                        }
                        else
                        {
                            renderer.materials = new Material[]{
                                renderer.material,
                                RJTowerTreeFoliage
                            };
                        }

                    }
                    else
                    {

                        switch (renderer.sharedMaterial.name)
                        {
                            case "Fronds_0":
                                renderer.sharedMaterial = Fronds_0;
                                break;
                            case "RJTowerTreeFoliage_LOD2":
                                renderer.sharedMaterial = RJTowerTreeFoliage;
                                renderer.gameObject.transform.parent.localScale *= 1.4f;
                                break;
                            case "RJDistantTreeFoliage_LOD2":
                                renderer.sharedMaterial = RJDistantTreeFoliage;
                                renderer.gameObject.transform.parent.localScale *= 1.5f;
                                break;
                            case "RJfern_LOD0":
                                renderer.sharedMaterial = RJfern_LOD0;
                                break;
                            case "spmRJgrass1_LOD0":
                                renderer.sharedMaterial = spmRJgrass1_LOD0;
                                renderer.gameObject.transform.parent.localScale *= 2f;
                                break;
                            case "spmRJgrass2_LOD0":
                                renderer.sharedMaterial = spmRJgrass2_LOD0;
                                renderer.gameObject.transform.parent.localScale *= 2f;
                                break;
                            case "RJHangingMoss_LOD0":
                                renderer.sharedMaterial = RJHangingMoss_LOD0;
                                if (NetworkServer.active)
                                {
                                    if (UnityEngine.Random.Range(0f, 5f) > 4)
                                    {
                                        if (renderer.gameObject.activeInHierarchy)
                                        {
                                            NetworkServer.Spawn(GameObject.Instantiate(FruitFall, renderer.gameObject.transform.parent));
                                        }
                                    }                            
                                }
                                break;
                            case "RJMossFoliage_LOD0":
                                renderer.sharedMaterial = RJMossFoliage_LOD0;
                                break;
                            case "RJpinkshroom_LOD0":
                                renderer.sharedMaterial = RJpinkshroom_LOD0;
                                break;
                            case "RJShroomFoliage_LOD0":
                                renderer.sharedMaterial = RJShroomFoliage_LOD0;
                                break;
                            case "RJTreeBigFoliage":
                            case "RJTreeBigFoliage_LOD0":
                            case "RJTreeBigFoliage_LOD0 (Instance)":
                                renderer.sharedMaterial = RJTreeBigFoliage;
                                break;
                            case "matRootJungleSpore":
                                renderer.sharedMaterial = matRootJungleSpore;
                                break;
                            case "matRJFogFloor":
                                renderer.sharedMaterial = matRJFogFloor;
                                break;
                            case "matRJMossPatch1":
                                renderer.sharedMaterial = matRJMossPatch1;
                                break;
                            case "matRJMossPatch2":
                                renderer.sharedMaterial = matRJMossPatch2;
                                break;
                            case "matRJMossPatchLarge":
                                renderer.sharedMaterial = matRJMossPatchLarge;
                                break;
                            case "matRJMossPatchTriplanar":
                                renderer.sharedMaterial = matRJMossPatchTriplanar;
                                break;
                            case "matBBPebble":
                                renderer.sharedMaterial = matRJPebble;
                                break;
                            case "matSGRock":
                                renderer.sharedMaterial = matRJRock;
                                break;
                            case "matRJSandstone":
                                renderer.sharedMaterial = matRJSandstone;
                                break;
                            case "matRJShroomBig":
                                renderer.sharedMaterial = matRJShroomBig;
                                break;
                            case "matRJShroomShelf":
                                renderer.sharedMaterial = matRJShroomShelf;
                                break;
                            case "matRJShroomSmall":
                                renderer.sharedMaterial = matRJShroomSmall;
                                break;
                            case "matRJTemple":
                                renderer.sharedMaterial = matRJTemple;
                                break;
                            case "matRJTerrain":
                                renderer.sharedMaterial = matRJTerrain;
                                break;
                            case "matRJTerrain2":
                                renderer.sharedMaterial = matRJTerrain2;
                                break;
                            case "matRJTree":
                                renderer.sharedMaterial = matRJTree;
                                break;
                            case "matRJTreeLOD":
                                renderer.sharedMaterial = matRJTreeLOD;
                                break;
                        }
                    }
                }
            }
            #endregion
        }

        public static void Fall()
        {
            Material matBHFallGrassGlow = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallGrassGlow.mat").WaitForCompletion());
            Material matBHFallDistantTree = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDistantTree.mat").WaitForCompletion());
            Material matBHFallShurb = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShurb.mat").WaitForCompletion());
            Material matBHFallClouds = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallClouds.mat").WaitForCompletion());
            Material matBHFallDistantMountainClouds = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDistantMountainClouds.mat").WaitForCompletion());
            Material matBHFallDomeTrim = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDomeTrim.mat").WaitForCompletion());
            Material matBHFallEnvfxLeaves = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallEnvfxLeaves.mat").WaitForCompletion());
            Material matBHFallFlower = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallFlower.mat").WaitForCompletion());
            Material matBHFallGodray = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallGodray.mat").WaitForCompletion());
            Material matBHFallHiveBase = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallHiveBase.mat").WaitForCompletion());
            Material matBHFallHiveBubble = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallHiveBubble.mat").WaitForCompletion());
            Material matBHFallHiveDecal = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallHiveDecal.mat").WaitForCompletion());
            Material matBHFallPebble = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallPebble.mat").WaitForCompletion());
            Material matBHFallPlatformSimple = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallPlatformSimple.mat").WaitForCompletion());
            Material matBHFallPlatformTerrain = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallPlatformTerrain.mat").WaitForCompletion());
            Material matBHFallShroomDrips = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShroomDrips.mat").WaitForCompletion());
            Material matBHFallShroomPath = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShroomPath.mat").WaitForCompletion());
            Material matBHFallShroomTunnel = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallShroomTunnel.mat").WaitForCompletion());
            Material matBHFallSilhouette = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallSilhouette.mat").WaitForCompletion());
            Material matBHFallStatueBeam = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallStatueBeam.mat").WaitForCompletion());
            Material matBHFallStatueCrystals = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallStatueCrystals.mat").WaitForCompletion());
            Material matBHFallTempleTrim = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallTempleTrim.mat").WaitForCompletion());
            Material matBHFallTerrainVines = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallTerrainVines.mat").WaitForCompletion());


        }

    }
}