using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

namespace Assignments
{
    /// <summary>
    /// Represents the main entry point for the application and handles initial setup.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the program and start the musical controller.
        /// </summary>
        public static void Main()
        {
            List<string> notes = new List<string>();
            string? userChoice;
            do
            {
                PrintNotes(notes);
                Console.WriteLine("Enter the below notes to play music : ");
                Console.WriteLine("A A# B C C# D D# E F F# G G#");
                Console.Write("Enter the note : ");
                userChoice = Console.ReadLine();
                PlayBeep(userChoice);
                if (userChoice != null)
                {
                    notes.Add(userChoice);
                }
            }
            while (true);
        }

        /// <summary>
        /// Play beep based on user input.
        /// </summary>
        /// <param name="userChoice">User input.</param>
        public static void PlayBeep(string? userChoice)
        {
            if (userChoice == null)
            {
                return;
            }

            for (int i = 0; i < userChoice.Length; i++)
            {
                string s = userChoice[i].ToString();
                if (i < userChoice.Length)
                {
                    if (userChoice[i] + 1 == '#')
                    {
                        s += "#";
                    }
                }

                switch (s)
                {
                    case "C":
                        Console.Beep(261, 50);
                        break;
                    case "C#":
                        Console.Beep(277, 100);
                        break;
                    case "D":
                        Console.Beep(293, 150);
                        break;
                    case "D#":
                        Console.Beep(311, 200);
                        break;
                    case "E":
                        Console.Beep(329, 220);
                        break;
                    case "F":
                        Console.Beep(349, 260);
                        break;
                    case "F#":
                        Console.Beep(369, 300);
                        break;
                    case "G":
                        Console.Beep(392, 330);
                        break;
                    case "G#":
                        Console.Beep(415, 360);
                        break;
                    case "A":
                        Console.Beep(440, 400);
                        break;
                    case "A#":
                        Console.Beep(466, 420);
                        break;
                    case "B":
                        Console.Beep(493, 460);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Print the list of notes present.
        /// </summary>
        /// <param name="notes">List notes.</param>
        public static void PrintNotes(List<string> notes)
        {
            Console.WriteLine("Previous Notes : ");
            if (notes.Count == 0)
            {
                Console.WriteLine("None..");
                return;
            }

            foreach (string note in notes)
            {
                Console.WriteLine(note);
            }

            Console.WriteLine();
        }
    }
}