namespace GreetingsApp
{
    /// <summary>
    /// Provides the greeting messages.
    /// </summary>
    public class Greet
    {
        /// <summary>
        /// Returns the greeting to be sent in morning.
        /// </summary>
        /// <param name="name">Name of the person to greet.</param>
        /// <returns>Morning greeting message.</returns>
        public string MorningGreet(string name)
        {
            return $"Good Morning! " + $"{name}";
        }

        /// <summary>
        /// Returns the greeting to be sent in afternoon.
        /// </summary>
        /// <param name="name">Name of the person to greet.</param>
        /// <returns>Afternoon greeting message.</returns>
        public string AfternoonGreet(string name)
        {
            return $"Good Afternoon! " + $"{name}";
        }

        /// <summary>
        /// Returns the greeting to be sent in evening.
        /// </summary>
        /// <param name="name">Name of the person to greet.</param>
        /// <returns>Evening greeting message.</returns>
        public string EveningGreet(string name)
        {
            return $"Good Evening! " + $"{name}";
        }

        /// <summary>
        /// Returns the greeting to be sent in night.
        /// </summary>
        /// <param name="name">Name of the person to greet.</param>
        /// <returns>Night greeting message.</returns>
        public string NightGreet(string name)
        {
            return $"Good Night! " + $"{name}";
        }
    }
}
