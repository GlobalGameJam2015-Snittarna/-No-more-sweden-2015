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
        public Vector2 Position { get; set; }
        

        public Vector2 Velocity 
        { 
            // God is dead
            get { return new Vector2((float)Math.Cos(Globals.DegreesToRadian(Angle)), (float)Math.Sin(Globals.DegreesToRadian(Angle))); } 
            
        }

        public Rectangle Frame { get; set; }

        public Texture2D Sprite { get; set; }

        public Rectangle HitBox
        {
            get 
            {
                Point tmp = new Point(Frame.Width, Frame.Height);

                if(Frame == new Rectangle(0, 0, 0, 0))
                {
                    tmp = new Point(Sprite.Width, Sprite.Height);
                }

                return new Rectangle((int)Position.X - tmp.X/ 2, (int)Position.Y - tmp.Y / 2, tmp.X * (int)Scale, tmp.Y * (int)Scale); 
            }
        }

        public short MaxAnimationCount { get; set; }
        public short AnimationCount { get; set; }
        public short MaxFrame { get; set; }

        public bool AnimationDone
        {
            get { return Frame.X >= MaxFrame * Frame.Width + MaxFrame - 1; }
        }

        public void Animate()
        {
            AnimationCount += 1;

            if (AnimationDone)
            {
                Frame = new Rectangle(0, Frame.Y, Frame.Width, Frame.Height);
            }

            if (AnimationCount >= MaxAnimationCount)
            {
                Frame = new Rectangle(Frame.X + (Frame.Width + 1), Frame.Y, Frame.Width, Frame.Height);
                AnimationCount = 0;
            }
        }

        public float Depth { get; set; }
        public float Angle { get; set; }
        public float Speed { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }

        public float DistanceTo(Vector2 target)
        {
            return (float)Math.Sqrt((Position.X - target.X) * (Position.X - target.X) + (Position.Y - target.Y) * (Position.Y - target.Y)); 
        }

        public sbyte Health { get; set; }

        public Color Color { get; set; }

        public bool solid;

        public virtual void Update() { }

        public virtual void DrawSprite(SpriteBatch spriteBatch) 
        {
            Rectangle sourceRectangle = Frame;

            if (Frame == new Rectangle(0, 0, 0, 0))
            {
                sourceRectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
            }

            Vector2 orgin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

            spriteBatch.Draw(Sprite, Position, sourceRectangle, Color, Globals.DegreesToRadian(Rotation), orgin, Scale, SpriteEffects.None, Depth);
        }
    }
}
