﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace no_more_sweden_2015
{
    class Globals
    {
        public static float DegreesToRadian(float degree)
        {
            return degree * (float)Math.PI / 180;
        }

        public static float RadianToDegree(float radian)
        {
            return radian * 180 / (float)Math.PI;
        }

        public static Vector2 VectorFromAngle(float angle)
        {
            return new Vector2((float)Math.Cos(Globals.DegreesToRadian(angle)), (float)Math.Sin(Globals.DegreesToRadian(angle)));
        }

        public static float Lerp(float s, float e, float t)
        {
            return s + t * (e - s);
        }

        public static int numberOfPlayers = 0;

        public const float G = .16f;

        public static Color[] playersColors;

        public void AddScore(PlayerIndex player, int score)
        {
            foreach (Player p in GameObjectManager.gameObjects.Where(O => O is Player))
                p.Score += score;
        }

        public static bool LineToRect(Lazer lazer, Rectangle box)
        {
            Vector2 rectDelta = new Vector2(box.Center.X, box.Center.Y) - lazer.Position;
            float boxAngle = (float)Math.Atan2(rectDelta.Y, rectDelta.X);

            float angle = lazer.Angle;

            if (angle < -180) angle += 360;
            if (angle > 180) angle -= 360;

            if (boxAngle > angle - 45 && boxAngle < angle + 45)
            {
                for (int i = 0; i < lazer.length; i++)
                {
                    if (i > rectDelta.Length() + 20) return false;

                    Vector2 walkersPos = lazer.Position + Globals.VectorFromAngle(lazer.Angle) * i;

                    if (box.Intersects(new Rectangle((int)(walkersPos.X - lazer.width / 2), (int)(walkersPos.Y - lazer.width / 2), (int)lazer.width, (int)lazer.width))) return true;
                }
            }
            else
                return false;


            return false;
        }

    }
}
