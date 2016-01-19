using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheatMod.Windows
{
    class WeatherWindow : CMWindow
    {
        private float sliderValue = 1.2F;
        private float previousSliderValue = 1.2F;

        public WeatherWindow(CheatModController cheatController) : base(cheatController) {
            windowName = "Cheat Mod Weather And Time Control";
            WindowRect = new Rect(620, 20, 400, 200);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Toggle rain"))
            {
                WeatherController.Instance.debugToggleRain();
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Set lightintensity:");
            sliderValue = GUILayout.HorizontalSlider(sliderValue, 0F, 2F);
            if(sliderValue != previousSliderValue)
            {
                previousSliderValue = sliderValue;
                //CheatModController.setLightIntensity(sliderValue);
            }
            if(GUILayout.Button("Reset"))
            {
                sliderValue = 1.2F;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set speed 1x"))
            {
                float oldTimeSpeed = Time.timeScale;
                Time.timeScale = 1;
                EventManager.Instance.RaiseOnTimeSpeedChanged(oldTimeSpeed, 1);
            }
            if (GUILayout.Button("Set speed 5x"))
            {
                float oldTimeSpeed = Time.timeScale;
                Time.timeScale = 5;
                EventManager.Instance.RaiseOnTimeSpeedChanged(oldTimeSpeed, 5);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set speed 10x"))
            {
                float oldTimeSpeed = Time.timeScale;
                Time.timeScale = 10;
                EventManager.Instance.RaiseOnTimeSpeedChanged(oldTimeSpeed, 10);
            }
            if (GUILayout.Button("Set speed 15x"))
            {
                float oldTimeSpeed = Time.timeScale;
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
