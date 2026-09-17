# Task 1: Understanding and Implementing Events and Delegates in C#

This task demonstrates how to create a simple notification system using the **Publisher-Subscriber pattern**. It showcases how a class can trigger an alert and securely broadcast messages to other parts of an application.

---

## 🔍 Line-by-Line Explanation

### `public class Task1`
This is the main root class that wraps our execution logic and houses our nested `Notifier` system.

### `public void NotificationService()`
This acts as the entry point or driver workflow for our task.
* **`Notifier notifier = new Notifier();`**  
  Creates a new instance of the publisher object (`Notifier`) so we can access its events.
* **`notifier.OnAction += DisplayMessage;`**  
  Uses the `+=` operator to register (subscribe) our local `DisplayMessage` method to the event.
* **`notifier.PerformAction("...");`**  
  Asks the notifier to run its action, which internally triggers the announcement broadcast.
* **`notifier.OnAction -= DisplayMessage;`**  
  Uses the `-=` operator to safely unregister (unsubscribe) our method from the event, ensuring clean memory management.

### `private void DisplayMessage(string message)`
This is our **Subscriber Handler**. It must perfectly match the delegate's signature (accepting exactly one `string` and returning `void`). Inside, it takes the incoming notification string and prints it using `Console.WriteLine`.

### `public class Notifier`
This is the **Publisher Class**. It contains the mechanism for declaring the communication rules and firing the alerts.

### `public delegate void Notify(string message);`
This defines the **Delegate**. Think of it as a formal contract or blueprint. It states: *"Any method that wants to hold hands with our system must accept one string and return nothing (void)."*

### `public event Notify? OnAction;`
This defines the **Event**. It acts as a guard dog around our delegate type. The `?` means it starts off as `null` because when the system turns on, no one has subscribed to it yet.

### `public void PerformAction(string message)`
This is a standard helper method used to trigger the event securely from inside the publisher.
* **`if (OnAction != null)`**  
  Crucial safety check! If you try to fire an event when zero methods are subscribed, your app will crash with a `NullReferenceException`. This ensures someone is listening first.
* **`OnAction(message);`**  
  Fires the event, sending the string payload zooming down the pipeline to every attached subscriber.

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

# Task 2 - Type System: `var` vs `dynamic`
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

# Task 3 - Sorting an Array Using Anonymous Methods

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
# Task 5: Advanced Use of Delegates for Sorting

This task demonstrates how to use a custom **Delegate** to decouple sorting rules from the sorting engine. By passing different methods into the same execution loop, we can sort a list of complex objects dynamically by Name, Category, or Price.

---

## 💻 Code Implementation

```csharp
using System;
using System.Collections.Generic;

namespace AdvancedFeature.Tasks
{
    /// <summary>
    /// Represents a commercial item with distinct properties.
    /// </summary>
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public double Price { get; set; }

        public override string ToString()
        {
            return $"Name: {Name,-12} | Category: {Category,-10} | Price: ${Price:F2}";
        }
    }

    /// <summary>
    /// Demonstrates advanced delegate manipulation for flexible sorting routines.
    /// </summary>
    public class Task5
    {
        // 1. Declare the custom sorting delegate contract
        public delegate int SortDelegate(Product p1, Product p2);

        /// <summary>
        /// Entry driver method to configure and run the complex sorting operations.
        /// </summary>
        public void ExecuteAdvancedSorting()
        {
            // 2. Initialize a list of Product objects
            List<Product> products = new List<Product>
            {
                new Product { Name = "Laptop", Category = "Electronics", Price = 1200.50 },
                new Product { Name = "Coffee Maker", Category = "Appliances", Price = 89.99 },
                new Product { Name = "Smartphone", Category = "Electronics", Price = 799.00 },
                new Product { Name = "Blender", Category = "Appliances", Price = 45.50 },
                new Product { Name = "Desk Chair", Category = "Furniture", Price = 150.00 }
            };

            // 3. Create instances of SortDelegate pointing to different comparison strategies
            SortDelegate nameSorter = new SortDelegate(SortByName);
            SortDelegate categorySorter = new SortDelegate(SortByCategory);
            SortDelegate priceSorter = new SortDelegate(SortByPrice);

            // 4. Call SortAndDisplay three times, changing the sorting blueprint each time
            Console.WriteLine("--- SORT BY NAME ---");
            SortAndDisplay(products, nameSorter);

            Console.WriteLine("\n--- SORT BY CATEGORY ---");
            SortAndDisplay(products, categorySorter);

            Console.WriteLine("\n--- SORT BY PRICE ---");
            SortAndDisplay(products, priceSorter);
        }

        /// <summary>
        /// A generic orchestrator that sorts a list using the provided rule logic and prints it.
        /// </summary>
        public void SortAndDisplay(List<Product> productList, SortDelegate sortingRule)
        {
            // Create a temporary copy to avoid mutating the original sequence order directly
            List<Product> sortedList = new List<Product>(productList);

            // Use built-in Comparison execution by converting our custom delegate signature inline
            sortedList.Sort((x, y) => sortingRule(x, y));

            // Output the formatted results to the terminal window
            foreach (var product in sortedList)
            {
                Console.WriteLine(product);
            }
        }

        // ==========================================
        // COMPATIBLE TARGET METHODS FOR THE DELEGATE
        // ==========================================

        public static int SortByName(Product p1, Product p2)
        {
            return string.Compare(p1.Name, p2.Name, StringComparison.OrdinalIgnoreCase);
        }

        public static int SortByCategory(Product p1, Product p2)
        {
            return string.Compare(p1.Category, p2.Category, StringComparison.OrdinalIgnoreCase);
        }

        public static int SortByPrice(Product p1, Product p2)
        {
            return p1.Price.CompareTo(p2.Price);
        }
    }
}
```

---

## 🔍 Line-by-Line Explanation

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

## 🚀 Expected Output

When you run this application, the console displays the collection structured cleanly through three unique view states:

```text
--- SORT BY NAME ---
Name: Blender      | Category: Appliances | Price: \$45.50
Name: Coffee Maker | Category: Appliances | Price: \$89.99
Name: Desk Chair   | Category: Furniture  | Price: \$150.00
Name: Laptop       | Category: Electronics| Price: \$1200.50
Name: Smartphone   | Category: Electronics| Price: \$799.00

--- SORT BY CATEGORY ---
Name: Coffee Maker | Category: Appliances | Price: \$89.99
Name: Blender      | Category: Appliances | Price: \$45.50
Name: Laptop       | Category: Electronics| Price: \$1200.50
Name: Smartphone   | Category: Electronics| Price: \$799.00
Name: Desk Chair   | Category: Furniture  | Price: \$150.00

--- SORT BY PRICE ---
Name: Blender      | Category: Appliances | Price: \$45.50
Name: Coffee Maker | Category: Appliances | Price: \$89.99
Name: Desk Chair   | Category: Furniture  | Price: \$150.00
Name: Smartphone   | Category: Electronics| Price: \$799.00
Name: Laptop       | Category: Electronics| Price: \$1200.50
```
