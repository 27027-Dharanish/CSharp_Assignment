namespace Assignment4.Model
{
    /// <summary>
    /// Represents a expense available within the expense tracker.
    /// </summary>
    public class Expense
    {
        /// <summary>
        /// Gets or sets the amount for the expense.
        /// </summary>
        /// <value>
        /// A decimal representing amount for the expense.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date on which the expense is added.
        /// </summary>
        /// <value>
        /// A  representing amount for the expense.
        /// </value>
        public DateTime ExpenseAddedDate { get; set; }
    }
}
