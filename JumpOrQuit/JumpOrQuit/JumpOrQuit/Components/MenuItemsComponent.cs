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


namespace JumpOrQuit.Components 
{
    public class MenuItemsComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game game;
        public List<MenuItem> items;
        public MenuItem selectedItem;
        public MouseState mouse;
        private Vector2 pos;
        private Color unselectedColor;
        private Color selectedColor;
        protected SoundEffect menuSelect;
        private int height;

        public MenuItemsComponent(Game game, Vector2 pos, Color unselectedColor, Color selectedColor, int height)
            : base(game)
        {
            this.game = game;
            this.pos = pos;
            this.unselectedColor = unselectedColor;
            this.selectedColor = selectedColor;
            this.height = height;
            this.items = new List<MenuItem>();
            this.selectedItem = null;
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

            this.menuSelect.Play();
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

            this.menuSelect.Play();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.menuSelect = game.Content.Load<SoundEffect>(@"SFX\menu_select");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // KEYBOARD INPUT
            if (game.NewKeyboardKey(Keys.Up))
            {
                this.SelectPreviousItem();
            }

            if (game.NewKeyboardKey(Keys.Down))
            {
                this.SelectNextItem();
            }

            // MOUSE MOVEMENT
            if (Math.Abs(game.getMousePosChange().Y) > 0)
            {
                foreach (MenuItem item in this.items)
                {

                    Rectangle bounds = new Rectangle((int)item.pos.X, (int)item.pos.Y, (int)item.size.X, (int)item.size.Y);

                    if (bounds.Contains(game.mouse.X, game.mouse.Y) && this.selectedItem != item)
                    {
                        this.selectedItem = item;
                        this.menuSelect.Play();
                        break;
                    }
                }
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

                game.spriteBatch.MuchCoolerFont(game.fontDistInking, item.text, item.pos, color, item.scale);
                item.size = game.fontDistInking.MeasureString(item.text);
            }

            game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
