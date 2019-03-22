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
    public class ResourceManager
    {
        public List<Model> model =  new List<Model>();
        public BoundingSphere boundingSphere;
        public List<string> path = new List<string>();

        public void Initalize()
        {
            path.Add("Models/Ship1/p1_saucer");
        }

        public void LoadModel(ContentManager content, int id)
        {
            model.Add(content.Load<Model>(path[0]));

            foreach (ModelMesh m in model[id].Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, m.BoundingSphere);
            }
        }
    }
}
