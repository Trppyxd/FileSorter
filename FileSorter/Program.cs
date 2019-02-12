using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                if (from dir in subDirectories where dir.Extension == files.) // fuck my life
                {
                    
                }
                Console.WriteLine(file.Name);
                
            }
        }
    }
}
