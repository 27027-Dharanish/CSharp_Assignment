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
            int userChoice;
            do
            {
                ConsoleActivity.ShowFinancialTrackerMenu();
                userChoice = ExpenseHelper.GetMenuChoiceFromUser();
                switch (userChoice)
                {
                    case (int)Enums.FinancialOption.ViewSummary:
                        this.HandleViewSummary();
                        break;
                    case (int)Enums.FinancialOption.ManageIncome:
                        this.HandleManageIncome();
                        break;
                    case (int)Enums.FinancialOption.ManageExpense:
                        this.HandleManageExpense();
                        break;
                    case (int)Enums.FinancialOption.Exit:
                        // This case is just to escape from the default being executing.
                        break;
                    default:
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != (int)Enums.FinancialOption.Exit);
        }

        private void HandleManageIncome()
        {
            int userChoice;
            do
            {
                ConsoleActivity.ShowIncomeMenu();
                userChoice = ExpenseHelper.GetMenuChoiceFromUser();
                switch (userChoice)
                {
                    case (int)Enums.TransactionOperation.AddNewTransaction:
                        this.HandleAddNewIncome();
                        break;
                    case (int)Enums.TransactionOperation.ViewTransaction:
                        this.HandleViewAllIncome();
                        break;
                    case (int)Enums.TransactionOperation.EditTransaction:
                        this.HandleEditIncome();
                        break;
                    case (int)Enums.TransactionOperation.DeleteTransaction:
                        this.HandleDeleteIncome();
                        break;
                    case (int)Enums.TransactionOperation.Exit:
                        // This case is just to escape from the default being executing.
                        break;
                    default:
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != (int)Enums.TransactionOperation.Exit);
        }

        private void HandleAddNewIncome()
        {
            (bool isValidIncomeAmount, decimal incomeAmount) = ExpenseHelper.GetAmountWithRetry(ConsoleActivity.ShowAddNewIncomeHeader, "income amount");
            if (!isValidIncomeAmount)
            {
                return;
            }

            (bool isValidTransactionDate, DateOnly transactionDate) = ExpenseHelper.GetTransactionDateWithRetry(ConsoleActivity.ShowAddNewIncomeHeader);
            if (!isValidTransactionDate)
            {
                return;
            }

            string? incomeSource = this.GetIncomeSource(out bool isValidSource);
            if (!isValidSource)
            {
                return;
            }

            if (this._financialTrackerService.AddNewIncome(incomeAmount, transactionDate, incomeSource))
            {
                ConsoleActivity.ShowAddNewIncomeHeader();
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole("Income added successfully!!");
                ConsoleActivity.WaitInConsole();
            }
        }

        private string? GetIncomeSource(out bool isValidSource)
        {
            bool isIncome = true;
            ConsoleActivity.ShowIncomeMenu();
            string[] incomeSourceList = this._financialTrackerService.GetIncomeSource();
            (bool isValidIncomeSource, string? sourceSelected) = ExpenseHelper.GetSourceOrCategory(ConsoleActivity.ShowAddNewIncomeHeader, incomeSourceList, isIncome);
            if (!isValidIncomeSource)
            {
                isValidSource = false;
                return default;
            }

            isValidSource = true;
            return sourceSelected;
        }

        private void HandleViewAllIncome()
        {
            ConsoleActivity.ShowViewIncomeHeader();
            List<Income> incomeTransaction = this._financialTrackerService.GetAllIncome();
            ConsoleActivity.PrintIncomeInConsole(incomeTransaction);
        }

        private void HandleEditIncome()
        {
            ConsoleActivity.ShowEditIncomeHeader();
            (bool isValidId, int transactionId) = ExpenseHelper.GetTransactionIdWithRetry(ConsoleActivity.ShowEditIncomeHeader);
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
                Console.WriteLine("Income before editing :");
                ConsoleActivity.PrintIncome(transaction);
                ConsoleActivity.PrintEmptyLine();
                string? userChoiceInput = ConsoleActivity.ShowTransactionEditMenu("source");
                if (ExpenseHelper.ValidateUserChoice(userChoiceInput, out int userChoice) && transaction is Income income)
                {
                    decimal newAmount = income.Amount;
                    DateOnly newDate = income.TransactionDate;
                    string? newSource = income.Source;
                    if (userChoice == (int)Enums.TransactionField.Amount)
                    {
                        (bool isValidAmount, newAmount) = ExpenseHelper.GetAmountWithRetry(ConsoleActivity.ShowEditIncomeHeader, "new income amount");
                        if (!isValidAmount)
                        {
                            return;
                        }
                    }
                    else if (userChoice == (int)Enums.TransactionField.TransactionDate)
                    {
                        (bool isValidDate, newDate) = ExpenseHelper.GetTransactionDateWithRetry(ConsoleActivity.ShowEditIncomeHeader);
                        if (!isValidDate)
                        {
                            return;
                        }
                    }
                    else if (userChoice == (int)Enums.TransactionField.SourceOrCategory)
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
                    ConsoleActivity.PrintInConsole("Income edited successfully!!");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    ConsoleActivity.PrintInvalidMessage("Enter a valid choice!!");
                    return;
                }
            }
        }

        private void HandleDeleteIncome()
        {
            ConsoleActivity.ShowDeleteIncomeHeader();
            (bool isValidId, int transactionId) = ExpenseHelper.GetTransactionIdWithRetry(ConsoleActivity.ShowEditIncomeHeader);
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
                    ConsoleActivity.ShowDeleteIncomeHeader();
                    ConsoleActivity.PrintInConsole("Income deleted successfully!!");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    ConsoleActivity.PrintInvalidMessage("Income deletion failed!!");
                }
            }
        }

        private void HandleManageExpense()
        {
            int userChoice;
            do
            {
                ConsoleActivity.ShowExpenseMenu();
                userChoice = ExpenseHelper.GetMenuChoiceFromUser();
                switch (userChoice)
                {
                    case (int)Enums.TransactionOperation.AddNewTransaction:
                        this.HandleAddNewExpense();
                        break;
                    case (int)Enums.TransactionOperation.ViewTransaction:
                        this.HandleViewAllExpense();
                        break;
                    case (int)Enums.TransactionOperation.EditTransaction:
                        this.HandleEditExpense();
                        break;
                    case (int)Enums.TransactionOperation.DeleteTransaction:
                        this.HandleDeleteExpense();
                        break;
                    case (int)Enums.TransactionOperation.Exit:
                        // This case is just to escape from the default being executing.
                        break;
                    default:
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != (int)Enums.TransactionOperation.Exit);
        }

        private void HandleAddNewExpense()
        {
            (bool isValidExpenseAmount, decimal expenseAmount) = ExpenseHelper.GetAmountWithRetry(ConsoleActivity.ShowAddNewExpenseHeader, "expense amount");
            if (!isValidExpenseAmount)
            {
                return;
            }

            (bool isValidTransactionDate, DateOnly transactionDate) = ExpenseHelper.GetTransactionDateWithRetry(ConsoleActivity.ShowAddNewExpenseHeader);
            if (!isValidTransactionDate)
            {
                return;
            }

            string? expenseCategory = this.GetExpenseCategory(out bool isValidCategory);
            if (!isValidCategory)
            {
                return;
            }

            if (this._financialTrackerService.AddNewExpense(expenseAmount, transactionDate, expenseCategory))
            {
                ConsoleActivity.ShowAddNewExpenseHeader();
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole("Expense added successfully!!");
                ConsoleActivity.WaitInConsole();
            }
        }

        private string? GetExpenseCategory(out bool isValidCategory)
        {
            bool isIncome = false;
            ConsoleActivity.ShowAddNewExpenseHeader();
            string[] expenseCategoryList = this._financialTrackerService.GetExpenseCategories();
            (bool isValidExpenseCategory, string? categorySelected) = ExpenseHelper.GetSourceOrCategory(ConsoleActivity.ShowAddNewExpenseHeader, expenseCategoryList, isIncome);
            if (!isValidExpenseCategory)
            {
                isValidCategory = false;
                return default;
            }

            isValidCategory = true;
            return categorySelected;
        }

        private void HandleViewAllExpense()
        {
            ConsoleActivity.ShowViewExpenseHeader();
            List<Expense> expenseTransaction = this._financialTrackerService.GetAllExpense();
            ConsoleActivity.PrintExpenseInConsole(expenseTransaction);
        }

        private void HandleEditExpense()
        {
            ConsoleActivity.ShowEditExpenseHeader();
            (bool isValidId, int transactionId) = ExpenseHelper.GetTransactionIdWithRetry(ConsoleActivity.ShowEditExpenseHeader);
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
                Console.WriteLine("Expense before editing :");
                ConsoleActivity.PrintExpense(transaction);
                ConsoleActivity.PrintEmptyLine();
                string? userChoiceInput = ConsoleActivity.ShowTransactionEditMenu("category");
                if (ExpenseHelper.ValidateUserChoice(userChoiceInput, out int userChoice) && transaction is Expense expense)
                {
                    decimal newAmount = expense.Amount;
                    DateOnly newDate = expense.TransactionDate;
                    string? newCategory = expense.Category;
                    if (userChoice == (int)Enums.TransactionField.Amount)
                    {
                        (bool isValidAmount, newAmount) = ExpenseHelper.GetAmountWithRetry(ConsoleActivity.ShowEditExpenseHeader, "new expense amount");
                        if (!isValidAmount)
                        {
                            return;
                        }
                    }
                    else if (userChoice == (int)Enums.TransactionField.TransactionDate)
                    {
                        (bool isValidDate, newDate) = ExpenseHelper.GetTransactionDateWithRetry(ConsoleActivity.ShowEditExpenseHeader);
                        if (!isValidDate)
                        {
                            return;
                        }
                    }
                    else if (userChoice == (int)Enums.TransactionField.SourceOrCategory)
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
                    ConsoleActivity.PrintInConsole("Expense edited successfully!!");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    ConsoleActivity.PrintInvalidMessage("Enter a valid choice!!");
                    return;
                }
            }
        }

        private void HandleDeleteExpense()
        {
            ConsoleActivity.ShowDeleteExpenseHeader();
            (bool isValidId, int transactionId) = ExpenseHelper.GetTransactionIdWithRetry(ConsoleActivity.ShowDeleteExpenseHeader);
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
                    ConsoleActivity.ShowDeleteExpenseHeader();
                    ConsoleActivity.PrintInConsole("Expense deleted successfully!!");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    ConsoleActivity.PrintInvalidMessage("Expense deletion failed!!");
                }
            }
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
                ConsoleActivity.PrintInConsole($"Total Income  : {totalIncome}");
            }

            (decimal totalExpense, bool isExpensePresent) = this._financialTrackerService.GetTotalExpense();
            if (!isExpensePresent)
            {
                ConsoleActivity.PrintInConsole("No Expense!!");
            }
            else
            {
                ConsoleActivity.PrintInConsole($"Total Expense : {totalExpense}");
            }

            ConsoleActivity.PrintInConsole($"Net Balance   : {totalIncome - totalExpense}");
            if (totalIncome - totalExpense < 0)
            {
                ConsoleActivity.PrintInConsole("Your net balance is negative. Expense crossed the your total income!!");
            }

            ConsoleActivity.WaitInConsole();
        }
    }
}
