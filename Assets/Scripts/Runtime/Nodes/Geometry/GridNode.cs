
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
        protected ConstructionPlane editplane = new ConstructionPlane();
        [SerializeField]
        protected float width = 10.0f;
        [SerializeField]
        protected float height = 10.0f;
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
            Point a = new();
            Point b = new();
            Point c = new();
            Point d = new();

            Point e = new();
            Point f = new();
            Point g = new();
            Point h = new();

            Point i = new();
            Point j = new();
            Point k = new();
            Point l = new();

            Point m = new();
            Point n = new();
            Point o = new();
            Point p = new();

            //

            a.position = editplane.up * height;
            b.position = editplane.right * width;
            c.position = editplane.right * width;
            d.position = editplane.right * width;

            e.position = editplane.up * height;
            f.position = editplane.left * width;
            g.position = editplane.left * width;
            h.position = editplane.left * width;

            i.position = editplane.up * height;
            j.position = editplane.right * width;
            k.position = editplane.right * width;
            l.position = editplane.right * width;

            m.position = editplane.up * height;
            n.position = editplane.left * width;
            o.position = editplane.left * width;
            p.position = editplane.left * width;

            //

            int index1 = m_geometry.AddPoint(a);
            int index2 = m_geometry.AddPoint(b);
            int index3 = m_geometry.AddPoint(c);
            int index4 = m_geometry.AddPoint(d);

            int index5 = m_geometry.AddPoint(e);
            int index6 = m_geometry.AddPoint(f);
            int index7 = m_geometry.AddPoint(g);
            int index8 = m_geometry.AddPoint(h);

            int index9 = m_geometry.AddPoint(i);
            int index10 = m_geometry.AddPoint(j);
            int index11 = m_geometry.AddPoint(k);
            int index12 = m_geometry.AddPoint(l);

            int index13 = m_geometry.AddPoint(m);
            int index14 = m_geometry.AddPoint(n);
            int index15 = m_geometry.AddPoint(o);
            int index16 = m_geometry.AddPoint(p);

            //

            Prim p1 = new();
            Prim p2 = new();
            Prim p3 = new();
            Prim p4 = new();
            Prim p5 = new();
            Prim p6 = new();
            Prim p7 = new();
            Prim p8 = new();
            Prim p9 = new();

            //

            p1.points.Add(index1);
            p1.points.Add(index2);
            p1.points.Add(index3);
            p1.points.Add(index4);

            p2.points.Add(index2);
            p2.points.Add(index5);
            p2.points.Add(index4);
            p2.points.Add(index7);

            p3.points.Add(index5);
            p3.points.Add(index6);
            p3.points.Add(index7);
            p3.points.Add(index8);

            p4.points.Add(index3);
            p4.points.Add(index4);
            p4.points.Add(index9);
            p4.points.Add(index10);

            p5.points.Add(index4);
            p5.points.Add(index7);
            p5.points.Add(index10);
            p5.points.Add(index13);

            p6.points.Add(index7);
            p6.points.Add(index8);
            p6.points.Add(index13);
            p6.points.Add(index14);

            p7.points.Add(index9);
            p7.points.Add(index10);
            p7.points.Add(index11);
            p7.points.Add(index12);

            p8.points.Add(index10);
            p8.points.Add(index13);
            p8.points.Add(index12);
            p8.points.Add(index15);

            p9.points.Add(index13);
            p9.points.Add(index14);
            p9.points.Add(index15);
            p9.points.Add(index16);

            //

            m_geometry.AddPrim(p1);
            m_geometry.AddPrim(p2);
            m_geometry.AddPrim(p3);
            m_geometry.AddPrim(p4);
            m_geometry.AddPrim(p5);
            m_geometry.AddPrim(p6);
            m_geometry.AddPrim(p7);
            m_geometry.AddPrim(p8);
            m_geometry.AddPrim(p9);





            return m_geometry;
        }


        #endregion
    }
}