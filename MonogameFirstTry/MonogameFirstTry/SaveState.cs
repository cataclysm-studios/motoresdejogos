using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class SaveState : Command
    {
        private Message executeDebugMessage = new Message(MessageType.Console, "");

        public override void Execute(List<Ship> ships, ShipModel shipModel)
        {
            SaveManager.Instance.UpdateShipList(ships);
            SaveManager.Instance.SaveShipStates();
            MessageBus.Instance.AddMessage(executeDebugMessage);
        }

        public override void Execute(Ship ship, GameTime gameTime, List<Command> commands)
        {
            throw new NotImplementedException();
        }
    }
}
