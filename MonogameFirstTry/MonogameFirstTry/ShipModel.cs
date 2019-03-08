using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class ShipModel
    {
        public Model model;
        public BoundingSphere boundingSphere;

        public void LoadShipModel(ContentManager content)
        {
            model = content.Load<Model>("Models/Ship1/p1_saucer");

            foreach (ModelMesh m in model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, m.BoundingSphere);
            }
        }
    }
}
