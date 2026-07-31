using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Assignment1
{
    /// <summary>
    /// Helper class for validation.
    /// </summary>
    internal class Helper
    {
        /// <summary>
        /// Validate number.
        /// </summary>
        /// <param name="number">Phone number from user</param>
        /// <returns>Return true if valid number</returns>
        public static bool IsValidNumber(string? number)
        {
            if (number == null)
            {
                return false;
            }

            string pattern = @"^[0-9]{10}$";
            return Regex.IsMatch(number, pattern);
        }

        /// <summary>
        /// Validate the email.
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns>Return true if valid email</returns>
        public static bool IsValidEmail(string? email)
        {
            if (email == null)
            {
                return false;
            }

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// Check whether the string is null or not.
        /// </summary>
        /// <param name="content">Content from the user</param>
        /// <returns>Return null or not</returns>
        public static bool IsNull(string? content)
        {
            if (content == null)
            {
                return true;
            }

            return false;
        }
    }
}
