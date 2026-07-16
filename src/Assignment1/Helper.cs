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
    /// Helper
    /// </summary>
    internal class Helper
    {
        /// <summary>
        /// validnumber
        /// </summary>
        /// <param name="number">phone</param>
        /// <returns>true</returns>
        public bool IsValidNumber(string? number)
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
        /// emial
        /// </summary>
        /// <param name="email">ema</param>
        /// <returns>bool</returns>
        public bool IsValidEmail(string? email)
        {
            if (email == null)
            {
                return false;
            }

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// isnull
        /// </summary>
        /// <param name="content">content</param>
        /// <returns>true</returns>
        public bool IsNull(string? content)
        {
            if (content == string.Empty)
            {
                return true;
            }

            return false;
        }
    }
}
