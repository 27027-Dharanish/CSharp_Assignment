namespace MemoryOptimization
{
    /// <summary>
    /// Demonstrates memory allocation and release for memory usage analysis.
    /// </summary>
    public class OptimizedMemoryEater
    {
        /// <summary>
        /// Allocates the memory and releases their references.
        /// </summary>
        /// <param name="token">Cancelation token</param>
        public void Allocate(CancellationToken token = default)
        {
            while (!token.IsCancellationRequested)
            {
                int[] buffer = new int[1000];
                double allocatedKb = GC.GetTotalMemory(false) / 1024.0;
                Console.WriteLine($"Current memory allocated: {allocatedKb:F2} Kb");
            }
        }
    }
}
