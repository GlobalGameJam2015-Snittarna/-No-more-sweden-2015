using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace no_more_sweden_2015
{
    class Explosions : GameObject
    {
        bool dangerous;

        public Explosions(Vector2 position2, float scale2, bool dagnerous2, Color color2, Random random2)
        {
            Position = position2;

            Sprite = AssetManager.explosion;

            Rotation = random2.Next(360);
            MaxAnimationCount = (short)random2.Next(2, 5);
            MaxFrame = 7;

            Frame = new Rectangle(0, 0, 128, 128);

            dangerous = dagnerous2;

            Color = color2;
            Scale = scale2;
        }

        public override void Update()
        {
            Animate();

            if (Frame.X >= MaxFrame * Frame.Width + MaxFrame - 1)
            {
                GameObjectManager.Remove(this);
            }

            base.Update();
        }
    }
}
