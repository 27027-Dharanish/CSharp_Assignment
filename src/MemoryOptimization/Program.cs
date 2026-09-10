namespace MemoryOptimization
{
    /// <summary>
    /// Serves as the entry point for the memory management understanding assignment.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Creates instances of both standard and optimized memory consumers and triggers their respective memory allocation routines.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("---- Starting Testing ----");
            Console.WriteLine();
            Console.WriteLine("Testing optimized memory eater : ");
            using (OptimizedMemoryEater memoryModifier = new OptimizedMemoryEater())
            {
                memoryModifier.Allocate();
            }

            Console.ReadKey();
        }
    }
}
