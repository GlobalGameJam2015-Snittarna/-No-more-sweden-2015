using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace no_more_sweden_2015
{
    class ReverseShot : Projectile
    {
        public ReverseShot(Vector2 position2, byte damege2, float angle2, float speed2, PlayerIndex playerIndex2)
        {
            Position = position2;

            Speed = speed2;
            Angle = angle2;

            Damege = damege2;

            Sprite = AssetManager.genericProjectile;
            Color = Color.Red;
            Scale = 1;

            PlayerIndex = playerIndex2;
        }

        public override void Update()
        {
            CollisionCheck();

            Deaccelerate(0.3f, -10);

            Expire(1000);

            MoveFoward();

            base.Update();
        }
    }
}
