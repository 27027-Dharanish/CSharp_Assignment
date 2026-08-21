using FinanceTracker.Core.ExpenseTrackerInterface;
using FinanceTracker.Core.FinancialTrackerConstant;
using FinanceTracker.Core.Model;

namespace FinanceTracker.Service
{
    /// <summary>
    /// Provides core business logic for managing transaction, processing transaction, and interacting with repository.
    /// </summary>
    public class FinancialTrackerService : IFinancialTrackerService
    {
        private readonly IFinancialTrackerRepository _financialRepository;
        private int _transactionIdCounter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialTrackerService"/> class.
        /// </summary>
        /// <param name="repository">Repository for managing finance data.</param>
        public FinancialTrackerService(IFinancialTrackerRepository repository)
        {
            this._financialRepository = repository;
            this._transactionIdCounter = 0;
        }

        /// <inheritdoc />
        public bool CreateNewTransaction(decimal amount, DateOnly date, string? transactionType, bool isIncome)
        {
            Transaction newTransaction;
            this._transactionIdCounter++;
            if (isIncome)
            {
                newTransaction = new Income(this._transactionIdCounter, amount, date, transactionType);
            }
            else
            {
                newTransaction = new Expense(this._transactionIdCounter, amount, date, transactionType);
            }

            this._financialRepository.AddNewTransaction(newTransaction);
            return true;
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
            return FinanceConstant.IncomeSources;
        }

        /// <inheritdoc />
        public string[] GetExpenseCategories()
        {
            return FinanceConstant.ExpenseCategories;
        }

        /// <inheritdoc />
        public (bool, Transaction?) GetTransactionIfExist(int id)
        {
            Transaction? matchedTransaction = this._financialRepository.GetTransactionCopyUsingId(id);
            if (matchedTransaction != null)
            {
                return (true, matchedTransaction);
            }

            return (false, matchedTransaction);
        }

        /// <inheritdoc />
        public int GetIncomeCount()
        {
            return this._financialRepository.GetFilteredTransactionCount<Income>();
        }

        /// <inheritdoc />
        public int GetExpenseCount()
        {
            return this._financialRepository.GetFilteredTransactionCount<Expense>();
        }

        /// <inheritdoc />
        public bool IsValidateAmount(decimal amount)
        {
            if (amount < 0)
            {
                return false;
            }
            else if (amount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
