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
        public void Start()
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
                        Task3 task = new Task3();
                        task.HandleTask3();
                        break;
                    case MenuItems.Task4:
                        ThreadSafeLogger log = new ThreadSafeLogger();
                        log.LogError("Error logged");
                        ConsoleActivity.PrintAndWait("Error logged successfully..");
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

            Task2 processor = new Task2();
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
            Task1 task = new Task1();
            task.GenerateOneGBFile();
            task.ReadFileUsingFileStream();
            task.ReadFileUsingBufferedStream();
            task.ConvertUpperCase();
        }
    }
}
