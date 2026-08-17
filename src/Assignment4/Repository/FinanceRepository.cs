using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Core.Model;

namespace Assignment4.Repository
{
    /// <summary>
    /// Provides a centralized data repository for storing, retrieving expense info entities.
    /// </summary>
    public class FinanceRepository : IFinancialTrackerRepository
    {
        private readonly List<Transaction> _financeTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceRepository"/> class.
        /// </summary>
        public FinanceRepository()
        {
            this._financeTracker = new ();
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

            return true;
        }

        /// <inheritdoc />
        public List<Transaction> GetAllTransaction()
        {
            return this._financeTracker.ToList();
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
                return true;
            }
            else if (matchedTransaction is Expense expense)
            {
                expense.Amount = newAmount;
                expense.TransactionDate = newDate;
                expense.Category = newSourceOrCategory;
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
    }
}
