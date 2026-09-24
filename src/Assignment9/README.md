# Fluent QueryBuilder Engine using Expression Trees

This project implements a type-safe **Fluent Query Builder** in **LINQ Expressions** and dynamic **Expression Trees**. 

It allows you to dynamically compose complex queries—such as filtering, conditional comparisons, custom text parsing (`Contains`, `StartsWith`), sorting, and relational joining—using an intuitive, chainable pipeline (**Fluent API**) before executing and materializing the data.

---

## Understanding the Fluent API Design
A **Fluent API** is an architectural design pattern that allows method calls to be chained together sequentially using dot notation (`.Method1().Method2()`). 

This is achieved by having each configuration method modify the internal query state and then return `this` (the current instance of the `QueryBuilder<T>` object). All operations are deferred (lazy-evaluated) until you explicitly invoke `.Execute()`, which finally runs the compiled query pipeline against your dataset.

### Usage Example
```csharp
List<Product> results = new QueryBuilder<Product>(productSource)
    .Filter(p => p.IsActive)                                    // 1. Strongly-typed lambda filter
    .Filter("ProductName", FilterOption.StartsWith, "Smart")   // 2. Dynamic string factory filter
    .Filter("Price", FilterOption.LessThanOrEqualTo, 1500.00)  // 3. Dynamic generic numeric comparison
    .Sort(p => p.Price)                                        // 4. Strongly-typed sort
    .Execute();                                                // 5. Final materialization
```

---

## Deep-Dive Method Breakdown

### 1. `Constructor: QueryBuilder(IEnumerable<T> list)`
* **How it works:** Accepts any standard in-memory collection (`IEnumerable<T>`) and converts it into an internal `IQueryable<T>` data pipeline using `.AsQueryable()`. This conversion enables the collection to seamlessly integrate with custom expression tree modifiers downstream.

### 2. `Filter(Expression<Func<T, bool>> predicate)`
* **How it works:** Accepts a strongly-typed, compilation-safe LINQ lambda expression (e.g., `x => x.Age > 18`). It appends a standard `.Where()` filter statement to the data stream and returns the builder instance to continue chaining.

### 3. `Sort<TKey>(Expression<Func<T, TKey>> keySelector)`
* **How it works:** Dynamically tracks ordering rules by applying the `.OrderBy()` extension method across the target sorting field specified by the strongly-typed `keySelector` callback function.

### 4. `Join<TInner, TKey, TResult>(...)`
* **How it works:** Merges the active builder collection dataset with a secondary external collection (`IEnumerable<TInner>`) based on matching structural relational keys. Because a successful join shifts the underlying data layout into a new structural shape, it returns a **brand-new instance** of `QueryBuilder<TResult>` mapped to the merged output entity.

### 5. `Filter(string propertyName, FilterOption operation, string value)` [String Specialized]
* **How it works:** Dynamically parses textual strings at runtime without using hardcoded reflection wrappers.
  * **Expression Tree Construction:** It generates a parameter representation of the object (`x`), reflects down into the property (`x.PropertyName`), wraps your string keyword inside a constant placeholder (`value`), and builds an inline method execution call mapping directly to `string.Contains`, `string.StartsWith`, or `string.EndsWith`.
  * **Compilation:** It stitches these steps into an executable condition block `Expression.Lambda<Func<T, bool>>` and hands it over to the query processor.

### 6. `Filter<TValue>(string propertyName, FilterOption operation, TValue value)` [Generic Comparison]
* **How it works:** Handles dynamic conditional logic checks (`>=`, `<=`, `==`) for any data structures supporting scalar comparisons (`where TValue : IComparable`).
  * It maps your criteria rules directly to structural assembly expressions: `Expression.GreaterThanOrEqual`, `Expression.LessThanOrEqual`, or `Expression.Equal`. This allows your code to run complex conditional evaluations against numbers, dates, or custom keys completely dynamically based on pure string property names.

### 7. `Execute()`
* **How it works:** Acts as the mandatory **terminal pipeline terminator**. It takes all the combined expression trees and criteria layers built up in your chain, compiles them down, triggers database/collection iteration using `.ToList()`, and returns the final materialized data results.

---

## Learnings

* **Expression Trees vs. Raw Lambdas:** A standard lambda `Func<T, bool>` compiles directly into executable IL code at runtime. An `Expression<Func<T, bool>>` compiles into an **Abstract Syntax Tree (AST)** data structure. This means the engine can break apart, inspect, edit, and re-wire code logic dynamically based on user input before running it.
* **State Preservation via Immutability:** Notice that while filtering and sorting update the internal pointer reference states and return `this`, the `Join` method explicitly constructs a new query builder because changing the underlying structure type parameters breaks fluent type chaining constraints otherwise.
