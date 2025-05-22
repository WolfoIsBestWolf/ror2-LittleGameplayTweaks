using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using System.Collections.Generic;
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



        public static void RiskConfig()
        {
            ModSettingsManager.SetModDescription("Prismatic Run and other junk");
            Texture2D texPrism = new Texture2D(128, 128, TextureFormat.DXT1, false)
            {
                filterMode = FilterMode.Bilinear
            };
            texPrism.LoadImage(Properties.Resources.texPrismRuleOn, false);
            ModSettingsManager.SetModIcon(Sprite.Create(texPrism, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f)));


            List<ConfigEntry<bool>> resetB = new List<ConfigEntry<bool>>()
            {
                 cfgPrismTimerAlwaysRun,
            };

            var entries = ConfigFileLTG.GetConfigEntries();
            foreach (ConfigEntryBase entry in entries)
            {
                if (entry.SettingType == typeof(bool))
                {
                    var temp = (ConfigEntry<bool>)entry;
                    ModSettingsManager.AddOption(new CheckBoxOption(temp, !resetB.Contains(temp)));
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