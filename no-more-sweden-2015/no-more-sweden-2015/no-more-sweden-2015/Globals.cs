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

        public const float G = 0.16f;

        public static Color[] playersColors;

        public void AddScore(PlayerIndex player, int score)
        {
            foreach (Player p in GameObjectManager.gameObjects.Where(O => O is Player))
                p.Score += score;   
            
        }

        public bool aabbContainsSegment(float x1, float y1, float x2, float y2, float minX, float minY, float maxX, float maxY)
        {
            // Completely outside.
            if ((x1 <= minX && x2 <= minX) || (y1 <= minY && y2 <= minY) || (x1 >= maxX && x2 >= maxX) || (y1 >= maxY && y2 >= maxY))
                return false;

            float m = (y2 - y1) / (x2 - x1);

            float y = m * (minX - x1) + y1;
            if (y > minY && y < maxY) return true;

            y = m * (maxX - x1) + y1;
            if (y > minY && y < maxY) return true;

            float x = (minY - y1) / m + x1;
            if (x > minX && x < maxX) return true;

            x = (maxY - y1) / m + x1;
            if (x > minX && x < maxX) return true;

            return false;
        }
    }
}
