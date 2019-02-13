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
    }
}
