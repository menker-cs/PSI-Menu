/*
using BepInEx;
using Photon.Pun;
using MenkerMenu.Mods.Categories;
using MenkerMenu.Menu;
using UnityEngine;
using MenkerMenu.Utilities;
using System.Collections.Generic;
using static MenkerMenu.Menu.ButtonHandler;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine.UI;
using System;
using static MenkerMenu.Utilities.NotificationLib;
using GorillaNetworking;
using MenkerMenu.Mods;

namespace dfgsfe
{
    [BepInPlugin("PSI", "Psi.GayrillaTag.GUI", "1.0.0")]
    public class Gui : BaseUnityPlugin
    {
        private bool open = true;
        public static KeyCode OpenAndCloseGUI = KeyCode.Z;
        static Vector2 scrollPosition;
        public static Rect window = new Rect(20f, 20f, 600f, 800f);
        private Vector2 dragstart;
        private bool dragging = false;
        private Vector2 targetPosition;
        private float dragSmoothSpeed = 15f;
        public static List<ButtonHandler.Button> buttons = new List<ButtonHandler.Button>();
        private float targetAlpha = 0.8f;
        private float currentAlpha = 0f;
        private bool isAnimating = false;
        private float animationSpeed = 4f;
        private float colorLerpValue = 0f;
        private float colorLerpSpeed = 0.5f;
        private List<Snowflake> snowflakes = new List<Snowflake>();
        private const int SNOWFLAKE_COUNT = 100;
        private bool showCategories = true;
        private string currentCategory = "";
        private Rect topBar;
        private Rect sidePanel;
        private float sidePanelWidth = 150f;
        private bool showModCategories = false;
        private int cornerRadius = 4;
        private Color sidePanelColor;
        private float buttonHoverAlpha = 0.8f;
        private float buttonPressedAlpha = 0.6f;
        private float rainbowHue = 0f;
        private Color mainBackgroundColor = new Color(0f, 0f, 0f, 0.7f);
        private Color[] colorGradient = new Color[4];
        private float colorTransitionSpeed = 0.8f;
        private float colorOffset = 0f;

        private enum MainCategory
        {
            None,
            Mods,
            Performance,
            Room,
            Settings
        }

        private MainCategory currentMainCategory = MainCategory.None;

        private class Snowflake
        {
            public float x;
            public float y;
            public float speed;
            public float size;
            public float alpha;
            public float rotation;
        }

        void Start()
        {
            InitializeUI();
            InitializeSnowflakes();
            InitializeColorGradient();
            sidePanelColor = new Color(0.1f, 0.1f, 0.1f, 0.8f);
        }

        private void InitializeColorGradient()
        {
            colorGradient[0] = mainBackgroundColor;
            colorGradient[1] = mainBackgroundColor;
            colorGradient[2] = mainBackgroundColor;
            colorGradient[3] = mainBackgroundColor;
        }

        private void InitializeUI()
        {
            topBar = new Rect(Screen.width / 2 - 150f, 5f, 300f, 30f);
            sidePanel = new Rect(window.x, window.y, sidePanelWidth, window.height);
            window.width += sidePanelWidth;
            targetPosition = window.position;
        }

        private void InitializeSnowflakes()
        {
            for (int i = 0; i < SNOWFLAKE_COUNT; i++)
            {
                snowflakes.Add(new Snowflake
                {
                    x = UnityEngine.Random.Range(0, (int)window.width),
                    y = UnityEngine.Random.Range(0, (int)window.height),
                    speed = UnityEngine.Random.Range(1f, 3f),
                    size = UnityEngine.Random.Range(1f, 3f),
                    alpha = UnityEngine.Random.Range(0.3f, 0.8f),
                    rotation = UnityEngine.Random.Range(0f, 360f)
                });
            }
        }

        private Color GetRainbowColor()
        {
            rainbowHue += Time.deltaTime * 0.003f;
            if (rainbowHue > 1f) rainbowHue -= 1f;
            return Color.HSVToRGB(rainbowHue, 0.7f, 1f);
        }

        public void OnGUI()
        {
            colorOffset += Time.deltaTime * colorTransitionSpeed;
            if (colorOffset > 1f) colorOffset -= 1f;

            Color currentColor = mainBackgroundColor;
            var backgroundTexture = new Texture2D(1, 1);
            backgroundTexture.SetPixel(0, 0, new Color(currentColor.r, currentColor.g, currentColor.b, currentAlpha));
            backgroundTexture.Apply();

            var customStyle = CreateCustomStyle(backgroundTexture);
            DrawTopBar(customStyle);

            if (!open) return;

            HandleFadeAnimation();
            var buttonStyle = CreateEnhancedButtonStyle();

            window.position = Vector2.Lerp(window.position, targetPosition, Time.deltaTime * dragSmoothSpeed);
            DrawWindowFrame(customStyle);
            DrawTitle();
            DrawSidePanel(buttonStyle);

            if (!showCategories)
            {
                DrawToggles(buttonStyle);
                DrawBackButton(buttonStyle);
            }

            DrawMainButtons(buttonStyle);
            HandleDragging();
            DrawSnowflakes();

            UnityEngine.Object.Destroy(backgroundTexture);
        }

        private void DrawTopBar(GUIStyle customStyle)
        {
            GUI.color = new Color(mainBackgroundColor.r, mainBackgroundColor.g, mainBackgroundColor.b, 0.7f);
            GUI.Box(topBar, "", customStyle);

            var topBarStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                richText = true,
                alignment = TextAnchor.MiddleCenter
            };

            GUI.Label(topBar, CreateRichText($"PSI | PSI ON TOP | FPS: {Mathf.Round(1f / Time.deltaTime)}", GetRainbowColor()), topBarStyle);
        }

        private void DrawTitle()
        {
            var titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 22,
                fontStyle = FontStyle.Bold,
                richText = true,
                alignment = TextAnchor.MiddleCenter,
                padding = new RectOffset(10, 10, 5, 5)
            };

            GUI.Label(new Rect(window.x, window.y + 5, window.width, 30),
                     CreateRichText("PSI ON TOP", GetRainbowColor()),
                     titleStyle);
        }

        private void DrawSidePanel(GUIStyle buttonStyle)
        {
            var enhancedStyle = CreateEnhancedButtonStyle();
            float startY = window.y + 45f;
            float buttonSpacing = 42f;

            GUI.color = new Color(sidePanelColor.r, sidePanelColor.g, sidePanelColor.b, 0.7f);
            GUI.Box(new Rect(window.x, window.y, sidePanelWidth, window.height), "", CreateCustomStyle(null));

            string[] mainCategories = { "Mods", "Performance", "Room", "Settings" };
            for (int i = 0; i < mainCategories.Length; i++)
            {
                float yPos = startY + (i * buttonSpacing);
                var rect = new Rect(window.x + 10f, yPos, sidePanelWidth - 20f, 35f);

                bool isSelected = currentMainCategory == (MainCategory)(i + 1);
                GUI.color = isSelected ? GetRainbowColor() : new Color(sidePanelColor.r, sidePanelColor.g, sidePanelColor.b, 0.7f);

                if (GUI.Button(rect, CreateRichText(mainCategories[i], GUI.color), enhancedStyle))
                {
                    HandleCategorySelection(i);
                }
            }

            DrawCategoryContent(startY, buttonSpacing, enhancedStyle);
        }

        private void HandleCategorySelection(int index)
        {
            StartButtonAnimation();
            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(Main.ActuallSound, false, 0.1f);
            currentMainCategory = (MainCategory)(index + 1);
            if (currentMainCategory == MainCategory.Mods)
            {
                showModCategories = true;
            }
        }

        private void DrawCategoryContent(float startY, float buttonSpacing, GUIStyle style)
        {
            float contentX = window.x + sidePanelWidth;
            switch (currentMainCategory)
            {
                case MainCategory.Performance:
                    DrawPerformanceInfo(contentX, startY, buttonSpacing, style);
                    break;
                case MainCategory.Mods when showModCategories:
                    DrawModCategories(contentX, startY, buttonSpacing, style);
                    break;
                case MainCategory.Room:
                    DrawRoomControls(contentX, startY, buttonSpacing, style);
                    break;
                case MainCategory.Settings:
                    DrawSettingsControls(contentX, startY, buttonSpacing, style);
                    break;
            }
        }

        private void DrawSnowflakes()
        {
            foreach (var snow in snowflakes)
            {
                snow.y += snow.speed;
                if (snow.y > window.height) snow.y = 0;

                float snowX = window.x + (snow.x % window.width);
                float snowY = window.y + snow.y;

                if (window.Contains(new Vector2(snowX, snowY)))
                {
                    Matrix4x4 matrix = GUI.matrix;
                    GUIUtility.RotateAroundPivot(snow.rotation, new Vector2(snowX + snow.size / 2, snowY + snow.size / 2));
                    GUI.color = new Color(1, 1, 1, snow.alpha * 0.7f);
                    GUI.DrawTexture(new Rect(snowX, snowY, snow.size, snow.size), Texture2D.whiteTexture);
                    GUI.matrix = matrix;
                }
                snow.rotation += Time.deltaTime * 50f;
            }
        }

        private void DrawBackButton(GUIStyle buttonStyle)
        {
            float backButtonY = window.y + window.height - 600f;
            if (GUI.Button(new Rect(window.x + sidePanelWidth + 10f, backButtonY,
                window.width - sidePanelWidth - 20f, 35f),
                CreateRichText("Back", GetRainbowColor()), buttonStyle))
            {
                ResetCategories();
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(Main.ActuallSound, false, 0.1f);
            }
        }

        private void ResetCategories()
        {
            showCategories = true;
            currentCategory = "";
            currentMainCategory = MainCategory.Mods;
            showModCategories = false;
        }

        private string roomCodeInput = string.Empty;
        private void DrawRoomControls(float x, float startY, float buttonSpacing, GUIStyle style)
        {
            var roomStyle = new GUIStyle(style);
            roomStyle.normal.textColor = GetRainbowColor();
            roomStyle.fontSize = 16;

            GUI.Label(new Rect(x + 10f, startY + 20f, window.width - sidePanelWidth - 20f, 35f),
                CreateRichText("Enter Room Code:", GetRainbowColor()), roomStyle);

            GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);
            roomCodeInput = GUI.TextField(new Rect(x + 10f, startY + 65f,
                window.width - sidePanelWidth - 20f, 35f), roomCodeInput, roomStyle);

            if (GUI.Button(new Rect(x + 10f, startY + 120f,
                window.width - sidePanelWidth - 20f, 35f),
                CreateRichText("Join Room", GetRainbowColor()), style))
            {
                if (!string.IsNullOrEmpty(roomCodeInput))
                {
                    PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(roomCodeInput, JoinType.Solo);
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(Main.ActuallSound, false, 0.1f);
                }
            }
        }

        private void DrawModCategories(float x, float startY, float buttonSpacing, GUIStyle style)
        {
            for (int i = 2; i <= 13; i++)
            {
                try
                {
                    float yPos = startY + ((i - 2) * buttonSpacing);
                    var rect = new Rect(x + 10f, yPos, window.width - sidePanelWidth - 20f, 35f);

                    Color buttonColor = GetRainbowColor();
                    buttonColor.a = 0.7f;
                    if (rect.Contains(Event.current.mousePosition))
                    {
                        buttonColor.a = buttonHoverAlpha;
                    }

                    if (GUI.Button(rect, CreateRichText(ModButtons.buttons[i].buttonText, buttonColor), style))
                    {
                        HandleModCategorySelection(i);
                    }
                }
                catch { }
            }
        }

        private void HandleModCategorySelection(int index)
        {
            StartButtonAnimation();
            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(Main.ActuallSound, false, 0.1f);

            if (index == 2)
                buttons = GetButtonInfoByPage(Category.Settings);
            else if (index == 3)
                buttons = GetButtonInfoByPage(Category.Room);
            else if (index == 4)
                buttons = GetButtonInfoByPage(Category.Safety);
            else if (index == 5)
                buttons = GetButtonInfoByPage(Category.Move);
            else if (index == 6)
                buttons = GetButtonInfoByPage(Category.Player);
            else if (index == 7)
                buttons = GetButtonInfoByPage(Category.Visuals);
            else if (index == 8)
                buttons = GetButtonInfoByPage(Category.World);
            else if (index == 9)
                buttons = GetButtonInfoByPage(Category.Fun);
            else if (index == 10)
                buttons = GetButtonInfoByPage(Category.Guardian);
            else if (index == 11)
                buttons = GetButtonInfoByPage(Category.OP);
            else if (index == 12)
                buttons = GetButtonInfoByPage(Category.Experimental);
            else if (index == 13)
                buttons = GetButtonInfoByPage(Category.Creds);

            currentCategory = $"Category {index - 2}";
            showCategories = false;
            showModCategories = false;
        }

        private void DrawPerformanceInfo(float x, float startY, float buttonSpacing, GUIStyle style)
        {
            var performanceStyle = new GUIStyle(style)
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleLeft
            };

            string[] performanceMetrics = {
                $"FPS: {Mathf.Round(1f / Time.deltaTime)}",
                $"Memory: {System.GC.GetTotalMemory(false) / 1048576}MB",
                $"Ping: {PhotonNetwork.GetPing()}ms",
                $"Players Online: {PhotonNetwork.CurrentRoom?.PlayerCount ?? 0}",
                $"Frame Time: {(Time.deltaTime * 1000f).ToString("F1")}ms"
            };

            for (int i = 0; i < performanceMetrics.Length; i++)
            {
                GUI.Label(new Rect(x + 10f, startY + buttonSpacing * i, window.width - sidePanelWidth - 20f, 35f),
                    CreateRichText(performanceMetrics[i], GetRainbowColor()), performanceStyle);
            }
        }

        private void DrawToggles(GUIStyle buttonStyle)
        {
            float startY = window.y + 45f;
            float buttonSpacing = 42f;
            float scrollViewHeight = window.height - 145f;

            scrollPosition = GUI.BeginScrollView(
                new Rect(window.x + sidePanelWidth, startY, window.width - sidePanelWidth, scrollViewHeight),
                scrollPosition,
                new Rect(0, 0, window.width - sidePanelWidth - 40f, buttons.Count * buttonSpacing)
            );

            for (int i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];
                var buttonRect = new Rect(10f, i * buttonSpacing, window.width - sidePanelWidth - 40f, 35f);

                Color buttonColor = button.Enabled ? GetRainbowColor() : Color.white;
                buttonColor.a = 0.7f;
                if (buttonRect.Contains(Event.current.mousePosition))
                {
                    buttonColor.a = buttonHoverAlpha;
                }

                if (GUI.Button(buttonRect, CreateRichText(button.buttonText, buttonColor), buttonStyle))
                {
                    HandleToggleButton(button);
                }
            }

            GUI.EndScrollView();
        }

        private void HandleToggleButton(ButtonHandler.Button button)
        {
            button.Enabled = !button.Enabled;
            if (button.Enabled)
            {
                button.onEnable.Invoke();
            }
            else
            {
                button.onDisable.Invoke();
            }
            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(Main.ActuallSound, false, 0.1f);
        }

        float disconnectButtonY;
        private void DrawMainButtons(GUIStyle buttonStyle)
        {
            var enhancedStyle = CreateEnhancedButtonStyle();
            disconnectButtonY = window.y + window.height - 45f;

            var buttonRect = new Rect(window.x + sidePanelWidth + 10f, disconnectButtonY,
                window.width - sidePanelWidth - 20f, 35f);

            Color buttonColor = GetRainbowColor();
            buttonColor.a = 0.7f;
            if (buttonRect.Contains(Event.current.mousePosition))
            {
                buttonColor.a = buttonHoverAlpha;
            }

            if (GUI.Button(buttonRect, CreateRichText("Disconnect", buttonColor), enhancedStyle))
            {
                StartButtonAnimation();
                if (PhotonNetwork.InRoom)
                {
                    PhotonNetwork.Disconnect();
                }
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(Main.ActuallSound, false, 0.1f);
            }
        }

        private void StartButtonAnimation()
        {
            isAnimating = true;
            targetAlpha = 0.85f;
            currentAlpha = 1f;
        }

        public void Update()
        {
            if (UnityInput.Current.GetKeyDown(OpenAndCloseGUI))
            {
                open = !open;
                isAnimating = true;
                targetAlpha = open ? 1f : 0f;
            }

            UpdateSnowflakes();
        }

        private void UpdateSnowflakes()
        {
            foreach (var snow in snowflakes)
            {
                snow.rotation += Time.deltaTime * 50f;
                if (snow.rotation >= 360f) snow.rotation = 0f;
            }
        }

        private void HandleDragging()
        {
            if (Event.current.type == EventType.MouseDown && window.Contains(Event.current.mousePosition))
            {
                dragging = true;
                dragstart = Event.current.mousePosition - new Vector2(window.x, window.y);
            }
            else if (Event.current.type == EventType.MouseUp)
            {
                dragging = false;
            }

            if (dragging)
            {
                Vector2 newPosition = Event.current.mousePosition - dragstart;
                newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width - window.width);
                newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height - window.height);
                targetPosition = newPosition;
            }
        }

        private void HandleFadeAnimation()
        {
            if (isAnimating)
            {
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * animationSpeed);
                if (Mathf.Abs(currentAlpha - targetAlpha) < 0.01f)
                {
                    currentAlpha = targetAlpha;
                    isAnimating = false;
                }
            }
        }

        private GUIStyle CreateCustomStyle(Texture2D backgroundTexture)
        {
            var style = new GUIStyle(GUI.skin.box);
            style.normal.background = CreateRoundedTexture(cornerRadius, true);
            style.border = new RectOffset(cornerRadius, cornerRadius, cornerRadius, cornerRadius);
            style.margin = new RectOffset(4, 4, 4, 4);
            style.padding = new RectOffset(4, 4, 4, 4);
            return style;
        }

        private void DrawWindowFrame(GUIStyle style)
        {
            GUI.color = new Color(mainBackgroundColor.r, mainBackgroundColor.g, mainBackgroundColor.b, 0.7f);
            GUI.Box(new Rect(window.x, window.y, window.width, window.height), "", style);
            GUI.backgroundColor = new Color(mainBackgroundColor.r, mainBackgroundColor.g, mainBackgroundColor.b, 0.7f);
            GUI.contentColor = GetRainbowColor();
        }

        private static GUIStyle CreateEnhancedButtonStyle()
        {
            var style = new GUIStyle(GUI.skin.button);

            style.normal.background = CreateNovaButtonTexture(4, new Color(0, 0, 0, 0.7f), 0.5f);
            style.hover.background = CreateNovaButtonTexture(4, new Color(0, 0, 0, 0.7f), 0.7f);
            style.active.background = CreateNovaButtonTexture(4, new Color(0, 0, 0, 0.7f), 0.3f);

            style.normal.textColor = Color.white;
            style.hover.textColor = Color.white;
            style.active.textColor = Color.white;

            style.border = new RectOffset(4, 4, 4, 4);
            style.margin = new RectOffset(5, 5, 5, 5);
            style.padding = new RectOffset(8, 8, 8, 8);
            style.fontSize = 14;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;

            return style;
        }

        private string CreateRichText(string text, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>";
        }

        private static Texture2D CreateRoundedTexture(int radius, bool border)
        {
            int size = radius * 2 + 1;
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), new Vector2(radius, radius));
                    if (distance <= radius)
                    {
                        float alpha = border ?
                            (distance > radius - 1f ? 0.5f : 0.3f) :
                            Mathf.Lerp(0.7f, 0.5f, distance / radius);
                        texture.SetPixel(x, y, new Color(0, 0, 0, alpha));
                    }
                    else
                    {
                        texture.SetPixel(x, y, Color.clear);
                    }
                }
            }
            texture.Apply();
            return texture;
        }

        private static Texture2D CreateNovaButtonTexture(int radius, Color baseColor, float intensity)
        {
            int size = radius * 2 + 1;
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            Color accentColor = new Color(0.2f, 0.2f, 0.2f, 0.7f);

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), new Vector2(radius, radius));
                    if (distance <= radius)
                    {
                        float alpha = Mathf.Lerp(intensity, intensity * 0.9f, distance / radius);
                        Color pixelColor = Color.Lerp(baseColor, accentColor, distance / radius * 0.5f);
                        texture.SetPixel(x, y, new Color(pixelColor.r, pixelColor.g, pixelColor.b, alpha));
                    }
                    else
                    {
                        texture.SetPixel(x, y, Color.clear);
                    }
                }
            }
            texture.Apply();
            return texture;
        }

        private void DrawSettingsControls(float x, float startY, float buttonSpacing, GUIStyle style)
        {
            float currentY = startY;

            GUI.Label(new Rect(x + 10f, currentY, window.width - sidePanelWidth - 20f, 35f),
                CreateRichText("Corner Radius: " + cornerRadius, Color.white), style);
            cornerRadius = (int)GUI.HorizontalSlider(new Rect(x + 10f, currentY + 35f,
                window.width - sidePanelWidth - 20f, 20f), cornerRadius, 1, 10);

            currentY += buttonSpacing * 2;

            GUI.Label(new Rect(x + 10f, currentY, window.width - sidePanelWidth - 20f, 35f),
                CreateRichText("Animation Speed: " + animationSpeed.ToString("F1"), Color.white), style);
            animationSpeed = GUI.HorizontalSlider(new Rect(x + 10f, currentY + 35f,
                window.width - sidePanelWidth - 20f, 20f), animationSpeed, 1f, 10f);

            currentY += buttonSpacing * 2;

            GUI.Label(new Rect(x + 10f, currentY, window.width - sidePanelWidth - 20f, 35f),
                CreateRichText("Window Opacity: " + (targetAlpha * 100).ToString("F0") + "%", Color.white), style);
            targetAlpha = GUI.HorizontalSlider(new Rect(x + 10f, currentY + 35f,
                window.width - sidePanelWidth - 20f, 20f), targetAlpha, 0.1f, 1f);
        }
    }
}
*/