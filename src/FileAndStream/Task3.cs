using System.Text;
using FileAndStream.View;

namespace FileAndStream
{
    /// <summary>
    /// Handle task3 .
    /// </summary>
    public class Task3
    {
        /// <summary>
        /// Enhanced and modified version of task3.
        /// </summary>
        public void HandleTask3()
        {
            string path = "test.txt";
            string data = "This is some test data";
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                fileStream.Write(buffer, 0, buffer.Length);
            }

            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Console.Write(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                }

                ConsoleActivity.WaitInConsole();
            }
        }
    }
}
