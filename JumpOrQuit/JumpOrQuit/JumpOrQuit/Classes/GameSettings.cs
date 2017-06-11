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
        public List<Texture2D> avaibleBackgrounds;

        public Dictionary<string, SoundEffect> sounds;
        public Dictionary<string, Texture2D> textures;

        public int rampsCount, rampThickness;
        public bool soundEnabled, vimMode;

        public int activeSpriteKey, activeRampKey, activeBackgroundKey, defaultPlayerLives, defaultScrollingSpeed;

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

        public Texture2D activeBackground
        {
            get
            {
                return this.avaibleBackgrounds.ElementAt(activeBackgroundKey);
            }
        }

        public GameSettings()
        {
            this.activeSpriteKey = activeRampKey = activeBackgroundKey = 0;
            this.rampsCount = 3;
            this.soundEnabled = true;
            this.vimMode = false;
            this.rampThickness = 25
                ;
            this.defaultPlayerLives = 3;
            this.defaultScrollingSpeed = 4;

            this.avaibleSprites = new List<Dictionary<string, Texture2D>>();
            this.avaibleRamps = new List<Texture2D>();
            this.sounds = new Dictionary<string, SoundEffect>();
            this.textures = new Dictionary<string, Texture2D>();
            this.avaibleBackgrounds = new List<Texture2D>();
        }

        public void addSprite(Dictionary<string, Texture2D> sprite)
        {
            this.avaibleSprites.Add(sprite);
        }

    }
}
