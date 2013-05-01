using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Challenge3
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
                string value = Console.ReadLine();
                ParseText(value);
                iTotal--;
            }
        }

        private static void ParseText(string value)
        {
            List<string> finalList = new List<string>();

            Regex regex = new Regex("([.><][^.<>]*)");
            MatchCollection group = regex.Matches(value);
            foreach (var item in group)
            {
                string pattern = item.ToString();
                if (pattern.StartsWith("."))
                {
                    finalList.Add(pattern);                    
                }
                if (pattern.StartsWith(">"))
                {
                    if (finalList.Contains("<" + pattern.Substring(1))
                        || finalList.Contains("." + pattern.Substring(1)))
                    {
                        Console.WriteLine("invalid");
                        return;
                    }

                    finalList.Add(pattern);                    
                }

                if (pattern.StartsWith("<"))
                {
                    string otherPattern = ">" + pattern.Substring(1);
                    int index = finalList.IndexOf(otherPattern);
                    if (index != -1)
                    {
                        if (finalList.Count - index > 2)
                        {
                            Console.WriteLine("valid");
                            return; 
                        }
                        continue;
                    }
                    finalList.Insert(finalList.Count - 1, pattern);
                }
            }

            Console.WriteLine(string.Join(",", finalList.ToArray())
                .Replace("<",string.Empty)
                .Replace(">",string.Empty)
                .Replace(".",string.Empty));
        }
    }
}
