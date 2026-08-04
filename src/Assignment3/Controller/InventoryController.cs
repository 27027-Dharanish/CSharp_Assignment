using System.ComponentModel;
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
        /// Starts the execution flow for the inventory controller management options.
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
            try
            {
                int userChoice;
                do
                {
                    ConsoleActivity.InventoryMenu();
                    string? userChoiceString = ConsoleActivity.GetInputFromConsole("option");
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
                            ConsoleActivity.PrintInConsole("Enter a valid input!!");
                            ConsoleActivity.WaitInConsole();
                            break;
                    }
                }
                while (userChoice != (int)Enums.InventoryOption.Exit);
            }
            catch (ArgumentNullException e)
            {
                ConsoleActivity.PrintInConsole("Argument null exception : " + e.Message);
            }
            catch (Exception e)
            {
                ConsoleActivity.PrintInConsole("Exception occurred and exception message :" + e.Message);
            }
        }

        /// <summary>
        /// Collects user input and adds a new product to the inventory.
        /// </summary>
        private void HandleAddNewProduct()
        {
            ConsoleActivity.ClearConsole();
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("!!Add New Product in Inventory!!");
            ConsoleActivity.PrintEmptyLine();
            string? productID = ConsoleActivity.GetInputFromConsole("Product Id");
            if (!this.ProductIDValidator(productID))
            {
                return;
            }

            string? productName = ConsoleActivity.GetInputFromConsole("Product name");
            if (!this.ProductNameValidator(productName))
            {
                return;
            }

            string? productPrice = ConsoleActivity.GetInputFromConsole("Product price");
            if (!decimal.TryParse(productPrice, out decimal price))
            {
                ConsoleActivity.PrintInvalidField("Product price must be in decimal");
                return;
            }

            if (!InventoryHelper.ProductPriceValidator(price))
            {
                return;
            }

            string? productQuantity = ConsoleActivity.GetInputFromConsole("Product Quantity");
            int.TryParse(productQuantity, out int quantity);
            if (!InventoryHelper.ProductQuantityValidator(quantity))
            {
                return;
            }

            if (this._service.AddNewProductToInventory(productID, productName, price, quantity))
            {
                ConsoleActivity.ClearConsole();
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole("Product Added Successfully!!");
                ConsoleActivity.WaitInConsole();
            }
            else
            {
                ConsoleActivity.ClearConsole();
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole("Failed to add product!!");
                ConsoleActivity.WaitInConsole();
            }
        }

        /// <summary>
        /// Retrieves all product sorted by name and displays them.Shows an empty-list message if none exist.
        /// </summary>
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

        /// <summary>
        /// Coordinates the process for modifying an existing product.
        /// </summary>
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
                    break;
            }
        }

        /// <summary>
        /// Edit the product details using the product ID.
        /// </summary>
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

        /// <summary>
        /// Edit the product using the product name.
        /// </summary>
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

        /// <summary>
        /// Enable the user to select and edit individual fields of a product.
        /// </summary>
        /// <param name="product">Product to be updated</param>
        private void GetInputAndEditContact(Product product)
        {
            ConsoleActivity.ClearConsole();
            ConsoleActivity.PrintInConsole("Product to be edited!!");
            ConsoleActivity.PrintProductInConsole(product);
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
                if (!InventoryHelper.ProductPriceValidator(newPrice))
                {
                    return;
                }

                price = newPrice;
            }
            else if (fieldNumber == (int)Enums.ProductFieldToBeEdited.Quantity)
            {
                string? newQuantity = ConsoleActivity.GetInputFromConsole("new product quantity");
                int.TryParse(newQuantity, out int productQuantity);
                if (!InventoryHelper.ProductQuantityValidator(productQuantity))
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

        /// <summary>
        /// Coordinates the process for searching an existing product.
        /// </summary>
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
                    return;
            }
        }

        /// <summary>
        /// Search the product from inventory using the product ID.
        /// </summary>
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

        /// <summary>
        /// Search the product from the inventory using the product name.
        /// </summary>
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

        /// <summary>
        /// Coordinates the process for deleting an existing product.
        /// </summary>
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
                    break;
            }
        }

        /// <summary>
        /// Delete the product using the product ID.
        /// </summary>
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
                    ConsoleActivity.PrintInConsole("Product deleted successfully");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    ConsoleActivity.PrintInConsole("Product deletion failed");
                    ConsoleActivity.WaitInConsole();
                }
            }
        }

        /// <summary>
        /// Delete the product using the product name.
        /// </summary>
        private void DeleteUsingProductName()
        {
            ConsoleActivity.ClearConsole();
            string? name = ConsoleActivity.GetInputFromConsole("product Name");
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleActivity.PrintInvalidField("name");
                return;
            }

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
                    ConsoleActivity.PrintInConsole("Product deleted successfully");
                    ConsoleActivity.WaitInConsole();
                }
                else
                {
                    ConsoleActivity.PrintInConsole("Product deletion failed");
                    ConsoleActivity.WaitInConsole();
                }
            }
        }

        /// <summary>
        /// Check whether the name is valid and duplicate exist or not.
        /// </summary>
        /// <param name="productName">Name to be validated</param>
        /// <returns>True if name is valid else false</returns>
        private bool ProductNameValidator(string? productName)
        {
            if (!InventoryHelper.IsValidProductName(productName))
            {
                return false;
            }
            else if (this._service.IsNameAlreadyExist(productName))
            {
                ConsoleActivity.PrintDuplicateFoundInConsole(productName);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the id is valid and duplicate exist.
        /// </summary>
        /// <param name="productID">Id to be checked</param>
        /// <returns>True if product id is valid else false</returns>
        private bool ProductIDValidator(string? productID)
        {
            if (!InventoryHelper.IsValidProductId(productID))
            {
                return false;
            }
            else if (this._service.IsIdAlreadyExist(productID))
            {
                ConsoleActivity.PrintDuplicateFoundInConsole(productID);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the product inventory is empty.
        /// </summary>
        /// <returns>True if inventory is empty else false</returns>
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
