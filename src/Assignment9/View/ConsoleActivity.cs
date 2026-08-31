using System.Runtime.CompilerServices;
using Assignment9.Core.Model;
using ConsoleTables;

namespace Assignment9.View
{
    /// <summary>
    /// Handles user interaction activities by managing standard input and output via the console.
    /// </summary>
    public static class ConsoleActivity
    {
        private static ConsoleTable _productTable = new ConsoleTable("Product Id", "Product Name", "Price", "Category");

        /// <summary>
        /// Print the given content in the console.
        /// </summary>
        /// <param name="content">Content that need to be printed.</param>
        public static void PrintInConsole(string content)
        {
            Console.WriteLine(content);
        }

        /// <summary>
        /// Prompts the user and reads their text input from the console.
        /// </summary>
        /// <param name="label">Label that requested for input.</param>
        /// <returns>Text entered by the user.</returns>
        public static string? GetStringInput(string label)
        {
            PrintEmptyLine();
            Console.Write($"Enter the {label} : ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Wait in the console until user presses any key.
        /// </summary>
        public static void WaitInConsole()
        {
            PrintEmptyLine();
            PrintInConsole("Press any key to continue!!");
            Console.ReadKey();
        }

        /// <summary>
        /// Print invalid field warning in console.
        /// </summary>
        /// <param name="content">Invalid message to be printed.</param>
        public static void PrintInvalidMessage(string content)
        {
            PrintEmptyLine();
            PrintInConsole(content);
            WaitInConsole();
        }

        /// <summary>
        /// Print empty line in console.
        /// </summary>
        public static void PrintEmptyLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Show the financial menu option available.
        /// </summary>
        /// <param name="header">Menu header.</param>
        /// <param name="menuItem">List of menu item for transaction operation.</param>
        public static void ShowMenu(string header, string[] menuItem)
        {
            ShowHeader(header);
            PrintItems(menuItem);
            PrintInConsole(new string('-', 40));
        }

        /// <summary>
        /// Show the header for the transaction operation.
        /// </summary>
        /// <param name="header">Name of the header.</param>
        public static void ShowHeader(string header)
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole($"          {header}");
            PrintInConsole(new string('=', 40));
        }

        /// <summary>
        /// Clear the console.
        /// </summary>
        public static void ClearConsole()
        {
            Console.Clear();
            Console.Write("\x1b[3J");
        }

        /// <summary>
        /// Print the list of items in console.
        /// </summary>
        /// <param name="items">Items to be printed.</param>
        public static void PrintItems(string[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                PrintInConsole($"{i + 1}. {items[i]}");
            }
        }

        /// <summary>
        /// Exit from the expense tracker application.
        /// </summary>
        public static void ExitApplication()
        {
            ClearConsole();
            PrintInConsole(new string('=', 70));
            PrintInConsole("          Thank you for using the application");
            PrintInConsole(new string('=', 70));
            PrintEmptyLine();
            WaitInConsole();
        }

        /// <summary>
        /// Get integer value input from user.
        /// </summary>
        /// <param name="label">label of the input field.</param>
        /// <returns>The prompted integer value.</returns>
        public static int GetIntegerInput(string label)
        {
            string? userInput = GetStringInput(label);
            int.TryParse(userInput, out int value);
            return value;
        }

        /// <summary>
        /// Print and wait in console.
        /// </summary>
        /// <param name="content">Content to be printed in console.</param>
        public static void PrintAndWait(string content)
        {
            PrintInConsole(content);
            WaitInConsole();
        }

        /// <summary>
        /// Print the list of product in table format.
        /// </summary>
        /// <param name="products">List of product to be printed.</param>
        public static void PrintProduct(List<Product> products)
        {
            _productTable.Rows.Clear();
            int i = 0;
            foreach (Product product in products)
            {
                _productTable.AddRow(++i, product.ProductName, product.ProductPrice, product.Category);
            }

            _productTable.Write();
        }

        /// <summary>
        /// Print the product name and price.
        /// </summary>
        /// <param name="products">List of product.</param>
        public static void PrintNameAndPrice((string, decimal)[] products)
        {
            ConsoleTable productNameAndPrice = new ConsoleTable("Product Name", "Product Price");
            foreach (var product in products)
            {
                productNameAndPrice.AddRow(product.Item1, product.Item2);
            }

            productNameAndPrice.Write();
        }

        /// <summary>
        /// Print the product category values.
        /// </summary>
        /// <param name="products">List of product.</param>
        public static void PrintCategory((string, int, string, decimal)[] products)
        {
            ConsoleTable productTable = new ConsoleTable("Category", "Count", "ProductName", "Price");
            foreach (var product in products)
            {
                productTable.AddRow(product.Item1, product.Item2, product.Item3, product.Item4);
            }

            productTable.Write();
        }

        /// <summary>
        /// Print the detail of the supplier and product.
        /// </summary>
        /// <param name="products">List of product.</param>
        public static void PrintInnerJoinTable((int, string, string)[] products)
        {
            ConsoleTable productTable = new ConsoleTable("Supplier Id", "Supplier Name", "Product Name");
            foreach (var product in products)
            {
                productTable.AddRow(product.Item1, product.Item2, product.Item3);
            }

            productTable.Write();
        }

        /// <summary>
        /// Print the list of numbers in console.
        /// </summary>
        /// <param name="numbers">List of numbers to be printed.</param>
        public static void PrintNumber(int[] numbers)
        {
            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }
        }

        /// <summary>
        /// Print the pair of numbers in console and its target value.
        /// </summary>
        /// <param name="pairNumbers">Array of pair of numbers.</param>
        /// <param name="target">Target number.</param>
        public static void PrintPairNumbers((int, int)[] pairNumbers, int target)
        {
            foreach (var number in pairNumbers)
            {
                PrintInConsole($"{number.Item1} + {number.Item2} = {target}");
            }
        }

        /// <summary>
        /// Print product and its supplier name.
        /// </summary>
        /// <param name="productAndSupplier">List of product and its supplier name.</param>
        public static void PrintProductAndSupplierName(List<(string ProductName, string SupplierName)> productAndSupplier)
        {
            ConsoleTable productSupplierName = new ConsoleTable("Product name", "Supplier name");
            foreach (var detail in productAndSupplier)
            {
                productSupplierName.AddRow(detail.ProductName, detail.SupplierName);
            }

            productSupplierName.Write();
        }

        /// <summary>
        /// Print the supplier details in console.
        /// </summary>
        /// <param name="suppliers">List of supplier available.</param>
        public static void PrintSupplierDetails(List<Supplier> suppliers)
        {
            ConsoleTable supplierTable = new ConsoleTable("Supplier Id", "SupplierName");
            foreach (Supplier supplier in suppliers)
            {
                supplierTable.AddRow(supplier.SupplierId, supplier.SupplierName);
            }

            supplierTable.Write();
        }
    }
}