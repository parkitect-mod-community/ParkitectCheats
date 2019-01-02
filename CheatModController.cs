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

using System.Collections;
using System.Collections.Generic;
using CheatsMod.Windows;
using UnityEngine;

namespace CheatsMod
{
    public class CheatModController : MonoBehaviour
    {
        private LightMoodController _lightMoodController;
        private float LightIntensityValue = 1.2F;

        public List<CmWindow> windows = new List<CmWindow>();

        public void Load()
        {
            Debug.Log("Started CheatModController");

            _lightMoodController = FindObjectOfType<LightMoodController>();

            windows.Add(new MainWindow(this));
            windows.Add(new AdvancedWindow(this));
            windows.Add(new ConfirmWindow(this));
            windows.Add(new MessageWindow(this));
            windows.Add(new WeatherWindow(this));
            windows.Add(new GlobalToggles(this));
        }

        private void OnDestroy()
        {
            setLightIntensity(1.2F);
        }

        private void Update()
        {
            if (_lightMoodController != null)
                _lightMoodController.keyLight.intensity = LightIntensityValue;


            if (Input.GetKeyDown(Main.configuration.settings.openWindow))
            {
                Debug.Log("Toggled Cheatmod window");

                CmWindow mainWindow = GetWindow<MainWindow>();
                Debug.Log(mainWindow.ToString());
                mainWindow.ToggleWindowState();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
                foreach (var window in windows)
                    if (window.isOpen)
                        window.CloseWindow();
        }

        public T GetWindow<T>() where T : CmWindow
        {
            foreach (var window in windows)
                if (window is T)
                    return (T) window;

            return null;
        }

        private void OnGUI()
        {
            foreach (var window in windows)
                if (window.isOpen)
                    window.DrawWindow();
        }


        public void setLightIntensity(float intensity)
        {
            LightIntensityValue = intensity;
        }

        private IEnumerator UpdateTime()
        {
            if (_lightMoodController != null)
                for (;;)
                {
                    _lightMoodController.keyLight.intensity = LightIntensityValue;

                    yield return new WaitForSeconds(0.5F);
                }
        }
    }
}