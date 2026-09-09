using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collections.View;

namespace Collections
{
    internal class Queue
    {
        private Queue<string> _queue = new Queue<string>();

        /// <summary>
        /// Handle all the operation available in queue operation.
        /// </summary>
        public void ExecuteQueueOperation()
        {
            ConsoleActivity.ShowHeader("Task 3 - Queue");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Adding person in queue");
            for (int i = 1; i <= 5; i++)
            {
                string? personName = ConsoleActivity.GetStringInput($"{i} person name : ");
                if (personName != null)
                {
                    this._queue.Enqueue(personName);
                }
            }

            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("The first person in the queue : " + this._queue.Dequeue());
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Person in queue are : ");
            foreach (string person in this._queue)
            {
                ConsoleActivity.PrintInConsole(person);
            }

            ConsoleActivity.WaitInConsole();
        }
    }
}
