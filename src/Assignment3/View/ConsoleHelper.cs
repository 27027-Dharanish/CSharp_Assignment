using Assignment_3.View;

namespace Assignment3.View
{
    /// <summary>
    /// Provides utility and supporting methods to assist with inventory management operations.
    /// </summary>
    public class ConsoleHelper
    {
        /// <summary>
        /// Check whether the content is null or contain whitespace.
        /// </summary>
        /// <param name="content">Content need to be checked</param>
        /// <returns>True if whitespace or null, else false</returns>
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
        /// <returns>True if content contain all letters else false</returns>
        public static bool IsOnlyChar(string? content)
        {
            if (content == null)
            {
                return false;
            }

            return content.All(char.IsLetter);
        }

        /// <summary>
        /// Check whether the product name is valid.
        /// </summary>
        /// <param name="productName">The name of the product</param>
        /// <returns>True if product name is valid else false</returns>
        public static bool IsValidProductName(string? productName)
        {
            if (IsEmpty(productName))
            {
                ConsoleActivity.PrintInConsole("Product name cannot be null or empty!!");
                return false;
            }
            else if (!IsOnlyChar(productName))
            {
                ConsoleActivity.PrintInConsole("Product name must contain only character!!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the product quantity is valid.
        /// </summary>
        /// <param name="quantity">Product quantity</param>
        /// <returns>True if quantity is valid else false</returns>
        public static bool ProductQuantityValidator(int quantity)
        {
            if (quantity >= int.MaxValue)
            {
                ConsoleActivity.PrintInConsole("Quantity value exceeded the range...");
                ConsoleActivity.PrintInConsole("Quantity must be within : " + int.MaxValue);
                return false;
            }
            else if (quantity == 0)
            {
                ConsoleActivity.PrintInConsole("Quantity cannot be Rs.0 ....");
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
        /// <returns>True if price is valid else false</returns>
        public static bool ProductPriceValidator(decimal price)
        {
            if (price >= decimal.MaxValue)
            {
                ConsoleActivity.PrintInConsole("Price value exceeded the range...");
                ConsoleActivity.PrintInConsole("Price value must be within : " + decimal.MaxValue);
                return false;
            }
            else if (price == 0)
            {
                ConsoleActivity.PrintInConsole("Price cannot be Rs.0 ....");
                return false;
            }
            else if (price < 0)
            {
                ConsoleActivity.PrintInConsole("Price cannot be negative...");
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
                ConsoleActivity.PrintInConsole("Product ID cannot be null or empty!!");
                return false;
            }
            else if (!IsOnlyDigit(productID))
            {
                ConsoleActivity.PrintInConsole("Product ID must contain only digit and cannot be negative!!");
                return false;
            }

            return true;
        }
    }
}
