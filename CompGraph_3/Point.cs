using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Windows.Forms.DataVisualization.Charting;

namespace CompGraph_3
{
    public class Point
    {

        public float X;
        public float Y;
        public float Z;

        public Point(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public Point (float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Point() { }
 
    }
}
