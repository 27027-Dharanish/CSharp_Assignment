using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Model.BankingModels;

namespace Assignment2
{
    /// <summary>
    /// Helper class for Banking application.
    /// </summary>
    internal static class BankingHelper
    {
        /// <summary>
        /// Check whether the string is empty or not.
        /// </summary>
        /// <param name="content">string to be checked</param>
        /// <returns>Return whether the string is empty or not</returns>
        public static bool IsNotEmpty(string? content)
        {
            if (content == string.Empty)
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
    }
}
