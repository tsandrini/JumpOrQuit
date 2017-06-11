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

    public class MenuComponent : DrawableGameComponent
    {
        private Game game;
        private GameSettings settings;
        private MenuItemsComponent menuItems;

        public MenuComponent(Game game, GameSettings settings, MenuItemsComponent menuItems)
            : base(game)
        {
            this.game = game;
            this.menuItems = menuItems;
            this.settings = settings;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if ((!settings.vimMode && this.game.KeyPressed(Keys.Enter)) || (settings.vimMode && this.game.KeyPressed(Keys.Space)))
            {
                this.ItemSubmitted();
            }

            base.Update(gameTime);
        }

        private void ItemSubmitted()
        {
            switch (this.menuItems.selectedItem.identifier)
            {
                case "exit":
                    {
                        this.game.Exit();
                        break;
                    }
                case "new-game":
                    {
                        this.game.gameState = GameState.Playing;
                        this.game.SwitchWindows(this.game.ingameWindow);
                        break;
                    }
                case "settings":
                    {
                        this.game.gameState = GameState.Menu;
                        this.game.SwitchWindows(this.game.settingsScreen);
                        break;
                    }
            }

            if (this.settings.soundEnabled) this.settings.sounds["menu.confirm"].Play();
        }
    }
}