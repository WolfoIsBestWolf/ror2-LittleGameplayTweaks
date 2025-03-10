using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class LakesNightVillageNight
    {
        public static void EditDccs()
        {
            DirectorCardCategorySelection dccsLakesnightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesnightInteractables_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightInteractables_DLC1.asset").WaitForCompletion();
            try
            {
                //dccsLakesnightMonsters.categories[0].cards[1].minimumStageCompletions = 1; //Grandparent
                dccsLakesnightMonsters.categories[0].cards[4].minimumStageCompletions = 2; //Imp Boss
                dccsLakesnightMonsters.categories[1].cards[2].minimumStageCompletions = 2; //Elder Lemurian
                dccsLakesnightMonsters.categories[1].cards[3].minimumStageCompletions = 4; //Void Reaver
                //dccsLakesnightMonsters.categories[2].cards[2].minimumStageCompletions = 1; //Imp

                dccsLakesnightInteractables_DLC1.categories[0].cards[0].minimumStageCompletions = 1; //Void Camp
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Error with Dccs WLoop");
                Debug.LogWarning(e);
            }

        }


        public static void LakesNight_AddStage1FriendlyMonsters(DirectorCardCategorySelection dccs)
        {
            if (dccs == null || !LoopVariantsMain.AddMonsters)
            {
                return;
            }

            //Add Vagrant?

        }

        public static void VillageNight_AddStage1FriendlyMonsters(DirectorCardCategorySelection dccs)
        {
            if (dccs == null || !LoopVariantsMain.AddMonsters)
            {
                return;
            }

            //Add Titan Queen

        }

    }
}