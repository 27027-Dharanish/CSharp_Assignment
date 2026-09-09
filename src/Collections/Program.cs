using Collections;
using Collections.View;

namespace Assignments
{
    /// <summary>
    /// Represents the main entry point for the application and handles initial setup.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the program and start the controller
        /// </summary>
        public static void Main()
        {
            TaskList list = new TaskList();
            list.HandleListOperation();
            Stack stack = new Stack();
            stack.HandleStackOperation();
            Queue queue = new Queue();
            queue.ExecuteQueueOperation();
            Dictionary dictionary = new Dictionary();
            dictionary.HandleDictionaryOperation();
        }
    }
}