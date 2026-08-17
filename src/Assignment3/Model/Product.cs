using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3.Model
{
    /// <summary>
    /// Represents a product available within the inventory management system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="id">Id of the new product</param>
        /// <param name="name">Name of the new product</param>
        /// <param name="price">Price of the new product</param>
        /// <param name="quantity">Quantity of the new product</param>
        public Product(string? id, string? name, decimal price, int quantity)
        {
            this.ProductId = id;
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
        }

        /// <summary>
        /// Gets the id for the product.
        /// </summary>
        /// <value>
        /// A string representing id for the product
        /// </value>
        public string? ProductId { get; init; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        /// <value>
        /// A string representing product name
        /// </value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the price of product.
        /// </summary>
        /// <value>
        /// A decimal representing price of product
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        /// <value>
        /// A integer value representing quantity of the product
        /// </value>
        public int Quantity { get; set; }
    }
}
