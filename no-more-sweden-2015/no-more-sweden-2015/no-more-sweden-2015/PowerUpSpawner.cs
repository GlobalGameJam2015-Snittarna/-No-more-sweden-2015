using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace no_more_sweden_2015
{
    class PowerUpSpawner
    {
        short spawnCount;

        short maxSpawnCount;

        public PowerUpSpawner()
        {
            maxSpawnCount = 128 * 3;
        }

        public void Update()
        {
            Random random = new Random();

            spawnCount += 1;

            if (spawnCount >= maxSpawnCount)
            {

                GameObjectManager.Add(new PowerUp(new Vector2(random.Next((int)Game1.camera.Pos.X - 320, (int)Game1.camera.Pos.X + 321), Game1.camera.Pos.Y - 300), 2));//(byte)random.Next(0, 5)));  


                spawnCount = 0;
            }
        }
    }
}
