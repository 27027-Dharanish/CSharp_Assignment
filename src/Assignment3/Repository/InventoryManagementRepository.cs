using Assignment_3.Model;

namespace Assignment_3.Repository
{
    /// <summary>
    /// Provides a centralized data repository for storing, retrieving, editing, deleting Inventory entities.
    /// </summary>
    public class InventoryManagementRepository
    {
        private readonly List<Product> _inventoryList = new ();

        /// <summary>
        ///  Adds a new product to the inventory.
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <param name="name">Name of the product</param>
        /// <param name="price">Price of the product</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <returns>Return true if new product added or false if failed</returns>
        public bool AddNewProduct(string? id, string? name, decimal price, int quantity)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Product newProduct = this.CreateNewProduct(id, name, price, quantity);
            int previousInventoryCount = this._inventoryList.Count;
            this._inventoryList.Add(newProduct);
            if (previousInventoryCount == this._inventoryList.Count)
            {
                // return false because the previous inventory count and current are same, this says that no new item is added.
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the duplicate inventory after performing deep copy of original inventory list.
        /// </summary>
        /// <returns>Return the copy of original inventory</returns>
        public List<Product> GetInventoryProduct()
        {
            List<Product> duplicateInventory = new ();
            foreach (Product product in this._inventoryList)
            {
                duplicateInventory.Add(this.CreateNewProduct(product.ProductId, product.Name, product.Price, product.Quantity));
            }

            return duplicateInventory;
        }

        /// <summary>
        /// Search the product using the name of the product.
        /// </summary>
        /// <param name="productName">Name of the product</param>
        /// <param name="returnDuplicateProduct">If true the deep copy of the product is returned else the actual reference is returned</param>
        /// <returns>Return the product matched with the name</returns>
        public Product? SearchProductByName(string? productName, bool returnDuplicateProduct = true)
        {
            Product? matchedProduct = this._inventoryList.Find(product => string.Equals(productName, product.Name, StringComparison.OrdinalIgnoreCase));
            if (matchedProduct == null)
            {
                return null;
            }

            if (returnDuplicateProduct)
            {
                return this.CreateNewProduct(matchedProduct.ProductId, matchedProduct.Name, matchedProduct.Price, matchedProduct.Quantity);
            }

            return matchedProduct;
        }

        /// <summary>
        /// Search the product using the ProductId of the product.
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="returnDuplicateProduct">If true, the deep copy of the product is returned else the actual reference is returned</param>
        /// <returns>Return the product matched with the ID</returns>
        public Product? SearchProductByProductId(string? id, bool returnDuplicateProduct = true)
        {
            Product? matchedProduct = this._inventoryList.Find(product => string.Equals(product.ProductId, id, StringComparison.OrdinalIgnoreCase));
            if (matchedProduct == null)
            {
                return null;
            }

            if (returnDuplicateProduct)
            {
                return this.CreateNewProduct(matchedProduct.ProductId, matchedProduct.Name, matchedProduct.Price, matchedProduct.Quantity);
            }

            return matchedProduct;
        }

        /// <summary>
        /// Edit the product using name.
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <param name="name">Name of the product</param>
        /// <param name="price">Price of the product</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <returns>Return the updated product</returns>
        public Product? EditProductFromInventoryById(string? id, string? name, decimal price, int quantity)
        {
            Product? productToBeEdit = this.SearchProductByProductId(id, false);
            if (productToBeEdit == null)
            {
                return null;
            }

            productToBeEdit.Name = name;
            productToBeEdit.Price = price;
            productToBeEdit.Quantity = quantity;

            return this.SearchProductByName(name);
        }

        /// <summary>
        /// Check if product name exist or not.
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <returns>Return true if product name exist else false</returns>
        public bool CheckIfNameExist(string? name)
        {
            Product? checkIfProductExist = this.SearchProductByName(name);
            if (checkIfProductExist == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if product id exist or not.
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <returns>Return true of product id exist else false</returns>
        public bool CheckIfIdExist(string? id)
        {
            Product? checkIfProductExist = this.SearchProductByProductId(id);
            if (checkIfProductExist == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Delete the product using ID.
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <returns>Return true if product got deleted</returns>
        public bool DeleteProductById(string? id)
        {
            Product? productToBeEdit = this.SearchProductByProductId(id, false);
            if (productToBeEdit == null)
            {
                return false;
            }

            this._inventoryList.Remove(productToBeEdit);
            return true;
        }

        /// <summary>
        /// Delete the product using name.
        /// </summary>
        /// <param name="name">Id of the product</param>
        /// <returns>Return true if product got deleted</returns>
        public bool DeleteProductByName(string? name)
        {
            Product? productToBeEdit = this.SearchProductByName(name, false);
            if (productToBeEdit == null)
            {
                return false;
            }

            this._inventoryList.Remove(productToBeEdit);
            return true;
        }

        /// <summary>
        /// Get the count of product in the inventory.
        /// </summary>
        /// <returns>Return the count of product in inventory</returns>
        public int GetInventoryCount()
        {
            return this._inventoryList.Count;
        }

        /// <summary>
        /// Create a new product with the given parameter.
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="name">Product name</param>
        /// <param name="price">Product price</param>
        /// <param name="quantity">Product quantity</param>
        /// <returns>Return the product that have been created</returns>
        private Product CreateNewProduct(string? id, string? name, decimal price, int quantity)
        {
            Product newProduct = new (id);
            newProduct.Name = name;
            newProduct.Price = price;
            newProduct.Quantity = quantity;
            return newProduct;
        }
    }
}
