namespace MathApp
{
    /// <summary>
    /// Performs arithmetic operations.
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Adds two numbers and returns their result.
        /// </summary>
        /// <param name="firstNumber">First number to be added.</param>
        /// <param name="secondNumber">Second number to be added.</param>
        /// <returns>Returns the addition result.</returns>
        public int Add(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }

        /// <summary>
        /// Subtracts two numbers and returns their result.
        /// </summary>
        /// <param name="firstNumber">Minuend of the subtraction.</param>
        /// <param name="secondNumber">Subtrahend of the subtraction.</param>
        /// <returns>Returns the subtraction result.</returns>
        public int Subtract(int firstNumber, int secondNumber)
        {
            return firstNumber - secondNumber;
        }

        /// <summary>
        /// Multiplies two numbers and returns their result.
        /// </summary>
        /// <param name="firstNumber">First number to be multiplied.</param>
        /// <param name="secondNumber">Second number to be multiplied.</param>
        /// <returns>Returns the multiplication result.</returns>
        public int Multiply(int firstNumber, int secondNumber)
        {
            return firstNumber * secondNumber;
        }

        /// <summary>
        /// Divides two numbers and returns their result.
        /// </summary>
        /// <param name="firstNumber">Dividend of the division.</param>
        /// <param name="secondNumber">Divisor of the division.</param>
        /// <returns>Returns the division result.</returns>
        public int Divide(int firstNumber, int secondNumber)
        {
            return firstNumber / secondNumber;
        }
    }
}
