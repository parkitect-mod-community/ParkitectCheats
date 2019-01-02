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
using System.Collections.Generic;
using System.IO;
using MiniJSON;
using UnityEngine;

namespace CheatsMod
{
    public class Configuration
    {
        private static GUIStyle ToggleButtonStyleNormal;
        private static GUIStyle ToggleButtonStyleToggled;
        private readonly string _path;

        private int keySelectionId = -1;


        public Configuration()
        {
            _path = FilePaths.getFolderPath("cheats_mod.config");
            ;
            settings = new CheatsMod.ModSettings();
        }

        public CheatsMod.ModSettings settings { get; }

        public void Save()
        {
            var context = new SerializationContext(SerializationContext.Context.Savegame);

            using (var stream = new FileStream(_path, FileMode.Create))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(Json.Serialize(Serializer.serialize(context, settings)));
                }
            }
        }

        public void Load()
        {
            try
            {
                if (File.Exists(_path))
                    using (var reader = new StreamReader(_path))
                    {
                        string jsonString;

                        var context = new SerializationContext(SerializationContext.Context.Savegame);
                        while ((jsonString = reader.ReadLine()) != null)
                        {
                            var values = (Dictionary<string, object>) Json.Deserialize(jsonString);
                            Serializer.deserialize(context, settings, values);
                        }

                        reader.Close();
                    }
            }
            catch (Exception e)
            {
                Debug.Log("Couldn't properly load settings file! " + e);
            }
        }


        public void DrawGUI()
        {
            if (ToggleButtonStyleNormal == null)
            {
                ToggleButtonStyleNormal = "Button";
                ToggleButtonStyleToggled = new GUIStyle(ToggleButtonStyleNormal);
                ToggleButtonStyleToggled.normal.background = ToggleButtonStyleToggled.active.background;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Open Window:");
            settings.openWindow = KeyToggle(settings.openWindow, 0);
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            settings.debugBuildMode = GUILayout.Toggle(settings.debugBuildMode, "Ingame Debug Window");
            GUILayout.EndHorizontal();
        }

        public KeyCode KeyToggle(KeyCode character, int id)
        {
            if (GUILayout.Button(character.ToString(),
                keySelectionId == id ? ToggleButtonStyleToggled : ToggleButtonStyleNormal)) keySelectionId = id;

            if (keySelectionId == id)
            {
                KeyCode e;
                if (FetchKey(out e))
                {
                    keySelectionId = -1;
                    return e;
                }
            }

            return character;
            //selectedKey = Keyb
        }

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
    }
}