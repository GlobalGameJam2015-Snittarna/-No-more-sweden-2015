using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace no_more_sweden_2015
{
    abstract class GameObject
    {
        public Vector2 Pos { get; set; }
        public Vector2 Velocity 
        { 
            // God is dead
            get { return new Vector2((float)Math.Cos(Globals.DegreesToRadian(Angle)), (float)Math.Sin(Globals.DegreesToRadian(Angle))); } 
        }

        public Texture2D Sprite { get; set; }

        public Rectangle HitBox
        {
            get { return new Rectangle((int)Pos.X, (int)Pos.Y, Sprite.Width, Sprite.Height); }
        }

        public float Depth { get; set; }
        public float Angle { get; set; }
        public float Speed { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }

        public Color Color { get; set; }

        public virtual void Update() { }

        public virtual void DrawSprite(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Sprite, Pos, new Rectangle(0, 0, Sprite.Width, Sprite.Height), Color.White, Rotation, new Vector2(Sprite.Width / 2, Sprite.Height / 2), Scale, SpriteEffects.None, Depth);
        }
    }
}
