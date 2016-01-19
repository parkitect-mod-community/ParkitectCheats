using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheatMod.Windows
{
    class MessageWindow : CMWindow
    {
        public string message = "No message set";

        public MessageWindow(CheatModController cheatController) : base(cheatController)
        {
            windowName = "Cheat Mod Message";
            drawCloseButton = false;
            WindowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 50);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(message);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Close"))
            {
                CloseWindow();
            }
            GUILayout.EndHorizontal();
        }
    }
}
