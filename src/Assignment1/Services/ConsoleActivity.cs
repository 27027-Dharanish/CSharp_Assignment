using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Assignment1;
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
        private Helper _help = new Helper();

        /// <summary>
        /// repo
        /// </summary>
        /// <param name="repo">s</param>
        public void GetUserData()
        {
            Console.Clear();
            Console.WriteLine("New Contact Adding:");
            Console.WriteLine("Enter the name: ");
            string? name = Console.ReadLine();
            if (this._help.IsNull(name))
            {
                Console.WriteLine("Name cannot be Empty!!");
                return;
            }

            Console.WriteLine("Enter the number: ");
            string? number = Console.ReadLine();
            if (this._help.IsNull(number))
            {
                Console.WriteLine("Number cannot be Empty!!");
                return;
            }

            if (!this._help.IsValidNumber(number))
            {
                Console.WriteLine("Invalid Number!!");
                return;
            }

            Console.WriteLine("Ente the email: ");
            string? email = Console.ReadLine();
            if (this._help.IsNull(name))
            {
                Console.WriteLine("Email cannot be Empty!!");
                return;
            }

            if (!this._help.IsValidEmail(email))
            {
                Console.WriteLine("Invalid email!!");
                return;
            }

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
                    case "S":
                    case "s":
                        Console.Clear();
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
                            Console.Clear();
                            Console.WriteLine("Enter the vaid Number!!");
                        }

                        break;
                    case "u":
                    case "U":
                        this.ChooseEditContact();
                        break;

                    case "D":
                    case "d":
                        Console.WriteLine("Enter the name to delete: ");
                        string? name = Console.ReadLine();

                        this.DeleteContactUsingName(name);
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
                Console.Clear();
                Console.WriteLine("No contacts found!!");
                return;
            }

            Console.Clear();
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

            Console.WriteLine("Press any key to continue!!");
            Console.ReadKey();
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
            if (name != null)
            {
                ContactInfo? contact = this._contactManager.SearchContact(name, "0");
                if (contact != null)
                {
                    Console.Clear();
                    Console.WriteLine("\n--- Contact Found ---");
                    Console.WriteLine();
                    Console.WriteLine("Name: " + contact.GetName());
                    Console.WriteLine("PhoneNumber: " + contact.GetNumber());
                    Console.WriteLine("Email: " + contact.GetEmail());
                    Console.WriteLine("Notes: " + contact.GetNotes());
                    Console.WriteLine("==========================");
                    Console.WriteLine("Press any key to continue!!");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Search number
        /// </summary>
        public void SearchContactByNumber()
        {
            Console.WriteLine("Enter the number: ");
            string? number = Console.ReadLine();
            if (this._help.IsNull(number))
            {
                Console.WriteLine("Name cannot be Empty!!");
                return;
            }

            if (number != null)
            {
                ContactInfo? contact = this._contactManager.SearchContact(number, "1");
                if (contact != null)
                {
                    Console.Clear();
                    Console.WriteLine("\n--- Contact Found ---");
                    Console.WriteLine();
                    Console.WriteLine("Name: " + contact.GetName());
                    Console.WriteLine("PhoneNumber: " + contact.GetNumber());
                    Console.WriteLine("Email: " + contact.GetEmail());
                    Console.WriteLine("Notes: " + contact.GetNotes());
                    Console.WriteLine("==========================");
                    Console.WriteLine("Press any key to continue!!");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// chhosee edit
        /// </summary>
        public void ChooseEditContact()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Select method to search: ");
            Console.WriteLine("1.By name");
            Console.WriteLine("2.By number");
            Console.WriteLine("Enter the option ( 1 or 2 ): ");
            string? choiceSearch = Console.ReadLine();
            ContactInfo? contact = null;
            if (choiceSearch == "1")
            {
                Console.WriteLine("Enter the name to edit: ");
                string? name = Console.ReadLine();
                if (name != null)
                {
                    contact = this._contactManager.SearchContact(name, "0");
                }
            }
            else if (choiceSearch == "2")
            {
                Console.WriteLine("Enter the number to edit: ");
                string? number = Console.ReadLine();
                if (number != null)
                {
                    contact = this._contactManager.SearchContact(number, "1");
                }
            }
            else
            {
                Console.WriteLine("Enter the vaid Number!!");
                return;
            }

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Choose the field to Edit: ");
            Console.WriteLine("1.Name");
            Console.WriteLine("2.PhoneNumber");
            Console.WriteLine("3.Email");
            Console.WriteLine("4.Notes");
            string? contactSearch = Console.ReadLine();
            if (contactSearch == "1")
            {
                Console.WriteLine("Enter the new name: ");
                string? newName = Console.ReadLine();
                if (contact != null)
                {
                    this._contactManager.UpdateContact(newName, contact.GetNumber(), contact.GetEmail(), contact.GetNotes(), contact);
                }
            }
            else if (contactSearch == "2")
            {
                Console.WriteLine("Enter the new number: ");
                string? newNumber = Console.ReadLine();
                if (contact != null && this._help.IsValidNumber(newNumber))
                {
                    this._contactManager.UpdateContact(contact.GetName(), newNumber, contact.GetEmail(), contact.GetNotes(), contact);
                }
            }
            else if (contactSearch == "3")
            {
                Console.WriteLine("Enter the new email: ");
                string? newEmail = Console.ReadLine();
                if (contact != null && this._help.IsValidEmail(newEmail))
                {
                    this._contactManager.UpdateContact(contact.GetName(), contact.GetNumber(), newEmail, contact.GetNotes(), contact);
                }
            }
            else if (contactSearch == "4")
            {
                Console.WriteLine("Enter the new notes: ");
                string? newNotes = Console.ReadLine();
                if (contact != null)
                {
                    this._contactManager.UpdateContact(contact.GetName(), contact.GetNumber(), contact.GetNotes(), newNotes, contact);
                }
            }
        }
    }
}