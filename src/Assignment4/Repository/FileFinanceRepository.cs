using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Reflection;
using FinanceTracker.Core.ExpenseTrackerInterface;
using FinanceTracker.Core.Model;

namespace FinanceTracker.Repository
{
    /// <summary>
    /// Provides a file-based implementation for persisting, retrieving,  and managing financial data records
    /// </summary>
    public class FileFinanceRepository : IFinancialTrackerRepository
    {
        /// <summary>
        /// Hold the file repository name.
        /// </summary>
        public const string FileName = "FinancialTracker.csv";

        /// <summary>
        /// Absolute path of the file to be stored.
        /// </summary>
        public static readonly string FileRepositoryPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        FileName);

        private List<Transaction> _transactionRepository;

        /// <summary>
               /// Initializes a new instance of the <see cref="Repository.FileFinanceRepository"/> class.
               /// </summary>
        public FileFinanceRepository()
        {
            this._transactionRepository = this.GetFinanceRecord();
        }

        /// <inheritdoc />
        public void AddNewTransaction(Transaction transaction)
        {
            this._transactionRepository.Add(transaction);
            this.AppendFinanceRecordInFile(transaction);
        }

        /// <inheritdoc />
        public List<Transaction> GetAllTransaction()
        {
            return this.GetFinanceRecord().ToList();
        }

        /// <inheritdoc />
        public Transaction? GetTransactionCopyUsingId(Guid id)
        {
            Transaction? matchedTransaction = this.SearchTransactionUsingIdFiles(id);
            if (matchedTransaction == null)
            {
                return null;
            }

            return matchedTransaction.CloneTransaction();
        }

        /// <inheritdoc />
        public bool DeleteTransactionById(Guid id)
        {
            Transaction? transactionToBeDeleted = this.SearchTransactionUsingId(id);
            if (transactionToBeDeleted == null)
            {
                return false;
            }

            this._transactionRepository.Remove(transactionToBeDeleted);
            this.WriteFinanceRecord(this._transactionRepository);
            return true;
        }

        /// <inheritdoc />
        public bool EditTransactionById(Guid transactionId, decimal newAmount, DateOnly newDate, string? newSourceOrCategory)
        {
            Transaction? matchedTransaction = this.SearchTransactionUsingId(transactionId);
            if (matchedTransaction is Income income)
            {
                income.Amount = newAmount;
                income.TransactionDate = newDate;
                income.Source = newSourceOrCategory;
                this.WriteFinanceRecord(this._transactionRepository);
                return true;
            }
            else if (matchedTransaction is Expense expense)
            {
                expense.Amount = newAmount;
                expense.TransactionDate = newDate;
                expense.Category = newSourceOrCategory;
                this.WriteFinanceRecord(this._transactionRepository);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public List<T> FilterTransaction<T>()
            where T : Transaction
        {
            List<Transaction> transactions = this.GetFinanceRecord();
            return transactions
                .OfType<T>()
                .ToList();
        }

        private Transaction? SearchTransactionUsingId(Guid id)
        {
            return this._transactionRepository.Find(transaction => transaction.Id == id);
        }

        private Transaction? SearchTransactionUsingIdFiles(Guid id)
        {
            try
            {
                if (!File.Exists(FileName))
                {
                    return default;
                }

                string[] lines = this.ReadFinanceRecord();
                string[] context;
                foreach (string line in lines)
                {
                    context = line.Split(",");
                    if (context.Length != 5)
                    {
                        continue;
                    }

                    Guid.TryParse(context[0], out Guid transactionId);
                    if (id != transactionId)
                    {
                        continue;
                    }

                    Transaction? parsedTransaction = this.ParseTransactionFromCsv(context);
                    if (parsedTransaction != null)
                    {
                        return parsedTransaction;
                    }
                }

                return default;
            }
            catch (IOException)
            {
                throw;
            }
        }

        private void WriteFinanceRecord(List<Transaction> transactions)
        {
            try
            {
                string[] financialRecord = new string[transactions.Count];
                int counter = 0;
                foreach (Transaction transaction in transactions)
                {
                    financialRecord[counter++] = this.CsvSerializer(transaction);
                }

                File.WriteAllLines(FileRepositoryPath, financialRecord);
            }
            catch (IOException)
            {
                throw;
            }
        }

        private void AppendFinanceRecordInFile(Transaction transaction)
        {
            try
            {
                string line = this.CsvSerializer(transaction);
                File.AppendAllText(FileRepositoryPath, line);
            }
            catch (IOException)
            {
                throw;
            }
        }

        private string CsvSerializer(Transaction transaction)
        {
            if (transaction is Income income)
            {
                return $"{income.Id},{income.Amount},{income.TransactionDate},{income.Source},Income{Environment.NewLine}";
            }
            else if (transaction is Expense expense)
            {
                return $"{expense.Id},{expense.Amount},{expense.TransactionDate},{expense.Category},Expense{Environment.NewLine}";
            }

            return string.Empty;
        }

        private List<Transaction> GetFinanceRecord()
        {
            string[] lines = this.ReadFinanceRecord();
            string[] context;
            List<Transaction> transactionList = new List<Transaction>();
            foreach (string line in lines)
            {
                context = line.Split(",");
                if (context.Length != 5)
                {
                    continue;
                }

                Transaction? parsedTransaction = this.ParseTransactionFromCsv(context);
                if (parsedTransaction == null)
                {
                    continue;
                }
                else
                {
                    transactionList.Add(parsedTransaction);
                }
            }

            return transactionList;
        }

        private Transaction? ParseTransactionFromCsv(string[] context)
        {
            Guid.TryParse(context[0], out Guid transactionId);
            decimal.TryParse(context[1], out decimal transactionAmount);
            DateOnly.TryParse(context[2], out DateOnly transactionDate);
            if (context[4] == "Income")
            {
                return new Income(transactionId, transactionAmount, transactionDate, context[3]);
            }
            else if (context[4] == "Expense")
            {
                return new Expense(transactionId, transactionAmount, transactionDate, context[3]);
            }

            return default;
        }

        private string[] ReadFinanceRecord()
        {
            string[] lines;
            try
            {
                if (!File.Exists(FileRepositoryPath))
                {
                    return Array.Empty<string>();
                }

                lines = File.ReadAllLines(FileRepositoryPath);
                return lines;
            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}
