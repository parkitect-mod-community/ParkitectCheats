using System.Collections.ObjectModel;
using UnityEngine;

namespace CheatMod.Windows
{
    public class AdvancedWindow : CMWindow
    {
        private string money = "";
        private string guests = "";
        private int parsedGuests = 0;
        private string speed = "";
        private int parsedSpeed = 1;
        private string cleanliness = "";
        private string priceSatisfaction = "";
        private string setGuests = "";
        private int parsedSetGuests = 0;

        public AdvancedWindow(CheatModController cheatController) : base(cheatController) {
            
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
                bool parsed = int.TryParse(money, out value);
                if (parsed)
                {
                    GameController.Instance.park.parkInfo.setMoney (value);
                }
                else
                {
                    
                    MessageWindow mw = _controller.GetWindow<MessageWindow>();
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
                bool parsed = int.TryParse(guests, out value);
                if (parsed)
                {
                    parsedGuests = value;
                    if (value > 200)
                    {
                        ConfirmWindow w = _controller.GetWindow<ConfirmWindow> ();
                        w.Signal += (bool yn) => {
							confirmGuests(yn);
                        };
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
                    MessageWindow mw = _controller.GetWindow<MessageWindow> ();
                    mw.message = "Please enter a valid integer";
                    mw.OpenWindow();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Set speed:");
            speed = GUILayout.TextField(speed);
            if (GUILayout.Button("Confirm")) {
                int value;
                bool parsed = int.TryParse(speed, out value);
                if (parsed)
                {
                    parsedSpeed = value;
                    if(value > 15)
                    {
                        ConfirmWindow w = _controller.GetWindow<ConfirmWindow> ();
                        w.Signal += (yn) => {
                            confirmSpeed (yn);
                        };
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
                    MessageWindow mw = _controller.GetWindow<MessageWindow> ();
                    mw.message = "Please enter a valid integer";
                    mw.OpenWindow();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Rate cleanliness:");
            cleanliness = GUILayout.TextField(cleanliness);
            if (GUILayout.Button("Confirm")) {
                int value;
                bool parsed = int.TryParse(cleanliness, out value);
                if (parsed)
                {
                    GameController.Instance.park.parkInfo.rateCleanliness(value);
                }
                else
                {
                    MessageWindow mw = _controller.GetWindow<MessageWindow> ();
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
                bool parsed = int.TryParse(priceSatisfaction, out value);
                if (parsed)
                {
                    GameController.Instance.park.parkInfo.ratePriceSatisfaction(value);
                }
                else
                {
                    MessageWindow mw =_controller.GetWindow<MessageWindow> ();
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
                bool parsed = int.TryParse(setGuests, out value);
                if (parsed)
                {
                    int guestAmount = GameController.Instance.park.getGuests().Count;
                    parsedSetGuests = value - guestAmount;
                    if (value > 200)
                    {
                        ConfirmWindow w = _controller.GetWindow<ConfirmWindow> ();
                        w.Signal += (bool yn) => {
                            confirmSetGuests(yn);
                        };
                        w.message = "Spawning more than 200 guests can decrease performance!";
                        w.OpenWindow();
                    }
                    else
                    {
                        confirmSetGuests(true);
                    }
                }
                else {
                    MessageWindow mw =_controller.GetWindow<MessageWindow> ();
                    mw.message = "Please enter a valid integer";
                    mw.OpenWindow();
                }
            }
            GUILayout.EndHorizontal();
        }

        bool confirmGuests(bool yn)
        {
            if (yn)
            {
                for(int i = 0; i < parsedGuests; i++)
                {
                    GameController.Instance.park.spawnGuest();
                }
            }

            return true;
        }

        bool confirmSpeed(bool yn)
        {
            if (yn)
            {
                float oldTimeScale = Time.timeScale;
                Time.timeScale = parsedSpeed;
                EventManager.Instance.RaiseOnTimeSpeedChanged(oldTimeScale, parsedSpeed);
            }
            return true;
        }

        bool confirmSetGuests(bool yn)
        {

            if (yn)
            {
                Debug.Log("confirmSetGuests");
                Debug.Log(parsedSetGuests);
                if (parsedSetGuests > 0)
                {
                    Debug.Log("adding guests");
                    for (int i = 0; i < parsedSetGuests; i++)
                    {
                        
                        GameController.Instance.park.spawnGuest();
                        
                    }
                }
                else {
                    Debug.Log("Removing guests");
                    ReadOnlyCollection<Guest> guestListReadOnly = GameController.Instance.park.getGuests();
                    Guest[] guestList = new Guest[guestListReadOnly.Count];
                    guestListReadOnly.CopyTo(guestList, 0);
                    for(int i = 0; i < parsedSetGuests * -1; i++)
                    {
                        guestList[i].Kill();
                    }
                }
            }
            return true;
        }
    }
}
