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
    /// Helper class for validation
    /// </summary>
    internal class Helper
    {
        /// <summary>
        /// method to check whether a number is valid
        /// </summary>
        /// <param name="number">phone number from user</param>
        /// <returns>true or false</returns>
        public static bool IsValidNumber(string? number)
        {
            if (number == null)
            {
                return false;
            }
            else if (int.TryParse(number, out int result) && number.Length == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// validate the email
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns>true or false</returns>
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
        /// check whether the string is null or not
        /// </summary>
        /// <param name="content">content from the user</param>
        /// <returns>true or false</returns>
        public static bool IsNull(string? content)
        {
            if (content == string.Empty)
            {
                return true;
            }

            return false;
        }
    }
}
