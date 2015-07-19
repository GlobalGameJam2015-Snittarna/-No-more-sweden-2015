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
        public static Texture2D genericProjectile, explosion, playerBody, playerWing, playerFlap,
<<<<<<< HEAD
            powerUpBox, powerUps, flames, backgroundObject, rocket, ground, dirt, shield,lazer;
=======
<<<<<<< HEAD
            powerUpBox, powerUps, flames, backgroundObject, rocket, ground, dirt, shield, ammo;
=======
<<<<<<< HEAD
            powerUpBox, powerUps, flames, backgroundObject, rocket, ground, dirt, lazer;
=======
            powerUpBox, powerUps, flames, backgroundObject, rocket, ground, dirt, shield;
>>>>>>> origin/master
>>>>>>> origin/master
>>>>>>> origin/master

        public static SpriteFont font;

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
            backgroundObject = content.Load<Texture2D>("backgroundObjects");
            rocket = content.Load<Texture2D>("rocket");
            lazer = content.Load<Texture2D>("lazer");
            ground = content.Load<Texture2D>("ground");
            dirt = content.Load<Texture2D>("dirt");
            shield = content.Load<Texture2D>("shield");
            ammo = content.Load<Texture2D>("ammo");

            font = content.Load<SpriteFont>("bigFont");
        }
    }
}
