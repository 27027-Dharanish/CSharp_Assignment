namespace FinanceTracker.Core.FinancialTrackerConstant
{
    /// <summary>
    /// Finance tracker constant.
    /// </summary>
    public class FinanceConstant
    {
        /// <summary>
        /// Specifies transaction ID cannot be found.
        /// </summary>
        public const string TransactionIdNotExistMessage = "Transaction Id not exist!!";

        /// <summary>
        /// Specifies invalid message.
        /// </summary>
        public const string InvalidChoiceMessage = "Invalid choice!!";

        /// <summary>
        /// Financial tracker menu.
        /// </summary>
        public static readonly string[] FinancialMenu = { "View Summary", "Manage Income", "Manage Expense", "BackUp Repository", "Exit" };

        /// <summary>
        /// Financial tracker income menu.
        /// </summary>
        public static readonly string[] IncomeMenu = { "Add New Income", "View All Income", "Edit Income", "Delete Income", "Exit" };

        /// <summary>
        /// Financial tracker expense menu.
        /// </summary>
        public static readonly string[] ExpenseMenu = { "Add New Expense", "View All Expense", "Edit Expense", "Delete Expense", "Exit" };

        /// <summary>
        /// List of income source available.
        /// </summary>
        public static readonly string[] IncomeSources = { "Salary", "Freelance", "Investment", "Business", "Rental", "Pocket Money", "Others" };

        /// <summary>
        /// List of expense category available.
        /// </summary>
        public static readonly string[] ExpenseCategories = { "Housing", "Groceries", "Transportation", "Healthcare", "Entertainment", "Insurance", "Food", "Shopping", "Others" };
    }
}
