using BepInEx;
using BepInEx.Configuration;

namespace LoopVariants
{
    public class WConfig
    {
        public static ConfigFile ConfigFileUNSORTED = new ConfigFile(Paths.ConfigPath + "\\Wolfo.LoopVariants.cfg", true);

        public static ConfigEntry<bool> cfgLoopWeather;

        public static ConfigEntry<float> Chance_PreLoop;
        public static ConfigEntry<float> Chance_Loop;

        public static ConfigEntry<bool> ApplyPreLoopToVanilla;
        public static ConfigEntry<bool> ApplyLoopToVanilla;


        public static void InitConfig()
        {
            cfgLoopWeather = ConfigFileUNSORTED.Bind(
                "!Main",
                "Loop Weather for some more stages",
                true,
                "Disable the entire mod if false"
            );
            Chance_PreLoop = ConfigFileUNSORTED.Bind(
                "Chances",
                "Loop Weather chance pre-loop",
                0f,
                "Chance for a loop weather to happen pre-loop"
            );
            Chance_Loop = ConfigFileUNSORTED.Bind(
                "Chances",
                "Loop Weather chance pre-loop",
                100f,
                "Chance for a loop weather to happen ploop"
            );
            ApplyPreLoopToVanilla = ConfigFileUNSORTED.Bind(
                "Chances",
                "Should apply pre loop chance to official stage variants",
                true,
                ""
            );

        }

    }
}