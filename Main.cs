using UnityEngine;

namespace CheatMod
{
    public class Main : IMod
    {
        public GameObject _go;

        public string Description
        {
            get
            {
                return "Cheats for Parkitect.";
            }
        }

        public string Name
        {
            get
            {
                return "Cheat Mod";
            }
        }

        public void onDisabled()
        {
            UnityEngine.Object.Destroy(_go);
        }

        public void onEnabled()
        {
            _go = new GameObject();
            var cheatModController = _go.AddComponent<CheatModController>();
          //  cheatModController.Load ();
        }

        public string Identifier { get; set; }

        public string Path { get; set; }
    }
}
