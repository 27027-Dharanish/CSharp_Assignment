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
        /// <param name="repository">Repository for managing finance data</param>
        public ExpenseTrackerService(IExpenseTrackerRepository repository)
        {
            this._financialRepository = repository;
            this._transactionIdCounter = 0;
        }

        /// <inheritdoc />
        public bool AddNewIncome(decimal amount, DateOnly date, string? source)
        {
            if (amount == 0)
            {
                return false;
            }

            this._transactionIdCounter++;
            Income newIncome = new (this._transactionIdCounter, amount, date);
            newIncome.Source = source;
            return this._financialRepository.AddNewTransaction(newIncome);
        }

        /// <inheritdoc />
        public bool AddNewExpense(decimal amount, DateOnly date, string? category)
        {
            if (amount == 0)
            {
                return false;
            }

            this._transactionIdCounter++;
            Expense newExpense = new (this._transactionIdCounter, amount, date);
            newExpense.Category = category;
            return this._financialRepository.AddNewTransaction(newExpense);
        }

        /// <inheritdoc />
        public (decimal, bool) GetTotalIncome()
        {
            List<Transaction> transactions = this._financialRepository.GetAllTransaction();
            decimal totalIncome = 0;
            if (transactions.Count == 0)
            {
                return (default, false);
            }

            foreach (Transaction transaction in transactions)
            {
                if (transaction is Income income)
                {
                    totalIncome += income.Amount;
                }
            }

            return (totalIncome, true);
        }

        /// <inheritdoc />
        public (decimal, bool) GetTotalExpense()
        {
            List<Transaction> transactions = this._financialRepository.GetAllTransaction();
            decimal totalExpense = 0;
            if (transactions.Count == 0)
            {
                return (default, false);
            }

            foreach (Transaction transaction in transactions)
            {
                if (transaction is Expense expense)
                {
                    totalExpense += expense.Amount;
                }
            }

            return (totalExpense, true);
        }

        /// <inheritdoc />
        public decimal GetTotalBalanceAmount()
        {
            (decimal totalIncome, bool isIncomePresent) = this.GetTotalIncome();
            (decimal totalExpense, bool isExpensePresent) = this.GetTotalExpense();
            return totalIncome - totalExpense;
        }

        /// <inheritdoc />
        public bool DeleteTransaction(int id)
        {
            return this._financialRepository.DeleteTransactionById(id);
        }

        /// <inheritdoc />
        public List<Transaction> GetAllTransaction()
        {
            return this._financialRepository.GetAllTransaction();
        }

        /// <inheritdoc />
        public bool EditTransactionById(int transactionId, decimal newAmount, DateOnly newDate, string? newSourceOrCategory)
        {
            return this._financialRepository.EditTransactionById(transactionId, newAmount, newDate, newSourceOrCategory);
        }

        /// <inheritdoc />
        public string[] GetIncomeSource()
        {
            return this._incomeSources;
        }

        /// <inheritdoc />
        public string[] GetExpenseCategories()
        {
            return this._expenseCategories;
        }

        /// <inheritdoc />
        public (bool, Transaction?) GetTransactionIfExist(int id)
        {
            Transaction? matchedTransaction = this._financialRepository.SearchTransactionUsingId(id);
            if (matchedTransaction != null)
            {
                return (true, matchedTransaction);
            }

            return (false, matchedTransaction);
        }

        /// <inheritdoc />
        public int GetIncomeCount()
        {
            return this._financialRepository.GetIncomeCount();
        }

        /// <inheritdoc />
        public int GetExpenseCount()
        {
            return this._financialRepository.GetExpenseCount();
        }
    }
}
