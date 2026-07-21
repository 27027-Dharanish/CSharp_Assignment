using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Model;
using Assignment2.Service.Shapes;
using Assignment2.View;

namespace Assignment2.Controller
{
    /// <summary>
    /// Overall Controller of the Oops project
    /// </summary>
    internal class ShapeController
    {
        private ConsoleActivity _console = new ();

        /// <summary>
        /// To store the shape constant.
        /// </summary>
        public enum Shapes
        {
            /// <summary>
            /// rectangle holds tha value 1.
            /// </summary>
            Rectangle = 1,

            /// <summary>
            /// circle holds the value 2.
            /// </summary>
            Circle = 2,
        }

        /// <summary>
        /// Start the shape controller
        /// </summary>
        public void StartShapeContorller()
        {
            this.ShowShapeOption();
        }

        /// <summary>
        /// Show the option available in the shapes
        /// </summary>
        public void ShowShapeOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Create new :");
            this._console.PrintInConsole("1.Rectange");
            this._console.PrintInConsole("2.Circle");
            this._console.PrintInConsole("Click Any other number to exit!!");
            string? userChoice = this._console.GetInputFromConsole("option (1 or 2)");
            if (int.TryParse(userChoice, out int userChoiceNumber))
            {
                if (userChoiceNumber == (int)Shapes.Rectangle)
                {
                    this.ShowRectangleOption();
                }
                else if (userChoiceNumber == (int)Shapes.Circle)
                {
                    this.ShowCircleOption();
                }
                else
                {
                    return;
                }
            }
            else
            {
                this._console.PrintInvalid();
                this.ShowShapeOption();
            }
        }

        /// <summary>
        /// Show the option available in the rectangle
        /// </summary>
        public void ShowRectangleOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Rectangle Operations:");
            string? color = this._console.GetInputFromConsole("Color of the rectangle");
            RectangleShape rectangle = new (color);
            string? lengthIp = this._console.GetInputFromConsole("Length");
            string? widthIp = this._console.GetInputFromConsole("Width");
            if (int.TryParse(lengthIp, out int lengthNumber))
            {
                if (double.TryParse(widthIp, out double widthNumber))
                {
                    rectangle.CalculateArea(lengthNumber, widthNumber);
                    var (rectangleColor, rectangleArea) = rectangle.PrintDetails();
                    this._console.PrintInConsole($"The rectangle of {rectangleColor} color and area is {rectangleArea}");
                    this._console.WaitInConsole();
                    this.ShowShapeOption();
                }
                else
                {
                    this._console.PrintInvalid();
                }
            }
            else
            {
                this._console.PrintInvalid();
            }
        }

        /// <summary>
        /// Show the option available in circle
        /// </summary>
        public void ShowCircleOption()
        {
            this._console.ClearConsole();
            this._console.PrintInConsole("Circle Operations:");
            string? color = this._console.GetInputFromConsole("Color of the circle");
            Circle circle = new (color);
            string? radiusIp = this._console.GetInputFromConsole("Radius");
            if (int.TryParse(radiusIp, out int radiusNumber))
            {
                circle.CalculateArea(radiusNumber);
                var (circleColor, circleArea) = circle.PrintDetails();
                this._console.PrintInConsole($"The rectangle of {circleColor} color and area is {circleArea}");
                this._console.WaitInConsole();
                this.ShowShapeOption();
            }
            else
            {
                this._console.PrintInvalid();
            }
        }
    }
}
