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
            Utilities.Sort();

            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
