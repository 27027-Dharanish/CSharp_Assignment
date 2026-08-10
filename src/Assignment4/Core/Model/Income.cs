using System.Transactions;

namespace Assignment4.Core.Model
{
    /// <summary>
    /// Represents a income available within the expense tracker.
    /// </summary>
    public class Income : Transaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Income"/> class.
        /// </summary>
        /// <param name="id">Id for the transaction</param>
        /// <param name="amount">IAmount of the transaction</param>
        /// <param name="date">Transaction date</param>
        public Income(int id, decimal amount, DateOnly date)
            : base(id, amount, date)
        {
        }

        /// <summary>
        /// Gets or sets the source of income.
        /// </summary>
        /// <value>
        /// A string representing the source of income.
        /// </value>
        public string? Source { get; set; }
    }
}
