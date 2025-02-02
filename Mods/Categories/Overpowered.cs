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

namespace MenkerMenu.Mods.Categories
{
    public class Overpowered
    {
        public static bool RUNRPC(PhotonView photonView, string method, Player player, object[] parameters)
        {
            if (photonView != null && parameters != null && !string.IsNullOrEmpty(method))
            {
                var rpcHash = new ExitGames.Client.Photon.Hashtable
                {
                    { 0, photonView.ViewID },
                    { 2, (int)(PhotonNetwork.ServerTimestamp + -int.MaxValue) },
                    { 3, method },
                    { 4, parameters }
                };

                if (photonView.Prefix > 0)
                {
                    rpcHash[1] = (short)photonView.Prefix;
                }
                if (PhotonNetwork.PhotonServerSettings.RpcList.Contains(method))
                {
                    rpcHash[5] = (byte)PhotonNetwork.PhotonServerSettings.RpcList.IndexOf(method);
                }
                if (PhotonNetwork.NetworkingClient.LocalPlayer.ActorNumber == player.ActorNumber)
                {
                    typeof(PhotonNetwork).GetMethod("ExecuteRpc", BindingFlags.Static | BindingFlags.NonPublic).Invoke(typeof(PhotonNetwork), new object[]
                    {
                        (ExitGames.Client.Photon.Hashtable)rpcHash, (Player)PhotonNetwork.LocalPlayer
                    });
                }
                else
                {
                    PhotonNetwork.NetworkingClient.LoadBalancingPeer.OpRaiseEvent(200, rpcHash, new RaiseEventOptions
                    {
                        TargetActors = new int[]
                        {
                            player.ActorNumber,
                        }
                    }, new SendOptions
                    {
                        Reliability = true,
                        DeliveryMode = DeliveryMode.ReliableUnsequenced,
                        Encrypt = false
                    });
                }
            }
            return false;
        }
    }
}
