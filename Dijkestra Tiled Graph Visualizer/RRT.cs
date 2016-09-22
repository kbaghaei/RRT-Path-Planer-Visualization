using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Dijkestra_Tiled_Graph_Visualizer
{
    class RRT
    {
        bool[,] validLocations;

        LinkedList<TreeNode> treeNodes;
        LinkedList<TreeEdge> treeEdges;

        public LinkedList<TreeNode> Nodes
        {
            get { return treeNodes; }
        }
        public LinkedList<TreeEdge> Edges
        {
            get { return treeEdges; }
        }

        TreeNode root;

        TreeNode dest;

        int maxWidth;
        int maxHeight;

        string debugOutput = "";
        public RRT(int primaryNodeCount,bool [,] validLocationPixels,int maxH,int maxW,PointF rootPos,PointF dest)
        {
            
            root = new TreeNode(rootPos);
            this.dest = new TreeNode(dest);

            maxWidth = maxW;
            maxHeight = maxH;

            treeEdges = new LinkedList<TreeEdge>();
            treeNodes = new LinkedList<TreeNode>();            
            treeNodes.AddFirst(root);
            validLocations = validLocationPixels;

            float step = 10;
            float angle = 0;
            for (int i = 0; i < primaryNodeCount; i++)
            {
                float x = rootPos.X + (float)Math.Cos(angle) * 50;
                float y = rootPos.Y + (float)Math.Sin(angle) * 50;
                
                //ExtendTree(new TreeNode(new PointF(x,y)), step);
                AddLeaf(root, new TreeNode(new PointF(x, y)), step);
                angle += (float)(Math.PI * 2) / primaryNodeCount;
                debugOutput += string.Format("ang: {0} sin: {1}\n", (angle/Math.PI) * 180,(float)Math.Cos(angle));
            }

         //   Form1.showMessage(debugOutput);
        }
        public void AddLeaf(TreeNode node,TreeNode leaf,float stepSize)
        {
            float dx = leaf.Position.X - node.Position.X;
            float dy = leaf.Position.Y - node.Position.Y;

            float incline = Math.Abs(dy / dx);
            float Xstep = stepSize * Math.Sign(dx);
            float Ystep = stepSize * incline * Math.Sign(dy);

            int steps = (int)(Math.Abs(dx / stepSize));
            PointF dummyPoint = new PointF(node.Position.X, node.Position.Y);
            PointF lastDummyPoint = new PointF(-1, -1);

            //bool found_a_valid_point = false;
            for (int i = 0; i < steps; i++)
            {
                dummyPoint.X += Xstep;
                dummyPoint.Y += Ystep;

                if (PointIsValid(dummyPoint, 4))
                {
                    //found_a_valid_point = true;
                    lastDummyPoint = dummyPoint;
                }
                else
                {                    
                    break;
                }
            }

            if (lastDummyPoint.X < 0)
            {                
                return;
            }

            TreeNode newNode = new TreeNode(lastDummyPoint);

            // create bidirectional connection:
            TreeEdge edge1 = node.AddNeighbour(newNode);
            TreeEdge edge2 = newNode.AddNeighbour(node);

            treeEdges.AddFirst(edge1);
            treeEdges.AddFirst(edge2);

            // Add new node to tree:
            treeNodes.AddFirst(newNode);

            debugOutput = string.Format("{0} \n {1} \n", debugOutput,new PointF(newNode.Position.X - root.Position.X,newNode.Position.Y - root.Position.Y).ToString());

        }
        public TreeNode ExtendTree(TreeNode extTreeNode,int StepsNum)
        {
            TreeNode nearest = null;
            float minDist = float.PositiveInfinity;
            foreach (TreeNode tn in treeNodes)
            {
                float tmp = DistanceOf2PointsSquared(tn.Position,extTreeNode.Position);
                if (tmp <= minDist)
                {
                    minDist = tmp;
                    nearest = tn;
                }
            }
            float dx = extTreeNode.Position.X - nearest.Position.X;
            float dy = extTreeNode.Position.Y - nearest.Position.Y;

            if (dx * dx + dy * dy > 10000)
            {
                float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                dx = (dx / dist) * 100;
                dy = (dy / dist) * 100;
            }
            


            if (Math.Abs(dx) < 10 && Math.Abs(dy) < 10)
                StepsNum = 1;
           
            float Xstep = dx / StepsNum;
            float Ystep = dy / StepsNum;
            
            PointF dummyPoint = new PointF(nearest.Position.X,nearest.Position.Y);
            PointF lastDummyPoint = new PointF(-1,-1);

            //bool found_a_valid_point = false;
            for (int i = 0; i < StepsNum; i++)
            {                
                dummyPoint.X += Xstep;
                dummyPoint.Y += Ystep;

                if (PointIsValid(dummyPoint, 4))
                {
                    //found_a_valid_point = true;
                    lastDummyPoint = dummyPoint;
                }
                else
                {
                    break;
                }
               // if (DistanceOf2PointsSquared(lastDummyPoint, extTreeNode.Position) > 2500)
                 //   break;
            }

            if (lastDummyPoint.X < 0)
                return null;

            TreeNode newNode = new TreeNode(lastDummyPoint);

            // create bidirectional connection:
            TreeEdge edge1 = nearest.AddNeighbour(newNode);
            TreeEdge edge2 = newNode.AddNeighbour(nearest);

            treeEdges.AddFirst(edge1);
            treeEdges.AddFirst(edge2);

            // Add new node to tree:
            treeNodes.AddFirst(newNode);

            return newNode;
        }

        public TreeNode ExtendTreeRand(TreeNode extNode,int StepsNum)
        {
            TreeNode nearest = null;
            float minDist = float.PositiveInfinity;
            foreach (TreeNode tn in treeNodes)
            {
                float tmp = DistanceOf2PointsSquared(tn.Position,extNode.Position);
                if (tmp <= minDist)
                {
                    minDist = tmp;
                    nearest = tn;
                }
            }

            ///PointF randPoint = TreeNode.RandomConfigPiont(100, 100, 0.5f);


            float dx = extNode.Position.X - nearest.Position.X;
            float dy = extNode.Position.Y - nearest.Position.Y;

            if (dx * dx + dy * dy > 10000)
            {
                float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                dx = (dx / dist) * 100;
                dy = (dy / dist) * 100;
            }
            

            if (Math.Abs(dx) < 10 && Math.Abs(dy) < 10)
                StepsNum = 1;

            float Xstep = dx / StepsNum;
            float Ystep = dy / StepsNum;

            PointF dummyPoint = new PointF(nearest.Position.X,nearest.Position.Y);
            PointF lastDummyPoint = new PointF(-1,-1);

            //bool found_a_valid_point = false;
            for (int i = 0; i < StepsNum; i++)
            {                
                dummyPoint.X += Xstep;
                dummyPoint.Y += Ystep;

                if (PointIsValid(dummyPoint, 4))
                {
                    //found_a_valid_point = true;
                    lastDummyPoint = dummyPoint;
                }
                else
                {
                    break;
                }
                if (DistanceOf2PointsSquared(lastDummyPoint, nearest.Position) > 2500)
                    break;
            }

            if (lastDummyPoint.X < 0)
                return null;

            TreeNode newNode = new TreeNode(lastDummyPoint);

            // create bidirectional connection:
            TreeEdge edge1 = nearest.AddNeighbour(newNode);
            TreeEdge edge2 = newNode.AddNeighbour(nearest);

            treeEdges.AddFirst(edge1);
            treeEdges.AddFirst(edge2);

            // Add new node to tree:
            treeNodes.AddFirst(newNode);

            return newNode;
        }
        
        
        public static float DistanceOf2PointsSquared(PointF p1,PointF p2)
        {
            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;
            return dx * dx + dy * dy;
        }

        private bool PointIsValid(PointF pf,float raduis)
        {
            float continousX = pf.X;
            float continousY = pf.Y;

            int x = (int)continousX;
            int y = (int)continousY;

            if (x < maxWidth && x >= 0 && y >= 0 && y < maxHeight)
                if (!validLocations[(int)continousX, (int)continousY])
                    return false;

            for (int i = 0; i < 4; i++)
            {
                continousX = raduis * (float)Math.Cos((Math.PI / 2) * i);
                continousY = raduis * (float)Math.Sin((Math.PI / 2) * i);

                x = (int)continousX;
                y = (int)continousY;
                
                if( x < maxWidth && x >= 0 && y >= 0 && y < maxHeight)
                    if(!validLocations[x,y])
                        return false;
            }
            return true;
        }

        public bool MergeWith(RRT extTree,int numberOfAttempts)
        {
            RRT tree1 = this;
            RRT tree2 = extTree;

            TreeNode newnode1 = tree2.root;

            for (int i = 0; i < numberOfAttempts; i++)
            {
                
                TreeNode rand = new TreeNode(TreeNode.RandomConfigPiont(maxHeight,maxWidth,1));
                newnode1 = tree1.ExtendTreeRand(rand,10);

                if(newnode1 != null)
                {
                    TreeNode newnode2 = tree2.ExtendTree(newnode1, 10);
                    if (newnode2 != null)
                    {
                        if (TreeNode.Overlap(newnode1,newnode2))
                        {
                            foreach (TreeEdge edge in extTree.treeEdges)
                            {
                                this.treeEdges.AddFirst(edge);
                            }
                            foreach (TreeNode tn in extTree.treeNodes)
                            {
                                this.treeNodes.AddFirst(tn);
                            }
                            Form1.showMessage("Merged");
                            return true;
                        }                        
                    }
                    RRT tmp = tree1;
                    tree1 = tree2;
                    tree2 = tmp;
                }
            }
            Form1.showMessage("Failed");
            return false;
        }
        private LinkedList<TreeEdge> Path;

        public LinkedList<TreeEdge> CalcAndGetPath()
        {
            Path = Dijkestra.LinkedListDijkestra(root, dest, treeEdges, treeNodes);
            return Path;
        }
    }
    #region TreeNode Class
    class TreeNode
    {
        static int nodeIndexProvider = 0;
        
        int index;
        PointF position;
        LinkedList<TreeEdge> adjacentEdges;

        public LinkedList<TreeEdge> NeighbourEdges
        {
            get { return adjacentEdges; }
        }

        public PointF Position
        {
            get { return position; }
        }
        public int GetID
        {
            get { return index; }
        }
        public TreeNode(PointF pos)
        {
            position = pos;
            index = nodeIndexProvider++;
            adjacentEdges = new LinkedList<TreeEdge>();
        }

        public TreeEdge AddNeighbour(TreeNode neighbour)
        {
            foreach(TreeEdge node in adjacentEdges)
            {
                if(node.Destination.GetID == this.index)
                    return null;
            }
            TreeEdge edge = new TreeEdge(this, neighbour, RRT.DistanceOf2PointsSquared(this.position, neighbour.position));
            adjacentEdges.AddFirst(edge);
            return edge;
        }
        public static PointF RandomConfigPiont(float maxH,float maxW,float offset)
        {
            float h = maxH *(offset - (float)Form1.RandomGen.NextDouble());
            float w = maxW *(offset - (float)Form1.RandomGen.NextDouble());

            return new PointF(h, w);
        }
        public static bool Overlap(TreeNode nd1,TreeNode nd2)
        {
            if(RRT.DistanceOf2PointsSquared(nd1.Position,nd2.Position) <= 4)
                return true;
            return false;
        }

    }
    #endregion
    #region TreeEdge Class

    class TreeEdge
    {
        static int idIndexer = 0;

        int index;

        float cost;

        public float Cost
        {
            get { return cost; }
        }

        TreeNode nodeSource;
        TreeNode nodeDestination;

        public TreeNode Source
        {
            get { return nodeSource; }
        }
        public TreeNode Destination
        {
            get { return nodeDestination; }
        }
        public TreeEdge(TreeNode ndSrc, TreeNode ndDst,float cost)
        {
            index = idIndexer++;
            nodeDestination = ndDst;
            nodeSource = ndSrc;
            this.cost = cost;
        }
        
    }
#endregion
}
