using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    class ShipModel
    {
        public Model model;

        public void LoadShipModel(ContentManager content)
        {
            model = content.Load<Model>("Models/Ship1/p1_saucer");
        }
    }
}
