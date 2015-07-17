using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace no_more_sweden_2015
{
    class Explosions : GameObject
    {
        bool dangerous;

        public Explosions(Vector2 position2, float scale2, bool dagnerous2, Color color2, Random random2)
        {
            Position = position2;

            Scale = scale2;
        }
    }
}
