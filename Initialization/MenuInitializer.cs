using HarmonyLib;
using UnityEngine;
using System;
using MenkerMenu.Utilities;
using MenkerMenu.Menu;

namespace MenkerMenu.Initialization
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player), "LateUpdate")]
    internal class MenuInitializer
    {
        private static GameObject menuObject = null;

        static void Postfix()
        {
            if (menuObject != null && GameObject.Find(PluginInfo.menuName) != null) return;

            if (menuObject != null)
            {
                Debug.LogWarning($"{PluginInfo.menuName} was unexpectedly destroyed. Reinitializing...");
            }

            try
            {
                menuObject = new GameObject(PluginInfo.menuName);
                Debug.Log($"Initializing {PluginInfo.menuName}...");
                menuObject.AddComponent<Main>();
                menuObject.AddComponent<NotificationLib>();
                GameObject.DontDestroyOnLoad(menuObject);
                Debug.Log($"{PluginInfo.menuName} successfully initialized.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to initialize {PluginInfo.menuName}: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
