using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Models
{
    /// <summary>
    /// Contact Info
    /// </summary>
    internal class ContactInfo
    {
            private Guid? _id;
            private string? _name;
            private string? _number;
            private string? _email;
            private string? _notes;

            /// <summary>
            /// set id
            /// </summary>
            public void SetId()
            {
                this._id = Guid.NewGuid();
            }

            /// <summary>
            /// get id
            /// </summary>
            /// <returns>id</returns>
            public Guid? GetId()
            {
                return this._id;
            }

            /// <summary>
            /// name
            /// </summary>
            /// <returns>return name</returns>
            public string? GetName()
            {
                return this._name;
            }

            /// <summary>
            /// setname
            /// </summary>
            /// <param name="value">name</param>
            public void SetName(string? value)
            {
                this._name = value;
            }

            /// <summary>
            /// setphone
            /// </summary>
            /// <param name="value">number</param>
            public void SetNumber(string? value)
            {
                this._number = value;
            }

            /// <summary>
            /// getnumber
            /// </summary>
            /// <returns>number</returns>
            public string? GetNumber()
            {
                return this._number;
            }

            /// <summary>
            /// setemail
            /// </summary>
            /// <param name="value">email</param>
            public void SetEmail(string? value)
            {
                this._email = value;
            }

            /// <summary>
            /// email
            /// </summary>
            /// <returns>getemail</returns>
            public string? GetEmail()
            {
                return this._email;
            }

            /// <summary>
            /// setNotes
            /// </summary>
            /// <param name="value">notes</param>
            public void SetNotes(string? value)
            {
                this._notes = value;
            }

            /// <summary>
            /// getnotes
            /// </summary>
            /// <returns>notes</returns>
            public string? GetNotes()
            {
                return this._notes;
            }
    }
}
