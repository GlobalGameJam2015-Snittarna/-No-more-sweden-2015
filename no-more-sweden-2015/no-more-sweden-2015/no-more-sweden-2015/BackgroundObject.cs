using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace no_more_sweden_2015
{
    class BackgroundObject : GameObject
    {
        public BackgroundObject(Vector2 position2, byte frame, Random random2)
        {
            Position = position2;

            Rotation = random2.Next(360);

            Sprite = AssetManager.backgroundObject;
            Scale = 1;
            Color = Color.White;
            Frame = new Rectangle(frame * 128 + frame, 0, 128, 128);
        }
    }
}
