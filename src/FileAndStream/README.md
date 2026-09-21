# 1. File Streams

A **Stream** is an abstraction representing a sequence of bytes. Instead of loading an entire file or dataset into RAM all at once, streams allow you to read, write, and manipulate data sequentially, bit by bit.


### `System.IO.Stream` (The Base Class)
This is the abstract foundation for all stream types in .NET. It defines core operational behaviors via properties and methods like `Read()`, `Write()`, `Seek()`, `Flush()`, and `Length`. It establishes a unified contract, ensuring that whether you are writing data to a physical file, a web endpoint, or a block of memory, the code interface remains identical.

### `System.IO.MemoryStream` (In-Memory Buffer Stream)
A `MemoryStream` encapsulates an internal, resizable byte array stored directly in system RAM. 
* **Primary Use:** Acts as a fast, temporary storage buffer for binary or text data before it is transformed or routed elsewhere.
* **Mechanism:** It avoids physical disk latency entirely. However, because it resides in RAM, storing massive files inside a `MemoryStream` will directly inflate your application's private memory consumption.

### `System.IO.FileStream` (The Storage Pipeline)
A `FileStream` creates a direct, active pipeline between your application code and a physical file located on the disk subsystem.
* **Primary Use:** Reading from or writing bytes directly to permanent disk storage.


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
 
### Modified Flow
 
String
   ↓
MemoryStream
   ↓
CopyTo()
   ↓
FileStream
   ↓
File
 
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

## Objective
The objective of this task is to make the logging system thread-safe when multiple users try to write error messages to the same file at the same time.

***

## Problem
Multiple users may call `LogError()` simultaneously.

If multiple threads write to the same file at the same time, it can cause:
* File access conflicts
* `IOException`
* File sharing problems
* Unexpected log output
* Performance issues

***

## Solution
A `lock` is used to allow only one thread at a time to write to the file.

```csharp
lock (_lock)
{
    File.AppendAllText(LogFileName, logMessage);
}
```


### Example Usage
```csharp
ThreadSafeLogger.LogError("Database connection failed");
ThreadSafeLogger.LogError("Invalid user input");
ThreadSafeLogger.LogError("File not found");
```

***

### Why Use a Static Lock?
```csharp
private static readonly object _lock = new object();
```
`static` means all instances of `ThreadSafeLogger` share the same lock. For example:
```csharp
ThreadSafeLogger logger1 = new ThreadSafeLogger();
ThreadSafeLogger logger2 = new ThreadSafeLogger();
```
Both objects use the same `_lock`. This is important because both objects are accessing the same physical file.

### Why Use `readonly`?
```csharp
private static readonly object _lock;
```
`readonly` prevents the lock object from being replaced after it is initialized. This keeps the exact same lock object instance throughout the application lifecycle.

### Why Use `File.AppendAllText()`?
```csharp
File.AppendAllText(LogFileName, logMessage);
```
This avoids unnecessary byte array conversions and memory allocations in RAM.

***

## Execution Flow

```text
Multiple Users
      ↓
   LogError()
      ↓
     lock
      ↓
One thread at a time
      ↓
ErrorLog.txt
```

***

## Key Learning
* Multiple threads can execute `LogError()` at the exact same time.
* `lock` protects the file-writing operation from race conditions.
* Only one thread can enter the scoped `lock` block at any given instance.
* Other threads wait in a queue until the current execution thread finishes.
* `static` ensures all logger class instances reference a unified global lock.
* `File.AppendAllText()` writes directly to the disk subsystem.
* An intermediate `MemoryStream` buffer is not required for simple line logging.

## Conclusion
The logging system is now completely thread-safe for concurrent writes targeting the same log file. Utilizing a `lock` block blocks simultaneous disk access attempts, safely circumventing cross-thread conflicts and ensuring data integrity.
