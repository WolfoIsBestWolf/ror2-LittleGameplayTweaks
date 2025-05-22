using UnityEngine;

namespace LoopVariants
{
    public class Tools
    {


        public static void UpTransformByY(Transform transform, float amount)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + amount, transform.localPosition.z);
        }
    }
}