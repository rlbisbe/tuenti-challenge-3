using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Challenge5
{
    class Piece
    {
        public Complex position { get; set; }
        public int value { get; set; }
    }

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

                Solve(initialPosition, time, board);
            }
        }

        private static void Solve(string position, int time, string coords)
        {
            //Define the list and the board
            int result = 0;
            Complex currentPosition;
            Complex previousPosition;

            List<Piece> pieces = new List<Piece>();
            string[] positions = coords.Split('#');
            foreach (var item in positions)
            {
                string[] parameters = item.Split(',');
                pieces.Add(new Piece()
                {
                    position = new Complex(int.Parse(parameters[0]),
                    int.Parse(parameters[1])),
                    value = int.Parse(parameters[2]) 
                });                
            }

            string[] aposition = position.Split(',');
            currentPosition = new Complex(int.Parse(aposition[0]), 
                int.Parse(aposition[1]));

            //Console.WriteLine(currentPosition);
            //Console.WriteLine(result);

            for (int i = 0; i < time; i++)
            {
                int max = int.MinValue;
                int maxFuture = int.MinValue;
                
                Piece next = null;
                Piece future = null;

                foreach (var item in pieces)
                {
                    Complex distance = item.position - currentPosition;
                    double abs = Complex.Abs(distance);
                    if (abs == 1)
                    {
                        if (item.value >= max)
                        {
                            max = item.value;
                            next = item;
                        }
                        continue;
                    }

                    if (item.value >= maxFuture)
                    {
                        maxFuture = item.value;
                        future = item;
                    }
                }

                if (next != null)
                {
                    Complex move = next.position - currentPosition;
                    previousPosition = currentPosition;
                    currentPosition = currentPosition + move;
                    result += next.value;
                    pieces.Remove(next);
                    //Console.WriteLine(currentPosition);
                    //Console.WriteLine(result);
                    continue;
                }

                if (future != null)
                {
                    Complex move = future.position - currentPosition;
                    Complex c;
                    double x;
                    double y;
                    x = move.Real / Math.Abs(move.Real);
                    y = move.Imaginary / Math.Abs(move.Imaginary);

                    if (Math.Abs(x) > 0)
                        c = new Complex(x, 0);
                    else
                        c = new Complex(0, y);

                    Complex newPosition = currentPosition + c;
                    if (newPosition == currentPosition)
                    {
                        if (Math.Abs(c.Real) == 1)
                        {
                            c = new Complex(0, 1);
                            newPosition = currentPosition + c;
                            if (Complex.Abs((future.position - newPosition)) 
                                > Complex.Abs(future.position - currentPosition))
                            {
                                c = new Complex(0, -1);
                            }
                        }
                        else
                        {
                            c = new Complex(1, 0);
                            newPosition = currentPosition + c;
                            if (Complex.Abs((future.position - newPosition))
                                > Complex.Abs(future.position - currentPosition))
                            {
                                c = new Complex(-1, 0);
                            }
                        }
                    }

                    previousPosition = currentPosition;
                    currentPosition = currentPosition + c;
                    //Console.WriteLine(currentPosition);
                }
            }

            Console.WriteLine(result);
        }
    }
}
