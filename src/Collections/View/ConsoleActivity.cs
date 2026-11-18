namespace Collections.View
{
    /// <summary>
    /// Handles user interaction activities by managing standard input and output via the console.
    /// </summary>
    public static class ConsoleActivity
    {
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
            Console.Write("\x1b[3J");
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
        /// Get integer value input from user.
        /// </summary>
        /// <param name="label">label of the input field.</param>
        /// <returns>The prompted integer value.</returns>
        public static int GetIntegerInput(string label)
        {
            string? userInput = GetStringInput(label);
            int.TryParse(userInput, out int value);
            return value;
        }

        /// <summary>
        /// Print and wait in console.
        /// </summary>
        /// <param name="content">Content to be printed in console.</param>
        public static void PrintAndWait(string content)
        {
            PrintInConsole(content);
            WaitInConsole();
        }
    }
}
