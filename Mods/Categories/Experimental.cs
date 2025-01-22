using System;
using System.Collections.Generic;
using System.Text;
using GorillaLocomotion;
using static MenkerMenu.Menu.Main;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Utilities.ColorLib;
using static MenkerMenu.Menu.ButtonHandler;
using static MenkerMenu.Mods.ModButtons;
using static MenkerMenu.Utilities.GunTemplate;
using static MenkerMenu.Mods.Categories.Settings;
using static MenkerMenu.Utilities.RigManager;
using UnityEngine;
using BepInEx;
using MenkerMenu.Utilities;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using Photon.Pun;
using System.Threading.Tasks;

namespace MenkerMenu.Mods.Categories
{
    public class Experimental
    {
        public static void CopyIDGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                string id = LockedPlayer.Creator.UserId;
                GUIUtility.systemCopyBuffer = id;
            }, true);
        }
    }
}
