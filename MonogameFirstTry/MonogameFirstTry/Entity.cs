using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public abstract class Entity
    {
        public enum Collision { BoundingSphere, BoundingBox }
        public bool drawn = false;
        public BoundingSphere bounds;
        public BoundingSphere boundsphere;
        public Collision collision;
        public int ID;
        public int InsideNodes;

        public enum Type { DYNAMIC , STATIC }

        public Type type;

        public abstract void Draw(Matrix viewMatrix, Matrix projectionMatrix);

        public abstract bool CheckCollision(BoundingSphere boundingSphere);
    }
}
