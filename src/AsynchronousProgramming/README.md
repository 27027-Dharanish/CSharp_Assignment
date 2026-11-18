# Asynchronous Programming

## Task 1 - Async/Await

- Use `async` and `await` with Task when working with asynchronous operations.
- `await` allows the program to wait for an asynchronous operation without blocking the calling thread.
- `HttpClient` provides asynchronous methods like `GetStringAsync()` for I/O-bound operations.
- Async methods should return `Task` or `Task<T>` when a result needs to be returned.
- The async method must be called with `await` instead of synchronously waiting for the result.
- Asynchronous programming is particularly useful for I/O-bound operations such as network requests, database connections, file handling operations.

## Task 2 - Task Parallel Library

- `Parallel.ForEach` can be used when independent operations need to be executed concurrently.
- TPL manages the underlying threads and scheduling instead of requiring threads to be created manually.
- Parallel execution is useful when the work can be divided into independent operations.
- Console operation takes time when as we are printing the results in the console.

## Task 3 - Multi-Threading

- The `Thread` class can be used when explicit thread creation and control are required.
- They are expensive operations as a new thread is created by the OS.
- Multiple independent operations can be executed on separate threads.
- `Join()` should be used when the main execution flow needs to wait for a thread to finish.
- Threading should be used when explicit thread-level control is required.
- Use higher-level abstractions such as `Task`.

## Task 4 - Multi-Layered Async/Await

- `Task.Run()` is suitable for moving CPU-bound work to a ThreadPool thread.
- CPU-bound and I/O-bound operations should be treated differently.
- `HttpClient` operations are asynchronous and should be awaited directly rather than unnecessarily wrapping them in `Task.Run()`.
- Async operations should be propagated through each layer using `async` and `await`.
- When one operation depends on the result of another, the dependent operation should await the previous operation before continuing.

## Task 5 - Debugging and Fixing Deadlock

- Should not use `.Result` or `.Wait()` on asynchronous operations as they blocks the thread.
- Synchronous blocking can cause deadlock when an asynchronous continuation requires a context that is currently blocked.
- Use `await` to wait for a task to complete.

```CSharp
        string result = await this.SomeAsyncOperation();

        Console.WriteLine(result);
```

## Task 6 - ConfigureAwait

- ConfigureAwait controls whether an awaited task resumes on the original context or from a thread pool thread.
- `ConfigureAwait(true)` default behavior
  - Captures the current context
  - Resumes on the same context (e.g., UI thread)

- `ConfigureAwait(false)`
  - Do not captures the current context
  - Resumes on a thread pool thread
  - Avoids unnecessary marshaling back to the original context

- `Thread.CurrentThread.ManagedThreadId` can be used to observe the thread executing code before and after an await.
- `ConfigureAwait(false)` is useful when the continuation does not depend on a specific synchronization context.
- In a console application, ConfigureAwait(false) usually makes no behavioral difference because there is no synchronization context.

## Task 7 - Async Void vs Async Task

- Prefer `async Task` or `async Task<T>` for normal asynchronous methods.
- `Task` allows the caller to await the operation and observe exceptions.
- Exceptions from an `async Task` method can be handled by the calling code when the task is awaited.
- `async void` should generally be avoided except when required by event-handler signatures.
- Exceptions from `async void` methods cannot normally be handled by the caller using a regular `try-catch`.
- Returning a `Task` provides better control and exception handling.