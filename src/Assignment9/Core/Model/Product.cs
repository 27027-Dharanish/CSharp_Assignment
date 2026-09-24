namespace Assignment9.Core.Model
{
    /// <summary>
    /// Serves as the foundational entity for all product record.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <param name="name">Name of the product.</param>
        /// <param name="price">Price of the product.</param>
        /// <param name="category">Category of the product.</param>
        /// <param name="supplierId">Supplier id of the product.</param>
        public Product(Guid id, string name, decimal price, string category, int supplierId)
        {
            this.ProductId = id;
            this.ProductName = name;
            this.ProductPrice = price;
            this.Category = category;
            this.SupplierId = supplierId;
        }

        /// <summary>
        /// Gets or sets the id for the product.
        /// </summary>
        /// <value>
        /// The product id.
        /// </value>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// A string representing the product name.
        /// </value>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the id for the product.
        /// </summary>
        /// <value>
        /// A decimal representing price of the product.
        /// </value>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Gets or sets the category for the product.
        /// </summary>
        /// <value>
        /// A string representing category of the product.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the product supplier id.
        /// </summary>
        /// <value>
        /// A int representing product supplier id.
        /// </value>
        public int SupplierId { get; set; }
    }
}
