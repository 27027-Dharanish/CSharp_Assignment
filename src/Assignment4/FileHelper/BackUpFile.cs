using System.Runtime.CompilerServices;
using Assignment4.Repository;

namespace Assignment4.FileHelper
{
    /// <summary>
    /// BackUp the file repository
    /// </summary>
    public class BackUpFile : FileFinanceRepository
    {
        private static readonly string _backupPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "FinancialTracker_Backup.csv");

        /// <summary>
        /// Create a backfile on target path.
        /// </summary>
        /// <returns>True if backup created; Otherwise false if no backup created</returns>
        public bool CreateBackUp()
        {
            if (File.Exists(this.GetFileRepositoryName()))
            {
                File.Copy(this.GetFileRepositoryName(), _backupPath, true);
                return true;
            }

            return false;
        }
    }
}
