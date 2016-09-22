using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dijkestra_Tiled_Graph_Visualizer
{
    class Dijkestra
    {
        static void showRoute(int start, int dest, int[] route)
        {            
            //Console.WriteLine("The Route:");
            int index = dest;

            while (true)
            {
                //Console.Write(index + " ");
                index = route[index];

                if (index == start)
                {
               //     Console.Write((graphVertex)index + " ");
                    break;
                }

            }
        }
        public static int[] dijkestra(int start, int n, float[,] weights) //LinkedList<Edge>
        {
            int i, vnear = start;
            // Edge e;
            int[] touch = new int[n];
            float[] length = new float[n];

            //  LinkedList<Edge> edgesOfGraph = new LinkedList<Edge>();          

            for (i = 0; i < n; i++)
            {
                touch[i] = start;
                length[i] = weights[start, i];
            }

            for (int j = 0; j < n; j++)
            {
                float min = float.PositiveInfinity;
                for (i = 0; i <= n - 1; i++)
                    if (length[i] >= 0 && length[i] < min)
                    {
                        min = length[i];
                        vnear = i;
                    }
                //  e = new Edge(touch[vnear], vnear);

                //  edgesOfGraph.AddLast(e);


                for (i = 0; i <= n - 1; i++)
                {
                    if (length[vnear] + weights[vnear, i] < length[i])
                    {
                        length[i] = length[vnear] + weights[vnear, i];
                        touch[i] = vnear;
                    }
                }



                length[vnear] = -1;
            }
            Console.WriteLine("this: {0}", length[1]);

            //return edgesOfGraph;
            touch[start] = start;
            return touch;
        }
        
        public static LinkedList<TreeEdge> LinkedListDijkestra(TreeNode start,TreeNode dest,LinkedList<TreeEdge> graphEdges,LinkedList<TreeNode> graphNodes) //LinkedList<Edge>
        {

           // TreeNode vnear = start;
            TreeNode[] nodes = graphNodes.ToArray<TreeNode>();
            float[] length = new float[graphNodes.Count];                        
            TreeNode [] touch = new TreeNode[graphNodes.Count];
            int[] touchIndices = new int[graphNodes.Count];

            TreeEdge[] edges = graphEdges.ToArray<TreeEdge>();

            int i,vnear = 0;
            int startIndex = 0;
            int destIndex = 0;

            for (i = 0; i < nodes.Length; i++)
            {
                touch[i] = start;
                length[i] = edges[i].Cost;
                if (nodes[i].GetID == start.GetID)
                    startIndex = i;
                if (nodes[i].GetID == dest.GetID)
                    destIndex = i;
            }
            for (i = 0; i < nodes.Length; i++)
                touchIndices[i] = startIndex;
           

            for (int j = 0; j < nodes.Length; j++)
            {
                float min = float.PositiveInfinity;
                for (i = 0; i < nodes.Length; i++)
                    if (length[i] >= 0 && length[i] < min)
                    {
                        min = length[i];
                        vnear = i;
                    }
                //  e = new Edge(touch[vnear], vnear);

                //  edgesOfGraph.AddLast(e);


                for (i = 0; i < nodes.Length ; i++)
                {
                    float weight = 0;
                    foreach (TreeEdge e in nodes[i].NeighbourEdges)
                    {
                        if (e.Destination.GetID == nodes[vnear].GetID)
                        {
                            weight = e.Cost;
                            break;
                        }
                    }
                    if (length[vnear] + weight < length[i])
                    {
                        length[i] = length[vnear] + weight;
                        touch[i] = nodes[vnear];
                        touchIndices[i] = vnear;
                    }
                }



                length[vnear] = -1;
            }

            touch[startIndex] = start;
            touchIndices[startIndex] = startIndex;
            
           
            
            // Create route comrised of edges:

            int index = destIndex;

            LinkedList<TreeEdge> Path = new LinkedList<TreeEdge>();

            while (true)
            {
                   
                //Console.Write(index + " ")                

                TreeEdge edge = null;
                foreach (TreeEdge e in nodes[touchIndices[index]].NeighbourEdges)
                {
                    if (e.Destination.GetID == nodes[index].GetID)
                    {
                        edge = e;
                        break;
                    }
                }
                Path.AddFirst(edge);

                index = touchIndices[index];

                if (index == startIndex)
                {
                    //     Console.Write((graphVertex)index + " ");
                    break;
                }

                

            }

            TreeEdge edgeTmp = null;
            foreach (TreeEdge e in nodes[index].NeighbourEdges)
            {
                if (e.Destination.GetID == nodes[touchIndices[index]].GetID)
                {
                    edgeTmp = e;
                    break;
                }
            }

            Path.AddFirst(edgeTmp);

            return Path;
            /*
            int i, vnear = start;
            // Edge e;
            int[] touch = new int[n];
            float[] length = new float[n];

            //  LinkedList<Edge> edgesOfGraph = new LinkedList<Edge>();          

            for (i = 0; i <= n - 1; i++)
            {
                touch[i] = start;
                length[i] = weights[start, i];
            }

            for (int j = 0; j < n; j++)
            {
                float min = float.PositiveInfinity;
                for (i = 0; i <= n - 1; i++)
                    if (length[i] >= 0 && length[i] < min)
                    {
                        min = length[i];
                        vnear = i;
                    }
                //  e = new Edge(touch[vnear], vnear);

                //  edgesOfGraph.AddLast(e);


                for (i = 0; i <= n - 1; i++)
                {
                    if (length[vnear] + weights[vnear, i] < length[i])
                    {
                        length[i] = length[vnear] + weights[vnear, i];
                        touch[i] = vnear;
                    }
                }



                length[vnear] = -1;
            }
            Console.WriteLine("this: {0}", length[1]);

            //return edgesOfGraph;
            touch[start] = start;
            return touch;
             */ 
            
        }            
    
    }
}
