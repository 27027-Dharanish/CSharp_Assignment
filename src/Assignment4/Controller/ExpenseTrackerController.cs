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
                Logger.WriteLog("Info", "Showing finance tracker menu");
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

        private void ManageIncome()
        {
            TransactionOperation userChoice;
            do
            {
                Logger.WriteLog("Info", "Showing income menu");
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
                        Logger.WriteLog("Warn", "Invalid choice entered");
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
                Logger.WriteLog("Info", "Showing expense menu");
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
                        Logger.WriteLog("Warn", "Invalid choice entered");
                        ConsoleActivity.PrintInvalidMessage("Invalid choice!!");
                        break;
                }
            }
            while (userChoice != TransactionOperation.Back);
        }

        private void AddTransaction(GetSourceOrCategory getSourceOrCategory, bool isIncome)
        {
            Logger.WriteLog("Info", "Execute add new transaction");
            string inputLabel = isIncome ? "Income" : "Expense";
            string headerMessage = isIncome ? "ADD NEW INCOME" : "ADD NEW EXPENSE";
            (bool isValidAmount, decimal amount) = InputGetter.GetAmountWithRetry(headerMessage, inputLabel);
            if (!isValidAmount)
            {
                Logger.WriteLog("Warn", "Failed to get amount");
                return;
            }

            (bool isValidTransactionDate, DateOnly transactionDate) = InputGetter.GetTransactionDateWithRetry(headerMessage);
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
            
            Logger.WriteLog("Info", "Transaction adding requested");
            ConsoleActivity.ShowHeader(headerMessage);
            if (addNewTransaction(amount, transactionDate, sourceOrCategory, isIncome))
            {
                Logger.WriteLog("Success", "Transaction added");
                ConsoleActivity.PrintEmptyLine();
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

            Logger.WriteLog("Info", "Show all transaction");
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
                    Logger.WriteLog("Info", "Transaction deletion");
                    ConsoleActivity.PrintInConsole(deleteLabel + " deleted successfully!!");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    Logger.WriteLog("Warn", "Failed to delete transaction");
                    ConsoleActivity.PrintInvalidMessage(deleteLabel + " deletion failed!!");
                }
            }
        }

        private void EditTransaction<T>(Func<int> getTransactionCount, bool isIncome)
            where T : Transaction
        {
            List<T> transactions = this._financialTrackerService.GetFilteredTransaction<T>();
            Logger.WriteLog("Info", "Execute edit transaction");
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
                        Logger.WriteLog("Warn", "Invalid choice entered");
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
                    Logger.WriteLog("Warn", "Invalid choice entered");
                    ConsoleActivity.PrintInvalidMessage("Enter a valid choice!!");
                    return;
                }
            }
        }

        private string? GetExpenseCategory(out bool isValidCategory)
        {
            Logger.WriteLog("Info", "Get expense category");
            ConsoleActivity.ShowHeader("ADD NEW EXPENSE");
            string[] expenseCategoryList = this._financialTrackerService.GetExpenseCategories();
            (bool isValidExpenseCategory, string? categorySelected) = InputGetter.GetTransactionTag("ADD NEW EXPENSE", expenseCategoryList, false);
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
            ConsoleActivity.ShowHeader("ADD NEW INCOME");
            string[] incomeSourceList = this._financialTrackerService.GetIncomeSource();
            (bool isValidIncomeSource, string? sourceSelected) = InputGetter.GetTransactionTag("ADD NEW INCOME", incomeSourceList, true);
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
