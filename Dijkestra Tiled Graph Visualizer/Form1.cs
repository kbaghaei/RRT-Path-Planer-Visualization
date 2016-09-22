using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Dijkestra_Tiled_Graph_Visualizer
{
    public partial class Form1 : Form
    {
        static float ZERO = 1E-08f;
        static float INF = float.PositiveInfinity;
        static float edgeOfSquare = 20;
        public static Random RandomGen = new Random();
        int[, ,] pixelData;
        bool [,] validLocations;

        public Form1()
        {
            InitializeComponent();
        }
        public static void showMessage(string str)
        {
            MessageBox.Show(str);
        }
        private void btnDrawPath_Click(object sender, EventArgs e)
        {            
            
            Graphics g = this.CreateGraphics();
            Pen myPen = new Pen(Brushes.Cyan, 2);

            RRT rrt1 = new RRT(5,validLocations,600,800,primaryPoint,secondaryPoint);
            RRT rrt2 = new RRT(5,validLocations,600,800,secondaryPoint,primaryPoint);

            //Stopwatch timer = new Stopwatch();
            DateTime dt = DateTime.Now;
            //timer.Start();

            bool merged = rrt1.MergeWith(rrt2,100);

            ///timer.Stop();

            MessageBox.Show(string.Format("merged: {0} time: {1}",merged,DateTime.Now.Subtract(dt).TotalMilliseconds));//timer.Elapsed.TotalMilliseconds));

            foreach (TreeEdge te in rrt1.Edges)
            {
                g.DrawLine(myPen, te.Source.Position, te.Destination.Position);
            }

            foreach (TreeNode tn in rrt1.Nodes)
            {
                DrawCrossHair(g, tn.Position, 4, Pens.Green);
            }
            
            MessageBox.Show(string.Format("{0} {1}",rrt1.Edges.Count,rrt1.Nodes.Count));
            myPen = new Pen(Brushes.Orange, 2);

            myPen.Color = Color.Red;
            DrawCrossHair(g, primaryPoint, 10, myPen);

            myPen.Color = Color.Yellow;
            DrawCrossHair(g, secondaryPoint, 10, myPen);



            if (!merged)
            {
                foreach (TreeEdge te in rrt2.Edges)
                {
                    g.DrawLine(myPen, te.Source.Position, te.Destination.Position);
                }

                foreach (TreeNode tn in rrt2.Nodes)
                {
                    DrawCrossHair(g, tn.Position, 4, Pens.Violet);
                }
            }

       //     LinkedList<TreeEdge> path = rrt1.CalcAndGetPath();

         //   foreach (TreeEdge te in path)
           // {
             //   g.DrawLine(myPen, te.Source.Position, te.Destination.Position);
           // }
            /*
            int primaryIndex = getIndexOfPoint(primaryPoint, PointsArray);

            routePoints = Dijkestra.dijkestra(primaryIndex, PointsArray.Length, AdjacencyMatrix);


            int destIndex = getIndexOfPoint(secondaryPoint,PointsArray);
            int startIndex = getIndexOfPoint(primaryPoint, PointsArray);
    
            Pen penTmp = new Pen(Brushes.Red,4);
            DrawCrossHair(g, PointsArray[destIndex], 10, penTmp);

            penTmp.Color = Color.Yellow;
            DrawCrossHair(g, PointsArray[startIndex], 10, penTmp);


            int routePointsCount = 0;

            int index = destIndex;
            while (true)
            {
                g.DrawLine(myPen, PointsArray[index], PointsArray[routePoints[index]]);
                index = routePoints[index];

                

                if (index == startIndex)
                {
                    g.DrawLine(myPen, PointsArray[index], PointsArray[routePoints[index]]);
                    break;
                }
                routePointsCount++;
            }

            */
            //MessageBox.Show(routePointsCount.ToString());
        }
        #region Graph Provide

        // LinkedList<PointF> Points;
        private PointF primaryPoint;
        private PointF secondaryPoint;
        private PointF [] PointsArray;
    //    private int[] routePoints;
    //    private float[,] AdjacencyMatrix;

        private static int primary_secondary = 0;

        private string imageFilePath;
     //   private float Edge_of_Square = edgeOfSquare;

        /*
        private void btnGenGraph_Click(object sender, EventArgs e)
        {
            
            Image image = Image.FromFile(imageFilePath);

            Bitmap bitmap = new Bitmap(image);

            int imgHeight = image.Height;
            int imgWidth = image.Width;

           // this.BackgroundImage = image;
            this.Width = imgWidth;
            this.Height = imgHeight;

            int[, ,] pixelData = new int[imgWidth, imgHeight, 3];

            for (int yi = 0; yi < imgHeight; yi++)
                for (int xi = 0; xi < imgWidth; xi++)
                {
                    Color pixel = bitmap.GetPixel(xi, yi);
                    pixelData[xi, yi, 0] = pixel.R;
                    pixelData[xi, yi, 1] = pixel.G;
                    pixelData[xi, yi, 2] = pixel.B;
                }

            bitmap.Dispose();

            Points = new LinkedList<PointF>();

            Points.AddFirst(primaryPoint);

            Queue<PointF> PointsQueue = new Queue<PointF>();

            PointsQueue.Enqueue(primaryPoint);

            while (PointsQueue.Count != 0)
            {
                PointF tmp = PointsQueue.Dequeue();
                PointF Neighbour = new PointF();
                for (int i = 0; i < 8; i++)
                {
                    if (!DoesNeighbourExist(i, tmp, Points, Edge_of_Square, ref Neighbour,this.Width, this.Height, pixelData))
                    {
                        Points.AddLast(Neighbour);
                        PointsQueue.Enqueue(Neighbour);

                    }
                }
            }

            AdjacencyMatrix = new float[Points.Count,Points.Count];
           

            float[,] pointsCoordinates = new float[Points.Count, 2];

            PointsArray = Points.ToArray();

            for(int i = 0 ; i < PointsArray.Length; i++)
            {
                pointsCoordinates[i, 0] = PointsArray[i].X;
                pointsCoordinates[i, 1] = PointsArray[i].Y; 
            }

            float SquaredOfEdgeOfSquare = Edge_of_Square * Edge_of_Square;

            for (int i = 0; i < PointsArray.Length; i++)
            {
                for (int j = 0; j < PointsArray.Length; j++)
                {
                    if (i != j)
                    {
                        float dx = PointsArray[i].X - PointsArray[j].X;
                        float dy = PointsArray[i].Y - PointsArray[j].Y;

                        float distanceSquare = dx * dx + dy * dy;

                        // Within an 'edge' radius: (horizontol and vartical)
                        // Or whitin an 'edge * sqrt(2) radius': (for diameters)
                        if (Math.Abs(distanceSquare - SquaredOfEdgeOfSquare) <= ZERO)
                        {
                            AdjacencyMatrix[j, i] = AdjacencyMatrix[i, j] = 1;
                        }
                        else if (Math.Abs(distanceSquare - 2 * SquaredOfEdgeOfSquare) <= ZERO)
                        {
                            AdjacencyMatrix[j, i] = AdjacencyMatrix[i, j] = 1.41421356f;
                        }
                        else
                        {
                            AdjacencyMatrix[j, i] = AdjacencyMatrix[i, j] = INF;
                        }

                    }
                    else
                    {
                        AdjacencyMatrix[j, i] = AdjacencyMatrix[i, j] = INF;
                    }
                }
            }
            
            Graphics g = this.CreateGraphics();

            for (int i = 0; i < PointsArray.Length; i++)
            {
                DrawCrossHair(g, PointsArray[i], 3,Pens.Black);
                for (int j = 0; j < PointsArray.Length; j++)
                {
                    if (i != j && AdjacencyMatrix[i, j] != 0 && !float.IsInfinity(AdjacencyMatrix[i,j]))
                    {
                        g.DrawLine(Pens.DarkBlue, PointsArray[i], PointsArray[j]);
                    }
                }
            }
            g.Dispose();

        }
        */


        private static void DrawCrossHair(Graphics g, PointF pf, float raduis,Pen pen)
        {
            g.DrawEllipse(pen, pf.X - raduis, pf.Y - raduis, raduis * 2, raduis * 2);
            g.DrawLine(pen, new PointF(pf.X - raduis - 3, pf.Y), new PointF(pf.X + raduis + 3, pf.Y));
            g.DrawLine(pen, new PointF(pf.X, pf.Y - raduis - 3), new PointF(pf.X, pf.Y + raduis + 3));
        }

        /*
        private static bool DoesNeighbourExist(int i, PointF refPoint, LinkedList<PointF> points, float EdgeOfSquare, ref PointF Neighbour, int maxX, int maxY, int[, ,] pixelsData)
        {
            PointF changesInPoint = new PointF(0, 0);
            PointF obstacleAvoidanceOffset = new PointF(0, 0);

            switch (i)
            {
                case 0:
                    {
                        changesInPoint.X = 0;
                        changesInPoint.Y = EdgeOfSquare;
                    }
                    break;
                case 1:
                    {
                        changesInPoint.X = EdgeOfSquare;
                        changesInPoint.Y = EdgeOfSquare;

                    }
                    break;
                case 2:
                    {
                        changesInPoint.X = EdgeOfSquare;
                        changesInPoint.Y = 0;

                    }
                    break;
                case 3:
                    {
                        changesInPoint.X = EdgeOfSquare;
                        changesInPoint.Y = -EdgeOfSquare;

                    }
                    break;
                case 4:
                    {
                        changesInPoint.X = 0;
                        changesInPoint.Y = -EdgeOfSquare;
                    }
                    break;
                case 5:
                    {
                        changesInPoint.X = -EdgeOfSquare;
                        changesInPoint.Y = -EdgeOfSquare;
                    }
                    break;
                case 6:
                    {
                        changesInPoint.X = -EdgeOfSquare;
                        changesInPoint.Y = 0;
                    }
                    break;
                case 7:
                    {
                        changesInPoint.X = -EdgeOfSquare;
                        changesInPoint.Y = EdgeOfSquare;
                    }
                    break;
            }

            foreach (PointF pf in points)
            {
                if (Math.Abs(changesInPoint.X + refPoint.X - pf.X) <= ZERO && Math.Abs(changesInPoint.Y + refPoint.Y - pf.Y) <= ZERO)
                    return true;
            }

            Neighbour = new PointF(refPoint.X + changesInPoint.X, refPoint.Y + changesInPoint.Y);

            if (!isPointValid(Neighbour, maxX, maxY, pixelsData))
            {
                return true; // implying: no neighbours can be placed there!
            }
            return false;
        }
        */
        
        
        private const int EdgeOffset = 20;

        private static bool isPointValid(PointF pf, float MaxX, float MaxY, int[, ,] pixelData)
        {
            if ((int)pf.X < 0 || (int)pf.Y < 0 || (int)pf.Y >= MaxY || (int)pf.X >= MaxX)
                return false;
            if (pixelData[(int)pf.X, (int)pf.Y, 0] == 255 && pixelData[(int)pf.X, (int)pf.Y, 1] == 255 && pixelData[(int)pf.X, (int)pf.Y, 2] == 255)
            {
                if (pf.X <= MaxX - EdgeOffset && pf.X >= EdgeOffset && pf.Y <= MaxY - EdgeOffset && pf.Y >= EdgeOffset)
                    return true;
            }
            return false;

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            float raduis = 4;
            Graphics g = this.CreateGraphics();
            if (primary_secondary == 0)
            {
                primaryPoint.X = e.X;
                primaryPoint.Y = e.Y;

                DrawCrossHair(g, primaryPoint, raduis, Pens.Red);

                primary_secondary++;
                MessageBox.Show("here! 0");
            }
            else
            {

                secondaryPoint.X = e.X;
                secondaryPoint.Y = e.Y;
                primary_secondary = 0;

                DrawCrossHair(g, secondaryPoint, raduis, Pens.Red);

                MessageBox.Show("here! 1");
            }


           
            g.Dispose();
        }
        
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            imageFilePath = "e:\\vstemp\\graph.bmp";
            this.BackgroundImage = Image.FromFile(imageFilePath);

            Image img = Image.FromFile(imageFilePath);
            Bitmap bitmap = new Bitmap(img);

            int imgHeight = img.Height;
            int imgWidth = img.Width;

            validLocations = new bool[imgWidth, imgHeight];
            pixelData = new int[imgWidth, imgHeight, 3];

            for (int yi = 0; yi < imgHeight; yi++)
                for (int xi = 0; xi < imgWidth; xi++)
                {
                    Color pixel = bitmap.GetPixel(xi, yi);
                    pixelData[xi, yi, 0] = pixel.R;
                    pixelData[xi, yi, 1] = pixel.G;
                    pixelData[xi, yi, 2] = pixel.B;

                    if (pixel.R + pixel.B + pixel.G == 0)
                    {
                        validLocations[xi, yi] = false;
                    }
                    else
                    {
                        validLocations[xi, yi] = true;
                    }
                }

            bitmap.Dispose();           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();

            g.Clear(Color.White);

            this.BackgroundImage = Image.FromFile(imageFilePath);
        }


        
        private void btnGenGraph_Click(object sender, EventArgs e)
        {
            

        }
    }
}
