using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FileAndStream.View;

namespace FileAndStream
{
    /// <summary>
    /// Handle asynchronous version of read and write.
    /// </summary>
    public class Task2
    {
        private readonly List<string> _fileNames = new List<string>
        {
            "BigDataFile1.txt",
            "BigDataFile2.txt",
            "BigDataFile3.txt",
        };

        /// <summary>
        /// Coordinates concurrent file reading using Task.WhenAll.
        /// </summary>
        /// <returns>representing the asynchronous operation.</returns>
        public async Task ProcessMultipleFilesConcurrentlyAsync()
        {
            List<Task> tasks = new List<Task>();

            foreach (var fileName in this._fileNames)
            {
                tasks.Add(this.ProcessFileAsync("BigDataFile.txt", fileName));
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Convert the input file content to uppercase and copy to output file path.
        /// </summary>
        /// <param name="inputFilePath">The input file path.</param>
        /// <param name="outputFilePath">The output file path.</param>
        /// <returns>Return the task.</returns>
        public async Task ProcessFileAsync(string inputFilePath, string outputFilePath)
        {
            try
            {
                using (FileStream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
                using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous))
                using (BufferedStream reader = new BufferedStream(inputStream))
                using (BufferedStream writer = new BufferedStream(outputStream))
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int bytesRead;

                    while ((bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        string chunkText = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        byte[] processedBuffer = Encoding.UTF8.GetBytes(chunkText.ToUpper());

                        using (MemoryStream memoryWriter = new MemoryStream())
                        {
                            memoryWriter.Write(processedBuffer, 0, processedBuffer.Length);
                            memoryWriter.Position = 0;
                            await memoryWriter.CopyToAsync(writer);
                        }
                    }

                    await writer.FlushAsync();
                    ConsoleActivity.PrintInConsole($"{Path.GetFileName(outputFilePath)} has been processed successfully!");
                }
            }
            catch (IOException ex)
            {
                ConsoleActivity.PrintAndWait($"IO Error on {outputFilePath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                ConsoleActivity.PrintAndWait($"General Error on {outputFilePath}: {ex.Message}");
            }
        }
    }
}
