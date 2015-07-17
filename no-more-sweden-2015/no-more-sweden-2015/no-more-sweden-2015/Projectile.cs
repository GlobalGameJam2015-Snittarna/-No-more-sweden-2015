using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace no_more_sweden_2015
{
    abstract class Projectile : GameObject
    {
        public byte Damege { get; set; }
        public byte ExplosionSize { get; set; }

        public bool explosive;

        public void MoveFoward()
        {
            Position += Velocity * new Vector2(Speed, Speed);
        }

        public void Deaccelerate(bool goReverse, float deaceelSpeed)
        {
            if (goReverse || !goReverse && Speed > 0.1f)
                Speed -= deaceelSpeed;
        }
         
        public void Impact()
        {
            if (explosive)
            {

            }

            GameObjectManager.Remove(this);
        }
    }
}
