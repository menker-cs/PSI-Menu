using static MenkerMenu.Utilities.GunTemplate;
using static MenkerMenu.Utilities.Variables;
using static MenkerMenu.Utilities.ColorLib;
using static MenkerMenu.Utilities.HandOrbs;
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
using static MenkerMenu.Mods.Categories.Overpowered;
using static MenkerMenu.Mods.Categories.World;
using static MenkerMenu.Menu.ButtonHandler;
using static MenkerMenu.Menu.Optimizations;
using static MenkerMenu.Menu.Optimizations.ResourceLoader;
using static MenkerMenu.Menu.Main;
using UnityEngine;
using Fusion;
using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using MenkerMenu.Utilities;
using MenkerMenu.Mods.Categories;

namespace MenkerMenu.Mods
{
    public enum Category
    {
        // Starting Page
        Home,

        // Mod Categories
        Settings,
        Room,
        Player,
        Move,
        Visuals,
        Fun,
        Experimental,
        World,
        Creds,
        Safety,
        Guardian,
        OP,
        Unlisted,
    }
    public class ModButtons
    {
        public static Button[] buttons =
        {
#region Starting Page
            new Button("Settings", Category.Home, false, false, ()=>ChangePage(Category.Settings)),
            new Button("Room", Category.Home, false, false, ()=>ChangePage(Category.Room)),
            new Button("Safety", Category.Home, false, false, ()=>ChangePage(Category.Safety)),
            new Button("Movement", Category.Home, false, false, ()=>ChangePage(Category.Move)),
            new Button("Player", Category.Home, false, false, ()=>ChangePage(Category.Player)),
            new Button("Visual", Category.Home, false, false, ()=>ChangePage(Category.Visuals)),
            new Button("World", Category.Home, false, false, ()=>ChangePage(Category.World)),
            new Button("Fun", Category.Home, false, false, ()=>ChangePage(Category.Fun)),
            new Button("Guardian Mods", Category.Home, false, false, ()=>ChangePage(Category.OP)),
            new Button("Guardian Mods", Category.Home, false, false, ()=>ChangePage(Category.Guardian)),
            new Button("Experimental", Category.Home, false, false, ()=>ChangePage(Category.Experimental)),
            new Button("Creds", Category.Home, false, false, ()=>ChangePage(Category.Creds)),
            #endregion

            #region Settings
            new Button("Disable All Mods", Category.Settings, false, false, ()=>DisableAllMods()),
            new Button("Switch Hands", Category.Settings, true, false, ()=>SwitchHands(true), ()=>SwitchHands(false)),
            new Button("Disconnect Button", Category.Settings, true, true, ()=>ToggleDisconnectButton(true), ()=>ToggleDisconnectButton(false)),
            new Button("Toggle Notifications", Category.Settings, true, true, ()=>ToggleNotifications(true), ()=>ToggleNotifications(false)),
            new Button("Clear Notifications", Category.Settings, false, false, ()=>ClearNotifications()),
            new Button("Change Fly Speed", Category.Settings, false, false, ()=>FlySpeed()),
            new Button("Change Speed Boost", Category.Settings, false, false, ()=>SpeedSpeed()),
            new Button("Change ESP Color", Category.Settings, false, false, ()=>ESPChange()),

            #endregion

            #region Room
            new Button("Quit Game", Category.Room, true, false, ()=>QuitGTAG()),
            new Button("Join Random", Category.Room, false, false, ()=>JoinRandomPublic()),
            new Button("Disconnect", Category.Room, false, false, ()=>Disconnect()),
            new Button("Primary Disconnect", Category.Room, true, false, ()=>PrimaryDisconnect()),
            new Button("Check If Master", Category.Room, false, false, ()=>IsMasterCheck()),
            new Button("Disable Network Triggers", Category.Room, false, false, ()=>DisableNetworkTriggers()),
            new Button("Enable Network Triggers", Category.Room, false, false, ()=>EnableNetworkTriggers()),
            new Button("Join Code Mods", Category.Room, false, false, ()=>JoinRoom("MODS")),
            new Button("Join Code Menker", Category.Room, false, false, ()=>JoinRoom("MENKER")),
            new Button("Join Code PBBV", Category.Room, false, false, ()=>JoinRoom("PBBV")),
            new Button("Join Code Daisy09", Category.Room, false, false, ()=>JoinRoom("DAISY09")),
            new Button("Set Mode Hunt", Category.Room, false, false, ()=>SetMode("Hunt")),
            new Button("Set Mode Paintbrawl", Category.Room, false, false, ()=>SetMode("Paintbrawl")),
            new Button("Set Mode Ghost", Category.Room, false, false, ()=>SetMode("Ghost")),
            new Button("Set Mode Ambush", Category.Room, false, false, ()=>SetMode("Ambush")),
            new Button("Set Mode ERROR", Category.Room, false, false, ()=>SetMode("ERROR")),
            new Button("Mute Everyone", Category.Room, false, false, ()=>MuteAll()),
            new Button("Report Everyone", Category.Room, false, false, ()=>ReportAll()),
            new Button("Copy Self ID", Category.Fun, false, false, ()=> CopySelfID()),
            #endregion

            #region Safety
            new Button("AntiReport [USE]", Category.Safety, true, true, ()=>AntiReport()),
            new Button("Anti RPC [USE]", Category.Safety, true, false, ()=>RPC()),
            #endregion

            #region Movement
            new Button("Platforms [G]", Category.Move, true, false, ()=>Platforms()),
            new Button("Invis Platforms [G]", Category.Move, true, false, ()=>InvisPlatforms()),
            new Button("Force Tag Freeze", Category.Move, false, false, ()=>TagFreeze()),
            new Button("No Tag Freeze", Category.Move, true, false, ()=>NoTagFreeze()),
            new Button("NoClip [T]", Category.Move, true, false, ()=>Noclip()),
            new Button("Speed Boost", Category.Move, true, false, ()=>Speedboost()),
            new Button("Fly [P]", Category.Move, true, false, ()=>Fly()),
            new Button("Car Monkey [T]", Category.Move, true, false, ()=>carmonkey()),
            new Button("WASD [PC]", Category.Move, true, false, ()=>WASDFly()),
            new Button("Up & Down [T]", Category.Move, true, false, ()=>UpAndDown()),
            new Button("No Gravity", Category.Move, true, false, ()=>ZeroGravity()),
            new Button("Low Gravity", Category.Move, true, false, ()=>LowGravity()),
            new Button("High Gravity", Category.Move, true, false, ()=>HighGravity()),
            new Button("Reverse Gravity", Category.Move, true, false, ()=>ReverseGravity(), ()=>GravityFixRig()),
            new Button("TP Gun", Category.Move, true, false, ()=>TPGun()),
            new Button("TP To Player Gun", Category.Move, true, false, ()=>TPPlayerGun()),
            new Button("Hover Gun", Category.Move, true, false, ()=>HoverGun()),
            #endregion

            #region Player
            new Button("Long Arms", Category.Player, true, false, ()=>LongArms(), ()=>FixArms()),
            new Button("Very Long Arms", Category.Player, true, false, ()=>VeryLongArms(), ()=>FixArms()),
            new Button("Upsidedown Head", Category.Player, true, false, ()=>UpsidedownHead(), ()=>FixHead()),
            new Button("Backwards Head", Category.Player, true, false, ()=>BackwardsHead(), ()=>FixHead()),
            new Button("Hand Orbs", Category.Player, true, false, ()=>HandOrbs1()),
            new Button("Invis Monke", Category.Player, true, false, ()=>InvisibleMonke()),
            new Button("Ghost Monke", Category.Player, true, false, ()=>GhostMonke()),
            new Button("Head Spin 1", Category.Player, true, false, ()=>HeadSpin(), ()=>FixHead()),
            new Button("Head Spin 2", Category.Player, true, false, ()=>HeadSpiny(), ()=>FixHead()),
            new Button("Freeze Rig", Category.Player, true, false, ()=>FreezeRig()),
            new Button("Fake Lag", Category.Player, true, false, ()=>FakeLag()),
            new Button("Grab Rig", Category.Player, true, false, ()=>GrabRig()),
            new Button("Rig Gun", Category.Player, true, false, ()=>RigGun1()),
            new Button("Spaz Rig", Category.Player, true, false, ()=>Spaz()),
            new Button("Annoy Player Gun", Category.Player, true, false, ()=>AnnoyPlayerGun()),

            new Button("Flick Tag Gun", Category.Player, true, false, ()=>FlickTagGun()),
            new Button("Tag Aura", Category.Player, true, false, ()=>TagAura()),
            new Button("Tag Gun", Category.Player, true, false, ()=>TagGun()),
            new Button("Tag All", Category.Player, true, false, ()=>TagAll()),
            new Button("Tag Self", Category.Player, true, false, ()=>TagSelf()),
            #endregion

            #region Visuals
            new Button("ESP", Category.Visuals, true, false, ()=>ESP(), ()=>DisableESP()),
            new Button("Tracers", Category.Visuals, true, false, ()=>Tracers()),
            new Button("2D Box ESP", Category.Visuals, true, false, ()=>BoxESP(false)),
            new Button("3D Box ESP", Category.Visuals, true, false, ()=>BoxESP(true)),
            new Button("Sphere ESP", Category.Visuals, true, false, ()=>BallESP()),
            new Button("CSGO ESP", Category.Visuals, true, false, ()=>CSGO(), ()=>DisableCSGO()),
            new Button("Ball Halo Orbit", Category.Visuals, true, false, ()=>BallHaloOrbit()),
            new Button("Visualize Anti Report", Category.Visuals, true, false, ()=>VisReport(true), ()=>VisReport(false)),
            new Button("FPS Boost", Category.Visuals, true, false, ()=>FPSboost(), ()=> fixFPS()),
            #endregion

            #region World
            new Button("Disable QuitBox", Category.World, true, false, ()=>DisableQuitBox(), ()=>EnableQuitBox()),
            new Button("Unlock Comp", Category.World, true, false, ()=>UnlockComp()),
            new Button("Enable I Lava You Update", Category.World, true, false, ()=>EnableILavaYou(), ()=>DisableILavaYou()),
            new Button("Enable Rain", Category.World, true, false, ()=>Rain(), ()=>Rain1()),
            new Button("Change Time Night", Category.World, false, false, ()=> NightTimeMod()),
            new Button("Change Time Day", Category.World, false, false, ()=> idkTimeMod()),
            new Button("Custom Boards", Category.Unlisted, false, true, ()=> Coc()),
            #endregion

            #region Fun
            new Button("Grab Bug", Category.Fun, true, false, ()=> GrabBug()),
            new Button("Bug Gun", Category.Fun, true, false, ()=> BugGun()),
            new Button("Grab Bat", Category.Fun, true, false, ()=> GrabBat()),
            new Button("Bat Gun", Category.Fun, true, false, ()=> BatGun()),
            new Button("Copy ID Gun", Category.Experimental, true, false, ()=> CopyIDGun()),
            #endregion

            #region Guardian
            new Button("Always Guardian [BUGGY]", Category.Guardian, true, false, ()=> AlwaysGuardian()),
            new Button("Void All [T]", Category.Guardian, true, false, ()=> VoidAll()),
            new Button("Void Gun [NW]", Category.Guardian, true, false, ()=> VoidGun()),
            new Button("Grab All", Category.Guardian, true, false, ()=> GrabAll()),
            new Button("Drop All [BUGGY]", Category.Guardian, false, false, ()=> Drop()),
            #endregion

            #region OP
            new Button("Lag All", Category.OP, true, false, ()=> LagAll()),
            new Button("Lag Gun", Category.OP, true, false, ()=> LagGun()),
            new Button("Crash All [SLOW]", Category.OP, true, false, ()=> InstaCrashAll()),
            new Button("Crash Gun [SLOW]", Category.OP, true, false, ()=> InstantsCrashGun()),
            new Button("Instant Crash All [BUGGY]", Category.OP, true, false, ()=> CrashAll()),
            #endregion 

            #region Credits
            new Button("Menu Credits:", Category.Creds, false, false, ()=>Placeholder()),
            new Button("Menker", Category.Creds, false, false, ()=>Placeholder()),
            new Button("Nova", Category.Creds, false, false, ()=>Placeholder()),
            new Button("NxO", Category.Creds, false, false, ()=>Placeholder()),
            new Button("diddy master", Category.Creds, false, false, ()=>Placeholder()),
            new Button("Diddy Master", Category.Creds, false, false, ()=>Placeholder()),
            new Button("Cha554", Category.Creds, false, false, ()=>Placeholder()),
            new Button("Nova", Category.Creds, false, false, ()=>Placeholder()),
            new Button("Join The Discord!", Category.Creds, false, false, ()=>Discord()),
            #endregion
        };
    }
}
