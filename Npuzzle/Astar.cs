using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Npuzzle
{
    public class Astar
    {
        public int n;
        public int[,] thestate;
        public int[,] final_state;
        public int x_zero;
        public int y_zero;
        public char costfn_type;
        vertix parent_v;
        HashSet<string> previous_matrices = new HashSet<string>();
        PriorityQueue <vertix , int> q = new PriorityQueue <vertix , int>();     

        public Astar(int[,] s, int n , char c_fn_type)
        {
            this.thestate = s;
            this.n = n;
            this.costfn_type = c_fn_type;
            final_state = new int[n, n];
            get_final_state();

            int cost=0;
            if (costfn_type=='h' || costfn_type == 'H')
                 cost = hamming_fn(thestate);
            else if (costfn_type=='m' || costfn_type == 'M')
                 cost = manhattan_fn(thestate);
            int depth = 0;        
            int heuristic = cost + depth;
            parent_v = null;
            vertix intial_v = new vertix(thestate, heuristic, depth , parent_v);
            previous_matrices.Add(str_data(thestate));

            parent_v = intial_v;
            start_moving();
        }

        public void start_moving()
        {            
            while (parent_v.heuristic - parent_v.depth != 0)
            {
                get_zero_pos(thestate);
                if (x_zero != 0)
                    moveup();
                if (x_zero != n - 1)
                    movedown();
                if (y_zero != 0)
                    moveleft();
                if (y_zero != n - 1)
                    moveright();             
                parent_v = q.Dequeue();
                thestate = parent_v.matrix;
                previous_matrices.Add(str_data(thestate));
            }
            Console.WriteLine("Number of movements = " + parent_v.depth);          
        }

        public void moveup()
        {
            int[,] matrix = thestate.Clone() as int [,];
            int tmp = matrix[x_zero - 1, y_zero];
            matrix[x_zero - 1, y_zero] = 0;
            matrix[x_zero, y_zero] = tmp;
            steps(matrix);
        }

        public void movedown()
        {
            int[,] matrix = thestate.Clone() as int[,];
            int tmp = matrix[x_zero + 1, y_zero];
            matrix[x_zero + 1, y_zero] = 0;
            matrix[x_zero, y_zero] = tmp;            
            steps(matrix);         
        }

        public void moveleft()
        {
            int[,] matrix = thestate.Clone() as int[,];
            int tmp = matrix[x_zero, y_zero - 1];
            matrix[x_zero, y_zero - 1] = 0;
            matrix[x_zero, y_zero] = tmp;
            steps(matrix);
        }

        public void moveright()
        {
            int[,] matrix = thestate.Clone() as int[,];
            int tmp = matrix[x_zero, y_zero + 1];
            matrix[x_zero, y_zero + 1] = 0;
            matrix[x_zero, y_zero] = tmp;
            steps(matrix);           
        }

        public void steps(int[,] matrix)
        {
            int cost = 0;
            if (costfn_type == 'h'|| costfn_type == 'H')
                cost = hamming_fn(matrix);
            else if (costfn_type == 'm'|| costfn_type == 'M')
                cost = manhattan_fn(matrix);
            int depth = parent_v.depth+1;
            int heuristic = cost + depth;
            vertix v = new vertix(matrix, heuristic, depth ,parent_v);
            string data = str_data(matrix);
            bool found = previous_matrices.Contains(data);
            if (found == false)
            {
                q.Enqueue(v, v.heuristic);
            }
        }
        
        public int hamming_fn(int[,] matrix)
        {
            int haming_cost = 0;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (matrix[r, c] != 0)
                    {
                        if (matrix[r, c] != final_state[r, c])
                            haming_cost++;
                    }
                }
            }
            return haming_cost;
        }

        public int manhattan_fn(int[,] matrix)
        {
            int manhattan_cost = 0;
            int row, col;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (matrix[r, c] != 0)
                    {
                        row = (matrix[r, c] - 1) / n;
                        col = (matrix[r, c] - 1) % n;
                        manhattan_cost += (Math.Abs(r - row) + Math.Abs(c - col));
                    }
                }
            }
            return manhattan_cost;
        }

        public string str_data(int[,] matrix)
        {
            string data = "";
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    data += matrix[r, c];
                    data += " ";
                }
            }
            return data;
        }
      
        public void get_zero_pos(int[,] state)
        {
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (state[r, c] == 0)
                    {
                        x_zero = r;
                        y_zero = c;
                    }
                }
            }
        }

        public void get_final_state()
        {
            int val = 1;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (r == n - 1 && c == n - 1)
                        val = 0;
                    final_state[r, c] = val;
                    val++;
                }               
            }
        }

        public void Show_Path()
        {
            List<vertix> path = new List<vertix>();
            path.Add(parent_v);          
            while (true)
            {
                parent_v = parent_v.parent;
                if (parent_v != null)
                    path.Add(parent_v);
                else
                    break;
            }

            for (int i = path.Count - 1; i >= 0; i--)
            {
                for (int r = 0; r < n; r++)
                {
                    for (int c = 0; c < n; c++)
                    {
                        Console.Write(path[i].matrix[r, c]);
                        Console.Write(" ");
                    }
                    Console.WriteLine();      
                }                             
                Console.WriteLine();    
            }
        }


    }
}
