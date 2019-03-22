using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonogameFirstTry
{
    public class RotateShipRight : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(Ship ship, GameTime gameTime, List<Command> commands)
        {
            ship.RotateRight(gameTime);
            executeDebugMessage.MessageText = ship.ToString() + " executed rotate right";
            MessageBus.Instance.AddMessage(executeDebugMessage);
        }

        public override void Execute(List<Ship> ships, ResourceManager resourceManager)
        {
            throw new NotImplementedException();
        }
    }
}
