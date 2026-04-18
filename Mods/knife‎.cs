using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Void.Mods
{
    internal class knife‎
    {
        [Obsolete]
        public static void spawnKnife()
        {
            GameObject knife = GameObject.CreatePrimitive(PrimitiveType.Cube);
            knife.transform.position = GTPlayer.Instance.transform.position + GTPlayer.Instance.transform.forward * 2f;
            knife.transform.localScale = Vector3.one * 0.5f;
            Rigidbody rb = knife.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = GTPlayer.Instance.transform.forward * 10f;
            UnityEngine.Object.Destroy(knife, 5f);
        }
        public static void knifeInit()
        {
            global::System.Console.WriteLine("Knife mod initialized!");
        }
        public static void knifeTest()
        {
            global::System.Console.WriteLine("Knife test successful!");
        }

        internal static void Main()
        {
            throw new NotImplementedException();
        }
    }
}
