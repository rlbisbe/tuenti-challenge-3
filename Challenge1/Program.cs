using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge1
{
    class Program
    {
        static void Main(string[] args)
        {
            string value;
            do
            {
                value = Console.ReadLine();
                ProcessNumbers(value);

            } while (!string.IsNullOrEmpty(value));
        }

        static void ProcessNumbers(string numbers)
        {
            string[] values = numbers.Split(' ');
            List<int> intValues = new List<int>();
            List<int> minPositions = new List<int>();
            List<int> maxPositions = new List<int>();
            
            foreach (var item in values)
            {
                int value;
                if (int.TryParse(item, out value))
                    intValues.Add(value);
            }

            if (intValues[0] < intValues[1])
            {
                minPositions.Add(0);
            }

            for (int i = 1; i < intValues.Count - 1; i++)
            {
                if (intValues[i - 1] >= intValues[i] && intValues[i] < intValues[i + 1])
                {
                    minPositions.Add(i);
                    continue;
                }

                if (intValues[i - 1] <= intValues[i] && intValues[i] > intValues[i + 1])
                    maxPositions.Add(i);
            }

            if (intValues[intValues.Count - 2] < intValues[intValues.Count - 1])
            {
                maxPositions.Add(intValues.Count - 1);
            }

            Console.WriteLine("Min");
            foreach (var item in minPositions)
            {
                Console.WriteLine(intValues[item]);
            }

            Console.WriteLine("Max");
            foreach (var item in maxPositions)
            {
                Console.WriteLine(intValues[item]);
            }
        }
    }
}
