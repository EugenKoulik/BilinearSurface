using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Windows.Forms.DataVisualization.Charting;


namespace CompGraph_3
{
    internal class BilinearSurface
    {
        private Point p00;
        private Point p01;
        private Point p10;
        private Point p11;

        public BilinearSurface(Point P00, Point P01, Point P10, Point P11)
        {

            foreach (Point currentCoord in new List<Point> { P00, P01, P10, P11 })
            {
                if (currentCoord.X > 1 || currentCoord.X > 1 || currentCoord.Z > 1)
                {
                    throw new ArgumentException("Point coordinates exceed unit range");
                }
            }

            p00 = P00;
            p01 = P01;
            p10 = P10;
            p11 = P11;
        }

        public List<Point> GetPoints(float step)
        {
            var points = new List<Point>();

            for (int i = 0; i < (1 / step); i++)
            {
                for (int j = 0; j < (1 / step); j++)
                {
                    points.Add(GetPoint(new Point(i * step, j * step)));
                }
            }

            return points;
        }

        private Point GetPoint(Point Point)
        {
            if (Point.X < 0 || Point.X > 1 || Point.Y < 0 || Point.Y > 1) throw new ArgumentException("Point coordinates exceed unit range");

            var newPoint = new Point();

            newPoint.X = (p00.X * (1 - Point.X) * (1 - Point.Y)) + (p01.X * (1 - Point.X) * Point.Y) + (p10.X * Point.X * (1 - Point.Y)) + (p11.X * Point.X * Point.Y);
            newPoint.Y = (p00.Y * (1 - Point.X) * (1 - Point.Y)) + (p01.Y * (1 - Point.X) * Point.Y) + (p10.Y * Point.X * (1 - Point.Y)) + (p11.Y * Point.X * Point.Y);
            newPoint.Z = (p00.Z * (1 - Point.X) * (1 - Point.Y)) + (p01.Z * (1 - Point.X) * Point.Y) + (p10.Z * Point.X * (1 - Point.Y)) + (p11.Z * Point.X * Point.Y);

            return newPoint;
        }    

        public List<Point> RotateX(List<Point> points, double degree)
        {
            var angle = Math.PI * degree / 180.0;

            double[,] primaryCords = new double[1, 4];

            var result = new List<Point>();

            double[,] rotateMatrix =
            {
                {1.0, 0.0, 0.0, 0.0},
                {0.0, Math.Cos(angle), Math.Sin(angle), 0.0},
                {0.0, -Math.Sin(angle), Math.Cos(angle), 0.0},
                {0.0, 0.0, 0.0, 1.0}

            };

            foreach (var point in points)
            {
                primaryCords[0, 0] = point.X;
                primaryCords[0, 1] = point.Y;
                primaryCords[0, 2] = point.Z;
                primaryCords[0, 3] = 1;

                primaryCords = matrixMul(primaryCords, rotateMatrix);

                result.Add(new Point((float)primaryCords[0, 0], (float)primaryCords[0, 1], (float)primaryCords[0, 2]));
            }

            return result;
        }

        public List<Point> RotateY(List<Point> points, double degree)
        {
            var angle = Math.PI * degree / 180.0;

            double[,] primaryCords = new double[1, 4];

            var result = new List<Point>();

            double[,] rotateMatrix =
            {
                {Math.Cos(angle), 0.0, -Math.Sin(angle), 0.0},
                {0.0, 1.0, 0.0, 0.0},
                {Math.Sin(angle), 0.0, Math.Cos(angle), 0.0},
                {0.0, 0.0, 0.0, 1.0}

            };

            foreach (var point in points)
            {
                primaryCords[0, 0] = point.X;
                primaryCords[0, 1] = point.Y;
                primaryCords[0, 2] = point.Z;
                primaryCords[0, 3] = 1;

                primaryCords = matrixMul(primaryCords, rotateMatrix);

                result.Add(new Point((float)primaryCords[0,0], (float)primaryCords[0,1], (float)primaryCords[0,2]));
            }

            return result;
        }

        public List<Point> RotateZ(List<Point> points, double degree)
        {

            var angle = Math.PI * degree / 180.0;

            double[,] primaryCords = new double[1, 4];

            var result = new List<Point>();

            double[,] rotateMatrix =
            {
                {Math.Cos(angle), Math.Sin(angle), 0.0, 0.0},
                {-Math.Sin(angle), Math.Cos(angle), 0.0, 0.0},
                {0.0, 0.0, 1.0, 0.0},
                {0.0, 0.0, 0.0, 1.0}

            };

            foreach (var point in points)
            {
                primaryCords[0, 0] = point.X;
                primaryCords[0, 1] = point.Y;
                primaryCords[0, 2] = point.Z;
                primaryCords[0, 3] = 1;

                primaryCords = matrixMul(primaryCords, rotateMatrix);

                result.Add(new Point((float)primaryCords[0, 0], (float)primaryCords[0, 1], (float)primaryCords[0, 2]));
            }

            return result;

        }

        private double[,] matrixMul(double[,] matrix1, double[,] matrix2)
        {
            double[,] result = new double[matrix1.Rank, matrix2.Length];

            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            return result;

        }
    }
}
