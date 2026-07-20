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
    /// Repository for storing the list of ContactInfo
    /// </summary>
    internal class Repository
    {
        private List<ContactInfo> _contacts = new List<ContactInfo>();

        /// <summary>
        /// Add new contact to the _contacts
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
        /// Return the list of contact available
        /// </summary>
        /// <returns>List of contact</returns>
        public List<ContactInfo> GetContact()
        {
            // Used to list to create a new instance in the memory
            return this._contacts.ToList();
        }

        /// <summary>
        /// Find the guid mapped contact
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>The guid matched contact</returns>
        public ContactInfo? FindById(Guid id)
        {
            return this._contacts.Find(contact => contact.GetId() == id);
        }

        /// <summary>
        /// Find the contact in _contacts using name
        /// </summary>
        /// <param name="name">name to be searched</param>
        /// <returns>Contactinfo of the given name</returns>
        public ContactInfo? FindByName(string name)
        {
            return this._contacts.Find(contact => contact != null && string.Equals(contact.GetName(), name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Find the contact using the number
        /// </summary>
        /// <param name="number">num</param>
        /// <returns>return the contactinfo using the number</returns>
        public ContactInfo? FindByNumber(string? number)
        {
            return this._contacts.Find(contact => string.Equals(contact.GetNumber(), number, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Remove the contact from the _contacts
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>return whether the contact removed or not</returns>
        public bool RemoveContact(ContactInfo contact)
        {
            return this._contacts.Remove(contact);
        }
    }
}
