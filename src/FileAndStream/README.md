#  File Streams

A **Stream** is an abstraction representing a sequence of bytes. Instead of loading an entire file or dataset into RAM all at once, streams allow you to read, write, and manipulate data sequentially, bit by bit.

### `System.IO.Stream` (The Base Class)
This is the abstract foundation for all stream types in .NET. It defines core operational behaviors via properties and methods like `Read()`, `Write()`, `Seek()`, `Flush()`, and `Length`. It establishes a unified contract, ensuring that whether you are writing data to a physical file, a web endpoint, or a block of memory, the code interface remains identical.

A `MemoryStream` encapsulates an internal, resizable byte array stored directly in system RAM. 
* **Primary Use:** Acts as a fast, temporary storage buffer for binary or text data before it is transformed or routed elsewhere.
* **Mechanism:** It avoids physical disk latency entirely. However, because it resides in RAM, storing massive files inside a `MemoryStream` will directly inflate your application's private memory consumption.

A `FileStream` creates a direct, active pipeline between your application code and a physical file located on the disk subsystem.
* **Primary Use:** Reading from or writing bytes directly to permanent disk storage.

 A `BufferedStream` creates an intermediate memory buffer between your application code and another underlying data stream (like a `FileStream`).
* **Primary Use:** Grouping frequent, small data transfers into fewer, larger blocks to minimize slow physical I/O or network system calls.



 ---

# Task 3 -> File Handling and Memory Efficiency
 
## Objective
 
To understand how to write data to a file, read data from a file, and identify unnecessary memory usage.
  
## Identified Memory Issue
 
The following code creates an unnecessary copy of the data:
byte[] writeBuffer = memoryStream.ToArray();
ToArray() creates a new byte array containing all the data in the MemoryStream.
For large amounts of data, this requires additional memory.
 
## Modified Approach
 
Instead of converting the entire MemoryStream into a new byte array, we can use CopyTo().

## Example
 
using (MemoryStream memoryStream = new MemoryStream())
{
    byte[] buffer = Encoding.ASCII.GetBytes(data);
 
    memoryStream.Write(buffer, 0, buffer.Length);
 
    memoryStream.Position = 0;
 
    using (FileStream fileStream = new FileStream(
        path,
        FileMode.Create,
        FileAccess.Write))
    {
        memoryStream.CopyTo(fileStream);
    }
}
 
## Why CopyTo() Is Better
 
- Avoids creating a complete duplicate byte array.
- Reduces unnecessary memory allocation.
- Transfers data directly from MemoryStream to FileStream.
- Better for handling large amounts of data.

## Conclusion
 
The main memory issue was the unnecessary copy created by ToArray().
Using CopyTo() or writing directly to FileStream makes the program more memory-efficient.

# Task 4 - Thread-Safe Logging

This outlines process of logging using the current `ThreadSafeLogger` implementation.

## The Issue: `IOException` (File In Use)

If multiple independent application instances (processes) attempt to log to the same file simultaneously using `File.AppendAllText`, the application will crash with a **`System.IO.IOException`**.

### Why This Happens:
* **Scope of `lock`:** The C# `lock` keyword only coordinates threads within the **same application process**. It cannot block or communicate with other external processes.

---

## How It Works
* **Dynamic File Generation:** Generates a distinct file for each user (`ErrorLog_{userId}.txt`).
* **Granular Locking:** Uses a `ConcurrentDictionary` (`_userLocks`) to manage locks at the individual user level. Threads writing logs for `User_A` will never block threads writing logs for `User_B`.
