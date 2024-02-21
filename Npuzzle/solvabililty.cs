using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Npuzzle
{
    public class solvabililty
    {
        public bool Is_solvable(List<int> data, int input)
        {
            int inversions = get_inversions_counter(data);
            int blankrow = 0;
            if (input % 2 == 0)
            {
                blankrow = get_blank_pos(data, input);
            }
            if ((input % 2 != 0) && (inversions % 2 == 0))
                return true;
            else if ((input % 2 == 0) && (blankrow % 2 != 0) && (inversions % 2 != 0))
                return true;
            else if ((input % 2 == 0) && (blankrow % 2 == 0) && (inversions % 2 == 0))
                return true;
            else
                return false;
        }

        public int get_inversions_counter(List<int> data)
        {
            int inv_counter = 0;
            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i] == 0 || data[i] == 1)
                    continue;
                for (int j = i + 1; j < data.Count; j++)
                    if ((data[i] > data[j]) && (data[j] != 0))
                        inv_counter++;
            }
            return inv_counter;
        }

        public int get_blank_pos(List<int> data, int input)
        {
            int rowindex = 1;
            int counter = 0;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == 0)
                    return rowindex;
                counter++;
                if (counter == input)
                {
                    rowindex++;
                    counter = 0;
                }
            }
            return 0;
        }
    }
}
