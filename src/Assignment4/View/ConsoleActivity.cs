using ConsoleTables;
using FinanceTracker.Core.Model;

namespace FinanceTracker.View
{
    /// <summary>
    /// Handles user interaction activities by managing standard input and output via the console.
    /// </summary>
    public static class ConsoleActivity
    {
        private static ConsoleTable _incomeTable = new ConsoleTable("Transaction ID", "Amount", "Transaction Date", "Source");
        private static ConsoleTable _expenseTable = new ConsoleTable("Transaction ID", "Amount", "Transaction Date", "Category");

        /// <summary>
        /// Print the given content in the console.
        /// </summary>
        /// <param name="content">Content that need to be printed.</param>
        public static void PrintInConsole(string content)
        {
            Console.WriteLine(content);
        }

        /// <summary>
        /// Prompts the user and reads their text input from the console.
        /// </summary>
        /// <param name="label">Label that requested for input.</param>
        /// <returns>Text entered by the user.</returns>
        public static string? GetStringInput(string label)
        {
            PrintEmptyLine();
            Console.Write($"Enter the {label} : ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Wait in the console until user presses any key.
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
        /// <param name="content">Invalid message to be printed.</param>
        public static void PrintInvalidMessage(string content)
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
        /// <param name="header">Menu header.</param>
        /// <param name="menuItem">List of menu item for transaction operation.</param>
        public static void ShowMenu(string header, string[] menuItem)
        {
            ShowHeader(header);
            PrintItems(menuItem);
            PrintInConsole(new string('-', 40));
        }

        /// <summary>
        /// Show the header for the transaction operation.
        /// </summary>
        /// <param name="header">Name of the header.</param>
        public static void ShowHeader(string header)
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
        /// <returns>True if the user pressed Enter without typing text, otherwise false.</returns>
        public static bool IsEmptyInput()
        {
            return Console.ReadLine() == string.Empty;
        }

        /// <summary>
        /// Print the list of items in console.
        /// </summary>
        /// <param name="items">Items to be printed.</param>
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
        /// <param name="transactions">The list of transaction records to display.</param>
        public static void PrintIncome(List<Transaction> transactions)
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
        /// <param name="transactions">The list of transaction records to display.</param>
        public static void PrintExpense(List<Transaction> transactions)
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
        /// <param name="transaction">Transaction to be printed.</param>
        public static void PrintTransaction(Transaction transaction)
        {
            PrintEmptyLine();
            PrintInConsole($"Transaction Id: {transaction.Id}\nTransaction amount : {transaction.Amount}\nTransaction Date : {transaction.TransactionDate}");
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
        /// Exit from the expense tracker application.
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

        /// <summary>
        /// Print the content and wait in console.
        /// </summary>
        /// <param name="content">The content to be printed.</param>
        public static void PrintAndWait(string content)
        {
            PrintInConsole(content);
            WaitInConsole();
        }
    }
}
