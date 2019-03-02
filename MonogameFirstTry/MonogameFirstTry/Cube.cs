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
        private float timer = 3;
        private const float TIMER = 3;

        private Model model;
        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Message positionMessage;

        public Cube(Vector3 pos)
        {
            world = Matrix.CreateTranslation(pos);
            positionMessage = new Message(MessageType.Console, world.Translation.ToString());
            MessageBus.Instance.AddMessage(positionMessage);
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
            world *= Matrix.CreateRotationY(0.005f);
            /*timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(timer < 0)
            {
                positionMessage.MessageText = world.Translation.ToString();
                MessageBus.Instance.AddMessage(positionMessage);
                timer = TIMER;
            }*/
        }

        public void LoadContent(ContentManager content)
        {
            model = content.Load<Model>("Models/Ship1/p1_saucer");
        }
    }
}
