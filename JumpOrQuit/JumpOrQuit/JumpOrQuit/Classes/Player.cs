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
        public bool falling, jumping, left, right, canJump, crouching;
        public string lastSprite, currentSprite;
        public int spriteDuration, maxSpriteDuration, maxJumpDuration, fallingSpeed, movementSpeed, jumpingSpeed, height, width;

        private string walkSprite, standSprite;

        public Player(Vector2 pos)
        {
            this.pos = this.startingPos = pos;
        }

        public void Reset(Dictionary<string, Texture2D> sprite)
        {
            this.pos = startingPos;
            this.sprite = sprite;
            this.origins = new Vector2(0, sprite["idle"].Height);
            this.width = sprite["idle"].Width;
            this.height = sprite["idle"].Height;

            this.jumping = this.left = this.right = this.falling = this.canJump = this.crouching = false;
            this.fallingSpeed = 10;
            this.movementSpeed = 6;
            this.jumpingSpeed = 10;
            this.maxSpriteDuration = 15;
            this.maxJumpDuration = 27;

            this.walkSprite = "walk1";
            this.standSprite = "idle";

            this.lastSprite = this.currentSprite = "stand";
        }

        public void Update()
        {
            lastSprite = currentSprite;

            if (jumping && canJump && (spriteDuration < maxJumpDuration || currentSprite != "jump"))
            {
                this.currentSprite = "jump";
                this.pos.Y -= jumpingSpeed;
            }
            else if (falling)
            {
                this.currentSprite = "fall";
                this.pos.Y += fallingSpeed;
                this.canJump = false;
            }
            else if (crouching)
            {
                if (left || right)
                {
                    this.currentSprite = "slide";
                }
                else
                {
                    this.currentSprite = "duck";
                }
                this.canJump = true;
            }
            else if (left || right)
            {
                this.currentSprite = walkSprite;
                this.canJump = true;
            }
            else
            {
                this.currentSprite = standSprite;
                this.canJump = true;
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
            else if (spriteDuration > maxSpriteDuration && !jumping && !falling)
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
                0.8f, 
                this.left ? SpriteEffects.FlipHorizontally: SpriteEffects.None,
                0
            );
        }
    }
}
