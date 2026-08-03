using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Models
{
    /// <summary>
    /// The Model of storing the contact information of the user.
    /// </summary>
    internal class ContactInfo
    {
            private Guid? _id;
            private string? _name;
            private string? _number;
            private string? _email;
            private string? _notes;

            /// <summary>
            /// Set the guid for each contact.
            /// </summary>
            public void SetId()
            {
                this._id = Guid.NewGuid();
            }

            /// <summary>
            /// Return the guid.
            /// </summary>
            /// <returns>Id</returns>
            public Guid? GetId()
            {
                return this._id;
            }

            /// <summary>
            /// Return the name.
            /// </summary>
            /// <returns>Return name</returns>
            public string? GetName()
            {
                return this._name;
            }

            /// <summary>
            /// Set the name of the contact in contactinfo.
            /// </summary>
            /// <param name="value">name</param>
            public void SetName(string? value)
            {
                this._name = value;
            }

            /// <summary>
            /// Setphone of the user in contactinfo.
            /// </summary>
            /// <param name="value">Number</param>
            public void SetNumber(string? value)
            {
                this._number = value;
            }

            /// <summary>
            /// Get the contact number.
            /// </summary>
            /// <returns>Number</returns>
            public string? GetNumber()
            {
                return this._number;
            }

            /// <summary>
            /// Set the email of the contact in contact info.
            /// </summary>
            /// <param name="value">Email</param>
            public void SetEmail(string? value)
            {
                this._email = value;
            }

            /// <summary>
            /// Return the email address.
            /// </summary>
            /// <returns>Return the Email</returns>
            public string? GetEmail()
            {
                return this._email;
            }

            /// <summary>
            /// Set the notes for the contact.
            /// </summary>
            /// <param name="value">Notes</param>
            public void SetNotes(string? value)
            {
                this._notes = value;
            }

            /// <summary>
            /// Get the notes of the particular contact.
            /// </summary>
            /// <returns>notes</returns>
            public string? GetNotes()
            {
                return this._notes;
            }
    }
}
