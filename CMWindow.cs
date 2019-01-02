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

namespace CheatsMod
{
    public class CmWindow
    {
        protected readonly CheatModController Controller;

        private readonly int _id;
        protected bool drawCloseButton = true;
        public bool isOpen;
        private readonly Rect TitleBarRect = new Rect(0, 0, 200000000, 20);
        protected bool usesSkin = true;

        protected string windowName = "CMWindow";
        protected Rect WindowRect = new Rect(20, 20, 200, 200);

        public CmWindow(CheatModController controller)
        {
            _id = WindowIdManager.GetWindowId();
            Controller = controller;
        }

        public void ToggleWindowState()
        {
            isOpen = !isOpen;
        }

        public void OpenWindow()
        {
            isOpen = true;
        }

        public void CloseWindow()
        {
            isOpen = false;
        }

        public void DrawWindow()
        {
            WindowRect = GUILayout.Window(_id, WindowRect, DrawMain, windowName);
        }

        public void DrawMain(int windowId)
        {
            if (drawCloseButton)
                if (GUI.Button(new Rect(WindowRect.width - 21, 6, 15, 15), "x"))
                    CloseWindow();
            GUI.BeginGroup(new Rect(0, /*27*/0, WindowRect.width, WindowRect.height /* - 33*/));
            DrawContent();

            GUI.EndGroup();
            GUI.DragWindow(TitleBarRect);
        }

        public virtual void DrawContent()
        {
        }
    }
}