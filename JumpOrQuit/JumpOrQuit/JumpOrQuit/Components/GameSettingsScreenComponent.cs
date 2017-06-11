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
    public class GameSettingsScreenComponent : DrawableGameComponent
    {
        private Game game;
        private GameSettings settings;
        private MenuItemsComponent menuItems;

        public GameSettingsScreenComponent(Game game, GameSettings settings, MenuItemsComponent menuItems)
            : base(game)
        {
            this.game = game;
            this.settings = settings;
            this.menuItems = menuItems;
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
            if ((!settings.vimMode && this.game.KeyPressed(Keys.Enter) || (settings.vimMode && game.KeyPressed(Keys.Space))))
            {
                switch (this.menuItems.selectedItem.identifier)
                {
                    case "player-sprite":
                        {
                            if (this.game.settings.activeSprite == this.game.settings.avaibleSprites.Last())
                            {
                                this.game.settings.activeSpriteKey = 0;
                            }
                            else
                            {
                                this.game.settings.activeSpriteKey++;
                            }
                            break;
                        }
                    case "music-enabled":
                        {
                            this.game.settings.soundEnabled = !this.game.settings.soundEnabled;
                            break;
                        }
                    case "back":
                        {
                            this.game.gameState = GameState.Menu;
                            this.game.SwitchWindows(this.game.menuWindow);
                            break;
                        }
                }

                if (this.settings.soundEnabled) this.settings.sounds["menu.confirm"].Play();
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.game.spriteBatch.Begin();

            this.game.spriteBatch.Draw(
                this.game.settings.activeSprite["idle"],
                new Rectangle(500, 500, 100, 100),
                Color.White
            );

            this.game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
