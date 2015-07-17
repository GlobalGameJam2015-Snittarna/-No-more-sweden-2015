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
        public byte gunType;

        Vector2 velocity;

        public Player(PlayerIndex newPlayerIndex, Vector2 newPosition)
        {
            playerIndex = newPlayerIndex;
            Angle = -90;
            Speed = 8;
            turnSpeed = 4;
            Scale = 1;
            Color = Color.White;
            Position = newPosition;
            Sprite = AssetManager.playerBody;
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
                turnSpeed = 4;
            }
            else
            {
                velocity.Y += Globals.G;
                turnSpeed = 6;
            }

            Position += velocity;
            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.playerWing, Position + Globals.VectorFromAngle(Angle - 90) * 5, null, Color.White, Globals.DegreesToRadian(Rotation), new Vector2(AssetManager.playerWing.Width / 2, AssetManager.playerWing.Height), new Vector2(1, (float)Math.Abs(Math.Sin(Globals.DegreesToRadian(Angle)))), SpriteEffects.None, 0);
            spriteBatch.Draw(AssetManager.playerWing, Position - Globals.VectorFromAngle(Angle - 90) * 5, null, Color.White, Globals.DegreesToRadian(Rotation), new Vector2(AssetManager.playerWing.Width / 2, 0), new Vector2(1, (float)Math.Abs(Math.Sin(Globals.DegreesToRadian(Angle)))), SpriteEffects.FlipVertically, 0);
            base.DrawSprite(spriteBatch);
        }

        float TurnDirection()
        {
            return Math.Sign(GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Left.X);
        }
    }
}