namespace Assignment2.View
{
    /// <summary>
    /// Handle User input and output via console.
    /// </summary>
    public class ConsoleActivity
    {
        /// <summary>
        /// Print the data in the console.
        /// </summary>
        /// <param name="data">The content to be printed in console</param>
        public void PrintInConsole(string? data)
        {
            Console.WriteLine(data);
        }

        /// <summary>
        /// Get the input from the console.
        /// </summary>
        /// <param name="content">Content to get form the console</param>
        /// <returns>Return the data got from the console</returns>
        public string? GetInputFromConsole(string? content)
        {
            Console.WriteLine("Enter the " + content + " : ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Print the invalid message in console.
        /// </summary>
        public void PrintInvalid()
        {
            Console.WriteLine("Invalid Input!!");
        }

        /// <summary>
        /// Wait in console until user click any key.
        /// </summary>
        public void WaitInConsole()
        {
            Console.WriteLine("Press any Key to continue!!");
            Console.ReadKey();
        }

        /// <summary>
        /// Clear the console.
        /// </summary>
        public void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Print invalid message for a particular field input.
        /// </summary>
        /// <param name="field">Name of the field</param>
        public void PrintInvalidField(string? field)
        {
            Console.WriteLine("Enter the valid " + field);
        }

        /// <summary>
        /// Print empty line in console.
        /// </summary>
        public void PrintEmptyLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Print breaker to make console more readable.
        /// </summary>
        public void PrintBreaker()
        {
            Console.WriteLine(new string('=', 15));
        }

        /// <summary>
        /// Show the shape available in the menu.
        /// </summary>
        /// <returns>Return the option selected by the user</returns>
        public string? ShowShapeAvailableMenu()
        {
            Console.Clear();
            Console.WriteLine("Create new :");
            Console.WriteLine("1.Rectangle");
            Console.WriteLine("2.Circle");
            Console.WriteLine("3.Exit");
            Console.WriteLine();
            Console.Write("Enter the option to perform : ");
            string? userChoice = Console.ReadLine();
            return userChoice;
        }

        /// <summary>
        /// Show the employee menu.
        /// </summary>
        /// <returns>Return user selected option</returns>
        public string? ShowEmployeeMenu()
        {
            Console.Clear();
            Console.WriteLine("Create new Employee Profile:");
            Console.WriteLine("1.Manager");
            Console.WriteLine("2.Developer");
            Console.WriteLine("3.Exit");
            Console.WriteLine("Enter the option : ");
            string? userChoice = Console.ReadLine();
            return userChoice;
        }

        /// <summary>
        /// Show the option available in bank operation.
        /// </summary>
        /// <returns>Return the option selected by the user</returns>
        public string? ShowBankOptionMenu()
        {
            Console.Clear();
            Console.WriteLine("!!Bank Application!!");
            Console.WriteLine();
            Console.WriteLine("Select the operation to perform :");
            Console.WriteLine("1.Create new account");
            Console.WriteLine("2.Log In to Existing account");
            Console.WriteLine("3.Exit");
            Console.WriteLine();
            Console.Write("Enter the option : ");
            string? userChoice = Console.ReadLine();
            return userChoice;
        }

        /// <summary>
        /// Show the menu option available in creating of new account.
        /// </summary>
        /// <returns>Return the user selected option</returns>
        public string? ShowCreateNewAccountMenu()
        {
            Console.Clear();
            Console.WriteLine("Account Creation!!");
            Console.WriteLine();
            Console.WriteLine("Select the type of account :");
            Console.WriteLine("1.Saving Account");
            Console.WriteLine("2.Checking Account");
            Console.WriteLine("3.Exit");
            Console.WriteLine();
            Console.Write("Account type : ");
            string? accountType = Console.ReadLine();
            return accountType;
        }
    }
}
