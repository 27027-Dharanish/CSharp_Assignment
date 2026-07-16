using System.Runtime.CompilerServices;
using Assignment1.Persistence;
using Assignment1.Services;

namespace Assignments
{
    /// <summary>
    /// First assignment
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Program function
        /// </summary>
        /// <param name="args">Welcome</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Contact Manager");
            ConsoleActivity activity = new ConsoleActivity();
            activity.ShowOption();
        }
    }
}