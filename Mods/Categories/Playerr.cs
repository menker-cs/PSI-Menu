using System;
using System.Collections.Generic;
using System.Text;
using static MenkerMenu.Utilities.GunTemplate;
using static MenkerMenu.Utilities.ColorLib;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Utilities.HandOrbs;
using static MenkerMenu.Menu.Main;
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
    public class Playerr
    {
        // General toggle and state variables
        private static bool isOn = false;
        private static bool wasButtonPressed = false;

        public static bool isOn2 = false;
        private static bool wasButtonPressed2 = false;

        public static void GhostMonke()
        {
            bool isButtonCurrentlyPressed = pollerInstance.rightControllerPrimaryButton;
            if (!wasButtonPressed && isButtonCurrentlyPressed | UnityInput.Current.GetKey(KeyCode.P))
            {
                isOn = !isOn;
            }

            wasButtonPressed = isButtonCurrentlyPressed;

            if (isOn)
            {
                taggerInstance.offlineVRRig.enabled = false;
            }
            else
            {
                taggerInstance.offlineVRRig.enabled = true;
            }
        }

        public static void InvisibleMonke()
        {
            bool isButtonCurrentlyPressed = pollerInstance.rightControllerPrimaryButton;
            if (!wasButtonPressed && isButtonCurrentlyPressed | UnityInput.Current.GetKey(KeyCode.P))
            {
                isOn = !isOn;
            }

            wasButtonPressed = isButtonCurrentlyPressed;

            if (isOn)
            {
                taggerInstance.offlineVRRig.enabled = false;
                taggerInstance.offlineVRRig.transform.position = new Vector3(999f, 999f, 999f);
            }
            else
            {
                taggerInstance.offlineVRRig.enabled = true;
            }
        }
        public static void Spaz()
        {
            GorillaTagger.Instance.offlineVRRig.head.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360));
            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360));
            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360));

            GorillaTagger.Instance.offlineVRRig.head.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 180), (float)UnityEngine.Random.Range(0, 180));
            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 180), (float)UnityEngine.Random.Range(0, 180));
            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.eulerAngles = new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 180), (float)UnityEngine.Random.Range(0, 180));
        }
        public static void RigGun1()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GunTemplate.spherepointer.transform.position + new Vector3(0f, 1f, 0f);
                HandOrbs1();
            }, false);
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void FixHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 0f;
        }
        public static void BackwardsHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 180f;
        }
        public static void UpsidedownHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 180f;
        }
        public static void LongArms()
        {
            GorillaTagger.Instance.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        public static void VeryLongArms()
        {
            GorillaTagger.Instance.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        public static void FixArms()
        {
            GorillaTagger.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        public static void GrabRig()
        {
            if (ControllerInputPoller.instance.rightGrab | UnityInput.Current.GetKey(KeyCode.G))
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;

                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                HandOrbs1();
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void FreezeRig()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f | UnityInput.Current.GetKey(KeyCode.T))
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;

                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.headCollider.transform.position;
                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.headCollider.transform.position;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void HeadSpin()
        {
            VRMap head = RigManager.GetOwnVRRig().head;
            head.trackingRotationOffset.x += 15f;
        }
        public static void HeadSpiny()
        {
            VRMap head = RigManager.GetOwnVRRig().head;
            head.trackingRotationOffset.y += 15f;
        }
        public static void AnnoyPlayerGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = LockedPlayer.transform.position + Random(1.25f);
                HandOrbs1();
            }, true);
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void FakeLag()
        {
            if (ControllerInputPoller.instance.rightGrab | UnityInput.Current.GetKey(KeyCode.G))
            {
                DelayedAction(0.75f, delegate
                {
                    fake++;
                    if (fake > 2)
                    {
                        fake = 1;
                    }
                    if (fake == 1)
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                    }
                    if (fake == 2)
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                    }
                });
            }
            else
            {
                if (!GorillaTagger.Instance.offlineVRRig.enabled)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }
        public static void LookAtClosest()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.2f | UnityInput.Current.GetKey("T"))
            {
                GorillaTagger.Instance.offlineVRRig.headConstraint.LookAt(Close.headMesh.transform.position);
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.LookAt(Close.headMesh.transform.position);
            }
        }
        public static void LookAtGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GorillaTagger.Instance.offlineVRRig.headConstraint.LookAt(LockedPlayer.headMesh.transform.position);
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.LookAt(LockedPlayer.headMesh.transform.position);
            }, true);
        }


        public static VRRig Close = RigManager.GetClosestVRRig();

        public static Vector3 Random(float dis)
        {
            Vector3[] ran = new Vector3[]
            {
                new Vector3(dis, 0f, 0f),
                new Vector3(dis, dis, 0f),
                new Vector3(dis, dis, dis),
                new Vector3(dis, 0f, dis),
                new Vector3(0f, 0f, dis),
                new Vector3(0f, dis, dis),
                new Vector3(0f, dis, 0f),
                new Vector3(-dis, 0f, 0f),
                new Vector3(-dis, -dis, 0f),
                new Vector3(-dis, -dis, -dis),
                new Vector3(-dis, 0f, -dis),
                new Vector3(0f, 0f, -dis),
                new Vector3(0f, -dis, -dis),
                new Vector3(0f, -dis, 0f),
            };
            return ran[UnityEngine.Random.Range(0, ran.Length)];
        }
        public static float delay;
        public static void DelayedAction(float time, Action action)
        {
            if (Time.time > delay)
            {
                action.Invoke();
                delay = Time.time + time;
            }
        }
        public static int fake = 1;
    }
}
