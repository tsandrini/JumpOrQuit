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

        private int dotsCount, ticks;

        public LoadingScreenComponent(Game game, GameSettings settings)
            : base(game)
        {
            this.game = game;
            this.loading = false;
            this.settings = settings;
            this.dotsCount = 3;
            this.ticks = 0;
        }

        public void Load()
        {
            // Load player sprites
            this.settings.addSprite(TextureContent.LoadDictionaryContent<Texture2D>(this.game.Content, @"Graphics\Sprites\Adventurer"));
            this.settings.addSprite(TextureContent.LoadDictionaryContent<Texture2D>(this.game.Content, @"Graphics\Sprites\Female"));
            this.settings.addSprite(TextureContent.LoadDictionaryContent<Texture2D>(this.game.Content, @"Graphics\Sprites\Player"));
            this.settings.addSprite(TextureContent.LoadDictionaryContent<Texture2D>(this.game.Content, @"Graphics\Sprites\Soldier"));
            this.settings.addSprite(TextureContent.LoadDictionaryContent<Texture2D>(this.game.Content, @"Graphics\Sprites\Zombie"));

            this.settings.avaibleRamps = TextureContent.LoadListContent<Texture2D>(this.game.Content, @"Graphics\Ramps");
            this.settings.avaibleBackgrounds = TextureContent.LoadListContent<Texture2D>(this.game.Content, @"Graphics\Backgrounds\ingame");

            // TEXTURES - MISC
            this.settings.textures.Add("hearth", this.game.Content.Load<Texture2D>(@"Graphics\hearth"));
            this.settings.textures.Add("star", this.game.Content.Load<Texture2D>(@"Graphics\star"));
            this.settings.textures.Add("sound.enabled", this.game.Content.Load<Texture2D>(@"Graphics\sound_enabled"));
            this.settings.textures.Add("sound.disabled", this.game.Content.Load<Texture2D>(@"Graphics\sound_disabled"));
            this.settings.textures.Add("vim-mode", this.game.Content.Load<Texture2D>(@"Graphics\vim_mode"));
            this.settings.textures.Add("logo", this.game.Content.Load<Texture2D>(@"Graphics\logo"));

            // SOUNDS
            this.settings.sounds.Add("menu.select", this.game.Content.Load<SoundEffect>(@"SFX\menu\menu_select"));
            this.settings.sounds.Add("menu.confirm", this.game.Content.Load<SoundEffect>(@"SFX\menu\menu_confirm"));

            this.settings.sounds.Add("game.jump.1", this.game.Content.Load<SoundEffect>(@"SFX\ingame\jump1"));
            this.settings.sounds.Add("game.jump.2", this.game.Content.Load<SoundEffect>(@"SFX\ingame\jump2"));
            this.settings.sounds.Add("game.jump.3", this.game.Content.Load<SoundEffect>(@"SFX\ingame\jump3"));
            this.settings.sounds.Add("game.jump.4", this.game.Content.Load<SoundEffect>(@"SFX\ingame\jump4"));

            this.settings.sounds.Add("game.death", this.game.Content.Load<SoundEffect>(@"SFX\ingame\death"));
            this.settings.sounds.Add("game.end", this.game.Content.Load<SoundEffect>(@"SFX\ingame\end"));

            // FONTS
            this.settings.fonts.Add("ingame", this.game.Content.Load<SpriteFont>(@"Fonts\ingameFont"));
            this.settings.fonts.Add("ingame.bigger", this.game.Content.Load<SpriteFont>(@"Fonts\biggerIngameFont"));
            this.settings.fonts.Add("menu.bigger", this.game.Content.Load<SpriteFont>(@"Fonts\biggerMenuFont"));
            this.settings.fonts.Add("paragraph", this.game.Content.Load<SpriteFont>(@"Fonts\paragraphFont"));

            Thread.Sleep(3000); // In case someone has NASA pc :D

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

            if (ticks > 40)
            {
                dotsCount = dotsCount != 4 ? dotsCount + 1 : 1;
                ticks = 0;
            }

            ticks++;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.game.spriteBatch.Begin();

            this.game.spriteBatch.MuchCoolerFont(
                this.settings.fonts["menu"],
                "Hra se naèítá" + new string('.', dotsCount),
                new Vector2(this.game.viewport.Width * 0.28f, this.game.viewport.Height * 0.45f),
                Color.DarkCyan, 2);

            this.game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}