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
            Console.WriteLine("Press any key to continue!!");
            Console.ReadKey();
        }

        /// <summary>
        /// Print invalid field warning in console.
        /// </summary>
        /// <param name="content">Field that raise invalid request</param>
        public static void PrintInvalidMessage(string? content)
        {
            PrintEmptyLine();
            Console.WriteLine(content);
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
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          EXPENSE TRACKER MENU");
            Console.WriteLine(new string('=', 40));
            Console.WriteLine(" 1. View Summary\n 2. Manage Income\n 3. Manage Expense\n 4. Exit");
            Console.WriteLine(new string('-', 40));
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
            Console.Clear();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          MANAGE  INCOME");
            Console.WriteLine(new string('=', 40));
            Console.WriteLine(" 1. Add New Income\n 2. View All Income\n 3. Edit Income\n 4. Delete Income\n 5. Exit");
            Console.WriteLine(new string('-', 40));
        }

        /// <summary>
        /// Show the menu option available in expense menu option.
        /// </summary>
        public static void ShowExpenseMenu()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          MANAGE  EXPENSE");
            Console.WriteLine(new string('=', 40));
            Console.WriteLine(" 1. Add New Expense\n 2. View All Expense\n 3. Edit Expense\n 4. Delete Expense\n 5. Exit");
            Console.WriteLine(new string('-', 40));
        }

        /// <summary>
        /// Print the income header information in console.
        /// </summary>
        public static void ShowAddNewIncomeHeader()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          ADD NEW INCOME");
            Console.WriteLine(new string('=', 40));
            ConsoleActivity.PrintEmptyLine();
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
                Console.WriteLine($"{i + 1}. {items[i]}");
            }
        }

        /// <summary>
        /// Print the expense header information in console.
        /// </summary>
        public static void ShowAddNewExpenseHeader()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          ADD NEW EXPENSE");
            Console.WriteLine(new string('=', 40));
            ConsoleActivity.PrintEmptyLine();
        }

        /// <summary>
        /// Print the view income header information in console.
        /// </summary>
        public static void ShowViewIncomeHeader()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          VIEW ALL INCOME");
            Console.WriteLine(new string('=', 40));
            ConsoleActivity.PrintEmptyLine();
        }

        /// <summary>
        /// Print the view expense header information in console.
        /// </summary>
        public static void ShowViewExpenseHeader()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          VIEW ALL EXPENSE");
            Console.WriteLine(new string('=', 40));
            ConsoleActivity.PrintEmptyLine();
        }

        /// <summary>
        /// Print the edit income header information in console.
        /// </summary>
        public static void ShowEditIncomeHeader()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          EDIT INCOME");
            Console.WriteLine(new string('=', 40));
            ConsoleActivity.PrintEmptyLine();
        }

        /// <summary>
        /// Print the edit expense header information in console.
        /// </summary>
        public static void ShowEditExpenseHeader()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          EDIT EXPENSE");
            Console.WriteLine(new string('=', 40));
            ConsoleActivity.PrintEmptyLine();
        }

        /// <summary>
        /// Prints a list of income transactions to the console in a clean, formatted table.
        /// </summary>
        /// <param name="incomeTransaction">The list of income records to display</param>
        public static void PrintIncomeInConsole(List<Income> incomeTransaction)
        {
            var incomeTable = new ConsoleTable("Transaction ID", "Amount", "Transaction Date", "Source");
            foreach (Income income in incomeTransaction)
            {
                incomeTable.AddRow(income.Id, income.Amount, income.TransactionDate, income.Source);
            }

            incomeTable.Write();
            Console.ReadKey();
        }

        /// <summary>
        /// Prints a list of expense transactions to the console in a clean, formatted table.
        /// </summary>
        /// <param name="expenseTransaction">The list of expense records to display</param>
        public static void PrintExpenseInConsole(List<Expense> expenseTransaction)
        {
            var expenseTable = new ConsoleTable("Transaction ID", "Amount", "Transaction Date", "Category");
            foreach (Expense expense in expenseTransaction)
            {
                expenseTable.AddRow(expense.Id, expense.Amount, expense.TransactionDate, expense.Category);
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
            ShowEditIncomeHeader();
            PrintEmptyLine();
            if (transaction is Income income)
            {
                PrintEmptyLine();
                Console.WriteLine("Transaction Id: " + income.Id);
                Console.WriteLine("Income amount : " + income.Amount);
                Console.WriteLine("Transaction Date : " + income.TransactionDate);
                Console.WriteLine("Income Source : " + income.Source);
                Console.WriteLine();
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
                Console.WriteLine("Transaction Id: " + expense.Id);
                Console.WriteLine("Income amount : " + expense.Amount);
                Console.WriteLine("Transaction Date : " + expense.TransactionDate);
                Console.WriteLine("Expense category : " + expense.Category);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays a menu allowing the user to select which field of a transaction they want to edit.
        /// </summary>
        /// <param name="transactionType">dynamic category name to show for option</param>
        /// <returns>The menu option string typed by the user</returns>
        public static string? ShowTransactionEditMenu(string? transactionType)
        {
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Choose field to edit : ");
            Console.WriteLine(" 1. Amount");
            Console.WriteLine(" 2. Transaction Date");
            Console.WriteLine($" 3. {transactionType}");
            ConsoleActivity.PrintEmptyLine();
            return GetInputFromUser("option");
        }

        /// <summary>
        /// Print delete income header.
        /// </summary>
        public static void ShowDeleteIncomeHeader()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          DELETE INCOME");
            Console.WriteLine(new string('=', 40));
            ConsoleActivity.PrintEmptyLine();
        }

        /// <summary>
        /// Print delete expense header.
        /// </summary>
        public static void ShowDeleteExpenseHeader()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          DELETE EXPENSE");
            Console.WriteLine(new string('=', 40));
            ConsoleActivity.PrintEmptyLine();
        }
    }
}
