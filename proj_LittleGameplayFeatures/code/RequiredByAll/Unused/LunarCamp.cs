﻿using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MiscContent
{
    public class LunarCamp
    {
        public static InteractableSpawnCard iscLunarCamp = Object.Instantiate(Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/DLC1/VoidCamp/iscVoidCamp.asset").WaitForCompletion());

        public static void Start()
        {

            DirectorCardCategorySelection dccsLunarCampMonsters = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            DirectorCardCategorySelection dccsLunarCampInteractables = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            DirectorCardCategorySelection dccsLunarCampFlavorProps = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();

            DirectorCard LoopLunarExploder = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 10,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarGolem = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard LoopLunarWisp = new DirectorCard
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            iscLunarCamp.name = "iscLunarCamp";
            dccsLunarCampMonsters.name = "dccsLunarCampMonsters";
            dccsLunarCampMonsters.AddCategory("Minibosses", 6);
            dccsLunarCampMonsters.AddCard(0, LoopLunarExploder);
            dccsLunarCampMonsters.AddCard(0, LoopLunarGolem);
            dccsLunarCampMonsters.AddCard(0, LoopLunarWisp);


            dccsLunarCampInteractables.name = "dccsLunarCampInteractables";
            dccsLunarCampInteractables.AddCategory("Chests", 6);


            dccsLunarCampFlavorProps.name = "dccsLunarCampFlavorProps";

        }
    }

}