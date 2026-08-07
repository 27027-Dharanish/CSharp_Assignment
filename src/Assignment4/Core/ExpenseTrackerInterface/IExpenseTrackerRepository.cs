using Assignment4.Core.Model;

namespace Assignment4.Core.ExpenseTrackerInterface
{
    /// <summary>
    /// Provides the data persistence abstraction layer for transactions, categories, and financial data within the expense tracker.
    /// </summary>
    public interface IExpenseTrackerRepository
    {
        /// <summary>
        /// Adds a new transaction record to the tracking system and verifies its successful insertion.
        /// </summary>
        /// <param name="transaction">The financial transaction</param>
        /// <returns>True if transaction added successfully else false</returns>
        public bool AddNewTransaction(Transaction transaction);

        /// <summary>
        /// Retrieves a list of all transaction records stored in the tracking system.
        /// </summary>
        /// <returns>List containing all transaction record</returns>
        public List<Transaction> GetAllTransaction();

        /// <summary>
        /// Search transaction using transaction id and return copy of it if necessary.
        /// </summary>
        /// <param name="id">Id of the transaction</param>
        /// <param name="isReturnCopy">Says whether the actual reference or duplicate need</param>
        /// <returns>Transaction that matched with the id</returns>
        public Transaction? SearchTransactionUsingId(int id, bool isReturnCopy = true);

        /// <summary>
        /// Delete the transaction using the transaction id.
        /// </summary>
        /// <param name="id">Transaction id of transaction</param>
        /// <returns>Status of transaction deletion</returns>
        public bool DeleteTransactionById(int id);

        /// <summary>
        /// Filters and retrieves a collection of all recorded expense transactions.
        /// </summary>
        /// <returns>All the expense from transaction</returns>
        public List<Expense> ViewExpense();

        /// <summary>
        /// Filters and retrieves a collection of all recorded income transactions.
        /// </summary>
        /// <returns>All the income from transaction</returns>
        public List<Income> ViewIncome();

        /// <summary>
        /// Updates the financial properties of an existing transaction by its ID.
        /// </summary>
        /// <param name="transactionId">Transaction id of transaction that needed to be edited</param>
        /// <param name="newAmount">New transaction amount</param>
        /// <param name="newDate">New date</param>
        /// <param name="newSourceOrCategory">New source or category</param>
        /// <returns>Status of edit transaction</returns>
        public bool EditTransactionById(int transactionId, decimal newAmount, DateOnly newDate, string? newSourceOrCategory);

        /// <summary>
        /// Get the total number of transaction count.
        /// </summary>
        /// <returns>Count of transaction</returns>
        public int GetTransactionCount();

        /// <summary>
        /// Get the income count.
        /// </summary>
        /// <returns>Income count</returns>
        public int GetIncomeCount();

        /// <summary>
        /// Get the expense count.
        /// </summary>
        /// <returns>expense count</returns>
        public int GetExpenseCount();
    }
}
