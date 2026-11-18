namespace AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Contains the method for demonstrating and preventing the deadlock condition.
    /// </summary>
    public class DeadlockDebugger
    {
        /// <summary>
        /// Executes an asynchronous operation and displays its result.
        /// </summary>
        /// <returns>A task representing the asynchronous execution of the methods.</returns>
        public async Task DeadlockMethod()
        {
            string result = await this.SomeAsyncOperation();
            ConsoleActivity.PrintInConsole(result);
        }

        /// <summary>
        /// Performs a asynchronous operation that completes after one second.
        /// </summary>
        /// <returns>A task containing the string result of the asynchronous operation.</returns>
        private async Task<string> SomeAsyncOperation()
        {
            await Task.Delay(1000);
            return "Hello, World!";
        }
    }
}
