using AdvancedFeature.Core.Model;
using AdvancedFeature.View;

namespace AdvancedFeature.Tasks
{
    /// <summary>
    /// Implementing and demonstrating manipulation of records.
    /// </summary>
    public class BookRecordDemo
    {
        /// <summary>
        /// Perform the task related to record manipulation with help of the Book model.
        /// </summary>
        public void ExecuteRecordManipulation()
        {
            Book book1 = new Book("Atomic Habit", "Ramji", 1200);
            Book book2 = new Book("C# Basics", "John Doe", 27);
            ConsoleActivity.ShowHeader("Record manipulation");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("--- Books present ---");
            ConsoleActivity.PrintInConsole($"Book 1 record : {book1.Title}  --  {book1.Author}  --  {book1.ISBN}");
            ConsoleActivity.PrintInConsole($"Book 2 record : {book2.Title}  --  {book2.Author}  --  {book2.ISBN}");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("--- Demonstrating Value Equality ---\n");
            Book book1Duplicate = new Book("Atomic Habit", "Ramji", 1200);
            ConsoleActivity.PrintInConsole($"Is book1 == book1Duplicate = {book1 == book1Duplicate}\n");
            ConsoleActivity.PrintInConsole("--- Demonstrating Immutability ---");

            // UNCOMMENTING THE LINE BELOW WILL CAUSE A COMPILE ERROR:
            // book1.Title = "New Title";
            ConsoleActivity.PrintInConsole("Note: Record properties are 'init-only' by default and cannot be changed after initialization.");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("--- Demonstrating 'with' Expression ---\n");
            Book updatedBook = book2 with { Title = "The C# Player's Guide (Revised Edition)" };
            ConsoleActivity.PrintInConsole($"Original Book: {book1.Title}");
            ConsoleActivity.PrintInConsole($"New Copied Book: {updatedBook.Title}\n");
            ConsoleActivity.PrintInConsole("--- Demonstrating Deconstruction ---\n");
            this.DisplayBook(book1);
            ConsoleActivity.WaitInConsole();
        }

        /// <summary>
        /// Display the book details.
        /// </summary>
        /// <param name="book">Book record to be displayed.</param>
        public void DisplayBook(Book book)
        {
            var (title, author, isbn) = book;
            ConsoleActivity.PrintInConsole($"Book record : {book.Title}  --  {book.Author}  --  {book.ISBN}");
        }
    }
}
