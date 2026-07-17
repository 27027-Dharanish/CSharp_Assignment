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
    /// Repo for stroing values contains the list of ContactInfo
    /// </summary>
    internal class Repository
    {
        private List<ContactInfo> _contacts = new List<ContactInfo>();

        /// <summary>
        /// add new contact to the _contacts
        /// </summary>
        /// <param name="newContact">new contact items</param>
        public void AddNewContact(ContactInfo newContact)
        {
            this._contacts.Add(newContact);
        }

        /// <summary>
        /// Get the contact from _contacts
        /// </summary>
        /// <returns>list</returns>
        public List<ContactInfo> GetContact()
        {
            // Used to list to create a new instance in the memory
            return this._contacts.ToList();
        }

        /// <summary>
        /// Find the guid mapped contact
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>contact</returns>
        public ContactInfo? FindById(Guid id)
        {
            return this._contacts.Find(c => c.GetId() == id);
        }

        /// <summary>
        /// Find the contact in _contacts by name
        /// </summary>
        /// <param name="name">nma</param>
        /// <returns>name</returns>
        public ContactInfo? FindByName(string name)
        {
            return this._contacts.Find(c => c != null && string.Equals(c.GetName(), name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// find the contact using the number
        /// </summary>
        /// <param name="number">num</param>
        /// <returns>contact</returns>
        public ContactInfo? FindByNumber(string? number)
        {
            return this._contacts.Find(c => string.Equals(c.GetNumber(), number, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// remove the contact from the _contacts
        /// </summary>
        /// <param name="contact">contact</param>
        /// <returns>co</returns>
        public bool RemoveContact(ContactInfo contact)
        {
            return this._contacts.Remove(contact);
        }
    }
}
