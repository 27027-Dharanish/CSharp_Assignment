using System.Runtime.CompilerServices;

namespace Assignment4.FileHelper
{
    /// <summary>
    /// Handles application logging by writing messages to a log file.
    /// </summary>
    public class Logger
    {
        private static readonly string _loggerFile = "FinancialTrackerLog.txt";

        /// <summary>
        /// Write the log in to file.
        /// </summary>
        /// <param name="status">Status of the log</param>
        /// <param name="message">Log message</param>
        public static void WriteLog(string status, string message)
        {
            File.AppendAllText(_loggerFile, $"{DateTime.Now} - [{status}] {message}\n");
        }
    }
}
