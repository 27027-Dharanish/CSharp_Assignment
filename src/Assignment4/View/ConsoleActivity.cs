using Assignment4.Core.Model;
using ConsoleTables;

namespace Assignment4.View
{
    /// <summary>
    /// Handles user interaction activities by managing standard input and output streams via the console.
    /// </summary>
    public static class ConsoleActivity
    {
        /// <summary>
        /// Print the given content in the console.
        /// </summary>
        /// <param name="content">Content that need to be printed</param>
        public static void PrintInConsole(string? content)
        {
            Console.WriteLine(content);
        }

        /// <summary>
        /// Prompts the user and reads their text input from the console.
        /// </summary>
        /// <param name="field">Field requested</param>
        /// <returns>Text entered by the user</returns>
        public static string? GetInputFromUser(string? field)
        {
            PrintEmptyLine();
            Console.Write($"Enter the {field} : ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Wait in until user press any key in console.
        /// </summary>
        public static void WaitInConsole()
        {
            PrintEmptyLine();
            PrintInConsole("Press any key to continue!!");
            Console.ReadKey();
        }

        /// <summary>
        /// Print invalid field warning in console.
        /// </summary>
        /// <param name="content">Field that raise invalid request</param>
        public static void PrintInvalidMessage(string? content)
        {
            PrintEmptyLine();
            PrintInConsole(content);
            WaitInConsole();
        }

        /// <summary>
        /// Print empty line in console.
        /// </summary>
        public static void PrintEmptyLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Show the menu option available in the financial tracker.
        /// </summary>
        public static void ShowFinancialTrackerMenu()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          EXPENSE TRACKER MENU");
            PrintInConsole(new string('=', 40));
            PrintInConsole(" 1. View Summary\n 2. Manage Income\n 3. Manage Expense\n 4. Exit");
            PrintInConsole(new string('-', 40));
        }

        /// <summary>
        /// Clear the console.
        /// </summary>
        public static void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Show the menu option available in income menu option.
        /// </summary>
        public static void ShowIncomeMenu()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          MANAGE  INCOME");
            PrintInConsole(new string('=', 40));
            PrintInConsole(" 1. Add New Income\n 2. View All Income\n 3. Edit Income\n 4. Delete Income\n 5. Exit");
            PrintInConsole(new string('-', 40));
        }

        /// <summary>
        /// Show the menu option available in expense menu option.
        /// </summary>
        public static void ShowExpenseMenu()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          MANAGE  EXPENSE");
            PrintInConsole(new string('=', 40));
            PrintInConsole(" 1. Add New Expense\n 2. View All Expense\n 3. Edit Expense\n 4. Delete Expense\n 5. Exit");
            PrintInConsole(new string('-', 40));
        }

        /// <summary>
        /// Print the income header information in console.
        /// </summary>
        public static void ShowAddNewIncomeHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          ADD NEW INCOME");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Prompts the user to press the Enter key to confirm an action.
        /// </summary>
        /// <returns>True if the user pressed Enter without typing text, otherwise false</returns>
        public static bool PressEnterToConfirm()
        {
            return Console.ReadLine() == string.Empty;
        }

        /// <summary>
        /// Print the list of items in console.
        /// </summary>
        /// <param name="items">Items to be printed</param>
        public static void PrintItems(string[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                PrintInConsole($"{i + 1}. {items[i]}");
            }
        }

        /// <summary>
        /// Print the expense header information in console.
        /// </summary>
        public static void ShowAddNewExpenseHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          ADD NEW EXPENSE");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Print the view income header information in console.
        /// </summary>
        public static void ShowViewIncomeHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          VIEW ALL INCOME");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Print the view expense header information in console.
        /// </summary>
        public static void ShowViewExpenseHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          VIEW ALL EXPENSE");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Print the edit income header information in console.
        /// </summary>
        public static void ShowEditIncomeHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          EDIT INCOME");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Print the edit expense header information in console.
        /// </summary>
        public static void ShowEditExpenseHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          EDIT EXPENSE");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Prints a list of income transactions to the console in a clean, formatted table.
        /// </summary>
        /// <param name="transactions">The list of transaction records to display</param>
        public static void PrintIncomeInConsole(List<Transaction> transactions)
        {
            var incomeTable = new ConsoleTable("Transaction ID", "Amount", "Transaction Date", "Source");
            foreach (Transaction transaction in transactions)
            {
                if (transaction is Income income)
                {
                    incomeTable.AddRow(income.Id, income.Amount, income.TransactionDate, income.Source);
                }
            }

            incomeTable.Write();
            Console.ReadKey();
        }

        /// <summary>
        /// Prints a list of expense transactions to the console in a clean, formatted table.
        /// </summary>
        /// <param name="transactions">The list of transaction records to display</param>
        public static void PrintExpenseInConsole(List<Transaction> transactions)
        {
            var expenseTable = new ConsoleTable("Transaction ID", "Amount", "Transaction Date", "Category");
            foreach (Transaction transaction in transactions)
            {
                if (transaction is Expense expense)
                {
                    expenseTable.AddRow(expense.Id, expense.Amount, expense.TransactionDate, expense.Category);
                }
            }

            expenseTable.Write();
            Console.ReadKey();
        }

        /// <summary>
        /// Print the income details in console.
        /// </summary>
        /// <param name="transaction">Income transaction</param>
        public static void PrintIncome(Transaction transaction)
        {
            PrintEmptyLine();
            if (transaction is Income income)
            {
                PrintEmptyLine();
                PrintInConsole("Transaction Id: " + income.Id);
                PrintInConsole("Income amount : " + income.Amount);
                PrintInConsole("Transaction Date : " + income.TransactionDate);
                PrintInConsole("Income Source : " + income.Source);
                PrintEmptyLine();
            }
        }

        /// <summary>
        /// Print the expense details in console.
        /// </summary>
        /// <param name="transaction">Expense transaction</param>
        public static void PrintExpense(Transaction transaction)
        {
            ShowEditExpenseHeader();
            PrintEmptyLine();
            if (transaction is Expense expense)
            {
                PrintEmptyLine();
                PrintInConsole("Transaction Id: " + expense.Id);
                PrintInConsole("Income amount : " + expense.Amount);
                PrintInConsole("Transaction Date : " + expense.TransactionDate);
                PrintInConsole("Expense category : " + expense.Category);
                PrintEmptyLine();
            }
        }

        /// <summary>
        /// Displays a menu allowing the user to select which field of a transaction they want to edit.
        /// </summary>
        /// <param name="transactionType">dynamic category name to show for option</param>
        /// <returns>The menu option string typed by the user</returns>
        public static string? ShowTransactionEditMenu(string? transactionType)
        {
            PrintEmptyLine();
            PrintInConsole("Choose field to edit : ");
            PrintInConsole(" 1. Amount");
            PrintInConsole(" 2. Transaction Date");
            PrintInConsole($" 3. {transactionType}");
            PrintEmptyLine();
            return GetInputFromUser("option");
        }

        /// <summary>
        /// Print delete income header.
        /// </summary>
        public static void ShowDeleteIncomeHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          DELETE INCOME");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Print delete expense header.
        /// </summary>
        public static void ShowDeleteExpenseHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          DELETE EXPENSE");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Print the summary header in console.
        /// </summary>
        public static void ShowSummaryHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("          INCOME-EXPENSE SUMMARY");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Show transaction not found message
        /// </summary>
        public static void ShowNoTransactionMessage()
        {
            PrintEmptyLine();
            PrintInConsole("No transaction made until now!!");
            PrintInConsole("Add some transaction to perform operation!!!");
            WaitInConsole();
        }

        /// <summary>
        /// exit from the expense tracker application
        /// </summary>
        public static void ExitApplication()
        {
            ClearConsole();
            PrintInConsole(new string('=', 70));
            PrintInConsole("          Thank you for using the application");
            PrintInConsole(new string('=', 70));
            PrintEmptyLine();
            WaitInConsole();
        }
    }
}
