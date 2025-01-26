﻿using HarmonyLib;
using static MenkerMenu.Utilities.Variables;

namespace MenkerMenu.Utilities.Patches
{
    [HarmonyPatch(typeof(VRRig), "OnDisable", MethodType.Normal)]
    public static class RigPatch
    {
        public static bool Prefix(VRRig __instance)
        {
            return !(__instance == taggerInstance.offlineVRRig);
        }
    }

    [HarmonyPatch(typeof(VRRigJobManager), "DeregisterVRRig")]
    public static class RigPatch2
    {
        public static bool Prefix(VRRigJobManager __instance, VRRig rig)
        {
            return !rig.isOfflineVRRig;
        }
    }
}
