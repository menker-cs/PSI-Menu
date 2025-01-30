using System;
using System.Collections.Generic;
using System.Text;
using static MenkerMenu.Utilities.GunTemplate;
using static MenkerMenu.Menu.Main;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Utilities.ColorLib;
using static MenkerMenu.Utilities.RigManager;
using static MenkerMenu.Menu.ButtonHandler;
using static MenkerMenu.Mods.ModButtons;
using static MenkerMenu.Mods.Categories.Settings;
using UnityEngine;
using Valve.VR;
using System.Reflection;
using BepInEx;
using Photon.Voice;
using MenkerMenu.Utilities;
using MenkerMenu.Utilities;

namespace MenkerMenu.Mods.Categories
{
    public class Move
    {
        public static void Fly()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton | UnityInput.Current.GetKey(KeyCode.P))
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * flyspeedchangerspeed;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        public static void TriggerFly()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.2f | UnityInput.Current.GetKey(KeyCode.T))
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * flyspeedchangerspeed;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        public static void NoclipFly()
        {
            foreach (MeshCollider collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                if (ControllerInputPoller.instance.rightControllerPrimaryButton | UnityInput.Current.GetKey(KeyCode.P))
                {
                    collider.enabled = false;
                    GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * flyspeedchangerspeed;
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                else
                {
                    collider.enabled = true;
                }
            }
        }
        public static void carmonkey()
        {
            Vector3 forward = GorillaLocomotion.Player.Instance.headCollider.transform.forward;
            forward.y = 0;
            forward.Normalize();

            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.2f | UnityInput.Current.GetKey(KeyCode.T))
            {
                GorillaLocomotion.Player.Instance.transform.position += forward * Time.deltaTime * 25;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            if (ControllerInputPoller.instance.leftControllerIndexFloat > 0.2f | UnityInput.Current.GetKey(KeyCode.Y))
            {
                GorillaLocomotion.Player.Instance.transform.position -= forward * Time.deltaTime * 25;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        public static void LowGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
        }

        public static void ZeroGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (9.81f / Time.deltaTime)), ForceMode.Acceleration);
        }

        public static void HighGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.down * (Time.deltaTime * (7.77f / Time.deltaTime)), ForceMode.Acceleration);
        }

        public static void ReverseGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (19.62f / Time.deltaTime)), ForceMode.Acceleration);
            GorillaLocomotion.Player.Instance.rightControllerTransform.parent.rotation = Quaternion.Euler(180f, 0f, 0f);
        }

        public static void GravityFixRig()
        {
            GorillaLocomotion.Player.Instance.rightControllerTransform.parent.rotation = Quaternion.identity;
        }
        public static void Noclip()
        {
            foreach (MeshCollider collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.2f | UnityInput.Current.GetKey(KeyCode.T))
                {
                    collider.enabled = false;
                }
                else
                {
                    collider.enabled = true;
                }
            }
        }
        public static void Speedboost()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = speedboostchangerspeed;
            GorillaLocomotion.Player.Instance.jumpMultiplier = speedboostchangerspeed;
        }
        public static void Platform(ref GameObject platform, bool grabbing, Transform position, bool invis)
        {
            if (grabbing)
            {
                if (platform == null)
                {
                    platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    platform.transform.localScale = new Vector3(0.28f, 0.015f, 0.28f);
                    platform.transform.position = position.position + new Vector3(0f, -0.02f, 0f);
                    platform.transform.rotation = position.rotation * Quaternion.Euler(0f, 0f, -90f);
                    platform.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    platform.GetComponent<Renderer>().material.color = RoyalBlueTransparent;
                    platform.AddComponent<GorillaSurfaceOverride>().overrideIndex = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/sky jungle entrance 2/ElevatorClouds/Cloud_Platform_001 Variant").GetComponent<GorillaSurfaceOverride>().overrideIndex;

                    if (invis == true)
                    {
                        platform.GetComponent<Renderer>().enabled = false;
                    }
                }
            }
            else if (platform != null)
            {
                GameObject.Destroy(platform);
                platform = null;
            }
        }
        public static void Platforms()
        {
            Platform(ref leftPlatform, ControllerInputPoller.instance.leftGrab | UnityInput.Current.GetKey(KeyCode.H), GorillaLocomotion.Player.Instance.leftControllerTransform, false);
            Platform(ref rightPlatform, ControllerInputPoller.instance.rightGrab | UnityInput.Current.GetKey(KeyCode.G), GorillaLocomotion.Player.Instance.rightControllerTransform, false);
        }
        public static void InvisPlatforms()
        {
            Platform(ref leftPlatform, ControllerInputPoller.instance.leftGrab | UnityInput.Current.GetKey(KeyCode.H), GorillaLocomotion.Player.Instance.leftControllerTransform, true);
            Platform(ref rightPlatform, ControllerInputPoller.instance.rightGrab | UnityInput.Current.GetKey(KeyCode.G), GorillaLocomotion.Player.Instance.rightControllerTransform, true);
        }
        public static void TPGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GorillaLocomotion.Player.Instance.transform.position = GunTemplate.spherepointer.transform.position;
                GorillaTagger.Instance.transform.position = GunTemplate.spherepointer.transform.position;
            }, false);
        }
        public static void NoTagFreeze()
        {
            GorillaLocomotion.Player.Instance.disableMovement = false;
        }
        public static void TagFreeze()
        {
            GorillaLocomotion.Player.Instance.disableMovement = true;
        }
        public static void CarMonkey()
        {
            Vector3 forward = GorillaLocomotion.Player.Instance.headCollider.transform.forward;
            forward.y = 0;
            forward.Normalize();

            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.2f | UnityInput.Current.GetKey(KeyCode.T))
            {
                GorillaLocomotion.Player.Instance.transform.position += forward * Time.deltaTime * 25;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            if (ControllerInputPoller.instance.leftControllerIndexFloat > 0.2f | UnityInput.Current.GetKey(KeyCode.Y))
            {
                GorillaLocomotion.Player.Instance.transform.position -= forward * Time.deltaTime * 25;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        public static void UpAndDown()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.2f | UnityInput.Current.GetKey(KeyCode.T))
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.up * Time.deltaTime * 15f;
            }
            if (ControllerInputPoller.instance.leftControllerIndexFloat > 0.2f | UnityInput.Current.GetKey(KeyCode.Y))
            {
                GorillaLocomotion.Player.Instance.transform.position -= GorillaLocomotion.Player.Instance.headCollider.transform.up * Time.deltaTime * 15f;
            }
        }
          public static void WASDFly()
          {
              float currentSpeed = 5;
              Transform bodyTransform = Camera.main.transform;
              GorillaTagger.Instance.rigidbody.useGravity = false;
              GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
              if (UnityInput.Current.GetKey(KeyCode.LeftShift))
              {
                  currentSpeed *= 2.5f;
              }
              if (UnityInput.Current.GetKey(KeyCode.W) || UnityInput.Current.GetKey(KeyCode.UpArrow))
              {
                  bodyTransform.position += bodyTransform.forward * currentSpeed * Time.deltaTime;
              }
              if (UnityInput.Current.GetKey(KeyCode.A) || UnityInput.Current.GetKey(KeyCode.LeftArrow))
              {
                  bodyTransform.position += -bodyTransform.right * currentSpeed * Time.deltaTime;
              }
              if (UnityInput.Current.GetKey(KeyCode.S) || UnityInput.Current.GetKey(KeyCode.DownArrow))
              {
                  bodyTransform.position += -bodyTransform.forward * currentSpeed * Time.deltaTime;
              }
              if (UnityInput.Current.GetKey(KeyCode.D) || UnityInput.Current.GetKey(KeyCode.RightArrow))
              {
                  bodyTransform.position += bodyTransform.right * currentSpeed * Time.deltaTime;
              }
              if (UnityInput.Current.GetKey(KeyCode.Space))
              {
                  bodyTransform.position += bodyTransform.up * currentSpeed * Time.deltaTime;
              }
              if (UnityInput.Current.GetKey(KeyCode.LeftControl))
              {
                  bodyTransform.position += -bodyTransform.up * currentSpeed * Time.deltaTime;
              }
              if (UnityInput.Current.GetMouseButton(1))
              {
                  Vector3 pos = UnityInput.Current.mousePosition - oldMousePos;
                  float x = bodyTransform.localEulerAngles.x - pos.y * 0.3f;
                  float y = bodyTransform.localEulerAngles.y + pos.x * 0.3f;
                  bodyTransform.localEulerAngles = new Vector3(x, y, 0f);
              }
              oldMousePos = UnityInput.Current.mousePosition;
          }
          public static void TPPlayerGun()
          {
              GunTemplate.StartBothGuns(() =>
              {
                  GorillaLocomotion.Player.Instance.transform.position = LockedPlayer.transform.position;
                  GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
              }, true);
          }
          public static void HoverGun()
          {
              GunTemplate.StartBothGuns(() =>
              {
                  GorillaLocomotion.Player.Instance.transform.position = LockedPlayer.transform.position + new Vector3(0f, 2.5f, 0f);
              }, true);
          }
         public static void TPToRandom()
         {
             GorillaLocomotion.Player.Instance.transform.position = RigManager.GetRandomVRRig(false).transform.position;
             GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
         }
         
          private static Vector3 oldMousePos;

          public static bool rp = false;
          public static bool lp = false;

          public static GameObject rightPlat;
          public static GameObject leftPlat;

          public static GameObject leftPlatform;
          public static GameObject rightPlatform;
    }
}
