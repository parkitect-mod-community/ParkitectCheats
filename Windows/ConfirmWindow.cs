/**
* Copyright 2019 Michael Pollind
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using UnityEngine;

namespace CheatsMod.Windows
{
    public class ConfirmWindow : CmWindow
    {
        public delegate void OnSignal(bool yn);

        // private Func<bool, bool> fn;
        public string message = "Please confirm";

        public ConfirmWindow(CheatModController cheatController) : base(cheatController)
        {
            windowName = "Are you sure?";
            drawCloseButton = false;
            WindowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100);
        }

        public event OnSignal Signal;

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(message);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Yes"))
            {
                Signal(true);
                _clearInvocation();
                CloseWindow();
            }

            if (GUILayout.Button("No"))
            {
                Signal(false);
                _clearInvocation();
                CloseWindow();
            }

            GUILayout.EndHorizontal();
        }

        private void _clearInvocation()
        {
            foreach (var invocation in Signal.GetInvocationList()) Signal -= invocation as OnSignal;
        }
    }
}