using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace no_more_sweden_2015
{
    class Rocket : Projectile
    {
        float accel;

        public Rocket(Vector2 position2, byte damege2, float angle2, float speed2, float accel2, PlayerIndex playerIndex2)
        {
            Position = position2;

            Speed = speed2;
            Angle = angle2;

            Damege = damege2;

            accel = accel2;

            Sprite = AssetManager.rocket;
            Color = Color.Green;
            Scale = 0.5f;

            PlayerIndex = playerIndex2;
        }

        public override void Update()
        {
            Rotation = Angle;

            Angle = Globals.Lerp(Angle, -270, 0.1f);

            MoveFoward();
            CollisionCheck();

            if (Speed <= 20)
            {
                Deaccelerate(-accel, -1);
            }

            base.Update();
        }
    }
}

