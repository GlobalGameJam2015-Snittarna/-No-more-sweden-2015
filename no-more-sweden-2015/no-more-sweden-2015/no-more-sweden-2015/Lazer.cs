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

            width = 6;

            Sprite = AssetManager.lazer;

            isLazer = true;

            score = 200;
            explosive = true;
        }

        public override void Update()
        {
            width = MathHelper.Lerp(width, 0 , 0.11f);

            if (width < 1f) GameObjectManager.Remove(this);

            CollisionCheck();

            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Sprite, Position, null, Color.White, Globals.DegreesToRadian(Angle), new Vector2(0, Sprite.Height/2), new Vector2(length, width), SpriteEffects.None, 1);

            //base.DrawSprite(spriteBatch);
        }
    }
}
