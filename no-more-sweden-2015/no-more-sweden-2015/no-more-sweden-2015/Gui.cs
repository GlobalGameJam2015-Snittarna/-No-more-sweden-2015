using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace no_more_sweden_2015
{
    class Gui
    {
        float[] displayScore;

        byte[] gunTypes;

        int[] ammos;

        int getDataCount;

        string[] names;

        public Gui()
        {
            displayScore = new float[Globals.numberOfPlayers];
            ammos = new int[Globals.numberOfPlayers];
            gunTypes = new byte[Globals.numberOfPlayers];
            names = new string[Globals.numberOfPlayers];
        }

        public void Update()
        {
            foreach (Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                switch (p.playerIndex)
                {
                    case PlayerIndex.One:
                        displayScore[0] = Globals.Lerp(displayScore[0], p.Score, 0.1f);
                        gunTypes[0] = p.GunType;
                        names[0] = "Player 1: ";
                        if (p.GunType != 0)
                        {
                            ammos[0] = p.currentMaxAmmo - p.currentAmmo;
                        }
                        break;
                    case PlayerIndex.Two:
                        displayScore[1] = Globals.Lerp(displayScore[1], p.Score, 0.1f);
                        gunTypes[1] = p.GunType;
                        names[1] = "Player 2: ";
                        if (p.GunType != 0)
                        {
                            ammos[1] = p.currentMaxAmmo - p.currentAmmo;
                        }
                        break;
                    case PlayerIndex.Three:
                        displayScore[2] = Globals.Lerp(displayScore[2], p.Score, 0.1f);
                        gunTypes[2] = p.GunType;
                        names[2] = "Player 3: ";
                        if (p.GunType != 0)
                        {
                            ammos[2] = p.currentMaxAmmo - p.currentAmmo;
                        }
                        break;
                    case PlayerIndex.Four:
                        displayScore[3] = Globals.Lerp(displayScore[3], p.Score, 0.1f);
                        gunTypes[3] = p.GunType;
                        names[3] = "Player 4: ";
                        if (p.GunType != 0)
                        {
                            ammos[3] = p.currentMaxAmmo - p.currentAmmo;
                        }
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Globals.numberOfPlayers; i++)
            {
                bool pushOutFromScreen = ((i + 1) % 2 == 0);

                string playerText = names[i] + Convert.ToInt32(displayScore[i]);

                // For show
                if (i < 2)
                {
                    spriteBatch.DrawString(AssetManager.font, playerText, new Vector2((i * 640) - (AssetManager.font.MeasureString(playerText).X) * Convert.ToInt32(pushOutFromScreen) - 1, 10 - 1), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.DrawString(AssetManager.font, playerText, new Vector2((i * 640) - 600 - (AssetManager.font.MeasureString(playerText).X) * Convert.ToInt32(pushOutFromScreen) - 1, 450 - 1), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                }

                if (i < 2)
                {
                    spriteBatch.DrawString(AssetManager.font, playerText, new Vector2((i * 640) - (AssetManager.font.MeasureString(playerText).X) * Convert.ToInt32(pushOutFromScreen), 10), Globals.playersColors[i]);

                    if (gunTypes[i] != 0)
                    {
                        byte frame = 0;

                        if (gunTypes[i] == 2)
                        {
                            frame = 4;
                        }
                        if (gunTypes[i] == 3)
                        {
                            frame = 8;
                        }

                        for (int j = 0; j < ammos[i]/4; j++)
                        {
                            sbyte tmp = (pushOutFromScreen) ? (sbyte)-1 : (sbyte)1;
                            spriteBatch.Draw(AssetManager.ammo, new Vector2((i * 640) + (j*2) * tmp, 32), new Rectangle(frame, 0, 4, 16), Color.White);
                        }
                    }
                }
                else
                {
                    spriteBatch.DrawString(AssetManager.font, playerText, new Vector2((i * 640) - 600 - (AssetManager.font.MeasureString(playerText).X) * Convert.ToInt32(pushOutFromScreen), 450), Globals.playersColors[i]);
                    if (gunTypes[i] != 0)
                    {
                        byte frame = 0;

                        if (gunTypes[i] == 2)
                        {
                            frame = 4;
                        }
                        if (gunTypes[i] == 3)
                        {
                            frame = 8;
                        }

                        for (int j = 0; j < ammos[i] / 8; j++)
                        {
                            sbyte tmp = (pushOutFromScreen) ? (sbyte)-1 : (sbyte)1;
                            spriteBatch.Draw(AssetManager.ammo, new Vector2((i*300) - 600 + (j * 2) * tmp, 450), new Rectangle(frame, 0, 4, 16), Color.White);
                        }
                    }
                }
            }
        }
    }
}
