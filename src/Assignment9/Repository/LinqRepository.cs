using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment9.Core.Model;

namespace Assignment9.Repository
{
    /// <summary>
    /// Provides a centralized data repository for storing, retrieving product infos.
    /// </summary>
    public class LinqRepository
    {
        private List<Product> _products = new List<Product>
        {
            new Product(Guid.NewGuid(), "Pen", 20, "Stationery", 1),
            new Product(Guid.NewGuid(), "Notebook", 45, "Book", 1),
            new Product(Guid.NewGuid(), "Comic", 100, "Book", 1),
            new Product(Guid.NewGuid(), "Atomic habits", 300, "Book", 1),
            new Product(Guid.NewGuid(), "Rich dad poor dad", 180, "Book", 1),
            new Product(Guid.NewGuid(), "Desk Organizer", 150, "Office Supply", 2),
            new Product(Guid.NewGuid(), "Ergonomic Chair", 2500, "Office Supply", 2),
            new Product(Guid.NewGuid(), "Bottle", 100, "Items", 3),
            new Product(Guid.NewGuid(), "Coffee Mug", 35, "Kitchenware", 4),
            new Product(Guid.NewGuid(), "Toaster", 450, "Appliances", 5),
            new Product(Guid.NewGuid(), "Blender", 850, "Appliances", 5),
            new Product(Guid.NewGuid(), "Wireless Mouse", 120, "Electronics", 6),
            new Product(Guid.NewGuid(), "Mechanical Keyboard", 650, "Electronics", 6),
            new Product(Guid.NewGuid(), "Bluetooth Headphones", 1200, "Electronics", 6),
            new Product(Guid.NewGuid(), "Smart Watch", 3500, "Electronics", 6),
            new Product(Guid.NewGuid(), "Yoga Mat", 180, "Fitness", 7),
            new Product(Guid.NewGuid(), "Dumbbells", 750, "Fitness", 7),
            new Product(Guid.NewGuid(), "Backpack", 320, "Travel", 8),
        };

        private List<Supplier> _suppliers = new List<Supplier>
        {
            new Supplier(1, "Thor"),
            new Supplier(2, "Ram"),
            new Supplier(3, "Vijay"),
            new Supplier(4, "Virat"),
            new Supplier(5, "Samuel"),
            new Supplier(6, "Rajat"),
            new Supplier(7, "Rohit"),
            new Supplier(8, "Messi"),
        };

        private int[] _numbers = new int[] { 100, 200, 35, 25, 500, 300, 100 };

        /// <summary>
        /// Get the list of product available.
        /// </summary>
        /// <returns>List of product available.</returns>
        public List<Product> GetProductList()
        {
            return this._products;
        }

        /// <summary>
        /// Get teh list of supplier available.
        /// </summary>
        /// <returns>List of supplier.</returns>
        public List<Supplier> GetSuppliers()
        {
            return this._suppliers;
        }

        /// <summary>
        /// Get the list of numbers.
        /// </summary>
        /// <returns>List of numbers</returns>
        public int[] GetNumbers()
        {
            return this._numbers;
        }
    }
}
