using MemoryOptimization;

namespace Assignments
{
    /// <summary>
    /// Serves as the entry point for the memory management understanding assignment.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Creates a MemoryEater instance and starts the memory.
        /// </summary>
        public static void Main()
        {
            MemoryEater me = new MemoryEater();
            me.Allocate();
            OptimizedMemoryEater optimized = new OptimizedMemoryEater();
            optimized.Allocate();
        }
    }
}