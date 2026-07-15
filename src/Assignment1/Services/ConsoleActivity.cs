using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Models;
using Assignment1.Persistence;
using Assignment1.Services;

namespace Assignment1.Services
{
    /// <summary>
    /// Get console data
    /// </summary>
    internal class ConsoleActivity
    {
        private ContactManager _contactManager = new ContactManager();

        /// <summary>
        /// repo
        /// </summary>
        /// <param name="repo">s</param>
        public void GetUserData()
        {
            Console.WriteLine("New Contact Adding:");
            Console.WriteLine("Enter the name: ");
            string? name = Console.ReadLine();
            Console.WriteLine("Enter the number: ");
            string? number = Console.ReadLine();
            Console.WriteLine("Ente the email: ");
            string? email = Console.ReadLine();
            Console.WriteLine("Enter any notes: ");
            string? notes = Console.ReadLine();
            this._contactManager.AddContact(name, number, email, notes);
        }

        /// <summary>
        /// show option
        /// </summary>
        public void ShowOption()
        {
            string? userInput;
            do
            {
                Console.WriteLine();
                Console.WriteLine("[A]dd new contact");
                Console.WriteLine("[V]iew contact");
                Console.WriteLine("[S]earch contact");
                Console.WriteLine("[D]elete contact");
                Console.WriteLine("[U]pdate or edit contact");
                Console.WriteLine("[E]xit");
                Console.WriteLine();
                Console.WriteLine("Enter the Option:");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "a":
                    case "A":
                        this.GetUserData();
                        break;
                    case "v":
                    case "V":
                        this.PrintContact(this._contactManager.GetAllContacts());
                        break;
                    case "D":
                    case "d":
                        Console.WriteLine("Enter the name to delete: ");
                        string? name = Console.ReadLine();
                        this.DeleteContactUsingName(name);
                        break;
                    case "S":
                    case "s":
                        Console.WriteLine();
                        Console.WriteLine("Select method to search: ");
                        Console.WriteLine("1.By name");
                        Console.WriteLine("2.By number");
                        Console.WriteLine("Enter the option ( 1 or 2 ): ");
                        string? choiceSearch = Console.ReadLine();
                        if (choiceSearch == "1")
                        {
                            this.SearchContactByName();
                        }
                        else if (choiceSearch == "2")
                        {
                            this.SearchContactByNumber();
                        }
                        else
                        {
                            Console.WriteLine("Enter the vaid Number!!");
                        }

                        break;
                    case "u":
                    case "U":
                        // EditContact(contactDetails);
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }
            while (userInput != "e" && userInput != "E");
        }

        /// <summary>
        /// summary
        /// </summary>
        /// <param name="contactList">manager</param>
        public void PrintContact(List<ContactInfo> contactList)
        {
            if (contactList.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            Console.WriteLine("\nThe contacts are: ");
            for (int i = 0; i < contactList.Count; i++)
            {
                Console.WriteLine("=====================");
                Console.WriteLine("Name: " + contactList[i].GetName());
                Console.WriteLine("PhoneNumber: " + contactList[i].GetNumber());
                Console.WriteLine("Email: " + contactList[i].GetEmail());
                Console.WriteLine("Notes: " + contactList[i].GetNotes());
                Console.WriteLine("=====================");
            }
        }

        /// <summary>
        /// deletecontact
        /// </summary>
        /// <param name="name">name</param>
        public void DeleteContactUsingName(string? name)
        {
            this._contactManager.DeleteContact(name);
        }

        /// <summary>
        /// get name
        /// </summary>
        public void SearchContactByName()
        {
            Console.WriteLine("Enter the name: ");
            string? name = Console.ReadLine();
            ContactInfo? contact = this._contactManager.SearchContact(name,"0");
            if (contact != null)
            {
                Console.WriteLine("\n--- Contact Found ---");
                Console.WriteLine();
                Console.WriteLine("Name: " + contact.GetName());
                Console.WriteLine("PhoneNumber: " + contact.GetNumber());
                Console.WriteLine("Email: " + contact.GetEmail());
                Console.WriteLine("Notes: " + contact.GetNotes());
                Console.WriteLine("==========================");
            }
        }

        /// <summary>
        /// Search number
        /// </summary>
        public void SearchContactByNumber()
        {
            Console.WriteLine("Enter the number: ");
            string? number = Console.ReadLine();
            ContactInfo? contact = this._contactManager.SearchContact(number, "1");
            if (contact != null)
            {
                Console.WriteLine("\n--- Contact Found ---");
                Console.WriteLine();
                Console.WriteLine("Name: " + contact.GetName());
                Console.WriteLine("PhoneNumber: " + contact.GetNumber());
                Console.WriteLine("Email: " + contact.GetEmail());
                Console.WriteLine("Notes: " + contact.GetNotes());
                Console.WriteLine("==========================");
            }
        }
    }
}