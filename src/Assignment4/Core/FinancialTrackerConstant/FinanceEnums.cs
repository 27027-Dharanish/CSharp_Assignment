namespace FinanceTracker.Core.FinancialTrackerConstant
{
    /// <summary>
    /// Specifies the financial option available in expense tracker.
    /// </summary>
    public enum FinancialOption
    {
        /// <summary>
        /// Option to view the summary.
        /// </summary>
        Summary = 1,

        /// <summary>
        /// Option to add, view, or modify income.
        /// </summary>
        Income,

        /// <summary>
        /// Option to add, view, or modify expense transactions.
        /// </summary>
        Expense,

        /// <summary>
        /// Option to create backup for file repository.
        /// </summary>
        BackUpRepository,

        /// <summary>
        /// Option to close and exit the application.
        /// </summary>
        Exit,
    }

    /// <summary>
    /// Specifies the fields of a transaction.
    /// </summary>
    public enum TransactionField
    {
        /// <summary>
        /// The financial value of the transaction.
        /// </summary>
        Amount = 1,

        /// <summary>
        /// The date when the transaction occurred.
        /// </summary>
        TransactionDate,

        /// <summary>
        /// The origin or classification label of the transaction.
        /// </summary>
        SourceOrCategory,
    }

    /// <summary>
    /// Specifies the type of operation to perform on a transaction.
    /// </summary>
    public enum TransactionOperation
    {
        /// <summary>
        /// Indicates a new transaction should be added.
        /// </summary>
        Add = 1,

        /// <summary>
        /// Indicates an existing transaction's details should be displayed.
        /// </summary>
        View,

        /// <summary>
        /// Indicates an existing transaction's details should be modified.
        /// </summary>
        Edit,

        /// <summary>
        /// Indicates an existing transaction should be removed.
        /// </summary>
        Delete,

        /// <summary>
        /// Option to go back to main menu.
        /// </summary>
        Back,
    }
}
