using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    class Program
    {
        static StreamReader streamReader;
        static String path = @"C:\Users\hesha_000\Desktop\Sudoku Solver\Sudoku Solver\TestCases\";

        static void Main(string[] args)
        {
            char[,] board = new char[9,9];
            String test_case_name = "case1.txt";
           
            readTestCase(path + test_case_name, board);

            Stopwatch stopwatch = Stopwatch.StartNew();

            Sequential sequential = new Sequential(9);
            sequential.solveSudoku(board);

            printBoard(board);

            stopwatch.Stop();
            

            Console.WriteLine("Time is: " + stopwatch.ElapsedMilliseconds);
        }

        public static bool readTestCase(String path, char[,] board)
        {
            try
            {
                streamReader = new StreamReader(path);
                String line;
                int j = 0;
                long g = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        board[j, i] = line[i];
                    }
                    j++;
                }
                streamReader.Close();
                return true;
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static void printBoard(char[,] board)
        {
            int length = (int)Math.Sqrt(board.Length);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                    Console.Write(board[i, j] + " ");
                Console.WriteLine();
            }

        }
    }
}
