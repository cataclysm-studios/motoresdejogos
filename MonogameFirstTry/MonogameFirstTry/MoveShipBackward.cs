using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class MoveShipBackward : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(Camera cam, List<Ship> ships, Ship ship, GameTime gameTime, List<Command> commands, ResourceManager resourceManager)
        {
            ship.MoveBackward(gameTime);
            executeDebugMessage.MessageText = ship.ToString() + " executed move Backwards";
            MessageBus.Instance.AddMessage(executeDebugMessage);
        }
    }
}
