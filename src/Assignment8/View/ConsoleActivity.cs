namespace Assignment4.View;

/// <summary>
/// Handles user interaction activities by managing standard input and output streams via the console.
/// </summary>
public class ConsoleActivity
{
    /// <summary>
    /// Print the given content in console.
    /// </summary>
    /// <param name="content">Content to be printed in console</param>
    public static void PrintInConsole(string? content)
    {
        Console.WriteLine(content);
    }

    /// <summary>
    /// Get input form the console.
    /// </summary>
    /// <param name="field">Field that require input</param>
    /// <returns>Input from the user</returns>
    public static string GetInputFromUser(string? field)
    {
        Console.Write($"Enter the {field} : ");
        return Console.ReadLine() ?? string.Empty;
    }

    /// <summary>
    /// Clear the console.
    /// </summary>
    public static void ClearConsole()
    {
        Console.Clear();
    }

    /// <summary>
    /// Show the header.
    /// </summary>
    /// <param name="header">Error handler header</param>
    public static void ShowHeader(string? header)
    {
        ClearConsole();
        PrintInConsole(new string('=', 40));
        PrintInConsole($"             {header}");
        PrintInConsole(new string('=', 40));
    }

    /// <summary>
    /// Print the menu item in console.
    /// </summary>
    /// <param name="items">Menu item</param>
    public static void PrintMenu(string[] items)
    {
        int i = 0;
        foreach (string item in items)
        {
            PrintInConsole($"{++i}. {item}");
        }
    }

    /// <summary>
    /// Print empty line.
    /// </summary>
    public static void PrintEmptyLine()
    {
        Console.WriteLine();
    }

    /// <summary>
    /// Print and wait in console.
    /// </summary>
    /// <param name="content">Content to be printed in console</param>
    public static void PrintAndWaitInConsole(string? content)
    {
        PrintEmptyLine();
        PrintInConsole(content);
        PrintInConsole("Press any key to continue...");
        Console.ReadKey();
    }
}