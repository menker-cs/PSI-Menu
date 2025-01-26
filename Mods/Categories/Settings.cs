﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem;
using UnityEngine;
using Object = UnityEngine.Object;
using static MenkerMenu.Utilities.ColorLib;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Menu.Main;
using static MenkerMenu.Menu.ButtonHandler;
using static MenkerMenu.Menu.Optimizations;
using MenkerMenu.Utilities;
using MenkerMenu.Menu;
using static MenkerMenu.Mods.Categories.Move;
using static MenkerMenu.Utilities.Patches.OtherPatches;
using static MenkerMenu.Utilities.GunTemplate;
using System.Linq;
using Oculus.Platform;

namespace MenkerMenu.Mods.Categories
{
    public class Settings
    {
        public static void SwitchHands(bool setActive)
        {
            rightHandedMenu = setActive;
        }

        public static void ClearNotifications()
        {
            NotificationLib.ClearAllNotifications();
        }

        public static void ToggleNotifications(bool setActive)
        {
            toggleNotifications = setActive;
        }

        public static void ToggleDisconnectButton(bool setActive)
        {
            toggledisconnectButton = setActive;
        }
        public static void FlySpeed()
        {
            flyspeedchanger++;
            if (flyspeedchanger > 4)
            {
                flyspeedchanger = 1;
            }
            if (flyspeedchanger == 1)
            {
                flyspeedchangerspeed = 15f;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>Fly Speed</color><color=white>] Normal</color>");
            }
            if (flyspeedchanger == 2)
            {
                flyspeedchangerspeed = 7f;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>Fly Speed</color><color=white>] Slow</color>");
            }
            if (flyspeedchanger == 3)
            {
                flyspeedchangerspeed = 30f;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>Fly Speed</color><color=white>] Fast</color>");
            }
            if (flyspeedchanger == 4)
            {
                flyspeedchangerspeed = 60f;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>Fly Speed</color><color=white>] Very Fast</color>");
            }

        }
        public static void SpeedSpeed()
        {
            speedboostchanger++;
            if (speedboostchanger > 4)
            {
                speedboostchanger = 1;
            }
            if (speedboostchanger == 1)
            {
                speedboostchangerspeed = 8f;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>Speed:</color><color=white>] Normal</color>");
            }
            if (speedboostchanger == 2)
            {
                speedboostchangerspeed = 6f;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>Speed:</color><color=white>] Slow</color>");
            }
            if (speedboostchanger == 3)
            {
                speedboostchangerspeed = 30f;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>Speed:</color><color=white>] Fast</color>");
            }
            if (speedboostchanger == 4)
            {
                speedboostchangerspeed = 99f;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>Speed:</color><color=white>] Very Fast</color>");
            }

        }
        public static void ESPChange()
        {
            espSetting++;
            if (espSetting > 4)
            {
                espSetting = 1;
            }
            if (espSetting == 1)
            {
                espColor = 1;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>ESP Color:</color><color=white>] Infection</color>");
            }
            if (espSetting == 2)
            {
                espColor = 2;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>ESP Color:</color><color=white>] Casual</color>");
            }
            if (espSetting == 3)
            {
                espColor = 3;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>ESP Color:</color><color=white>] RGB</color>");
            }
            if (espSetting == 4)
            {
                espColor = 4;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>ESP Color:</color><color=white>] Menu Color</color>");
            }
        }
        public static void VisReport(bool Enabled)
        {
            if (Enabled)
            {
                VisReportBool = true;
            }
            else
            {
                VisReportBool = false;
            }
        }
        public static void Discord()
        {
            UnityEngine.Application.OpenURL("https://discord.gg/WFJ9nJQxnr");
        }

        public static int espColor = 1;
        public static int espSetting;

        public static int speedboostchanger;
        public static float speedboostchangerspeed = 15;

        public static int flyspeedchanger;
        public static float flyspeedchangerspeed = 15;

        public static bool VisReportBool = false;
    }
}
