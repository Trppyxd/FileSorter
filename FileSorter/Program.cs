using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter
{
    class Program
    {
        public static string DirectoryPath = string.Empty;
        static void Main(string[] args)
        {
            string directoryPath = DirectoryPath;
            DirectoryInfo directoryInfo;

            Console.WriteLine("Enter \"Y\" to create randomly generated files for testing purposes.");
            var input = Console.ReadLine().ToLower();
            if (input == "y")
            {
                bool loop = true;

                while (loop)
                {
                    try
                    {
                        Console.WriteLine("Enter target path:");
                        string targetDirectory = Console.ReadLine();
                        Console.WriteLine("Enter file amount: ");
                        int fileAmount = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                        Utilities.CreateTestFiles(targetDirectory, fileAmount);
                        loop = false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"[ERROR] {e.Message}");
                        loop = true;
                    }
                }
            }
            
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
                catch (Exception e)
                {
                    directoryPath = "";
                    //Console.Clear();
                    Console.WriteLine("Invalid directory path, try again!");
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
                    bool loop = true;
                    while (loop)
                    {
                        try
                        {
                            var fInfo = new FileInfo(fileSourceName);
                            File.Move(fileSourceName, fileDestName);
                            
                            Console.WriteLine($@"Moved .\{file.Name} >> .\{fileExt}\{file.Name}");
                            loop = false;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"[ERROR] {e.Message} {Environment.NewLine}" +
                                              $"The file {file.Name} is in use by another process, waiting 5 sec!");
                            System.Threading.Thread.Sleep(5000);

                            loop = true;
                        }

                    }
                }
            }
            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
