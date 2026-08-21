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

    /// <summary>
    /// Specifies the reason why an input considered as invalid.
    /// </summary>
    public enum ValidationStatus
    {
        /// <summary>
        /// The value contains characters that are not digits.
        /// </summary>
        NotDigit = 1,

        /// <summary>
        /// The value is null, empty, or consists only of whitespace.
        /// </summary>
        NullOrWhiteSpace,

        /// <summary>
        /// The value is outside the allowed minimum or maximum bounds.
        /// </summary>
        ExceededRange,

        /// <summary>
        /// The date provided is in the future but must be in the past or present.
        /// </summary>
        FutureDate,

        /// <summary>
        /// The date string does not match the required format.
        /// </summary>
        DateFormat,

        /// <summary>
        /// The numeric value is exactly zero.
        /// </summary>
        Zero,

        /// <summary>
        /// The numeric value is less than zero.
        /// </summary>
        Negative,

        /// <summary>
        /// The value is in correct format and passed validation.
        /// </summary>
        Success,

        /// <summary>
        /// The value representing to use current date.
        /// </summary>
        UseCurrentDate,
    }
}
