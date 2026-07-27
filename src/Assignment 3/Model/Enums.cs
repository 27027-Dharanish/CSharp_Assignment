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
    internal class Enums
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
    }
}
