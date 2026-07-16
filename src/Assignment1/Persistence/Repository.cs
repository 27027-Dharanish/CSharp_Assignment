using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Models;
using Assignment1.Services;

namespace Assignment1.Persistence
{
    /// <summary>
    /// Repo for stroing values
    /// </summary>
    internal class Repository
    {
        private List<ContactInfo> _contacts = new List<ContactInfo>();

        /// <summary>
        /// add new contact
        /// </summary>
        /// <param name="newContact">new contact items</param>
        public void AddNewContact(ContactInfo newContact)
        {
            this._contacts.Add(newContact);
        }

        /// <summary>
        /// get contact
        /// </summary>
        /// <returns>list</returns>
        public List<ContactInfo> GetContact()
        {
            return this._contacts;
        }

        /// <summary>
        /// guid
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>contact</returns>
        public ContactInfo? FindById(Guid id)
        {
            return this._contacts.Find(c => c.GetId() == id);
        }

        /// <summary>
        /// findname
        /// </summary>
        /// <param name="name">nma</param>
        /// <returns>name</returns>
        public ContactInfo? FindByName(string name)
        {
            return this._contacts.Find(c => c != null && string.Equals(c.GetName(), name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// number
        /// </summary>
        /// <param name="number">num</param>
        /// <returns>contact</returns>
        public ContactInfo? FindByNumber(string? number)
        {
            return this._contacts.Find(c => string.Equals(c.GetNumber(), number, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// remive
        /// </summary>
        /// <param name="contact">contact</param>
        /// <returns>co</returns>
        public bool RemoveContact(ContactInfo contact)
        {
            return this._contacts.Remove(contact);
        }
    }
}
