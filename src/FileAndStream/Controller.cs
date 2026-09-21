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
            ConsoleActivity.ShowHeader("Multi-user log simulation...");
            List<Task> userTasks = new List<Task>();
            for (int i = 1; i <= 5; i++)
            {
                string userId = $"User_{i}";
                Task userTask = Task.Run(() =>
                {
                    Console.WriteLine($"[Thread Active] {userId} is attempting to write a log...");
                    ThreadSafeLogger.LogError($"{userId} - connection timeout.");
                });

                userTasks.Add(userTask);
            }

            await Task.WhenAll(userTasks);
            Console.WriteLine("\nAll users finished logging!");
            ConsoleActivity.WaitInConsole();
        }
    }
}
