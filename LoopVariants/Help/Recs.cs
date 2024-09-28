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