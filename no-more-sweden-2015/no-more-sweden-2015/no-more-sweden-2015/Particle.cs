using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace no_more_sweden_2015
{
    class Particle : GameObject
    {
        short lifeTime;
        short maxLifeTime;

        float velY;

        public Particle(Vector2 position2, Texture2D sprite2, float angle2, float speed2, float scale2, Color color2, short maxLifeTime2)
        {
            Position = position2;

            Angle = angle2;
            Speed = speed2;

            maxLifeTime = maxLifeTime2;

            Color = color2;
            Scale = scale2;
            Sprite = sprite2;

            Depth = 1;
        }

        public override void Update()
        {
            if (Position.Y >= 0)
            {
                GameObjectManager.Add(new Explosion(Position, 0.4f, false, Color.MediumVioletRed, rnd));
                GameObjectManager.Remove(this);
            }

            Position += Velocity * Speed;

            Position += new Vector2(0, velY);

            velY += Globals.G;

            lifeTime += 1;

            Rotation += Speed / 5;

            if (lifeTime >= maxLifeTime)
            {
                GameObjectManager.Remove(this);
            }

            base.Update();
        }
    }
}
