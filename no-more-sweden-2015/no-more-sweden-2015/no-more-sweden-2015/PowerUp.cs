﻿using System;
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
            Position += Velocity * Speed;

            foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
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
                }
            }

            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            base.DrawSprite(spriteBatch);
            spriteBatch.Draw(AssetManager.powerUps, Position, new Rectangle(16 * type + type, 0, 16, 16), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, Depth + 0.01f);
        }
    }
}
