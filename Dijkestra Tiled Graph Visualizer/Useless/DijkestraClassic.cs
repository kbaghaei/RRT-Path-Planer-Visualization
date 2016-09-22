/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dijkestra_classic
{
    class Program
    {
        enum graphVertex { T, O, A, B, C, D, E, F }

        static void Main(string[] args)
        {            

            float I =  float.PositiveInfinity;
            float[,] weights = new float[8, 8]
            {{I,I,I,I,I,5,7,3},
            {I,I,2,5,4,I,I,I},
            {I,2,I,2,I,7,I,12},
            {I,5,2,I,1,4,3,I},
            {I,4,I,1,I,I,4,I},
            {5,I,7,4,I,I,1,I},
            {7,I,I,3,4,1,I,I},
            {3,I,12,I,I,I,I,I}};

          //  LinkedList<Edge> dijk = dijkestra(8, weights);

         //   foreach(Edge e in dijk)
            int[] route = dijkestra((int)graphVertex.F,8, weights);
            foreach(int a in route)
            {
                //Console.WriteLine("{0}{1}", ((graphVertex)e.startIndex).ToString(), ((graphVertex)e.destIndex).ToString());
                Console.WriteLine((graphVertex)a);
            }
            showRoute((int)graphVertex.F,(int)graphVertex.O, route);
            Console.ReadKey(true);
        }
        static void showRoute(int start,int dest,int [] route)
        {

            Console.WriteLine("The Route:");
            int index = dest;

            while(true)
            {
                Console.Write((graphVertex)index+ " ");
                index = route[index];
                
                if (index == start)
                {
                    Console.Write((graphVertex)index + " ");
                    break;
                }
                 
            }
        }
        static int[] dijkestra(int start,int n, float[,] weights) //LinkedList<Edge>
        {
            int i, vnear = start;
           // Edge e;
            int [] touch = new int[n];
            float[] length = new float[n];

          //  LinkedList<Edge> edgesOfGraph = new LinkedList<Edge>();          

            for (i = 0; i <= n-1; i++)
            {
                touch[i] = start;
                length[i] = weights[start, i];
            }

            for (int j = 0; j < n; j++)
            {
                float min =  float.PositiveInfinity;
                for (i = 0; i <= n-1; i++)
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
            Console.WriteLine("this: {0}",length[1]);

            //return edgesOfGraph;
            touch[start] = start;
            return touch;
        }
    }
    class Edge
    {
        public int startIndex;
        public int destIndex;
        public int weight;

        public Edge(int startVertex, int destVertex, int _weight)
        {
            startIndex = startVertex;
            destIndex = destVertex;
            weight = _weight;
        }
        public Edge(int startVertex, int destVertex)
        {
            startIndex = startVertex;
            destIndex = destVertex;
            weight = 0;
        }
        public override string ToString()
        {
            return string.Format("<{0},{1}>", startIndex, destIndex);
        }
    }

}
*/