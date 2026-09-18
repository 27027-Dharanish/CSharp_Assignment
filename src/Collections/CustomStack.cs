using Collections.View;

namespace Collections
{
    /// <summary>
    /// Handles tasks related to a generic stack and performs operations on it.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack.</typeparam>
    public class CustomStack<T>
    {
        // Explicitly naming the namespace avoids conflicts if your class is named Stack
        private System.Collections.Generic.Stack<T> _stack = new System.Collections.Generic.Stack<T>();

        /// <summary>
        /// Pushes a new item onto the top of the stack.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        public void PushItem(T item)
        {
            this._stack.Push(item);
            ConsoleActivity.PrintInConsole($"Pushed: {item}");
        }

        /// <summary>
        /// Pops and returns the top item from the stack.
        /// </summary>
        public void PopItem()
        {
            if (this._stack.Count > 0)
            {
                T poppedItem = this._stack.Pop();
                ConsoleActivity.PrintAndWait($"Popped item: {poppedItem}");
                return;
            }

            ConsoleActivity.PrintAndWait("The stack is empty! Nothing to pop.");
        }

        /// <summary>
        /// Reverses a given collection of items using the generic stack and displays the result.
        /// </summary>
        /// <param name="items">The collection of elements to reverse.</param>
        public void ReverseAndDisplay(IEnumerable<T> items)
        {
            ConsoleActivity.ShowHeader("Stack - Reverse word");
            ConsoleActivity.PrintEmptyLine();

            if (items == null)
            {
                ConsoleActivity.PrintAndWait("Invalid input: The collection is null.");
                return;
            }

            foreach (T item in items)
            {
                this._stack.Push(item);
            }

            List<T> reversedList = new List<T>();
            while (this._stack.Count != 0)
            {
                reversedList.Add(this._stack.Pop());
            }

            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Original order : " + string.Join(string.Empty, items));
            ConsoleActivity.PrintAndWait("Reversed order : " + string.Join(string.Empty, reversedList));
        }
    }
}
