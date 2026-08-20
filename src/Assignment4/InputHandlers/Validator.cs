using FinanceTracker.View;

namespace FinanceTracker.Helper
{
    /// <summary>
    /// Validate the input given by the user.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Checks if the entered id is a valid.
        /// </summary>
        /// <param name="userTransactionId">The raw text typed by the user.</param>
        /// <param name="transactionId">The verified, usable number generated if the check passes.</param>
        /// <returns>True if the text is a valid id; otherwise, false.</returns>
        public static bool IsTransactionId(string? userTransactionId, out int transactionId)
        {
            transactionId = default;
            if (string.IsNullOrWhiteSpace(userTransactionId) || !userTransactionId.All(char.IsDigit))
            {
                ConsoleActivity.PrintInConsole("Transaction ID must contain numbers only!!");
                return false;
            }
            else if (int.TryParse(userTransactionId, out int parsedTransactionID))
            {
                transactionId = parsedTransactionID;
                return true;
            }
            else
            {
                ConsoleActivity.PrintInConsole("!!Transaction id exceeded the range " + int.MaxValue);
                return false;
            }
        }

        /// <summary>
        /// Checks if the chosen menu number matches a valid option in the list.
        /// </summary>
        /// <param name="userChoiceInput">The menu number typed by the user.</param>
        /// <param name="predefinedList">The list of available options.</param>
        /// <param name="transactionTag">The actual tag of the chosen option.</param>
        /// <returns>True if the selection is a valid within the list; otherwise, false.</returns>
        public static bool IsTransactionTag(string? userChoiceInput, string[] predefinedList, out string transactionTag)
        {
            transactionTag = string.Empty;
            int listLength = predefinedList.Length;
            if (int.TryParse(userChoiceInput, out int userChoice))
            {
                if (userChoice > 0 && userChoice <= listLength)
                {
                    transactionTag = predefinedList[userChoice - 1];
                    return true;
                }
                else
                {
                    ConsoleActivity.PrintInConsole($"Choice must be within range 1-{listLength}");
                    return false;
                }
            }
            else
            {
                ConsoleActivity.PrintInConsole("Choice must be a number!!");
                return false;
            }
        }

        /// <summary>
        /// Checks if the entered text is a valid date.
        /// </summary>
        /// <param name="date">The raw date text typed by the user.</param>
        /// <param name="transactionDate">The verified, usable date.</param>
        /// <returns>True if the text matches a real, accepted date format; otherwise, false.</returns>
        public static bool IsTransactionDate(string? date, out DateOnly transactionDate)
        {
            transactionDate = default;
            if (date == null)
            {
                ConsoleActivity.PrintInConsole("Date cannot be null!!");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(date))
            {
                ConsoleActivity.PrintInConsole($"Transaction date : {DateOnly.FromDateTime(DateTime.Today)}\nPress enter to confirm!!");
                if (ConsoleActivity.IsEmptyInput())
                {
                    transactionDate = DateOnly.FromDateTime(DateTime.Today);
                    return true;
                }

                return false;
            }
            else if (DateOnly.TryParse(date, out DateOnly userDate))
            {
                if (userDate >= DateOnly.FromDateTime(DateTime.Today))
                {
                    ConsoleActivity.PrintInConsole("The transaction date cannot be in the future.");
                    return false;
                }
                else
                {
                    transactionDate = userDate;
                    return true;
                }
            }
            else
            {
                ConsoleActivity.PrintInConsole("The transaction date must follow the format (DD-MM-YYYY or DD/MM/YYYY)");
                return false;
            }
        }

        /// <summary>
        /// Checks if the entered text is a valid amount.
        /// </summary>
        /// <param name="userAmount">The raw text typed by the user.</param>
        /// <param name="transactionAmount">The verified, usable numeric amount.</param>
        /// <returns>True if the amount is a valid; otherwise, false.</returns>
        public static bool IsTransactionAmount(string? userAmount, out decimal transactionAmount)
        {
            transactionAmount = default;
            if (string.IsNullOrWhiteSpace(userAmount))
            {
                ConsoleActivity.PrintInConsole("Amount cannot be null or contain white space!!");
                return false;
            }
            else if (decimal.TryParse(userAmount, out decimal amount))
            {
                if (amount < 0)
                {
                    ConsoleActivity.PrintInConsole("Amount cannot be negative!!");
                    return false;
                }
                else if (amount == 0)
                {
                    ConsoleActivity.PrintInConsole("Amount cannot be Rs.0 ....");
                    return false;
                }
                else
                {
                    transactionAmount = amount;
                    return true;
                }
            }
            else
            {
                ConsoleActivity.PrintInConsole("Amount must contain only digit!!");
                return false;
            }
        }

        /// <summary>
        /// Checks if the entered text is a valid menu selection number.
        /// </summary>
        /// <param name="choice">The raw choice text typed by the user.</param>
        /// <param name="userChoice">The verified, usable selection choice.</param>
        /// <returns>True if the text is a valid; otherwise, false.</returns>
        public static bool IsChoice(string? choice, out int userChoice)
        {
            userChoice = default;
            if (string.IsNullOrWhiteSpace(choice))
            {
                ConsoleActivity.PrintInConsole("Choice cannot be null or empty!!");
                return false;
            }
            else if (int.TryParse(choice, out userChoice))
            {
                return true;
            }

            return false;
        }
    }
}
