namespace AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Contains the implementation of multi threading.
    /// </summary>
    public class MultiThreading
    {
        private int[] _integerArray = new int[10000];
        private int _sum = 0;
        private int _count = 0;

        /// <summary>
        /// Calculates the average value of array.
        /// </summary>
        public void CalculateAverage()
        {
            for (int i = 0; i < this._integerArray.Length; i++)
            {
                this._integerArray[i] = i + 1;
            }

            Thread calculateSum = new Thread(this.CalculateSum);
            Thread calculateCount = new Thread(this.CalculateCount);
            calculateSum.Start();
            calculateCount.Start();
            calculateSum.Join();
            calculateCount.Join();
            Console.WriteLine("Average value: " + ((double)this._sum / this._count));
        }

        private void CalculateSum()
        {
            int sum = 0;
            foreach (var item in this._integerArray)
            {
                sum += item;
            }

            this._sum = sum;
        }

        private void CalculateCount()
        {
            int count = 0;
            foreach (var item in this._integerArray)
            {
                count++;
            }

            this._count = count;
        }
    }
}
