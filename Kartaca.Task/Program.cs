using System;
using System.Text;

namespace Kartaca.Task
{
    class Program
    {
        static void Main(string[] args)
        {

        }
        public string SolveFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("File name is null or empty.");
            }
            return Encoding.UTF8.GetString(Convert.FromBase64String(fileName));
        }
    }
}
