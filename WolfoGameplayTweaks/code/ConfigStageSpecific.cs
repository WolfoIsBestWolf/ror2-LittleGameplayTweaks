using BepInEx;
using BepInEx.Configuration;

namespace LittleGameplayTweaks
{
    public class ConfigStages
	{
        public static ConfigFile ConfigFileSTAGES = new ConfigFile(Paths.ConfigPath + "\\Wolfo.Little_Gameplay_Tweaks.Stage_Specific_SpawnPool_Disable.cfg", true);

        public static ConfigEntry<bool> Stage_1_Golem;
        public static ConfigEntry<bool> Stage_1_Roost;
        public static ConfigEntry<bool> Stage_1_Snow;
        public static ConfigEntry<bool> Stage_1_Lake;
        public static ConfigEntry<bool> Stage_1_Village;
        public static ConfigEntry<bool> Stage_2_Goolake;
        public static ConfigEntry<bool> Stage_2_Swamp;
        public static ConfigEntry<bool> Stage_2_Ancient;
        public static ConfigEntry<bool> Stage_2_Temple;
        public static ConfigEntry<bool> Stage_3_Frozen;
        public static ConfigEntry<bool> Stage_3_Wisp;
        public static ConfigEntry<bool> Stage_3_Sulfur;
        public static ConfigEntry<bool> Stage_3_Tree;
        public static ConfigEntry<bool> Stage_4_Damp_Abyss;
        public static ConfigEntry<bool> Stage_4_Ship;
        public static ConfigEntry<bool> Stage_4_Root_Jungle;
        public static ConfigEntry<bool> Stage_5_Sky;
        public static ConfigEntry<bool> Stage_5_Helminth;
        public static ConfigEntry<bool> Stage_X_Arena_Void;


        public static void InitConfig()
        {
            UnityEngine.Debug.LogWarning(ConfigFileSTAGES);

            Stage_1_Golem = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Golem Plains",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_1_Roost = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Distant Roost",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_1_Snow = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Snowy Forest",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_1_Lake = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Verdan Falls",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_1_Village = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Shattered Abodes",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_2_Goolake = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aquaduct",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_2_Swamp = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Wetlands",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_2_Ancient = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aphelian Sanctuary",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_2_Temple = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Reformed Altar",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_3_Frozen = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Rally Point",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_3_Wisp = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Scorched Acres",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_3_Sulfur = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_3_Tree = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Treeborn",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_4_Damp_Abyss = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Abyssal Depths",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_4_Ship = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sirens Call",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_4_Root_Jungle = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sundered Grove",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_5_Sky = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Sky Meadow",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_5_Helminth = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Helminth",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );
            Stage_X_Arena_Void = ConfigFileSTAGES.Bind(
                "Stage X",
                "Void Fields",
                true,
                "Stage specific changes as described on the mod pages wiki section"
            );

        }

    }
}