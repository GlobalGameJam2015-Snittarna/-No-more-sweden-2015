using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace no_more_sweden_2015
{
    class GameObjectManager
    {
        public static List<GameObject> gameObjects = new List<GameObject>();
        static List<GameObject> gameObjectsToAdd = new List<GameObject>();
        static List<GameObject> gameObjectsToRemove = new List<GameObject>();

        public static void Update()
        {
            foreach (GameObject g in gameObjects)
                g.Update();

            foreach (GameObject g in gameObjectsToAdd)
                gameObjects.Add(g);
            gameObjectsToAdd.Clear();

            foreach (GameObject g in gameObjectsToRemove)
                gameObjects.Remove(g);
            gameObjectsToRemove.Clear();
        }

        public static void Add(GameObject gameObject)
        {
            gameObjectsToAdd.Add(gameObject);
        }

        public static void Remove(GameObject gameObject)
        {
            gameObjectsToRemove.Add(gameObject);
        }
    }
}
