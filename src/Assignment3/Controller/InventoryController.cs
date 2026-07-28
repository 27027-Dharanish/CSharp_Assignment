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
                        this.HandleViewAllProduct();
                        break;
                    case (int)Enums.InventoryOption.EditInventory:
                        this.HandleEditProduct();
                        break;
                    case (int)Enums.InventoryOption.SearchProduct:
                        this.HandleSearchProduct();
                        break;
                    case (int)Enums.InventoryOption.DeleteProduct:
                        this.HandleDeleteProduct();
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
            ConsoleActivity.ClearConsole();
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("!!Add New Product in Inventory!!");
            ConsoleActivity.PrintEmptyLine();
            string? productID = ConsoleActivity.GetInputFromConsole("Product Id");
            if (!this.ProductIDValidator(productID))
            {
                this.ShowInventoryManagementOption();
            }

            string? productName = ConsoleActivity.GetInputFromConsole("Product name");
            if (!this.ProductNameValidator(productName))
            {
                this.ShowInventoryManagementOption();
            }

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

        private void HandleViewAllProduct()
        {
            ConsoleActivity.ClearConsole();
            ConsoleActivity.PrintInConsole("====== List of Product ======");
            ConsoleActivity.PrintEmptyLine();
            if (this.IsInventoryEmpty())
            {
                return;
            }

            List<Product> products = this._service.GetAllFromInventory();
            foreach (Product product in products)
            {
                ConsoleActivity.PrintProductInConsole(product);
                ConsoleActivity.PrintInConsole("-----------------");
                ConsoleActivity.PrintEmptyLine();
            }

            ConsoleActivity.WaitInConsole();
        }

        private void HandleEditProduct()
        {
            ConsoleActivity.ClearConsole();
            if (this.IsInventoryEmpty())
            {
                return;
            }

            ConsoleActivity.ShowSearchProductMenu("Edit");
            string? userChoiceInput = ConsoleActivity.GetInputFromConsole("option");
            int.TryParse(userChoiceInput, out int userChoice);
            switch (userChoice)
            {
                case (int)Enums.SearchUsingField.SearchUsingID:
                    this.EditUsingProductID();
                    break;
                case (int)Enums.SearchUsingField.SearchUsingProductName:
                    this.EditUsingProductName();
                    break;
                default:
                    ConsoleActivity.PrintInvalidField("option");
                    this.ShowInventoryManagementOption();
                    break;
            }
        }

        private void EditUsingProductID()
        {
            ConsoleActivity.ClearConsole();
            string? id = ConsoleActivity.GetInputFromConsole("product id");
            Product? product = this._service.SearchProductUsingID(id);
            if (product == null)
            {
                ConsoleActivity.PrintInConsole("Product not found!!");
                ConsoleActivity.WaitInConsole();
            }
            else
            {
                this.GetInputAndEditContact(product);
            }
        }

        private void EditUsingProductName()
        {
            ConsoleActivity.ClearConsole();
            string? name = ConsoleActivity.GetInputFromConsole("product Name");
            Product? product = this._service.SearchProductUsingName(name);
            if (product == null)
            {
                ConsoleActivity.PrintInConsole("Product not found!!");
                ConsoleActivity.WaitInConsole();
            }
            else
            {
                this.GetInputAndEditContact(product);
            }
        }

        private void GetInputAndEditContact(Product product)
        {
            ConsoleActivity.ShowMenuToEdit();
            string? id = product.ProductId;
            string? name = product.Name;
            decimal price = product.Price;
            int quantity = product.Quantity;
            string? fieldToEdit = ConsoleActivity.GetInputFromConsole("field to edit");
            int.TryParse(fieldToEdit, out int fieldNumber);
            if (fieldNumber == (int)Enums.ProductFieldToBeEdited.Name)
            {
                string? newProductName = ConsoleActivity.GetInputFromConsole("new product name");
                if (!this.ProductNameValidator(newProductName))
                {
                    return;
                }

                name = newProductName;
            }
            else if (fieldNumber == (int)Enums.ProductFieldToBeEdited.Price)
            {
                string? newProductPrice = ConsoleActivity.GetInputFromConsole("new product price");
                decimal.TryParse(newProductPrice, out decimal newPrice);
                if (!this.ProductPriceValidator(newPrice))
                {
                    return;
                }

                price = newPrice;
            }
            else if (fieldNumber == (int)Enums.ProductFieldToBeEdited.Quantity)
            {
                string? newQuantity = ConsoleActivity.GetInputFromConsole("new product quantity");
                int.TryParse(newQuantity, out int productQuantity);
                if (!this.ProductQuantityValidator(productQuantity))
                {
                    return;
                }

                quantity = productQuantity;
            }
            else
            {
                ConsoleActivity.PrintInConsole("Invalid Input!!");
                ConsoleActivity.WaitInConsole();
                return;
            }

            Product? updatedProduct = this._service.EditProductById(id, name, price, quantity);
            if (updatedProduct != null)
            {
                ConsoleActivity.ClearConsole();
                ConsoleActivity.PrintInConsole("Product Updated successfully!!");
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintProductInConsole(updatedProduct);
                ConsoleActivity.WaitInConsole();
            }
        }

        private void HandleSearchProduct()
        {
            ConsoleActivity.ClearConsole();
            if (this.IsInventoryEmpty())
            {
                return;
            }

            ConsoleActivity.ShowSearchProductMenu("Search");
            string? userChoiceInput = ConsoleActivity.GetInputFromConsole("option");
            int.TryParse(userChoiceInput, out int userChoice);
            switch (userChoice)
            {
                case (int)Enums.SearchUsingField.SearchUsingID:
                    this.SearchUsingProductID();
                    break;
                case (int)Enums.SearchUsingField.SearchUsingProductName:
                    this.SearchUsingProductName();
                    break;
                default:
                    ConsoleActivity.PrintInvalidField("option");
                    this.ShowInventoryManagementOption();
                    break;
            }
        }

        private void SearchUsingProductID()
        {
            ConsoleActivity.ClearConsole();
            string? productId = ConsoleActivity.GetInputFromConsole("Product Id");
            Product? product = this._service.SearchProductUsingID(productId);
            if (product == null)
            {
                ConsoleActivity.PrintInConsole("Product not found!!");
                ConsoleActivity.WaitInConsole();
            }
            else
            {
                ConsoleActivity.PrintInConsole("--Product Found--");
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintProductInConsole(product);
                ConsoleActivity.WaitInConsole();
            }
        }

        private void SearchUsingProductName()
        {
            ConsoleActivity.ClearConsole();
            string? productName = ConsoleActivity.GetInputFromConsole("Product Name");
            Product? product = this._service.SearchProductUsingName(productName);
            if (product == null)
            {
                ConsoleActivity.PrintInConsole("Product not found!!");
                ConsoleActivity.WaitInConsole();
            }
            else
            {
                ConsoleActivity.PrintInConsole("--Product Found--");
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintProductInConsole(product);
                ConsoleActivity.WaitInConsole();
            }
        }

        private void HandleDeleteProduct()
        {
            ConsoleActivity.ClearConsole();
            if (this.IsInventoryEmpty())
            {
                return;
            }

            ConsoleActivity.ShowSearchProductMenu("Delete");
            string? userChoiceInput = ConsoleActivity.GetInputFromConsole("option");
            int.TryParse(userChoiceInput, out int userChoice);
            switch (userChoice)
            {
                case (int)Enums.SearchUsingField.SearchUsingID:
                    this.DeleteUsingProductID();
                    break;
                case (int)Enums.SearchUsingField.SearchUsingProductName:
                    this.DeleteUsingProductName();
                    break;
                default:
                    ConsoleActivity.PrintInvalidField("option");
                    this.ShowInventoryManagementOption();
                    break;
            }
        }

        private void DeleteUsingProductID()
        {
            ConsoleActivity.ClearConsole();
            string? id = ConsoleActivity.GetInputFromConsole("product ID");
            Product? product = this._service.SearchProductUsingID(id);
            if (product == null)
            {
                ConsoleActivity.PrintInConsole("Product not found!!");
                ConsoleActivity.WaitInConsole();
            }
            else
            {
                if (this._service.DeleteProductById(product.ProductId))
                {
                    ConsoleActivity.PrintInConsole("Contact deleted succesfully");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    ConsoleActivity.PrintInConsole("Contact deletion failed");
                    ConsoleActivity.WaitInConsole();
                }
            }
        }

        private void DeleteUsingProductName()
        {
            ConsoleActivity.ClearConsole();
            string? name = ConsoleActivity.GetInputFromConsole("product Name");
            Product? product = this._service.SearchProductUsingName(name);
            if (product == null)
            {
                ConsoleActivity.PrintInConsole("Product not found!!");
                ConsoleActivity.WaitInConsole();
            }
            else
            {
                if (this._service.DeleteProductByName(product.Name))
                {
                    ConsoleActivity.PrintInConsole("Contact deleted succesfully");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    ConsoleActivity.PrintInConsole("Contact deletion failed");
                    ConsoleActivity.WaitInConsole();
                }
            }
        }

        private bool ProductNameValidator(string? productName)
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
            else if (this._service.IsNameAlreadyExist(productName))
            {
                ConsoleActivity.PrintDuplicateFoundInConsole(productName);
                return false;
            }

            return true;
        }

        private bool ProductIDValidator(string? productID)
        {
            if (InventoryHelper.IsEmpty(productID))
            {
                ConsoleActivity.PrintInvalidField("product ID");
                return false;
            }
            else if (!InventoryHelper.IsOnlyDigit(productID))
            {
                ConsoleActivity.PrintInConsole("Product ID must be Digit!!");
                ConsoleActivity.WaitInConsole();
                return false;
            }
            else if (this._service.IsIdAlreadyExist(productID))
            {
                ConsoleActivity.PrintDuplicateFoundInConsole(productID);
                return false;
            }

            return true;
        }

        private bool ProductPriceValidator(decimal price)
        {
            if (price >= decimal.MaxValue)
            {
                ConsoleActivity.PrintInConsole("Price value exeeded the range...");
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

        private bool ProductQuantityValidator(int quantity)
        {
            if (quantity >= int.MaxValue)
            {
                ConsoleActivity.PrintInConsole("Quantity value exeeded the range...");
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

        private bool IsInventoryEmpty()
        {
            if (this._service.InventoryCount() == 0)
            {
                ConsoleActivity.PrintInConsole("No Product found!!");
                ConsoleActivity.WaitInConsole();
                return true;
            }

            return false;
        }
    }
}
