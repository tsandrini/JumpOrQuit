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
using JumpOrQuit.Helpers;

using DrawableGameComponent = JumpOrQuit.Classes.RefreshableGameComponent;

namespace JumpOrQuit.Components
{
    public class LevelComponent : DrawableGameComponent
    {
        private Game game;
        private Player player;

        private bool gameRunning;

        public LevelComponent(Game game)
            : base(game)
        {
            this.game = game;
            this.gameRunning = true;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.game.settings.addSprite(TextureContent.LoadListContent<Texture2D>(this.game.Content, @"Graphics\Sprites\Player"));
            this.player = new Player(this.game.settings.activeSprite, new Vector2(500, 500));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.gameRunning)
            {
                this.player.Update();
                this.player.jumping = this.game.KeyDown(Keys.Space);

                if (game.KeyDown(Keys.Left))
                {
                    this.player.left = true;
                    this.player.right = false;
                }
                else
                {
                    this.player.left = false;
                    this.player.right = this.game.KeyDown(Keys.Right);
                }

            }

            if (this.game.KeyDown(Keys.Escape))
            {
                this.game.SwitchWindows(this.game.menuWindow);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();

            this.player.Draw(this.game.spriteBatch);
            this.game.spriteBatch.MuchCoolerFont(this.game.menuFont, this.player.spriteDuration.ToString(), new Vector2(100, 100), Color.Black, 1);

            game.spriteBatch.End();

            base.Draw(gameTime);
        }
         

        public override void Refresh()
        {
            this.player.Reset(this.game.settings.activeSprite);
        }
    }
}
