using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class Ship : Entity
    {
        protected bool shipActive = true;

        public bool ShipActive
        {
            get { return shipActive; }
            set { shipActive = value; }
        }

        [JsonProperty]
        private string name = "";
        //private float timer = 3;
        private const float TIMER = 3;
        private ResourceManager resourceManager;
        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        [JsonProperty]
        private Vector3 position;
        [JsonProperty]
        private float rotationY;
        private Message positionMessage;
        private Message rotationMessage;
        private Message debugMessage;

        public BoundingSphere boundingSphere;

        public Ship(Vector3 pos, string name)
        {
            this.name = name;
            position = pos;
            rotationY = 0f;
            world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(pos);
            positionMessage = new Message(MessageType.Console, world.Translation.ToString());
            rotationMessage = new Message(MessageType.Console, world.Forward.ToString());
            debugMessage = new Message(MessageType.Console, "");
            MessageBus.Instance.AddMessage(positionMessage);
            MessageBus.Instance.AddMessage(rotationMessage);
        }

        public void LoadModel(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
            /*
            foreach (ModelMesh m in this.resourceManager.model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, m.BoundingSphere);
            }*/
            boundingSphere = resourceManager.boundingSphere;
            boundingSphere.Center = position;
            Console.WriteLine("centro: " + boundingSphere.Center);
        }

        public override bool CheckCollision(Ship otherShip)
        {
            if (boundingSphere.Intersects(otherShip.boundingSphere))
            {
                debugMessage.MessageText = "colidiu com a nave " + otherShip.name;
                MessageBus.Instance.AddMessage(new Message(MessageType.ParticleEffect, otherShip.GetPosition(),100));
                MessageBus.Instance.AddMessage(debugMessage);
                return true;
            }
            return false;
            
        }

        public void CheckOctreeCollision(BoundingBox bounds)
        {
            if (boundingSphere.Intersects(bounds))
            {
                debugMessage.MessageText = "colidiu com a bounds";
                MessageBus.Instance.AddMessage(debugMessage);
            }
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            //DebugShapeRenderer.AddBoundingSphere(this.boundingSphere,Color.Red);

            if (shipActive && resourceManager != null && Game1.cam.frustum.Intersects(boundingSphere))
            {
                foreach (ModelMesh mesh in resourceManager.model[0].Meshes)
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

        public void Instantiate(Vector3 position, float rotY)
        {
            shipActive = true;
            SetPosition(position);
            SetRotation(rotY);
        }

        public void UpdateShip(GameTime gameTime)
        {
            if (shipActive)
            {
                position += world.Forward * Settings.SuggestedEnemyMovementSpeedFactor * gameTime.ElapsedGameTime.Milliseconds;
                world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(position);
                boundingSphere.Center = position;

                //se ja passou pelo player mas não rebentou (Saiu do ecra para baixo)
                if(position.Z >= Settings.MaxZPos && shipActive)
                {
                    SetPosition(new Vector3(Settings.SuggestedEnemyStartingPosition.X + Dice.RollDice(-280, 280), 0, Settings.SuggestedEnemyStartingPosition.Z + Dice.RollDice(-100, 0)));
                    SetRotation(Settings.EnemyRotationFactor);
                }
            }
        }

        public Vector3 GetPosition()
        {
            return position;
        }


        public void SetPosition(Vector3 newPos)
        {
            position = newPos;
            world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(position);
        }

        public void SetRotation(float rotY)
        {
            rotationY = rotY;
            world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(position);
        }


        public void MoveForward(GameTime gameTime)
        {
            position += world.Forward * Settings.SuggestedPlayerMovementSpeedFactor * gameTime.ElapsedGameTime.Milliseconds;
            world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(position);
            positionMessage.MessageText = world.Translation.ToString();
            MessageBus.Instance.AddMessage(positionMessage);
            boundingSphere.Center = position;
        }
        public void MoveBackward(GameTime gameTime)
        {
            position -= world.Forward * Settings.SuggestedPlayerMovementSpeedFactor * gameTime.ElapsedGameTime.Milliseconds;
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
        public void StrafeLeft(GameTime gameTime)
        {
            position += world.Left * Settings.SuggestedPlayerMovementSpeedFactor * gameTime.ElapsedGameTime.Milliseconds;
            world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(position);
            positionMessage.MessageText = world.Translation.ToString();
            MessageBus.Instance.AddMessage(positionMessage);
            boundingSphere.Center = position;
        }
        public void StrafeRight(GameTime gameTime)
        {
            position += world.Right * Settings.SuggestedPlayerMovementSpeedFactor * gameTime.ElapsedGameTime.Milliseconds;
            world = Matrix.CreateRotationY(rotationY) * Matrix.CreateTranslation(position);
            positionMessage.MessageText = world.Translation.ToString();
            MessageBus.Instance.AddMessage(positionMessage);
            boundingSphere.Center = position;
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
