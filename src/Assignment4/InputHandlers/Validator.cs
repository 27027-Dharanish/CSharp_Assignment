using FinanceTracker.Core.FinancialTrackerConstant;
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
        public static ValidationStatus IsTransactionId(string? userTransactionId, out int transactionId)
        {
            transactionId = default;
            if (string.IsNullOrWhiteSpace(userTransactionId))
            {
                return ValidationStatus.NullOrWhiteSpace;
            }
            else if (!userTransactionId.All(char.IsDigit))
            {
                return ValidationStatus.NotDigit;
            }
            else if (int.TryParse(userTransactionId, out int parsedTransactionID))
            {
                transactionId = parsedTransactionID;
                return ValidationStatus.Success;
            }
            else
            {
                return ValidationStatus.ExceededRange;
            }
        }

        /// <summary>
        /// Checks if the chosen menu number matches a valid option in the list.
        /// </summary>
        /// <param name="userChoiceInput">The menu number typed by the user.</param>
        /// <param name="predefinedList">The list of available options.</param>
        /// <param name="transactionTag">The actual tag of the chosen option.</param>
        /// <returns>True if the selection is a valid within the list; otherwise, false.</returns>
        public static ValidationStatus IsTransactionTag(string? userChoiceInput, string[] predefinedList, out string transactionTag)
        {
            transactionTag = string.Empty;
            int listLength = predefinedList.Length;
            if (int.TryParse(userChoiceInput, out int userChoice))
            {
                if (userChoice > 0 && userChoice <= listLength)
                {
                    transactionTag = predefinedList[userChoice - 1];
                    return ValidationStatus.Success;
                }
                else
                {
                    return ValidationStatus.ExceededRange;
                }
            }
            else
            {
                return ValidationStatus.NotDigit;
            }
        }

        /// <summary>
        /// Checks if the entered text is a valid date.
        /// </summary>
        /// <param name="date">The raw date text typed by the user.</param>
        /// <param name="transactionDate">The verified, usable date.</param>
        /// <returns>True if the text matches a real, accepted date format; otherwise, false.</returns>
        public static ValidationStatus IsTransactionDate(string? date, out DateOnly transactionDate)
        {
            transactionDate = default;
            if (date == null)
            {
                return ValidationStatus.NullOrWhiteSpace;
            }
            else if (string.IsNullOrWhiteSpace(date))
            {
                return ValidationStatus.UseCurrentDate;
            }
            else if (DateOnly.TryParse(date, out DateOnly userDate))
            {
                if (userDate >= DateOnly.FromDateTime(DateTime.Today))
                {
                    return ValidationStatus.FutureDate;
                }
                else
                {
                    return ValidationStatus.Success;
                }
            }
            else
            {
                return ValidationStatus.DateFormat;
            }
        }

        /// <summary>
        /// Checks if the entered text is a valid amount.
        /// </summary>
        /// <param name="userAmount">The raw text typed by the user.</param>
        /// <param name="transactionAmount">The verified, usable numeric amount.</param>
        /// <returns>True if the amount is a valid; otherwise, false.</returns>
        public static ValidationStatus IsTransactionAmount(string? userAmount, out decimal transactionAmount)
        {
            transactionAmount = default;
            if (string.IsNullOrWhiteSpace(userAmount))
            {
                return ValidationStatus.NullOrWhiteSpace;
            }
            else if (decimal.TryParse(userAmount, out decimal amount))
            {
                transactionAmount = amount;
                return ValidationStatus.Success;
            }
            else
            {
                return ValidationStatus.NotDigit;
            }
        }

        /// <summary>
        /// Checks if the entered text is a valid menu selection number.
        /// </summary>
        /// <param name="choice">The raw choice text typed by the user.</param>
        /// <param name="userChoice">The verified, usable selection choice.</param>
        /// <returns>True if the text is a valid; otherwise, false.</returns>
        public static ValidationStatus IsChoice(string? choice, out int userChoice)
        {
            userChoice = default;
            if (string.IsNullOrWhiteSpace(choice))
            {
                return ValidationStatus.NullOrWhiteSpace;
            }
            else if (int.TryParse(choice, out userChoice))
            {
                return ValidationStatus.Success;
            }

            return ValidationStatus.NotDigit;
        }
    }
}
