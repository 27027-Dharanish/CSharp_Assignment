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
            Name,

            /// <summary>
            /// Represents the price of the product.
            /// </summary>
            Price,

            /// <summary>
            /// Represents the quantity of the product.
            /// </summary>
            Quantity,
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
            ViewAllProduct,

            /// <summary>
            /// Represents editing inventory.
            /// </summary>
            EditInventory,

            /// <summary>
            /// Represents searching product.
            /// </summary>
            SearchProduct,

            /// <summary>
            /// Represents searching product.
            /// </summary>
            DeleteProduct,

            /// <summary>
            /// Represents exit from the inventory management system.
            /// </summary>
            Exit,
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
            SearchUsingProductName,

            /// <summary>
            /// Represents exit from the inventory management system.
            /// </summary>
            Exit,
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
            Price,

            /// <summary>
            /// Represents the quantity of the product.
            /// </summary>
            Quantity,
        }
    }
}
