using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Assignment1.Models;
using Assignment1.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Assignment1.Controller
{
    /// <summary>
    /// Act as bridge between view and service.
    /// </summary>
    internal class ContactController
    {
        private ConsoleActivity _consoleActivity;
        private ContactManager _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactController"/> class.
        /// </summary>
        /// <param name="consoleActivity">Consoleactivitys</param>
        /// <param name="service">Services for contact manager</param>
        public ContactController(ConsoleActivity consoleActivity, ContactManager service)
        {
            this._consoleActivity = consoleActivity;
            this._service = service;
        }

        /// <summary>
        /// Specifies the search using constant.
        /// </summary>
        public enum SearchUsingConstant
        {
            /// <summary>
            /// Represent name of the contact.
            /// </summary>
            SearchUsingName = 1,

            /// <summary>
            /// Represent phone number of the conatct.
            /// </summary>
            SearchUsingNumber = 2,
        }

        /// <summary>
        /// List of fields available in the contact repository.
        /// </summary>
        public enum ContactFieldConstant
        {
            /// <summary>
            /// Represent name of the contact.
            /// </summary>
            Name = 1,

            /// <summary>
            /// Represent number of the conatct.
            /// </summary>
            Number = 2,

            /// <summary>
            /// Represent Email id.
            /// </summary>
            Email = 3,

            /// <summary>
            /// Represent notes for the conatact.
            /// </summary>
            Notes = 4,
        }

        /// <summary>
        /// List of constant for searching.
        /// </summary>
        public enum SearchType
        {
            /// <summary>
            /// Search for the constant using name.
            /// </summary>
            ByName = 1,

            /// <summary>
            /// Search for the contact using number.
            /// </summary>
            ByNumber = 2,
        }

        /// <summary>
        /// Starts the execution flow for the contact manager.
        /// </summary>
        public void StartContactManager()
        {
            this.ShowContactOption();
        }

        /// <summary>
        /// Show option available in contact manager.
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
                        this.EditContactByNameOrNumber();
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
        /// Get the contact data from user.
        /// </summary>
        public void GetUserData()
        {
            this._consoleActivity.ClearConsole();
            this._consoleActivity.PrintInConsole("New Contact Adding:");
            string? name = this._consoleActivity.GetInputFromUser("name");
            if (string.IsNullOrWhiteSpace(name))
            {
                this._consoleActivity.PrintInConsole("Name cannot be Empty!!");
                this._consoleActivity.WaitInConsole();
                return;
            }
            else if (this._service.IsNamePresent(name))
            {
                this._consoleActivity.PrintInConsole("Name already present in contact!!");
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
            else if (!Helper.IsValidNumber(number))
            {
                this._consoleActivity.PrintInConsole("Invalid Number!!");
                this._consoleActivity.WaitInConsole();
                return;
            }
            else if (this._service.IsNumberPresent(number))
            {
                this._consoleActivity.PrintInConsole("Number already present in contact!!");
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
        /// Responsible for sending contact information from the controller to the service.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="number">Phone number</param>
        /// <param name="email">Email id</param>
        /// <param name="notes">Notes about the contact</param>
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
        /// Print the contact given to it.
        /// </summary>
        /// <param name="contactList">List of contact availabe</param>
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
        /// Handle the search contact request.
        /// </summary>
        public void SearhContactHandler()
        {
            if (this._service.IsContactEmpty())
            {
                this._consoleActivity.PrintInConsole("!!No Contact found!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

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
        /// Search the contact using name and number.
        /// </summary>
        /// <param name="searchOption">Says what field to use for saerch</param>
        public void SearchContactByField(string? searchOption)
        {
            if (this._service.IsContactEmpty())
            {
                this._consoleActivity.PrintInConsole("!!No Contact found!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            ContactInfo? contact = null;
            if (searchOption == "SearchUsingName")
            {
                string? name = this._consoleActivity.GetInputFromUser("name");
                if (name != null)
                {
                    contact = this._service.SearchContact(name, (int)SearchUsingConstant.SearchUsingName);
                }
            }
            else
            {
                string? number = this._consoleActivity.GetInputFromUser("Phone Number");
                if (number != null)
                {
                    contact = this._service.SearchContact(number, (int)SearchUsingConstant.SearchUsingNumber);
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
        /// Delete the contact using name.
        /// </summary>
        /// <param name="name">Name that used to delete</param>
        public void DeleteContactUsingName()
        {
            if (this._service.IsContactEmpty())
            {
                this._consoleActivity.PrintInConsole("!!No Contact found!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            this._consoleActivity.PrintInConsole("Deletion of contact using Name!!");
            string? name = this._consoleActivity.GetInputFromUser("name to Delete");
            if (this._service.DeleteContact(name) && !string.IsNullOrWhiteSpace(name))
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
        /// Edit the contact using name and number.
        /// </summary>
        public void EditContactByNameOrNumber()
        {
            if (this._service.IsContactEmpty())
            {
                this._consoleActivity.PrintInConsole("!!No Contact found!!");
                this._consoleActivity.WaitInConsole();
                return;
            }

            string? choiceSearch = this.ChooseNameOrNumber();
            ContactInfo? contact = null;
            if (int.TryParse(choiceSearch, out int choiceSearchNumber))
            {
                if (choiceSearchNumber == (int)SearchType.ByName)
                {
                    string? name = this._consoleActivity.GetInputFromUser("name to edit");
                    if (name != null)
                    {
                        contact = this._service.SearchContact(name, (int)SearchUsingConstant.SearchUsingName);
                    }
                }
                else if (choiceSearchNumber == (int)SearchType.ByNumber)
                {
                    string? number = this._consoleActivity.GetInputFromUser("number to edit");
                    if (number != null)
                    {
                        contact = this._service.SearchContact(number, (int)SearchUsingConstant.SearchUsingNumber);
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

            this._consoleActivity.ClearConsole();
            this._consoleActivity.PrintInConsole("Contact Details to be edited:");
            this._consoleActivity.PrintContactInConsole(contact);
            string? contactSearch = this.GetFieldNameToEdit();
            if (int.TryParse(contactSearch, out int contactSearchNumber))
            {
                if (contactSearchNumber == (int)ContactFieldConstant.Name)
                {
                    string? newName = this._consoleActivity.GetInputFromUser("new name");
                    if (this._service.IsNamePresent(newName))
                    {
                        this._consoleActivity.PrintInConsole("Name already present in contact!!");
                        this._consoleActivity.WaitInConsole();
                        return;
                    }

                    if (contact != null && this._service.UpdateContact(newName, contact.GetNumber(), contact.GetEmail(), contact.GetNotes(), contact) && !string.IsNullOrWhiteSpace(newName))
                    {
                        this.ContactUpdateSuccess(contact);
                    }
                    else
                    {
                        this.ContactUpdateFailed("Name");
                    }
                }
                else if (contactSearchNumber == (int)ContactFieldConstant.Number)
                {
                    string? newNumber = this._consoleActivity.GetInputFromUser("new number");
                    if (this._service.IsNumberPresent(newNumber))
                    {
                        this._consoleActivity.PrintInConsole("Number already present in contact!!");
                        this._consoleActivity.WaitInConsole();
                        return;
                    }

                    if (contact != null && Helper.IsValidNumber(newNumber) && this._service.UpdateContact(contact.GetName(), newNumber, contact.GetEmail(), contact.GetNotes(), contact))
                    {
                        this.ContactUpdateSuccess(contact);
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
                        this.ContactUpdateSuccess(contact);
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
                        this.ContactUpdateSuccess(contact);
                    }
                    else
                    {
                        this.ContactUpdateFailed("Notes");
                    }
                }
                else
                {
                    this._consoleActivity.PrintInConsole("Invalid input!!");
                }
            }
            else
            {
                this._consoleActivity.PrintInConsole("Invalid input!!");
            }
        }

        /// <summary>
        /// Get the option from user to edit using name and number.
        /// </summary>
        /// <returns>Returns the option selected by the user</returns>
        public string? GetFieldNameToEdit()
        {
            string? contactSearchOption = this._consoleActivity.GetInputFromUserChooseField();
            return contactSearchOption;
        }

        /// <summary>
        /// Show option available for searching (name and number).
        /// </summary>
        /// <returns>Return 1 or 2 as string</returns>
        public string? ChooseNameOrNumber()
        {
            string? choiceSearch = this._consoleActivity.GetInputFromUserChooseNameOrNumber();
            return choiceSearch;
        }

        /// <summary>
        /// Show the contact after the contact.
        /// </summary>
        /// <param name="contact">Updated contact info</param>
        public void ContactUpdateSuccess(ContactInfo contact)
        {
            this._consoleActivity.ClearConsole();
            this._consoleActivity.PrintInConsole("Contact Updated Successfully!!");
            this._consoleActivity.PrintContactInConsole(contact);
        }

        /// <summary>
        /// Show contact update status failed.
        /// </summary>
        /// <param name="field">The field that get failed to update</param>
        public void ContactUpdateFailed(string? field)
        {
            this._consoleActivity.PrintInConsole($"Invalid {field}!!");
            this._consoleActivity.WaitInConsole();
        }
    }
}
