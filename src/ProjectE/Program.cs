using DisplayApp;

namespace Assignments;

/// <summary>
/// Program is the entry class of the application.
/// </summary>
public class Program
{
    /// <summary>
    /// Starts and execute the application.
    /// </summary>
    public static void Main()
    {
        ConsoleWriter consoleWriter = new ConsoleWriter();
        consoleWriter.WriteColored("PROJECT E (WILL BE BUILD AFTER PROJECT C-DISPLAY APP)", ConsoleColor.White);
        consoleWriter.WriteColored("\nDisplaying message from project E", ConsoleColor.Green);
        Console.ReadKey();
    }
}