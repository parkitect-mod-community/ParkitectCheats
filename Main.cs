using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CheatMod
{
    public class Main : IMod , IModSettings 
    {
        public GameObject _go;
        public string Identifier { get; set; }
        public static Configuration configuration{ get; private set; }

        public void onEnabled()
        {
            if (configuration == null) {
                configuration = new Configuration ();
                configuration.Load ();
                configuration.Save ();
            }

            if (configuration.settings.debugBuildMode) {
               // Global.IS_RELEASE_BUILD = false;
				//Global.is
                ScriptableSingleton<DebugPreferences>.Instance.drawDebugUI = true;
                //new GameObject ("Mod tools").AddComponent<DebugToolsMenu> ();
            }

             
            _go = new GameObject();
            var modController = _go.AddComponent<CheatModController>();
            modController.Load ();

        }

        public void onDisabled()
        {
            Object.Destroy(_go);
        }


        public string Name => "Cheat Mod";

        public string Description => "Cheats for Parkitect.";

        string IMod.Identifier => "CheatsMod";


        #region Implementation of IModSettings

        private bool FetchKey(out KeyCode outKey)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown (key)) {
                    outKey = key;
                    return true;
                }
            }
            outKey = KeyCode.A;
            return false;
        }


        public void onDrawSettingsUI() {
            configuration.DrawGUI ();
        }

        public void onSettingsOpened() {
            if (configuration == null)
                configuration = new Configuration ();
            configuration.Load ();

        }
        public void onSettingsClosed() {
            configuration.Save ();
        }

        #endregion
    }
}
