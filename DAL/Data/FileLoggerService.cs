using System;
using System.Collections.Generic;
using System.IO; // Needed for File operations
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class FileLoggerService : ILoggerService
    {
        private static readonly object _lock = new(); // Proper locking

        private readonly string logFile = "log.txt";

        public FileLoggerService()
        {
            if (!File.Exists(logFile))
                File.Create(logFile).Close();
        }

        public void Log(string message)
        {
            lock (_lock)
            {
                File.AppendAllText(logFile, $"{DateTime.Now}: {message}{Environment.NewLine}");
            }
        }

        // C# 13: params collection expression
        public void LogBatch(params string[] messages)
        {
            messages.ToList().ForEach(Log); // C#13 feature: Method group natural type
        }
    }
}
