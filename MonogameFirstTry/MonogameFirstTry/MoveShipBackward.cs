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

        public override void Execute(Ship ship, GameTime gameTime, List<Command> commands)
        {
            ship.MoveBackward(gameTime);
            executeDebugMessage.MessageText = ship.ToString() + " executed move Backwards";
            MessageBus.Instance.AddMessage(executeDebugMessage);
        }

        public override void Execute(List<Ship> ships, ResourceManager resourceManager)
        {
            throw new NotImplementedException();
        }
    }
}
