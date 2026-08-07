using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Core.Model;
using Assignment4.Repository;

namespace Assignment4.Service
{
    /// <summary>
    /// Provides core business logic for managing income, processing expense, and interacting with the expense tracker repository.
    /// </summary>
    public class ExpenseTrackerService : IExpenseTrackerService
    {
        private readonly string[] _incomeSources = { "Salary", "Freelance", "Investment", "Business", "Rental", "Pocket Money", "Others" };
        private readonly string[] _expenseCategories = { "Housing", "Groceries", "Transportation", "Healthcare", "Entertainment", "Insurance", "Food", "Shopping", "Others" };

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
        public bool AddNewIncome(decimal amount, DateOnly date, string? source)
        {
            this._transactionIdCounter++;
            Income newIncome = new (this._transactionIdCounter);
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
        public bool AddNewExpense(decimal amount, DateOnly date, string? category)
        {
            this._transactionIdCounter++;
            Expense newExpense = new (this._transactionIdCounter);
            newExpense.Amount = amount;
            newExpense.TransactionDate = date;
            newExpense.Category = category;
            return this._financialRepository.AddNewTransaction(newExpense);
        }

        /// <summary>
        /// Calculates the total sum of all recorded income transactions
        /// </summary>
        /// <returns>Total income from all source</returns>
        public decimal GetTotalIncome()
        {
            List<Transaction> transactions = this._financialRepository.GetAllTransaction();
            decimal totalIncome = 0;
            foreach (Transaction transaction in transactions)
            {
                if (transaction is Income income)
                {
                    totalIncome += income.Amount;
                }
            }

            return totalIncome;
        }

        /// <summary>
        /// Calculates the total sum of all recorded expense transactions.
        /// </summary>
        /// <returns>Total expense from all source</returns>
        public decimal GetTotalExpense()
        {
            List<Transaction> transactions = this._financialRepository.GetAllTransaction();
            decimal totalExpense = 0;
            foreach (Transaction transaction in transactions)
            {
                if (transaction is Expense expense)
                {
                    totalExpense += expense.Amount;
                }
            }

            return totalExpense;
        }

        /// <summary>
        /// Calculates the remaining net balance by subtracting total expenses from total income.
        /// </summary>
        /// <returns>Remaining balance amount after all expense</returns>
        public decimal GetTotalBalanceAmount()
        {
            return this.GetTotalIncome() - this.GetTotalExpense();
        }

        /// <summary>
        /// Deletes a specific transaction record using its unique identifier.
        /// </summary>
        /// <param name="id">Id of the transaction</param>
        /// <returns>Status of transaction deletion</returns>
        public bool DeleteTransaction(int id)
        {
            return this._financialRepository.DeleteTransactionById(id);
        }

        /// <summary>
        ///  Retrieves a list of all recorded income transactions from the repository layer.
        /// </summary>
        /// <returns>Collection of income</returns>
        public List<Income> GetAllIncome()
        {
            return this._financialRepository.ViewIncome();
        }

        /// <summary>
        /// Retrieves a list of all recorded expense transactions from the repository layer.
        /// </summary>
        /// <returns>Collection of all expense</returns>
        public List<Expense> GetAllExpense()
        {
            return this._financialRepository.ViewExpense();
        }

        /// <summary>
        /// Updates the financial properties of an existing transaction by its ID.
        /// </summary>
        /// <param name="transactionId">Transaction id of transaction that needed to be edited</param>
        /// <param name="newAmount">New transaction amount</param>
        /// <param name="newDate">New date</param>
        /// <param name="newSourceOrCategory">New source or category</param>
        /// <returns>Status of edit transaction</returns>
        public bool EditTransactionById(int transactionId, decimal newAmount, DateOnly newDate, string? newSourceOrCategory)
        {
            return this._financialRepository.EditTransactionById(transactionId, newAmount, newDate, newSourceOrCategory);
        }

        /// <summary>
        /// Get the list of available income source.
        /// </summary>
        /// <returns>Collection of income source</returns>
        public string[] GetIncomeSource()
        {
            return this._incomeSources;
        }

        /// <summary>
        /// Get the list of available expense categories.
        /// </summary>
        /// <returns>Collection of expense categories</returns>
        public string[] GetExpenseCategories()
        {
            return this._expenseCategories;
        }

        /// <summary>
        /// Checks if a transaction exists and returns it if found.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction.</param>
        /// <returns>A tuple containing a true/false success status and the matched transaction data (or null if not found)</returns>
        public (bool, Transaction?) GetTransactionIfExist(int id)
        {
            Transaction? matchedTransaction = this._financialRepository.SearchTransactionUsingId(id);
            if (matchedTransaction != null)
            {
                return (true, matchedTransaction);
            }

            return (false, matchedTransaction);
        }
    }
}
