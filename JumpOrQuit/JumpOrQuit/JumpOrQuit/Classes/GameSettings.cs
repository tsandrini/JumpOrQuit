using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace JumpOrQuit.Classes
{
    public class GameSettings
    {
        public List<Dictionary<string, Texture2D>> avaibleSprites;
        public List<Texture2D> avaibleRamps;
        public Dictionary<string, SoundEffect> sounds;

        public int rampsCount, rampThickness;
        public bool soundEnabled;

        private int activeSpriteKey, activeRampKey;

        public Dictionary<string, Texture2D> activeSprite
        {
            get
            {
                return this.avaibleSprites.ElementAt(activeSpriteKey);
            }
        }

        public Texture2D activeRamp
        {
            get
            {
                return this.avaibleRamps.ElementAt(activeRampKey);
            }
        }

        public GameSettings()
        {
            this.activeSpriteKey = activeRampKey = 0;
            this.rampsCount = 2;
            this.soundEnabled = true;
            this.rampThickness = 20;

            this.avaibleSprites = new List<Dictionary<string, Texture2D>>();
            this.avaibleRamps = new List<Texture2D>();
            this.sounds = new Dictionary<string, SoundEffect>();
        }

        public void addSprite(Dictionary<string, Texture2D> sprite)
        {
            this.avaibleSprites.Add(sprite);
        }

    }
}
