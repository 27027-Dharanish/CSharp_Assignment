using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3.View
{
    /// <summary>
    /// Handles user interaction activities by managing standard input and output streams via the console.
    /// </summary>
    public static class ConsoleActivity
    {
        /// <summary>
        /// Print the content to the console.
        /// </summary>
        /// <param name="content">Content to be printed in console</param>
        public static void PrintInConsole(string? content)
        {
            Console.WriteLine(content);
        }

        /// <summary>
        /// Get the input from the user via console.
        /// </summary>
        /// <param name="inputToGet">The input user must enter</param>
        /// <returns>Return the data entered by the user</returns>
        public static string? GetInputFromConsole(string? inputToGet)
        {
            Console.WriteLine($"Enter the {inputToGet}");
            return Console.ReadLine();
        }
    }
}
