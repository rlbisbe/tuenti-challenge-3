using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Challenge5
{
    class Program
    {
        static void Main(string[] args)
        {
            string total = Console.ReadLine();

            int iTotal;

            if (!int.TryParse(total, out iTotal))
                return;

            while (iTotal > 0)
            {
                iTotal--;
                string bounds = Console.ReadLine();
                string initialPosition = Console.ReadLine();
                string sTime = Console.ReadLine();
                Console.ReadLine();
                string board = Console.ReadLine();
                int time;

                if (!int.TryParse(sTime, out time))
                    continue;

                Solve(initialPosition, time, board, bounds, initialPosition);
            }
        }

        private static void Solve(string position, int time, string coords, string bounds, string initialPosition)
        {
            string[] sbounds = bounds.Split(',');
            string[] scoords = initialPosition.Split(',');
            int[,] board = new int[int.Parse(sbounds[0]), int.Parse(sbounds[1])];
            bool[,] visited = new bool[int.Parse(sbounds[0]), int.Parse(sbounds[1])];

            string[] positions = coords.Split('#');
            foreach (var item in positions)
            {
                string[] parameters = item.Split(',');
                board[int.Parse(parameters[0]),
                int.Parse(parameters[1])] = int.Parse(parameters[2]);
            }

            Console.WriteLine(FindResult(board, int.Parse(scoords[0]),
                int.Parse(scoords[1]), time, visited));

            //Console.ReadKey();
        }

        private static int FindResult(int[,] board, int x, int y, int time, bool[,] visited)
        {
            if (x >= board.GetLength(0) || x < 0)
                return 0;

            if (y >= board.GetLength(1) || y < 0)
                return 0;
            
            if (visited[x, y])
                return 0;
    
            if (time == 0)
                return board[x, y];

            visited = (bool[,])visited.Clone();
            visited[x, y] = true;

            int[] possibleResults = 
            {
                FindResult(board, x + 1, y, time - 1, visited),
                FindResult(board, x, y + 1, time - 1, visited),
                FindResult(board, x, y - 1, time - 1, visited),
                FindResult(board, x - 1, y, time - 1, visited)
            };

            //Debug!
            //for (int i = 0; i < time; i++)
            //{
            //    Console.Write(" ");
            //}
            //Console.WriteLine("pos {3}. {0} {1} - {2} {4}", x, y, board[x, y] + possibleResults.Max(), time,  board[x, y]);
            return board[x, y] + possibleResults.Max();
        }
    }
}
