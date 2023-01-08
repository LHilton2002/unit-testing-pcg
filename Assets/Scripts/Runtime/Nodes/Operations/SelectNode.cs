
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





                // Iterate through the points in the parent geometry
                foreach (Point p in parent_geometry.points)
                {
                    bool inside = Vector3.Distance(p.position, point) <= radius;
                    if ((selmode == SelectionMode.Inside && inside) || (selmode == SelectionMode.Outside && !inside))
                    {
                        p.selected = true;
                        // If we are selecting points and prims, or prims only, we also need to select the prims that contain the selected point
                        if (seltype == SelectionType.PointsAndPrims || seltype == SelectionType.PrimsOnly)
                        {
                            foreach (Prim prim in parent_geometry.prims)
                            {
                                if (prim.ContainsPoint(p))
                                {
                                    prim.selected = true;
                                }
                            }
                        }
                    }
                }




                // If we are selecting prims only, we need to iterate through the prims and select them if they are inside or outside the sphere
                if (seltype == SelectionType.PrimsOnly)
                {
                    foreach (Prim p in parent_geometry.prims)
                    {
                        bool inside = false;
                        // Check if any of the points of the prim are inside the sphere
                        foreach (Point pt in parent_geometry.points)
                        {
                            if (Vector3.Distance(pt.position, point) <= radius)
                            {
                                inside = true;
                                break;
                            }
                        }
                        if ((selmode == SelectionMode.Inside && inside) || (selmode == SelectionMode.Outside && !inside))
                        {
                            p.selected = true;
                        }
                    }
                }




                // Copy the modified points and prims from the parent geometry to the node's geometry
                m_geometry.points.AddRange(parent_geometry.points.FindAll(p => p.selected));
                m_geometry.prims.AddRange(parent_geometry.prims.FindAll(p => p.selected));



            }

            return m_geometry;
        }


        #endregion
    }
}