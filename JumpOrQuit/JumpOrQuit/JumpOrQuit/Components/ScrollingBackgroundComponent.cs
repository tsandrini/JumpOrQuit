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

using DrawableGameComponent = JumpOrQuit.Core.RefreshableGameComponent;

namespace JumpOrQuit.Components
{
    public class ScrollingBackgroundComponent : DrawableGameComponent
    {
        private Game game;
        private List<ScrollingBackground> backgrounds;

        public ScrollingBackgroundComponent(Game game)
            : base(game)
        {
            this.game = game;
            this.backgrounds = new List<ScrollingBackground>();
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < backgrounds.Count; i++)
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

                backgrounds[i].Update();
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

        public void AddBackground(ScrollingBackground background)
        {
            if (!this.backgrounds.Contains(background))
            {
                this.backgrounds.Add(background);
            }
        }
    }
}