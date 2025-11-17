using UnityEngine;
using Lib.Globals;

namespace Lib.Entities
{
    [System.Serializable]
    public class Player
    {
        public PlayerControls controls;

        public Player()
        {
            this.controls = new PlayerControls(KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.Return);
        }
    }
}