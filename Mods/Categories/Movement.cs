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
        public static void Platforms()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (!RPA)
                {
                    RP = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RP.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    RP.GetComponent<Renderer>().material.color = new Color32(0, 8, 252, 80);
                    RP.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                    RP.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    RP.transform.position = GorillaTagger.Instance.rightHandTransform.position - Vector3.up * 0.045f;
                    RPA = true;
                }
            }
            else
            {
                GameObject.Destroy(RP);
                RPA = false;
            }

            if (ControllerInputPoller.instance.leftGrab)
            {
                if (!LPA)
                {
                    LP = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LP.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    LP.GetComponent<Renderer>().material.color = new Color32(0, 8, 252, 80);
                    LP.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                    LP.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    LP.transform.position = GorillaTagger.Instance.leftHandTransform.position - Vector3.up * 0.045f; ;
                    LPA = true;
                }
            }
            else
            {
                GameObject.Destroy(LP);
                LPA = false;
            }
        }
        public static void StickyPlatforms()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (!RPA2)
                {
                    RP2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RP2.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    RP2.GetComponent<Renderer>().material.color = new Color32(0, 8, 252, 80);
                    RP2.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                    RP2.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    RP2.transform.position = GorillaTagger.Instance.rightHandTransform.position - Vector3.up * 0.045f;

                    RP22 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RP22.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    RP22.GetComponent<Renderer>().material.color = new Color32(0, 8, 252, 80);
                    RP22.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                    RP22.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    RP22.transform.position = GorillaTagger.Instance.rightHandTransform.position + Vector3.up * 0.045f;
                    RPA2 = true;
                }
            }
            else
            {
                GameObject.Destroy(RP2);
                GameObject.Destroy(RP22);
                RPA2 = false;
            }

            if (ControllerInputPoller.instance.leftGrab)
            {
                if (!LPA2)
                {
                    LP2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LP2.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    LP2.GetComponent<Renderer>().material.color = new Color32(0, 8, 252, 80);
                    LP2.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                    LP2.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    LP2.transform.position = GorillaTagger.Instance.leftHandTransform.position - Vector3.up * 0.045f;

                    LP22 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LP22.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    LP22.GetComponent<Renderer>().material.color = new Color32(0, 8, 252, 80);
                    LP22.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                    LP22.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    LP22.transform.position = GorillaTagger.Instance.leftHandTransform.position + Vector3.up * 0.045f;
                    LPA2 = true;
                }
            }
            else
            {
                GameObject.Destroy(LP2);
                GameObject.Destroy(LP22);
                LPA2 = false;
            }
        }
        public static bool RPA = false;
        public static bool LPA = false;
        public static GameObject RP;
        public static GameObject LP;

        public static bool RPA2 = false;
        public static bool LPA2 = false;
        public static GameObject RP2;
        public static GameObject LP2;
        public static GameObject RP22;
        public static GameObject LP22;
        public static GameObject Pointy;
        public static bool Ir = false;
        public static void Checkpoint()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (!Ir)
                {
                    Pointy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Pointy.GetComponent<Renderer>().material.color = MenuColor;
                    Pointy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    Pointy.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    Pointy.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                    Ir = true;
                }
            }
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GorillaTagger.Instance.transform.position = Pointy.transform.position;
                GorillaLocomotion.Player.Instance.transform.position = Pointy.transform.position;
            }
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
            {
                Ir = false;
                GameObject.Destroy(Pointy);
            }
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
