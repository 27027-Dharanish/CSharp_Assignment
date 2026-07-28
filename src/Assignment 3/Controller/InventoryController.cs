using System.Diagnostics;
using Assignment_3.Model;
using Assignment_3.Service;
using Assignment_3.View;

namespace Assignment_3.Controller
{
    /// <summary>
    /// Manages Inventory, connect view and Inventory Management service.
    /// </summary>
    public class InventoryController
    {
        private readonly InventoryManagementService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryController"/> class.
        /// </summary>
        /// <param name="service">Inventory management service</param>
        public InventoryController(InventoryManagementService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Starts the execution flow for the Inventory controller management options.
        /// </summary>
        public void StartInventoryManagement()
        {
            this.ShowInventoryManagementOption();
        }

        /// <summary>
        /// Show the option available in the inventory management.
        /// </summary>
        public void ShowInventoryManagementOption()
        {
            int userChoice;
            do
            {
                ConsoleActivity.InventoryMenu();
                string? userChoiceString = ConsoleActivity.GetInputFromConsole("option to perform");
                int.TryParse(userChoiceString, out userChoice);
                switch (userChoice)
                {
                    case (int)Enums.InventoryOption.AddNewProduct:
                        this.HandleAddNewProduct();
                        break;
                    case (int)Enums.InventoryOption.ViewAllProduct:
                        // this.HandleViewAllProduct();
                        break;
                    case (int)Enums.InventoryOption.EditInventory:
                        // this.
                        break;
                    case (int)Enums.InventoryOption.SearchProduct:
                        // this.
                        break;
                    case (int)Enums.InventoryOption.DeleteProduct:
                        // this/
                        break;
                    case (int)Enums.InventoryOption.Exit:
                        // This prevent default case from executing.
                        break;
                    default:
                        ConsoleActivity.PrintInConsole("Exiting from the inventory!!");
                        break;
                }
            }
            while (userChoice != (int)Enums.InventoryOption.Exit);
        }

        private void HandleAddNewProduct()
        {
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("!!Add New Product in Inventory!!");
            ConsoleActivity.PrintEmptyLine();
            string? productID = ConsoleActivity.GetInputFromConsole("Product Id");
            this.ProductIDValidator(productID);
            string? productName = ConsoleActivity.GetInputFromConsole("Product name");
            this.ProductNameValidator(productName);
            string? productPrice = ConsoleActivity.GetInputFromConsole("Product price");
            if (!decimal.TryParse(productPrice, out decimal price))
            {
                ConsoleActivity.PrintInvalidField("product price");
                this.ShowInventoryManagementOption();
            }

            this.ProductPriceValidator(price);
            string? productQuantity = ConsoleActivity.GetInputFromConsole("Product Quantity");
            int.TryParse(productQuantity, out int quantity);
            this.ProductQuantityValidator(quantity);
            if (this._service.AddNewProductToInventory(productID, productName, price, quantity))
            {
                ConsoleActivity.ClearConsole();
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole("Contact Added Successfully!!");
                ConsoleActivity.WaitInConsole();
            }
            else
            {
                ConsoleActivity.ClearConsole();
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole("Failed to add Contact!!");
                ConsoleActivity.WaitInConsole();
            }
        }

        private void ProductNameValidator(string? productName)
        {
            if (InventoryHelper.IsEmpty(productName))
            {
                ConsoleActivity.PrintInvalidField("product Name");
                this.ShowInventoryManagementOption();
            }
            else if (this._service.IsIdAlreadyExist(productName))
            {
                ConsoleActivity.PrintDuplicateFoundInConsole(productName);
                this.ShowInventoryManagementOption();
            }
        }

        private void ProductIDValidator(string? productID)
        {
            if (InventoryHelper.IsEmpty(productID))
            {
                ConsoleActivity.PrintInvalidField("product ID");
                this.ShowInventoryManagementOption();
            }
            else if (this._service.IsIdAlreadyExist(productID))
            {
                ConsoleActivity.PrintDuplicateFoundInConsole(productID);
                this.ShowInventoryManagementOption();
            }
        }

        private void ProductPriceValidator(decimal price)
        {
            if (price >= decimal.MaxValue)
            {
                ConsoleActivity.PrintInConsole("Price value exeeded the range...");
                ConsoleActivity.WaitInConsole();
                this.ShowInventoryManagementOption();
            }
            else if (price < 0)
            {
                ConsoleActivity.PrintInConsole("Price cannot be negative...");
                ConsoleActivity.WaitInConsole();
                this.ShowInventoryManagementOption();
            }
        }

        private void ProductQuantityValidator(int quantity)
        {
            if (quantity >= int.MaxValue)
            {
                ConsoleActivity.PrintInConsole("Quantity value exeeded the range...");
                ConsoleActivity.WaitInConsole();
                this.ShowInventoryManagementOption();
            }
            else if (quantity < 0)
            {
                ConsoleActivity.PrintInConsole("Quantity cannot be negative...");
                ConsoleActivity.WaitInConsole();
                this.ShowInventoryManagementOption();
            }
        }
    }
}
