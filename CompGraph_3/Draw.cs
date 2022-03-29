using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraph_3
{
    internal static class Draw
    {
        private static Brush brush = new SolidBrush(Color.Green);

        private static Pen pen = new Pen(Color.Red);

        private static Font drawFont = new Font("Arial", 8);

        private static StringFormat drawFormat = new StringFormat();

        public static void Landmark(Graphics g, List<PointF> points)
        {
            foreach (var item in points)
            {
                g.FillEllipse(brush, item.X * 100 + 100, item.Y * 100 + 400, 5, 5);
            }

            PointF _Xp = new PointF(points[3].X * 100 + 105, points[3].Y * 100 + 405);
            PointF _Yp = new PointF(points[2].X * 100 + 105, points[2].Y * 100 + 405);
            PointF _Zp = new PointF(points[1].X * 100 + 105, points[1].Y * 100 + 405);
            PointF _0P = new PointF(points[0].X * 100 + 105, points[0].Y * 100 + 405);

            g.DrawString("X", drawFont, brush,_Xp.X, _Xp.Y, drawFormat);
            g.DrawString("Y", drawFont, brush, _Yp.X, _Yp.Y, drawFormat);
            g.DrawString("Z", drawFont, brush, _Zp.X, _Zp.Y, drawFormat);
            g.DrawString("0", drawFont, brush, _0P.X, _0P.Y, drawFormat);

            g.DrawLine(pen, _0P, _Xp);
            g.DrawLine(pen, _0P, _Yp);
            g.DrawLine(pen, _0P, _Zp);
        }

        public static void Points(Graphics g, List<PointF> points)
        {
            foreach (var item in points)
            {
                g.FillEllipse(brush, item.X * 200 + 300, item.Y * 200 + 300, 15, 15);
            }
        }

        public static void Lines(Graphics g, List<PointF> points)
        {

            if(points.Count > 0)
            {
                g.DrawLines(pen, points.ToArray());
            }
        }
    }
}
