using System;
using System.Collections.Generic;
using System.Text;
using GorillaLocomotion;
using static MenkerMenu.Menu.Main;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Utilities.ColorLib;
using static MenkerMenu.Menu.ButtonHandler;
using static MenkerMenu.Mods.ModButtons;
using static MenkerMenu.Mods.Categories.Settings;
using UnityEngine;
using BepInEx;
using MenkerMenu.Utilities;
using UnityEngine.InputSystem;
using GorillaNetworking;
using static MenkerMenu.Utilities.HandOrbs;
using Photon.Pun;

namespace MenkerMenu.Mods.Categories
{
    public class Fun
    {
        public static void CopySelfID()
        {
            string id = PhotonNetwork.LocalPlayer.UserId;
            NotificationLib.SendNotification("<color=grey>[</color><color=green>SUCCESS</color><color=grey>]</color> " + id);
        }
        public static void GrabBug()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Bug.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                Bug.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            }
        }
        public static void BugGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                Bug.transform.position = GunTemplate.spherepointer.transform.position;
            }, false);
        }
        public static void GrabBat()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Bat.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                Bat.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            }
        }
        public static void BatGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                Bat.transform.position = GunTemplate.spherepointer.transform.position;
            }, false);
        }
        public static void SnipeBug()
        { 
            GorillaTagger.Instance.rightHandTransform.transform.position = Bug.transform.position;
        }
        public static void SnipeBat()
        {
            GorillaTagger.Instance.rightHandTransform.transform.position = Bat.transform.position;
        }

        public static GameObject Bat = GameObject.Find("Cave Bat Holdable");
        public static GameObject Bug = GameObject.Find("Floating Bug Holdable");
    }
}
