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
using System.Xml.Linq;
using MenkerMenu.Utilities;


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
                        color.a = 0.1f;
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
                    color.a = 0.1f;
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
                        UnityEngine.Color color = vrrig.playerColor;
                        color.a = 0.1f;
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
                    UnityEngine.Color color = vrrig.playerColor;
                    color.a = 0.1f;
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
                        color.a = 0.1f;                        
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
                    color.a = 0.1f;
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
                        UnityEngine.Color color = RoyalBlue;
                        color.a = 0.1f;
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
                    UnityEngine.Color color = RoyalBlue;
                    color.a = 0.1f;
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
        public static void MenkerEsp()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig && rig != GorillaTagger.Instance.offlineVRRig)
                {
                    if (rig.Creator.NickName == "MENKER")
                    {
                        rig.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    }
                }
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
                            UnityEngine.Color color = UnityEngine.Color.red;
                            color.a = 0.1f;
                            ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            ESPBall.GetComponent<Renderer>().material.color = color;
                        }
                        else
                        {
                            UnityEngine.Color color = UnityEngine.Color.green;
                            color.a = 0.1f;
                            ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            ESPBall.GetComponent<Renderer>().material.color = color;
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
                        UnityEngine.Color color = vrrig.playerColor;
                        color.a = 0.1f;
                        ESPBall.GetComponent<Renderer>().material.color = color;
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
                        color.a = 0.1f;
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
                        UnityEngine.Color color = RoyalBlue;
                        color.a = 0.1f;
                        ESPBall.GetComponent<Renderer>().material.color = color;
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
                            ESPBox.transform.localScale = new Vector3(0.5f, 1f, 0f);
                            ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                            if (vrrig.mainSkin.material.name.Contains("fected"))
                            {
                                UnityEngine.Color color = UnityEngine.Color.red;
                                color.a = 0.1f;
                                ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBox.GetComponent<Renderer>().material.color = color;
                            }
                            else
                            {
                                UnityEngine.Color color = UnityEngine.Color.red;
                                color.a = 0.1f;
                                ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBox.GetComponent<Renderer>().material.color = color;
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
                            ESPBox.transform.localScale = new Vector3(0.5f, 1f, 0f);
                            ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                            ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            UnityEngine.Color color = vrrig.playerColor;
                            color.a = 0.1f;
                            ESPBox.GetComponent<Renderer>().material.color = color;
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
                            ESPBox.transform.localScale = new Vector3(0.5f, 1f, 0f);
                            ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                            ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            color.a = 0.1f;
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
                            ESPBox.transform.localScale = new Vector3(0.5f, 1f, 0f);
                            ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                            ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                            UnityEngine.Color color = RoyalBlue;
                            color.a = 0.1f;
                            ESPBox.GetComponent<Renderer>().material.color = color;
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
                                UnityEngine.Color color = UnityEngine.Color.red;
                                color.a = 0.1f;
                                ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBox.GetComponent<Renderer>().material.color = color;
                            }
                            else
                            {
                                UnityEngine.Color color = UnityEngine.Color.green;
                                color.a = 0.1f;
                                ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBox.GetComponent<Renderer>().material.color = color;
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
                            UnityEngine.Color color = vrrig.playerColor;
                            color.a = 0.1f;
                            ESPBox.GetComponent<Renderer>().material.color = color;
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
                            color.a = 0.1f;
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
                            UnityEngine.Color color = RoyalBlue;
                            color.a = 0.1f;
                            ESPBox.GetComponent<Renderer>().material.color = color;
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
                            UnityEngine.Color color = UnityEngine.Color.red;
                            color.a = 0.1f;
                            tracer2.startColor = color;
                            tracer2.endColor = color;
                        }
                        else
                        {
                            UnityEngine.Color color = UnityEngine.Color.green;
                            color.a = 0.1f;
                            tracer2.startColor = color;
                            tracer2.endColor = color;
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
                        UnityEngine.Color color = vrrig.playerColor;
                        color.a = 0.1f;
                        Line.startColor = color;
                        Line.endColor = color;
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
                        color.a = 0.1f;
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
                        UnityEngine.Color color = RoyalBlue;
                        color.a = 0.1f;
                        Line.startColor = color;
                        Line.endColor = color;
                        Line.material.shader = Shader.Find("GUI/Text Shader");

                        UnityEngine.Object.Destroy(line, Time.deltaTime);
                    }
                }
            }
        }
        public static void Skeleton()
        {
            // 1 = casual
            // 2 = infection
            // 3 = rainbow
            // 4 = menu color
            if (espColor == 1)
            {
                foreach (VRRig Player in GorillaParent.instance.vrrigs)
                {
                    if (Player == GorillaTagger.Instance.offlineVRRig) continue;

                    UnityEngine.Color color;
                    if (RigIsInfected(Player))
                    {
                        color = UnityEngine.Color.red;
                    }
                    else
                    {
                        color = UnityEngine.Color.green;
                    }

                    Material material = new Material(Shader.Find("GUI/Text Shader"));
                    material.color = color;
                    if (!Player.head.rigTarget.gameObject.GetComponent<LineRenderer>())
                    {
                        Player.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                    }
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().endWidth = 0.025f;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().startWidth = 0.025f;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().material = material;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(0, Player.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(1, Player.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));
                    for (int b = 0; b < Enumerable.Count<int>(bones); b += 2)
                    {
                        if (!Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>())
                        {
                            Player.mainSkin.bones[bones[b]].gameObject.AddComponent<LineRenderer>();
                        }
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().endWidth = 0.025f;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().startWidth = 0.025f;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().material = material;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().SetPosition(0, Player.mainSkin.bones[bones[b]].position);
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().SetPosition(1, Player.mainSkin.bones[bones[b + 1]].position);
                    }
                }
            }
            else if (espColor == 2)
            {
                foreach (VRRig Player in GorillaParent.instance.vrrigs)
                {
                    if (Player == GorillaTagger.Instance.offlineVRRig) continue;

                    UnityEngine.Color color;
                    color.a = 0.1f;
                    color = vrrig.playerColor;
                    Material material = new Material(Shader.Find("GUI/Text Shader"));
                    material.color = color;
                    if (!Player.head.rigTarget.gameObject.GetComponent<LineRenderer>())
                    {
                        Player.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                    }
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().endWidth = 0.025f;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().startWidth = 0.025f;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().material = material;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(0, Player.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(1, Player.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));
                    for (int b = 0; b < Enumerable.Count<int>(bones); b += 2)
                    {
                        if (!Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>())
                        {
                            Player.mainSkin.bones[bones[b]].gameObject.AddComponent<LineRenderer>();
                        }
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().endWidth = 0.025f;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().startWidth = 0.025f;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().material = material;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().SetPosition(0, Player.mainSkin.bones[bones[b]].position);
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().SetPosition(1, Player.mainSkin.bones[bones[b + 1]].position);
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

                foreach (VRRig Player in GorillaParent.instance.vrrigs)
                {
                    if (Player == GorillaTagger.Instance.offlineVRRig) continue;

                    color.a = 0.1f;
                    Material material = new Material(Shader.Find("GUI/Text Shader"));
                    material.color = color;
                    if (!Player.head.rigTarget.gameObject.GetComponent<LineRenderer>())
                    {
                        Player.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                    }
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().endWidth = 0.025f;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().startWidth = 0.025f;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().material = material;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(0, Player.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(1, Player.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));
                    for (int b = 0; b < Enumerable.Count<int>(bones); b += 2)
                    {
                        if (!Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>())
                        {
                            Player.mainSkin.bones[bones[b]].gameObject.AddComponent<LineRenderer>();
                        }
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().endWidth = 0.025f;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().startWidth = 0.025f;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().material = material;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().SetPosition(0, Player.mainSkin.bones[bones[b]].position);
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().SetPosition(1, Player.mainSkin.bones[bones[b + 1]].position);
                    }
                }
            }
            else if (espColor == 4)
            {
                foreach (VRRig Player in GorillaParent.instance.vrrigs)
                {
                    if (Player == GorillaTagger.Instance.offlineVRRig) continue;

                    UnityEngine.Color color;
                    color.a = 0.1f;
                    color = RoyalBlue;
                    Material material = new Material(Shader.Find("GUI/Text Shader"));
                    material.color = color;
                    if (!Player.head.rigTarget.gameObject.GetComponent<LineRenderer>())
                    {
                        Player.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                    }
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().endWidth = 0.025f;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().startWidth = 0.025f;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().material = material;
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(0, Player.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                    Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(1, Player.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));
                    for (int b = 0; b < Enumerable.Count<int>(bones); b += 2)
                    {
                        if (!Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>())
                        {
                            Player.mainSkin.bones[bones[b]].gameObject.AddComponent<LineRenderer>();
                        }
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().endWidth = 0.025f;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().startWidth = 0.025f;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().material = material;
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().SetPosition(0, Player.mainSkin.bones[bones[b]].position);
                        Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().SetPosition(1, Player.mainSkin.bones[bones[b + 1]].position);
                    }
                }
            }
        }
        public static void DisableSkeleton()
        {
            foreach (VRRig Player in GorillaParent.instance.vrrigs)
            {
                for (int j = 0; j < Enumerable.Count<int>(bones); j += 2)
                {
                    if (Player.mainSkin.bones[bones[j]].gameObject.GetComponent<LineRenderer>())
                    {
                        GameObject.Destroy(Player.mainSkin.bones[bones[j]].gameObject.GetComponent<LineRenderer>());
                    }
                    if (Player.head.rigTarget.gameObject.GetComponent<LineRenderer>())
                    {
                        GameObject.Destroy(Player.head.rigTarget.gameObject.GetComponent<LineRenderer>());
                    }
                }
            }
        }
        public static void DistanceESP()
        {
            foreach (VRRig Player in GorillaParent.instance.vrrigs)
            {
                if (Player == GorillaTagger.Instance.offlineVRRig) continue;
                distance = new GameObject($"{Player.playerName}'s Distance");
                TextMesh textMesh = distance.AddComponent<TextMesh>();
                textMesh.fontSize = 20;
                textMesh.fontStyle = FontStyle.Normal;
                textMesh.characterSize = 0.1f;
                textMesh.anchor = TextAnchor.MiddleCenter;
                textMesh.alignment = TextAlignment.Center;
                textMesh.color = RoyalBlue;
                textMesh.text = Player.playerName;
                float textWidth = textMesh.GetComponent<Renderer>().bounds.size.x;
                distance.transform.position = Player.headMesh.transform.position + new Vector3(0f, .65f, 0f);
                distance.transform.LookAt(Camera.main.transform.position);
                distance.transform.Rotate(0, 180, 0);
                distance.GetComponent<TextMesh>().text = $"{Convert.ToInt32(Vector3.Distance(GorillaLocomotion.Player.Instance.headCollider.transform.position, Player.transform.position))}m";
                GameObject.Destroy(distance, Time.deltaTime);
            }
        }
        public static void Nametags()
        {
            foreach (VRRig Player in GorillaParent.instance.vrrigs)
            {
                if (Player == GorillaTagger.Instance.offlineVRRig) continue;
                name = new GameObject($"{Player.playerName}'s Nametag");
                TextMesh textMesh = name.AddComponent<TextMesh>();
                textMesh.fontSize = 20;
                textMesh.fontStyle = FontStyle.Normal;
                textMesh.characterSize = 0.1f;
                textMesh.anchor = TextAnchor.MiddleCenter;
                textMesh.alignment = TextAlignment.Center;
                textMesh.color = RoyalBlue;
                textMesh.text = Player.playerName;
                float textWidth = textMesh.GetComponent<Renderer>().bounds.size.x;
                name.transform.position = Player.headMesh.transform.position + new Vector3(0f, .90f, 0f);
                name.transform.LookAt(Camera.main.transform.position);
                name.transform.Rotate(0, 180, 0);
                name.GetComponent<TextMesh>().text = RigManager.GetPlayerFromVRRig(Player).NickName;
                GameObject.Destroy(name, Time.deltaTime);
            }
        }
        public static void PrismESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    UnityEngine.Color playerColor = vrrig.playerColor;
                    GameObject pyramidWireframe = new GameObject("PyramidWireframe");
                    pyramidWireframe.transform.position = vrrig.transform.position;
                    LineRenderer lineRenderer = pyramidWireframe.AddComponent<LineRenderer>();
                    Shader alwaysVisibleShader = Shader.Find("GUI/Text Shader");
                    if (!alwaysVisibleShader) alwaysVisibleShader = Shader.Find("Unlit/Color");
                    Material seeThroughMaterial = new Material(alwaysVisibleShader);
                    seeThroughMaterial.color = new UnityEngine.Color(playerColor.r, playerColor.g, playerColor.b, 0.8f);
                    lineRenderer.material = seeThroughMaterial;
                    lineRenderer.startWidth = 0.025f;
                    lineRenderer.endWidth = 0.025f;
                    lineRenderer.positionCount = 16;
                    lineRenderer.useWorldSpace = true;
                    float bodyWidth = 0.6f;
                    float bodyHeight = 1.2f;
                    Vector3 base1 = vrrig.transform.position + new Vector3(-bodyWidth / 2, 0f, -bodyWidth / 2);
                    Vector3 base2 = vrrig.transform.position + new Vector3(bodyWidth / 2, 0f, -bodyWidth / 2);
                    Vector3 base3 = vrrig.transform.position + new Vector3(bodyWidth / 2, 0f, bodyWidth / 2);
                    Vector3 base4 = vrrig.transform.position + new Vector3(-bodyWidth / 2, 0f, bodyWidth / 2);
                    Vector3 top = vrrig.transform.position + new Vector3(0f, bodyHeight, 0f);
                    Vector3[] edges = {
         base1, base2, base2, base3, base3, base4, base4, base1,
         base1, top, base2, top, base3, top, base4, top
     };
                    lineRenderer.SetPositions(edges);
                    GameObject.Destroy(pyramidWireframe, Time.deltaTime);
                }
            }
        }
        public static void SnakeESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    UnityEngine.Color playerColor = vrrig.playerColor;
                    GameObject trailObject = new GameObject("PlayerTrail");
                    trailObject.transform.position = vrrig.transform.position;
                    trailObject.transform.SetParent(vrrig.transform);
                    TrailRenderer trailRenderer = trailObject.AddComponent<TrailRenderer>();
                    trailRenderer.material = new Material(Shader.Find("Unlit/Color"));
                    trailRenderer.material.color = new UnityEngine.Color(playerColor.r, playerColor.g, playerColor.b, 0.5f);
                    trailRenderer.time = 2f;
                    trailRenderer.startWidth = 0.2f;
                    trailRenderer.endWidth = 0f;
                    trailRenderer.startColor = playerColor;
                    trailRenderer.endColor = new UnityEngine.Color (playerColor.r, playerColor.g, playerColor.b, 0f);
                    trailRenderer.autodestruct = true;
                    GameObject.Destroy(trailObject, trailRenderer.time + 0.5f);
                }
            }
        }

        public static GameObject name;
        public static GameObject distance;
        public static int[] bones = new int[]
  {
            4,
            3,
            5,
            4,
            19,
            18,
            20,
            19,
            3,
            18,
            21,
            20,
            22,
            21,
            25,
            21,
            29,
            21,
            31,
            29,
            27,
            25,
            24,
            22,
            6,
            5,
            7,
            6,
            10,
            6,
            14,
            6,
            16,
            14,
            12,
            10,
            9,
            7
  };
        public static float l;
        public static bool fps;
    }
}

