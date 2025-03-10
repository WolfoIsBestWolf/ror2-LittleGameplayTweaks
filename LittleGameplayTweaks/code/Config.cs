using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using RiskOfOptions.OptionConfigs;
using UnityEngine;
using RoR2;

namespace LittleGameplayTweaks
{
    public class WConfig
	{
        public static ConfigFile ConfigFileUNSORTED = new ConfigFile(Paths.ConfigPath + "\\Wolfo.Little_Gameplay_Tweaks.cfg", true);

        public static ConfigEntry<bool> EclipseAllowVoid;
        public static ConfigEntry<bool> EclipseAllowTwisted;
        public static ConfigEntry<bool> EclipseAllowArtifactWorld;

        public static ConfigEntry<EclipseArtifact> EclipseAllowChoiceArtifacts;
        public static ConfigEntry<bool> EclipseAllowChoiceWeirdArtifacts;
        public static ConfigEntry<bool> EclipseAllowChoiceAllowAllArtifacts;

        public static ConfigEntry<bool> PrismElites;
        //public static ConfigEntry<bool> PrismDefaultRemix;
        public static ConfigEntry<bool> PrismAllowChoiceArtifacts;


        public static ConfigEntry<bool> LevelMaximum;
        public static ConfigEntry<int> LevelMaximumFinalBoss;
        public static ConfigEntry<bool> cheaperTier2;
        public static ConfigEntry<bool> disableNewContent;

        public static ConfigEntry<bool> onlyUpdateMostRecentSpawnPools;
        public static ConfigEntry<bool> portalAfterLimbo;


        public static ConfigEntry<bool> SulfurPoolsSkin;

 
        public static ConfigEntry<bool> DCCSEnemyChanges;
        public static ConfigEntry<bool> DCCSEnemyChangesLooping;
        public static ConfigEntry<bool> DCCSEnemyNewFamilies;
        public static ConfigEntry<bool> FamiliesStage1;
        public static ConfigEntry<float> DCCSEnemyFamilyChance;
        
        //
        public static ConfigEntry<bool> DCCSInteractableChanges;
        //public static ConfigEntry<bool> DCCSCategoryChest;
        public static ConfigEntry<bool> DCCSInteractableCostChanges;
        public static ConfigEntry<bool> DccsAddCategoryToSots;

        public static ConfigEntry<float> InteractablesMountainMultiplier;

        //
        //Changes - Stages
        public static ConfigEntry<bool> DCCSInteractablesStageCredits;
        public static ConfigEntry<bool> cfgVoidStagesNoTime;
        public static ConfigEntry<bool> cfgGoldShoresCredits;
        //
        //Changes - Interactables
        public static ConfigEntry<bool> FasterPrinter;
        public static ConfigEntry<bool> FasterScrapper;
        public static ConfigEntry<bool> FasterShrines;
        public static ConfigEntry<int> FasterDeepVoidSignal;
        public static ConfigEntry<bool> FasterArenaCells;
        public static ConfigEntry<bool> RegenArenaCells;

        public static ConfigEntry<bool> InteractableNoLunarCost;
        public static ConfigEntry<bool> InteractableHealingShrine;
        public static ConfigEntry<bool> InteractableBloodShrineMoreGold;
        public static ConfigEntry<bool> InteractableBloodShrineLessCost;
        public static ConfigEntry<bool> InteractablesCombatShrineHP;

        public static ConfigEntry<bool> InteractableFastHalcyShrine;
        public static ConfigEntry<bool> InteractableHalcShrineNerf;
        public static ConfigEntry<bool> InteractableHalcyShrineHalcy;

        public static ConfigEntry<int> InteractableRedSoupAmount;
        public static ConfigEntry<int> MegaDroneCost;
        public static ConfigEntry<int> TurretDroneCost;
        //
        //Changes - Character
        public static ConfigEntry<bool> CharactersCaptainKeepInHiddemRealm;
 
        public static ConfigEntry<bool> CharactersHuntressLysateCell;
        public static ConfigEntry<bool> CharactersCommandoInvul;
        public static ConfigEntry<bool> CharactersVoidFiendEquip;

        public static ConfigEntry<bool> BuffMegaDroneStats;
        public static ConfigEntry<bool> cscGreaterWispCredits;

        //
        //Changes - Enemies
        public static ConfigEntry<bool> cfgScavBossItem;
        public static ConfigEntry<bool> cfgScavMoreItemsElites;
        public static ConfigEntry<bool> cfgScavNewTwisted;
        public static ConfigEntry<bool> cfgScavTwistedScaling;
        public static ConfigEntry<bool> cfgMendingCoreBuff;
        public static ConfigEntry<bool> cfgElderLemurianBands;
        //public static ConfigEntry<bool> cfgVoidlingNerf;
        //
        //Rates
        public static ConfigEntry<bool> VoidPortalChance;
        public static ConfigEntry<float> ShopChancePercentage;
        public static ConfigEntry<float> YellowPercentage;
        public static ConfigEntry<float> BonusAspectDropRate;
        //
        //public static ConfigEntry<bool> GuaranteedRedToWhite;
        public static ConfigEntry<bool> CelestialStage10;
        public static ConfigEntry<bool> ThirdLunarSeer;
        public static ConfigEntry<bool> EclipseDifficultyAlways;
        //
       
        public enum EclipseArtifact
        {
            Off,
            Some,
            Blacklist,
            All,
        }

        public static void InitConfig()
        {
            EclipseAllowTwisted = ConfigFileUNSORTED.Bind(
                "Gamemodes",
                "Eclipse allow Moment Whole",
                true,
                "Allows Celestial Portal to spawn so you can use Twisted Scav as an alternate final boss in Eclipse. The obelisk will always take you to Moment Whole even if you do not have Beads."
            );
            EclipseAllowVoid = ConfigFileUNSORTED.Bind(
                "Gamemodes",
                "Eclipse allow Void Locus",
                true,
                "Allows Void Portals to spawn so you can use Voidling as an alternate final boss in Eclipse. Does not affect the Deep Void Portal after Mithrix of course."
            );
            EclipseAllowArtifactWorld = ConfigFileUNSORTED.Bind(
                "Gamemodes",
                "Eclipse allow Bulwarks Ambry",
                false,
                "Allows computer. For artifact"
            );
            EclipseAllowChoiceArtifacts = ConfigFileUNSORTED.Bind(
                "Gamemodes",
                "Eclipse choose some Artifacts",
                EclipseArtifact.Blacklist,
                "Allows the choice of often considered challenge or variety artifacts.\n\nWhitelist : Specifically made Whitelist of challenge or variety Vanilla Artifacts.\nBlacklist : All except Artifacts that make the game easier. (Includes Modded ones) (Command, Rebirth)\nAll : All"
            );
 
            /*PrismDefaultRemix = ConfigFileUNSORTED.Bind(
                "Gamemodes",
                "Prismatic Remix",
                true,
                "Makes Remixed Prismatic Trials the default rule choice"
            );*/
            PrismAllowChoiceArtifacts = ConfigFileUNSORTED.Bind(
                "Gamemodes",
                "Prismatic choose Artifacts",
                true,
                "Choose what artifacts you want to enable in Prismatic runs instead of random ones."
            );

            LevelMaximum = ConfigFileUNSORTED.Bind(
                "!Main",
                "999 Maximum Level",
                false,
                "Makes enemy level go up to level 999. This will make looping a lot more dangerous.\n\nWill update mid-run"
            );
            LevelMaximumFinalBoss = ConfigFileUNSORTED.Bind(
                "!Main",
                "Level Maximum for Final Bosses",
                200,
                "Final bosses get bonus stats because of the 99 level limit. So both higher level and bonus stats can make them far too tanky."
            );

            LevelMaximum.SettingChanged += LevelMaximum_SettingChanged;
            cheaperTier2 = ConfigFileUNSORTED.Bind(
                "!Main",
                "Cheaper Tier 2 Elites",
                true,
                "Makes Tier 2 Elites cost 30x instead of 36x.\n\nWill update mid-run if changed"
            );
            cheaperTier2.SettingChanged += CheaperTier2_SettingChanged;
            disableNewContent = ConfigFileUNSORTED.Bind(
                "!Main",
                "Disable New Content",
                false,
                "Disable new content added by mod. This needs to be done if you are using this mod as a client/host with people who do not have the mod."
            );


            onlyUpdateMostRecentSpawnPools = ConfigFileUNSORTED.Bind(
                "!Main",
                "Only update one set of spawn pools",
                true,
                "Auto detects what DLC you have and picks and appropriate one."
            );

            //DCCS Enemy
            DCCSEnemyChanges = ConfigFileUNSORTED.Bind(
                "Spawnpool - Enemy",
                "Enemy Spawn Pools (Pre Loop)",
                true,
                "Adds and changes some enemy variety"
            );
            SulfurPoolsSkin = ConfigFileUNSORTED.Bind(
                "Spawnpool - Enemy",
                "Sulfur Pool Beetles Skin",
                true,
                "ReImplement the unused Sulfur Pools skin"
            );
            DCCSEnemyChangesLooping = ConfigFileUNSORTED.Bind(
                "Spawnpool - Enemy",
                "Enemy Spawn Pools (Loop)",
                true,
                "All stages will have enemies that will only appear after looping"
            );
            DCCSEnemyNewFamilies = ConfigFileUNSORTED.Bind(
                "Spawnpool - Enemy",
                "Family event additions and changes",
                true,
                "Add new family events and reorganizes existing ones"
            );
            FamiliesStage1 = ConfigFileUNSORTED.Bind(
                "Spawnpool - Enemy",
                "Stage 1 Family Events",
                true,
                "Allow most family events to happen on stage 1"
            );
            DCCSEnemyFamilyChance = ConfigFileUNSORTED.Bind(
                "Spawnpool - Enemy",
                "Family event weight",
                2.22f,
                "Weight for family event. Vanilla is 2"
            );
            //
            //DCCS
            DCCSInteractableChanges = ConfigFileUNSORTED.Bind(
                "Spawnpool - Interactables",
                "Interactable Spawn Pools",
                true,
                "Mountain Shrine on Sulfur Pools, No Gunner Turret on Stage 4 & 5, Rare Printers and Cleansing Pools are a bit more common."
            );
            DccsAddCategoryToSots = ConfigFileUNSORTED.Bind(
                "Spawnpool - Interactables",
                "Large Category Chests on Sots Stages",
                true,
                "Seekers of the Storm changes do not have Large Category Chests in the pool by default"
            );
            /*DCCSCategoryChest = ConfigFileUNSORTED.Bind(
                "Spawnpool - Interactables",
                "Category Chest limitation",
                false,
                "Only one type of Category chest per stage. This feature was cut from the mod, but config for people who still want it."
            );*/
            DCCSInteractableCostChanges = ConfigFileUNSORTED.Bind(
                "Spawnpool - Interactables",
                "Cheaper Interactable Credits",
                true,
                "TC280 costs 35 instead of 40.\nShrine of Order costs 5 instead of 30.\nCombat/Blood shrines 15 instead of 20."
            );


            InteractablesMountainMultiplier = ConfigFileUNSORTED.Bind(
                "Spawnpool - Interactables",
                "Mountain Shrine multiplier",
                2f,
                "Multiply Mountain Shrine weight compared to other shrines"
            );
            //
            //
            DCCSInteractablesStageCredits = ConfigFileUNSORTED.Bind(
                "Changes - Stages",
                "Makes certain stages have more credits",
                true,
                "Plains & Roost & Aquaduct get +20, Sirens & Grove get +30."
            );
            cfgVoidStagesNoTime = ConfigFileUNSORTED.Bind(
                "Changes - Stages",
                "Make all 3 Void Stages untimed stages",
                true,
                "Untimed but counts as stage completed"
            );
            cfgGoldShoresCredits = ConfigFileUNSORTED.Bind(
                "Changes - Stages",
                "Gilded Coast gets interactables",
                true,
                "Mostly combat shrines"
            );
            CelestialStage10 = ConfigFileUNSORTED.Bind(
                "Changes - Stages",
                "Celestial Portal Stage 10",
                true,
                "An additional celestial portal will spawn on the last stage of a loop"
            );

            //Changes - Interactables
            FasterPrinter = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Faster Printers",
                true,
                "Slighty faster interactables"
            );
            FasterScrapper = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Faster Scrappers",
                true,
                "Slighty faster interactables"
            );
            FasterShrines = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Faster Shrines",
                true,
                "Slighty faster interactables"
            );
            FasterDeepVoidSignal = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Faster Deep Void Signal",
                45,
                "Vanilla is 60."
            );
            FasterArenaCells = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Void Fields Cells - Faster early cells",
                true,
                "30s for the first 4, 45s for the next 4, 60s for the last. "
            );
            RegenArenaCells = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Void Fields Cells - Regen",
                true,
                "Cells give regeneration before actvating so you can heal faster."
            );
            InteractableBloodShrineMoreGold = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Blood Shrine reward scale with Difficulty",
                true,
                "Normally Blood Shrines Reward is only based on HP quickly making them a bad source of money"
            );
            InteractableBloodShrineLessCost = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Blood Shrine 50 70 90",
                true,
                "Less cost because 75 blood cost breaks items."
            );
            InteractableFastHalcyShrine = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Halcy Shrine suck gold faster",
                true,
                "Suck gold at rate of 3 instead of 1"
            );
            InteractableHalcShrineNerf = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Halcy Shrine - Less Monsters",
                true,
                "Halcyon Shrines get 300 credits for monsters, which is a lot. A Void Seed as a whole is 120 Credits.\nThis setting takes it down to 80 per wave 240 Total"
            );
            InteractableHalcyShrineHalcy = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Halcy Shrine - Spawn Halcyonites",
                true,
                "Have a chance to spawn Halcyonties instead of Golems"
            );
            InteractablesCombatShrineHP = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Combat Shrine HP scaling in multiplayer",
                false,
                "Enemies spawned by combat shrines have multiplied hp per player in vanilla."
            );
            InteractableRedSoupAmount = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "RedToWhite Cauldron extra item amount",
                1,
                "This is in addition to the 3 that Vanilla pays out with\nie If you want 5 items total, set it to 2.  Set to 0 to disable."
            );

            InteractableNoLunarCost = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Cost - Free Lunars",
                false,
                "Everything that costs Lunar Coins will cost 0"
            );

            MegaDroneCost = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Cost - TC-280 Prototype",
                300,
                "Vanilla is 350"
            );
            BuffMegaDroneStats = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "TC-280 Prototype buff stats",
                true,
                "Give Tc280 20 armor and resistance to aoe attacks like Empathy Cores have"
            );

            TurretDroneCost = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Cost - Gunner Turret",
                25,
                "Vanilla is 35"
            );
            InteractableHealingShrine = ConfigFileUNSORTED.Bind(
              "Changes - Interactables",
              "Cost - Shrine of Woods",
              true,
              "Make Shrine of Woods cost significantly less"
          );
 
            //Changes - Characters
            CharactersCaptainKeepInHiddemRealm = ConfigFileUNSORTED.Bind(
                "Changes - Characters",
                "Captain Hidden Realms",
                true,
                "Should Captain be allowed to have fun in Hidden and Void realms."
            );
            CharactersHuntressLysateCell = ConfigFileUNSORTED.Bind(
              "Changes - Characters",
              "Huntress Balista Lysate Cell",
              true,
              "Lysate Cell will combined 2 casts of Ballista if you have the stock available."
            );
            CharactersCommandoInvul = ConfigFileUNSORTED.Bind(
              "Changes - Characters",
              "Commando Invulnerable Tactical Dive",
              true,
              "Tactical Dive will make you Invulnerable like in RoRR"
            );
            CharactersVoidFiendEquip = ConfigFileUNSORTED.Bind(
              "Changes - Characters",
              "Void Fiend 2 Equipment",
              false,
              "Void Fiend gets two equipment slots switching between them as he switches modes\nMade this as a joke idk how this would be helpful"
            );

            //
            //Changes - Enemies
            cfgScavBossItem = ConfigFileUNSORTED.Bind(
                "Changes - Enemies",
                "Scavs as proper Bosses",
                true,
                "Scavs are allowed to spawn as a boss under normal conditions and gain a Boss item"
            );
            cfgScavMoreItemsElites = ConfigFileUNSORTED.Bind(
                "Changes - Enemies",
                "Boss and Elite scavs get more items",
                true,
                "Boss Scavs get a small amount more items while Elite scavs get a lot more."
            );
            cfgScavNewTwisted = ConfigFileUNSORTED.Bind(
                "Changes - Enemies",
                "New Twisted Scavengers",
                true,
                "Adds new Twisted Scavengers"
            );
            cfgScavTwistedScaling = ConfigFileUNSORTED.Bind(
                "Changes - Enemies",
                "Fix Twisted Scavengers not scaling harder in Multiplayer",
                true,
                "Mithrix and Voidling gets more of a HP boost in Multiplayer, Twisteds do not due to a bug."
            );
            cfgMendingCoreBuff = ConfigFileUNSORTED.Bind(
                "Changes - Enemies",
                "Mending Healing Orb buff",
                true,
                "Healing Core heals more and with some invulnerability so you don't instantly kill it."
            );
            cfgElderLemurianBands = ConfigFileUNSORTED.Bind(
                 "Changes - Enemies",
                 "Aquaduct Elder Lemurian Buff",
                 true,
                 "They will be able to activate their bands and scale with level."
             );
            cscGreaterWispCredits = ConfigFileUNSORTED.Bind(
                 "Changes - Enemies",
                 "Cheaper Greater Wisps",
                 true,
                 "Greater Wisps cost a lot for being not much of a threat, this makes them more common"
             );
            //
            //Misc Unsorted
            EclipseDifficultyAlways = ConfigFileUNSORTED.Bind(
                "!Main",
                "Eclipse 8 Difficulty always available",
                false,
                "Allows you to choose 8 Eclipse in normal lobbies."
            );
            EclipseDifficultyAlways.SettingChanged += EclipseDifficultyAlways_SettingChanged; 

            /*GuaranteedRedToWhite = ConfigFileUNSORTED.Bind(
               "Other",
               "Guaranteed RedToWhite Cauldron on Commencement",
               true,
               "It does not guarantee it will be a good item in any way. Simply makes it more consistent at getting rid of Red Scrap if you have any."
           );*/

            ThirdLunarSeer = ConfigFileUNSORTED.Bind(
                "Changes - Stages",
                "Bazaar : Third Lunar Seer",
                true,
                "Future proofing for when they add more stage without really making the bazaar more powerful."
            );
            //
            //Rates
            YellowPercentage = ConfigFileUNSORTED.Bind(
                "Rates",
                "Boss Item TP Percent",
                20f,
                "Percent for a yellow item to replace a green from a teleporter. Vanilla is 15"
            );
            BonusAspectDropRate = ConfigFileUNSORTED.Bind(
                "Rates",
                "Aspect Drop Rate",
                0.067f,
                "Percent Drop rate for Aspects. Vanilla is 0.025 or 1/4000. Does not need to be changed if ZetAspects is changed."
            );
            ShopChancePercentage = ConfigFileUNSORTED.Bind(
                "Rates",
                "Natural Bazaar Portal Chance",
                15f,
                "Vanilla is 5%"
            );
            VoidPortalChance = ConfigFileUNSORTED.Bind(
                "Rates",
                "More Void Portals",
                true,
                "Guaranteed Void Portal on Primordial Teleporters with 20% chance on normal teleporters starting stage 4. Vanilla would be 10% after stage 7."
            );
            ////////////////////////
            RiskConfig();
        }

        private static void EclipseDifficultyAlways_SettingChanged(object sender, System.EventArgs e)
        {
            RuleChoiceDef tempR = RuleCatalog.FindChoiceDef("Difficulty.Eclipse8");
            if (tempR != null)
            {
                tempR.excludeByDefault = !WConfig.EclipseDifficultyAlways.Value;
            }
        }

        private static void CheaperTier2_SettingChanged(object sender, System.EventArgs e)
        {
            if (CombatDirector.eliteTiers.Length > 1)
            {
                if (cheaperTier2.Value)
                {
                    CombatDirector.eliteTiers[LittleGameplayTweaks.FindTier2Elite()].costMultiplier = CombatDirector.baseEliteCostMultiplier * 5f;
                }
                else
                {
                    CombatDirector.eliteTiers[LittleGameplayTweaks.FindTier2Elite()].costMultiplier = CombatDirector.baseEliteCostMultiplier * 6f;
                }
            }   
        }

        private static void LevelMaximum_SettingChanged(object sender, System.EventArgs e)
        {
            if (WConfig.LevelMaximum.Value)
            {
                RoR2.Run.ambientLevelCap = 999;
                On.RoR2.LevelUpEffectManager.OnCharacterLevelUp += LittleGameplayTweaks.LevelUpEffectManager_OnCharacterLevelUp;
                On.RoR2.LevelUpEffectManager.OnRunAmbientLevelUp += LittleGameplayTweaks.LevelUpEffectManager_OnRunAmbientLevelUp;
            }
            else
            {
                RoR2.Run.ambientLevelCap = 99;
                On.RoR2.LevelUpEffectManager.OnCharacterLevelUp -= LittleGameplayTweaks.LevelUpEffectManager_OnCharacterLevelUp;
                On.RoR2.LevelUpEffectManager.OnRunAmbientLevelUp -= LittleGameplayTweaks.LevelUpEffectManager_OnRunAmbientLevelUp;
            }
        }

        public static void RiskConfig()
        {
 
            ModSettingsManager.SetModDescription("Mostly spawn pool tweaks");

            ModSettingsManager.AddOption(new CheckBoxOption(LevelMaximum, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cheaperTier2, false));
            ModSettingsManager.AddOption(new CheckBoxOption(disableNewContent, true));
            ModSettingsManager.AddOption(new CheckBoxOption(EclipseDifficultyAlways, false));

            ModSettingsManager.AddOption(new CheckBoxOption(EclipseAllowVoid, false));
            ModSettingsManager.AddOption(new CheckBoxOption(EclipseAllowTwisted, false));
            ModSettingsManager.AddOption(new CheckBoxOption(EclipseAllowArtifactWorld, false));
            ModSettingsManager.AddOption(new ChoiceOption(EclipseAllowChoiceArtifacts, false));
            ModSettingsManager.AddOption(new CheckBoxOption(PrismAllowChoiceArtifacts, false));
            //ModSettingsManager.AddOption(new CheckBoxOption(PrismDefaultRemix, false));

            ModSettingsManager.AddOption(new CheckBoxOption(FasterPrinter, true));
            ModSettingsManager.AddOption(new CheckBoxOption(FasterScrapper, true));
            ModSettingsManager.AddOption(new CheckBoxOption(FasterShrines, true));
            ModSettingsManager.AddOption(new CheckBoxOption(FasterArenaCells, true));
            ModSettingsManager.AddOption(new CheckBoxOption(RegenArenaCells, true));
            ModSettingsManager.AddOption(new IntFieldOption(InteractableRedSoupAmount, false));
            ModSettingsManager.AddOption(new CheckBoxOption(InteractableNoLunarCost, false));
            ModSettingsManager.AddOption(new CheckBoxOption(InteractableHealingShrine, true));
            ModSettingsManager.AddOption(new CheckBoxOption(InteractableBloodShrineMoreGold, true));
            ModSettingsManager.AddOption(new CheckBoxOption(InteractableBloodShrineLessCost, true));
            ModSettingsManager.AddOption(new CheckBoxOption(InteractablesCombatShrineHP, true));
            ModSettingsManager.AddOption(new CheckBoxOption(InteractableHalcShrineNerf, true));

            ModSettingsManager.AddOption(new CheckBoxOption(DCCSInteractablesStageCredits, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgVoidStagesNoTime, true));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgGoldShoresCredits, true));
            ModSettingsManager.AddOption(new CheckBoxOption(ThirdLunarSeer, false));
            ModSettingsManager.AddOption(new CheckBoxOption(CelestialStage10, false));

            ModSettingsManager.AddOption(new CheckBoxOption(cfgMendingCoreBuff, true));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgElderLemurianBands, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgScavBossItem, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgScavMoreItemsElites, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cscGreaterWispCredits, true));

            ModSettingsManager.AddOption(new CheckBoxOption(BuffMegaDroneStats, true));
            ModSettingsManager.AddOption(new CheckBoxOption(CharactersCaptainKeepInHiddemRealm, true));
            ModSettingsManager.AddOption(new CheckBoxOption(CharactersHuntressLysateCell, true));
            ModSettingsManager.AddOption(new CheckBoxOption(CharactersCommandoInvul, true));

            ModSettingsManager.AddOption(new CheckBoxOption(DCCSEnemyChanges, true));
            ModSettingsManager.AddOption(new CheckBoxOption(DCCSEnemyChangesLooping, true));
            ModSettingsManager.AddOption(new CheckBoxOption(DCCSEnemyNewFamilies, true));
            ModSettingsManager.AddOption(new CheckBoxOption(FamiliesStage1, true));
            ModSettingsManager.AddOption(new CheckBoxOption(SulfurPoolsSkin, true));

            ModSettingsManager.AddOption(new CheckBoxOption(DCCSInteractableChanges, true));
            ModSettingsManager.AddOption(new CheckBoxOption(DCCSInteractableCostChanges, true));
            ModSettingsManager.AddOption(new CheckBoxOption(DccsAddCategoryToSots, true));
            ModSettingsManager.AddOption(new FloatFieldOption(InteractablesMountainMultiplier, true));

            ModSettingsManager.AddOption(new CheckBoxOption(onlyUpdateMostRecentSpawnPools, true));


        }
    }
}