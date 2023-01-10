
using System.Collections.Generic;
using UnityEngine;

namespace MiniDini.Nodes
{
    /// <summary>
    /// <see cref="Node"/> that transforms geometry
    /// </summary>
    [System.Serializable]
    public class TransformNode : Node
    {
        [SerializeField]
        public Vector3 translation = new Vector3(0, 0, 0);

        [SerializeField]
        public Vector3 rotation = new Vector3(0, 0, 0);

        [SerializeField]
        public Vector3 scale = new Vector3(1, 1, 1);

        #region Overrides of Node

        public override string GetDescription() { return "A node that transforms selected geometry"; }

        /// <summary>
        /// Get the geometry for this Node.
        /// </summary>
        /// <returns>A geometry object</returns>
        public override Geometry GetGeometry()
        {
            if (m_geometry == null)
            {
                Debug.Log("TransformNode:Geometry was null in GetGeometry, so creating");
                // create new geometry container
                m_geometry = new Geometry();
            }

            m_geometry.Empty();

            // here is where we construct the geometry 
            List<Node> parents = GetParents();

            if (parents.Count > 0)
            {
                Geometry parentGeom = parents[0].GetGeometry();

                for (int i = 0; i < parentGeom.points.Count; i++)
                {
                    Point p = parentGeom.points[i];

                    // new point
                    Point newPoint = new Point();
                    newPoint.position = p.position;

                    // rotate
                    Quaternion rotationQuat = Quaternion.Euler(rotation);
                    newPoint.position = rotationQuat * newPoint.position;

                    // translate
                    newPoint.position += translation;

                    m_geometry.points.Add(newPoint);
                }

                // copy prims back
                for (int i = 0; i < parentGeom.prims.Count; i++)
                {
                    Prim parentPrim = parentGeom.prims[i];
                    Prim newPrim = new Prim();

                    for (int j = 0; j < parentPrim.points.Count; j++)
                    {
                        newPrim.points.Add(parentPrim.points[j]);
                    }

                    m_geometry.prims.Add(newPrim);
                }
            }

            return m_geometry;
        }

        #endregion
    }
}
