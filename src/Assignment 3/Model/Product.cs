using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3.Model
{
    /// <summary>
    /// give the summary for the inventory management
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="id">Id of the new product</param>
        public Product(string? id)
        {
            this.ProductId = id;
        }

        /// <summary>
        /// Gets the id for the product
        /// </summary>
        /// <value>
        /// A string representing id for the product
        /// </value>
        public string? ProductId { get; init; }

        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        /// <value>
        /// A string representing product name
        /// </value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the price of product
        /// </summary>
        /// <value>
        /// A decimal representing price of product
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product
        /// </summary>
        /// <value>
        /// A integer value representing quantity of the product
        /// </value>
        public int Quantity { get; set; }
    }
}
