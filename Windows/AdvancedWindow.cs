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
    public class AdvancedWindow : CmWindow
    {
        private string cleanliness = "";
        private string guests = "";
        private string money = "";
        private int parsedGuests;
        private int parsedSetGuests;
        private int parsedSpeed = 1;
        private string priceSatisfaction = "";
        private string setGuests = "";
        private string speed = "";

        public AdvancedWindow(CheatModController cheatController) : base(cheatController)
        {
            windowName = "Advanced Cheat Mod";
            WindowRect = new Rect(620, 20, 400, 200);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Set money:");
            money = GUILayout.TextField(money);
            if (GUILayout.Button("Confirm"))
            {
                int value;
                var parsed = int.TryParse(money, out value);
                if (parsed)
                {
                    GameController.Instance.park.parkInfo.setMoney(value);
                }
                else
                {
                    var mw = Controller.GetWindow<MessageWindow>();
                    mw.message = "Please enter a valid integer";
                    mw.OpenWindow();
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Spawn guests:");
            guests = GUILayout.TextField(guests);
            if (GUILayout.Button("Confirm"))
            {
                int value;
                var parsed = int.TryParse(guests, out value);
                if (parsed)
                {
                    parsedGuests = value;
                    if (value > 200)
                    {
                        var w = Controller.GetWindow<ConfirmWindow>();
                        w.Signal += yn => { confirmGuests(yn); };
                        w.message = "Spawning more than 200 guests can decrease performance!";
                        w.OpenWindow();
                    }
                    else
                    {
                        confirmGuests(true);
                    }
                }
                else
                {
                    var mw = Controller.GetWindow<MessageWindow>();
                    mw.message = "Please enter a valid integer";
                    mw.OpenWindow();
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Set speed:");
            speed = GUILayout.TextField(speed);
            if (GUILayout.Button("Confirm"))
            {
                int value;
                var parsed = int.TryParse(speed, out value);
                if (parsed)
                {
                    parsedSpeed = value;
                    if (value > 15)
                    {
                        var w = Controller.GetWindow<ConfirmWindow>();
                        w.Signal += yn => { confirmSpeed(yn); };
                        w.message = "Setting the time too high can decrease performance!";
                        w.OpenWindow();
                    }
                    else
                    {
                        confirmSpeed(true);
                    }
                }
                else
                {
                    var mw = Controller.GetWindow<MessageWindow>();
                    mw.message = "Please enter a valid integer";
                    mw.OpenWindow();
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Rate cleanliness:");
            cleanliness = GUILayout.TextField(cleanliness);
            if (GUILayout.Button("Confirm"))
            {
                int value;
                var parsed = int.TryParse(cleanliness, out value);
                if (parsed)
                {
                    GameController.Instance.park.parkInfo.rateCleanliness(value);
                }
                else
                {
                    var mw = Controller.GetWindow<MessageWindow>();
                    mw.message = "Please enter a valid integer";
                    mw.OpenWindow();
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Rate price satisfaction:");
            priceSatisfaction = GUILayout.TextField(priceSatisfaction);
            if (GUILayout.Button("Confirm"))
            {
                int value;
                var parsed = int.TryParse(priceSatisfaction, out value);
                if (parsed)
                {
                    GameController.Instance.park.parkInfo.ratePriceSatisfaction(value);
                }
                else
                {
                    var mw = Controller.GetWindow<MessageWindow>();
                    mw.message = "Please enter a valid integer";
                    mw.OpenWindow();
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Set amount of guests:");
            setGuests = GUILayout.TextField(setGuests);
            if (GUILayout.Button("Confirm"))
            {
                int value;
                var parsed = int.TryParse(setGuests, out value);
                if (parsed)
                {
                    var guestAmount = GameController.Instance.park.getGuests().Count;
                    parsedSetGuests = value - guestAmount;
                    if (value > 200)
                    {
                        var w = Controller.GetWindow<ConfirmWindow>();
                        w.Signal += yn => { confirmSetGuests(yn); };
                        w.message = "Spawning more than 200 guests can decrease performance!";
                        w.OpenWindow();
                    }
                    else
                    {
                        confirmSetGuests(true);
                    }
                }
                else
                {
                    var mw = Controller.GetWindow<MessageWindow>();
                    mw.message = "Please enter a valid integer";
                    mw.OpenWindow();
                }
            }

            GUILayout.EndHorizontal();
        }

        private bool confirmGuests(bool yn)
        {
            if (yn)
                for (var i = 0; i < parsedGuests; i++)
                    GameController.Instance.park.spawnGuest();

            return true;
        }

        private bool confirmSpeed(bool yn)
        {
            if (yn)
            {
                var oldTimeScale = Time.timeScale;
                Time.timeScale = parsedSpeed;
                EventManager.Instance.RaiseOnTimeSpeedChanged(oldTimeScale, parsedSpeed);
            }

            return true;
        }

        private bool confirmSetGuests(bool yn)
        {
            if (yn)
            {
                Debug.Log("confirmSetGuests");
                Debug.Log(parsedSetGuests);
                if (parsedSetGuests > 0)
                {
                    Debug.Log("adding guests");
                    for (var i = 0; i < parsedSetGuests; i++) GameController.Instance.park.spawnGuest();
                }
                else
                {
                    Debug.Log("Removing guests");
                    var guestListReadOnly = GameController.Instance.park.getGuests();
                    var guestList = new Guest[guestListReadOnly.Count];
                    guestListReadOnly.CopyTo(guestList, 0);
                    for (var i = 0; i < parsedSetGuests * -1; i++) guestList[i].Kill();
                }
            }

            return true;
        }
    }
}