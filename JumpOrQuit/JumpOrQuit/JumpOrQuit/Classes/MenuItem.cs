using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpOrQuit.Classes
{
    public class MenuItem
    {
        public string text { get; }
        public Vector2 pos { get; }
        public Vector2 size { get; set; }
        public float scale { get; }
        public string identifier;

        public MenuItem(string text, Vector2 pos, string identifier)
        {
            this.text = text;
            this.pos = pos;
            this.scale = 0.6f;
            this.identifier = identifier;
        }

        public MenuItem(string text, Vector2 pos, float scale)
        {
            this.text = text;
            this.pos = pos;
            this.scale = scale;
        }
    }
}
