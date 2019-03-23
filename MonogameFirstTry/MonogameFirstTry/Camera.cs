using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class Camera
    {
        public Matrix view;
        public Vector3 position;
        public Vector3 target;
        private Vector3 left;
        private Vector3 up;
        public Matrix projection;
        public BoundingFrustum frustum;

        public Camera()
        {
            position = new Vector3(0, 80, 200);
            target = new Vector3(0, 0, 0);
            left = new Vector3(-1, 0, 0);
            up = new Vector3(0, 1, 0);
            view = Matrix.CreateLookAt(position, target, Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 10000f);
            frustum = new BoundingFrustum(view * projection);
        }

        public void MoveForward(GameTime gameTime)
        {
            position += (target-position) * 0.005f * gameTime.ElapsedGameTime.Milliseconds;
            view = Matrix.CreateLookAt(position, target, Vector3.UnitY);
        }
        public void MoveBackward(GameTime gameTime)
        {
            position -= (target - position) * 0.005f * gameTime.ElapsedGameTime.Milliseconds;
            view = Matrix.CreateLookAt(position, target, Vector3.UnitY);
        }
        public void StrafeLeft(GameTime gameTime)
        {
            position += left * 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            target += left * 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            view = Matrix.CreateLookAt(position, target, Vector3.UnitY);
        }
        public void StrafeRight(GameTime gameTime)
        {
            position -= left * 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            target -= left * 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            view = Matrix.CreateLookAt(position, target, Vector3.UnitY);
        }
        public void MoveUp(GameTime gameTime)
        {
            position += up * 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            target += up * 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            view = Matrix.CreateLookAt(position, target, Vector3.UnitY);
        }
        public void MoveDown(GameTime gameTime)
        {
            position -= up * 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            target -= up * 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            view = Matrix.CreateLookAt(position, target, Vector3.UnitY);
        }

        public Matrix View()
        {
            return view;
        }

        public Matrix Projection()
        {
            return projection;
        }

        public BoundingFrustum Frustum()
        {
            return frustum;
        }
    }
}
