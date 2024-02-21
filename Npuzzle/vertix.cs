using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Npuzzle
{
    public class vertix
    {
        public int[,] matrix;
        public int heuristic;
        public int depth;
        public vertix parent;

        public vertix(int[,] m, int h, int d , vertix p)
        {
            this.matrix = m;
            this.heuristic = h;
            this.depth = d;
            this.parent = p;
        }
    }
}
