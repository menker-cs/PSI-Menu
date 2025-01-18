using g3;
using MenkerMenu.Utilities;
using MenkerMenu.Utilities.Rigshit;
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
                            RigShit.GetNetworkViewFromVRRig(l).SendRPC("GrabbedByPlayer", RpcTarget.Others, new object[] { true, false, false });
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
        public static void VoidGun()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                GunTemplate.StartBothGuns(delegate
                {
                    if (isLocked && GunTemplate.LockedPlayer != null)
                    {
                        foreach (VRRig rig in GorillaParent.instance.vrrigs)
                        {
                            if (Time.time > bounce)
                            {
                                bounce2 += Time.deltaTime;
                                bounce = Time.time + 0.1f;
                                GorillaGuardianManager guardianmanager = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
                                if (guardianmanager.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
                                {
                                        RigShit.GetNetworkViewFromVRRig(rig).SendRPC("GrabbedByPlayer", RigShit.GetPlayerFromVRRig(rig), new object[] { true, false, false });
                                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                                        GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(-55.12f, 150.94f, 6.51f);
                                        if (GorillaTagger.Instance.offlineVRRig.transform.position == new Vector3(-55.12f, 150.94f, 6.51f) && rig.transform.position == GorillaTagger.Instance.offlineVRRig.rightHandTransform.position || rig.transform.position == GorillaTagger.Instance.offlineVRRig.leftHandTransform.position)
                                        {

                                            RigShit.GetNetworkViewFromVRRig(rig).SendRPC("DroppedByPlayer", RigShit.GetPlayerFromVRRig(rig), new object[] { true, false, false });
                                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                                        }
                                }
                            }
                        }
                    }
                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f || Mouse.current.leftButton.isPressed)
                    {
                        VRRig telling = GunTemplate.raycastHit.collider.GetComponentInParent<VRRig>();
                        if (telling && telling != GorillaTagger.Instance.offlineVRRig)
                        {
                            isLocked = true;
                            GunTemplate.LockedPlayer = telling;
                        }
                    }
                    else
                    {
                        RigShit.GetNetworkViewFromVRRig(GunTemplate.LockedPlayer).SendRPC("DroppedByPlayer", RigShit.GetPlayerFromVRRig(GunTemplate.LockedPlayer), new object[] { true, false, false });
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                    }
                }, true);
            }
            else
            {
                RigShit.GetNetworkViewFromVRRig(GunTemplate.LockedPlayer).SendRPC("DroppedByPlayer", RigShit.GetPlayerFromVRRig(GunTemplate.LockedPlayer), new object[] { true, false, false });
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static float bounce = 0f;
        public static float bounce2 = 5f;
        public static TappableGuardianIdol[] GetGuradianRocks()
        {
            if (Time.time > Tineer)
            {
                atg = null;
                Tineer = Time.time + 5f;
            }
            if (atg == null)
            {
                atg = UnityEngine.Object.FindObjectsOfType<TappableGuardianIdol>();
            }
            return atg;
        }
        private static float Tineer = -1f;
        public static TappableGuardianIdol[] atg = null;
        public static void RPCFlush()
        {
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
            GorillaNot.instance.rpcCallLimit = int.MaxValue;
            PhotonNetwork.RemoveBufferedRPCs(GorillaTagger.Instance.myVRRig.ViewID, null, null);
            PhotonNetwork.OpCleanActorRpcBuffer(PhotonNetwork.LocalPlayer.ActorNumber);
            PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig.GetView);
        }
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
    }
}
