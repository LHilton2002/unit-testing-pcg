
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
            if (m_geometry == null)
            {
                Debug.Log("CubeNode:Geometry was null in GetGeometry, so creating");
                // create new geometry container
                m_geometry = new Geometry();
            }

            m_geometry.Empty();

            // here is where we construct the geometry for a cube


            Point a = new();
            a.position = editplane.up * size;

            Point b = new();
            b.position = editplane.right * size;

            Point c = new();
            c.position = editplane.down * size;

            Point d = new();
            d.position = editplane.left * size;

            Point e = new();
            e.position = editplane.normal * size + editplane.up * size;

            Point f = new();
            f.position = editplane.normal * size + editplane.right * size;

            Point g = new();
            g.position = editplane.normal * size + editplane.down * size;

            Point h = new();
            h.position = editplane.normal * size + editplane.left * size;



            int index1 = m_geometry.AddPoint(a);
            int index2 = m_geometry.AddPoint(b);
            int index3 = m_geometry.AddPoint(c);
            int index4 = m_geometry.AddPoint(d);
            int index5 = m_geometry.AddPoint(e);
            int index6 = m_geometry.AddPoint(f);
            int index7 = m_geometry.AddPoint(g);
            int index8 = m_geometry.AddPoint(h);

            Prim pa = new();
            Prim pb = new();
            Prim pc = new();
            Prim pd = new();
            Prim pe = new();
            Prim pf = new();

            pa.points.Add(index1);
            pa.points.Add(index2);
            pa.points.Add(index3);
            pa.points.Add(index4);

            pb.points.Add(index5);
            pb.points.Add(index8);
            pb.points.Add(index7);
            pb.points.Add(index6);

            pc.points.Add(index1);
            pc.points.Add(index5);
            pc.points.Add(index6);
            pc.points.Add(index2);

            pd.points.Add(index3);
            pd.points.Add(index2);
            pd.points.Add(index6);
            pd.points.Add(index7);

            pe.points.Add(index4);
            pe.points.Add(index3);
            pe.points.Add(index7);
            pe.points.Add(index8);

            pf.points.Add(index1);
            pf.points.Add(index4);
            pf.points.Add(index8);
            pf.points.Add(index5);

            m_geometry.AddPrim(pa);
            m_geometry.AddPrim(pb);
            m_geometry.AddPrim(pc);
            m_geometry.AddPrim(pd);
            m_geometry.AddPrim(pe);
            m_geometry.AddPrim(pf);



            return m_geometry;
        }

        #endregion
    }
}