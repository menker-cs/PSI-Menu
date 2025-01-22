using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Mods.Categories.Settings;
using Photon.Pun;
using Object = UnityEngine.Object;
using System.Linq;
using Photon.Realtime;
using UnityEngine.InputSystem.Controls;


namespace MenkerMenu.Mods.Categories
{
    public class Visuals1 
    {
        public static void CasualTracers()
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
        public static void InfectionTracers()
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

                    Line.material.shader = Shader.Find("GUI/Text Shader");
                    if (RigIsInfected(vrrig))
                    {
                        Line.startColor = Color.red;
                        Line.endColor = Color.red;
                    }
                    else
                    {
                        Line.startColor = Color.green;
                        Line.endColor = Color.green;
                    }

                    UnityEngine.Object.Destroy(line, Time.deltaTime);
                }
            }
        }
        public static void BoxESP(bool d)
        {
            if (d == false)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        GameObject ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        ESPBox.transform.position = vrrig.transform.position;
                        ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                        ESPBox.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                        GameObject.Destroy(ESPBox.GetComponent<BoxCollider>());
                        ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        ESPBox.GetComponent<Renderer>().material.color = vrrig.playerColor;
                        UnityEngine.Object.Destroy (ESPBox, Time.deltaTime);
                    }
                }
            }
            else if (d == true)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    GameObject ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ESPBox.transform.position = vrrig.transform.position;
                    ESPBox.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
                    GameObject.Destroy(ESPBox.GetComponent<BoxCollider>());
                    ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    ESPBox.GetComponent<Renderer>().material.color = vrrig.playerColor;
                    UnityEngine.Object.Destroy(ESPBox, Time.deltaTime);
                }
            }
        }
    }
}

