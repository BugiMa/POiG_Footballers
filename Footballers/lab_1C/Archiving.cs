using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab_1C
{
    static class Archiving
    {
        public static void SaveFootballerToFile(string file, Footballer[] fballers)
        {
            using (StreamWriter stream = new StreamWriter(file))
            {
                foreach (var p in fballers)
                    stream.WriteLine(p.ToFileFormat());
                stream.Close();
            }
        }
        public static Footballer[] ReadFootballerFromFile(string file)
        {
            Footballer[] fballers = null;
            if (File.Exists(file))
            {
                var sfballers = File.ReadAllLines(file);
                var n = sfballers.Length;
                if (n > 0)
                {
                    fballers = new Footballer[n];
                    for (int i = 0; i < n; i++)
                        fballers[i] = Footballer.CreateFromString(sfballers[i]);
                    return fballers;
                }
            }
            return fballers;
        }
    }
}
