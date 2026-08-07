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

        /// <summary>
        /// Specifies the target fields of a transaction that can be edited.
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
            /// Indicates a new transaction should be created and added.
            /// </summary>
            AddNewTransaction = 1,

            /// <summary>
            /// Indicates an existing transaction's details should be displayed.
            /// </summary>
            ViewTransaction,

            /// <summary>
            /// Indicates an existing transaction's details should be modified.
            /// </summary>
            EditTransaction,

            /// <summary>
            /// Indicates an existing transaction should be removed.
            /// </summary>
            DeleteTransaction,

            /// <summary>
            /// Option to gracefully close and exit the application.
            /// </summary>
            Exit,
        }
    }
}
