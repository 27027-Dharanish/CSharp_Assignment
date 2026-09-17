using AdvancedFeature.View;

namespace AdvancedFeature.Tasks
{
    /// <summary>
    /// Understand and implement sorting with anonymous methods.
    /// </summary>
    public class Task3
    {
        /// <summary>
        /// Entry method to demonstrate sorting an array using an anonymous method.
        /// </summary>
        public void ExecuteSorting()
        {
            ConsoleActivity.ShowHeader("Sort numbers");
            ConsoleActivity.PrintEmptyLine();
            int[] numbers = { 56, 12, 89, 4, 42, 23 };
            ConsoleActivity.PrintInConsole("Original Array: " + string.Join(", ", numbers));
            Array.Sort(numbers, delegate(int x, int y)
            {
                return x.CompareTo(y);
            });

            ConsoleActivity.PrintAndWait("Sorted Array (Ascending): " + string.Join(", ", numbers));
        }
    }
}
