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
        /// <param name="id">Id for the expense</param>
        public Expense(int id)
            : base(id)
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
