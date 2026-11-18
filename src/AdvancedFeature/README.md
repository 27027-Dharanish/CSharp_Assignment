# Task 1 -> Understanding and Implementing Events and Delegates in C#

This task demonstrates how to create a simple notification system using the **Publisher-Subscriber pattern**. It showcases how a class can trigger an alert and securely broadcast messages to other parts of an application.

---

## How It Works (The Real-World Analogy)

Think of this pattern like a **YouTube Channel** subscription setup:
1. **The Delegate (`Notify`):** This is the concept of a notification itself (it must contain video details, it sends an alert to a phone).
2. **The Event (`OnAction`):** This is the **Subscribe Button** on the channel page. It securely tracks who wants notifications.
3. **The Subscription (`+=`):** A user clicks the button. Now, your phone handler (`DisplayMessage`) is linked to the channel.
4. **The Trigger (`PerformAction`):** The creator uploads a new video. The system checks the subscriber list and instantly flashes the message across everyone's screens.

---

## Learnings

* **Delegates hold Methods:** Normal variables hold data like numbers (`5`) or text (`"Hello"`). A delegate is a special type of variable that **holds a function**, allowing you to execute code dynamically later on.
* **Events offer Protection:** If you use a raw delegate out in the open, any outside class can type `notifier.OnAction = null;` and accidentally erase every single subscriber. Adding the `event` keyword locks it down so outside classes can **only** use `+=` (add) or `-=` (remove).
* **Loose Coupling:** Notice that the `Notifier` class has absolutely no idea what `DisplayMessage` does. It doesn't know if the message is being printed to a console, written to a file, or texted to a phone. It just shouts the message into the void—making your code highly modular and flexible!

---

# Task 2 -> Type System: `var` vs `dynamic`
## Overview

Understanding when types are resolved is critical for writing robust C# code. While both keywords allow you to declare variables without explicitly naming the type, they handle type enforcement completely differently.

*   **`var`**: Statically typed. The compiler infers the type at **compile-time**.
*   **`dynamic`**: Dynamically typed. The type is resolved at **runtime** via the Dynamic Language Runtime (DLR).

---

## Key Differences at a Glance

| Feature | `var` | `dynamic` |
| :--- | :--- | :--- |
| **Type Resolution** | **Compile-time** (Static) | **Runtime** (Dynamic) |
| **Initialization** | **Mandatory** upon declaration | **Optional** (Can be assigned later) |
| **Type Mutation** |  **No**. The inferred type is permanent |  **Yes**. Type can change freely |
| **IntelliSense Support** |  Full IDE auto-complete |  None (Bypasses compile checks) |
| **Errors Caught** | At **Compile-time** | At **Runtime** (Throws exceptions) |

---

# Task 3 -> Sorting an Array Using Anonymous Methods

This task demonstrates how to use an **anonymous method** to customize the behavior of the built-in `Array.Sort` method to sort an array of integers in ascending order.

---

## Explanation for the anonymous method

### `Array.Sort(numbers, delegate(int x, int y) { ... });`
This is the core engine of the task. 
* **`Array.Sort`**: A built-in C# utility. By default, it knows how to sort numbers, but here we explicitly pass a custom comparison rule as a second argument.
* **`delegate(int x, int y)`**: This is the **Anonymous Method**. The `delegate` keyword defines an inline function without a name. It intercepts the sorting process by grabbing two numbers from the array (`x` and `y`) to check which one is smaller.

### `return x.CompareTo(y);`
Inside the anonymous method body, we use C#'s `CompareTo` tool:
* If `x` is smaller than `y`, it returns a **negative number** (tells C# `x` comes first).
* If `x` is larger than `y`, it returns a **positive number** (tells C# `y` comes first).
* If they are equal, it returns **0**.
* *Tip:* Flipping this to `return y.CompareTo(x);` would instantly sort the array in **descending order**.

---

## Learnings

* An anonymous method is a block of code passed directly as a parameter. Instead of creating a brand-new, permanent function elsewhere in your class just to use it once, you declare it exactly where it is needed using the `delegate` keyword.
* **Inline Callbacks:**  
  This approach keeps your codebase clean. The sorting logic is tightly coupled right alongside the execution line, making it easy to read from top to bottom.
* **The Evolution of C#:**  
  While anonymous methods (`delegate(int x, int y) { ... }`) were introduced early in C# to pass inline code, modern C# code heavily favors ultra-short **Lambda Expressions** (like `(x, y) => x.CompareTo(y)`) to accomplish the exact same goal with even fewer characters!

---
# Task 5 -> Advanced Use of Delegates for Sorting

This task demonstrates how to use a custom **Delegate** to decouple sorting rules from the sorting engine. By passing different methods into the same execution loop, we can sort a list of complex objects dynamically by Name, Category, or Price.

---


##  Learning

### `public delegate int SortDelegate(Product p1, Product p2);`
This defines our **Delegate blueprint**. It forces a contract: *"Any method that wants to act as a sorter here must take exactly two `Product` arguments and return an `int` (-1, 0, or 1)."*

### `SortDelegate nameSorter = new SortDelegate(SortByName);`
We wrap our normal method `SortByName` inside the delegate container. `nameSorter` now acts as an executable pointer to that specific logic layout.

### `public void SortAndDisplay(List<Product> productList, SortDelegate sortingRule)`
This method is **strategy-agnostic**. It doesn't know *how* the items are going to be sorted. It blindly accepts whatever sorting script is passed down into the `sortingRule` parameter and asks the `sortedList.Sort()` algorithm to execute that exact rule when comparing items.

### `string.Compare(...)` and `CompareTo(...)`
These utility functions perform evaluations on individual object members:
* For **strings** (`Name`, `Category`), `string.Compare` organizes alphabetic positions.
* For **doubles** (`Price`), `.CompareTo` computes basic numerical values.

---


# Task 6 -> Records Demonstration

This project demonstrates the core characteristics of **Records** in C#, including concise declaration, value-based equality testing, immutability behaviors, non-destructive mutation via the `with` expression, and positional object deconstruction.

---

## Core Features Explained

### 1. Positional Record Syntax
`public record Book(string Title, string Author, string ISBN);`  
By using this brief single-line syntax, the C# compiler automatically builds **init-only positional properties**, a constructor matching these arguments, a customized `ToString()` formatter, and built-in value-comparison engine properties under the hood.

### 2. Value-Based Equality (`==`)
In standard classes, `==` checks if two reference pointers point to the exact same physical memory block. With records, `==` evaluates the **actual content** inside the properties. Because `book1` and `book1Duplicate` contain identical property data strings, the check safely returns `True`.

### 3. Absolute Immutability
Attempting to directly write `book1.Title = "New Title";` throws a compile-time error: *`Property or indexer 'Book.Title' cannot be assigned to -- it is read only`*. This guarantees that once data enters the instance, it can never be altered by unpredictable runtime mutations.

### 4. Non-Destructive Mutation (`with`)
The `with` expression copies the entire original record structure into a brand new memory block while applying localized modifications cleanly in place. The original `book1` record is left completely untouched and pristine.

### 5. Automatic Deconstruction
Positional records natively bundle built-in deconstructors. Writing `var (title, author, isbn) = book;` implicitly dissects the `Book` record fields into standalone local variables mapped precisely to their initial construction positions.

---

# Task 7 -> Implementing Advanced Pattern Matching

This task demonstrates the power of C# **Type Pattern Matching** inside a `switch` statement to safely inspect a generic base type hierarchy (`Shape`), identify the underlying derived concrete implementation, and extract properties cleanly inline.

---

## How Pattern Matching Works

### 1. The Power of Type Patterns
Historically, to process this you had to write tedious type-casting checks using old structural patterns:
```csharp
if (shape is Circle) { 
    Circle c = (Circle)shape; // Messy double work!
}
```
With modern C# Type Pattern Matching (`case Circle c:`), the compiler performs a **two-in-one check**. It verifies if the `shape` variable is a `Circle`. If true, it safely unboxes it and casts it directly into a local variable named **`c`** which is instantly ready for use within that `case` scope block.

### 2. Safeguarding against Nulls and Unknowns
* **`case null:`** Explicitly checks if the object instance is completely unassigned. It intercepts the sequence early, bypassing execution to protect the application from throwing standard `NullReferenceExceptions`.
* **`default:`** Acts as a global safety catchment net. If a developer introduces a new shape subclass later on (such as a `Pentagon`) but forgets to update this method, the code cleanly falls to `default` instead of failing unexpectedly.

---

