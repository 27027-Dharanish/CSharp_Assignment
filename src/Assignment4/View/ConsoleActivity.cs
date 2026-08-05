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
        public static void PrintInvalidField(string? content)
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
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("          EXPENSE TRACKER MENU          ");
            Console.WriteLine(new string('=', 40));
            Console.WriteLine(" 1. View Summary\n 2. Manage Income\n 3. Manage Expense\n 4. Exit");
            Console.WriteLine("----------------------------------------");
        }
    }
}
