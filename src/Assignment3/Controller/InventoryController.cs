using Assignment_3.Model;
using Assignment_3.Service;
using Assignment_3.View;
using Assignment3.View;

namespace Assignment_3.Controller
{
    /// <summary>
    /// Manages inventory, connect view and inventory management service.
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
                Enums.InventoryOption userChoice;
                do
                {
                    ConsoleActivity.InventoryMenu();
                    string? userChoiceString = ConsoleActivity.GetInputFromConsole("option");
                    int.TryParse(userChoiceString, out int choice);
                    userChoice = (Enums.InventoryOption)choice;
                    switch (userChoice)
                    {
                        case Enums.InventoryOption.AddNewProduct:
                            this.HandleAddNewProduct();
                            break;
                        case Enums.InventoryOption.ViewAllProduct:
                            this.HandleViewAllProduct();
                            break;
                        case Enums.InventoryOption.EditInventory:
                            this.HandleEditProduct();
                            break;
                        case Enums.InventoryOption.SearchProduct:
                            this.HandleSearchProduct();
                            break;
                        case Enums.InventoryOption.DeleteProduct:
                            this.HandleDeleteProduct();
                            break;
                        case Enums.InventoryOption.Exit:
                            // This prevent default case from executing.
                            break;
                        default:
                            ConsoleActivity.PrintInConsole("Enter a valid input!!");
                            ConsoleActivity.WaitInConsole();
                            break;
                    }
                }
                while (userChoice != Enums.InventoryOption.Exit);
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
            ConsoleActivity.ShowInventoryHeader("ADD NEW PRODUCT");
            (bool isValidProductId, string? productID) = this.GetProductIdWithRetry("ADD NEW PRODUCT", ConsoleActivity.ShowInventoryHeader);
            if (!isValidProductId)
            {
                return;
            }

            (bool isValidProductName, string? productName) = this.GetProductNameWithRetry("ADD NEW PRODUCT", ConsoleActivity.ShowInventoryHeader);
            if (!isValidProductName)
            {
                return;
            }

            (bool isValidProductPrice, decimal productPrice) = this.GetProductPriceWithRetry("ADD NEW PRODUCT", ConsoleActivity.ShowInventoryHeader);
            if (!isValidProductPrice)
            {
                return;
            }

            (bool isValidProductQuantity, int productQuantity) = this.GetProductQuantityWithRetry("ADD NEW PRODUCT", ConsoleActivity.ShowInventoryHeader);
            if (!isValidProductQuantity)
            {
                return;
            }

            ConsoleActivity.ShowInventoryHeader("ADD NEW PRODUCT");
            if (this._service.AddNewProductToInventory(productID, productName, productPrice, productQuantity))
            {
                ConsoleActivity.PrintInConsole("Product Added Successfully!!");
            }
            else
            {
                ConsoleActivity.PrintInConsole("Failed to add product!!");
            }

            ConsoleActivity.WaitInConsole();
        }

        /// <summary>
        /// Retrieves all product sorted by name and displays them .Shows an empty-list message if none exist.
        /// </summary>
        private void HandleViewAllProduct()
        {
            ConsoleActivity.ClearConsole();
            if (this.IsInventoryEmpty())
            {
                return;
            }

            ConsoleActivity.ShowInventoryHeader("VIEW PRODUCT FROM INVENTORY");
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
            Enums.SearchUsingField userChoice;
            do
            {
                ConsoleActivity.ClearConsole();
                if (this.IsInventoryEmpty())
                {
                    return;
                }

                ConsoleActivity.ShowInventoryHeader("EDIT PRODUCT IN INVENTORY");
                ConsoleActivity.ShowSearchProductMenu("Edit");
                string? userChoiceInput = ConsoleActivity.GetInputFromConsole("option");
                int.TryParse(userChoiceInput, out int choice);
                userChoice = (Enums.SearchUsingField)choice;
                switch (userChoice)
                {
                    case Enums.SearchUsingField.SearchUsingID:
                        this.ExecuteEditProduct(this.GetProductIdWithRetry, this._service.SearchProductUsingID);
                        break;
                    case Enums.SearchUsingField.SearchUsingProductName:
                        this.ExecuteEditProduct(this.GetProductNameWithRetry, this._service.SearchProductUsingName);
                        break;
                    case Enums.SearchUsingField.Exit:
                        break;
                    default:
                        ConsoleActivity.PrintAndWaitInConsole("Invalid option");
                        break;
                }
            }
            while (userChoice != Enums.SearchUsingField.Exit);
        }

        /// <summary>
        /// Finds a product for editing by retrieving validated user input and looking up the matching data item.
        /// </summary>
        /// <param name="getProductDetails">Get the user for product details and returns a validation status</param>
        /// <param name="searchProduct">The method that searches for the product</param>
        private void ExecuteEditProduct(Func<string?, Action<string?>, bool, (bool, string?)> getProductDetails, Func<string?, Product?> searchProduct)
        {
            ConsoleActivity.ShowInventoryHeader("EDIT PRODUCT FROM INVENTORY");
            (bool isValidProductDetails, string? productDetails) = getProductDetails("EDIT PRODUCT FROM INVENTORY", ConsoleActivity.ShowInventoryHeader, true);
            if (!isValidProductDetails)
            {
                return;
            }

            Product? product = searchProduct(productDetails);
            if (product == null)
            {
                ConsoleActivity.PrintAndWaitInConsole("Product not found!!");
            }
            else
            {
                this.GetInputForEdit(product);
            }
        }

        /// <summary>
        /// Enable the user to select and edit individual fields of a product.
        /// </summary>
        /// <param name="product">Product to be updated</param>
        private void GetInputForEdit(Product product)
        {
            ConsoleActivity.ShowInventoryHeader("EDIT PRODUCT IN INVENTORY");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Product to be edited!!");
            ConsoleActivity.PrintProductInConsole(product);
            ConsoleActivity.ShowMenuToEdit();
            string? id = product.ProductId;
            string? name = product.Name;
            decimal price = product.Price;
            int quantity = product.Quantity;
            string? fieldToEdit = ConsoleActivity.GetInputFromConsole("field to edit");
            int.TryParse(fieldToEdit, out int number);
            Enums.ProductFieldToBeEdited fieldNumber = (Enums.ProductFieldToBeEdited)number;
            switch (fieldNumber)
            {
                case Enums.ProductFieldToBeEdited.Name:
                    (bool isValidProductName, string? productName) = this.GetProductNameWithRetry("EDIT PRODUCT IN INVENTORY", ConsoleActivity.ShowInventoryHeader);
                    if (!isValidProductName)
                    {
                        return;
                    }

                    name = productName;
                    break;
                case Enums.ProductFieldToBeEdited.Price:
                    (bool isValidProductPrice, decimal productPrice) = this.GetProductPriceWithRetry("EDIT PRODUCT IN INVENTORY", ConsoleActivity.ShowInventoryHeader);
                    if (!isValidProductPrice)
                    {
                        return;
                    }

                    price = productPrice;
                    break;
                case Enums.ProductFieldToBeEdited.Quantity:
                    (bool isValidProductQuantity, int productQuantity) = this.GetProductQuantityWithRetry("EDIT PRODUCT IN INVENTORY", ConsoleActivity.ShowInventoryHeader);
                    if (!isValidProductQuantity)
                    {
                        return;
                    }

                    quantity = productQuantity;
                    break;
                default:
                    ConsoleActivity.PrintAndWaitInConsole("Invalid Input!!");
                    return;
            }

            this.EditProduct(id, name, price, quantity);
        }

        /// <summary>
        /// Updates an existing product's details.
        /// </summary>
        /// <param name="id">The unique identifier of the product to modify.</param>
        /// <param name="name">The new name to apply to the product.</param>
        /// <param name="price">The updated price value for the product.</param>
        /// <param name="quantity">The updated stock quantity value for the product.</param>
        private void EditProduct(string? id, string? name, decimal price, int quantity)
        {
            ConsoleActivity.ShowInventoryHeader("EDIT PRODUCT IN INVENTORY");
            ConsoleActivity.PrintEmptyLine();
            Product? updatedProduct = this._service.EditProductById(id, name, price, quantity);
            if (updatedProduct != null)
            {
                ConsoleActivity.PrintInConsole("Product updated successfully!!");
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintProductInConsole(updatedProduct);
            }
            else
            {
                ConsoleActivity.PrintInConsole("Product failed successfully!!");
                ConsoleActivity.PrintEmptyLine();
            }

            ConsoleActivity.WaitInConsole();
        }

        /// <summary>
        /// Coordinates the process for searching an existing product.
        /// </summary>
        private void HandleSearchProduct()
        {
            Enums.SearchUsingField userChoice;
            do
            {
                ConsoleActivity.ClearConsole();
                if (this.IsInventoryEmpty())
                {
                    return;
                }

                ConsoleActivity.ShowInventoryHeader("SEARCH PRODUCT FROM INVENTORY");
                ConsoleActivity.ShowSearchProductMenu("Search");
                string? userChoiceInput = ConsoleActivity.GetInputFromConsole("option");
                int.TryParse(userChoiceInput, out int choice);
                userChoice = (Enums.SearchUsingField)choice;
                switch (userChoice)
                {
                    case Enums.SearchUsingField.SearchUsingID:
                        this.ExecuteProductSearch("Product Id", this._service.SearchProductUsingID);
                        break;
                    case Enums.SearchUsingField.SearchUsingProductName:
                        this.ExecuteProductSearch("Product name", this._service.SearchProductUsingName);
                        break;
                    case Enums.SearchUsingField.Exit:
                        break;
                    default:
                        ConsoleActivity.PrintAndWaitInConsole("Invalid option!!");
                        return;
                }
            }
            while (userChoice != Enums.SearchUsingField.Exit);
        }

        /// <summary>
        /// Search and display a single product using a custom search criterion function.
        /// </summary>
        /// <param name="inputLabel">The descriptive prompt text displayed to the user when asking for search input.</param>
        /// <param name="searchCriteria">The logic function that takes the user's input string and returns the matching Product object or null.</param>
        private void ExecuteProductSearch(string inputLabel, Func<string?, Product?> searchCriteria)
        {
            ConsoleActivity.ShowInventoryHeader("SEARCH PRODUCT FROM INVENTORY");
            string? userInput = ConsoleActivity.GetInputFromConsole(inputLabel);
            Product? product = searchCriteria(userInput);
            ConsoleActivity.ShowInventoryHeader("SEARCH PRODUCT FROM INVENTORY");
            if (product == null)
            {
                ConsoleActivity.PrintInConsole("Product not found!!");
            }
            else
            {
                ConsoleActivity.PrintInConsole("--Product Found--");
                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintProductInConsole(product);
            }

            ConsoleActivity.WaitInConsole();
        }

        /// <summary>
        /// Coordinates the process for deleting an existing product.
        /// </summary>
        private void HandleDeleteProduct()
        {
            Enums.SearchUsingField userChoice;
            do
            {
                ConsoleActivity.ClearConsole();
                if (this.IsInventoryEmpty())
                {
                    return;
                }

                ConsoleActivity.ShowInventoryHeader("DELETE PRODUCT IN INVENTORY");
                ConsoleActivity.ShowSearchProductMenu("Delete");
                string? userChoiceInput = ConsoleActivity.GetInputFromConsole("option");
                int.TryParse(userChoiceInput, out int choice);
                userChoice = (Enums.SearchUsingField)choice;
                switch (userChoice)
                {
                    case Enums.SearchUsingField.SearchUsingID:
                        this.ExecuteProductDelete("Product Id", this._service.DeleteProductById, this._service.IsIdAlreadyExist);
                        break;
                    case Enums.SearchUsingField.SearchUsingProductName:
                        this.ExecuteProductDelete("Product Name", this._service.DeleteProductByName, this._service.IsNameAlreadyExist);
                        break;
                    case Enums.SearchUsingField.Exit:
                        break;
                    default:
                        ConsoleActivity.PrintAndWaitInConsole("Invalid option");
                        break;
                }
            }
            while (userChoice != Enums.SearchUsingField.Exit);
        }

        /// <summary>
        /// Find and delete a product based on user input, processing validation.
        /// </summary>
        /// <param name="inputLabel">Display the input label</param>
        /// <param name="deleteProduct">Executes the actual deletion and returns true if successful.</param>
        /// <param name="checkProductExist">The validation function that checks if the entered product identifier exists in the system.</param>
        private void ExecuteProductDelete(string inputLabel, Func<string?, bool> deleteProduct, Func<string?, bool> checkProductExist)
        {
            ConsoleActivity.ShowInventoryHeader("DELETE PRODUCT IN INVENTORY");
            string? userInput = ConsoleActivity.GetInputFromConsole(inputLabel);
            ConsoleActivity.ShowInventoryHeader("DELETE PRODUCT IN INVENTORY");
            if (checkProductExist(userInput))
            {
                if (deleteProduct(userInput))
                {
                    ConsoleActivity.PrintAndWaitInConsole("Product deleted successfully");
                }
                else
                {
                    ConsoleActivity.PrintAndWaitInConsole("Product deletion failed");
                }
            }
            else
            {
                ConsoleActivity.PrintAndWaitInConsole("Product not found!!");
            }
        }

        /// <summary>
        /// Check whether the product inventory is empty.
        /// </summary>
        /// <returns>True if inventory is empty else false</returns>
        private bool IsInventoryEmpty()
        {
            if (this._service.InventoryCount() == 0)
            {
                ConsoleActivity.PrintAndWaitInConsole("No Product found!!");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Prompts the user for a product id with retry attempts.
        /// </summary>
        /// <param name="headerName">Name of the header</param>
        /// <param name="action">The UI layout or header action</param>
        /// <param name="isIgnoreDuplicate">Whether to ignore duplicate or not</param>
        /// <returns>A tuple of bool whether id is valid and string of valid id</returns>
        private (bool, string?) GetProductIdWithRetry(string? headerName, Action<string?> action, bool isIgnoreDuplicate = false)
        {
            int userAttempt = 4;
            do
            {
                action(headerName);
                string? productID = ConsoleActivity.GetInputFromConsole("Product Id");
                if (!ConsoleHelper.IsValidProductId(productID))
                {
                    userAttempt--;
                }
                else if (!isIgnoreDuplicate && this._service.IsIdAlreadyExist(productID))
                {
                    ConsoleActivity.PrintDuplicateFoundInConsole(productID);
                    userAttempt--;
                }
                else
                {
                    return (true, productID);
                }

                ConsoleActivity.PrintAndWaitInConsole($"{userAttempt} attempts remaining!!");
            }
            while (userAttempt > 0);
            return (false, default);
        }

        /// <summary>
        /// Prompts the user for a product name with retry attempts
        /// </summary>
        /// <param name="headerName">Name of the header</param>
        /// <param name="action">The UI layout or header action</param>
        /// <returns>>A tuple indicating if the input is valid, and the name</returns>
        private (bool, string?) GetProductNameWithRetry(string? headerName, Action<string?> action, bool isIgnoreDuplicate = false)
        {
            int userAttempt = 4;
            do
            {
                action(headerName);
                string? productName = ConsoleActivity.GetInputFromConsole("product name");
                if (!ConsoleHelper.IsValidProductName(productName))
                {
                    userAttempt--;
                }
                else if (!isIgnoreDuplicate && this._service.IsNameAlreadyExist(productName))
                {
                    ConsoleActivity.PrintDuplicateFoundInConsole(productName);
                    userAttempt--;
                }
                else
                {
                    return (true, productName);
                }

                ConsoleActivity.PrintAndWaitInConsole($"{userAttempt} attempts remaining!!");
            }
            while (userAttempt > 0);
            return (false, default);
        }

        /// <summary>
        /// Prompts the user for a product price with retry attempts
        /// </summary>
        /// <param name="headerName">Name of the header</param>
        /// <param name="action">The UI layout or header action</param>
        /// <returns>>A tuple indicating if the input is valid, and the parsed price value</returns>
        private (bool, decimal) GetProductPriceWithRetry(string? headerName, Action<string?> action)
        {
            int userAttempt = 4;
            do
            {
                action(headerName);
                string? productPrice = ConsoleActivity.GetInputFromConsole("Product price");
                if (!ConsoleHelper.IsOnlyDigit(productPrice))
                {
                    ConsoleActivity.PrintInConsole("Product price must be in decimal and cannot be negative!!");
                    userAttempt--;
                }
                else if (decimal.TryParse(productPrice, out decimal price))
                {
                    if (!ConsoleHelper.ProductPriceValidator(price))
                    {
                        userAttempt--;
                    }
                    else
                    {
                        return (true, price);
                    }
                }
                else
                {
                    ConsoleActivity.PrintInConsole("Price value exceeded the range...");
                    ConsoleActivity.PrintInConsole("Price value must be within : " + decimal.MaxValue);
                    userAttempt--;
                }

                ConsoleActivity.PrintAndWaitInConsole($"{userAttempt} attempts remaining!!");
            }
            while (userAttempt > 0);
            return (false, default);
        }

        /// <summary>
        /// Prompts the user for a product quantity with retry attempts
        /// </summary>
        /// <param name="headerName">Name of the header</param>
        /// <param name="action">The UI layout or header action</param>
        /// <returns>>A tuple indicating if the input is valid, and the parsed quantity value</returns>
        private (bool, int) GetProductQuantityWithRetry(string? headerName, Action<string?> action)
        {
            int userAttempt = 4;
            do
            {
                action(headerName);
                string? productQuantity = ConsoleActivity.GetInputFromConsole("Product quantity");
                if (!ConsoleHelper.IsOnlyDigit(productQuantity))
                {
                    ConsoleActivity.PrintInConsole("Product quantity must be in decimal and cannot be negative!!");
                    userAttempt--;
                }
                else if (int.TryParse(productQuantity, out int quantity))
                {
                    if (!ConsoleHelper.ProductQuantityValidator(quantity))
                    {
                        userAttempt--;
                    }
                    else
                    {
                        return (true, quantity);
                    }
                }
                else
                {
                    ConsoleActivity.PrintInConsole("Quantity value exceeded the range...");
                    ConsoleActivity.PrintInConsole("Quantity must be within : " + int.MaxValue);
                    userAttempt--;
                }

                ConsoleActivity.PrintAndWaitInConsole($"{userAttempt} attempts remaining!!");
            }
            while (userAttempt > 0);
            return (false, default);
        }
    }
}
