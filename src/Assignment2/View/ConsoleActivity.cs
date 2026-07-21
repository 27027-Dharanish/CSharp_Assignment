using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.View
{
    /// <summary>
    /// Perform console activity for shapes.
    /// </summary>
    internal class ConsoleActivity
    {
        /// <summary>
        /// Print the data in the console.
        /// </summary>
        /// <param name="data">the content to be printed in console</param>
        public void PrintInConsole(string? data)
        {
            Console.WriteLine(data);
        }

        /// <summary>
        /// Get the input from the console.
        /// </summary>
        /// <param name="content">content to get form the console</param>
        /// <returns>return the data got from the console</returns>
        public string? GetInputFromConsole(string? content)
        {
            Console.WriteLine("Enter the " + content + " : ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Print the invalid message in console
        /// </summary>
        public void PrintInvalid()
        {
            Console.WriteLine("Invalid Input!!");
        }

        /// <summary>
        /// Wait in console until user click any key
        /// </summary>
        public void WaitInConsole()
        {
            Console.WriteLine("Press any Key to continue!!");
            Console.ReadKey();
        }

        /// <summary>
        /// Clear the console
        /// </summary>
        public void ClearConsole()
        {
            Console.Clear();
        }
    }
}
