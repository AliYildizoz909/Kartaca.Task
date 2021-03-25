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
        static void Main(string[] args)
        {
            var encryptedPath = AppDomain.CurrentDomain.BaseDirectory + "Encrypted";

            string[] filePaths = Directory.GetFiles(encryptedPath);

            List<FilePair> filePairs = new List<FilePair>();
            string allByteContent = "";
            for (int i = 0; i < filePaths.Length; i++)
            {
                var filePath = filePaths[i];
                filePairs.Add(new FilePair(filePath, int.Parse(SolveFileName(Path.GetFileName(filePath)))));
            }

            filePairs = filePairs.OrderBy(pair => pair.Order).ToList();

            foreach (var filePair in filePairs)
            {
                allByteContent += " " + File.ReadAllText(filePair.FilePath);
            }

            Console.WriteLine("----------------------------Task descriptions-----------------------------");
            Console.WriteLine(SolveFileContent(allByteContent));

        }
        public static string SolveFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("File name is null or empty.");
            }
            return System.Text.ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(fileName));
        }

        public static string SolveFileContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new Exception("Content is null or empty.");
            }

            return Encoding.ASCII.GetString(GetBytesFromBinaryString(content));
        }
        public static Byte[] GetBytesFromBinaryString(String binary)
        {
            var list = new List<Byte>();
            var binaryStringArray2 = binary.Split(" ");

            for (int i = 0; i < binaryStringArray2.Length; i++)
            {
                if (binaryStringArray2[i].Length == 8)
                {
                    list.Add(Convert.ToByte(binaryStringArray2[i], 2));
                }
            }
            return list.ToArray();
        }
        public class FilePair
        {
            public FilePair(string filePath, int order)
            {
                FilePath = filePath;
                Order = order;
            }

            public int Order { get; set; }
            public string FilePath { get; set; }
        }
    }
}
