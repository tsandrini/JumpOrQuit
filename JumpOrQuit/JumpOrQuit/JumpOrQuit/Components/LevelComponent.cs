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
        private ScrollingBackgroundComponent scrollingBackground;

        private GameSettings settings;
        private Player player;
        private List<Ramp> ramps;
        private Ramp currentRamp, lastRamp;

        private Random random;
        private bool canScroll, canEarnPoint;
        // Current points used for increasing difficulty
        private int points, currentPoints, lives, difficulty;

        public LevelComponent(Game game, GameSettings settings, ScrollingBackgroundComponent scrollingBackground)
            : base(game)
        {
            this.game = game;
            this.settings = settings;
            this.player = new Player(new Vector2(this.game.viewport.Width * 0.5f, (this.game.viewport.Height - (this.settings.rampThickness * 1.5f))));
            this.ramps = new List<Ramp>();
            this.scrollingBackground = scrollingBackground;
            this.random = new Random();

            this.DrawOrder = (int)DisplayLayer.Player;
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
                // Checks whether playing is falling or not
                foreach (Ramp ramp in ramps)
                {
                    if (ramp.InBounds((int)player.pos.X, (int)player.pos.Y))
                    {
                        if (ramp != ramps.First())
                        {
                            canScroll = true;
                            this.scrollingBackground.canScroll = true;
                            
                            // Don't fall but still go down
                            this.player.pos.Y += ramp.scrollingSpeed;
                        }

                        this.lastRamp = this.currentRamp;
                        this.currentRamp = ramp;
                        this.canEarnPoint = currentRamp != lastRamp;
                        this.player.falling = false;
                        break;
                    }
                    // If not standing on any ramp
                    else if (ramp == ramps.Last())
                    {
                        this.player.falling = true;
                    }
                }

                this.player.jumping = (!settings.vimMode && (game.KeyDown(Keys.Space) || game.KeyDown(Keys.Up)) || (settings.vimMode && game.KeyDown(Keys.K)));
                this.player.crouching = (!settings.vimMode && this.game.KeyDown(Keys.Down)) || (settings.vimMode && game.KeyDown(Keys.J));

                if (((!settings.vimMode && game.KeyDown(Keys.Left)) || (settings.vimMode && game.KeyDown(Keys.H))) && this.player.pos.X > 0)
                {
                    this.player.left = true;
                    this.player.right = false;
                }
                else if (((!settings.vimMode && game.KeyDown(Keys.Right)) || (settings.vimMode && game.KeyDown(Keys.L)))
                    && (this.player.pos.X + this.player.width) < this.game.viewport.Width)
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
                                if (ramp.position.Y >= 0)
                                {
                                    ramp.position.Y = 0 - (ramp.sizes.Y - this.game.viewport.Height);
                                }
                            }
                            else if (ramp.position.Y > this.game.viewport.Height)
                            {
                                ramp.position.Y = 0;
                                ramp.sizes.X = random.Next((int)(game.viewport.Width * 0.2f), (int)(game.viewport.Width * 0.4f));
                                ramp.position.X = random.Next((int)(this.game.viewport.Width * 0.1f), (int)(this.game.viewport.Width - ramp.sizes.X - settings.rampThickness));
                            }
                        }
                    }

                    // Player fell of the screen
                    if ((this.player.pos.Y - this.player.height) >= this.game.viewport.Height)
                    {
                        this.lives--;
                        if (lives != 0)
                        {
                            if (this.settings.soundEnabled) this.game.settings.sounds["game.death"].Play();
                            this.Redraw();
                        }
                        else
                        {
                            this.game.gameState = GameState.GameEnded;
                        }
                    }
                }

                if (this.canEarnPoint)
                {
                    this.points += 5;
                    this.currentPoints += 5;
                    this.canEarnPoint = false;
                }

                // Difficulty is increased dynamically. 
                if (this.currentPoints != 0 && (this.currentPoints % (10 * Math.Pow(2, difficulty)) == 0))
                {
                    IncreaseDifficulty();
                }

                // Jumping sounds
                if (settings.soundEnabled && this.player.canJump && 
                    ((!settings.vimMode && (game.KeyPressed(Keys.Up) || game.KeyPressed(Keys.Space))) || (settings.vimMode && game.KeyPressed(Keys.K))))
                {
                    this.game.settings.sounds["game.jump." + random.Next(1, 4).ToString()].Play();
                }
            }

            if (this.game.KeyPressed(Keys.Escape))
            {
                this.game.gameState = GameState.Menu;
                this.game.SwitchWindows(this.game.menuWindow);
            }

            // Player's lost
            if (this.game.gameState == GameState.GameEnded)
            {
                if (this.settings.soundEnabled) this.game.settings.sounds["game.end"].Play();
                this.game.gameState = GameState.Menu;
                this.game.SwitchWindows(this.game.menuWindow);
            }


            base.Update(gameTime);
        }

        public void IncreaseDifficulty()
        {
            this.difficulty++;
            this.player.fallingSpeed++;
            this.player.jumpingSpeed++;
            this.player.movementSpeed++;
            this.player.maxJumpDuration--;

            this.settings.activeBackgroundKey = this.settings.activeBackground == this.settings.avaibleBackgrounds.Last() 
                ? 0 : this.settings.activeBackgroundKey + 1;

            this.scrollingBackground.ChangeTexture(this.settings.activeBackground);
            
            this.settings.activeRampKey = this.settings.activeRamp == this.settings.avaibleRamps.Last()
                ? 0 : this.settings.activeRampKey + 1;

            foreach (Ramp ramp in this.ramps)
            {
                ramp.SetTexture(this.settings.activeRamp);
                ramp.scrollingSpeed++;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();

            this.game.spriteBatch.MuchCoolerFont(game.menuFont, "Points: " + this.points.ToString(), new Vector2(50, 50), Color.AliceBlue, 1);
            this.game.spriteBatch.MuchCoolerFont(game.menuFont, "HP: " + this.lives.ToString(), new Vector2(50, 100), Color.AliceBlue, 1);
            this.game.spriteBatch.MuchCoolerFont(game.menuFont, "Lvl: " + this.difficulty.ToString(), new Vector2(50, 150), Color.AliceBlue, 1);

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
            this.points = this.currentPoints = 0;
            this.lives = this.settings.defaultPlayerLives;

            this.Redraw();
        }

        public void Redraw()
        {
            this.ramps.Clear();
            this.canScroll = this.canEarnPoint = this.scrollingBackground.canScroll =  false;
            this.difficulty = 1;
            this.currentPoints = 0;

            // Main base ramp
            this.ramps.Add(new Ramp(
                    new Vector2(0, this.game.viewport.Height - this.settings.rampThickness),
                    new Vector2(this.game.viewport.Width, this.settings.rampThickness),
                    this.settings.defaultScrollingSpeed
            ));

            // Side ramp
            this.ramps.Add(new Ramp(
                new Vector2(0, - this.game.viewport.Height * 0.5f),
                new Vector2(this.settings.rampThickness, this.game.viewport.Height * 1.5f),
                this.settings.defaultScrollingSpeed
            ));

            // Side ramp
            this.ramps.Add(new Ramp(
                new Vector2(this.game.viewport.Width - this.settings.rampThickness, - this.game.viewport.Height * 0.5f),
                new Vector2(this.settings.rampThickness, this.game.viewport.Height * 1.5f),
                this.settings.defaultScrollingSpeed
            ));

            this.currentRamp = this.lastRamp = this.ramps.First();

            int distanceBetween = this.game.viewport.Height / this.game.settings.rampsCount;
            
            for (int i = 0; i < this.settings.rampsCount; i++)
            {
                this.ramps.Add(new Ramp(
                    new Vector2(random.Next((int) (game.viewport.Width * 0.2f), (int) (game.viewport.Width * 0.8f)), distanceBetween * i + this.game.viewport.Width * 0.1f),
                    new Vector2(random.Next((int) (game.viewport.Width * 0.2f), (int) (game.viewport.Width * 0.4f)), settings.rampThickness),
                    this.settings.defaultScrollingSpeed
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
