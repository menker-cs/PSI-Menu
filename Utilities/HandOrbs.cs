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

            LOrb.GetComponent<Renderer>().material.color = RoyalBlueTransparent;
            ROrb.GetComponent<Renderer>().material.color = RoyalBlueTransparent;

            UnityEngine.Object.Destroy(LOrb, Time.deltaTime);
            UnityEngine.Object.Destroy(ROrb, Time.deltaTime);
        }
        #endregion
        public static GameObject LOrb;
        public static GameObject ROrb;
    }
}
