using UnityEngine;
using Lib.Globals;
using System;

namespace Lib.Entities
{
    /// <summary>
    /// Représente une entité avec un nom et une position actuelle.
    /// </summary>
    [Serializable]
    public class Entity
    {
        private int currentPos = 0;

        public string Name { get; set; }
        public int CurrentPos
        {
            get { return currentPos; }
            set { currentPos = value; }
        }

        public Entity(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Représente un joueur.
    /// </summary>
    /// <remarks>Hérite de la classe <see cref="Entity"/>.</remarks>
    [Serializable]
    public class Player : Entity
    {
        public Controls Controls { get; set; }

        public Player() : base("Player")
        {
            Controls = new Controls(KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.Return);
        }
        public Player(string name) : base(name)
        {
            Controls = new Controls(KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.Return);
        }
        public Player(Controls controls) : base("Player")
        {
            Controls = controls;
        }
        public Player(string name, Controls controls) : base(name)
        {
            Controls = controls;
        }
    }

    [Serializable]
    public class Bot : Player
    {
        public Bot() : base("Bot") { }
    }
}