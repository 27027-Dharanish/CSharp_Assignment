using Collections.View;

namespace Collections
{
    /// <summary>
    /// Handles generic operation.
    /// </summary>
    public static class GenericCollection
    {
        /// <summary>
        /// Perform the list of generic operation on task 6.
        /// </summary>
        public static void Operation()
        {
            ConsoleActivity.ShowHeader("Task 6.1: IEnumerable Reusability");
            int[] arrayData = { 10, 20, 30 };
            int arraySum = SumOfElements(arrayData);
            ConsoleActivity.PrintInConsole($"Sum of Array elements: {arraySum}");
            List<int> listData = new List<int> { 5, 15, 25, 35 };
            int listSum = SumOfElements(listData);
            ConsoleActivity.PrintInConsole($"Sum of List elements: {listSum}");
            Queue<int> queueData = new Queue<int>();
            queueData.Enqueue(1);
            queueData.Enqueue(2);
            queueData.Enqueue(3);
            int queueSum = SumOfElements(queueData);
            ConsoleActivity.PrintInConsole($"Sum of Queue elements: {queueSum}");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.ShowHeader("Task 6.2: IReadOnlyDictionary Immutability");
            IReadOnlyDictionary<string, int> myDict = GenerateDictionary();
            PrintDictionary(myDict);
            ConsoleActivity.PrintInConsole("\n[Compiler Test] Attempting to modify the read-only dictionary...");
            ConsoleActivity.PrintAndWait("Result: Compilation fails natively! Read-only interfaces hide the setter indexer completely.");
        }

        /// <summary>
        /// Calculate the sum of element given in collections.
        /// </summary>
        /// <param name="elements">Element that used to calculate sum.</param>
        /// <returns>Sum of elements</returns>
        public static int SumOfElements(IEnumerable<int> elements)
        {
            int sum = 0;
            foreach (int num in elements)
            {
                sum += num;
            }

            return sum;
        }

        /// <summary>
        /// Create a readonly dictionary and return it.
        /// </summary>
        /// <returns>IReadOnly dictionary.</returns>
        public static IReadOnlyDictionary<string, int> GenerateDictionary()
        {
            Dictionary<string, int> internalDict = new Dictionary<string, int>
            {
                { "Apple", 5 },
                { "Banana", 12 },
                { "Orange", 7 },
            };

            return internalDict;
        }

        /// <summary>
        /// Print the given dictionary in console.
        /// </summary>
        /// <param name="dictionary">Dictionary that needed to be printed on console.</param>
        public static void PrintDictionary(IReadOnlyDictionary<string, int> dictionary)
        {
            Console.WriteLine("Dictionary Elements:");
            foreach (var element in dictionary)
            {
                Console.WriteLine($"{element.Key} -> {element.Value}");
            }
        }
    }
}
