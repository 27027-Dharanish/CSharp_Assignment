namespace FileAndStream
{
    /// <summary>
    /// Log the error and thread safe.
    /// </summary>
    public class ThreadSafeLogger
    {
        private const string LogFileName = "ErrorLog.txt";
        private static readonly object _lock = new object();

        /// <summary>
        /// Log the error with the current time.
        /// </summary>
        /// <param name="message">Error message to be logged.</param>
        public static void LogError(string message)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR - {message}{Environment.NewLine}";
            lock (_lock)
            {
                File.AppendAllText(LogFileName, logMessage);
            }
        }

        /// <summary>
        /// The logging method so that each user's error is logged to a unique file.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <param name="message">Error message to be logged.</param>
        public static void LogErrorIndependent(string userId, string message)
        {
            string userLogFile = $"ErrorLog_{userId}.txt";
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR - {message}{Environment.NewLine}";
            File.AppendAllText(userLogFile, logMessage);
        }
    }
}
