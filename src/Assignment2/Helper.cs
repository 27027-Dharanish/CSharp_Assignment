using Assignment2.Model.BankingModels;
using Assignment2.View;

namespace Assignment2
{
    /// <summary>
    /// Helper class for application.
    /// </summary>
    public static class Helper
    {
        private static ConsoleActivity _console = new ConsoleActivity();

        /// <summary>
        /// Check whether the string is empty or not.
        /// </summary>
        /// <param name="content">string to be checked</param>
        /// <returns>Return whether the string is empty or not</returns>
        public static bool IsNotEmpty(string? content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if bankAccount is null.
        /// </summary>
        /// <param name="account">Account from the repository</param>
        /// <returns>True if account is not null and false if account is null</returns>
        public static bool IsBankAccountNull(BankAccount? account)
        {
            if (account == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check whether the string is digit or not.
        /// </summary>
        /// <param name="input">The input string to be checked</param>
        /// <returns>Return whether the input is digit or not</returns>
        public static bool IsNotDigit(string? input)
        {
            if (input == null)
            {
                return false;
            }

            return !input.Any(char.IsDigit);
        }

        /// <summary>
        /// Check whether the name is valid.
        /// </summary>
        /// <param name="name">Name to be checked</param>
        /// <returns>True if name is valid else false</returns>
        public static bool IsValidName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _console.PrintInConsole("Name cannot be empty!!");
                _console.WaitInConsole();
                return false;
            }
            else if (!Helper.IsNotDigit(name))
            {
                _console.PrintInConsole("Name cannot contain digit!!");
                _console.WaitInConsole();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the salary is valid.
        /// </summary>
        /// <param name="salary">Salary that needed to be checked</param>
        /// <returns>True if salary is valid else false</returns>
        public static bool IsValidSalary(string? salary)
        {
            if (salary == null)
            {
                _console.PrintInConsole("Salary cannot be null!!");
                return false;
            }
            else if (!salary.All(char.IsDigit))
            {
                _console.PrintInConsole("Salary must be in digits and cannot be negative!!");
                _console.WaitInConsole();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the amount is valid.
        /// </summary>
        /// <param name="amount">Amount that needed to be checked</param>
        /// <returns>True if amount is valid else false</returns>
        public static bool IsValidAmount(string? amount)
        {
            if (amount == null)
            {
                _console.PrintInConsole("Amount cannot be null!!");
                return false;
            }
            else if (!amount.All(char.IsDigit))
            {
                _console.PrintInConsole("Amount must be in digits and cannot be negative!!");
                _console.WaitInConsole();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the given value is negative or not.
        /// </summary>
        /// <param name="value">Value that needed to be check</param>
        /// <returns>True if negative number else false</returns>
        public static bool IsNegativeNumber(double value)
        {
            if (value < 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check whether the shape color is valid.
        /// </summary>
        /// <param name="color">Color of the shape</param>
        /// <returns>True if shape color is valid</returns>
        public static bool IsShapeColorValid(string? color)
        {
            if (!Helper.IsNotDigit(color))
            {
                _console.PrintInConsole("Shape color cannot be digit!!");
                _console.WaitInConsole();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(color))
            {
                _console.PrintInConsole("Shape color cannot be Empty!!");
                _console.WaitInConsole();
                return false;
            }

            return true;
        }
    }
}
