using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpOrQuit.Classes
{
    public class CoolFont : SpriteBatch
    {
        public CoolFont(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {

        }

        public void CoolerFont(SpriteFont spriteFont, string text, Vector2 pos, Color color)
        {
            DrawString(spriteFont, text, new Vector2(pos.X + 2, pos.Y + 2), Color.Black * 0.8f);
            DrawString(spriteFont, text, pos, color);
        }

        public void MuchCoolerFont(SpriteFont spriteFont, string text, Vector2 pos, Color color, float scale)
        {
            DrawString(spriteFont, text, new Vector2(pos.X + 2, pos.Y + 2), Color.Black * 0.8f, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            DrawString(spriteFont, text, pos, color, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }
    }
}
