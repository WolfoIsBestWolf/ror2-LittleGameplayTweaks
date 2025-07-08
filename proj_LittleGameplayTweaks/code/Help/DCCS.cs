using HG;
using RoR2;
using UnityEngine;

namespace LittleGameplayTweaks
{
    public class DCCS
    {
        //RiskyMod broke my spawn pool changes maybe other mods do too so I guess we'll just do this
        public static DirectorCard FindSpawnCard(DirectorCard[] insert, string LookingFor)
        {
            for (int i = 0; i < insert.Length; i++)
            {
                if (insert[i].spawnCard.name.EndsWith(LookingFor))
                {
                    //Debug.Log("Found " + LookingFor);
                    return insert[i];
                }
            }
            Debug.LogWarning("Couldn't find " + LookingFor);
            return null;
        }


        public static void RemoveCard(DirectorCardCategorySelection dccs, int cat, int card)
        {
            ArrayUtils.ArrayRemoveAtAndResize(ref dccs.categories[cat].cards, card);
        }

        public enum Category
        {
            Damage,
            Healing,
            Utility,
        }
        public static void MatchCategoryChests(DirectorCardCategorySelection.Category category, int start, int cat)
        {
            var keep = category.cards[start + (int)cat];
            category.cards[start] = keep;
            category.cards[start+1] = keep;
            category.cards[start+2] = keep;

        }

        public static void MultWholeCateory(DirectorCardCategorySelection dccs, int category, int mult)
        {
            for (int i = 0; i < dccs.categories[category].cards.Length; i++)
            {
                dccs.categories[category].cards[i].selectionWeight *= mult;
            }
        }

    }
}