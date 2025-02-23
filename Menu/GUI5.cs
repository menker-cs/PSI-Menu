using System;
using System.Collections.Generic;
using BepInEx;
using UnityEngine;
using UnityEngine.XR;
using MenkerMenu.Utilities;
using MenkerMenu.Mods;
using System.Linq;
using MenkerMenu.Menu;
using MenkerMenu.Mods;
using MenkerMenu.Utilities;
using UnityEngine.PlayerLoop;
using Oculus.Platform;
using UnityEngine.Playables;

namespace PsiTemp.Menu
{
    [BepInPlugin("org.psi.ui.com", "GayrillaTag", "1.3.0")]
    public class PSIUI : BaseUnityPlugin
    {
        private static bool isUIShown = true;
        public static Texture2D ButtonTexture = new Texture2D(2, 2);
        public static Texture2D ButtonHoverTexture = new Texture2D(2, 2);
        public static Texture2D ButtonClickTexture = new Texture2D(2, 2);
        public static Texture2D Background = new Texture2D(2, 2);
        public static Texture2D scrollTex = new Texture2D(2, 2);
        public static Vector2 Scroller = Vector2.zero;
        public static GUITab CurrentTab = GUITab.Home;
        public static float deltaTime;
        private string searchString = "Search For Modules Here";
        public enum GUITab
        {
            Home, Modules, Search
        }
        public static Texture2D ApplyTexture(Color color)
        {
            Texture2D texture2D = new Texture2D(30, 30);
            Color[] array = new Color[900];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = color;
            }
            texture2D.SetPixels(array);
            texture2D.Apply();
            return texture2D;
        }
        public static void SendNotification(string text)
        {
            notifications.Add(new Notification { Text = text, Timer = 4.5f, Rect = new Rect(-400, Screen.height - 60 - (notifications.Count * 53.5f), 255, 50), State = NotificationState.In });
            for (int i = 0; i < notifications.Count; i++)
                notifications[i].Rect.y = Screen.height - 60 - (i * 53.5f);
        }
        private enum NotificationState { In, Visible, Out }
        private static List<Notification> notifications = new List<Notification>();
        private Texture2D NotificationTexture, OutlineTexture;
        private class Notification
        {
            public string Text;
            public float Timer;
            public Rect Rect;
            public NotificationState State;
        }
        private void Start() => ChangeUITheme(NotificationColor, OutlineColor);
        public Color NotificationColor = ColorLib.back, OutlineColor = ColorLib.DarkDodgerBlue;
        public void ChangeUITheme(Color Notification, Color Outline)
        {
            NotificationTexture = new Texture2D(1, 1);
            NotificationTexture.SetPixel(0, 0, Notification);
            OutlineTexture = new Texture2D(1, 1);
            OutlineTexture.SetPixel(0, 0, Outline);
            foreach (Texture2D texture in new Texture2D[] { NotificationTexture, OutlineTexture })
                texture.Apply();
        }
        public void Update()
        {
            for (int i = 0; i < notifications.Count; i++)
            {
                Notification notification = notifications[i];
                switch (notification.State)
                {
                    case NotificationState.In:
                        notification.Rect.x = Mathf.Min(notification.Rect.x + 800 * Time.deltaTime, 10);
                        if (notification.Rect.x >= 10)
                        {
                            notification.State = NotificationState.Visible;
                            notification.Timer = 4.5f;
                        }
                        break;
                    case NotificationState.Visible:
                        notification.Timer -= Time.deltaTime;
                        if (notification.Timer <= 0)
                            notification.State = NotificationState.Out;
                        break;
                    case NotificationState.Out:
                        notification.Rect.x = Mathf.Max(notification.Rect.x - 800 * Time.deltaTime, -400);
                        if (notification.Rect.x <= -400)
                            notifications.RemoveAt(i--);
                        break;
                }
            }

            if (UnityInput.Current.GetKeyDown(KeyCode.RightShift))
            {
                isUIShown = !isUIShown;
            }

            ButtonTexture = ApplyTexture(new Color32(28, 28, 28, byte.MaxValue));
            ButtonHoverTexture = ApplyTexture(new Color32(38, 38, 38, byte.MaxValue));
            ButtonClickTexture = ApplyTexture(new Color32(48, 48, 48, byte.MaxValue));
            Background = ApplyTexture(new Color32(18, 18, 18, byte.MaxValue));
            scrollTex = ApplyTexture(Color.clear);
        }
        public void OnGUI()
        {
            Material colorMaterial = new Material(Shader.Find("GUI/Text Shader"))
            {
                color = Color.Lerp(ColorLib.SkyBlue, new Color32(8, 90, 177, byte.MaxValue), Mathf.PingPong(Time.time, 1.5f))
            };
            colorMaterial.SetFloat("_Mode", 2f);

            float ols = 1.17f;
            foreach (Notification notification in notifications)
            {
                GUI.DrawTexture(notification.Rect, NotificationTexture);
                GUI.DrawTexture(new Rect(notification.Rect.x, notification.Rect.y, ols, notification.Rect.height), OutlineTexture);
                GUI.DrawTexture(new Rect(notification.Rect.x + notification.Rect.width - ols, notification.Rect.y, ols, notification.Rect.height), OutlineTexture);
                GUI.DrawTexture(new Rect(notification.Rect.x, notification.Rect.y, notification.Rect.width, ols), OutlineTexture);
                GUI.DrawTexture(new Rect(notification.Rect.x, notification.Rect.y + 10, notification.Rect.width, ols), OutlineTexture);
                GUI.Label(new Rect(notification.Rect.x + .75f, notification.Rect.y - 19.9f, notification.Rect.width, notification.Rect.height), "Psi Menu Notification", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontSize = 9, fontStyle = FontStyle.Bold, normal = { textColor = colorMaterial.color } });
                GUI.Label(new Rect(notification.Rect.x - .75f, notification.Rect.y - 19.9f, notification.Rect.width, notification.Rect.height), $"{DateTime.Now.ToShortTimeString()}", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight, fontSize = 9, fontStyle = FontStyle.Bold, normal = { textColor = colorMaterial.color } });
                GUI.DrawTexture(new Rect(notification.Rect.x, notification.Rect.y + notification.Rect.height - ols, notification.Rect.width, ols), OutlineTexture);
                GUI.Label(new Rect(notification.Rect.x, notification.Rect.y + 4.5f, notification.Rect.width, notification.Rect.height), notification.Text, new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 12, fontStyle = FontStyle.Normal, normal = { textColor = colorMaterial.color } });
            }

            if (!XRSettings.isDeviceActive)
            {
                if (isUIShown)
                {
                    GUIRect = GUI.Window(1, GUIRect, new GUI.WindowFunction(MainGUI), "Psi Menu");
                }
                else { TextThingy(); }
                Watermark();
                DrawArrayLists();
            }
            else
            {
                Watermark();
                DrawArrayLists();
            }
        }
        public void MainGUI(int WindowId)
        {
            try
            {
                SetTextures();
                deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
                GUI.DrawTexture(new Rect(0f, 0f, GUIRect.width, GUIRect.height), Background, ScaleMode.StretchToFill, false, 0f, GUI.color, Vector4.zero, new Vector4(6f, 6f, 6f, 6f));
                GUI.DrawTexture(new Rect(0f, 0f, 100f, GUIRect.height), Background, ScaleMode.StretchToFill, false, 0f, GUI.color, Vector4.zero, new Vector4(6f, 0f, 0f, 6f));

                GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
                DrawTab(GUITab.Home);
                DrawTab(GUITab.Modules);
                DrawTab(GUITab.Search);
                GUILayout.EndHorizontal();
                GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
                GUILayout.Label(EnumUtilExt.GetName<GUITab>(CurrentTab), Array.Empty<GUILayoutOption>());
                GUILayout.Space(5f);
                switch (CurrentTab)
                {
                    case GUITab.Home:
                        DrawHome();
                        break;
                    case GUITab.Modules:
                        DrawModules();
                        break;
                    case GUITab.Search:
                        DrawSearch();
                        break;
                }
                GUILayout.EndVertical();
                GUI.DragWindow();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                throw;
            }
        }
        public Rect GUIRect = new Rect(10f, 10f, 500f, 400f);
        public void DrawTab(GUITab _GUITab)
        {
            GUILayoutOption[] gUILayoutOptions = new GUILayoutOption[]
            {
                GUILayout.Width(GUIRect.width / 3.2f),
                GUILayout.Height(27f)
            };
            Material buttonMaterial = new Material(Shader.Find("GUI/Text Shader"))
            {
                color = Color.Lerp(ColorLib.SkyBlue, new Color32(8, 90, 177, byte.MaxValue), Mathf.PingPong(Time.time, 1.5f))
            };
            buttonMaterial.SetFloat("_Mode", 2f);

            GUI.contentColor = buttonMaterial.color;
            if (RoundedTabButton(EnumUtilExt.GetName<GUITab>(_GUITab), gUILayoutOptions))
            {
                CurrentTab = _GUITab;
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, false, 0.4f);
            }
            GUI.contentColor = buttonMaterial.color;
        }
        public void DrawHome()
        {
            GUI.DrawTexture(new Rect(8f, 80f, 482f, 310f), ButtonTexture, ScaleMode.StretchToFill, false, 0f, GUI.color, Vector4.zero, new Vector4(2.8f, 2.8f, 2.8f, 2.8f));

            string welcomeMessage =
            "Right Shift To Hide UI\n" +
            "╔════════════════════════════════╗\n" +
            "                      Welcome To Psi Menu\n" +
            "╚════════════════════════════════╝\n" +
            "Miscellaneous\n" +
            " - Credits\n" +
            "     - Menker | Owner\n" +
            "     - Nova | Mod Developer\n" +
            "     - Revenant/f3 | GUI Developer\n" +
            " - Updates\n" +
            "     - Enable Hoverboard [SS]\n" +
            "     - UI Redesign";

            GUILayout.Label(welcomeMessage);
        }
        public void DrawModules()
        {
            GUILayout.EndVertical();

            Scroller = GUILayout.BeginScrollView(Scroller, GUILayout.Width(GUIRect.width - 34f), GUILayout.Height(GUIRect.height - 134f));
            GUILayout.BeginVertical();

            List<ButtonHandler.Button> list = new List<ButtonHandler.Button>();

            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Room));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Safety));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Move));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Player));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Visuals));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.World));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Fun));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Draw));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Guardian));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.OP));
            foreach (ButtonHandler.Button buttonInfo in list)
            {
                string buttonLabel = $"{(buttonInfo.Enabled ? "Enabled ● " : "Disabled ● ")}{buttonInfo.buttonText}{(buttonInfo.Enabled ? " ●" : " ●")}";
                if (RoundedButton(buttonLabel))
                {
                    ButtonHandler.ToggleButton(buttonInfo);
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, false, 0.4f);
                }
            }

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.BeginVertical();
        }
        public void DrawSearch()
        {
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            searchString = GUILayout.TextField(searchString, new GUILayoutOption[]
            {
                GUILayout.Width(GUIRect.width - 25f)
            });
            GUILayout.EndHorizontal();
            GUIStyle invisibleScrollbar = new GUIStyle(GUI.skin.verticalScrollbar);
            invisibleScrollbar.normal.background = null;
            invisibleScrollbar.hover.background = null;
            invisibleScrollbar.active.background = null;
            invisibleScrollbar.onNormal.background = null;
            invisibleScrollbar.onHover.background = null;
            invisibleScrollbar.onActive.background = null;
            invisibleScrollbar.fixedWidth = 0f;
            Scroller = GUILayout.BeginScrollView(Scroller, invisibleScrollbar, GUILayout.Width(GUIRect.width - 34f), GUILayout.Height(GUIRect.height - 134f));

            List<ButtonHandler.Button> list = new List<ButtonHandler.Button>();
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Room).Where(b => !string.IsNullOrEmpty(b.buttonText) && b.buttonText.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Safety).Where(b => !string.IsNullOrEmpty(b.buttonText) && b.buttonText.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Move).Where(b => !string.IsNullOrEmpty(b.buttonText) && b.buttonText.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Player).Where(b => !string.IsNullOrEmpty(b.buttonText) && b.buttonText.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Visuals).Where(b => !string.IsNullOrEmpty(b.buttonText) && b.buttonText.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.World).Where(b => !string.IsNullOrEmpty(b.buttonText) && b.buttonText.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Fun).Where(b => !string.IsNullOrEmpty(b.buttonText) && b.buttonText.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Draw).Where(b => !string.IsNullOrEmpty(b.buttonText) && b.buttonText.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.Guardian).Where(b => !string.IsNullOrEmpty(b.buttonText) && b.buttonText.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(Category.OP).Where(b => !string.IsNullOrEmpty(b.buttonText) && b.buttonText.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));

            foreach (ButtonHandler.Button buttonInfo in list)
            {
                string buttonLabel = $"{(buttonInfo.Enabled ? "Enabled ● " : "Disabled ● ")}{buttonInfo.buttonText}{(buttonInfo.Enabled ? " ●" : " ●")}";
                if (RoundedButton(buttonLabel))
                {
                    ButtonHandler.ToggleButton(buttonInfo);
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, false, 0.4f);
                }
            }
            GUILayout.EndScrollView();
            GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
        }
        public void DrawArrayLists()
        {
            Material colorMaterial = new Material(Shader.Find("GUI/Text Shader"))
            {
                color = Color.Lerp(ColorLib.SkyBlue, new Color32(8, 90, 177, byte.MaxValue), Mathf.PingPong(Time.time, 1.5f))
            };
            colorMaterial.SetFloat("_Mode", 2f);

            GUIStyle titleStyle = new GUIStyle(GUI.skin.box)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                richText = true,
                wordWrap = false,
                normal = { textColor = colorMaterial.color }
            };

            GUILayout.Space(3);

            string titleText = MenkerMenu.Initialization.PluginInfo.menuName + " " + MenkerMenu.Initialization.PluginInfo.menuVersion;
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label(titleText, titleStyle, GUILayout.Width(titleStyle.CalcSize(new GUIContent(titleText)).x + 10f), GUILayout.Height(30f));
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.box)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                richText = true,
                wordWrap = false,
                normal = { textColor = colorMaterial.color }
            };

            foreach (ButtonHandler.Button button in ModButtons.buttons)
            {
                if (button.Enabled)
                {
                    float buttonWidth = buttonStyle.CalcSize(new GUIContent(button.buttonText)).x + 10f;
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    GUILayout.Label(button.buttonText, buttonStyle, GUILayout.Width(buttonWidth), GUILayout.Height(30f));
                    GUILayout.EndHorizontal();
                    GUILayout.Space(1);
                }
            }
        }
        public static bool RoundedButton(Rect rect, string content, params GUILayoutOption[] options)
        {
            Texture2D texture = ButtonTexture;
            if (rect.Contains(Event.current.mousePosition))
            {
                texture = ButtonHoverTexture;
            }
            if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
            {
                texture = ButtonClickTexture;
                return true;
            }
            DrawTexture(rect, texture, 6, default(Vector4));
            DrawText(new Rect(rect.x, rect.y - 3f, rect.width, 25f), content, 12, Color.white, FontStyle.Normal, true, true);
            return false;
        }
        public static bool RoundedButton(string content, params GUILayoutOption[] options)
        {
            Texture2D texture = ButtonTexture;
            Rect rect = GUILayoutUtility.GetRect(new GUIContent(content), GUI.skin.button, options);
            if (rect.Contains(Event.current.mousePosition))
            {
                texture = ButtonHoverTexture;
            }
            if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
            {
                texture = ButtonClickTexture;
                return true;
            }
            DrawTexture(rect, texture, 6, default(Vector4));
            DrawText(new Rect(rect.x, rect.y - 3f, rect.width, 25f), content, 12, Color.white, FontStyle.Normal, true, true);
            return false;
        }
        public static bool RoundedTabButton(string content, params GUILayoutOption[] options)
        {
            Texture2D texture = ButtonTexture;
            Rect rect = GUILayoutUtility.GetRect(new GUIContent(content), GUI.skin.button, options);
            if (rect.Contains(Event.current.mousePosition))
            {
                texture = ButtonHoverTexture;
            }
            if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
            {
                texture = ButtonClickTexture;
                return true;
            }
            DrawTexture(rect, texture, 6, default(Vector4));
            DrawTabText(new Rect(rect.x, rect.y - 3f, rect.width, 25f), content, 12, Color.white, FontStyle.Normal, true, true);
            return false;
        }
        private static void DrawTexture(Rect rect, Texture2D texture, int borderRadius, Vector4 borderRadius4 = default(Vector4))
        {
            if (borderRadius4 == Vector4.zero)
            {
                borderRadius4 = new Vector4((float)borderRadius, (float)borderRadius, (float)borderRadius, (float)borderRadius);
            }
            GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, false, 0f, GUI.color, Vector4.zero, borderRadius4);
        }
        public static void DrawText(Rect rect, string text, int fontSize = 12, Color textColor = default(Color), FontStyle fontStyle = FontStyle.Normal, bool centerX = false, bool centerY = true)
        {
            GUIStyle guistyle = new GUIStyle(GUI.skin.label);
            guistyle.fontSize = fontSize;
            guistyle.fontStyle = FontStyle.Bold;
            guistyle.normal.textColor = textColor;
            float x = centerX ? (rect.x + rect.width / 2f - guistyle.CalcSize(new GUIContent(text)).x / 2f) : rect.x;
            float y = centerY ? (rect.y + rect.height / 2f - guistyle.CalcSize(new GUIContent(text)).y / 2f) : rect.y;
            GUI.Label(new Rect(x, y, rect.width, rect.height), new GUIContent(text), guistyle);
        }
        public static void DrawTabText(Rect rect, string text, int fontSize = 12, Color textColor = default(Color), FontStyle fontStyle = FontStyle.Normal, bool centerX = false, bool centerY = true)
        {
            GUIStyle guistyle = new GUIStyle(GUI.skin.label);
            guistyle.fontSize = fontSize;
            guistyle.fontStyle = FontStyle.Bold;
            guistyle.normal.textColor = textColor;
            guistyle.alignment = TextAnchor.MiddleLeft;
            float x = centerX ? (rect.x + rect.width / 2f - guistyle.CalcSize(new GUIContent(text)).x / 2f) : rect.x;
            float y = centerY ? (rect.y + rect.height / 2f - guistyle.CalcSize(new GUIContent(text)).y / 2f) : rect.y;
            GUI.Label(new Rect(x, y, rect.width, rect.height), new GUIContent(text), guistyle);
        }
        public void Watermark()
        {
            Material buttonMaterial = new Material(Shader.Find("GUI/Text Shader"))
            {
                color = Color.Lerp(ColorLib.SkyBlue, new Color32(8, 90, 177, byte.MaxValue), Mathf.PingPong(Time.time, 1.5f))
            };
            buttonMaterial.SetFloat("_Mode", 2f);

            string labelText = "Psi Menu";
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.fontSize = 55;
            labelStyle.normal.textColor = buttonMaterial.color;
            float labelWidth = labelStyle.CalcSize(new GUIContent(labelText)).x;
            float labelHeight = labelStyle.CalcSize(new GUIContent(labelText)).y;
            float labelX = Screen.width - labelWidth - 50;
            float labelY = Screen.height - labelHeight - 50;
            GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), labelText, labelStyle);
        }
        public void TextThingy()
        {
            Material buttonMaterial = new Material(Shader.Find("GUI/Text Shader"))
            {
                color = Color.Lerp(ColorLib.SkyBlue, new Color32(8, 90, 177, byte.MaxValue), Mathf.PingPong(Time.time, 1.5f))
            };
            buttonMaterial.SetFloat("_Mode", 2f);

            string labelText = "Right Shift To Show UI";
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.fontSize = 30;
            labelStyle.normal.textColor = buttonMaterial.color;
            float labelWidth = labelStyle.CalcSize(new GUIContent(labelText)).x;
            float labelHeight = labelStyle.CalcSize(new GUIContent(labelText)).y;
            float labelX = Screen.width - labelWidth - 25;
            float labelY = Screen.height - labelHeight - 25;
            GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), labelText, labelStyle);
        }
        public static void SetTextures()
        {
            GUI.skin.label.richText = true;
            GUI.skin.button.richText = true;
            GUI.skin.window.richText = true;
            GUI.skin.textField.richText = true;
            GUI.skin.box.richText = true;
            GUI.skin.window.border.bottom = 5;
            GUI.skin.window.border.left = 5;
            GUI.skin.window.border.top = 5;
            GUI.skin.window.border.right = 5;
            GUI.skin.window.active.background = null;
            GUI.skin.window.normal.background = null;
            GUI.skin.window.hover.background = null;
            GUI.skin.window.focused.background = null;
            GUI.skin.window.onFocused.background = null;
            GUI.skin.window.onActive.background = null;
            GUI.skin.window.onHover.background = null;
            GUI.skin.window.onNormal.background = null;
            GUI.skin.button.active.background = ButtonClickTexture; ;
            GUI.skin.button.active.textColor = Color.cyan; ;
            GUI.skin.button.normal.background = ButtonHoverTexture;
            GUI.skin.button.hover.background = ButtonTexture;
            GUI.skin.button.hover.textColor = Color.cyan;
            GUI.skin.button.onActive.background = ButtonClickTexture;
            GUI.skin.button.onActive.textColor = Color.cyan;
            GUI.skin.button.onHover.textColor = Color.cyan;
            GUI.skin.button.onHover.background = ButtonHoverTexture;
            GUI.skin.button.onNormal.background = ButtonTexture;

            GUI.skin.verticalScrollbar.active.background = Background;
            GUI.skin.verticalScrollbar.normal.background = Background;
            GUI.skin.verticalScrollbar.hover.background = Background;
            GUI.skin.verticalScrollbar.focused.background = Background;
            GUI.skin.verticalScrollbar.onFocused.background = Background;
            GUI.skin.verticalScrollbar.onActive.background = Background;
            GUI.skin.verticalScrollbar.onHover.background = Background;
            GUI.skin.verticalScrollbar.onNormal.background = Background;

            GUI.skin.verticalScrollbarThumb.active.background = Background;
            GUI.skin.verticalScrollbarThumb.normal.background = Background;
            GUI.skin.verticalScrollbarThumb.hover.background = Background;
            GUI.skin.verticalScrollbarThumb.focused.background = Background;
            GUI.skin.verticalScrollbarThumb.onFocused.background = Background;
            GUI.skin.verticalScrollbarThumb.onActive.background = Background;
            GUI.skin.verticalScrollbarThumb.onHover.background = Background;
            GUI.skin.verticalScrollbarThumb.onNormal.background = Background;

            GUI.skin.verticalScrollbar.border = new RectOffset(0, 0, 0, 0);
            GUI.skin.verticalScrollbar.fixedWidth = 0f;
            GUI.skin.verticalScrollbar.fixedHeight = 0f;
            GUI.skin.verticalScrollbarThumb.fixedHeight = 0f;
            GUI.skin.verticalScrollbarThumb.fixedWidth = 5f;
        }
    }
}