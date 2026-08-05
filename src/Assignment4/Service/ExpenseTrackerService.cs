using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Core.Model;
using Assignment4.Repository;

namespace Assignment4.Service
{
    /// <summary>
    /// Provides core business logic for managing income, processing expense, and interacting with the expense tracker repository.
    /// </summary>
    public class ExpenseTrackerService
    {
        private readonly IExpenseTrackerRepository _financialRepository;
        private int _transactionIdCounter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseTrackerService"/> class.
        /// </summary>
        public ExpenseTrackerService()
        {
            this._financialRepository = new FinanceRepository();
            this._transactionIdCounter = 0;
        }

        /// <summary>
        /// Creates and records a new income transaction.
        /// </summary>
        /// <param name="amount">Income amount</param>
        /// <param name="date">Date of income</param>
        /// <param name="source">Source of income</param>
        /// <returns>Status of income added in repository</returns>
        public bool AddNewIncome(decimal amount, DateTime date, string? source)
        {
            this._transactionIdCounter++;
            Income newIncome = new Income(this._transactionIdCounter);
            newIncome.Amount = amount;
            newIncome.TransactionDate = date;
            newIncome.Source = source;
            return this._financialRepository.AddNewTransaction(newIncome);
        }

        /// <summary>
        /// Creates and records a new expense transaction.
        /// </summary>
        /// <param name="amount">Expense amount</param>
        /// <param name="date">Date of expense</param>
        /// <param name="category">Category of expense</param>
        /// <returns>Status of expense added in repository</returns>
        public bool AddNewExpense(decimal amount, DateTime date, string? category)
        {
            this._transactionIdCounter++;
            Expense newExpense = new Expense(this._transactionIdCounter);
            newExpense.Amount = amount;
            newExpense.TransactionDate = date;
            newExpense.Category = category;
            return this._financialRepository.AddNewTransaction(newExpense);
        }
        public List<Transaction>
    }
}
