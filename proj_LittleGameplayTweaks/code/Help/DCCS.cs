using HG;
using RoR2;
using UnityEngine;

namespace LittleGameplayTweaks
{
    public class DCCS
    {
        //RiskyMod broke my spawn pool changes maybe other mods do too so I guess we'll just do this
        public static int FindSpawnCard(DirectorCard[] insert, string LookingFor)
        {
            for (int i = 0; i < insert.Length; i++)
            {
                if (insert[i].spawnCard.name.EndsWith(LookingFor))
                {
                    //Debug.Log("Found " + LookingFor);
                    return i;
                }
            }
            Debug.LogWarning("Couldn't find " + LookingFor);
            return -1;
        }


        public static void RemoveCard(DirectorCardCategorySelection dccs, int cat, int card)
        {
            ArrayUtils.ArrayRemoveAtAndResize(ref dccs.categories[cat].cards, card);
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