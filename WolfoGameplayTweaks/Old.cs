/*
         
            DirectorCardCategorySelection dccsParentFamily = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsParentFamily.AddCategory("Champions", 2);
            dccsParentFamily.AddCategory("Minibosses", 6);
            dccsParentFamily.AddCategory("Basic Monsters", 0);
            dccsParentFamily.AddCard(0, DSGrandparent);
            dccsParentFamily.AddCard(1, DSParent);
            dccsParentFamily.name = "dccsFamilyParent";

            DirectorCardCategorySelection dccsLunarChimeraFamily = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsLunarChimeraFamily.AddCategory("Champions", 0);
            dccsLunarChimeraFamily.AddCategory("Minibosses", 0);
            dccsLunarChimeraFamily.AddCategory("Basic Monsters", 4);
            dccsLunarChimeraFamily.AddCard(2, DSLunarExploder);
            dccsLunarChimeraFamily.AddCard(2, DSLunarGolem);
            dccsLunarChimeraFamily.AddCard(2, DSLunarWisp);
            dccsLunarChimeraFamily.name = "dccsFamilyLunarChimera";


            DirectorCardCategorySelection dccsVoidFamily = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsVoidFamily.AddCategory("Champions", 6);
            dccsVoidFamily.AddCategory("Minibosses", 6);
            dccsVoidFamily.AddCategory("Basic Monsters", 6);
            dccsVoidFamily.AddCard(0, DSVoidReaver);
            dccsVoidFamily.AddCard(1, DSVoidReaver);
            dccsVoidFamily.AddCard(2, DSVoidReaver);
            dccsVoidFamily.name = "dccsFamilyVoid";

            DirectorCardCategorySelection dccsGolemDepthsFamily = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsGolemDepthsFamily.AddCategory("Champions", 2);
            dccsGolemDepthsFamily.AddCategory("Minibosses", 6);
            dccsGolemDepthsFamily.AddCategory("Basic Monsters", 0);
            dccsGolemDepthsFamily.AddCard(0, DSTitanDampCaves);
            dccsGolemDepthsFamily.AddCard(1, DSGolem);
            dccsGolemDepthsFamily.name = "dccsFamilyGolemDampCave";

            DirectorCardCategorySelection dccsJellyfishFamily = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsJellyfishFamily.AddCategory("Champions", 2);
            dccsJellyfishFamily.AddCategory("Minibosses", 0);
            dccsJellyfishFamily.AddCategory("Basic Monsters", 6);
            dccsJellyfishFamily.AddCard(0, DSVagrant);
            dccsJellyfishFamily.AddCard(2, DSJellyfish);
            dccsJellyfishFamily.name = "dccsFamilyJellyfish";

            DirectorCardCategorySelection dccsBeetleFamily = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsBeetleFamily.AddCategory("Champions", 2);
            dccsBeetleFamily.AddCategory("Minibosses", 2);
            dccsBeetleFamily.AddCategory("Basic Monsters", 4);
            dccsBeetleFamily.AddCard(0, DSBeetleQueen);
            dccsBeetleFamily.AddCard(1, DSBeetleGuard);
            dccsBeetleFamily.AddCard(2, DSBeetle);
            dccsBeetleFamily.name = "dccsFamilyBeetle";

            DirectorCardCategorySelection dccsWispFamily = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsWispFamily.AddCategory("Champions", 2);
            dccsWispFamily.AddCategory("Minibosses", 2);
            dccsWispFamily.AddCategory("Basic Monsters", 4);
            dccsWispFamily.AddCard(0, DSGrovetender);
            dccsWispFamily.AddCard(1, DSGreaterWisp);
            dccsWispFamily.AddCard(2, DSWisp);
            dccsWispFamily.name = "dccsFamilyWisp";

            DirectorCardCategorySelection dccsLemurianFamily = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsLemurianFamily.AddCategory("Champions", 0);
            dccsLemurianFamily.AddCategory("Minibosses", 2);
            dccsLemurianFamily.AddCategory("Basic Monsters", 4);
            dccsLemurianFamily.AddCard(1, DSElderLemurian);
            dccsLemurianFamily.AddCard(2, DSLemurian);
            dccsLemurianFamily.name = "dccsFamilyLemurian";

            DirectorCardCategorySelection dccsImpFamily = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsImpFamily.AddCategory("Champions", 2);
            dccsImpFamily.AddCategory("Minibosses", 0);
            dccsImpFamily.AddCategory("Basic Monsters", 3);
            dccsImpFamily.AddCard(0, DSImpBoss);
            dccsImpFamily.AddCard(2, DSImp);
            dccsImpFamily.name = "dccsFamilyImp";
            


ParentFamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsParentFamily,
    familySelectionChatString = "<style=cWorldEvent>[WARNING] You feel the warmth of the sun..</style>",
    selectionWeight = 1,
    minimumStageCompletion = 1,
    maximumStageCompletion = 99
};

dccsParentFamily.



Parent2FamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsParentFamily,
    familySelectionChatString = "<style=cWorldEvent>[WARNING] You feel the warmth of the sun..</style>",
    selectionWeight = 1,
    minimumStageCompletion = 12,
    maximumStageCompletion = 99
};

Lunar2FamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsLunarChimeraFamily,
    familySelectionChatString = "FAMILY_LUNAR",
    selectionWeight = 1,
    minimumStageCompletion = 12,
    maximumStageCompletion = 99
};
Lunar0FamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsLunarChimeraFamily,
    familySelectionChatString = "FAMILY_LUNAR",
    selectionWeight = 1,
    minimumStageCompletion = 1,
    maximumStageCompletion = 99
};


ClayFamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsClayFamily,
    familySelectionChatString = "<style=cWorldEvent>[WARNING] You feel parasitic influences..</style>",
    selectionWeight = 1,
    minimumStageCompletion = 5,
    maximumStageCompletion = 99
};

RoboBallFamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsRoboballFamily,
    familySelectionChatString = "<style=cWorldEvent>[WARNING] You hear a whirring of wings and machinery..</style>",
    selectionWeight = 1,
    minimumStageCompletion = 1,
    maximumStageCompletion = 99
};



VanillaGolemDampCaveFamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsGolemDepthsFamily,
    familySelectionChatString = "FAMILY_GOLEM",
    selectionWeight = 1,
    minimumStageCompletion = 1,
    maximumStageCompletion = 99
};

VanillaJellyFishFamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsJellyfishFamily,
    familySelectionChatString = "FAMILY_JELLYFISH",
    selectionWeight = 1,
    minimumStageCompletion = 1,
    maximumStageCompletion = 99
};

VanillaBeetleFamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsBeetleFamily,
    familySelectionChatString = "FAMILY_BEETLE",
    selectionWeight = 1,
    minimumStageCompletion = 1,
    maximumStageCompletion = 99
};

VanillaWispFamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsWispFamily,
    familySelectionChatString = "FAMILY_WISP",
    selectionWeight = 1,
    minimumStageCompletion = 1,
    maximumStageCompletion = 5
};

VanillaLemurianFamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsLemurianFamily,
    familySelectionChatString = "FAMILY_LEMURIAN",
    selectionWeight = 1,
    minimumStageCompletion = 1,
    maximumStageCompletion = 99
};

VanillaImpFamilyEvent = new ClassicStageInfo.MonsterFamily
{
    monsterFamilyCategories = dccsImpFamily,
    familySelectionChatString = "FAMILY_IMP",
    selectionWeight = 1,
    minimumStageCompletion = 5,
    maximumStageCompletion = 99
};


*/



/*
switch (SceneInfo.instance.sceneDef.baseSceneName)
{
    case "wispgraveyard":
        self.possibleMonsterFamilies = self.possibleMonsterFamilies.Add(VanillaBeetleFamilyEvent, VanillaWispFamilyEvent);
        break;
    case "dampcavesimple":
        self.possibleMonsterFamilies[0].maximumStageCompletion = 99;
        self.possibleMonsterFamilies[1].maximumStageCompletion = 99;
        break;
    case "golemplains":
        self.possibleMonsterFamilies[0].monsterFamilyCategories.categories[0].cards[0].spawnCard = TitanPlains;
        break;
    case "goolake":
        self.possibleMonsterFamilies[1].monsterFamilyCategories.categories[0].cards[0].spawnCard = TitanGoolake;
        break;
}
if (FamilyEventAdditions.Value == true)
{
    switch (SceneInfo.instance.sceneDef.baseSceneName)
    {
        case "blackbeach":
            self.possibleMonsterFamilies[0] = VanillaLemurianFamilyEvent;
            break;
        case "goolake":
            self.possibleMonsterFamilies[0] = ClayFamilyEvent;
            self.possibleMonsterFamilies[0].selectionWeight = 4f;
            break;
        case "frozenwall":
            self.possibleMonsterFamilies[1] = RoboBallFamilyEvent;
            self.possibleMonsterFamilies[0].minimumStageCompletion = 0;
            break;
        case "wispgraveyard":
            self.possibleMonsterFamilies = self.possibleMonsterFamilies.Add(ClayFamilyEvent);
            break;
        case "dampcavesimple":
            self.possibleMonsterFamilies = self.possibleMonsterFamilies.Add(VanillaGolemDampCaveFamilyEvent);
            break;
        case "rootjungle":
            self.possibleMonsterFamilies[2] = VanillaJellyFishFamilyEvent;
            self.possibleMonsterFamilies = self.possibleMonsterFamilies.Add(ClayFamilyEvent);
            self.possibleMonsterFamilies[1].selectionWeight = 4;
            self.possibleMonsterFamilies[2].selectionWeight = 4;
            self.possibleMonsterFamilies[3].selectionWeight = 4;
            break;
        case "shipgraveyard":
            self.possibleMonsterFamilies[3] = RoboBallFamilyEvent;
            self.possibleMonsterFamilies[3].selectionWeight = 1;
            break;
        case "skymeadow":
            //self.possibleMonsterFamilies = self.possibleMonsterFamilies.Add(ParentFamilyEvent, Parent2FamilyEvent, Lunar2FamilyEvent);
            self.possibleMonsterFamilies[0].selectionWeight = 1;
            self.possibleMonsterFamilies[0].minimumStageCompletion = 1;
            self.possibleMonsterFamilies[0].maximumStageCompletion = 99;
            self.possibleMonsterFamilies = self.possibleMonsterFamilies.Add(ParentFamilyEvent);
            break;
        case "arena":
            //self.possibleMonsterFamilies = self.possibleMonsterFamilies.Add(VoidFamilyEvent);
            break;
        case "artifactworld":
            //self.possibleMonsterFamilies = self.possibleMonsterFamilies.Add(Lunar0FamilyEvent);
            break;
    }

    for (int i = 0; i < self.possibleMonsterFamilies.Length; i++)
    {
        if (self.possibleMonsterFamilies[i].minimumStageCompletion == 1)
        {
            self.possibleMonsterFamilies[i].minimumStageCompletion = 0;
        }
        if (self.possibleMonsterFamilies[i].maximumStageCompletion == 5)
        {
            self.possibleMonsterFamilies[i].maximumStageCompletion = 99;
        }
    }
}
*/



/*
DirectorCard ADShrineCleanse = new DirectorCard
{
    spawnCard = RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscShrineCleanse"),
    selectionWeight = 10,
    minimumStageCompletions = 0
};
DirectorCard ADDuplicatorLarge = new DirectorCard
{
    spawnCard = RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscDuplicatorLarge"),
    selectionWeight = 9,
    minimumStageCompletions = 1
};
DirectorCard ADDuplicatorMilitary = new DirectorCard
{
    spawnCard = RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscDuplicatorMilitary"),
    selectionWeight = 3,
    minimumStageCompletions = 4
};
DirectorCard ADDuplicatorWild = new DirectorCard
{
    spawnCard = RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscDuplicatorWild"),
    selectionWeight = 3,
    minimumStageCompletions = 1
};

DirectorAPI.Helpers.RemoveExistingInteractableFromStage(DirectorAPI.Helpers.InteractableNames.CleansingPool, DirectorAPI.Stage.WetlandAspect);
DirectorAPI.Helpers.AddNewInteractableToStage(ADShrineCleanse, DirectorAPI.InteractableCategory.Shrines, DirectorAPI.Stage.WetlandAspect);

DirectorAPI.Helpers.RemoveExistingInteractableFromStage(DirectorAPI.Helpers.InteractableNames.CleansingPool, DirectorAPI.Stage.ScorchedAcres);
DirectorAPI.Helpers.AddNewInteractableToStage(ADShrineCleanse, DirectorAPI.InteractableCategory.Shrines, DirectorAPI.Stage.ScorchedAcres);

DirectorAPI.Helpers.RemoveExistingInteractableFromStage(DirectorAPI.Helpers.InteractableNames.CleansingPool, DirectorAPI.Stage.SirensCall);
DirectorAPI.Helpers.AddNewInteractableToStage(ADShrineCleanse, DirectorAPI.InteractableCategory.Shrines, DirectorAPI.Stage.SirensCall);

DirectorAPI.Helpers.RemoveExistingInteractable(DirectorAPI.Helpers.InteractableNames.PrinterUncommon);
DirectorAPI.Helpers.RemoveExistingInteractable(DirectorAPI.Helpers.InteractableNames.PrinterLegendary);
DirectorAPI.Helpers.AddNewInteractable(ADDuplicatorLarge, DirectorAPI.InteractableCategory.Duplicator);
DirectorAPI.Helpers.AddNewInteractable(ADDuplicatorMilitary, DirectorAPI.InteractableCategory.Duplicator);
*/

/*
if (SceneInfo.instance && SceneInfo.instance.sceneDef)
{
    //Debug.LogWarning(SceneInfo.instance.sceneDef);
    //Debug.LogWarning(self.possibleMonsterFamilies);
    //Debug.LogWarning(self.possibleMonsterFamilies.Length);
    //Debug.LogWarning(self.interactableCategories);


    if (InteractableChanges.Value == true)
    {
        
        for (int i = 0; i < self.interactableCategories.categories.Length; i++)
        {
            for (int j = 0; j < self.interactableCategories.categories[i].cards.Length; j++)
            {
                if (i == 0)
                {
                    switch (self.interactableCategories.categories[i].cards[j].spawnCard.name)
                    {
                        case "iscLunarChest":
                            self.interactableCategories.categories[i].cards[j].selectionWeight = 2;
                            break;
                    }

                }
                else if (i == 2)
                {
                    switch (self.interactableCategories.categories[i].cards[j].spawnCard.name)
                    {
                        case "iscShrineCleanse":
                            if (self.interactableCategories.categories[i].cards[j].selectionWeight == 3)
                            {
                                self.interactableCategories.categories[i].cards[j].selectionWeight = 10;
                            }
                            break;
                    }
                }
                else if (i == 5)
                {
                    switch (self.interactableCategories.categories[i].cards[j].spawnCard.name)
                    {
                        case "iscRadarTower":
                            self.interactableCategories.categories[i].cards[j].selectionWeight *= 4;
                            break;
                    }
                }
                else if (i == 6)
                {
                    switch (self.interactableCategories.categories[i].cards[j].spawnCard.name)
                    {


                        case "iscScrapper":
                            if (self.interactableCategories.categories[i].cards[j].selectionWeight == 12)
                            {
                                self.interactableCategories.categories[i].cards[j].selectionWeight = 13;
                            }
                            break;
                        case "iscDuplicatorLarge":
                            if (self.interactableCategories.categories[i].cards[j].selectionWeight == 6)
                            {
                                self.interactableCategories.categories[i].cards[j].selectionWeight = 9;
                            }
                            break;
                        case "iscDuplicatorMilitary":
                            if (self.interactableCategories.categories[i].cards[j].selectionWeight == 1)
                            {
                                self.interactableCategories.categories[i].cards[j].selectionWeight = 3;
                            }
                            if (self.interactableCategories.categories[i].cards[j].minimumStageCompletions == 4)
                            {
                                self.interactableCategories.categories[i].cards[j].minimumStageCompletions = 3;
                            }
                            break;
                        case "iscDuplicatorWild":
                            if (self.interactableCategories.categories[i].cards[j].selectionWeight == 2)
                            {
                                self.interactableCategories.categories[i].cards[j].selectionWeight = 3;
                            }
                            break;
                    }
                }
                //Debug.LogWarning(self.interactableCategories.categories[i].cards[j].spawnCard);
            }

        }
    }
}
*/

//Captain
/*
Texture2D texCaptainJacketDiffuseW = new Texture2D(512, 512, TextureFormat.DXT1, false);
texCaptainJacketDiffuseW.LoadImage(Properties.Resources.texCaptainJacketDiffuseW, false);
texCaptainJacketDiffuseW.filterMode = FilterMode.Bilinear;
texCaptainJacketDiffuseW.wrapMode = TextureWrapMode.Clamp;

Texture2D texCaptainPaletteW2 = new Texture2D(256, 256, TextureFormat.DXT1, false);
texCaptainPaletteW2.LoadImage(Properties.Resources.texCaptainPaletteW2, false);
texCaptainPaletteW2.filterMode = FilterMode.Bilinear;
texCaptainPaletteW2.wrapMode = TextureWrapMode.Clamp;

Texture2D texCaptainPaletteW = new Texture2D(256, 256, TextureFormat.DXT1, false);
texCaptainPaletteW.LoadImage(Properties.Resources.texCaptainPaletteW, false);
texCaptainPaletteW.filterMode = FilterMode.Bilinear;
texCaptainPaletteW.wrapMode = TextureWrapMode.Clamp;





CharacterModel.RendererInfo[] CaptainRedRenderInfos = new CharacterModel.RendererInfo[7];
Array.Copy(CaptainSkinWhite.rendererInfos, CaptainRedRenderInfos, 7);

Material matCaptainAlt = Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
Material matCaptainAlt2 = Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
Material matCaptainArmorAlt = Instantiate(CaptainSkinWhite.rendererInfos[2].defaultMaterial);
Material matCaptainJacketAlt = Instantiate(CaptainSkinWhite.rendererInfos[3].defaultMaterial);
Material matCaptainRobotBitsAlt = Instantiate(CaptainSkinWhite.rendererInfos[4].defaultMaterial);

matCaptainAlt.mainTexture = texCaptainPaletteW;
matCaptainAlt2.mainTexture = texCaptainPaletteW2;
matCaptainArmorAlt.color = new Color(0.3f, 0.255f, 0.25f, 1f);
//_EmColor is juts fucking weird
matCaptainJacketAlt.mainTexture = texCaptainJacketDiffuseW;
matCaptainRobotBitsAlt.SetColor("_EmColor", new Color(1f, 1f, 0f, 1f));
matCaptainRobotBitsAlt.color = new Color32(203, 171, 88, 255);

CaptainRedRenderInfos[0].defaultMaterial = matCaptainAlt; //matCaptainAlt
CaptainRedRenderInfos[1].defaultMaterial = matCaptainAlt2; //matCaptainAlt
CaptainRedRenderInfos[2].defaultMaterial = matCaptainArmorAlt; //matCaptainArmorAlt
CaptainRedRenderInfos[3].defaultMaterial = matCaptainJacketAlt; //matCaptainJacketAlt
CaptainRedRenderInfos[4].defaultMaterial = matCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
CaptainRedRenderInfos[5].defaultMaterial = matCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
CaptainRedRenderInfos[6].defaultMaterial = matCaptainAlt; //matCaptainAlt

LoadoutAPI.SkinDefInfo CaptainRedSkinInfos = new LoadoutAPI.SkinDefInfo
{
   BaseSkins = CaptainSkinWhite.baseSkins,

   NameToken = "Rouge",
   UnlockableDef = null,
   RootObject = CaptainSkinWhite.rootObject,
   RendererInfos = CaptainRedRenderInfos,
   Name = "skinCaptainWolfo",
   //Icon = texTreebotBlueSkinIconS,
};
LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), CaptainRedSkinInfos);
*/
/*
On.RoR2.ScavengerItemGranter.Start += (orig, self) =>
{
    orig(self);
    Inventory inventory = self.gameObject.GetComponent<Inventory>();
    //Debug.LogWarning("Scav Grant Item " + inventory.currentEquipmentIndex);


    EquipmentDef tempdef = inventory.currentEquipmentState.equipmentDef;

    if (tempdef == RoR2Content.Equipment.Recycle)
    {
        do
        {
            inventory.GiveRandomEquipment();
            tempdef = inventory.currentEquipmentState.equipmentDef;
        }
        while (tempdef == RoR2Content.Equipment.Recycle);
    }

};
*/