using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class RotateShipLeft : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(Ship ship, GameTime gameTime, List<Command> commands)
        {
            ship.RotateLeft(gameTime);
            executeDebugMessage.MessageText = ship.ToString() + " executed rotate right";
            MessageBus.Instance.AddMessage(executeDebugMessage);
        }
    }
}
