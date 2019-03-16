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
        public abstract void Execute(Ship ship, GameTime gameTime, List<Command> commands);
        public abstract void Execute(List<Ship> ships, ShipModel shipModel);
    }
}
