using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.Tools
{
    public class HQFile
    {
        public static string GetFile(string Path)
        {
            return System.IO.File.ReadAllText(Path, Encoding.UTF8);
        }

        public static void SaveFile(string Path, string Data)
        {
            System.IO.File.WriteAllText(Path, Data, Encoding.UTF8);
        }

        public static void Append(string Path, string Data)
        {
            System.IO.File.AppendAllText(Path, Data, Encoding.UTF8);
        }

    }
}
