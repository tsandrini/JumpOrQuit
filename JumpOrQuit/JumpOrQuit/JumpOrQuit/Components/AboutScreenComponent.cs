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

using JumpOrQuit.Enums;
using JumpOrQuit.Classes;

using DrawableGameComponent = JumpOrQuit.Core.RefreshableGameComponent;

namespace JumpOrQuit.Components
{
    public class AboutScreenComponent : DrawableGameComponent
    {
        private Game game;
        private GameSettings settings;

        public AboutScreenComponent(Game game, GameSettings settings)
            : base(game)
        {
            this.game = game;
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

        public override void Update(GameTime gameTime)
        {
            if (this.game.KeyPressed(Keys.Escape))
            {
                this.game.SwitchWindows(this.game.menuWindow);
                if (this.settings.soundEnabled) this.settings.sounds["menu.confirm"].Play();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.game.spriteBatch.Begin();

            this.game.spriteBatch.MuchCoolerFont(
                this.settings.fonts["menu.bigger"],
                "O høe",
                new Vector2(this.game.viewport.Width * 0.3f, this.game.viewport.Height * 0.05f),
                Color.LightCyan,
                1f
            );

            this.game.spriteBatch.CoolerFont(
                this.settings.fonts["paragraph"],
                "Vzniklo v rámci kompletace roèníkové práce na GLP.\nAutorem této hry je Tomáš Sandrini.\nVeškerá práva vyhrazena (èi nikoliv?)\nŠiøte mír a open-source! \n\ntsandrini.cz",
                new Vector2(this.game.viewport.Width * 0.2f, this.game.viewport.Height * 0.3f),
                Color.AntiqueWhite
            );

            this.game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
