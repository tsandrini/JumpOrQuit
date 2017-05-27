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
using JumpOrQuit.Components;

// Aliases
using GameWindow = JumpOrQuit.Classes.GameWindow; // Override the default game window

namespace JumpOrQuit
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public CoolFont spriteBatch;

        public KeyboardState keys, lastKey;
        public MouseState mouse, lastMouse;
        public SpriteFont menuFont;
        protected GameWindow loadingScreen, menuWindow, ingameWindow;

        public Viewport viewport
        {
            get
            {
                return this.graphics.GraphicsDevice.Viewport;
            }
        }

        public Vector2 mousePosChange
        {
            get
            {
                return new Vector2(mouse.X - lastMouse.X, mouse.Y - lastMouse.Y);
            }
        }

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            MenuItemsComponent menuItems = new MenuItemsComponent(
                this,
                new Vector2(this.viewport.Width * 0.45f, this.viewport.Height * 0.75f),
                Color.Blue,
                Color.Yellow,
                72
            );

            menuItems.AddItem("Hrát", "new-game");
            menuItems.AddItem("Nastavení", "settings");
            menuItems.AddItem("Odejít", "exit");

            MenuComponent menu = new MenuComponent(this, menuItems);
            this.Components.Add(menu);

            this.menuWindow = new GameWindow(this, menu, menuItems);

            foreach (GameComponent component in this.Components)
            {
                this.SwitchComponent(component, false);
            }

            // BASE GRAPHICS STUFF
            this.IsMouseVisible = false;
            //this.graphics.IsFullScreen = true;
            this.graphics.PreferredBackBufferHeight = 1080;
            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.ApplyChanges();


            this.SwitchWindows(menuWindow);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new CoolFont(GraphicsDevice);
            this.menuFont = Content.Load<SpriteFont>(@"Fonts\menuFont");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            this.lastMouse = mouse;
            this.lastKey = keys;
            this.keys = Keyboard.GetState();
            this.mouse = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        public bool IsMouseClicked()
        {
            return lastMouse.LeftButton == ButtonState.Released && mouse.LeftButton == ButtonState.Pressed;
        }

        public bool KeyPressed(Keys key)
        {
            return keys.IsKeyDown(key) && lastKey.IsKeyUp(key);
        }

        public void SwitchWindows(GameWindow gameWindow)
        {
            GameComponent[] granted = gameWindow.ReturnComponents();
            foreach (GameComponent component in Components)
            {
                bool enabled = granted.Contains(component);
                this.SwitchComponent(component, enabled);
            }

            this.lastKey = keys;
        }

        private void SwitchComponent(GameComponent component, bool state)
        {
            component.Enabled = state;
            if (component is DrawableGameComponent)
            {
                ((DrawableGameComponent)component).Visible = state;
            }
        }
    }
}
