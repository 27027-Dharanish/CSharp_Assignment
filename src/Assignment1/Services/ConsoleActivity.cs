using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Assignment1;
using Assignment1.Controller;
using Assignment1.Models;
using Assignment1.Persistence;
using Assignment1.Services;

namespace Assignment1.Services
{
    /// <summary>
    /// Interact with the console for printing and getting user data.
    /// </summary>
    internal class ConsoleActivity
    {
        /// <summary>
        /// Get the user data by console.
        /// </summary>
        /// <param name="contentToGet">Content to get from the user</param>
        /// <returns>Return the content from the user</returns>
        public string? GetInputFromUser(string? contentToGet)
        {
            Console.WriteLine("Enter the " + contentToGet + ": ");
            string? content = Console.ReadLine();
            if (content != null)
            {
                return content;
            }

            return " ";
        }

        /// <summary>
        /// Print the contact in console.
        /// </summary>
        /// <param name="contact">The contact data from the controller</param>
        public void PrintContactInConsole(ContactInfo contact)
        {
            if (contact != null)
            {
                Console.WriteLine("Name: " + contact.GetName());
                Console.WriteLine("PhoneNumber: " + contact.GetNumber());
                Console.WriteLine("Email: " + contact.GetEmail());
                Console.WriteLine("Notes: " + contact.GetNotes());
                this.PrintEmptyLine();
                Console.WriteLine("==========================");
                this.WaitInConsole();
            }
        }

        /// <summary>
        /// Print the data to the console.
        /// </summary>
        /// <param name="data">The data that user want to show in console</param>
        public void PrintInConsole(string data)
        {
            Console.WriteLine(data);
        }

        /// <summary>
        /// Print empty line in console.
        /// </summary>
        public void PrintEmptyLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Clear the console.
        /// </summary>
        public void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Wait in console until we click any key.
        /// </summary>
        public void WaitInConsole()
        {
            Console.WriteLine("Press any key to continue!!");
            Console.ReadKey();
        }
    }
}