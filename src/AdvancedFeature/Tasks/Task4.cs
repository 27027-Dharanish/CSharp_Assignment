using System.Linq;
using AdvancedFeature.View;

namespace AdvancedFeature.Tasks
{
    /// <summary>
    /// Understanding and using of lambda statement and expression.
    /// </summary>
    public class Task4
    {
        /// <summary>
        /// Execute the operation using the lambda expressions and statements.
        /// </summary>
        public void ExecuteLambda()
        {
            ConsoleActivity.ShowHeader("Lambda Expression");
            ConsoleActivity.PrintEmptyLine();
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            ConsoleActivity.PrintInConsole($"The original list element are : {string.Join(", ", numbers)}");
            var evenNumbers = numbers.Where(number => number % 2 == 0);
            ConsoleActivity.PrintInConsole($"The even number present in the list are : {string.Join(", ", evenNumbers)}");
            var oddNumbers = numbers.Where(number => number % 2 != 0);
            var squaredOddNumbers = oddNumbers.Select(number =>
            {
                return number * number;
            });
            ConsoleActivity.PrintAndWait($"The square of odd number present in the list : {string.Join(", ", squaredOddNumbers)}");
        }
    }
}
