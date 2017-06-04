using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpOrQuit.Classes
{
    public class Obstacle
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 sizes;
        public int scrollingSpeed;

        public Obstacle(Vector2 position, Vector2 sizes)
        {
            this.sizes = sizes;
            this.position = position;
            this.scrollingSpeed = 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(
                    texture, 
                    new Rectangle( (int) position.X, (int) position.Y, (int) sizes.X, (int) sizes.Y), 
                    Color.White
                );
            }
        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public bool InBounds(int x, int y)
        {
            Rectangle rect = new Rectangle((int) this.position.X, (int) this.position.Y, (int) this.sizes.X, (int) this.sizes.Y);

            return rect.Contains(x, y);
        }
    }

    public class Ramp : Obstacle
    {
        public Ramp(Vector2 position, Vector2 sizes) : base (position, sizes)
        {
        }
    
        public void Update()
        {
            this.position.Y += scrollingSpeed;
        }
    }
}
