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
    /// Contact manager act as service
    /// </summary>
    internal class ContactManager
    {
        private Repository _repo = new Repository();

        /// <summary>
        /// add the new contact to the repo
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="number">number</param>
        /// <param name="email">email</param>
        /// <param name="notes">notes</param>
        /// <returns>return whether contact added successfully or not</returns>
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
        /// search the contact form the repo
        /// </summary>
        /// <param name="content">name</param>
        /// <param name="option">option</param>
        /// <returns>matched contact</returns>
        public ContactInfo? SearchContact(string content, string? option)
        {
            ContactInfo? contact = null;
            if (option == "SearchUsingName")
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
        /// get all the contact form the repo
        /// </summary>
        /// <returns>get all contact</returns>
        public List<ContactInfo> GetAllContacts()
        {
            List<ContactInfo> contacts = this._repo.GetContact();
            contacts.Sort((x, y) => string.Compare(x.GetName(), y.GetName(), StringComparison.OrdinalIgnoreCase));
            return contacts;
        }

        /// <summary>
        /// delete the contact using name
        /// </summary>
        /// <param name="name">name use for delete</param>
        /// <returns>bool value</returns>
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
        /// Update the contact using get and set
        /// </summary>
        /// <param name="targetName">newname</param>
        /// <param name="newNumber">number</param>
        /// <param name="newEmail">emial</param>
        /// <param name="newNotes">notes</param>
        /// <param name="contact">note</param>
        /// <returns>return if contact updated or not</returns>
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
    }
}