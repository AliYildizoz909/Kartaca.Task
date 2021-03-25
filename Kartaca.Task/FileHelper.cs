using System;
using System.Collections.Generic;
using System.Text;

namespace Kartaca.Task
{
    public class FileHelper
    {
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

            return Encoding.ASCII.GetString((byte[]) GetBytesFromBinaryString(content));
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
    }
}
