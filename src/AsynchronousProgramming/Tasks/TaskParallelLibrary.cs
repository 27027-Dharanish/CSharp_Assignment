using System.Diagnostics;

namespace AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Contains the implementation of task parallel library.
    /// </summary>
    internal class TaskParallelLibrary
    {
        /// <summary>
        /// Squares the element in the library.
        /// </summary>
        public void CalculateSquare()
        {
            int[] integers = new int[10_000];
            for (int i = 0; i < integers.Length; i++)
            {
                integers[i] = i + 1;
            }

            long timeForSequentialSquaring = this.SequentialSquaring(integers);
            long timeForParallelSquaring = this.ParallelSquaring(integers);
            ConsoleActivity.PrintInConsole($"Time taken for sequential squaring: {timeForSequentialSquaring} ms\n" +
                $"Time taken for parallel squaring: {timeForParallelSquaring} ms");
        }

        private long SequentialSquaring(int[] integers)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (var value in integers)
            {
                ConsoleActivity.PrintInConsole($"{value}^2 = {value * value}");
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long ParallelSquaring(int[] integers)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Parallel.ForEach(integers, (value) =>
            {
                ConsoleActivity.PrintInConsole($"{value}^2 = {value * value}");
            });

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
