﻿/**
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
    public class MessageWindow : CmWindow
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
            if (GUILayout.Button("Close")) CloseWindow();
            GUILayout.EndHorizontal();
        }
    }
}