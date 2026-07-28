using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    /// <summary>
    /// Provides utility and supporting methods to assist with inventory management operations.
    /// </summary>
    internal class InventoryHelper
    {
        /// <summary>
        /// Check whether the content is null or contain whitespace.
        /// </summary>
        /// <param name="content">Content need to be checked</param>
        /// <returns>Return true if whitespace or null, else false</returns>
        public static bool IsEmpty(string? content)
        {
            return string.IsNullOrWhiteSpace(content);
        }

        /// <summary>
        /// Check whether the content is digit or not.
        /// </summary>
        /// <param name="content">Content to be checked</param>
        /// <returns>Return true if content contain all digit else false</returns>
        public static bool IsOnlyDigit(string? content)
        {
            if (content == null)
            {
                return false;
            }

            return content.All(char.IsDigit);
        }

        /// <summary>
        /// Check whether the content contain letters or not.
        /// </summary>
        /// <param name="content">Content to be checked</param>
        /// <returns>Return true if content contain all letters else false</returns>
        public static bool IsOnlyChar(string? content)
        {
            if (content == null)
            {
                return false;
            }

            return content.All(char.IsLetter);
        }
    }
}
