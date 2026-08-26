using FinanceTracker.Core.Model;

namespace FinanceTracker.Core.ExpenseTrackerInterface
{
    /// <summary>
    /// Defines the business logic contract for the expense tracker..
    /// </summary>
    public interface IFinancialTrackerService
    {
        /// <summary>
        /// Creates and records a new income transaction.
        /// </summary>
        /// <param name="amount">Income amount</param>
        /// <param name="date">Date of income</param>
        /// <param name="context">Source of transaction</param>
        /// <param name="isIncome">True if transaction is income; Otherwise transaction is expense</param>
        /// <returns>Status of income added in repository</returns>
        public bool CreateNewTransaction(decimal amount, DateOnly date, string? context, bool isIncome);

        /// <summary>
        /// Calculate the total transaction amount.
        /// </summary>
        /// <typeparam name="T">Transaction type</typeparam>
        /// <returns>Tuple containing of total amount and boolean value indicating whether the calculation is success or not</returns>
        public (decimal, bool) GetTotalTransactionAmount<T>()
            where T : Transaction;

        /// <summary>
        /// Calculates the remaining net balance by subtracting total expenses from total income.
        /// </summary>
        /// <returns>Remaining balance amount after all expense</returns>
        public decimal GetTotalBalanceAmount();

        /// <summary>
        /// Deletes a specific transaction record using its unique identifier.
        /// </summary>
        /// <param name="id">Id of the transaction</param>
        /// <returns>Status of transaction deletion</returns>
        public bool DeleteTransaction(Guid id);

        /// <summary>
        ///  Retrieves a list of all recorded transactions from the repository layer.
        /// </summary>
        /// <returns>Collection of transaction</returns>
        public List<Transaction> GetAllTransaction();

        /// <summary>
        /// Updates the financial properties of an existing transaction by its ID.
        /// </summary>
        /// <param name="transactionId">Transaction id of transaction that needed to be edited</param>
        /// <param name="newAmount">New transaction amount</param>
        /// <param name="newDate">New date</param>
        /// <param name="newSourceOrCategory">New source or category</param>
        /// <returns>Status of edit transaction</returns>
        public bool EditTransactionById(Guid transactionId, decimal newAmount, DateOnly newDate, string? newSourceOrCategory);

        /// <summary>
        /// Checks whether a transaction exists and returns it if found.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction.</param>
        /// <returns>A tuple containing a true/false success status and the matched transaction data (or null if not found)</returns>
        public (bool, Transaction?) GetTransactionIfExist(Guid id);

        /// <summary>
        /// Get the expense count.
        /// </summary>
        /// <returns>No.of transaction occurred</returns>
        public int GetExpenseCount();

        /// <summary>
        /// Get the income count.
        /// </summary>
        /// <returns>No.of transaction occurred</returns>
        public int GetIncomeCount();

        /// <summary>
        /// Retrieves a filtered list of transactions.
        /// </summary>
        /// <typeparam name="T">The specific type of transaction</typeparam>
        /// <returns>The filtered transactions matching the requested type.</returns>
        public List<T> GetFilteredTransaction<T>()
            where T : Transaction;

        /// <summary>
        /// Check whether the amount is valid.
        /// </summary>
        /// <param name="amount">Amount to be validated.</param>
        /// <returns>True if amount is valid; Otherwise false.</returns>
        public bool IsValidateAmount(decimal amount);
    }
}