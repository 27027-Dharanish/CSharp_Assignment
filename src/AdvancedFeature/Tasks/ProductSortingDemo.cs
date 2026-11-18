using AdvancedFeature.Core.Model;
using AdvancedFeature.View;

namespace AdvancedFeature.Tasks
{
    /// <summary>
    /// Demonstrates advanced use of custom delegates to control sorting behavior dynamically over a Product collection.
    /// </summary>
    public class ProductSortingDemo
    {
        private readonly List<Product> _products = new List<Product>()
        {
            new Product("Water bottle", "category", 300),
            new Product("Wireless Headphones", "Electronics", 1500),
            new Product("Mechanical Keyboard", "Electronics", 2500),
            new Product("USB-C Hub", "Electronics", 800),
            new Product("Power Bank", "Electronics", 1200),
            new Product("Yoga Mat", "Fitness", 600),
            new Product("Dumbbells Set", "Fitness", 1800),
            new Product("Running Shoes", "Fitness", 3500),
            new Product("Backpack", "Accessories", 2200),
            new Product("Coffee Mug", "Kitchenware", 450),
            new Product("Desk Organizer", "Office Supplies", 700),
        };

        /// <summary>
        /// Defines the signature rule required for custom product comparison.
        /// </summary>
        /// <param name="product1">The first product to evaluate.</param>
        /// <param name="product2">The second product to evaluate against.</param>
        /// <returns>A signed integer indicating the relative order of the objects (-1, 0, or 1).</returns>
        public delegate int SortDelegate(Product product1, Product product2);

        /// <summary>
        /// Sorts two products alphabetically based on their names.
        /// </summary>
        /// <param name="product1">The first product to compare.</param>
        /// <param name="product2">The second product to compare.</param>
        /// <returns>A signed integer representing the relative alphabetic order position.</returns>
        public int SortByName(Product product1, Product product2)
        {
            return string.Compare(product1.ProductName, product2.ProductName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sorts two products alphabetically based on their categories.
        /// </summary>
        /// <param name="product1">The first product to compare.</param>
        /// <param name="product2">The second product to compare.</param>
        /// <returns>A signed integer representing the relative category sequence position.</returns>
        public int SortByCategory(Product product1, Product product2)
        {
            return string.Compare(product1.Category, product2.Category, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sorts two products numerically based on their prices.
        /// </summary>
        /// <param name="product1">The first product to compare.</param>
        /// <param name="product2">The second product to compare.</param>
        /// <returns>A signed integer representing the relative ascending value position.</returns>
        public int SortByPrice(Product product1, Product product2)
        {
            return product1.Price.CompareTo(product2.Price);
        }

        /// <summary>
        /// Configures delegate instances and executes all three sorting strategies sequentially.
        /// </summary>
        public void ExecuteAdvancedDelegate()
        {
            ConsoleActivity.ShowHeader("Sorting of the product");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("--- Sorting By Name ---");
            ConsoleActivity.PrintEmptyLine();
            SortDelegate sortProduct = this.SortByName;
            this.SortAndDisplay(sortProduct, this._products);
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("--- Sorting By Category ---");
            ConsoleActivity.PrintEmptyLine();
            sortProduct = this.SortByCategory;
            this.SortAndDisplay(sortProduct, this._products);
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("--- Sorting By Price ---");
            ConsoleActivity.PrintEmptyLine();
            sortProduct = this.SortByPrice;
            this.SortAndDisplay(sortProduct, this._products);
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.WaitInConsole();
        }

        /// <summary>
        /// Sorts a local copy of the product list using the specified delegate rule and displays the ordered output in the console.
        /// </summary>
        /// <param name="sort">The structural custom delegate method reference used for comparison steps.</param>
        /// <param name="products">The data collection of products to be evaluated.</param>
        public void SortAndDisplay(SortDelegate sort, List<Product> products)
        {
            List<Product> temporaryList = new List<Product>(products);
            temporaryList.Sort((x, y) => sort(x, y));
            int i = 1;
            foreach (Product product in temporaryList)
            {
                ConsoleActivity.PrintInConsole($"{i++} . {product.ProductName} -- {product.Category} -- {product.Price}");
            }
        }
    }
}
