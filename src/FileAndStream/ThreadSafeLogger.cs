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
        public void LogError(string message)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR - {message}{Environment.NewLine}";
            lock (_lock)
            {
                File.AppendAllText(LogFileName, logMessage);
            }
        }
    }
}
