using RoR2;
using RoR2.ExpansionManagement;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WolfoLibrary;

namespace LittleGameplayTweaks
{
    
    public class DCCS_ApplyAsNeeded
    {
        public static void InteractableDCCS_Changes(DirectorCardCategorySelection dccs)
        {
            if (dccs == null)
            {
                return;
            }

            //Blended DCCS have stuff mixed around often
            int chestIndex = dccs.FindCategoryIndexByName("Chests");
            //int shrineIndex = dccs.FindCategoryIndexByName("Shrines");
            //int droneIndex = dccs.FindCategoryIndexByName("Drones");
            int duplicatorIndex = dccs.FindCategoryIndexByName("Duplicator");
            int rareIndex = dccs.FindCategoryIndexByName("Rare");
            int voidIndex = dccs.FindCategoryIndexByName("Void Stuff");
            int stormIndex = dccs.FindCategoryIndexByName("Storm Stuff");
 
            //Ideally Void Category would shrink based on how many void items or smth, not plainly just expanions

            try
            {
 
                bool dlcStage = SceneInfo.instance.sceneDef.requiredExpansion != null;

            
                float mult = 2f / ((float)ClassicStageInfo.instance.interactableCategories.expansionsInEffect.Count + (dlcStage ? 1f : 0f));
                if(mult > 1f)
                {
                    mult = 1f;
                }
 
                DirectorCardCategorySelection.Category category;
                if (chestIndex != -1)
                {
                    category = dccs.categories[chestIndex];
                    for (int card = 0; card < category.cards.Length; card++)
                    {
                        if (category.cards[card].spawnCard.name.StartsWith("iscCategoryChest2"))
                        {
                            if (SceneInfo.instance.sceneDef.baseSceneName != "helminthroost")
                            {
                                category.cards[card].selectionWeight = 8;
                            }
                            break;
                        }
                        /*else if (category.cards[card].spawnCard.name == "iscCategoryChestDamage")
                        {
                            MatchCategoryChestsToLarge(category, card);
                            break;
                        }*/
                    }
                }
                /*if (shrineIndex != -1)
                {
                    category = dccs.categories[shrineIndex];
                }
                if (droneIndex != -1)
                {
                    category = dccs.categories[droneIndex];
                }*/
                if (duplicatorIndex != -1)
                {
                    category = dccs.categories[duplicatorIndex];
                    for (int card = 0; card < category.cards.Length; card++)
                    {
                        if (category.cards[card].GetSpawnCard().name.EndsWith("Military"))
                        {
                            category.cards[card].selectionWeight = 2;
                            //category.cards[card].minimumStageCompletions = 3;
                        }
                        else if (category.cards[card].GetSpawnCard().name.EndsWith("iscDrone"))
                        {
                            //Drone Scrapper
                            //Drone Combiner

                            //Increase weight
                            category.cards[card].selectionWeight = 10;
                        }
                       
                    }
                }
                if (rareIndex != -1)
                {
                    //Slightly more Rare for fun
                    dccs.categories[rareIndex].selectionWeight += 0.05f;
                }
                if (voidIndex != -1)
                {
                    //Slightly more void for fun
                    //But good chunk more for VV
                    if (DCCS_Interactables.VanillaVoidsInstalled)
                    {
                        dccs.categories[voidIndex].selectionWeight += 1f;
                    }
                    dccs.categories[voidIndex].selectionWeight *= mult;
                }
                if (stormIndex != -1)
                {
                    category = dccs.categories[stormIndex];
                    dccs.categories[stormIndex].selectionWeight *= mult;
 
                    if (SceneInfo.instance.sceneDef.stageOrder == 1)
                    {
                        dccs.categories[stormIndex].selectionWeight *= 0.66f;
                    }

                    /*for (int card = 0; card < category.cards.Length; card++)
                    {
                        if (category.cards[card].GetSpawnCard().name.EndsWith("ess"))
                        {
                            //Revive Shrine not on Stage 2
                            category.cards[card].minimumStageCompletions = 2;
                            break;
                        }
                    }*/
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("REPORT THIS");
                Debug.LogWarning(e);
            }
        }


        public static void RemoveMonsterBasedOnSotVReplacement(DirectorCardCategorySelection dccs)
        {
            if (!WConfig.cfgSotV_EnemyRemovals.Value)
            {
                return;
            }
            if (Run.instance && Run.instance.IsExpansionEnabled(DLCS.DLC1))
            {
                string scene = SceneInfo.instance.sceneDef.baseSceneName;
                switch (scene)
                {
                    case "golemplains":
                        //They remove my guy jellyfish? wtf
                        DccsUtil.RemoveCard(dccs, 2, "fish"); //-> Alpha
                        break;
                    case "frozenwall":
                        DccsUtil.RemoveCard(dccs, 2, "Lemurian"); //-> Vermin
                        DccsUtil.RemoveCard(dccs, 2, "Wisp"); //-> Pest
                        //Reaver
                        break;
                    case "dampcavesimple":
                        DccsUtil.RemoveCard(dccs, 1, "GreaterWisp"); //-> Gup
                        break;
                    case "shipgraveyard":
                        DccsUtil.RemoveCard(dccs, 2, "Beetle"); //-> Larva
                        //Reaver
                        break;
                    case "rootjungle":
                        DccsUtil.RemoveCard(dccs, 1, "LemurianBruiser"); //-> Gup
                        DccsUtil.RemoveCard(dccs, 2, "Lemurian"); //-> Larva
                        break;
                    case "skymeadow":
                        //RemoveCard(dccs, 0, "MagmaWorm"); //-> XI
                        //Reaver
                        break;
                }
            }
        }

    }


}