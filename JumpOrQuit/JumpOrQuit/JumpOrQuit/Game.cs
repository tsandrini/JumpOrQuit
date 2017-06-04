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
using JumpOrQuit.Helpers;
using JumpOrQuit.Enums; 

// Aliases
using GameWindow = JumpOrQuit.Core.GameWindow; // Override the default game window
using DrawableGameComponent = JumpOrQuit.Core.RefreshableGameComponent;

namespace JumpOrQuit
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        public CoolFont spriteBatch;
        public GameState gameState;

        public KeyboardState keys, lastKey;
        public MouseState mouse, lastMouse;
        public SpriteFont menuFont;
        public GameWindow loadingScreen, menuWindow, ingameWindow;
        public GameSettings settings;

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
            this.settings = new GameSettings();

            GameSettingsComponent settingsComponent = new GameSettingsComponent(this, settings, graphics);
            this.Components.Add(settingsComponent);

            LoadingScreenComponent loading = new LoadingScreenComponent(this, settings);
            this.Components.Add(loading);

            MenuItemsComponent menuItems = new MenuItemsComponent(
                this,
                settings,
                new Vector2(this.viewport.Width * 0.45f, this.viewport.Height * 0.75f),
                Color.Blue,
                Color.Yellow,
                72
            );

            menuItems.AddItem("Hrát", "new-game");
            menuItems.AddItem("Nastavení", "settings");
            menuItems.AddItem("Odejít", "exit");

            MenuComponent menu = new MenuComponent(this,settings, menuItems);
            this.Components.Add(menu);

            LevelComponent level = new LevelComponent(this, settings);
            this.Components.Add(level);

            this.loadingScreen = new GameWindow(this, loading);
            this.menuWindow = new GameWindow(this, menu, menuItems, settingsComponent);
            this.ingameWindow = new GameWindow(this, level, settingsComponent);

            foreach (GameComponent component in this.Components)
            {
                this.SwitchComponent(component, false);
            }

            // BASE GRAPHICS STUFF
            this.IsMouseVisible = false;
            //this.graphics.IsFullScreen = true;
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.ApplyChanges();

            this.SwitchWindows(loadingScreen);

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

        public bool KeyDown(Keys key)
        {
            return keys.IsKeyDown(key);
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
                if (state == true)
                {
                    ((DrawableGameComponent)component).Refresh();
                }
            }
        }
    }
}
