using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using JumpOrQuit.Components;

namespace JumpOrQuit.Classes
{
    public class Player
    {
        public Dictionary<string, Texture2D> sprite;
        public Vector2 pos, startingPos, origins;
        public bool falling, jumping, left, right;
        public string lastSprite, currentSprite;
        public int spriteDuration, maxSpriteDuration, fallingSpeed, movementSpeed, jumpingSpeed;

        private string walkSprite, standSprite;

        public Player(Dictionary<string, Texture2D> sprite, Vector2 pos)
        {
            this.pos = this.startingPos = pos;
            this.Reset(sprite);
        }

        public void Reset(Dictionary<string, Texture2D> sprite)
        {
            this.sprite = sprite;
            this.origins = new Vector2(sprite["idle"].Width / 2f, sprite["idle"].Height / 2f);
            this.falling = this.left = this.right;
            this.fallingSpeed = this.movementSpeed = jumpingSpeed = 3;
            this.maxSpriteDuration = 10;

            this.walkSprite = "walk1";
            this.standSprite = "idle";
        }

        public void Update()
        {
            lastSprite = currentSprite;

            if (falling)
            {
                this.currentSprite = "fall";
                this.pos.Y += fallingSpeed;
            }
            else if (jumping) 
            {
                this.currentSprite = "jump";
                this.pos.Y -= jumpingSpeed;
            } 
            else if (left || right)
            {
                this.currentSprite = walkSprite;
            }
            else
            {
                this.currentSprite = standSprite;
            }

            if (right)
            {
                this.pos.X += movementSpeed;
            } 
            else if (left)
            {
                this.pos.X -= movementSpeed;
            }


            if (currentSprite != lastSprite)
            {
                spriteDuration = 0;
            }
            else if (spriteDuration > maxSpriteDuration)
            {
                walkSprite = walkSprite == "walk1" ? "walk2" : "walk1";
                standSprite = standSprite == "idle" ? "stand" : "idle";
            }

            spriteDuration++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                sprite[currentSprite], 
                pos, 
                null, 
                Color.White, 
                0, 
                origins, 
                1, 
                this.left ? SpriteEffects.FlipHorizontally: SpriteEffects.None,
                0
            );
        }
    }
}
