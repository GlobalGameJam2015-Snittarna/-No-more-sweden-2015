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
    class Camera
    {
        public float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.5f) _zoom = 0.5f; if (_zoom > 1.5f) _zoom = 1.5f; } // Negative zoom will flip image
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public Camera()
        {
            _zoom = 1f;
            _pos = Vector2.Zero;
        }

        public void MoveToMid()
        {
            Vector2 mid = Vector2.Zero;
            int count = 0;
            foreach (Player p in GameObjectManager.gameObjects.Where(O => O is Player))
            {
                mid += p.Position;
                count++;
            }

            mid /= count;

            Pos = mid;
        }

        public void Update()
        {
            MoveToMid();
            CalculateLongest();
            Pos = new Vector2(Pos.X, MathHelper.Clamp(Pos.Y, -2000, -148));
        }

        public void CalculateLongest()
        {
            Vector2 longestDist = Vector2.Zero;

            foreach (Player p1 in GameObjectManager.gameObjects.Where(O1 => O1 is Player))
                foreach (Player p2 in GameObjectManager.gameObjects.Where(O2 => O2 is Player))
                    if (p1 != p2)
                    {
                        Vector2 currentDist = new Vector2(MathHelper.Distance(p1.Position.X, p2.Position.X), MathHelper.Distance(p1.Position.Y, p2.Position.Y));
                        if (currentDist.Length() > longestDist.Length()) longestDist = currentDist;
                    }

            Zoom = CalculateZoom(longestDist.Length());

        }

        public float CalculateZoom(float x)
        {
            return 1.5f - (0.001f * x);
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return _transform;
        }
    }
}
