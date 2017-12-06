using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace TSP_DepthBreadthFirst
{
    class Graph
    {
        public int vertices;
        public static List<List<Node>> adjList = new List<List<Node>>();
        public static List<Node> nodeList = new List<Node>();
        public static Stack<int> nStack = new Stack<int>();
        public static Stack<int> pStack = new Stack<int>();

        public Graph()
        {
            vertices = nodeList.Count();

        }
        public void AddEdge(int v, int e)
        {
            if (v > adjList.Count())
            {
                adjList.Add(new List<Node>());
            }
            adjList[v-1].Add(nodeList[e-1]);
            
        }
        public static void DFS(int v)
        {
            if(v == nodeList.Count - 1)
            {
                List<int> arr = new List<int>();

                Console.Write("Path:");
                while (pStack.Count != 0)
                {
                    arr.Add(pStack.Pop());                    
                }
                for (int m = arr.Count - 1; m >= 0; --m)
                {
                    Console.Write(" " + arr[m]);
                }
                Console.WriteLine();

                nStack.Pop();
                for (int m = arr.Count - 1; m > 0; --m)
                {
                    pStack.Push(arr[m]);
                }
                if (nStack.Any())
                {
                    DFS(nStack.Peek() - 1);
                }                
            }
            else
            {   
                for (int j = 0; j < adjList[v].Count(); ++j)
                {
                    if (nStack.Count() != 0)
                    {
                        if (adjList[v][j].getVisited() == false)
                        {
                            nStack.Push(adjList[v][j].getIndex());
                            pStack.Push(adjList[v][j].getIndex());

                            adjList[v][j].setVisited(true);
                            for (int l = nStack.Peek(); l < nodeList.Count; ++l)
                            {
                                nodeList[l].setVisited(false);
                            }
                            DFS(adjList[v][j].getIndex() - 1);
                        }
                    }
                        
                    //If node directly connects to goal state
                    if(nStack.Count() != 0)
                    {
                        if (adjList[v][j].getIndex() == nodeList.Count() && adjList[v].Count > 1 && adjList[v][j].getVisited() == false)
                        {
                            nStack.Push(adjList[v][j].getIndex());
                            pStack.Push(adjList[v][j].getIndex());
                            DFS(nStack.Peek() - 1);
                        }
                    }
                }
                if (nStack.Any())
                {
                    for (int l = nStack.Peek(); l < nodeList.Count; ++l)
                    {
                        nodeList[l].setVisited(false);
                    }
                }

                if (nStack.Any())
                {
                    nStack.Pop();
                    pStack.Pop();
                    if (nStack.Any())
                    {
                        DFS(nStack.Peek() - 1);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        static void Main(string[] args)
        {
            string path;
            //double bestDistance = 0;

            //Stack<int> stack = new Stack<int>();
            //Stack<int> printStack = new Stack<int>();
            //List<Node> shortestTravelList = new List<Node>();
            //                                      //1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11
            //int[,] adjMatrix = new int[11, 11]   {{ 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 }
            //                                     ,{ 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 }
            //                                     ,{ 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0 }
            //                                     ,{ 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 }
            //                                     ,{ 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 }
            //                                     ,{ 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 }
            //                                     ,{ 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0 }
            //                                     ,{ 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 }
            //                                     ,{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
            //                                     ,{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
            //                                     ,{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};

            System.Console.WriteLine("Please enter the file name with extension");
            path = "..\\..\\TestFiles\\" + System.Console.ReadLine();

            StreamReader file = new StreamReader(path);
            var lines = File.ReadLines(path);

            //Skip to where the nodes start in the file
            foreach (string line in lines.Skip(7))
            {
                int a, i = 0;
                double x, y;

                System.Console.WriteLine(line);
                string[] result = line.Split();

                //Split data rows into id, x position, and y position
                a = Convert.ToInt32(result[i]);
                x = Convert.ToDouble(result[i + 1]);
                y = Convert.ToDouble(result[i + 2]);

                Node node = new Node(a, x, y);
                nodeList.Add(node);

                Console.WriteLine("The Node " + node.getIndex() + " (" + node.getXPos() + "," + node.getYPos() + ") has been added");
            }

            Graph graph = new Graph();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 7);
            graph.AddEdge(5, 8);
            graph.AddEdge(6, 8);
            graph.AddEdge(7, 9);
            graph.AddEdge(7, 10);
            graph.AddEdge(8, 9);
            graph.AddEdge(8, 10);
            graph.AddEdge(8, 11);
            graph.AddEdge(9, 11);
            graph.AddEdge(10, 11);

            /*Print Connected nodes*/
            for (int i = 0; i < nodeList.Count-1; i++)
            {
                Console.Write(nodeList[i].getIndex() + " is connected to ");

                for (int j = 0; j < adjList[i].Count; j++)
                {

                    Console.Write(adjList[i][j].getIndex() + " ");
                }
                Console.WriteLine();
            }
            

            foreach (Node node in nodeList)
            {
                Console.Write(node.ToString());
            }
            Console.WriteLine();

            //DepthFirstSearch(nodeList, adjMatrix, 0, stack, printStack);
            //Console.Write("Optimal Path: ");
            //foreach (Node node in shortestTravelList)
            //{
            //    Console.Write(node.ToString());
            //}
            //Console.Write(" Distance: " + bestDistance);
            if (nStack.Any() == false)
            {
                nStack.Push(nodeList[0].getIndex());
                pStack.Push(nodeList[0].getIndex());

                nodeList[0].setVisited(true);
                DFS(0);
            }
            Console.ReadKey();
        }//End Main()

        private static void getDistance(List<Node> list, ref List<Node> bestList, ref double bestD)
        {
            double adjDistance = 0;
            double cycleDistance = 0;

            //Calculate distance traveled between each node in a permutated list
            for (int i = 0; i < list.Count() - 1; i++)
            {
                adjDistance = Math.Sqrt(Math.Pow((Convert.ToDouble(list[i + 1].getXPos()) - Convert.ToDouble(list[i].getXPos())), 2)
                    + Math.Pow((Convert.ToDouble(list[i + 1].getYPos()) - Convert.ToDouble(list[i].getYPos())), 2));
                cycleDistance += adjDistance;

                //Add distance from last node to first node to total distance traveled
                if (i == list.Count() - 2)
                {

                    adjDistance = Math.Sqrt(Math.Pow((Convert.ToDouble(list[0].getXPos()) - Convert.ToDouble(list[i + 1].getXPos())), 2)
                    + Math.Pow((Convert.ToDouble(list[0].getYPos()) - Convert.ToDouble(list[i + 1].getYPos())), 2));
                    cycleDistance += adjDistance;
                }
            }

            //If bestDistance is uninitialized, set to first distance value
            if (bestD == 0)
            {
                bestD = cycleDistance;
            }

            //Check to see if a new bestDistance has been found
            else
            {
                if (cycleDistance < bestD)
                {
                    bestD = cycleDistance;
                    bestList.Clear();
                    getBestList(list, ref bestList);
                }
                else
                {
                }
            }
        }

        private static void getBestList(List<Node> list, ref List<Node> bestList)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                int a; double x, y = 0;
                a = list[i].getIndex();
                x = list[i].getXPos();
                y = list[i].getYPos();

                Node node = new Node(a, x, y);
                bestList.Add(node);
            }
        }       
    }
}
