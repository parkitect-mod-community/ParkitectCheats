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
    public class WeatherWindow : CmWindow
    {
        private float previousSliderValue = 1.2F;
        private float sliderValue = 1.2F;

        public WeatherWindow(CheatModController cheatController) : base(cheatController)
        {
            windowName = "Cheat Mod Weather And Time Control";
            WindowRect = new Rect(620, 20, 400, 200);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Toggle rain")) GameController.Instance.park.weatherController.debugToggleRain();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Set lightintensity:");
            sliderValue = GUILayout.HorizontalSlider(sliderValue, 0F, 2F);
            if (sliderValue != previousSliderValue)
            {
                previousSliderValue = sliderValue;
                Controller.setLightIntensity(sliderValue);
            }

            if (GUILayout.Button("Reset")) sliderValue = 1.2F;
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set speed 1x"))
            {
                var oldTimeSpeed = Time.timeScale;
                Time.timeScale = 1;
                EventManager.Instance.RaiseOnTimeSpeedChanged(oldTimeSpeed, 1);
            }

            if (GUILayout.Button("Set speed 5x"))
            {
                var oldTimeSpeed = Time.timeScale;
                Time.timeScale = 5;
                EventManager.Instance.RaiseOnTimeSpeedChanged(oldTimeSpeed, 5);
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set speed 10x"))
            {
                var oldTimeSpeed = Time.timeScale;
                Time.timeScale = 10;
                EventManager.Instance.RaiseOnTimeSpeedChanged(oldTimeSpeed, 10);
            }

            if (GUILayout.Button("Set speed 15x"))
            {
                var oldTimeSpeed = Time.timeScale;
                Time.timeScale = 15;
                EventManager.Instance.RaiseOnTimeSpeedChanged(oldTimeSpeed, 15);
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Current speed: " + Time.timeScale);
            GUILayout.EndHorizontal();
        }
    }
}