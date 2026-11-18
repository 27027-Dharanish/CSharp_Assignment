using System.Diagnostics;
using FileAndStream.View;

namespace FileAndStream
{
    /// <summary>
    /// Control and coordinate the overall action.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Start the execution flow and switch between tasks.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.<returns>
        public async Task Start()
        {
            MenuItems userInput;
            do
            {
                ConsoleActivity.ShowMenu("File and stream", new string[] { "Task 1", "Task 2", "Task 3", "Task 4", "Exit" });
                userInput = (MenuItems)ConsoleActivity.GetIntegerInput("option");
                switch (userInput)
                {
                    case MenuItems.Task1:
                        this.RunSynchronousProcess();
                        break;
                    case MenuItems.Task2:
                        this.RunConcurrentProcessing();
                        break;
                    case MenuItems.Task3:
                        OptimizedFileProcessor task = new OptimizedFileProcessor();
                        task.HandleTask3();
                        break;
                    case MenuItems.Task4:
                        await this.RunLogger();
                        break;
                    case MenuItems.Exit:
                        break;
                    default:
                        ConsoleActivity.PrintAndWait("Invalid choice..");
                        break;
                }
            }
            while (userInput != MenuItems.Exit);
        }

        /// <summary>
        /// Synchronous wrapper method that runs and times the asynchronous file processing.
        /// </summary>
        public void RunConcurrentProcessing()
        {
            ConsoleActivity.ShowHeader("Task 2: Concurrent Async Processing");
            ConsoleActivity.PrintInConsole("Initializing parallel task execution wrapper...");

            FileProcessorAsync processor = new FileProcessorAsync();
            Stopwatch watch = new Stopwatch();

            watch.Start();
            try
            {
                processor.ProcessMultipleFilesConcurrentlyAsync().GetAwaiter().GetResult();
                watch.Stop();

                ConsoleActivity.PrintInConsole("\n==================================================");
                ConsoleActivity.PrintInConsole("All concurrent tasks have successfully completed!");
                ConsoleActivity.PrintAndWait($"Total processing time for all 3 files: {watch.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                watch.Stop();
                ConsoleActivity.PrintAndWait($"An error occurred during execution: {ex.Message}");
            }
        }

        /// <summary>
        /// File data processor process one file at a time.
        /// </summary>
        public void RunSynchronousProcess()
        {
            FileProcessor task = new FileProcessor();
            task.GenerateOneGBFile();
            task.ReadFileUsingFileStream();
            task.ReadFileUsingBufferedStream();
            task.ConvertUpperCase();
        }

        /// <summary>
        /// Execute the logger operation with multiple user.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task RunLogger()
        {
            ConsoleActivity.ShowHeader("Multi-user log simulation");
            int userCount = 20;
            int logsPerUser = 15;
            ConsoleActivity.PrintInConsole($"[Test 1] Starting initial shared logging system...");
            Stopwatch stopWatch = Stopwatch.StartNew();
            List<Task> sharedTasks = new List<Task>();
            for (int i = 1; i <= userCount; i++)
            {
                string userId = $"User_{i}";
                sharedTasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < logsPerUser; j++)
                    {
                        ThreadSafeLogger.LogError($"{userId} - connection timeout.");
                    }
                }));
            }

            await Task.WhenAll(sharedTasks);
            stopWatch.Stop();
            ConsoleActivity.PrintInConsole($"[Result] Initial system finished in: {stopWatch.ElapsedMilliseconds} ms\n");
            ConsoleActivity.PrintInConsole($"[Test 2] Starting improved independent file logging system...");
            stopWatch.Reset();
            stopWatch.Start();
            List<Task> independentTasks = new List<Task>();
            for (int i = 1; i <= userCount; i++)
            {
                string userId = $"User_{i}";
                independentTasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < logsPerUser; j++)
                    {
                        ThreadSafeLogger.LogErrorIndependent(userId, $"{userId} - connection timeout.");
                    }
                }));
            }

            await Task.WhenAll(independentTasks);
            stopWatch.Stop();
            ConsoleActivity.PrintAndWait($"[Result] Improved system finished in: {stopWatch.ElapsedMilliseconds} ms\n");
        }
    }
}
