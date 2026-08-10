using System.Xml.Linq;
using Assignment_3.Model;
using Assignment_3.Repository;
using Assignment3.Interface;

namespace Assignment_3.Service
{
    /// <summary>
    /// Coordinate the business logic for the inventory management system.
    /// </summary>
    public class InventoryManagementService
    {
        private readonly IInventoryRepository _productInventory = new InventoryManagementRepository();

        /// <summary>
        /// Add new product to the inventory.
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <param name="productName">Name of the product</param>
        /// <param name="price">Price of the product</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <returns>Return if product added or not</returns>
        public bool AddNewProductToInventory(string? id, string? productName, decimal price, int quantity)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Product newProduct = new (id, productName, price, quantity);
            return this._productInventory.AddNewProduct(newProduct);
        }

        /// <summary>
        /// Get all product available in inventory.
        /// </summary>
        /// <returns>List of all product</returns>
        public List<Product> GetAllFromInventory()
        {
            List<Product> products = this._productInventory.GetInventoryProduct();
            products.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase));
            return products;
        }

        /// <summary>
        /// Check if the product name already exist.
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <returns>True if name already exist else false</returns>
        public bool IsNameAlreadyExist(string? name)
        {
            Product? checkIfProductExist = this._productInventory.SearchProductByName(name);
            if (checkIfProductExist == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if the product ID already exist.
        /// </summary>
        /// <param name="id">Name of the id</param>
        /// <returns>True if id already exist else false</returns>
        public bool IsIdAlreadyExist(string? id)
        {
            Product? checkIfProductExist = this._productInventory.SearchProductByProductId(id);
            if (checkIfProductExist == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the count of product in the inventory.
        /// </summary>
        /// <returns>The count of product in inventory</returns>
        public int InventoryCount()
        {
            return this._productInventory.GetInventoryCount();
        }

        /// <summary>
        /// Search the product using the name.
        /// </summary>
        /// <param name="productName">Name of the product</param>
        /// <returns>The product matched with the name</returns>
        public Product? SearchProductUsingName(string? productName)
        {
            return this._productInventory.SearchProductByName(productName);
        }

        /// <summary>
        /// Search the product using the id.
        /// </summary>
        /// <param name="productId">ID of the product</param>
        /// <returns>The product matched with the Id</returns>
        public Product? SearchProductUsingID(string? productId)
        {
            return this._productInventory.SearchProductByProductId(productId);
        }

        /// <summary>
        /// Edit the product from the inventory by using id.
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <param name="name">Name of the product</param>
        /// <param name="price">Price of the product</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <returns>The product that got edited</returns>
        public Product? EditProductById(string? id, string? name, decimal price, int quantity)
        {
            return this._productInventory.EditProductFromInventoryById(id, name, price, quantity);
        }

        /// <summary>
        /// Delete product from the inventory by Id.
        /// </summary>
        /// <param name="id">Id that used to deleted</param>
        /// <returns>True if product got deleted else false</returns>
        public bool DeleteProductById(string? id)
        {
            Product? matchedProduct = this.SearchProductUsingID(id);
            if (matchedProduct != null)
            {
                return this._productInventory.DeleteProduct(matchedProduct);
            }

            return false;
        }

        /// <summary>
        /// Delete product from the inventory by name.
        /// </summary>
        /// <param name="name">Name that used to deleted</param>
        /// <returns>True if product got deleted else false</returns>
        public bool DeleteProductByName(string? name)
        {
            Product? matchedProduct = this.SearchProductUsingName(name);
            if (matchedProduct != null)
            {
                return this._productInventory.DeleteProduct(matchedProduct);
            }

            return false;
        }
    }
}
