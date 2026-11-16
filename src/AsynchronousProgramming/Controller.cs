using AsynchronousProgramming.Tasks;

namespace AsynchronousProgramming
{
    /// <summary>
    /// Controls the flow between the different tasks.
    /// </summary>
    public class Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Controls the flow between the different tasks.
        /// </summary>
        /// <returns>A asynchronous task representing the operation to get user input.</returns>
        public async Task Run()
        {
            string menuMessage = "     Asynchronous Programming        \n" +
                "1. Implementing Async/Await\n" +
                "2. Understanding Task Parallel Library\n" +
                "3. Multi-Threading\n" +
                "4. Multi-Layered Async/Await Operations\n" +
                "5. Fixing Deadlock Conditions\n" +
                "6. ConfigureAwait\n" +
                "7. Difference between Async Void and Async Task with Exceptions\n" +
                "8. Exit\n";

            while (true)
            {
                try
                {
                    ConsoleActivity.PrintInConsole(menuMessage);
                    int option = ConsoleActivity.GetIntegerInput("Enter the option :");

                    switch (option)
                    {
                        case 1:
                            AsyncAwait httpClient = new AsyncAwait(this._httpClient);
                            string content = await httpClient.DownloadContentAsync();
                            ConsoleActivity.PrintInConsole(content);
                            break;

                        case 2:
                            TaskParallelLibrary taskParallelLibrary = new TaskParallelLibrary();
                            taskParallelLibrary.CalculateSquare();
                            break;

                        case 3:
                            MultiThreading multiThreading = new MultiThreading();
                            multiThreading.CalculateAverage();
                            break;

                        case 4:
                            MultiLayeredAsync multiLayeredAsync = new MultiLayeredAsync(this._httpClient);
                            int result = await multiLayeredAsync.MethodC();
                            ConsoleActivity.PrintInConsole("Number of key-value pairs: " + result);
                            break;

                        case 5:
                            DeadlockDebugger deadlockDebugger = new DeadlockDebugger();
                            await deadlockDebugger.DeadlockMethod();
                            break;

                        case 6:
                            ConfigureAwait configureAwait = new ConfigureAwait();
                            await configureAwait.Run();
                            break;

                        case 7:
                            await Task7();
                            break;

                        case 8:
                            return;

                        default:
                            ConsoleActivity.PrintInConsole("Enter a valid option");
                            break;
                    }

                    ConsoleActivity.WaitInConsole();
                }
                catch (Exception ex)
                {
                    ConsoleActivity.PrintInConsole(ex.ToString());
                }
            }
        }

        private static async Task Task7()
        {
            ErrorHandling errorHandling = new ErrorHandling();

            try
            {
                await errorHandling.TaskMethod();
            }
            catch (Exception)
            {
                ConsoleActivity.PrintInConsole("Exception is handled");
            }

            try
            {
                errorHandling.VoidMethod();
            }
            catch (Exception)
            {
                ConsoleActivity.PrintInConsole("Exception is unhandled");
            }
        }
    }
}
