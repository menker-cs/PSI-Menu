using GorillaNetworking;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using static MenkerMenu.Utilities.NotificationLib;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Utilities.ColorLib;
using static MenkerMenu.Mods.Categories.Room;
using MenkerMenu.Utilities;
using System.Diagnostics;
using Valve.VR;
using Cinemachine;
using HarmonyLib;
using System.Collections;
using System.Reflection;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using BepInEx;
using MenkerMenu.Utilities.Rig;
using Photon.Realtime;
using GorillaLocomotion;

namespace MenkerMenu.Mods.Categories
{
    public class Safety
    {
        public static void AntiReport()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        Vector3 rHand = vrrig.rightHandTransform.position;
                        Vector3 lHand = vrrig.leftHandTransform.position;
                        rHand = vrrig.rightHandTransform.position + vrrig.rightHandTransform.forward * 0.125f;
                        lHand = vrrig.leftHandTransform.position + vrrig.leftHandTransform.forward * 0.125f;
                        float num = antireportSens;
                        foreach (GorillaPlayerScoreboardLine gorillaPlayerScoreboardLine in GorillaScoreboardTotalUpdater.allScoreboardLines)
                        {
                            if (gorillaPlayerScoreboardLine.linePlayer == NetworkSystem.Instance.LocalPlayer)
                            {
                                Vector3 reportButton = gorillaPlayerScoreboardLine.reportButton.gameObject.transform.position + new Vector3(0f, 0.001f, 0.0004f);
                                if (Vector3.Distance(reportButton, lHand) < num)
                                {
                                    NotificationLib.SendNotification("<color=blue>Anti-Report:</color> : " + vrrig.playerText1.text + " Attempted to Report You!");
                                    Disconnect();
                                }
                                if (Vector3.Distance(reportButton, rHand) < num)
                                {
                                    NotificationLib.SendNotification("<color=blue>Anti-Report</color> : " + vrrig.playerText1.text + " Attempted to <color=red>Report</color> You!");
                                    Disconnect();
                                }
                            }
                            
                        }
                    }
                }
            }
        }
        public static void VisualizeAntiReport(Vector3 position, float range)
        {
            GameObject report = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            UnityEngine.Object.Destroy(report, Time.deltaTime);
            UnityEngine.Object.Destroy(report.GetComponent<Collider>());
            UnityEngine.Object.Destroy(report.GetComponent<Rigidbody>());
            report.transform.position = position;
            report.transform.localScale = new Vector3(range, range, range);
            Color clr = RoyalBlue;
            clr.a = 0.25f;
            report.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
            report.GetComponent<Renderer>().material.color = clr;
        }

        private static float antireportSens = 0.4f;

    }
}
