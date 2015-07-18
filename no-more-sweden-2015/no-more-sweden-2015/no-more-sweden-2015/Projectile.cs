using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace no_more_sweden_2015
{
    abstract class Projectile : GameObject
    {
        public byte Damege { get; set; }
        public byte ExplosionSize { get; set; }

        public bool explosive;

        public PlayerIndex PlayerIndex { get; set; }

        public void CollisionCheck()
        {
            foreach(Player g in GameObjectManager.gameObjects.Where(item => item.solid == true))
            {
                if (HitBox.Intersects(g.HitBox) && g.playerIndex != PlayerIndex)
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

        public void Deaccelerate(float deaceelSpeed, float limit)
        {
            if (Speed > limit)
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
                GameObjectManager.Add(new Explosions(Position, Sprite.Width/24, false, Color.Yellow, random));
            }

            GameObjectManager.Remove(this);
        }
    }
}
