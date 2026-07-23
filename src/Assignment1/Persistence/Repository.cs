using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Controller;
using Assignment1.Models;
using Assignment1.Services;

namespace Assignment1.Persistence
{
    /// <summary>
    /// Provides a centralized data repository for storing, retrieving contact info entities
    /// </summary>
    internal class Repository
    {
        private List<ContactInfo> _contacts = new List<ContactInfo>();

        /// <summary>
        /// Add new contact.
        /// </summary>
        /// <param name="newContact">new contact items</param>
        /// <returns>Return whether contact added or not</returns>
        public bool AddNewContact(ContactInfo newContact)
        {
            if (newContact != null)
            {
                this._contacts.Add(newContact);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Return the list of contact available.
        /// </summary>
        /// <returns>List of contact</returns>
        public List<ContactInfo> GetContact()
        {
            List<ContactInfo> contactList = new List<ContactInfo>();
            foreach (var contact in this._contacts)
            {
                ContactInfo copy = new ContactInfo();
                copy.SetName(contact.GetName());
                copy.SetNumber(contact.GetNumber());
                copy.SetEmail(contact.GetEmail());
                copy.SetNotes(contact.GetNotes());
                contactList.Add(copy);
            }

            return contactList;
        }

        /// <summary>
        /// Find the guid mapped contact.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>The guid matched contact</returns>
        public ContactInfo? FindById(Guid id)
        {
            return this._contacts.Find(contact => contact.GetId() == id);
        }

        /// <summary>
        /// Find the contact using name.
        /// </summary>
        /// <param name="name">Name to be searched</param>
        /// <returns>Contactinfo of the given name</returns>
        public ContactInfo? FindByName(string name)
        {
            return this._contacts.Find(contact => contact != null && string.Equals(contact.GetName(), name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Find the contact using the number.
        /// </summary>
        /// <param name="number">Number used for searching</param>
        /// <returns>Return the contactinfo</returns>
        public ContactInfo? FindByNumber(string? number)
        {
            return this._contacts.Find(contact => string.Equals(contact.GetNumber(), number, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Remove the contact.
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>Return whether the contact removed or not</returns>
        public bool RemoveContact(ContactInfo contact)
        {
            return this._contacts.Remove(contact);
        }
    }
}
