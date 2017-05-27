using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpOrQuit.Classes
{
    public class Background
    {
        public Texture2D texture;
        public Rectangle rectangle;

        public Background(Texture2D texture, Rectangle rectangle)
        {
            this.texture = texture;
            this.rectangle = rectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    public class ScrollingBackground : Background
    {
        public int scrollingSpeed;

        public ScrollingBackground(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
            scrollingSpeed = 1;
        }

        public ScrollingBackground(Texture2D texture, Rectangle rectangle, int scrollingSpeed) : base(texture, rectangle)
        {
            this.scrollingSpeed = scrollingSpeed;
        }

        public void Update()
        {
            this.rectangle.X -= scrollingSpeed;
        }
    }

}
