using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collections.View;

namespace Collections
{
    /// <summary>
    /// Handle the queue operation.
    /// </summary>
    /// <typeparam name="T">The type of element in the queue.</typeparam>
    public class CustomQueue<T>
    {
        private Queue<T> _queue = new Queue<T>();

        /// <summary>
        /// Add the item in the queue.
        /// </summary>
        /// <param name="item">Item to be added in queue.</param>
        public void AddItem(T item)
        {
            this._queue.Enqueue(item);
            ConsoleActivity.PrintAndWait($"{item} added successfully!");
        }

        /// <summary>
        /// Remove the item from the queue.
        /// </summary>
        public void RemoveItem()
        {
            if (this._queue.Count > 0)
            {
                ConsoleActivity.PrintAndWait($"Dequeued person name : {this._queue.Dequeue()}");
                return;
            }

            ConsoleActivity.PrintAndWait("No person there to remove!");
        }

        /// <summary>
        /// Display all the element available in queue.
        /// </summary>
        public void DisplayAllElement()
        {
            if (this._queue.Count > 0)
            {
                ConsoleActivity.PrintInConsole("Item present in list :");
                int i = 1;
                foreach (T item in this._queue)
                {
                    ConsoleActivity.PrintInConsole($"{i++}.{item}");
                }

                ConsoleActivity.WaitInConsole();
            }

            ConsoleActivity.PrintAndWait("No item in the queue!");
        }
    }
}
