using Assignment4.View;

namespace Assignment4
{
    /// <summary>
    /// Provides helper methods to get user input with retry attempt and validate it.
    /// </summary>
    public static class InputValidatorHelper
    {
        /// <summary>
        /// Prompts the user for an amount with retry attempts.
        /// </summary>/// <param name="header">Determines whether to display the header that requires an ID.</param>
        /// <param name="inputField">The name of the input field to display to the user</param>
        /// <returns>A tuple containing a success flag and the validated amount returns>
        public static (bool, decimal) GetAmountWithRetry(string? header, string? inputField)
        {
            int maxUserAttempt = 4;
            do
            {
                ConsoleActivity.ClearConsole();
                ConsoleActivity.ShowFinancialTrackerHeader(header);
                string? userAmount = ConsoleActivity.GetInputFromUser(inputField);
                if (string.IsNullOrWhiteSpace(userAmount))
                {
                    ConsoleActivity.PrintInConsole("Amount cannot be null or contain white space!!");
                    maxUserAttempt--;
                }
                else if (decimal.TryParse(userAmount, out decimal amount))
                {
                    if (amount < 0)
                    {
                        ConsoleActivity.PrintInConsole("Amount cannot be negative!!");
                        maxUserAttempt--;
                    }
                    else if (amount == 0)
                    {
                        ConsoleActivity.PrintInConsole("Amount cannot be Rs.0 ....");
                        maxUserAttempt--;
                    }
                    else
                    {
                        return (true, amount);
                    }
                }
                else
                {
                    ConsoleActivity.PrintInConsole("Amount must contain only digit!!");
                    maxUserAttempt--;
                }

                ConsoleActivity.PrintInConsole($"{maxUserAttempt} attempts remaining!!");
                ConsoleActivity.WaitInConsole();
            }
            while (maxUserAttempt > 0);
            return (false, default);
        }

        /// <summary>
        /// Prompts the user for a transaction date with retry attempts.
        /// </summary>
        /// <param name="header">Determines whether to display the header that requires date.</param>
        /// <returns>A tuple containing a success flag  and the validated transaction date</returns>
        public static (bool, DateOnly) GetTransactionDateWithRetry(string? header)
        {
            int maxUserAttempt = 4;
            do
            {
                ConsoleActivity.ClearConsole();
                ConsoleActivity.ShowFinancialTrackerHeader(header);
                ConsoleActivity.PrintInConsole("The transaction date must follow the format (DD-MM-YYYY or DD/MM/YYYY)\nOr Just press enter to add today's date");
                ConsoleActivity.PrintEmptyLine();
                string? userDate = ConsoleActivity.GetInputFromUser("Transaction date");
                if (userDate == null)
                {
                    ConsoleActivity.PrintInConsole("Date cannot be null!!");
                    maxUserAttempt--;
                }
                else if (string.IsNullOrWhiteSpace(userDate))
                {
                    ConsoleActivity.ShowFinancialTrackerHeader(header);
                    ConsoleActivity.PrintInConsole($"Transaction date : {DateOnly.FromDateTime(DateTime.Today)}\nPress enter to confirm!!");
                    if (ConsoleActivity.IsEmptyInputToConfirm())
                    {
                        return (true, DateOnly.FromDateTime(DateTime.Today));
                    }

                    maxUserAttempt--;
                }
                else if (DateOnly.TryParse(userDate, out DateOnly transactionDate))
                {
                    if (transactionDate >= DateOnly.FromDateTime(DateTime.Today))
                    {
                        ConsoleActivity.PrintInConsole("The transaction date cannot be in the future.");
                        maxUserAttempt--;
                    }
                    else
                    {
                        return (true, transactionDate);
                    }
                }
                else
                {
                    ConsoleActivity.PrintInConsole("The transaction date must follow the format (DD-MM-YYYY or DD/MM/YYYY)");
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
        /// Prompts the user to select a source or category from a list with retry attempts.
        /// </summary>
        /// <param name="header">The console header menu screen.</param>
        /// <param name="predefinedList">The list of valid options the user can choose from.</param>
        /// <param name="isIncome">True when selecting an income source; otherwise selects an expense category. </param>
        /// <returns>A tuple containing a true/false success status and the selected string value.</returns>
        public static (bool, string?) GetSourceOrCategory(string? header, string[] predefinedList, bool isIncome)
        {
            int maxUserAttempt = 4;
            do
            {
                int listLength = predefinedList.Length;
                ConsoleActivity.ShowFinancialTrackerHeader(header);
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole(isIncome ? "Select the source of income:" : "Select the expense category:");
                ConsoleActivity.PrintItems(predefinedList);
                string? userChoiceInput = ConsoleActivity.GetInputFromUser($"option [1-{listLength}]");
                if (ValidateUserChoice(userChoiceInput, out int userChoice))
                {
                    if (userChoice > 0 && userChoice <= listLength)
                    {
                        return (true, predefinedList[userChoice - 1]);
                    }
                    else
                    {
                        ConsoleActivity.PrintInConsole($"Choice must be within range 1-{listLength}");
                        maxUserAttempt--;
                    }
                }
                else
                {
                    ConsoleActivity.PrintInConsole("Choice must be number!!");
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
        /// Prompts the user for a transaction ID and retries until a valid input is entered.
        /// </summary>
        /// <param name="header">Show the header which require ID</param>
        /// <returns>A tuple where the first value indicates success (true/false) and the second value is the valid transaction ID</returns>
        public static (bool, int) GetTransactionIdWithRetry(string? header)
        {
            int maxUserAttempt = 4;
            do
            {
                ConsoleActivity.ShowFinancialTrackerHeader(header);
                ConsoleActivity.PrintEmptyLine();
                string? transactionIDInput = ConsoleActivity.GetInputFromUser("transaction ID");
                if (string.IsNullOrWhiteSpace(transactionIDInput) || !transactionIDInput.All(char.IsDigit))
                {
                    ConsoleActivity.PrintInConsole("Transaction ID must contain numbers only!!");
                    maxUserAttempt--;
                }
                else if (int.TryParse(transactionIDInput, out int transactionID))
                {
                    return (true, transactionID);
                }
                else
                {
                    ConsoleActivity.PrintInConsole("!!Transaction exceeded the range " + int.MaxValue);
                    maxUserAttempt--;
                }

                ConsoleActivity.PrintInConsole($"{maxUserAttempt} attempts remaining!!");
                ConsoleActivity.WaitInConsole();
            }
            while (maxUserAttempt > 0);
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
