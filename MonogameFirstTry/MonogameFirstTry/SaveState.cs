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

        public override void Execute(Camera cam, List<Ship> ships, Ship ship, GameTime gameTime, List<Command> commands, ResourceManager resourceManager)
        {
            SaveManager.Instance.UpdateShipList(ships);
            SaveManager.Instance.SaveShipStates();
            MessageBus.Instance.AddMessage(executeDebugMessage);
        }
    }
}
