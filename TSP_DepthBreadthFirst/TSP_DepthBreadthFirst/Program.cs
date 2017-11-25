using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace TSP_DepthBreadthFirst
{
    class Program
    {
        protected Stack<int> stack = new Stack<int>();
        static void Main(string[] args)
        {
            string path;
            double bestDistance = 0;
            List<Node> shortestTravelList = new List<Node>();
            //1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11
            int[,] adjMatrix = new int[11, 11]  {{ 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 }
                                                 ,{ 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 }
                                                 ,{ 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0 }
                                                 ,{ 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 }
                                                 ,{ 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 }
                                                 ,{ 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 }
                                                 ,{ 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0 }
                                                 ,{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }
                                                 ,{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
                                                 ,{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
                                                 ,{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
            //Dictionary<Node,>

            List<Node> nodeList = new List<Node>();
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

            for (int i = 0; i < nodeList.Count; i++)
            {
                Console.Write(nodeList[i].getIndex() + " is connected to ");
                for (int j = 0; j < nodeList.Count; j++)
                {
                    if (adjMatrix[i, j] == 1)
                    {
                        Console.Write(nodeList[j].getIndex() + " ");
                    }
                    //Console.Write(adjMatrix[i, j]); 
                }
                Console.WriteLine();
            }

            foreach (Node node in nodeList)
            {
                Console.Write(node.ToString());
            }
            Console.WriteLine();




            //PermuteList(nodeList, ref shortestTravelList, 0, nodeList.Count(), ref bestDistance);
            Console.Write("Optimal Path: ");
            foreach (Node node in shortestTravelList)
            {
                Console.Write(node.ToString());
            }
            Console.Write(" Distance: " + bestDistance);
            Console.ReadKey();
        }//End Main()


        //private static void PermuteList(List<Node> list, ref List<Node> bestList, int k, int n, ref double bestDistance)
        //{
        //    for (int i = k + 1; i < n; i++)
        //    {
        //        swap(list, k, i);
        //        getDistance(list, ref bestList, ref bestDistance);
        //        foreach (Node node in list)
        //        {
        //            Console.Write(node.ToString());
        //        }
        //        Console.WriteLine();

        //        PermuteList(list, ref bestList, k + 1, n, ref bestDistance);
        //        swap(list, k, i);
        //        //Console.WriteLine(list[]);
        //    }

        //}

        //private static void swap(List<Node> nl, int i, int j)
        //{
        //    Node temp = new Node();
        //    temp = nl[i];
        //    nl[i] = nl[j];
        //    nl[j] = temp;

        //}

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

        void DepthFirstSearch(List<Node> nodeList, int[,] adjList, int k)
        {
            if (nodeList[0].getVisited() == false)
            {
                nodeList[0].setVisited();
                stack.Push(nodeList[0].getIndex());
                DepthFirstSearch(nodeList, adjList, 0);
            }

            for (int i = k; i < nodeList.Count; ++i)
            {
                for (int j = 0; j < nodeList.Count; ++j)
                {
                    if (adjList[i, j] == 1 && nodeList[i].getVisited() == false)
                    {
                        stack.Push(nodeList[j].getIndex());
                        DepthFirstSearch(nodeList, adjList, j);
                    }
                    //If goal state 11 is reached
                    if (i == nodeList.Count - 1)
                    {

                    }
                }

            }
        }
    }
}
