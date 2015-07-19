using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace no_more_sweden_2015
{
    class Explosion : GameObject
    {
        bool dangerous;

        public Explosion(Vector2 position2, float scale2, bool dagnerous2, Color color2, Random random2)
        {
            Position = position2;

            Sprite = AssetManager.explosion;

            Rotation = random2.Next(360);
            MaxAnimationCount = (short)random2.Next(2, 5);
            MaxFrame = 7;

            Frame = new Rectangle(0, 0, 128, 128);

            dangerous = dagnerous2;

            Depth = 0.1f;

            AssetManager.explosionSfx.Play(0.2f, 0, 0);

            Color = color2;
            Scale = scale2;
        }

        public override void Update()
        {
            Animate();

            if (AnimationDone)
            {
                GameObjectManager.Remove(this);
            }

            base.Update();
        }
    }
}
