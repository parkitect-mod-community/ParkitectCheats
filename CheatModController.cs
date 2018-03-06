using System.Collections;
using System.Collections.Generic;
using CheatMod.Windows;
using UnityEngine;

namespace CheatMod
{
    public class CheatModController : MonoBehaviour
    {
        private LightMoodController _lightMoodController;
        private float LightIntensityValue = 1.2F;

        public List<CMWindow> windows = new List<CMWindow>();

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

        void OnDestroy()
        {
            setLightIntensity(1.2F);
        }

        void Update()
        {

            if (_lightMoodController != null)
                _lightMoodController.keyLight.intensity = LightIntensityValue;


            if (Input.GetKeyDown(Main.configuration.settings.openWindow))
            {
                Debug.Log("Toggled Cheatmod window");

                CMWindow mainWindow = GetWindow<MainWindow>();
                Debug.Log(mainWindow.ToString());
                mainWindow.ToggleWindowState();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                foreach (CMWindow window in windows)
                {
                    if (window.isOpen)
                    {
                        window.CloseWindow();
                    }
                }
            }
        }

        public T GetWindow<T>() where T : CMWindow
        {
            foreach (CMWindow window in windows)
            {

                if (window is T)
                {
                    return (T) window;

                }
            }

            return null;
        }

        void OnGUI()
        {
            foreach (CMWindow window in windows)
            {
                if (window.isOpen)
                {
                    window.DrawWindow();
                }
            }
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
