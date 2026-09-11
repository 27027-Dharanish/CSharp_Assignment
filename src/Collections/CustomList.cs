using Collections.View;

namespace Collections
{
    /// <summary>
    /// Handles tasks related to a generic list and performs operations on it.
    /// </summary>
    /// <typeparam name="T">The type of element in the list.</typeparam>
    public class CustomList<T>
    {
        private List<T> _books = new List<T>();

        /// <summary>
        /// Add new book in the list.
        /// </summary>
        /// <param name="book">Book item to be added.</param>
        public void AddNewBook(T book)
        {
            this._books.Add(book);
            ConsoleActivity.PrintAndWait($"{book} added successfully!");
        }

        /// <summary>
        /// Remove the book from the list.
        /// </summary>
        /// <param name="book">The book that needed to be removed.</param>
        public void RemoveBook(T book)
        {
            if (this._books.Remove(book))
            {
                ConsoleActivity.PrintAndWait($"{book} removed successfully!");
                return;
            }

            ConsoleActivity.PrintAndWait("No such book exist in list!");
        }

        /// <summary>
        /// Check whether the book exist in the list or not.
        /// </summary>
        /// <param name="book">The book that needed to be checked.</param>
        public void CheckIfBookExist(T book)
        {
            if (this._books.Contains(book))
            {
                ConsoleActivity.PrintAndWait($"The {book} exist in the list.");
                return;
            }

            ConsoleActivity.PrintAndWait($"No such book exist with the name {book} in the list");
        }

        /// <summary>
        /// Display books present in the list.
        /// </summary>
        public void DisplayAllBooks()
        {
            if (this._books.Count > 0)
            {
                ConsoleActivity.PrintInConsole("Books present in list :");
                int i = 1;
                foreach (T book in this._books)
                {
                    ConsoleActivity.PrintInConsole($"{i++}.{book}");
                }
            }

            ConsoleActivity.WaitInConsole();
        }
    }
}
