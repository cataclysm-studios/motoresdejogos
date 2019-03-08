using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    class Camera
    {
        private Matrix view;
        private Matrix projection;
        public BoundingFrustum frustum;

        public Camera()
        {
            view = Matrix.CreateLookAt(new Vector3(200, 200, 200), new Vector3(80, 0, 0), Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 1000f);
            frustum = new BoundingFrustum(view * projection);
        }

        public void MoveCameraTo()
        {

        }

        public Matrix View()
        {
            return view;
        }

        public Matrix Projection()
        {
            return projection;
        }
    }
}
