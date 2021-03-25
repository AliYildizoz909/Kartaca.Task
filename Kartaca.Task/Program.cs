using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;

namespace Kartaca.Task
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //var encryptedPath = AppDomain.CurrentDomain.BaseDirectory + "Encrypted";

            string[] filePaths = Directory.GetFiles("Encrypted");

            List<FilePair> filePairs = new List<FilePair>();
            string allByteContent = "";
            for (int i = 0; i < filePaths.Length; i++)
            {
                var filePath = filePaths[i];
                filePairs.Add(new FilePair(filePath, int.Parse(FileHelper.SolveFileName(Path.GetFileName(filePath)))));
            }

            filePairs = filePairs.OrderBy(pair => pair.Order).ToList();

            foreach (var filePair in filePairs)
            {
                allByteContent += " " + File.ReadAllText(filePair.FilePath);
            }

            Console.WriteLine("----------------------------Task descriptions-----------------------------");
            Console.WriteLine(FileHelper.SolveFileContent(allByteContent));
            Console.ReadKey();
        }
    }
}
