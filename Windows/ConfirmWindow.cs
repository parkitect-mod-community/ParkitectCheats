using System;
using UnityEngine;

namespace CheatMod.Windows
{
    class ConfirmWindow : CMWindow
    {
        private Func<bool, bool> fn;
        public string message = "Please confirm";

        public ConfirmWindow(CheatModController cheatController) : base(cheatController) {
            windowName = "Are you sure?";
            drawCloseButton = false;
            WindowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(message);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Yes")) {
                fn(true);
                CloseWindow();
            }
            if (GUILayout.Button("No"))
            {
                fn(false);
                CloseWindow();
            }
            GUILayout.EndHorizontal();
        }

        public void setCode(Func<bool, bool> fnp) {
            fn = fnp;
        }
    }
}
