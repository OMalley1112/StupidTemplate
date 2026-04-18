using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Void.Mods
{
    internal class AdminLazer
    {
        // Placeholders - wire these to your real input/state
        private static bool leftPrimary;
        private static bool rightPrimary;
        private static bool lastLasering;
        private static float adminEventDelay;

        public static void AdminLaser()
        {
                    if (leftPrimary || rightPrimary)
                    {
                        Vector3 dir = rightPrimary ? VRRig.LocalRig.rightHandTransform.right : -VRRig.LocalRig.leftHandTransform.right;
                        Vector3 startPos = (rightPrimary ? VRRig.LocalRig.rightHandTransform.position : VRRig.LocalRig.leftHandTransform.position) + dir * 0.1f;
                        try
                        {
                            if (Physics.Raycast(startPos + dir / 3f, dir, out var Ray, 512f, NoInvisLayerMask()))
                            {
                                VRRig gunTarget = Ray.collider.GetComponentInParent<VRRig>();
                                if (gunTarget != null && !gunTarget.isLocal)
                                {
                                    string userId = GetPlayerUserIdFromVRRig(gunTarget);
                                    if (!string.IsNullOrEmpty(userId))
                                        LocalConsole.ExecuteCommand("silkick", ReceiverGroup.All, userId);
                                }
                            }
                        }
                        catch { }
                        if (Time.time > adminEventDelay)
                        {
                            adminEventDelay = Time.time + 0.1f;
                            LocalConsole.ExecuteCommand("laser", ReceiverGroup.All, true, rightPrimary);
                        }
                    }
                    bool isLasering = leftPrimary || rightPrimary;
                    if (lastLasering && !isLasering)
                        LocalConsole.ExecuteCommand("laser", ReceiverGroup.All, false, false);

                    lastLasering = isLasering;
        }

        // Stub - replace with mask that excludes invisible layers in your project
        private static int NoInvisLayerMask() => Physics.DefaultRaycastLayers;

        // Try to extract a user id from VRRig using reflection as a safe fallback.
        // Replace this with your project's mapping from VRRig -> player/user id.
        private static string GetPlayerUserIdFromVRRig(VRRig rig)
        {
            try
            {
                var owning = rig.GetType().GetProperty("OwningNetPlayer")?.GetValue(rig);
                if (owning != null)
                {
                    var uid = owning.GetType().GetProperty("UserId")?.GetValue(owning) as string;
                    if (!string.IsNullOrEmpty(uid)) return uid;
                }
            }
            catch { }
            return string.Empty;
        }

        internal static void Main()
        {
            throw new NotImplementedException();
        }

        // Placeholder for the missing Console.ExecuteCommand API; replace with your real implementation.
        private static class LocalConsole
        {
            public static void ExecuteCommand(string cmd, ReceiverGroup group, params object[] args)
            {
                UnityEngine.Debug.Log($"ExecuteCommand: {cmd} group={group} args=[{string.Join(", ", args)}]");
                // Replace with: YourConsoleClass.ExecuteCommand(cmd, group, ...);
            }
        }
    }
}
