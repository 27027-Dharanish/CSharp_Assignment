using FinanceTracker.Core.ExpenseTrackerInterface;
using FinanceTracker.Core.Model;

namespace FinanceTracker.Repository
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
        public void AddNewTransaction(Transaction transaction)
        {
            this._financeTracker.Add(transaction);
        }

        /// <inheritdoc />
        public List<Transaction> GetAllTransaction()
        {
            return this._financeTracker.ToList();
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

            return this._financeTracker.Remove(transactionToBeDeleted);
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

        private Transaction? SearchTransactionUsingId(Guid id)
        {
            Transaction? matchedTransaction = this._financeTracker.Find(transaction => transaction.Id == id);
            if (matchedTransaction == null)
            {
                return null;
            }

            return matchedTransaction;
        }
    }
}
