using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Npuzzle
{
    public class ReadFiles
    {
        int n;
        List<int> datasolvability = new List<int>();
        int[,] data ;
        char costfn_type;

        public ReadFiles(char c_fn_type)
        {
            this.costfn_type = c_fn_type;

            string path = "Testcases\\Complete\\Complete Test\\V. Large test case\\TEST.txt";
            List<string> file = File.ReadAllLines(path).ToList();
            n = int.Parse(file[0]);

            int startline;
            if (file[1] == "")
                startline = 2;
            else
                startline = 1;
        
            data = new int[n, n];
            int row = -1;
            int col;
            for (int i = startline; i < file.Count; i++)
            {
                row++;
                col = -1;
                foreach (string ch in file[i].Split(" "))
                {
                    if (ch != "")
                    {
                        col++;
                        data[row, col] = (int.Parse(ch));
                        datasolvability.Add(int.Parse(ch));
                    }
                }
            }
            check_solvability();
        }

        public void check_solvability()
        {
            solvabililty puzzle = new solvabililty();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (puzzle.Is_solvable(datasolvability, n))
            {
                Console.WriteLine("Solvable");                
                Astar a = new Astar(data, n , costfn_type);
                if (n == 3)
                    a.Show_Path();
                Console.WriteLine("Execution Time = "+stopwatch.Elapsed);            
            }
            else
                Console.WriteLine("NOT Solvable");
        }

    }
}

//Test Cases...
//sample:
//solvable
//Testcases\\Sample\\Sample Test\\Solvable Puzzles\\8 Puzzle (1).txt
//Testcases\\Sample\\Sample Test\\Solvable Puzzles\\8 Puzzle (2).txt
//Testcases\\Sample\\Sample Test\\Solvable Puzzles\\8 Puzzle (3).txt
//Testcases\\Sample\\Sample Test\\Solvable Puzzles\\15 Puzzle - 1.txt
//Testcases\\Sample\\Sample Test\\Solvable Puzzles\\24 Puzzle 1.txt
//Testcases\\Sample\\Sample Test\\Solvable Puzzles\\24 Puzzle 2.txt
//NOT solvable
//Testcases\\Sample\\Sample Test\\Unsolvable Puzzles\\8 Puzzle - Case 1.txt
//Testcases\\Sample\\Sample Test\\Unsolvable Puzzles\\8 Puzzle(2) - Case 1.txt
//Testcases\\Sample\\Sample Test\\Unsolvable Puzzles\\8 Puzzle(3) - Case 1.txt
//Testcases\\Sample\\Sample Test\\Unsolvable Puzzles\\15 Puzzle - Case 2.txt
//Testcases\\Sample\\Sample Test\\Unsolvable Puzzles\\15 Puzzle - Case 3.txt

//complete:
//solvable
//Manhatting & Hamming
//Testcases\\Complete\\Complete Test\\Solvable puzzles\\Manhattan & Hamming\\50 Puzzle.txt
//Testcases\\Complete\\Complete Test\\Solvable puzzles\\Manhattan & Hamming\\99 Puzzle - 1.txt
//Testcases\\Complete\\Complete Test\\Solvable puzzles\\Manhattan & Hamming\\99 Puzzle - 2.txt
//Testcases\\Complete\\Complete Test\\Solvable puzzles\\Manhattan & Hamming\\9999 Puzzle.txt
//Manhattan Only
//Testcases\\Complete\\Complete Test\\Solvable puzzles\\Manhattan Only\\15 Puzzle 1.txt
//Testcases\\Complete\\Complete Test\\Solvable puzzles\\Manhattan Only\\15 Puzzle 3.txt
//Testcases\\Complete\\Complete Test\\Solvable puzzles\\Manhattan Only\\15 Puzzle 4.txt
//Testcases\\Complete\\Complete Test\\Solvable puzzles\\Manhattan Only\\15 Puzzle 5.txt
//NOT solvable
//Testcases\\Complete\\Complete Test\\Unsolvable puzzles\\15 Puzzle 1 - Unsolvable.txt
//Testcases\\Complete\\Complete Test\\Unsolvable puzzles\\99 Puzzle - Unsolvable Case 1.txt
//Testcases\\Complete\\Complete Test\\Unsolvable puzzles\\99 Puzzle - Unsolvable Case 2.txt
//Testcases\\Complete\\Complete Test\\Unsolvable puzzles\\9999 Puzzle.txt
//Very Large Case
//Testcases\\Complete\\Complete Test\\V. Large test case\\TEST.txt   