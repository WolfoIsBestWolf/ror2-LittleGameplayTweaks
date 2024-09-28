using BepInEx;
using BepInEx.Configuration;

namespace LoopVariants
{
    public class WConfig
    {
        public static ConfigFile ConfigFileSTAGES = new ConfigFile(Paths.ConfigPath + "\\Wolfo.LoopVariants.cfg", true);

        public static ConfigEntry<bool> cfgLoopWeather;

        public static ConfigEntry<float> Chance_PreLoop;
        public static ConfigEntry<float> Chance_Loop;
 
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
            Chance_PreLoop = ConfigFileSTAGES.Bind(
                "Chances",
                "Loop Weather chance pre-loop",
                0f,
                "% Chance for a loop weather to happen pre-loop"
            );
            Chance_Loop = ConfigFileSTAGES.Bind(
                "Chances",
                "Loop Weather chance post-loop",
                100f,
                "% Chance for a loop weather to happen post-loop"
            );
            LoopVariant_OnPreLoop_Vanilla = ConfigFileSTAGES.Bind(
                "Chances",
                "Apply pre loop chance to official stage variants",
                true,
                "ie should stage 1 Viscous Falls be allowed if pre-loop chance succeeds."
            );
            PreLoopVariant_PostLoop_Vanilla = ConfigFileSTAGES.Bind(
                "Chances",
                "Apply loop chance to official stage variants",
                true,
                "ie should Shattered Abodes be allowed on loop if the chance says to not use loop weather"
            );
            MultiplayerTesting = ConfigFileSTAGES.Bind(
                "Testing",
                "Multiplayer Testing",
                false,
                "Removes kicking from lobbies so you can join yourself with 2 game instances on the same account."
            );

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

        public static ConfigEntry<bool> Stage_4_Damp_Abyss;
        public static ConfigEntry<bool> Stage_4_Ship;
        public static ConfigEntry<bool> Stage_4_Root_Jungle;

        public static ConfigEntry<bool> Stage_5_Sky;
        public static ConfigEntry<bool> Stage_5_Helminth;
        public static ConfigEntry<bool> Stage_6_Commencement;

        

        public static void InitConfigStages()
        {
 ;

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
                "Disable Elder Lemurians from spawning here Stage 1. Will still spawn on loops"
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
                "Stage 6",
                "Commencement",
                true,
                "Enable alt weather for this stage"
            );*/

        }

    }
}