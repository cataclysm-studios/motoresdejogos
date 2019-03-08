using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class StrafeRight : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(Ship ship, GameTime gameTime, List<Command> commands)
        {
            ship.StrafeRight(gameTime);
            executeDebugMessage.MessageText = ship.ToString() + " executed move forward";
            MessageBus.Instance.AddMessage(executeDebugMessage);
        }
    }
}
