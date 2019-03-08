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
    public class Ship
    {
        protected bool shipActive = true;

        public bool ShipActive
        {
            get { return shipActive; }
            set { shipActive = value; }
        }

        private float timer = 3;
        private const float TIMER = 3;
        private ShipModel shipModel;
        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Vector3 position;
        private float rotationY;
        private Message positionMessage;
        private Message rotationMessage;
        private Message debugMessage;

        public BoundingSphere boundingSphere;

        public Ship(Vector3 pos)
        { 
            position = pos;
            rotationY = 0;
            world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(pos);
            positionMessage = new Message(MessageType.Console, world.Translation.ToString());
            rotationMessage = new Message(MessageType.Console, world.Forward.ToString());
            debugMessage = new Message(MessageType.Console, "");
            MessageBus.Instance.AddMessage(positionMessage);
            MessageBus.Instance.AddMessage(rotationMessage);
        }

        public void LoadModel(ShipModel shipModel)
        {
            this.shipModel = shipModel;
            /*
            foreach (ModelMesh m in this.shipModel.model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, m.BoundingSphere);
            }*/
            boundingSphere = shipModel.boundingSphere;
        }

        public void isColliding(BoundingSphere otherShip, int ID)
        {
            if (boundingSphere.Intersects(otherShip))
            {
                debugMessage.MessageText = "colidiu com a nave " + ID;
                MessageBus.Instance.AddMessage(debugMessage);
            }
        }

        public void DrawShip(Matrix view, Matrix projection)
        {
            if (shipActive)
            {
                foreach (ModelMesh mesh in shipModel.model.Meshes)
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
        }

        public void Instantiate(Vector3 position)
        {
            shipActive = true;
            this.position = position;
        }

        public void UpdateShip(GameTime gameTime)
        {
            //world *= Matrix.CreateRotationY(0.005f);
            /*
            
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(timer < 0)
            {
                positionMessage.MessageText = world.Translation.ToString();
                MessageBus.Instance.AddMessage(positionMessage);
                timer = TIMER;
            }

            */
        }
        public void MoveForward(GameTime gameTime)
        {
            position += world.Forward * 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(position);
            positionMessage.MessageText = world.Translation.ToString();
            MessageBus.Instance.AddMessage(positionMessage);
            boundingSphere.Center = position;
        }
        public void MoveBackward(GameTime gameTime)
        {
            position -= world.Forward * 5;
            world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(position);
            positionMessage.MessageText = world.Translation.ToString();
            MessageBus.Instance.AddMessage(positionMessage);
            boundingSphere.Center = position;
        }
        public void RotateRight(GameTime gameTime)
        {
            rotationY -= 0.05f;
            world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(position);
            rotationMessage.MessageText = world.Forward.ToString();
            MessageBus.Instance.AddMessage(rotationMessage);
        }
        public void RotateLeft(GameTime gameTime)
        {
            rotationY += 0.05f;
            world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(position);
            rotationMessage.MessageText = world.Forward.ToString();
            MessageBus.Instance.AddMessage(rotationMessage);
        }
        /*public void ReplayActions(GameTime gameTime, List<Command> commands)
        {
            foreach (Command actions in commands)
            {
                actions.Execute(this, gameTime, commands);
            }
        }*/
    }
}
