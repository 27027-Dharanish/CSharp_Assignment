using System.Linq.Expressions;
using FinanceTracker.Repository;

namespace FinanceTracker.FileHelper
{
    /// <summary>
    /// Handle creating backup copy of the financial data.
    /// </summary>
    public class BackUpFile
    {
        private static readonly string _backupPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "FinancialTracker_Backup.csv");

        /// <summary>
        /// Create a backfile on target path.
        /// </summary>
        /// <returns>True if backup created; Otherwise false if no backup created</returns>
        public bool CreateBackUp()
        {
            try
            {
                string? sourcePath = FileFinanceRepository.FileRepositoryName;
                if (File.Exists(sourcePath))
                {
                    DateTime sourceFileLastEdit = File.GetLastWriteTimeUtc(sourcePath);
                    DateTime backupFileLastEdit = File.GetLastWriteTimeUtc(sourcePath);
                    if (backupFileLastEdit == sourceFileLastEdit)
                    {
                        return true;
                    }

                    File.Copy(FileFinanceRepository.FileRepositoryName, _backupPath, true);
                    File.SetLastWriteTimeUtc(sourcePath, File.GetLastWriteTimeUtc(_backupPath));
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
