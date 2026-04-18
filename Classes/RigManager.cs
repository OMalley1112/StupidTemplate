using System.Collections.Generic;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace StupidTemplate.Classes
{
    public class RigManager
    {
        public static VRRig GetVRRigFromPlayer(Player p) =>
            GorillaGameManager.instance.FindPlayerVRRig(p);

        public static VRRig GetRandomVRRig(bool includeSelf)
        {
            var rigs = UnityEngine.Object.FindObjectsByType<VRRig>(FindObjectsSortMode.None);
            if (rigs == null || rigs.Length == 0) return null;

            if (includeSelf)
                return rigs[Random.Range(0, rigs.Length)];

            // exclude local rig
            var candidates = new List<VRRig>();
            foreach (var r in rigs)
                if (r != VRRig.LocalRig)
                    candidates.Add(r);

            if (candidates.Count == 0) return null;
            return candidates[Random.Range(0, candidates.Count)];
        }

        public static VRRig GetClosestVRRig()
        {
            var rigs = UnityEngine.Object.FindObjectsByType<VRRig>(FindObjectsSortMode.None);
            if (rigs == null || rigs.Length == 0) return null;

            float minDist = float.MaxValue;
            VRRig outRig = null;
            var bodyPos = GorillaTagger.Instance.bodyCollider.transform.position;

            foreach (var vrrig in rigs)
            {
                float d = Vector3.Distance(bodyPos, vrrig.transform.position);
                if (d < minDist)
                {
                    minDist = d;
                    outRig = vrrig;
                }
            }

            return outRig;
        }

        public static PhotonView GetPhotonViewFromVRRig(VRRig p) =>
            (PhotonView)Traverse.Create(p).Field("photonView").GetValue();

        public static Player GetRandomPlayer(bool includeSelf)
        {
            if (includeSelf)
                return PhotonNetwork.PlayerList[Random.Range(0, PhotonNetwork.PlayerList.Length - 1)];
            else
                return PhotonNetwork.PlayerListOthers[Random.Range(0, PhotonNetwork.PlayerListOthers.Length - 1)];
        }

        public static Player GetPlayerFromVRRig(VRRig p) =>
            GetPhotonViewFromVRRig(p).Owner;

        public static Player GetPlayerFromID(string id)
        {
            Player found = null;
            foreach (Player target in PhotonNetwork.PlayerList)
            {
                if (target.UserId == id)
                {
                    found = target;
                    break;
                }
            }
            return found;
        }

        public static Color GetPlayerColor(VRRig Player)
        {
            if (Player.bodyRenderer.cosmeticBodyType == GorillaBodyType.Skeleton)
                return Color.green;

            switch (Player.setMatIndex)
            {
                case 1:
                    return Color.red;
                case 2:
                case 11:
                    return new Color32(255, 128, 0, 255);
                case 3:
                case 7:
                    return Color.blue;
                case 12:
                    return Color.green;
                default:
                    return Player.playerColor;
            }
        }
    }
}