using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonogameFirstTry
{

    //    Copyright (C) 2011 Daniel Rosser
    //    This program is free software: you can redistribute it and/or modify
    //    it under the terms of the GNU General Public License as published by
    //    the Free Software Foundation, either version 3 of the License, or
    //    (at your option) any later version.
    //
    //    This program is distributed in the hope that it will be useful,
    //    but WITHOUT ANY WARRANTY; without even the implied warranty of
    //    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    //    GNU General Public License for more details.
    //
    //    You should have received a copy of the GNU General Public License
    //    along with this program. See /LICENSES/gpl-3.0.txt
    //    If not, see <http://www.gnu.org/licenses/>.
    //
    //    Daniel Rosser <danoli3@gmail.com> 



    public class Octree
    {
        private const int maxObjectsInNode = 1;
        private const float minSize = 75.0f;

        // center coordinate point for the octree
        private Vector3 center;
        // size of the octree space
        private float size;

        List<Entity> objects;
        public BoundingBox bounds = default(BoundingBox);

        Octree parent = null;
        Octree nodeUFL;
        Octree nodeUFR;
        Octree nodeUBL;
        Octree nodeUBR;
        Octree nodeDFL;
        Octree nodeDFR;
        Octree nodeDBL;
        Octree nodeDBR;
        List<Octree> children;

        public static int modelsDrawn;
        private static int modelsStoredinOctreeTree;
        private static int modelsFullyContainedInNodes;
        private static int modelsIntersectionsInNodes;
        int collisionNodesTraversedLast;
        int collisionObjectCompaisions;

        private static String collisionName;

        public int ModelsDrawn { get { return modelsDrawn; } set { modelsDrawn = value; } }
        public int ModelsInOctree { get { return modelsStoredinOctreeTree; } set { modelsStoredinOctreeTree = value; } }
        public int ModelsFullyContainedInNodes { get { return modelsFullyContainedInNodes; } set { modelsFullyContainedInNodes = value; } }
        public int ModelsIntersectionsInNodes { get { return modelsIntersectionsInNodes; } set { modelsIntersectionsInNodes = value; } }



        public static int modelsInNodes;
        public int ModelsInNodes { get { return modelsInNodes; } set { modelsInNodes = value; } }

        /// <summary>
        /// Octree - Creates a spatial partitioning octree
        /// </summary>
        /// <param name="center">Center of the Octree</param>
        /// <param name="size">How large is the octree</param>
        /// <param name="parent">The parent node (if not the root)</param>
        public Octree(Vector3 center, float size, Octree parent = null)
        {
            this.parent = parent;
            this.center = center;
            this.size = size;
            objects = new List<Entity>();
            children = new List<Octree>(8);

            Vector3 diagonalVector = new Vector3(size / 2.0f, size / 2.0f, size / 2.0f);
            bounds = new BoundingBox(center - diagonalVector, center + diagonalVector);

        }

        /// <summary>
        /// Return the Size of the Octree
        /// </summary>
        /// <returns>float</returns>
        public float WorldSize()
        {
            return size;
        }

        /// <summary>
        /// Returns the 8 children of this node so one can iterate over them
        /// </summary>
        /// <returns>List of Octree Nodes</returns>
        public List<Octree> GetChildren()
        {
            return children;
        }

        /// <summary>
        /// NeedSplit - Does this Node need to be split (max objects contained)
        /// </summary>
        private Boolean NeedSplit
        {
            get
            {
                return (objects.Count != 0 && maxObjectsInNode < objects.Count && size >= minSize);
            }
        }
        /// <summary>
        /// Is this Node Empty
        /// </summary>
        protected Boolean IsEmpty
        {
            get
            {
                return ((objects.Count == 0) && (HasChildren == false));
            }
        }

        /// <summary>
        /// Are the children nodes emtpy
        /// </summary>
        protected Boolean AreChildrenEmpty
        {
            get
            {
                if (HasChildren == false)
                    return true;

                for (int i = 0; i < 8; ++i)
                {
                    if (!children[i].IsEmpty)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Does this Node have children
        /// </summary>
        private Boolean HasChildren
        {
            get
            {
                return (children.Count != 0);
            }
        }


        /// <summary>
        /// What type of containment is this bounding box to this
        /// </summary>
        /// <param name="bsbounds">ref bouding box</param>
        /// <returns>ContainmentType (Disjoint, Intersects, Contained)</returns>
        protected ContainmentType Containment(ref BoundingBox bbbounds)
        {
            ContainmentType containmentType;
            bounds.Contains(ref bbbounds, out containmentType);
            return (containmentType);
        }
        /// <summary>
        /// What type of containment is this bounding sphere to this
        /// </summary>
        /// <param name="bsbounds">ref bouding sphere</param>
        /// <returns>ContainmentType (Disjoint, Intersects, Contained)</returns>
        protected ContainmentType Containment(ref BoundingSphere bsbounds)
        {
            ContainmentType containmentType;
            bounds.Contains(ref bsbounds, out containmentType);
            return (containmentType);
        }


        /// <summary>
        /// Can this Node contain this bounding sphere
        /// </summary>
        /// <param name="bsbounds">ref bouding sphere</param>
        /// <returns>Boolean</returns>
        protected Boolean CanContain(ref BoundingSphere bsbounds)
        {
            return (ContainmentType.Contains == Containment(ref bsbounds));
        }
        /// <summary>
        /// Can this Node contain this bounding box
        /// </summary>
        /// <param name="bsbounds">ref bouding box</param>
        /// <returns>Boolean</returns>
        protected Boolean CanContain(ref BoundingBox bbounds)
        {
            return (ContainmentType.Contains == Containment(ref bbounds));
        }
        /// <summary>
        /// Can this bounding box intersect
        /// </summary>
        /// <param name="bbounds">ref bounding box</param>
        /// <returns>Boolean</returns>
        protected Boolean CanIntersect(ref BoundingBox bbounds)
        {
            return (ContainmentType.Intersects == Containment(ref bbounds));
        }
        /// <summary>
        /// Can this bounding sphere intersect
        /// </summary>
        /// <param name="bbounds">ref bounding box</param>
        /// <returns>Boolean</returns>
        protected Boolean CanIntersect(ref BoundingSphere bsbounds)
        {
            return (ContainmentType.Intersects == Containment(ref bsbounds));
        }
        /// <summary>
        /// Can this Octree node contain or intersect this Entity
        /// </summary>
        /// <param name="entity">Object Entity</param>
        /// <returns>Boolean</returns>
        protected Boolean CanContainOrIntersect(Entity entity)
        {
            if (entity.collision == Entity.Collision.BoundingSphere)
                return CanContainOrIntersect(entity.boundsphere);
            else
                return CanContainOrIntersect(entity.bounds);

        }

        protected Boolean CanContainOrIntersect(BoundingBox bbounds)
        {
            if (ContainmentType.Disjoint == Containment(ref bbounds))
                return false;
            else
                return true;
        }
        protected Boolean CanContainOrIntersect(BoundingSphere bsbounds)
        {
            if (ContainmentType.Disjoint == Containment(ref bsbounds))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Can this Octree Node contain this entity
        /// </summary>
        /// <param name="entity">Object Entity</param>
        /// <returns>Boolean</returns>
        public Boolean CanContain(Entity entity)
        {
            if (entity.collision == Entity.Collision.BoundingSphere)
                return CanContain(ref entity.boundsphere);
            else
                return CanContain(ref entity.bounds);
        }

        public Boolean containsObjects()
        {
            if (objects.Count != 0)
                return true;

            if (children.Count != 0)
            {

                for (int index = 0; index < children.Count; index++)
                {
                    if (children[index].containsObjects() == true)
                        return true;
                }
            }

            return false;
        }


        public void Add(Entity entity)
        {
            entity.ID = modelsStoredinOctreeTree++;
            AddObject(entity);
        }

        /// <summary>
        /// Find the First Parent of the Entity
        /// </summary>
        /// <param name="entity">Object Entity</param>
        /// <returns>Octree</returns>
        private Octree FindEntityParent(Entity entity)
        {
            Octree octree = null;
            for (int index = 0; index < objects.Count; index++)
            {
                if (objects[index] == entity)
                {
                    return this;
                }
            }

            int child = 0;
            while ((octree == null) && (child < children.Count))
            {
                octree = children[child++].FindEntityParent(entity);
                if (octree != null)
                    break;
            }

            return octree;
        }

        /// <summary>
        /// Remove Entity based on its ID and return it // TODO add recursive
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        private Entity RemoveEntity(int modelID)
        {
            Entity entity = null;

            for (int index = 0; index < objects.Count; index++)
            {
                if (objects[index].ID == modelID)
                {
                    entity = objects[index];
                    objects.Remove(entity);
                    Collapse(this);
                    return entity;
                }
            }

            int child = 0;
            while ((entity == null) && (child < children.Count))
            {
                entity = children[child++].RemoveEntity(modelID);
                if (entity != null)
                    break;
            }

            return entity;
        }

        private void RemoveObject(int objectIndex, Boolean merge = true)
        {
            // remove the object            
            objects.RemoveAt(objectIndex);
            modelsInNodes--;

            if (merge == true)
            {
                // if all the children are empty lets clear the list
                if (AreChildrenEmpty)
                    children.Clear();

                // try and collapse / merge the Octree by notifying the parent to check other children
                if (IsEmpty)
                {
                    if (parent != null)
                        parent.AttemptMerge(this);
                }
            }
        }

        public Boolean RemoveObject(Entity octreeEntity)
        {
            if (octreeEntity == null) { throw new NullReferenceException("octreeEntity"); }

            if (!CanContain(octreeEntity)) // Object is definitely not in this tree
                return false;

            if (RemoveObjectHere(octreeEntity))//was pressent in this node
            {
                return true;
            }
            else //check child nodes
            {
                if (children.Count == 0)
                    return false;

                foreach (Octree child in children)
                {
                    if (child.RemoveObject(octreeEntity))
                        return true;
                }
            }

            return false;
        }

        public void RemoveObjectRecursively(Entity octreeEntity)
        {
            if (octreeEntity == null) { throw new NullReferenceException("octreeEntity"); }

            RemoveObjectHere(octreeEntity, false);
            if (children.Count != 0)
            {
                foreach (Octree child in children)
                    child.RemoveObjectRecursively(octreeEntity);
            }
        }

        protected Boolean RemoveObjectHere(Entity octreeEntity, Boolean merge = true)
        {
            if (objects.Count == 0)
                return false;       // no objects to remove            

            int objIndex = objects.IndexOf(octreeEntity);
            if (-1 == objIndex)
                return false;

            octreeEntity.InsideNodes--;
            RemoveObject(objIndex, merge);
            return true;
        }

        protected void AttemptMerge(Octree abandonedChild)
        {
            if (AreChildrenEmpty)
            {
                children.Clear();

                if (parent != null && objects.Count == 0)
                {
                    parent.AttemptMerge(this);
                }
            }
        }

        public void Collapse(Octree octree)
        {
            int childrenNumber = octree.children.Count;
            if (childrenNumber != 0)
            {
                int counter = 0;
                for (int index = 0; index < childrenNumber; index++)
                {
                    if (octree.children[index].containsObjects() == false && octree.parent != null)
                    {
                        octree.children[index].Collapse();
                        counter++;
                    }
                }
                if (counter == childrenNumber)
                {
                    for (int index = (childrenNumber - 1); index >= 0; index--)
                    {
                        octree.children.Remove(octree.children[index]);
                    }
                    //octree.children.Clear();
                }

            }

            if (octree.objects.Count > 0)
                return;
            else if (octree.children.Count > 0)
                return;
            else
            {
                if (octree.parent != null)
                {
                    octree.parent.Collapse(octree.parent);
                }
            }

        }

        public void Collapse()
        {
            int childrenNumber = children.Count;
            if (childrenNumber != 0)
            {
                int counter = 0;
                for (int index = 0; index < childrenNumber; index++)
                {
                    if (children[index].containsObjects() == false)
                    {
                        children[index].Collapse();
                        if (parent != null)
                            counter++; // meaning we don't remove the top octree root
                    }
                }
                if (counter == childrenNumber)
                {
                    for (int index = (childrenNumber - 1); index >= 0; index--)
                    {
                        children.Remove(children[index]);
                    }
                    //children.Clear();
                }

            }

        }


        public Boolean AddObject(Entity octreeEntity)
        {
            if (octreeEntity == null) { throw new NullReferenceException("octreeEntity"); }

            if (!CanContainOrIntersect(octreeEntity))
                return false;

            //if (!CanContain(octreeEntity) || 

            if (!HasChildren || NeedSplit) // If no children
            {
                AddObjectHere(octreeEntity);

                if (NeedSplit)
                {
                    CreateChildren();
                    DistributeObjectsToChildren();
                }
            }
            else
            {
                if (!AddObjectToChildren(octreeEntity))
                {
                    AddObjectHere(octreeEntity);
                }
            }

            return true;
        }

        protected void AddObjectHere(Entity octreeEntity)
        {
            modelsInNodes++;
            octreeEntity.InsideNodes++;
            // debug remove in production
            if (CanContain(octreeEntity) == true)
                modelsFullyContainedInNodes++;
            if (CanIntersect(ref octreeEntity.bounds) == true)
                modelsIntersectionsInNodes++;
            objects.Add(octreeEntity);
        }


        private void CreateChildren()
        {
            float sizeOver2 = size / 2.0f;
            float sizeOver4 = size / 4.0f;

            nodeUFR = new Octree(center + new Vector3(sizeOver4, sizeOver4, -sizeOver4), sizeOver2, this);
            nodeUFL = new Octree(center + new Vector3(-sizeOver4, sizeOver4, -sizeOver4), sizeOver2, this);
            nodeUBR = new Octree(center + new Vector3(sizeOver4, sizeOver4, sizeOver4), sizeOver2, this);
            nodeUBL = new Octree(center + new Vector3(-sizeOver4, sizeOver4, sizeOver4), sizeOver2, this);
            nodeDFR = new Octree(center + new Vector3(sizeOver4, -sizeOver4, -sizeOver4), sizeOver2, this);
            nodeDFL = new Octree(center + new Vector3(-sizeOver4, -sizeOver4, -sizeOver4), sizeOver2, this);
            nodeDBR = new Octree(center + new Vector3(sizeOver4, -sizeOver4, sizeOver4), sizeOver2, this);
            nodeDBL = new Octree(center + new Vector3(-sizeOver4, -sizeOver4, sizeOver4), sizeOver2, this);

            children.Add(nodeUFR);
            children.Add(nodeUFL);
            children.Add(nodeUBR);
            children.Add(nodeUBL);
            children.Add(nodeDFR);
            children.Add(nodeDFL);
            children.Add(nodeDBR);
            children.Add(nodeDBL);
        }

        protected void DistributeObjectsToChildren()
        {
            if (children.Count != 0 && objects.Count != 0)
            {
                for (int i = objects.Count - 1; i >= 0; --i)
                {
                    Entity octreeObject = objects[i];
                    if (AddObjectToChildren(octreeObject))
                    {
                        RemoveObject(i);
                    }
                    //else
                    //AddObjectHere(octreeObject); // it is left in the parent
                }
            }
        }

        protected Boolean AddObjectToChildren(Entity entity)
        {
            if (children.Count != 0)
            {
                for (int index = 0; index < children.Count; index++)
                {
                    if (children[index].CanContain(entity) == true)
                    {
                        children[index].AddObject(entity);
                        return true;
                    }
                }
                Boolean added = false;
                int counter = 0;
                for (int index = 0; index < children.Count; index++)
                {
                    if (children[index].Containment(ref entity.bounds) == ContainmentType.Intersects)
                        counter++;
                }
                if (counter == children.Count)
                    return false;

                for (int index = 0; index < children.Count; index++)
                {
                    if (children[index].Containment(ref entity.bounds) == ContainmentType.Intersects)
                    {
                        children[index].AddObject(entity);
                        added = true;
                    }

                }
                return added;
            }

            return false;
        }

        protected void AddObjectToParent(Entity octreeEntity)
        {
            if (octreeEntity == null) { throw new NullReferenceException("octreeEntity null"); }
            if (!AddObject(octreeEntity))
            {
                if (parent != null)
                    parent.AddObjectToParent(octreeEntity);
            }
        }

        /// <summary>
        /// Check if the Ray intersects the Octree node
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        private Boolean CheckRay(Ray ray)
        {
            if (ray.Intersects(this.bounds) != null)
            {
                return true;
            }
            else
                return false;
        }

        
        /*public Boolean ContainsDynamic(BoundingSphere bounds)
        {
            if (bounds == null) { throw new NullReferenceException("bounds null"); }
            List<Octree> containers = new List<Octree>(FindContainers(bounds));
            collisionNodesTraversedLast = containers.Count;
            if (containers.Count != 0)
            {
                foreach (Octree octree in containers)
                {
                    if (octree.objects.Count != 0)
                    {
                        collisionObjectCompaisions = 0;
                        foreach (Entity comparedEntity in octree.objects)
                        {
                            if (comparedEntity.CheckCollision(bounds) == true && comparedEntity.type == Entity.Type.DYNAMIC)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public Boolean ContainsStatic(BoundingSphere bounds)
        {
            if (bounds == null) { throw new NullReferenceException("bounds null"); }
            List<Octree> containers = new List<Octree>(FindContainers(bounds));
            collisionNodesTraversedLast = containers.Count;
            if (containers.Count != 0)
            {
                foreach (Octree octree in containers)
                {
                    if (octree.objects.Count != 0)
                    {
                        collisionObjectCompaisions = 0;
                        foreach (Entity comparedEntity in octree.objects)
                        {
                            if (comparedEntity.CheckCollision(bounds) == true && comparedEntity.type == Entity.Type.STATIC)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }*/

        /// <summary>
        /// If ObjectChanged Update the node that it is located in (as it has tranformed in space and possibly moved nodes)
        /// </summary>
        /// <param name="octreeEntity"></param>
        public void ObjectChanged(Entity octreeEntity)
        {
            if (octreeEntity == null) { throw new NullReferenceException("octreeEntity null"); }
            Octree octree = FindEntityParent(octreeEntity);

            if (octree != null)
            {
                if (octree.parent != null)
                    octree.parent.RemoveObjectRecursively(octreeEntity); //destroy all instances of this entity
                else
                    octree.RemoveObjectRecursively(octreeEntity);

                if (octreeEntity.InsideNodes != 0)
                    this.RemoveObjectRecursively(octreeEntity);

                if (!octree.CanContain(octreeEntity)) // if the current top node cant contain the object
                {
                    if (octree.parent != null) // if parent is not null
                    {
                        if (octree.parent.CanContain(octreeEntity)) // if the parent can contain
                        {
                            if (!octree.parent.AddObject(octreeEntity))
                            { throw new NullReferenceException("Octree: ObjectChanged(): Object could not be added to parent node"); }
                        }
                        else if (octree.parent.parent != null)
                        {
                            if (octree.parent.parent.CanContain(octreeEntity)) // if the parents parent can contain (2 branches up as max, else be better putting into root)
                            {
                                if (!octree.parent.parent.AddObject(octreeEntity))
                                { throw new NullReferenceException("Octree: ObjectChanged(): Object could not be added to parent.parent node"); }
                            }
                            else
                            {
                                if (!this.AddObject(octreeEntity))
                                { throw new NullReferenceException("Octree: ObjectChanged(): Object could not be added to the root octree"); }
                            }
                        }
                        else
                        {
                            if (!this.AddObject(octreeEntity))
                            { throw new NullReferenceException("Octree: ObjectChanged(): Object could not be added to the root octree"); }
                        }
                    }
                    else
                    {
                        if (!this.AddObject(octreeEntity))
                        { throw new NullReferenceException("Octree: ObjectChanged(): Object could not be added to the root octree"); }
                    }
                }
                else
                    if (!octree.AddObject(octreeEntity))
                    throw new NullReferenceException("Octree: ObjectChanged(): Object could not be added back to its current node");
            }
            else
            {
                //throw new NullReferenceException("Octree: ObjectChanged(): Parent of changed object was null? Debug this!");
                if (!this.AddObject(octreeEntity))
                    throw new NullReferenceException("Octree: ObjectChanged(): Object could not be added to the root octree!!!???? out of range?");
            }

            if (octree != null)
                Collapse(octree);


        }


        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="viewMatrix">View Matrix</param>
        /// <param name="projectionMatrix">Project Matrix</param>
        /// <param name="cameraFrustrum">Bounding Camera Frustrum</param>
        public void Draw(Matrix viewMatrix, Matrix projectionMatrix, BoundingFrustum cameraFrustrum)
        {
            ContainmentType cameraNodeContainment = cameraFrustrum.Contains(bounds);
            if (cameraNodeContainment != ContainmentType.Disjoint)
            {
                if (objects.Count != 0)
                {
                    foreach (Entity entity in objects)
                    {
                        if (entity.drawn == false)
                        {
                            entity.Draw(viewMatrix, projectionMatrix);
                            modelsDrawn++;
                            
                            entity.drawn = true;
                        }
                    }
                    //Console.WriteLine("Models Drawn: " + modelsDrawn);
                }

                foreach (Octree childNode in children)
                    childNode.Draw(viewMatrix, projectionMatrix, cameraFrustrum);
            }
        }

        public void DrawBoxLines(/*Matrix viewMatrix, Matrix projectionMatrix, GraphicsDevice device, BasicEffect basicEffect*/)
        {
            /*foreach (Octree childNode in children)
                childNode.DrawBoxLines(viewMatrix, projectionMatrix, device, basicEffect);

            if (children.Count == 0)
                BoundingBoxRenderer.DrawBoundingBox(bounds, device, viewMatrix, projectionMatrix, Color.Orange);*/

            if (children.Count == 0)
            {
                DebugShapeRenderer.AddBoundingBox(bounds, Color.Red);
            }

            foreach (Octree childnode in children)
            {
                childnode.DrawBoxLines();
            }
        }

        public void DrawZoneOfDeath(Matrix viewMatrix, Matrix projectionMatrix, GraphicsDevice device, BasicEffect basicEffect)
        {
            Vector3 diagonalVector = new Vector3(2, 2, 2);
            BoundingBoxRenderer.DrawBoundingBox(new BoundingBox(center - diagonalVector, center + diagonalVector), device, viewMatrix, projectionMatrix, Color.Orange);
        }
    }
}
