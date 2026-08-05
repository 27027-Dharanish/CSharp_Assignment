using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_3.Model;

namespace Assignment_3.View
{
    /// <summary>
    /// Handles user interaction activities by managing standard input and output streams via the console.
    /// </summary>
    public static class ConsoleActivity
    {
        /// <summary>
        /// Print the content to the console.
        /// </summary>
        /// <param name="content">Content to be printed in console</param>
        public static void PrintInConsole(string? content)
        {
            Console.WriteLine(content);
        }

        /// <summary>
        /// Get the input from the user via console.
        /// </summary>
        /// <param name="inputToGet">The input user must enter</param>
        /// <returns>The data entered by the user</returns>
        public static string? GetInputFromConsole(string? inputToGet)
        {
            Console.WriteLine($"Enter the {inputToGet} : ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Print empty line in console.
        /// </summary>
        public static void PrintEmptyLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Wait in console until user press any key.
        /// </summary>
        public static void WaitInConsole()
        {
            Console.WriteLine("Press any key to continue!!");
            Console.ReadKey();
        }

        /// <summary>
        /// Shows the menu option available in inventory management.
        /// </summary>
        public static void InventoryMenu()
        {
            Console.Clear();
            Console.WriteLine("!!Inventory Management System!!");
            Console.WriteLine();
            Console.WriteLine("Select the option to perform: ");
            Console.WriteLine();
            Console.WriteLine("1.Add new product");
            Console.WriteLine("2.View all product in inventory");
            Console.WriteLine("3.Edit product from inventory");
            Console.WriteLine("4.Search product from inventory");
            Console.WriteLine("5.Delete product");
            Console.WriteLine("6.Exit");
        }

        /// <summary>
        /// Print the invalid field with field name.
        /// </summary>
        /// <param name="field">Invalid field name</param>
        public static void PrintInvalidField(string? field)
        {
            Console.WriteLine("Invalid " + field);
            WaitInConsole();
        }

        /// <summary>
        /// Print warning to the user that duplicate value found.
        /// </summary>
        /// <param name="content">Duplicate content field</param>
        public static void PrintDuplicateFoundInConsole(string? content)
        {
            Console.WriteLine($"{content} already present in inventory!!");
            Console.WriteLine("Enter new value again..");
            WaitInConsole();
        }

        /// <summary>
        /// Print the product details in console.
        /// </summary>
        /// <param name="product">Product to be printed in console</param>
        public static void PrintProductInConsole(Product product)
        {
            Console.WriteLine("Product ID : " + product.ProductId);
            Console.WriteLine("Name : " + product.Name);
            Console.WriteLine("Price : " + product.Price);
            Console.WriteLine("Quantity : " + product.Quantity);
        }

        /// <summary>
        /// Clear the console.
        /// </summary>
        public static void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Show the menu option available in searching of a product.
        /// </summary>
        /// <param name="field">Field that uses search option</param>
        public static void ShowSearchProductMenu(string? field)
        {
            Console.Clear();
            Console.WriteLine($"{field} Product from Inventory!!");
            Console.WriteLine();
            Console.WriteLine($"{field} Using:");
            Console.WriteLine();
            Console.WriteLine("1.Product Id");
            Console.WriteLine("2.Product Name");
            Console.WriteLine();
        }

        /// <summary>
        /// Show the menu option available in edit option.
        /// </summary>
        public static void ShowMenuToEdit()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine();
            Console.WriteLine("Choose the field to edit!");
            Console.WriteLine();
            Console.WriteLine("1.Name");
            Console.WriteLine("2.Price");
            Console.WriteLine("3.Quantity");
            Console.WriteLine();
        }
    }
}
