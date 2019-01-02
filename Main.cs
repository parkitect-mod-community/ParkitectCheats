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

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CheatsMod
{
    public class Main : IMod, IModSettings
    {
        private GameObject _go;
        public string Identifier { get; set; }
        public static Configuration configuration { get; private set; }

        public void onEnabled()
        {
            if (configuration == null)
            {
                configuration = new Configuration();
                configuration.Load();
                configuration.Save();
            }

            if (configuration.settings.debugBuildMode)
                ScriptableSingleton<DebugPreferences>.Instance.drawDebugUI = true;


            _go = new GameObject();
            var modController = _go.AddComponent<CheatModController>();
            modController.Load();
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
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
                if (Input.GetKeyDown(key))
                {
                    outKey = key;
                    return true;
                }

            outKey = KeyCode.A;
            return false;
        }


        public void onDrawSettingsUI()
        {
            configuration.DrawGUI();
        }

        public void onSettingsOpened()
        {
            if (configuration == null)
                configuration = new Configuration();
            configuration.Load();
        }

        public void onSettingsClosed()
        {
            configuration.Save();
        }

        #endregion
    }
}