using Collections.View;

namespace Collections
{
    internal class Dictionary
    {
        private Dictionary<string, int> _studentRecord = new Dictionary<string, int>();

        /// <summary>
        /// Handle the operation in dictionary.
        /// </summary>
        public void HandleDictionaryOperation()
        {
            ConsoleActivity.ShowHeader("Task 4 - Dictionary");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Adding element in dictionary");
            this._studentRecord.Add("Thor", 10);
            this._studentRecord.Add("Sam", 5);
            this._studentRecord.Add("Ravi", 8);
            this._studentRecord.Add("Virat", 3);
            this._studentRecord.Add("Yeager", 9);
            ConsoleActivity.PrintEmptyLine();
            if (this._studentRecord.ContainsKey("Thor"))
            {
                this._studentRecord.Remove("Thor");
                ConsoleActivity.PrintInConsole("Removed student from the dictionary");
            }
            else
            {
                ConsoleActivity.PrintInConsole("Unable to remove the student from the dictionary");
            }

            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Display element from dictionary :");
            foreach (var record in this._studentRecord)
            {
                ConsoleActivity.PrintInConsole($"{record.Key} - {record.Value}");
            }

            ConsoleActivity.WaitInConsole();
        }
    }
}
