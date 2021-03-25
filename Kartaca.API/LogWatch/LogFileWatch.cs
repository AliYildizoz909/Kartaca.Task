using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Kartaca.API.LogWatch
{
    public class LogFileWatch
    {
        private FileSystemWatcher _fileWatcher;

        public LogFileWatch()
        {
            var logpath = GetLogsPath();
            if (!Directory.Exists(logpath))
            {
                Directory.CreateDirectory(GetLogsPath());
            }
            _fileWatcher = new FileSystemWatcher(GetLogsPath());
            _fileWatcher.Changed += OnChangedLogFile;
            _fileWatcher.Filter = "*.log";
            _fileWatcher.IncludeSubdirectories = true;
            _fileWatcher.EnableRaisingEvents = true;
        }

        private static bool ExistsHttpMethod(string line)
        {
            string[] httpMethods = { "get", "put", "post", "delete" };
            bool res = false;
            string lowerLine = line.ToLower();
            foreach (var httpMethod in httpMethods)
            {
                res = lowerLine.Contains(httpMethod.ToLower());
                if (res) break;
            }

            return res;
        }
        public static void OnChangedLogFile(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            var lastLine = File.ReadLines(e.FullPath).Last();
            if (ExistsHttpMethod(lastLine))
            {
                Console.WriteLine($"Line: {lastLine}");
            }
        }

        public string GetLogsPath()
        {
            var path = Path.Combine(Directory.GetParent(Directory
                    .GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName)
                        .FullName).FullName).FullName, "Logs");
            if (!Directory.Exists(path))
            {
                path = Directory.CreateDirectory(Path.Combine(Directory.CreateDirectory(path).FullName, "requests")).FullName;
            }
            else
            {
                path = Path.Combine(path, "requests");
                if (!Directory.Exists(path))
                {
                    path = Directory.CreateDirectory(path).FullName;
                }
            }
            return path;
        }
    }
}
