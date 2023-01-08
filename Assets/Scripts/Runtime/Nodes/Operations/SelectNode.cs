
using System.Collections.Generic;
using UnityEngine;

namespace MiniDini.Nodes
{
    /// <summary>
    /// <see cref="Node"/> that has a list of children.
    /// </summary>
    [System.Serializable]
    public class SelectNode : Node
    {
        public enum SelectionMode
		{
            Inside,
            Outside
		}

        public enum SelectionType
		{
            PointsOnly,
            PointsAndPrims,
            PrimsOnly
		}


        #region Overrides of Node

        // the idea here, is that we can select points within a sphere or outside it
        // we can also select any prim if a point of that prim is within the sphere (or outside if we set that as the mode)

        [SerializeField]
        public Vector3 point = Vector3.zero;
        [SerializeField]
        public float radius = 1.0f;

        [SerializeField]
        public SelectionMode selmode = SelectionMode.Inside;
        [SerializeField]
        public SelectionType seltype = SelectionType.PrimsOnly;




        public override string GetDescription() { return "Select incoming geometry"; }

        /// <summary>
        /// Get the geometry for this Node.
        /// </summary>
        /// <returns>A geometry object</returns>
        public override Geometry GetGeometry()
        {
            if (m_geometry == null)
            {
                Debug.Log("SelectNode:Geometry was null in GetGeometry, so creating");

                // create new geometry container if we don't have one from parent just so we return something
                if (m_geometry == null)
                    m_geometry = new Geometry();
            }

            m_geometry.Empty();

            // here is where we construct the geometry 
            List<Node> parents = GetParents();




            if (parents.Count > 0)
            {
                Geometry parent_geometry = parents[0].GetGeometry();
                // make a copy of first parents geometry (we should only have one parent!)
                m_geometry.Copy(parent_geometry);

                if (selmode == SelectionMode.Inside && seltype == SelectionType.PointsOnly)
                {
                    foreach (Point p in m_geometry.points)
                    {
                        bool inside = Vector3.Distance(p.position, point) <= radius;
                        if (inside) // (selmode == SelectionMode.Outside && !inside)
                        {
                            p.selected = true;
                            // If we are selecting points and prims, or prims only, we also need to select the prims that contain the selected point
                        }
                    }
                }
                else if (selmode == SelectionMode.Outside && seltype == SelectionType.PointsOnly)
                {
                    foreach(Point p in m_geometry.points)
                    {
                        bool outside = Vector3.Distance(p.position, point) > radius;
                        if (outside) p.selected = true;
                    }
                    
                }

                    // Iterate through the points in the parent geometry
                if ((seltype == SelectionType.PointsAndPrims || seltype == SelectionType.PrimsOnly) && selmode == SelectionMode.Inside)
                {
                    foreach (Prim prim in m_geometry.prims)
                    {
                        foreach (int p in prim.points)
                        {
                            bool primInside = Vector3.Distance(m_geometry.points[p].position, point) <= radius;
                            if (primInside)
                            {
                                prim.selected = true;
                            }
                        }
                    }
                }



                // If we are selecting prims only, we need to iterate through the prims and select them if they are inside or outside the sphere

                else if ((seltype == SelectionType.PointsAndPrims || seltype == SelectionType.PrimsOnly) && selmode == SelectionMode.Outside)
                {

                    foreach (Prim prim in m_geometry.prims)
                    {
                        foreach (int p in prim.points)
                        {
                            bool primInside = Vector3.Distance(m_geometry.points[p].position, point) <= radius;
                            if (primInside)
                            {
                                prim.selected = false;
                            }
                        }
                    }

                }




                // Copy the modified points and prims from the parent geometry to the node's geometry
                //m_geometry.points.AddRange(m_geometry.points.FindAll(p => p.selected));
                //m_geometry.prims.AddRange(m_geometry.prims.FindAll(p => p.selected));
            }

            return m_geometry;
        }


        #endregion
    }
}