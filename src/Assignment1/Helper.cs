using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            else if (int.TryParse(number, out int result))
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
            else if (email.Contains("@gmail.com"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
