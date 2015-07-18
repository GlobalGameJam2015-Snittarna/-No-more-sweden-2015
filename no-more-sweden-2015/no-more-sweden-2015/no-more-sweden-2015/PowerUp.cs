using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace no_more_sweden_2015
{
    class PowerUp : GameObject
    {
        byte type;

        float cosCount;

        public PowerUp(Vector2 position2, byte type2)
        {
            Position = position2;

            type = type2;

            Color = Color.White;
            Scale = 1;
            Sprite = AssetManager.powerUpBox;

            Speed = 3;

            Angle = 90;
        }

        public override void Update()
        {
            Random random = new Random();

            Position += Velocity * Speed;

            Rotation = (float)Math.Cos(cosCount) * 10;

            cosCount += 0.1f;

            if (Position.Y >= 0)
            {
                GameObjectManager.Remove(this);
                GameObjectManager.Add(new Explosion(Position, 1, false, Color.OrangeRed, random));
            }

            foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player && item.solid == true))                               
            {
                if (HitBox.Intersects(p.HitBox))
                {
                    if (type <= 4)
                    {
                        p.GunType = type;
                    }
                    else
                    {
                        if (type == 5)
                        {
                            if (p.Health < 3)
                                p.Health += 1;
                        }

                        if (type == 6)
                        {
                            p.InvicibleCounter = 255;
                        }
                    }

                    GameObjectManager.Remove(this);
                }
            }

            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            base.DrawSprite(spriteBatch);
            spriteBatch.Draw(AssetManager.powerUps, Position + new Vector2(0, 0), new Rectangle(144, 0, 16, 16), Color.White, Globals.DegreesToRadian(Rotation), new Vector2(8, 8), 1, SpriteEffects.None, Depth + 0.001f);
            spriteBatch.Draw(AssetManager.powerUps, Position + new Vector2(0, 0), new Rectangle(16 * type + type, 0, 16, 16), Color.White, Globals.DegreesToRadian(Rotation), new Vector2(8, 8), 1, SpriteEffects.None, Depth + 0.01f);
        }
    }
}
