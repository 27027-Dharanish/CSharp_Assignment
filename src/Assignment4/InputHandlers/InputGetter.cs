using FinanceTracker.Helper;
using FinanceTracker.View;

namespace FinanceTracker.FinanceTrackerHelper
{
    /// <summary>
    /// Handles getting the inputs from the user.
    /// </summary>
    public static class InputGetter
    {
        /// <summary>
        /// Get amount with retry attempts.
        /// </summary>
        /// <param name="header">The header that requires an ID.</param>
        /// <param name="label">The name of the input field to display to the user.</param>
        /// <returns>A tuple containing a success flag and the validated amount.<returns>
        public static (bool, decimal) GetAmountWithRetry(string header, string label)
        {
            int maxUserAttempt = 4;
            do
            {
                ConsoleActivity.ClearConsole();
                ConsoleActivity.ShowHeader(header);
                string? userAmount = ConsoleActivity.GetStringInput(label);
                if (Validator.IsTransactionAmount(userAmount, out decimal transactionAmount))
                {
                    return (true, transactionAmount);
                }
                else
                {
                    maxUserAttempt--;
                }

                ConsoleActivity.PrintInConsole($"{maxUserAttempt} attempts remaining!!");
                ConsoleActivity.WaitInConsole();
            }
            while (maxUserAttempt > 0);
            return (false, default);
        }

        /// <summary>
        /// Get transaction date with retry attempts.
        /// </summary>
        /// <param name="header">The header that requires date.</param>
        /// <returns>A tuple containing a success flag  and the validated transaction date.</returns>
        public static (bool, DateOnly) GetTransactionDateWithRetry(string header)
        {
            int maxUserAttempt = 4;
            do
            {
                ConsoleActivity.ClearConsole();
                ConsoleActivity.ShowHeader(header);
                ConsoleActivity.PrintInConsole("The transaction date must follow the format (DD-MM-YYYY or DD/MM/YYYY)\nOr Just press enter to add today's date");
                ConsoleActivity.PrintEmptyLine();
                string? userDate = ConsoleActivity.GetStringInput("Transaction date");
                if (Validator.IsTransactionDate(userDate, out DateOnly transactionDate))
                {
                    return (true, transactionDate);
                }
                else
                {
                    maxUserAttempt--;
                }

                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole($"{maxUserAttempt} attempts remaining!!");
                ConsoleActivity.WaitInConsole();
            }
            while (maxUserAttempt > 0);
            return (false, default);
        }

        /// <summary>
        /// Get source or category from a list with retry attempts.
        /// </summary>
        /// <param name="header">The console header menu screen.</param>
        /// <param name="predefinedList">The list of valid options.</param>
        /// <param name="isIncome">True when selecting an income source; otherwise selects an expense category.</param>
        /// <returns>A tuple containing a success status and the selected string value.</returns>
        public static (bool, string?) GetTransactionTag(string header, string[] predefinedList, bool isIncome)
        {
            int maxUserAttempt = 4;
            do
            {
                int listLength = predefinedList.Length;
                ConsoleActivity.ShowHeader(header);
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole(isIncome ? "Select the source of income:" : "Select the expense category:");
                ConsoleActivity.PrintItems(predefinedList);
                string? userChoiceInput = ConsoleActivity.GetStringInput($"option [1-{listLength}]");
                if (Validator.IsTransactionTag(userChoiceInput, predefinedList, out string transactionTag))
                {
                    return (true, transactionTag);
                }
                else
                {
                    maxUserAttempt--;
                }

                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole($"{maxUserAttempt} attempts remaining!!");
                ConsoleActivity.WaitInConsole();
            }
            while (maxUserAttempt > 0);
            return (false, default);
        }

        /// <summary>
        /// Get transaction ID and retries until a valid input is entered.
        /// </summary>
        /// <param name="header">Show the header which require ID.</param>
        /// <returns>A tuple containing success status and the valid transaction ID.</returns>
        public static (bool, int) GetTransactionIdWithRetry(string header)
        {
            int maxUserAttempt = 4;
            do
            {
                ConsoleActivity.ShowHeader(header);
                ConsoleActivity.PrintEmptyLine();
                string? userTransactionID = ConsoleActivity.GetStringInput("transaction ID");
                if (Validator.IsTransactionId(userTransactionID, out int transactionID))
                {
                    return (true, transactionID);
                }
                else
                {
                    maxUserAttempt--;
                }

                ConsoleActivity.PrintInConsole($"{maxUserAttempt} attempts remaining!!");
                ConsoleActivity.WaitInConsole();
            }
            while (maxUserAttempt > 0);
            return (false, default);
        }

        /// <summary>
        ///  Validates if user input is a valid choice.
        /// </summary>
        /// <param name="userInput">The text entered by the user.</param>
        /// <param name="userChoice">The variable that stores the converted number.</param>
        /// <returns>True if the input is a valid number; otherwise, false.</returns>
        public static bool GetUserChoice(string? userInput, out int userChoice)
        {
            userChoice = default;
            if (Validator.IsChoice(userInput, out userChoice))
            {
                return true;
            }

            return false;
        }
    }
}
