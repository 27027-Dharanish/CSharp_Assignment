using Collections.View;

namespace Collections
{
    /// <summary>
    /// Handle task related to stack.
    /// </summary>
    public class Stack
    {
        private Stack<char> _stack = new Stack<char>();

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

            foreach (char c in userInput)
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
