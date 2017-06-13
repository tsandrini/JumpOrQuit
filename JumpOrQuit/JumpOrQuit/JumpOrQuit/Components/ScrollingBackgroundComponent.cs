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
    public class ScrollingBackgroundComponent : DrawableGameComponent
    {
        private Game game;
        private GameSettings settings;

        public List<ScrollingBackground> backgrounds;
        public bool canScroll;

        public ScrollingBackgroundComponent(Game game, GameSettings settings)
            : base(game)
        {
            this.game = game;
            this.settings = settings;
            this.backgrounds = new List<ScrollingBackground>();
            this.canScroll = true;

            this.DrawOrder = (int)DisplayLayer.Background;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (canScroll)
            {
                for (int i = 0; i < backgrounds.Count; i++)
                {
                    if (backgrounds[i].horizontal)
                    {
                        if (backgrounds[i].rectangle.X + backgrounds[i].rectangle.Width <= 0)
                        {
                            if (backgrounds[i] == this.backgrounds.Last())
                            {
                                backgrounds[i].rectangle.X = backgrounds.First().rectangle.X + backgrounds.First().rectangle.Width;
                            }
                            else
                            {
                                backgrounds[i].rectangle.X = backgrounds[i + 1].rectangle.X + backgrounds[i + 1].rectangle.Width;
                            }
                        }
                    }
                    else
                    {
                        if (backgrounds[i].rectangle.Y - this.game.viewport.Height >= 0)
                        {
                            if (backgrounds[i] == this.backgrounds.Last())
                            {
                                backgrounds[i].rectangle.Y = backgrounds.First().rectangle.Y - backgrounds.First().rectangle.Height;
                            }
                            else
                            {
                                backgrounds[i].rectangle.Y = backgrounds[i + 1].rectangle.Y - backgrounds[i + 1].rectangle.Height;
                            }
                        }
                    }

                    backgrounds[i].Update();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.game.spriteBatch.Begin();

            foreach (ScrollingBackground background in this.backgrounds)
            {
                background.Draw(this.game.spriteBatch);
            }

            this.game.spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ChangeTexture(Texture2D texture)
        {
            foreach (ScrollingBackground background in backgrounds)
            {
                background.texture = texture;
            }
        }

        public override void Refresh()
        {
            if (this.game.GameStateChanged())
            {
                this.backgrounds.Clear();

                if (this.game.gameState == GameState.Menu)
                {
                    this.backgrounds.Add(new ScrollingBackground(
                        this.settings.activeBackground,
                        new Rectangle(0, 0, (int)(this.game.viewport.Width * 1.5f), this.game.viewport.Height),
                        true
                    ));

                    this.backgrounds.Add(new ScrollingBackground(
                        this.settings.activeBackground,
                        new Rectangle((int)(this.game.viewport.Width * 1.5f), 0, (int)(this.game.viewport.Width * 1.5f), this.game.viewport.Height),
                        true
                    ));
                }
                else if (this.game.gameState == GameState.Playing)
                {
                    this.backgrounds.Add(new ScrollingBackground(
                        this.settings.activeBackground,
                        new Rectangle(0, 0, this.game.viewport.Width, (int)(this.game.viewport.Height * 1.5f)),
                        false
                    ));

                    this.backgrounds.Add(new ScrollingBackground(
                        this.settings.activeBackground,
                        new Rectangle(0, - (int)(this.game.viewport.Height * 1.5f) , this.game.viewport.Width, (int)(this.game.viewport.Height * 1.5f)),
                        false
                    ));
                }
            }
        }
    }
}