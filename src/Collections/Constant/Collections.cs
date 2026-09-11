using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.Constant
{
    /// <summary>
    /// Holds the constant available to manage the menu options.
    /// </summary>
    public enum MenuOptions
    {
        /// <summary>
        /// Denote the list operation.
        /// </summary>
        ListOperation = 1,

        /// <summary>
        /// Specifies stack operation.
        /// </summary>
        StackOperation,

        /// <summary>
        /// Specifies queue operation.
        /// </summary>
        QueueOperation,

        /// <summary>
        /// Specifies dictionary operation.
        /// </summary>
        DictionaryOperation,

        /// <summary>
        /// Exit from the application.
        /// </summary>
        Exit,
    }

    /// <summary>
    /// Holds the constant for the operation available in list.
    /// </summary>
    public enum ListOperation
    {
        /// <summary>
        /// Specifies add operation.
        /// </summary>
        Add = 1,

        /// <summary>
        /// Specifies remove operation.
        /// </summary>
        Remove,

        /// <summary>
        /// Specifies contain operation.
        /// </summary>
        CheckExists,

        /// <summary>
        /// Specifies contain operation.
        /// </summary>
        Display,

        /// <summary>
        /// Exit from the list operation.
        /// </summary>
        Exit,
    }

    /// <summary>
    /// Holds the constant for the operation that can be done in queue.
    /// </summary>
    public enum QueueOperation
    {
        /// <summary>
        /// Specifies add operation in queue.
        /// </summary>
        AddItem = 1,

        /// <summary>
        /// Specifies remove operation in queue.
        /// </summary>
        RemoveItem,

        /// <summary>
        /// Specifies display operation in queue.
        /// </summary>
        Display,

        /// <summary>
        /// Exit from the queue operation.
        /// </summary>
        Exit,
    }

    /// <summary>
    /// Holds the constant for the operation that can be done in dictionary.
    /// </summary>
    public enum DictionaryOperation
    {
        /// <summary>
        /// Specifies add operation in dictionary.
        /// </summary>
        AddItem = 1,

        /// <summary>
        /// Specifies remove operation in dictionary.
        /// </summary>
        RemoveItem,

        /// <summary>
        /// Specifies contain operation in dictionary.
        /// </summary>
        CheckExists,

        /// <summary>
        /// Specifies display operation in dictionary.
        /// </summary>
        Display,

        /// <summary>
        /// Exit from the dictionary operation.
        /// </summary>
        Exit,
    }
}
