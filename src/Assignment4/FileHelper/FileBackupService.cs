using FinanceTracker.Repository;

namespace FinanceTracker.FileHelper
{
    /// <summary>
    /// Handle creating backup copy of the financial data.
    /// </summary>
    public static class FileBackupService
    {
        private static readonly string _backupPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "FinancialTracker_Backup.csv");

        /// <summary>
        /// Creates a backup for the repository data on target path.
        /// </summary>
        /// <returns>True if backup created; Otherwise false if no backup created</returns>
        public static bool CreateBackUp()
        {
            try
            {
                string? sourcePath = FileFinanceRepository.FileRepositoryPath;
                if (File.Exists(sourcePath))
                {
                    DateTime sourceFileLastEdit = File.GetLastWriteTimeUtc(sourcePath);
                    DateTime backupFileLastEdit = File.GetLastWriteTimeUtc(_backupPath);
                    if (backupFileLastEdit == sourceFileLastEdit)
                    {
                        return true;
                    }

                    File.Copy(FileFinanceRepository.FileName, _backupPath, true);
                    File.SetLastWriteTimeUtc(_backupPath, File.GetLastWriteTimeUtc(sourcePath));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}
