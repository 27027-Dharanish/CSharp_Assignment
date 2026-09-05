namespace FinanceTracker.Core.Model
{
    /// <summary>
    /// Serves as the foundational entity for all financial records.
    /// </summary>
    public abstract class Transaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="id">Id for the transaction</param>
        /// <param name="amount">IAmount of the transaction</param>
        /// <param name="date">Transaction date</param>
        public Transaction(Guid id, decimal amount, DateOnly date)
        {
            this.Id = id;
            this.Amount = amount;
            this.TransactionDate = date;
        }

        /// <summary>
        /// Gets the unique identifier for the transaction
        /// </summary>
        /// <value>
        /// A <see cref="Guid"/> representing the globally unique value for a transaction.
        /// </value>
        public Guid Id { get; init; }

        /// <summary>
        /// Gets or sets the amount for the expense.
        /// </summary>
        /// <value>
        /// A <see cref="decimal"/> representing amount for the expense.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date on which transaction held.
        /// </summary>
        /// <value>
        /// A <see cref="DateOnly"/> representing the date on which transaction occurred.
        /// </value>
        public DateOnly TransactionDate { get; set; }

        /// <summary>
        /// Clone the transaction.
        /// </summary>
        /// <returns>Cloned transaction.</returns>
        public abstract Transaction CloneTransaction();
    }
}
