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

namespace MenkerMenu.Mods.Categories
{
    public class Overpowered
    {
        public static void CrashAll()
        {
                if (Time.time > crashtimer)
                {
                    crashtimer = Time.time + 0.3f;
                    for (int i = 0; i < 100; i++)
                        BuilderTableNetworking.instance.PlayerEnterBuilder();
                    RPCFlush();
                }
        }
        public static void LagAll()
        {
            PhotonView view = GameObject.Find("Environment Objects/LocalObjects_Prefab/City_WorkingPrefab/Arcade_prefab/MainRoom/VRArea/ModIOArcadeTeleporter/NetObject_VRTeleporter").GetComponent<Photon.Pun.PhotonView>();
                if (Time.time > lagtimer)
                {
                    lagtimer = Time.time + 0.6f;
                    for (int i = 0; i < 250; i++)
                        view.RPC("ActivateTeleportVFX", RpcTarget.Others, new object[] { (short)UnityEngine.Random.Range(0, 7) });
                    RPCFlush();
                }
        }
        public static void LagGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                if (Time.time > lagtimer)
                {
                    lagtimer = Time.time + 0.6f;
                    PhotonView view = GameObject.Find("Environment Objects/LocalObjects_Prefab/City_WorkingPrefab/Arcade_prefab/MainRoom/VRArea/ModIOArcadeTeleporter/NetObject_VRTeleporter").GetComponent<Photon.Pun.PhotonView>();
                    for (int i = 0; i < 250; i++)
                        view.RPC("ActivateTeleportVFX", NetPlayerToPlayer(GetPlayerFromVRRig(LockedPlayer)), new object[] { (short)UnityEngine.Random.Range(0, 7) });
                    RPCFlush();
                }
            }, true);
        }

        public static float lagtimer = 0;
        public static float crashtimer = 0;
    }
}
