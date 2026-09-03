namespace Assignment9.Core.Model
{
    /// <summary>
    /// Serves as the foundational entity for all supplier record.
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Supplier"/> class.
        /// </summary>
        /// <param name="id">Supplier id.</param>
        /// <param name="name">Supplier name.</param>
        public Supplier(int id, string name)
        {
            this.SupplierId = id;
            this.SupplierName = name;
        }

        /// <summary>
        /// Gets or sets the supplier id.
        /// </summary>
        /// <value>
        /// A int representing supplier id.
        /// </value>
        public int SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the supplier name.
        /// </summary>
        /// <value>
        /// A string representing supplier name.
        /// </value>
        public string SupplierName { get; set; }
    }
}
