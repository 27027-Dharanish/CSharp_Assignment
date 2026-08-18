using Assignment4.Core.ExpenseTrackerInterface;
using Assignment4.Core.Model;
using Assignment4.FileHelper;
using Assignment4.View;

namespace Assignment4.Controller
{
    /// <summary>
    /// Handles the logic for managing and tracking user expenses.
    /// </summary>
    public class ExpenseTrackerController
    {
        private readonly string[] _financialMenu = { "View Summary", "Manage Income", "Manage Expense", "BackUp Repository", "Exit" };
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
        /// Starts the execution flow for the finance tracker.
        /// </summary>
        public void StartExpenseTracker()
        {
            Logger.WriteLog("Info", "Finance tracker started");
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
                Logger.WriteLog("Info", "Showing finance tracker menu");
                ConsoleActivity.ShowFinancialTrackerMenu("FINANCE TRACKER MENU", this._financialMenu);
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
                    case FinancialOption.BackUpRepository:
                        this.HandleBackup();
                        break;
                    case FinancialOption.Exit:
                        ConsoleActivity.ExitApplication();
                        break;
                    default:
                        Logger.WriteLog("Warn", "Invalid choice entered");
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
                Logger.WriteLog("Info", "Showing income menu");
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
                        Logger.WriteLog("Warn", "Invalid choice entered");
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
                Logger.WriteLog("Info", "Showing expense menu");
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
                        Logger.WriteLog("Warn", "Invalid choice entered");
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != TransactionOperation.Exit);
        }

        private void ExecuteAddNewTransaction(string? inputLabel, string? headerMessage, GetSourceOrCategory getSourceOrCategory, Func<decimal, DateOnly, string?, bool, bool> addNewTransaction, FinancialOption transactionType)
        {
            Logger.WriteLog("Info", "Execute add new transaction");
            (bool isValidAmount, decimal amount) = InputValidatorHelper.GetAmountWithRetry(headerMessage, inputLabel);
            if (!isValidAmount)
            {
                Logger.WriteLog("Warn", "Failed to get amount");
                return;
            }

            (bool isValidTransactionDate, DateOnly transactionDate) = InputValidatorHelper.GetTransactionDateWithRetry(headerMessage);
            if (!isValidTransactionDate)
            {
                Logger.WriteLog("Warn", "Failed to get transaction date");
                return;
            }

            string? sourceOrCategory = getSourceOrCategory(out bool isValidSourceOrCategory);
            if (!isValidSourceOrCategory)
            {
                Logger.WriteLog("Warn", "Failed to get transaction context");
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

            ConsoleActivity.ShowFinancialTrackerHeader(headerMessage);
            ConsoleActivity.PrintEmptyLine();
            Logger.WriteLog("Info", "Transaction adding requested");
            if (addNewTransaction(amount, transactionDate, sourceOrCategory, isIncome))
            {
                Logger.WriteLog("Success", "Transaction added");
                ConsoleActivity.PrintInConsole(inputLabel + " added successfully!!");
                ConsoleActivity.WaitInConsole();
            }
            else
            {
                Logger.WriteLog("Warn", "Transaction failed");
                ConsoleActivity.PrintInConsole(inputLabel + " failed!!");
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

            Logger.WriteLog("Info", "Show all transaction");
            ConsoleActivity.ShowFinancialTrackerHeader(headerMessage);
            List<Transaction> transaction = getTransaction();
            printTransaction(transaction);
        }

        private void ExecuteDeleteTransaction<T>(string? deleteLabel, string? headerMessage, Func<int> getTransactionCount)
            where T : Transaction
        {
            List<T> transactions = this._financialTrackerService.GetFilteredTransaction<T>();
            Logger.WriteLog("Info", "Execute delete transaction");
            ConsoleActivity.ShowFinancialTrackerHeader(headerMessage);
            if (getTransactionCount() < 1)
            {
                ConsoleActivity.ShowNoTransactionMessage();
                return;
            }

            (bool isValidId, int transactionId) = InputValidatorHelper.GetTransactionIdWithRetry(headerMessage);
            if (!isValidId)
            {
                Logger.WriteLog("Warn", "Failed to get id for deletion");
                return;
            }

            if (transactionId <= 0 || transactionId > transactions.Count)
            {
                Logger.WriteLog("Warn", "Transaction not exist");
                ConsoleActivity.PrintInvalidMessage("Transaction Id not exist!!");
                return;
            }

            (bool isTransactionExist, Transaction? transaction) = this._financialTrackerService.GetTransactionIfExist(transactions[transactionId - 1].Id);
            if (!isTransactionExist)
            {
                Logger.WriteLog("Warn", "Transaction not exist");
                ConsoleActivity.PrintInvalidMessage("Transaction Id not exist!!");
                return;
            }
            else if (transaction != null)
            {
                Logger.WriteLog("Info", "Transaction deletion requested");
                if (this._financialTrackerService.DeleteTransaction(transaction.Id))
                {
                    Logger.WriteLog("Success", "Transaction deleted");
                    ConsoleActivity.ShowFinancialTrackerHeader(headerMessage);
                    ConsoleActivity.PrintInConsole(deleteLabel + " deleted successfully!!");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    Logger.WriteLog("Warn", "Failed to delete transaction");
                    ConsoleActivity.ShowFinancialTrackerHeader(headerMessage);
                    ConsoleActivity.PrintInvalidMessage(deleteLabel + " deletion failed!!");
                }
            }
        }

        private void ExecuteEditTransaction<T>(string? inputLabel, string? header, Func<int> getTransactionCount)
            where T : Transaction
        {
            List<T> transactions = this._financialTrackerService.GetFilteredTransaction<T>();
            Logger.WriteLog("Info", "Execute edit transaction");
            ConsoleActivity.ShowFinancialTrackerHeader(header);
            if (getTransactionCount() < 1)
            {
                ConsoleActivity.ShowNoTransactionMessage();
                return;
            }

            (bool isValidId, int transactionId) = InputValidatorHelper.GetTransactionIdWithRetry(header);
            if (!isValidId)
            {
                Logger.WriteLog("Warn", "Failed to get transaction id");
                return;
            }

            if (transactionId <= 0 || transactionId > transactions.Count)
            {
                Logger.WriteLog("Warn", "Transaction not exist");
                ConsoleActivity.PrintInvalidMessage("Transaction Id not exist!!");
                return;
            }

            (bool isTransactionExist, Transaction? transaction) = this._financialTrackerService.GetTransactionIfExist(transactions[transactionId - 1].Id);
            if (!isTransactionExist || transaction is not T)
            {
                Logger.WriteLog("Warn", "Transaction id not exist");
                ConsoleActivity.PrintInvalidMessage("Transaction Id not exist!!");
                return;
            }
            else if (transaction != null)
            {
                Logger.WriteLog("Info", "Transaction editing requested");
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
                        Logger.WriteLog("Warn", "Invalid choice entered");
                        ConsoleActivity.PrintInvalidMessage("Enter a valid choice!!");
                        return;
                    }

                    this._financialTrackerService.EditTransactionById(matchedTransaction.Id, newAmount, newDate, context);
                    (bool isTransactionUpdated, Transaction? updatedTransaction) = this._financialTrackerService.GetTransactionIfExist(transactionId);
                    if (isTransactionUpdated && updatedTransaction != null)
                    {
                        Logger.WriteLog("Info", "Transaction editing requested");
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
                    Logger.WriteLog("Warn", "Invalid choice entered");
                    ConsoleActivity.PrintInvalidMessage("Enter a valid choice!!");
                    return;
                }
            }
        }

        private string? GetExpenseCategory(out bool isValidCategory)
        {
            Logger.WriteLog("Info", "Get expense category");
            bool isIncome = false;
            ConsoleActivity.ShowFinancialTrackerHeader("ADD NEW EXPENSE");
            string[] expenseCategoryList = this._financialTrackerService.GetExpenseCategories();
            (bool isValidExpenseCategory, string? categorySelected) = InputValidatorHelper.GetSourceOrCategory("ADD NEW EXPENSE", expenseCategoryList, isIncome);
            if (!isValidExpenseCategory)
            {
                Logger.WriteLog("Warn", "Failed to get expense category");
                isValidCategory = false;
                return default;
            }

            isValidCategory = true;
            return categorySelected;
        }

        private string? GetIncomeSource(out bool isValidSource)
        {
            Logger.WriteLog("Info", "Get income source");
            bool isIncome = true;
            ConsoleActivity.ShowFinancialTrackerHeader("ADD NEW INCOME");
            string[] incomeSourceList = this._financialTrackerService.GetIncomeSource();
            (bool isValidIncomeSource, string? sourceSelected) = InputValidatorHelper.GetSourceOrCategory("ADD NEW INCOME", incomeSourceList, isIncome);
            if (!isValidIncomeSource)
            {
                Logger.WriteLog("Warn", "Failed to get income source");
                isValidSource = false;
                return default;
            }

            isValidSource = true;
            return sourceSelected;
        }

        private void HandleBackup()
        {
            Logger.WriteLog("Info", "File backup");
            ConsoleActivity.ShowFinancialTrackerHeader("BACKUP FINANCIAL REPOSITORY");
            BackUpFile backup = new BackUpFile();
            if (backup.CreateBackUp())
            {
                ConsoleActivity.PrintInConsole("File Backup created successfully!");
            }
            else
            {
                ConsoleActivity.PrintInConsole("No file exist for backup");
            }

            ConsoleActivity.WaitInConsole();
        }

        private void HandleViewSummary()
        {
            Logger.WriteLog("Info", "Showing transaction summary");
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
