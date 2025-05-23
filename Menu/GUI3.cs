﻿/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using BepInEx;
using Cinemachine;
using GorillaNetworking;
using GorillaTag.GuidedRefs;
using MenkerMenu.Menu;
using Photon.Pun;
using Photon.Realtime;
using POpusCodec.Enums;
using MenkerMenu.Mods.Categories;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static NetworkSystem;
using static ONSPPropagationMaterial;
using static Photon.Pun.UtilityScripts.TabViewManager;
using MenkerMenu.Mods;

namespace StupidTemplate.Menu
{
    [BepInPlugin("PsiGUI", "GayrillaTag", "1.0.0")]
    internal class GUITEMP : BaseUnityPlugin
    {
        private bool GUIShown = true, shouldHideRoom = false;

        public Rect window = new Rect(100, 100, 500, 300);

        public Vector2[] scroll = new Vector2[30];

        public static int Page = 0, Theme = 0;

        public static Texture2D infectedTexture = null, versionTexture, patchNotesTexture, addPresetTexture;


        string[] pages = new string[]
        {
            "Home",
            "Movement"
        };

        string roomStr = "text here", searchString = "Query to search";
        public static float deltaTime, fov = 60;

        public static Font myFont;
        private float speed = 10;
        private bool campause;
        public static bool freecam;
        private bool fpc;



        public void OnGUI()
        {
            if (!GUIShown)
                return;

            window = GUI.Window(1, window, Window, "");
        }

        public void Window(int id)
        {
            try
            {
                if (myFont == null)
                {
                    myFont = Font.CreateDynamicFontFromOSFont("Gill Sans Nova", 18);

                }
                UILib.SetTextures();
                UILib.Init();
                deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

                GUI.DrawTexture(new Rect(0f, 0f, window.width, window.height), UILib.windowTexture, ScaleMode.StretchToFill, false, 0f, GUI.color, Vector4.zero, new Vector4(6f, 6f, 6f, 6f));
                GUI.DrawTexture(new Rect(0f, 0f, 100f, window.height), UILib.sidePannelTexture, ScaleMode.StretchToFill, false, 0f, GUI.color, Vector4.zero, new Vector4(6f, 0f, 0f, 6f));
                GUIStyle lb = new GUIStyle(GUI.skin.label);
                lb.font = myFont;
                GUI.Label(new Rect(140, 5, window.width, 30), "Psi Menu", lb);

                GUILayout.BeginArea(new Rect(7.5f, 30, 100, window.height));
                GUILayout.BeginVertical();
                for (int i = 0; i < pages.Length; i++)
                {
                    GUILayout.Space(5);
                    string page = pages[i];
                    if (UILib.RoundedPageButton(page, i, GUILayout.Width(85)))
                    {
                        Page = i;
                        Debug.Log("Switched to page: " + page);
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndArea();
                if (Page == 0)
                {
                    GUI.DrawTexture(new Rect(110f, 70f, 150f, 70f), UILib.sidePannelTexture, ScaleMode.StretchToFill, false, 0f, GUI.color, Vector4.zero, new Vector4(2.8f, 2.8f, 2.8f, 2.8f));
                    GUI.DrawTexture(new Rect(110f, 150f, 380f, 200f), UILib.sidePannelTexture, ScaleMode.StretchToFill, false, 0f, GUI.color, Vector4.zero, new Vector4(2.8f, 2.8f, 2.8f, 2.8f));

                    var fontSyle = new GUIStyle("label");
                    fontSyle.font = myFont;
                    fontSyle.normal.textColor = Color.white;
                    fontSyle.fontSize = 20;
                    fontSyle.alignment = TextAnchor.MiddleLeft;
                    var fontSyle2 = new GUIStyle("label");
                    fontSyle2.font = myFont;
                    fontSyle2.normal.textColor = Color.gray * 1.2f;
                    fontSyle2.fontSize = 17;
                    fontSyle2.alignment = TextAnchor.MiddleLeft;
                    GUI.Label(new Rect(115, 25, 200, 45), "Home", fontSyle2);

                    var fontSyle3 = new GUIStyle("label");
                    fontSyle3.font = myFont;
                    fontSyle3.normal.textColor = Color.white;
                    fontSyle3.fontSize = 13;
                    fontSyle3.alignment = TextAnchor.MiddleLeft;
                    GUI.Label(new Rect(142, 68, 100, 30), "Version", fontSyle3);
                    GUI.Label(new Rect(118, 72, 25, 25), versionTexture);
                    var fontSyle4 = new GUIStyle("label");
                    fontSyle4.font = myFont;
                    fontSyle4.normal.textColor = Color.white;
                    fontSyle4.fontSize = 18;
                    fontSyle4.alignment = TextAnchor.MiddleLeft;
                    GUI.Label(new Rect(123, 95, 100, 30), 2 + "." + 0, fontSyle4);

                    GUI.Label(new Rect(142, 152, 100, 30), "Patch Notes", fontSyle3);
                    GUI.Label(new Rect(118, 155, 25, 25), patchNotesTexture);

                    GUI.Label(new Rect(125, 185, 200, 150), string.Concat(new string[]
                    {
                        "idk bruh"
                    }));

                    roomStr = shouldHideRoom ? GUI.PasswordField(new Rect(265, 70, 150, 25), roomStr, '⋆') : GUI.TextField(new Rect(265, 70, 150, 25), roomStr);

                    if (UILib.RoundedButton(new Rect(420, 70, 75, 20), "HIDE"))
                    {
                        shouldHideRoom = !shouldHideRoom;
                    }
                    if (UILib.RoundedButton(new Rect(265, 98, 150, 20), "Join Room"))
                    {
                        roomStr = Regex.Replace(roomStr.ToUpper(), "[^a-zA-Z0-9]", "");

                        PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(roomStr, JoinType.Solo);
                    }
                    if (UILib.RoundedButton(new Rect(265, 120, 150, 20), "Set Name"))
                    {
                        roomStr = Regex.Replace(roomStr.ToUpper(), "[^a-zA-Z0-9]", "");

                        PhotonNetwork.LocalPlayer.NickName = roomStr;
                        PlayerPrefs.SetString("playerName", roomStr);
                        GorillaComputer.instance.offlineVRRigNametagText.text = roomStr;
                        GorillaTagger.Instance.offlineVRRig.playerName = roomStr;
                        PlayerPrefs.Save();
                    }
                }
                GUILayout.BeginArea(new Rect(115, 30, 370, window.height - 50));
                GUILayout.BeginVertical();
                switch (Page)
                {
                    case 0:
                        var fontSyle2 = new GUIStyle("label");
                        fontSyle2.font = myFont;
                        fontSyle2.normal.textColor = Color.gray * 1.2f;
                        fontSyle2.fontSize = 17;
                        fontSyle2.alignment = TextAnchor.MiddleLeft;
                        GUI.Label(new Rect(115, 25, 200, 45), "Movement", fontSyle2);

                        UILib.RoundedToggleButton("", Move.Platforms(), );
                        break;
                }
                GUI.DragWindow(new Rect(0, 0, 100000, 100000));
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
        static class UILib
        {


            public static Texture2D sidePannelTexture, TextBox = new Texture2D(2, 2), pageButtonTexture = new Texture2D(2, 2), pageButtonHoverTexture = new Texture2D(2, 2), buttonTexture = new Texture2D(2, 2), buttonHoverTexture = new Texture2D(2, 2), buttonClickTexture = new Texture2D(2, 2), windowTexture = new Texture2D(2, 2), boxTexture = new Texture2D(2, 2);



            public static void Init()
            {
                switch (Theme)
                {
                    case 0:
                        pageButtonHoverTexture = ApplyColorFilter(new Color32(247, 30, 98, 255));
                        pageButtonTexture = ApplyColorFilter(new Color32(247, 30, 98, 255));
                        buttonTexture = ApplyColorFilter(new Color32(25, 25, 25, 255));
                        buttonHoverTexture = ApplyColorFilter(new Color32(247, 30, 98, 255));
                        buttonClickTexture = ApplyColorFilter(new Color32(44, 44, 44, 255));
                        windowTexture = ApplyColorFilter(new Color32(25, 25, 25, 255));
                        sidePannelTexture = ApplyColorFilter(new Color32(30, 30, 30, 255));
                        boxTexture = ApplyColorFilter(new Color32(25, 25, 25, 255));
                        TextBox = CreateRoundedTexture2(12, new Color32(25, 25, 25, 255));
                        break;
                }
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

                GUI.skin.button.active.background = buttonClickTexture;
                GUI.skin.button.normal.background = buttonHoverTexture;
                GUI.skin.button.hover.background = buttonTexture;

                GUI.skin.button.onActive.background = buttonClickTexture;
                GUI.skin.button.onHover.background = buttonHoverTexture;
                GUI.skin.button.onNormal.background = buttonTexture;

                GUI.skin.box.active.background = boxTexture;
                GUI.skin.box.normal.background = boxTexture;
                GUI.skin.box.hover.background = boxTexture;

                GUI.skin.box.onActive.background = boxTexture;
                GUI.skin.box.onHover.background = boxTexture;
                GUI.skin.box.onNormal.background = boxTexture;

                GUI.skin.textField.active.background = TextBox;
                GUI.skin.textField.normal.background = TextBox;
                GUI.skin.textField.hover.background = TextBox;
                GUI.skin.textField.focused.background = TextBox;

                GUI.skin.textField.onFocused.background = TextBox;
                GUI.skin.textField.onActive.background = TextBox;
                GUI.skin.textField.onHover.background = TextBox;
                GUI.skin.textField.onNormal.background = TextBox;

                GUI.skin.horizontalSlider.active.background = buttonTexture;
                GUI.skin.horizontalSlider.normal.background = buttonTexture;
                GUI.skin.horizontalSlider.hover.background = buttonTexture;
                GUI.skin.horizontalSlider.focused.background = buttonTexture;

                GUI.skin.horizontalSlider.onFocused.background = buttonTexture;
                GUI.skin.horizontalSlider.onActive.background = buttonTexture;
                GUI.skin.horizontalSlider.onHover.background = buttonTexture;
                GUI.skin.horizontalSlider.onNormal.background = buttonTexture;



                GUI.skin.verticalScrollbar.border = new RectOffset(0, 0, 0, 0);

                GUI.skin.verticalScrollbar.fixedWidth = 0f;

                GUI.skin.verticalScrollbar.fixedHeight = 0f;

                GUI.skin.verticalScrollbarThumb.fixedHeight = 0f;

                GUI.skin.verticalScrollbarThumb.fixedWidth = 5f;
            }

            public static Texture2D ApplyColorFilter(Color color)
            {
                Texture2D texture = new Texture2D(30, 30);

                Color[] colors = new Color[30 * 30];
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = color;
                }
                texture.SetPixels(colors);
                texture.Apply();
                return texture;
            }


            private static Texture2D CreateRoundedTexture2(int size, Color color)
            {
                Texture2D texture = new Texture2D(size, size);
                Color[] colors = new Color[size * size];
                for (int i = 0; i < size * size; i++)
                {
                    int x = i % size;
                    int y = i / size;
                    if (Mathf.Sqrt((x - size / 2) * (x - size / 2) + (y - size / 2) * (y - size / 2)) <= size / 2)
                    {
                        colors[i] = color;
                    }
                    else
                    {
                        colors[i] = Color.clear;
                    }
                }
                texture.SetPixels(colors);
                texture.Apply();
                return texture;
            }

            public static Texture2D CreateRoundedTexture(int size, Color color)
            {
                Texture2D texture = new Texture2D(size, size);
                Color[] colors = new Color[size * size];
                float radius = size / 2f;
                float radiusSquared = radius * radius;

                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        float distanceSquared = (x - radius) * (x - radius) + (y - radius) * (y - radius);
                        if (distanceSquared <= radiusSquared)
                        {
                            float alpha = 1f - distanceSquared / radiusSquared; // Smoothly fade out the edges
                            colors[y * size + x] = new Color(color.r, color.g, color.b, alpha);
                        }
                        else
                        {
                            colors[y * size + x] = Color.clear;
                        }
                    }
                }

                texture.SetPixels(colors);
                texture.Apply();
                return texture;
            }
            public static bool RoundedToggleButton(string content, ButtonHandler.Button button, params GUILayoutOption[] options)
            {
                Texture2D texture = button.Enabled ? buttonClickTexture : buttonTexture;
                var rect = GUILayoutUtility.GetRect(new GUIContent(content), GUI.skin.button, options);
                if (rect.Contains(Event.current.mousePosition))
                {
                    texture = buttonHoverTexture;
                }
                if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                {
                    texture = buttonClickTexture;
                    return true;
                }
                string modName = button.buttonText;
                DrawTexture(rect, texture, 6);
                DrawText(new Rect(rect.x, rect.y - 3, rect.width, 25f), modName, 12, Color.white);
                return false;
            }

            public static bool RoundedPlayerButton(string content, params GUILayoutOption[] options)
            {
                Texture2D texture = buttonTexture;
                var rect = GUILayoutUtility.GetRect(new GUIContent(content), GUI.skin.button, options);
                if (rect.Contains(Event.current.mousePosition))
                {
                    texture = buttonHoverTexture;
                }
                if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                {
                    texture = buttonClickTexture;
                    return true;
                }
                DrawTexture(rect, texture, 6);
                DrawText(new Rect(rect.x, rect.y, rect.width, 25f), content, 12, Color.white);
                return false;
            }

            public static bool RoundedButton(string content, params GUILayoutOption[] options)
            {
                Texture2D texture = buttonTexture;
                var rect = GUILayoutUtility.GetRect(new GUIContent(content), GUI.skin.button, options);
                if (rect.Contains(Event.current.mousePosition))
                {
                    texture = buttonHoverTexture;
                }
                if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                {
                    texture = buttonClickTexture;
                    return true;
                }
                DrawTexture(rect, texture, 6);
                DrawText(new Rect(rect.x, rect.y - 3, rect.width, 25f), content, 12, Color.white);
                return false;
            }

            public static bool RoundedButton(string content, GUIStyle style, params GUILayoutOption[] options)
            {
                Texture2D texture = buttonTexture;
                var rect = GUILayoutUtility.GetRect(new GUIContent(content), style, options);
                if (rect.Contains(Event.current.mousePosition))
                {
                    texture = buttonHoverTexture;
                }
                if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                {
                    texture = buttonClickTexture;
                    return true;
                }
                DrawTexture(rect, texture, 6);
                DrawText(new Rect(rect.x, rect.y - 3, rect.width, 25f), content, 12, Color.white);
                return false;
            }

            public static bool RoundedButton(string content, bool refrence, params GUILayoutOption[] options)
            {
                Texture2D texture = buttonTexture;
                var rect = GUILayoutUtility.GetRect(new GUIContent(content), GUI.skin.button, options);
                if (rect.Contains(Event.current.mousePosition))
                {
                    texture = buttonHoverTexture;
                }
                if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                {
                    texture = buttonClickTexture;
                    return true;
                }
                if (refrence)
                {
                    texture = buttonClickTexture;
                }
                DrawTexture(rect, texture, 6);
                DrawText(new Rect(rect.x, rect.y - 3, rect.width, 25f), content, 12, Color.white);
                return false;
            }

            public static bool RoundedButton(Rect rect, string content, params GUILayoutOption[] options)
            {
                Texture2D texture = buttonTexture;
                if (rect.Contains(Event.current.mousePosition))
                {
                    texture = buttonHoverTexture;
                }
                if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                {
                    texture = buttonClickTexture;
                    return true;
                }
                DrawTexture(rect, texture, 6);
                DrawText(new Rect(rect.x, rect.y - 3, rect.width, 25f), content, 12, Color.white);
                return false;
            }

            public static bool PlayerButton(string content, params GUILayoutOption[] options)
            {
                Texture2D texture = buttonTexture;
                var rect = GUILayoutUtility.GetRect(new GUIContent(content), GUI.skin.button, options);

                DrawText(new Rect(rect.x, rect.y - 3, rect.width, 25f), content, 12, Color.white);
                return false;
            }


            public static void DrawText(Rect rect, string text, int fontSize = 12, Color textColor = default)
            {
                GUIStyle _style = new GUIStyle(GUI.skin.label);
                _style.fontSize = fontSize;
                _style.font = myFont;
                _style.normal.textColor = textColor;
                float X = rect.x + rect.width / 2f - _style.CalcSize(new GUIContent(text)).x / 2f;
                float Y = rect.y + rect.height / 2f - _style.CalcSize(new GUIContent(text)).y / 2f;
                GUI.Label(new Rect(X, Y, rect.width, rect.height), new GUIContent(text), _style);
            }
            static Rect pageButtonRect;
            public static bool RoundedPageButton(string content, int i, params GUILayoutOption[] options)
            {
                if (Page == i)
                {
                    Texture2D texture = pageButtonTexture;
                    pageButtonRect = GUILayoutUtility.GetRect(new GUIContent(content), GUI.skin.button, options);
                    if (pageButtonRect.Contains(Event.current.mousePosition))
                    {
                        texture = pageButtonHoverTexture;
                    }
                    if (pageButtonRect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                    {
                        return true;
                    }
                    DrawTexture(pageButtonRect, texture, 8); DrawText(new Rect(pageButtonRect.x, pageButtonRect.y - 3, pageButtonRect.width, 25f), content, 12, Color.white);
                    return false;
                }
                else
                {
                    Texture2D texture = buttonTexture;
                    pageButtonRect = GUILayoutUtility.GetRect(new GUIContent(content), GUI.skin.button, options);
                    if (pageButtonRect.Contains(Event.current.mousePosition))
                    {
                        texture = buttonHoverTexture;
                    }
                    if (pageButtonRect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                    {
                        texture = buttonClickTexture;
                        return true;
                    }
                    DrawTexture(pageButtonRect, texture, 8); DrawText(new Rect(pageButtonRect.x, pageButtonRect.y - 3, pageButtonRect.width, 25f), content, 12, Color.white);
                    return false;
                }
            }

            private static void DrawTexture(Rect rect, Texture2D texture, int borderRadius, Vector4 borderRadius4 = default)
            {
                if (borderRadius4 == Vector4.zero)
                    borderRadius4 = new Vector4(borderRadius, borderRadius, borderRadius, borderRadius);
                GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, false, 0f, GUI.color, Vector4.zero, borderRadius4);
            }
        }

    }
}
*/