using Assignment_3.Model;

namespace Assignment3.Interface
{
    /// <summary>
    /// Defines the data access contracts for managing the product inventory.
    /// </summary>
    internal interface IInventoryRepository
    {
        /// <summary>
        ///  Adds a new product to the inventory.
        /// </summary>
        /// <param name="newProduct">New product</param>
        /// <returns>True if new product added or false if failed</returns>
        public bool AddNewProduct(Product newProduct);

        /// <summary>
        /// Get the duplicate inventory after performing deep copy of original inventory list.
        /// </summary>
        /// <returns>The copy of original inventory</returns>
        public List<Product> GetInventoryProduct();

        /// <summary>
        /// Search the product using the name of the product.
        /// </summary>
        /// <param name="productName">Name of the product</param>
        /// <param name="returnDuplicateProduct">If true the deep copy of the product is returned else the actual reference is returned</param>
        /// <returns>The product matched with the name</returns>
        public Product? SearchProductByName(string? productName, bool returnDuplicateProduct = true);

        /// <summary>
        /// Search the product using the productId of the product.
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="returnDuplicateProduct">If true, the deep copy of the product is returned else the actual reference is returned</param>
        /// <returns>The product matched with the ID</returns>
        public Product? SearchProductByProductId(string? id, bool returnDuplicateProduct = true);

        /// <summary>
        /// Edit the product from inventory using the product ID.
        /// </summary>
        /// <param name="updatedProduct">The updated product</param>
        /// <returns>Deep copy of the updated product</returns>
        public Product? EditProductFromInventoryById(Product updatedProduct);

        /// <summary>
        /// Delete the product using name.
        /// </summary>
        /// <param name="productToBeDeleted">Product to be deleted</param>
        /// <returns>True if product got deleted</returns>
        public bool DeleteProduct(Product productToBeDeleted);

        /// <summary>
        /// Get the count of product in the inventory.
        /// </summary>
        /// <returns>The count of product in inventory</returns>
        public int GetInventoryCount();
    }
}
