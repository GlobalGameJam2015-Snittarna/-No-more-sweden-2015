using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public const float G = 0.5f;
    }
}
