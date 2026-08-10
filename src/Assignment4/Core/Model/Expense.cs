namespace Assignment4.Core.Model
{
    /// <summary>
    /// Represents a expense available within the expense tracker.
    /// </summary>
    public class Expense : Transaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Expense"/> class.
        /// </summary>
        /// <param name="id">Id for the transaction</param>
        /// <param name="amount">IAmount of the transaction</param>
        /// <param name="date">Transaction date</param>
        public Expense(int id, decimal amount, DateOnly date)
            : base(id, amount, date)
        {
        }

        /// <summary>
        /// Gets or sets the category of the expense.
        /// </summary>
        /// <value>
        /// A string representing the category of the expense.
        /// </value>
        public string? Category { get; set; }
    }
}
