using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Void.Mods
{
    public class GTPlayer : MonoBehaviour
    {
        public static GTPlayer Instance { get; private set; }

          void Awake()
          {
              Instance = this;
          }
      }
  
    internal class wasdfly
    {
        public static void wasdflyTest()
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                GTPlayer.Instance.transform.position += Vector3.up * Time.deltaTime * 10f;
                GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
            }
        }
    }
}
