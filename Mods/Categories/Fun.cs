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
using static MenkerMenu.Utilities.GunTemplate;
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
            GUIUtility.systemCopyBuffer = id;
        }
        public static void GrabRocket()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GameObject rocket = GameObject.Find("RocketShip_Prefab");
                rocket.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                rocket.transform.position = new Vector3(0f, -0.0075f, 0f);
                rocket.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                rocket.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
            }
        }
        public static void GiveRocketAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                GameObject rocket = GameObject.Find("RocketShip_Prefab");
                rocket.transform.position = rig.transform.position;
            }
        }


        public static void RocketClosest()
        {
            GameObject rocket = GameObject.Find("RocketShip_Prefab");
            rocket.transform.position = RigManager.GetClosestVRRig().transform.position;
        }

        public static void RocketGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GameObject rocket = GameObject.Find("RocketShip_Prefab");
                rocket.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                rocket.transform.position = GunTemplate.spherepointer.transform.position;
            }, false);
        }
        public static void RocketAura()
        {
            GameObject rocket = GameObject.Find("RocketShip_Prefab");
            rocket.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            rocket.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));
            rocket.transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
        }
        public static void RocketHalo()
        {
            GameObject rocket = GameObject.Find("RocketShip_Prefab");
            rocket.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            rocket.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(MathF.Cos((float)Time.frameCount / 30), 1f, MathF.Sin((float)Time.frameCount / 30));
            rocket.transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
        }
        public static void CopyIDGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                string id = LockedPlayer.Creator.UserId;
                GUIUtility.systemCopyBuffer = id;
            }, true);
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
