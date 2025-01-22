using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Utilities.ColorLib;
using static MenkerMenu.Mods.Categories.Settings;
using Photon.Pun;
using Object = UnityEngine.Object;
using System.Linq;
using Photon.Realtime;
using UnityEngine.InputSystem.Controls;
using System.Drawing;


namespace MenkerMenu.Mods.Categories
{
    public class Visuals 
    {
        public static void VisualsPlaceholder()
        {
            
        }
        [Obsolete]
        public static void FPSboost()
        {
            fps = true;
            if (fps)
            {
                QualitySettings.masterTextureLimit = 999999999;
                QualitySettings.masterTextureLimit = 999999999;
                QualitySettings.globalTextureMipmapLimit = 999999999;
                QualitySettings.maxQueuedFrames = 60;
            }
        }

        [Obsolete]
        public static void fixFPS()
        {
            if (fps)
            {
                QualitySettings.masterTextureLimit = default;
                QualitySettings.masterTextureLimit = default;
                QualitySettings.globalTextureMipmapLimit = default;
                QualitySettings.maxQueuedFrames = default;
                fps = false;
            }
        }
        public static void CSGO()
        {
            // 1 = casual
            // 2 = infection
            // 3 = rainbow
            // 4 = menu color
            if (espColor == 1)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    bool flag = vrrig == GorillaTagger.Instance.offlineVRRig;
                    bool flag2 = !flag;
                    bool flag3 = flag2;
                    if (flag3)
                    {
                        UnityEngine.Color color;
                        if (RigIsInfected(vrrig))
                        {
                            color = UnityEngine.Color.red;
                        }
                        else
                        {
                            color = UnityEngine.Color.green;
                        }
                        bool flag7 = vrrig.gameObject.GetComponent<LineRenderer>() == null;
                        bool flag8 = flag7;
                        bool flag9 = flag8;
                        if (flag9)
                        {
                            vrrig.gameObject.AddComponent<LineRenderer>();
                        }
                        LineRenderer component = vrrig.gameObject.GetComponent<LineRenderer>();
                        component.startWidth = 0.04f;
                        component.endWidth = 0.04f;
                        component.material.shader = Shader.Find("GUI/Text Shader");
                        component.positionCount = 4;
                        component.loop = true;
                        component.useWorldSpace = true;
                        component.forceRenderingOff = false;
                        component.startColor = color;
                        component.endColor = color;
                        Vector3 position = vrrig.transform.position;
                        Vector3 vector = Vector3.Cross((GorillaTagger.Instance.headCollider.transform.position - position).normalized, Vector3.up).normalized * 0.43f;
                        Vector3 vector2 = Vector3.up * 0.5f;
                        Vector3 vector3 = position - vector - vector2;
                        Vector3 vector4 = position + vector - vector2;
                        Vector3 vector5 = position + vector + vector2;
                        Vector3 vector6 = position - vector + vector2;
                        component.SetPosition(0, vector3);
                        component.SetPosition(1, vector4);
                        component.SetPosition(2, vector5);
                        component.SetPosition(3, vector6);
                    }
                }
                foreach (VRRig Player in GorillaParent.instance.vrrigs)
                {
                    if (Player == GorillaTagger.Instance.offlineVRRig) continue;
                    UnityEngine.Color color;
                    if (RigIsInfected(Player))
                        color = UnityEngine.Color.red;
                    else
                        color = UnityEngine.Color.green;
                    var gameobject = new GameObject("Line");
                    LineRenderer lineRenderer = gameobject.AddComponent<LineRenderer>();
                    lineRenderer.startColor = color;
                    lineRenderer.endColor = color;
                    lineRenderer.startWidth = 0.01f;
                    lineRenderer.endWidth = 0.01f;
                    lineRenderer.positionCount = 2;
                    lineRenderer.useWorldSpace = true;
                    lineRenderer.SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                    lineRenderer.SetPosition(1, Player.transform.position);
                    lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                    UnityEngine.Object.Destroy(lineRenderer, Time.deltaTime);
                }
            }
            else if (espColor == 2)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    bool flag = vrrig == GorillaTagger.Instance.offlineVRRig;
                    bool flag2 = !flag;
                    bool flag3 = flag2;
                    if (flag3)
                    {
                        bool flag7 = vrrig.gameObject.GetComponent<LineRenderer>() == null;
                        bool flag8 = flag7;
                        bool flag9 = flag8;
                        if (flag9)
                        {
                            vrrig.gameObject.AddComponent<LineRenderer>();
                        }
                        LineRenderer component = vrrig.gameObject.GetComponent<LineRenderer>();
                        component.startWidth = 0.04f;
                        component.endWidth = 0.04f;
                        component.material.shader = Shader.Find("GUI/Text Shader");
                        component.positionCount = 4;
                        component.loop = true;
                        component.useWorldSpace = true;
                        component.forceRenderingOff = false;
                        component.startColor = vrrig.playerColor;
                        component.endColor = vrrig.playerColor;
                        Vector3 position = vrrig.transform.position;
                        Vector3 vector = Vector3.Cross((GorillaTagger.Instance.headCollider.transform.position - position).normalized, Vector3.up).normalized * 0.43f;
                        Vector3 vector2 = Vector3.up * 0.5f;
                        Vector3 vector3 = position - vector - vector2;
                        Vector3 vector4 = position + vector - vector2;
                        Vector3 vector5 = position + vector + vector2;
                        Vector3 vector6 = position - vector + vector2;
                        component.SetPosition(0, vector3);
                        component.SetPosition(1, vector4);
                        component.SetPosition(2, vector5);
                        component.SetPosition(3, vector6);
                    }
                }
                foreach (VRRig Player in GorillaParent.instance.vrrigs)
                {
                    if (Player == GorillaTagger.Instance.offlineVRRig) continue;

                    var gameobject = new GameObject("Line");
                    LineRenderer lineRenderer = gameobject.AddComponent<LineRenderer>();
                    lineRenderer.startColor = Player.playerColor;
                    lineRenderer.endColor = Player.playerColor;
                    lineRenderer.startWidth = 0.01f;
                    lineRenderer.endWidth = 0.01f;
                    lineRenderer.positionCount = 2;
                    lineRenderer.useWorldSpace = true;
                    lineRenderer.SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                    lineRenderer.SetPosition(1, Player.transform.position);
                    lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                    UnityEngine.Object.Destroy(lineRenderer, Time.deltaTime);
                }
            }
            else if (espColor == 3)
            {
                GradientColorKey[] array = new GradientColorKey[7];
                array[0].color = UnityEngine.Color.red;
                array[0].time = 0f;
                array[1].color = UnityEngine.Color.yellow;
                array[1].time = 0.2f;
                array[2].color = UnityEngine.Color.green;
                array[2].time = 0.3f;
                array[3].color = UnityEngine.Color.cyan;
                array[3].time = 0.5f;
                array[4].color = UnityEngine.Color.blue;
                array[4].time = 0.6f;
                array[5].color = UnityEngine.Color.magenta;
                array[5].time = 0.8f;
                array[6].color = UnityEngine.Color.red;
                array[6].time = 1f;
                Gradient gradient = new Gradient();
                gradient.colorKeys = array;
                float num = Mathf.PingPong(Time.time / 2f, 1f);
                UnityEngine.Color color = gradient.Evaluate(num);

                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    bool flag = vrrig == GorillaTagger.Instance.offlineVRRig;
                    bool flag2 = !flag;
                    bool flag3 = flag2;
                    if (flag3)
                    {
                        bool flag7 = vrrig.gameObject.GetComponent<LineRenderer>() == null;
                        bool flag8 = flag7;
                        bool flag9 = flag8;
                        if (flag9)
                        {
                            vrrig.gameObject.AddComponent<LineRenderer>();
                        }
                        LineRenderer component = vrrig.gameObject.GetComponent<LineRenderer>();
                        component.startWidth = 0.04f;
                        component.endWidth = 0.04f;
                        component.material.shader = Shader.Find("GUI/Text Shader");
                        component.positionCount = 4;
                        component.loop = true;
                        component.useWorldSpace = true;
                        component.forceRenderingOff = false;
                        component.startColor = color;
                        component.endColor = color;
                        Vector3 position = vrrig.transform.position;
                        Vector3 vector = Vector3.Cross((GorillaTagger.Instance.headCollider.transform.position - position).normalized, Vector3.up).normalized * 0.43f;
                        Vector3 vector2 = Vector3.up * 0.5f;
                        Vector3 vector3 = position - vector - vector2;
                        Vector3 vector4 = position + vector - vector2;
                        Vector3 vector5 = position + vector + vector2;
                        Vector3 vector6 = position - vector + vector2;
                        component.SetPosition(0, vector3);
                        component.SetPosition(1, vector4);
                        component.SetPosition(2, vector5);
                        component.SetPosition(3, vector6);
                    }
                }
                foreach (VRRig Player in GorillaParent.instance.vrrigs)
                {
                    if (Player == GorillaTagger.Instance.offlineVRRig) continue;

                    var gameobject = new GameObject("Line");
                    LineRenderer lineRenderer = gameobject.AddComponent<LineRenderer>();
                    lineRenderer.startColor = color;
                    lineRenderer.endColor = color;
                    lineRenderer.startWidth = 0.01f;
                    lineRenderer.endWidth = 0.01f;
                    lineRenderer.positionCount = 2;
                    lineRenderer.useWorldSpace = true;
                    lineRenderer.SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                    lineRenderer.SetPosition(1, Player.transform.position);
                    lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                    UnityEngine.Object.Destroy(lineRenderer, Time.deltaTime);
                }
            }
            else if (espColor == 4)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    bool flag = vrrig == GorillaTagger.Instance.offlineVRRig;
                    bool flag2 = !flag;
                    bool flag3 = flag2;
                    if (flag3)
                    {
                        bool flag7 = vrrig.gameObject.GetComponent<LineRenderer>() == null;
                        bool flag8 = flag7;
                        bool flag9 = flag8;
                        if (flag9)
                        {
                            vrrig.gameObject.AddComponent<LineRenderer>();
                        }
                        LineRenderer component = vrrig.gameObject.GetComponent<LineRenderer>();
                        component.startWidth = 0.04f;
                        component.endWidth = 0.04f;
                        component.material.shader = Shader.Find("GUI/Text Shader");
                        component.positionCount = 4;
                        component.loop = true;
                        component.useWorldSpace = true;
                        component.forceRenderingOff = false;
                        component.startColor = RoyalBlue;
                        component.endColor = RoyalBlue;
                        Vector3 position = vrrig.transform.position;
                        Vector3 vector = Vector3.Cross((GorillaTagger.Instance.headCollider.transform.position - position).normalized, Vector3.up).normalized * 0.43f;
                        Vector3 vector2 = Vector3.up * 0.5f;
                        Vector3 vector3 = position - vector - vector2;
                        Vector3 vector4 = position + vector - vector2;
                        Vector3 vector5 = position + vector + vector2;
                        Vector3 vector6 = position - vector + vector2;
                        component.SetPosition(0, vector3);
                        component.SetPosition(1, vector4);
                        component.SetPosition(2, vector5);
                        component.SetPosition(3, vector6);
                    }
                }
                foreach (VRRig Player in GorillaParent.instance.vrrigs)
                {
                    if (Player == GorillaTagger.Instance.offlineVRRig) continue;

                    var gameobject = new GameObject("Line");
                    LineRenderer lineRenderer = gameobject.AddComponent<LineRenderer>();
                    lineRenderer.startColor = RoyalBlue;
                    lineRenderer.endColor = RoyalBlue;
                    lineRenderer.startWidth = 0.01f;
                    lineRenderer.endWidth = 0.01f;
                    lineRenderer.positionCount = 2;
                    lineRenderer.useWorldSpace = true;
                    lineRenderer.SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                    lineRenderer.SetPosition(1, Player.transform.position);
                    lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                    UnityEngine.Object.Destroy(lineRenderer, Time.deltaTime);
                }
            }
        }
        public static void DisableCSGO()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                bool flag = vrrig.gameObject.GetComponent<LineRenderer>() != null;
                bool flag2 = flag;
                if (flag2)
                {
                    UnityEngine.Object.Destroy(vrrig.gameObject.GetComponent<LineRenderer>());
                }
            }

        }
        public static void ESP()
        {
            // 1 = casual
            // 2 = infection
            // 3 = rainbow
            // 4 = menu color
            if (espColor == 1)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        if (vrrig.mainSkin.material.name.Contains("fected"))
                        {
                            vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                            vrrig.mainSkin.material.color = UnityEngine.Color.red;
                        }
                        else
                        {
                            vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                            vrrig.mainSkin.material.color = UnityEngine.Color.green;
                        }
                    }
                }
            }
            else if (espColor == 2)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                        vrrig.mainSkin.material.color = vrrig.playerColor;
                    }
                }
            }
            else if (espColor == 3)
            {
                GradientColorKey[] array = new GradientColorKey[7];
                array[0].color = UnityEngine.Color.red;
                array[0].time = 0f;
                array[1].color = UnityEngine.Color.yellow;
                array[1].time = 0.2f;
                array[2].color = UnityEngine.Color.green;
                array[2].time = 0.3f;
                array[3].color = UnityEngine.Color.cyan;
                array[3].time = 0.5f;
                array[4].color = UnityEngine.Color.blue;
                array[4].time = 0.6f;
                array[5].color = UnityEngine.Color.magenta;
                array[5].time = 0.8f;
                array[6].color = UnityEngine.Color.red;
                array[6].time = 1f;
                Gradient gradient = new Gradient();
                gradient.colorKeys = array;
                float num = Mathf.PingPong(Time.time / 2f, 1f);
                UnityEngine.Color color = gradient.Evaluate(num);

                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                        vrrig.mainSkin.material.color = color;
                    }
                }
            }
            else if (espColor == 4) 
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                        vrrig.mainSkin.material.color = RoyalBlue;
                    }
                }
            }
        }
        public static void DisableESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    vrrig.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
                }
            }
        }
        public static void BallHaloOrbit()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                l += 5f * Time.deltaTime;
                GameObject gameObject = GameObject.CreatePrimitive(0);
                gameObject.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                UnityEngine.Object.Destroy(gameObject.gameObject.GetComponent<SphereCollider>());
                gameObject.gameObject.GetComponent<Renderer>().material.color = UnityEngine.Color.Lerp(new UnityEngine.Color(0f, 1f, 0f, 0.5f), new UnityEngine.Color(0f, 1f, 1f, 0.5f), Mathf.PingPong(Time.time, 1f));
                float num = GorillaTagger.Instance.offlineVRRig.headConstraint.transform.position.x + 0.6f * Mathf.Cos(l);
                float num2 = GorillaTagger.Instance.offlineVRRig.headConstraint.transform.position.y + -1f;
                float num3 = GorillaTagger.Instance.offlineVRRig.headConstraint.transform.position.z + 0.6f * Mathf.Sin(l);
                gameObject.gameObject.transform.position = vrrig.transform.position + new Vector3(0.6f * Mathf.Cos(l), -1f, 0.6f * Mathf.Sin(l)) + new Vector3(0f, 1.75f, 0f);
                GameObject gameObject2 = new GameObject("Line");
                LineRenderer lineRenderer = gameObject2.AddComponent<LineRenderer>();
                UnityEngine.Color endColor = UnityEngine.Color.Lerp(new UnityEngine.Color(0f, 1f, 0f, 0.5f), new UnityEngine.Color(0f, 1f, 1f, 0.5f), Mathf.PingPong(Time.time, 1f));
                UnityEngine.Color startColor = UnityEngine.Color.Lerp(new UnityEngine.Color(0f, 1f, 0f, 0.5f), new UnityEngine.Color(0f, 1f, 1f, 0.5f), Mathf.PingPong(Time.time, 1f));
                lineRenderer.startColor = startColor;
                lineRenderer.endColor = endColor;
                lineRenderer.startWidth = 0.02f;
                lineRenderer.endWidth = 0.02f;
                lineRenderer.positionCount = 2;
                lineRenderer.useWorldSpace = true;
                lineRenderer.SetPosition(0, gameObject.gameObject.transform.position);
                lineRenderer.SetPosition(1, GorillaTagger.Instance.headCollider.gameObject.transform.position);
                lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                UnityEngine.Object.Destroy(lineRenderer, Time.deltaTime);
                UnityEngine.Object.Destroy(gameObject2, Time.deltaTime);
                UnityEngine.Object.Destroy(gameObject.gameObject, 0.2f);
            }

        }
        public static void BallESP()
        {
            // 1 = casual
            // 2 = infection
            // 3 = rainbow
            // 4 = menu color
            if (espColor == 1)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        GameObject ESPBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        ESPBall.transform.position = vrrig.transform.position;
                        UnityEngine.Object.Destroy(ESPBall.GetComponent<BoxCollider>());
                        ESPBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                        ESPBall.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                        if (vrrig.mainSkin.material.name.Contains("fected"))
                        {
                            ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            ESPBall.GetComponent<Renderer>().material.color = UnityEngine.Color.red;
                        }
                        else
                        {
                            ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            ESPBall.GetComponent<Renderer>().material.color = UnityEngine.Color.green;
                        }
                        UnityEngine.Object.Destroy(ESPBall, Time.deltaTime);
                    }
                }
            }
            else if (espColor == 2)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        GameObject ESPBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        ESPBall.transform.position = vrrig.transform.position;
                        UnityEngine.Object.Destroy(ESPBall.GetComponent<BoxCollider>());
                        ESPBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                        ESPBall.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                        ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        ESPBall.GetComponent<Renderer>().material.color = vrrig.playerColor;
                        UnityEngine.Object.Destroy(ESPBall, Time.deltaTime);
                    }
                }
            }
            else if (espColor == 3)
            {
                GradientColorKey[] array = new GradientColorKey[7];
                array[0].color = UnityEngine.Color.red;
                array[0].time = 0f;
                array[1].color = UnityEngine.Color.yellow;
                array[1].time = 0.2f;
                array[2].color = UnityEngine.Color.green;
                array[2].time = 0.3f;
                array[3].color = UnityEngine.Color.cyan;
                array[3].time = 0.5f;
                array[4].color = UnityEngine.Color.blue;
                array[4].time = 0.6f;
                array[5].color = UnityEngine.Color.magenta;
                array[5].time = 0.8f;
                array[6].color = UnityEngine.Color.red;
                array[6].time = 1f;
                Gradient gradient = new Gradient();
                gradient.colorKeys = array;
                float num = Mathf.PingPong(Time.time / 2f, 1f);
                UnityEngine.Color color = gradient.Evaluate(num);

                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        GameObject ESPBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        ESPBall.transform.position = vrrig.transform.position;
                        UnityEngine.Object.Destroy(ESPBall.GetComponent<BoxCollider>());
                        ESPBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                        ESPBall.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                        ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        ESPBall.GetComponent<Renderer>().material.color = color;
                        UnityEngine.Object.Destroy(ESPBall, Time.deltaTime);
                    }
                }
            }
            else if (espColor == 4)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        GameObject ESPBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        ESPBall.transform.position = vrrig.transform.position;
                        UnityEngine.Object.Destroy(ESPBall.GetComponent<BoxCollider>());
                        ESPBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                        ESPBall.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                        ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        ESPBall.GetComponent<Renderer>().material.color = RoyalBlue;
                        UnityEngine.Object.Destroy(ESPBall, Time.deltaTime);
                    }
                }
            }
        }
        public static void BoxESP(bool d)
        {
            if (d == false)
            {
                if (espColor == 1)
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != GorillaTagger.Instance.offlineVRRig)
                        {
                            GameObject ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            ESPBox.transform.position = vrrig.transform.position;
                            UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                            ESPBox.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                            ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                            if (vrrig.mainSkin.material.name.Contains("fected"))
                            {
                                ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBox.GetComponent<Renderer>().material.color = UnityEngine.Color.red;
                            }
                            else
                            {
                                ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBox.GetComponent<Renderer>().material.color = UnityEngine.Color.green;
                            }
                            UnityEngine.Object.Destroy(ESPBox, Time.deltaTime);
                        }
                    }
                }
                if (espColor == 2)
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != GorillaTagger.Instance.offlineVRRig)
                        {
                            GameObject ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            ESPBox.transform.position = vrrig.transform.position;
                            UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                            ESPBox.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                            ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                            ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            ESPBox.GetComponent<Renderer>().material.color = vrrig.playerColor;
                            UnityEngine.Object.Destroy(ESPBox, Time.deltaTime);
                        }
                    }
                }
                if (espColor == 3)
                {
                    GradientColorKey[] array = new GradientColorKey[7];
                    array[0].color = UnityEngine.Color.red;
                    array[0].time = 0f;
                    array[1].color = UnityEngine.Color.yellow;
                    array[1].time = 0.2f;
                    array[2].color = UnityEngine.Color.green;
                    array[2].time = 0.3f;
                    array[3].color = UnityEngine.Color.cyan;
                    array[3].time = 0.5f;
                    array[4].color = UnityEngine.Color.blue;
                    array[4].time = 0.6f;
                    array[5].color = UnityEngine.Color.magenta;
                    array[5].time = 0.8f;
                    array[6].color = UnityEngine.Color.red;
                    array[6].time = 1f;
                    Gradient gradient = new Gradient();
                    gradient.colorKeys = array;
                    float num = Mathf.PingPong(Time.time / 2f, 1f);
                    UnityEngine.Color color = gradient.Evaluate(num);

                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != GorillaTagger.Instance.offlineVRRig)
                        {
                            GameObject ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            ESPBox.transform.position = vrrig.transform.position;
                            UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                            ESPBox.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                            ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                            ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            ESPBox.GetComponent<Renderer>().material.color = color;
                            UnityEngine.Object.Destroy(ESPBox, Time.deltaTime);
                        }
                    }
                }
                if (espColor == 4)
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != GorillaTagger.Instance.offlineVRRig)
                        {
                            GameObject ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            ESPBox.transform.position = vrrig.transform.position;
                            UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                            ESPBox.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                            ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                            ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            ESPBox.GetComponent<Renderer>().material.color = RoyalBlue;
                            UnityEngine.Object.Destroy(ESPBox, Time.deltaTime);
                        }
                    }
                }
            }
            else if (d == true)
            {
                if (espColor == 1)
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != GorillaTagger.Instance.offlineVRRig)
                        {
                            GameObject ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            ESPBox.transform.position = vrrig.transform.position;
                            UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                            ESPBox.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
                            if (vrrig.mainSkin.material.name.Contains("fected"))
                            {
                                ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBox.GetComponent<Renderer>().material.color = UnityEngine.Color.red;
                            }
                            else
                            {
                                ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBox.GetComponent<Renderer>().material.color = UnityEngine.Color.green;
                            }
                            UnityEngine.Object.Destroy(ESPBox, Time.deltaTime);
                        }
                    }
                }
                if (espColor == 2)
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != GorillaTagger.Instance.offlineVRRig)
                        {
                            GameObject ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            ESPBox.transform.position = vrrig.transform.position;
                            UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                            ESPBox.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
                            ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            ESPBox.GetComponent<Renderer>().material.color = vrrig.playerColor;
                            UnityEngine.Object.Destroy(ESPBox, Time.deltaTime);
                        }
                    }
                }
                if (espColor == 3)
                {
                    GradientColorKey[] array = new GradientColorKey[7];
                    array[0].color = UnityEngine.Color.red;
                    array[0].time = 0f;
                    array[1].color = UnityEngine.Color.yellow;
                    array[1].time = 0.2f;
                    array[2].color = UnityEngine.Color.green;
                    array[2].time = 0.3f;
                    array[3].color = UnityEngine.Color.cyan;
                    array[3].time = 0.5f;
                    array[4].color = UnityEngine.Color.blue;
                    array[4].time = 0.6f;
                    array[5].color = UnityEngine.Color.magenta;
                    array[5].time = 0.8f;
                    array[6].color = UnityEngine.Color.red;
                    array[6].time = 1f;
                    Gradient gradient = new Gradient();
                    gradient.colorKeys = array;
                    float num = Mathf.PingPong(Time.time / 2f, 1f);
                    UnityEngine.Color color = gradient.Evaluate(num);

                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != GorillaTagger.Instance.offlineVRRig)
                        {
                            GameObject ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            ESPBox.transform.position = vrrig.transform.position;
                            UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                            ESPBox.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
                            ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            ESPBox.GetComponent<Renderer>().material.color = color;
                            UnityEngine.Object.Destroy(ESPBox, Time.deltaTime);
                        }
                    }
                }
                if (espColor == 4)
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != GorillaTagger.Instance.offlineVRRig)
                        {
                            GameObject ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            ESPBox.transform.position = vrrig.transform.position;
                            UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                            ESPBox.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
                            ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            ESPBox.GetComponent<Renderer>().material.color = RoyalBlue;
                            UnityEngine.Object.Destroy(ESPBox, Time.deltaTime);
                        }
                    }
                }
            }
        }
        public static void Tracers()
        {
            if (espColor == 1)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        GameObject tracer1 = new GameObject("Line");
                        LineRenderer tracer2 = tracer1.AddComponent<LineRenderer>();
                        tracer2.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                        tracer2.SetPosition(1, vrrig.transform.position);
                        tracer2.startWidth = 0.0225f;
                        tracer2.endWidth = 0.0225f;

                        tracer2.material.shader = Shader.Find("GUI/Text Shader");

                        UnityEngine.Object.Destroy(tracer1, Time.deltaTime);

                        if (vrrig.mainSkin.material.name.Contains("fected"))
                        {
                            tracer2.startColor = UnityEngine.Color.red;
                            tracer2.endColor = UnityEngine.Color.red;
                        }
                        else
                        {
                            tracer2.startColor = UnityEngine.Color.green;
                            tracer2.endColor = UnityEngine.Color.green;
                        }
                    }
                }
            }
            else if (espColor == 2)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        GameObject line = new GameObject("Line");
                        LineRenderer Line = line.AddComponent<LineRenderer>();
                        Line.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                        Line.SetPosition(1, vrrig.transform.position);
                        Line.startWidth = 0.0225f;
                        Line.endWidth = 0.0225f;

                        Line.startColor = vrrig.playerColor;
                        Line.endColor = vrrig.playerColor;
                        Line.material.shader = Shader.Find("GUI/Text Shader");

                        UnityEngine.Object.Destroy(line, Time.deltaTime);
                    }
                }
            }
            else if (espColor == 3)
            {
                GradientColorKey[] array = new GradientColorKey[7];
                array[0].color = UnityEngine.Color.red;
                array[0].time = 0f;
                array[1].color = UnityEngine.Color.yellow;
                array[1].time = 0.2f;
                array[2].color = UnityEngine.Color.green;
                array[2].time = 0.3f;
                array[3].color = UnityEngine.Color.cyan;
                array[3].time = 0.5f;
                array[4].color = UnityEngine.Color.blue;
                array[4].time = 0.6f;
                array[5].color = UnityEngine.Color.magenta;
                array[5].time = 0.8f;
                array[6].color = UnityEngine.Color.red;
                array[6].time = 1f;
                Gradient gradient = new Gradient();
                gradient.colorKeys = array;
                float num = Mathf.PingPong(Time.time / 2f, 1f);
                UnityEngine.Color color = gradient.Evaluate(num);

                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        GameObject line = new GameObject("Line");
                        LineRenderer Line = line.AddComponent<LineRenderer>();
                        Line.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                        Line.SetPosition(1, vrrig.transform.position);
                        Line.startWidth = 0.0225f;
                        Line.endWidth = 0.0225f;

                        Line.startColor = color;
                        Line.endColor = color;
                        Line.material.shader = Shader.Find("GUI/Text Shader");

                        UnityEngine.Object.Destroy(line, Time.deltaTime);
                    }
                }
            }
            else if (espColor == 4)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        GameObject line = new GameObject("Line");
                        LineRenderer Line = line.AddComponent<LineRenderer>();
                        Line.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                        Line.SetPosition(1, vrrig.transform.position);
                        Line.startWidth = 0.0225f;
                        Line.endWidth = 0.0225f;

                        Line.startColor = RoyalBlue;
                        Line.endColor = RoyalBlue;
                        Line.material.shader = Shader.Find("GUI/Text Shader");

                        UnityEngine.Object.Destroy(line, Time.deltaTime);
                    }
                }
            }
        }
        public static void RGBSky()
        {
            GradientColorKey[] array = new GradientColorKey[7];
            array[0].color = UnityEngine.Color.red;
            array[0].time = 0f;
            array[1].color = UnityEngine.Color.yellow;
            array[1].time = 0.2f;
            array[2].color = UnityEngine.Color.green;
            array[2].time = 0.3f;
            array[3].color = UnityEngine.Color.cyan;
            array[3].time = 0.5f;
            array[4].color = UnityEngine.Color.blue;
            array[4].time = 0.6f;
            array[5].color = UnityEngine.Color.magenta;
            array[5].time = 0.8f;
            array[6].color = UnityEngine.Color.red;
            array[6].time = 1f;
            Gradient gradient = new Gradient();
            gradient.colorKeys = array;
            float num = Mathf.PingPong(Time.time / 2f, 1f);
            UnityEngine.Color color = gradient.Evaluate(num);
            Renderer Sky = GameObject.Find("Environment Objects/LocalObjects_Prefab/Standard Sky").GetComponent<Renderer>();
            Sky.material.shader = Shader.Find("GorillaTag/UberShader");
            Sky.material.color = color;
        }

        public static float l;
        public static bool fps;
    }
}

