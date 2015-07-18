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
<<<<<<< HEAD
        public static Texture2D genericProjectile, explosion, playerBody, playerWing, playerFlap, powerUpBox, powerUps;
=======
        public static Texture2D genericProjectile, explosion, playerBody, playerWing, powerUpBox, powerUps, flames;
>>>>>>> origin/master

        public static void Load(ContentManager content)
        {
            genericProjectile = content.Load<Texture2D>("genericProjectile");
            explosion = content.Load<Texture2D>("explosion");
            playerBody = content.Load<Texture2D>("playerBody");
            playerWing = content.Load<Texture2D>("playerWing");
            playerFlap = content.Load<Texture2D>("playerFlap");

            powerUpBox = content.Load<Texture2D>("powerUpBox");
            powerUps = content.Load<Texture2D>("powerUps");
            flames = content.Load<Texture2D>("flames");
        }
    }
}
