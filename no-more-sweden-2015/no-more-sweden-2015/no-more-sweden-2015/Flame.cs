using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace no_more_sweden_2015
{
    class Flame : Projectile
    {
        public Flame(Vector2 position2, byte damege2, float angle2, float speed2, PlayerIndex playerIndex2)
        {
            Random random = new Random();

            Position = position2;
            
            Speed = speed2;
            Angle = angle2;

            Damege = damege2;

            MaxAnimationCount = (short)random.Next(2, 4);
            MaxFrame = 11;

            Sprite = AssetManager.flames;
            Frame = new Rectangle(0, 0, 32, 32);
            Color = Color.White;
            Scale = 1;

            PlayerIndex = playerIndex2;
        }

        public override void Update()
        {
            MoveFoward();
            Deaccelerate(0.1f, -0.1f);

            Animate();

            if (AnimationDone) GameObjectManager.Remove(this); 

            base.Update();
        }
    }
}
