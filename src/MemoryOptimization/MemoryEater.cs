using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int count = 0;
            while (count < 10)
            {
                this._memAlloc.Add(new int[1000]);
                count++;
                Thread.Sleep(10);
            }
        }
    }
}
