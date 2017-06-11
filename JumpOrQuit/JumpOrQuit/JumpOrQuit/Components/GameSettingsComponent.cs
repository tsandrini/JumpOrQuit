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
using JumpOrQuit.Enums;

using DrawableGameComponent = JumpOrQuit.Core.RefreshableGameComponent;

namespace JumpOrQuit.Components
{
    public class GameSettingsComponent : DrawableGameComponent
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

            this.DrawOrder = (int)DisplayLayer.MenuBack;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
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

            if (this.game.KeyPressed(Keys.V))
            {
                this.settings.vimMode = !this.settings.vimMode;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.game.spriteBatch.Begin();

            this.game.spriteBatch.Draw(
                this.settings.textures["sound." + (this.game.settings.soundEnabled ? "enabled" : "disabled")],
                new Rectangle(
                    (int)(this.game.viewport.Width * 0.93f),
                    (int)(this.game.viewport.Height * 0.01f),
                    64,
                    64
                ),
                Color.White
            );

            if (this.game.settings.vimMode)
            {
                this.game.spriteBatch.Draw(
                    this.settings.textures["vim-mode"],
                    new Rectangle(
                        (int)(this.game.viewport.Width * 0.93f),
                        (int)(this.game.viewport.Height * 0.15f),
                        64,
                        64
                    ),
                    Color.White
                );
            }

            this.game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
