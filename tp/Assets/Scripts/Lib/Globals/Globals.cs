using UnityEngine;
using System;

namespace Lib.Globals
{
    [Serializable]
    public struct Controls
    {
        public KeyCode Up { get; private set; }
        public KeyCode Right { get; private set; }
        public KeyCode Down { get; private set; }
        public KeyCode Left { get; private set; }
        public KeyCode Action { get; private set; }
        public readonly KeyCode[] AllControls
        {
            get
            {
                return new KeyCode[5] { Up, Right, Down, Left, Action };
            }
        }

        public Controls(KeyCode up, KeyCode right, KeyCode down, KeyCode left, KeyCode action)
        {
            Up = up;
            Right = right;
            Down = down;
            Left = left;
            Action = action;
        }

        public override readonly string ToString()
        {
            return $"Up: {Up}, Right: {Right}, Down: {Down}, Left: {Left}, Action: {Action}";
        }
    }
}