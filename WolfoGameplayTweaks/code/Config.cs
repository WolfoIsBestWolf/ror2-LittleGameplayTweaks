using BepInEx;
using BepInEx.Configuration;

namespace LittleGameplayTweaks
{
    public class WConfig
	{
        public static ConfigFile ConfigFileUNSORTED = new ConfigFile(Paths.ConfigPath + "\\Wolfo.Little_Gameplay_Tweaks.cfg", true);

        public static ConfigEntry<bool> LevelMaximum;

        public static ConfigEntry<bool> SulfurPoolsSkin;


        public static ConfigEntry<bool> DCCSEnemyChanges;
        public static ConfigEntry<bool> DCCSEnemyChangesLooping;
        public static ConfigEntry<bool> DCCSEnemyNewFamilies;
        public static ConfigEntry<float> DCCSEnemyFamilyChance;
        //
        public static ConfigEntry<bool> DCCSInteractableChanges;
        public static ConfigEntry<bool> DCCSCategoryChest;
        public static ConfigEntry<bool> DCCSInteractableCostChanges;
       

        public static ConfigEntry<float> InteractablesMountainMultiplier;

        //
        //Changes - Stages
        public static ConfigEntry<bool> DCCSInteractablesStageCredits;
        public static ConfigEntry<bool> cfgVoidStagesNoTime;
        public static ConfigEntry<bool> cfgGoldShoresCredits;
        public static ConfigEntry<bool> cfgLoopWeather;
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
        public static ConfigEntry<bool> InteractableBloodShrineScaleWithTime;
        public static ConfigEntry<bool> InteractableBloodShrineLessCost;
        public static ConfigEntry<bool> InteractablesCombatShrineHP;

        public static ConfigEntry<bool> InteractableFastHalcyShrine;

        public static ConfigEntry<int> InteractableRedSoupAmount;
        public static ConfigEntry<int> MegaDroneCost;
        public static ConfigEntry<int> TurretDroneCost;
        //
        //Changes - Character
        public static ConfigEntry<bool> CharactersCaptainKeepInHiddemRealm;
        public static ConfigEntry<bool> CharactersEngineerWarbanner;
        public static ConfigEntry<bool> CharactersHuntressLysateCell;
        public static ConfigEntry<bool> CharactersCommandoInvul;
        public static ConfigEntry<bool> CharactersVoidFiendEquip;
        //
        //Changes - Items
        //public static ConfigEntry<bool> ItemsCaptainMatrix;
        //public static ConfigEntry<bool> ItemsScopeBuff;
        //public static ConfigEntry<bool> ItemsKnurlArmor;
        //public static ConfigEntry<bool> ItemsVoidRing;
        //public static ConfigEntry<bool> ItemsEuologyLunarElites;
        //public static ConfigEntry<bool> ItemSquidMechanical;
        //public static ConfigEntry<bool> ItemDefenseNucleus;
        //public static ConfigEntry<bool> MinionsInherit;
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
        public static ConfigEntry<bool> GuaranteedRedToWhite;
        public static ConfigEntry<bool> ThirdLunarSeer;
        public static ConfigEntry<bool> EclipseDifficultyAlways;
        //
        //Prism
        public static ConfigEntry<bool> cfgPrismaticElites;


        public static void InitConfig()
        {
            LevelMaximum = ConfigFileUNSORTED.Bind(
                "!Optional",
                "999 Maximum Level",
                false,
                "Makes enemy level go up to level 999. This will make looping a lot more dangerous but it's my preferred way to play."
            );

            //DCCS Enemy
            DCCSEnemyChanges = ConfigFileUNSORTED.Bind(
                "Spawnpool - Enemy",
                "Changes to spawn enemy spawn pools (Pre Loop)",
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
                "Changes to spawn enemy spawn pools (Loop)",
                true,
                "All stages will have enemies that will only appear after looping"
            );
            DCCSEnemyNewFamilies = ConfigFileUNSORTED.Bind(
                "Spawnpool - Enemy",
                "Family event additions and changes",
                true,
                "Add new family events and reorganizes existing ones"
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
                "Changes to spawn interactable spawn pools",
                true,
                "Mountain Shrine on Sulfur Pools, No Gunner Turret on Stage 4 & 5, Rare Printers and Cleansing Pools are a bit more common."
            );
            DCCSCategoryChest = ConfigFileUNSORTED.Bind(
                "Spawnpool - Interactables",
                "Category Chest limitation",
                false,
                "Only one type of Category chest per stage. This feature was cut from the mod, but config for people who still want it."
            );
            DCCSInteractableCostChanges = ConfigFileUNSORTED.Bind(
                "Spawnpool - Interactables",
                "Makes various interactables cheaper to spawn",
                true,
                "Combat and Blood shrines most notably"
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
                "Plains & Roost & Aquaduct get +20, Sirens & Grove get +40."
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
            cfgLoopWeather = ConfigFileUNSORTED.Bind(
                "Changes - Stages",
                "Loop Weather for some more stages",
                true,
                "Nothing nearly as fancy as the other ones."
            );
            //
            //Changes - Interactables
            FasterPrinter = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Faster Printers",
                true,
                ""
            );
            FasterScrapper = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Faster Scrappers",
                true,
                ""
            );
            FasterShrines = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Faster Shrines",
                true,
                ""
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
            InteractableBloodShrineScaleWithTime = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Blood Shrine reward scale with Difficulty",
                true,
                "Normally Blood Shrines Reward is only based on HP quickly making them a bad source of money"
            );
            InteractableBloodShrineLessCost = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Blood Shrine 50 70 90",
                true,
                "Less cost cuz 75 blood cost breaks items. Also means you get less gold ig"
            );
            InteractableFastHalcyShrine = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Halcy Shrine suck gold faster",
                true,
                "Suck gold at rate of 3 instead of 1"
            );
            InteractablesCombatShrineHP = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "Combat Shrine enemies multipled HP in multiplayer",
                false,
                "Enemies spawned by combat shrines have multiplied hp in vanilla."
            );
            InteractableRedSoupAmount = ConfigFileUNSORTED.Bind(
                "Changes - Interactables",
                "RedToWhite Cauldron extra item amount",
                1,
                "This is in addition to the 3 that Vanilla pays out with\nie If you want 4 set it to 2"
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

            //
            //Changes - Items
            /*ItemsCaptainMatrix = ConfigFileUNSORTED.Bind(
                "Changes - Items",
                "Red Defense Microbots",
                false,
                "Make Defensive Microbots available as a normal item"
            );
            ItemsScopeBuff = ConfigFileUNSORTED.Bind(
                "Changes - Items",
                "Laser Scope gives 10 crit",
                true,
                "Other crit items give crit chance on first stack"
            );
            ItemsKnurlArmor = ConfigFileUNSORTED.Bind(
                "Changes - Items",
                "Knurl gives 14 Armor",
                true,
                "The stats it gives arent that important and not many items give armor."
            );
            ItemsEuologyLunarElites = ConfigFileUNSORTED.Bind(
                "Changes - Items",
                "Eulogy Zero Perfected Elites",
                true,
                "Should Eulogy have a chance to replace Tier 1 elites with a Perfected elite"
            );
            ItemSquidMechanical = ConfigFileUNSORTED.Bind(
                "Changes - Items",
                "Squid Polyp Mechanical",
                true,
                "Make Squid Turret benefit from Captain Microbots, Spare Drone Parts and immune to Void Infestors."
           );
            ItemDefenseNucleus = ConfigFileUNSORTED.Bind(
                "Changes - Items",
                "Defense Nucleus Buff",
                true,
                "Constructs get 200 Wake of Vultures"
            );
            MinionsInherit = ConfigFileUNSORTED.Bind(
                "Changes - Items",
                "Minions Inherit Elite Equip",
                true,
                "If you have a Elite Aspect your minions spawn as Elites"
            );*/
            //
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
            CharactersEngineerWarbanner = ConfigFileUNSORTED.Bind(
              "Changes - Characters",
              "Engineer Warbanner Turrets",
              true,
              "Should Engi Turrets inherit Warbanner"
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
                "Mending Healing Core",
                true,
                "Mending Healing Core heals more and with some invulnerability."
            );
            cfgElderLemurianBands = ConfigFileUNSORTED.Bind(
                 "Changes - Enemies",
                 "Aquaduct Elder Lemurian Buff",
                 true,
                 "They will be able to activate their bands and scale with level."
             );
            /*cfgVoidlingNerf = ConfigFileUNSORTED.Bind(
                "Changes - Enemies",
                "Nerf Voidlings damage",
                false,
                "Voidling will deal 40% less damage and have 20/10/0% less health depending on phase."
            );*/
            //
            //Misc Unsorted
            EclipseDifficultyAlways = ConfigFileUNSORTED.Bind(
                "Other",
                "Eclipse 8 Difficulty always available",
                false,
                "Allows you to choose 8 Eclipse in normal lobbies."
            );

            GuaranteedRedToWhite = ConfigFileUNSORTED.Bind(
               "Other",
               "Guaranteed RedToWhite Cauldron on Commencement",
               true,
               "It does not guarantee it will be a good item in any way. Simply makes it more consistent at getting rid of Red Scrap if you have any."
           );

            ThirdLunarSeer = ConfigFileUNSORTED.Bind(
                "Other",
                "Third Lunar Seer",
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
                20f,
                "Vanilla is 5%"
            );
            VoidPortalChance = ConfigFileUNSORTED.Bind(
                "Rates",
                "More Void Portals",
                true,
                "Guaranteed Void Portal on Primordial Teleporters with 20% chance on normal teleporters starting stage 4. Vanilla would be 10% after stage 7."
            );
            ////////////////////////
        }

    }
}