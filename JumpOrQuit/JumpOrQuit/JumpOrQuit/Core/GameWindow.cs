using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JumpOrQuit.Core
{
    public class GameWindow
    {
        private List<GameComponent> components;
        private Game game;

        public GameWindow(Game game, params GameComponent[] components)
        {
            this.game = game;
            this.components = new List<GameComponent>();
            foreach (GameComponent component in components)
            {
                AddComponent(component);
            }
        }

        public void AddComponent(GameComponent component)
        {
            components.Add(component);
            if (!game.Components.Contains(component))
            {
                game.Components.Add(component);
            }
        }

        public GameComponent[] ReturnComponents()
        {
            return components.ToArray();
        }

    }
}
