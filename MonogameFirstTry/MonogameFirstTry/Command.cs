using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public abstract class Command
    {
        public abstract void Execute(Camera cam, List<Ship> ships, Ship ship, GameTime gameTime, List<Command> commands, ResourceManager resourceManager);
    }

    public class MoveCamForward : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(Camera cam, List<Ship> ships, Ship ship, GameTime gameTime, List<Command> commands, ResourceManager resourceManager)
        {
            cam.MoveForward(gameTime);
            //executeDebugMessage.MessageText = ship.ToString() + " executed move forward";
            //MessageBus.Instance.AddMessage(executeDebugMessage);
        }
    }
    public class MoveCamBackward : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(Camera cam, List<Ship> ships, Ship ship, GameTime gameTime, List<Command> commands, ResourceManager resourceManager)
        {
            cam.MoveBackward(gameTime);
            //executeDebugMessage.MessageText = ship.ToString() + " executed move forward";
            //MessageBus.Instance.AddMessage(executeDebugMessage);
        }
    }
    public class StrafeCamLeft : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(Camera cam, List<Ship> ships, Ship ship, GameTime gameTime, List<Command> commands, ResourceManager resourceManager)
        {
            cam.StrafeLeft(gameTime);
            //executeDebugMessage.MessageText = ship.ToString() + " executed move forward";
            //MessageBus.Instance.AddMessage(executeDebugMessage);
        }
    }
    public class StrafeCamRight : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(Camera cam, List<Ship> ships, Ship ship, GameTime gameTime, List<Command> commands, ResourceManager resourceManager)
        {
            cam.StrafeRight(gameTime);
            //executeDebugMessage.MessageText = ship.ToString() + " executed move forward";
            //MessageBus.Instance.AddMessage(executeDebugMessage);
        }
    }
    public class MoveCamUp : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(Camera cam, List<Ship> ships, Ship ship, GameTime gameTime, List<Command> commands, ResourceManager resourceManager)
        {
            cam.MoveUp(gameTime);
            //executeDebugMessage.MessageText = ship.ToString() + " executed move forward";
            //MessageBus.Instance.AddMessage(executeDebugMessage);
        }
    }
    public class MoveCamDown : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(Camera cam, List<Ship> ships, Ship ship, GameTime gameTime, List<Command> commands, ResourceManager resourceManager)
        {
            cam.MoveDown(gameTime);
            //executeDebugMessage.MessageText = ship.ToString() + " executed move forward";
            //MessageBus.Instance.AddMessage(executeDebugMessage);
        }
    }
}
