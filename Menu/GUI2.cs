/*
using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.CompilerServices;
using MenkerMenu.Menu;
using MenkerMenu.Mods;

namespace StupidTemplate.Menu
{
    [BepInPlugin("com.psi.gorillatag.gui", "PSI GUI", "1.1")]
    public class UI : BaseUnityPlugin
    {
        public static int selectedTab;
        public static Rect windowRect = new Rect(10, 10, 500, 450);
        public static bool showGUI = true;
        private static string roomName = "";
        private static bool wasdMode;
        public static Vector2 scrollPos = Vector2.zero;
        private static Color theme = Color.red;
        private static Color accentColor = new Color32(148, 6, 251, 255); // Purple
        private string searchQuery = "";
        private string room = "";

        private static List<ButtonHandler.Button> filteredButtons = new List<ButtonHandler.Button>();

        private List<Star> stars = new List<Star>();
        private float starSpeed = 0.1f;
        public static Vector2 scrollpos = Vector2.zero;


        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            room = GUILayout.TextField(room, GUILayout.Width(125), GUILayout.Height(20));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Join Specific Room", GUILayout.Height(20), GUILayout.Width(125)))
            {
                PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(room, JoinType.Solo);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Name", GUILayout.Height(20), GUILayout.Width(125)))
            {
                PhotonNetwork.LocalPlayer.NickName = room;
                PhotonNetwork.NickName = room;
                PhotonNetwork.NetworkingClient.NickName = room;
                GorillaComputer.instance.currentName = room;
                GorillaComputer.instance.savedName = room;
                GorillaComputer.instance.offlineVRRigNametagText.text = room;
                GorillaLocomotion.Player.Instance.name = room;
                NetworkSystem.Instance.name = room;
                NetworkSystem.Instance.SetMyNickName(room);
                PlayerPrefs.SetString("playerName", room);
                PlayerPrefs.Save();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Toggle Gui", GUILayout.Height(20), GUILayout.Width(125)))
            {
                GuiOn = !GuiOn;
            }
            GUILayout.EndHorizontal();

            if (GuiOn)
            {
                DrawStarryBackgroundInWindow(windowRect);
                MoveStars();
                GUI.backgroundColor = Color.grey;
                windowRect = GUI.Window(0, windowRect, Mods, $"                                                                                                                                                            N\n                                                                                                                                                            O\n                                                                                                                                                            V\n                                                                                                                                                            A\n                                                                                                                                                            \n                                                                                                                                                            ");
            }

        }
        public static bool GuiOn = false;
        public static string searchthing = "";
        private void Mods(int windowID)
        {
            GUILayout.BeginVertical(GUI.skin.box);
            GUI.backgroundColor = Color.grey;
            if (GUILayout.Button("Disconnect"))
            {
                PhotonNetwork.Disconnect();
            }
            GUILayout.Label("Search:", GUI.skin.label);
            searchQuery = GUILayout.TextField(searchQuery, GUILayout.Width(windowRect.width - 30));
            scrollpos = GUILayout.BeginScrollView(scrollpos, GUILayout.Width(windowRect.width - 30), GUILayout.Height(windowRect.height - 40));

                foreach(ButtonHandler.Button info in ModButtons.buttons)
                {
                    if (string.IsNullOrEmpty(searchQuery) || info.buttonText.ToLower().Contains(searchQuery.ToLower()))
                    {
                        GUILayout.BeginHorizontal();

                        if (GUILayout.Button(new GUIContent(info.buttonText), GetButtonStyle(info)))
                        {
                            GUI.backgroundColor = Color.grey;
                            if (info.isToggle)
                            {
                                info.Enabled = !info.Enabled;
                            }
                            else
                            {
                                if (info.onEnable != null)
                                {
                                    try { info.onEnable.Invoke(); } catch { }
                                }
                            }
                            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, false, 0.25f);
                        }

                        GUILayout.EndHorizontal();
                    }
                }
            GUILayout.EndScrollView();
            GUI.DragWindow();
        }
        private void DrawStarryBackgroundInWindow(Rect windowRect)
        {
            GUI.color = Color.red;//new Color32(72, 63, 63, 255);
            GUI.Box(new Rect(windowRect.xMin - 2, windowRect.yMin - 2, windowRect.width + 4, windowRect.height + 4), "");

            GUI.backgroundColor = Color.black;
            GUI.Box(windowRect, "");

            if (stars.Count == 0)
            {
                GenerateStars(windowRect);
            }

            foreach (var star in stars)
            {
                GUI.color = Color.white;
                GUI.DrawTexture(new Rect(star.x, star.y, star.size, star.size), Texture2D.whiteTexture);
            }
        }

        private void GenerateStars(Rect windowRect)
        {
            int starCount = 100;
            for (int i = 0; i < starCount; i++)
            {
                float x = UnityEngine.Random.Range(windowRect.xMin, windowRect.xMax);
                float y = UnityEngine.Random.Range(windowRect.yMin, windowRect.yMax);
                float size = UnityEngine.Random.Range(1f, 3f);

                Vector2 direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;

                stars.Add(new Star(x, y, size, direction));
            }
        }

        private void MoveStars()
        {
            foreach (var star in stars)
            {
                star.x += star.direction.x * starSpeed;
                star.y += star.direction.y * starSpeed;

                if (star.x < windowRect.xMin || star.x > windowRect.xMax || star.y < windowRect.yMin || star.y > windowRect.yMax)
                {
                    star.x = UnityEngine.Random.Range(windowRect.xMin, windowRect.xMax);
                    star.y = UnityEngine.Random.Range(windowRect.yMin, windowRect.yMax);
                }
            }
        }
        private GUIStyle GetButtonStyle(ButtonHandler.Button module)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = (bool)module.Enabled ? theme : Color.white;
            style.active.textColor = (bool)module.Enabled ? theme : Color.red;
            style.focused.textColor = (bool)module.Enabled ? theme : Color.white;
            style.hover.textColor = (bool)module.Enabled ? theme : Color.white;
            return style;
        }
    }

    public class Star
    {
        public float x;
        public float y;
        public float size;
        public Vector2 direction;

        public Star(float x, float y, float size, Vector2 direction)
        {
            this.x = x;
            this.y = y;
            this.size = size;
            this.direction = direction;
        }
    }
}
*/