using System.Collections.Generic;
using UnityEngine;

namespace MiniDini.Nodes
{
    /// <summary>
    /// <see cref="Node"/> that has a list of children.
    /// </summary>
    [System.Serializable]
    public class GridNode : Node
    {
        [SerializeField]
        public ConstructionPlane editplane = new ConstructionPlane();
        [SerializeField]
        public float width = 10f;
        [SerializeField]
        public float height = 10f;
        [SerializeField]
        public uint rows = 1;
        [SerializeField]
        public uint columns = 1;

        #region Overrides of Node

        public override string GetDescription() { return "A grid made of NxM quads"; }

        /// <summary>
        /// Get the geometry for this Node.
        /// </summary>
        /// <returns>A geometry object</returns>
        public override Geometry GetGeometry()
        {
            if (m_geometry == null)
            {
                Debug.Log("GridNode:Geometry was null in GetGeometry, so creating");
                // create new geometry container
                m_geometry = new Geometry();
            }

            m_geometry.Empty();

            // here is where we construct the geometry for a grid

            List<int> indexs = new List<int>();
            List<Prim> prims = new List<Prim>();

            for (int i = 0; i < rows + 1; i++)
            {
                for (int j = 0; j < columns + 1; j++)
                {
                    Point a = new();
                    if (j % columns == 0)
                    {
                        a.position = editplane.right * width * i;
                    }
                    else
                    {
                        a.position = (editplane.up * height * j) + (editplane.right * width * i);
                    }
                    int index1 = m_geometry.AddPoint(a);
                    indexs.Add(index1);
                }
            }

            for (int i = 0; i < rows * columns; i++)
            {
                Prim p = new();
                m_geometry.AddPrim(p);
                prims.Add(p);
            }

            prims[0].points.Add(indexs[0]);
            prims[0].points.Add(indexs[1]);
            prims[0].points.Add(indexs[5]);
            prims[0].points.Add(indexs[4]);

            prims[1].points.Add(indexs[1]);
            prims[1].points.Add(indexs[2]);
            prims[1].points.Add(indexs[6]);
            prims[1].points.Add(indexs[5]);

            prims[2].points.Add(indexs[2]);
            prims[2].points.Add(indexs[3]);
            prims[2].points.Add(indexs[7]);
            prims[2].points.Add(indexs[6]);

            prims[3].points.Add(indexs[4]);
            prims[3].points.Add(indexs[5]);
            prims[3].points.Add(indexs[9]);
            prims[3].points.Add(indexs[8]);

            prims[4].points.Add(indexs[5]);
            prims[4].points.Add(indexs[6]);
            prims[4].points.Add(indexs[10]);
            prims[4].points.Add(indexs[9]);

            prims[5].points.Add(indexs[6]);
            prims[5].points.Add(indexs[7]);
            prims[5].points.Add(indexs[11]);
            prims[5].points.Add(indexs[10]);

            prims[6].points.Add(indexs[8]);
            prims[6].points.Add(indexs[9]);
            prims[6].points.Add(indexs[13]);
            prims[6].points.Add(indexs[12]);

            prims[7].points.Add(indexs[9]);
            prims[7].points.Add(indexs[10]);
            prims[7].points.Add(indexs[14]);
            prims[7].points.Add(indexs[13]);

            prims[8].points.Add(indexs[10]);
            prims[8].points.Add(indexs[11]);
            prims[8].points.Add(indexs[15]);
            prims[8].points.Add(indexs[14]);

            return m_geometry;
        }


        #endregion
    }
}