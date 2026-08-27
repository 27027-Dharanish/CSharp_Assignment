namespace Assignment9.Service;

/// <summary>
/// Query builder class containing.
/// </summary>
/// <typeparam name="T">Type parameter that contains the IEnumerable.</typeparam>
public class QueryBuilder<T>
    where T : class
{
    private IEnumerable<T> _list;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryBuilder{T}"/> class.
    /// </summary>
    /// <param name="list">A list of elements.</param>
    public QueryBuilder(IEnumerable<T> list)
    {
        this._list = list;
    }

    /// <summary>
    /// Filters the collection.
    /// </summary>
    /// <param name="predicate">Predicate</param>
    /// <returns>returns the predicate</returns>
    public QueryBuilder<T> Filter(Func<T, bool> predicate)
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
    public QueryBuilder<T> Sort<TKey>(Func<T, TKey> keySelector)
    {
        this._list = this._list.OrderBy(keySelector);
        return this;
    }

    /// <summary>
    /// Executes and materialize the collections.
    /// </summary>
    /// <returns>A materialized collection</returns>
    public List<T> Execute()
    {
        return this._list.ToList();
    }
}
