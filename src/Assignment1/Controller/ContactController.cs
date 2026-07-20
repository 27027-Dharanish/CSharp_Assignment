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
    /// contact controller - it act as bridge between view and service
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
        /// list of fields available in the contact repository
        /// </summary>
        public enum ContactFieldConstant
        {
            /// <summary>
            /// name of the contact
            /// </summary>
            Name = 1,

            /// <summary>
            /// Phone Number of the conatct
            /// </summary>
            Number = 2,

            /// <summary>
            /// Email id
            /// </summary>
            Email = 3,

            /// <summary>
            /// notes for the conatact
            /// </summary>
            Notes = 4,
        }

        /// <summary>
        /// list of constant for searching
        /// </summary>
        public enum SearchType
        {
            /// <summary>
            /// search for the constant using name
            /// </summary>
            ByName = 1,

            /// <summary>
            /// search for the contact using number
            /// </summary>
            ByNumber = 2,
        }

        /// <summary>
        /// this method start the contact manager
        /// </summary>
        public void StartContactManager()
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
                        this.SearhContactHandler();
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
        /// Get the contact data from user
        /// </summary>
        public void GetUserData()
        {
            this._consoleActivity.ClearConsole();
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
            this.SendContactInformation(name, number, email, notes);
            this._consoleActivity.WaitInConsole();
        }

        /// <summary>
        /// responsible for sending contact information from the controller to the service
        /// </summary>
        /// <param name="name">name </param>
        /// <param name="number">phone number</param>
        /// <param name="email">email id</param>
        /// <param name="notes">notes about the contact</param>
        public void SendContactInformation(string? name, string? number, string? email, string? notes)
        {
            if (this._service.CreateNewContact(name, number, email, notes))
            {
                this._consoleActivity.PrintInConsole("Contact added successfully!!");
            }
            else
            {
                this._consoleActivity.PrintInConsole("Conatact not added!!");
            }
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
            foreach (ContactInfo contact in contactList)
            {
                this._consoleActivity.PrintInConsole("=====================");
                this._consoleActivity.PrintEmptyLine();
                this._consoleActivity.PrintInConsole("Name: " + contact.GetName());
                this._consoleActivity.PrintInConsole("PhoneNumber: " + contact.GetNumber());
                this._consoleActivity.PrintInConsole("Email: " + contact.GetEmail());
                this._consoleActivity.PrintInConsole("Notes: " + contact.GetNotes());
                this._consoleActivity.PrintEmptyLine();
                this._consoleActivity.PrintInConsole("=====================");
            }

            this._consoleActivity.WaitInConsole();
        }

        /// <summary>
        /// Handle the search contact request from the switch
        /// </summary>
        public void SearhContactHandler()
        {
            string? choiceSearch = this.ChooseNameOrNumber();
            if (int.TryParse(choiceSearch, out var choiceNumber))
            {
                if (choiceNumber == (int)SearchType.ByName)
                {
                    this.SearchContactByField("SearchUsingName");
                }
                else if (choiceNumber == (int)SearchType.ByNumber)
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
        }

        /// <summary>
        /// Search the contact using name and number
        /// </summary>
        /// <param name="searchOption">Says what field to use for saerch</param>
        public void SearchContactByField(string? searchOption)
        {
            ContactInfo? contact = null;
            if (searchOption == "SearchUsingName")
            {
                string? name = this._consoleActivity.GetInputFromUser("name");
                if (name != null)
                {
                    contact = this._service.SearchContact(name, searchOption);
                }
            }
            else
            {
                string? number = this._consoleActivity.GetInputFromUser("Phone Number");
                if (number != null)
                {
                    contact = this._service.SearchContact(number, searchOption);
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
            if (this._service.DeleteContact(name) && name == string.Empty)
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
            string? choiceSearch = this.ChooseNameOrNumber();
            ContactInfo? contact = null;
            if (int.TryParse(choiceSearch, out int choiceSearchNumber))
            {
                if (choiceSearchNumber == (int)SearchType.ByName)
                {
                    string? name = this._consoleActivity.GetInputFromUser("name to edit");
                    if (name != null)
                    {
                        contact = this._service.SearchContact(name, "SearchUsingName");
                    }
                }
                else if (choiceSearchNumber == (int)SearchType.ByNumber)
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
            }

            if (contact == null)
            {
                this._consoleActivity.PrintInConsole("Invalid details given !!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            string? contactSearch = this.GetFieldNameToEdit();
            if (int.TryParse(contactSearch, out int contactSearchNumber))
            {
                if (contactSearchNumber == (int)ContactFieldConstant.Name)
                {
                    string? newName = this._consoleActivity.GetInputFromUser("new name");
                    if (contact != null && this._service.UpdateContact(newName, contact.GetNumber(), contact.GetEmail(), contact.GetNotes(), contact))
                    {
                        this.ContactUpdateSuccess();
                    }
                    else
                    {
                        this.ContactUpdateFailed("Name");
                    }
                }
                else if (contactSearchNumber == (int)ContactFieldConstant.Number)
                {
                    string? newNumber = this._consoleActivity.GetInputFromUser("new number");
                    if (contact != null && Helper.IsValidNumber(newNumber) && this._service.UpdateContact(contact.GetName(), newNumber, contact.GetEmail(), contact.GetNotes(), contact))
                    {
                        this.ContactUpdateSuccess();
                    }
                    else
                    {
                        this.ContactUpdateFailed("Number");
                    }
                }
                else if (contactSearchNumber == (int)ContactFieldConstant.Email)
                {
                    string? newEmail = this._consoleActivity.GetInputFromUser("new email");
                    if (contact != null && Helper.IsValidEmail(newEmail) && this._service.UpdateContact(contact.GetName(), contact.GetNumber(), newEmail, contact.GetNotes(), contact))
                    {
                        this.ContactUpdateSuccess();
                    }
                    else
                    {
                        this.ContactUpdateFailed("Email");
                    }
                }
                else if (contactSearchNumber == (int)ContactFieldConstant.Notes)
                {
                    string? newNotes = this._consoleActivity.GetInputFromUser("new notes");
                    if (contact != null && this._service.UpdateContact(contact.GetName(), contact.GetNumber(), contact.GetEmail(), newNotes, contact))
                    {
                        this.ContactUpdateSuccess();
                    }
                }
                else
                {
                    this.ContactUpdateFailed("Notes");
                }
            }
        }

        /// <summary>
        /// Get the option from user to edit using name or number
        /// </summary>
        /// <returns>returns the optoin selected by the user</returns>
        public string? GetFieldNameToEdit()
        {
            this._consoleActivity.ClearConsole();
            this._consoleActivity.PrintEmptyLine();
            this._consoleActivity.PrintEmptyLine();
            this._consoleActivity.PrintInConsole("Choose the field to Edit: ");
            this._consoleActivity.PrintInConsole("1.Name");
            this._consoleActivity.PrintInConsole("2.PhoneNumber");
            this._consoleActivity.PrintInConsole("3.Email");
            this._consoleActivity.PrintInConsole("4.Notes");
            string? contactSearchOption = this._consoleActivity.GetInputFromUser("Option");
            return contactSearchOption;
        }

        /// <summary>
        /// choose name or number
        /// </summary>
        /// <returns>return the whether name=1 or number=2</returns>
        public string? ChooseNameOrNumber()
        {
            this._consoleActivity.PrintInConsole("Select method to search: ");
            this._consoleActivity.PrintInConsole("1.By name");
            this._consoleActivity.PrintInConsole("2.By number");
            string? choiceSearch = this._consoleActivity.GetInputFromUser("Option ( 1 or 2 )");
            return choiceSearch;
        }

        /// <summary>
        /// contact update status success
        /// </summary>
        public void ContactUpdateSuccess()
        {
            this._consoleActivity.PrintInConsole("Contact Updated Successfully!!");
            this._consoleActivity.WaitInConsole();
        }

        /// <summary>
        /// contact update status failed
        /// </summary>
        /// <param name="field">the field that get failed to update</param>
        public void ContactUpdateFailed(string? field)
        {
            this._consoleActivity.PrintInConsole($"Invalid {field}!!");
            this._consoleActivity.WaitInConsole();
        }
    }
}
