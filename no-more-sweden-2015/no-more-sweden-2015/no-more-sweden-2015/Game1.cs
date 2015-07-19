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
    /// 

    enum GameState { StartScreen, Game, Win };
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        static internal GameState gameState = GameState.StartScreen;

        static internal int delay = 128*2;

        Gui gui;
        internal static Camera camera = new Camera();

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
            graphics.IsFullScreen = true;
        }

        PowerUpSpawner powerUpSpawner = new PowerUpSpawner();

        Random rnd = new Random();

        protected override void Initialize()
        {
            AssetManager.Load(Content);

            foreach (PlayerIndex playerIndex in Enum.GetValues(typeof(PlayerIndex)))
                if (GamePad.GetState(playerIndex).IsConnected) Globals.numberOfPlayers++;

            Globals.playersColors = new Color[Globals.numberOfPlayers];

            int slice = 640 / (Globals.numberOfPlayers + 1);

            for (int i = 1; i <= Globals.numberOfPlayers; i++)
            {
                GameObjectManager.Add(new Player(indexes[i - 1], new Vector2(slice * i, -100), rnd));
            }

            Random random = new Random();

            for (int i = 0; i < 500; i++)
            {
                GameObjectManager.Add(new BackgroundObject(new Vector2(random.Next(-9200, 9200), random.Next(-1000, -100)), (byte)random.Next(3), random));
            }

            for (int i = -100; i < 100; i++)
            {
                GameObjectManager.Add(new BackgroundObject(new Vector2(i*128+6, -64), (byte)random.Next(3, 7), random));
            }

            gui = new Gui();

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

            switch (gameState)
            {
                case GameState.StartScreen:
                    if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A)) gameState = GameState.Game;
                    break;
                case GameState.Game:
                    if (delay > 0) delay -= 1;

                    if (delay <= 0 || delay >= (128*2)-38)
                    {
                        powerUpSpawner.Update();

                        GameObjectManager.Update();
                        gui.Update();
                        camera.Update();

                        gui.Update();
                    }
                    break;
                case GameState.Win:
                    if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.B))
                    {
                        gameState = GameState.Game;
                        GameObjectManager.gameObjects.Clear();
                        Globals.numberOfPlayers = 0;
                        Initialize();
                    }
                    break;
            }
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start)) gameState = GameState.Win;
            GC.Collect();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null, null, camera.get_transformation(GraphicsDevice));
            for (int i = -1000; i < 1000; i++)
            {
                spriteBatch.Draw(AssetManager.ground, new Vector2(i * 32, 0), new Rectangle(0, 0, 32, 16), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f); 
            }
            foreach (GameObject g in GameObjectManager.gameObjects)
            {
                g.DrawSprite(spriteBatch);
            }
            spriteBatch.Draw(AssetManager.dirt, new Rectangle((int)(camera.Pos.X - 600) - 400, 16, 2000, 800), new Rectangle(0, 0, 600, 800), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.End();
            spriteBatch.Begin();
            gui.Draw(spriteBatch);
            if (delay > 0) spriteBatch.DrawString(AssetManager.font, "GET READY!", new Vector2(320, 240), Color.White, 0, new Vector2(AssetManager.font.MeasureString("GET READY!").X/2, AssetManager.font.MeasureString("GET READY!").Y/2), 1, SpriteEffects.None, 0);
            if (gameState == GameState.StartScreen)
            {
                spriteBatch.Draw(AssetManager.startScreen, Vector2.Zero, Color.White);
                spriteBatch.DrawString(AssetManager.font, "PRESS # TO START", new Vector2(225, 400), Color.White);
                spriteBatch.DrawString(AssetManager.font, "# TO SHOOT AND LEFT THUMBSTICK TO MOVE\nSHOOT ALL OTHER PLAYERS AND GET POWER-UPS", new Vector2(320, 240), Color.White, 0, new Vector2(AssetManager.font.MeasureString("# TO SHOOT AND LEFT THUMBSTICK TO MOVE\nSHOOT ALL OTHER PLAYERS AND GET POWER-UPS").X / 2, AssetManager.font.MeasureString("# TO SHOOT, RIGHT TRIGGER TO ACCELERATE AND LEFT THUMBSTICK TO MOVE\nSHOOT ALL OTHER PLAYERS AND GET POWER-UPS").Y / 2), 1, SpriteEffects.None, 0);

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
