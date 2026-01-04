using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using UnityEngine;

namespace LittleGameplayFeatures
{
    public class WConfig
    {
        public static ConfigFile ConfigFileLTG = new ConfigFile(Paths.ConfigPath + "\\Wolfo.LittleGameplayFeatures.cfg", true);

        public static ConfigEntry<bool> cfgScavNewTwisted;
        public static ConfigEntry<bool> cfgRedMultiShop;
        public static ConfigEntry<bool> cfgNewFamilies;
        public static ConfigEntry<bool> cfgPrismRun;
        public static ConfigEntry<bool> cfgPrismTimerAlwaysRun;


        public static void InitConfig()
        {

            cfgPrismTimerAlwaysRun = ConfigFileLTG.Bind(
               "Prismatic",
               "Prismatic | Always run timer",
               true,
               "Should the timer always run, like during Prismatic Trials,\nor stop during certain stages."
            );


            cfgPrismRun = ConfigFileLTG.Bind(
                  "Content",
                  "Prismatic Run Gamemode",
                  true,
                  "Prismatic Trial but as a whole run."
            );
            cfgPrismRun.SettingChanged += CfgPrismRun_SettingChanged;
            cfgScavNewTwisted = ConfigFileLTG.Bind(
               "Content",
               "New Twisted Scavengers",
               true,
               "Adds new Twisted Scavengers with DLC items"
          );

            cfgRedMultiShop = ConfigFileLTG.Bind(
                  "Content",
                  "Legendary Multi Shop",
                  true,
                  "Very rare interactable"
            );
            cfgNewFamilies = ConfigFileLTG.Bind(
                  "Content",
                  "New Family Events",
                  true,
                  "Adds family events for multiple groups lacking it.\nClay, Solus, Worms, Vermin"
            );


        }

        private static void CfgPrismRun_SettingChanged(object sender, System.EventArgs e)
        {
            PrismaticRunMaker.runObject.GetComponent<PrismRun>().userPickable = cfgPrismRun.Value;
        }

        public static void RiskConfig()
        {
            ModSettingsManager.SetModDescription("Prismatic Run and other junk");
            Texture2D texPrism = new Texture2D(128, 128, TextureFormat.DXT1, false)
            {
                filterMode = FilterMode.Bilinear
            };
            texPrism.LoadImage(Properties.Resources.texPrismRuleOn, false);
            ModSettingsManager.SetModIcon(Sprite.Create(texPrism, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f)));



            CheckBoxConfig overwriteName = new CheckBoxConfig
            {
                category = "Config",
                restartRequired = false,
            };
            CheckBoxConfig overwriteNameR = new CheckBoxConfig
            {
                category = "Config",
                restartRequired = true,
            };
            ModSettingsManager.AddOption(new CheckBoxOption(cfgPrismTimerAlwaysRun, overwriteName));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgPrismRun, overwriteName));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgRedMultiShop, overwriteNameR));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgScavNewTwisted, overwriteNameR));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgNewFamilies, overwriteNameR));

        }
    }
}