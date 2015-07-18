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
        public PlayerIndex playerIndex;
        float turnSpeed;
        public byte GunType { private get; set; }
        public byte InvicibleCounter { private get; set; }
        public int Score { get; private set; }

        public int fireRate = 16;
        public int fireTimer;

        int respawnTimer;

        Vector2 velocity;

        Random rnd = new Random();

        public enum State
        {
            living,
            dying,
            dead
        }

        public State currentState = State.living;

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
            fireTimer = fireRate;
            Color = Color.CadetBlue;
            Health = 15;
            Depth = 1;

            velocity.Y = -8;
        }

        public override void Update()
        {

            switch (currentState)
            {
                case State.living:

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

                    Angle += turnSpeed * TurnDirection();
                    Rotation = Angle;
                    Position += velocity;

                    if (Position.Y > 0)
                    {
                        Health = 0;
                        GameObjectManager.Add(new Explosion(Position, 0.5f + (float)rnd.NextDouble(), false, Color.Red, rnd));
                    }

                    fireTimer++;

                    if (GamePad.GetState(playerIndex).Buttons.A == ButtonState.Pressed && fireTimer >= fireRate)
                    {
                        Shoot();
                        fireTimer = 0;
                    }

                    if (Health <= 0) currentState = State.dying;

                    break;
                case State.dying:

                    velocity.Y += Globals.G;
                    GameObjectManager.Add(new Explosion(Position, 0.5f + (float)rnd.NextDouble(), false, Color.Red, rnd));
                    Position += velocity;

                    Vector2 vel = Vector2.Normalize(velocity);

                    Angle = Globals.RadianToDegree((float)Math.Atan2(vel.Y, vel.X));
                    Rotation = Angle;

                    if (Position.Y >= 0) currentState = State.dead;

                    break;
                case State.dead:
                    velocity = Vector2.Zero;
                    respawnTimer++;

                    if (respawnTimer == 60 * 4)
                    {
                        Position = Game1.camera.Pos - Vector2.UnitY * 500;
                        Health = 15;
                        currentState = State.living;
                        Angle = 90;
                        respawnTimer = 0;
                    }
                    break;
            }
            if (InvicibleCounter > 0)
            {
                InvicibleCounter--;
                solid = false;
            }
            else
                solid = true;

            base.Update();
        }

        void Shoot()
        {
            switch (GunType)
            {
                case 0: // simple shot;
                    fireRate = 16;
                    GameObjectManager.Add(new SimpleBullet(Position + Velocity * 20, 5, Angle, Speed * 1.5f, playerIndex));
                    break;
                case 1: // reverse shot;
                    fireRate = 16;
                    GameObjectManager.Add(new ReverseShot(Position + Velocity * 20, 5, Angle, Speed * 1.5f, playerIndex));
                    break;
                case 2: // flame shot;
                    fireRate = 4;
                    GameObjectManager.Add(new Flame(Position + Velocity * 20, 5, Angle + rnd.Next(-8, 9), Speed * 1.5f, playerIndex));
                    break;
                case 3: // divebomb shot;
                    fireRate = 16;
                    GameObjectManager.Add(new Rocket(Position + Velocity * 20, 5, Angle, Speed * 1.5f, 0.1f, playerIndex));
                    break;
                case 4: // spread shot;
                    fireRate = 32;
                    for (int i = -2; i < 3; i++)
                    {
                        GameObjectManager.Add(new SimpleBullet(Position + Velocity * 20, 5, Angle + 5 * i, Speed * 1.5f, playerIndex));
                    }
                    break;
                case 5: // machine shot;
                    fireRate = 80;
                    GameObjectManager.Add(new SimpleBullet(Position + Velocity * 20, 5, Angle, Speed * 1.5f, playerIndex));
                    break;
            }
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            if (currentState != State.dead)
            {
                spriteBatch.Draw(AssetManager.playerWing, Position + Globals.VectorFromAngle(Angle - 90) * 5, null, Color, Globals.DegreesToRadian(Rotation), new Vector2(AssetManager.playerWing.Width / 2, AssetManager.playerWing.Height), new Vector2(1, (float)Math.Abs(Math.Sin(Globals.DegreesToRadian(Angle)))), SpriteEffects.None, Depth);
                spriteBatch.Draw(AssetManager.playerWing, Position - Globals.VectorFromAngle(Angle - 90) * 5, null, Color, Globals.DegreesToRadian(Rotation), new Vector2(AssetManager.playerWing.Width / 2, 0), new Vector2(1, (float)Math.Abs(Math.Sin(Globals.DegreesToRadian(Angle)))), SpriteEffects.FlipVertically, Depth);

                spriteBatch.Draw(AssetManager.playerFlap, Position - (Velocity * 14) + Globals.VectorFromAngle(Angle - 90) * 2, null, Color, Globals.DegreesToRadian(Rotation), new Vector2(AssetManager.playerFlap.Width / 2, AssetManager.playerFlap.Height), new Vector2(1, (float)Math.Abs(Math.Sin(Globals.DegreesToRadian(Angle)))), SpriteEffects.None, Depth);
                spriteBatch.Draw(AssetManager.playerFlap, Position - (Velocity * 14) - Globals.VectorFromAngle(Angle - 90) * 2, null, Color, Globals.DegreesToRadian(Rotation), new Vector2(AssetManager.playerFlap.Width / 2, 0), new Vector2(1, (float)Math.Abs(Math.Sin(Globals.DegreesToRadian(Angle)))), SpriteEffects.FlipVertically, Depth);

                base.DrawSprite(spriteBatch);
            }
        }

        float TurnDirection()
        {
            return Math.Sign(GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Left.X);
        }
    }
}