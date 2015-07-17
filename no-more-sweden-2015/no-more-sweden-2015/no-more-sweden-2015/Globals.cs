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

        public const float G = 0;//0.20f;
    }
}
