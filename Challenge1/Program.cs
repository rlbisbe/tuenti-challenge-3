using System;
using System.Collections.Generic;

namespace Challenge1
{
    class Program
    {
        static void Main(string[] args)
        {
            string total = Console.ReadLine();

            int iTotal;

            if (!int.TryParse(total, out iTotal))
                return;

            while (iTotal >= 0)
            {
                string budget = Console.ReadLine();
                string value = Console.ReadLine();
                ProcessNumbers(budget, value);
                iTotal--;
            }
        }

        static void ProcessNumbers(string budget, string numbers)
        {
            long iBudget;
            long remainder = 0;
            long actions = -1;

            if (!long.TryParse(budget, out iBudget))
                return;

            string[] values = numbers.Split(' ');
            List<long> intValues = new List<long>();

            foreach (var item in values)
            {
                long value;
                if (long.TryParse(item, out value))
                    intValues.Add(value);
            }

            if (intValues[0] < intValues[1])
            {
                actions = iBudget / intValues[0];
                remainder = iBudget % intValues[0];
                iBudget = -1;
            }

            for (int i = 1; i < intValues.Count - 1; i++)
            {
                //We have a min
                if (intValues[i - 1] >= intValues[i] && intValues[i] < intValues[i + 1])
                {
                    if (iBudget == -1)
                        continue;

                    actions = iBudget / intValues[i];
                    remainder = iBudget % intValues[i];
                    iBudget = -1;

                    continue;
                }

                //We have a max
                if (intValues[i - 1] <= intValues[i] && intValues[i] > intValues[i + 1])
                {
                    if (actions != -1)
                    {
                        iBudget = actions * intValues[i] + remainder;
                        remainder = 0;
                        actions = -1;
                    }
                }
            }

            if (intValues[intValues.Count - 2] < intValues[intValues.Count - 1])
            {
                if (actions != -1)
                {
                    iBudget = actions * intValues[intValues.Count - 1] + remainder;
                    remainder = 0;
                    actions = -1;
                }
            }

            Console.WriteLine(iBudget);
        }
    }
}
