using Microsoft.Xna.Framework;
using Newtonsoft.Json;
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
        [JsonIgnore]
        public BoundingSphere bounds;
        [JsonIgnore]
        public BoundingSphere boundsphere;
        [JsonIgnore]
        public Collision collision;
        public int ID;
        [JsonIgnore]
        public int InsideNodes;


        public enum Type { DYNAMIC , STATIC }

        [JsonIgnore]
        public Type type;

        public abstract void Draw(Matrix viewMatrix, Matrix projectionMatrix);

        public abstract bool CheckCollision(Ship otherShip);
    }
}
