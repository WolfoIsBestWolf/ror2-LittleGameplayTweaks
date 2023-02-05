using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace LittleGameplayTweaks
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.Wolfo.LittleGameplayTweaks", "LittleGameplayTweaks", "2.0.4")]
    [R2APISubmoduleDependency(nameof(ContentAddition), nameof(LanguageAPI), nameof(PrefabAPI), nameof(ItemAPI), nameof(LoadoutAPI), nameof(EliteAPI))]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]


    public class LittleGameplayTweaks : BaseUnityPlugin
    {
        //nameof(DirectorAPI), 
        static readonly System.Random random = new System.Random();
        public static bool WasLastWaveRandomSurvivor = false;

        public static GameEndingDef InfiniteTowerEnding = ScriptableObject.CreateInstance<GameEndingDef>();

        public static SceneDef PreviousSceneDef = null;
        public static SceneCollection BackupSceneCollection = null;

        public static CharacterSpawnCard cscBeetleGuardInherit = null;

        private static GameObject RedToWhiteSoup = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/LunarCauldron, RedToWhite Variant");

        private static CombatDirector.EliteTierDef[] SimuEliteTiersBackup;
        public static string LastSimuWaveName;


        public static SpawnCard iscVoidOutroPortal = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidOutroPortal/iscVoidOutroPortal.asset").WaitForCompletion();


        public static ItemDef ScrapWhiteSuppressed = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/DLC1/ScrapVoid/ScrapWhiteSuppressed.asset").WaitForCompletion();
        public static ItemDef ScrapGreenSuppressed = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/DLC1/ScrapVoid/ScrapGreenSuppressed.asset").WaitForCompletion();
        public static ItemDef ScrapRedSuppressed = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/DLC1/ScrapVoid/ScrapRedSuppressed.asset").WaitForCompletion();

        public static EliteDef EliteDefLunarEulogy = ScriptableObject.CreateInstance<EliteDef>();
        public static CombatDirector.EliteTierDef Tier1EliteTierBackup;
        public static CombatDirector.EliteTierDef TierLunarEliteTierBackup;

        private static GameObject MoffeinClayMan = null;
        public static bool RedSoupBought = false;
        public static int LastCheckedBeadAmount = 0;
        public static SceneDef[] prevDestinations = new SceneDef[] { };
        public static SceneDef[] prevDestinationsClone = new SceneDef[] { };


        public static List<ExplicitPickupDropTable> AllScavCompatibleDropTables = new List<ExplicitPickupDropTable>();

        private static Rect recnothing = new Rect(0, 0, 0, 0);
        private static Rect recwide = new Rect(0, 0, 384, 256);
        private static Rect rechalftall = new Rect(0, 0, 256, 320);
        private static Rect rechalfwide = new Rect(0, 0, 320, 256);
        private static Rect rectall = new Rect(0, 0, 256, 384);
        private static Rect rec512 = new Rect(0, 0, 512, 512);
        private static Rect rec320 = new Rect(0, 0, 320, 320);
        private static Rect rec256 = new Rect(0, 0, 256, 256);
        private static Rect rec192 = new Rect(0, 0, 192, 192);
        private static Rect rec128 = new Rect(0, 0, 128, 128);
        private static Rect rec106 = new Rect(0, 0, 106, 106);
        private static Rect rec64 = new Rect(0, 0, 64, 64);
        private static Vector2 half = new Vector2(0.5f, 0.5f);


        public static ArtifactDef ArtifactDefSingleMonster = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/SingleMonsterType");
        public static ArtifactDef ArtifactDefEliteOnly = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/EliteOnly");
        public static ArtifactDef ArtifactDefShadowClone = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/ShadowClone");
        public static ArtifactDef ArtifactDefSingleMonsterType = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/SingleMonsterType");


        public static BasicPickupDropTable dtAllTier = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITFamilyWaveDamage = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITFamilyWaveHealing = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITFamilyWaveUtility = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static BasicPickupDropTable dtITBasicWaveOnKill = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static BasicPickupDropTable dtITBasicBonusLunar = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITBasicBonusVoid = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static BasicPickupDropTable dtITSpecialEquipmentDroneBoss = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITSpecialLunarBoss = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITSpecialVoidBoss = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITSpecialVoidling = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITSpecialBossYellow = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static BasicPickupDropTable dtITSpecialBossWave = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITSpecialBossWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITVoid = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITVoid.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITLunar = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITLunar.asset").WaitForCompletion();

        public static BasicPickupDropTable dtMonsterTeamTier1Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/MonsterTeamGainsItems/dtMonsterTeamTier1Item.asset").WaitForCompletion();
        public static BasicPickupDropTable dtMonsterTeamTier2Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/MonsterTeamGainsItems/dtMonsterTeamTier2Item.asset").WaitForCompletion();
        public static BasicPickupDropTable dtMonsterTeamTier3Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/MonsterTeamGainsItems/dtMonsterTeamTier3Item.asset").WaitForCompletion();

        public static ArenaMonsterItemDropTable dtArenaMonsterTier1 = Addressables.LoadAssetAsync<ArenaMonsterItemDropTable>(key: "RoR2/Base/arena/dtArenaMonsterTier1.asset").WaitForCompletion();
        public static ArenaMonsterItemDropTable dtArenaMonsterTier2 = Addressables.LoadAssetAsync<ArenaMonsterItemDropTable>(key: "RoR2/Base/arena/dtArenaMonsterTier2.asset").WaitForCompletion();
        public static ArenaMonsterItemDropTable dtArenaMonsterTier3 = Addressables.LoadAssetAsync<ArenaMonsterItemDropTable>(key: "RoR2/Base/arena/dtArenaMonsterTier3.asset").WaitForCompletion();

        public static BasicPickupDropTable dtAISafeTier1Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/Common/dtAISafeTier1Item.asset").WaitForCompletion();
        public static BasicPickupDropTable dtAISafeTier2Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/Common/dtAISafeTier2Item.asset").WaitForCompletion();
        public static BasicPickupDropTable dtAISafeTier3Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/Common/dtAISafeTier3Item.asset").WaitForCompletion();
        public static BasicPickupDropTable dtAISafeRandomVoid = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static InfiniteTowerRun InfiniteTowerRunBase = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerRun.prefab").WaitForCompletion().GetComponent<InfiniteTowerRun>();

        public static CharacterSpawnCard[] AllCSCEquipmentDronesIT;
        public static CharacterSpawnCard cscVoidInfestorIT;
        //public static CharacterSpawnCard cscVultureNoCeiling;

        //public static bool WasSimuCrabIndicator = false;
        //public static GameObject SimuCrabPointer = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidCamp/VoidCampPositionIndicator.prefab").WaitForCompletion(), "InfiniteTowerWardPositionIndicator", false);

        public static GameObject InfiniteTowerWaveFamilyBeetle = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyBeetle", true);
        public static GameObject InfiniteTowerWaveFamilyGolem = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyGolem", true);
        public static GameObject InfiniteTowerWaveFamilyJellyfish = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyJellyfish", true);
        public static GameObject InfiniteTowerWaveFamilyClay = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyClay", true);

        public static GameObject InfiniteTowerWaveFamilyWisp = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyWisp", true);
        public static GameObject InfiniteTowerWaveFamilyLemurian = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyLemurian", true);
        public static GameObject InfiniteTowerWaveFamilyImp = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyImp", true);
        public static GameObject InfiniteTowerWaveFamilyRoboBall = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyRoboBall", true);
        public static GameObject InfiniteTowerWaveFamilyConstruct = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyConstruct", true);

        public static GameObject InfiniteTowerWaveFamilyParent = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyParent", true);
        public static GameObject InfiniteTowerWaveFamilyGup = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyGup", true);
        public static GameObject InfiniteTowerWaveFamilyVermin = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyVermin", true);


        public static GameObject InfiniteTowerWaveBossArtifactEliteOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactEliteOnly", true);
        public static GameObject InfiniteTowerWaveBossArtifactDoppelganger = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactDoppelganger", true);



        public static GameObject InfiniteTowerWaveVoidElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveVoidElites", true);
        public static GameObject InfiniteTowerWaveLunarElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveLunarElites", true);

        public static GameObject InfiniteTowerWaveBossVoidElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossVoidElites", true);


        public static GameObject InfiniteTowerWaveBossScav = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion();
        public static GameObject InfiniteTowerWaveBossBrother = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossBrother.prefab").WaitForCompletion();

        public static GameObject InfiniteTowerWaveBossScavLunar = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossScavLunar", true);
        public static GameObject InfiniteTowerWaveBossSuperRoboBallBoss = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossSuperRoboBallBoss", true);
        public static GameObject InfiniteTowerWaveBossTitanGold = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossTitanGold", true);
        public static GameObject InfiniteTowerWaveBossVoidRaidCrab = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossVoidRaidCrab", true);

        public static GameObject InfiniteTowerWaveBossEquipmentDrone = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossEquipmentDrone", true);


        public static GameObject[] ITAllSpecialBossWaves = new GameObject[] { InfiniteTowerWaveBossScav, InfiniteTowerWaveBossBrother, InfiniteTowerWaveBossScavLunar, InfiniteTowerWaveBossSuperRoboBallBoss, InfiniteTowerWaveBossTitanGold, InfiniteTowerWaveBossVoidRaidCrab };

        //
        //
        //UI
        public static GameObject InfiniteTowerCurrentWaveUIFamilyBeetle = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyBeetle", false);
        public static GameObject InfiniteTowerCurrentWaveUIFamilyGolem = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyGolem", false);
        public static GameObject InfiniteTowerCurrentWaveUIFamilyJellyfish = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyJellyfish", false);
        public static GameObject InfiniteTowerCurrentWaveUIFamilyClay = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyClay", false);

        public static GameObject InfiniteTowerCurrentWaveUIFamilyWisp = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyWisp", false);
        public static GameObject InfiniteTowerCurrentWaveUIFamilyLemurian = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyLemurian", false);
        public static GameObject InfiniteTowerCurrentWaveUIFamilyImp = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyImp", false);
        public static GameObject InfiniteTowerCurrentWaveUIFamilyRoboBall = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyRoboBall", false);
        public static GameObject InfiniteTowerCurrentWaveUIFamilyConstruct = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyConstruct", false);

        public static GameObject InfiniteTowerCurrentWaveUIFamilyParent = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyParent", false);
        public static GameObject InfiniteTowerCurrentWaveUIFamilyGup = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyGup", false);
        public static GameObject InfiniteTowerCurrentWaveUIFamilyVermin = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyGup", false);

        public static GameObject InfiniteTowerCurrentBossWaveUIArtifactEliteOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossWaveUIArtifactEliteOnly", false);
        public static GameObject InfiniteTowerCurrentBossWaveUIArtifactDoppelganger = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossWaveUIArtifactDoppelganger", false);

        public static GameObject InfiniteTowerCurrentLunarEliteWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentLunarEliteWaveUI", false);
        public static GameObject InfiniteTowerCurrentVoidEliteWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentVoidEliteWaveUI", false);

        public static GameObject InfiniteTowerCurrentBossVoidEliteWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossVoidEliteWaveUI", false);

        public static GameObject InfiniteTowerCurrentBossScavLunarWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossScavWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossScavLunarWaveUI", false);
        public static GameObject InfiniteTowerCurrentBossSuperRoboBallBossWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossSuperRoboBallBossWaveUI", false);
        public static GameObject InfiniteTowerCurrentBossTitanGoldWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossTitanGoldWaveUI", false);
        public static GameObject InfiniteTowerCurrentBossVoidRaidWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossVoidRaidWaveUI", false);
        public static GameObject InfiniteTowerCurrentBossEquipmentDroneWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossEquipmentDroneWaveUI", false);


        //Prerequesite
        public static RoR2.InfiniteTowerWaveArtifactPrerequisites ArtifactEliteOnlyDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave11OrGreaterPrerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveCountPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/Wave11OrGreaterPrerequisite.asset").WaitForCompletion();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave21OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave26OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave41OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave46OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();

        public static InfiniteTowerMaxWaveCountPrerequisites Wave30OrLowerPrerequisite = ScriptableObject.CreateInstance<InfiniteTowerMaxWaveCountPrerequisites>();
        public static InfiniteTowerMaxWaveCountPrerequisites Wave50OrLowerPrerequisite = ScriptableObject.CreateInstance<InfiniteTowerMaxWaveCountPrerequisites>();
        public static InfiniteTowerMaxWaveCountPrerequisites Wave90OrLowerPrerequisite = ScriptableObject.CreateInstance<InfiniteTowerMaxWaveCountPrerequisites>();

        //Family
        public static FamilyDirectorCardCategorySelection dccsBeetleFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsBeetleFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsGolemFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGolemFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsGolemFamilySimu = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGolemFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsGolemFamilyAbyssal = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGolemFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsGupFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGupFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsImpFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsImpFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsJellyfishFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsJellyfishFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsLemurianFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLemurianFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsLunarFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsMushroomFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsMushroomFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsParentFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsParentFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsWispFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsWispFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsAcidLarvaFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsAcidLarvaFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsConstructFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsConstructFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsVoidFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsVoidFamily.asset").WaitForCompletion();

        public static FamilyDirectorCardCategorySelection dccsVoidFamilyNoBarnacle = Instantiate(dccsVoidFamily);
        public static FamilyDirectorCardCategorySelection dccsClayFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
        public static FamilyDirectorCardCategorySelection dccsRoboBallFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
        public static FamilyDirectorCardCategorySelection dccsVerminFamily = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();
        public static FamilyDirectorCardCategorySelection dccsVerminFamilySnowy = ScriptableObject.CreateInstance<FamilyDirectorCardCategorySelection>();

        public static DirectorCardCategorySelection dccsVoidInfestorOnly = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();

        //
        //
        //Dccs Monster Stages
        public static DirectorCardCategorySelection dccsGolemplainsMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsMonstersDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsBlackBeachMonstersDLC = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachMonstersDLC.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsSnowyForestMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/snowyforest/dccsSnowyForestMonstersDLC1.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsGooLakeMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeMonstersDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsFoggySwampMonstersDLC = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampMonstersDLC.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsAncientLoftMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/ancientloft/dccsAncientLoftMonstersDLC1.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsFrozenWallMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallMonstersDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsWispGraveyardMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardMonstersDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsSulfurPoolsMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsMonstersDLC1.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsDampCaveMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveMonstersDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsShipgraveyardMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardMonstersDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsRootJungleMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleMonstersDLC1.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsSkyMeadowMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowMonstersDLC1.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsArtifactWorldMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/artifactworld/dccsArtifactWorldMonstersDLC1.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsITGolemplainsMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itgolemplains/dccsITGolemplainsMonsters.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsITGooLakeMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itgoolake/dccsITGooLakeMonsters.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsITAncientLoftMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itancientloft/dccsITAncientLoftMonsters.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsITFrozenWallMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itfrozenwall/dccsITFrozenWallMonsters.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsITDampCaveMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itdampcave/dccsITDampCaveMonsters.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsITSkyMeadowMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itskymeadow/dccsITSkyMeadowMonsters.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsITMoonMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itmoon/dccsITMoonMonsters.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsGoldshoresMonstersDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goldshores/dccsGoldshoresMonstersDLC1.asset").WaitForCompletion();
        //
        //
        //Dccs Interactables Stages
        public static DirectorCardCategorySelection dccsGolemplainsInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/golemplains/dccsGolemplainsInteractablesDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsBlackBeachInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/blackbeach/dccsBlackBeachInteractablesDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsSnowyForestInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/snowyforest/dccsSnowyForestInteractablesDLC1.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsGooLakeInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goolake/dccsGooLakeInteractablesDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsFoggySwampInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/foggyswamp/dccsFoggySwampInteractablesDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsAncientLoftInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/ancientloft/dccsAncientLoftInteractablesDLC1.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsFrozenWallInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/frozenwall/dccsFrozenWallInteractablesDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsWispGraveyardInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/wispgraveyard/dccsWispGraveyardInteractablesDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsSulfurPoolsInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/sulfurpools/dccsSulfurPoolsInteractablesDLC1.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsDampCaveInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveInteractablesDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsShipgraveyardInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardInteractablesDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsRootJungleInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/rootjungle/dccsRootJungleInteractablesDLC1.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsSkyMeadowInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/skymeadow/dccsSkyMeadowInteractablesDLC1.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsArtifactWorldInteractablesDLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/artifactworld/dccsArtifactWorldInteractablesDLC1.asset").WaitForCompletion();

        public static DirectorCardCategorySelection dccsInfiniteTowerInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dccsInfiniteTowerInteractables.asset").WaitForCompletion();



        public static DirectorCardCategorySelection dccsITGolemPlainsInteractablesW = null;
        public static DirectorCardCategorySelection dccsITGooLakeInteractablesW = null;
        public static DirectorCardCategorySelection dccsITAncientLoftInteractablesW = null;
        public static DirectorCardCategorySelection dccsITFrozenWallInteractablesW = null;
        public static DirectorCardCategorySelection dccsITDampCaveInteractablesW = null;
        public static DirectorCardCategorySelection dccsITSkyMeadowInteractablesW = null;
        public static DirectorCardCategorySelection dccsITMoonInteractablesW = null;

        //
        //
        //DccsPool Monsters Stages
        public static DccsPool dpGolemplainsMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/golemplains/dpGolemplainsMonsters.asset").WaitForCompletion();
        public static DccsPool dpBlackBeachMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/blackbeach/dpBlackBeachMonsters.asset").WaitForCompletion();
        public static DccsPool dpSnowyForestMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/snowyforest/dpSnowyForestMonsters.asset").WaitForCompletion();

        public static DccsPool dpGooLakeMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/goolake/dpGooLakeMonsters.asset").WaitForCompletion();
        public static DccsPool dpFoggySwampMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/foggyswamp/dpFoggySwampMonsters.asset").WaitForCompletion();
        public static DccsPool dpAncientLoftMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/ancientloft/dpAncientLoftMonsters.asset").WaitForCompletion();

        public static DccsPool dpFrozenWallMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/frozenwall/dpFrozenWallMonsters.asset").WaitForCompletion();
        public static DccsPool dpWispGraveyardMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/wispgraveyard/dpWispGraveyardMonsters.asset").WaitForCompletion();
        public static DccsPool dpSulfurPoolsMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/sulfurpools/dpSulfurPoolsMonsters.asset").WaitForCompletion();

        public static DccsPool dpDampCaveMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/dampcave/dpDampCaveMonsters.asset").WaitForCompletion();
        public static DccsPool dpShipgraveyardMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/shipgraveyard/dpShipgraveyardMonsters.asset").WaitForCompletion();
        public static DccsPool dpRootJungleMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/rootjungle/dpRootJungleMonsters.asset").WaitForCompletion();

        public static DccsPool dpSkyMeadowMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/skymeadow/dpSkyMeadowMonsters.asset").WaitForCompletion();

        public static DccsPool dpMoonMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/moon/dpMoonMonsters.asset").WaitForCompletion();
        public static DccsPool dpArtifactWorldMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/artifactworld/dpArtifactWorldMonsters.asset").WaitForCompletion();
        public static DccsPool dpVoidStageMonsters = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/DLC1/voidstage/dpVoidStageMonsters.asset").WaitForCompletion();

        public static DccsPool[] AllDccsPools = { dpGolemplainsMonsters , dpBlackBeachMonsters , dpSnowyForestMonsters ,
        dpGooLakeMonsters,dpFoggySwampMonsters,dpAncientLoftMonsters,dpFrozenWallMonsters,dpWispGraveyardMonsters,dpSulfurPoolsMonsters,dpDampCaveMonsters,
        dpShipgraveyardMonsters,dpRootJungleMonsters,dpSkyMeadowMonsters,dpMoonMonsters,dpArtifactWorldMonsters,dpVoidStageMonsters};

        //
        public static DccsPool dpBlackBeachInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/blackbeach/dpBlackBeachInteractables.asset").WaitForCompletion();
        public static DccsPool dpFoggySwampInteractables = Addressables.LoadAssetAsync<DccsPool>(key: "RoR2/Base/foggyswamp/dpFoggySwampInteractables.asset").WaitForCompletion();
        //public static RoR2.Navigation.NodeGraph foggyswampAirNodesNodegraph = Addressables.LoadAssetAsync<RoR2.Navigation.NodeGraph>(key: "RoR2/Base/foggyswamp/foggyswampAirNodesNodegraph.asset").WaitForCompletion();




        private static CharacterSpawnCard TitanGoolake = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscTitanGooLake");
        private static CharacterSpawnCard TitanPlains = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscTitanGolemPlains");



        public static ConfigEntry<bool> WolfoSkinsConfig;
        public static ConfigEntry<bool> SkinsGreenMando;
  
        public static ConfigEntry<bool> EulogyLunarElites;
        public static ConfigEntry<bool> SimuMultiplayerChanges;

        public static ConfigEntry<bool> EnemyChanges;
        public static ConfigEntry<bool> EnemyChangesLooping;
        public static ConfigEntry<bool> InteractableChanges;
        public static ConfigEntry<bool> InteractableCostChanges;
        public static ConfigEntry<bool> StageCreditChanges;

        public static ConfigEntry<float> FamilyEventChance;
        public static ConfigEntry<bool> FamilyEventAdditions;

        public static ConfigEntry<bool> ScavAsProperBoss;

        public static ConfigEntry<bool> DronesInherit;
        public static ConfigEntry<bool> MinionsInherit;

        public static ConfigEntry<bool> VoidPotentialLootChanges;

        public static ConfigEntry<bool> NoLunarCost;
        public static ConfigEntry<int> HealingShrineCost;
        public static ConfigEntry<float> HealingShrineMulti;
        public static ConfigEntry<int> GoldShrineCost;
        public static ConfigEntry<int> RadarScannerCost;
        public static ConfigEntry<float> BloodShrineMulti;
        public static ConfigEntry<bool> BloodShrineScaleWithTime;
        public static ConfigEntry<int> EquipmentMultishopCost;
        public static ConfigEntry<int> RedSoupAmount;
        public static ConfigEntry<int> ScrapperAmount;

        public static ConfigEntry<int> GunDroneCost;
        public static ConfigEntry<int> HealingDroneCost;
        public static ConfigEntry<int> MissileDroneCost;
        public static ConfigEntry<int> FlameDroneCost;
        public static ConfigEntry<int> EmergencyDroneCost;
        public static ConfigEntry<int> MegaDroneCost;
        public static ConfigEntry<int> TurretDroneCost;

        public static ConfigEntry<float> ShopChancePercentage;
        public static ConfigEntry<float> YellowPercentage;
        public static ConfigEntry<float> BonusAspectDropRate;
        public static float AspectDropRate;

        public static ConfigEntry<bool> CaptainKeepInHiddemRealm;
        public static ConfigEntry<bool> SimulacrumEnemyItemChanges;
        public static ConfigEntry<bool> SquidTurretMechanical;


        public static ConfigEntry<int> ConfigSimuEndingStartAtXWaves;
        public static ConfigEntry<int> ConfigSimuEndingEveryXWaves;
        public static ConfigEntry<int> ConfigSimuForcedBossStartAtXWaves;
        public static ConfigEntry<int> ConfigSimuForcedBossEveryXWaves;

        public static int SimuEndingStartAtXWaves;
        public static int SimuEndingEveryXWaves;
        public static int SimuEndingWaveRest;
        public static int SimuForcedBossStartAtXWaves;
        public static int SimuForcedBossEveryXWaves;
        public static int SimuForcedBossWaveRest;


        private void InitConfig()
        {
            WolfoSkinsConfig = Config.Bind(
                "1 - Main",
                "Extra Skins",
                true,
                "2 recolor Skins made by me"
            );
            SkinsGreenMando = Config.Bind(
                "1 - Main",
                "Readd unused skins",
                true,
                "1 Unused SotV Commando Skin"
            );


            EnemyChanges = Config.Bind(
                "2a - Enemies",
                "Mix up enemy variety",
                true,
                "Enable the non loop enemy pool changes as specificed on the mod page"
            );

            EnemyChangesLooping = Config.Bind(
                "2a - Enemies",
                "Mix up enemy variety (Loop)",
                true,
                "Enable the loop enemy pool changes as specificed on the mod page"
            );

            ScavAsProperBoss = Config.Bind(
                "2a - Enemies",
                "Scavs as proper Bosses",
                true,
                "Scavs are allowed to spawn as a boss under normal conditions and gain a Boss item"
            );


            FamilyEventAdditions = Config.Bind(
                "2b - Family Events",
                "Add extra Family Events",
                true,
                "Adds 3 new family events (Parent, Clay, Solus/Vulture) to add more diversity\nClay is post loop only unless Clay Man mod is installed."
            );

            FamilyEventChance = Config.Bind(
                "2b - Family Events",
                "Family Event Chance",
                0.02f,
                "Chance of a Family Event occuring on any stage. (100%) 1.00 - 0.00 (0%)\nVanilla is 2% (0.02)"
            );

            InteractableChanges = Config.Bind(
                "3a - Interactables",
                "Changes to Interactables Rarity",
                true,
                "Make certain Interactables more common"
            );
            InteractableCostChanges = Config.Bind(
                "3a - Interactables",
                "Changes to Director Credit",
                true,
                "Enable the lowered cost of certain interactables"
            );
            VoidPotentialLootChanges = Config.Bind(
                "3a - Interactables",
                "Void Potentials All loot tiers",
                true,
                "Should Void Potential Stalks draw from ever tier or just from the small chest loot pool"
            );


            ///////////
            ///

            MinionsInherit = Config.Bind(
                "4 - General",
                "Minions Inherit Elite Equip",
                true,
                "Should Minions such as Beetle Guard Ally or Empathy Cores inherit Elite Equipment"
            );
            DronesInherit = Config.Bind(
                "4 - General",
                "Drones Inherit Elite Equipment",
                true,
                "Should Drones inherit Elite Equipment too"
            );
            CaptainKeepInHiddemRealm = Config.Bind(
                "4 - General",
                "Captain Hidden Realms",
                true,
                "Should Captain be allowed to have fun in Hidden and Void realms."
            );

            EulogyLunarElites = Config.Bind(
                "4a - Items",
                "Eulogy Zero Perfected Elites",
                true,
                "Should Eulogy have a chance to replace Tier 1 elites with a Perfected elite"
            );
            SquidTurretMechanical = Config.Bind(
                "4a - Items",
                "Squid Polyp Mechanical",
                true,
                "Should the Squid Turret be mechanical and benefit from Captain Microbots and Spare Drone Parts."
           );

            SimuMultiplayerChanges = Config.Bind(
                "4b - Simulacrum",
                "Multiplayer Scaling Changes",
                true,
                "Should the amount of interactables, the money you gain and the type of enemies you fight scale with Multiplayer. Normally Simularum gives every enemy a 100% more hp per player."
            );
            SimulacrumEnemyItemChanges = Config.Bind(
                "4b - Simulacrum",
                "Stricted Enemy Blacklist",
                true,
                "Should enemies have the same blacklist as Void Fields or Evolution. In Vanilla it's the same blacklist as Scavengers which leads to many items regular enemies can't use."
            );

            ConfigSimuEndingStartAtXWaves = Config.Bind(
                "4ba - Simulacrum Ending",
                "Ending Start Wave",
                60,
                "This is the first wave the ending Portal appears (only use steps of 10)"
            );
            ConfigSimuEndingEveryXWaves = Config.Bind(
                "4ba - Simulacrum Ending",
                "Ending Portal Every X Waves",
                30,
                "The ending portal will appear every X waves. (only use steps of 10)"
            );
            ConfigSimuForcedBossStartAtXWaves = Config.Bind(
                "4bb - Simulacrum Forced Boss",
                "Forced Special Boss Start Wave",
                60,
                "A forced special boss is meant to be paired with the wave where the ending portal spawns so it's less of just a random end. (only use steps of 10)"
            );
            ConfigSimuForcedBossEveryXWaves = Config.Bind(
                "4bb - Simulacrum Forced Boss",
                "Forced Special Boss Every X Waves",
                60,
                "The forced special boss will appear every X waves. (only use steps of 10)"
            );


            ////////////

            YellowPercentage = Config.Bind(
                "7 - Drop Rates",
                "Boss Item Percent",
                0.25f,
                "Percent of Green Items from Teleporters that are replaced with Yellow Items. From (0.00 - 1.00)"
            );
            BonusAspectDropRate = Config.Bind(
                "7 - Drop Rates",
                "Aspect Drop Rate",
                0.1f,
                "Drop rate for Aspects. For chance Divide 100 by X for 1 in X chance"
            );
            ShopChancePercentage = Config.Bind(
                "7 - Drop Rates",
                "Natural Bazaar Portal Chance",
                0.25f,
                "Vanilla is 0.05 due to an overseight, the intended value is 0.375"
            );
            /////////////////////////////////////////////

            RedSoupAmount = Config.Bind(
                "8 - Price Changer - Main",
                "RedToWhite Cauldron extra item amount",
                0,
                "This is in addition to the 3 that Vanilla pays out with\nie If you want 5 set it to 2"
            );


            NoLunarCost = Config.Bind(
                "8 - Price Changer - Main",
                "All Lunar Coin purchases cost 0",
                false,
                ""
            );


            ScrapperAmount = Config.Bind(
                "8 - Price Changer - Main",
                "Scrapper Item Amount",
                10,
                "How many items to scrap at once. Vanilla is 10"
            );

            /////////////////////////////////////////////

            MegaDroneCost = Config.Bind(
                "8b - Price Changer - Drones",
                "TC 280 Prototype Price",
                300,
                "Vanilla is 350"
            );
            FlameDroneCost = Config.Bind(
                "8b - Price Changer - Drones",
                "Flame Drone Price",
                100,
                "Vanilla is 100"
            );
            EmergencyDroneCost = Config.Bind(
                "8b - Price Changer - Drones",
                "Emergency Drone Price",
                100,
                "Vanilla is 100"
            );
            MissileDroneCost = Config.Bind(
                "8b - Price Changer - Drones",
                "Missile Drone Price",
                60,
                "Vanilla is 60"
            );
            HealingDroneCost = Config.Bind(
                "8b - Price Changer - Drones",
                "Healing Drone Price",
                40,
                "Vanilla is 40"
            );
            GunDroneCost = Config.Bind(
                "8b - Price Changer - Drones",
                "Gunner Drone Price",
                40,
                "Vanilla is 40"
            );
            TurretDroneCost = Config.Bind(
                "8b - Price Changer - Drones",
                "Gunner Turret Price",
                30,
                "Vanilla is 35"
            );

            /////////////////////////////////////////////
            HealingShrineCost = Config.Bind(
                "8a - Price Changer",
                "Healing Shrine Price",
                15,
                "Vanilla is 25"
            );
            HealingShrineMulti = Config.Bind(
                "8a - Price Changer",
                "Healing Shrine Price Multiplier on Use",
                1.25f,
                "Vanilla is 1.5"
            );
            GoldShrineCost = Config.Bind(
                "8a - Price Changer",
                "Gold Shrine Price",
                200,
                "Vanilla is 200"
            );

            EquipmentMultishopCost = Config.Bind(
                "8a - Price Changer",
                "Equipment Multi Shop Price",
                50,
                "Vanilla is 50"
            );

            BloodShrineMulti = Config.Bind(
                "8a - Price Changer",
                "Blood Shrine Health to Money Ratio",
                0.5f,
                "Vanilla is 0.5. For every 2 health gain 1 gold."
            );
            BloodShrineScaleWithTime = Config.Bind(
                "8a - Price Changer",
                "Blood Shrine reward scale with Difficulty",
                true,
                "Normally Blood Shrines Reward is only based on HP quickly making them a bad source of money"
            );

            RadarScannerCost = Config.Bind(
                "8a - Price Changer",
                "Radar Scanner Price",
                50,
                "Vanilla is 150"
            );

            ////////////////////////
        }




        internal static void ModSupport()
        {
            /*
            for(int i = 0;i < ItemCatalog.itemDefs.Length; i++)
            {
                Debug.LogWarning("");
                Debug.LogWarning("- "+ItemCatalog.itemDefs[i].name + "  tier:"+ ItemCatalog.itemDefs[i].tier+" -");
                /*
                for (int tags = 0; tags < ItemCatalog.itemDefs[i].tags.Length;tags++)
                {
                    Debug.LogWarning(ItemCatalog.itemDefs[i].tags[tags]);
                }
                
            }
            */

            InfiniteTowerRunBase.blacklistedItems = InfiniteTowerRunBase.blacklistedItems.Remove(InfiniteTowerRunBase.blacklistedItems[2]);


            RoR2Content.Items.MonstersOnShrineUse.tags = RoR2Content.Items.MonstersOnShrineUse.tags.Add(ItemTag.InteractableRelated);
            DLC1Content.Items.MushroomVoid.tags = DLC1Content.Items.MushroomVoid.tags.Add(ItemTag.SprintRelated);

            DLC1Content.Items.MoveSpeedOnKill.tags = DLC1Content.Items.MoveSpeedOnKill.tags.Add(ItemTag.OnKillEffect);


            RoR2Content.Items.ParentEgg.tags[0] = ItemTag.Healing;
            RoR2Content.Items.ShieldOnly.tags[0] = ItemTag.Healing;
            RoR2Content.Items.LunarUtilityReplacement.tags[0] = ItemTag.Healing;
            RoR2Content.Items.RandomDamageZone.tags[0] = ItemTag.Damage;
            DLC1Content.Items.HalfSpeedDoubleHealth.tags[0] = ItemTag.Healing;
            DLC1Content.Items.LunarSun.tags[0] = ItemTag.Damage;

            DLC1Content.Items.MinorConstructOnKill.tags = DLC1Content.Items.MinorConstructOnKill.tags.Add(ItemTag.Utility);
            RoR2Content.Items.Knurl.tags = RoR2Content.Items.Knurl.tags.Remove(ItemTag.Utility);





            RoR2Content.Items.Infusion.tags = RoR2Content.Items.Infusion.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.GhostOnKill.tags = RoR2Content.Items.GhostOnKill.tags.Remove(ItemTag.Damage);
            RoR2Content.Items.HeadHunter.tags = RoR2Content.Items.HeadHunter.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.BarrierOnKill.tags = RoR2Content.Items.BarrierOnKill.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.BarrierOnOverHeal.tags = RoR2Content.Items.BarrierOnOverHeal.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.FallBoots.tags = RoR2Content.Items.FallBoots.tags.Remove(ItemTag.Damage);

            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Remove(ItemTag.Damage);
            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Add(ItemTag.Healing);

            DLC1Content.Items.AttackSpeedAndMoveSpeed.tags = DLC1Content.Items.AttackSpeedAndMoveSpeed.tags.Remove(ItemTag.Utility);
            DLC1Content.Items.ElementalRingVoid.tags = DLC1Content.Items.ElementalRingVoid.tags.Remove(ItemTag.Utility);
            /*
            RoR2Content.Items.PersonalShield.tags = RoR2Content.Items.PersonalShield.tags.Add(ItemTag.Healing);
            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Add(ItemTag.Healing);
            DLC1Content.Items.ImmuneToDebuff.tags = DLC1Content.Items.ImmuneToDebuff.tags.Add(ItemTag.Healing);
            */

            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.ShockNearby.tags = RoR2Content.Items.ShockNearby.tags.Add(ItemTag.Count);
            DLC1Content.Items.CritDamage.tags = DLC1Content.Items.CritDamage.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.DroneWeapons.tags = DLC1Content.Items.DroneWeapons.tags.Add(ItemTag.AIBlacklist);
            //RoR2Content.Items.GhostOnKill.tags = RoR2Content.Items.GhostOnKill.tags.Add(ItemTag.AIBlacklist);

            RoR2Content.Items.FlatHealth.tags = RoR2Content.Items.FlatHealth.tags.Remove(ItemTag.OnKillEffect);
            RoR2Content.Items.FlatHealth.tags = RoR2Content.Items.FlatHealth.tags.Add(ItemTag.Count);

            DLC1Content.Items.RegeneratingScrap.tags = DLC1Content.Items.RegeneratingScrap.tags.Add(ItemTag.AIBlacklist);


            //RoR2Content.Items.BarrierOnOverHeal.tags = RoR2Content.Items.BarrierOnOverHeal.tags.Add(ItemTag.SprintRelated);
            //RoR2Content.Items.BonusGoldPackOnKill.tags = RoR2Content.Items.BonusGoldPackOnKill.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.Infusion.tags = RoR2Content.Items.Infusion.tags.Add(ItemTag.AIBlacklist);
            //RoR2Content.Items.GoldOnHit.tags = RoR2Content.Items.GoldOnHit.tags.Add(ItemTag.AIBlacklist);



            RoR2Content.Items.RoboBallBuddy.tags = RoR2Content.Items.RoboBallBuddy.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.MinorConstructOnKill.tags = DLC1Content.Items.MinorConstructOnKill.tags.Add(ItemTag.AIBlacklist);



            RoR2Content.Items.CaptainDefenseMatrix.tags = RoR2Content.Items.CaptainDefenseMatrix.tags.Add(ItemTag.CannotSteal);
            DLC1Content.Items.BearVoid.tags = DLC1Content.Items.BearVoid.tags.Add(ItemTag.BrotherBlacklist);

            RoR2Content.Items.WardOnLevel.tags = RoR2Content.Items.WardOnLevel.tags.Remove(ItemTag.CannotCopy);
            RoR2Content.Items.TonicAffliction.tags = RoR2Content.Items.TonicAffliction.tags.Add(ItemTag.CannotCopy);


            //RoR2Content.Items.TPHealingNova.tags = RoR2Content.Items.TPHealingNova.tags.Remove(ItemTag.CannotCopy);


            /*
            CharacterSpawnCard[] CSCList = FindObjectsOfType(typeof(CharacterSpawnCard)) as CharacterSpawnCard[];
            for (var i = 0; i < CSCList.Length; i++)
            {
                //Debug.LogWarning(CSCList[i]);
                switch (CSCList[i].name)
                {
                    case "cscArchWisp":
                        DirectorCard DC_ArchWisp = new DirectorCard
                        {
                            spawnCard = CSCList[i],
                            selectionWeight = 1,
                            
                            preventOverhead = true,
                            minimumStageCompletions = 0,
                            spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                        };
                        //RoR2Content.mixEnemyMonsterCards.AddCard(1, DC_ArchWisp);  //
                        break;
                    case "cscClayMan":
                        DirectorCard DC_ClayMan = new DirectorCard
                        {
                            spawnCard = CSCList[i],
                            selectionWeight = 1,
                            
                            preventOverhead = false,
                            minimumStageCompletions = 0,
                            spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                        };
                        MoffeinClayMan = CSCList[i].prefab;
                        ClayFamilyEvent.monsterFamilyCategories.AddCategory("Basic Monsters", 6);
                        ClayFamilyEvent.monsterFamilyCategories.AddCard(2, DC_ClayMan);
                        ClayFamilyEvent.minimumStageCompletion = 1;
                        break;
                    case "cscAncientWisp":
                        DirectorCard DC_AncientWisp = new DirectorCard
                        {
                            spawnCard = CSCList[i],
                            selectionWeight = 1,
                            
                            preventOverhead = true,
                            minimumStageCompletions = 0,
                            spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                        };
                        //RoR2Content.mixEnemyMonsterCards.AddCard(0, DC_AncientWisp);  //30
                        break;


                }
            }
            */
        }








        public static void DumpInfo()
        {

            
            foreach (var item in Addressables.ResourceLocators)
            {
                foreach (var key in item.Keys)
                {
                    Debug.LogWarning(key);
                }
            }
            


            DirectorCardCategorySelection[] allinteractables = new DirectorCardCategorySelection[] { dccsGolemplainsInteractablesDLC1, dccsBlackBeachInteractablesDLC1, dccsSnowyForestInteractablesDLC1, dccsGooLakeInteractablesDLC1, dccsFoggySwampInteractablesDLC1, dccsAncientLoftInteractablesDLC1, dccsFrozenWallInteractablesDLC1, dccsWispGraveyardInteractablesDLC1, dccsSulfurPoolsInteractablesDLC1, dccsDampCaveInteractablesDLC1, dccsShipgraveyardInteractablesDLC1, dccsRootJungleInteractablesDLC1, dccsSkyMeadowInteractablesDLC1, dccsArtifactWorldInteractablesDLC1 };

            Debug.LogWarning("");
            Debug.LogWarning("");
            for (int dccs = 0; allinteractables.Length > dccs; dccs++)
            {
                Debug.LogWarning("");
                Debug.LogWarning("--------------------");
                Debug.LogWarning("");
                Debug.LogWarning(allinteractables[dccs].name);


                for (int cat = 0; allinteractables[dccs].categories.Length > cat; cat++)
                {
                    Debug.LogWarning("--[" + cat + "]--" + allinteractables[dccs].categories[cat].name + "--" + "  wt:" + allinteractables[dccs].categories[cat].selectionWeight);
                    for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                    {
                        Debug.LogWarning("[" + card + "] " + allinteractables[dccs].categories[cat].cards[card].spawnCard.name + "  wt:" + allinteractables[dccs].categories[cat].cards[card].selectionWeight + "  minStage:" + allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions);

                    }
                    Debug.LogWarning("");
                }


            }
            Debug.LogWarning("");
            Debug.LogWarning("");



            Debug.LogWarning("");
            DirectorCardCategorySelection[] allmonsters = new DirectorCardCategorySelection[] { dccsGolemplainsMonstersDLC1, dccsBlackBeachMonstersDLC, dccsSnowyForestMonstersDLC1, dccsGooLakeMonstersDLC1, dccsFoggySwampMonstersDLC, dccsAncientLoftMonstersDLC1, dccsFrozenWallMonstersDLC1, dccsWispGraveyardMonstersDLC1, dccsSulfurPoolsMonstersDLC1, dccsDampCaveMonstersDLC1, dccsShipgraveyardMonstersDLC1, dccsRootJungleMonstersDLC1, dccsSkyMeadowMonstersDLC1, dccsITGolemplainsMonsters, dccsITGooLakeMonsters, dccsITAncientLoftMonsters, dccsITFrozenWallMonsters, dccsITDampCaveMonsters, dccsITSkyMeadowMonsters, dccsITMoonMonsters };

            Debug.LogWarning("All Monsters");
            Debug.LogWarning("");
            for (int dccs = 0; allmonsters.Length > dccs; dccs++)
            {
                Debug.LogWarning("");
                Debug.LogWarning("--------------------");
                Debug.LogWarning("");
                Debug.LogWarning(allmonsters[dccs].name);


                for (int cat = 0; allmonsters[dccs].categories.Length > cat; cat++)
                {
                    Debug.LogWarning("--[" + cat + "]--" + allmonsters[dccs].categories[cat].name + "--" + "  wt:" + allmonsters[dccs].categories[cat].selectionWeight);
                    for (int card = 0; allmonsters[dccs].categories[cat].cards.Length > card; card++)
                    {
                        Debug.LogWarning("[" + card + "] " + allmonsters[dccs].categories[cat].cards[card].spawnCard.name + "  wt:" + allmonsters[dccs].categories[cat].cards[card].selectionWeight + "  minStage:" + allmonsters[dccs].categories[cat].cards[card].minimumStageCompletions);

                    }
                    Debug.LogWarning("");
                }


            }
            Debug.LogWarning("");
            Debug.LogWarning("");
        }



        public static void NoOrbitalStrikeBlocking()
        {
            for (int i = 0; i < SceneCatalog.allSceneDefs.Length; i++)
            {
                //Debug.LogWarning(SceneCatalog.allSceneDefs[i] + " " +  SceneCatalog.allSceneDefs[i].stageOrder);

                SceneCatalog.allSceneDefs[i].blockOrbitalSkills = false;

            }
        }



        public static void WolfoSkins()
        {


            /*LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleBody"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleDefault.asset").WaitForCompletion());
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleBody"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleSulfur.asset").WaitForCompletion());
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleGuardBody"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleGuardDefault.asset").WaitForCompletion());
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleGuardBody"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleGuardSulfur.asset").WaitForCompletion());
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleQueen2Body"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleQueen2Default.asset").WaitForCompletion());
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/BeetleQueen2Body"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Beetle/skinBeetleQueen2Sulfur.asset").WaitForCompletion());
            */
            Texture2D texTreebotBlueFlowerDiffuse = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texTreebotBlueFlowerDiffuse.LoadImage(Properties.Resources.texTreebotBlueFlowerDiffuse, false);
            texTreebotBlueFlowerDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueFlowerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueLeafDiffuse = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texTreebotBlueLeafDiffuse.LoadImage(Properties.Resources.texTreebotBlueLeafDiffuse, false);
            texTreebotBlueLeafDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueTreeBarkDiffuse = new Texture2D(256, 1024, TextureFormat.DXT5, false);
            texTreebotBlueTreeBarkDiffuse.LoadImage(Properties.Resources.texTreebotBlueTreeBarkDiffuse, false);
            texTreebotBlueTreeBarkDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texTreebotBlueSkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texTreebotBlueSkinIcon.LoadImage(Properties.Resources.texTreebotBlueSkinIcon, false);
            texTreebotBlueSkinIcon.filterMode = FilterMode.Bilinear;
            Sprite texTreebotBlueSkinIconS = Sprite.Create(texTreebotBlueSkinIcon, rec128, half);

            SkinDef GreenFlowerRex = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];


            CharacterModel.RendererInfo[] REXBlueRenderInfos = new CharacterModel.RendererInfo[4];
            Array.Copy(GreenFlowerRex.rendererInfos, REXBlueRenderInfos, 4);

            Material matREXBlueRobot = Instantiate(GreenFlowerRex.rendererInfos[0].defaultMaterial);
            Material matREXBlueFlower = Instantiate(GreenFlowerRex.rendererInfos[1].defaultMaterial);
            Material matREXBlueLeaf = Instantiate(GreenFlowerRex.rendererInfos[2].defaultMaterial);
            Material matREXBlueBark = Instantiate(GreenFlowerRex.rendererInfos[3].defaultMaterial);

            matREXBlueRobot.color = new Color(0.65f, 0.65f, 0.65f, 1);
            matREXBlueRobot.SetColor("_EmColor", new Color(0.7f, 0.7f, 1f, 1));

            matREXBlueFlower.mainTexture = texTreebotBlueFlowerDiffuse;
            matREXBlueLeaf.mainTexture = texTreebotBlueLeafDiffuse;
            //matREXBlueBark.mainTexture = texTreebotBlueTreeBarkDiffuse;
            matREXBlueBark.color = new Color32(190, 175, 200, 255);

            REXBlueRenderInfos[0].defaultMaterial = matREXBlueRobot;
            REXBlueRenderInfos[1].defaultMaterial = matREXBlueFlower;
            REXBlueRenderInfos[2].defaultMaterial = matREXBlueLeaf;
            REXBlueRenderInfos[3].defaultMaterial = matREXBlueBark;




            LoadoutAPI.SkinDefInfo BlueFlowerRexInfo = new LoadoutAPI.SkinDefInfo
            {
                BaseSkins = GreenFlowerRex.baseSkins,

                NameToken = "Lilly",
                UnlockableDef = null,
                RootObject = GreenFlowerRex.rootObject,
                RendererInfos = REXBlueRenderInfos,
                Name = "skinTreebotWolfo",
                Icon = texTreebotBlueSkinIconS,
            };
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody"), BlueFlowerRexInfo);



            SkinDef CaptainSkinWhite = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];
            //Pink stuff test

            Texture2D texCaptainPinkSkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texCaptainPinkSkinIcon.LoadImage(Properties.Resources.texCaptainPinkSkinIcon, false);
            texCaptainPinkSkinIcon.filterMode = FilterMode.Bilinear;
            Sprite texCaptainPinkSkinIconS = Sprite.Create(texCaptainPinkSkinIcon, rec128, half);

            Texture2D PinktexCaptainJacketDiffuseW = new Texture2D(512, 512, TextureFormat.DXT1, false);
            PinktexCaptainJacketDiffuseW.LoadImage(Properties.Resources.PinktexCaptainJacketDiffuseW, false);
            PinktexCaptainJacketDiffuseW.filterMode = FilterMode.Bilinear;
            PinktexCaptainJacketDiffuseW.wrapMode = TextureWrapMode.Clamp;

            Texture2D PinktexCaptainPaletteW = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW.LoadImage(Properties.Resources.PinktexCaptainPaletteW, false);
            PinktexCaptainPaletteW.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW.wrapMode = TextureWrapMode.Clamp;

            Texture2D PinktexCaptainPaletteW2 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW2.LoadImage(Properties.Resources.PinktexCaptainPaletteW2, false);
            PinktexCaptainPaletteW2.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW2.wrapMode = TextureWrapMode.Clamp;

            //Pallete for HAT
            Texture2D PinktexCaptainPaletteW3 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW3.LoadImage(Properties.Resources.PinktexCaptainPaletteW3, false);
            PinktexCaptainPaletteW3.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW3.wrapMode = TextureWrapMode.Clamp;

            CharacterModel.RendererInfo[] CaptainPinkRenderInfos = new CharacterModel.RendererInfo[7];
            Array.Copy(CaptainSkinWhite.rendererInfos, CaptainPinkRenderInfos, 7);

            Material PinkmatCaptainAlt = Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainAlt2 = Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainAlt3 = Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainArmorAlt = Instantiate(CaptainSkinWhite.rendererInfos[2].defaultMaterial);
            Material PinkmatCaptainJacketAlt = Instantiate(CaptainSkinWhite.rendererInfos[3].defaultMaterial);
            Material PinkmatCaptainRobotBitsAlt = Instantiate(CaptainSkinWhite.rendererInfos[4].defaultMaterial);

            PinkmatCaptainAlt.mainTexture = PinktexCaptainPaletteW;
            PinkmatCaptainAlt2.mainTexture = PinktexCaptainPaletteW2;
            PinkmatCaptainAlt3.mainTexture = PinktexCaptainPaletteW3;
            //PinkmatCaptainArmorAlt.color = new Color32(255, 223, 188, 255);
            PinkmatCaptainArmorAlt.color = new Color32(255, 190, 135, 255);//(255, 195, 150, 255);
            //_EmColor is juts fucking weird
            PinkmatCaptainJacketAlt.mainTexture = PinktexCaptainJacketDiffuseW;
            PinkmatCaptainRobotBitsAlt.SetColor("_EmColor", new Color(1.2f, 0.6f, 1.2f, 1f));
            PinkmatCaptainRobotBitsAlt.color = new Color32(255, 190, 135, 255);

            CaptainPinkRenderInfos[0].defaultMaterial = PinkmatCaptainAlt; //matCaptainAlt
            CaptainPinkRenderInfos[1].defaultMaterial = PinkmatCaptainAlt3; //matCaptainAlt
            CaptainPinkRenderInfos[2].defaultMaterial = PinkmatCaptainArmorAlt; //matCaptainArmorAlt
            CaptainPinkRenderInfos[3].defaultMaterial = PinkmatCaptainJacketAlt; //matCaptainJacketAlt
            CaptainPinkRenderInfos[4].defaultMaterial = PinkmatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainPinkRenderInfos[5].defaultMaterial = PinkmatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainPinkRenderInfos[6].defaultMaterial = PinkmatCaptainAlt2; //matCaptainAlt //Skirt

            LoadoutAPI.SkinDefInfo CaptainPinkSkinInfos = new LoadoutAPI.SkinDefInfo
            {
                BaseSkins = CaptainSkinWhite.baseSkins,

                NameToken = "Honeymoon",
                UnlockableDef = null,
                RootObject = CaptainSkinWhite.rootObject,
                RendererInfos = CaptainPinkRenderInfos,
                Name = "skinCaptainWolfo",
                Icon = texCaptainPinkSkinIconS,
            };
            LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), CaptainPinkSkinInfos);








            GameObject VoidMegaCrabBodyPrefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidMegaCrab/VoidMegaCrabBody.prefab").WaitForCompletion();
            GameObject VoidMegaCrabBodyMdl = VoidMegaCrabBodyPrefab.transform.GetChild(0).GetChild(3).gameObject;


            CharacterModel.RendererInfo[] VoidSuperMegaCrabRenderInfos = new CharacterModel.RendererInfo[4];
            RoR2.SkinDef.MeshReplacement[] VoidSuperMegaCrabMeshReplacements = new RoR2.SkinDef.MeshReplacement[2];

            VoidMegaCrabBodyMdl.GetComponent<CharacterModel>().baseRendererInfos.CopyTo(VoidSuperMegaCrabRenderInfos, 0);
            VoidSuperMegaCrabRenderInfos[2].defaultMaterial = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matVoidSuperMegaCrabShell.mat").WaitForCompletion();
            VoidSuperMegaCrabRenderInfos[3].defaultMaterial = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matVoidSuperMegaCrabMetal.mat").WaitForCompletion();

            GameObject tempmodel = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/mdlVoidSuperMegaCrab.fbx").WaitForCompletion();
            //Debug.LogWarning(tempmodel);
            //Debug.LogWarning(tempmodel.transform.childCount);

            VoidSuperMegaCrabMeshReplacements[0].mesh = Addressables.LoadAssetAsync<Mesh>(key: "RoR2/DLC1/mdlVoidSuperMegaCrab.fbx").WaitForCompletion();
            VoidSuperMegaCrabMeshReplacements[0].renderer = VoidMegaCrabBodyMdl.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();



        }







        public void Awake()
        {
            InitConfig();
            FamilyEventMaker();
            SimuChanges();
            //DumpInfo();
            if (WolfoSkinsConfig.Value == true)
            {
                WolfoSkins();
            }
            if (SkinsGreenMando.Value == true)
            {
                LoadoutAPI.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody"), Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/skinCommandoMarine.asset").WaitForCompletion());
            }

            On.RoR2.UI.MainMenu.MainMenuController.Start += OneTimeOnlyLateRunner;
            RunArtifactManager.onArtifactEnabledGlobal += RunArtifactManager_onArtifactEnabledGlobal;
            RunArtifactManager.onArtifactDisabledGlobal += RunArtifactManager_onArtifactDisabledGlobal;

            //GameObject MoonArenaDynamicPillar = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/moon/MoonArenaDynamicPillar.prefab").WaitForCompletion();
            


            cscBeetleGuardInherit = Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Beetle/cscBeetleGuard.asset").WaitForCompletion());
            cscBeetleGuardInherit.name = "cscBeetleGuardInherit";

            
            On.EntityStates.BeetleQueenMonster.SummonEggs.SummonEgg += (orig, self) =>
            {
                EntityStates.BeetleQueenMonster.SummonEggs.spawnCard = cscBeetleGuardInherit;
                cscBeetleGuardInherit.inventoryToCopy = self.characterBody.inventory;
                orig(self);
                cscBeetleGuardInherit.inventoryToCopy = null;
            };



            Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/Base/Grandparent/bdOverheat.asset").WaitForCompletion().isDebuff = true;

            Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/DLC1/RegeneratingScrap/RegeneratingScrap.asset").WaitForCompletion().canRemove = false;
            //Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/Jetpack/Jetpack.asset").WaitForCompletion().canBeRandomlyTriggered = true;


            /*
            On.EntityStates.Gup.BaseSplitDeath.OnExit += (orig, self) =>
            {
                orig(self);
                if (self.characterBody && self.characterBody.master && self.characterBody.master.IsDeadAndOutOfLivesServer())
                {
                    Destroy(self.characterBody.master.gameObject);
                }
            };
            */
            On.EntityStates.MinorConstruct.DeathState.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.characterBody && self.characterBody.name.Equals("MinorConstructAttachableBody(Clone)") && self.characterBody.master)
                {
                    Destroy(self.characterBody.master.gameObject);
                }
            };


            //Would need to hook into affix void behavior or smth to prevent it with timed buffs
            
            On.RoR2.CharacterBody.OnEquipmentLost += (orig, self, equipmentDef) =>
            {
                if (equipmentDef == DLC1Content.Equipment.EliteVoidEquipment && !self.healthComponent.alive)
                {
                    return;
                }
                orig(self, equipmentDef);
            };
            
            On.RoR2.AffixVoidBehavior.OnEnable += (orig, self) =>
            {
                orig(self);
                //Debug.LogWarning(self);
                foreach (CharacterBody.TimedBuff timedbuff in self.body.timedBuffs)
                {
                    if (timedbuff.buffIndex == DLC1Content.Buffs.EliteVoid.buffIndex)
                    {
                        Destroy(self);
                    }
                }
            };



            On.EntityStates.ScavMonster.FindItem.PickupIsNonBlacklistedItem += (orig, self, pickupIndex) =>
            {
                PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
                if (pickupDef == null)
                {
                    return false;
                }
                ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);
                return !(itemDef == null) && itemDef.DoesNotContainTag(ItemTag.AIBlacklist) && itemDef.DoesNotContainTag(ItemTag.SprintRelated) && itemDef.DoesNotContainTag(ItemTag.OnStageBeginEffect);
            };


            /*
            SimuCrabPointer.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.8679f, 0.4137f, 0.7328f, 1);
            SimuCrabPointer.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.8679f, 0.4137f, 0.7328f, 1);
            SimuCrabPointer.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.8679f, 0.4137f, 0.7328f, 1);
            SimuCrabPointer.transform.GetChild(2).GetChild(0).GetComponent<ObjectScaleCurve>().timeMax = 2f;
            */

         




            EliteDef EliteDefVoid = Addressables.LoadAssetAsync<EliteDef>(key: "RoR2/DLC1/EliteVoid/edVoid.asset").WaitForCompletion();
            EliteDefVoid.healthBoostCoefficient = 2f;
            EliteDef EliteDefLunar = Addressables.LoadAssetAsync<EliteDef>(key: "RoR2/Base/EliteLunar/edLunar.asset").WaitForCompletion();
            EliteDefLunarEulogy.modifierToken = EliteDefLunar.modifierToken;
            EliteDefLunarEulogy.eliteEquipmentDef = EliteDefLunar.eliteEquipmentDef;
            EliteDefLunarEulogy.color = EliteDefLunar.color;
            EliteDefLunarEulogy.shaderEliteRampIndex = EliteDefLunar.shaderEliteRampIndex;
            EliteDefLunarEulogy.healthBoostCoefficient = 3.2f;
            EliteDefLunarEulogy.damageBoostCoefficient = EliteDefLunar.damageBoostCoefficient;
            EliteDefLunarEulogy.name = "edLunarEulogy";

            CombatDirector.EliteTierDef noelitetier = new CombatDirector.EliteTierDef { };
            List<CombatDirector.EliteTierDef> etdList = new List<CombatDirector.EliteTierDef>();
            etdList.Add(noelitetier);
            IEnumerable<CombatDirector.EliteTierDef> empty = etdList;
            EliteAPI.Add(new CustomElite(EliteDefLunarEulogy, empty));



            On.RoR2.ClassicStageInfo.Awake += ClassicStageInfoMethod;
            GameModeCatalog.availability.CallWhenAvailable(ModSupport);
            if (CaptainKeepInHiddemRealm.Value == true)
            {
                SceneCatalog.availability.CallWhenAvailable(NoOrbitalStrikeBlocking);
            }
          

            EquipmentDef VoidAffix = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/DLC1/EliteVoid/EliteVoidEquipment.asset").WaitForCompletion();
            GameObject VoidAffixDisplay = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/EliteVoid/DisplayAffixVoid.prefab").WaitForCompletion(), "PickupAffixVoidW", false);
            VoidAffixDisplay.transform.GetChild(0).GetChild(1).SetAsFirstSibling();
            VoidAffixDisplay.transform.GetChild(1).localPosition = new Vector3(0f, 0.7f, 0f);
            VoidAffixDisplay.transform.GetChild(0).eulerAngles = new Vector3(330, 0, 0);

            LanguageAPI.Add("EQUIPMENT_AFFIXVOID_NAME", "Voidborn Curiosity", "en");
            LanguageAPI.Add("EQUIPMENT_AFFIXVOID_PICKUP", "Lose your aspect of self.", "en");
            LanguageAPI.Add("EQUIPMENT_AFFIXVOID_DESC", "Increases <style=cIsHealing>maximum health</style> by <style=cIsHealing>50%</style> and decrease <style=cIsDamage>base damage</style> by <style=cIsDamage>30%</style>. <style=cIsDamage>Collapse</style> enemies on hit and <style=cIsHealing>block</style> incoming damage once every <style=cIsUtility>15 seconds</style>. ", "en");
            VoidAffix.dropOnDeathChance = 0.00025f;

            Texture2D UniqueAffixVoid = new Texture2D(128, 128, TextureFormat.DXT5, false);
            UniqueAffixVoid.LoadImage(Properties.Resources.UniqueAffixVoid, false);
            UniqueAffixVoid.filterMode = FilterMode.Bilinear;
            UniqueAffixVoid.wrapMode = TextureWrapMode.Clamp;
            Sprite UniqueAffixVoidS = Sprite.Create(UniqueAffixVoid, rec128, half);

            VoidAffix.pickupIconSprite = UniqueAffixVoidS;
            VoidAffix.pickupModelPrefab = VoidAffixDisplay;
            GameModeCatalog.availability.CallWhenAvailable(EquipmentBonusRate);

            On.RoR2.CharacterMaster.RespawnExtraLifeVoid += (orig, self) =>
            {
                orig(self);
                if (self.inventory.currentEquipmentIndex != EquipmentIndex.None && EquipmentCatalog.GetEquipmentDef(self.inventory.currentEquipmentIndex).name.StartsWith("Elite"))
                {
                    CharacterMasterNotificationQueue.PushEquipmentTransformNotification(self, self.inventory.currentEquipmentIndex, DLC1Content.Equipment.EliteVoidEquipment.equipmentIndex, CharacterMasterNotificationQueue.TransformationType.ContagiousVoid);
                    self.inventory.SetEquipment(new EquipmentState(DLC1Content.Equipment.EliteVoidEquipment.equipmentIndex, Run.FixedTimeStamp.negativeInfinity, 0), 0);

                }
            };


            if (EulogyLunarElites.Value == true)
            {
                On.RoR2.CombatDirector.EliteTierDef.GetRandomAvailableEliteDef += (orig, self, rng) =>
                {
                    //Debug.LogWarning("Cost: "+ self.costMultiplier+ " Length: " + self.eliteTypes.Length);
                    if (self.costMultiplier < 8 && self.costMultiplier > 1)
                    {
                        int itemCountGlobal = Util.GetItemCountGlobal(DLC1Content.Items.RandomlyLunar.itemIndex, false, false);
                        if (itemCountGlobal > 0)
                        {
                            itemCountGlobal++;
                            if (rng.nextNormalizedFloat < 0.05f * (float)itemCountGlobal)
                            {
                                Debug.Log("Eulogy Zero Lunar Elite");
                                return EliteDefLunarEulogy;
                            }
                        }
                    }
                    return orig(self, rng);
                };
                LanguageAPI.Add("ITEM_RANDOMLYLUNAR_PICKUP", "Items, equipment and elites have a small chance to transform into a Lunar version instead.", "en");
                LanguageAPI.Add("ITEM_RANDOMLYLUNAR_DESC", "Items and equipment have a <style=cIsUtility>5% <style=cStack>(+5% per stack)</style></style> chance to become a <style=cIsLunar>Lunar</style> item or equipment and Elite Enemies have a <style=cIsUtility>10% <style=cStack>(+5% per stack)</style></style> spawn as <style=cIsLunar>Perfected</style> instead.", "en");

            }










            On.RoR2.BasicPickupDropTable.PassesFilter += BasicPickupDropTable_PassesFilter;

            //Scav
            if (ScavAsProperBoss.Value == true)
            {

                On.RoR2.ScavengerItemGranter.Start += (orig, self) =>
                {
                    orig(self);

                    CharacterBody tempbod = self.GetComponent<CharacterMaster>().GetBody();
                    Inventory tempinv = self.GetComponent<Inventory>();
                    //Debug.LogWarning(tempbod);
                    if (tempbod && tempbod.isBoss)
                    {
                        DeathRewards deathreward = tempbod.GetComponent<DeathRewards>();
                        if (deathreward)
                        {
                            deathreward.bossDropTable = DropTableForBossScav;
                        }
                        ItemDef tempdef = (ItemDef)DropTableForBossScav.pickupEntries[0].pickupDef;
                        if (tempdef.DoesNotContainTag(ItemTag.AIBlacklist) && tempdef.DoesNotContainTag(ItemTag.SprintRelated))
                        {
                            Debug.Log("Giving Boss Scav " + tempdef);
                            tempinv.GiveItem(tempdef, 1);
                        }
                        else
                        {
                            Debug.Log(tempdef + " is Blacklisted for Scavs, resorting to ShinyPearl");
                            tempinv.GiveItem(RoR2Content.Items.ShinyPearl, 1);
                        }
                    }
                    if (Run.instance is InfiniteTowerRun)
                    {
                        PickupIndex pickupIndex = dtAISafeRandomVoid.GenerateDrop(Run.instance.treasureRng);
                        ItemDef itemdef = ItemCatalog.GetItemDef(pickupIndex.itemIndex);
                        Debug.Log("Giving Simu Scav " + itemdef);
                        tempinv.GiveItem(itemdef, 1);
                    }
                };

                RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScav").forbiddenAsBoss = false;
                RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGolem").forbiddenAsBoss = false;
            }

            //ScavThingRandomizer();

            RoR2.CharacterAI.AISkillDriver[] skilllist = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/ScavMaster").GetComponents<RoR2.CharacterAI.AISkillDriver>();
            for (var i = 0; i < skilllist.Length; i++)
            {
                if (skilllist[i].customName.Contains("Sit"))
                {
                    skilllist[i].maxUserHealthFraction = 0.95f;
                    skilllist[i].minUserHealthFraction = 0.15f;
                    //skilllist[i].moveTargetType = RoR2.CharacterAI.AISkillDriver.TargetType.NearestFriendlyInSkillRange;
                    skilllist[i].selectionRequiresOnGround = false;
                    skilllist[i].maxDistance = 1000;
                    skilllist[i].minDistance = 60;
                }
            }





            skilllist = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/EngiWalkerTurretMaster").GetComponents<RoR2.CharacterAI.AISkillDriver>();
            for (var i = 0; i < skilllist.Length; i++)
            {
                if (skilllist[i].customName.Contains("ReturnToLeader"))
                {
                    skilllist[i].shouldSprint = true;
                    if (skilllist[i].minDistance == 110)
                    {
                        skilllist[i].minDistance = 60;
                    }
                }
            }

            //RoR2.LegacyResourcesAPI.Load<GameObject>("prefabs/charactermasters/UrchinTurretMaster")

            //

            //Focused and Beads
            LanguageAPI.Add("ITEM_LUNARTRINKET_DESC", "Once guided to the <style=cIsUtility>monolith</style>, make something fractured whole again and <style=cIsDamage>fight 1 <style=cStack>(+1 per stack)</style> entities</style>", "en");

            //LanguageAPI.Add("ITEM_FOCUSEDCONVERGENCE_DESC", "Teleporters charge <style=cIsUtility>30%<style=cStack> (+30% per stack)</style> faster</style>, but the size of the Teleporter zone is <style=cIsHealth>50%</style><style=cStack> (-50% per stack)</style> smaller down to a minimum radius of 2.5m", "en");
            HoldoutZoneController.FocusConvergenceController.cap = 999999;

            On.RoR2.HoldoutZoneController.Awake += (orig, self) =>
            {
                orig(self);
                if (self.minimumRadius == 0)
                {
                    self.minimumRadius = 3;
                }
            };


            On.RoR2.ScriptedCombatEncounter.BeginEncounter += (orig, self) =>
            {
                if (self.name.Equals("ScavLunarEncounter"))
                {
                    int beadcount = Util.GetItemCountForTeam(TeamIndex.Player, RoR2Content.Items.LunarTrinket.itemIndex, false, true);

                    if (beadcount == 0) { beadcount = 1; }

                    //var x = self.spawns[0].explicitSpawnPosition.localPosition.x;
                    //var y = self.spawns[0].explicitSpawnPosition.localPosition.y;
                    //var z = self.spawns[0].explicitSpawnPosition.localPosition.z;

                    LastCheckedBeadAmount = beadcount;

                    for (int i = 0; i < beadcount; i++)
                    {
                        //self.spawns[0].explicitSpawnPosition.localPosition = new Vector3(x, y, z);
                        //y = y + 10;
                        self.hasSpawnedServer = false;
                        orig(self);
                    }
                    return;

                }

                orig(self);


            };

            On.EntityStates.Missions.LunarScavengerEncounter.FadeOut.OnEnter += (orig, self) =>
            {
                orig(self);

                if (LastCheckedBeadAmount > 1)
                {
                    int temp = LastCheckedBeadAmount;
                    if (temp > 10) { temp = 10; };
                    float mult = EntityStates.Missions.LunarScavengerEncounter.FadeOut.duration / 4 * (temp - 1);

                    self.SetFieldValue<float>("duration", EntityStates.Missions.LunarScavengerEncounter.FadeOut.duration + mult);
                }
            };

            On.EntityStates.ScavMonster.Death.OnPreDestroyBodyServer += (orig, self) =>
            {
                //Debug.LogWarning("On.EntityStates.ScavMonster.OnPreDestroyBodyServer.OnEnter" + self.spawnCard.name + " " + self.outer.name);
                if (self.outer.name.StartsWith("ScavLunar"))
                {
                    //Debug.LogWarning(self.GetType());
                    self.shouldDropPack = true;
                }
                orig(self);
            };






            //On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;

            StupidPriceChanger();

            GameObject Teleporter1 = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Teleporters/Teleporter1");
            GameObject Teleporter2 = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Teleporters/LunarTeleporter Variant");

            //Logger.LogMessage(YellowPercentage.Value * 100 + "% Yellow Percentage");
            Teleporter1.GetComponent<BossGroup>().bossDropChance = YellowPercentage.Value;
            Teleporter2.GetComponent<BossGroup>().bossDropChance = YellowPercentage.Value;

            Teleporter1.GetComponent<TeleporterInteraction>().baseShopSpawnChance = ShopChancePercentage.Value;
            Teleporter2.GetComponent<TeleporterInteraction>().baseShopSpawnChance = ShopChancePercentage.Value;

            Teleporter1.GetComponent<PortalSpawner>().minStagesCleared = 4;

            Teleporter2.GetComponent<PortalSpawner>().minStagesCleared = 4;
            Teleporter2.GetComponent<PortalSpawner>().spawnChance = 0.2f;

            /*
            CharacterBody VoidJailerBody = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidJailer/VoidJailerBody.prefab").WaitForCompletion().GetComponent<CharacterBody>();
            VoidJailerBody.baseArmor = 10;

            CharacterBody VoidMegaCrabBody = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/VoidMegaCrabBody").GetComponent<CharacterBody>();
            VoidMegaCrabBody.baseArmor = 60;
            */

            CharacterBody AffixEarthHealerBody = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/EliteEarth/AffixEarthHealerBody.prefab").WaitForCompletion().GetComponent<CharacterBody>();
            //AffixEarthHealerBody.baseArmor = 60;
            AffixEarthHealerBody.levelDamage = 4;
            AffixEarthHealerBody.levelMaxHealth = 15;

            /*
            dtLockbox.tier2Weight = 7.5f;
            dtLockbox.tier3Weight = 2.5f;
            LanguageAPI.Add("ITEM_TREASURECACHE_DESC", "A <style=cIsUtility>hidden cache</style> containing an item (<style=cIsHealing>75%</style>/<style=cIsHealth>25%</style>) will appear in a random location <style=cIsUtility>on each stage</style>. Opening the cache <style=cIsUtility>consumes</style> this item.", "en");
            */
            BasicPickupDropTable dtLockbox = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/TreasureCache/dtLockbox.asset").WaitForCompletion();
            //dtLockbox.canDropBeReplaced = false;
            //BasicPickupDropTable dtVoidLockbox = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/TreasureCacheVoid/dtVoidLockbox.asset").WaitForCompletion();
            //dtVoidLockbox.canDropBeReplaced = false;
            
            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Chest1StealthedVariant/Chest1StealthedVariant.prefab").WaitForCompletion().GetComponent<RoR2.ChestBehavior>().dropTable = dtLockbox;


            //Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/TreasureCacheVoid/dtVoidLockbox.asset").WaitForCompletion().canDropBeReplaced = false;
            //Addressables.LoadAssetAsync<FreeChestDropTable>(key: "RoR2/DLC1/FreeChest/dtFreeChest.asset").WaitForCompletion().canDropBeReplaced = false;


            //Inherit Elite Equipment
            if (MinionsInherit.Value == true)
            {
                if (DronesInherit.Value == false)
                {
                    On.RoR2.MinionOwnership.MinionGroup.AddMinion += MinionsInheritNoDrones;
                }
                else if (DronesInherit.Value == true)
                {
                    On.RoR2.MinionOwnership.MinionGroup.AddMinion += MinionsInheritWithDrones;
                }
            }





            if (SquidTurretMechanical.Value == true)
            {
                Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Squid/SquidTurretBody.prefab").WaitForCompletion().GetComponent<CharacterBody>().bodyFlags |= CharacterBody.BodyFlags.Mechanical;
            }
        }



        public static IEnumerator DelayedRespawn(PlayerCharacterMasterController playerCharacterMasterController, float delay)
        {
            yield return new WaitForSeconds(delay);
            CharacterBody temp = playerCharacterMasterController.master.GetBody();
            if (temp)
            {
                playerCharacterMasterController.master.Respawn(temp.footPosition, temp.transform.rotation);
            }
            yield break;
        }

        private void RunArtifactManager_onArtifactEnabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            
            if (artifactDef == RoR2Content.Artifacts.randomSurvivorOnRespawnArtifactDef)
            {
                foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
                {
                    playerCharacterMasterController.StartCoroutine(DelayedRespawn(playerCharacterMasterController, 0.25f));
                };
            };
        }

        private void RunArtifactManager_onArtifactDisabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            
            if (artifactDef == RoR2Content.Artifacts.randomSurvivorOnRespawnArtifactDef)
            {
                foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
                {
                    playerCharacterMasterController.SetBodyPrefabToPreference();
                    playerCharacterMasterController.StartCoroutine(DelayedRespawn(playerCharacterMasterController, 0f));
                };
            };
            
        }



        private bool BasicPickupDropTable_PassesFilter(On.RoR2.BasicPickupDropTable.orig_PassesFilter orig, BasicPickupDropTable self, PickupIndex pickupIndex)
        {

            PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
            if (pickupDef.itemIndex != ItemIndex.None)
            {
                ItemDef temp = ItemCatalog.GetItemDef(pickupDef.itemIndex);

                if (self.bannedItemTags.Length > 0)
                {
                    for (int i = 0; i < self.bannedItemTags.Length; i++)
                    {
                        if (temp.ContainsTag(self.bannedItemTags[i]))
                        {
                            return false;
                        }
                    }
                }
                if (self.requiredItemTags.Length > 0)
                {
                    for (int i = 0; i < self.requiredItemTags.Length; i++)
                    {
                        if (!temp.ContainsTag(self.requiredItemTags[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            //Debug.LogWarning(self + "  " + pickupIndex);

            return true;
        }

        private void OneTimeOnlyLateRunner(On.RoR2.UI.MainMenu.MainMenuController.orig_Start orig, RoR2.UI.MainMenu.MainMenuController self)
        {
            orig(self);


            //EntityStates.BeetleQueenMonster.SummonEggs.spawnCard = cscBeetleGuardInherit;


            DLC1Content.Items.RegeneratingScrap.canRemove = false;

            ExplicitPickupDropTable[] ExplicitPickupDropTableList = Resources.FindObjectsOfTypeAll(typeof(ExplicitPickupDropTable)) as ExplicitPickupDropTable[];
            //Debug.LogWarning(ExplicitPickupDropTableList.Length);
            for (int i = 0; i < ExplicitPickupDropTableList.Length; i++)
            {
                //Debug.LogWarning(ExplicitPickupDropTableList[i]);
                ExplicitPickupDropTableList[i].canDropBeReplaced = false;

                if (ExplicitPickupDropTableList[i].name.StartsWith("dtBoss"))
                {
                    foreach (RoR2.ExplicitPickupDropTable.PickupDefEntry entry in ExplicitPickupDropTableList[i].pickupEntries)
                    {
                        ItemDef tempitemdef = (entry.pickupDef as ItemDef);
                        if (tempitemdef && tempitemdef.tier == ItemTier.Boss && tempitemdef.DoesNotContainTag(ItemTag.WorldUnique))
                        {
                            AllScavCompatibleDropTables.Add(ExplicitPickupDropTableList[i]);
                            //Debug.LogWarning(ExplicitPickupDropTableList[i]);
                        }
                    }
                }


            }

            /*
            foreach (RoR2.Items.ContagiousItemManager.TransformationInfo transformationInfo in RoR2.Items.ContagiousItemManager.transformationInfos)
            {
                Debug.LogWarning(" ");
                Debug.LogWarning(transformationInfo);
                Debug.LogWarning(ItemCatalog.GetItemDef(transformationInfo.originalItem));
                Debug.LogWarning(ItemCatalog.GetItemDef(transformationInfo.transformedItem));
            }
            */



            On.RoR2.UI.MainMenu.MainMenuController.Start -= OneTimeOnlyLateRunner;
        }

        public static void EquipmentBonusRate()
        {
            for (int i = 0; i < EliteCatalog.eliteDefs.Length; i++)
            {
                //Debug.LogWarning(EliteCatalog.eliteDefs[i].eliteEquipmentDef);
                if (EliteCatalog.eliteDefs[i].eliteEquipmentDef.dropOnDeathChance == 0.00025f)
                {
                    EliteCatalog.eliteDefs[i].eliteEquipmentDef.dropOnDeathChance = BonusAspectDropRate.Value / 100;
                }
            }


        }



        public void StupidPriceChanger()
        {




            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineHealing").GetComponent<RoR2.PurchaseInteraction>().cost = HealingShrineCost.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineHealing").GetComponent<RoR2.ShrineHealingBehavior>().costMultiplierPerPurchase = HealingShrineMulti.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineGoldshoresAccess").GetComponent<RoR2.PurchaseInteraction>().cost = GoldShrineCost.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/RadarTower").GetComponent<RoR2.PurchaseInteraction>().cost = RadarScannerCost.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Shrines/ShrineBlood").GetComponent<RoR2.ShrineBloodBehavior>().goldToPaidHpRatio = BloodShrineMulti.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/TripleShopEquipment").GetComponent<RoR2.MultiShopController>().baseCost = EquipmentMultishopCost.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/Scrapper").GetComponent<RoR2.ScrapperController>().maxItemsToScrapAtATime = ScrapperAmount.Value;

            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/LunarCauldron, RedToWhite Variant").GetComponent<ShopTerminalBehavior>().dropVelocity = new Vector3(5, 10, 5);

            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BrokenDrones/Drone1Broken").GetComponent<RoR2.PurchaseInteraction>().cost = GunDroneCost.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BrokenDrones/Drone2Broken").GetComponent<RoR2.PurchaseInteraction>().cost = HealingDroneCost.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BrokenDrones/EmergencyDroneBroken").GetComponent<RoR2.PurchaseInteraction>().cost = EmergencyDroneCost.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BrokenDrones/FlameDroneBroken").GetComponent<RoR2.PurchaseInteraction>().cost = FlameDroneCost.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BrokenDrones/MegaDroneBroken").GetComponent<RoR2.PurchaseInteraction>().cost = MegaDroneCost.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BrokenDrones/MissileDroneBroken").GetComponent<RoR2.PurchaseInteraction>().cost = MissileDroneCost.Value;
            RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BrokenDrones/Turret1Broken").GetComponent<RoR2.PurchaseInteraction>().cost = TurretDroneCost.Value;


            if (NoLunarCost.Value == true)
            {
                On.RoR2.PurchaseInteraction.Awake += (orig, self) =>
                {
                    orig(self);
                    if (self.costType == CostTypeIndex.LunarCoin)
                    {
                        self.cost = 0;
                    }
                };
            }

            if (BloodShrineScaleWithTime.Value == true)
            {
                On.RoR2.ShrineBloodBehavior.Start += (orig, self) =>
                {
                    orig(self);
                    self.goldToPaidHpRatio *= Mathf.Pow(Run.instance.difficultyCoefficient, 0.55f);
                };
            }




            if (RedSoupAmount.Value != 0)
            {
                On.RoR2.PurchaseInteraction.SetAvailable += (orig, self, newAvailable) =>
                {
                    if (self.name.Contains("LunarCauldron, RedToWhite Variant"))
                    {
                        if (newAvailable == true)
                        {

                            RedSoupBought = true;
                        }
                    }
                    orig(self, newAvailable);


                };

                On.RoR2.ShopTerminalBehavior.DropPickup += (orig, self) =>
                {
                    orig(self);
                    if (RedSoupBought == true)
                    {
                        if (self.name.Contains("LunarCauldron, RedToWhite Variant"))
                        {
                            for (int i = 0; i < RedSoupAmount.Value; i++)
                            {
                                orig(self);
                            };
                            RedSoupBought = false;
                        }
                    }
                };


            }


        }



        public void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            orig(self, damageReport);

            if (Util.CheckRoll(AspectDropRate, null) && damageReport.victimBody && damageReport.victimBody.isElite)
            {
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(damageReport.victimBody.equipmentSlot.equipmentIndex), damageReport.victimBody.transform.position + Vector3.up * 1.5f, Vector3.up * 20f);
            }
        }


        public static void CreateEquipmentDroneSpawnCards()
        {
            ItemDef AdaptiveArmor = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/AdaptiveArmor/AdaptiveArmor.asset").WaitForCompletion();
            ItemDef BoostHp = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/BoostHp/BoostHp.asset").WaitForCompletion();
            ItemDef MinHealthPercentage = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/MinHealthPercentage/MinHealthPercentage.asset").WaitForCompletion();

            EquipmentDef CommandMissile = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/CommandMissile/CommandMissile.asset").WaitForCompletion();
            EquipmentDef Blackhole = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/Blackhole/Blackhole.asset").WaitForCompletion();
            EquipmentDef BFG = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/BFG/BFG.asset").WaitForCompletion();
            EquipmentDef GummyClone = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/DLC1/GummyClone/GummyClone.asset").WaitForCompletion();
            EquipmentDef Lightning = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/Lightning/Lightning.asset").WaitForCompletion();
            EquipmentDef Molotov = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/DLC1/Molotov/Molotov.asset").WaitForCompletion();
            EquipmentDef FireBallDash = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/FireBallDash/FireBallDash.asset").WaitForCompletion();
            EquipmentDef Saw = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/Saw/Saw.asset").WaitForCompletion();
            EquipmentDef TeamWarCry = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/TeamWarCry/TeamWarCry.asset").WaitForCompletion();
            EquipmentDef VendingMachine = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/DLC1/VendingMachine/VendingMachine.asset").WaitForCompletion();
            EquipmentDef Meteor = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/Meteor/Meteor.asset").WaitForCompletion();
            EquipmentDef CrippleWard = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/CrippleWard/CrippleWard.asset").WaitForCompletion();
            EquipmentDef QuestVolatileBattery = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/QuestVolatileBattery/QuestVolatileBattery.asset").WaitForCompletion();
            EquipmentDef PassiveHealing = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/PassiveHealing/PassiveHealing.asset").WaitForCompletion();


            CharacterSpawnCard cscEquipmentDroneIT = Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscEquipmentDroneIT.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/EquipmentDroneMaster.prefab").WaitForCompletion();
            cscEquipmentDroneIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 }, new ItemCountPair { itemDef = BoostHp, count = 0 } };
            cscEquipmentDroneIT.name = "cscEquipmentDroneIT";

            CharacterSpawnCard cscEquipmentDroneITCommandMissile = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITCommandMissile.name = "cscEquipmentDroneITCommandMissile";
            cscEquipmentDroneITCommandMissile.equipmentToGrant = new EquipmentDef[] { CommandMissile };

            CharacterSpawnCard cscEquipmentDroneITBlackhole = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITBlackhole.name = "cscEquipmentDroneITBlackhole";
            cscEquipmentDroneITBlackhole.equipmentToGrant = new EquipmentDef[] { Blackhole };

            CharacterSpawnCard cscEquipmentDroneITBFG = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITBFG.name = "cscEquipmentDroneITBFG";
            cscEquipmentDroneITBFG.equipmentToGrant = new EquipmentDef[] { BFG };

            CharacterSpawnCard cscEquipmentDroneITGummyClone = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITGummyClone.name = "cscEquipmentDroneITGummyClone";
            cscEquipmentDroneITGummyClone.equipmentToGrant = new EquipmentDef[] { GummyClone };

            CharacterSpawnCard cscEquipmentDroneITLightning = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITLightning.name = "cscEquipmentDroneITLightning";
            cscEquipmentDroneITLightning.equipmentToGrant = new EquipmentDef[] { Lightning };

            CharacterSpawnCard cscEquipmentDroneITMolotov = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITMolotov.name = "cscEquipmentDroneITMolotov";
            cscEquipmentDroneITMolotov.equipmentToGrant = new EquipmentDef[] { Molotov };

            CharacterSpawnCard cscEquipmentDroneITFireBallDash = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITFireBallDash.name = "cscEquipmentDroneITFireBallDash";
            cscEquipmentDroneITFireBallDash.equipmentToGrant = new EquipmentDef[] { FireBallDash };

            CharacterSpawnCard cscEquipmentDroneITSaw = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITSaw.name = "cscEquipmentDroneITSaw";
            cscEquipmentDroneITSaw.equipmentToGrant = new EquipmentDef[] { Saw };

            CharacterSpawnCard cscEquipmentDroneITTeamWarCry = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITTeamWarCry.name = "cscEquipmentDroneITTeamWarCry";
            cscEquipmentDroneITTeamWarCry.equipmentToGrant = new EquipmentDef[] { TeamWarCry };

            CharacterSpawnCard cscEquipmentDroneITVendingMachine = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITVendingMachine.name = "cscEquipmentDroneITVendingMachine";
            cscEquipmentDroneITVendingMachine.equipmentToGrant = new EquipmentDef[] { VendingMachine };

            CharacterSpawnCard cscEquipmentDroneITMeteor = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITMeteor.name = "cscEquipmentDroneITMeteor";
            cscEquipmentDroneITMeteor.equipmentToGrant = new EquipmentDef[] { Meteor };

            CharacterSpawnCard cscEquipmentDroneITCrippleWard = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITCrippleWard.name = "cscEquipmentDroneITCrippleWard";
            cscEquipmentDroneITCrippleWard.equipmentToGrant = new EquipmentDef[] { CrippleWard };

            CharacterSpawnCard cscEquipmentDroneITQuestVolatileBattery = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITQuestVolatileBattery.name = "cscEquipmentDroneITQuestVolatileBattery";
            cscEquipmentDroneITQuestVolatileBattery.equipmentToGrant = new EquipmentDef[] { QuestVolatileBattery };

            CharacterSpawnCard cscEquipmentDroneITPassiveHealing = Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITPassiveHealing.name = "cscEquipmentDroneITPassiveHealing";
            cscEquipmentDroneITPassiveHealing.equipmentToGrant = new EquipmentDef[] { PassiveHealing };


            AllCSCEquipmentDronesIT = new CharacterSpawnCard[] { 
                //cscEquipmentDroneITCommandMissile, 
                //cscEquipmentDroneITLightning,
                //cscEquipmentDroneITBFG,
                //cscEquipmentDroneITBlackhole,
                cscEquipmentDroneITGummyClone,
                cscEquipmentDroneITMolotov, 
                //cscEquipmentDroneITFireBallDash,
                //cscEquipmentDroneITSaw, 
                cscEquipmentDroneITTeamWarCry,
                cscEquipmentDroneITVendingMachine,
                cscEquipmentDroneITMeteor,
                 //cscEquipmentDroneITCrippleWard,
                cscEquipmentDroneITQuestVolatileBattery,
                //cscEquipmentDroneITPassiveHealing,
            };

        }




        public static void SimuChanges()
        {
            /*
            On.RoR2.InfiniteTowerSafeWardController.Awake += (orig, self) =>
            {
                orig(self);

                if (self.positionIndicator)
                {
                    GameObject PositionTransform  = new GameObject("PositionTransform");
                    PositionTransform.transform.SetParent(self.gameObject.transform);
                    PositionTransform.transform.localPosition = new Vector3(0, 0.5f, 0);
                    self.positionIndicator.targetTransform = PositionTransform.transform;
                }
            };
            */



            SimuEndingEveryXWaves = ConfigSimuEndingEveryXWaves.Value;
            SimuEndingStartAtXWaves = ConfigSimuEndingStartAtXWaves.Value;
            SimuEndingWaveRest = ConfigSimuEndingStartAtXWaves.Value % ConfigSimuEndingEveryXWaves.Value;

            SimuForcedBossEveryXWaves = ConfigSimuForcedBossEveryXWaves.Value;
            SimuForcedBossStartAtXWaves = ConfigSimuForcedBossStartAtXWaves.Value;
            SimuForcedBossWaveRest = ConfigSimuForcedBossStartAtXWaves.Value % ConfigSimuForcedBossEveryXWaves.Value;


            GameEndingDef VoidEnding = Addressables.LoadAssetAsync<GameEndingDef>(key: "RoR2/DLC1/GameModes/VoidEnding.asset").WaitForCompletion();
            GameEndingDef ObliterationEnding = Addressables.LoadAssetAsync<GameEndingDef>(key: "RoR2/Base/ClassicRun/ObliterationEnding.asset").WaitForCompletion();

            ContentAddition.AddGameEndingDef(InfiniteTowerEnding);

           


            InfiniteTowerEnding.endingTextToken = "Simulation Suspended";
            InfiniteTowerEnding.lunarCoinReward = 5;
            InfiniteTowerEnding.showCredits = false;
            InfiniteTowerEnding.isWin = false;
            InfiniteTowerEnding.gameOverControllerState = ObliterationEnding.gameOverControllerState;
            InfiniteTowerEnding.material = VoidEnding.material;
            InfiniteTowerEnding.icon = VoidEnding.icon;
            InfiniteTowerEnding.backgroundColor = new Color(0.65f, 0.3f, 0.5f, 0.8f);
            InfiniteTowerEnding.foregroundColor = new Color(0.75f, 0.4f, 0.55f, 1);

            InfiniteTowerEnding.cachedName = "InfiniteTowerEnding";
           





            On.RoR2.InfiniteTowerRun.OnWaveAllEnemiesDefeatedServer += InfiniteTowerRun_OnWaveAllEnemiesDefeatedServer;

            On.RoR2.InfiniteTowerWaveCategory.SelectWavePrefab += InfiniteTowerWaveCategory_SelectWavePrefab;
            On.RoR2.InfiniteTowerRun.BeginNextWave += InfiniteTowerRun_BeginNextWave;
            On.RoR2.InfiniteTowerWaveController.OnAllEnemiesDefeatedServer += InfiniteTowerWaveController_OnAllEnemiesDefeatedServer;
            On.RoR2.InfiniteTowerRun.CleanUpCurrentWave += InfiniteTowerRun_CleanUpCurrentWave;

            //On.EntityStates.InfiniteTowerSafeWard.Active.OnEnter += Active_OnEnter;
            On.EntityStates.InfiniteTowerSafeWard.AwaitingActivation.OnEnter += AwaitingActivation_OnEnter;
            On.EntityStates.InfiniteTowerSafeWard.Travelling.OnEnter += Travelling_OnEnter;
            On.EntityStates.InfiniteTowerSafeWard.Burrow.OnEnter += Burrow_OnEnter;
            On.EntityStates.InfiniteTowerSafeWard.Unburrow.OnEnter += (orig, self) =>
            {
                orig(self);


                //Debug.LogWarning(self.duration);
                var temp = Run.instance.GetComponent<InfiniteTowerRun>().waveInstance;
                if (temp)
                {
                    self.duration = temp.GetComponent<InfiniteTowerWaveController>().secondsAfterWave;
                }
            };

            On.RoR2.ClassicStageInfo.RebuildCards += ClassicStageInfo_RebuildCards;


            ItemTag[] TagsAISafe = { ItemTag.AIBlacklist, ItemTag.SprintRelated, ItemTag.PriorityScrap, ItemTag.InteractableRelated, ItemTag.HoldoutZoneRelated, ItemTag.OnStageBeginEffect };
            ItemTag[] TagsMonsterTeamGain = { ItemTag.AIBlacklist, ItemTag.OnKillEffect, ItemTag.EquipmentRelated, ItemTag.SprintRelated, ItemTag.PriorityScrap, ItemTag.InteractableRelated, ItemTag.HoldoutZoneRelated, ItemTag.Count };
            ItemTag[] TagArenaMonsters = { ItemTag.AIBlacklist, ItemTag.OnKillEffect, ItemTag.EquipmentRelated, ItemTag.SprintRelated, ItemTag.PriorityScrap, ItemTag.InteractableRelated, ItemTag.OnStageBeginEffect, ItemTag.HoldoutZoneRelated, ItemTag.Count };

            dtMonsterTeamTier1Item.bannedItemTags = TagsMonsterTeamGain;
            dtMonsterTeamTier2Item.bannedItemTags = TagsMonsterTeamGain;
            dtMonsterTeamTier3Item.bannedItemTags = TagsMonsterTeamGain;

            dtArenaMonsterTier1.bannedItemTags = TagsMonsterTeamGain;
            dtArenaMonsterTier2.bannedItemTags = TagsMonsterTeamGain;
            dtArenaMonsterTier3.bannedItemTags = TagsMonsterTeamGain;

            dtAISafeTier1Item.bannedItemTags = TagsAISafe;
            dtAISafeTier2Item.bannedItemTags = TagsAISafe;
            dtAISafeTier3Item.bannedItemTags = TagsAISafe;
            dtAISafeRandomVoid.bannedItemTags = TagsAISafe;

            dtAISafeRandomVoid.tier1Weight = 0;
            dtAISafeRandomVoid.tier2Weight = 0;
            dtAISafeRandomVoid.tier3Weight = 0;
            dtAISafeRandomVoid.voidTier1Weight = 6;
            dtAISafeRandomVoid.voidTier2Weight = 3;
            dtAISafeRandomVoid.voidTier3Weight = 1;
            dtAISafeRandomVoid.canDropBeReplaced = false;
            dtAISafeRandomVoid.name = "dtAISafeRandomVoid";

            if (SimulacrumEnemyItemChanges.Value == true)
            {
                InfiniteTowerRunBase.enemyItemPattern[0].dropTable = dtMonsterTeamTier1Item;
                InfiniteTowerRunBase.enemyItemPattern[1].dropTable = dtMonsterTeamTier1Item;
                InfiniteTowerRunBase.enemyItemPattern[2].dropTable = dtMonsterTeamTier2Item;
                InfiniteTowerRunBase.enemyItemPattern[3].dropTable = dtMonsterTeamTier2Item;
                InfiniteTowerRunBase.enemyItemPattern[4].dropTable = dtMonsterTeamTier3Item;
            }


            On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += (orig, self, sceneDirector) =>
            {
                orig(self, sceneDirector);
                float num = 0.5f + (float)Run.instance.participatingPlayerCount * 0.5f;
                if (SimuMultiplayerChanges.Value == false)
                {
                    num = 1;
                }
                sceneDirector.interactableCredit += Math.Max((int)(sceneDirector.interactableCredit * -0.1f) , 100 - self.waveIndex/10 * 30);
                sceneDirector.interactableCredit = (int)(num * sceneDirector.interactableCredit);
                if (RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.sacrificeArtifactDef))
                {
                    sceneDirector.interactableCredit /= 2;
                }
                sceneDirector.interactableCredit = Math.Min(sceneDirector.interactableCredit, 8400);

                Debug.Log("InfiniteTower "+ sceneDirector.interactableCredit + " interactable credits.");
            };


            if (SimuMultiplayerChanges.Value == true)
            {
                On.RoR2.InfiniteTowerRun.RecalculateDifficultyCoefficentInternal += (orig, self) =>
                {
                    DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(self.selectedDifficulty);

                    float mpSnum = 1.01f + (float)self.participatingPlayerCount * 0.01f;
                    float num = 1.5f * (float)self.waveIndex;
                    float num2 = 0.0506f * difficultyDef.scalingValue;
                    float num3 = Mathf.Pow(mpSnum, (float)self.waveIndex);
                    self.difficultyCoefficient = (1f + num2 * num) * num3;
                    self.compensatedDifficultyCoefficient = self.difficultyCoefficient;
                    self.ambientLevel = Mathf.Min((self.difficultyCoefficient - 1f) / 0.33f + 1f, 9999f);
                    int ambientLevelFloor = self.ambientLevelFloor;
                    self.ambientLevelFloor = Mathf.FloorToInt(self.ambientLevel);
                    if (ambientLevelFloor != self.ambientLevelFloor && ambientLevelFloor != 0 && self.ambientLevelFloor > ambientLevelFloor)
                    {
                        self.OnAmbientLevelUp();
                    }
                };
            }

            


            //Spawn Cards
            ItemDef AdaptiveArmor = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/AdaptiveArmor/AdaptiveArmor.asset").WaitForCompletion();
            ItemDef BoostHp = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/BoostHp/BoostHp.asset").WaitForCompletion();

            CharacterSpawnCard cscTitanGoldIT = Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscTitanGoldIT.name = "cscTitanGoldIT";
            cscTitanGoldIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 }, new ItemCountPair { itemDef = BoostHp, count = 2 } };

            CharacterSpawnCard cscSuperRoboBallBossIT = Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/RoboBallBoss/cscSuperRoboBallBoss.asset").WaitForCompletion());
            cscSuperRoboBallBossIT.name = "cscSuperRoboBallBossIT";
            cscSuperRoboBallBossIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 }, new ItemCountPair { itemDef = BoostHp, count = 0 } };

            MultiCharacterSpawnCard cscScavLunarIT = Instantiate(Addressables.LoadAssetAsync<MultiCharacterSpawnCard>(key: "RoR2/Base/ScavLunar/cscScavLunar.asset").WaitForCompletion());
            cscScavLunarIT.name = "cscScavLunarIT";
            cscScavLunarIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 0 }, new ItemCountPair { itemDef = BoostHp, count = 0 } };

            CharacterSpawnCard cscMiniVoidRaidCrabPhase3IT = Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidRaidCrab/cscMiniVoidRaidCrabPhase3.asset").WaitForCompletion());
            cscMiniVoidRaidCrabPhase3IT.name = "cscMiniVoidRaidCrabPhase3IT";

            cscVoidInfestorIT = Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/EliteVoid/cscVoidInfestor.asset").WaitForCompletion());
            cscVoidInfestorIT.name = "cscVoidInfestorIT";
            cscVoidInfestorIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 }, new ItemCountPair { itemDef = BoostHp, count = 10 } };
            DirectorCard SimuWaveVoidInfestor = new DirectorCard
            {
                spawnCard = cscVoidInfestorIT,
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            dccsVoidInfestorOnly.AddCategory("Basic Monsters", 6);
            dccsVoidInfestorOnly.AddCard(0, SimuWaveVoidInfestor);
            dccsVoidInfestorOnly.name = "dccsVoidInfestorOnly";
            dccsVoidInfestorOnly.categories[0].cards[0].spawnCard = cscVoidInfestorIT;


            CreateEquipmentDroneSpawnCards();







            InteractableSpawnCard SoupWhiteGreenISC = ScriptableObject.CreateInstance<InteractableSpawnCard>();
            SoupWhiteGreenISC.name = "iscSoupWhiteGreen";
            SoupWhiteGreenISC.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/LunarCauldron, WhiteToGreen");
            SoupWhiteGreenISC.sendOverNetwork = true;
            SoupWhiteGreenISC.hullSize = HullClassification.Golem;
            SoupWhiteGreenISC.nodeGraphType = MapNodeGroup.GraphType.Ground;
            SoupWhiteGreenISC.requiredFlags = NodeFlags.None;
            SoupWhiteGreenISC.forbiddenFlags = NodeFlags.NoChestSpawn;
            SoupWhiteGreenISC.directorCreditCost = 9;
            SoupWhiteGreenISC.occupyPosition = true;
            SoupWhiteGreenISC.eliteRules = SpawnCard.EliteRules.Default;
            SoupWhiteGreenISC.orientToFloor = true;
            SoupWhiteGreenISC.slightlyRandomizeOrientation = false;
            SoupWhiteGreenISC.skipSpawnWhenSacrificeArtifactEnabled = false;

            InteractableSpawnCard SoupGreenRedISC = ScriptableObject.CreateInstance<InteractableSpawnCard>();
            SoupGreenRedISC.name = "iscSoupGreenRed";
            SoupGreenRedISC.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/LunarCauldron, GreenToRed Variant");
            SoupGreenRedISC.sendOverNetwork = true;
            SoupGreenRedISC.hullSize = HullClassification.Golem;
            SoupGreenRedISC.nodeGraphType = MapNodeGroup.GraphType.Ground;
            SoupGreenRedISC.requiredFlags = NodeFlags.None;
            SoupGreenRedISC.forbiddenFlags = NodeFlags.NoChestSpawn;
            SoupGreenRedISC.directorCreditCost = 6;
            SoupGreenRedISC.occupyPosition = true;
            SoupGreenRedISC.eliteRules = SpawnCard.EliteRules.Default;
            SoupGreenRedISC.orientToFloor = true;
            SoupGreenRedISC.slightlyRandomizeOrientation = false;
            SoupGreenRedISC.skipSpawnWhenSacrificeArtifactEnabled = false;

            InteractableSpawnCard SoupRedWhiteISC = ScriptableObject.CreateInstance<InteractableSpawnCard>();
            SoupRedWhiteISC.name = "iscSoupRedWhite";
            SoupRedWhiteISC.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/LunarCauldron, RedToWhite Variant");
            SoupRedWhiteISC.sendOverNetwork = true;
            SoupRedWhiteISC.hullSize = HullClassification.Golem;
            SoupRedWhiteISC.nodeGraphType = MapNodeGroup.GraphType.Ground;
            SoupRedWhiteISC.requiredFlags = NodeFlags.None;
            SoupRedWhiteISC.forbiddenFlags = NodeFlags.NoChestSpawn;
            SoupRedWhiteISC.directorCreditCost = 3;
            SoupRedWhiteISC.occupyPosition = true;
            SoupRedWhiteISC.eliteRules = SpawnCard.EliteRules.Default;
            SoupRedWhiteISC.orientToFloor = true;
            SoupRedWhiteISC.slightlyRandomizeOrientation = false;
            SoupRedWhiteISC.skipSpawnWhenSacrificeArtifactEnabled = false;



            SceneDef itgolemplains = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itgolemplains/itgolemplains.asset").WaitForCompletion();
            SceneDef itfrozenwall = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itfrozenwall/itfrozenwall.asset").WaitForCompletion();
            SceneDef itdampcave = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itdampcave/itdampcave.asset").WaitForCompletion();
            SceneDef rootjungle = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/Base/rootjungle/rootjungle.asset").WaitForCompletion();
            SceneDef wispgraveyard = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/Base/wispgraveyard/wispgraveyard.asset").WaitForCompletion();
            SceneDef ancientloft = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/ancientloft/ancientloft.asset").WaitForCompletion();
            SceneDef dampcavesimple = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/Base/dampcavesimple/dampcavesimple.asset").WaitForCompletion();
            SceneDef goolake = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/Base/goolake/goolake.asset").WaitForCompletion();

            MusicTrackDef MusicVoidFields = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/Base/Common/muSong08.asset").WaitForCompletion();
            MusicTrackDef MusicVoidStage = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_08.asset").WaitForCompletion();
            MusicTrackDef MusicSnowyForest = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_03.asset").WaitForCompletion();
            MusicTrackDef MusicSulfurPoolsBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muBossfightDLC1_12.asset").WaitForCompletion();

            itgolemplains.mainTrack = MusicVoidFields;
            //itfrozenwall.mainTrack = MusicSnowyForest;
            itdampcave.mainTrack = MusicVoidStage;
            rootjungle.bossTrack = MusicSulfurPoolsBoss;
            //wispgraveyard.mainTrack = ancientloft.mainTrack;
            //goolake.bossTrack = dampcavesimple.bossTrack;



            //
            //Wave Tokens
            LanguageAPI.Add("INFINITETOWER_WAVE_BEGIN_ARTIFACT_RANDOMLOADOUT", "<style=cWorldEvent>[WARNING] Replicating combat test with <style=cArtifact>Transpose</style> augment..</style>", "en");
            LanguageAPI.Add("INFINITETOWER_WAVE_BEGIN_ARTIFACT_SINGLEELITETYPE", "<style=cWorldEvent>[WARNING] Replicating combat test with <style=cArtifact>Brigade</style> augment..</style>", "en");

            LanguageAPI.Add("INFINITETOWER_WAVE_BEGIN_ARTIFACT_RANDOMSURVIVOR", "<style=cWorldEvent>[WARNING] Replicating combat test with <style=cArtifact>Metamorphosis</style> augment..</style>", "en");
            LanguageAPI.Add("INFINITETOWER_WAVE_BEGIN_ARTIFACT_SACRIFICE", "<style=cWorldEvent>[WARNING] Replicating combat test with <style=cArtifact>Sacrifice</style> augment..</style>", "en");
            LanguageAPI.Add("INFINITETOWER_WAVE_BEGIN_ARTIFACT_EVOLUTION", "<style=cWorldEvent>[WARNING] Replicating combat test with <style=cArtifact>Evolution</style> augment..</style>", "en");

            LanguageAPI.Add("INFINITETOWER_WAVE_BEGIN_ARTIFACT_ELITEONLY", "<style=cWorldEvent>[WARNING] Replicating advanced combat test with <style=cArtifact>Honor</style> augment..</style>", "en");
            LanguageAPI.Add("INFINITETOWER_WAVE_BEGIN_ARTIFACT_INVADINGDOPPELGANGER", "<style=cWorldEvent>[WARNING] Replicating doppelganger combat test with <style=cArtifact>Vengence</style> augment..</style>", "en");




            //


            // (0) (0) Chest1 (240)
            // (0) (1) Chest2 (40)
            // (0) (2) Equipment Barrel (20)
            // (0) (3) Triple Shop (80)
            // (0) (4) Lunar Chest (10)
            // (0) (5) Category1 Damage (20)
            // (0) (6) Category1 Healing (20)
            // (0) (7) Category1 Utility (20)
            // (0) (8) Casino (10)
            // (0) (9) Equipment Triple Shop (20)
            //////(1) Shrines (1)
            // (1) (0) Blood Shrine (20)

            //////(2) Rare (0.4)
            // 
            //

            //////(3) Dupli (8)
            //(3,0) Duplicator [30]
            //(3,1) DuplicatorLarge [6]
            //(3,2) DuplicatorMilitary [1]
            //(3,3) DuplicatorWild [2]
            //(3,4) Scrapper [12]



            DirectorCard ADVoidTriple = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidTriple/iscVoidTriple.asset").WaitForCompletion(),
                selectionWeight = 1,
            };



            dccsInfiniteTowerInteractables.categories[1].selectionWeight = 1.5f;
            dccsInfiniteTowerInteractables.categories[3].cards[1].selectionWeight = 8;
            dccsInfiniteTowerInteractables.categories[3].cards[2].selectionWeight = 2;
            dccsInfiniteTowerInteractables.categories[3].cards[2].minimumStageCompletions = 1;
            dccsInfiniteTowerInteractables.categories[3].cards[3].minimumStageCompletions = 1;
            dccsInfiniteTowerInteractables.categories[3].cards[4].selectionWeight = 16;

            dccsInfiniteTowerInteractables.categories[4].selectionWeight = 4.5f;
            dccsInfiniteTowerInteractables.categories[4].cards[0].selectionWeight = 3;
            dccsInfiniteTowerInteractables.AddCard(4, ADVoidTriple);


            dccsInfiniteTowerInteractables.categories[0].cards = dccsInfiniteTowerInteractables.categories[0].cards.Remove(dccsInfiniteTowerInteractables.categories[0].cards[8], dccsInfiniteTowerInteractables.categories[0].cards[7], dccsInfiniteTowerInteractables.categories[0].cards[6], dccsInfiniteTowerInteractables.categories[0].cards[5]);

            dccsInfiniteTowerInteractables.categories[2].cards = dccsInfiniteTowerInteractables.categories[2].cards.Remove(dccsInfiniteTowerInteractables.categories[2].cards[0]);
            dccsInfiniteTowerInteractables.categories[2].selectionWeight = 0.02f;
            dccsInfiniteTowerInteractables.categories[2].cards[0].minimumStageCompletions = 1;

            dccsITGolemPlainsInteractablesW = Instantiate(dccsInfiniteTowerInteractables);
            dccsITGooLakeInteractablesW = Instantiate(dccsInfiniteTowerInteractables);
            dccsITAncientLoftInteractablesW = Instantiate(dccsInfiniteTowerInteractables);
            dccsITFrozenWallInteractablesW = Instantiate(dccsInfiniteTowerInteractables);
            dccsITDampCaveInteractablesW = Instantiate(dccsInfiniteTowerInteractables);
            dccsITSkyMeadowInteractablesW = Instantiate(dccsInfiniteTowerInteractables);
            dccsITMoonInteractablesW = Instantiate(dccsInfiniteTowerInteractables);


            dccsITGolemPlainsInteractablesW.name = "dccsITGolemPlainsInteractablesW";
            dccsITGooLakeInteractablesW.name = "dccsITGooLakeInteractablesW";
            dccsITAncientLoftInteractablesW.name = "dccsITAncientLoftInteractablesW";
            dccsITFrozenWallInteractablesW.name = "dccsITFrozenWallInteractablesW";
            dccsITDampCaveInteractablesW.name = "dccsITDampCaveInteractablesW";
            dccsITSkyMeadowInteractablesW.name = "dccsITSkyMeadowInteractablesW";
            dccsITMoonInteractablesW.name = "dccsITMoonInteractablesW";




            DirectorCard ADBrokenMegaDrone = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<InteractableSpawnCard>("spawncards/interactablespawncard/iscBrokenMegaDrone"),
                selectionWeight = 4,
                minimumStageCompletions = 4,
            };

            DirectorCard ADCategoryChest1Damage = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/CategoryChest/iscCategoryChestDamage.asset").WaitForCompletion(),
                selectionWeight = 60,
            };
            DirectorCard ADCategoryChest1Healing = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/CategoryChest/iscCategoryChestHealing.asset").WaitForCompletion(),
                selectionWeight = 60,
            };
            DirectorCard ADCategoryChest1Utility = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/CategoryChest/iscCategoryChestUtility.asset").WaitForCompletion(),
                selectionWeight = 60,
            };


            DirectorCard ADCategoryChest2Damage = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                selectionWeight = 9,
            };
            DirectorCard ADCategoryChest2Healing = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Healing.asset").WaitForCompletion(),
                selectionWeight = 9,
            };
            DirectorCard ADCategoryChest2Utility = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Utility.asset").WaitForCompletion(),
                selectionWeight = 9,
            };
            DirectorCard ADCategoryChest2Damage2 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                selectionWeight = 2,
            };
            DirectorCard ADCategoryChest2Healing2 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Healing.asset").WaitForCompletion(),
                selectionWeight = 2,
            };
            DirectorCard ADCategoryChest2Utility2 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Utility.asset").WaitForCompletion(),
                selectionWeight = 2,
            };
            DirectorCard ADGreenMultiShop = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/TripleShopLarge/iscTripleShopLarge.asset").WaitForCompletion(),
                selectionWeight = 20,
            };
            DirectorCard ADGreenMultiShop40 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/TripleShopLarge/iscTripleShopLarge.asset").WaitForCompletion(),
                selectionWeight = 40,
            };

            DirectorCard ADAdaptiveChest = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/CasinoChest/iscCasinoChest.asset").WaitForCompletion(),
                selectionWeight = 10,
            };


            DirectorCard ADShrineChance = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineChance/iscShrineChance.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard ADShrineChanceSandy = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineChance/iscShrineChanceSandy.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard ADShrineChanceSnowy = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineChance/iscShrineChanceSnowy.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard ADShrineCleanse = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                selectionWeight = 2,
            };
            DirectorCard ADShrineCleanse1 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                selectionWeight = 1,
            };
            DirectorCard ADShrineHealing = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineHealing/iscShrineHealing.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard ADShrineBoss10 = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBoss/iscShrineBoss.asset").WaitForCompletion(),
                selectionWeight = 14,
            };
            DirectorCard ADShrineOrder = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion(),
                selectionWeight = 2,
            };
            DirectorCard ADSoupWhiteGreen = new DirectorCard
            {
                spawnCard = SoupWhiteGreenISC,
                selectionWeight = 20,
            };
            DirectorCard ADSoupGreenRed = new DirectorCard
            {
                spawnCard = SoupGreenRedISC,
                selectionWeight = 10,
            };
            DirectorCard ADSoupRedWhite = new DirectorCard
            {
                spawnCard = SoupRedWhiteISC,
                selectionWeight = 5,
            };



            //GolemPlains (Healing)
            //GooLake (Damage)
            //AncientLoft (Utility)
            //FrozenWall (Utility)
            //DampCave (Damage)
            //SkyMeadow (Healing)

            dccsArtifactWorldInteractablesDLC1.AddCard(0, ADCategoryChest2Damage2);
            dccsArtifactWorldInteractablesDLC1.AddCard(0, ADCategoryChest2Healing2);
            dccsArtifactWorldInteractablesDLC1.AddCard(0, ADCategoryChest2Utility2);
            //
            dccsITGolemPlainsInteractablesW.AddCard(0, ADCategoryChest1Healing);
            dccsITGolemPlainsInteractablesW.AddCard(0, ADCategoryChest2Healing);

            dccsITGolemPlainsInteractablesW.categories[1].cards[0] = ADShrineChance;
            dccsITGolemPlainsInteractablesW.AddCard(1, ADShrineHealing);
            dccsITGolemPlainsInteractablesW.categories[1].selectionWeight = 4;
            //
            dccsITGooLakeInteractablesW.AddCard(0, ADCategoryChest1Damage);
            dccsITGooLakeInteractablesW.AddCard(0, ADCategoryChest2Damage);
            dccsITGooLakeInteractablesW.AddCard(0, ADGreenMultiShop);

            dccsITGooLakeInteractablesW.categories[1].selectionWeight = 3f;
            dccsITGooLakeInteractablesW.AddCard(1, ADShrineChance);

            //dccsITGooLakeInteractablesW.AddCard(1, ADShrineHealing);
            //
            dccsITAncientLoftInteractablesW.AddCard(0, ADCategoryChest1Utility);
            dccsITAncientLoftInteractablesW.AddCard(0, ADCategoryChest2Utility);

            dccsITAncientLoftInteractablesW.categories[1].cards[0] = ADShrineCleanse;
            dccsITAncientLoftInteractablesW.AddCard(1, ADShrineHealing);
            dccsITAncientLoftInteractablesW.categories[1].selectionWeight = 6.5f;
            //
            dccsITFrozenWallInteractablesW.AddCard(0, ADCategoryChest1Utility);
            dccsITFrozenWallInteractablesW.AddCard(0, ADCategoryChest2Utility);

            dccsITFrozenWallInteractablesW.AddCard(0, ADAdaptiveChest);
            dccsITFrozenWallInteractablesW.categories[1].selectionWeight = 4f;
            dccsITFrozenWallInteractablesW.categories[1].cards[0].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBloodSnowy.asset").WaitForCompletion();
            dccsITFrozenWallInteractablesW.AddCard(1, ADShrineChanceSnowy);
            //
            dccsITDampCaveInteractablesW.AddCard(0, ADCategoryChest1Damage);
            dccsITDampCaveInteractablesW.AddCard(0, ADCategoryChest2Damage);

            dccsITDampCaveInteractablesW.AddCard(0, ADGreenMultiShop);

            //
            dccsITSkyMeadowInteractablesW.AddCard(0, ADCategoryChest1Healing);
            dccsITSkyMeadowInteractablesW.AddCard(0, ADCategoryChest2Healing);

            dccsITSkyMeadowInteractablesW.AddCard(0, ADGreenMultiShop);
            //
            dccsITMoonInteractablesW.categories[2].selectionWeight = 0.06f;
            dccsITMoonInteractablesW.categories[4].selectionWeight = 5.5f;

            dccsITMoonInteractablesW.AddCard(0, ADGreenMultiShop40);

            dccsITMoonInteractablesW.categories[0].cards[4].selectionWeight = 40;
            dccsITMoonInteractablesW.categories[0].cards = dccsITMoonInteractablesW.categories[0].cards.Remove(dccsITMoonInteractablesW.categories[0].cards[5]);
            dccsITMoonInteractablesW.categories[3].cards = dccsITMoonInteractablesW.categories[3].cards.Remove(dccsITMoonInteractablesW.categories[3].cards[3], dccsITMoonInteractablesW.categories[3].cards[2], dccsITMoonInteractablesW.categories[3].cards[1], dccsITMoonInteractablesW.categories[3].cards[0]);
            dccsITMoonInteractablesW.AddCard(3, ADSoupRedWhite);
            dccsITMoonInteractablesW.AddCard(3, ADSoupGreenRed);
            dccsITMoonInteractablesW.AddCard(3, ADSoupWhiteGreen);

            dccsITMoonInteractablesW.categories[4].selectionWeight = 9;

            dccsITMoonInteractablesW.categories[1].selectionWeight = 3.5f;
            dccsITMoonInteractablesW.categories[1].cards[0] = ADShrineOrder;


            

            

            //Drop Pools
            dtITSpecialBossWave.tier3Weight = 90;
            dtITSpecialBossWave.bossWeight = 10;

            dtITVoid.voidTier1Weight = 60;
            dtITVoid.voidTier2Weight = 30;
            dtITVoid.voidTier3Weight = 10;
            dtITVoid.voidBossWeight = 5;

            
            dtAllTier.name = "dtAllTier";
            dtAllTier.tier1Weight = 80;
            dtAllTier.tier2Weight = 20;
            dtAllTier.tier3Weight = 2;
            dtAllTier.bossWeight = 4;
            dtAllTier.equipmentWeight = 15;
            dtAllTier.lunarItemWeight = 10;
            dtAllTier.voidTier1Weight = 50;
            dtAllTier.voidTier2Weight = 15;
            dtAllTier.voidTier3Weight = 4;
            //dtAllTier.voidBossWeight = 2;


            dtITSpecialLunarBoss.tier1Weight = 0;
            dtITSpecialLunarBoss.tier2Weight = 0;
            dtITSpecialLunarBoss.tier3Weight = 7;
            dtITSpecialLunarBoss.bossWeight = 0;
            dtITSpecialLunarBoss.lunarCombinedWeight = 3;
            dtITSpecialLunarBoss.name = "dtITSpecialLunarBoss";

            dtITBasicBonusVoid.tier1Weight = 80;
            dtITBasicBonusVoid.tier2Weight = 10;
            dtITBasicBonusVoid.tier3Weight = 0.25f;
            dtITBasicBonusVoid.bossWeight = 0;
            dtITBasicBonusVoid.voidTier1Weight = 60;
            dtITBasicBonusVoid.voidTier2Weight = 20;
            dtITBasicBonusVoid.voidTier3Weight = 5;
            dtITBasicBonusVoid.name = "dtITBasicBonusVoid";

            dtITBasicBonusLunar.tier1Weight = 80;
            dtITBasicBonusLunar.tier2Weight = 10;
            dtITBasicBonusLunar.tier3Weight = 0.25f;
            dtITBasicBonusLunar.bossWeight = 0;
            dtITBasicBonusLunar.lunarItemWeight = 70;
            dtITBasicBonusLunar.name = "dtITBasicBonusLunar";

            dtITSpecialVoidBoss.tier1Weight = 0;
            dtITSpecialVoidBoss.tier2Weight = 0;
            dtITSpecialVoidBoss.tier3Weight = 0;
            dtITSpecialVoidBoss.bossWeight = 20;
            dtITSpecialVoidBoss.voidTier1Weight = 60;
            dtITSpecialVoidBoss.voidTier2Weight = 30;
            dtITSpecialVoidBoss.voidTier3Weight = 5;
            dtITSpecialVoidBoss.voidBossWeight = 10;
            dtITSpecialVoidBoss.name = "dtITSpecialVoidBoss";

            dtITSpecialVoidling.tier1Weight = 0;
            dtITSpecialVoidling.tier2Weight = 0;
            dtITSpecialVoidling.tier3Weight = 0;
            dtITSpecialVoidling.bossWeight = 0;
            dtITSpecialVoidling.voidTier1Weight = 0;
            dtITSpecialVoidling.voidTier2Weight = 2;
            dtITSpecialVoidling.voidTier3Weight = 1;
            dtITSpecialVoidling.voidBossWeight = 1;
            dtITSpecialVoidling.name = "dtITSpecialVoidling";

            dtITSpecialBossYellow.tier1Weight = 0;
            dtITSpecialBossYellow.tier2Weight = 20;
            dtITSpecialBossYellow.tier3Weight = 0;
            dtITSpecialBossYellow.bossWeight = 80;
            dtITSpecialBossYellow.name = "dtITSpecialBossYellow";


            dtITFamilyWaveDamage.tier1Weight = 80;
            dtITFamilyWaveDamage.tier2Weight = 6;
            dtITFamilyWaveDamage.bossWeight = 6;
            dtITFamilyWaveDamage.tier3Weight = 0.6f;
            dtITFamilyWaveDamage.name = "dtITFamilyWaveDamage";
            dtITFamilyWaveDamage.requiredItemTags = new ItemTag[] { ItemTag.Damage };

            dtITFamilyWaveHealing.tier1Weight = 80;
            dtITFamilyWaveHealing.tier2Weight = 6;
            dtITFamilyWaveHealing.bossWeight = 6;
            dtITFamilyWaveHealing.tier3Weight = 0.6f;
            dtITFamilyWaveHealing.name = "dtITFamilyWaveHealing";
            dtITFamilyWaveHealing.requiredItemTags = new ItemTag[] { ItemTag.Healing };

            dtITFamilyWaveUtility.tier1Weight = 80;
            dtITFamilyWaveUtility.tier2Weight = 6;
            dtITFamilyWaveUtility.bossWeight = 6;
            dtITFamilyWaveUtility.tier3Weight = 0.6f;
            dtITFamilyWaveUtility.name = "dtITFamilyWaveUtility";
            dtITFamilyWaveUtility.requiredItemTags = new ItemTag[] { ItemTag.Utility };

            ItemTag[] OnKillTagThing = { ItemTag.OnKillEffect };
            dtITBasicWaveOnKill.requiredItemTags = OnKillTagThing;
            dtITBasicWaveOnKill.tier1Weight = 80;
            dtITBasicWaveOnKill.tier2Weight = 12;
            dtITBasicWaveOnKill.tier3Weight = 0.5f;
            dtITBasicWaveOnKill.bossWeight = 0.5f;
            dtITBasicWaveOnKill.name = "dtITBasicWaveOnKill";


            //66 to 33
            //


            dtITSpecialEquipmentDroneBoss.requiredItemTags = new ItemTag[] { ItemTag.EquipmentRelated };
            dtITSpecialEquipmentDroneBoss.tier1Weight = 0f;
            dtITSpecialEquipmentDroneBoss.tier2Weight = 16f;
            dtITSpecialEquipmentDroneBoss.tier3Weight = 8f;
            dtITSpecialEquipmentDroneBoss.bossWeight = 0f;
            dtITSpecialEquipmentDroneBoss.lunarItemWeight = 16f;
            dtITSpecialEquipmentDroneBoss.equipmentWeight = 48f;
            dtITSpecialEquipmentDroneBoss.lunarEquipmentWeight = 12f;
            dtITSpecialEquipmentDroneBoss.name = "dtITSpecialEquipmentDroneBoss";

            ArtifactEliteOnlyDisabledPrerequisite.bannedArtifact = ArtifactDefEliteOnly;
            ArtifactEliteOnlyDisabledPrerequisite.name = "ArtifactEliteOnlyDisabledPrerequisite";

            Wave21OrGreaterPrerequisite.minimumWaveCount = 21;
            Wave21OrGreaterPrerequisite.name = "Wave21OrGreaterPrerequisite";
            Wave26OrGreaterPrerequisite.minimumWaveCount = 26;
            Wave26OrGreaterPrerequisite.name = "Wave26OrGreaterPrerequisite";
            Wave41OrGreaterPrerequisite.minimumWaveCount = 41;
            Wave41OrGreaterPrerequisite.name = "Wave41OrGreaterPrerequisite";

            Wave30OrLowerPrerequisite.maximumwavecount = 30;
            Wave30OrLowerPrerequisite.name = "Wave30OrLowerPrerequisite";
            Wave50OrLowerPrerequisite.maximumwavecount = 50;
            Wave50OrLowerPrerequisite.name = "Wave50OrLowerPrerequisite";
            Wave90OrLowerPrerequisite.maximumwavecount = 90;
            Wave90OrLowerPrerequisite.name = "Wave90OrLowerPrerequisite";

            //
            //Evo Test
            GameObject InfiniteTowerWaveArtifactEvolution = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactEvolution", true);
            GameObject InfiniteTowerCurrentArtifactEvolutionWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactEvolutionWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacEvolutionDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefMonsterTeamGainsItems = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/MonsterTeamGainsItems");

            InfiniteTowerWaveArtifactEvolution.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefMonsterTeamGainsItems;
            InfiniteTowerWaveArtifactEvolution.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactEvolutionWaveUI;
            InfiniteTowerWaveArtifactEvolution.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITBasicWaveOnKill;

            InfiniteTowerCurrentArtifactEvolutionWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefMonsterTeamGainsItems.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactEvolutionWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Evolution";
            InfiniteTowerCurrentArtifactEvolutionWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters have additional items during this wave.";

            ArtifacEvolutionDisabledPrerequisite.bannedArtifact = ArtifactDefMonsterTeamGainsItems;
            ArtifacEvolutionDisabledPrerequisite.name = "ArtifacEvolutionDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactEvolution = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactEvolution, weight = 2, prerequisites = ArtifacEvolutionDisabledPrerequisite };

            //RoR2.InfiniteTowerWaveCategory ITBasicWaves = Addressables.LoadAssetAsync<RoR2.InfiniteTowerWaveCategory>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveCategories/CommonWaveCategory.asset").WaitForCompletion();
            //ITBasicWaves.wavePrefabs = ITBasicWaves.wavePrefabs.Add(ITBasicArtifactEvolution);

            GameObject InfiniteTowerWaveArtifactSacrifice = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactSacrifice", true);
            GameObject InfiniteTowerCurrentArtifactSacrificeWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactSacrificeWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacSacrificeDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefSacrifice = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/Sacrifice");

            InfiniteTowerWaveArtifactSacrifice.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefSacrifice;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactSacrificeWaveUI;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITBasicWaveOnKill;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().baseCredits = 230;

            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefSacrifice.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Sacrifice";
            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters have a chance drop items on death.";

            ArtifacSacrificeDisabledPrerequisite.bannedArtifact = ArtifactDefSacrifice;
            ArtifacSacrificeDisabledPrerequisite.name = "ArtifacSacrificeDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactSacrifice = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactSacrifice, weight = 1, prerequisites = Wave50OrLowerPrerequisite };



            GameObject InfiniteTowerWaveArtifactRandomSurvivor = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactRandomSurvivor", true);
            GameObject InfiniteTowerCurrentArtifactRandomSurvivorWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactRandomSurvivorWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacRandomSurvivorDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefRandomSurvivor = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/RandomSurvivorOnRespawn");

            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefRandomSurvivor;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactRandomSurvivorWaveUI;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().baseCredits = 230;

            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefRandomSurvivor.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Metamorphosis";
            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Play as a random survivor for this wave.";

            ArtifacRandomSurvivorDisabledPrerequisite.bannedArtifact = ArtifactDefRandomSurvivor;
            ArtifacRandomSurvivorDisabledPrerequisite.name = "ArtifacRandomSurvivorDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactRandomSurvivor = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactRandomSurvivor, weight = 0.25f, prerequisites = ArtifacRandomSurvivorDisabledPrerequisite };



            //
            //Setting Monster Cards
            InfiniteTowerWaveFamilyBeetle.GetComponent<CombatDirector>().monsterCards = dccsBeetleFamily;
            InfiniteTowerWaveFamilyGolem.GetComponent<CombatDirector>().monsterCards = dccsGolemFamily;
            InfiniteTowerWaveFamilyJellyfish.GetComponent<CombatDirector>().monsterCards = dccsJellyfishFamily;
            InfiniteTowerWaveFamilyClay.GetComponent<CombatDirector>().monsterCards = dccsClayFamily;

            InfiniteTowerWaveFamilyWisp.GetComponent<CombatDirector>().monsterCards = dccsWispFamily;
            InfiniteTowerWaveFamilyLemurian.GetComponent<CombatDirector>().monsterCards = dccsLemurianFamily;
            InfiniteTowerWaveFamilyRoboBall.GetComponent<CombatDirector>().monsterCards = dccsRoboBallFamily;
            InfiniteTowerWaveFamilyImp.GetComponent<CombatDirector>().monsterCards = dccsImpFamily;
            InfiniteTowerWaveFamilyConstruct.GetComponent<CombatDirector>().monsterCards = dccsConstructFamily;

            InfiniteTowerWaveFamilyParent.GetComponent<CombatDirector>().monsterCards = dccsParentFamily;
            InfiniteTowerWaveFamilyGup.GetComponent<CombatDirector>().monsterCards = dccsGupFamily;
            InfiniteTowerWaveFamilyVermin.GetComponent<CombatDirector>().monsterCards = dccsVerminFamily;

            InfiniteTowerWaveBossArtifactEliteOnly.AddComponent<ArtifactEnabler>();
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefEliteOnly;


            InfiniteTowerWaveBossTitanGold.GetComponent<CombatDirector>().monsterCards = dccsGoldshoresMonstersDLC1;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<CombatDirector>().monsterCards = dccsShipgraveyardMonstersDLC1;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<CombatDirector>().monsterCards = dccsVoidFamilyNoBarnacle;



            //InfiniteTowerWaveBossVoidElites.AddComponent<Inventory>();
            CombatDirector WaveVoidEliteDirector = InfiniteTowerWaveBossVoidElites.AddComponent<CombatDirector>();
            WaveVoidEliteDirector.monsterCredit = 666;
            WaveVoidEliteDirector.monsterCards = dccsVoidInfestorOnly;
            WaveVoidEliteDirector.skipSpawnIfTooCheap = false;
            WaveVoidEliteDirector.teamIndex = TeamIndex.Void;
            RangeFloat TempRangeFloatThing = new RangeFloat { max = 1, min = 1 };
            WaveVoidEliteDirector.moneyWaveIntervals = WaveVoidEliteDirector.moneyWaveIntervals.Add(TempRangeFloatThing);
            WaveVoidEliteDirector.creditMultiplier = 4;

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerBossWaveController>().baseCredits = 700;
            //InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerBossWaveController>().immediateCreditsFraction = 0.25f;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerBossWaveController>().secondsBeforeSuddenDeath *= 1.25f;

            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().baseCredits = 450;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().immediateCreditsFraction = 0.20f;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().secondsBeforeSuddenDeath *= 1.25f;

            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscScavLunarIT;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscTitanGoldIT;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscSuperRoboBallBossIT;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscMiniVoidRaidCrabPhase3IT;

            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.2f;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.2f;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.2f;

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[] { new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 1, spawnCard = AllCSCEquipmentDronesIT[4], spawnDistance = DirectorCore.MonsterSpawnDistance.Far }, new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 3, spawnCard = AllCSCEquipmentDronesIT[4], spawnDistance = DirectorCore.MonsterSpawnDistance.Far } };
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 450;

            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 350;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 350;

            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsAfterWave = 15;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath *= 2;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath *= 2f;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath *= 2f;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath *= 2;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath *= 2f;

            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 35;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.75f;

            InfiniteTowerWaveLunarElites.GetComponent<CombatDirector>().eliteBias *= 1.3f;
            InfiniteTowerWaveVoidElites.GetComponent<CombatDirector>().eliteBias *= 1.3f;

            //Setting Drop Table
            GameObject[] ITFamilyWaves = { InfiniteTowerWaveFamilyBeetle, InfiniteTowerWaveFamilyGolem, InfiniteTowerWaveFamilyJellyfish, InfiniteTowerWaveFamilyClay, InfiniteTowerWaveFamilyWisp, InfiniteTowerWaveFamilyLemurian, InfiniteTowerWaveFamilyRoboBall, InfiniteTowerWaveFamilyImp, InfiniteTowerWaveFamilyConstruct, InfiniteTowerWaveFamilyParent, InfiniteTowerWaveFamilyGup, InfiniteTowerWaveFamilyVermin };

            for (int i = 0; i < ITFamilyWaves.Length; i++)
            {
                InfiniteTowerWaveController temp = ITFamilyWaves[i].GetComponent<InfiniteTowerWaveController>();
                CombatDirector combatdirector = ITFamilyWaves[i].GetComponent<CombatDirector>();
                //temp.rewardDropTable = dtITFamilyWave;
                temp.baseCredits = 220;
                temp.immediateCreditsFraction = 0.4f;
                combatdirector.eliteBias *= 1.2f;
                combatdirector.skipSpawnIfTooCheap = false;
            }



            InfiniteTowerWaveFamilyJellyfish.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveDamage;
            InfiniteTowerWaveFamilyWisp.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveDamage;
            InfiniteTowerWaveFamilyLemurian.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveDamage;
            InfiniteTowerWaveFamilyImp.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveDamage;

            InfiniteTowerWaveFamilyGolem.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveHealing;
            InfiniteTowerWaveFamilyClay.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveHealing;
            InfiniteTowerWaveFamilyParent.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveHealing;
            InfiniteTowerWaveFamilyGup.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveHealing;

            InfiniteTowerWaveFamilyBeetle.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveUtility;
            InfiniteTowerWaveFamilyRoboBall.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveUtility;
            InfiniteTowerWaveFamilyConstruct.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveUtility;
            InfiniteTowerWaveFamilyVermin.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITFamilyWaveUtility;



            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITSpecialBossYellow;

            //InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().baseCredits = 500;
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITSpecialBossYellow;

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.VoidTier1;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITSpecialVoidBoss;

            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITBasicBonusLunar;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITBasicBonusVoid;



            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITSpecialBossWave;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITSpecialBossWave;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITSpecialBossWave;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITSpecialBossWave;

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITSpecialEquipmentDroneBoss;


            //Setting UI
            InfiniteTowerWaveFamilyBeetle.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyBeetle;
            InfiniteTowerWaveFamilyGolem.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyGolem;
            InfiniteTowerWaveFamilyJellyfish.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyJellyfish;
            InfiniteTowerWaveFamilyClay.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyClay;

            InfiniteTowerWaveFamilyWisp.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyWisp;
            InfiniteTowerWaveFamilyLemurian.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyLemurian;
            InfiniteTowerWaveFamilyRoboBall.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyRoboBall;
            InfiniteTowerWaveFamilyImp.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyImp;
            InfiniteTowerWaveFamilyConstruct.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyConstruct;

            InfiniteTowerWaveFamilyParent.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyParent;
            InfiniteTowerWaveFamilyGup.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyGup;
            InfiniteTowerWaveFamilyVermin.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyVermin;

            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossWaveUIArtifactEliteOnly;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossWaveUIArtifactDoppelganger;

            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentLunarEliteWaveUI;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentVoidEliteWaveUI;

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidEliteWaveUI;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossScavLunarWaveUI;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossSuperRoboBallBossWaveUI;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossTitanGoldWaveUI;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidRaidWaveUI;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossEquipmentDroneWaveUI;

            //UI Text
            InfiniteTowerCurrentWaveUIFamilyBeetle.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Beetle";
            InfiniteTowerCurrentWaveUIFamilyBeetle.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The ground begins to shift beneath you";

            InfiniteTowerCurrentWaveUIFamilyGolem.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Earth";
            InfiniteTowerCurrentWaveUIFamilyGolem.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The earth rumbles and groans";

            InfiniteTowerCurrentWaveUIFamilyJellyfish.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Jellyfish";
            InfiniteTowerCurrentWaveUIFamilyJellyfish.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The air crackles and arcs";

            InfiniteTowerCurrentWaveUIFamilyClay.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Aphelia";
            InfiniteTowerCurrentWaveUIFamilyClay.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "You feel parasitic influences";

            InfiniteTowerCurrentWaveUIFamilyWisp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Embers";
            InfiniteTowerCurrentWaveUIFamilyWisp.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The air begins to burn";

            InfiniteTowerCurrentWaveUIFamilyLemurian.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Lemuria";
            InfiniteTowerCurrentWaveUIFamilyLemurian.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The ground's temperature begins to rise";

            InfiniteTowerCurrentWaveUIFamilyRoboBall.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Solus X";
            InfiniteTowerCurrentWaveUIFamilyRoboBall.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The whirring of wings and machinery";

            InfiniteTowerCurrentWaveUIFamilyImp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Red Plane";
            InfiniteTowerCurrentWaveUIFamilyImp.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A tear in the fabric of the universe";

            InfiniteTowerCurrentWaveUIFamilyConstruct.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Constructs";
            InfiniteTowerCurrentWaveUIFamilyConstruct.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "You have tripped an ancient alarm";

            InfiniteTowerCurrentWaveUIFamilyParent.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Parenthood";
            InfiniteTowerCurrentWaveUIFamilyParent.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The stars begin to twinkle";

            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Gup";
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The air smells of sweet strawberries";

            InfiniteTowerCurrentWaveUIFamilyVermin.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Vermin";
            InfiniteTowerCurrentWaveUIFamilyVermin.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Squeaks and chirps around you";

            //
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Honor";
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are stronger, more numerous, and guaranteed to be Elite.";

            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Vengence";
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A relentless doppelganger invades!";

            //
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Collapse";
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Voidtouched.";

            InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Perfection";
            InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Perfected.";
            //

            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Infestation";
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A swarm of Void Infestors has been released from the cell.";

            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Twisted Scavenger";
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat the Twisted Scavenger for a special reward.";

            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Alloy Worship Unit";
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat the Alloy Worship Unit for a special reward.";

            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Aurelionite";
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat Aurelionite for a special reward.";

            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of「Voidling」";
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "D??ef?eat「Voidling」for a special reward.";

            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Equipment Drones";
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A swarm of Equipment Drones has been released from the cell.";


            //
            //UI Text Color
            Color FamilyEventIconColor = new Color(1f, 0.8f, 0.7f, 1);
            Color FamilyEventOutlineColor = new Color(1f, 0.7f, 0.5f, 1);

            Texture2D texITWaveScavLunarIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveScavLunarIcon.LoadImage(Properties.Resources.texITWaveScavLunarIcon, false);
            texITWaveScavLunarIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveScavLunarIconS = Sprite.Create(texITWaveScavLunarIcon, rec64, half);

            Texture2D texITWaveLunarEliteIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveLunarEliteIcon.LoadImage(Properties.Resources.texITWaveLunarEliteIcon, false);
            texITWaveLunarEliteIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveLunarEliteIconS = Sprite.Create(texITWaveLunarEliteIcon, rec64, half);

            Texture2D texITWaveTitanGoldIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveTitanGoldIcon.LoadImage(Properties.Resources.texITWaveTitanGoldIcon, false);
            texITWaveTitanGoldIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveTitanGoldIconS = Sprite.Create(texITWaveTitanGoldIcon, rec64, half);

            Texture2D texITWaveBossIconEquipment = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveBossIconEquipment.LoadImage(Properties.Resources.texITWaveBossIconEquipment, false);
            texITWaveBossIconEquipment.filterMode = FilterMode.Bilinear;
            Sprite texITWaveBossIconEquipmentS = Sprite.Create(texITWaveBossIconEquipment, rec64, half);


            InfiniteTowerCurrentWaveUIFamilyBeetle.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyBeetle.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyBeetle.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            InfiniteTowerCurrentWaveUIFamilyGolem.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyGolem.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyGolem.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            InfiniteTowerCurrentWaveUIFamilyJellyfish.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyJellyfish.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyJellyfish.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            InfiniteTowerCurrentWaveUIFamilyClay.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyClay.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyClay.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            InfiniteTowerCurrentWaveUIFamilyWisp.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyWisp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyWisp.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            InfiniteTowerCurrentWaveUIFamilyLemurian.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyLemurian.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyLemurian.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            InfiniteTowerCurrentWaveUIFamilyRoboBall.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyRoboBall.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyRoboBall.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            InfiniteTowerCurrentWaveUIFamilyImp.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyImp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyImp.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            InfiniteTowerCurrentWaveUIFamilyConstruct.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyConstruct.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyConstruct.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            InfiniteTowerCurrentWaveUIFamilyParent.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyParent.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyParent.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            InfiniteTowerCurrentWaveUIFamilyVermin.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyVermin.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyVermin.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;


            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefEliteOnly.smallIconSelectedSprite;
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);

            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefShadowClone.smallIconSelectedSprite;
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);

            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveScavLunarIconS;
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.8f, 1f, 1);

            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveTitanGoldIconS;
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.6f, 1);
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.6f, 1);
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 0.4f, 1);

            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveBossIconEquipmentS;
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.55f, 0.1f, 1);
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);


            InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveLunarEliteIconS;


            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 1f, 1);



            //





            float ITFamilyWaveWeight = 0.65f;
            float ITSpecialBossWaveWeight = 1.8f;
            float ITSpecialBossWeightMultiplier = 1;
            //float BossAugments = 3 / 11;


            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBeetleFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyBeetle, weight = ITFamilyWaveWeight };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITGolemFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyGolem, weight = ITFamilyWaveWeight };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITJellyfishFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyJellyfish, weight = ITFamilyWaveWeight };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITClayFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyClay, weight = ITFamilyWaveWeight };

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITWispFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyWisp, weight = ITFamilyWaveWeight };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITLemurianFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyLemurian, weight = ITFamilyWaveWeight };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITRoboBallFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyRoboBall, weight = ITFamilyWaveWeight };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITImpFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyImp, weight = ITFamilyWaveWeight };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITConstructFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyConstruct, weight = ITFamilyWaveWeight };

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITParentFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyParent, weight = ITFamilyWaveWeight };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITGupFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyGup, weight = ITFamilyWaveWeight };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITVerminFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyVermin, weight = ITFamilyWaveWeight, prerequisites = Wave50OrLowerPrerequisite };

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITVoidElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVoidElites, weight = 3 };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITLunarElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLunarElites, weight = 4 };


            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossArtifactEliteOnly = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossArtifactEliteOnly, weight = 5, prerequisites = Wave50OrLowerPrerequisite };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossArtifactDoppelganger = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossArtifactDoppelganger, weight = 5 }; //5 normally

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossVoidElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossVoidElites, weight = 7 }; //7 normally

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossScavLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossScavLunar, weight = ITSpecialBossWaveWeight * ITSpecialBossWeightMultiplier, prerequisites = Wave26OrGreaterPrerequisite };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossTitanGold = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossTitanGold, weight = ITSpecialBossWaveWeight * ITSpecialBossWeightMultiplier, prerequisites = Wave11OrGreaterPrerequisite };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossSuperRoboBallBoss = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossSuperRoboBallBoss, weight = ITSpecialBossWaveWeight * ITSpecialBossWeightMultiplier, prerequisites = Wave11OrGreaterPrerequisite };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossVoidRaidCrab = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossVoidRaidCrab, weight = ITSpecialBossWaveWeight * ITSpecialBossWeightMultiplier, prerequisites = Wave41OrGreaterPrerequisite };
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossEquipmentDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossEquipmentDrone, weight = 7, prerequisites = Wave11OrGreaterPrerequisite };




            RoR2.InfiniteTowerWaveCategory ITBasicWaves = Addressables.LoadAssetAsync<RoR2.InfiniteTowerWaveCategory>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveCategories/CommonWaveCategory.asset").WaitForCompletion();
            for (int i = 0; i < ITBasicWaves.wavePrefabs.Length; i++)
            {

                switch (ITBasicWaves.wavePrefabs[i].wavePrefab.name)
                {
                    case "InfiniteTowerWaveArtifactEnigma":
                        ITBasicWaves.wavePrefabs[i].weight = 0;
                        break;
                    case "InfiniteTowerWaveArtifactWeakAssKnees":
                        ITBasicWaves.wavePrefabs[i].weight = 0.5f;
                        break;
                    case "InfiniteTowerWaveArtifactMixEnemy":
                        ITBasicWaves.wavePrefabs[i].weight = 2;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().baseCredits = 230;
                        break;
                    case "InfiniteTowerWaveArtifactBomb":
                    case "InfiniteTowerWaveArtifactWispOnDeath":
                        ITBasicWaves.wavePrefabs[i].weight = 2;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtITBasicWaveOnKill;
                        break;
                    case "InfiniteTowerWaveArtifactSingleMonsterType":
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefSingleMonster.smallIconSelectedSprite;
                        break;
                    case "InfiniteTowerWaveArtifactRandomLoadout":
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        break;
                };
            }

            RoR2.InfiniteTowerWaveCategory ITBossWaves = Addressables.LoadAssetAsync<RoR2.InfiniteTowerWaveCategory>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveCategories/BossWaveCategory.asset").WaitForCompletion();
            for (int i = 0; i < ITBossWaves.wavePrefabs.Length; i++)
            {
                if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBoss"))
                {
                    ITBossWaves.wavePrefabs[i].weight = 90;
                }
                if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossVoid"))
                {
                    ITBossWaves.wavePrefabs[i].weight = 7;
                }
                else if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossScav"))
                {
                    ITBossWaves.wavePrefabs[i].weight = ITSpecialBossWaveWeight;

                    InfiniteTowerExplicitSpawnWaveController temp = ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                    temp.rewardDisplayTier = ItemTier.Boss;
                    temp.rewardDropTable = dtITSpecialBossYellow;
                    temp.baseCredits = 150;
                    temp.secondsBeforeSuddenDeath *= 1.5f;
                }
                else if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossBrother"))
                {
                    ITBossWaves.wavePrefabs[i].weight = ITSpecialBossWaveWeight;
                    ITBossWaves.wavePrefabs[i].prerequisites = Wave21OrGreaterPrerequisite;

                    InfiniteTowerExplicitSpawnWaveController temp = ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                    //temp.rewardDropTable = dtITSpecialLunarBoss;

                    temp.baseCredits = 50;
                    temp.combatDirector.monsterCards = dccsLunarFamily;
                    temp.secondsBeforeSuddenDeath *= 2;

                }
            }


            ITBasicWaves.wavePrefabs = ITBasicWaves.wavePrefabs.Add(ITBasicArtifactRandomSurvivor, ITBasicArtifactEvolution, ITBasicArtifactSacrifice, ITBeetleFamily, ITGolemFamily, ITJellyfishFamily, ITClayFamily, ITWispFamily, ITLemurianFamily, ITRoboBallFamily, ITImpFamily, ITConstructFamily, ITParentFamily, ITGupFamily, ITVerminFamily, ITLunarElites, ITVoidElites);
            ITBossWaves.wavePrefabs = ITBossWaves.wavePrefabs.Add(ITBossArtifactEliteOnly, ITBossArtifactDoppelganger, ITBossVoidElites, ITBossScavLunar, ITBossVoidRaidCrab, ITBossSuperRoboBallBoss, ITBossTitanGold, ITBossEquipmentDrone);


            /*
            for (int i = 0; i < ITBossWaves.wavePrefabs.Length; i++)
            {
                if (ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerExplicitSpawnWaveController>())
                {
                    ITBossWaves.wavePrefabs[i].wavePrefab.AddComponent<Inventory>();
                }
            }
            */







            //Other Stage Interactable Changes




            DirectorCardCategorySelection[] allinteractables = new DirectorCardCategorySelection[] { dccsGolemplainsInteractablesDLC1, dccsBlackBeachInteractablesDLC1, dccsSnowyForestInteractablesDLC1, dccsGooLakeInteractablesDLC1, dccsFoggySwampInteractablesDLC1, dccsAncientLoftInteractablesDLC1, dccsFrozenWallInteractablesDLC1, dccsWispGraveyardInteractablesDLC1, dccsSulfurPoolsInteractablesDLC1, dccsDampCaveInteractablesDLC1, dccsShipgraveyardInteractablesDLC1, dccsRootJungleInteractablesDLC1, dccsSkyMeadowInteractablesDLC1, dccsArtifactWorldInteractablesDLC1 };


            for (int dccs = 0; allinteractables.Length > dccs; dccs++)
            {
                for (int cat = 0; allinteractables[dccs].categories.Length > cat; cat++)
                {

                    if (allinteractables[dccs].categories[cat].name.Equals("Chests"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (!allinteractables[dccs].name.Equals("dccsSkyMeadowInteractablesDLC1"))
                            {
                                if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.StartsWith("iscCategoryChest"))
                                {
                                    allinteractables[dccs].categories[cat].cards[card].selectionWeight *= 3;
                                }
                            }
                            /*
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.StartsWith("iscLunarChest"))
                            {
                                if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 10)
                                {
                                    allinteractables[dccs].categories[cat].cards[card].selectionWeight = 10;
                                }
                            }
                            */
                        }
                    }
                    else if (allinteractables[dccs].categories[cat].name.Equals("Shrines"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.Equals("iscShrineCleanse"))
                            {
                                if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 3)
                                {
                                    allinteractables[dccs].categories[cat].cards[card].selectionWeight = 10;
                                }
                            }
                        }
                    }
                    else if (allinteractables[dccs].categories[cat].name.Equals("Drones"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.Equals("iscBrokenEquipmentDrone"))
                            {

                                allinteractables[dccs].categories[cat].cards[card].selectionWeight = 4;

                            }
                        }
                    }

                    else if (allinteractables[dccs].categories[cat].name.Equals("Rare"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            if (allinteractables[dccs].categories[cat].cards[card].spawnCard.name.Equals("iscRadarTower"))
                            {
                                allinteractables[dccs].categories[cat].cards[card].selectionWeight = 20;
                            }
                        }
                    }
                    else if (allinteractables[dccs].categories[cat].name.Equals("Duplicator"))
                    {
                        for (int card = 0; allinteractables[dccs].categories[cat].cards.Length > card; card++)
                        {
                            switch (allinteractables[dccs].categories[cat].cards[card].spawnCard.name)
                            {

                                /*
                                case "iscScrapper":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 12)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].selectionWeight = 12;
                                    }
                                    break;
                                    */
                                case "iscDuplicator":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 30)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].selectionWeight = 25;
                                    }
                                    break;
                                case "iscDuplicatorLarge":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 6)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].selectionWeight = 7;
                                    }
                                    break;
                                case "iscDuplicatorMilitary":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 1)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].selectionWeight = 3;
                                    }
                                    if (allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions == 4)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions = 3;
                                    }
                                    break;
                                case "iscDuplicatorWild":
                                    if (allinteractables[dccs].categories[cat].cards[card].selectionWeight == 2)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].selectionWeight = 3;
                                    }
                                    if (allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions == 1)
                                    {
                                        allinteractables[dccs].categories[cat].cards[card].minimumStageCompletions = 2;
                                    }
                                    break;
                            }
                        }
                    }

                }
            }



            //CategoryChest shenanigans

            if (InteractableChanges.Value == true)
            {
                dccsGolemplainsInteractablesDLC1.categories[0].cards = dccsGolemplainsInteractablesDLC1.categories[0].cards.Remove(dccsGolemplainsInteractablesDLC1.categories[0].cards[7], dccsGolemplainsInteractablesDLC1.categories[0].cards[5]);
                dccsBlackBeachInteractablesDLC1.categories[0].cards = dccsBlackBeachInteractablesDLC1.categories[0].cards.Remove(dccsBlackBeachInteractablesDLC1.categories[0].cards[6], dccsBlackBeachInteractablesDLC1.categories[0].cards[5]);
                dccsSnowyForestInteractablesDLC1.categories[0].cards = dccsSnowyForestInteractablesDLC1.categories[0].cards.Remove(dccsSnowyForestInteractablesDLC1.categories[0].cards[7], dccsSnowyForestInteractablesDLC1.categories[0].cards[6]);

                dccsGooLakeInteractablesDLC1.categories[0].cards = dccsGooLakeInteractablesDLC1.categories[0].cards.Remove(dccsGooLakeInteractablesDLC1.categories[0].cards[8], dccsGooLakeInteractablesDLC1.categories[0].cards[7]);
                dccsFoggySwampInteractablesDLC1.categories[0].cards = dccsFoggySwampInteractablesDLC1.categories[0].cards.Remove(dccsFoggySwampInteractablesDLC1.categories[0].cards[8], dccsFoggySwampInteractablesDLC1.categories[0].cards[6]);
                dccsAncientLoftInteractablesDLC1.categories[0].cards = dccsAncientLoftInteractablesDLC1.categories[0].cards.Remove(dccsAncientLoftInteractablesDLC1.categories[0].cards[6], dccsAncientLoftInteractablesDLC1.categories[0].cards[5]);

                dccsFrozenWallInteractablesDLC1.categories[0].cards = dccsFrozenWallInteractablesDLC1.categories[0].cards.Remove(dccsFrozenWallInteractablesDLC1.categories[0].cards[6], dccsFrozenWallInteractablesDLC1.categories[0].cards[5]);
                dccsWispGraveyardInteractablesDLC1.categories[0].cards = dccsWispGraveyardInteractablesDLC1.categories[0].cards.Remove(dccsWispGraveyardInteractablesDLC1.categories[0].cards[6], dccsWispGraveyardInteractablesDLC1.categories[0].cards[5]);
                dccsSulfurPoolsInteractablesDLC1.categories[0].cards = dccsSulfurPoolsInteractablesDLC1.categories[0].cards.Remove(dccsSulfurPoolsInteractablesDLC1.categories[0].cards[7], dccsSulfurPoolsInteractablesDLC1.categories[0].cards[5]);

                dccsDampCaveInteractablesDLC1.categories[0].cards = dccsDampCaveInteractablesDLC1.categories[0].cards.Remove(dccsDampCaveInteractablesDLC1.categories[0].cards[7], dccsDampCaveInteractablesDLC1.categories[0].cards[6]);
                dccsShipgraveyardInteractablesDLC1.categories[0].cards = dccsShipgraveyardInteractablesDLC1.categories[0].cards.Remove(dccsShipgraveyardInteractablesDLC1.categories[0].cards[7], dccsShipgraveyardInteractablesDLC1.categories[0].cards[5]);
                dccsRootJungleInteractablesDLC1.categories[0].cards = dccsRootJungleInteractablesDLC1.categories[0].cards.Remove(dccsRootJungleInteractablesDLC1.categories[0].cards[6], dccsRootJungleInteractablesDLC1.categories[0].cards[5]);


                dccsBlackBeachInteractablesDLC1.categories[2].selectionWeight = 7;
                dccsBlackBeachInteractablesDLC1.categories[2].cards[2].selectionWeight = 2;
                dccsBlackBeachInteractablesDLC1.categories[7].selectionWeight = 5;

                dccsDampCaveInteractablesDLC1.categories[2].cards[0].selectionWeight *= 2;
                dccsDampCaveInteractablesDLC1.categories[2].cards[1].selectionWeight *= 3;
                dccsDampCaveInteractablesDLC1.categories[2].cards[2].selectionWeight *= 2;

                dccsSulfurPoolsInteractablesDLC1.categories[2].cards = dccsSulfurPoolsInteractablesDLC1.categories[2].cards.Remove(dccsSulfurPoolsInteractablesDLC1.categories[2].cards[3], dccsSulfurPoolsInteractablesDLC1.categories[2].cards[2]);
                dccsSulfurPoolsInteractablesDLC1.AddCard(2, ADShrineBoss10);


                dccsArtifactWorldInteractablesDLC1.categories[2].cards[1] = ADShrineCleanse1;

                dccsSnowyForestInteractablesDLC1.AddCard(3, ADBrokenMegaDrone);
            }




        }

        private static void EventFunctions_BeginEnding(On.RoR2.EventFunctions.orig_BeginEnding orig, EventFunctions self, GameEndingDef gameEndingDef)
        {
            if (gameEndingDef == DLC1Content.GameEndings.VoidEnding && Run.instance.GetComponent<InfiniteTowerRun>())
            {
                orig(self, InfiniteTowerEnding);
                return;
            }
            On.RoR2.EventFunctions.BeginEnding -= EventFunctions_BeginEnding;
            orig(self, gameEndingDef);
        }

        private static void InfiniteTowerRun_OnWaveAllEnemiesDefeatedServer(On.RoR2.InfiniteTowerRun.orig_OnWaveAllEnemiesDefeatedServer orig, InfiniteTowerRun self, InfiniteTowerWaveController wc)
        {
            orig(self, wc);
            if (self.IsStageTransitionWave())
            {
                Debug.Log("\nPreviousSceneDef " + PreviousSceneDef + "\n" + "CurrentSceneDef " + Stage.instance.sceneDef + "\n" + "NextSceneDef " + self.nextStageScene);
                if (PreviousSceneDef != null && PreviousSceneDef == self.nextStageScene)
                {
                    int preventInfiniteLoop = 0;
                    //Debug.Log("Preventing repeat scene");
                    do
                    {
                        preventInfiniteLoop++;
                        self.PickNextStageSceneFromCurrentSceneDestinations();
                        Debug.Log("ReplacementSceneDef " + self.nextStageScene);
                    }
                    while (self.nextStageScene == PreviousSceneDef && preventInfiniteLoop < 10);
                }
                PreviousSceneDef = Stage.instance.sceneDef;
            }

        }

        private static void Burrow_OnEnter(On.EntityStates.InfiniteTowerSafeWard.Burrow.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.Burrow self)
        {
            self.radius = 25;
            orig(self);
        }

        private static void Travelling_OnEnter(On.EntityStates.InfiniteTowerSafeWard.Travelling.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.Travelling self)
        {
            self.radius = 25;
            orig(self);
            //WasSimuCrabIndicator = true;
            //GameObject tempindicator = Instantiate(SimuCrabPointer, self.outer.gameObject.transform);
            //tempindicator.GetComponent<PositionIndicator>().targetTransform = self.outer.gameObject.transform;
        }

        private static void AwaitingActivation_OnEnter(On.EntityStates.InfiniteTowerSafeWard.AwaitingActivation.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.AwaitingActivation self)
        {
            //Debug.LogWarning((Run.instance as InfiniteTowerRun).waveInstance);
            if ((Run.instance as InfiniteTowerRun).waveInstance)
            {
                self.radius = 60;
            }
            else
            {
                self.radius = 25;
            }
            orig(self);

            //self.radius = 60;
        }

        private static void InfiniteTowerWaveController_OnAllEnemiesDefeatedServer(On.RoR2.InfiniteTowerWaveController.orig_OnAllEnemiesDefeatedServer orig, InfiniteTowerWaveController self)
        {
            orig(self);

            switch (self.name)
            {

                case "InfiniteTowerWaveBossScavLunar(Clone)":
                case "InfiniteTowerWaveBossBrother(Clone)":
                    self.rewardDisplayTier = ItemTier.Lunar;
                    self.rewardDropTable = dtITLunar;
                    self.DropRewards();
                    break;
                case "InfiniteTowerWaveBossVoidRaidCrab(Clone)":
                    self.rewardDisplayTier = ItemTier.VoidTier3;
                    self.rewardDropTable = dtITSpecialVoidling;
                    self.DropRewards();
                    break;
                case "InfiniteTowerWaveBossVoidElites(Clone)":
                    self.gameObject.GetComponents<CombatDirector>()[0].enabled = false;
                    self.gameObject.GetComponents<CombatDirector>()[1].enabled = false;
                    break;
            }

            InfiniteTowerRun run = Run.instance.GetComponent<InfiniteTowerRun>();




            if (run.waveIndex >= SimuEndingStartAtXWaves && run.waveIndex % SimuEndingEveryXWaves == SimuEndingWaveRest)
            {


                GameObject EndingPortal = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(iscVoidOutroPortal, new DirectorPlacementRule
                {
                    minDistance = 30f,
                    maxDistance = 45f,
                    placementMode = DirectorPlacementRule.PlacementMode.Approximate,
                    position = run.safeWardController.transform.position,
                    spawnOnTarget = run.safeWardController.transform
                }, run.safeWardRng));

                EndingPortal.GetComponent<GenericDisplayNameProvider>().displayToken = "Simulated Exit Rift";
                EndingPortal.GetComponent<GenericInteraction>().contextToken = "End Simulation?";
                EndingPortal.GetComponent<GenericObjectiveProvider>().objectiveToken = "Continue or end the <style=cIsVoid>Simulation</style> ";
                EndingPortal.GetComponent<ObjectScaleCurve>().baseScale = new Vector3(0.7f, 0.7f, 0.7f);

                On.RoR2.EventFunctions.BeginEnding += EventFunctions_BeginEnding;

                Destroy(EndingPortal.transform.GetChild(1).gameObject);
                Destroy(EndingPortal.transform.GetChild(0).GetChild(1).GetChild(0).gameObject);
                EndingPortal.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Light>().color = new Color(1f, 0.2f, 0.4f, 1f);
            }


        }



        private static void InfiniteTowerRun_BeginNextWave(On.RoR2.InfiniteTowerRun.orig_BeginNextWave orig, InfiniteTowerRun self)
        {
            orig(self);

            if (self.waveInstance)
            {
                /*
                if (WasLastWaveRandomSurvivor == true && self.waveInstance.name != "InfiniteTowerWaveArtifactRandomSurvivor(Clone)")
                {
                    WasLastWaveRandomSurvivor = false;
                    foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
                    {
                        Debug.LogWarning("Metamorphosis disable " + playerCharacterMasterController.body);
                        playerCharacterMasterController.SetBodyPrefabToPreference();
                        CharacterBody temp = playerCharacterMasterController.body;
                        if (temp)
                        {
                            playerCharacterMasterController.master.Respawn(temp.footPosition, temp.transform.rotation);
                        }
                    };
                }
                */
                switch (self.waveInstance.name)
                {
                    case "InfiniteTowerWaveBossArtifactDoppelganger(Clone)":
                        CombatSquad WaveSquad = self.waveInstance.GetComponent<CombatSquad>();
                        CombatSquad[] bossgrouplist2 = FindObjectsOfType(typeof(CombatSquad)) as CombatSquad[];
                        for (var i = 0; i < bossgrouplist2.Length; i++)
                        {
                            //Debug.LogWarning(bossgrouplist2[i]);
                            if (bossgrouplist2[i].name.Equals("ShadowCloneEncounter(Clone)") || bossgrouplist2[i].name.Equals("ShadowCloneEncounterAltered"))
                            {
                                foreach (CharacterMaster charactermaster in bossgrouplist2[i].membersList)
                                {
                                    WaveSquad.AddMember(charactermaster);
                                }
                            }
                        }
                        break;
                    case "InfiniteTowerWaveBossVoidElites(Clone)":
                        self.waveInstance.GetComponents<CombatDirector>()[1].monsterCredit *= Math.Max(1, ((self.waveIndex / 10) + 1f) * 0.35f);
                        break;
                    case "InfiniteTowerWaveBossBrother(Clone)":
                        self.waveInstance.AddComponent<PhaseCounter>().phase = 3;
                        break;
                    case "InfiniteTowerWaveBossScav(Clone)":
                        if (!Stage.instance.scavPackDroppedServer)
                        {
                            self.waveInstance.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsAfterWave = 15;
                        }
                        break;
                    case "InfiniteTowerWaveArtifactRandomSurvivor(Clone)":
                        /*
                        foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
                        {
                            WasLastWaveRandomSurvivor = true;
                            Debug.LogWarning("Metamorphosis enable " + playerCharacterMasterController.body);
                            CharacterBody temp = playerCharacterMasterController.body;

                            if (temp)
                            {
                                playerCharacterMasterController.master.Respawn(temp.footPosition, temp.transform.rotation);
                            }
                        };
                        */
                        break;
                }


                if (Run.instance)
                {
                    Run.instance.GetComponent<RoR2.EnemyInfoPanelInventoryProvider>().MarkAsDirty();
                }
                self.waveInstance.GetComponent<CombatDirector>().goldRewardCoefficient *= self.participatingPlayerCount;

            }
            /*
            if (WasSimuCrabIndicator)
            {
                PositionIndicator indicator = self.safeWardController.gameObject.GetComponentInChildren<PositionIndicator>();
                if (indicator)
                {
                    Destroy(indicator.gameObject);
                }
            }
            */


        }


        private static void InfiniteTowerRun_CleanUpCurrentWave(On.RoR2.InfiniteTowerRun.orig_CleanUpCurrentWave orig, InfiniteTowerRun self)
        {
            if (self.waveInstance)
            {
                switch (self.waveInstance.name)
                {

                    case "InfiniteTowerWaveBossVoidElites(Clone)":
                        CombatDirector.eliteTiers[1].eliteTypes = CombatDirector.eliteTiers[1].eliteTypes.Remove(DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void);
                        CombatDirector.eliteTiers[1].eliteTypes = CombatDirector.eliteTiers[1].eliteTypes.Remove(DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void);
                        CombatDirector.eliteTiers[2].eliteTypes = CombatDirector.eliteTiers[2].eliteTypes.Remove(DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void);
                        break;
                    case "InfiniteTowerWaveVoidElites(Clone)":
                        CombatDirector.eliteTiers[1] = Tier1EliteTierBackup;
                        CombatDirector.eliteTiers[2].eliteTypes = CombatDirector.eliteTiers[2].eliteTypes.Remove(DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void);
                        CombatDirector.eliteTiers[3].eliteTypes = CombatDirector.eliteTiers[3].eliteTypes.Remove(DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void);
                        CombatDirector.eliteTiers[4] = TierLunarEliteTierBackup;
                        break;
                    case "InfiniteTowerWaveLunarElites(Clone)":
                        CombatDirector.eliteTiers[1] = Tier1EliteTierBackup;
                        CombatDirector.eliteTiers[2].eliteTypes = CombatDirector.eliteTiers[2].eliteTypes.Remove(EliteDefLunarEulogy, EliteDefLunarEulogy, EliteDefLunarEulogy, EliteDefLunarEulogy);
                        CombatDirector.eliteTiers[3].eliteTypes = CombatDirector.eliteTiers[3].eliteTypes.Remove(EliteDefLunarEulogy, EliteDefLunarEulogy, EliteDefLunarEulogy, EliteDefLunarEulogy);
                        break;
                }
                ArtifactEnabler temp = self.waveInstance.GetComponent<ArtifactEnabler>();
                if (temp && temp.artifactDef == ArtifactDefSingleMonsterType && temp.artifactWasEnabled == false && Stage.instance)
                {
                    Stage.instance.singleMonsterTypeBodyIndex = BodyIndex.None;
                }
                if (Run.instance)
                {
                    Run.instance.GetComponent<RoR2.EnemyInfoPanelInventoryProvider>().MarkAsDirty();
                }

            }
            Debug.Log("WaveCleanUp  " + self.waveInstance);
            orig(self);



        }



        private static GameObject InfiniteTowerWaveCategory_SelectWavePrefab(On.RoR2.InfiniteTowerWaveCategory.orig_SelectWavePrefab orig, InfiniteTowerWaveCategory self, InfiniteTowerRun run, Xoroshiro128Plus rng)
        {
            GameObject temp = orig(self, run, rng);
            Debug.Log("SelectWavePrefab  " + temp);


            //Debug.LogWarning(run.waveIndex % 50);
            if (run.waveIndex >= SimuForcedBossStartAtXWaves && run.waveIndex % SimuForcedBossEveryXWaves == SimuForcedBossWaveRest)
            {
                temp = ITAllSpecialBossWaves[random.Next(ITAllSpecialBossWaves.Length)];
            }



            //EntityStates.InfiniteTowerSafeWard.Active.
            bool IsBossVoidEliteWave = false;
            //bool IsBossEquipmentWave = false;
            //bool IsVoidlingWave = false;
            //bool IsMithrixWave = false;
            float bonusspecialmultiplier = 1;
            switch (temp.name)
            {
                case "InfiniteTowerWaveBossArtifactDoppelganger":
                    RoR2.Artifacts.DoppelgangerInvasionManager.PerformInvasion(Run.instance.bossRewardRng);
                    Run.instance.GetComponent<InfiniteTowerRun>().safeWardController.wardStateMachine.state.SetFieldValue("radius", 80f);
                    break;
                case "InfiniteTowerWaveBossScav":
                    ScavThingRandomizer();
                    break;
                case "InfiniteTowerWaveBossVoidElites":
                    IsBossVoidEliteWave = true;
                    bonusspecialmultiplier = 3f;
                    if (CombatDirector.eliteTiers.Length > 3)
                    {
                        CombatDirector.eliteTiers[1].eliteTypes = CombatDirector.eliteTiers[1].eliteTypes.Add(DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void);
                        CombatDirector.eliteTiers[1].eliteTypes = CombatDirector.eliteTiers[1].eliteTypes.Add(DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void);
                        CombatDirector.eliteTiers[2].eliteTypes = CombatDirector.eliteTiers[2].eliteTypes.Add(DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void);
                    }
                    break;
                case "InfiniteTowerWaveVoidElites":
                    if (CombatDirector.eliteTiers.Length > 3)
                    {
                        Tier1EliteTierBackup = CombatDirector.eliteTiers[1];
                        CombatDirector.eliteTiers[1] = new CombatDirector.EliteTierDef
                        {
                            costMultiplier = 5f,
                            eliteTypes = new EliteDef[] { DLC1Content.Elites.Void },
                            isAvailable = ((SpawnCard.EliteRules rules) => CombatDirector.NotEliteOnlyArtifactActive() && rules == SpawnCard.EliteRules.Default),
                            canSelectWithoutAvailableEliteDef = false,
                        };
                        CombatDirector.eliteTiers[2].eliteTypes = CombatDirector.eliteTiers[2].eliteTypes.Add(DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void);
                        CombatDirector.eliteTiers[3].eliteTypes = CombatDirector.eliteTiers[3].eliteTypes.Add(DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void, DLC1Content.Elites.Void);
                        TierLunarEliteTierBackup = CombatDirector.eliteTiers[4];
                        CombatDirector.eliteTiers[4] = new CombatDirector.EliteTierDef
                        {
                            costMultiplier = 5f,
                            eliteTypes = new EliteDef[] { DLC1Content.Elites.Void },
                            isAvailable = ((SpawnCard.EliteRules rules) => rules == SpawnCard.EliteRules.Lunar),
                            canSelectWithoutAvailableEliteDef = false
                        };
                    }
                    break;
                case "InfiniteTowerWaveLunarElites":
                    if (CombatDirector.eliteTiers.Length > 3)
                    {
                        Tier1EliteTierBackup = CombatDirector.eliteTiers[1];
                        CombatDirector.eliteTiers[1] = new CombatDirector.EliteTierDef
                        {
                            costMultiplier = 5f,
                            eliteTypes = new EliteDef[] { EliteDefLunarEulogy },
                            isAvailable = ((SpawnCard.EliteRules rules) => CombatDirector.NotEliteOnlyArtifactActive() && rules == SpawnCard.EliteRules.Default),
                            canSelectWithoutAvailableEliteDef = false,
                        };
                        CombatDirector.eliteTiers[2].eliteTypes = CombatDirector.eliteTiers[2].eliteTypes.Add(EliteDefLunarEulogy, EliteDefLunarEulogy, EliteDefLunarEulogy, EliteDefLunarEulogy);
                        CombatDirector.eliteTiers[3].eliteTypes = CombatDirector.eliteTiers[3].eliteTypes.Add(EliteDefLunarEulogy, EliteDefLunarEulogy, EliteDefLunarEulogy, EliteDefLunarEulogy);
                    }
                    break;
                case "InfiniteTowerWaveBossEquipmentDrone":
                    if (run.waveIndex > 10)
                    {
                        bonusspecialmultiplier = 1.5f;
                    }
                    List<CharacterSpawnCard> tempCscEqList = new List<CharacterSpawnCard>(AllCSCEquipmentDronesIT);
                    int firsteq = random.Next(tempCscEqList.Count);
                    InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = tempCscEqList[firsteq];
                    tempCscEqList.Remove(tempCscEqList[firsteq]);
                    int secondeq = random.Next(tempCscEqList.Count);
                    InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[1].spawnCard = tempCscEqList[secondeq];
                    break;
                case "InfiniteTowerWaveBossVoidRaidCrab":
                    bonusspecialmultiplier = 1.3f;
                    Run.instance.GetComponent<InfiniteTowerRun>().safeWardController.wardStateMachine.state.SetFieldValue("radius", 140f);
                    break;
                case "InfiniteTowerWaveBossBrother":
                    bonusspecialmultiplier = 2.4f;
                    Run.instance.GetComponent<InfiniteTowerRun>().safeWardController.wardStateMachine.state.SetFieldValue("radius", 80f);
                    break;
                case "InfiniteTowerWaveBossSuperRoboBallBoss":
                    bonusspecialmultiplier = 0.76f;
                    break;
                case "InfiniteTowerWaveBossTitanGold":
                    bonusspecialmultiplier = 0.9f;
                    break;
            }
            Debug.Log(Run.instance.GetComponent<InfiniteTowerRun>().safeWardController.wardStateMachine.state);
            InfiniteTowerExplicitSpawnWaveController tempexplicitcontroller = temp.GetComponent<InfiniteTowerExplicitSpawnWaveController>();


            if (tempexplicitcontroller || IsBossVoidEliteWave)
            {
                float num = 1f;
                float num2 = 1f;
                num += Run.instance.difficultyCoefficient / 2.5f * Math.Max(1, (run.waveIndex / 10) * 0.2f + 0.2f);
                num2 += Run.instance.difficultyCoefficient / 30f * Math.Max(1, (run.waveIndex / 10) * 0.075f);
                num *= bonusspecialmultiplier;
                num /= (1 + ((run.participatingPlayerCount- 1) * 0.33f));
                int num3 = Mathf.Max(1, Run.instance.livingPlayerCount);
                num *= Mathf.Pow((float)num3, 0.5f);
                int grantHp = Mathf.RoundToInt((num - 1f) * 10f);
                int grantDamage = Mathf.RoundToInt((num2 - 1f) * 10f);
                Debug.LogFormat(temp + " Special Scaling: currentBoostHpCoefficient={0}, currentBoostDamageCoefficient={1}", new object[]
                {
                       grantHp,
                       grantDamage
                });


                ItemCountPair[] itemsToGrant = new ItemCountPair[] {
                    new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = grantHp },
                    new ItemCountPair { itemDef = RoR2Content.Items.BoostDamage, count = grantDamage },
                    new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 0},
                };

                if (tempexplicitcontroller)
                {
                    foreach (InfiniteTowerExplicitSpawnWaveController.SpawnInfo spawnInfo in tempexplicitcontroller.spawnList)
                    {
                        foreach (ItemCountPair itemPair in spawnInfo.spawnCard.itemsToGrant)
                        {
                            if (itemPair.itemDef == RoR2Content.Items.AdaptiveArmor && itemPair.count > 0)
                            {
                                itemsToGrant[2].count = itemPair.count;
                            }
                        }
                        spawnInfo.spawnCard.itemsToGrant = itemsToGrant;
                    }
                }
                else if (IsBossVoidEliteWave)
                {
                    itemsToGrant[2].count = 1;
                    cscVoidInfestorIT.itemsToGrant = itemsToGrant;
                }

            }

            return temp;
        }

        private static void ClassicStageInfo_RebuildCards(On.RoR2.ClassicStageInfo.orig_RebuildCards orig, ClassicStageInfo self)
        {
            if (Run.instance && SceneInfo.instance)
            {
                switch (SceneInfo.instance.sceneDef.baseSceneName)
                {
                    case "blackbeach":
                        self.monsterDccsPool = dpBlackBeachMonsters;
                        self.interactableDccsPool = dpBlackBeachInteractables;
                        break;
                    case "foggyswamp":
                        self.monsterDccsPool = dpFoggySwampMonsters;
                        self.interactableDccsPool = dpFoggySwampInteractables;
                        //SceneInfo.instance.airNodesAsset = foggyswampAirNodesNodegraph;
                        Debug.LogWarning(SceneInfo.instance.airNodesAsset);
                        break;
                    case "goolake":
                        self.sceneDirectorInteractibleCredits += 30;
                        break;
                    case "rootjungle":
                        self.sceneDirectorInteractibleCredits += 40;
                        break;
                }
            }

            orig(self);
            //Debug.LogWarning(self.sceneDirectorInteractibleCredits);

            if (Run.instance && SceneInfo.instance)
            {
                Debug.Log("Running");
                switch (SceneInfo.instance.sceneDef.baseSceneName)
                {
                    case "itgolemplains":
                        self.interactableCategories = dccsITGolemPlainsInteractablesW;
                        break;
                    case "itgoolake":
                        self.interactableCategories = dccsITGooLakeInteractablesW;
                        break;
                    case "itancientloft":
                        self.interactableCategories = dccsITAncientLoftInteractablesW;
                        break;
                    case "itfrozenwall":
                        self.interactableCategories = dccsITFrozenWallInteractablesW;
                        break;
                    case "itdampcave":
                        self.interactableCategories = dccsITDampCaveInteractablesW;
                        break;
                    case "itskymeadow":
                        self.interactableCategories = dccsITSkyMeadowInteractablesW;
                        break;
                    case "itmoon":
                        self.interactableCategories = dccsITMoonInteractablesW;
                        GameObject MoonArenaDynamicPillar = GameObject.Find("/HOLDER: Stage");
                        if (MoonArenaDynamicPillar)
                        {
                            Vector3 mooncolumnlocalpos = new Vector3(7.2f, -1.08f, 0f);
                            Vector3 mooncolumnrotation = new Vector3(270.0198f, 0f, 0f);
                            Vector3 mooncolumnlocalscale = new Vector3(1f, 1f, 1f);

                            MoonArenaDynamicPillar.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                            MoonArenaDynamicPillar.transform.GetChild(2).GetChild(0).localPosition = mooncolumnlocalpos;
                            MoonArenaDynamicPillar.transform.GetChild(2).GetChild(0).localEulerAngles = mooncolumnrotation;
                            MoonArenaDynamicPillar.transform.GetChild(2).GetChild(0).localScale = mooncolumnlocalscale;

                            MoonArenaDynamicPillar.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                            MoonArenaDynamicPillar.transform.GetChild(3).GetChild(0).localPosition = mooncolumnlocalpos;
                            MoonArenaDynamicPillar.transform.GetChild(3).GetChild(0).localEulerAngles = mooncolumnrotation;
                            MoonArenaDynamicPillar.transform.GetChild(3).GetChild(0).localScale = mooncolumnlocalscale;

                            MoonArenaDynamicPillar.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
                            MoonArenaDynamicPillar.transform.GetChild(4).GetChild(0).localPosition = mooncolumnlocalpos;
                            MoonArenaDynamicPillar.transform.GetChild(4).GetChild(0).localEulerAngles = mooncolumnrotation;
                            MoonArenaDynamicPillar.transform.GetChild(4).GetChild(0).localScale = mooncolumnlocalscale;

                            MoonArenaDynamicPillar.transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
                            MoonArenaDynamicPillar.transform.GetChild(5).GetChild(0).localPosition = mooncolumnlocalpos;
                            MoonArenaDynamicPillar.transform.GetChild(5).GetChild(0).localEulerAngles = mooncolumnrotation;
                            MoonArenaDynamicPillar.transform.GetChild(5).GetChild(0).localScale = mooncolumnlocalscale;

                        }


                        /*
                        GameObject MoonArenaDynamicPillar = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/moon/MoonArenaDynamicPillar.prefab").WaitForCompletion();
                        Instantiate(MoonArenaDynamicPillar);
                        MoonArenaDynamicPillar.transform.localPosition = new Vector3(0, 0, 0);
                        foreach (Transform child in MoonArenaDynamicPillar.transform)
                        {
                            child.gameObject.SetActive(true);
                        }
                        */
                        break;
                }
            }
        }

        public void FamilyEventMaker()
        {
            ClassicStageInfo.monsterFamilyChance = FamilyEventChance.Value;

            DirectorCard DSTitanDampCaves = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscTitanDampCave"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DSBeetleQueen = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBeetleQueen"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSVagrant = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVagrant"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSImpBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImpBoss"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSRoboBallBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSGrovetender = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DSGolem = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGolem"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSGreaterWisp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGreaterWisp"),

                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSRoboBallMini = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopRoboBallMini = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSElderLemurian = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DSClayTemp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBruiser"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSParent = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscParent"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSLemurian = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurian"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSWisp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLesserWisp"),

                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSBeetle = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBeetle"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSJellyfish = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscJellyfish"),

                preventOverhead = true,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSImp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImp"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSVulture = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            CharacterSpawnCard cscVultureNoCeiling = Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Vulture/cscVulture.asset").WaitForCompletion());
            cscVultureNoCeiling.name = "cscVultureNoCeiling";
            cscVultureNoCeiling.requiredFlags = NodeFlags.None;
            DirectorCard DSVultureNoCeling = new DirectorCard
            {
                spawnCard = cscVultureNoCeiling,
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DSVultureNoCeling.spawnCard = cscVultureNoCeiling;
            //Debug.LogWarning(DSVultureNoCeling.spawnCard);
            DirectorCard DSBeetleGuard = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBeetleGuard"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSClayBoss = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBoss"),
                selectionWeight = 1,

                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSGrandparent = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSLunarExploder = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                selectionWeight = 1,

                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSLunarGolem = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 1,

                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSLunarWisp = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 1,

                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DSVoidReaver = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DSClayGrenadier = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            DirectorCard DSGeep = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGeepBody"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard DSGip = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };



            DirectorCard DSMagmaWorm = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard DSElectricWorm = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard SimuElectricWorm = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSBison = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBison"),

                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSBlindPest = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVermin"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSBlindVermin = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVermin"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSBlindPestSnowy = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVerminSnowy"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSBlindVerminSnowy = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVerminSnowy"),
                preventOverhead = false,
                selectionWeight = 1,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };



            DirectorCard DSAcidLarva = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscAcidLarva"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopAcidLarva = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscAcidLarva"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard DSHermitCrab = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };


            dccsGolemFamilyAbyssal.categories[0].cards[0] = DSTitanDampCaves;
            dccsGolemFamilyAbyssal.name = "dccsGolemFamilyAbyssal";


            dccsVoidFamilyNoBarnacle.categories[2].cards = dccsVoidFamilyNoBarnacle.categories[2].cards.Remove(dccsVoidFamilyNoBarnacle.categories[2].cards[1]);
            dccsVoidFamilyNoBarnacle.name = "dccsVoidFamilyNoBarnacle";

            dccsClayFamily.AddCategory("Champions", 2);
            dccsClayFamily.AddCategory("Minibosses", 4);
            dccsClayFamily.AddCard(0, DSClayBoss);
            dccsClayFamily.AddCard(1, DSClayTemp);
            dccsClayFamily.AddCard(1, DSClayGrenadier);
            dccsClayFamily.name = "dccsClayFamily";
            dccsClayFamily.minimumStageCompletion = 2;
            dccsClayFamily.maximumStageCompletion = 1000000;
            dccsClayFamily.selectionChatString = "<style=cWorldEvent>[WARNING] You feel parasitic influences..</style>";

            dccsRoboBallFamily.AddCategory("Champions", 4);
            dccsRoboBallFamily.AddCategory("Minibosses", 4);
            dccsRoboBallFamily.AddCategory("Basic Monsters", 4);
            dccsRoboBallFamily.AddCard(0, DSRoboBallBoss);
            dccsRoboBallFamily.AddCard(1, DSRoboBallMini);
            dccsRoboBallFamily.AddCard(2, DSVultureNoCeling);
            dccsRoboBallFamily.name = "dccsRoboBallFamily";
            dccsRoboBallFamily.minimumStageCompletion = 0;
            dccsRoboBallFamily.maximumStageCompletion = 1000000;
            dccsRoboBallFamily.selectionChatString = "<style=cWorldEvent>[WARNING] You hear a whirring of wings and machinery..</style>";

            dccsVerminFamily.AddCategory("Basic Monsters", 6);
            dccsVerminFamily.AddCard(0, DSBlindPest);
            dccsVerminFamily.AddCard(0, DSBlindVermin);
            dccsVerminFamily.name = "dccsVerminFamily";
            dccsVerminFamily.minimumStageCompletion = 0;
            dccsVerminFamily.maximumStageCompletion = 5;
            dccsVerminFamily.selectionChatString = "<style=cWorldEvent>[WARNING] You hear squeaks and chirps around you..</style>";

            dccsVerminFamilySnowy.AddCategory("Basic Monsters", 6);
            dccsVerminFamilySnowy.AddCard(0, DSBlindPestSnowy);
            dccsVerminFamilySnowy.AddCard(0, DSBlindVerminSnowy);
            dccsVerminFamilySnowy.name = "dccsVerminSnowyFamily";
            dccsVerminFamilySnowy.minimumStageCompletion = 0;
            dccsVerminFamilySnowy.maximumStageCompletion = 14;
            dccsVerminFamilySnowy.selectionChatString = "<style=cWorldEvent>[WARNING] You hear squeaks and chirps around you..</style>";






            dccsLemurianFamily.minimumStageCompletion = 0;
            dccsJellyfishFamily.minimumStageCompletion = 0;
            dccsGupFamily.minimumStageCompletion = 0;
            dccsBeetleFamily.minimumStageCompletion = 0;
            dccsImpFamily.minimumStageCompletion = 0;
            dccsWispFamily.minimumStageCompletion = 0;
            dccsConstructFamily.minimumStageCompletion = 0;

            dccsLemurianFamily.maximumStageCompletion = 14;
            dccsLunarFamily.minimumStageCompletion = 3;
            dccsLunarFamily.maximumStageCompletion = 1000000;
            dccsVoidFamily.minimumStageCompletion = 4;

            dccsParentFamily.AddCategory("Champions", 4);
            dccsParentFamily.AddCard(1, DSGrandparent);
            dccsParentFamily.selectionChatString = "<style=cWorldEvent>[WARNING] The stars begin to twinkle..</style>";

            dccsGupFamily.categories[0].cards[0].selectionWeight = 3;
            dccsGupFamily.AddCard(0, DSGeep);
            dccsGupFamily.AddCard(0, DSGip);

            RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGeepBody").directorCreditCost = 60;
            RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody").directorCreditCost = 25;
            RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVermin").directorCreditCost = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVerminSnowy").directorCreditCost;


            //Family Event Changes
            //0 is Normal
            //1 is Family
            //2 is Void
            RoR2.ExpansionManagement.ExpansionDef[] ExpansionNone = { };
            RoR2.ExpansionManagement.ExpansionDef[] ExpansionDLC1 = { };
            DccsPool.PoolEntry[] NoPoolEntries = { };


            DccsPool.ConditionalPoolEntry FamilyExtraVermin = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsVerminFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraBeetle = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsBeetleFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraClay = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsClayFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraGolemAbyssal = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsGolemFamilyAbyssal, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraParent = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsParentFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraLunar = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsLunarFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraImp = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsImpFamily, requiredExpansions = ExpansionNone };
            DccsPool.ConditionalPoolEntry FamilyExtraVoid = new DccsPool.ConditionalPoolEntry { weight = 1, dccs = dccsVoidFamily, requiredExpansions = ExpansionNone };
            DccsPool.Category CategoryFamilyArtifactWorld = new DccsPool.Category { categoryWeight = 0.02f, name = "Family", alwaysIncluded = NoPoolEntries, includedIfNoConditionsMet = NoPoolEntries };
            DccsPool.Category CategoryFamilyMoon2 = new DccsPool.Category { categoryWeight = 0.02f, name = "Family", alwaysIncluded = NoPoolEntries, includedIfNoConditionsMet = NoPoolEntries };


            /*for (int i = 0; i < AllDccsPools.Length; i++)
            {
                if (AllDccsPools[i].poolCategories.Length > 1)
                {
                    Debug.LogWarning(" ");
                    Debug.LogWarning(AllDccsPools[i].name);
                    for (int ii = 0; ii < AllDccsPools[i].poolCategories[1].includedIfConditionsMet.Length; ii++)
                    {
                        Debug.LogWarning("["+ ii+"] "+AllDccsPools[i].poolCategories[1].includedIfConditionsMet[ii].dccs.name);
                    }
                        
                }
                
            }*/

            dpGolemplainsMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsConstructFamily;

            dpSnowyForestMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsVerminFamilySnowy;
            dpSnowyForestMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsImpFamily;

            dpGooLakeMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsClayFamily;

            dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet = dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet.Remove(dpFoggySwampMonsters.poolCategories[1].includedIfConditionsMet[0]);


            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet = dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraVermin, FamilyExtraLunar);
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[0].dccs = dccsClayFamily;
            dpAncientLoftMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsLemurianFamily;

            dpFrozenWallMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsRoboBallFamily;

            dpWispGraveyardMonsters.poolCategories[1].includedIfConditionsMet = dpWispGraveyardMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraClay, FamilyExtraBeetle);

            dpSulfurPoolsMonsters.poolCategories[1].includedIfConditionsMet = dpSulfurPoolsMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraParent);

            dpDampCaveMonsters.poolCategories[1].includedIfConditionsMet = dpDampCaveMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraGolemAbyssal);

            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet = dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet.Remove(dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[4]);
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[2].dccs = dccsRoboBallFamily;
            dpShipgraveyardMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsConstructFamily;

            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet = dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraVermin);
            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet[3].dccs = dccsClayFamily;
            dpRootJungleMonsters.poolCategories[1].includedIfConditionsMet[1].dccs = dccsJellyfishFamily;

            dpSkyMeadowMonsters.poolCategories[1].includedIfConditionsMet[0].weight = 2;
            dpSkyMeadowMonsters.poolCategories[1].includedIfConditionsMet = dpSkyMeadowMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraParent);


            dpArtifactWorldMonsters.poolCategories = dpArtifactWorldMonsters.poolCategories.Add(CategoryFamilyArtifactWorld);
            dpArtifactWorldMonsters.poolCategories[1].includedIfConditionsMet = dpArtifactWorldMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraLunar, FamilyExtraImp);

            dpMoonMonsters.poolCategories = dpMoonMonsters.poolCategories.Add(CategoryFamilyMoon2);
            dpMoonMonsters.poolCategories[1].includedIfConditionsMet = dpMoonMonsters.poolCategories[1].includedIfConditionsMet.Add(FamilyExtraVoid);




            for (int i = 0; i < AllDccsPools.Length; i++)
            {
                //AllDccsPools[i].poolCategories[1].categoryWeight = 1;
            }


            if (EnemyChanges.Value == true)
            {



                //dccsGolemplainsMonstersDLC1

                dccsSnowyForestMonstersDLC1.categories[1].cards = dccsSnowyForestMonstersDLC1.categories[1].cards.Remove(dccsSnowyForestMonstersDLC1.categories[1].cards[2]); //Remove Reaver
                dccsSnowyForestMonstersDLC1.categories[1].cards[1] = DSBison; //Bison replaces Greater Wisp
                dccsSnowyForestMonstersDLC1.categories[2].cards[3].minimumStageCompletions = 0; //Pre Loop Vermin
                dccsSnowyForestMonstersDLC1.categories[2].cards = dccsSnowyForestMonstersDLC1.categories[2].cards.Remove(dccsSnowyForestMonstersDLC1.categories[2].cards[1]); //Remove Wisp


                dccsSulfurPoolsMonstersDLC1.categories[0].cards[2] = DSMagmaWorm; //Beetle Queen replaced by Magma Worm
                dccsSulfurPoolsMonstersDLC1.categories[2].cards[2] = DSAcidLarva; //Beetle replaced by Acid Larva
                dccsSulfurPoolsMonstersDLC1.AddCard(1, DSHermitCrab);


                dccsGooLakeMonstersDLC1.categories[1].cards[3].minimumStageCompletions = 0; //Pre Loop Clay Grenadier


                dccsFrozenWallMonstersDLC1.categories[0].cards[0] = DSRoboBallBoss; //Clay Dunestrider replaced by Solus Unit

                dccsWispGraveyardMonstersDLC1.categories[2].cards[3].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/Vermin/cscVermin.asset").WaitForCompletion();
                dccsWispGraveyardMonstersDLC1.categories[2].cards[3].minimumStageCompletions = 3;

                dccsRootJungleMonstersDLC1.categories[0].cards[1] = DSGrovetender; //Titan replaced by Grovetender
                dccsRootJungleMonstersDLC1.categories[1].cards[0].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/Golem/cscGolemNature.asset").WaitForCompletion();

                dccsShipgraveyardMonstersDLC1.AddCard(0, DSElectricWorm);

                dccsArtifactWorldMonstersDLC1.AddCard(0, DSGrovetender);
                dccsArtifactWorldMonstersDLC1.AddCard(0, DSMagmaWorm);
                dccsArtifactWorldMonstersDLC1.AddCard(1, DSClayGrenadier);
                //dccsArtifactWorldMonstersDLC1.AddCard(2, DSBlindVermin);
                //Bison ig

                dccsITFrozenWallMonsters.AddCard(0, SimuElectricWorm);
                dccsITDampCaveMonsters.AddCard(0, SimuElectricWorm);
                dccsITSkyMeadowMonsters.AddCard(0, SimuElectricWorm);

                /*
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage(DirectorAPI.Helpers.MonsterNames.StoneTitanDistantRoost, DirectorAPI.Stage.SunderedGrove);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage(DirectorAPI.Helpers.MonsterNames.Grovetender, DirectorAPI.Stage.SunderedGrove);
                DirectorAPI.Helpers.AddNewMonsterToStage(DSGrovetender, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SunderedGrove);

                DirectorAPI.Helpers.RemoveExistingMonsterFromStage(DirectorAPI.Helpers.MonsterNames.OverloadingWorm, DirectorAPI.Stage.SirensCall);
                DirectorAPI.Helpers.AddNewMonsterToStage(DSElectricWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SirensCall);

                DirectorAPI.Helpers.RemoveExistingMonsterFromStage(DirectorAPI.Helpers.MonsterNames.MagmaWorm, DirectorAPI.Stage.ArtifactReliquary);
                DirectorAPI.Helpers.AddNewMonsterToStage(DSMagmaWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.ArtifactReliquary);



                DirectorAPI.Helpers.AddNewMonsterToStage(DSMagmaWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.Custom, "sulfurpools");
                DirectorAPI.Helpers.AddNewMonsterToStage(DSElectricWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.Custom, "sulfurpools");
                */


            }





            if (EnemyChangesLooping.Value == true)
            {

                DirectorCard LoopLunarExploder = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 7,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopLunarGolem = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 7,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopLunarWisp = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 7,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };

                DirectorCard SimuLoopVulture = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 3,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopVulture = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 3,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopMiniMushroom = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMiniMushroom"),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 3,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopJellyfish = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscJellyfish"),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 2,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };

                DirectorCard LoopElderLemurian = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser"),

                    preventOverhead = false,
                    selectionWeight = 1,
                    minimumStageCompletions = 3,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopBlindPest = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscFlyingVermin"),
                    preventOverhead = true,
                    selectionWeight = 1,
                    minimumStageCompletions = 5,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Far
                };

                DirectorCard LoopImp = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImp"),
                    preventOverhead = true,
                    selectionWeight = 1,
                    minimumStageCompletions = 3,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };

                DirectorCard LoopImpBoss = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImpBoss"),
                    preventOverhead = true,
                    selectionWeight = 1,
                    minimumStageCompletions = 3,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };

                DirectorCard LoopParent = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscParent"),
                    preventOverhead = false,
                    selectionWeight = 1,
                    minimumStageCompletions = 4,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopGrandparent = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 4,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopClayGrenadier = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 4,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopHermitCrab = new DirectorCard
                {
                    spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 2,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };

                DirectorCard LoopMinorConstruct = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/MajorAndMinorConstruct/cscMinorConstruct.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 2,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopMegaConstruct = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/MajorAndMinorConstruct/cscMegaConstruct.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 2,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopRoboBallBoss = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/RoboBallBoss/cscRoboBallBoss.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 4,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };


                DirectorCard SimuLoopGrovetender = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Gravekeeper/cscGravekeeper.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 4,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };

                DirectorCard SimuLoopVagrant = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Vagrant/cscVagrant.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 2,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };

                DirectorCard SimuLoopGup = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/Gup/cscGupBody.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 2,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard SimuLoopBrass = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Bell/cscBell.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 2,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard SimuLoopGreaterWisp = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/GreaterWisp/cscGreaterWisp.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 3,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard SimuLoopGolemSandy = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Golem/cscGolemSandy.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = false,
                    minimumStageCompletions = 3,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };



                DirectorCard SimuLoopVoidBarnacle = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidBarnacle/cscVoidBarnacle.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 5,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard SimuLoopVoidReaver = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Nullifier/cscNullifier.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 3,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard SimuLoopVoidJailer = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 5,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard SimuLoopVoidDevestator = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 5,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard LoopBell = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Bell/cscBell.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 2,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };

                DirectorCard DoubleLoopVoidBarnacle = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidBarnacle/cscVoidBarnacle.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 5,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard DoubleLoopVoidReaver = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Nullifier/cscNullifier.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 5,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard DoubleLoopVoidJailer = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 5,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                DirectorCard DoubleLoopVoidDevestator = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                    selectionWeight = 1,
                    preventOverhead = true,
                    minimumStageCompletions = 5,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };


                dccsITGolemplainsMonsters.AddCard(0, LoopMegaConstruct);
                dccsITGolemplainsMonsters.AddCard(2, LoopMinorConstruct);
                dccsITGolemplainsMonsters.AddCard(2, LoopHermitCrab);


                //dccsITGooLakeMonsters.AddCard(0, SimuLoopVagrant);
                //dccsITGooLakeMonsters.AddCard(1, SimuLoopGup);
                dccsITGooLakeMonsters.AddCard(1, SimuLoopGolemSandy);
                //Has Loop Templar by default

                dccsITAncientLoftMonsters.AddCard(1, LoopElderLemurian);
                dccsITAncientLoftMonsters.AddCard(2, LoopJellyfish);

                dccsITFrozenWallMonsters.AddCard(0, LoopRoboBallBoss);
                dccsITFrozenWallMonsters.AddCard(2, SimuLoopVulture);

                dccsITDampCaveMonsters.AddCard(0, SimuLoopGrovetender);
                dccsITDampCaveMonsters.AddCard(1, LoopMiniMushroom);

                dccsITSkyMeadowMonsters.AddCard(1, SimuLoopGreaterWisp);
                dccsITSkyMeadowMonsters.AddCard(2, SimuLoopBrass);

                dccsITMoonMonsters.AddCategory("Minibosses", 1);
                dccsITMoonMonsters.AddCard(1, SimuLoopVoidDevestator);
                dccsITMoonMonsters.AddCard(1, SimuLoopVoidReaver);
                dccsITMoonMonsters.AddCard(1, SimuLoopVoidJailer);
                dccsITMoonMonsters.AddCard(1, SimuLoopVoidBarnacle);
                dccsITMoonMonsters.categories[0].cards[0].selectionWeight = 3;
                dccsITMoonMonsters.categories[0].cards[1].selectionWeight = 3;
                dccsITMoonMonsters.categories[0].cards[1].selectionWeight = 2;

                //Golem Plains already has Lamps and Hermit Crab
                dccsBlackBeachMonstersDLC.AddCard(2, LoopVulture);

                dccsSnowyForestMonstersDLC1.AddCard(0, LoopImpBoss); //Loop Imps
                dccsSnowyForestMonstersDLC1.AddCard(2, LoopImp);


                //Goolake already has Templar
                dccsGooLakeMonstersDLC1.AddCard(2, LoopAcidLarva);

                dccsFoggySwampMonstersDLC.AddCard(2, LoopMiniMushroom);

                dccsAncientLoftMonstersDLC1.AddCard(0, SimuLoopGrovetender);
                dccsAncientLoftMonstersDLC1.AddCard(1, LoopElderLemurian); //Loop Elder Lemurian
                //
                dccsFrozenWallMonstersDLC1.AddCard(2, LoopVulture);

                dccsWispGraveyardMonstersDLC1.AddCard(1, DSClayGrenadier);

                dccsSulfurPoolsMonstersDLC1.AddCard(0, LoopGrandparent); //Loop Parents
                dccsSulfurPoolsMonstersDLC1.AddCard(2, LoopParent);

                dccsDampCaveMonstersDLC1.AddCard(0, LoopGrandparent); //Loop Parents
                dccsDampCaveMonstersDLC1.AddCard(1, LoopParent);

                dccsShipgraveyardMonstersDLC1.AddCard(1, LoopRoboBallMini);

                dccsRootJungleMonstersDLC1.AddCard(2, LoopBlindPest);

                dccsSkyMeadowMonstersDLC1.AddCard(2, LoopLunarWisp);
                dccsSkyMeadowMonstersDLC1.AddCard(2, LoopLunarGolem);
                dccsSkyMeadowMonstersDLC1.AddCard(2, LoopLunarExploder);


                //VoidStuff
                //dccsGolemplainsMonstersDLC1.AddCard(1, Void);
                //dccsBlackBeachMonstersDLC.AddCard(2, DoubleLoopVoidBarnacle);
                dccsSnowyForestMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);

                //dccsGooLakeMonstersDLC1.AddCard(1, DoubleLoopVoidJailer);
                dccsFoggySwampMonstersDLC.AddCard(1, DoubleLoopVoidJailer);
                //dccsAncientLoftMonstersDLC1.AddCard(0, DoubleLoopVoidDevestator); 
                //
                dccsFrozenWallMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);
                //dccsWispGraveyardMonstersDLC1.AddCard(1, DoubleLoopVoidJailer);
                //dccsSulfurPoolsMonstersDLC1.AddCard(0, DoubleLoopVoidDevestator);

                dccsDampCaveMonstersDLC1.AddCard(0, DoubleLoopVoidDevestator);
                dccsShipgraveyardMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);
                dccsRootJungleMonstersDLC1.AddCard(1, DoubleLoopVoidJailer);

                dccsSkyMeadowMonstersDLC1.AddCard(0, DoubleLoopVoidDevestator);
                //dccsSkyMeadowMonstersDLC1.AddCard(1, DoubleLoopVoidJailer);
                //dccsSkyMeadowMonstersDLC1.AddCard(1, DoubleLoopVoidReaver);

            }




           

            if (InteractableCostChanges.Value == true)
            {
                //DirectorAPI.Helpers.AddSceneInteractableCredits(40, DirectorAPI.Stage.DistantRoost);
                if (VoidPotentialLootChanges.Value == true)
                {
                    Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidTriple/VoidTriple.prefab").WaitForCompletion().GetComponent<RoR2.OptionChestBehavior>().dropTable = dtAllTier;
                }


                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/DuplicatorWild/iscDuplicatorWild.asset").WaitForCompletion().directorCreditCost = 15;


                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidTriple/iscVoidTriple.asset").WaitForCompletion().directorCreditCost = 25;
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidSuppressor/iscVoidSuppressor.asset").WaitForCompletion().directorCreditCost = 5;
                //Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidCoinBarrel/iscVoidCoinBarrel.asset").WaitForCompletion().directorCreditCost = 15;
                RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscLunarChest").directorCreditCost = 14;
                RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscLunarChest").requiredFlags = 0;
                RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscBrokenMegaDrone").directorCreditCost = 25;
                RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscShrineRestack").directorCreditCost = 10;
                RoR2.LegacyResourcesAPI.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCard/iscShrineHealing").directorCreditCost = 10;

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBlood.asset").WaitForCompletion().directorCreditCost = 15;
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBloodSandy.asset").WaitForCompletion().directorCreditCost = 15;
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBloodSnowy.asset").WaitForCompletion().directorCreditCost = 15;

                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombat.asset").WaitForCompletion().directorCreditCost = 15;
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombatSandy.asset").WaitForCompletion().directorCreditCost = 15;
                Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCombat/iscShrineCombatSnowy.asset").WaitForCompletion().directorCreditCost = 15;


            }

        }








        //public static int indexSc;
        //public static string scavloottemp;
        public static bool FirstScav = true;
        public static ExplicitPickupDropTable DropTableForBossScav = null;
        //public static PickupDef ScavLootPickupdef;
        //public static SerializablePickupIndex BossPickupScav = new SerializablePickupIndex() { pickupName = "ItemIndex.ShinyPearl" };

        public static void ScavThingRandomizer()
        {
            DropTableForBossScav = AllScavCompatibleDropTables[random.Next(AllScavCompatibleDropTables.Count)];

            /*
            int indexSc = random.Next(Run.instance.availableBossDropList.Count);
            PickupIndex temppick = Run.instance.availableBossDropList[indexSc];
            ScavLootPickupdef = PickupCatalog.GetPickupDef(temppick);
            string tempname = ScavLootPickupdef.internalName;



            BossPickupScav.pickupName = tempname;
            scavloottemp = tempname.Replace("ItemIndex.", "");
            */
            //FirstScav = true;
            //Debug.LogWarning(tempname);
            //Debug.LogWarning(scavloottemp);
            //Debug.LogWarning(BossPickupScav.pickupName);

        }




        public static void MinionsInheritNoDrones(On.RoR2.MinionOwnership.MinionGroup.orig_AddMinion orig, NetworkInstanceId ownerId, global::RoR2.MinionOwnership minion)
        {

            orig(ownerId, minion);

            if (!NetworkServer.active) { return; }
            if (minion.ownerMaster.inventory && minion.ownerMaster.inventory.GetEquipmentIndex() != EquipmentIndex.None)
            {
                //Debug.LogWarning(EquipmentCatalog.GetEquipmentDef(minion.ownerMaster.inventory.GetEquipmentIndex()).name);
                if (minion.ownerMaster.inventory.currentEquipmentState.equipmentDef.name.StartsWith("Elite"))
                {
                    if (minion.name.Contains("SquidTurret") || minion.name.Contains("AllyMaster") || minion.name.Contains("Buddy") || minion.name.Contains("ParentPodMaster"))
                    {
                        Inventory inventory = minion.gameObject.GetComponent<Inventory>();
                        //inventory.SetEquipmentIndex(minion.ownerMaster.inventory.GetEquipmentIndex());
                        inventory.SetEquipment(new EquipmentState(minion.ownerMaster.inventory.GetEquipmentIndex(), Run.FixedTimeStamp.negativeInfinity, 0), 0);
                        inventory.GiveItem(RoR2Content.Items.BoostDamage, 10);
                        inventory.GiveItem(RoR2Content.Items.BoostHp, 30);
                    }
                }
            }

        }

        public static void MinionsInheritWithDrones(On.RoR2.MinionOwnership.MinionGroup.orig_AddMinion orig, NetworkInstanceId ownerId, global::RoR2.MinionOwnership minion)
        {

            orig(ownerId, minion);

            if (!NetworkServer.active) { return; }
            if (minion.ownerMaster.inventory && minion.ownerMaster.inventory.GetEquipmentIndex() != EquipmentIndex.None)
            {
                //Debug.LogWarning(EquipmentCatalog.GetEquipmentDef(minion.ownerMaster.inventory.GetEquipmentIndex()).name);
                if (minion.ownerMaster.inventory.currentEquipmentState.equipmentDef.name.StartsWith("Elite"))
                {
                    if (minion.name.Contains("Turret") || minion.name.Equals("MinorConstructOnKillMaster(Clone)") || minion.name.Equals("MinorConstructAttachableMaster(Clone)") || minion.name.Equals("VoidMegaCrabMaster(Clone)") || minion.name.Contains("AllyMaster") || minion.name.Contains("Buddy") || minion.name.Contains("ParentPodMaster") || minion.name.Contains("Drone"))
                    {
                        Inventory inventory = minion.gameObject.GetComponent<Inventory>();
                        //inventory.SetEquipmentIndex(minion.ownerMaster.inventory.GetEquipmentIndex());
                        inventory.SetEquipment(new EquipmentState(minion.ownerMaster.inventory.GetEquipmentIndex(), Run.FixedTimeStamp.negativeInfinity, 0), 0);
                        inventory.GiveItem(RoR2Content.Items.BoostDamage, 10);
                        inventory.GiveItem(RoR2Content.Items.BoostHp, 30);
                    }
                }
            }

        }



        public static void ClassicStageInfoMethod(On.RoR2.ClassicStageInfo.orig_Awake orig, global::RoR2.ClassicStageInfo self)
        {
            orig(self);
            ScavThingRandomizer();
        }

    }



    public class InfiniteTowerMaxWaveCountPrerequisites : InfiniteTowerWavePrerequisites
    {
        public override bool AreMet(InfiniteTowerRun run)
        {
            return run.waveIndex <= this.maximumwavecount;
        }


        [SerializeField]
        public int maximumwavecount;
    }

}