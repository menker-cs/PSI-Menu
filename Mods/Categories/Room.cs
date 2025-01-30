using GorillaNetworking;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using static MenkerMenu.Utilities.NotificationLib;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Menu.Main;
using MenkerMenu.Utilities;
using System.Diagnostics;
using Valve.VR;
using Cinemachine;
using HarmonyLib;
using System.Reflection;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using UnityEngine.XR;
using Photon.Realtime;
using ExitGames.Client.Photon;
using BepInEx;

namespace MenkerMenu.Mods.Categories
{
    public class Room : MonoBehaviourPunCallbacks
    {
        public static string roomCode;

        public static void QuitGTAG()
        {
            Application.Quit();
        }

        public static void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        public static void JoinRandomPublic()
        {
            if (PhotonNetwork.InRoom)
            {
                UnityEngine.Debug.LogWarning("<color=blue>Photon</color> : Already connected to a room.");
                NotificationLib.SendNotification("<color=blue>Photon</color> : Already connected to a room.");
                return;
            }

            string currentMap = DetectCurrentMap();
            if (currentMap == null)
            {
                UnityEngine.Debug.LogError("<color=blue>Photon</color> : Unable to detect the current map.");
                NotificationLib.SendNotification("<color=blue>Photon</color> : Unable to detect the current map.");
                return;
            }

            string path = GetPathForGameMode(currentMap);
            if (path == null)
            {
                UnityEngine.Debug.LogError($"<color=blue>Photon</color> : No valid path found for map: {currentMap}.");
                NotificationLib.SendNotification($"<color=blue>Photon</color> : No valid path found for map: {currentMap}.");
                return;
            }

            GorillaNetworkJoinTrigger joinTrigger = GameObject.Find(path)?.GetComponent<GorillaNetworkJoinTrigger>();
            if (joinTrigger == null)
            {
                UnityEngine.Debug.LogError($"<color=blue>Photon</color> : Join trigger not found for path: {path}.");
                NotificationLib.SendNotification($"<color=blue>Photon</color> : Join trigger not found for path: {path}.");
                return;
            }

            PhotonNetworkController.Instance.AttemptToJoinPublicRoom(joinTrigger, JoinType.Solo);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            if (returnCode == ErrorCode.GameFull)
            {
                UnityEngine.Debug.LogWarning($"OnJoinRoomFailed : Failed to join room '{roomCode}'. Reason: Is Full.");
                NotificationLib.SendNotification($"<color=red>Error</color> : Failed to join room '{roomCode}'. Reason: Is Full.");
            }
            else
            {
                UnityEngine.Debug.LogWarning($"OnJoinRoomFailed: Failed to join room '{roomCode}'. Reason: {message}.");
                NotificationLib.SendNotification($"<color=red>Error</color>: Failed to join room '{roomCode}'. Reason: {message}.");
            }
        }
        public static void JoinRoom(string RoomCode)
        {
            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(RoomCode, JoinType.Solo);
        }
        public static void PrimaryDisconnect()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton | UnityInput.Current.GetKey(KeyCode.F))
            {
                PhotonNetwork.Disconnect();
            }
        }
        public static void DisableNetworkTriggers()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(false);
        }
        public static void EnableNetworkTriggers()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(true);
        }
        public static void ReportAll()
        {
            GorillaPlayerScoreboardLine[] Board = UnityEngine.Object.FindObjectsOfType<GorillaPlayerScoreboardLine>();
            foreach (GorillaPlayerScoreboardLine Report in Board)
            {
                if (Report.linePlayer != null)
                {
                    Report.PressButton(true, GorillaPlayerLineButton.ButtonType.HateSpeech);
                }
            }
        }
        public static void MuteAll()
        {
            GorillaPlayerScoreboardLine[] Board = UnityEngine.Object.FindObjectsOfType<GorillaPlayerScoreboardLine>();
            foreach (GorillaPlayerScoreboardLine Mute in Board)
            {
                if (Mute.linePlayer != null)
                {
                    Mute.PressButton(true, GorillaPlayerLineButton.ButtonType.Mute);
                    Mute.muteButton.isOn = true;
                    Mute.muteButton.UpdateColor();
                }
            }
        }
        public static void UnMuteAll()
        {
            GorillaPlayerScoreboardLine[] Board = UnityEngine.Object.FindObjectsOfType<GorillaPlayerScoreboardLine>();
            foreach (GorillaPlayerScoreboardLine Mute in Board)
            {
                if (Mute.linePlayer != null)
                {
                    Mute.PressButton(false, GorillaPlayerLineButton.ButtonType.Mute);
                    Mute.muteButton.isOn = false;
                    Mute.muteButton.UpdateColor();
                }
            }
        }
        public static void SetMode(string GameMode)
        {
            // Ghost, Paintbrawl, Ambush, Hunt

            foreach (WatchableStringSO stringSO in Resources.FindObjectsOfTypeAll<WatchableStringSO>())
            {
                stringSO.Value = GameMode.ToUpper();
                stringSO.InitialValue = GameMode.ToUpper();
            }
        }
    }
}