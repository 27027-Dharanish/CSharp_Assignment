using System.Security.Cryptography.X509Certificates;
using MemoryManagement.View;

namespace MemoryManagement
{
    /// <summary>
    /// Class to demonstrate use of IDisposable interface and using statement.
    /// </summary>
    public class IDisposableDemo
    {
        private string _filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DemoFile.txt");

        /// <summary>
        /// Execute task 4 to show the use case of dispose method and using keyword in file stream.
        /// </summary>
        public void ExecuteTask4()
        {
            ConsoleActivity.ShowHeader("Task 4");
            using (Writer writer = new Writer(this._filePath))
            {
                writer.Write("This is 1st line");
                ConsoleActivity.PrintInConsole("Data wrote in file successfully!\n");
            }

            using (Reader reader = new Reader(this._filePath))
            {
                ConsoleActivity.PrintInConsole("The content in file is : ");
                string? content = reader.ReadOneLine();
                if (content != null)
                {
                    ConsoleActivity.PrintAndWait(content);
                }
                else
                {
                    ConsoleActivity.PrintAndWait("No content in file.");
                }
            }
        }

        /// <summary>
        /// Provide the write functionality.
        /// </summary>
        public class Writer : IDisposable
        {
            private StreamWriter _writer;

            /// <summary>
            /// Initializes a new instance of the <see cref="Writer"/> class.
            /// </summary>
            /// <param name="filePath">The actual file path where write operation take place.</param>
            public Writer(string filePath)
            {
                this._writer = new StreamWriter(filePath);
            }

            /// <summary>
            /// Write the content in file using stream writer.
            /// </summary>
            /// <param name="content">Content to be write in file.</param>
            public void Write(string content)
            {
                this._writer.Write(content);
            }

            /// <summary>
            /// The dispose of unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                this._writer.Dispose();
            }
        }

        /// <summary>
        /// Provide the read functionality.
        /// </summary>
        public class Reader
            : IDisposable
        {
            private StreamReader _reader;

            /// <summary>
            /// Initializes a new instance of the <see cref="Reader"/> class.
            /// </summary>
            /// <param name="filePath">The actual file path where read operation take place.</param>
            public Reader(string filePath)
            {
                this._reader = new StreamReader(filePath);
            }

            /// <summary>
            /// Read a line from the file.
            /// </summary>
            /// <returns>The line read from the file.</returns>
            public string? ReadOneLine()
            {
                return this._reader.ReadLine();
            }

            /// <summary>
            /// Dispose the unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                this._reader.Dispose();
            }
        }
    }
}
