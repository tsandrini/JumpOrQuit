using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JumpOrQuit.Core
{
    public class RefreshableGameComponent : DrawableGameComponent
    {
        public RefreshableGameComponent(Microsoft.Xna.Framework.Game game) : base(game)
        {
        }

        public virtual void Refresh()
        {

        }
    }
}
