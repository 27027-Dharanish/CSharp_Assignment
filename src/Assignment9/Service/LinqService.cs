using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment9.Core.Model;
using Assignment9.Repository;

namespace Assignment9.Service
{
    /// <summary>
    /// Defines the business logic contract for the linq tasks.
    /// </summary>
    public class LinqService
    {
        private LinqRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinqService"/> class.
        /// </summary>
        /// <param name="repository">The repository for storing and retrieving things.</param>
        public LinqService(LinqRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Get the list of all product from the repository.
        /// </summary>
        /// <returns>List of product.</returns>
        public List<Product> GetAllProduct()
        {
            return this._repository.GetProductList();
        }

        /// <summary>
        /// Get the product details under electronics category and price grater than $500.
        /// </summary>
        /// <param name="averagePrice">The average price of the filtered product.</param>
        /// <returns>Tuple of name and price of the product</returns>
        public (string Name, decimal Price)[] GetProductUnderElectronics(out decimal averagePrice)
        {
            List<Product> products = this._repository.GetProductList();
            var filteredProducts = products
                .Where(product => product.Category == "Electronics")
                .Where(product => product.ProductPrice >= 500)
                .Select(product => (product.ProductName, product.ProductPrice))
                .OrderByDescending(product => product.ProductPrice)
                .ToArray();
            averagePrice = filteredProducts
                .Average(product => product.ProductPrice);
            return filteredProducts;
        }

        /// <summary>
        /// Group the product by category, count, maximum product name and its price.
        /// </summary>
        /// <returns>Filtered product category, count and its product details.</returns>
        public (string category, int count, string productName, decimal price)[] GroupProductByCategory()
        {
            List<Product> products = this._repository.GetProductList();
            var filteredProduct = products
                .GroupBy(product => product.Category)
                .Select(group => (
                    group.Key,
                    group.Count(),
                    group.MaxBy(product => product.ProductPrice) !.ProductName,
                    group.Max(product => product.ProductPrice)))
                .ToArray();
            return filteredProduct;
        }

        /// <summary>
        /// Get the product and product supplier mapped list.
        /// </summary>
        /// <returns>List of product and its supplier.</returns>
        public (int, string, string)[] MatchProductAndSupplier()
        {
            List<Supplier> suppliers = this._repository.GetSuppliers();
            List<Product> products = this._repository.GetProductList();
            return products
                .Join(
                    suppliers,
                    product => product.SupplierId,
                    supplier => supplier.SupplierId,
                    (product, supplier) => (
                        supplier.SupplierId,
                        supplier.SupplierName,
                        product.ProductName))
                .ToArray();
        }

        /// <summary>
        /// Get the array of numbers.
        /// </summary>
        /// <returns>The array of numbers.</returns>
        public int[] GetArrayOfNumbers()
        {
            return this._repository.GetNumbers();
        }

        /// <summary>
        /// Get the second largest element from the array.
        /// </summary>
        /// <returns>Second largest number.</returns>
        public int GetSecondLargestNumber()
        {
            int[] numbers = this._repository.GetNumbers();
            return numbers
                .Distinct()
                .OrderByDescending(number => number)
                .Skip(1)
                .FirstOrDefault();
        }

        /// <summary>
        /// Get list of product of category books.
        /// </summary>
        /// <returns>List of product.</returns>
        public List<Product> GetBookProducts()
        {
            List<Product> products = this._repository.GetProductList();
            return products
                .Where(product => product.Category == "Book")
                .ToList();
        }

        /// <summary>
        /// Sort the product by the price.
        /// </summary>
        /// <returns>List of sorted product.</returns>
        public List<Product> SortBookByPrice()
        {
            List<Product> bookProduct = this.GetBookProducts();
            return bookProduct
                .OrderBy(product => product.ProductPrice)
                .ToList();
        }

        /// <summary>
        /// Get product under book category and sort by its price.
        /// </summary>
        /// <returns>List of product.</returns>
        public List<Product> OptimizedSortBookByPrice()
        {
            List<Product> products = this._repository.GetProductList();
            return products
                .Where(product => product.Category == "Book")
                .OrderBy(product => product.ProductPrice)
                .ToList();
        }

        /// <summary>
        /// Get the pair of number which its sum equal to the target number.
        /// </summary>
        /// <param name="target">The target number.</param>
        /// <returns>List of pair of numbers.</returns>
        public (int, int)[] GetPairNumberMatchTarget(int target)
        {
            int[] numbers = this._repository.GetNumbers();
            return numbers.SelectMany((num1, index1) => numbers
            .Where((num2, index2) => index2 > index1 && num1 + num2 == target)
            .Select(num2 => (num1, num2))).Distinct().ToArray();
        }
    }
}
