using System;
using System.Collections.Generic;
using BepInEx;
using GorillaNetworking;
using MenkerMenu.Menu;
using MenkerMenu.Mods;
using MenkerMenu.Menu;
using static MenkerMenu.Mods.Categories.Room;
using Photon.Pun;
using UnityEngine;

namespace MenkerMenu.Menu
{
    [BepInPlugin("GUI", "GUI", "3.0.0")]
    public class MainGui : BaseUnityPlugin
    {
        public static int selectedtab;
        public static Rect windowRect = new Rect(60f, 20f, 800f, 500f);
        public static bool showGUI = true;

        private static Vector3 oldMousePos;
        public static Texture2D button = new Texture2D(1, 1);
        public static Texture2D backgrond = new Texture2D(1, 1);
        // Textures for buttons and background

        // Function to handle WASD movement
        public static void Keyboarding()
        {
            float currentSpeed = 5;
            Transform bodyTransform = Camera.main.transform;
            GorillaTagger.Instance.rigidbody.useGravity = false;
            GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;

            if (UnityInput.Current.GetKey(KeyCode.LeftShift))
            {
                currentSpeed *= 2.5f;
            }

            if (UnityInput.Current.GetKey(KeyCode.W) || UnityInput.Current.GetKey(KeyCode.UpArrow))
            {
                bodyTransform.position += bodyTransform.forward * currentSpeed * Time.deltaTime;
            }
            if (UnityInput.Current.GetKey(KeyCode.A) || UnityInput.Current.GetKey(KeyCode.LeftArrow))
            {
                bodyTransform.position += -bodyTransform.right * currentSpeed * Time.deltaTime;
            }
            if (UnityInput.Current.GetKey(KeyCode.S) || UnityInput.Current.GetKey(KeyCode.DownArrow))
            {
                bodyTransform.position += -bodyTransform.forward * currentSpeed * Time.deltaTime;
            }
            if (UnityInput.Current.GetKey(KeyCode.D) || UnityInput.Current.GetKey(KeyCode.RightArrow))
            {
                bodyTransform.position += bodyTransform.right * currentSpeed * Time.deltaTime;
            }
            if (UnityInput.Current.GetKey(KeyCode.Space))
            {
                bodyTransform.position += bodyTransform.up * currentSpeed * Time.deltaTime;
            }
            if (UnityInput.Current.GetKey(KeyCode.LeftControl))
            {
                bodyTransform.position += -bodyTransform.up * currentSpeed * Time.deltaTime;
            }
            if (UnityInput.Current.GetMouseButton(1))
            {
                Vector3 pos = UnityInput.Current.mousePosition - oldMousePos;
                float x = bodyTransform.localEulerAngles.x - pos.y * 0.3f;
                float y = bodyTransform.localEulerAngles.y + pos.x * 0.3f;
                bodyTransform.localEulerAngles = new Vector3(x, y, 0f);
            }

            oldMousePos = UnityInput.Current.mousePosition;
        }
        private float buttonCooldown = 0.1f;
        private float lastButtonPressTime = 0f;

        void Update()
        {
            if (UnityInput.Current.GetKeyDown(KeyCode.F1) && Time.time >= lastButtonPressTime + buttonCooldown)
            {
                showGUI = !showGUI;
                lastButtonPressTime = Time.time;
            }
        }

        void OnGUI()
        {
            if (showGUI)
            {
                float num = 200f;
                float num2 = 200f;
                GUIStyle guistyle = new GUIStyle(GUI.skin.button)
                {
                    normal =
                    {
                        background = null
                    },
                    active =
                    {
                        background = null
                    },
                    hover =
                    {
                        background = null
                    }
                };
                GUI.DrawTexture(new Rect(20f, 20f, num, num2), backgrond);
                if (showGUI)
                {
                    windowRect = GUI.Window(0, windowRect, new GUI.WindowFunction(DrawMainWindow), "");

                }
                DrawActiveButtons();

            }
            else
            {
            }
        }
        private void DrawComputerPage(GUIStyle buttonStyle)
        {
            GUILayout.Label("Computer", new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter
            }, Array.Empty<GUILayoutOption>());
            roomCodeInput = GUILayout.TextField(roomCodeInput, new GUILayoutOption[]
            {
        GUILayout.Width(800f)
            });
            if (GUILayout.Button("Join Room", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(roomCodeInput, 0);
            }
            if (GUILayout.Button("Quit Game", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Application.Quit();
            }
            if (GUILayout.Button("Disconnect", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                PhotonNetwork.Disconnect();
            }
            if (GUILayout.Button("Join Random Public", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                JoinRandomPublic();
            }
            if (GUILayout.Button("US Servers", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                PhotonNetwork.ConnectToRegion("us");
            }
            if (GUILayout.Button("EU Servers", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                PhotonNetwork.ConnectToRegion("eu");
            }
        }

        private List<string> activeButtons = new List<string>();
        private void DrawActiveButtons()
        {
            float num = 200f;
            float num2 = 30f;
            float num3 = 5f;
            GUIStyle guistyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 16,
                normal =
                {
                    textColor = Color.white
                },
                alignment = TextAnchor.MiddleCenter
            };
            GUILayout.BeginArea(new Rect(Screen.width - num - 20f, 20f, num, num2 * (activeButtons.Count + 1)));
            GUILayout.Space(10f);
            foreach (string text in activeButtons)
            {
                GUILayout.Label(text, guistyle, Array.Empty<GUILayoutOption>());
                GUILayout.Space(num3);
            }
            GUILayout.EndArea();
        }
        private int currentPage = 0;

        private Color sidePanelColor = new Color(0.05f, 0.05f, 0.05f);

        private Color buttonColor = new Color(0.05f, 0.05f, 0.05f);

        private Color activeButtonColor = new Color(0.83f, 0.18f, 0.4f);

        private Color buttonTextColor = Color.white;

        private Color mainContentColor = new Color(0.1f, 0.1f, 0.1f);
        private static bool customToggle;

        public static Vector2 scrollPos1 = Vector2.zero;

        public static Vector2 scrollPos2 = Vector2.zero;

        private string searchQuery = "";

        private string roomCodeInput = "";

        private Vector2 scrollPosition = Vector2.zero;

        private Texture2D buttonImage;

        private bool isImageLoaded = false;
        private void DrawModsPage(GUIStyle buttonStyle)
        {
            GUILayout.Label("Search", new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter
            }, Array.Empty<GUILayoutOption>());
            searchQuery = GUILayout.TextField(searchQuery, buttonStyle, new GUILayoutOption[]
            {
                GUILayout.Height(20f)
            });
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, Array.Empty<GUILayoutOption>());
            foreach (ButtonHandler.Button buttonInfo in ModButtons.buttons)
            {
                if (string.IsNullOrEmpty(searchQuery) || buttonInfo.buttonText.ToLower().Contains(searchQuery.ToLower()))
                {
                    GUIStyle guistyle = new GUIStyle(buttonStyle)
                    {
                        normal =
                            {
                                textColor = buttonInfo.Enabled ? Color.green : Color.blue
                            }
                    };
                    if (GUILayout.Button(buttonInfo.buttonText, guistyle, Array.Empty<GUILayoutOption>()))
                    {
                        buttonInfo.Enabled = !buttonInfo.Enabled;
                        if (buttonInfo.Enabled)
                        {
                            activeButtons.Add(buttonInfo.buttonText);
                        }
                        else
                        {
                            activeButtons.Remove(buttonInfo.buttonText);
                        }
                    }
                }
            }
            GUILayout.EndScrollView();
        }
        private Texture2D MakeTex(int width, int height, Color col)
        {
            Texture2D texture2D = new Texture2D(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    texture2D.SetPixel(i, j, col);
                }
            }
            texture2D.Apply();
            return texture2D;
        }
        private void DrawMainWindow(int windowID)
        {
            GUIStyle guistyle = new GUIStyle(GUI.skin.button)
            {
                normal =
        {
            background = MakeTex(1, 1, buttonColor),
            textColor = buttonTextColor
        },
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter
            };
            GUIStyle guistyle2 = new GUIStyle(GUI.skin.label)
            {
                fontSize = 22,
                normal =
        {
            textColor = buttonTextColor
        },
                alignment = TextAnchor.MiddleCenter
            };
            GUIStyle guistyle3 = new GUIStyle
            {
                normal =
        {
            background = MakeTex(1, 1, sidePanelColor)
        }
            };
            GUIStyle guistyle4 = new GUIStyle
            {
                normal =
        {
            background = MakeTex(1, 1, mainContentColor)
        }
            };
            GUILayout.BeginArea(new Rect(0f, 0f, 200f, windowRect.height), guistyle3);
            GUILayout.Label("Version 1.0", guistyle2, Array.Empty<GUILayoutOption>());
            GUILayout.Space(10f);
            if (GUILayout.Button("Mods", guistyle, Array.Empty<GUILayoutOption>()))
            {
                currentPage = 0;
            }
            if (GUILayout.Button("Computer", guistyle, Array.Empty<GUILayoutOption>()))
            {
                currentPage = 1;
            }
            GUILayout.Space(20f);
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(200f, 0f, windowRect.width - 200f, windowRect.height), guistyle4);
            GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
            GUILayout.Label(string.Format("Psi Menu | Hello {0}! \n FPS: {1}", PhotonNetwork.NickName.ToLower(), Mathf.RoundToInt(1f / Time.deltaTime)), guistyle2, Array.Empty<GUILayoutOption>());
            switch (currentPage)
            {
                case 0:
                    DrawModsPage(guistyle);
                    break;
                case 1:
                    DrawComputerPage(guistyle);
                    break;
                default:
                    GUILayout.Label("Select a page from the sidebar.", guistyle2, Array.Empty<GUILayoutOption>());
                    break;
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
            GUI.DragWindow();
        }
    }
}
