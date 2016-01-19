using System.Collections.Generic;
using UnityEngine;
using CheatMod.Windows;
using CheatMod.Reference;
using System.Collections;
using System.Linq;
namespace CheatMod
{
    class CheatModController : MonoBehaviour
    {
        //private LightMoodController _lightMoodController;
        //private float LightIntensityValue = 1.2F;

        public List<CMWindow> windows = new List<CMWindow>();

        public void Load()
        {
            Debug.Log("Started CheatModController");
           // _lightMoodController = FindObjectOfType<LightMoodController>();
             
            windows.Add (new MainWindow (this));
            windows.Add (new AdvancedWindow (this));
            windows.Add (new ConfirmWindow (this));
            windows.Add (new MessageWindow (this));
            windows.Add (new WeatherWindow (this));
        }

        void OnDestroy()
        {
            setLightIntensity(1.2F);
        }

        void Update() {
           // _lightMoodController.keyLight.intensity = LightIntensityValue;
            if (Input.GetKeyDown(KeyCode.T) || (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.T))) {
                Debug.Log("Toggled Cheatmod window");
                CMWindow mainWindow = windows.Single (x => x is MainWindow);
                Debug.Log(mainWindow);
                mainWindow.ToggleWindowState();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                windows.ForEach(delegate(CMWindow window){
                    window.CloseWindow();
                });
            }
        }

        public T GetWindow<T>() where T : CMWindow
        {
            return (T) windows.Single (x => x is T);
        }

        void OnGUI()
        {
            windows.ForEach(delegate(CMWindow window){
                if (window.isOpen) {
                    window.DrawWindow();
                }
            });
        }


        public void setLightIntensity(float intensity)
        {
          //  LightIntensityValue = intensity;
        }

        /*private IEnumerator UpdateTime()
        {
            /*for (;;) { 
                _lightMoodController.keyLight.intensity = LightIntensityValue;

                yield return new UnityEngine.WaitForSeconds(0.5F);
            }*/
        //}
    }
}
