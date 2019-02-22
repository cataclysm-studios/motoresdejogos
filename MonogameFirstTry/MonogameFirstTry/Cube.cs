using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    class Cube
    {
        private Model model;
        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));

        public Cube(Vector3 pos)
        {
            world = Matrix.CreateTranslation(pos);
        }

        public void DrawCube(Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
        }

        public void UpdateCube(GameTime gameTime)
        {
            //world = Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds);
        }

        public void LoadContent(ContentManager content)
        {
            model = content.Load<Model>("Space_Invader");
        }
    }
}
