using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using UnityEngine;

namespace LoopVariants
{
    public static class WConfig
    {
        public static ConfigFile ConfigFileSTAGES = new ConfigFile(Paths.ConfigPath + "\\Wolfo.LoopVariants.cfg", true);

        public static ConfigEntry<bool> cfgGameplayChanges;
        public static ConfigEntry<EnumEnemyAdds> Monster_Additions;
        public static ConfigEntry<bool> Name_Changes;

        public static ConfigEntry<float> Chance_PreLoop;
        public static ConfigEntry<float> Chance_Loop;
        public static ConfigEntry<float> Chance_Loop_2;
        public static ConfigEntry<bool> Alternate_Chances;

        public static ConfigEntry<bool> LoopVariant_OnPreLoop_Vanilla;
        public static ConfigEntry<bool> PreLoopVariant_PostLoop_Vanilla;



        public enum EnumEnemyAdds
        {
            Never,
            LittleGameplayTweaks,
            Always
        }


        public static void InitConfig()
        {

            cfgGameplayChanges = ConfigFileSTAGES.Bind(
                "!Main",
                "Enable Gameplay changes for Loop Variants",
                true,
                "Disabled automatically if you play with people who don't have the mod.\nDoes not include new gameobjects that might block a path.\nSome changes will not activate if you are already on the stage.\nIf you plan on playing with people that do not have this mod it's better to disable this.\n\nAquaduct : Slowing Tar\nSulfur Pools : Weaker but Lethal Helfire Pods\nSundered Grove : Healing Fruits"
            );
            Monster_Additions = ConfigFileSTAGES.Bind(
               "!Main",
               "Add monsters to weather variants",
               EnumEnemyAdds.LittleGameplayTweaks,
               "Add additional monsters to the spawn pool of variants.\n0 : Disable Enemy Additions entirely.\n1 : Enable if LittleGameplayTweaks mod is installed.\n2 : Always Enable."
           );
            Monster_Additions.SettingChanged += Monster_Additions_SettingChanged;
            Name_Changes = ConfigFileSTAGES.Bind(
               "!Main",
               "Name Changes",
               true,
               "Should variants display a different name like official variants."
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
                "Loop Weather chance consecutive-loops",
                50f,
                "% Chance for a loop weather to happen loop 2+\n(Stage 11+)\n-1 will just use loop 1 Chance instead"
            );
            Alternate_Chances = ConfigFileSTAGES.Bind(
                "Chances",
                "Alternate Chances between loops",
                false,
                "Weather sets alternate between loops.\nGo standard, loop, standard, loop.\nIgnores consecutive loop setting."
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

            WIP = ConfigFileSTAGES.Bind(
                "Testing",
                "Wip",
                false,
                "Not much to see"
            );
            InitConfigStages();
            RiskConfig();
        }

        public static void Monster_Additions_SettingChanged(object sender, System.EventArgs e)
        {
            if (Monster_Additions.Value == WConfig.EnumEnemyAdds.Always)
            {
                LoopVariantsMain.AddMonsters = true;
            }
            else if (Monster_Additions.Value == WConfig.EnumEnemyAdds.LittleGameplayTweaks)
            {
                LoopVariantsMain.AddMonsters = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Wolfo.LittleGameplayTweaks");
            }
            else if (Monster_Additions.Value == WConfig.EnumEnemyAdds.Never)
            {
                LoopVariantsMain.AddMonsters = false;
            }
            Debug.Log("Variant Monsters : " + LoopVariantsMain.AddMonsters);
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
                name = "Gameplay Changes",
            };

            ModSettingsManager.AddOption(new CheckBoxOption(cfgGameplayChanges, overwriteName));
            var overwriteName3 = new ChoiceConfig
            {
                name = "Add monsters to         weather variants",
            };
            ModSettingsManager.AddOption(new ChoiceOption(Monster_Additions, overwriteName3));
            ModSettingsManager.AddOption(new CheckBoxOption(Name_Changes));


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

            ModSettingsManager.AddOption(new CheckBoxOption(Alternate_Chances));


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
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_1_Snow));

            overwriteName = new CheckBoxConfig
            {
                name = "Friendly Viscious/Disturbed SpawnPool",
                restartRequired = true,
            };
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_1_LakesVillageNight, overwriteName));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_1_VillageNight_Credits));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Goolake));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Goolake_River));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Swamp));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Ancient));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_2_Ancient));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Temple));


            ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Wisp));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_3_Wisp));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Sulfur));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Sulfur_Hellfire));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Sulfur_ExtraLights));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_4_Damp_Abyss));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_4_Damp_Abyss));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_4_Root_Jungle));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_4_Root_Jungle));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_4_Root_Jungle_Fruit));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_5_Helminth));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_5_Helminth));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_6_Meridian));

        }


        public static ConfigEntry<bool> Stage_1_Golem;
        public static ConfigEntry<bool> Stage_1_Roost;
        public static ConfigEntry<bool> Stage_1_Snow;
        public static ConfigEntry<bool> Stage_1_LakesVillageNight;
        public static ConfigEntry<bool> Stage_1_LakesNight;
        public static ConfigEntry<bool> Stage_1_VillageNight;
        public static ConfigEntry<bool> Stage_1_VillageNight_Credits;

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
        public static ConfigEntry<bool> WIP;


        public static ConfigEntry<bool> Enemy_1_Golem;
        public static ConfigEntry<bool> Enemy_1_Roost;
        public static ConfigEntry<bool> Enemy_1_Snow;
        public static ConfigEntry<bool> Enemy_1_Lakes;
        public static ConfigEntry<bool> Enemy_1_Village;

        public static ConfigEntry<bool> Enemy_2_Goolake;
        public static ConfigEntry<bool> Enemy_2_Swamp;
        public static ConfigEntry<bool> Enemy_2_Ancient;
        public static ConfigEntry<bool> Enemy_2_Temple;

        public static ConfigEntry<bool> Enemy_3_Frozen;
        public static ConfigEntry<bool> Enemy_3_Wisp;
        public static ConfigEntry<bool> Enemy_3_Sulfur;

        public static ConfigEntry<bool> Enemy_4_Damp_Abyss;
        public static ConfigEntry<bool> Enemy_4_Ship;
        public static ConfigEntry<bool> Enemy_4_Root_Jungle;

        public static ConfigEntry<bool> Enemy_5_Sky;
        public static ConfigEntry<bool> Enemy_5_Helminth;

        public static ConfigEntry<bool> Enemy_6_Commencement;
        public static ConfigEntry<bool> Enemy_6_Meridian;



        public static void InitConfigStages()
        {

            Stage_1_Golem = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Titanic Plains",
                true,
                "Enable alt weather for this stage. Sunset Plains"
            );
            Stage_1_Roost = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Distant Roost",
                true,
                "Enable alt weather for this stage. Not implemented"
            );
            Stage_1_Snow = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Siphoned Forest",
                true,
                "Enable alt weather for this stage. Night Time Aurora Borealis"
            );
            Enemy_1_Snow = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Siphoned Forest : Mobs",
                true,
                "Add mobs to Variant : Greater Wisps"
            );
            Stage_1_LakesVillageNight = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Viscious Falls & Disturbed Impact - Spawn Pool Nerf",
                true,
                "Make spawn pools more Stage 1 friendly.\nDisables Elder Lemurian, Void Reavers from spawning here Stage 1."
            );
            Stage_1_VillageNight_Credits = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Disturbed Impact : Credits Nerf",
                true,
                "Disturbed Impact has 310 Credits, the highest of any stage 1. This nerfs it to 280 pre loop."
            );
            Stage_2_Goolake = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Abandoned Aquaduct",
                true,
                "Enable alt weather for this stage. Tar Filled, Green sick-ish feeling"
            );
            Stage_2_Goolake_River = ConfigFileSTAGES.Bind(
                 "Stage 2",
                 "Abandoned Aquaduct - River of Tar",
                 true,
                 "Enable the Tar River in the alt of this stage"
             );
            Stage_2_Swamp = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Wetland Aspect",
                true,
                "Enable alt weather for this stage. Foggy and Rainy"
            );
            Stage_2_Ancient = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aphelian Sanctuary",
                true,
                "Enable alt weather for this stage. Night Time/Eclipsed Sun"
            );
            Enemy_2_Ancient = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aphelian Sanctuary : Mobs",
                true,
                "Add mobs to Variant : Lunar Exploders always, Lunar Golem and Wisp on loops."
            );
            Stage_2_Temple = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Reformed Altar",
                true,
                "Enable alt weather for this stage : Golden with Dieback leaves"
            );
            Stage_3_Frozen = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Rallypoint Delta",
                true,
                "Enable alt weather for this stage Not Implemented"
            );
            Stage_3_Wisp = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Scorched Acres",
                true,
                "Enable alt weather for this stage : Dusk"
            );
            Enemy_3_Wisp = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Scorched Acres : Mobs",
                true,
                "Add mobs to Variant : Child"
            );
            Stage_3_Sulfur = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool",
                true,
                "Enable alt weather for this stage: Blue Lava"
            );
            Stage_3_Sulfur_Hellfire = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool : Helfire",
                false,
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
                "Enable alt weather for this stage : More Red, vaguely Imp themed"
            );
            Enemy_4_Damp_Abyss = ConfigFileSTAGES.Bind(
               "Stage 4",
               "Abyssal Depths : Mobs",
               true,
               "Add mobs to Variant : Void Reavers/Barnacles/Imps always, Void Jailer/Devestator/Imp Overlords post-loop"
           );
            Stage_4_Ship = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sirens Call",
                true,
                "Enable alt weather for this stage / Not Implemented"
            );
            Stage_4_Root_Jungle = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sundered Grove",
                true,
                "Enable alt weather for this stage"
            );
            Enemy_4_Root_Jungle = ConfigFileSTAGES.Bind(
               "Stage 4",
               "Sundered Grove : Mobs",
               true,
               "Add mobs to Variant : Geep & Gip"
           );
            Stage_4_Root_Jungle_Fruit = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sundered Grove - Healing Fruit",
                true,
                "Spawn 30-40 Healing Fruits like the Healing Fruit in Treeborn or Eggs in Sirens Call"
            );
            Stage_5_Sky = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Sky Meadow",
                true,
                "Enable alt weather for this stage. Not Implemented"
            );
            Stage_5_Helminth = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Helminth Hatchery",
                true,
                "Enable alt weather for this stage"
            );
            Enemy_5_Helminth = ConfigFileSTAGES.Bind(
              "Stage 5",
              "Helminth Hatchery : Mobs",
              true,
              "Add mobs to Variant : Halcyonite"
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