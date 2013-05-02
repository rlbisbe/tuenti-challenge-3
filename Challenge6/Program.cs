using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Challenge6
{
    class Program
    {
        static void Main(string[] args)
        {
            //string total = Console.ReadLine();

            //int iTotal;

            //if (!int.TryParse(total, out iTotal))
            //    return;

            //while (iTotal > 0)
            //{
            //    iTotal--;
            //    string bounds = Console.ReadLine();
            //    string initialPosition = Console.ReadLine();
            //    string sTime = Console.ReadLine();
            //    Console.ReadLine();
            //    string board = Console.ReadLine();
            //    int time;

            //    if (!int.TryParse(sTime, out time))
            //        continue;

            //    Solve(initialPosition, time, board, bounds, initialPosition);
            //}

            Solve("4 5 1 3", null);
            Console.ReadKey();

        }

        private static void Solve(string header, string[] unparsedBoard)
        {

            string[] sHeader = header.Split(' ');
            char[,] board = new char[int.Parse(sHeader[1]), int.Parse(sHeader[0])];
            bool[,] visited = new bool[int.Parse(sHeader[1]), int.Parse(sHeader[0])];

            board[0, 0] = '#'; board[0, 1] = '#'; board[0, 2] = '#'; board[0, 3] = '#';
            board[1, 0] = '#'; board[1, 1] = 'X'; board[1, 2] = '.'; board[1, 3] = '#';
            board[2, 0] = '#'; board[2, 1] = '.'; board[2, 2] = '.'; board[2, 3] = '#';
            board[3, 0] = '#'; board[3, 1] = '.'; board[3, 2] = '.'; board[3, 3] = 'O';
            board[4, 0] = '#'; board[4, 1] = '#'; board[4, 2] = '#'; board[4, 3] = '#';


            int totalTime = 0;
            Console.WriteLine(FindResult(board, 1, 1, visited, 0, 0, totalTime));
        }

        private static uint FindResult(char[,] board, int x, int y, bool[,] visited, int xSlide, int ySlide, int time)
        {
            if (x >= board.GetLength(0) || x < 0)
                return int.MaxValue;

            if (y >= board.GetLength(1) || y < 0)
                return int.MaxValue;

            if (visited[x, y])
                return int.MaxValue;

            if (board[x, y] == '#')
                return uint.MaxValue;

            if (board[x, y] == 'O')
                return 0;

            visited = (bool[,])visited.Clone();
            visited[x, y] = true;

            uint result = 0;

            for (int i = 0; i < time; i++)
            {
                Console.Write("\t");
            }
            Console.WriteLine("(B) - {2} {0},{1} . - {3} {4}", x, y, time, xSlide, ySlide);

            int mul = 1;
            if (xSlide != 0 || ySlide != 0)
            {
                bool stopped = false;
                while (!stopped)
                {
                    int newX = x + mul * xSlide;
                    int newY = y + mul * ySlide;

                    visited[newX, newY] = true;
                    for (int i = 0; i < time; i++)
                    {
                        Console.Write("\t");
                    }
                    Console.WriteLine("(S) - {2} {0},{1} . - {3} {4} ({5}) [{6}]",
                        newX, newY, time, xSlide, ySlide, result, 
                        board[newX, newY]);

                    if (board[newX, newY] == '#' || board[newX, newY] == 'O')
                        stopped = true;
                    else
                    {   
                        result += 1;
                        mul++;
                    }
                }
            }
            mul--;
            Console.WriteLine(mul);
            x = x + mul * xSlide;
            y = y + mul * ySlide;
            Console.WriteLine("Next pos: {0},{1}",x,y);
            uint[] possibleResults = {
                FindResult(board, x + 1, y, visited, 1, 0, time + 1),
                FindResult(board, x, y + 1, visited, 0, 1, time + 1),
                FindResult(board, x, y - 1, visited, 0, - 1, time + 1),
                FindResult(board, x - 1, y, visited, - 1, 0, time + 1)
        
            };
            //Debug!
            uint sum = 3 + result;
            var min = possibleResults.Min();

            if (min < int.MaxValue)
            {
                for (int i = 0; i < time; i++)
                {
                    Console.Write("\t");
                }
                Console.WriteLine("(T) - pos {3}. {0} {1} . - {5} {6} -  {4} {2}", x, y, sum + min, time, board[x, y], xSlide, ySlide);
            }

            return sum + min;
        }
    }
}
