### Assignment 5 - Error Handling
 
# Task 1 - DivideByZeroException
 
Implemented exception handling for division by zero using:
 
- "try"
- "catch"
- "finally"
 
The "DivideByZeroException" is caught and a meaningful error message is displayed.

The "finally" block displays a message indicating that it has been executed.
 
---
 
### Task 2 - IndexOutOfRangeException
 
## Implemented handling for "IndexOutOfRangeException".
 
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
