using System.Runtime.CompilerServices;
using Assignment1.Controller;
using Assignment1.Persistence;
using Assignment1.Services;

namespace Assignments
{
    /// <summary>
    /// Console Based Contact Manager
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// the entry point of the porgram main function
        /// </summary>
        public static void Main()
        {
            ConsoleActivity activity = new ConsoleActivity();
            ContactManager service = new ContactManager();
            ContactController contactManager = new ContactController(activity, service);
            contactManager.Start();
        }
    }
}