using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Npuzzle
{
    public class priortyq
    {
        public static List<vertix> list = new List<vertix>();

        public void min_heapify(int index)
        {
            int min_index;
            int left_index = 2 * index + 1;
            int right_index = 2 * index + 2;
            if (left_index < list.Count && list[left_index].heuristic < list[index].heuristic)
                min_index = left_index;
            else
                min_index = index;
            if (right_index < list.Count && list[right_index].heuristic < list[min_index].heuristic)
                min_index = right_index;
            if (min_index != index)
            {
                vertix tmp = list[index];
                list[index] = list[min_index];
                list[min_index] = tmp;
                min_heapify(min_index);
            }
        }

        public void Enqueue(vertix node)
        {
            list.Add(node);
            int i = list.Count - 1;
            int parent_index;
            while (i > 0)
            {
                parent_index = (i - 1) / 2;
                if (list[parent_index].heuristic > list[i].heuristic)
                {
                    vertix tmp = list[parent_index];
                    list[parent_index] = list[i];
                    list[i] = tmp;
                }
                i = parent_index;
            }
        }

        public vertix dequeue()
        {
            vertix minimum = list[0];
            if (list.Count != 0)
            {
                minimum = list[0];
                list[0] = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                min_heapify(0);
            }
            return minimum;
        }

        public void display()
        {
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine(list[i].heuristic);
        }

    }
}
