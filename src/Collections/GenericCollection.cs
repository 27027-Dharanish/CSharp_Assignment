using Collections.View;

namespace Collections
{
    internal class GenericCollection
    {
        /// <summary>
        /// Handle it.
        /// </summary>
        /// <typeparam name="T">t</typeparam>
        public class Stack<T>
        {
            private Stack<T> _stack = new Stack<T>();

            /// <summary>
            /// Perform the list of operation available in the stack.
            /// </summary>
            public void HandleStackOperation()
            {
                ConsoleActivity.ShowHeader("Task 2 - Stack");
                ConsoleActivity.PrintEmptyLine();
                string? userInput = ConsoleActivity.GetStringInput("input");
                if (string.IsNullOrEmpty(userInput))
                {
                    ConsoleActivity.PrintAndWait("Invalid input");
                    return;
                }

                foreach (var c in userInput)
                {
                    this._stack.Push(c);
                }

                string reverse = string.Empty;
                while (this._stack.Count != 0)
                {
                    reverse += this._stack.Pop();
                }

                ConsoleActivity.PrintEmptyLine();
                ConsoleActivity.PrintInConsole("Original string : " + userInput);
                ConsoleActivity.PrintAndWait("Reversed string : " + reverse);
            }
        }
    }
}
