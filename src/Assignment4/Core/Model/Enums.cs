namespace Assignment4.Core.Model
{
    /// <summary>
    /// Acts as a central container for all application enums.
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// Specifies the financial option available in expense tracker.
        /// </summary>
        public enum FinancialOption
        {
            /// <summary>
            /// Option to view the overall financial summary and total balance.
            /// </summary>
            ViewSummary = 1,

            /// <summary>
            /// Option to add, view, or modify income transactions.
            /// </summary>
            ManageIncome = 2,

            /// <summary>
            /// Option to add, view, or modify expense transactions.
            /// </summary>
            ManageExpense = 3,

            /// <summary>
            /// Option to gracefully close and exit the application.
            /// </summary>
            Exit = 4,
        }
    }
}
