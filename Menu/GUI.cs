using System.Buffers.Text;
using System.Collections.Generic;
using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using MenkerMenu.Utilities;
using static MenkerMenu.Initialization.PluginInfo;
using static MenkerMenu.Utilities.Variables;
using MenkerMenu.Menu;
using UnityEngine;
using MenkerMenu.Mods;

namespace MenkerMenu.Menu
{
    [BepInPlugin("Psi.GUI", "GayrillaTag", "1.0")]
    public class UI : BaseUnityPlugin
    {
        private Rect windowRect = new Rect(10, 10, 700, 500);
        static bool showGUI = true;
        private Vector2 scrollPosition = Vector2.zero;
        private int selectedTab = 0;
        //private string roomCode = "";
        private Color theme = new Color32(0, 0, 0, 255);
        private bool wasdthing = false;
        public static Vector2 scrollpos1 = Vector2.zero;
        public static Vector2 scrollpos2 = Vector2.zero;
        private float moveSpeed = 5f;
        private float shiftMultiplier = 2.5f;
        private float verticalSpeed = 5f;
        private float arrowKeyTurnSpeed = 50f;
        private string searchQuery = "";
        private float buttonCooldown = 0.1f;
        private float lastButtonPressTime = 0f;
        private Texture2D buttonTexture;
        private Texture2D buttonTextureActive;
        private Texture2D tabTexture;
        private Texture2D tabTextureActive;
        private string userInput = "";

        public static void ToggleGUI(bool yes)
        {
            showGUI = yes;
        }
        void Start()
        {
            //regular buttons
            buttonTexture = CreateTexture(new Color32(25, 25, 25, 255));
            buttonTextureActive = CreateTexture(new Color32(65, 65, 65, 255));

            //tab buttons
            tabTexture = CreateTexture(new Color32(50, 50, 50, 255));
            tabTextureActive = CreateTexture(new Color32(255, 0, 255, 255));
        }

        void Update()
        {
            if (UnityInput.Current.GetKeyDown(KeyCode.RightShift) && Time.time >= lastButtonPressTime + buttonCooldown)
            {
                //showGUI = !showGUI;
                lastButtonPressTime = Time.time;
            }
        }

        void OnGUI()
        {
            GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 24,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.white }
            };

            GUI.Label(new Rect(10, 10, 300, 50), $"<color=#FF0000>P</color><color=#FF7F00>S</color><color=#FFFF00>I </color><color=#00FF00>O</color><color=#0000FF>N </color><color=#4B0082>T</color><color=#8B00FF>O</color><color=#FF0000>P</color> {menuVersion}", titleStyle);

            if (showGUI)
            {
                int fps = (Time.deltaTime > 0) ? Mathf.RoundToInt(1.0f / Time.deltaTime) : 0;

                GUI.backgroundColor = theme;
                windowRect = GUI.Window(0, windowRect, DrawMainWindow, "");
            }

            GUIStyle roomStatusStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter,
                richText = true
            };
            float baseY = Screen.height - 30;

            string roomStatus;
            if (PhotonNetwork.InRoom)
            {
                roomStatus = $"<color=green>In Room: {PhotonNetwork.CurrentRoom.Name}</color>";
            }
            else
            {
                roomStatus = "<color=red>Not Connected To A Room</color>";
            }
            Rect roomStatusRect = new Rect(Screen.width / 2 - 150, baseY, 300, 30);
            GUI.Label(roomStatusRect, roomStatus, roomStatusStyle);
        }

        private void DrawMainWindow(int windowID)
        {
            GUIStyle guiTitleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.white }
            };
            fps = (Time.deltaTime > 0) ? Mathf.RoundToInt(1.0f / Time.deltaTime) : 0;

            GUILayout.BeginHorizontal();
            GUILayout.Label($"<color=#FF0000>P</color><color=#FF7F00>S</color><color=#FFFF00>I </color><color=#00FF00>O</color><color=#0000FF>N </color><color=#4B0082>T</color><color=#8B00FF>O</color><color=#FF0000>P</color> {menuVersion} [{fps}]", guiTitleStyle);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            DrawTabs();

            GUILayout.Space(5);
            switch (selectedTab)
            {
                case 0: DrawRoomTab(); break;
                case 1: DrawSelfTab(); break;
                case 2: DrawPlayerListTab(); break;
                case 3: DrawMiscTab(); break;
                case 4: DrawMenuButtonsTab(); break;
            }

            GUI.DragWindow();
        }
        private void DrawTabs()
        {
            GUILayout.BeginHorizontal();

            string[] tabNames = { "Room", "Self", "Player List", "Console", "Menu Buttons" };
            for (int i = 0; i < tabNames.Length; i++)
            {
                if (GUILayout.Button(tabNames[i], GetTabStyle(i == selectedTab)))
                {
                    selectedTab = i;
                }
            }

            GUILayout.EndHorizontal();
        }
        private void DrawRoomTab()
        {
            GUILayout.Label("Room", GUI.skin.label);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Join Room", GetButtonStyle(false)))
            {
                if (!string.IsNullOrEmpty(userInput))
                {
                    PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(userInput, JoinType.Solo);
                }
            }
            if (GUILayout.Button("Set Name", GetButtonStyle(false)))
            {
                if (!string.IsNullOrEmpty(userInput))
                {
                    PhotonNetwork.NickName = userInput;
                }
            }
            if (GUILayout.Button("Disconnect", GetButtonStyle(false)))
            {
                PhotonNetwork.Disconnect();
            }
            GUILayout.EndHorizontal();
            userInput = GUILayout.TextField(userInput, 25);
            string serverIP = PhotonNetwork.ServerAddress;
            int ping = PhotonNetwork.GetPing();
            int playerCount = PhotonNetwork.CountOfPlayers;
            int roomCount = PhotonNetwork.CountOfRooms;
            string gameVersion = PhotonNetwork.AppVersion;
            bool isMasterClient = PhotonNetwork.IsMasterClient;

            GUILayout.Label($"Server IP: {serverIP}");
            GUILayout.Label($"Ping: {ping}");
            GUILayout.Label($"Player Count: {playerCount}");
            GUILayout.Label($"Room Count: {roomCount}");
            GUILayout.Label($"Game Version: {gameVersion}");
            GUILayout.Label($"Master Client: {isMasterClient}");
        }

        private void DrawSelfTab()
        {
            GUILayout.Label("Self", GUI.skin.label);


            wasdthing = GUILayout.Toggle(wasdthing, "WASD");
            if (wasdthing)
                wasdarrow();

            GUILayout.Label("Movement Speed");
            moveSpeed = GUILayout.HorizontalSlider(moveSpeed, 1f, 20f);

            GUILayout.Label("Shift Speed");
            shiftMultiplier = GUILayout.HorizontalSlider(shiftMultiplier, 1f, 5f);

            GUILayout.Label("Vertical Speed");
            verticalSpeed = GUILayout.HorizontalSlider(verticalSpeed, 1f, 20f);

            GUILayout.Label("Turning Speed");
            arrowKeyTurnSpeed = GUILayout.HorizontalSlider(arrowKeyTurnSpeed, 10f, 200f);
        }

        private void DrawPlayerListTab()
        {
            GUILayout.Label("Player List", GUI.skin.label);

            Player[] players = PhotonNetwork.PlayerList;

            if (players.Length == 0)
            {
                GUILayout.Label("Not in a room", GUI.skin.label);
                return;
            }

            GUIStyle smallTextStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 24
            };

            foreach (Player player in players)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label($"Nickname: {player.NickName}   ID: {player.UserId}", smallTextStyle);

                GUILayout.EndHorizontal();
                GUILayout.Space(10);
            }
        }

        private List<string> logMessages = new List<string>();

        private bool autoScroll = true;

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            string logEntry = $"[{type}] {logString}";

            if (type == LogType.Error || type == LogType.Exception)
            {
                logEntry += $"\nStackTrace:\n{stackTrace}";
            }

            logMessages.Add(logEntry);
            if (logMessages.Count > 100)
            {
                logMessages.RemoveAt(0);
            }
            float contentHeight = logMessages.Count * 20f;
            float viewHeight = 400f;
            float maxScroll = contentHeight - viewHeight;

            if (scrollPosition.y >= maxScroll - 50f)
            {
                autoScroll = true;
            }

            if (autoScroll)
            {
                scrollPosition.y = maxScroll;
            }
        }

        private void DrawMiscTab()
        {
            GUILayout.Label("Console Log", GUI.skin.label);

            float consoleHeight = Mathf.Min(Screen.height * 0.5f, 350f);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(consoleHeight));

            foreach (string log in logMessages)
            {
                GUILayout.Label(log, GUI.skin.label);
            }

            GUILayout.EndScrollView();

            if (Event.current.type == EventType.Repaint)
            {
                float contentHeight = logMessages.Count * 20f;
                float viewHeight = consoleHeight;
                float maxScroll = contentHeight - viewHeight;

                if (scrollPosition.y < maxScroll - 50f)
                {
                    autoScroll = false;
                }
            }
        }


        private GUIStyle GetButtonStyle(bool isActive)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.background = isActive ? buttonTextureActive : buttonTexture;
            style.active.background = buttonTextureActive;
            style.hover.background = buttonTextureActive;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            return style;
        }

        private GUIStyle GetTabStyle(bool isActive)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.background = isActive ? tabTextureActive : tabTexture;
            style.active.background = tabTextureActive;
            style.hover.background = tabTextureActive;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            return style;
        }

        private void DrawMenuButtonsTab()
        {
            scrollpos1 = GUILayout.BeginScrollView(scrollpos1, GUILayout.Width(windowRect.width - 30), GUILayout.Height(windowRect.height - 40));

            foreach (ButtonHandler.Button info in ModButtons.buttons)
            {
                // foreach (ButtonHandler.Button info in btninfo)
                //  {
                if (string.IsNullOrEmpty(searchQuery) || info.buttonText.ToLower().Contains(searchQuery.ToLower()))
                {
                    GUILayout.BeginHorizontal();

                    bool isActive = info.Enabled;

                    if (GUILayout.Button(new GUIContent(info.buttonText), GetButtonStyle(isActive)))
                    {
                        GUI.backgroundColor = Color.green;
                        info.Enabled = !info.Enabled;
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, false, 0.25f);
                    }

                    GUILayout.EndHorizontal();
                }
                //   }
            }

            GUILayout.EndScrollView();
        }

        public void wasdarrow()
        {
            float currentSpeed = moveSpeed;
            Transform bodyTransform = Camera.main.transform;

            GorillaTagger.Instance.rigidbody.useGravity = false;
            GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;

            if (UnityInput.Current.GetKey(KeyCode.LeftShift))
            {
                currentSpeed *= shiftMultiplier;
            }

            if (UnityInput.Current.GetKey(KeyCode.W))
            {
                bodyTransform.position += bodyTransform.forward * currentSpeed * Time.deltaTime;
            }
            if (UnityInput.Current.GetKey(KeyCode.A))
            {
                bodyTransform.Rotate(0, -arrowKeyTurnSpeed * Time.deltaTime, 0);
            }
            if (UnityInput.Current.GetKey(KeyCode.S))
            {
                bodyTransform.position += -bodyTransform.forward * currentSpeed * Time.deltaTime;
            }
            if (UnityInput.Current.GetKey(KeyCode.D))
            {
                bodyTransform.Rotate(0, arrowKeyTurnSpeed * Time.deltaTime, 0);
            }
        }

        private Texture2D CreateTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }
    }
}