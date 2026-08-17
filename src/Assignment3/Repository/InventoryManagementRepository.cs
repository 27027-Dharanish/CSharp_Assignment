using Assignment_3.Model;
using Assignment3.Interface;

namespace Assignment_3.Repository
{
    /// <summary>
    /// Provides a centralized data repository for storing, retrieving, editing, deleting inventory entities.
    /// </summary>
    public class InventoryManagementRepository : IInventoryRepository
    {
        private readonly List<Product> _inventoryList = new ();

        /// <inheritdoc />
        public bool AddNewProduct(Product newProduct)
        {
            int previousInventoryCount = this._inventoryList.Count;
            this._inventoryList.Add(newProduct);
            if (previousInventoryCount == this._inventoryList.Count)
            {
                // return false because the previous inventory count and current are same, this says that no new item is added.
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public List<Product> GetInventoryProduct()
        {
            List<Product> duplicateInventory = new ();
            foreach (Product product in this._inventoryList)
            {
                duplicateInventory.Add(this.CreateNewProduct(product.ProductId, product.Name, product.Price, product.Quantity));
            }

            return duplicateInventory;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public Product? EditProductFromInventoryById(Product updatedProduct)
        {
            Product? productToBeEdit = this.SearchProductByProductId(updatedProduct.ProductId, false);
            if (productToBeEdit == null)
            {
                return null;
            }

            productToBeEdit.Name = updatedProduct.Name;
            productToBeEdit.Price = updatedProduct.Price;
            productToBeEdit.Quantity = updatedProduct.Quantity;

            // Return deep copy of the product that got edited
            return this.CreateNewProduct(updatedProduct.ProductId, updatedProduct.Name, updatedProduct.Price, updatedProduct.Quantity);
        }

        /// <inheritdoc />
        public bool DeleteProduct(Product productToBeDeleted)
        {
            this._inventoryList.Remove(productToBeDeleted);
            return true;
        }

        /// <inheritdoc />
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
        /// <returns>The product that have been created</returns>
        private Product CreateNewProduct(string? id, string? name, decimal price, int quantity)
        {
            Product newProduct = new (id, name, price, quantity);
            return newProduct;
        }
    }
}
