using FileAndStream;

namespace Assignments
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task1 task1 = new Task1();
            task1.GenerateOneGBFile();
            task1.ReadFileUsingFileStream();
            task1.ReadFileUsingBufferedStream();
            task1.ConvertUpperCase();
        }
    }
}