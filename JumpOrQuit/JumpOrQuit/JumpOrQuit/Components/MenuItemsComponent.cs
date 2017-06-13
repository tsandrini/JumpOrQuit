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
    public class MenuItemsComponent : DrawableGameComponent
    {
        private Game game;
        private GameSettings settings;
        public List<MenuItem> items;
        public MenuItem selectedItem;
        public MouseState mouse;
        private Vector2 pos;
        private Color unselectedColor;
        private Color selectedColor;
        private int height;

        public MenuItemsComponent(Game game, GameSettings settings, Vector2 pos, Color unselectedColor, Color selectedColor, int height)
            : base(game)
        {
            this.game = game;
            this.settings = settings;
            this.pos = pos;
            this.unselectedColor = unselectedColor;
            this.selectedColor = selectedColor;
            this.height = height;
            this.items = new List<MenuItem>();
            this.selectedItem = null;

            this.DrawOrder = (int)DisplayLayer.MenuBack;
        }

        public void AddItem(string text, string identifier)
        {
            Vector2 p = new Vector2(pos.X, pos.Y + items.Count * height);
            MenuItem item = new MenuItem(text, p, identifier);
            this.items.Add(item);

            if (this.selectedItem == null)
            {
                this.selectedItem = item;
            }
        }

        private void SelectNextItem()
        {
            int index = this.items.IndexOf(this.selectedItem);

            if (index < this.items.Count - 1)
            {
                this.selectedItem = this.items[index + 1];
            }
            else
            {
                this.selectedItem = this.items[0];
            }

            if (this.settings.soundEnabled) this.settings.sounds["menu.select"].Play();
        }

        private void SelectPreviousItem()
        {
            int index = this.items.IndexOf(this.selectedItem);

            if (index > 0)
            {
                this.selectedItem = this.items[index - 1];

            }
            else
            {
                this.selectedItem = this.items[this.items.Count - 1];
            }

            if (this.settings.soundEnabled) this.settings.sounds["menu.select"].Play();
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
            if ((!settings.vimMode && game.KeyPressed(Keys.Up)) || (settings.vimMode && game.KeyPressed(Keys.K)))
            {
                this.SelectPreviousItem();
            }

            if ((!settings.vimMode && game.KeyPressed(Keys.Down)) || (settings.vimMode && game.KeyPressed(Keys.J)))
            {
                this.SelectNextItem();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();

            foreach (MenuItem item in this.items)
            {
                Color color = this.unselectedColor;
                if (item == this.selectedItem)
                {
                    color = this.selectedColor;
                }

                game.spriteBatch.MuchCoolerFont(settings.fonts["menu"], item.text, item.pos, color, item.scale);
                item.size = settings.fonts["menu"].MeasureString(item.text);
            }

            game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
