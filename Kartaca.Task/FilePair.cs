using System;
using System.Collections.Generic;
using System.Text;

namespace Kartaca.Task
{
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
