using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Challenge6
{
    enum Visited : byte
    {
        none = 1,
        left = 2,
        right = 4,
        top = 8,
        down = 16,
    }

    class Program
    {
        static Visited GetFromCoordinates(int x, int y)
        {
            if (x == 1)
                return Visited.right;

            if (x == -1)
                return Visited.left;

            if (y == 1)
                return Visited.top;

            if (y == -1)
                return Visited.down;

            return Visited.none;
        }

        static void Main(string[] args)
        {
            string total = Console.ReadLine();

            int iTotal;

            if (!int.TryParse(total, out iTotal))
                return;

            while (iTotal > 0)
            {
                iTotal--;
                string key = Console.ReadLine();

                int rows = int.Parse(key.Split(' ')[1]);
                List<string> board = new List<string>();
                Encoding iso = Encoding.GetEncoding(1250);
                Encoding utf8 = Encoding.UTF8;
                for (int i = 0; i < rows; i++)
                {
                    string originalMsg = Console.ReadLine();
                    byte[] utfBytes = iso.GetBytes(originalMsg);
                    byte[] isoBytes = Encoding.Convert(iso, utf8, utfBytes);
                    string msg = utf8.GetString(isoBytes);
                    board.Add(msg.Replace("TA", "."));
                }

                Solve(key, board.ToArray());

            }
        }

        private static void Solve(string header, string[] unparsedBoard)
        {

            string[] sHeader = header.Split(' ');
            int xpos = 0;
            int ypos = 0;
            char[,] board = new char[int.Parse(sHeader[1]), int.Parse(sHeader[0])];
            Visited[,] visited = new Visited[int.Parse(sHeader[1]), int.Parse(sHeader[0])];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = unparsedBoard[i][j];
                    if (board[i, j] == 'X')
                    {
                        xpos = i;
                        ypos = j;
                    }
                }
            }

            int totalTime = 0;
            float speed = 1 / float.Parse(sHeader[2]);
            int reactionTime = int.Parse(sHeader[3]);
            Console.WriteLine(Math.Round(FindResult(board, xpos, ypos, visited, 0, 0, totalTime, speed, reactionTime)));
        }

        private static float FindResult(char[,] board, int x, int y, Visited[,] visited, int xSlide, int ySlide, int time, float speed, int reactionTime)
        {
            if (x >= board.GetLength(0) || x < 0)
                return float.MaxValue;

            if (y >= board.GetLength(1) || y < 0)
                return float.MaxValue;

            Visited v = GetFromCoordinates(xSlide, ySlide);
            if ((visited[x, y] & v) == v)
            {
#if DEBUG
                for (int i = 0; i < time; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("(V) - {2} {0},{1} . - {3} {4} [{5}]", x, y, time, xSlide, ySlide, board[x, y]);
#endif
                return float.MaxValue;
            }

            if (board[x, y] == '#')
                return float.MaxValue;

            if (board[x, y] == 'O')
            {
#if DEBUG
                Console.WriteLine("Found it! 0 ");
#endif
                return speed;
            }

            visited = (Visited[,])visited.Clone();
            visited[x, y] = visited[x, y] | v;

            float result = 0;

#if DEBUG
            for (int i = 0; i < time; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("(B) - {2} {0},{1} . - {3} {4} [{5}]", x, y, time, xSlide, ySlide, board[x, y]);
#endif
            int mul = 1;
            int newX = 0;
            int newY = 0;
            bool stopped = true;
            if (xSlide != 0 || ySlide != 0)
            {
                result = speed;
                stopped = false;
                while (!stopped)
                {
                    newX = x + mul * xSlide;
                    newY = y + mul * ySlide;

                    visited[newX, newY] = visited[newX, newY] | GetFromCoordinates(xSlide, ySlide);

#if DEBUG
                    for (int i = 0; i < time; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine("(S) - {2} {0},{1} . - {3} {4} ({5}) [{6}]",
                        newX, newY, time, xSlide, ySlide, result,
                        board[newX, newY]);
#endif

                    if (board[newX, newY] == 'O')
                    {
#if DEBUG
                        Console.WriteLine("Found it! {0} ", result);
#endif
                        return result + speed;
                    }
                    if (board[newX, newY] == '#')
                        stopped = true;
                    else
                    {
                        result += speed;
                        mul++;
                    }
                }
            }
            mul--;

            //Debug!
            float sum = result;
#if DEBUG
            Console.WriteLine(sum);
#endif
            x = x + mul * xSlide;
            y = y + mul * ySlide;

            if (stopped)
            {
                sum += reactionTime;
            }

            float[] results = 
            {
                 FindResult(board, x + 1, y, visited, 1, 0, time + 1, speed, reactionTime),
                 FindResult(board, x - 1, y, visited, -1, 0, time + 1, speed, reactionTime),
                 FindResult(board, x, y - 1, visited, 0, -1, time + 1, speed, reactionTime),
                 FindResult(board, x, y + 1, visited, 0, 1, time + 1, speed, reactionTime),
            };

            float min = results.Min();
#if DEBUG
            if (min < float.MaxValue)
            {
                for (int i = 0; i < time; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("(E) - {2} {0},{1} . - {3} {4} [{5}]", x, y, time, xSlide, ySlide, sum + min);
            }
#endif
            return sum + min;
        }
    }
}
