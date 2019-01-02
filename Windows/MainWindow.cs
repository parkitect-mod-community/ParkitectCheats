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
    public class MainWindow : CmWindow
    {
        public MainWindow(CheatModController cheatController) : base(cheatController)
        {
            windowName = "Cheat Mod";
            WindowRect = new Rect(20, 20, 700, 200);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Spawn 1 Guest")) GameController.Instance.park.spawnGuest();
            if (GUILayout.Button("Spawn 5 Guests"))
                for (byte i = 0; i < 5; i++)
                    GameController.Instance.park.spawnGuest();
            if (GUILayout.Button("Spawn 10 Guests"))
                for (byte i = 0; i < 10; i++)
                    GameController.Instance.park.spawnGuest();
            if (GUILayout.Button("Spawn 100 Guest"))
                for (byte i = 0; i < 100; i++)
                    GameController.Instance.park.spawnGuest();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Give $1K"))
                GameController.Instance.park.parkInfo.moneyTransaction(1000, MonthlyTransactions.Transaction.WAGES);
            if (GUILayout.Button("Give $10K"))
                GameController.Instance.park.parkInfo.moneyTransaction(10000, MonthlyTransactions.Transaction.WAGES);
            if (GUILayout.Button("Give $100K"))
                GameController.Instance.park.parkInfo.moneyTransaction(100000, MonthlyTransactions.Transaction.WAGES);
            if (GUILayout.Button("Give $1M"))
                GameController.Instance.park.parkInfo.moneyTransaction(1000000, MonthlyTransactions.Transaction.WAGES);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Rate cleanliness 1")) GameController.Instance.park.parkInfo.rateCleanliness(1.0F);
            if (GUILayout.Button("Rate cleanliness 10")) GameController.Instance.park.parkInfo.rateCleanliness(5.0F);
            if (GUILayout.Button("Rate cleanliness 100")) GameController.Instance.park.parkInfo.rateCleanliness(10.0F);
            GUILayout.Label("Current Rating: " + GameController.Instance.park.parkInfo.RatingCleanliness);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Rate price statisfaction 1"))
                GameController.Instance.park.parkInfo.ratePriceSatisfaction(1.0F);
            if (GUILayout.Button("Rate price statisfaction 10"))
                GameController.Instance.park.parkInfo.ratePriceSatisfaction(5.0F);
            if (GUILayout.Button("Rate price statisfaction 100"))
                GameController.Instance.park.parkInfo.ratePriceSatisfaction(10.0F);
            GUILayout.Label("Current Rating: " + GameController.Instance.park.parkInfo.RatingPriceSatisfaction);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open advanced window")) Controller.GetWindow<AdvancedWindow>().OpenWindow();
            if (GUILayout.Button("Open weather and time options")) Controller.GetWindow<WeatherWindow>().OpenWindow();

            if (GUILayout.Button("Open global toggles")) Controller.GetWindow<GlobalToggles>().OpenWindow();
            GUILayout.EndHorizontal();
        }
    }
}