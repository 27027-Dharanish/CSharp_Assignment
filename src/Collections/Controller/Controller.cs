using Collections.Constant;
using Collections.View;

namespace Collections.Controller
{
    /// <summary>
    /// Control and coordinate the action to be done in collections.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Handle all the collection and control the flow.
        /// </summary>
        public void HandleCollections()
        {
            MenuOptions userChoice;
            do
            {
                ConsoleActivity.ShowMenu("Collections", new string[] { "List", "Stack", "Queue", "Dictionary", "Exit" });
                userChoice = (MenuOptions)ConsoleActivity.GetIntegerInput("choice");
                switch (userChoice)
                {
                    case MenuOptions.ListOperation:
                        this.HandleListTask();
                        break;
                    case MenuOptions.StackOperation:
                        this.HandleStackTask();
                        break;
                    case MenuOptions.QueueOperation:
                        this.HandleQueueTask();
                        break;
                    case MenuOptions.DictionaryOperation:
                        this.HandleDictionaryOperation();
                        break;
                    case MenuOptions.Exit:
                        ConsoleActivity.ExitApplication();
                        break;
                    default:
                        ConsoleActivity.PrintAndWait("Enter a valid choice!");
                        break;
                }
            }
            while (userChoice != MenuOptions.Exit);
        }

        private void HandleListTask()
        {
            TaskList<string> books = new TaskList<string>();
            ListOperation userChoice;
            do
            {
                ConsoleActivity.ShowMenu("Manage list of books", new string[] { "Add new book", "Remove book", "Check whether a book exist", "Display all books", "Exit" });
                userChoice = (ListOperation)ConsoleActivity.GetIntegerInput("operation to be performed");
                switch (userChoice)
                {
                    case ListOperation.Add:
                        books.AddNewBook(ConsoleActivity.GetStringInput("book name") ?? string.Empty);
                        break;
                    case ListOperation.Remove:
                        books.RemoveBook(ConsoleActivity.GetStringInput("book name to be removed") ?? string.Empty);
                        break;
                    case ListOperation.CheckExists:
                        books.CheckIfBookExist(ConsoleActivity.GetStringInput("book name to be checked") ?? string.Empty);
                        break;
                    case ListOperation.Display:
                        books.DisplayAllBooks();
                        break;
                    case ListOperation.Exit:
                        break;
                    default:
                        ConsoleActivity.PrintAndWait("Enter a valid choice!");
                        break;
                }
            }
            while (userChoice != ListOperation.Exit);
        }

        private void HandleStackTask()
        {
            CustomStack<char> charStack = new CustomStack<char>();
            ConsoleActivity.ShowHeader("Stack - Reverse word");
            string? userInput = ConsoleActivity.GetStringInput("Enter a string to reverse");
            if (!string.IsNullOrEmpty(userInput))
            {
                char[] characters = userInput.ToCharArray();
                charStack.ReverseAndDisplay(characters);
            }
            else
            {
                ConsoleActivity.PrintAndWait("Invalid input provided.");
            }
        }

        private void HandleQueueTask()
        {
            CustomQueue<string> persons = new CustomQueue<string>();
            QueueOperation userChoice;
            do
            {
                ConsoleActivity.ShowMenu("Queue of persons", new string[] { "Add new person", "Remove person", "Display all books", "Exit" });
                userChoice = (QueueOperation)ConsoleActivity.GetIntegerInput("operation to be performed");
                switch (userChoice)
                {
                    case QueueOperation.AddItem:
                        persons.AddItem(ConsoleActivity.GetStringInput("book name") ?? string.Empty);
                        break;
                    case QueueOperation.RemoveItem:
                        persons.RemoveItem();
                        break;
                    case QueueOperation.Display:
                        persons.DisplayAllElement();
                        break;
                    case QueueOperation.Exit:
                        break;
                    default:
                        ConsoleActivity.PrintAndWait("Enter a valid choice!");
                        break;
                }
            }
            while (userChoice != QueueOperation.Exit);
        }

        private void HandleDictionaryOperation()
        {
            CustomDictionary<string, int> studentRecords = new CustomDictionary<string, int>();
            DictionaryOperation userChoice;

            do
            {
                ConsoleActivity.ShowMenu("Dictionary of Student Records", new string[] { "Add new student record", "Remove student record", "Check if student exists", "Display all records", "Exit" });
                userChoice = (DictionaryOperation)ConsoleActivity.GetIntegerInput("operation to be performed");

                switch (userChoice)
                {
                    case DictionaryOperation.AddItem:
                        string keyInput = ConsoleActivity.GetStringInput("student name (Key)") ?? string.Empty;
                        if (string.IsNullOrEmpty(keyInput))
                        {
                            ConsoleActivity.PrintAndWait("Student name cannot be empty!");
                            break;
                        }

                        string valueInput = ConsoleActivity.GetStringInput("student ID (Value)") ?? "0";
                        if (int.TryParse(valueInput, out int studentId))
                        {
                            studentRecords.AddRecord(keyInput, studentId);
                        }
                        else
                        {
                            ConsoleActivity.PrintAndWait("Invalid ID. It must be a valid number!");
                        }
                        break;

                    case DictionaryOperation.RemoveItem:
                        string removeKey = ConsoleActivity.GetStringInput("student name to remove") ?? string.Empty;
                        studentRecords.RemoveRecord(removeKey);
                        break;

                    case DictionaryOperation.CheckExists:
                        string checkKey = ConsoleActivity.GetStringInput("student name to check") ?? string.Empty;
                        studentRecords.CheckIfKeyExists(checkKey);
                        break;

                    case DictionaryOperation.Display:
                        studentRecords.DisplayAllRecords();
                        break;

                    case DictionaryOperation.Exit:
                        break;

                    default:
                        ConsoleActivity.PrintAndWait("Enter a valid choice!");
                        break;
                }
            }
            while (userChoice != DictionaryOperation.Exit);
        }
    }
}
