using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace no_more_sweden_2015
{
    class AssetManager
    {
        public static Texture2D genericProjectile;

        public void Load(ContentManager content)
        {
            genericProjectile = content.Load<Texture2D>("genericProjectile");
        }
    }
}
