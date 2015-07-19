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

        public short lifeTimeCount;

        public int score;

        public bool isLazer = false;
        public int length;
        public Vector2 endPoint = Vector2.Zero;

        public override void Update()
        {
            if (Position.Y >= 0) Impact();
            base.Update();
        }

        public void CollisionCheck()
        {

            if (!isLazer)
            {
                foreach (Player p in GameObjectManager.gameObjects.Where(item => item.solid == true))
                {
                    if (HitBox.Intersects(p.HitBox) && p.playerIndex != PlayerIndex)
                    {
                        p.Health -= (sbyte)Damege;
                        if (p.Health <= 0) score *= 5;
                        foreach (Player p2 in GameObjectManager.gameObjects.Where(item => item is Player))
                        {
                            if (p2.playerIndex == PlayerIndex)
                                p2.Score += score;
                        }
                        Impact();
                    }
                }
            }
            else
            {

                foreach (Lazer l in GameObjectManager.gameObjects.Where(item => item is Lazer))
                {
                    foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
                    {
                        if (p.playerIndex != l.PlayerIndex)
                        {
                            if (Globals.LineToRect(l, p.HitBox))
                            {
                                //Environment.Exit(1);
                                p.Health -= (sbyte)Damege;
                                if (p.Health <= 0) score *= 5;
                                foreach (Player p2 in GameObjectManager.gameObjects.Where(item => item is Player))
                                {
                                    if (p2.playerIndex == PlayerIndex)
                                        p2.Score += score;
                                }
                                Impact();
                            }
                        }
                    }
                }
            }
        }

        public void Expire(short maxLifeTimeCount)
        {
            lifeTimeCount += 1;
            if (lifeTimeCount >= maxLifeTimeCount) GameObjectManager.Remove(this);
        }

        public void MoveFoward()
        {
            Vector2 vel = Velocity * Speed;
            Position += vel;
            score += (int)vel.Length();
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
                GameObjectManager.Add(new Explosion(Position, ExplosionSize, false, Color.Orange, random));
            }
            else
            {
                GameObjectManager.Add(new Explosion(Position, 0.4f, false, Color.MediumVioletRed, random));
            }



            GameObjectManager.Remove(this);
        }
    }
}
