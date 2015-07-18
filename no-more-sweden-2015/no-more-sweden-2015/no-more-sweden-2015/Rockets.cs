using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace no_more_sweden_2015
{
    class Rocket : Projectile
    {
        PlayerIndex targetIndex;

        float accel;
        float wantedAngle;

        short followTime;
        short followTimeCount;

        public Rocket(Vector2 position2, byte damege2, float angle2, float speed2, float accel2, PlayerIndex playerIndex2)
        {
            Position = position2;

            Speed = speed2;
            Angle = angle2;

            if (Angle > 180) Angle -= 360;
            if (Angle < -180) Angle += 360;

            Damege = damege2;

            accel = accel2;

            Sprite = AssetManager.rocket;
            Color = Color.Green;
            Scale = 0.5f;

            followTime = 64;

            PlayerIndex = playerIndex2;
        }

        public override void Update()
        {
            Rotation = Angle;

            MoveFoward();
            CollisionCheck();

           // Deaccelerate(accel, 1.2f);

            if (followTime < followTimeCount)
            {
                Expire(1000);
            }

            if (followTime >= followTimeCount)
            {
                followTimeCount += 1;

                foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
                {
                    if (p.playerIndex != PlayerIndex)
                    {
                        if (p.HitBox.Intersects(HitBox))
                        {
                            Impact();
                            p.Health -= (sbyte)Damege;
                        }

                        if (p.playerIndex == targetIndex)
                        {
                            wantedAngle = Globals.RadianToDegree((float)Math.Atan2(p.Position.Y - Position.Y, p.Position.X - Position.X));

                            if (wantedAngle < Angle) Angle -= 5f;
                            if (wantedAngle > Angle) Angle += 5f;
                        }

                        if (Globals.numberOfPlayers >= 3)
                        {
                            foreach (Player p2 in GameObjectManager.gameObjects.Where(item2 => item2 is Player))
                            {
                                if (p != p2 && p2.playerIndex != PlayerIndex)
                                {
                                    if (DistanceTo(p2.Position) <= DistanceTo(p.Position))
                                    {
                                        targetIndex = p.playerIndex;
                                    }
                                }
                            }
                        }
                        else
                        {
                            targetIndex = p.playerIndex;
                        }
                    }
                }
                
            }
            base.Update();
        }
    }
}

