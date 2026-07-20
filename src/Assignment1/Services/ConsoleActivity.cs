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
    /// the class interact with the console for printing and getting user data
    /// </summary>
    internal class ConsoleActivity
    {
        /// <summary>
        /// get the user data by console
        /// </summary>
        /// <param name="contentToGet">content to get from the user</param>
        /// <returns>return the content from the user</returns>
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
        /// print the contact in console
        /// </summary>
        /// <param name="contact">the contact data from the controller</param>
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
        /// print the data to the console
        /// </summary>
        /// <param name="data">the data that user want to show in console</param>
        public void PrintInConsole(string data)
        {
            Console.WriteLine(data);
        }

        /// <summary>
        /// print empty line in console
        /// </summary>
        public void PrintEmptyLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// clear the console
        /// </summary>
        public void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// wait in console until we click any key
        /// </summary>
        public void WaitInConsole()
        {
            Console.WriteLine("Press any key to continue!!");
            Console.ReadKey();
        }
    }
}