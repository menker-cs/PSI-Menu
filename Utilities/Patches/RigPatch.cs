using HarmonyLib;
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
}
