using FinanceTracker.Core.Model;

namespace FinanceTracker.Core.ExpenseTrackerInterface
{
    /// <summary>
    /// Provides the data persistence abstraction layer for transactions, categories, and financial data within the expense tracker.
    /// </summary>
    public interface IFinancialTrackerRepository
    {
        /// <summary>
        /// Adds a new transaction record to the tracking system and verifies its successful insertion.
        /// </summary>
        /// <param name="transaction">The financial transaction.</param>
        public void AddNewTransaction(Transaction transaction);

        /// <summary>
        /// Retrieves a list of all transaction records stored in the tracking system.
        /// </summary>
        /// <returns>List containing all transaction record.</returns>
        public List<Transaction> GetAllTransaction();

        /// <summary>
        /// Search transaction using transaction id and return copy of it if necessary.
        /// </summary>
        /// <param name="id">Id of the transaction.</param>
        /// <returns>Copy of the transaction that matched with the id.</returns>
        public Transaction? GetTransactionCopyUsingId(int id);

        /// <summary>
        /// Delete the transaction using the transaction id.
        /// </summary>
        /// <param name="id">Transaction id of transaction.</param>
        /// <returns>Status of transaction deletion.</returns>
        public bool DeleteTransactionById(int id);

        /// <summary>
        /// Updates the financial properties of an existing transaction by its ID.
        /// </summary>
        /// <param name="transactionId">Transaction id of transaction that needed to be edited.</param>
        /// <param name="newAmount">New transaction amount.</param>
        /// <param name="newDate">New date.</param>
        /// <param name="newSourceOrCategory">New source or category.</param>
        /// <returns>Status of edit transaction.</returns>
        public bool EditTransactionById(int transactionId, decimal newAmount, DateOnly newDate, string? newSourceOrCategory);

        /// <summary>
        /// Filter the transaction into income or expense.
        /// </summary>
        /// <typeparam name="T">Indicate the transaction type.</typeparam>
        /// <returns>The count of the particular transaction.</returns>
        public int GetFilteredTransactionCount<T>()
            where T : Transaction;
    }
}
