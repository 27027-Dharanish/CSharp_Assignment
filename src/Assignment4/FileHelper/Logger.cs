namespace FinanceTracker.FileHelper
{
    /// <summary>
    /// Handles application logging by writing messages to a log file.
    /// </summary>
    public static class Logger
    {
        private static readonly string _loggerFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "FinancialTrackerLog.txt");

        /// <summary>
        /// Write the information log in to file.
        /// </summary>
        /// <param name="message">Log message.</param>
        public static void LogInformation(string message)
        {
            File.AppendAllText(_loggerFile, $"{DateTime.Now} - [Info] {message}{Environment.NewLine}");
        }

        /// <summary>
        /// Write the error log in the file.
        /// </summary>
        /// <param name="error">Error message.</param>
        public static void LogError(string error)
        {
            File.AppendAllText(_loggerFile, $"{DateTime.Now} - [Error] {error}{Environment.NewLine}");
        }
    }
}
