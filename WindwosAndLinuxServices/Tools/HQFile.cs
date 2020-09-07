using System;
using System.Collections.Generic;
using System.Text;

namespace WindwosAndLinuxServices.Tools
{
    public class HQFile
    {
        public static string GetFile(string Path)
        {

            if (!System.IO.File.Exists(Path))
            {
                string dic = System.IO.Path.GetDirectoryName(Path);
                System.IO.Directory.CreateDirectory(dic);

                System.IO.File.Create(Path).Close();

            }

            return System.IO.File.ReadAllText(Path, Encoding.UTF8);
        }

        public static void SaveFile(string Path, string Data)
        {

            if (!System.IO.File.Exists(Path))
            {
                string dic = System.IO.Path.GetDirectoryName(Path);
                System.IO.Directory.CreateDirectory(dic);

                System.IO.File.Create(Path).Close();

            }


            System.IO.File.WriteAllText(Path, Data, Encoding.UTF8);
        }

        public static void Append(string Path, string Data)
        {

            if (!System.IO.File.Exists(Path))
            {
                string dic = System.IO.Path.GetDirectoryName(Path);

                System.IO.Directory.CreateDirectory(dic);

                System.IO.File.Create(Path).Close();

            }

            System.IO.File.AppendAllText(Path, Data, Encoding.UTF8);

        }



    }
}
