using BepInEx;
using HarmonyLib;
using static MenkerMenu.Initialization.PluginInfo;

namespace MenkerMenu.Initialization
{
    [BepInPlugin(menuGUID, menuName, menuVersion)]
    public class BepInExInitializer : BaseUnityPlugin
    {
        public static BepInEx.Logging.ManualLogSource LoggerInstance;

        void Awake()
        {
            LoggerInstance = Logger;
            new Harmony(menuGUID).PatchAll();
        }
    }
}
