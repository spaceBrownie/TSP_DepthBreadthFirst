using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_DepthBreadthFirst
{
    class Node
    {
        private int index;
        private double x, y;
        private bool visited = false;

        public Node()
        {
            setIndex(0);
            setXPos(0);
            setYPos(0);

        }
        public Node(int a, double b, double c)
        {
            setIndex(a);
            setXPos(b);
            setYPos(c);
        }

        //Getters
        public int getIndex()
        {
            return index;
        }
        public double getXPos()
        {
            return x;
        }
        public double getYPos()
        {
            return y;
        }

        public bool getVisited()
        {
            return visited;
        }

        //Setters
        public void setIndex(int index)
        {
            this.index = index;
        }
        public void setXPos(double x)
        {
            this.x = x;
        }
        public void setYPos(double y)
        {
            this.y = y;
        }
        public void setVisited()
        {
            this.visited = true;
        }



        public override string ToString()
        {
            //return "{" + getIndex() + ": (" + getXPos() + "," + getYPos() + ")}";
            return "{" + getIndex() + "}";
        }
    }
}
