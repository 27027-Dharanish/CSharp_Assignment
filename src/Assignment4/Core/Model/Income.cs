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
        /// <param name="id">Id for the income</param>
        public Income(int id)
            : base(id)
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
