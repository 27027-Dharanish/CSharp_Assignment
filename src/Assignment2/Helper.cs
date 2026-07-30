using Assignment2.Model.BankingModels;

namespace Assignment2
{
    /// <summary>
    /// Helper class for application.
    /// </summary>
    public static class Helper
    {
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
        /// Check if BankAccount is null.
        /// </summary>
        /// <param name="account">Account from the repository</param>
        /// <returns>Return true if account is not null and false if account is null</returns>
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
    }
}
