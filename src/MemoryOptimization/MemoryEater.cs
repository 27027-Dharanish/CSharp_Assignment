namespace MemoryOptimization
{
    /// <summary>
    /// Demonstrates memory allocation and release for memory usage analysis.
    /// </summary>
    public class MemoryEater
    {
        private readonly List<int[]> _memAlloc = new List<int[]>();

        /// <summary>
        /// Allocates the memory and releases their references.
        /// </summary>
        public void Allocate()
        {
            while (true)
            {
                this._memAlloc.Add(new int[1000]);
                Thread.Sleep(10);
            }
        }
    }
}
