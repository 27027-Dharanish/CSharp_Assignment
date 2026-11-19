namespace DisplayApp
{
    /// <summary>
    /// Writes data in console in specialized color format.
    /// </summary>
    public class ConsoleWriter
    {
        /// <summary>
        /// Displays the message in console in colored format.
        /// </summary>
        /// <param name="message">Message to be displayed in console.</param>
        /// <param name="color">Color in which the message should be displayed.</param>
        public void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
