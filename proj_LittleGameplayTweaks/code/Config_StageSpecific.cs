using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
        public static ConfigEntry<bool> Stage_F_Moon;
        public static ConfigEntry<bool> Stage_F_Meridian;
        public static ConfigEntry<bool> Stage_X_Arena_Void;
        public static ConfigEntry<bool> Stage_X_Void_Seed;
        public static ConfigEntry<bool> Stage_X_GoldShores;
        public static ConfigEntry<bool> Stage_X_HalcShrine;


        public static void InitConfig()
        {
            Stage_1_Golem = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Golem Plains",
                true,
                "Loop TC280"
            );
            Stage_1_Roost = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Distant Roost",
                true,
                "Adds Loop Vultures\n\nAdds Loop Cleansing Pool"
            );
            Stage_1_Snow = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Snowy Forest",
                true,
                "Removes Greater Wisp\nAdds Bison\nBlind Vermin now pre-loop\nBlind enemies slightly rarer\nAdds Loop Imp & Imp Overlord\n\nAdds Loop TC-280\nAdds rare Shrine of Order"
            );
            Stage_1_Lake = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Verdan Falls",
                true,
                "Adds Jellyfish\nAdds Loop Solus Control Unit\nAdds Loop Vulture\n\nMakes Cleansing Pool rarer\nAdds Large Healing Chests\nAdds Large Utility Chests"
            );
            Stage_1_Village = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Shattered Abodes",
                true,
                "Adds Halcyonite\nAdds Loop Lunar Chimera\nAdds Loop Alloy Vulture\n\nAdds Shrine of Order\nAdds Loop Cleansing Pool\nAdds Loop TC-280\nAdds Large Damage Chest\nAdds Large Utility chest"
            );
            Stage_2_Goolake = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aquaduct",
                true,
                "Adds Clay Apothecary\nAdds Loop Hermit Crab"
            );
            Stage_2_Swamp = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Wetlands",
                true,
                "Adds Loop Acid Larva\nAdds Loop Mushroom\n\n"
            );
            Stage_2_Ancient = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aphelian Sanctuary",
                true,
                "Adds Loop Elder Lemurian\nAdds Loop Grovetender\n\nAdds rare Shrine of Order"
            );
            Stage_2_Temple = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Reformed Altar",
                true,
                "Adds Grovetender\nAdds Loop Bison\nAdds Loop Brass Contrapion\nRemoves Blind Pest\n\nAdds Shrine of Order\nAdds all 3 Large Category Chests"
            );
            Stage_3_Frozen = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Rally Point",
                true,
                "Adds Solus Control Unit\nRemoves Clay Dunestrider\nAdds Loop Solus Probe\nAdds Loop Vulture"
            );
            Stage_3_Wisp = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Scorched Acres",
                true,
                "Adds Clay Apothecary\nAdds Loop Blind Vermin"
            );
            Stage_3_Sulfur = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool",
                true,
                "Adds Magma Worm\nAdds Overloading Worm\nAdds Hermit Crab\nAdds Acid Larva\nAdds Loop Grandparent\nAdds Loop Parent\n\nAdds Mountain Shrine\nAdds Emergency Drone\n\nStage has +20 credits"
            );
            Stage_3_Tree = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Treeborn",
                true,
                "Adds Blind Pest\nAdds Loop Mushroom\n\nAdds Large Damage Chest\nAdds Large Healing Chest"
            );
            Stage_4_Damp_Abyss = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Abyssal Depths",
                true,
                "Adds Loop Parent\nAdds Loop Grandparent\nAdds Loop Void Jailer\n"
            );
            Stage_4_Ship = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sirens Call",
                true,
                "Adds Overloading Worm\nAdds Loop Solus Probe\nAdds Loop Alpha Construct\nAdds Loop XI Construct\n\nAdds TC280\nMore common Equipment Drone"
            );
            Stage_4_Root_Jungle = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sundered Grove",
                true,
                "Adds Grovetender\nRemoves Stone Titan\nRemoves Loop Halcyonite\nAdds Loop Blind Pest\nAdds Loop Void Jailer\n"
            );
            Stage_5_Sky = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Sky Meadow",
                true,
                "Adds Loop Lunar Chimera\nAdds Loop Void Devestator\n"
            );
            Stage_5_Helminth = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Helminth",
                true,
                "Adds Halcyonite\nRemoves Greater Wisp\nAdds Grandparent\nAdds Hermit Crabs\nAdds all 3 Large Category Chests"
            );
            Stage_F_Moon = ConfigFileSTAGES.Bind(
                 "Stage Other",
                 "Commencement",
                 true,
                 "Allows all Void enemies to spawn during the escape sequence."
             );
            Stage_F_Meridian = ConfigFileSTAGES.Bind(
                 "Stage Other",
                 "Prime Meridian",
                 true,
                 "Adds Loop Lunar Chimera"
             );
            Stage_X_Arena_Void = ConfigFileSTAGES.Bind(
                "Stage Other",
                "Void Fields",
                true,
                ""
            );
            Stage_X_GoldShores = ConfigFileSTAGES.Bind(
                "Stage Other",
                "Gilded Coast",
                true,
                "Adds Grandparents\nAdds Parents\nAdds Grovetenders\nAdds Brass Contraptions"
            );

        }
        public static void RiskConfig()
        {

            ModSettingsManager.SetModDescription("Stage specific config for DCCS / Spawnpool / Stage changes added by LittleGameplayTweaks", "LGT_SpawnPools", "Spawnpools");
            Texture2D icon = Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/Base/ArtifactShell/texUnidentifiedKillerIcon.png").WaitForCompletion();
            ModSettingsManager.SetModIcon(Sprite.Create(icon, new Rect(0, 0, 128, 128), new Vector2(-0.5f, -0.5f)), "LGT_SpawnPools", "Spawnpools");


            var entries = ConfigFileSTAGES.GetConfigEntries();
            foreach (ConfigEntryBase entry in entries)
            {
                ModSettingsManager.AddOption(new CheckBoxOption((ConfigEntry<bool>)entry, true), "LGT_SpawnPools", "Spawnpools");

            }
        }
    }
}