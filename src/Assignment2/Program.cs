using Assignment2.Controller;

namespace Assignments
{
    /// <summary>
    /// Represents the main entry point for the application and handles initial setup.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main entry point of the program
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Hello, World!");
            ShapeController shape = new ShapeController();
            shape.StartShapeContorller();
        }
    }
}