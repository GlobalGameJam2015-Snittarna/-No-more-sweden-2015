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

        public Gui()
        {
            displayScore = new float[Globals.numberOfPlayers];
        }

        public void Update()
        {
            foreach(Player p in GameObjectManager.gameObjects.Where(item => item is Player))
            {
                switch (p.playerIndex)
                {
                    case PlayerIndex.One:
                        displayScore[0] = Globals.Lerp(displayScore[0], p.Score, 0.1f);
                        break;
                    case PlayerIndex.Two:
                        displayScore[1] = Globals.Lerp(displayScore[1], p.Score, 0.1f);
                        break;
                    case PlayerIndex.Three:
                        displayScore[2] = Globals.Lerp(displayScore[2], p.Score, 0.1f);
                        break;
                    case PlayerIndex.Four:
                        displayScore[3] = Globals.Lerp(displayScore[3], p.Score, 0.1f);
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Globals.numberOfPlayers; i++)
            {
                bool pushOutFromScreen = ((i+1) % 2 == 0);

                string playerText =  "PLAYER " + (i + 1) + ": " + Convert.ToInt32(displayScore[i]);

                if (i < 2)
                    spriteBatch.DrawString(AssetManager.font, playerText, new Vector2((i * 640)-(AssetManager.font.MeasureString(playerText).X)*Convert.ToInt32(pushOutFromScreen), 10), new Color(150 + i * 50, 150, 150));
                else
                    spriteBatch.DrawString(AssetManager.font, playerText, new Vector2((i * 300) - 600 - (AssetManager.font.MeasureString(playerText).X) * Convert.ToInt32(pushOutFromScreen), 450), new Color(150, 150 + i * 50, 150));
            }
        }
    }
}
