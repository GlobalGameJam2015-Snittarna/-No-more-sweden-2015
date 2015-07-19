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
        public byte GunType { get; set; }
        public byte InvicibleCounter { private get; set; }
        public int Score { get; set; }

        public int fireRate = 16;
        public int fireTimer;

        public int currentMaxAmmo;
        public int currentAmmo;

        public byte healedCount;

        int cR;
        int cG;
        int cB;

        int respawnTimer;

        bool hasEjectedWings;

        Vector2 velocity;

        Random rnd = new Random();

        Color currentColor;

        public enum State
        {
            living,
            dying,
            dead
        }

        public State currentState = State.living;

        public Player(PlayerIndex newPlayerIndex, Vector2 newPosition, Random rnd)
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
            Health = 30;
            Depth = 0.98f;

            currentColor = new Color(rnd.Next(100, 255), rnd.Next(100, 255), rnd.Next(100, 255));
            Color = currentColor;

            cR = (255 - currentColor.R) / 30;
            cG = currentColor.G / 30;
            cB = currentColor.B / 30;

            for (int i = 0; i < Globals.playersColors.Count(); i++)
            {
                Console.WriteLine(Globals.playersColors[i]);
                if (Globals.playersColors[i] == new Color(0, 0, 0, 0))
                {
                    Globals.playersColors[i] = Color;
                    break;
                }
            }

            GunType = 0;
            velocity.Y = -8;
        }

        public override void Update()
        {
            switch (currentState)
            {
                case State.living:

                    if (healedCount >= 1)
                    {
                        healedCount += 1;
                        if (healedCount >= 16) healedCount = 0;
                    }

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
                        AssetManager.explosionSfx.Play(0.2f, 0, 0);
                    }

                    fireTimer++;

                    if (GamePad.GetState(playerIndex).Buttons.A == ButtonState.Pressed && fireTimer >= fireRate && Game1.delay <= 0)
                    {
                        Shoot();
                        fireTimer = 0;
                    }

                    //if (GamePad.GetState(playerIndex).Buttons.B == ButtonState.Pressed) currentState = State.dying;

                    Color = new Color(currentColor.R + cR * (30 - Health), cG * Health, cB * Health);


                    if (Health <= 0)
                    {
                        currentState = State.dying;
                        if (!hasEjectedWings)
                        {
                            AssetManager.explosionSfx.Play(0.2f, 0, 0);
                            GameObjectManager.Add(new Particle(Position, AssetManager.playerWing, Angle+90, rnd.Next(8, 11), 1, Color, 2000));
                            GameObjectManager.Add(new Particle(Position, AssetManager.playerWing, Angle-90, rnd.Next(8, 11), 1, Color, 2000));
                            hasEjectedWings = true;
                        }
                    }

                    break;
                case State.dying:

                    velocity.Y += Globals.G;
                    GameObjectManager.Add(new Explosion(Position, 0.5f + ((float)rnd.NextDouble() * 0.2f), false, Color.Red, rnd));
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
                        Health = 30;
                        currentState = State.living;
                        Angle = 90;
                        respawnTimer = 0;
                        GunType = 0;
                        currentAmmo = 0;
                        Color = currentColor;
                        hasEjectedWings = false;
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

            GunType = 3;
            base.Update();
        }

        void Shoot()
        {
            currentAmmo++;

            if (GunType != 3 && GunType != 2) AssetManager.shoot.Play();

            if (GunType == 2) AssetManager.flameSfx.Play(0.3f, 0, 0);
            if (GunType == 3) AssetManager.lazerSfx.Play();

            switch (GunType)
            {
                case 0: // simple shot;
                    fireRate = 16;
                    GameObjectManager.Add(new SimpleBullet(Position + Velocity * 20, 10, Angle, Speed * 1.5f, playerIndex));
                    break;
                case 1: // reverse shot;
                    currentMaxAmmo = 50;
                    fireRate = 16;
                    GameObjectManager.Add(new ReverseShot(Position + Velocity * 20, 10, Angle, Speed * 2, playerIndex));
                    break;
                case 2: // flame shot;
                    fireRate = 1;
                    currentMaxAmmo = 200;
                    GameObjectManager.Add(new Flame(Position + Velocity * 20, 1, Angle + rnd.Next(-8, 9), Speed * 1.5f, playerIndex));
                    break;
                case 3: // LAZER shot;
                    fireRate = 32;
                    currentMaxAmmo = 1;
                    GameObjectManager.Add(new Lazer(Position + Velocity * 20, Angle, 100, playerIndex));
                    break;
                case 4: // spread shot;
                    fireRate = 32;
                    currentMaxAmmo = 20;
                    for (int i = -2; i < 3; i++)
                        GameObjectManager.Add(new SimpleBullet(Position + Velocity * 20, 10, Angle + 5 * i, Speed * 1.5f, playerIndex));
                    break;
                case 5: // machine shot;
                    fireRate = 4;
                    currentMaxAmmo = 400;
                    GameObjectManager.Add(new SimpleBullet(Position + Velocity * 20, 2, Angle + rnd.Next(-2, 3), Speed * 1.5f, playerIndex));
                    break;
            }

            if (currentAmmo == currentMaxAmmo)
            {
                GunType = 0;
            }
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            if (currentState != State.dead)
            {
                if (currentState != State.dying)
                {
                    spriteBatch.Draw(AssetManager.playerWing, Position + Globals.VectorFromAngle(Angle - 90) * 5, null, Color, Globals.DegreesToRadian(Rotation), new Vector2(AssetManager.playerWing.Width / 2, AssetManager.playerWing.Height), new Vector2(1, (float)Math.Abs(Math.Sin(Globals.DegreesToRadian(Angle)))), SpriteEffects.None, Depth);
                    spriteBatch.Draw(AssetManager.playerWing, Position - Globals.VectorFromAngle(Angle - 90) * 5, null, Color, Globals.DegreesToRadian(Rotation), new Vector2(AssetManager.playerWing.Width / 2, 0), new Vector2(1, (float)Math.Abs(Math.Sin(Globals.DegreesToRadian(Angle)))), SpriteEffects.FlipVertically, Depth);
                }
                spriteBatch.Draw(AssetManager.playerFlap, Position - (Velocity * 14) + Globals.VectorFromAngle(Angle - 90) * 2, null, Color, Globals.DegreesToRadian(Rotation), new Vector2(AssetManager.playerFlap.Width / 2, AssetManager.playerFlap.Height), new Vector2(1, (float)Math.Abs(Math.Sin(Globals.DegreesToRadian(Angle)))), SpriteEffects.None, Depth);
                spriteBatch.Draw(AssetManager.playerFlap, Position - (Velocity * 14) - Globals.VectorFromAngle(Angle - 90) * 2, null, Color, Globals.DegreesToRadian(Rotation), new Vector2(AssetManager.playerFlap.Width / 2, 0), new Vector2(1, (float)Math.Abs(Math.Sin(Globals.DegreesToRadian(Angle)))), SpriteEffects.FlipVertically, Depth);

                if (!solid)
                {
                    spriteBatch.Draw(AssetManager.shield, Position, new Rectangle(0, 0, 48, 48), Color.White, 0, new Vector2(24, 24), 1, SpriteEffects.None, Depth + 0.01f);
                }

                if(healedCount >= 1)
                spriteBatch.Draw(AssetManager.healed, Position, new Rectangle(0, 0, 32, 32), Color.White, 0, new Vector2(16, 16), 0.7f, SpriteEffects.None, Depth + 0.01f);

                base.DrawSprite(spriteBatch);
            }
        }

        float TurnDirection()
        {
            return Math.Sign(GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Left.X);
        }
    }
}