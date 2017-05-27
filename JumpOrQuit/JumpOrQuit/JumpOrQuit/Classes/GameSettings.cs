using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpOrQuit.Classes
{
    public class GameSettings
    {
        public List<Dictionary<string, Texture2D>> avaibleSprites;
        private int activeSpriteKey;

        public Dictionary<string, Texture2D> activeSprite
        {
            get
            {
                return this.avaibleSprites.ElementAt(activeSpriteKey);
            }
        }

        public GameSettings()
        {
            this.activeSpriteKey = 0;
            this.avaibleSprites = new List<Dictionary<string, Texture2D>>();
        }

        public void addSprite(Dictionary<string, Texture2D> sprite)
        {
            this.avaibleSprites.Add(sprite);
        }

    }
}
