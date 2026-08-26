namespace FinanceTracker.Core.Model
{
    /// <summary>
    /// Represents a expense available within the expense tracker.
    /// </summary>
    public class Expense : Transaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Expense"/> class.
        /// </summary>
        /// <param name="id">Id for the transaction.</param>
        /// <param name="amount">IAmount of the transaction.</param>
        /// <param name="date">Transaction date.</param>
        /// <param name="category">Category of the expense.</param>
        public Expense(int id, decimal amount, DateOnly date, string? category)
            : base(id, amount, date)
        {
            this.Category = category;
        }

        /// <summary>
        /// Gets or sets the category of the expense.
        /// </summary>
        /// <value>
        /// A string representing the category of the expense.
        /// </value>
        public string? Category { get; set; }

        /// <inheritdoc />
        public override Transaction CloneTransaction()
        {
            Expense cloneTransaction = new Expense(this.Id, this.Amount, this.TransactionDate, this.Category);
            return cloneTransaction;
        }
    }
}
