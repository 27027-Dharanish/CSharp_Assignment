namespace Assignment9.Core.Constant
{
    /// <summary>
    /// Specifies the linq task available.
    /// </summary>
    public enum LinqTask
    {
        /// <summary>
        /// Option to view the task 1.
        /// </summary>
        Task1 = 1,

        /// <summary>
        /// Option to view the task 2.
        /// </summary>
        Task2,

        /// <summary>
        /// Option to view the task 3.
        /// </summary>
        Task3,

        /// <summary>
        /// Option to view the task 4.
        /// </summary>
        Task4,

        /// <summary>
        /// Option to view the task 5.
        /// </summary>
        Task5,

        /// <summary>
        /// Option to exit from application.
        /// </summary>
        Exit,
    }

    /// <summary>
    /// The option available in the filter.
    /// </summary>
    public enum FilterOption
    {
        /// <summary>
        /// Option to select contains operation.
        /// </summary>
        Contains = 1,

        /// <summary>
        /// Option to select starts with operation.
        /// </summary>
        StartsWith,

        /// <summary>
        /// Option to select ends with operation.
        /// </summary>
        EndsWith,

        /// <summary>
        /// Option to select greater than or equal to operation.
        /// </summary>
        GreaterThanOrEqualTo,

        /// <summary>
        /// Option to select less than or equal to operation.
        /// </summary>
        LessThanOrEqualTo,

        /// <summary>
        /// Option to select equal to operation.
        /// </summary>
        Equal,
    }
}
