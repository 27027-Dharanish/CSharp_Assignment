using System.Linq.Expressions;
using Assignment9.Core.Constant;

namespace Assignment9.Service;

/// <summary>
/// Query builder class containing.
/// </summary>
/// <typeparam name="T">Type parameter that contains the IEnumerable.</typeparam>
public class QueryBuilder<T>
    where T : class
{
    private IQueryable<T> _list;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryBuilder{T}"/> class.
    /// </summary>
    /// <param name="list">A list of elements.</param>
    public QueryBuilder(IEnumerable<T> list)
    {
        this._list = list.AsQueryable();
    }

    /// <summary>
    /// Filters the collection.
    /// </summary>
    /// <param name="predicate">Predicate</param>
    /// <returns>returns the predicate</returns>
    public QueryBuilder<T> Filter(Expression<Func<T, bool>> predicate)
    {
        this._list = this._list.Where(predicate);
        return this;
    }

    /// <summary>
    /// Sorts the collection.
    /// </summary>
    /// <typeparam name="TKey">Type parameter</typeparam>
    /// <param name="keySelector">Key selector</param>
    /// <returns>A filtered result for sort</returns>
    public QueryBuilder<T> Sort<TKey>(Expression<Func<T, TKey>> keySelector)
    {
        this._list = this._list.OrderBy(keySelector);
        return this;
    }

    /// <summary>
    /// Joins the current collection with another collection.
    /// </summary>
    /// <typeparam name="TInner">Type of the inner collection.</typeparam>
    /// <typeparam name="TKey">Type of the join key.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="inner">Collection to join.</param>
    /// <param name="outerKey">Key selector for the current collection.</param>
    /// <param name="innerKey">Key selector for the inner collection.</param>
    /// <param name="resultSelector">Selects the result from matching records.</param>
    /// <returns>Query builder with the joined data.</returns>
    public QueryBuilder<TResult> Join<TInner, TKey, TResult>(
        IEnumerable<TInner> inner,
        Func<T, TKey> outerKey,
        Func<TInner, TKey> innerKey,
        Func<T, TInner, TResult> resultSelector)
        where TResult : class
    {
        var result = this._list.Join(inner, outerKey, innerKey, resultSelector);
        return new QueryBuilder<TResult>(result);
    }

    /// <summary>
    /// Executes and materialize the collections.
    /// </summary>
    /// <returns>A materialized collection</returns>
    public List<T> Execute()
    {
        return this._list.ToList();
    }

    /// <summary>
    /// Custom string filter factory to handle operations like Contains, StartsWith, and EndsWith dynamically.
    /// </summary>
    /// <param name="propertyName">Property name to be filtered.</param>
    /// <param name="operation">Operation to be performed.</param>
    /// <param name="value">Value used to be done filter.</param>
    /// <returns>Query builder with the filtered data.</returns>
    public QueryBuilder<T> Filter(string propertyName, FilterOption operation, string value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var constant = Expression.Constant(value);
        Expression expression;
        switch (operation)
        {
            case FilterOption.Contains:
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                expression = Expression.Call(property, containsMethod!, constant);
                break;
            case FilterOption.StartsWith:
                var startWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                expression = Expression.Call(property, startWithMethod!, constant);
                break;
            case FilterOption.EndsWith:
                var endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                expression = Expression.Call(property, endsWithMethod!, constant);
                break;
            default:
                throw new NotSupportedException($"Operation {operation} is not supported!!");
        }

        var lambda = Expression.Lambda<Func<T, bool>>(expression, parameter);
        this._list = this._list.Where(lambda);
        return this;
    }

    /// <summary>
    /// Custom string filter factory to handle operations like greater than, less than and equal to dynamically.
    /// </summary>
    /// <typeparam name="TValue">Type of the value to be compared.</typeparam>
    /// <param name="propertyName">Property name to be filtered.</param>
    /// <param name="operation">Operation to be performed.</param>
    /// <param name="value">Value used to be done filter.</param>
    /// <returns>Query builder with the filtered data.</returns>
    public QueryBuilder<T> Filter<TValue>(string propertyName, FilterOption operation, TValue value)
        where TValue : IComparable
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var constant = Expression.Constant(value);
        Expression expression;
        switch (operation)
        {
            case FilterOption.GreaterThanOrEqualTo:
                expression = Expression.GreaterThanOrEqual(property, constant);
                break;
            case FilterOption.LessThanOrEqualTo:
                expression = Expression.LessThanOrEqual(property, constant);
                break;
            case FilterOption.Equal:
                expression = Expression.Equal(property, constant);
                break;
            default:
                throw new NotSupportedException($"Operation {operation} is not supported!!");
        }

        var lambda = Expression.Lambda<Func<T, bool>>(expression, parameter);
        this._list = this._list.Where(lambda);
        return this;
    }
}
