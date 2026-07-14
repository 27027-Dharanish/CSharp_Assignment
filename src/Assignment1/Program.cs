using System.Runtime.CompilerServices;

namespace Assignments
{
    /// <summary>
    /// First assignment
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Program function
        /// </summary>
        /// <param name="args">Welcome</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Contact Manager Application");
            string? userInput = null;
            List<string?[]> contactDetails = new List<string?[]>();
            do
            {
                Console.WriteLine();
                Console.WriteLine("[A]dd new contact");
                Console.WriteLine("[V]iew contact");
                Console.WriteLine("[S]earch contact");
                Console.WriteLine("[D]elete contact");
                Console.WriteLine("[U]pdate or edit contact");
                Console.WriteLine("[E]xit");
                Console.WriteLine();
                Console.WriteLine("Enter the Option:");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "a":
                    case "A":
                        contactDetails.Add(AddNewContact());
                        break;
                    case "v":
                    case "V":
                        ViewContact(contactDetails);
                        break;
                    case "D":
                    case "d":
                        DeleteContact(contactDetails);
                        break;
                    case "S":
                    case "s":
                        SearchContact(contactDetails);
                        break;
                    case "u":
                    case "U":
                        EditContact(contactDetails);
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }
            while (userInput != "e" && userInput != "E");
        }
        private static string[] AddNewContact()
        {
            string?[] contactTemp = new string?[4];
            Console.WriteLine("New Contact Adding:");
            Console.WriteLine("Enter the name: ");
            contactTemp[0] = Console.ReadLine();
            Console.WriteLine("Enter the number: ");
            contactTemp[1] = Console.ReadLine();
            Console.WriteLine("Ente the email: ");
            contactTemp[2] = Console.ReadLine();
            Console.WriteLine("Enter any notes: ");
            contactTemp[3] = Console.ReadLine();
            return contactTemp;
        }
        private static void ViewContact(List<string[]> contactDetails)
        {
            Console.WriteLine("The contact are :");
            if (contactDetails.Count == 0)
            {
                Console.WriteLine("No contact are there to display!!");
                return;
            }
            for (int i = 0; i < contactDetails.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Name: " + contactDetails[i][0]);
                Console.WriteLine("Phone: " + contactDetails[i][1]);
                Console.WriteLine("Email: " + contactDetails[i][2]);
                Console.WriteLine("Notes: " + contactDetails[i][3]);
                Console.WriteLine("================================================");
            }
        }
        private static void SearchContact(List<string?[]> contactDetails)
        {
            Console.WriteLine("Enter the Contact Name: ");
            string? searchName = Console.ReadLine();
            for (int i = 0; i < contactDetails.Count; i++)
            {
                if (string.Equals(searchName, contactDetails[i][0], StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Contact {searchName} found");
                    Console.WriteLine();
                    Console.WriteLine("Phone: " + contactDetails[i][1]);
                    Console.WriteLine("Email: " + contactDetails[i][2]);
                    Console.WriteLine("Notes: " + contactDetails[i][3]);
                    Console.WriteLine();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("User not found!!");
                }
            }
        }
        private static void DeleteContact(List<string?[]> contactDetails)
        {
            Console.WriteLine();
            Console.WriteLine("Enter the name to delete: ");
            string? nameDelete = Console.ReadLine();
            int removedCount = contactDetails.RemoveAll(arr =>
            arr != null && arr.Length > 0 &&
            string.Equals(arr[0], nameDelete, StringComparison.OrdinalIgnoreCase));
            if (removedCount == 0)
            {
                Console.WriteLine("No name is found");
            }
            else
            {
                Console.WriteLine($"{nameDelete} contact removed.");
            }
        }
        private static void EditContact(List<string?[]> contactDetails)
        {
            Console.WriteLine();
            Console.WriteLine("Enter the contact name to edit: ");
            string? nameEdit = Console.ReadLine();
            for (int i = 0; i < contactDetails.Count; i++)
            {
                if (string.Equals(nameEdit, contactDetails[i][0], StringComparison.OrdinalIgnoreCase))
                {
                    int choice;
                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Contact {nameEdit} found");
                        Console.WriteLine();
                        Console.WriteLine("Select the field to edit!!");
                        Console.WriteLine();
                        Console.WriteLine("1.Name");
                        Console.WriteLine("2.Phone Number");
                        Console.WriteLine("3.Email");
                        Console.WriteLine("4.Notes");
                        Console.WriteLine("5.Delete contact");
                        Console.WriteLine("Press 0 for Exit");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Enter the number: ");
                        choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("Enter the new name: ");
                                string? newName = Console.ReadLine();
                                contactDetails[i][0] = newName;
                                break;
                            case 2:
                                Console.WriteLine("Enter the new number: ");
                                string? newNumber = Console.ReadLine();
                                contactDetails[i][1] = newNumber;
                                break;
                            case 3:
                                Console.WriteLine("Enter the new email: ");
                                string? newEmail = Console.ReadLine();
                                contactDetails[i][2] = newEmail;
                                break;
                            case 4:
                                Console.WriteLine("Enter the new Notes: ");
                                string? newNotes = Console.ReadLine();
                                contactDetails[i][3] = newNotes;
                                break;
                            case 5:
                                DeleteContact(contactDetails);
                                break;
                            default:
                                Console.WriteLine("Invalid Option");
                                break;
                        }
                    }
                    while (choice != 0);
                }
                else
                {
                    Console.WriteLine("User not found!!");
                }
            }
        }
    }
}