using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_3.Model;
using Assignment_3.View;

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

        /// <summary>
        /// Check whether the product quantity is valid.
        /// </summary>
        /// <param name="quantity">Product quantity</param>
        /// <returns>Return true if quantity is valid else false</returns>
        public static bool ProductQuantityValidator(int quantity)
        {
            if (quantity >= int.MaxValue)
            {
                ConsoleActivity.PrintInConsole("Quantity value exceeded the range...");
                ConsoleActivity.WaitInConsole();
                return false;
            }
            else if (quantity < 0)
            {
                ConsoleActivity.PrintInConsole("Quantity cannot be negative...");
                ConsoleActivity.WaitInConsole();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the product price is valid.
        /// </summary>
        /// <param name="price">Product price</param>
        /// <returns>Return true if price is valid else false</returns>
        public static bool ProductPriceValidator(decimal price)
        {
            if (price >= decimal.MaxValue)
            {
                ConsoleActivity.PrintInConsole("Price value exceeded the range...");
                ConsoleActivity.WaitInConsole();
                return false;
            }
            else if (price < 0)
            {
                ConsoleActivity.PrintInConsole("Price cannot be negative...");
                ConsoleActivity.WaitInConsole();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the product id is valid.
        /// </summary>
        /// <param name="productID">The id of the product</param>
        /// <returns>True if product id is valid else false</returns>
        public static bool IsValidProductId(string? productID)
        {
            if (IsEmpty(productID))
            {
                ConsoleActivity.PrintInvalidField("product ID");
                return false;
            }
            else if (!IsOnlyDigit(productID))
            {
                ConsoleActivity.PrintInConsole("Product ID must be Digit!!");
                ConsoleActivity.WaitInConsole();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the product name is valid.
        /// </summary>
        /// <param name="productName">The name of the product</param>
        /// <returns>True if product name is valid else false</returns>
        public static bool IsValidProductName(string? productName)
        {
            if (InventoryHelper.IsEmpty(productName))
            {
                ConsoleActivity.PrintInvalidField("product Name");
                return false;
            }
            else if (!InventoryHelper.IsOnlyChar(productName))
            {
                ConsoleActivity.PrintInConsole("Product name must be character!!");
                ConsoleActivity.WaitInConsole();
                return false;
            }

            return true;
        }
    }
}
