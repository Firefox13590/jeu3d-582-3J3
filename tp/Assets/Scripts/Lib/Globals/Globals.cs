using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lib.Globals
{
    [Serializable]
    public struct Controls
    {
        public KeyCode up;
        public KeyCode right;
        public KeyCode down;
        public KeyCode left;
        public KeyCode action;
        public readonly KeyCode[] allControls
        {
            get
            {
                return new KeyCode[5] { up, right, down, left, action };
            }
        }

        public Controls(KeyCode up, KeyCode right, KeyCode down, KeyCode left, KeyCode action)
        {
            this.up = up;
            this.right = right;
            this.down = down;
            this.left = left;
            this.action = action;
        }

        public override readonly string ToString()
        {
            return $"Up: {up}, Right: {right}, Down: {down}, Left: {left}, Action: {action}";
        }
    }
}