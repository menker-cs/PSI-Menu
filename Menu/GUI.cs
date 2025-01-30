/*using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using static MenkerMenu.Utilities.ColorLib;
using static MenkerMenu.Mods.Categories.Move;
using static MenkerMenu.Mods.Categories.Player;
using static MenkerMenu.Mods.Categories.Room;
using static MenkerMenu.Mods.Categories.Settings;
using static MenkerMenu.Mods.Categories.Safety;
using static MenkerMenu.Mods.Categories.Advantage;
using static MenkerMenu.Mods.Categories.Experimental;
using static MenkerMenu.Mods.Categories.Fun;
using static MenkerMenu.Mods.Categories.Guardian;
using static MenkerMenu.Mods.Categories.Visuals;
using static MenkerMenu.Mods.Categories.World;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using MenkerMenu.Mods.Categories;
using Photon.Realtime;
using UnityEngine.UIElements;
using MenkerMenu.Menu;

namespace GorillaTagPlugin
{
    [BepInPlugin("com.Gui.GayrillaTag", "GUI", "1.0.0")]
    public class GorillaTagGui : BaseUnityPlugin
    {
        private bool isGuiEnabled = false;
        private string buttonText = "Enable GUI";
        private string windowTitle = "Psi Menu";
        private Rect windowRectangle = new Rect(60, 20, 800, 500);
        private int currentPage = 0;

        private Color sidePanelColor = new Color(0.8f, 0.0f, 0.8f);
        private Color buttonColor = new Color(0.8f, 0.0f, 0.8f);
        private Color activeButtonColor = new Color(1.0f, 0.5f, 1.0f);
        private Color buttonTextColor = Color.white;
        private Color mainContentColor = new Color(0.9f, 0.1f, 0.9f);

        private Texture2D buttonImage;
        private bool imagething = false;

        public void Start()
        //image link for gui on button
        {
            StartCoroutine(LoadTextureFromURL("https://cdn.discordapp.com/attachments/1298799649590607942/1331458787931848786/image_2025-01-21_220138760-removebg-preview.png?ex=6791b120&is=67905fa0&hm=dd22e7455e19fba7c495fcb5b6cf46f00c52f9eb8abb40daa996cf96237bef7e&"));
        }

        private System.Collections.IEnumerator LoadTextureFromURL(string url)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                buttonImage = ((DownloadHandlerTexture)www.downloadHandler).texture;
                imagething = true;
            }
            else
            {
                Debug.LogError("Failed to load texture: " + www.error);
            }
        }

        void OnGUI()
        {
            if (!isGuiEnabled)
            {
                feedbackText = "";
                feedbackTimeRemaining = 0f;
            }

            if (imagething && buttonImage != null)
            {
                float buttonWidth = 200;
                float buttonHeight = 200;

                GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
                {
                    normal = { background = null },
                    active = { background = null },
                    hover = { background = null }
                };

                if (GUI.Button(new Rect(20, 20, buttonWidth, buttonHeight), "", buttonStyle))
                {
                    isGuiEnabled = !isGuiEnabled;
                    buttonText = isGuiEnabled ? "Disable GUI" : "Enable GUI";
                }

                GUI.DrawTexture(new Rect(20, 20, buttonWidth, buttonHeight), buttonImage);
            }

            if (isGuiEnabled)
            {
                windowRectangle = GUI.Window(10000, windowRectangle, MainGUI, windowTitle);
            }
            if (feedbackTimeRemaining > 0f)
            {
                GUIStyle feedbackStyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 26,
                    normal = { textColor = Color.white },
                    alignment = TextAnchor.MiddleCenter
                };

                Rect rect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 25, 300, 50);
                GUI.Label(rect, feedbackText, feedbackStyle);
            }

        }
        void HideFeedbackText()
        {
            showFeedback = false;
        }
        private IEnumerator ShowCustomFeedback(string message, float duration)
        {
            feedbackText = message;
            feedbackTimeRemaining = duration;

            while (feedbackTimeRemaining > 0f)
            {
                feedbackTimeRemaining -= Time.deltaTime;
                yield return null;
            }

            feedbackText = "";
            feedbackTimeRemaining = 0f;
        }
        void MainGUI(int windowID)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                normal = { background = MakeTex(1, 1, buttonColor), textColor = buttonTextColor },
                active = { background = MakeTex(1, 1, activeButtonColor) },
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter,
            };

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 22,
                normal = { textColor = buttonTextColor },
                alignment = TextAnchor.MiddleCenter
            };

            GUIStyle sidePanelStyle = new GUIStyle()
            {
                normal = { background = MakeTex(1, 1, sidePanelColor) }
            };

            GUIStyle mainContentStyle = new GUIStyle()
            {
                normal = { background = MakeTex(1, 1, mainContentColor) }
            };

            GUILayout.BeginArea(new Rect(0, 0, 250, windowRectangle.height), sidePanelStyle);
            GUILayout.Label("Developer Build", labelStyle);
            GUILayout.Space(10);
            if (GUILayout.Button("Room", buttonStyle)) currentPage = 0;
            if (GUILayout.Button("Game", buttonStyle)) currentPage = 1;
            GUILayout.Space(20);
            GUILayout.Label("Player", labelStyle);
            if (GUILayout.Button("Movement", buttonStyle)) currentPage = 2;
            if (GUILayout.Button("Rig", buttonStyle)) currentPage = 3;
            GUILayout.Space(20);
            GUILayout.Label("Visual", labelStyle);
            if (GUILayout.Button("ESP", buttonStyle)) currentPage = 4;
            if (GUILayout.Button("World", buttonStyle)) currentPage = 5;
            GUILayout.Space(20);
            GUILayout.Label("Fun", labelStyle);
            if (GUILayout.Button("Fun", buttonStyle)) currentPage = 6;
            GUILayout.Space(20);
            GUILayout.Label("Misc", labelStyle);
            if (GUILayout.Button("Theme", buttonStyle)) currentPage = 7;
            if (GUILayout.Button("Credits", buttonStyle)) currentPage = 9;
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(250, 0, windowRectangle.width - 250, windowRectangle.height), mainContentStyle);
            GUILayout.BeginVertical();
            GUILayout.Label(windowTitle, labelStyle);

            switch (currentPage)
            {
                case 0: RoomPage(buttonStyle); break;
                case 1: GamePage(buttonStyle); break;
                case 2: MovementPage(buttonStyle); break;
                case 3: RigPage(buttonStyle); break;
                case 4: ESPPage(buttonStyle); break;
                case 5: World(buttonStyle); break;
                case 6: Fun(buttonStyle); break;
                case 7: ThemePage(buttonStyle); break;
                case 9: CreditsPage(buttonStyle); break;
                default: break;
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();

            GUI.DragWindow();
        }

        void RoomPage(GUIStyle buttonStyle)
        {
            GUILayout.Space(10);
            roomCodeInput = GUILayout.TextField(roomCodeInput, 25);
            if (GUILayout.Button("Join Room", buttonStyle))
            {
                Debug.Log($"Joining room: {roomCodeInput}");
                PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(roomCodeInput, 0);
                string customMessage = "Joining...";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));


            }
            if (GUILayout.Button("Set Name", buttonStyle))
            {
                PhotonNetwork.LocalPlayer.NickName = roomCodeInput;
                PhotonNetwork.NickName = roomCodeInput;
                PhotonNetwork.NetworkingClient.NickName = roomCodeInput;
                GorillaComputer.instance.currentName = roomCodeInput;
                GorillaComputer.instance.savedName = roomCodeInput;
                GorillaComputer.instance.offlineVRRigNametagText.text = roomCodeInput;
                GorillaLocomotion.Player.Instance.name = roomCodeInput;
                NetworkSystem.Instance.name = roomCodeInput;
                NetworkSystem.Instance.SetMyNickName(roomCodeInput);
                PlayerPrefs.SetString("playerName", roomCodeInput);
                PlayerPrefs.Save();
            }
            if (GUILayout.Button("Join Random (Forest)", buttonStyle))
            {
                Debug.Log("Joining random room");
                PhotonNetworkController.Instance.AttemptToJoinPublicRoom(GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>(), 0);
                string customMessage = "Joining...";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));


            }
            if (GUILayout.Button("Disconnect", buttonStyle))
            {
                Debug.Log("Disconnecting...");
                PhotonNetwork.Disconnect();

                string customMessage = "Disconnected";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }
            if (GUILayout.Button("Quit Game", buttonStyle))
            {
                Application.Quit();
            }
            if (GUILayout.Button("Delete TOS", buttonStyle))
            {
                string customMessage = "Enjoy!";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));

                GameObject root = GameObject.Find("Miscellaneous Scripts/PrivateUIRoom/Root").gameObject;
                root.SetActive(false);

                GameObject Geode = GameObject.Find("Miscellaneous Scripts/PrivateUIRoom/ReportOccluder/Geode").gameObject;
                Geode.SetActive(false);

                GameObject Canvas = GameObject.Find("Miscellaneous Scripts/MetaReporting/Canvas").gameObject;
                Canvas.SetActive(false);

                GameObject Geode2 = GameObject.Find("Miscellaneous Scripts/MetaReporting/ReportOccluder/Geode").gameObject;
                Geode2.SetActive(false);
            }
        }
        private static bool toggleNotif = true;
        void GamePage(GUIStyle buttonStyle)
        {
            if (GUILayout.Button("Toggle Notifs", buttonStyle))
            {
                toggleNotif = !toggleNotif;

                if (toggleNotif)
                {
                    Settings.ToggleNotifications(true);
                }
                else
                {
                    Settings.ToggleNotifications(false);
                }
            }
            if (GUILayout.Button("Change Fly Speed", buttonStyle))
            {
                Settings.FlySpeed();

                string customMessage = "Changed Speed";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }

            if (GUILayout.Button("Change Speed Boost", buttonStyle))
            {
                Settings.SpeedSpeed();

                string customMessage = "Changed Speed";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }

            if (GUILayout.Button("Change ESP Color", buttonStyle))
            {
                Settings.ESPChange();

                string customMessage = "Changed Color";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }
        }

        void MovementPage(GUIStyle buttonStyle)
        {
            if (GUILayout.Button("FreeCam", buttonStyle))
            {
                isWASDEnabled = !isWASDEnabled;

                if (isWASDEnabled)
                {
                    Move.WASDFly();
                }

                string customMessage = isWASDEnabled ? "FreeCam Enabled" : "FreeCam Disabled";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }

            if (GUILayout.Button("Noclip", buttonStyle))
            {
                isNCEnabled = !isNCEnabled;

                if (isNCEnabled)
                {
                    foreach (MeshCollider collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.2f | UnityInput.Current.GetKey(KeyCode.T))
                        {
                            collider.enabled = false;
                        }
                        else
                        {
                            collider.enabled = true;
                        }
                    }
                }

                string customMessage = isNCEnabled ? "Noclip Enabled" : "Noclip Disabled";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }
        }

        void RigPage(GUIStyle buttonStyle)
        {
            if (GUILayout.Button("Ghost Monke", buttonStyle))
            {
                GhostToggled = !GhostToggled;

                if (GhostToggled)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    string customMessage = "Ghost Enabled";
                    StartCoroutine(ShowCustomFeedback(customMessage, 3f));
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                    string customMessage = "Ghost Disabled";
                    StartCoroutine(ShowCustomFeedback(customMessage, 3f));
                }
            }

            if (GUILayout.Button("Invis Monke", buttonStyle))
            {
                InvisToggled = !InvisToggled;

                if (InvisToggled)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(100f, 0f, 100f);
                    string customMessage = "Invisible Enabled";
                    StartCoroutine(ShowCustomFeedback(customMessage, 3f));
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                    string customMessage = "Invisible Disabled";
                    StartCoroutine(ShowCustomFeedback(customMessage, 3f));
                }
            }
        }

        void Update()
        {

        }

        void ESPPage(GUIStyle buttonStyle)
        {
            if (GUILayout.Button("ESP", buttonStyle))
            {
                ESPActive = !ESPActive;

                if (ESPActive)
                {
                    Visuals.ESP();
                }
                else
                {
                    Visuals.DisableESP();
                }
                string customMessage = ESPActive ? "ESP Enabled" : "ESP Disabled";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }

            if (GUILayout.Button("Tracers [NW For GUI]", buttonStyle))
            {
                tracersActive = !tracersActive;

                if (tracersActive)
                {
                    if (espColor == 1)
                    {
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                line = new GameObject("Line");
                                LineRenderer Line = line.AddComponent<LineRenderer>();
                                Line.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                                Line.SetPosition(1, vrrig.transform.position);
                                Line.startWidth = 0.0225f;
                                Line.endWidth = 0.0225f;

                                Line.material.shader = Shader.Find("GUI/Text Shader");

                                if (vrrig.mainSkin.material.name.Contains("fected"))
                                {
                                    Line.startColor = UnityEngine.Color.red;
                                    Line.endColor = UnityEngine.Color.red;
                                }
                                else
                                {
                                    Line.startColor = UnityEngine.Color.green;
                                    Line.endColor = UnityEngine.Color.green;
                                }
                            }
                        }
                    }
                    else if (espColor == 2)
                    {
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                line = new GameObject("Line");
                                LineRenderer Line = line.AddComponent<LineRenderer>();
                                Line.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                                Line.SetPosition(1, vrrig.transform.position);
                                Line.startWidth = 0.0225f;
                                Line.endWidth = 0.0225f;

                                Line.startColor = vrrig.playerColor;
                                Line.endColor = vrrig.playerColor;
                                Line.material.shader = Shader.Find("GUI/Text Shader");
                            }
                        }
                    }
                    else if (espColor == 3)
                    {
                        GradientColorKey[] array = new GradientColorKey[7];
                        array[0].color = UnityEngine.Color.red;
                        array[0].time = 0f;
                        array[1].color = UnityEngine.Color.yellow;
                        array[1].time = 0.2f;
                        array[2].color = UnityEngine.Color.green;
                        array[2].time = 0.3f;
                        array[3].color = UnityEngine.Color.cyan;
                        array[3].time = 0.5f;
                        array[4].color = UnityEngine.Color.blue;
                        array[4].time = 0.6f;
                        array[5].color = UnityEngine.Color.magenta;
                        array[5].time = 0.8f;
                        array[6].color = UnityEngine.Color.red;
                        array[6].time = 1f;
                        Gradient gradient = new Gradient();
                        gradient.colorKeys = array;
                        float num = Mathf.PingPong(Time.time / 2f, 1f);
                        UnityEngine.Color color = gradient.Evaluate(num);

                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                line = new GameObject("Line");
                                LineRenderer Line = line.AddComponent<LineRenderer>();
                                Line.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                                Line.SetPosition(1, vrrig.transform.position);
                                Line.startWidth = 0.0225f;
                                Line.endWidth = 0.0225f;

                                Line.startColor = color;
                                Line.endColor = color;
                                Line.material.shader = Shader.Find("GUI/Text Shader");
                            }
                        }
                    }
                    else if (espColor == 4)
                    {
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                line = new GameObject("Line");
                                LineRenderer Line = line.AddComponent<LineRenderer>();
                                Line.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                                Line.SetPosition(1, vrrig.transform.position);
                                Line.startWidth = 0.0225f;
                                Line.endWidth = 0.0225f;

                                Line.startColor = RoyalBlue;
                                Line.endColor = RoyalBlue;
                                Line.material.shader = Shader.Find("GUI/Text Shader");
                            }
                        }
                    }
                }
                else
                {
                    UnityEngine.Object.Destroy(line, Time.deltaTime);
                }
                string customMessage = tracersActive ? "Tracers Enabled" : "Tracers Disabled";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }

            if (GUILayout.Button("2D Box ESP", buttonStyle))
            {
                box2 = !box2;

                if (box2)
                {
                    if (espColor == 1)
                    {
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                ESPBox.transform.position = vrrig.transform.position;
                                UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                                ESPBox.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                                ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                                if (vrrig.mainSkin.material.name.Contains("fected"))
                                {
                                    ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                    ESPBox.GetComponent<Renderer>().material.color = UnityEngine.Color.red;
                                }
                                else
                                {
                                    ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                    ESPBox.GetComponent<Renderer>().material.color = UnityEngine.Color.green;
                                }
                            }
                        }
                    }
                    if (espColor == 2)
                    {
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                ESPBox.transform.position = vrrig.transform.position;
                                UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                                ESPBox.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                                ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                                ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBox.GetComponent<Renderer>().material.color = vrrig.playerColor;
                            }
                        }
                    }
                    if (espColor == 3)
                    {
                        GradientColorKey[] array = new GradientColorKey[7];
                        array[0].color = UnityEngine.Color.red;
                        array[0].time = 0f;
                        array[1].color = UnityEngine.Color.yellow;
                        array[1].time = 0.2f;
                        array[2].color = UnityEngine.Color.green;
                        array[2].time = 0.3f;
                        array[3].color = UnityEngine.Color.cyan;
                        array[3].time = 0.5f;
                        array[4].color = UnityEngine.Color.blue;
                        array[4].time = 0.6f;
                        array[5].color = UnityEngine.Color.magenta;
                        array[5].time = 0.8f;
                        array[6].color = UnityEngine.Color.red;
                        array[6].time = 1f;
                        Gradient gradient = new Gradient();
                        gradient.colorKeys = array;
                        float num = Mathf.PingPong(Time.time / 2f, 1f);
                        UnityEngine.Color color = gradient.Evaluate(num);

                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                ESPBox.transform.position = vrrig.transform.position;
                                UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                                ESPBox.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                                ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                                ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBox.GetComponent<Renderer>().material.color = color;
                            }
                        }
                    }
                    if (espColor == 4)
                    {
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                ESPBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                ESPBox.transform.position = vrrig.transform.position;
                                UnityEngine.Object.Destroy(ESPBox.GetComponent<BoxCollider>());
                                ESPBox.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                                ESPBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                                ESPBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBox.GetComponent<Renderer>().material.color = RoyalBlue;
                            }
                        }
                    }
                }
                else
                {
                    UnityEngine.Object.Destroy(ESPBox, Time.deltaTime);
                }
                string customMessage = box2 ? "ESP Enabled" : "ESP Disabled";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }

            if (GUILayout.Button("ESP", buttonStyle))
            {
                ESPActive = !ESPActive;

                if (ESPActive)
                {
                    Visuals.ESP();
                }
                else
                {
                    Visuals.DisableESP();
                }
                string customMessage = ESPActive ? "ESP Enabled" : "ESP Disabled";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }

            if (GUILayout.Button("Sphere ESP", buttonStyle))
            {
                sphere = !sphere;

                if (sphere)
                {
                    if (espColor == 1)
                    {
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                ESPBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                ESPBall.transform.position = vrrig.transform.position;
                                UnityEngine.Object.Destroy(ESPBall.GetComponent<BoxCollider>());
                                ESPBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                                ESPBall.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                                if (vrrig.mainSkin.material.name.Contains("fected"))
                                {
                                    ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                    ESPBall.GetComponent<Renderer>().material.color = UnityEngine.Color.red;
                                }
                                else
                                {
                                    ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                    ESPBall.GetComponent<Renderer>().material.color = UnityEngine.Color.green;
                                }
                            }
                        }
                    }
                    else if (espColor == 2)
                    {
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                GameObject ESPBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                ESPBall.transform.position = vrrig.transform.position;
                                UnityEngine.Object.Destroy(ESPBall.GetComponent<BoxCollider>());
                                ESPBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                                ESPBall.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                                ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBall.GetComponent<Renderer>().material.color = vrrig.playerColor;
                            }
                        }
                    }
                    else if (espColor == 3)
                    {
                        GradientColorKey[] array = new GradientColorKey[7];
                        array[0].color = UnityEngine.Color.red;
                        array[0].time = 0f;
                        array[1].color = UnityEngine.Color.yellow;
                        array[1].time = 0.2f;
                        array[2].color = UnityEngine.Color.green;
                        array[2].time = 0.3f;
                        array[3].color = UnityEngine.Color.cyan;
                        array[3].time = 0.5f;
                        array[4].color = UnityEngine.Color.blue;
                        array[4].time = 0.6f;
                        array[5].color = UnityEngine.Color.magenta;
                        array[5].time = 0.8f;
                        array[6].color = UnityEngine.Color.red;
                        array[6].time = 1f;
                        Gradient gradient = new Gradient();
                        gradient.colorKeys = array;
                        float num = Mathf.PingPong(Time.time / 2f, 1f);
                        UnityEngine.Color color = gradient.Evaluate(num);

                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                GameObject ESPBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                ESPBall.transform.position = vrrig.transform.position;
                                UnityEngine.Object.Destroy(ESPBall.GetComponent<BoxCollider>());
                                ESPBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                                ESPBall.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                                ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBall.GetComponent<Renderer>().material.color = color;
                            }
                        }
                    }
                    else if (espColor == 4)
                    {
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                GameObject ESPBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                ESPBall.transform.position = vrrig.transform.position;
                                UnityEngine.Object.Destroy(ESPBall.GetComponent<BoxCollider>());
                                ESPBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                                ESPBall.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                                ESPBall.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                                ESPBall.GetComponent<Renderer>().material.color = RoyalBlue;
                            }
                        }
                    }
                }
                else
                {
                    UnityEngine.Object.Destroy(ESPBall, Time.deltaTime);
                }
                string customMessage = ESPActive ? "ESP Enabled" : "ESP Disabled";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }

            if (GUILayout.Button("CSGO ESP", buttonStyle))
            {
                CSGO = !CSGO;

                if (CSGO)
                {
                    Visuals.CSGO();
                }
                else
                {
                    Visuals.DisableCSGO();
                }
                string customMessage = CSGO ? "ESP Enabled" : "ESP Disabled";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }
        }

        void World(GUIStyle buttonStyle)
        {
            GUILayout.Label("World", buttonStyle);
        }
        void Fun(GUIStyle buttonStyle)
        {
            GUILayout.Label("Fun", buttonStyle);
        }

        void ThemePage(GUIStyle buttonStyle)
        {
            if (GUILayout.Button("Crimson Theme", buttonStyle))
            {
                sidePanelColor = Color.black;
                buttonColor = Color.black;
                activeButtonColor = Color.green;
                buttonTextColor = Color.red;
                mainContentColor = Color.black;
            }

            if (GUILayout.Button("Magenta", buttonStyle))
            {
                sidePanelColor = new Color(0.8f, 0.0f, 0.8f);
                buttonColor = new Color(0.8f, 0.0f, 0.8f);
                activeButtonColor = new Color(1.0f, 0.5f, 1.0f);
                buttonTextColor = Color.white;
                mainContentColor = new Color(0.9f, 0.1f, 0.9f);
            }

            if (GUILayout.Button("Purple", buttonStyle))
            {
                sidePanelColor = new Color(0.4f, 0.0f, 0.6f);
                buttonColor = new Color(0.4f, 0.0f, 0.6f);
                activeButtonColor = new Color(0.6f, 0.3f, 0.8f);
                buttonTextColor = Color.white;
                mainContentColor = new Color(0.5f, 0.1f, 0.7f);
            }

            if (GUILayout.Button("Blue", buttonStyle))
            {
                sidePanelColor = DarkDodgerBlue;
                buttonColor = RoyalBlue;
                activeButtonColor = DarkDodgerBlue;
                buttonTextColor = Color.white;
                mainContentColor = DodgerBlue;
            }

            if (GUILayout.Button("Green", buttonStyle))
            {
                sidePanelColor = new Color(0.0f, 0.8f, 0.0f);
                buttonColor = new Color(0.0f, 0.8f, 0.0f);
                activeButtonColor = new Color(0.3f, 1.0f, 0.3f);
                buttonTextColor = Color.white;
                mainContentColor = new Color(0.1f, 0.9f, 0.1f);
            }

            if (GUILayout.Button("Yellow", buttonStyle))
            {
                sidePanelColor = new Color(0.8f, 0.7f, 0.0f);
                buttonColor = new Color(0.8f, 0.7f, 0.0f);
                activeButtonColor = new Color(1.0f, 0.9f, 0.3f);
                buttonTextColor = Color.black;
                mainContentColor = new Color(0.9f, 0.8f, 0.2f);
            }

            if (GUILayout.Button("Red", buttonStyle))
            {
                sidePanelColor = new Color(0.8f, 0.0f, 0.0f);
                buttonColor = new Color(0.8f, 0.0f, 0.0f);
                activeButtonColor = new Color(1.0f, 0.3f, 0.3f);
                buttonTextColor = Color.white;
                mainContentColor = new Color(0.9f, 0.1f, 0.1f);
            }

            if (GUILayout.Button("Orange", buttonStyle))
            {
                sidePanelColor = new Color(1.0f, 0.5f, 0.0f);
                buttonColor = new Color(1.0f, 0.5f, 0.0f);
                activeButtonColor = new Color(1.0f, 0.7f, 0.3f);
                buttonTextColor = Color.black;
                mainContentColor = new Color(1.0f, 0.6f, 0.2f);
            }


            if (GUILayout.Button("Cyan", buttonStyle))
            {
                sidePanelColor = new Color(0.0f, 0.7f, 0.7f);
                buttonColor = new Color(0.0f, 0.7f, 0.7f);
                activeButtonColor = new Color(0.3f, 0.9f, 0.9f);
                buttonTextColor = Color.black;
                mainContentColor = new Color(0.1f, 0.8f, 0.8f);
            }


            if (GUILayout.Button("Pink", buttonStyle))
            {
                sidePanelColor = new Color(1.0f, 0.2f, 0.6f);
                buttonColor = new Color(1.0f, 0.2f, 0.6f);
                activeButtonColor = new Color(1.0f, 0.5f, 0.8f);
                buttonTextColor = Color.white;
                mainContentColor = new Color(1.0f, 0.4f, 0.7f);
            }


            if (GUILayout.Button("Dark Mode", buttonStyle))
            {
                sidePanelColor = new Color(0.05f, 0.05f, 0.05f);
                buttonColor = new Color(0.05f, 0.05f, 0.05f);
                activeButtonColor = new Color(0.83f, 0.18f, 0.40f);
                buttonTextColor = Color.white;
                mainContentColor = new Color(0.1f, 0.1f, 0.1f);
            }

            if (GUILayout.Button("Light Mode", buttonStyle))
            {
                sidePanelColor = new Color(0.9f, 0.9f, 0.9f);
                buttonColor = new Color(0.9f, 0.9f, 0.9f);
                activeButtonColor = new Color(0.83f, 0.18f, 0.40f);
                buttonTextColor = Color.black;
                mainContentColor = Color.white;
            }


        }

        void WhatsNewPage(GUIStyle buttonStyle)
        {

            if (GUILayout.Button("What's New?", buttonStyle))
            {
                string customMessage = "The entire fucking menu lol";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }
        }

        void CreditsPage(GUIStyle buttonStyle)
        {
            GUILayout.Label("Click On A Person To Open Their Link/s", buttonStyle);

            if (GUILayout.Button("Menker: Owner", buttonStyle))
            {
                if (!hasOpenedLink)
                {
                    hasOpenedLink = true;

                    Application.OpenURL("https://guns.lol/menker");

                    string customMessage = "Thanks!";
                    StartCoroutine(ShowCustomFeedback(customMessage, 7.5f));
                }
            }

        }


        private Texture2D MakeTex(int width, int height, Color col)
        {
            Texture2D texture = new Texture2D(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    texture.SetPixel(x, y, col);
                }
            }
            texture.Apply();
            return texture;
        }
        public static bool IsPlayerInfected(VRRig player)
        {
            return player.mainSkin.material.name.Contains("fected");
        }

        private string roomCodeInput = "";
        private List<GameObject> tracerObjects = new List<GameObject>();
        private List<LineRenderer> lineRenderers = new List<LineRenderer>();
        private bool showFeedback = false;
        private string feedbackText = "";
        private float feedbackTimeRemaining = 0f;
        private float feedbackDuration = 2f;
        public static bool hasOpenedLink = false;

        private bool box2 = false;
        private bool box3 = false;
        private bool sphere = false;
        private bool ESPActive = false;
        private bool CSGO = false;
        private bool tracersActive = false;
        private bool isWASDEnabled = false;
        private bool isNCEnabled = false;
        private static bool GhostToggled = false;
        private static bool InvisToggled = false;

        private GameObject ESPBox;
        private GameObject line;
        private GameObject ESPBall;
    }
}
*/