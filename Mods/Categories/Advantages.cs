using System;
using System.Collections.Generic;
using System.Text;
using static MenkerMenu.Utilities.Variables;
using UnityEngine;
using UnityEngine.InputSystem;
using MenkerMenu.Utilities;
using MenkerMenu.Utilities.Patches;
using UnityEngine.XR;
using UnityEngine.Animations.Rigging;
using System.Reflection;
using MenkerMenu.Utilities;
using static MenkerMenu.Utilities.HandOrbs;
using static MenkerMenu.Utilities.GunTemplate;
using Oculus.Interaction.Input;
using BepInEx;
using UnityEngine.ProBuilder.MeshOperations;

namespace MenkerMenu.Mods.Categories
{
    public class Advantage
    {
        /*
        None,
        Frozen,
        Slowed,
        Dead,
        Infected,
        It
        */
        public static void TagGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                if (IAmInfected)
                {
                    if (!RigIsInfected(LockedPlayer))
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.transform.position = GunTemplate.spherepointer.transform.position - new Vector3(0f, 5f, 0f);
                        GorillaTagger.Instance.leftHandTransform.position = spherepointer.transform.position;
                    }
                    else
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                    }
                }
            }, true);
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void TagAll()
        {
            if (IAmInfected)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.2f | UnityInput.Current.GetKey(KeyCode.T))
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (!RigIsInfected(vrrig))
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.transform.position - new Vector3(0f, 5f, 0f);
                            GorillaTagger.Instance.leftHandTransform.position = vrrig.transform.position;
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                            break;
                        }
                    }
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void TagSelf()
        {
            if (!IAmInfected)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.2f | UnityInput.Current.GetKey(KeyCode.T))
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (RigIsInfected(vrrig))
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.rightHandTransform.position;
                            GorillaTagger.Instance.myVRRig.transform.position = vrrig.rightHandTransform.position;
                            break;
                        }
                    }
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
                NotificationLib.SendNotification("<color=white>[</color><color=blue>Tag Self:</color><color=white>] You are already tagged</color>");
            }
        }

        public static void FlickTagGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GorillaTagger.Instance.leftHandTransform.position = spherepointer.transform.position;
            }, true);
            {
                GorillaTagger.Instance.leftHandTransform = GorillaTagger.Instance.leftHandTransform;
            }
        }
        public static void TagAura()
        {
            if (IAmInfected && ControllerInputPoller.instance.rightGrab | UnityInput.Current.GetKey(KeyCode.G))
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (!RigIsInfected(vrrig))
                    {
                        if (Vector3.Distance(taggerInstance.offlineVRRig.transform.position, vrrig.transform.position) < 5)
                        {
                            GorillaTagger.Instance.rightHandTransform.position = vrrig.transform.position;
                        }
                    }
                }
            }
        }
    }
}
