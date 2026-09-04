using MemoryManagement.View;

namespace MemoryManagement
{
    /// <summary>
    /// Class to demonstrate the difference between value and reference type.
    /// </summary>
    public class ValueAndReferenceTypes
    {
        private string _studentName = "Ravi";
        private int _studentAge = 19;

        /// <summary>
        /// Start task 1 which show the functionality of value and reference type.
        /// </summary>
        public void ExecuteTask1()
        {
            ConsoleActivity.ShowHeader("Task 1");
            ConsoleActivity.PrintInConsole("Value type :");
            ConsoleActivity.PrintInConsole($"Student name : {this._studentName}{Environment.NewLine}Student age : {this._studentAge}");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Reference type : ");
            Student student = new Student("Ravi", 19);
            ConsoleActivity.PrintInConsole($"Student name : {student.Name}{Environment.NewLine}Student age : {student.Age}");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole($"Lets change both the value by passing through a method{Environment.NewLine}{Environment.NewLine}Values after modification are : ");
            this.ChangeStudentDetails(this._studentName, this._studentAge, student);
            ConsoleActivity.PrintInConsole("Value type :");
            ConsoleActivity.PrintInConsole($"Student name : {this._studentName}{Environment.NewLine}Student age : {this._studentAge}");
            ConsoleActivity.PrintEmptyLine();
            ConsoleActivity.PrintInConsole("Reference type : ");
            ConsoleActivity.PrintInConsole($"Student name : {student.Name}{Environment.NewLine}Student age : {student.Age}");
            ConsoleActivity.PrintAndWait("Here, only the reference type value get changed and value type remain same.");
        }

        /// <summary>
        /// Change the student details to demonstrate working of value and reference type.
        /// </summary>
        /// <param name="studentName">Value typed student name.</param>
        /// <param name="age">Value typed student age.</param>
        /// <param name="student">Reference typed student details.</param>
        public void ChangeStudentDetails(string studentName, int age, Student student)
        {
            studentName = "Thor";
            age = 30;
            student.Name = "Thor";
            student.Age = 30;
        }

        /// <summary>
        /// Start the task 2 which demonstrate the memory usage in stack and heap.
        /// </summary>
        public void ExecuteTask2()
        {
            ConsoleActivity.ShowHeader("Task 2");
            this.SumOfNumbersInArray();
            this.SumOfNumbers();
        }

        private void SumOfNumbersInArray()
        {
            int[] numberArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            int sum = 0;
            foreach (int number in numberArray)
            {
                sum += number;
            }

            ConsoleActivity.PrintInConsole($"Sum of array elements : {sum}");
        }

        private void SumOfNumbers()
        {
            int num1 = 10, num2 = 20, num3 = 30, num4 = 40, num5 = 50, num6 = 60;
            int sum = num1 + num2 + num3 + num4 + num5 + num6;
            ConsoleActivity.PrintAndWait($"Sum of local variable : {sum}");
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
