using MemoryManagement.View;

namespace MemoryManagement
{
    /// <summary>
    /// Class to manually trigger garbage collection and observe the impact on memory usage.
    /// </summary>
    public class GarbageCollection
    {
        /// <summary>
        /// Execute task3 to show the usage of garbage collection.
        /// </summary>
        public void ExecuteTask3()
        {
            ConsoleActivity.ShowHeader("Task 3");
            ConsoleActivity.PrintInConsole("Initial stage");
            Student student;
            for (int i = 0; i < 1_00_000; i++)
            {
                student = new Student("Sample name", i);
            }

            ConsoleActivity.PrintInConsole("Object initialized successfully!");
            ConsoleActivity.PrintInConsole("Calling garbage collector");
            GC.Collect();
            ConsoleActivity.PrintAndWait("Garbage collector executed successfully!");
        }

        /// <summary>
        /// Represents the student details.
        /// </summary>
        public class Student
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Student"/> class.
            /// </summary>
            /// <param name="name">Name of the student.</param>
            /// <param name="age">Age of the student.</param>
            public Student(string name, int age)
            {
                this.Name = name;
                this.Age = age;
            }

            /// <summary>
            /// Gets or sets the student name.
            /// </summary>
            /// <value>
            /// Name of the student.
            /// </value>
            public string? Name { get; set; }

            /// <summary>
            /// Gets or sets the student age.
            /// </summary>
            /// <value>
            /// Age of the student.
            /// </value>
            public int Age { get; set; }
        }
    }
}
