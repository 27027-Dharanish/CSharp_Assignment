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
            Console.Write($"Enter the {inputToGet} : ");
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
            PrintInConsole("Press any key to continue!!");
            Console.ReadKey();
        }

        /// <summary>
        /// Shows the menu option available in inventory management.
        /// </summary>
        public static void InventoryMenu()
        {
            ClearConsole();
            ShowInventoryHeader();
            PrintEmptyLine();
            PrintInConsole("Select the option to perform: ");
            PrintEmptyLine();
            PrintInConsole("1.Add new product");
            PrintInConsole("2.View all product");
            PrintInConsole("3.Edit product");
            PrintInConsole("4.Search product");
            PrintInConsole("5.Delete product");
            PrintInConsole("6.Exit");
        }

        /// <summary>
        /// Print and wait in the console
        /// </summary>
        /// <param name="content">Content to print in console</param>
        public static void PrintAndWaitInConsole(string? content)
        {
            PrintInConsole(content);
            WaitInConsole();
        }

        /// <summary>
        /// Print warning to the user that duplicate value found.
        /// </summary>
        /// <param name="content">Duplicate content field</param>
        public static void PrintDuplicateFoundInConsole(string? content)
        {
            PrintInConsole($"{content} already present in inventory!!");
        }

        /// <summary>
        /// Print the product details in console.
        /// </summary>
        /// <param name="product">Product to be printed in console</param>
        public static void PrintProductInConsole(Product product)
        {
            PrintInConsole("Product ID : " + product.ProductId);
            PrintInConsole("Name : " + product.Name);
            PrintInConsole("Price : " + product.Price);
            PrintInConsole("Quantity : " + product.Quantity);
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
            PrintEmptyLine();
            PrintInConsole($"{field} Using:");
            PrintEmptyLine();
            PrintInConsole("1.Product Id");
            PrintInConsole("2.Product Name");
            PrintInConsole("3.Exit");
            PrintEmptyLine();
        }

        /// <summary>
        /// Show the menu option available in edit option.
        /// </summary>
        public static void ShowMenuToEdit()
        {
            PrintInConsole("----------------------------");
            PrintEmptyLine();
            PrintInConsole("Choose the field to edit!");
            PrintEmptyLine();
            PrintInConsole("1.Name");
            PrintInConsole("2.Price");
            PrintInConsole("3.Quantity");
            PrintEmptyLine();
        }

        /// <summary>
        /// Print the add new product header in console.
        /// </summary>
        public static void ShowInventoryHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("   !!Inventory Management System!!");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Print the add new product header in console.
        /// </summary>
        public static void ShowAddNewProductHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("   !!Add New Product in Inventory!!");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Print the edit product header in console.
        /// </summary>
        public static void ShowEditProductHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("        Edit Product");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Print the view product header in console.
        /// </summary>
        public static void ShowViewProductHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("    !!View Product in Inventory!!");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Print the search product header in console.
        /// </summary>
        public static void ShowSearchProductHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("    !!Search Product in Inventory!!");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }

        /// <summary>
        /// Print the delete product header in console.
        /// </summary>
        public static void ShowDeleteProductHeader()
        {
            ClearConsole();
            PrintInConsole(new string('=', 40));
            PrintInConsole("    !!Delete Product in Inventory!!");
            PrintInConsole(new string('=', 40));
            PrintEmptyLine();
        }
    }
}
