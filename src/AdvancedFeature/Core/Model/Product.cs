namespace AdvancedFeature.Core.Model
{
    /// <summary>
    /// Represents a product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="name">Name of the product.</param>
        /// <param name="category">Category of the product.</param>
        /// <param name="price">Price of the product.</param>
        public Product(string name, string category, decimal price)
        {
            this.ProductName = name;
            this.Category = category;
            this.Price = price;
        }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        /// <value>
        /// The product name.
        /// </value>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the product category.
        /// </summary>
        /// <value>
        /// The product category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        /// <value>
        /// The product price.
        /// </value>
        public decimal Price { get; set; }
    }
}
