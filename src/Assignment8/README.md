# Assignment 5 - Error Handling
 
## Task 1 - DivideByZeroException
 
Implemented exception handling for division by zero using:
 
- try : Encloses the code that might cause an error so the system can monitor it for crashes.
- catch : Intercepts and handles the error if one occurs, preventing the program from crashing.
- finally: Runs guaranteed cleanup code at the end, regardless of whether an error happened or not.
 
The "DivideByZeroException" is caught and a meaningful error message is displayed.

The "finally" block displays a message indicating that it has been executed.
 
---
 
## Task 2 - IndexOutOfRangeException
 
# Implemented handling for "IndexOutOfRangeException".
 
- Access an invalid array index.
- Catch the exception.
- Throw a new exception with a meaningful message.
- Catch and display the new exception.
 
---
 
## Task 3 - Custom Exception
 
# Created a custom exception:
 
"InvalidUserInputException"
 
The exception is thrown when the user provides invalid input and is caught.
 
---
 
## Task 4 - Global Unhandled Exception
 
# Implemented global exception handling using:
 
"AppDomain.CurrentDomain.UnhandledException"
 
An unhandled exception is generated and the global event displays a custom error message.
 
---
 
## Task 5 - Stack Trace
 
# Implemented exception handling and printed the exception's stack trace.

A stack trace shows:
 
- Where the exception occurred.
- Which method caused the exception.
 
Example
 
   at Assignment4.controller.ErrorHandlerController.HandleTask5()
   in C:\Dharanish\C#_Intern_Assignment\CSharp_Assignment\src\Assignment8\Controller\ErrorHandlerController.cs:line 201



   ## Learning from the assignment : 

### C# Exception Handling: `throw` vs `throw ex`

* **`throw`**: Preserves the original stack trace, allowing you to **trace the error back to its exact root cause**.
* **`throw ex`**: Resets the stack trace, making the error appear as if it **originated where you re-threw it** and wiping out the historical path.

---

### Detailed Breakdown

### `throw`
* Re-throws the exception currently caught by the `catch` block without modifying its historical data.
* High. Keeps the original line number and method name where the failure first occurred.
* Use this when you need to log an error or run cleanup code, but still want the exception to bubble up to a higher handler.

### `throw ex`
* Manually re-throws the specific exception variable (`ex`), which signals to the .NET runtime that a new exception sequence has started.
* Low. Overwrites the original error location with the line number of the `throw ex` statement.
* Destroys valuable troubleshooting history, making bugs in production much harder to isolate.

---

### Code Comparison

```csharp
// OPTION A: Preserves history (DO THIS)
catch (Exception)
{
    Log("An error occurred");
    throw; 
}

// OPTION B: Wipes history (AVOID THIS)
catch (Exception ex)
{
    Log("An error occurred");
    throw ex; 
}
```

## AppDomain.UnhandledException

* **The Ultimate Safety Net**: It is a special event that catches **any unhandled error** across your entire application right before it crashes.
* **Last Chance to Log**: Use it to **save error details** to a file or database so you know why the app crashed in production.
* **Cannot Stop the Crash**: This event **cannot prevent the application from closing**; it only lets you run final code before the process ends.
* **Global Scope**: It monitors the **entire application environment (AppDomain)**, catching errors from any thread, not just the main one.
