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
        public static readonly string FileRepositoryName = "FinancialTracker.csv";
        private List<Transaction> _financeTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileFinanceRepository"/> class.
        /// </summary>
        public FileFinanceRepository()
        {
            this._financeTracker = this.ReadFinanceRecordFromFile();
        }

        /// <inheritdoc />
        public void AddNewTransaction(Transaction transaction)
        {
            if (!File.Exists(FileRepositoryName))
            {
                File.Create(FileRepositoryName);
            }

            this._financeTracker.Add(transaction);
            this.AppendFinanceRecordInFile(transaction);
        }

        /// <inheritdoc />
        public List<Transaction> GetAllTransaction()
        {
            this._financeTracker = this.ReadFinanceRecordFromFile().ToList();
            return this._financeTracker;
        }

        /// <inheritdoc />
        public Transaction? GetTransactionCopyUsingId(Guid id)
        {
            Transaction? matchedTransaction = this.SearchTransactionUsingId(id);
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

            this._financeTracker.Remove(transactionToBeDeleted);
            this.WriteFinanceRecord(this._financeTracker);
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
                this.WriteFinanceRecord(this._financeTracker);
                return true;
            }
            else if (matchedTransaction is Expense expense)
            {
                expense.Amount = newAmount;
                expense.TransactionDate = newDate;
                expense.Category = newSourceOrCategory;
                this.WriteFinanceRecord(this._financeTracker);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public List<T> FilterTransaction<T>()
            where T : Transaction
        {
            List<T> filteredTransaction = new List<T>();
            List<Transaction> transactions = this.ReadFinanceRecordFromFile();
            foreach (Transaction transaction in transactions)
            {
                if (transaction is T matchedTransaction)
                {
                    filteredTransaction.Add(matchedTransaction);
                }
            }

            return filteredTransaction;
        }

        private Transaction? SearchTransactionUsingId(Guid id)
        {
            Transaction? matchedTransaction = this._financeTracker.Find(transaction => transaction.Id == id);
            if (matchedTransaction == null)
            {
                return null;
            }

            return matchedTransaction;
        }

        private void WriteFinanceRecord(List<Transaction> transactions)
        {
            string[] financialRecord = new string[transactions.Count];
            int counter = 0;
            foreach (Transaction transaction in transactions)
            {
                if (transaction is Income income)
                {
                    financialRecord[counter] = $"{income.Id},{income.Amount},{income.TransactionDate},{income.Source},Income";
                }
                else if (transaction is Expense expense)
                {
                    financialRecord[counter] = $"{expense.Id},{expense.Amount},{expense.TransactionDate},{expense.Category},Expense";
                }

                counter++;
            }

            File.WriteAllLines(FileRepositoryName, financialRecord);
        }

        private void AppendFinanceRecordInFile(Transaction transaction)
        {
            if (transaction is Income income)
            {
                File.AppendAllText(FileRepositoryName, $"{income.Id},{income.Amount},{income.TransactionDate},{income.Source},Income\n");
            }
            else if (transaction is Expense expense)
            {
                File.AppendAllText(FileRepositoryName, $"{expense.Id},{expense.Amount},{expense.TransactionDate},{expense.Category},Expense\n");
            }
        }

        private List<Transaction> ReadFinanceRecordFromFile()
        {
            List<Transaction> transactionList = new List<Transaction>();
            if (!File.Exists(FileRepositoryName))
            {
                return transactionList;
            }

            string[] lines = File.ReadAllLines(FileRepositoryName);
            string[] context;
            foreach (string line in lines)
            {
                context = line.Split(",");
                if (context.Length != 5)
                {
                    continue;
                }

                Guid.TryParse(context[0], out Guid transactionId);
                decimal.TryParse(context[1], out decimal transactionAmount);
                DateOnly.TryParse(context[2], out DateOnly transactionDate);
                if (context[4] == "Income")
                {
                    Income income = new Income(transactionId, transactionAmount, transactionDate, context[3]);
                    transactionList.Add(income);
                }
                else if (context[4] == "Expense")
                {
                    Expense expense = new Expense(transactionId, transactionAmount, transactionDate, context[3]);
                    transactionList.Add(expense);
                }
            }

            return transactionList;
        }
    }
}
