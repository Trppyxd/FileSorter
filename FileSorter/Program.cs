using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = "";
            DirectoryInfo directoryInfo;

            while (directoryPath == "")
            {
                Console.WriteLine("Enter directory path: ");
                directoryPath = Console.ReadLine();

                try
                {
                    directoryInfo = new DirectoryInfo(directoryPath);

                    if (directoryInfo.GetFiles().Length == 0)
                    {
                        directoryPath = "";
                        Console.Clear();
                    }
                }
                catch (Exception)
                {
                    directoryPath = "";
                    Console.Clear();
                }
            }

            directoryInfo = new DirectoryInfo(directoryPath);


            var files = directoryInfo.GetFiles();
            var subDirectories = directoryInfo.GetDirectories();

            foreach (var file in files)
            {
                string fileExt = file.Extension.TrimStart('.');
                string fileSourceName = file.FullName;
                string fileDestName = $@"{directoryPath}\{fileExt}\{file.Name}";

                if (!Directory.Exists($@"{directoryPath}\{fileExt}"))
                {
                    Directory.CreateDirectory($@"{directoryPath}\{fileExt}");
                    Console.WriteLine($"Created directory {fileExt}");
                }

                if (!File.Exists(fileDestName))
                {
                    File.Move(fileSourceName, fileDestName);
                    Console.WriteLine($@"Moved .\{file.Name} TO .\{fileExt}\{file.Name}");
                }
            }
            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
