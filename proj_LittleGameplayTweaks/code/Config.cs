using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace LittleGameplayTweaks
{
    public class WConfig
    {
        public static ConfigFile ConfigFileLGT = new ConfigFile(Paths.ConfigPath + "\\Wolfo.Little_Gameplay_Tweaks.cfg", true);

        public enum MatchCategory
        {
            MatchSmallToBig,
            RandomOneSmall,
            NoChange,
        }
        public static ConfigEntry<bool> cfgMoreLoop;
        public static ConfigEntry<bool> cfgVoidTripleAllTier;
        public static ConfigEntry<bool> cfgMasstweak;

        public static ConfigEntry<bool> cfgE1Unnerf;


        public static ConfigEntry<bool> cfgOverloadingWorm;
        public static ConfigEntry<bool> cfgEarlyScavs;
        public static ConfigEntry<MatchCategory> cfgMatchCategory;
        public static ConfigEntry<bool> VoidSeedsScale;
        public static ConfigEntry<bool> VoidSeedsMore;
        public static ConfigEntry<bool> VoidCradlesMore;

        public static ConfigEntry<bool> cfgBlightBuffWhatever;
        public static ConfigEntry<float> cfgShrineBossMult;


        public static ConfigEntry<bool> cfgEquipmentDroneThing;

        public static ConfigEntry<bool> cfgLoopDifficultTeleporters;


        public static ConfigEntry<bool> EclipseAllowVoid;
        public static ConfigEntry<bool> EclipseAllowTwisted;
        public static ConfigEntry<bool> EclipseAllowArtifactWorld;

        public static ConfigEntry<EclipseArtifact> EclipseAllowChoiceArtifacts;
        public static ConfigEntry<bool> EclipseAllowChoiceWeirdArtifacts;
        public static ConfigEntry<bool> EclipseAllowChoiceAllowAllArtifacts;

 
        public static ConfigEntry<bool> cheaperTier2;
        public static ConfigEntry<bool> earlyTier2;

        public static ConfigEntry<bool> portalAfterLimbo;

        public static ConfigEntry<bool> cfgDccsMonster;
        public static ConfigEntry<bool> cfgDccsFamily;
        public static ConfigEntry<bool> FamiliesStage1;
        public static ConfigEntry<float> cfgFamilyChance;
         
        public static ConfigEntry<bool> cfgDccsInteractables;
        public static ConfigEntry<bool> cfgCredits_Interactables;

    
        //Stages
        public static ConfigEntry<bool> cfgStageCredits_Interactables;
        public static ConfigEntry<bool> cfgStageCredits_Monsters;
        public static ConfigEntry<bool> cfgMonsterCreditLoopScale;
        //public static ConfigEntry<bool> cfgVoidStagesNoTime;
        public static ConfigEntry<bool> cfgGoldShoresCredits;
        //
        //Changes - Interactables
        public static ConfigEntry<bool> FasterPrinter;
        public static ConfigEntry<bool> FasterScrapper;
        public static ConfigEntry<bool> FasterShrines;
        public static ConfigEntry<bool> cfgVoidStagePillar;
        public static ConfigEntry<bool> cfgMassPillar;
        public static ConfigEntry<bool> FasterArenaCells;
        public static ConfigEntry<bool> RegenArenaCells;

        public static ConfigEntry<bool> InteractableNoLunarCost;
        public static ConfigEntry<bool> Shrine_Healing;
        public static ConfigEntry<bool> Shrine_Blood_Gold;
        public static ConfigEntry<bool> Shrine_Blood_NoBreak;
        public static ConfigEntry<bool> Shrine_Combat;

        public static ConfigEntry<bool> cfgHalcyon_FastDrain;
        public static ConfigEntry<bool> cfgHalcyon_ScaleHPMult;
        public static ConfigEntry<bool> cfgHalcyon_Spawnpool;
        public static ConfigEntry<bool> cfgHalcyon_NerfStats;
        public static ConfigEntry<bool> cfgHalcyon_NoForcedSots;

        public static ConfigEntry<int> InteractableRedSoupAmount;
        public static ConfigEntry<int> MegaDroneCost;
        public static ConfigEntry<int> TurretDroneCost;
        //
        //Changes - Character
        public static ConfigEntry<bool> CharactersCaptainKeepInHiddemRealm;

        public static ConfigEntry<bool> CharactersHuntressLysateCell;
        public static ConfigEntry<bool> CharactersCommandoInvul;
        //public static ConfigEntry<bool> CharactersVoidFiendEquip;

        public static ConfigEntry<bool> BuffMegaDroneStats;
        public static ConfigEntry<bool> cfgCredits_Monsters;

        //
        //Changes - Enemies
        public static ConfigEntry<bool> cfgScavBoss;
        public static ConfigEntry<bool> cfgScavMoreItemsElites;

        public static ConfigEntry<bool> cfgScavTwistedScaling;
        public static ConfigEntry<bool> cfgMendingCoreBuff;
        public static ConfigEntry<bool> cfgElderLemurianBands;

        //Rates
        public static ConfigEntry<bool> VoidPortalStage5;
        public static ConfigEntry<bool> cfgVoidPortalOUT;
        public static ConfigEntry<bool> cfgMSPortalOUT;

        public static ConfigEntry<int> cfgBossItemChance;
        public static ConfigEntry<int> cfgAspectDropRate;
        //
        //public static ConfigEntry<bool> GuaranteedRedToWhite;
        public static ConfigEntry<bool> CelestialStage10;
        public static ConfigEntry<bool> ThirdLunarSeer;
        public static ConfigEntry<bool> EclipseDifficultyAlways;
        //

        public static ConfigEntry<bool> cfgLunarTeleporterAlways;
        public static ConfigEntry<bool> cfgTwistedBuff;
        public static ConfigEntry<bool> cfgTwistedBuffEpic;
        public static ConfigEntry<bool> cfgBrotherHurtFix;
        public static ConfigEntry<bool> cfgVoidlingNerf;

        public static ConfigEntry<bool> cfgXIEliteFix;
        public static ConfigEntry<bool> cfgAddLevelCapPerStage;
        public static ConfigEntry<bool> cfgVoidCampScaling;

        public enum EclipseArtifact
        {
            Off,
            Blacklist,
            All,
        }
        public enum EclipseDifficulty
        {
            Off,
            E8,
            All,
        }
     
  

        public static void InitConfig()
        {

            #region Looping
            cfgMoreLoop = ConfigFileLGT.Bind(
             "Looping",
             "Loop Portal after Mithrix & Twisted Scav",
             true,
             "A portal spawns at spawn on Commencement, to allow you to loop back.\nYou cannot fight Mithrix twice in one run.\n\nA Loop Portal spawns near the center after beating Twisted Scavengers.\nYou can fight them multiple times."
            );            
            cfgLoopDifficultTeleporters = ConfigFileLGT.Bind(
             "Looping",
             "Scale | Teleporter Bosses",
             true,
             "During Loops, Teleporters spawn more bosses and bosses get additiona health.\nThis can result in Teleporters still being a more impactful event rather than a slaughter and waiting."
            );
            VoidSeedsScale = ConfigFileLGT.Bind(
                  "Looping",
                  "Scale | Void Seeds Monsters",
                  true,
                  "Void Seeds spawn more monsters during loops."
              );

            cfgMonsterCreditLoopScale = ConfigFileLGT.Bind(
                "Looping",
                "Scale | Stage starting Monsters",
                true,
                "Stages start with more monsters on them during loops, to encourage more chaos and earlier big T2 elites."
            );
            cfgAddLevelCapPerStage = ConfigFileLGT.Bind(
                "Looping",
                "Scale | Monster Level Cap",
                true,
                "Each stage completed during a loop increases the ambient level cap by 20.\nDoes not count Commencement\nJust a little boost so that enemies can continue scaling.\n\nShould be compatible with any other mods that change Level Cap."
            );
            cheaperTier2 = ConfigFileLGT.Bind(
              "Looping",
              "Cheaper Tier 2 Elites",
              true,
              "Makes Tier 2 Elites cost 30x instead of 36x.\nAdds more chaos to looping.\nWill update mid-run if changed"
          );
            earlyTier2 = ConfigFileLGT.Bind(
                "Looping",
                "Pre-Loop | Tier 2 Elites",
                false,
                "Tier 2 Elites start spawning on Stage 5.\nInstead of Stage 6.\n\nFor Loop haters."
            );
            cfgEarlyScavs = ConfigFileLGT.Bind(
              "Looping",
              "Pre-Loop | Scavengers",
              false,
              "Allow Scavengers to spawn as early as Stage 4 instead of Stage 6\n\nFor Loop haters.\n\nDoes not affect Sundered Grove either way."
            );

            VoidPortalStage5 = ConfigFileLGT.Bind(
                "Looping",
                "Void Portal Stage 5",
                true,
                "Guaranteed a portal to Void Locust every Stage 5, so it could be used as a proper alternative to Mithrix if desired.\nAlso doubles random spawn chance to 20%.\nVanilla is 10% on any stage after stage 7."
            );
            CelestialStage10 = ConfigFileLGT.Bind(
                "Looping",
                "Celestial Portal Stage 10",
                true,
                "Celestial Portals also spawn on every Stage 5, starting Stage 10.\n\nFor just more control or using Twisteds as a alt ending if wanted"
            );
            cfgLunarTeleporterAlways = ConfigFileLGT.Bind(
                "Looping",
                "Primordial Teleporter every stage",
                false,
                "After looping, Replace the teleporter on every stage with a Primordial Teleporter.\nThis is how it was in Risk of Rain Returns"
            );
 
            cheaperTier2.SettingChanged += Tier2_SettingChanged;
            earlyTier2.SettingChanged += Tier2_SettingChanged;

            #endregion

            #region Monster Changes
            cfgTwistedBuff = ConfigFileLGT.Bind(
               "Monsters",
               "Twisted Elite | Buff",
               true,
               "Twisted Elites no longer need to recharge\nTwisted Projectile can no longer killed."
           );
            cfgTwistedBuffEpic = ConfigFileLGT.Bind(
                "Monsters",
                "Twisted Elite | Disables Skills",
                true,
                "Twisted Projectile disables skills for 5 seconds instead of Lunar Ruin for 420 seconds."
            );
            cfgScavBoss = ConfigFileLGT.Bind(
                 "Monsters",
                 "Scavs as Bosses",
                 true,
                 "Scavs can spawn as a TP Boss under normal conditions but rarer than other teleporter bosses."
             );
            cfgScavMoreItemsElites = ConfigFileLGT.Bind(
                "Monsters",
                "Boss & Elite Scavs get more items",
                true,
                "Boss Scavs get 1 Yellow or 2 if Elite.\nElite Scavs get 4x4 Whites, 2x3 Greens, 2x2 Reds\nTier2 Scavs get 5x6 Whites 3x4 Greens, 2x3 Reds.\n\nThis is so that Scavengers continue to scale as a threat, with ever increasing amount of items.\nAlso because funny"
            );

            cfgMendingCoreBuff = ConfigFileLGT.Bind(
               "Monsters",
               "Mending Elite | On Death Buff",
               true,
               "Fixes Healing Cores not scaling with level, which is why they only ever healed 40.\nAlso increases their HP in general."
            );
          
            cfgBrotherHurtFix = ConfigFileLGT.Bind(
              "Monsters",
              "Mithrix | Phase 4",
              true,
              "Phase 4 is invulnerable a bit longer so you can't just one shot him while he's getting up.\nAlso immune to fall damage."
           );
            cfgVoidlingNerf = ConfigFileLGT.Bind(
               "Monsters",
               "Voidling | Scaling Nerf",
               true,
               "Nerf his special scaling by 20%"
            );
            cfgScavTwistedScaling = ConfigFileLGT.Bind(
             "Monsters",
             "Twisted Scav | Scaling Fix",
             true,
             "Twisted Scavengers always scale to 1 player because it thinks 0 players are alive.\nThis fixes that.\nOnly important for multiplayer hosts."
            );
            cfgXIEliteFix = ConfigFileLGT.Bind(
             "Monsters",
             "Elite XI | Elite Minions",
             true,
             "Fix what is likely an overseight, that unlike Beetle Queen and Solus, Elite XI does not spawn Elite minions.\n\nLike those cases, they do not gain elite stats."
            );
            cfgOverloadingWorm = ConfigFileLGT.Bind(
             "Monsters",
             "Overloading Worm Overloading Bomb",
             true,
             "The projectiles will be more spread out and leave behind overloading bubbles like the Elite."
            );
            cfgElderLemurianBands = ConfigFileLGT.Bind(
                 "Monsters",
                 "Aquaduct Elders activate Bands",
                 true,
                 "They will activate their Bands on hit and scale with level at half the rate."
             );


            #endregion
            #region Interactables Changes
            FasterPrinter = ConfigFileLGT.Bind(
               "Interactables",
               "Faster Printers",
              true,
               "Removes endlag, but keeps full lenght animation. Time 4s -> 2.83s."
           );
            FasterScrapper = ConfigFileLGT.Bind(
                "Interactables",
                "Faster Scrappers",
                true,
                "Generally a bit faster without removing the animation entirely."
            );
            FasterShrines = ConfigFileLGT.Bind(
                "Interactables",
                "Faster Shrines",
                true,
                "Shrines have 1 second of delay instead of 2 seconds."
            );

            cfgHalcyon_NoForcedSots = ConfigFileLGT.Bind(
                "Interactables",
                "Halcyon Shrine | No Forced Sots",
                true,
                "No longer force 2 SotS items, instead have 5 random items."
            );
           cfgHalcyon_FastDrain = ConfigFileLGT.Bind(
                "Interactables",
                "Halcyon Shrine | Faster Drain",
                true,
                "Suck gold at rate of 2 instead of 1. Mostly useful in Singleplayer."
            );
             
            cfgHalcyon_ScaleHPMult = ConfigFileLGT.Bind(
                "Interactables",
                "Halcyon Shrine | Multiply HP",
                false,
                "Multiply health of the Stone Golems (per player), spawned while charging.\nVanilla : True"
            );

            cfgHalcyon_NerfStats = ConfigFileLGT.Bind(
                "Interactables",
                "Halcyon Shrine | Stat Nerf",
                false,
                "Gilded Halcyonite spawned by Halcyon Shrines have 20% less stats. (Up to 28% to rounding)"
            );
 
            VoidSeedsMore = ConfigFileLGT.Bind(
                "Interactables",
                "Void Seed | More Monster",
                true,
                "Void Seeds spawn more monsters. This does still take away from the stages starting spawns."
            );
            VoidCradlesMore = ConfigFileLGT.Bind(
               "Interactables",
               "Void Cradles | Infestors",
               true,
               "Each Void Infestor has a 70% chance to spawn instead of 50%\n\nRare to see them actually have a chance to infest anything."
           );
            cfgVoidTripleAllTier = ConfigFileLGT.Bind(
               "Interactables",
               "Void Potential Chests contain any tier",
               true,
               "Void Potential Chests can contain any item from any tier"
           );
            Shrine_Blood_Gold = ConfigFileLGT.Bind(
                "Interactables",
                "Blood Shrine | Scale Rward",
                true,
                "Blood Shrines reward 25/40/60 Gold scaled with time with a small mult based on HP.\n\nThis results in more money even on Stage 1 and better scaling into late game\n\nNormally, Blood Shrines Reward is only based on HP, quickly making them a bad source of money"
            );
            Shrine_Blood_NoBreak = ConfigFileLGT.Bind(
                "Interactables",
                "Blood Purchases dont break items.",
                true,
                "Blood Purchases such as Shrine of Blood no longer break Elixir, Watch or Unstable Transmitter. Will activate any other low health item."
            );
            Shrine_Combat = ConfigFileLGT.Bind(
                "Interactables",
                "Combat Shrine | HP Scaling",
                false,
                "Multiply HP of enemies spawned (per player)\nVanilla : True"
            );
            Shrine_Healing = ConfigFileLGT.Bind(
              "Interactables",
              "Woods Shrine | Cheaper",
              true,
              "Shrine of Woods cost 10 instead of 25. Go up 1.4x instead of 1.5x per purchase. And allows 4 purchases instead of 3."
            );

            MegaDroneCost = ConfigFileLGT.Bind(
                "Interactables",
                "TC-280 | Cost",
                300,
                "Vanilla is 350"
            );
            BuffMegaDroneStats = ConfigFileLGT.Bind(
                "Interactables",
                "TC-280 | Stat Buff",
                true,
                "Give TC-280 better Regen and Resistence."
            );
          
            TurretDroneCost = ConfigFileLGT.Bind(
                "Interactables",
                "Gunner Turret | Cost",
                25,
                "Vanilla is 35"
            );
            cfgMasstweak = ConfigFileLGT.Bind(
                "Interactables",
                "Pillar of Mass | Tweak",
                true,
                "Charge 20% faster but spawn 20% more monsters."
            );
            InteractableRedSoupAmount = ConfigFileLGT.Bind(
                "Interactables",
                "RedToWhite Cauldron Extra item amount",
                0,
                "This is in addition to the 3 that Vanilla pays out with\nie If you want 5 items total, set it to 2.  Set to 0 to disable."
            );
            InteractableNoLunarCost = ConfigFileLGT.Bind(
                "Interactables",
                "Free Lunars",
                false,
                "Everything that costs Lunar Coins will cost 0"
            );
            #endregion
            #region Stages Changes
            cfgStageCredits_Interactables = ConfigFileLGT.Bind(
              "Stages",
              "Stage Credits adjustments",
              true,
              "Stage 1,2,3 get slightly more monster spawns on start. Titanic Plains & Distant Roost get 240\nTo balance against new Stage 1s have 280 credits or more.\n\nAbandoned Aquaduct gets 240 in Solo\nIn Multiplayer, Scales as 280 credits and then flat -60 for the Band secret. (Better for 3P,4P)\nInstead of scale as 220 which would be -150 credits during 4P for the secret in Vanilla\n\nSirens Call & Sundered Grove get flat +50\nTo balance against Abyssal Depths having +160 flat credits and people generally disliking these stages more."
          );
            cfgStageCredits_Monsters = ConfigFileLGT.Bind(
              "Stages",
              "Stage Starting Monster adjustments",
              true,
              "Most Stage 1,2,3s start with 40% more monster credits, so there's something more to do in the early game."
          );
            ThirdLunarSeer = ConfigFileLGT.Bind(
                "Stages",
                "Bazaar | Third Lunar Seer",
                true,
                "2 Seers was fine when there were 2 stages but we have a lot of stages now"
            );
            RegenArenaCells = ConfigFileLGT.Bind(
              "Stages",
              "Void Fields | Regen",
              true,
              "Cells give regeneration before actvating so you can heal faster."
          );
            FasterArenaCells = ConfigFileLGT.Bind(
               "Stages",
               "Void Fields | Faster Cells",
               true,
               "40s for the first 4, 50s for the next 4, 60s for the last.\n\nShave of 2 minutes of standing."
           );
            cfgVoidStagePillar = ConfigFileLGT.Bind(
                "Stages",
                "Void Locust | Easier Signals",
                true,
                "Charge in 48 seconds instead of 60s (25% faster)\n+30% larger radius"
            );
            /*cfgVoidStagesNoTime = ConfigFileLGT.Bind(
                "Stages",
                "Void Stage Parrity",
                true,
                "Void Locust and Planetarium will increase stage counter and be timed if Void Fields is."
            );*/

            cfgGoldShoresCredits = ConfigFileLGT.Bind(
              "Stages",
              "Gilded Coast spawns interactables",
              true,
              "Not a lot of credits. Shrines of Combat, Chance and Cleansing. Just a little more stuff I guess"
          );

            #endregion

            #region Survivors
            CharactersCaptainKeepInHiddemRealm = ConfigFileLGT.Bind(
               "Survivors",
               "Captain Hidden Realms",
               true,
               "Allow Captain Beacons on all stages. (Except Bazaar)"
           );
            CharactersHuntressLysateCell = ConfigFileLGT.Bind(
              "Survivors",
              "Huntress Ballista Lysate Cell",
              true,
              "With Lysate Cell, use 2 Ballista charges at once if available, for 6 shots."
            );
            CharactersCommandoInvul = ConfigFileLGT.Bind(
              "Survivors",
              "Commando Invulnerable Tactical Dive",
              false,
              "Tactical Dive will make you Invulnerable like in Risk of Rain"
            );
            cfgEquipmentDroneThing = ConfigFileLGT.Bind(
              "Survivors",
              "Equipment Drone AI Change",
              true,
              "Equipment Drones with certain equipment, like Radar Scanner, will use their equipment a lot more often."
            );
            #endregion

            #region DCCS
            cfgDccsMonster = ConfigFileLGT.Bind(
             "Spawnpools",
             "Monsters | Spawn Pools",
             true,
             "Adds and changes some enemy variety. If you wish to turn off changes on a specific stage, check the stage specific config."
            );
            cfgCredits_Monsters = ConfigFileLGT.Bind(
                "Spawnpools",
                "Monsters | Credits Adjustment",
                true,
                "Make some monsters who got nerfed over time cheaper and make Blind Pests rarer\n\nGreater Wisp | 200 -> 180\nGip | 40  -> 20 (Geep costs 40 already)\nParent | 100 -> 80\nVoid Reaver | 300 -> 250 (Worst aim)"
            );


            //
            //DCCS
            cfgDccsInteractables = ConfigFileLGT.Bind(
                "Spawnpools",
                "Interactables | Spawn Pools",
                true,
                "Mountain Shrine on Sulfur Pools\nNo Gunner Turret on Stage 4 & 5\nMili Printers, Large Printers & Cleansing Pools are a bit more common."
            );
            cfgMatchCategory = ConfigFileLGT.Bind(
                "Spawnpools",
                "Interactables | Match Categories",
                MatchCategory.RandomOneSmall,
                "Stages will only have 1 type of Category Chest. Which one will be determined at random."
            );
            cfgCredits_Interactables = ConfigFileLGT.Bind(
                "Spawnpools",
                "Interactables | Credits Adjustment",
                true,
                "TC-280 | 40 -> 35 (Stats buffed in mod)\nEmergency Drone | 30 -> 25\nShrine of Woods | 15 -> 5.\nShrine of Order | 30 -> 5.\nShrine of Combat | 20 -> 15 (Why money so expensive)\nShrine of Blood | 20 -> 15\nShrine of Shaping | 50 -> 40\nLunar Pod | 25 -> 15\nVoid Potential | 40 -> 30 (This is a worse multishop)\nCloaked Chest | 10 -> 2\nOvergrown Printer | 10 -> 15"
            );
          
            /*CategoryChestSots = ConfigFileLGT.Bind(
                "Spawnpools",
                "Large Category Chests on DLC2 Stages",
                true,
                "Seekers of the Storm stages do not have Large Category Chests in the pool for whatever reason"
            );*/
            /*DCCSCategoryChest = ConfigFileUNSORTED.Bind(
                "Spawnpools",
                "Category Chest limitation",
                false,
                "Only one type of Category chest per stage. This feature was cut from the mod, but config for people who still want it."
            );*/

            cfgHalcyon_Spawnpool = ConfigFileLGT.Bind(
                "Spawnpools",
                "Halcyon Shrine | Spawnpool",
                true,
                "Adds a chance to spawn Halcyonites instead of just Stone Golems"
            );
            cfgDccsFamily = ConfigFileLGT.Bind(
               "Spawnpools",
               "Family Events Additions",
               true,
               "Add more family events to some stages\nMost notably Lunar Family event can occur instead of Void Family on some stages."
           );
            FamiliesStage1 = ConfigFileLGT.Bind(
               "Spawnpools",
               "Family Events Stage 1",
               false,
               "Allow most family events to happen on stage 1"
           );

            #endregion
            #region Gamemodes
            cfgE1Unnerf = ConfigFileLGT.Bind(
                         "Eclipse",
                         "Eclipse 1 unnerf Squid/Mask",
                         true,
                         "Squid Polyp and Happiest Mask last the intended 30 seconds, instead of 15 seconds in Eclipse.\n\nThis happens because they use Health as a death timer, and starting at half health makes them last half as long."
                     );
            EclipseAllowVoid = ConfigFileLGT.Bind(
               "Eclipse",
               "Eclipse allow Void Locus",
               true,
               "Allows Void Portals to spawn so you can use Voidling as an alternate final boss in Eclipse. Does not affect the Deep Void Portal after Mithrix of course."
           );
            EclipseAllowTwisted = ConfigFileLGT.Bind(
                "Eclipse",
                "Eclipse allow Moment Whole",
                true,
                "Allows Celestial Portal to spawn so you can use Twisted Scav as an alternate final boss in Eclipse. The obelisk will always take you to Moment Whole even if you do not have Beads."
            );
           
            EclipseAllowArtifactWorld = ConfigFileLGT.Bind(
                "Eclipse",
                "Eclipse allow Bulwarks Ambry",
                true,
                "Allows computer. For artifact"
            );
            EclipseAllowChoiceArtifacts = ConfigFileLGT.Bind(
                "Eclipse",
                "Eclipse Artifacts",
                EclipseArtifact.Blacklist,
                "Allows the choice of often considered challenge or variety artifacts.\n\nBlacklist : All except Artifacts that make the game easier. (Includes Modded ones) (Command, Rebirth)\nAll : All"
            );
            EclipseDifficultyAlways = ConfigFileLGT.Bind(
               "Eclipse",
               "Eclipse 8 in normal lobbies",
               false,
               "Allows you to choose 8 Eclipse in default game mode and Simulacrum"
            );
            EclipseDifficultyAlways.SettingChanged += EclipseDifficultyAlways_SettingChanged;

            #endregion

            /*CharactersVoidFiendEquip = ConfigFileLGT.Bind(
              "Monsters & Survivors",
              "Void Fiend 2 Equipment",
              false,
              "Void Fiend gets two equipment slots switching between them as he switches modes\nMade this as a joke idk how this would be helpful"
            );*/
    
            cfgShrineBossMult = ConfigFileLGT.Bind(
              "Chances",
              "More Mountain Shrine",
              1.2f,
              "Increase the weight of Mountain Shrines. Extra chance is divided by player count as this is mostly meant for solo."
            );
            cfgFamilyChance = ConfigFileLGT.Bind(
                 "Chances",
                 "Family Event Chance",
                 2f,
                 "% chance for family event. Vanilla is 2"
             );
            cfgBossItemChance = ConfigFileLGT.Bind(
                "Chances",
                "Boss Item TP Percent",
                20,
                "Percent chance for a yellow item to replace a green from a teleporter.\nVanilla is 15"
            );
            cfgAspectDropRate = ConfigFileLGT.Bind(
                "Chances",
                "Aspect Drop Rate",
                1600,
                "1 in X drop chance for Elite Aspects. Vanilla is 1 in 4000."
            );
            cfgAspectDropRate.SettingChanged += LittleGameplayTweaks.EquipmentBonusRate;
        }

        public static void EclipseDifficultyAlways_SettingChanged(object sender, System.EventArgs e)
        {
            RuleChoiceDef tempR = RuleCatalog.FindChoiceDef("Difficulty.Eclipse8");
            if (tempR != null)
            {
                tempR.excludeByDefault = !WConfig.EclipseDifficultyAlways.Value;
            }
        }

        public static void Tier2_SettingChanged(object sender, System.EventArgs e)
        {
            if (CombatDirector.eliteTiers.Length > 1)
            {
                var eliteTier = CombatDirector.eliteTiers[Looping.FindTier2Elite()];
                float cost = (cheaperTier2.Value ? 5f : 6f) * CombatDirector.baseEliteCostMultiplier; ;
                float stage = earlyTier2.Value ? 4f : 5f;
                Debug.Log("Tier 2 Elites | AfterStage " + stage + " | Cost" + cost);

                eliteTier.costMultiplier = cost;
                eliteTier.isAvailable = ((SpawnCard.EliteRules rules) => Run.instance.stageClearCount >= stage && rules == SpawnCard.EliteRules.Default);
            }
        }
  

        public static void RiskConfig()
        {

            ModSettingsManager.SetModDescription("Mostly spawn pool tweaks");
            Texture2D icon = LegacyResourcesAPI.Load<Texture2D>("Textures/BodyIcons/GravekeeperBody");

            ModSettingsManager.SetModIcon(Sprite.Create(icon, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f)));


            List<ConfigEntry<bool>> resetB = new List<ConfigEntry<bool>>()
            {
                 cfgDccsMonster,
                 cfgDccsFamily,
                 FamiliesStage1,
                 cfgDccsInteractables,
                 cfgCredits_Monsters,
                 cfgCredits_Interactables,
                 //cfgVoidStagesNoTime,
                 FasterScrapper,
                 FasterShrines,
                 InteractableNoLunarCost,
                 Shrine_Healing, //Could be removed
                 Shrine_Combat,
                 cfgHalcyon_Spawnpool,
                 CharactersCaptainKeepInHiddemRealm,
                 CharactersCommandoInvul, //Change?
                 BuffMegaDroneStats,
                 cfgMendingCoreBuff,
                 cfgElderLemurianBands,
                 cfgTwistedBuff,
                 cfgTwistedBuffEpic,
                 cfgBrotherHurtFix,
                 cfgXIEliteFix,
                 cfgVoidStagePillar,
                VoidSeedsMore,
                 VoidCradlesMore,
                 cfgE1Unnerf,
                 cfgVoidTripleAllTier,
            };
            List<ConfigEntry<int>> resetI = new List<ConfigEntry<int>>()
            {
                MegaDroneCost,
                TurretDroneCost,
            };
        
            var entries = ConfigFileLGT.GetConfigEntries();
            Debug.Log("Config Values Total : " + entries.Length);
            Debug.Log("Config Values Reset : " + (resetB.Count + resetI.Count));
            foreach (ConfigEntryBase entry in entries)
            {
                if (entry.SettingType == typeof(bool))
                {
                    var temp = (ConfigEntry<bool>)entry;
                    ModSettingsManager.AddOption(new CheckBoxOption(temp, resetB.Contains(temp)));
                }
                else if (entry.SettingType == typeof(int))
                {
                    var temp = (ConfigEntry<int>)entry;
                    ModSettingsManager.AddOption(new IntFieldOption(temp, resetI.Contains(temp)));
                }
                else if (entry.SettingType == typeof(float))
                {
                    ModSettingsManager.AddOption(new FloatFieldOption((ConfigEntry<float>)entry, false));
                }
                else if (entry.SettingType == typeof(EclipseArtifact))
                {
                    ModSettingsManager.AddOption(new ChoiceOption((ConfigEntry<EclipseArtifact>)entry, false));
                }
                else if (entry.SettingType == typeof(EclipseDifficulty))
                {
                    ModSettingsManager.AddOption(new ChoiceOption((ConfigEntry<EclipseDifficulty>)entry, false));
                }
                else if (entry.SettingType == typeof(MatchCategory))
                {
                    ModSettingsManager.AddOption(new ChoiceOption((ConfigEntry<MatchCategory>)entry, true));
                }
                else
                {
                    Debug.LogWarning("Could not add config " + entry.Definition.Key + " of type : " + entry.SettingType);
                }
            }
            return;

        }
    }
}