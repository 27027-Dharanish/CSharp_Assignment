using System;
using System.ComponentModel.Design;
using System.Runtime;
using Assignment4.View;

namespace Assignment4
{
    /// <summary>
    /// Provides helper methods to validate user input.
    /// </summary>
    public static class InputValidatorHelper
    {
        /// <summary>
        /// Prompts the user for an amount with retry attempts.
        /// </summary>/// <param name="actionHeader">Determines whether to display the header that requires an ID.</param>
        /// <param name="field">The name of the input field to display to the user</param>
        /// <returns>A tuple containing a success flag (bool) and the validated amount (decimal)returns>
        public static (bool, decimal) GetAmountWithRetry(Action actionHeader, string? field)
        {
            int userAttempt = 4;
            do
            {
                ConsoleActivity.ClearConsole();
                actionHeader();
                string? userAmount = ConsoleActivity.GetInputFromUser(field);
                if (string.IsNullOrWhiteSpace(userAmount) || !userAmount.All(char.IsDigit))
                {
                    ConsoleActivity.PrintInConsole("Amount must contain numbers only!!");
                    userAttempt--;
                }
                else if (decimal.TryParse(userAmount, out decimal amount))
                {
                    if (amount == 0)
                    {
                        ConsoleActivity.PrintInConsole("Amount cannot be Rs.0 ....");
                        userAttempt--;
                    }
                    else
                    {
                        return (true, amount);
                    }
                }
                else
                {
                    ConsoleActivity.PrintInConsole("!!Amount exceeded the range " + decimal.MaxValue);
                    userAttempt--;
                }

                ConsoleActivity.PrintInConsole($"{userAttempt} attempts remaining!!");
                ConsoleActivity.WaitInConsole();
            }
            while (userAttempt > 0);
            return (false, default);
        }

        /// <summary>
        /// Prompts the user for a transaction date with retry attempts
        /// </summary>
        /// <param name="action">Determines whether to display the header that requires date.</param>
        /// <returns>A tuple containing a success flag (bool) and the validated transaction date (DateTime)</returns>
        public static (bool, DateOnly) GetTransactionDateWithRetry(Action action)
        {
            int userAttempt = 4;
            do
            {
                ConsoleActivity.ClearConsole();
                action();
                ConsoleActivity.PrintInConsole("The transaction date must follow the format (DD-MM-YYYY or DD/MM/YYYY)");
                ConsoleActivity.PrintInConsole("Or Just press enter to add today's date");
                ConsoleActivity.PrintEmptyLine();
                string? userDate = ConsoleActivity.GetInputFromUser("transaction date");
                if (userDate == null)
                {
                    ConsoleActivity.PrintInConsole("Date cannot be null!!");
                    userAttempt--;
                }
                else if (string.IsNullOrWhiteSpace(userDate))
                {
                    action();
                    ConsoleActivity.PrintInConsole($"Transaction date : {DateOnly.FromDateTime(DateTime.Today)}");
                    ConsoleActivity.PrintInConsole("Press enter to confirm!!");
                    if (ConsoleActivity.PressEnterToConfirm())
                    {
                        return (true, DateOnly.FromDateTime(DateTime.Today));
                    }

                    userAttempt--;
                }
                else if (DateOnly.TryParse(userDate, out DateOnly transactionDate))
                {
                    if (transactionDate >= DateOnly.FromDateTime(DateTime.Today))
                    {
                        ConsoleActivity.PrintInConsole("Transaction date must not be future!!");
                        userAttempt--;
                    }
                    else
                    {
                        return (true, transactionDate);
                    }
                }
                else
                {
                    ConsoleActivity.PrintInConsole("The transaction date must follow the format (DD-MM-YYYY or DD/MM/YYYY)");
                    userAttempt--;
                }

                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole($"{userAttempt} attempts remaining!!");
                ConsoleActivity.WaitInConsole();
            }
            while (userAttempt > 0);
            return (false, default);
        }

        /// <summary>
        /// Prompts the user to select a source or category from a list with retry attempts.
        /// </summary>
        /// <param name="action">The console header menu screen.</param>
        /// <param name="predefinedList">The list of valid options the user can choose from.</param>
        /// <param name="isIncome">True if selecting an income source; false if an expense category.</param>
        /// <returns>A tuple containing a true/false success status and the selected string value.</returns>
        public static (bool, string?) GetSourceOrCategory(Action action, string[] predefinedList, bool isIncome)
        {
            int userAttempt = 4;
            do
            {
                action();
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole(isIncome ? "Select the source of income:" : "Select the expense category:");
                ConsoleActivity.PrintItems(predefinedList);
                string? userChoiceInput = ConsoleActivity.GetInputFromUser($"option [1-{predefinedList.Length}]");
                if (ValidateUserChoice(userChoiceInput, out int userChoice))
                {
                    if (userChoice > 0 && userChoice <= predefinedList.Length)
                    {
                        return (true, predefinedList[userChoice - 1]);
                    }
                    else
                    {
                        ConsoleActivity.PrintInConsole($"Choice must be within range 1-{predefinedList.Length}");
                        userAttempt--;
                    }
                }
                else
                {
                    ConsoleActivity.PrintInConsole("Choice must be number!!");
                    userAttempt--;
                }

                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole($"{userAttempt} attempts remaining!!");
                ConsoleActivity.WaitInConsole();
            }
            while (userAttempt > 0);
            return (false, default);
        }

        /// <summary>
        /// Prompts the user for a transaction ID and retries until a valid input is entered.
        /// </summary>
        /// <param name="action">Show the header which require ID</param>
        /// <returns>A tuple where the first value indicates success (true/false) and the second value is the valid transaction ID</returns>
        public static (bool, int) GetTransactionIdWithRetry(Action action)
        {
            int userAttempt = 4;
            do
            {
                action();
                ConsoleActivity.PrintEmptyLine();
                string? transactionIDInput = ConsoleActivity.GetInputFromUser("transaction ID");
                if (string.IsNullOrWhiteSpace(transactionIDInput) || !transactionIDInput.All(char.IsDigit))
                {
                    ConsoleActivity.PrintInConsole("Transaction ID must contain numbers only!!");
                    userAttempt--;
                }
                else if (int.TryParse(transactionIDInput, out int transactionID))
                {
                    return (true, transactionID);
                }
                else
                {
                    ConsoleActivity.PrintInConsole("!!Transaction exceeded the range " + int.MaxValue);
                    userAttempt--;
                }

                ConsoleActivity.PrintInConsole($"{userAttempt} attempts remaining!!");
                ConsoleActivity.WaitInConsole();
            }
            while (userAttempt > 0);
            return (false, default);
        }

        /// <summary>
        ///  Validates if user input is a number and shows an error if blank.
        /// </summary>
        /// <param name="userInput">The text entered by the user.</param>
        /// <param name="userChoice">The variable that stores the converted number</param>
        /// <returns>True if the input is a valid number; otherwise, false.</returns>
        public static bool ValidateUserChoice(string? userInput, out int userChoice)
        {
            userChoice = default;
            if (string.IsNullOrWhiteSpace(userInput))
            {
                ConsoleActivity.PrintInConsole("Choice cannot be null or empty!!");
                return false;
            }
            else if (int.TryParse(userInput, out userChoice))
                {
                return true;
            }

            return false;
        }
    }
}
