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
    public class GlobalToggles : CmWindow
    {
        public GlobalToggles(CheatModController cheatController) : base(cheatController)
        {
            windowName = "Debug Toggles";
            drawCloseButton = true;
            WindowRect = new Rect(620, 20, 400, 200);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            Global.CAN_BUILD_OUTSIDE_PARKBOUNDS =
                GUILayout.Toggle(Global.CAN_BUILD_OUTSIDE_PARKBOUNDS, "CAN_BUILD_OUTSIDE_PARKBOUNDS");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            Global.NO_TRACKBUILDER_RESTRICTIONS =
                GUILayout.Toggle(Global.NO_TRACKBUILDER_RESTRICTIONS, "NO_TRACKBUILDER_RESTRICTIONS");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            Global.DRAW_COORDS_AT_MOUSE = GUILayout.Toggle(Global.DRAW_COORDS_AT_MOUSE, "DRAW_COORDS_AT_MOUSE");
            GUILayout.EndHorizontal();
         

            GUILayout.BeginHorizontal();
            Global.GUESTS_LIKE_EVERY_RIDE = GUILayout.Toggle(Global.GUESTS_LIKE_EVERY_RIDE, "GUESTS_LIKE_EVERY_RIDE");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            Global.PERSON_SPAWN = GUILayout.Toggle(Global.PERSON_SPAWN, "PERSON_SPAWN");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            Global.PERSON_FOOTPRINTS_ENABLED =
                GUILayout.Toggle(Global.PERSON_FOOTPRINTS_ENABLED, "PERSON_FOOTPRINTS_ENABLED");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            Global.PERSON_HEADLOOK_ENABLED =
                GUILayout.Toggle(Global.PERSON_HEADLOOK_ENABLED, "PERSON_HEADLOOK_ENABLED");
            GUILayout.EndHorizontal();

            base.DrawContent();
        }

        public float TextFloat(float input)
        {
            float output;
            if (float.TryParse(GUILayout.TextField(input.ToString()), out output)) return output;
            return input;
        }
    }
}