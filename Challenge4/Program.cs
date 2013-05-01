using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge4
{
    class Program
    {
        static void Main(string[] args)
        {
            // FIRST PART:
            //using (StreamWriter writer = new StreamWriter("sample.txt"))
            //{
            //    long j = 0;

            //    var fs = new FileStream(@"integers", FileMode.Open);
            //    var len = 399796;
            //    var bits = new byte[len];
            //    fs.Read(bits, 0, len);
            //    // Dump 16 bytes per line
            //    for (int ix = 0; ix < len; ix += 4, j ++)
            //    {
            //        var cnt = Math.Min(4, len - ix);
            //        var line = new byte[cnt];
            //        Array.Copy(bits, ix, line, 0, cnt);
            //        writer.WriteLine(string.Format("{1}", j, BitConverter.ToInt32(line, 0)));
            //    }

            //    len = 400000;
            //    bits = new byte[len];
            //    fs.Seek(8589534392, SeekOrigin.Begin);
            //    fs.Read(bits, 0, len);
            //    for (int ix = 0; ix < len; ix += 4, j++)
            //    {
            //        var cnt = Math.Min(4, len - ix);
            //        var line = new byte[cnt];
            //        Array.Copy(bits, ix, line, 0, cnt);
            //        writer.WriteLine(string.Format("{1}", j, BitConverter.ToInt32(line, 0)));
            //    }
            //}


            // SECOND PART:
            //List<int> myList = new List<int>();
            //int previous = 0;
            //using (StreamReader reader = new StreamReader("output.txt"))
            //{
            //    string line;
            //    while ((line = reader.ReadLine()) != null)
            //    {
            //        int res = int.Parse(line);
            //        if (res == previous + 2)
            //            myList.Add(previous + 1);

            //        previous = res;
            //    }
            //}

            //using (StreamWriter writer = new StreamWriter("output_sorted.txt"))
            //{
            //    foreach (var item in myList)
            //    {
            //        writer.WriteLine(item);
            //    }
            //}

            //THIRD PART
            List<int> myResult = new List<int>(new[]{
7303,
8243,
9854,
12009,
12793,
14346,
14680,
15093,
17857,
19375,
20084,
22525,
23054,
23250,
30197,
36318,
39334,
40018,
48871,
50654,
50721,
54592,
59393,
61063,
63138,
63241,
64549,
66259,
69103,
76165,
76685,
81278,
82333,
83089,
84011,
85250,
88429,
90254,
90271,
90981,
91165,
93661,
94654,
99088,
99146,
99612,
2147386534,
2147387515,
2147390868,
2147393636,
2147394767,
2147394776,
2147399790,
2147404278,
2147410474,
2147411181,
2147411772,
2147414329,
2147414440,
2147415261,
2147415351,
2147416362,
2147416780,
2147416956,
2147418296,
2147419403,
2147419606,
2147421475,
2147421911,
2147424275,
2147424781,
2147425007,
2147425958,
2147427008,
2147429783,
2147430753,
2147434866,
2147436265,
2147439441,
2147442423,
2147443250,
2147454548,
2147455603,
2147457507,
2147463138,
2147465967,
2147466563,
2147466673,
2147468436,
2147470025,
2147470723,
2147470869,
2147471405,
2147474036,
2147474185,
2147476664,
2147478255,
2147478824,
2147480866,
2147480904});

            string total = Console.ReadLine();

            int iTotal;

            if (!int.TryParse(total, out iTotal))
                return;

            while (iTotal > 0)
            {
                iTotal--;
                string value = Console.ReadLine();
                int index;
                if (!int.TryParse(value, out index))
                    continue; 
                
                Console.WriteLine(myResult[index - 1]);
            }
        }
    }
}
