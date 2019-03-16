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

        public override void Execute(List<Ship> ships, ShipModel shipModel)
        {
            SaveManager.Instance.LoadShipStates(ships, shipModel);
            MessageBus.Instance.AddMessage(executeDebugMessage);
        }

        public override void Execute(Ship ship, GameTime gameTime, List<Command> commands)
        {
            throw new NotImplementedException();
        }
        
    }
}
