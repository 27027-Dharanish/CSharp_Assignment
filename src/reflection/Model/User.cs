using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    /// <summary>Represents a user account with basic profile data.</summary>
    public class User
    {
        /// <summary>Initializes a new instance of the <see cref="User"/> class.</summary>
        /// <param name="username">The profile name of the user.</param>
        /// <param name="age">The age of the user.</param>
        /// <param name="isAdmin">The administrator status flag.</param>
        public User(string username, int age, bool isAdmin)
        {
            this.Username = username;
            this.Age = age;
            this.IsAdmin = isAdmin;
        }

        /// <summary>Gets or sets the profile name of the user.</summary>
        /// <value>The profile name of the user.</value>
        public string Username { get; set; }

        /// <summary>Gets or sets the age of the user.</summary>
        /// <value>The age of the user.</value>
        public int Age { get; set; }

        /// <summary>Gets or sets a value indicating whether the user has administrator privileges.</summary>
        /// <value>A value indicating whether the user has administrator privileges.</value>
        public bool IsAdmin { get; set; }

        /// <summary>Prints a greeting message to the console.</summary>
        public void SayHello()
        {
            Console.WriteLine($"Hello, my username is {this.Username}!");
        }

        /// <summary>Generates a comprehensive summary string of the profile status.</summary>
        /// <returns>A string containing user details.</returns>
        public string GetProfileSummary()
        {
            return $"User Profile: {this.Username}, Age: {this.Age}, Admin Status: {this.IsAdmin}";
        }
    }
}
