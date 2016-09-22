/*

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GraphProvider
{
    public partial class Form1 : Form
    {
        static float ZERO = 1E-08f;

        public Form1()
        {
            InitializeComponent();
        }

        private void openImageFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void saveGraphFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();                      
        }

        LinkedList<PointF> Points;
        private PointF primaryPoint;
        private string imageFilePath;
        private float Edge_of_Square = 40;

        private void createGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare pixel data:
   
            imageFilePath = openFileDialog1.FileName;
          

            Image image = Image.FromFile(imageFilePath);
            Bitmap bitmap = new Bitmap(image);
         
            int imgHeight = image.Height;
            int imgWidth = image.Width;

            this.BackgroundImage = image;
            this.Width = imgWidth;
            this.Height = imgHeight;

            int[, ,] pixelData = new int[imgWidth, imgHeight, 3];

            for(int yi = 0; yi < imgHeight ; yi++)
                for (int xi = 0; xi < imgWidth; xi++)
                {
                    Color pixel = bitmap.GetPixel(xi, yi);
                    pixelData[xi, yi, 0] = pixel.R;
                    pixelData[xi, yi, 1] = pixel.G;
                    pixelData[xi, yi, 2] = pixel.B;
                }

            bitmap.Dispose();
           // image.Dispose();

            //  Create Points for Graph:

            Points = new LinkedList<PointF>();

            Points.AddFirst(primaryPoint);

            Queue<PointF> PointsQueue= new Queue<PointF>();

            PointsQueue.Enqueue(primaryPoint);

            while (PointsQueue.Count != 0)
            {
                PointF tmp = PointsQueue.Dequeue();
                PointF Neighbour = new PointF();
                for (int i = 0; i < 8; i++)
                {
                    if (!DoesNeighbourExist(i, tmp, Points, Edge_of_Square, ref Neighbour, imgWidth, imgHeight, pixelData))
                    {
                        Points.AddLast(Neighbour);
                        PointsQueue.Enqueue(Neighbour);
                        //if (isPointValid(Neighbour,imgWidth,imgHeight,pixelData))
                        //{

                        //}
                    }
                }
            }

            MessageBox.Show(Points.Count.ToString());

            /*

            Graphics g = this.CreateGraphics();


            string str = "";
            int m = 0;            
            foreach(PointF pf in Points)
            {
                DrawCrossHair(g,pf,6);
                str += string.Format("({0},{1}) ", pf.X, pf.Y);
                if (m >= 4)
                {
                    str += "\n";
                    m = 0;
                }
                m++;
            }
            MessageBox.Show(str); */
       // }
/*
        private static void DrawCrossHair(Graphics g,PointF pf,float raduis)
        {
            g.DrawEllipse(Pens.Red, pf.X - raduis, pf.Y - raduis, raduis * 2, raduis * 2);
            g.DrawLine(Pens.Red, new PointF(pf.X - raduis - 3, pf.Y), new PointF(pf.X + raduis + 3, pf.Y));
            g.DrawLine(Pens.Red, new PointF(pf.X, pf.Y - raduis - 3), new PointF(pf.X, pf.Y + raduis + 3));
        }
        private static bool DoesNeighbourExist(int i,PointF refPoint, LinkedList<PointF> points,float EdgeOfSquare,ref PointF Neighbour,int maxX,int maxY,int [,,] pixelsData)
        {
            PointF changesInPoint = new PointF(0,0);
            PointF obstacleAvoidanceOffset = new PointF(0,0);

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


            /*
            for (int m = 1; m < 4; m++)
            {
                obstacleAvoidanceOffset.X = changesInPoint.X * (1 + m * 0.2f);
                obstacleAvoidanceOffset.Y = changesInPoint.Y * (1 + m * 0.2f);

                PointF obstacleDetectionWhisker = new PointF(obstacleAvoidanceOffset.X + refPoint.X, obstacleAvoidanceOffset.Y + refPoint.Y);

                if (!isPointValid(obstacleDetectionWhisker, maxX, maxY, pixelsData))
                {
                    return true; // implying: no neighbours can be placed there!
                }
            }
            */
 /*           
            Neighbour = new PointF(refPoint.X + changesInPoint.X, refPoint.Y + changesInPoint.Y);

            if (!isPointValid(Neighbour, maxX, maxY, pixelsData))
            {
                return true; // implying: no neighbours can be placed there!
            }
            return false;
        }

        private const int EdgeOffset = 20;

        private static bool isPointValid(PointF pf,float MaxX,float MaxY,int [,,] pixelData)
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
        protected override void  OnMouseClick(MouseEventArgs e)
        {
            float raduis = 4;

            Graphics g = this.CreateGraphics();

            primaryPoint.X = e.X;
            primaryPoint.Y = e.Y;

            DrawCrossHair(g, primaryPoint, raduis);

            g.Dispose();
 	
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            imageFilePath = openFileDialog1.FileName;
            Image image = Image.FromFile(imageFilePath);            
            this.BackgroundImage = image;
        }

        private string graphFilePath;

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            graphFilePath = saveFileDialog1.FileName;
            int [,] adjacencyMatrix = new int[Points.Count,Points.Count];
            float[,] pointsCoordinates = new float[Points.Count, 2];

            PointF [] PointsArray = Points.ToArray();

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
                        if (Math.Abs(distanceSquare - SquaredOfEdgeOfSquare) <= ZERO || Math.Abs(distanceSquare - 2 * SquaredOfEdgeOfSquare) <= ZERO)
                        {
                            adjacencyMatrix[j, i] = adjacencyMatrix[i, j] = 1;
                        }
                        else
                        {
                            adjacencyMatrix[j, i] = adjacencyMatrix[i, j] = 0;
                        }

                    }
                    else
                    {
                        adjacencyMatrix[j, i] = adjacencyMatrix[i, j] = 0;
                    }
                }
            }

            StreamWriter sw = new StreamWriter(graphFilePath);

            sw.WriteLine(PointsArray.Length);

            for (int i = 0; i < PointsArray.Length; i++)
            {
                sw.WriteLine("{0} {1} {2}", i, PointsArray[i].X, PointsArray[i].Y);
            }
            for (int i = 0; i < PointsArray.Length; i++)
            {
                for (int j = 0; j < PointsArray.Length; j++)
                {
                    if (j == PointsArray.Length - 1)
                        sw.Write("{0}", adjacencyMatrix[i, j]);
                    else
                        sw.Write("{0} ", adjacencyMatrix[i, j]);
                }
                sw.WriteLine();
            }
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("File Format:");
            sw.WriteLine("-number of graph points:");
            sw.WriteLine("-ID of graph Point <space> X of Point <space> Y of Point");
            sw.WriteLine("-Matrix of adjacency,each row of matrix is on a line");
            sw.WriteLine("-Comments");
            sw.Close();
            MessageBox.Show("File's ready! ;)");

            Graphics g = this.CreateGraphics();

            for (int i = 0; i < PointsArray.Length; i++)
            {
                DrawCrossHair(g, PointsArray[i], 3);
                for (int j = 0; j < PointsArray.Length; j++)
                {
                    if (i != j && adjacencyMatrix[i, j] == 1)
                    {
                        g.DrawLine(Pens.DarkBlue, PointsArray[i], PointsArray[j]);
                    }
                }
            }
            g.Dispose();
        }
    }
}
*/