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

        public Player(PlayerIndex newPlayerIndex)
        {
            playerIndex = newPlayerIndex;
            Angle = -90;
            Speed = 1;
        }

        public override void Update()
        {
            Angle += turnSpeed * TurnDirection();

            Position += Velocity;
            base.Update();
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            
            base.DrawSprite(spriteBatch);
        }

        sbyte TurnDirection()
        {
            return (sbyte)Vector2.Normalize(GamePad.GetState(playerIndex).ThumbSticks.Left).X;
        }
    }
}