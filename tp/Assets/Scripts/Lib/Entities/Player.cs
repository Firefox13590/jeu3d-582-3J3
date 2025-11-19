using UnityEngine;
using Lib.Globals;
using System;

namespace Lib.Entities
{
    public class Entity
    {
        public readonly string name;

        public Entity(string name)
        {
            this.name = name;
        }
    }

    [Serializable]
    public class Player : Entity
    {
        public Controls controls;

        public Player() : base("Player")
        {
            this.controls = new Controls(KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.Return);
        }
        public Player(Controls controls) : base("Player")
        {
            this.controls = controls;
        }
        public Player(string name, Controls controls) : base(name)
        {
            this.controls = controls;
        }
    }
}