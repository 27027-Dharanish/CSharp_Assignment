using System.Runtime.CompilerServices;
using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Core.Model;
using Assignment4.Service;
using Assignment4.View;

namespace Assignment4.Controller
{
    /// <summary>
    /// Handles the logic for managing and tracking user expenses.
    /// </summary>
    public class ExpenseTrackerController
    {
        private readonly IExpenseTrackerService _financialTrackerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseTrackerController"/> class.
        /// </summary>
        /// <param name="service">The application service layer business logic handling expense operations.</param>
        public ExpenseTrackerController(IExpenseTrackerService service)
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
            Enums.FinancialOption userChoice;
            do
            {
                ConsoleActivity.ShowFinancialTrackerMenu();
                int userInput = InputValidatorHelper.GetMenuChoiceFromUser();
                userChoice = (Enums.FinancialOption)userInput;
                switch (userChoice)
                {
                    case Enums.FinancialOption.ViewSummary:
                        this.HandleViewSummary();
                        break;
                    case Enums.FinancialOption.ManageIncome:
                        this.HandleManageIncome();
                        break;
                    case Enums.FinancialOption.ManageExpense:
                        this.HandleManageExpense();
                        break;
                    case Enums.FinancialOption.Exit:
                        // This case is just to escape from the default being executing.
                        break;
                    default:
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != Enums.FinancialOption.Exit);
        }

        private void HandleManageIncome()
        {
            Enums.TransactionOperation userChoice;
            do
            {
                ConsoleActivity.ShowIncomeMenu();
                int userInput = InputValidatorHelper.GetMenuChoiceFromUser();
                userChoice = (Enums.TransactionOperation)userInput;
                switch (userChoice)
                {
                    case Enums.TransactionOperation.AddNewTransaction:
                        this.ExecuteAddNewTransaction("Income", ConsoleActivity.ShowAddNewIncomeHeader, this.GetIncomeSource, this._financialTrackerService.AddNewIncome);
                        break;
                    case Enums.TransactionOperation.ViewTransaction:
                        // this.HandleViewAllIncome();
                        this.ExecuteViewAllTransaction("Income", ConsoleActivity.ShowViewIncomeHeader, this._financialTrackerService.GetIncomeCount, this._financialTrackerService.GetAllTransaction, ConsoleActivity.PrintIncomeInConsole);
                        break;
                    case Enums.TransactionOperation.EditTransaction:
                        this.HandleEditIncome();
                        break;
                    case Enums.TransactionOperation.DeleteTransaction:
                        this.ExecuteDeleteTransaction("Income", ConsoleActivity.ShowDeleteIncomeHeader, this._financialTrackerService.GetIncomeCount);
                        break;
                    case Enums.TransactionOperation.Exit:
                        // This case is just to escape from the default being executing.
                        break;
                    default:
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != Enums.TransactionOperation.Exit);
        }

        private void HandleManageExpense()
        {
            Enums.TransactionOperation userChoice;
            do
            {
                ConsoleActivity.ShowExpenseMenu();
                int userChoiceInput = InputValidatorHelper.GetMenuChoiceFromUser();
                userChoice = (Enums.TransactionOperation)userChoiceInput;
                switch (userChoice)
                {
                    case Enums.TransactionOperation.AddNewTransaction:
                        this.ExecuteAddNewTransaction("Expense", ConsoleActivity.ShowAddNewExpenseHeader, this.GetExpenseCategory, this._financialTrackerService.AddNewExpense);
                        break;
                    case Enums.TransactionOperation.ViewTransaction:
                        this.ExecuteViewAllTransaction("Expense", ConsoleActivity.ShowViewExpenseHeader, this._financialTrackerService.GetExpenseCount, this._financialTrackerService.GetAllTransaction, ConsoleActivity.PrintExpenseInConsole);
                        break;
                    case Enums.TransactionOperation.EditTransaction:
                        this.HandleEditExpense();
                        break;
                    case Enums.TransactionOperation.DeleteTransaction:
                        this.ExecuteDeleteTransaction("Expense", ConsoleActivity.ShowDeleteExpenseHeader, this._financialTrackerService.GetExpenseCount);
                        break;
                    case Enums.TransactionOperation.Exit:
                        // This case is just to escape from the default being executing.
                        break;
                    default:
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != Enums.TransactionOperation.Exit);
        }

        private void ExecuteAddNewTransaction(string? inputLabel, Action action, GetSourceOrCategory getSourceOrCategory, Func<decimal, DateOnly, string?, bool> addNewTransaction)
        {
            (bool isValidAmount, decimal amount) = InputValidatorHelper.GetAmountWithRetry(action, inputLabel);
            if (!isValidAmount)
            {
                return;
            }

            (bool isValidTransactionDate, DateOnly transactionDate) = InputValidatorHelper.GetTransactionDateWithRetry(action);
            if (!isValidTransactionDate)
            {
                return;
            }

            string? sourceOrCategory = getSourceOrCategory(out bool isValidSourceOrCategory);
            if (!isValidSourceOrCategory)
            {
                return;
            }

            if (addNewTransaction(amount, transactionDate, sourceOrCategory))
            {
                action();
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole(inputLabel + " added successfully!!");
                ConsoleActivity.WaitInConsole();
            }
        }

        private void ExecuteViewAllTransaction(string? inputLabel, Action action, Func<int> getTransactionCount, Func<List<Transaction>> getTransaction, Action<List<Transaction>> printTransaction)
        {
            action();
            if (getTransactionCount() < 1)
            {
                ConsoleActivity.ShowNoTransactionMessage();
                return;
            }

            action();
            List<Transaction> transaction = getTransaction();
            printTransaction(transaction);
        }

        private void ExecuteDeleteTransaction(string? deleteLabel, Action actionHeader, Func<int> getTransactionCount)
        {
            actionHeader();
            if (getTransactionCount() < 1)
            {
                ConsoleActivity.ShowNoTransactionMessage();
                return;
            }

            (bool isValidId, int transactionId) = InputValidatorHelper.GetTransactionIdWithRetry(actionHeader);
            if (!isValidId)
            {
                return;
            }

            (bool isTransactionExist, Transaction? transaction) = this._financialTrackerService.GetTransactionIfExist(transactionId);
            if (!isTransactionExist)
            {
                ConsoleActivity.PrintInvalidMessage("Transaction Id not exist!!");
                return;
            }
            else if (transaction != null)
            {
                if (this._financialTrackerService.DeleteTransaction(transaction.Id))
                {
                    actionHeader();
                    ConsoleActivity.PrintInConsole(deleteLabel + " deleted successfully!!");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    actionHeader();
                    ConsoleActivity.PrintInvalidMessage(deleteLabel + " deletion failed!!");
                }
            }
        }

        private void HandleEditIncome()
        {
            ConsoleActivity.ShowEditIncomeHeader();
            if (this._financialTrackerService.GetIncomeCount() < 1)
            {
                ConsoleActivity.ShowNoTransactionMessage();
                return;
            }

            (bool isValidId, int transactionId) = InputValidatorHelper.GetTransactionIdWithRetry(ConsoleActivity.ShowEditIncomeHeader);
            if (!isValidId)
            {
                return;
            }

            (bool isTransactionExist, Transaction? transaction) = this._financialTrackerService.GetTransactionIfExist(transactionId);
            if (!isTransactionExist)
            {
                ConsoleActivity.PrintInvalidMessage("Transaction Id not exist!!");
                return;
            }
            else if (transaction != null)
            {
                Enums.TransactionField userChoice;
                ConsoleActivity.ShowEditIncomeHeader();
                Console.WriteLine("Income before editing :");
                ConsoleActivity.PrintIncome(transaction);
                ConsoleActivity.PrintEmptyLine();
                string? userChoiceInput = ConsoleActivity.ShowTransactionEditMenu("source");
                if (InputValidatorHelper.ValidateUserChoice(userChoiceInput, out int userChoiceInt) && transaction is Income income)
                {
                    userChoice = (Enums.TransactionField)userChoiceInt;
                    decimal newAmount = income.Amount;
                    DateOnly newDate = income.TransactionDate;
                    string? newSource = income.Source;
                    if (userChoice == Enums.TransactionField.Amount)
                    {
                        (bool isValidAmount, newAmount) = InputValidatorHelper.GetAmountWithRetry(ConsoleActivity.ShowEditIncomeHeader, "new income amount");
                        if (!isValidAmount)
                        {
                            return;
                        }
                    }
                    else if (userChoice == Enums.TransactionField.TransactionDate)
                    {
                        (bool isValidDate, newDate) = InputValidatorHelper.GetTransactionDateWithRetry(ConsoleActivity.ShowEditIncomeHeader);
                        if (!isValidDate)
                        {
                            return;
                        }
                    }
                    else if (userChoice == Enums.TransactionField.SourceOrCategory)
                    {
                        newSource = this.GetIncomeSource(out bool isValidSource);
                        if (!isValidSource)
                        {
                            return;
                        }
                    }
                    else
                    {
                        ConsoleActivity.PrintInvalidMessage("Enter a valid choice!!");
                        return;
                    }

                    this._financialTrackerService.EditTransactionById(income.Id, newAmount, newDate, newSource);
                    (bool isContactUpdated, Transaction? updatedTransaction) = this._financialTrackerService.GetTransactionIfExist(transactionId);
                    if (isContactUpdated && updatedTransaction != null)
                    {
                        ConsoleActivity.ShowEditIncomeHeader();
                        ConsoleActivity.PrintInConsole("Income edited successfully!!");
                        ConsoleActivity.PrintEmptyLine();
                        ConsoleActivity.PrintInConsole("Updated Income: ");
                        ConsoleActivity.PrintIncome(updatedTransaction);
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

        private void HandleEditExpense()
        {
            ConsoleActivity.ShowEditExpenseHeader();
            if (this._financialTrackerService.GetExpenseCount() < 1)
            {
                ConsoleActivity.ShowNoTransactionMessage();
                return;
            }

            (bool isValidId, int transactionId) = InputValidatorHelper.GetTransactionIdWithRetry(ConsoleActivity.ShowEditExpenseHeader);
            if (!isValidId)
            {
                return;
            }

            (bool isTransactionExist, Transaction? transaction) = this._financialTrackerService.GetTransactionIfExist(transactionId);
            if (!isTransactionExist)
            {
                ConsoleActivity.PrintInvalidMessage("Transaction Id not exist!!");
                return;
            }
            else if (transaction != null)
            {
                Enums.TransactionField userChoice;
                Console.WriteLine("Expense before editing :");
                ConsoleActivity.PrintExpense(transaction);
                ConsoleActivity.PrintEmptyLine();
                string? userChoiceInput = ConsoleActivity.ShowTransactionEditMenu("category");
                if (InputValidatorHelper.ValidateUserChoice(userChoiceInput, out int userChoiceInt) && transaction is Expense expense)
                {
                    userChoice = (Enums.TransactionField)userChoiceInt;
                    decimal newAmount = expense.Amount;
                    DateOnly newDate = expense.TransactionDate;
                    string? newCategory = expense.Category;
                    if (userChoice == Enums.TransactionField.Amount)
                    {
                        (bool isValidAmount, newAmount) = InputValidatorHelper.GetAmountWithRetry(ConsoleActivity.ShowEditExpenseHeader, "new expense amount");
                        if (!isValidAmount)
                        {
                            return;
                        }
                    }
                    else if (userChoice == Enums.TransactionField.TransactionDate)
                    {
                        (bool isValidDate, newDate) = InputValidatorHelper.GetTransactionDateWithRetry(ConsoleActivity.ShowEditExpenseHeader);
                        if (!isValidDate)
                        {
                            return;
                        }
                    }
                    else if (userChoice == Enums.TransactionField.SourceOrCategory)
                    {
                        newCategory = this.GetExpenseCategory(out bool isValidCategory);
                        if (!isValidCategory)
                        {
                            return;
                        }
                    }
                    else
                    {
                        ConsoleActivity.PrintInvalidMessage("Enter a valid choice!!");
                        return;
                    }

                    this._financialTrackerService.EditTransactionById(expense.Id, newAmount, newDate, newCategory);
                    (bool isContactUpdated, Transaction? updatedTransaction) = this._financialTrackerService.GetTransactionIfExist(transactionId);
                    if (isContactUpdated && updatedTransaction != null)
                    {
                        ConsoleActivity.ShowEditExpenseHeader();
                        ConsoleActivity.PrintInConsole("Expense edited successfully!!");
                        ConsoleActivity.PrintEmptyLine();
                        ConsoleActivity.PrintInConsole("Updated expense: ");
                        ConsoleActivity.PrintExpense(updatedTransaction);
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
            ConsoleActivity.ShowAddNewExpenseHeader();
            string[] expenseCategoryList = this._financialTrackerService.GetExpenseCategories();
            (bool isValidExpenseCategory, string? categorySelected) = InputValidatorHelper.GetSourceOrCategory(ConsoleActivity.ShowAddNewExpenseHeader, expenseCategoryList, isIncome);
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
            ConsoleActivity.ShowIncomeMenu();
            string[] incomeSourceList = this._financialTrackerService.GetIncomeSource();
            (bool isValidIncomeSource, string? sourceSelected) = InputValidatorHelper.GetSourceOrCategory(ConsoleActivity.ShowAddNewIncomeHeader, incomeSourceList, isIncome);
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
            ConsoleActivity.ShowSummaryHeader();
            (decimal totalIncome, bool isIncomePresent) = this._financialTrackerService.GetTotalIncome();
            if (!isIncomePresent)
            {
                ConsoleActivity.PrintInConsole("No Income!!");
            }
            else
            {
                ConsoleActivity.PrintInConsole($"Total Income  : Rs.{totalIncome}");
            }

            (decimal totalExpense, bool isExpensePresent) = this._financialTrackerService.GetTotalExpense();
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
