using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_3.Model;

namespace Assignment_3.Service
{
    /// <summary>
    /// Coordinate the business logic for the inventory management system.
    /// </summary>
    public class InventoryManagementService
    {
        private InventoryManagementRepository _productInventory = new InventoryManagementRepository();

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
            return this._productInventory.AddNewProduct(id, productName, price, quantity);
        }

        /// <summary>
        /// Check if the product name already exist.
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <returns>Return true if name already exist else false</returns>
        public bool IsNameAlreadyExist(string? name)
        {
            return this._productInventory.CheckIfNameExist(name);
        }

        /// <summary>
        /// Check if the product ID already exist.
        /// </summary>
        /// <param name="name">Name of the id</param>
        /// <returns>Return true if id already exist else false</returns>
        public bool IsIdAlreadyExist(string? name)
        {
            return this._productInventory.CheckIfIdExist(name);
        }
    }
}
