using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using UnityEngine;
using UnityEngine.XR;
using MenkerMenu.Utilities;
using MenkerMenu.Menu;
using MenkerMenu.Mods;
using MenkerMenu;
using MenkerMenu.Menu;
using MenkerMenu.Mods.Categories;
using MenkerMenu.Utilities;
using MenkerMenu.Initialization;

namespace QuantumNexus.Menu
{
    [BepInPlugin("org.Psi.ui.com", "GayrillaTag", "1.0.1")]
    public class QuantumNexusUI : BaseUnityPlugin
    {
        private string UIName = "Psi Menu";
        private static bool isUIShown = true;
        public static Texture2D ButtonTexture = new Texture2D(2, 2);
        public static Texture2D ButtonHoverTexture = new Texture2D(2, 2);
        public static Texture2D ButtonClickTexture = new Texture2D(2, 2);
        public static Texture2D Background = new Texture2D(2, 2);
        public static Vector2 Scroller = Vector2.zero;
        public Rect GUIRect = new Rect(10f, 10f, 500f, 400f);
        public static GUITab CurrentTab = GUITab.Home;
        public static float deltaTime;
        public static int Theme = 0;
        private string searchString = "Search For Modules Here";
        public Vector2[] scroll = new Vector2[30];
        public enum GUITab
        {
            Home, Modules, Search, Settings
        }
        public static void Category(int i)
        {
            Variables.currentCategoryPage = i;
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

        public void Update()
        {
            if (UnityInput.Current.GetKeyDown(KeyCode.RightShift))
            {
                isUIShown = !isUIShown;
            }

            switch (Theme)
            {
                case 0:
                    Material buttonMaterial = new Material(Shader.Find("GUI/Text Shader"))
                    {
                        color = Color.Lerp(ColorLib.SkyBlue, new Color32(8, 90, 177, byte.MaxValue), Mathf.PingPong(Time.time, 1.5f))
                    };
                    buttonMaterial.SetFloat("_Mode", 2f);

                    ButtonTexture = ApplyTexture(new Color32(28, 28, 28, byte.MaxValue));
                    ButtonHoverTexture = ApplyTexture(buttonMaterial.color);
                    ButtonClickTexture = ApplyTexture(buttonMaterial.color);
                    Background = ApplyTexture(new Color32(18, 18, 18, byte.MaxValue));
                    break;

                case 1:
                    ButtonTexture = ApplyTexture(ColorLib.Purple);
                    ButtonHoverTexture = ApplyTexture(ColorLib.Magenta);
                    ButtonClickTexture = ApplyTexture(ColorLib.Magenta);
                    Background = ApplyTexture(ColorLib.Purple);
                    break;

                case 2:
                    ButtonTexture = ApplyTexture(ColorLib.DarkBlue);
                    ButtonHoverTexture = ApplyTexture(ColorLib.Navy);
                    ButtonClickTexture = ApplyTexture(ColorLib.Navy);
                    Background = ApplyTexture(ColorLib.Blue);
                    break;

                case 3:
                    ButtonTexture = ApplyTexture(ColorLib.DarkOrange);
                    ButtonHoverTexture = ApplyTexture(ColorLib.Red);
                    ButtonClickTexture = ApplyTexture(ColorLib.Red);
                    Background = ApplyTexture(ColorLib.Orange);
                    break;
            }
        }
        int fps;
        public void OnGUI()
        {
            fps = (Time.deltaTime > 0) ? Mathf.RoundToInt(1.0f / Time.deltaTime) : 0;

            if (!XRSettings.isDeviceActive)
            {
                if (isUIShown)
                {
                    GUIRect = GUI.Window(2838, GUIRect, new GUI.WindowFunction(MainGUI), UIName);
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
                DrawTab(GUITab.Settings);
                GUILayout.EndHorizontal();
                GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
                if (CurrentTab != GUITab.Search)
                {
                    GUILayout.Label(EnumUtilExt.GetName<GUITab>(CurrentTab), Array.Empty<GUILayoutOption>());
                }
                GUILayout.Space(5f);
                switch (CurrentTab)
                {
                    case GUITab.Home:
                        DrawHome();
                        break;
                    case GUITab.Modules:
                        DrawModules();
                        break;
                    case GUITab.Settings:
                        DrawSettings();
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
        public void DrawTab(GUITab _GUITab)
        {
            Material buttonMaterial = new Material(Shader.Find("GUI/Text Shader"))
            {
                color = Color.Lerp(ColorLib.SkyBlue, new Color32(8, 90, 177, byte.MaxValue), Mathf.PingPong(Time.time, 1.5f))
            };
            buttonMaterial.SetFloat("_Mode", 2f);

            GUILayoutOption[] gUILayoutOptions = new GUILayoutOption[]
            {
                GUILayout.Width(GUIRect.width / 3.2f),
                GUILayout.Height(27f)
            };
            GUI.contentColor = buttonMaterial.color;
            if (RoundedTabButton(EnumUtilExt.GetName<GUITab>(_GUITab), gUILayoutOptions))
            {
                CurrentTab = _GUITab;
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, false, 0.8f);
            }
            GUI.contentColor = buttonMaterial.color;
        }
        public void DrawHome()
        {
            Material buttonMaterial = new Material(Shader.Find("GUI/Text Shader"))
            {
                color = Color.Lerp(ColorLib.SkyBlue, new Color32(8, 90, 177, byte.MaxValue), Mathf.PingPong(Time.time, 1.5f))
            };
            buttonMaterial.SetFloat("_Mode", 2f);

            GUI.contentColor = buttonMaterial.color;

            string welcomeMessage =
                "Right Shift To Hide UI\n\n" +
                "Welcome To " + UIName + "\n\n" +
                "Miscellaneous\n" +
                " - Developers\n" +
                "     - Menker | Owner\n" +
                "     - Nova | Mod Developer\n" +
                "     - Revenant | UI Developer\n" +
                " - Updates\n" +
                "     - New GUI\n" +
                "     - Draw/Orb Mods [CS]";

            GUILayout.Label(welcomeMessage, Array.Empty<GUILayoutOption>());
            GUI.contentColor = buttonMaterial.color;
        }
        public void DrawModules()
        {
            GUILayout.EndVertical();

            int num;
            foreach (ButtonHandler.Button btn in ModButtons.buttons)
            {
                num = btn.Enabled ? 1 : 0;
            }

            Scroller = GUILayout.BeginScrollView(Scroller, GUILayout.Width(GUIRect.width - 34f), GUILayout.Height(GUIRect.height - 134f));
            GUILayout.Label($"Enabled Modules: ");
            GUILayout.BeginVertical();
            if (Variables.currentCategoryPage != 0)
            {
                if (RoundedButton("Return", GUILayout.Width(GUIRect.width - 58f), GUILayout.Height(22f)))
                {
                    Category(0);

                }
            }

            GUILayout.Space(4.7f);
            List<ButtonHandler.Button> list = ButtonHandler.GetButtonInfoByPage(MenkerMenu.Mods.Category.Room);
            list.AddRange(ButtonHandler.GetButtonInfoByPage(MenkerMenu.Mods.Category.Safety));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(MenkerMenu.Mods.Category.Move));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(MenkerMenu.Mods.Category.Player));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(MenkerMenu.Mods.Category.Visuals));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(MenkerMenu.Mods.Category.World));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(MenkerMenu.Mods.Category.Fun));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(MenkerMenu.Mods.Category.Draw));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(MenkerMenu.Mods.Category.Guardian));
            list.AddRange(ButtonHandler.GetButtonInfoByPage(MenkerMenu.Mods.Category.OP));

            foreach (ButtonHandler.Button buttonInfo in list)
            {
                string buttonLabel = $"{(buttonInfo.Enabled ? "ENABLED ● " : "Disabled ● ")}{buttonInfo.buttonText}{(buttonInfo.Enabled ? " ●" : " ●")}";
                if (RoundedButton(buttonLabel))
                {
                    buttonInfo.Enabled = !buttonInfo.Enabled;
                }
            }

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.BeginVertical();
        }
        public void DrawSettings()
        {
            GUILayout.EndVertical();
            if (RoundedButton(string.Format("Change Theme: {0}", Theme), Array.Empty<GUILayoutOption>()))
            {
                Theme = (Theme + 1) % 4;
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(8, false, 0.8f);
            }
            GUILayout.Label("More Themes + Settings Soon!!!!", Array.Empty<GUILayoutOption>());
            GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
        }
        
        public void DrawArrayLists()
        {
            Material buttonMaterial = new Material(Shader.Find("GUI/Text Shader"))
            {
                color = Color.Lerp(ColorLib.SkyBlue, new Color32(8, 90, 177, byte.MaxValue), Mathf.PingPong(Time.time, 1.5f))
            };
            buttonMaterial.SetFloat("_Mode", 2f);

            GUIStyle titleStyle = new GUIStyle(GUI.skin.box)
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                richText = true,
                wordWrap = false,
                normal = { textColor =  buttonMaterial.color }
            };

            GUILayout.Space(3);

            string titleText = UIName + $" V{MenkerMenu.Initialization.PluginInfo.menuVersion}";
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
                normal = { textColor = buttonMaterial.color }
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

            string labelText = UIName;
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
            GUI.skin.button.active.background = ButtonClickTexture;
            GUI.skin.button.normal.background = ButtonHoverTexture;
            GUI.skin.button.hover.background = ButtonTexture;
            GUI.skin.button.onActive.background = ButtonClickTexture;
            GUI.skin.button.onHover.background = ButtonHoverTexture;
            GUI.skin.button.onNormal.background = ButtonTexture;
            GUI.skin.horizontalSlider.active.background = ButtonTexture;
            GUI.skin.horizontalSlider.normal.background = ButtonTexture;
            GUI.skin.horizontalSlider.hover.background = ButtonTexture;
            GUI.skin.horizontalSlider.focused.background = ButtonTexture;
            GUI.skin.horizontalSlider.onFocused.background = ButtonTexture;
            GUI.skin.horizontalSlider.onActive.background = ButtonTexture;
            GUI.skin.horizontalSlider.onHover.background = ButtonTexture;
            GUI.skin.horizontalSlider.onNormal.background = ButtonTexture;
            GUI.skin.verticalScrollbar.border = new RectOffset(0, 0, 0, 0);
            GUI.skin.verticalScrollbar.fixedWidth = 0f;
            GUI.skin.verticalScrollbar.fixedHeight = 0f;
            GUI.skin.verticalScrollbarThumb.fixedHeight = 0f;
            GUI.skin.verticalScrollbarThumb.fixedWidth = 5f;
        }
    }
}
