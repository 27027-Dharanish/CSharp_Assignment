using UtilityApp;

namespace DisplayApp
{
    /// <summary>
    /// Program is the entry class of the application.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            Terminator terminator = new Terminator();
            ConsoleWriter consoleWriter = new ConsoleWriter();

            consoleWriter.WriteColored("This is a success message\n", ConsoleColor.Green);
            consoleWriter.WriteColored("This is a warning message\n", ConsoleColor.Yellow);
            consoleWriter.WriteColored("This is an error message\n", ConsoleColor.Red);
            terminator.Terminate();
        }
    }
}