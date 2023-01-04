
using System.Collections.Generic;
using UnityEngine;

namespace MiniDini.Nodes
{
    /// <summary>
    /// <see cref="Node"/> that has a list of children.
    /// </summary>
    [System.Serializable]
    public class CubeNode : Node
    {

        #region Overrides of Node

        [SerializeField]
        protected ConstructionPlane editplane = new ConstructionPlane();
        [SerializeField]
        protected float size = 1.0f;

        public override string GetDescription() { return "A single cube"; }

        /// <summary>
        /// Get the geometry for this Node.
        /// </summary>
        /// <returns>A geometry object</returns>
        public override Geometry GetGeometry()
        {
            // Cache the Geometry object in a local variable
            Geometry geometry = m_geometry;

            // If the Geometry object is null, create a new one
            if (geometry == null)
            {
                Debug.Log("CubeNode:Geometry was null in GetGeometry, so creating");
                geometry = new Geometry();
            }

            // Clear out any old data from the Geometry object
            geometry.Empty();

            // Create a new list of Prims
            List<Prim> prims = new List<Prim>();

            // Create the 8 points for the cube
            Point a = new Point { position = editplane.up * size };
            Point b = new Point { position = editplane.right * size };
            Point c = new Point { position = editplane.down * size };
            Point d = new Point { position = editplane.left * size };
            Point e = new Point { position = editplane.normal * size + editplane.up * size };
            Point f = new Point { position = editplane.normal * size + editplane.right * size };
            Point g = new Point { position = editplane.normal * size + editplane.down * size };
            Point h = new Point { position = editplane.normal * size + editplane.left * size };

            // Add the 8 points to the list
            List<Point> points = new List<Point> { a, b, c, d, e, f, g, h };

            // Add the points to the Geometry object
            for (int i = 0; i < points.Count; i++)
            {
                geometry.AddPoint(points[i]);
            }

            // Add the 6 Prims to the list
            for (int i = 0; i < 6; i++)
            {
                Prim prim = new Prim();
                switch (i)
                {
                    case 0:
                        prim.points.AddRange(new[] { 0, 1, 2, 3 });
                        break;
                    case 1:
                        prim.points.AddRange(new[] { 4, 5, 6, 7 });
                        break;
                    case 2:
                        prim.points.AddRange(new[] { 0, 4, 5, 1 });
                        break;
                    case 3:
                        prim.points.AddRange(new[] { 1, 5, 6, 2 });
                        break;
                    case 4:
                        prim.points.AddRange(new[] { 2, 6, 7, 3 });
                        break;
                    case 5:
                        prim.points.AddRange(new[] { 3, 7, 4, 0 });
                        break;
                }
                prims.Add(prim);
            }

            // Add the Prims to the Geometry object
            for (int i = 0; i < prims.Count; i++)
            {
                geometry.AddPrim(prims[i]);
            }

            // Return the Geometry object
            return geometry;

            #endregion
        }
    }
}