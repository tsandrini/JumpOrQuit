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
using JumpOrQuit.Enums;

using DrawableGameComponent = JumpOrQuit.Core.RefreshableGameComponent;

namespace JumpOrQuit.Components
{
    public class LevelComponent : DrawableGameComponent
    {
        private Game game;
        private GameSettings settings;
        private Player player;
        private List<Ramp> ramps;

        private Random random;
        private bool canScroll;

        public LevelComponent(Game game, GameSettings settings)
            : base(game)
        {
            this.game = game;
            this.settings = settings;
            this.player = new Player(new Vector2(500, 500));
            this.ramps = new List<Ramp>();
            this.random = new Random();
            this.canScroll = false;
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
            if (this.game.gameState == GameState.Playing)
            {
                foreach (Ramp ramp in ramps)
                {
                    if (ramp.InBounds((int)player.pos.X, (int)player.pos.Y))
                    {
                        if (ramp != ramps.First())
                        {
                            canScroll = true;
                        }
                        this.player.falling = false;
                        break;
                    }
                    else if (ramp == ramps.Last())
                    {
                        this.player.falling = true;
                    }
                }

                this.player.jumping = this.game.KeyDown(Keys.Space) || this.game.KeyDown(Keys.Up);
                this.player.crouching = this.game.KeyDown(Keys.Down);

                if (game.KeyDown(Keys.Left) && this.player.pos.X > 0)
                {
                    this.player.left = true;
                    this.player.right = false;
                }
                else if (game.KeyDown(Keys.Right) && (this.player.pos.X + this.player.width) < this.game.viewport.Width)
                {
                    this.player.left = false;
                    this.player.right = true;
                }
                else
                {
                    this.player.left = this.player.right = false;
                }

                this.player.Update();

                if (this.canScroll)
                {
                    foreach (Ramp ramp in this.ramps)
                    {
                        ramp.Update();

                        // If not base ramp
                        if (ramp != ramps.First())
                        {
                            // If side ramp
                            if (ramp == ramps[1] || ramp == ramps[2])
                            {
                                if (ramp.position.Y > 0)
                                {
                                    ramp.position.Y = 0 - game.viewport.Height - ramp.sizes.Y;
                                }
                            }
                            else if (ramp.position.Y > this.game.viewport.Height)
                            {
                                ramp.position.Y = 0;
                                ramp.position.X = random.Next((int)(game.viewport.Width * 0.2f), (int)(game.viewport.Width * 0.8f));
                            }
                        }
                    }
                }
            }

            if (this.game.KeyPressed(Keys.Escape))
            {
                this.game.SwitchWindows(this.game.menuWindow);
            }

            if (this.game.settings.soundEnabled && (this.game.KeyPressed(Keys.Space) || this.game.KeyPressed(Keys.Up)) && this.player.canJump)
            {
                this.game.settings.sounds["game.jump." + random.Next(1, 4).ToString()].Play();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();

            this.player.Draw(this.game.spriteBatch);

            foreach (Ramp ramp in this.ramps)
            {
                ramp.Draw(this.game.spriteBatch);
            }

            game.spriteBatch.End();

            base.Draw(gameTime);
        }
         

        public override void Refresh()
        {
            this.ramps.Clear();

            // Main base ramp
            this.ramps.Add(new Ramp(
                    new Vector2(0, this.game.viewport.Height - this.settings.rampThickness),
                    new Vector2(this.game.viewport.Width, this.settings.rampThickness)
            ));

            // Side ramp
            this.ramps.Add(new Ramp(
                new Vector2(0, 0),
                new Vector2(this.settings.rampThickness, this.game.viewport.Height + 100)
            ));

            // Side ramp
            this.ramps.Add(new Ramp(
                new Vector2(this.game.viewport.Width - this.settings.rampThickness, 0),
                new Vector2(this.settings.rampThickness, this.game.viewport.Height * 1.5f)
            ));

            int distanceBetween = this.game.viewport.Height / this.game.settings.rampsCount;
            
            for (int i = 0; i < this.settings.rampsCount; i++)
            {
                this.ramps.Add(new Ramp(
                    new Vector2(random.Next((int) (game.viewport.Width * 0.2f), (int) (game.viewport.Width * 0.8f)), distanceBetween * i + this.game.viewport.Width * 0.1f),
                    new Vector2(random.Next((int) (game.viewport.Width * 0.1f), (int) (game.viewport.Width * 0.3f)), settings.rampThickness)
                ));
            }

            foreach (Ramp ramp in this.ramps)
            {
                ramp.SetTexture(this.settings.activeRamp);
            }

            this.player.Reset(this.game.settings.activeSprite);
        }
    }
}
