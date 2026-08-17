using Assignment4.Core.Model;
using ConsoleTables;

namespace Assignment4.View
{
    /// <summary>
    /// Handles user interaction activities by managing standard input and output streams via the console.
    /// </summary>
    public static class ConsoleActivity
    {
        private static ConsoleTable _incomeTable = new ConsoleTable("Transaction ID", "Amount", "Transaction Date", "Source");
        private static ConsoleTable _expenseTable = new ConsoleTable("Transaction ID", "Amount", "Transaction Date", "Category");

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
        /// Show the financial menu option available.
        /// </summary>
        /// <param name="header">Menu header</param>
        /// <param name="menuItem">List of menu item for transaction operation</param>
        public static void ShowFinancialTrackerMenu(string? header, string[] menuItem)
        {
            ShowFinancialTrackerHeader(header);
            PrintItems(menuItem);

            PrintInConsole(new string('-', 40));
        }

        /// <summary>
        /// Show the header for the transaction operation.
        /// </summary>
        /// <param name="header">Name of the header</param>
        public static void ShowFinancialTrackerHeader(string? header)
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole($"          {header}");
            PrintInConsole(new string('=', 40));
        }

        /// <summary>
        /// Clear the console.
        /// </summary>
        public static void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Prompts the user to press the Enter key to confirm an action.
        /// </summary>
        /// <returns>True if the user pressed Enter without typing text, otherwise false</returns>
        public static bool IsEmptyInputToConfirm()
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
        /// Prints a list of income transactions to the console in a clean, formatted table.
        /// </summary>
        /// <param name="transactions">The list of transaction records to display</param>
        public static void PrintIncomeInConsole(List<Transaction> transactions)
        {
            _incomeTable.Rows.Clear();
            foreach (Transaction transaction in transactions)
            {
                if (transaction is Income income)
                {
                    _incomeTable.AddRow(income.Id, income.Amount, income.TransactionDate, income.Source);
                }
            }

            _incomeTable.Write();
            Console.ReadKey();
        }

        /// <summary>
        /// Prints a list of expense transactions to the console in a clean, formatted table.
        /// </summary>
        /// <param name="transactions">The list of transaction records to display</param>
        public static void PrintExpenseInConsole(List<Transaction> transactions)
        {
            _expenseTable.Rows.Clear();
            foreach (Transaction transaction in transactions)
            {
                if (transaction is Expense expense)
                {
                    _expenseTable.AddRow(expense.Id, expense.Amount, expense.TransactionDate, expense.Category);
                }
            }

            _expenseTable.Write();
            Console.ReadKey();
        }

        /// <summary>
        /// Print the transaction details in console.
        /// </summary>
        /// <param name="transaction">Transaction to be printed</param>
        public static void PrintTransaction(Transaction transaction)
        {
            PrintEmptyLine();
            PrintInConsole("Transaction Id: " + transaction.Id);
            PrintInConsole("Transaction amount : " + transaction.Amount);
            PrintInConsole("Transaction Date : " + transaction.TransactionDate);
            if (transaction is Income income)
            {
                PrintInConsole("Transaction source : " + income.Source);
            }
            else if (transaction is Expense expense)
            {
                PrintInConsole("Transaction category : " + expense.Category);
            }

            PrintEmptyLine();
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
        /// Show transaction not found message
        /// </summary>
        public static void ShowNoTransactionMessage()
        {
            PrintEmptyLine();
            PrintInConsole("No transaction made until now!!\nAdd some transaction to perform operation!!!");
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
