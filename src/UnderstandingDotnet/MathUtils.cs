namespace UnderstandingDotnet
{
    /// <summary>
    /// Provide the mathematical method for calculator operation.
    /// </summary>
    public class MathUtils
    {
        /// <summary>
        /// Add two integer numbers.
        /// </summary>
        /// <param name="x">First integer number.</param>
        /// <param name="y">Second integer number.</param>
        /// <returns>Addition result.</returns>
        public int Add(int x, int y) => x + y;

        /// <summary>
        /// Subtract two integer numbers.
        /// </summary>
        /// <param name="x">First integer number.</param>
        /// <param name="y">Second integer number.</param>
        /// <returns>Subtraction result.</returns>
        public int Subtract(int x, int y) => x - y;

        /// <summary>
        /// Multiply two integer numbers.
        /// </summary>
        /// <param name="x">First integer number.</param>
        /// <param name="y">Second integer number.</param>
        /// <returns>Multiplication result.</returns>
        public int Multiply(int x, int y) => x * y;

        /// <summary>
        /// Divide two integer numbers.
        /// </summary>
        /// <param name="x">First integer number.</param>
        /// <param name="y">Second integer number.</param>
        /// <returns>Division result.</returns>
        public double Divide(int x, int y) => x / y;
    }
}
