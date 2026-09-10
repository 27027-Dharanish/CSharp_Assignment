namespace MemoryOptimization
{
    /// <summary>
    /// Demonstrates memory allocation and release for memory usage analysis.
    /// </summary>
    public class OptimizedMemoryEater
        : IDisposable
    {
        private List<int[]>? _memAlloc;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptimizedMemoryEater"/> class.
        /// </summary>
        public OptimizedMemoryEater()
        {
            this._memAlloc = new List<int[]>();
            this._isDisposed = false;
        }

        /// <summary>
        /// Allocates the memory and releases their references.
        /// </summary>
        public void Allocate()
        {
            if (this._isDisposed)
            {
                Console.WriteLine("The class already got disposed!!");
            }
            else if (this._memAlloc != null)
            {
                int count = 0;
                while (count < 10)
                {
                    this._memAlloc.Add(new int[1000]);
                    count++;
                    double allocatedKb = GC.GetAllocatedBytesForCurrentThread() / 1024.0;
                    Console.WriteLine($"Current memory allocated: {allocatedKb:F2} Kb");
                }
            }
        }

        /// <summary>
        /// Releases the internal memory references, allowing the GC to reclaim them.
        /// </summary>
        public void Dispose()
        {
            this._memAlloc = null;
            this._isDisposed = true;
        }
    }
}
