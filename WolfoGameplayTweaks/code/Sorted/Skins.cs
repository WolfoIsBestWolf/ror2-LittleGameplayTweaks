using BepInEx;
using BepInEx.Configuration;
//using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Navigation;
//using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LittleGameplayTweaks
{
	public class SkinStuff
    {
        public static void WolfoSkins()
        {
            /*LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleBody"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleDefault.asset").WaitForCompletion());
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleBody"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleSulfur.asset").WaitForCompletion());
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleGuardBody"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleGuardDefault.asset").WaitForCompletion());
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleGuardBody"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleGuardSulfur.asset").WaitForCompletion());
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleQueen2Body"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleQueen2Default.asset").WaitForCompletion());
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleQueen2Body"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleQueen2Sulfur.asset").WaitForCompletion());
            */

            Material matCommandoDualiesMarine = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matCommandoDualiesMarine.mat").WaitForCompletion();
            // matCommandoDualiesMarine.shaderKeywords[1] = "a";
            matCommandoDualiesMarine.DisableKeyword("LIMBREMOVAL");

            //This removes Limb removal, not good but better than bugged ones I guess
            //I have no idea how to actually fix his Right Calf being both Calfs

            //Blue Tree Bot
            Texture2D texTreebotBlueFlowerDiffuse = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texTreebotBlueFlowerDiffuse.LoadImage(Properties.Resources.texTreebotBlueFlowerDiffuse, true);
            texTreebotBlueFlowerDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueFlowerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueLeafDiffuse = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texTreebotBlueLeafDiffuse.LoadImage(Properties.Resources.texTreebotBlueLeafDiffuse, true);
            texTreebotBlueLeafDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueTreeBarkDiffuse = new Texture2D(256, 1024, TextureFormat.DXT5, false);
            texTreebotBlueTreeBarkDiffuse.LoadImage(Properties.Resources.texTreebotBlueTreeBarkDiffuse, true);
            texTreebotBlueTreeBarkDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texTreebotBlueSkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texTreebotBlueSkinIcon.LoadImage(Properties.Resources.texTreebotBlueSkinIcon, true);
            texTreebotBlueSkinIcon.filterMode = FilterMode.Bilinear;
            Sprite texTreebotBlueSkinIconS = Sprite.Create(texTreebotBlueSkinIcon, WRect.rec128, WRect.half);

            SkinDef GreenFlowerRex = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];


            CharacterModel.RendererInfo[] REXBlueRenderInfos = new CharacterModel.RendererInfo[4];
            System.Array.Copy(GreenFlowerRex.rendererInfos, REXBlueRenderInfos, 4);

            Material matREXBlueRobot = Object.Instantiate(GreenFlowerRex.rendererInfos[0].defaultMaterial);
            Material matREXBlueFlower = Object.Instantiate(GreenFlowerRex.rendererInfos[1].defaultMaterial);
            Material matREXBlueLeaf = Object.Instantiate(GreenFlowerRex.rendererInfos[2].defaultMaterial);
            Material matREXBlueBark = Object.Instantiate(GreenFlowerRex.rendererInfos[3].defaultMaterial);

            matREXBlueRobot.color = new Color(0.65f, 0.65f, 0.65f, 1);
            matREXBlueRobot.SetColor("_EmColor", new Color(0.7f, 0.7f, 1f, 1));

            matREXBlueFlower.mainTexture = texTreebotBlueFlowerDiffuse;
            matREXBlueLeaf.mainTexture = texTreebotBlueLeafDiffuse;
            //matREXBlueBark.mainTexture = texTreebotBlueTreeBarkDiffuse;
            matREXBlueBark.color = new Color32(190, 175, 200, 255);

            REXBlueRenderInfos[0].defaultMaterial = matREXBlueRobot;
            REXBlueRenderInfos[1].defaultMaterial = matREXBlueFlower;
            REXBlueRenderInfos[2].defaultMaterial = matREXBlueLeaf;
            REXBlueRenderInfos[3].defaultMaterial = matREXBlueBark;

            R2API.SkinDefInfo BlueFlowerRexInfo = new R2API.SkinDefInfo
            {
                BaseSkins = GreenFlowerRex.baseSkins,
                NameToken = "Lilly",
                UnlockableDef = null,
                RootObject = GreenFlowerRex.rootObject,
                RendererInfos = REXBlueRenderInfos,
                Name = "skinTreebotWolfo",
                Icon = texTreebotBlueSkinIconS,
            };
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody"), BlueFlowerRexInfo);
            //
            //
            //Pink stuff test
            SkinDef CaptainSkinWhite = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            Texture2D texCaptainPinkSkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texCaptainPinkSkinIcon.LoadImage(Properties.Resources.texCaptainPinkSkinIcon, true);
            texCaptainPinkSkinIcon.filterMode = FilterMode.Bilinear;
            Sprite texCaptainPinkSkinIconS = Sprite.Create(texCaptainPinkSkinIcon, WRect.rec128, WRect.half);

            Texture2D PinktexCaptainJacketDiffuseW = new Texture2D(512, 512, TextureFormat.DXT1, false);
            PinktexCaptainJacketDiffuseW.LoadImage(Properties.Resources.PinktexCaptainJacketDiffuseW, true);
            PinktexCaptainJacketDiffuseW.filterMode = FilterMode.Bilinear;
            PinktexCaptainJacketDiffuseW.wrapMode = TextureWrapMode.Clamp;

            Texture2D PinktexCaptainPaletteW = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW.LoadImage(Properties.Resources.PinktexCaptainPaletteW, true);
            PinktexCaptainPaletteW.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW.wrapMode = TextureWrapMode.Clamp;

            Texture2D PinktexCaptainPaletteW2 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW2.LoadImage(Properties.Resources.PinktexCaptainPaletteW2, true);
            PinktexCaptainPaletteW2.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW2.wrapMode = TextureWrapMode.Clamp;

            //Pallete for HAT
            Texture2D PinktexCaptainPaletteW3 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW3.LoadImage(Properties.Resources.PinktexCaptainPaletteW3, true);
            PinktexCaptainPaletteW3.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW3.wrapMode = TextureWrapMode.Clamp;

            CharacterModel.RendererInfo[] CaptainPinkRenderInfos = new CharacterModel.RendererInfo[7];
            System.Array.Copy(CaptainSkinWhite.rendererInfos, CaptainPinkRenderInfos, 7);

            Material PinkmatCaptainAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainAlt2 = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainAlt3 = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainArmorAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[2].defaultMaterial);
            Material PinkmatCaptainJacketAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[3].defaultMaterial);
            Material PinkmatCaptainRobotBitsAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[4].defaultMaterial);

            PinkmatCaptainAlt.mainTexture = PinktexCaptainPaletteW;
            PinkmatCaptainAlt2.mainTexture = PinktexCaptainPaletteW2;
            PinkmatCaptainAlt3.mainTexture = PinktexCaptainPaletteW3;
            //PinkmatCaptainArmorAlt.color = new Color32(255, 223, 188, 255);
            PinkmatCaptainArmorAlt.color = new Color32(255, 190, 135, 255);//(255, 195, 150, 255);
            //_EmColor is juts fucking weird
            PinkmatCaptainJacketAlt.mainTexture = PinktexCaptainJacketDiffuseW;
            PinkmatCaptainRobotBitsAlt.SetColor("_EmColor", new Color(1.2f, 0.6f, 1.2f, 1f));
            PinkmatCaptainRobotBitsAlt.color = new Color32(255, 190, 135, 255);

            CaptainPinkRenderInfos[0].defaultMaterial = PinkmatCaptainAlt; //matCaptainAlt
            CaptainPinkRenderInfos[1].defaultMaterial = PinkmatCaptainAlt3; //matCaptainAlt
            CaptainPinkRenderInfos[2].defaultMaterial = PinkmatCaptainArmorAlt; //matCaptainArmorAlt
            CaptainPinkRenderInfos[3].defaultMaterial = PinkmatCaptainJacketAlt; //matCaptainJacketAlt
            CaptainPinkRenderInfos[4].defaultMaterial = PinkmatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainPinkRenderInfos[5].defaultMaterial = PinkmatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainPinkRenderInfos[6].defaultMaterial = PinkmatCaptainAlt2; //matCaptainAlt //Skirt

            R2API.SkinDefInfo CaptainPinkSkinInfos = new R2API.SkinDefInfo
            {
                BaseSkins = CaptainSkinWhite.baseSkins,
                NameToken = "Honeymoon",
                UnlockableDef = null,
                RootObject = CaptainSkinWhite.rootObject,
                RendererInfos = CaptainPinkRenderInfos,
                Name = "skinCaptainWolfo",
                Icon = texCaptainPinkSkinIconS,
            };
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), CaptainPinkSkinInfos);
            //
            //RoRR Red Bandit
            SkinDef BanditDefaultSkin = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef BanditAltSkin = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            CharacterModel.RendererInfo[] BanditRedRenderInfos = new CharacterModel.RendererInfo[8];
            System.Array.Copy(BanditDefaultSkin.rendererInfos, BanditRedRenderInfos, 8);

            Material matBanditRed1 = Object.Instantiate(BanditDefaultSkin.rendererInfos[0].defaultMaterial);
            //Material matBanditRed2      = Object.Instantiate(BanditDefaultSkin.rendererInfos[1].defaultMaterial);
            //Material matBanditRed3      = Object.Instantiate(BanditDefaultSkin.rendererInfos[2].defaultMaterial);
            Material matBandit2Coat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2CoatHat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2Shotgun = Object.Instantiate(BanditDefaultSkin.rendererInfos[4].defaultMaterial);
            Material matBandit2Knife = Object.Instantiate(BanditDefaultSkin.rendererInfos[5].defaultMaterial);
            //Material matBandit2Coat2    = Object.Instantiate(BanditDefaultSkin.rendererInfos[6].defaultMaterial);
            Material matBandit2Revolver = Object.Instantiate(BanditDefaultSkin.rendererInfos[7].defaultMaterial);

            Texture2D texBanditRedSkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texBanditRedSkinIcon.LoadImage(Properties.Resources.texBanditRedSkinIcon, true);
            texBanditRedSkinIcon.filterMode = FilterMode.Bilinear;
            Sprite texBanditRedSkinIconS = Sprite.Create(texBanditRedSkinIcon, WRect.rec128, WRect.half);

            Texture2D texBanditRedDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texBanditRedDiffuse.LoadImage(Properties.Resources.texBanditRedDiffuse, true);
            texBanditRedDiffuse.filterMode = FilterMode.Bilinear;
            texBanditRedDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedCoatDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texBanditRedCoatDiffuse.LoadImage(Properties.Resources.texBanditRedCoatDiffuse, true);
            texBanditRedCoatDiffuse.filterMode = FilterMode.Bilinear;
            texBanditRedCoatDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedEmission = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texBanditRedEmission.LoadImage(Properties.Resources.texBanditRedEmission, true);
            texBanditRedEmission.filterMode = FilterMode.Bilinear;
            texBanditRedEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditShotgunDiffuse = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texBanditShotgunDiffuse.LoadImage(Properties.Resources.texBanditShotgunDiffuse, true);
            texBanditShotgunDiffuse.filterMode = FilterMode.Bilinear;
            texBanditShotgunDiffuse.wrapMode = TextureWrapMode.Clamp;

            //
            matBanditRed1.mainTexture = texBanditRedDiffuse;
            matBandit2Coat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2CoatHat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2Shotgun.mainTexture = texBanditShotgunDiffuse;
            matBandit2Knife.mainTexture = texBanditShotgunDiffuse;
            matBandit2Revolver.mainTexture = texBanditShotgunDiffuse;

            matBanditRed1.SetTexture("_EmTex", texBanditRedEmission);
            matBanditRed1.SetColor("_EmColor", new Color(1f, 0.8f, 1f)); //0 0.3491 0.327 1
            //matBandit2Shotgun.SetColor("_EmColor", new Color(0.4f, 0.12f, 0.19f)); //100 30 50
            matBandit2Shotgun.SetColor("_EmColor", new Color(0.5f, 0.15f, 0.25f)); //100 30 50
            //matBandit2Coat.color = new Color(0.85f, 0.85f, 0.82f);
            matBandit2Coat.color = new Color(0.95f, 0.95f, 0.87f);

            BanditRedRenderInfos[0].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2AccessoriesMesh //texBandit2Diffuse
            BanditRedRenderInfos[1].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2ArmsMesh
            BanditRedRenderInfos[2].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2BodyMesh
            BanditRedRenderInfos[3].defaultMaterial = matBandit2Coat;     //matBandit2Coat     //Bandit2CoatMesh        //texBandit2CoatDiffuse
            BanditRedRenderInfos[4].defaultMaterial = matBandit2Shotgun;     //matBandit2Shotgun  //BanditShotgunMesh      //texBanditShotgunDiffuse
            BanditRedRenderInfos[5].defaultMaterial = matBandit2Knife;     //matBandit2Knife    //BladeMesh              //texBanditShotgunDiffuse
            BanditRedRenderInfos[6].defaultMaterial = matBandit2CoatHat;     //matBandit2Coat     //Bandit2HatMesh
            BanditRedRenderInfos[7].defaultMaterial = matBandit2Revolver;     //matBandit2Revolver //BanditPistolMesh       //texBanditShotgunDiffuse

            //
            RoR2.SkinDef.MeshReplacement[] BanditRedMesh = new SkinDef.MeshReplacement[5];
            BanditDefaultSkin.meshReplacements.CopyTo(BanditRedMesh, 0);

            BanditRedMesh[3] = BanditAltSkin.meshReplacements[2];


            R2API.SkinDefInfo BanditRedSkinInfos = new R2API.SkinDefInfo
            {
                Name = "skinBandit2Wolfo",
                NameToken = "Autumn",
                Icon = texBanditRedSkinIconS,
                BaseSkins = BanditAltSkin.baseSkins,
                MeshReplacements = BanditRedMesh,
                ProjectileGhostReplacements = BanditDefaultSkin.projectileGhostReplacements,
                RendererInfos = BanditRedRenderInfos,
                RootObject = BanditDefaultSkin.rootObject,
                UnlockableDef = null,
            };
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body"), BanditRedSkinInfos);
        }
    }
}