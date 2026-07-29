using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Controller;
using Assignment1.Models;
using Assignment1.Persistence;

namespace Assignment1.Services
{
    /// <summary>
    /// Coordinate the business logic for the contact manager.
    /// </summary>
    internal class ContactManager
    {
        private Repository _repo = new Repository();

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
        /// Add the new contact.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="number">Number</param>
        /// <param name="email">Email</param>
        /// <param name="notes">Notes</param>
        /// <returns>Return whether contact added successfully or not</returns>
        public bool CreateNewContact(string? name, string? number, string? email, string? notes)
        {
            ContactInfo newContact = new ContactInfo();
            newContact.SetId();
            newContact.SetName(name);
            newContact.SetNumber(number);
            newContact.SetEmail(email);
            newContact.SetNotes(notes);
            if (this._repo.AddNewContact(newContact))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Search the contact.
        /// </summary>
        /// <param name="content">name</param>
        /// <param name="option">option</param>
        /// <returns>Matched contact</returns>
        public ContactInfo? SearchContact(string content, int option)
        {
            ContactInfo? contact;
            if (option == (int)SearchUsingConstant.SearchUsingName)
            {
                contact = this._repo.FindByName(content);
            }
            else
            {
                contact = this._repo.FindByNumber(content);
            }

            return contact;
        }

        /// <summary>
        /// Get all the contact.
        /// </summary>
        /// <returns>Return all contact</returns>
        public List<ContactInfo> GetAllContacts()
        {
            List<ContactInfo> contacts = this._repo.GetContact();
            contacts.Sort((x, y) => string.Compare(x.GetName(), y.GetName(), StringComparison.OrdinalIgnoreCase));
            return contacts;
        }

        /// <summary>
        /// Delete the contact using name.
        /// </summary>
        /// <param name="name">Name use for delete</param>
        /// <returns>Return if contact deleted or not</returns>
        public bool DeleteContact(string? name)
        {
            if (name != null)
            {
                ContactInfo? contact = this._repo.FindByName(name);
                if (contact != null && this._repo.RemoveContact(contact))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Update the contact using get and set.
        /// </summary>
        /// <param name="targetName">new name</param>
        /// <param name="newNumber">new number</param>
        /// <param name="newEmail">new email</param>
        /// <param name="newNotes">new notes</param>
        /// <param name="contact">contact to be updated</param>
        /// <returns>Return if contact updated or not</returns>
        public bool UpdateContact(string? targetName, string? newNumber, string? newEmail, string? newNotes, ContactInfo? contact)
        {
            if (contact != null)
            {
                contact.SetName(targetName);
                contact.SetNumber(newNumber);
                contact.SetEmail(newEmail);
                contact.SetNotes(newNotes);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check whether the contact is empty or not.
        /// </summary>
        /// <returns>Return true if contact is empty or false</returns>
        public bool IsContactEmpty()
        {
            if (this._repo.GetContactCount() == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if the name already present in contact list.
        /// </summary>
        /// <param name="name">name to be searched</param>
        /// <returns>Return true if name exist</returns>
        public bool IsNamePresent(string? name)
        {
            return this._repo.CheckIfNameExist(name);
        }

        /// <summary>
        /// Check if the number already present in contact list.
        /// </summary>
        /// <param name="number">Number to be searched</param>
        /// <returns>Return true if number exist</returns>
        public bool IsNumberPresent(string? number)
        {
            return this._repo.CheckIfNumberExist(number);
        }
    }
}