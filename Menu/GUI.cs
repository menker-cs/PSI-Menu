using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using static MenkerMenu.Utilities.ColorLib;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

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
            if (GUILayout.Button("Yourself", buttonStyle)) currentPage = 2;
            if (GUILayout.Button("Others", buttonStyle)) currentPage = 3;
            GUILayout.Space(20);
            GUILayout.Label("Playstyle", labelStyle);
            if (GUILayout.Button("Legit", buttonStyle)) currentPage = 4;
            if (GUILayout.Button("Blatant", buttonStyle)) currentPage = 5;
            GUILayout.Space(20);
            GUILayout.Label("Misc", labelStyle);
            if (GUILayout.Button("Theme", buttonStyle)) currentPage = 6;
            if (GUILayout.Button("What's New?", buttonStyle)) currentPage = 7;
            if (GUILayout.Button("Credits", buttonStyle)) currentPage = 8;
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(250, 0, windowRectangle.width - 250, windowRectangle.height), mainContentStyle);
            GUILayout.BeginVertical();
            GUILayout.Label(windowTitle, labelStyle);

            switch (currentPage)
            {
                case 0: PlayerPage(buttonStyle); break;
                case 1: ChallengesPage(buttonStyle); break;
                case 2: OthersPage(buttonStyle); break;
                case 3: NoteboosPage(buttonStyle); break;
                case 4: EventsPage(buttonStyle); break;
                case 5: EventsPage2(buttonStyle); break;
                case 6: MiscPage(buttonStyle); break;
                case 7: WhatsNewPage(buttonStyle); break;
                case 8: Credits(buttonStyle); break;
                default: break;
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();

            GUI.DragWindow();
        }

        void PlayerPage(GUIStyle buttonStyle)
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
        }

        void ChallengesPage(GUIStyle buttonStyle)
        {
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

        public void WASD()
        {
            var player = GorillaLocomotion.Player.Instance;
            var rigidbody = GorillaTagger.Instance.rigidbody;
            float speed = 4.5f;
            player.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0.067f, 0f);
            bool W = UnityInput.Current.GetKey(KeyCode.W);
            bool A = UnityInput.Current.GetKey(KeyCode.A);
            bool S = UnityInput.Current.GetKey(KeyCode.S);
            bool D = UnityInput.Current.GetKey(KeyCode.D);
            bool Space = UnityInput.Current.GetKey(KeyCode.Space);
            bool Ctrl = UnityInput.Current.GetKey(KeyCode.LeftControl);
            if (Mouse.current.rightButton.isPressed)
            {
                Vector3 euler = player.rightControllerTransform.parent.rotation.eulerAngles;
                float sensitivity = 200f;
                euler.y += (Mouse.current.delta.x.ReadValue() / UnityEngine.Screen.width) * sensitivity;
                euler.x -= (Mouse.current.delta.y.ReadValue() / UnityEngine.Screen.height) * sensitivity;
                player.rightControllerTransform.parent.rotation = Quaternion.Euler(euler);
            }
            if (UnityInput.Current.GetKey(KeyCode.LeftShift))
                speed = 15f;
            Vector3 moveDirection = Vector3.zero;
            if (W) moveDirection += player.rightControllerTransform.parent.forward;
            if (S) moveDirection -= player.rightControllerTransform.parent.forward;
            if (A) moveDirection -= player.rightControllerTransform.parent.right;
            if (D) moveDirection += player.rightControllerTransform.parent.right;
            if (Space) moveDirection += Vector3.up;
            if (Ctrl) moveDirection -= Vector3.up;
            rigidbody.transform.position += moveDirection * Time.deltaTime * speed;
        }




        void OthersPage(GUIStyle buttonStyle)
        {
            if (GUILayout.Button("FreeCam", buttonStyle))
            {
                isWASDEnabled = !isWASDEnabled;
                isMovementActive = isWASDEnabled;

                string customMessage = isWASDEnabled ? "FreeCam Enabled" : "FreeCam Disabled";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }

            if (isWASDEnabled)
            {
                WASD();
            }

            if (GUILayout.Button("Noclip", buttonStyle))
            {
                isNCEnabled = !isNCEnabled;

                foreach (MeshCollider meshCollider in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    meshCollider.enabled = !isNCEnabled;
                }
                string customMessage = isNCEnabled ? "Noclip Enabled" : "Noclip Disabled";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }

            if (GUILayout.Button("Ghost", buttonStyle))
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

                GhostCooldown = Time.time;
            }
            if (GUILayout.Button("Invisible", buttonStyle))
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

                InvisCooldown = Time.time;
            }

        }

        void NoteboosPage(GUIStyle buttonStyle)
        {
            GUILayout.Label("Others", buttonStyle);

            if (GUILayout.Button("Tracers", buttonStyle))
            {
                tracersActive = !tracersActive;

                if (tracersActive)
                {
                    foreach (VRRig Player in GorillaParent.instance.vrrigs)
                    {
                        if (Player == GorillaTagger.Instance.offlineVRRig) continue;

                        Color color;
                        if (IsPlayerInfected(Player))
                            color = Color.red;
                        else
                            color = Player.mainSkin.material.color;

                        var gameObject = new GameObject("Line");
                        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
                        lineRenderer.startColor = color;
                        lineRenderer.endColor = color;
                        lineRenderer.startWidth = 0.01f;
                        lineRenderer.endWidth = 0.01f;
                        lineRenderer.positionCount = 2;
                        lineRenderer.useWorldSpace = true;
                        lineRenderer.material.shader = Shader.Find("GUI/Text Shader");

                        tracerObjects.Add(gameObject);
                        lineRenderers.Add(lineRenderer);
                    }
                }
                else
                {
                    foreach (GameObject tracer in tracerObjects)
                    {
                        UnityEngine.Object.Destroy(tracer);
                    }
                    tracerObjects.Clear();
                    lineRenderers.Clear();
                }
                string customMessage = tracersActive ? "Tracers Enabled" : "Tracers Disabled";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }
        }

        void Update()
        {

        }

        void EventsPage(GUIStyle buttonStyle)
        {
            if (GUILayout.Button("Legit", buttonStyle))
            {
                Debug.Log("Event 1 Triggered");
            }

            if (GUILayout.Button("Event 2", buttonStyle))
            {
                Debug.Log("Event 2 Triggered");
            }
        }

        void EventsPage2(GUIStyle buttonStyle)
        {
            GUILayout.Label("Blatant", buttonStyle);
        }

        void MiscPage(GUIStyle buttonStyle)
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
                string customMessage = "Idk, You Tell Me";
                StartCoroutine(ShowCustomFeedback(customMessage, 3f));
            }
        }

        void Credits(GUIStyle buttonStyle)
        {
            GUILayout.Label("Click On A Person To Open Their Link/s", buttonStyle);

            if (GUILayout.Button("CosmicCrystal: GUI", buttonStyle))
            {
                if (!hasOpenedLink)
                {
                    hasOpenedLink = true;

                    Application.OpenURL("https://discord.gg/skidded");
                    Application.OpenURL("https://guns.lol/cosmiccrystal");

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


        private bool tracersActive = false;
        private bool isWASDEnabled = false;
        private bool isMovementActive = false;
        private bool isNCEnabled = false;

        public static float GhostCooldown;
        public static bool GhostToggled;
        public static bool InvisToggled;
        public static float InvisCooldown;
    }
}
