using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonogameFirstTry
{
    public class LoadState : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(Camera cam, List<Ship> ships, Ship ship, GameTime gameTime, List<Command> commands, ResourceManager resourceManager)
        {
            SaveManager.Instance.LoadShipStates(ships, resourceManager);
            MessageBus.Instance.AddMessage(executeDebugMessage);
        }
        
    }
}
