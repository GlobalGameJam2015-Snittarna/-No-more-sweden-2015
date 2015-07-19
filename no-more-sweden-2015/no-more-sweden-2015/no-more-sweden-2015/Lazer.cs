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
    class Lazer : Projectile
    {
        public float width;
        Vector2 delta;

        public Lazer(Vector2 newPosition, float newAngle, byte newDamage, PlayerIndex newPlayerIndex)
        {
            Position = newPosition;
            Angle = newAngle;
            Damege = newDamage;
            PlayerIndex = newPlayerIndex;

            length = 10000;

            endPoint = Position + Globals.VectorFromAngle(Angle) * length;

            delta = endPoint - Position;

            width = 8;

            Sprite = AssetManager.lazer;

            isLazer = true;
            explosive = true;
        }

        public override void Update()
        {
            width -= 0.2f;

            if (width == 0) GameObjectManager.Remove(this);

            CollisionCheck();

            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Sprite, new Rectangle((int)Position.X, (int)Position.Y, length, (int)width), null, Color.White, Globals.DegreesToRadian(Angle), new Vector2(0, width / 2), SpriteEffects.None, 1);

            //base.DrawSprite(spriteBatch);
        }
    }
}
