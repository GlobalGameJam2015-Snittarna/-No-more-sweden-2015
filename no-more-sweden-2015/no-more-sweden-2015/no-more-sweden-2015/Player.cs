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
    class Player : GameObject
    {
        PlayerIndex playerIndex;
        float turnSpeed;
        float maxSpeed;
        float acceleration;

        Vector2 velocity;

        public Player(PlayerIndex newPlayerIndex)
        {
            playerIndex = newPlayerIndex;
            Angle = -90;
            Speed = 8;
            turnSpeed = 4;
            Scale = 1;
            Color = Color.White;
            Position = new Vector2(100, 100);
            Sprite = AssetManager.genericProjectile;
            acceleration = 0.25f;
            maxSpeed = 4;
        }

        public override void Update()
        {
            Angle += turnSpeed * TurnDirection();
            Console.WriteLine(TurnDirection());
            Rotation = Angle;

            if (GamePad.GetState(playerIndex).Triggers.Right > 0.5)
            {
                velocity.X = MathHelper.Lerp(velocity.X, Velocity.X * Speed, 0.1f);
                velocity.Y = MathHelper.Lerp(velocity.Y, Velocity.Y * Speed, 0.1f);
            }
            else
            {
                velocity.Y += Globals.G;
            }

            Position += velocity;
            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {

            base.DrawSprite(spriteBatch);
        }

        float TurnDirection()
        {
            return Math.Sign((GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Left).X);
        }
    }
}