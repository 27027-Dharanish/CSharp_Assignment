using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Models;
using Assignment1.Services;

namespace Assignment1.Controller
{
    /// <summary>
    /// Class for contact controller - it act as bridge between view and service
    /// </summary>
    internal class ContactController
    {
        private ConsoleActivity _consoleActivity;
        private ContactManager _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactController"/> class.
        /// constructor for contact controller
        /// </summary>
        /// <param name="consoleActivity">consoleactivitys</param>
        /// <param name="service">services</param>
        public ContactController(ConsoleActivity consoleActivity, ContactManager service)
        {
            this._consoleActivity = consoleActivity;
            this._service = service;
        }

        /// <summary>
        /// this method start the contact manager
        /// </summary>
        public void Start()
        {
            this.ShowContactOption();
        }

        /// <summary>
        /// show option available in contact manager
        /// </summary>
        public void ShowContactOption()
        {
            string? userInput;
            do
            {
                this._consoleActivity.PrintEmptyLine();
                this._consoleActivity.PrintInConsole("[A]dd new contact");
                this._consoleActivity.PrintInConsole("[V]iew contact");
                this._consoleActivity.PrintInConsole("[S]earch contact");
                this._consoleActivity.PrintInConsole("[D]elete contact");
                this._consoleActivity.PrintInConsole("[U]pdate or edit contact");
                this._consoleActivity.PrintInConsole("[E]xit");
                this._consoleActivity.PrintEmptyLine();
                userInput = this._consoleActivity.GetInputFromUser("Option");
                switch (userInput)
                {
                    case "a":
                    case "A":
                        this.GetUserData();
                        break;
                    case "v":
                    case "V":

                        this.PrintContact(this._service.GetAllContacts());
                        break;
                    case "S":
                    case "s":
                        this.HandleSearchContactFromSwitch();
                        break;
                    case "u":
                    case "U":
                        this.ChooseEditContact();
                        break;

                    case "D":
                    case "d":
                        this.DeleteContactUsingName();
                        break;
                    case "E":
                    case "e":
                        // This prevents the default case from printing an invalid message and allows the program to exit successfully
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        this._consoleActivity.WaitInConsole();
                        break;
                }

                this._consoleActivity.ClearConsole();
            }
            while (userInput != "e" && userInput != "E");
        }

        /// <summary>
        /// Get the data from user and pass to the service
        /// </summary>
        public void GetUserData()
        {
            Console.Clear();
            this._consoleActivity.PrintInConsole("New Contact Adding:");
            string? name = this._consoleActivity.GetInputFromUser("name");
            if (Helper.IsNull(name))
            {
                this._consoleActivity.PrintInConsole("Name cannot be Empty!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            string? number = this._consoleActivity.GetInputFromUser("Phone Number");
            if (Helper.IsNull(number))
            {
                this._consoleActivity.PrintInConsole("Number cannot be Empty!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            if (!Helper.IsValidNumber(number))
            {
                this._consoleActivity.PrintInConsole("Invalid Number!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            string? email = this._consoleActivity.GetInputFromUser("Email");
            if (Helper.IsNull(name))
            {
                this._consoleActivity.PrintInConsole("Email cannot be Empty!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            if (!Helper.IsValidEmail(email))
            {
                this._consoleActivity.PrintInConsole("Invalid email!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            string? notes = this._consoleActivity.GetInputFromUser("Notes");
            this._service.AddContact(name, number, email, notes);
            this._consoleActivity.PrintInConsole("Contact added successfully!!");
            this._consoleActivity.WaitInConsole();
        }

        /// <summary>
        /// print the contact given to it
        /// </summary>
        /// <param name="contactList">list of contact availabe</param>
        public void PrintContact(List<ContactInfo> contactList)
        {
            if (contactList.Count == 0)
            {
                this._consoleActivity.ClearConsole();
                this._consoleActivity.PrintInConsole("No contacts found!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            this._consoleActivity.ClearConsole();
            this._consoleActivity.PrintInConsole("\nThe contacts are: ");
            for (int i = 0; i < contactList.Count; i++)
            {
                this._consoleActivity.PrintInConsole("=====================");
                this._consoleActivity.PrintEmptyLine();
                this._consoleActivity.PrintInConsole("Name: " + contactList[i].GetName());
                this._consoleActivity.PrintInConsole("PhoneNumber: " + contactList[i].GetNumber());
                this._consoleActivity.PrintInConsole("Email: " + contactList[i].GetEmail());
                this._consoleActivity.PrintInConsole("Notes: " + contactList[i].GetNotes());
                this._consoleActivity.PrintEmptyLine();
                this._consoleActivity.PrintInConsole("=====================");
            }

            this._consoleActivity.WaitInConsole();
        }

        /// <summary>
        /// Handle the search contact request from the switch
        /// </summary>
        public void HandleSearchContactFromSwitch()
        {
            this._consoleActivity.ClearConsole();
            this._consoleActivity.PrintInConsole("Select method to search: ");
            this._consoleActivity.PrintInConsole("1.By name");
            this._consoleActivity.PrintInConsole("2.By number");
            this._consoleActivity.PrintInConsole("Enter the option ( 1 or 2 ): ");
            string? choiceSearch = this._consoleActivity.GetInputFromUser(" ");
            if (choiceSearch == "1")
            {
                this.SearchContactByField("SearchUsingName");
            }
            else if (choiceSearch == "2")
            {
                this.SearchContactByField("SearchUsingNumber");
            }
            else
            {
                Console.Clear();
                this._consoleActivity.PrintInConsole("Enter a vaid Number!!");
                this._consoleActivity.WaitInConsole();
            }
        }

        /// <summary>
        /// Search the contact using name and number
        /// </summary>
        /// <param name="searchUsing">Says what field to use for saerch</param>
        public void SearchContactByField(string? searchUsing)
        {
            ContactInfo? contact = null;
            if (searchUsing == "SearchUsingName")
            {
                string? name = this._consoleActivity.GetInputFromUser("name");
                if (name != null)
                {
                    contact = this._service.SearchContact(name, searchUsing);
                }
            }
            else
            {
                string? number = this._consoleActivity.GetInputFromUser("Phone Number");
                if (number != null)
                {
                    contact = this._service.SearchContact(number, searchUsing);
                }
            }

            if (contact != null)
            {
                this._consoleActivity.ClearConsole();
                this._consoleActivity.PrintInConsole("\n--- Contact Found ---");
                this._consoleActivity.PrintContactInConsole(contact);
            }
            else
            {
                this._consoleActivity.PrintInConsole("--- No Contact Found ---");
                this._consoleActivity.WaitInConsole();
            }
        }

        /// <summary>
        /// Delete the contact using name
        /// </summary>
        /// <param name="name">Name that used to delete</param>
        public void DeleteContactUsingName()
        {
            this._consoleActivity.PrintInConsole("Deletion of contact using Name!!");
            string? name = this._consoleActivity.GetInputFromUser("name to Delete");
            if (this._service.DeleteContact(name))
            {
                this._consoleActivity.PrintInConsole("Contact deleted successfully!!");
                this._consoleActivity.WaitInConsole();
            }
            else
            {
                this._consoleActivity.PrintInConsole("No such contact found!!");
                this._consoleActivity.WaitInConsole();
            }
        }

        /// <summary>
        /// Edit the contact using name or number
        /// </summary>
        public void ChooseEditContact()
        {
            this._consoleActivity.PrintInConsole("Select method to search: ");
            this._consoleActivity.PrintInConsole("1.By name");
            this._consoleActivity.PrintInConsole("2.By number");
            string? choiceSearch = this._consoleActivity.GetInputFromUser("Option ( 1 or 2 )");
            ContactInfo? contact = null;
            if (choiceSearch == "1")
            {
                string? name = this._consoleActivity.GetInputFromUser("name to edit");
                if (name != null)
                {
                    contact = this._service.SearchContact(name, "SearchUsingName");
                }
            }
            else if (choiceSearch == "2")
            {
                string? number = this._consoleActivity.GetInputFromUser("number to edit");
                if (number != null)
                {
                    contact = this._service.SearchContact(number, "SearchUsingNumber");
                }
            }
            else
            {
                this._consoleActivity.PrintInConsole("Enter a valid number!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            if (contact == null)
            {
                this._consoleActivity.PrintInConsole("Invalid details given!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            this._consoleActivity.ClearConsole();
            this._consoleActivity.PrintEmptyLine();
            this._consoleActivity.PrintEmptyLine();
            this._consoleActivity.PrintInConsole("Choose the field to Edit: ");
            this._consoleActivity.PrintInConsole("1.Name");
            this._consoleActivity.PrintInConsole("2.PhoneNumber");
            this._consoleActivity.PrintInConsole("3.Email");
            this._consoleActivity.PrintInConsole("4.Notes");
            string? contactSearch = this._consoleActivity.GetInputFromUser("Option");
            if (contactSearch == "1")
            {
                string? newName = this._consoleActivity.GetInputFromUser("new name");
                if (contact != null && this._service.UpdateContact(newName, contact.GetNumber(), contact.GetEmail(), contact.GetNotes(), contact))
                {
                    this._consoleActivity.PrintInConsole("Contact Updated Successfully!!");
                    this._consoleActivity.WaitInConsole();
                }
                else
                {
                    this._consoleActivity.PrintInConsole("Invalid name!!");
                }
            }
            else if (contactSearch == "2")
            {
                string? newNumber = this._consoleActivity.GetInputFromUser("new number");
                if (contact != null && Helper.IsValidNumber(newNumber) && this._service.UpdateContact(contact.GetName(), newNumber, contact.GetEmail(), contact.GetNotes(), contact))
                {
                    this._consoleActivity.PrintInConsole("Contact Updated Successfully!!");
                    this._consoleActivity.WaitInConsole();
                }
                else
                {
                    this._consoleActivity.PrintInConsole("Invalid Number!!");
                    this._consoleActivity.WaitInConsole();
                }
            }
            else if (contactSearch == "3")
            {
                string? newEmail = this._consoleActivity.GetInputFromUser("new email");
                if (contact != null && Helper.IsValidEmail(newEmail) && this._service.UpdateContact(contact.GetName(), contact.GetNumber(), newEmail, contact.GetNotes(), contact))
                {
                    this._consoleActivity.PrintInConsole("Contact Updated Successfully!!");
                    this._consoleActivity.WaitInConsole();
                }
                else
                {
                    this._consoleActivity.PrintInConsole("Invalid Email!!");
                    this._consoleActivity.WaitInConsole();
                }
            }
            else if (contactSearch == "4")
            {
                string? newNotes = this._consoleActivity.GetInputFromUser("new notes");
                if (contact != null && this._service.UpdateContact(contact.GetName(), contact.GetNumber(), contact.GetNotes(), newNotes, contact))
                {
                    this._consoleActivity.PrintInConsole("Contact Updated Successfully!!");
                    this._consoleActivity.WaitInConsole();
                }
            }
            else
            {
                this._consoleActivity.PrintInConsole("Invalid Option !!");
                this._consoleActivity.WaitInConsole();
            }
        }
    }
}
