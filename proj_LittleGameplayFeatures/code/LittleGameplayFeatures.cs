using BepInEx;
using R2API.Utils;
using RoR2;
using System.Security;
using System.Security.Permissions;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace LittleGameplayFeatures
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("Wolfo.LittleGameplayTweaksFeatures", "LittleGameplayFeatures", "3.5.0")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    public class LittleGameplayTweaks : BaseUnityPlugin
    {
        static readonly System.Random random = new System.Random();

        public void Awake()
        {
            Assets.Init(Info);
            WConfig.InitConfig();

            TwistedScavs.Start();
            TripleShopLegendary.Start();
            NewFamilyEvents.Families();
            PrismaticRunMaker.Start();

            GameModeCatalog.availability.CallWhenAvailable(LateMethod);
        }

  
        internal static void LateMethod()
        {
            WConfig.RiskConfig();
            NewFamilyEvents.ModSupport();
            PrismaticRunMaker.LateRunningMethod();
             TwistedScavs.CallLate();
        }


    }


}