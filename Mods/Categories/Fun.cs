using System;
using System.Collections.Generic;
using System.Text;
using GorillaLocomotion;
using static MenkerMenu.Menu.Main;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Utilities.ColorLib;
using static MenkerMenu.Menu.ButtonHandler;
using static MenkerMenu.Mods.ModButtons;
using static MenkerMenu.Mods.Categories.Settings;
using static MenkerMenu.Utilities.GunTemplate;
using UnityEngine;
using BepInEx;
using MenkerMenu.Utilities;
using UnityEngine.InputSystem;
using GorillaNetworking;
using static MenkerMenu.Utilities.HandOrbs;
using Photon.Pun;
using Oculus.Interaction.PoseDetection;
using Fusion;

namespace MenkerMenu.Mods.Categories
{
    public class Fun
    {
        #region bug bat ball
        public static void CopySelfID()
        {
            string id = PhotonNetwork.LocalPlayer.UserId;
            GUIUtility.systemCopyBuffer = id;
        }
        public static void GrabRocket()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GameObject rocket = GameObject.Find("RocketShip_Prefab");
                rocket.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                rocket.transform.position = new Vector3(0f, -0.0075f, 0f);
                rocket.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                rocket.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
            }
        }
        public static void GiveRocketAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                GameObject rocket = GameObject.Find("RocketShip_Prefab");
                rocket.transform.position = rig.transform.position;
            }
        }
        public static void RocketClosest()
        {
            GameObject rocket = GameObject.Find("RocketShip_Prefab");
            rocket.transform.position = RigManager.GetClosestVRRig().transform.position;
        }
        public static void RocketGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GameObject rocket = GameObject.Find("RocketShip_Prefab");
                rocket.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                rocket.transform.position = GunTemplate.spherepointer.transform.position;
            }, false);
        }
        public static void RocketAura()
        {
            GameObject rocket = GameObject.Find("RocketShip_Prefab");
            rocket.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            rocket.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));
            rocket.transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
        }
        public static void RocketHalo()
        {
            GameObject rocket = GameObject.Find("RocketShip_Prefab");
            rocket.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            rocket.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(MathF.Cos((float)Time.frameCount / 30), 1f, MathF.Sin((float)Time.frameCount / 30));
        }
        public static void CopyIDGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                string id = LockedPlayer.Creator.UserId;
                GUIUtility.systemCopyBuffer = id;
            }, true);
        }
        public static void GrabBug()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Bug.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                Bug.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            }
        }
        public static void BugGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                Bug.transform.position = GunTemplate.spherepointer.transform.position;
            }, false);
        }
        public static void GrabBat()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Bat.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                Bat.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            }
        }
        public static void BatGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                Bat.transform.position = GunTemplate.spherepointer.transform.position;
            }, false);
        }
        public static void SnipeBug()
        { 
            GorillaTagger.Instance.rightHandTransform.transform.position = Bug.transform.position;
        }
        public static void SnipeBat()
        {
            GorillaTagger.Instance.rightHandTransform.transform.position = Bat.transform.position;
        }
        public static void GrabSBall()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                SBall.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                SBall.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            }
        }
        public static void SBallGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                SBall.transform.position = GunTemplate.spherepointer.transform.position;
            }, false);
        }
        public static void SnipeSBall()
        {
            GorillaTagger.Instance.rightHandTransform.transform.position = SBall.transform.position;
        }
        public static void SBallHalo()
        {
            SBall.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(MathF.Cos((float)Time.frameCount / 30), 1f, MathF.Sin((float)Time.frameCount / 30));
            SBall.transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
        }
        public static void BugHalo()
        {
            Bug.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(MathF.Cos((float)Time.frameCount / 30), 1f, MathF.Sin((float)Time.frameCount / 30));
            Bug.transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
        }
        public static void BatHalo()
        {
            Bat.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(MathF.Cos((float)Time.frameCount / 30), 1f, MathF.Sin((float)Time.frameCount / 30));
            Bat.transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
        }

        public static GameObject Bat = GameObject.Find("Cave Bat Holdable");
        public static GameObject Bug = GameObject.Find("Floating Bug Holdable");
        public static GameObject SBall = GameObject.Find("GameBall");
#endregion

        #region fun spammers cs
        public static void Spam1()
        {
            if (pollerInstance.rightGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = GorillaLocomotion.Player.Instance.rightControllerTransform.forward * 10f;

                GameObject.Destroy(orb, 5f);
            }
            if (pollerInstance.leftGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = GorillaLocomotion.Player.Instance.leftControllerTransform.forward * 10f;

                GameObject.Destroy(orb, 5f);
            }
        }
        public static void Spam2()
        {
            if (pollerInstance.rightGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;
                Trail(orb, MenuColorT);

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = GorillaLocomotion.Player.Instance.rightControllerTransform.forward * 10f;

                GameObject.Destroy(orb, 5f);
            }
            if (pollerInstance.leftGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;
                Trail(orb, MenuColorT);

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = GorillaLocomotion.Player.Instance.leftControllerTransform.forward * 10f;

                GameObject.Destroy(orb, 5f);
            }
        }
        public static void Spam3()
        {
            if (pollerInstance.rightGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;
                Trail(orb, MenuColorT);

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.useGravity = false;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = GorillaLocomotion.Player.Instance.rightControllerTransform.forward * 10f;

                GameObject.Destroy(orb, 5f);
            }
            if (pollerInstance.leftGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;
                Trail(orb, MenuColorT);

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.useGravity = false;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = GorillaLocomotion.Player.Instance.leftControllerTransform.forward * 10f;

                GameObject.Destroy(orb, 5f);
            }
        }
        public static void Spam4()
        {
            if (pollerInstance.rightGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = GorillaLocomotion.Player.Instance.rightControllerTransform.forward * 10f;

            }
            if (pollerInstance.leftGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = GorillaLocomotion.Player.Instance.leftControllerTransform.forward * 10f;
            }
        }
        public static void Draw()
        {
            if (pollerInstance.rightGrab)
            {
                draw = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                draw.transform.position = taggerInstance.rightHandTransform.position;
                UnityEngine.Object.Destroy(draw.GetComponent<SphereCollider>());
                draw.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                draw.GetComponent<Renderer>().material.color = MenuColor;
                GameObject.Destroy(draw, 5f);
            }
            if (pollerInstance.leftGrab)
            {
                draw = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                draw.transform.position = taggerInstance.leftHandTransform.position;
                UnityEngine.Object.Destroy(draw.GetComponent<BoxCollider>());
                draw.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                draw.GetComponent<Renderer>().material.color = MenuColor;
                GameObject.Destroy(draw, 5f);
            }
        }
        public static void GravDraw()
        {
            if (pollerInstance.rightGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;

                GameObject.Destroy(orb, 5f);
            }
            if (pollerInstance.leftGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;

                GameObject.Destroy(orb, 5f);
            }
        }
        public static void GravDraw1()
        {
            if (pollerInstance.rightGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;

            }
            if (pollerInstance.leftGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;

            }
        }
        public static void BigSpam()
        {
            if (pollerInstance.rightGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                orb.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;
                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = body.velocity;

                GameObject.Destroy(orb, 5f);
            }
            if (pollerInstance.leftGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                orb.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = body.velocity;

                GameObject.Destroy(orb, 5f);
            }
        }
        public static void OrbGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = spherepointer.transform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = body.velocity;

                GameObject.Destroy(orb, 5f);
            }, false);
        }
        public static void OrbGun1()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                orb.transform.position = spherepointer.transform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = body.velocity;

                GameObject.Destroy(orb, 5f);
            }, false);
        }
        public static void SpazOrb()
        {
            if (pollerInstance.rightGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.rotation = new Quaternion(UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360));
                body.velocity = new Vector3(UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(-3, 3)) * 25f;

                GameObject.Destroy(orb, 5f);
            }
            if (pollerInstance.leftGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.rotation = new Quaternion(UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360));
                body.velocity = new Vector3(UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(-3, 3)) * 25f;

                GameObject.Destroy(orb, 5f);

                
            }
        }
        public static void OrbRain()
        {
            if (pollerInstance.rightGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position + new Vector3(UnityEngine.Random.Range(-20, 20), 10, UnityEngine.Random.Range(-20, 20));
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;
                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = body.velocity;
                GameObject.Destroy(orb, 5f);
            }
            if (pollerInstance.leftGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                orb.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position + new Vector3(UnityEngine.Random.Range(-20, 20), 10, UnityEngine.Random.Range(-20, 20));
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = body.velocity;
                GameObject.Destroy(orb, 5f);
            }
        }
        public static void OrbRain1()
        {
            if (pollerInstance.rightGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                orb.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position + new Vector3(UnityEngine.Random.Range(-20, 20), 10, UnityEngine.Random.Range(-20, 20));
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;
                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = body.velocity;
                Trail1(orb, MenuColorT);
                GameObject.Destroy(orb, 5f);
            }
            if (pollerInstance.leftGrab)
            {
                GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                orb.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                orb.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position + new Vector3(UnityEngine.Random.Range(-20, 20), 10, UnityEngine.Random.Range(-20, 20));
                orb.GetComponent<Renderer>().material.color = MenuColorT;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/pit ground").layer = orb.layer;

                Rigidbody body = orb.AddComponent<Rigidbody>();
                body.mass = 0.5f;
                body.drag = 0f;
                body.useGravity = true;
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
                body.velocity = body.velocity;
                Trail1(orb, MenuColorT);
                GameObject.Destroy(orb, 5f);
            }
        }

        static GameObject draw;
        #endregion
    }
}
