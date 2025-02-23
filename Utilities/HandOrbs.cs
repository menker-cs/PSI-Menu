using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static MenkerMenu.Utilities.ColorLib;

namespace MenkerMenu.Utilities
{
    internal class HandOrbs
    {
        #region handorbs

        public static void HandOrbs1()
        {
            Material colorMaterial = new Material(Shader.Find("GUI/Text Shader"))
            {
                color = Color.Lerp(ColorLib.SkyBlue, new Color32(8, 90, 177, byte.MaxValue), Mathf.PingPong(Time.time, 1.5f))
            };
            colorMaterial.SetFloat("_Mode", 2f);

            LOrb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            UnityEngine.Object.Destroy(LOrb.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(LOrb.GetComponent<SphereCollider>());
            LOrb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            LOrb.transform.position = GorillaTagger.Instance.leftHandTransform.position;

            ROrb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            UnityEngine.Object.Destroy(ROrb.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(ROrb.GetComponent<SphereCollider>());
            ROrb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            ROrb.transform.position = GorillaTagger.Instance.rightHandTransform.position;

            LOrb.GetComponent<Renderer>().material.color = colorMaterial.color;
            ROrb.GetComponent<Renderer>().material.color = colorMaterial.color;

            UnityEngine.Object.Destroy(LOrb, Time.deltaTime);
            UnityEngine.Object.Destroy(ROrb, Time.deltaTime);
        }
        #endregion
        public static GameObject LOrb;
        public static GameObject ROrb;
    }
}
