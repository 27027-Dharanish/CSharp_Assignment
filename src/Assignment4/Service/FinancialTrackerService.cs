using System.Diagnostics;
using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Core.Model;

namespace Assignment4.Service
{
    /// <summary>
    /// Provides core business logic for managing transaction, processing transaction, and interacting with repository.
    /// </summary>
    public class FinancialTrackerService : IFinancialTrackerService
    {
        private readonly string[] _incomeSources = { "Salary", "Freelance", "Investment", "Business", "Rental", "Pocket Money", "Others" };
        private readonly string[] _expenseCategories = { "Housing", "Groceries", "Transportation", "Healthcare", "Entertainment", "Insurance", "Food", "Shopping", "Others" };

        private readonly IFinancialTrackerRepository _financialRepository;
        private int _transactionIdCounter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialTrackerService"/> class.
        /// </summary>
        /// <param name="repository">Repository for managing finance data</param>
        public FinancialTrackerService(IFinancialTrackerRepository repository)
        {
            this._financialRepository = repository;
            this._transactionIdCounter = 0;
        }

        /// <inheritdoc />
        public bool AddNewTransaction(decimal amount, DateOnly date, string? context, bool isIncome)
        {
            if (amount == 0)
            {
                return false;
            }

            if (isIncome)
            {
                Income newIncome = new (this.GetTransactionId(), amount, date);
                newIncome.Source = context;
                return this._financialRepository.AddNewTransaction(newIncome);
            }
            else
            {
                this._transactionIdCounter++;
                Expense newExpense = new (this.GetTransactionId(), amount, date);
                newExpense.Category = context;
                return this._financialRepository.AddNewTransaction(newExpense);
            }
        }

        /// <inheritdoc />
        public List<Transaction> GetAllTransaction()
        {
            return this._financialRepository.GetAllTransaction();
        }

        /// <inheritdoc />
        public (decimal, bool) GetTotalTransactionAmount<T>()
            where T : Transaction
        {
            List<Transaction> transactions = this._financialRepository.GetAllTransaction();
            decimal totalAmount = 0;
            if (transactions.Count == 0)
            {
                return (default, false);
            }

            foreach (Transaction transaction in transactions)
            {
                if (transaction is T matchedTransaction)
                {
                    totalAmount += matchedTransaction.Amount;
                }
            }

            return (totalAmount, true);
        }

        /// <inheritdoc />
        public decimal GetTotalBalanceAmount()
        {
            (decimal totalIncome, bool isIncomePresent) = this.GetTotalTransactionAmount<Income>();
            (decimal totalExpense, bool isExpensePresent) = this.GetTotalTransactionAmount<Expense>();
            return totalIncome - totalExpense;
        }

        /// <inheritdoc />
        public bool DeleteTransaction(int id)
        {
            return this._financialRepository.DeleteTransactionById(id);
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
            return this._financialRepository.FilterTransaction<Income>().Count;
        }

        /// <inheritdoc />
        public int GetExpenseCount()
        {
            return this._financialRepository.FilterTransaction<Expense>().Count;
        }

        /// <summary>
        /// Retrieves a filtered list of transactions.
        /// </summary>
        /// <typeparam name="T">The specific type of transaction</typeparam>
        /// <returns>The filtered transactions matching the requested type.</returns>
        public List<T> GetFilteredTransaction<T>()
            where T : Transaction
        {
            return this._financialRepository.FilterTransaction<T>();
        }

        private int GetTransactionId()
        {
            do
            {
                this._transactionIdCounter++;
                Transaction? matchedTransaction = this._financialRepository.SearchTransactionUsingId(this._transactionIdCounter);
                if (matchedTransaction == null)
                {
                    return this._transactionIdCounter;
                }
            }
            while (true);
        }
    }
}
