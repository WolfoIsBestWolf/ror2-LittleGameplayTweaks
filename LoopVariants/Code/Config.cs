using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using RiskOfOptions.OptionConfigs;
using UnityEngine;

namespace LoopVariants
{
    public class WConfig
    {
        public static ConfigFile ConfigFileSTAGES = new ConfigFile(Paths.ConfigPath + "\\Wolfo.LoopVariants.cfg", true);

        public static ConfigEntry<bool> cfgLoopWeather;

        public static ConfigEntry<bool> cfgGameplayChanges;

        public static ConfigEntry<float> Chance_PreLoop;
        public static ConfigEntry<float> Chance_Loop;
        public static ConfigEntry<float> Chance_Loop_2;

        public static ConfigEntry<bool> LoopVariant_OnPreLoop_Vanilla;
        public static ConfigEntry<bool> PreLoopVariant_PostLoop_Vanilla;

        public static ConfigEntry<bool> MultiplayerTesting;


        public static void InitConfig()
        {
            cfgLoopWeather = ConfigFileSTAGES.Bind(
                "!Main",
                "Loop Weather for some more stages",
                true,
                "Disable the entire mod if false"
            );
            cfgGameplayChanges = ConfigFileSTAGES.Bind(
                "!Main",
                "Enable Gameplay changes for Loop Variants",
                true,
                "Does not include new gameobjects that might block a path.\nSome changes will not activate if you are already on the stage.\nIf you plan on playing with people that do not have this mod it's better to disable this.\n\nAquaduct : Slowing Tar\nSulfur Pools : Weaker but Lethal Helfire Pods\nSundered Grove : Healing Fruits"
            );
            Chance_PreLoop = ConfigFileSTAGES.Bind(
                "Chances",
                "Loop Weather chance pre-loop",
                0f,
                "% Chance for a loop weather to happen pre-loop\n(Stages 1 - 5)"
            );
            Chance_Loop = ConfigFileSTAGES.Bind(
                "Chances",
                "Loop Weather chance post-loop",
                100f,
                "% Chance for a loop weather to happen loop 1\n(Stages 6 - 10)"
            );
            Chance_Loop_2 = ConfigFileSTAGES.Bind(
                "Chances",
                "Loop Weather chance consecutive-loop",
                -1f,
                "% Chance for a loop weather to happen loop 2+\n(Stage 11+)\n-1 will just use loop 1 Chance instead"
            );
            LoopVariant_OnPreLoop_Vanilla = ConfigFileSTAGES.Bind(
                "Chances",
                "Apply pre loop chance to official stage variants",
                true,
                "Should stage 1 Viscous Falls be allowed if pre-loop chance succeeds."
            );
            PreLoopVariant_PostLoop_Vanilla = ConfigFileSTAGES.Bind(
                "Chances",
                "Apply loop chance to official stage variants",
                true,
                "Should stage 6 Shattered Abodes be allowed if post-loop chance fails."
            );
            MultiplayerTesting = ConfigFileSTAGES.Bind(
                "Testing",
                "Multiplayer Testing",
                false,
                "Removes kicking from lobbies so you can join yourself with 2 game instances on the same account."
            );

            InitConfigStages();
            RiskConfig();
        }

        public static void RiskConfig()
        {
            Texture2D TexChestCasinoIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            TexChestCasinoIcon.LoadImage(Properties.Resources.icon, true);
            TexChestCasinoIcon.filterMode = FilterMode.Bilinear;
            Sprite ChestCasinoIcon = Sprite.Create(TexChestCasinoIcon, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));
            ModSettingsManager.SetModIcon(ChestCasinoIcon);

            ModSettingsManager.SetModDescription("Diet Loop Weather Variants for more stages.");
          
            CheckBoxConfig overwriteName = new CheckBoxConfig
            {
                name = "Disable Entire Mod",
                restartRequired = true,
            };
            ModSettingsManager.AddOption(new CheckBoxOption(cfgLoopWeather, overwriteName));

            overwriteName = new CheckBoxConfig
            {
                name = "Gameplay Changes",
            };
            ModSettingsManager.AddOption(new CheckBoxOption(cfgGameplayChanges, overwriteName));

           
            FloatFieldConfig overwriteName2 = new FloatFieldConfig
            {
                name = "Chance pre-loop",
            };
            ModSettingsManager.AddOption(new FloatFieldOption(Chance_PreLoop, overwriteName2));
            overwriteName2 = new FloatFieldConfig
            {
                name = "Chance post-loop",
            };
            ModSettingsManager.AddOption(new FloatFieldOption(Chance_Loop, overwriteName2));
            overwriteName2 = new FloatFieldConfig
            {
                name = "Chance consecutive-loop",
            };
            ModSettingsManager.AddOption(new FloatFieldOption(Chance_Loop_2, overwriteName2));



            overwriteName = new CheckBoxConfig
            {
                name = "Chance Based Official Variants Pre-loop",
            };
            ModSettingsManager.AddOption(new CheckBoxOption(LoopVariant_OnPreLoop_Vanilla, overwriteName));
            overwriteName = new CheckBoxConfig
            {
                name = "Chance Based Official Variants Post-loop",
            };
            ModSettingsManager.AddOption(new CheckBoxOption(PreLoopVariant_PostLoop_Vanilla, overwriteName));


            ModSettingsManager.AddOption(new CheckBoxOption(Stage_1_Golem));
            //ModSettingsManager.AddOption(new CheckBoxOption(Stage_1_Roost));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_1_Snow));
            overwriteName = new CheckBoxConfig
            {
                name = "Friendly Viscious/Disturbed SpawnPool",
                restartRequired = true,
            };
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_1_LakesVillageNight, overwriteName));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Goolake));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Goolake_River));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Swamp));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Temple));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Wisp));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Sulfur));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Sulfur_Hellfire));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Sulfur_ExtraLights));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_4_Damp_Abyss));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_4_Root_Jungle));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_4_Root_Jungle_Fruit));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_5_Helminth));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_6_Meridian));

        }


        public static ConfigEntry<bool> Stage_1_Golem;
        public static ConfigEntry<bool> Stage_1_Roost;
        public static ConfigEntry<bool> Stage_1_Snow;
        public static ConfigEntry<bool> Stage_1_LakesVillageNight;

        public static ConfigEntry<bool> Stage_2_Goolake;
        public static ConfigEntry<bool> Stage_2_Goolake_River;
        public static ConfigEntry<bool> Stage_2_Goolake_Elders;
        public static ConfigEntry<bool> Stage_2_Swamp;
        public static ConfigEntry<bool> Stage_2_Ancient;
        public static ConfigEntry<bool> Stage_2_Temple;

        public static ConfigEntry<bool> Stage_3_Frozen;
        public static ConfigEntry<bool> Stage_3_Wisp;
        public static ConfigEntry<bool> Stage_3_Sulfur;
        public static ConfigEntry<bool> Stage_3_Sulfur_Hellfire;
        public static ConfigEntry<bool> Stage_3_Sulfur_ExtraLights;

        public static ConfigEntry<bool> Stage_4_Damp_Abyss;
        public static ConfigEntry<bool> Stage_4_Ship;
        public static ConfigEntry<bool> Stage_4_Root_Jungle;
        public static ConfigEntry<bool> Stage_4_Root_Jungle_Fruit;

        public static ConfigEntry<bool> Stage_5_Sky;
        public static ConfigEntry<bool> Stage_5_Helminth;

        public static ConfigEntry<bool> Stage_6_Commencement;
        public static ConfigEntry<bool> Stage_6_Meridian;



        public static void InitConfigStages()
        {

            Stage_1_Golem = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Golem Plains",
                true,
                "Enable alt weather for this stage"
            );
            /*Stage_1_Roost = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Distant Roost",
                true,
                "Enable alt weather for this stage"
            );*/
            Stage_1_Snow = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Snowy Forest",
                true,
                "Enable alt weather for this stage"
            );
            Stage_1_LakesVillageNight = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Viscious Falls & Disturbed Impact - Spawn Pool Nerf",
                true,
                "Make spawn pools more Stage 1 friendly.\nDisables Elder Lemurian, Void Reavers from spawning here Stage 1."
            );
            Stage_2_Goolake = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aquaduct",
                true,
                "Enable alt weather for this stage"
            );
            Stage_2_Goolake_River = ConfigFileSTAGES.Bind(
                 "Stage 2",
                 "Aquaduct - River of Tar",
                 true,
                 "Enable the Tar River in the alt of this stage"
             );
            Stage_2_Swamp = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Wetlands",
                true,
                "Enable alt weather for this stage"
            );
            Stage_2_Temple = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Reformed Altar",
                true,
                "Enable alt weather for this stage"
            );
            /*Stage_2_Ancient = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aphelian Sanctuary",
                true,
                "Enable alt weather for this stage"
            );
            Stage_2_Temple = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Reformed Altar",
                true,
                "Enable alt weather for this stage"
            );
            Stage_3_Frozen = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Rally Point",
                true,
                "Enable alt weather for this stage"
            );*/
            Stage_3_Wisp = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Scorched Acres",
                true,
                "Enable alt weather for this stage"
            );
            Stage_3_Sulfur = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool",
                true,
                "Enable alt weather for this stage"
            );
            Stage_3_Sulfur_Hellfire = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool : Helfire",
                true,
                "Should Sulfur Pods in alt weather do less overall damage but also add lethal helfire."
            );
            Stage_3_Sulfur_ExtraLights = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool : Reduce Lights",
                false,
                "Reduce Light amount on this stage. This might help optimization"
            );

            Stage_4_Damp_Abyss = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Abyssal Depths",
                true,
                "Enable alt weather for this stage"
            );
            /*Stage_4_Ship = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sirens Call",
                true,
                "Enable alt weather for this stage"
            );*/
            Stage_4_Root_Jungle = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sundered Grove",
                true,
                "Enable alt weather for this stage"
            );
            Stage_4_Root_Jungle_Fruit = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sundered Grove - Healing Fruit",
                true,
                "Spawn 30-40 Healing Fruits like the Healing Fruit in Treeborn or Eggs in Sirens Call"
            );
            /*Stage_5_Sky = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Sky Meadow",
                true,
                "Enable alt weather for this stage"
            );*/
            Stage_5_Helminth = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Helminth",
                true,
                "Enable alt weather for this stage"
            );
            /*Stage_6_Commencement = ConfigFileSTAGES.Bind(
                "Stage Final",
                "Commencement",
                true,
                "Enable alt weather for this stage"
            );*/
            Stage_6_Meridian = ConfigFileSTAGES.Bind(
                "Stage Final",
                "Prime Meridian",
                true,
                "Change trees to Golden Dieback coloration on loops"
            );
        }

    }
}