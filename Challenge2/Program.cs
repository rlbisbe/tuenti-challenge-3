using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2
{
    class Program
    {
        static Dictionary<string, List<string>> Values = new Dictionary<string, List<string>>();
        static void Main(string[] args)
        {
            string buffer = GetLine();

            string dictionaryName = buffer;
            ParseDictionary(dictionaryName);

            string total = GetLine();

            int iTotal;

            if (!int.TryParse(total, out iTotal))
                return;
            
            while (iTotal > 0)
            {
                string word = GetLine();
                Process(word);
                iTotal--;
            }
        }

        private static string GetLine()
        {
            string buffer;
            do
            {
                buffer = Console.ReadLine();
            } while (buffer.IndexOf('#') == 0);
            return buffer;
        }

        private static void ParseDictionary(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string key = GetBase(line);
                    if (!Values.ContainsKey(key))
                        Values[key] = new List<string>();

                    Values[key].Add(line);
                }
            }
        }

        private static void Process(string word)
        {
            string key = GetBase(word);
            if (!Values.ContainsKey(key))
            {
                Console.WriteLine(word + " -> ");
                return;
            }
            Values[key].Remove(word);
            Values[key].Sort();
            Console.WriteLine(word + " -> " + String.Join(" ", Values[key].ToArray()));
            Values[key].Add(word);
        }

        private static string GetBase(string word)
        {
            char[] array = word.ToCharArray();
            Array.Sort<char>(array);
            return String.Join("", array);
        }
    }
}
