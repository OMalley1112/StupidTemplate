using BepInEx;
using GorillaLocomotion;
using Photon.Realtime;
using StupidTemplate.Classes;
using StupidTemplate.Menu;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using static StupidTemplate.Menu.Main;

namespace StupidTemplate.Mods
{
    public class Movement
    {
        public static void WASDFly()
        { 

            bool LeftArrow = UnityInput.GetKey(Key.LeftArrow);
            if ((bool)!Buttons.GetIndex("Disable Stationary WASD Fly").enabled || W || A || S || D || Space || Ctrl)
            {
                GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
            }

            if (!menu)
            {
                Transform parentTransform = GTPlayer.Instance.GetControllerTransform(false).parent;

                float turnSpeed = 250f;

                if (LeftArrow)
                    parentTransform.eulerAngles += new Vector3(0, -turnSpeed, 0) * Time.deltaTime;
                bool RightArrow = UnityInput.GetKey(Key.RightArrow);
                if (RightArrow)
                    parentTransform.eulerAngles += new Vector3(0, turnSpeed, 0) * Time.deltaTime;
                if (!(bool)UnityInput.GetKey(Key.UpArrow))
                {
                }
                else
                    parentTransform.eulerAngles += new Vector3(-turnSpeed, 0, 0) * Time.deltaTime;
                if ((bool)UnityInput.GetKey(Key.DownArrow))
                    parentTransform.eulerAngles += new Vector3(turnSpeed, 0, 0) * Time.deltaTime;

                if (Mouse.current.rightButton.isPressed)
                {
                    Quaternion currentRotation = parentTransform.rotation;
                    Vector3 euler = currentRotation.eulerAngles;

                    if (startX < 0)
                    {
                        startX = (int)euler.y;
                        subThingy = Mouse.current.position.value.x / Screen.width;
                    }
                    if (startY < 0)
                    {
                        startY = (int)euler.x;
                        subThingyZ = Mouse.current.position.value.y / Screen.height;
                    }

                    float newX = startY - (Mouse.current.position.value.y / Screen.height - subThingyZ) * 360 * 1.33f;
                    float newY = startX + (Mouse.current.position.value.x / Screen.width - subThingy) * 360 * 1.33f;

                    newX = newX > 180f ? newX - 360f : newX;
                    newX = Mathf.Clamp(newX, -90f, 90f);

                    parentTransform.rotation = Quaternion.Euler(newX, newY, euler.z);
                }
                else
                {
                    startX = -1;
                    startY = -1;
                }

                float speed = FlySpeed;
                if (Shift)
                    speed *= 2f;
                else if (Alt)
                    speed /= 2;

                if (W)
                    GorillaTagger.Instance.rigidbody.transform.position += GTPlayer.Instance.GetControllerTransform(false).parent.forward * (Time.deltaTime * speed);

                if (S)
                    GorillaTagger.Instance.rigidbody.transform.position += GTPlayer.Instance.GetControllerTransform(false).parent.forward * (Time.deltaTime * -speed);

                if (A)
                    GorillaTagger.Instance.rigidbody.transform.position += GTPlayer.Instance.GetControllerTransform(false).parent.right * (Time.deltaTime * -speed);

                if (D)
                    GorillaTagger.Instance.rigidbody.transform.position += GTPlayer.Instance.GetControllerTransform(false).parent.right * (Time.deltaTime * speed);

                if (Space)
                    GorillaTagger.Instance.rigidbody.transform.position += new Vector3(0f, Time.deltaTime * speed, 0f);

                if (Ctrl)
                    GorillaTagger.Instance.rigidbody.transform.position += new Vector3(0f, Time.deltaTime * -speed, 0f);

                VRRig.LocalRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;
            }

            if (!W && !A && !S && !D && !Space && !Ctrl && lastPosition != Vector3.zero && (bool)!Buttons.GetIndex("Disable Stationary WASD Fly").enabled)
                GorillaTagger.Instance.rigidbody.transform.position = lastPosition;
            else
                lastPosition = GorillaTagger.Instance.rigidbody.transform.position;
        }
        public static void Noclip()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.8f)
            {
                MeshCollider[] colliders = UnityEngine.Object.FindObjectsOfType<MeshCollider>();
                foreach (MeshCollider collider in colliders)
                {
                    collider.enabled = false;
                }
            }
            else
            {
                MeshCollider[] colliders = UnityEngine.Object.FindObjectsOfType<MeshCollider>();
                foreach (MeshCollider collider in colliders)
                {
                    collider.enabled = true;
                }
            }
        }

        public static void AdminFly()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GTPlayer.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * 10f;
                GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
            }
        }
        public static void AdminTracers()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GameObject tracer = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tracer.transform.position = GTPlayer.Instance.transform.position + GTPlayer.Instance.transform.forward * 2f;
                tracer.transform.localScale = Vector3.one * 0.1f;
                ColorChanger colorChanger = tracer.AddComponent<ColorChanger>();
                colorChanger.colors = StupidTemplate.Settings.backgroundColor;
                Object.Destroy(tracer, 0.5f);
            }
        }
        public static void CasualTracers()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GameObject tracer = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tracer.transform.position = GTPlayer.Instance.transform.position + GTPlayer.Instance.transform.forward * 2f;
                tracer.transform.localScale = Vector3.one * 0.1f;
                ColorChanger colorChanger = tracer.AddComponent<ColorChanger>();
                colorChanger.colors = StupidTemplate.Settings.backgroundColor;
                Object.Destroy(tracer, 0.5f);
            }
        }
        public static void InfiniteJump()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GTPlayer.Instance.transform.position += Vector3.up * Time.deltaTime * 10f;
                GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
            }
        }
        public static void AdminLazer()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GTPlayer.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * 10f;
                GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
            }
        }
        public static void mnintravis()
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                GTPlayer.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * 10f;
                GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
            }
        }
        public static void Barkfly()
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                GTPlayer.Instance.transform.position += Vector3.up * Time.deltaTime * 10f;
                GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
            }
        }
        public static void wasdfly()
        {
            Vector3 direction = Vector3.zero;
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
                direction += GTPlayer.Instance.transform.forward;
            if (ControllerInputPoller.instance.leftControllerSecondaryButton)
                direction -= GTPlayer.Instance.transform.forward;
            if (ControllerInputPoller.instance.leftControllerPrimary2DAxis.x > 0.5f)
                direction += GTPlayer.Instance.transform.right;
            if (ControllerInputPoller.instance.leftControllerPrimary2DAxis.x < -0.5f)
                direction -= GTPlayer.Instance.transform.right;
            GTPlayer.Instance.transform.position += direction.normalized * Time.deltaTime * Settings.Movement.flySpeed;
            GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
        }
       
        public static void spawnCheezburger()
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                GameObject cheezburger = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cheezburger.transform.position = GTPlayer.Instance.transform.position + GTPlayer.Instance.transform.forward * 2f;
                cheezburger.transform.localScale = Vector3.one * 0.5f;
                ColorChanger colorChanger = cheezburger.AddComponent<ColorChanger>();
                colorChanger.colors = StupidTemplate.Settings.backgroundColor;
                Object.Destroy(cheezburger, 5f);
            }
        }
        public static void Fly()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GTPlayer.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * Settings.Movement.flySpeed;
                GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
            }
        }

        public static GameObject platl;
        public static GameObject platr;

        public static void Platforms()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (platl == null)
                {
                    platl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    platl.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    platl.transform.position = TrueLeftHand().position;
                    platl.transform.rotation = TrueLeftHand().rotation;

                    FixStickyColliders(platl);

                    ColorChanger colorChanger = platl.AddComponent<ColorChanger>();
                    colorChanger.colors = StupidTemplate.Settings.backgroundColor;
                }
                else
                {
                    if (platl != null)
                    {
                        Object.Destroy(platl);
                        platl = null;
                    }
                }
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                if (platr == null)
                {
                    platr = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    platr.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    platr.transform.position = TrueRightHand().position;
                    platr.transform.rotation = TrueRightHand().rotation;

                    FixStickyColliders(platr);

                    ColorChanger colorChanger = platr.AddComponent<ColorChanger>();
                    colorChanger.colors = StupidTemplate.Settings.backgroundColor;
                }
                else
                {
                    if (platr != null)
                    {
                        Object.Destroy(platr);
                        platr = null;
                    }
                }
            }
        }

        public static bool previousTeleportTrigger;
        private static Vector3 lastPosition;
        private static int startX;
        private static int startY;
        private static float subThingyZ;
        private static float subThingy;

        public static float FlySpeed { get; private set; }

        public static void TeleportGun()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                var GunData = RenderGun();
                GameObject NewPointer = GunData.NewPointer;

                if (ControllerInputPoller.TriggerFloat(XRNode.RightHand) > 0.5f && !previousTeleportTrigger)
                {
                    GTPlayer.Instance.TeleportTo(NewPointer.transform.position + Vector3.up, GTPlayer.Instance.transform.rotation);
                    GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
                }

                previousTeleportTrigger = ControllerInputPoller.TriggerFloat(XRNode.RightHand) > 0.5f;
            }
        }
    }
}
