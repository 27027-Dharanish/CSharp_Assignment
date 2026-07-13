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
                        // DeleteContact();
                        break;
                    case "S":
                    case "s":
                        SearchContact(contactDetails);
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
        private static void SearchContact(List<string[]> contactDetails)
        {
            Console.WriteLine("Enter the Contact Name: ");
            string? searchName = Console.ReadLine();
            for (int i = 0; i < contactDetails.Count; i++)
            {
                if (searchName == contactDetails[i][0])
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
    }
}