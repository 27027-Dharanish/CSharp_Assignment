using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Core.Model;

namespace Assignment4.Repository
{
    /// <summary>
    /// Provides a file-based implementation for persisting, retrieving,  and managing financial data records
    /// </summary>
    public class FileFinanceRepository : IFinancialTrackerRepository
    {
        private readonly string _fileRepositoryName = "FinancialTracker.csv";
        private List<Transaction> _financeTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileFinanceRepository"/> class.
        /// </summary>
        public FileFinanceRepository()
        {
            this._financeTracker = this.ReadFinanceRecordFromFile();
        }

        /// <inheritdoc />
        public bool AddNewTransaction(Transaction transaction)
        {
            int previousTransactionCount = this.GetTransactionCount();
            this._financeTracker.Add(transaction);
            if (previousTransactionCount == this.GetTransactionCount())
            {
                return false;
            }

            this.WriteFinanceRecord(this._financeTracker);
            return true;
        }

        /// <inheritdoc />
        public List<Transaction> GetAllTransaction()
        {
            this._financeTracker = this.ReadFinanceRecordFromFile();
            return this._financeTracker;
        }

        /// <inheritdoc />
        public Transaction? SearchTransactionUsingId(int id, bool isReturnCopy = true)
        {
            Transaction? matchedTransaction = this._financeTracker.Find(transaction => transaction != null && transaction.Id == id);
            if (matchedTransaction == null)
            {
                return null;
            }
            else if (isReturnCopy)
            {
                return this.CreateDuplicateTransaction(matchedTransaction);
            }

            return matchedTransaction;
        }

        /// <inheritdoc />
        public bool DeleteTransactionById(int id)
        {
            Transaction? transactionToBeDeleted = this.SearchTransactionUsingId(id, false);
            if (transactionToBeDeleted == null)
            {
                return false;
            }

            this._financeTracker.Remove(transactionToBeDeleted);
            this.WriteFinanceRecord(this._financeTracker);
            return true;
        }

        /// <inheritdoc />
        public bool EditTransactionById(int transactionId, decimal newAmount, DateOnly newDate, string? newSourceOrCategory)
        {
            Transaction? matchedTransaction = this.SearchTransactionUsingId(transactionId, false);
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
            List<Transaction> transactions = this.GetAllTransaction();
            foreach (Transaction transaction in transactions)
            {
                if (transaction is T matchedTransaction)
                {
                    filteredTransaction.Add(matchedTransaction);
                }
            }

            return filteredTransaction;
        }

        /// <summary>
        /// Get the file repository name.
        /// </summary>
        /// <returns>File name</returns>
        protected string GetFileRepositoryName()
        {
            return this._fileRepositoryName;
        }

        private Transaction? CreateDuplicateTransaction(Transaction transaction)
        {
            if (transaction is Income income)
            {
                Income incomeCopy = new Income(income.Id, income.Amount, income.TransactionDate);
                incomeCopy.Source = income.Source;
                return incomeCopy;
            }
            else if (transaction is Expense expense)
            {
                Expense expenseCopy = new Expense(expense.Id, expense.Amount, expense.TransactionDate);
                expenseCopy.Category = expense.Category;
                return expenseCopy;
            }

            return null;
        }

        private int GetTransactionCount()
        {
            return this._financeTracker.Count;
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

            File.WriteAllLines(this._fileRepositoryName, financialRecord);
        }

        private void AppendFinanceRecordInFile(Transaction transaction)
        {
            if (transaction is Income income)
            {
                File.AppendAllText(this._fileRepositoryName, $"{income.Id},{income.Amount},{income.TransactionDate},{income.Source},Income\n");
            }
            else if (transaction is Expense expense)
            {
                File.AppendAllText(this._fileRepositoryName, $"{expense.Id},{expense.Amount},{expense.TransactionDate},{expense.Category},Expense\n");
            }
        }

        private List<Transaction> ReadFinanceRecordFromFile()
        {
            List<Transaction> transactionList = new List<Transaction>();
            if (!File.Exists(this._fileRepositoryName))
            {
                return transactionList;
            }

            string[] lines = File.ReadAllLines(this._fileRepositoryName);
            string[] context;
            foreach (string line in lines)
            {
                context = line.Split(",");
                int.TryParse(context[0], out int transactionId);
                decimal.TryParse(context[1], out decimal transactionAmount);
                DateOnly.TryParse(context[2], out DateOnly transactionDate);
                if (context[4] == "Income")
                {
                    Income income = new Income(transactionId, transactionAmount, transactionDate);
                    income.Source = context[3];
                    transactionList.Add(income);
                }
                else if (context[4] == "Expense")
                {
                    Expense expense = new Expense(transactionId, transactionAmount, transactionDate);
                    expense.Category = context[3];
                    transactionList.Add(expense);
                }
            }

            return transactionList;
        }
    }
}