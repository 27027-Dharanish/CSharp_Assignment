using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Models;
using Assignment1.Persistence;

namespace Assignment1.Services
{
    /// <summary>
    /// Contact manager
    /// </summary>
    internal class ContactManager
    {
        private Repository _repo = new Repository();

        /// <summary>
        /// addcontact
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="number">nmber</param>
        /// <param name="email">email</param>
        /// <param name="notes">notes</param>
        public void AddContact(string? name, string? number, string? email, string? notes)
        {
            ContactInfo newContact = new ContactInfo();
            newContact.SetId();
            newContact.SetName(name);
            newContact.SetNumber(number);
            newContact.SetEmail(email);
            newContact.SetNotes(notes);
            this._repo.AddNewContact(newContact);
            Console.WriteLine("Contact added successfully.");
        }

        /// <summary>
        /// search
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="option">optio</param>
        /// <returns>contact</returns>
        public ContactInfo? SearchContact(string name, string? option)
        {
            ContactInfo? contact;
            if (option != "0")
            {
                contact = this._repo.FindByNumber(name);
            }
            else
            {
                contact = this._repo.FindByName(name);
            }

            if (contact != null)
            {
                return contact;
            }
            else
            {
                Console.WriteLine("Contact not found.");
                return null;
            }
        }

        /// <summary>
        /// getcontact
        /// </summary>
        /// <returns>get all comtact</returns>
        public List<ContactInfo> GetAllContacts()
        {
            List<ContactInfo> contacts = this._repo.GetContact();
            contacts.Sort((x, y) => string.Compare(x.GetName(), y.GetName(), StringComparison.OrdinalIgnoreCase));
            return contacts;
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="name">name</param>
        public void DeleteContact(string? name)
        {
            ContactInfo? contact = this._repo.FindByName(name);
            if (contact != null && this._repo.RemoveContact(contact))
            {
                Console.WriteLine();
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="targetName">newname</param>
        /// <param name="newNumber">number</param>
        /// <param name="newEmail">emial</param>
        /// <param name="newNotes">notes</param>
        /// <param name="contact">note</param>
        public void UpdateContact(string? targetName, string? newNumber, string? newEmail, string? newNotes, ContactInfo? contact)
        {
            if (contact != null)
            {
                contact.SetName(targetName);
                contact.SetNumber(newNumber);
                contact.SetEmail(newEmail);
                contact.SetNotes(newNotes);
                Console.WriteLine("Contact updated successfully!!");
            }
            else
            {
                Console.WriteLine("Contact not found!!");
            }
        }
    }
}