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
using System.Threading;

using JumpOrQuit.Enums;
using JumpOrQuit.Helpers;
using JumpOrQuit.Classes;

using DrawableGameComponent = JumpOrQuit.Core.RefreshableGameComponent;


namespace JumpOrQuit.Components
{

    public class LoadingScreenComponent : DrawableGameComponent
    {
        public bool loading;
        private Game game;
        private GameSettings settings;
        private Thread thread;

        public LoadingScreenComponent(Game game, GameSettings settings)
            : base(game)
        {
            this.game = game;
            this.loading = false;
            this.settings = settings;
        }

        public void Load()
        {
            // Load player sprites
            this.settings.addSprite(TextureContent.LoadListContent<Texture2D>(this.game.Content, @"Graphics\Sprites\Adventurer"));
            this.settings.addSprite(TextureContent.LoadListContent<Texture2D>(this.game.Content, @"Graphics\Sprites\Female"));
            this.settings.addSprite(TextureContent.LoadListContent<Texture2D>(this.game.Content, @"Graphics\Sprites\Player"));
            this.settings.addSprite(TextureContent.LoadListContent<Texture2D>(this.game.Content, @"Graphics\Sprites\Soldier"));
            this.settings.addSprite(TextureContent.LoadListContent<Texture2D>(this.game.Content, @"Graphics\Sprites\Zombie"));

            this.settings.avaibleRamps.Add(this.game.Content.Load<Texture2D>(@"Graphics\ramps\1"));

            this.settings.sounds.Add("menu.select", this.game.Content.Load<SoundEffect>(@"SFX\menu\menu_select"));
            this.settings.sounds.Add("menu.confirm", this.game.Content.Load<SoundEffect>(@"SFX\menu\menu_confirm"));

            this.settings.sounds.Add("game.jump.1", this.game.Content.Load<SoundEffect>(@"SFX\ingame\jump1"));
            this.settings.sounds.Add("game.jump.2", this.game.Content.Load<SoundEffect>(@"SFX\ingame\jump2"));
            this.settings.sounds.Add("game.jump.3", this.game.Content.Load<SoundEffect>(@"SFX\ingame\jump3"));
            this.settings.sounds.Add("game.jump.4", this.game.Content.Load<SoundEffect>(@"SFX\ingame\jump4"));

            Thread.Sleep(1500); // In case someone has NASA pc :D

            this.game.gameState = GameState.Menu;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.game.gameState == GameState.Loading && !loading)
            {
                this.thread = new Thread(new ThreadStart(this.Load));
                this.loading = true;
                this.thread.Start();
            }
            else if (this.game.gameState != GameState.Loading)
            {
                this.game.SwitchWindows(this.game.menuWindow);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.game.spriteBatch.Begin();

            this.game.spriteBatch.MuchCoolerFont(this.game.menuFont, "Naèítá se hra ...", new Vector2(300, 300), Color.CadetBlue, 2);

            this.game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}