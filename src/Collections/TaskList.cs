using Collections.View;

namespace Collections
{
    /// <summary>
    /// Handle task related to list and perform operation related to it.
    /// </summary>
    public class TaskList
    {
        private List<string> _books = new List<string>();

        /// <summary>
        /// Handle list of operation available in list.
        /// </summary>
        public void HandleListOperation()
        {
            ConsoleActivity.ShowHeader("Task 1 - List");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Adding new books");
            this._books.Add("Atomic Habit");
            this._books.Add("Rich Dad Poor Dad");
            this._books.Add("Money");
            this._books.Add("Akbar");
            this._books.Add("History of India");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Removing atomic habits from the list :");
            this._books.Remove("Atomic Habit");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Does list of books contain Rich Dad Poor Dad : " + this._books.Contains("Rich Dad Poor Dad"));
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Display all element in the list :");
            int i = 1;
            foreach (string book in this._books)
            {
                ConsoleActivity.PrintInConsole($"{i++}.{book}");
            }

            ConsoleActivity.WaitInConsole();
        }
    }
}
