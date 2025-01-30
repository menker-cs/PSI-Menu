using MenkerMenu.Utilities;
using static MenkerMenu.Mods.Categories.Safety;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MenkerMenu.Mods.Categories
{
    internal class Guardian
    {
        public static void VoidAll()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f || Mouse.current.rightButton.isPressed)
            {
                if (Time.time > bounce)
                {
                    bounce2 += Time.deltaTime;
                    bounce = Time.time + 0.1f;
                    GorillaGuardianManager guardianmanager = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
                    if (guardianmanager.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
                    {
                        foreach (VRRig l in GorillaParent.instance.vrrigs)
                        {
                            RigManager.GetNetworkViewFromVRRig(l).SendRPC("GrabbedByPlayer", RpcTarget.Others, new object[] { true, false, false });
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(-55.12f, 150.94f, 6.51f);
                        }
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static bool isLocked = false;

        public static float bounce = 0f;
        public static float bounce2 = 5f;
        public static TappableGuardianIdol[] GetGuradianRocks()
        {
            if (Time.time > time)
            {
                g = null;
                time = Time.time + 5f;
            }
            if (g == null)
            {
                g = UnityEngine.Object.FindObjectsOfType<TappableGuardianIdol>();
            }
            return g;
        }
        private static float time = -1f;
        public static TappableGuardianIdol[] g = null;
        public static void AlwaysGuardian()
        {
            {
                foreach (TappableGuardianIdol Gta5 in GetGuradianRocks())
                {
                    if (!Gta5.isChangingPositions)
                    {
                        GorillaGuardianManager gman = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
                        if (!gman.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer)) // gzm.enabled && 
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.offlineVRRig.transform.position = Gta5.transform.position;

                            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = Gta5.transform.position;
                            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = Gta5.transform.position;

                            Gta5.manager.photonView.RPC("SendOnTapRPC", RpcTarget.All, Gta5.tappableId, UnityEngine.Random.Range(0.2f, 0.4f));
                            RPCFlush();
                        }
                    }
                    else
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                    }
                }
            }
        }
        public static void GrabAll()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f || Mouse.current.rightButton.isPressed)
            {
                if (Time.time > bounce)
                {
                    bounce2 += Time.deltaTime;
                    bounce = Time.time + 0.1f;
                    GorillaGuardianManager guardianmanager = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
                    if (guardianmanager.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
                    {
                        foreach (VRRig l in GorillaParent.instance.vrrigs)
                        {
                            RigManager.GetNetworkViewFromVRRig(l).SendRPC("GrabbedByPlayer", RpcTarget.Others, new object[] { true, false, false });
                        }
                    }
                }
            }
        }
    }
}
