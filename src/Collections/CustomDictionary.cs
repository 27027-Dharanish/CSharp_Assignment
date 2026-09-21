using Collections.View;

namespace Collections
{
    /// <summary>
    /// Manages operations that can be performed on a generic dictionary.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    public class CustomDictionary<TKey, TValue>
        where TKey : notnull
    {
        private Dictionary<TKey, TValue> _records = new ();

        /// <summary>
        /// Adds a new key-value pair to the dictionary.
        /// </summary>
        /// <param name="key">The unique key to add.</param>
        /// <param name="value">The value associated with the key.</param>
        public void AddRecord(TKey key, TValue value)
        {
            if (this._records.ContainsKey(key))
            {
                ConsoleActivity.PrintAndWait($"Key '{key}' already exists in the dictionary!");
                return;
            }

            this._records.Add(key, value);
            ConsoleActivity.PrintAndWait($"Added: {key} - {value}");
        }

        /// <summary>
        /// Removes a record from the dictionary using its key.
        /// </summary>
        /// <param name="key">The key of the record to remove.</param>
        public void RemoveRecord(TKey key)
        {
            if (this._records.Remove(key))
            {
                ConsoleActivity.PrintAndWait($"Record with key '{key}' removed successfully!");
                return;
            }

            ConsoleActivity.PrintAndWait($"Unable to remove: Key '{key}' does not exist.");
        }

        /// <summary>
        /// Checks whether a specific key exists in the dictionary.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        public void CheckIfKeyExists(TKey key)
        {
            if (this._records.ContainsKey(key))
            {
                ConsoleActivity.PrintAndWait($"The key '{key}' exists in the dictionary.");
                return;
            }

            ConsoleActivity.PrintAndWait($"No record found with the key '{key}' in the dictionary.");
        }

        /// <summary>
        /// Displays all key-value records present in the dictionary.
        /// </summary>
        public void DisplayAllRecords()
        {
            ConsoleActivity.ShowHeader("Task 4 - Generic Dictionary");
            ConsoleActivity.PrintEmptyLine();

            if (this._records.Count > 0)
            {
                ConsoleActivity.PrintInConsole("Records present in dictionary:");
                foreach (var record in this._records)
                {
                    ConsoleActivity.PrintInConsole($"{record.Key} - {record.Value}");
                }
            }
            else
            {
                ConsoleActivity.PrintInConsole("The dictionary is empty.");
            }

            ConsoleActivity.WaitInConsole();
        }
    }
}
