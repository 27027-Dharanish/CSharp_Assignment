using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Core.Model;
using Assignment4.View;

namespace Assignment4.Controller
{
    /// <summary>
    /// Handles the logic for managing and tracking user expenses.
    /// </summary>
    public class ExpenseTrackerController
    {
        private readonly string[] _financialMenu = { "View Summary", "Manage Income", "Manage Expense", "Exit" };
        private readonly string[] _incomeMenu = { "Add New Income", "View All Income", "Edit Income", "Delete Income", "Exit" };
        private readonly string[] _expenseMenu = { "Add New Expense", "View All Expense", "Edit Expense", "Delete Expense", "Exit" };
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
        /// <returns><c>true</c> if the execution was successful; otherwise, <c>false</c>.</returns>
        public delegate string? GetSourceOrCategory(out bool result);

        /// <summary>
        /// Starts the execution flow for the expense tracker.
        /// </summary>
        public void StartExpenseTracker()
        {
            this.ShowExpenseTrackerMenu();
        }

        /// <summary>
        ///  Displays the menu options for the expense tracker application.
        /// </summary>
        public void ShowExpenseTrackerMenu()
        {
            FinancialOption userChoice;
            do
            {
                ConsoleActivity.ShowFinancialTrackerMenu("EXPENSE TRACKER MENU", this._financialMenu);
                string? menuChoice = ConsoleActivity.GetInputFromUser("option");
                int userInput;
                int.TryParse(menuChoice, out userInput);
                userChoice = (FinancialOption)userInput;
                switch (userChoice)
                {
                    case FinancialOption.ViewSummary:
                        this.HandleViewSummary();
                        break;
                    case FinancialOption.ManageIncome:
                        this.HandleManageIncome();
                        break;
                    case FinancialOption.ManageExpense:
                        this.HandleManageExpense();
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

        private void HandleManageIncome()
        {
            TransactionOperation userChoice;
            do
            {
                ConsoleActivity.ShowFinancialTrackerMenu("MANAGE INCOME", this._incomeMenu);
                string? menuChoice = ConsoleActivity.GetInputFromUser("option");
                int.TryParse(menuChoice, out int userInput);
                userChoice = (TransactionOperation)userInput;
                switch (userChoice)
                {
                    case TransactionOperation.AddNewTransaction:
                        this.ExecuteAddNewTransaction("Income", "ADD NEW INCOME", this.GetIncomeSource, this._financialTrackerService.AddNewTransaction, FinancialOption.ManageIncome);
                        break;
                    case TransactionOperation.ViewTransaction:
                        this.ExecuteViewAllTransaction("Income", "VIEW ALL INCOME", this._financialTrackerService.GetIncomeCount, this._financialTrackerService.GetAllTransaction, ConsoleActivity.PrintIncomeInConsole);
                        break;
                    case TransactionOperation.EditTransaction:
                        this.ExecuteEditTransaction<Income>("Income", "EDIT INCOME", this._financialTrackerService.GetIncomeCount);
                        break;
                    case TransactionOperation.DeleteTransaction:
                        this.ExecuteDeleteTransaction<Income>("Income", "DELETE INCOME", this._financialTrackerService.GetIncomeCount);
                        break;
                    case TransactionOperation.Exit:
                        // This case is just to escape from the default being executing.
                        break;
                    default:
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != TransactionOperation.Exit);
        }

        private void HandleManageExpense()
        {
            TransactionOperation userChoice;
            do
            {
                ConsoleActivity.ShowFinancialTrackerMenu("MANAGE EXPENSE", this._expenseMenu);
                string? menuChoice = ConsoleActivity.GetInputFromUser("option");
                int.TryParse(menuChoice, out int userChoiceInput);
                userChoice = (TransactionOperation)userChoiceInput;
                switch (userChoice)
                {
                    case TransactionOperation.AddNewTransaction:
                        this.ExecuteAddNewTransaction("Expense", "ADD NEW EXPENSE", this.GetExpenseCategory, this._financialTrackerService.AddNewTransaction, FinancialOption.ManageExpense);
                        break;
                    case TransactionOperation.ViewTransaction:
                        this.ExecuteViewAllTransaction("Expense", "VIEW ALL EXPENSE", this._financialTrackerService.GetExpenseCount, this._financialTrackerService.GetAllTransaction, ConsoleActivity.PrintExpenseInConsole);
                        break;
                    case TransactionOperation.EditTransaction:
                        this.ExecuteEditTransaction<Expense>("Expense", "EDIT EXPENSE", this._financialTrackerService.GetExpenseCount);
                        break;
                    case TransactionOperation.DeleteTransaction:
                        this.ExecuteDeleteTransaction<Expense>("Expense", "DELETE EXPENSE", this._financialTrackerService.GetExpenseCount);
                        break;
                    case TransactionOperation.Exit:
                        // This case is just to escape from the default being executing.
                        break;
                    default:
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != TransactionOperation.Exit);
        }

        private void ExecuteAddNewTransaction(string? inputLabel, string? headerMessage, GetSourceOrCategory getSourceOrCategory, Func<decimal, DateOnly, string?, bool, bool> addNewTransaction, FinancialOption transactionType)
        {
            (bool isValidAmount, decimal amount) = InputValidatorHelper.GetAmountWithRetry(headerMessage, inputLabel);
            if (!isValidAmount)
            {
                return;
            }

            (bool isValidTransactionDate, DateOnly transactionDate) = InputValidatorHelper.GetTransactionDateWithRetry(headerMessage);
            if (!isValidTransactionDate)
            {
                return;
            }

            string? sourceOrCategory = getSourceOrCategory(out bool isValidSourceOrCategory);
            if (!isValidSourceOrCategory)
            {
                return;
            }

            bool isIncome;
            if (transactionType == FinancialOption.ManageIncome)
            {
                isIncome = true;
            }
            else
            {
                isIncome = false;
            }

            if (addNewTransaction(amount, transactionDate, sourceOrCategory, isIncome))
            {
                ConsoleActivity.ShowFinancialTrackerHeader(headerMessage);
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole(inputLabel + " added successfully!!");
                ConsoleActivity.WaitInConsole();
            }
        }

        private void ExecuteViewAllTransaction(string? inputLabel, string? headerMessage, Func<int> getTransactionCount, Func<List<Transaction>> getTransaction, Action<List<Transaction>> printTransaction)
        {
            ConsoleActivity.ShowFinancialTrackerHeader(headerMessage);
            if (getTransactionCount() < 1)
            {
                ConsoleActivity.ShowNoTransactionMessage();
                return;
            }

            ConsoleActivity.ShowFinancialTrackerHeader(headerMessage);
            List<Transaction> transaction = getTransaction();
            printTransaction(transaction);
        }

        private void ExecuteDeleteTransaction<T>(string? deleteLabel, string? headerMessage, Func<int> getTransactionCount)
        {
            ConsoleActivity.ShowFinancialTrackerHeader(headerMessage);
            if (getTransactionCount() < 1)
            {
                ConsoleActivity.ShowNoTransactionMessage();
                return;
            }

            (bool isValidId, int transactionId) = InputValidatorHelper.GetTransactionIdWithRetry(headerMessage);
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
                if (this._financialTrackerService.DeleteTransaction(transaction.Id))
                {
                    ConsoleActivity.ShowFinancialTrackerHeader(headerMessage);
                    ConsoleActivity.PrintInConsole(deleteLabel + " deleted successfully!!");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    ConsoleActivity.ShowFinancialTrackerHeader(headerMessage);
                    ConsoleActivity.PrintInvalidMessage(deleteLabel + " deletion failed!!");
                }
            }
        }

        private void ExecuteEditTransaction<T>(string? inputLabel, string? header, Func<int> getTransactionCount)
            where T : Transaction
        {
            ConsoleActivity.ShowFinancialTrackerHeader(header);
            if (getTransactionCount() < 1)
            {
                ConsoleActivity.ShowNoTransactionMessage();
                return;
            }

            (bool isValidId, int transactionId) = InputValidatorHelper.GetTransactionIdWithRetry(header);
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
                ConsoleActivity.ShowFinancialTrackerHeader(header);
                ConsoleActivity.PrintInConsole($"{inputLabel} before editing :");
                ConsoleActivity.PrintTransaction(transaction);
                ConsoleActivity.PrintEmptyLine();
                string? userChoiceInput;
                if (typeof(T) == typeof(Income))
                {
                    userChoiceInput = ConsoleActivity.ShowTransactionEditMenu("source");
                }
                else
                {
                    userChoiceInput = ConsoleActivity.ShowTransactionEditMenu("category");
                }

                if (InputValidatorHelper.ValidateUserChoice(userChoiceInput, out int userChoiceInt) && transaction is T matchedTransaction)
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
                        (bool isValidAmount, newAmount) = InputValidatorHelper.GetAmountWithRetry(header, inputLabel);
                        if (!isValidAmount)
                        {
                            return;
                        }
                    }
                    else if (userChoice == TransactionField.TransactionDate)
                    {
                        (bool isValidDate, newDate) = InputValidatorHelper.GetTransactionDateWithRetry(header);
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
                        ConsoleActivity.ShowFinancialTrackerHeader(header);
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
            bool isIncome = false;
            ConsoleActivity.ShowFinancialTrackerHeader("ADD NEW EXPENSE");
            string[] expenseCategoryList = this._financialTrackerService.GetExpenseCategories();
            (bool isValidExpenseCategory, string? categorySelected) = InputValidatorHelper.GetSourceOrCategory("ADD NEW EXPENSE", expenseCategoryList, isIncome);
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
            bool isIncome = true;
            ConsoleActivity.ShowFinancialTrackerHeader("ADD NEW INCOME");
            string[] incomeSourceList = this._financialTrackerService.GetIncomeSource();
            (bool isValidIncomeSource, string? sourceSelected) = InputValidatorHelper.GetSourceOrCategory("ADD NEW INCOME", incomeSourceList, isIncome);
            if (!isValidIncomeSource)
            {
                isValidSource = false;
                return default;
            }

            isValidSource = true;
            return sourceSelected;
        }

        private void HandleViewSummary()
        {
            ConsoleActivity.ShowFinancialTrackerHeader("INCOME-EXPENSE SUMMARY");
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
