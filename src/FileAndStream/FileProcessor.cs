using System.Diagnostics;
using System.Text;
using FileAndStream.View;

namespace FileAndStream
{
    /// <summary>
    /// Handle file process like creating file, reading and writing using stream.
    /// </summary>
    public class FileProcessor
    {
        private readonly string _fileName = "BigDataFile.txt";
        private readonly string _fileNameContainUpperCaseContent = "UpperCase";

        /// <summary>
        /// Generate the file of size one giga byte.
        /// </summary>
        public void GenerateOneGBFile()
        {
            ConsoleActivity.ShowHeader("Create 1GB file");
            long targetFileSize = 1024L * 1024L * 1024L;
            string sampleData = $"This is a sample text just to fill up the space.{Environment.NewLine}";
            Console.WriteLine("Writing data into the file. It may take some time to complete...");
            using (StreamWriter writer = new StreamWriter(this._fileName, false))
            {
                while (writer.BaseStream.Length < targetFileSize)
                {
                    writer.Write(sampleData);
                }
            }

            Console.WriteLine("Successfully created one GB text file...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Read the content of the file and print the time taken to read the file.
        /// </summary>
        public void ReadFileUsingFileStream()
        {
            ConsoleActivity.ShowHeader("Reading file");
            ConsoleActivity.PrintInConsole("Reading the 1GB file in chunks....");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using (FileStream reader = new FileStream(this._fileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[64 * 1024];
                    while (reader.Read(buffer, 0, buffer.Length) > 0)
                    {
                        continue;
                    }

                    stopwatch.Stop();
                    ConsoleActivity.PrintAndWait("Completed reading in file\nTime taken to read the content of the file is : " + stopwatch.ElapsedMilliseconds + " ms");
                }
            }
            catch (OutOfMemoryException ex)
            {
                ConsoleActivity.PrintAndWait(ex.Message);
            }
            catch (IOException ex)
            {
                ConsoleActivity.PrintAndWait(ex.Message);
            }
        }

        /// <summary>
        /// Read the file using the buffered stream reader and print the time taken to read the content of the file.
        /// </summary>
        public void ReadFileUsingBufferedStream()
        {
            ConsoleActivity.ShowHeader("Read file using buffered stream");
            ConsoleActivity.PrintInConsole("Reading the 1GB file....");
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                using (FileStream fs = File.Open(this._fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs, 64 * 1024))
                {
                    byte[] buffer = new byte[64 * 1024];
                    while (bs.Read(buffer, 0, buffer.Length) > 0)
                    {
                        continue;
                    }
                }

                stopwatch.Stop();
                ConsoleActivity.PrintAndWait("Completed reading in file\nTime taken to read the content of the file is : " + stopwatch.ElapsedMilliseconds + " ms");
            }
            catch (OutOfMemoryException ex)
            {
                ConsoleActivity.PrintAndWait(ex.Message);
            }
            catch (IOException ex)
            {
                ConsoleActivity.PrintAndWait(ex.Message);
            }
        }

        /// <summary>
        /// Convert the content from the file to uppercase.
        /// </summary>
        public void ConvertUpperCase()
        {
            ConsoleActivity.ShowHeader("Convert file content to uppercase");
            ConsoleActivity.PrintInConsole("Processing data into uppercase. It may take some time...");
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                using (FileStream originalFile = File.Open(this._fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (FileStream processedFile = File.Open(this._fileNameContainUpperCaseContent, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                using (BufferedStream originalBufferedReader = new BufferedStream(originalFile, 64 * 1024))
                using (BufferedStream processedBufferWriter = new BufferedStream(processedFile, 64 * 1024))
                {
                    byte[] buffer = new byte[64 * 1024];
                    while (originalBufferedReader.Read(buffer, 0, buffer.Length) > 0)
                    {
                        string? content = Encoding.UTF8.GetString(buffer);
                        buffer = Encoding.UTF8.GetBytes(content.ToUpper());
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            memoryStream.Write(buffer, 0, buffer.Length);
                            memoryStream.Position = 0;
                            memoryStream.CopyTo(processedBufferWriter);
                        }
                    }
                }

                stopwatch.Stop();
                ConsoleActivity.PrintAndWait("Completed processing the file\nTime taken to convert all the content of the file to uppercase is : " + stopwatch.ElapsedMilliseconds + " ms");
            }
            catch (OutOfMemoryException ex)
            {
                ConsoleActivity.PrintAndWait(ex.Message);
            }
            catch (IOException ex)
            {
                ConsoleActivity.PrintAndWait(ex.Message);
            }
        }
    }
}
