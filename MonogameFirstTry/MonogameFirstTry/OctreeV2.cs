using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public enum OctreeIndex
    {
        BottomLeftFront = 0, //000,
        BottomRightFront = 2, //010,
        BottomRightBack = 3, //011,
        BottomLeftBack = 1, //001,
        TopLeftFront = 4, //100,
        TopRightFront = 6, //110,
        TopRightBack = 7, //111,
        TopLeftBack = 5, //101,
    }

    public class Octree<TType>
    {
        private OctreeNode<TType> node;
        private int depth;
        private int maxItemsPerNode = 1;

        public Octree(Vector3 position, float size, int depth)
        {
            node = new OctreeNode<TType>(position, size);
            node.Subdivide(depth);
        }

        public class OctreeNode<TType>
        {
            Vector3 position;
            float size;
            OctreeNode<TType>[] subNodes;
            IList<TType> value;

            public OctreeNode(Vector3 pos, float size)
            {
                position = pos;
                this.size = size;
            }

            public IEnumerable<OctreeNode<TType>> Nodes
            {
                get { return subNodes; }
            }

            public Vector3 Position
            {
                get { return position; }
            }

            public float Size
            {
                get { return size; }
            }

            public void Subdivide(int depth = 0)
            {
                subNodes = new OctreeNode<TType>[8];
                for (int i = 0; i < subNodes.Length; ++i)
                {
                    Vector3 newPos = position;
                    if ((i & 4) == 4)
                    {
                        newPos.Y += size * 0.25f;
                    }
                    else
                    {
                        newPos.Y -= size * 0.25f;
                    }

                    if ((i & 2) == 2)
                    {
                        newPos.X += size * 0.25f;
                    }
                    else
                    {
                        newPos.X -= size * 0.25f;
                    }

                    if ((i & 1) == 1)
                    {
                        newPos.Z += size * 0.25f;
                    }
                    else
                    {
                        newPos.Z -= size * 0.25f;
                    }

                    subNodes[i] = new OctreeNode<TType>(newPos, size * 0.5f);
                    if (depth > 0)
                    {
                        subNodes[i].Subdivide(depth - 1);
                    }
                }
            }

            public bool IsLeaf()
            {
                return subNodes == null;
            }
        }

        private int GetIndexOfPosition(Vector3 lookupPosition, Vector3 nodePosition)
        {
            int index = 0;

            index |= lookupPosition.Y > nodePosition.Y ? 4 : 0;
            index |= lookupPosition.X > nodePosition.X ? 2 : 0;
            index |= lookupPosition.Z > nodePosition.Z ? 1 : 0;

            return index;
        }

        public OctreeNode<TType> GetRoot()
        {
            return node;
        }
    }
}
