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

        public void CollisionCheck()
        {
            foreach(GameObject g in GameObjectManager.gameObjects.Where(item => item.solid == true))
            {
                if (HitBox.Intersects(g.HitBox))
                {
                    g.Health -= (sbyte)Damege;
                    Impact();
                }
            }
        }

        public void MoveFoward()
        {
            Position += Velocity * Speed;
        }

        public void Deaccelerate(bool goReverse, float deaceelSpeed)
        {
            if (goReverse || !goReverse && Speed > 0.1f)
                Speed -= deaceelSpeed;
        }
        
        public void Impact()
        {
            Random random = new Random();

            if (explosive)
            {
                GameObjectManager.Add(new Explosions(Position, ExplosionSize, false, Color.Orange, random));
            }
            else
            {
                GameObjectManager.Add(new Explosions(Position, Sprite.X, false, Color.Yellow, random));
            }

            GameObjectManager.Remove(this);
        }
    }
}
