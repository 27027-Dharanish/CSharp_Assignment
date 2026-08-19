namespace Assignment4
{
    /// <summary>
    /// Exception
    /// </summary>
    public class InvalidUserInputException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserInputException"/> class.
        /// Invalid user input exception.
        /// </summary>
        /// <param name="message">Brief note on the exception</param>
        public InvalidUserInputException(string? message)
            : base(message)
        {
        }
    }
}