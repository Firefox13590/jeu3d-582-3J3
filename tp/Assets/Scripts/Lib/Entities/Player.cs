using UnityEngine;
using Lib.Globals;

namespace Lib.Entities
{
    [System.Serializable]
    public class Player
    {
        public Controls controls;

        public Player()
        {
            this.controls = new Controls(KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.Return);
        }
    }
}