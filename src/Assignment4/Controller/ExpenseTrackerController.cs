using FinanceTracker.Core.ExpenseTrackerInterface;
using FinanceTracker.Core.FinancialTrackerConstant;
using FinanceTracker.Core.Model;
using FinanceTracker.FinanceTrackerHelper;
using FinanceTracker.View;

namespace FinanceTracker.Controller
{
    /// <summary>
    /// Handles the logic for managing and tracking user expenses.
    /// </summary>
    public class ExpenseTrackerController
    {
        private readonly IFinancialTrackerService _financialTrackerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseTrackerController"/> class.
        /// </summary>
        /// <param name="service">The application service layer business logic handling expense operations.</param>
        public ExpenseTrackerController(IFinancialTrackerService service)
        {
            this._financialTrackerService = service;
        }

        /// <summary>
        /// Defines a delegate that evaluates user input to determine a source or category using an out parameter.
        /// </summary>
        /// <param name="result">The boolean evaluation result output by the method logic.</param>
        /// <returns>Source or category of transaction.</returns>
        public delegate string? GetSourceOrCategory(out bool result);

        /// <summary>
        ///  Displays the menu options for the expense tracker application.
        /// </summary>
        public void Start()
        {
            FinancialOption userChoice;
            do
            {
                ConsoleActivity.ShowMenu("EXPENSE TRACKER MENU", FinanceConstant.FinancialMenu);
                string? menuChoice = ConsoleActivity.GetStringInput("option");
                InputGetter.GetUserChoice(menuChoice, out int userInput);
                userChoice = (FinancialOption)userInput;
                switch (userChoice)
                {
                    case FinancialOption.Summary:
                        this.ManageSummary();
                        break;
                    case FinancialOption.Income:
                        this.ManageIncome();
                        break;
                    case FinancialOption.Expense:
                        this.ManageExpense();
                        break;
                    case FinancialOption.Exit:
                        ConsoleActivity.ExitApplication();
                        break;
                    default:
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != FinancialOption.Exit);
        }

        private void ManageIncome()
        {
            TransactionOperation userChoice;
            do
            {
                ConsoleActivity.ShowMenu("MANAGE INCOME", FinanceConstant.IncomeMenu);
                string? menuChoice = ConsoleActivity.GetStringInput("option");
                InputGetter.GetUserChoice(menuChoice, out int userInput);
                userChoice = (TransactionOperation)userInput;
                switch (userChoice)
                {
                    case TransactionOperation.Add:
                        this.AddTransaction(this.GetIncomeSource, true);
                        break;
                    case TransactionOperation.View:
                        this.ViewTransaction(this._financialTrackerService.GetIncomeCount, ConsoleActivity.PrintIncome, true);
                        break;
                    case TransactionOperation.Edit:
                        this.EditTransaction<Income>(this._financialTrackerService.GetIncomeCount, true);
                        break;
                    case TransactionOperation.Delete:
                        this.DeleteTransaction<Income>(this._financialTrackerService.GetIncomeCount, true);
                        break;
                    case TransactionOperation.Back:
                        // This case is just to escape from the default being executing.
                        break;
                    default:
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != TransactionOperation.Back);
        }

        private void ManageExpense()
        {
            TransactionOperation userChoice;
            do
            {
                ConsoleActivity.ShowMenu("MANAGE EXPENSE", FinanceConstant.ExpenseMenu);
                string? menuChoice = ConsoleActivity.GetStringInput("option");
                InputGetter.GetUserChoice(menuChoice, out int userInput);
                userChoice = (TransactionOperation)userInput;
                switch (userChoice)
                {
                    case TransactionOperation.Add:
                        this.AddTransaction(this.GetExpenseCategory, false);
                        break;
                    case TransactionOperation.View:
                        this.ViewTransaction(this._financialTrackerService.GetExpenseCount, ConsoleActivity.PrintExpense, false);
                        break;
                    case TransactionOperation.Edit:
                        this.EditTransaction<Expense>(this._financialTrackerService.GetExpenseCount, false);
                        break;
                    case TransactionOperation.Delete:
                        this.DeleteTransaction<Expense>(this._financialTrackerService.GetExpenseCount, false);
                        break;
                    case TransactionOperation.Back:
                        // This case is just to escape from the default being executing.
                        break;
                    default:
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != TransactionOperation.Back);
        }

        private void AddTransaction(GetSourceOrCategory getSourceOrCategory, bool isIncome)
        {
            string inputLabel = isIncome ? "Income" : "Expense";
            string headerMessage = isIncome ? "ADD NEW INCOME" : "ADD NEW EXPENSE";
            (bool isValidAmount, decimal amount) = InputGetter.GetAmountWithRetry(headerMessage, inputLabel);
            if (!isValidAmount)
            {
                return;
            }

            (bool isValidTransactionDate, DateOnly transactionDate) = InputGetter.GetTransactionDateWithRetry(headerMessage);
            if (!isValidTransactionDate)
            {
                return;
            }

            string? sourceOrCategory = getSourceOrCategory(out bool isValidSourceOrCategory);
            if (!isValidSourceOrCategory)
            {
                return;
            }

            if (this._financialTrackerService.CreateNewTransaction(amount, transactionDate, sourceOrCategory, isIncome))
            {
                ConsoleActivity.ShowHeader(headerMessage);
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole(inputLabel + " added successfully!!");
                ConsoleActivity.WaitInConsole();
            }
        }

        private void ViewTransaction(Func<int> getTransactionCount, Action<List<Transaction>> printTransaction, bool isIncome)
        {
            string inputLabel = isIncome ? "Income" : "Expense";
            string headerMessage = isIncome ? "VIEW ALL INCOME" : "VIEW ALL EXPENSE";
            ConsoleActivity.ShowHeader(headerMessage);
            if (getTransactionCount() < 1)
            {
                ConsoleActivity.PrintInvalidMessage("No transaction made until now!!\nAdd some transaction to perform operation!!!");
                return;
            }

            ConsoleActivity.ShowHeader(headerMessage);
            List<Transaction> transaction = this._financialTrackerService.GetAllTransaction();
            printTransaction(transaction);
        }

        private void DeleteTransaction<T>(Func<int> getTransactionCount, bool isIncome)
        {
            string deleteLabel = isIncome ? "Income" : "Expense";
            string headerMessage = isIncome ? "DELETE INCOME" : "DELETE EXPENSE";
            ConsoleActivity.ShowHeader(headerMessage);
            if (getTransactionCount() < 1)
            {
                ConsoleActivity.PrintInvalidMessage("No transaction made until now!!\nAdd some transaction to perform operation!!!");
                return;
            }

            (bool isValidId, int transactionId) = InputGetter.GetTransactionIdWithRetry(headerMessage);
            if (!isValidId)
            {
                return;
            }

            (bool isTransactionExist, Transaction? transaction) = this._financialTrackerService.GetTransactionIfExist(transactionId);
            if (!isTransactionExist || transaction is not T)
            {
                ConsoleActivity.PrintInvalidMessage("Transaction Id not exist!!");
                return;
            }
            else if (transaction != null)
            {
                ConsoleActivity.ShowHeader(headerMessage);
                if (this._financialTrackerService.DeleteTransaction(transaction.Id))
                {
                    ConsoleActivity.PrintInConsole(deleteLabel + " deleted successfully!!");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    ConsoleActivity.PrintInvalidMessage(deleteLabel + " deletion failed!!");
                }
            }
        }

        private void EditTransaction<T>(Func<int> getTransactionCount, bool isIncome)
            where T : Transaction
        {
            string inputLabel = isIncome ? "Income" : "Expense";
            string headerMessage = isIncome ? "EDIT INCOME" : "EDIT EXPENSE";
            ConsoleActivity.ShowHeader(headerMessage);
            if (getTransactionCount() < 1)
            {
                ConsoleActivity.PrintInvalidMessage("No transaction made until now!!\nAdd some transaction to perform operation!!!");
                return;
            }

            (bool isValidId, int transactionId) = InputGetter.GetTransactionIdWithRetry(headerMessage);
            if (!isValidId)
            {
                return;
            }

            (bool isTransactionExist, Transaction? transaction) = this._financialTrackerService.GetTransactionIfExist(transactionId);
            if (!isTransactionExist || transaction is not T)
            {
                ConsoleActivity.PrintInvalidMessage("Transaction Id not exist!!");
                return;
            }
            else if (transaction != null)
            {
                TransactionField userChoice;
                ConsoleActivity.ShowHeader(headerMessage);
                ConsoleActivity.PrintInConsole($"{inputLabel} before editing :");
                ConsoleActivity.PrintTransaction(transaction);
                ConsoleActivity.PrintEmptyLine();
                if (typeof(T) == typeof(Income))
                {
                    ConsoleActivity.ShowTransactionEditMenu("source");
                }
                else
                {
                    ConsoleActivity.ShowTransactionEditMenu("category");
                }

                string? userChoiceInput = ConsoleActivity.GetStringInput("option");
                if (InputGetter.GetUserChoice(userChoiceInput, out int userChoiceInt) && transaction is T matchedTransaction)
                {
                    userChoice = (TransactionField)userChoiceInt;
                    decimal newAmount = matchedTransaction.Amount;
                    DateOnly newDate = matchedTransaction.TransactionDate;
                    string? context = default;
                    if (matchedTransaction is Income income)
                    {
                        context = income.Source;
                    }
                    else if (matchedTransaction is Expense expense)
                    {
                        context = expense.Category;
                    }

                    if (userChoice == TransactionField.Amount)
                    {
                        (bool isValidAmount, newAmount) = InputGetter.GetAmountWithRetry(headerMessage, inputLabel);
                        if (!isValidAmount)
                        {
                            return;
                        }
                    }
                    else if (userChoice == TransactionField.TransactionDate)
                    {
                        (bool isValidDate, newDate) = InputGetter.GetTransactionDateWithRetry(headerMessage);
                        if (!isValidDate)
                        {
                            return;
                        }
                    }
                    else if (userChoice == TransactionField.SourceOrCategory)
                    {
                        if (matchedTransaction is Income matchedIncome)
                        {
                            context = this.GetIncomeSource(out bool isValidSource);
                            if (!isValidSource)
                            {
                                return;
                            }
                        }
                        else
                        {
                            context = this.GetExpenseCategory(out bool isValidCategory);
                            if (!isValidCategory)
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        ConsoleActivity.PrintInvalidMessage("Enter a valid choice!!");
                        return;
                    }

                    this._financialTrackerService.EditTransactionById(matchedTransaction.Id, newAmount, newDate, context);
                    (bool isTransactionUpdated, Transaction? updatedTransaction) = this._financialTrackerService.GetTransactionIfExist(transactionId);
                    if (isTransactionUpdated && updatedTransaction != null)
                    {
                        ConsoleActivity.ShowHeader(headerMessage);
                        ConsoleActivity.PrintInConsole($"{inputLabel} edited successfully!!");
                        ConsoleActivity.PrintEmptyLine();
                        ConsoleActivity.PrintInConsole($"Updated {inputLabel}: ");
                        ConsoleActivity.PrintTransaction(updatedTransaction);
                        ConsoleActivity.WaitInConsole();
                    }
                }
                else
                {
                    ConsoleActivity.PrintInvalidMessage("Enter a valid choice!!");
                    return;
                }
            }
        }

        private string? GetExpenseCategory(out bool isValidCategory)
        {
            ConsoleActivity.ShowHeader("ADD NEW EXPENSE");
            string[] expenseCategoryList = this._financialTrackerService.GetExpenseCategories();
            (bool isValidExpenseCategory, string? categorySelected) = InputGetter.GetTransactionTag("ADD NEW EXPENSE", expenseCategoryList, false);
            if (!isValidExpenseCategory)
            {
                isValidCategory = false;
                return default;
            }

            isValidCategory = true;
            return categorySelected;
        }

        private string? GetIncomeSource(out bool isValidSource)
        {
            ConsoleActivity.ShowHeader("ADD NEW INCOME");
            string[] incomeSourceList = this._financialTrackerService.GetIncomeSource();
            (bool isValidIncomeSource, string? sourceSelected) = InputGetter.GetTransactionTag("ADD NEW INCOME", incomeSourceList, true);
            if (!isValidIncomeSource)
            {
                isValidSource = false;
                return default;
            }

            isValidSource = true;
            return sourceSelected;
        }

        private void ManageSummary()
        {
            ConsoleActivity.ShowHeader("INCOME-EXPENSE SUMMARY");
            (decimal totalIncome, bool isIncomePresent) = this._financialTrackerService.GetTotalTransactionAmount<Income>();
            if (!isIncomePresent)
            {
                ConsoleActivity.PrintInConsole("No Income!!");
            }
            else
            {
                ConsoleActivity.PrintInConsole($"Total Income  : Rs.{totalIncome}");
            }

            (decimal totalExpense, bool isExpensePresent) = this._financialTrackerService.GetTotalTransactionAmount<Expense>();
            if (!isExpensePresent)
            {
                ConsoleActivity.PrintInConsole("No Expense!!");
            }
            else
            {
                ConsoleActivity.PrintInConsole($"Total Expense : Rs.{totalExpense}");
            }

            ConsoleActivity.PrintInConsole($"Net Balance   : Rs.{totalIncome - totalExpense}");
            if (totalIncome - totalExpense < 0)
            {
                ConsoleActivity.PrintInConsole("Your net balance is negative. Expense crossed your total income!!");
            }

            ConsoleActivity.WaitInConsole();
        }
    }
}
