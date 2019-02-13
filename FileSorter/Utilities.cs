using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FileSorter
{
    public class Utilities
    {
        public static string DirectoryPath = string.Empty;

        public static void CreateTestFiles(string targetDirectory, int fileAmount) // ‪C:\Users\opilane\Desktop\asdsda 
        {
            if (fileAmount == 0) return;

            string fileName = string.Empty;
            string fileExtension = string.Empty;;
            Random rnd = new Random();
            
            //var fileNameList = new string[]
            //{
            //    "file1",
            //    "file2",
            //    "file3",
            //    "file4",
            //    "file5",
            //    "file6"
            //};

            var fileExtensionList = new string[]
            {
                ".txt",
                ".bat",
                ".rar",
                ".zip",
                ".docx",
                ".pub",
                ".pptx",
                ".xlsx",
                //".accdb",
                ".bmp"
            };

            var fileAmountInitial = fileAmount;

            while ( fileAmount > 0)
            {
                fileName = $"file{rnd.Next(10000)}";
                fileExtension = fileExtensionList[rnd.Next(fileExtensionList.Length)];

                // .Dispose() !!! Without dispose method the file instances will be locked and unable to be used by other methods. (Can't move the created files etc.)
                File.Create($@"{targetDirectory}\{fileName}{fileExtension}").Dispose(); 
  
                Console.WriteLine($@"Created file: {targetDirectory}\{fileName}{fileExtension}");

                fileAmount--;
            }

            Console.WriteLine($"Created {fileAmountInitial} files. {Environment.NewLine}Press any key to continue!");
            Console.ReadLine();

        }

        public static void Sort()
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

                        if (string.IsNullOrEmpty(targetDirectory))
                        {
                            return;
                        }

                        Console.WriteLine("Enter file amount: ");
                        int fileAmount = int.Parse(Console.ReadLine());

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
                Console.WriteLine("Enter directory path to sort: ");
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
                    Console.WriteLine("[ERROR] Invalid directory path, try again!");
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
                    Console.WriteLine($"[CREATE] directory {fileExt}");
                }

                if (!File.Exists(fileDestName))
                {
                    bool loop = true;
                    while (loop)
                    {
                        try
                        {
                            File.Move(fileSourceName, fileDestName);

                            Console.WriteLine($@"[MOVE] .\{file.Name} >> .\{fileExt}\{file.Name}");
                            loop = false;
                        }
                        catch (IOException)
                        {
                            Console.WriteLine($"[ERROR] The process can't access {file.Name}, the file is in use by another process!");


                            loop = false;
                        }
                        catch (Exception)
                        {
                            loop = true;
                        }


                    }
                }
            }
        }
    }
}
