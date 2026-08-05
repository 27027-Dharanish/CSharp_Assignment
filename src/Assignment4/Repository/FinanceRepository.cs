using System.Security.Principal;
using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Core.Model;

namespace Assignment4.Repository
{
    /// <summary>
    /// Provides a centralized data repository for storing, retrieving expense info entities.
    /// </summary>
    public class FinanceRepository : IExpenseTrackerRepository
    {
        private readonly List<Transaction> _financeTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceRepository"/> class.
        /// </summary>
        public FinanceRepository()
        {
            this._financeTracker = new ();
        }

        /// <summary>
        /// Adds a new transaction record to the tracking system and verifies its successful insertion.
        /// </summary>
        /// <param name="transaction">The financial transaction</param>
        /// <returns>True if transaction added successfully else false</returns>
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

        /// <summary>
        /// Retrieves a list of all transaction records stored in the tracking system.
        /// </summary>
        /// <returns>List containing all transaction record</returns>
        public List<Transaction> GetAllTransaction()
        {
            return this._financeTracker;
        }

        /// <summary>
        /// Search transaction using transaction id and return copy of it if necessary.
        /// </summary>
        /// <param name="id">Id of the transaction</param>
        /// <param name="isReturnCopy">Says whether the actual reference or duplicate need</param>
        /// <returns>Transaction that matched with the id</returns>
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

        /// <summary>
        /// Delete the transaction using the transaction id.
        /// </summary>
        /// <param name="id">Transaction id of transaction</param>
        /// <returns>Status of transaction deletion</returns>
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

        private int GetTransactionCount()
        {
            return this._financeTracker.Count;
        }

        private Transaction? CreateDuplicateTransaction(Transaction transaction)
        {
            if (transaction is Income income)
            {
                Income incomeCopy = new Income(income.Id);
                incomeCopy.Amount = income.Amount;
                incomeCopy.TransactionDate = income.TransactionDate;
                incomeCopy.Source = income.Source;
                return incomeCopy;
            }
            else if (transaction is Expense expense)
            {
                Expense expenseCopy = new Expense(expense.Id);
                expenseCopy.Amount = expense.Amount;
                expenseCopy.TransactionDate = expense.TransactionDate;
                expenseCopy.Category = expense.Category;
                return expenseCopy;
            }

            return null;
        }
    }
}
