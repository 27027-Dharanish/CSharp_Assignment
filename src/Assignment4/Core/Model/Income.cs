namespace FinanceTracker.Core.Model
{
    /// <summary>
    /// Represents a income available within the expense tracker.
    /// </summary>
    public class Income : Transaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Income"/> class.
        /// </summary>
        /// <param name="id">Id for the transaction.</param>
        /// <param name="amount">IAmount of the transaction.</param>
        /// <param name="date">Transaction date.</param>
        /// <param name="source">Source of income.</param>
        public Income(int id, decimal amount, DateOnly date, string? source)
            : base(id, amount, date)
        {
            this.Source = source;
        }

        /// <summary>
        /// Gets or sets the source of income.
        /// </summary>
        /// <value>
        /// A string representing the source of income.
        /// </value>
        public string? Source { get; set; }

        /// <inheritdoc />
        public override Transaction CloneTransaction()
        {
            Income cloneTransaction = new Income(this.Id, this.Amount, this.TransactionDate, this.Source);
            return cloneTransaction;
        }
    }
}
