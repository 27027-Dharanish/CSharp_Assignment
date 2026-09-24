using Assignment4.controller;

namespace Assignments
{
    /// <summary>
    /// Represents the main entry point for the error handler.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Start the error handler.
        /// </summary>
        public static void Main()
        {
            ErrorHandlerController errorHandleController = new ErrorHandlerController();
            errorHandleController.Start();
        }
    }
}
