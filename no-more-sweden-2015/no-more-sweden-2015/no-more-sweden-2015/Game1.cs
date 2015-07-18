using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace no_more_sweden_2015
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera camera = new Camera();

        PlayerIndex[] indexes = new PlayerIndex[]{
            PlayerIndex.One,
            PlayerIndex.Two,
            PlayerIndex.Three,
            PlayerIndex.Four
        };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            Content.RootDirectory = "Content";
        }

        

        protected override void Initialize()
        {
            AssetManager.Load(Content);

            int numberOfPlayers = 0;

            foreach (PlayerIndex playerIndex in Enum.GetValues(typeof(PlayerIndex)))
                if (GamePad.GetState(playerIndex).IsConnected) numberOfPlayers++;

            int slice = 640 / (numberOfPlayers + 1);

            for (int i = 1; i <= numberOfPlayers; i++)
            {
                GameObjectManager.Add(new Player(indexes[i - 1], new Vector2(slice * i, graphics.PreferredBackBufferHeight - 100)));
            }

            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                GameObjectManager.Add(new BackgroundObject(new Vector2(random.Next(-3200, 3200), random.Next(-1000, -100)), (byte)random.Next(3), random));
            }

            GameObjectManager.Add(new PowerUp(new Vector2(100, 0), 1));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            GameObjectManager.Update();
            camera.MoveToMid();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend,SamplerState.PointClamp, null, null, null, camera.get_transformation(GraphicsDevice));
            for (int i = -1000; i < 1000; i++)
            {
                spriteBatch.Draw(AssetManager.ground, new Vector2(i * 32, 0), new Rectangle(0, 0, 32, 16), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f); 
            }
            foreach (GameObject g in GameObjectManager.gameObjects)
            {
                g.DrawSprite(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
