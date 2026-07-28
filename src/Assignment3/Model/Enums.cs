using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3.Model
{
    /// <summary>
    /// Represents the enum available for the inventory management system.
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// Specifies the fields available in the product.
        /// </summary>
        public enum ProductField
        {
            /// <summary>
            /// Represents the id of the product.
            /// </summary>
            Id = 1,

            /// <summary>
            /// Represents the name of the product.
            /// </summary>
            Name = 2,

            /// <summary>
            /// Represents the price of the product.
            /// </summary>
            Price = 3,

            /// <summary>
            /// Represents the quantity of the product.
            /// </summary>
            Quantity = 4,
        }

        /// <summary>
        /// Specifies the option available in the inventory management system.
        /// </summary>
        public enum InventoryOption
        {
            /// <summary>
            /// Represents adding new product.
            /// </summary>
            AddNewProduct = 1,

            /// <summary>
            /// Represents viewing all product.
            /// </summary>
            ViewAllProduct = 2,

            /// <summary>
            /// Represents editing inventory.
            /// </summary>
            EditInventory = 3,

            /// <summary>
            /// Represents searching product.
            /// </summary>
            SearchProduct = 4,

            /// <summary>
            /// Represents searching product.
            /// </summary>
            DeleteProduct = 5,

            /// <summary>
            /// Represents exit from the inventory management system.
            /// </summary>
            Exit = 6,
        }

        /// <summary>
        /// Specifies the option available in the search product in the inventory.
        /// </summary>
        public enum SearchUsingField
        {
            /// <summary>
            /// Represents search using the ID.
            /// </summary>
            SearchUsingID = 1,

            /// <summary>
            /// Represents search using the product name.
            /// </summary>
            SearchUsingProductName = 2,
        }

        /// <summary>
        /// Specifies the fields available in the product for edit alone.
        /// </summary>
        public enum ProductFieldToBeEdited
        {
            /// <summary>
            /// Represents the name of the product.
            /// </summary>
            Name = 1,

            /// <summary>
            /// Represents the price of the product.
            /// </summary>
            Price = 2,

            /// <summary>
            /// Represents the quantity of the product.
            /// </summary>
            Quantity = 3,
        }
    }
}
