using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using JumpOrQuit.Classes;

namespace JumpOrQuit.Components
{
    public class GameSettingsComponent : Microsoft.Xna.Framework.GameComponent
    {
        private Game game;
        private GameSettings settings;
        private GraphicsDeviceManager graphics;

        public GameSettingsComponent(Game game, GameSettings settings, GraphicsDeviceManager graphics)
            : base(game)
        {
            this.game = game;
            this.settings = settings;
            this.graphics = graphics;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.game.KeyPressed(Keys.F))
            {
                this.graphics.IsFullScreen = !this.graphics.IsFullScreen;
                this.graphics.ApplyChanges();
            }

            if (this.game.KeyPressed(Keys.M))
            {
                this.settings.soundEnabled = !this.settings.soundEnabled;
            }

            base.Update(gameTime);
        }
    }
}
