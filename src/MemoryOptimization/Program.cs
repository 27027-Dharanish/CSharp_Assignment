namespace MemoryOptimization
{
    /// <summary>
    /// Serves as the entry point for the memory management understanding assignment.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Creates instances of optimized memory consumers and triggers their respective memory allocation routines.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("---- Starting Testing ----");
            Console.WriteLine();
            Console.WriteLine("Testing optimized memory eater : ");
            OptimizedMemoryEater memoryModifier = new OptimizedMemoryEater();
            using CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            memoryModifier.Allocate(cts.Token);
            Console.ReadKey();
        }
    }
}
