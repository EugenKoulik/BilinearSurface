using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Windows.Forms.DataVisualization.Charting;

namespace CompGraph_3
{
    public partial class Form1 : Form
    {

        private Graphics graphics;
        private List<Point> bilinearPoints;
        private BilinearSurface bilinearSurface;
        private List<Point> axesPoints;

        private int rotateStep = 5;

        private int previousValueX = 0;
        private int previousValueY = 0;
        private int previousValueZ = 0;

        public Form1()
        {
            

            bilinearSurface = new BilinearSurface(new Point(0, 0, 1), new Point(1, 1, 1), new Point(1, 0, 0), new Point(0, 1, 0));

            bilinearPoints = bilinearSurface.GetPoints(0.1f);

            axesPoints = new List<Point>(){new Point(0, 0, 0), new Point(0, 0, 1), new Point(0, 1, 0), new Point(1, 0, 0)};

            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            graphics = e.Graphics;

            Draw.Points(graphics, TransformCoords(PlaneRepresentation.IsometryProjection(TransformCoords(bilinearPoints))));

            Draw.Landmark(graphics, TransformCoords(PlaneRepresentation.IsometryProjection(TransformCoords(axesPoints))));
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

            bilinearPoints = bilinearSurface.RotateX(bilinearPoints, GetAngle(rotateStep, previousValueX, hScrollBar1.Value));
            axesPoints = bilinearSurface.RotateX(axesPoints, GetAngle(rotateStep, previousValueX, hScrollBar1.Value));

            pictureBox1.Invalidate();

            previousValueX = hScrollBar1.Value;
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {

            bilinearPoints = bilinearSurface.RotateY(bilinearPoints, GetAngle(rotateStep, previousValueY, hScrollBar2.Value));
            axesPoints = bilinearSurface.RotateY(axesPoints, GetAngle(rotateStep, previousValueY, hScrollBar2.Value));

            pictureBox1.Invalidate();

            previousValueY = hScrollBar2.Value;
        }

        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {

            bilinearPoints = bilinearSurface.RotateZ(bilinearPoints, GetAngle(rotateStep, previousValueZ, hScrollBar3.Value));
            axesPoints = bilinearSurface.RotateZ(axesPoints, GetAngle(rotateStep, previousValueZ, hScrollBar3.Value));

            pictureBox1.Invalidate();

            previousValueZ = hScrollBar3.Value;
        }

        private int GetAngle(int degree, int previousValue, int currentValue)
        {
            int angle = degree;

            if (previousValue > currentValue)
            {
                angle = -degree;
            }

            return angle;
        }

        public float[,] TransformCoords(List<Point> points)
        {
            var result = new float[points.Count, 4];

            int i = 0;

            foreach (Point point in points)
            {
                result[i, 0] = point.X;
                result[i, 1] = point.Y;
                result[i, 2] = point.Z;
                result[i, 3] = 0;

                i++;
            }

            result[points.Count - 1, 3] = 1;

            return result;
        }

        public List<PointF> TransformCoords(float[,] point)
        {

            var result = new List<PointF>();

            for (int i = 0; i < point.GetLength(0); i++)
            {
                result.Add(new PointF(point[i, 0], point[i, 1]));
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bilinearSurface = new BilinearSurface(

                new Point(float.Parse(textBox1.Text), float.Parse(textBox2.Text), float.Parse(textBox3.Text)),
                new Point(float.Parse(textBox6.Text), float.Parse(textBox5.Text), float.Parse(textBox4.Text)),
                new Point(float.Parse(textBox9.Text), float.Parse(textBox8.Text), float.Parse(textBox7.Text)),
                new Point(float.Parse(textBox12.Text), float.Parse(textBox11.Text), float.Parse(textBox10.Text))
                );

            bilinearPoints.Clear();

            bilinearPoints = bilinearSurface.GetPoints(0.1f);

            pictureBox1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            textBox1.Text = "0,0";
            textBox2.Text = "0,0";
            textBox3.Text = "1,0";

            textBox6.Text = "1,0";
            textBox5.Text = "1,0";
            textBox4.Text = "1,0";

            textBox9.Text = "1,0";
            textBox8.Text = "0,0";
            textBox7.Text = "0,0";

            textBox12.Text = "0,0";
            textBox11.Text = "1,0";
            textBox10.Text = "0,0";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bilinearSurface = new BilinearSurface(new Point(0, 0, 1), new Point(1, 1, 1), new Point(1, 0, 0), new Point(0, 1, 0));

            bilinearPoints.Clear();

            axesPoints.Clear();

            axesPoints = new List<Point>() { new Point(0, 0, 0), new Point(0, 0, 1), new Point(0, 1, 0), new Point(1, 0, 0) };

            bilinearPoints = bilinearSurface.GetPoints(0.1f);

            textBox1.Text = "0,0";
            textBox2.Text = "0,0";
            textBox3.Text = "1,0";

            textBox6.Text = "1,0";
            textBox5.Text = "1,0";
            textBox4.Text = "1,0";

            textBox9.Text = "1,0";
            textBox8.Text = "0,0";
            textBox7.Text = "0,0";

            textBox12.Text = "0,0";
            textBox11.Text = "1,0";
            textBox10.Text = "0,0";

            pictureBox1.Invalidate();
        }
    }
}