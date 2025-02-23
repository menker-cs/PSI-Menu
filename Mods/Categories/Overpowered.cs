using System;
using System.Collections.Generic;
using System.Text;
using static MenkerMenu.Utilities.GunTemplate;
using static MenkerMenu.Utilities.ColorLib;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Utilities.HandOrbs;
using static MenkerMenu.Menu.Main;
using static MenkerMenu.Utilities.RigManager;
using static MenkerMenu.Mods.Categories.Settings;
using static MenkerMenu.Mods.Categories.Safety;
using UnityEngine;
using static MenkerMenu.Mods.Categories.Guardian;
using UnityEngine.InputSystem;
using MenkerMenu.Utilities;
using UnityEngine.XR;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit;
using GorillaNetworking;
using MenkerMenu.Utilities.Patches;
using MenkerMenu.Menu;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using HarmonyLib;
using BepInEx;
using MenkerMenu.Utilities;
using GorillaTagScripts;
using System.Threading;
using PlayFab.ProfilesModels;
using System.Linq;
using System.Reflection;
using PlayFab;

namespace MenkerMenu.Mods.Categories
{
    public class Overpowered
    {
        public static void bangun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                if (GunTemplate.LockedPlayer)
                {
                    PlayFabAuthenticationAPI.ForgetAllCredentials();
                }

            }, true);
        }
        public static void LagAll()
        {
            bool lag = Time.time > r;
            if (lag)
            {
                r = Time.time + 0.2f;
                for (int i = 0; i < 20; i++)
                {
                    FriendshipGroupDetection.Instance.photonView.RPC("RequestPartyGameMode", Photon.Pun.RpcTarget.Others, new object[1]);
                    FriendshipGroupDetection.Instance.photonView.RPC("NotifyPartyGameModeChanged", Photon.Pun.RpcTarget.Others, new object[1]);
                    FriendshipGroupDetection.Instance.photonView.RPC("VerifyPartyMember", Photon.Pun.RpcTarget.Others, new object[1]);
                    FriendshipGroupDetection.Instance.photonView.RPC("PartyMemberIsAboutToGroupJoin", Photon.Pun.RpcTarget.Others, new object[1]);
                    FlushRPCs(0.4f);
                }
            }
        }
        public static void LagGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                bool lag = Time.time > r;
                if (lag)
                {
                    if (ControllerInputPoller.instance.rightGrab)
                    {
                        r = Time.time + 0.2f;
                        for (int i = 0; i < 20; i++)
                        {
                            FriendshipGroupDetection.Instance.photonView.RPC("RequestPartyGameMode", RigManager.GetPlayerFromVRRig(GunTemplate.LockedPlayer), new object[1]);
                            FriendshipGroupDetection.Instance.photonView.RPC("NotifyPartyGameModeChanged", RigManager.GetPlayerFromVRRig(GunTemplate.LockedPlayer), new object[1]);
                            FriendshipGroupDetection.Instance.photonView.RPC("VerifyPartyMember", RigManager.GetPlayerFromVRRig(GunTemplate.LockedPlayer), new object[1]);
                            FriendshipGroupDetection.Instance.photonView.RPC("PartyMemberIsAboutToGroupJoin", RigManager.GetPlayerFromVRRig(GunTemplate.LockedPlayer), new object[1]);
                            Safety.FlushRPCs(0.4f);
                        }
                    }

                }

            }, true);

        }
        static float r = 1f;
        public static void EnableBoard()
        {
            GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/HoverboardVisual").SetActive(true);
        }
        public static void HideBoard()
        {
            GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/HoverboardVisual").SetActive(false);
        }
    }
}
