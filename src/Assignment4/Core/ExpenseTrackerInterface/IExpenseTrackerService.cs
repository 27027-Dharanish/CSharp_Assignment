using Assignment4.Core.Model;

namespace Assignment4.Core.ExpenseTrackerInterface
{
    /// <summary>
    /// Defines the business logic contract for the expense tracker..
    /// </summary>
    public interface IExpenseTrackerService
    {
        /// <summary>
        /// Creates and records a new income transaction.
        /// </summary>
        /// <param name="amount">Income amount</param>
        /// <param name="date">Date of income</param>
        /// <param name="source">Source of income</param>
        /// <returns>Status of income added in repository</returns>
        public bool AddNewIncome(decimal amount, DateOnly date, string? source);

        /// <summary>
        /// Creates and records a new expense transaction.
        /// </summary>
        /// <param name="amount">Expense amount</param>
        /// <param name="date">Date of expense</param>
        /// <param name="category">Category of expense</param>
        /// <returns>Status of expense added in repository</returns>
        public bool AddNewExpense(decimal amount, DateOnly date, string? category);

        /// <summary>
        /// Calculates the total sum of all recorded income transactions
        /// </summary>
        /// <returns>Total income from all source</returns>
        public (decimal, bool) GetTotalIncome();

        /// <summary>
        /// Calculates the total sum of all recorded expense transactions.
        /// </summary>
        /// <returns>Total expense from all source</returns>
        public (decimal, bool) GetTotalExpense();

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
        public bool DeleteTransaction(int id);

        /// <summary>
        ///  Retrieves a list of all recorded income transactions from the repository layer.
        /// </summary>
        /// <returns>Collection of income</returns>
        public List<Income> GetAllIncome();

        /// <summary>
        /// Retrieves a list of all recorded expense transactions from the repository layer.
        /// </summary>
        /// <returns>Collection of all expense</returns>
        public List<Expense> GetAllExpense();

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
        /// Get the list of available income source.
        /// </summary>
        /// <returns>Collection of income source</returns>
        public string[] GetIncomeSource();

        /// <summary>
        /// Checks if a transaction exists and returns it if found.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction.</param>
        /// <returns>A tuple containing a true/false success status and the matched transaction data (or null if not found)</returns>
        public (bool, Transaction?) GetTransactionIfExist(int id);

        /// <summary>
        /// Get the list of available expense categories.
        /// </summary>
        /// <returns>Collection of expense categories</returns>
        public string[] GetExpenseCategories();

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
    }
}
