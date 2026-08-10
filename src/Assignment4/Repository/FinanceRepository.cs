using System.Runtime.CompilerServices;
using System.Security.Principal;
using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Core.Model;

namespace Assignment4.Repository
{
    /// <summary>
    /// Provides a centralized data repository for storing, retrieving expense info entities.
    /// </summary>
    public class FinanceRepository : IExpenseTrackerRepository
    {
        private readonly List<Transaction> _financeTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceRepository"/> class.
        /// </summary>
        public FinanceRepository()
        {
            this._financeTracker = new ();
        }

        /// <summary>
        /// Adds a new transaction record to the tracking system and verifies its successful insertion.
        /// </summary>
        /// <param name="transaction">The financial transaction</param>
        /// <returns>True if transaction added successfully else false</returns>
        public bool AddNewTransaction(Transaction transaction)
        {
            int previousTransactionCount = this.GetTransactionCount();
            this._financeTracker.Add(transaction);
            if (previousTransactionCount == this.GetTransactionCount())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Retrieves a list of all transaction records stored in the tracking system.
        /// </summary>
        /// <returns>List containing all transaction record</returns>
        public List<Transaction> GetAllTransaction()
        {
            return this._financeTracker;
        }

        /// <summary>
        /// Search transaction using transaction id and return copy of it if necessary.
        /// </summary>
        /// <param name="id">Id of the transaction</param>
        /// <param name="isReturnCopy">Says whether the actual reference or duplicate need</param>
        /// <returns>Transaction that matched with the id</returns>
        public Transaction? SearchTransactionUsingId(int id, bool isReturnCopy = true)
        {
            Transaction? matchedTransaction = this._financeTracker.Find(transaction => transaction != null && transaction.Id == id);
            if (matchedTransaction == null)
            {
                return null;
            }
            else if (isReturnCopy)
            {
                return this.CreateDuplicateTransaction(matchedTransaction);
            }

            return matchedTransaction;
        }

        /// <summary>
        /// Delete the transaction using the transaction id.
        /// </summary>
        /// <param name="id">Transaction id of transaction</param>
        /// <returns>Status of transaction deletion</returns>
        public bool DeleteTransactionById(int id)
        {
            Transaction? transactionToBeDeleted = this.SearchTransactionUsingId(id, false);
            if (transactionToBeDeleted == null)
            {
                return false;
            }

            this._financeTracker.Remove(transactionToBeDeleted);
            return true;
        }

        /// <summary>
        /// Filters and retrieves a collection of all recorded transactions.
        /// </summary>
        /// <returns>All the transaction</returns>
        public List<Transaction> ViewTransaction()
        {
            List<Transaction> transactions = this.GetAllTransaction();
            return transactions;
        }

        /// <summary>
        /// Check whether the transaction id exist or not.
        /// </summary>
        /// <param name="id">Transaction id that to be checked</param>
        /// <returns>True if transaction id exist else false</returns>
        public bool IsTransactionIdExist(int id)
        {
            if (this.SearchTransactionUsingId(id) != null)
            {
                return true;
            }

            return false;
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
            Transaction? matchedTransaction = this.SearchTransactionUsingId(transactionId, false);
            if (matchedTransaction is Income income)
            {
                income.Amount = newAmount;
                income.TransactionDate = newDate;
                income.Source = newSourceOrCategory;
                return true;
            }
            else if (matchedTransaction is Expense expense)
            {
                expense.Amount = newAmount;
                expense.TransactionDate = newDate;
                expense.Category = newSourceOrCategory;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the total number of transaction count.
        /// </summary>
        /// <returns>Count of transaction</returns>
        public int GetTransactionCount()
        {
            return this._financeTracker.Count;
        }

        /// <summary>
        /// Filters and retrieves a collection of all recorded income transactions.
        /// </summary>
        /// <returns>All the income from transaction</returns>
        public List<Income> ViewIncome()
        {
            List<Income> allIncome = new List<Income>();
            List<Transaction> transactions = this.GetAllTransaction();
            foreach (Transaction transaction in transactions)
            {
                if (transaction is Income income)
                {
                    allIncome.Add(income);
                }
            }

            return allIncome;
        }

        /// <summary>
        /// Filters and retrieves a collection of all recorded expense transactions.
        /// </summary>
        /// <returns>All the expense from transaction</returns>
        public List<Expense> ViewExpense()
        {
            List<Expense> allExpense = new List<Expense>();
            List<Transaction> transactions = this.GetAllTransaction();
            foreach (Transaction transaction in transactions)
            {
                if (transaction is Expense expense)
                {
                    allExpense.Add(expense);
                }
            }

            return allExpense;
        }

        /// <summary>
        /// Get the income count.
        /// </summary>
        /// <returns>Income count</returns>
        public int GetIncomeCount()
        {
            return this.ViewIncome().Count;
        }

        /// <summary>
        /// Get the expense count.
        /// </summary>
        /// <returns>expense count</returns>
        public int GetExpenseCount()
        {
            return this.ViewExpense().Count;
        }

        private Transaction? CreateDuplicateTransaction(Transaction transaction)
        {
            if (transaction is Income income)
            {
                Income incomeCopy = new Income(income.Id, income.Amount, income.TransactionDate);
                incomeCopy.Source = income.Source;
                return incomeCopy;
            }
            else if (transaction is Expense expense)
            {
                Expense expenseCopy = new Expense(expense.Id, expense.Amount, expense.TransactionDate);
                expenseCopy.Category = expense.Category;
                return expenseCopy;
            }

            return null;
        }
    }
}
