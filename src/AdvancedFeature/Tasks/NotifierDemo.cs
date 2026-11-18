using AdvancedFeature.View;

namespace AdvancedFeature.Tasks
{
    /// <summary>
    /// Understand and implement events and delegate using notification.
    /// </summary>
    public class NotifierDemo
    {
        /// <summary>
        /// Entry method to demonstrate the notification service using events and delegates.
        /// </summary>
        public void NotificationService()
        {
            ConsoleActivity.ShowHeader("Notification Bar");
            ConsoleActivity.PrintEmptyLine();
            Thread.Sleep(2000);
            Notifier notifier = new Notifier();
            notifier.OnAction += this.AlertUser;
            notifier.PerformAction("User notification raised!!");
            Thread.Sleep(2000);
            ConsoleActivity.PrintEmptyLine();
            notifier.PerformAction("New notification!!");
            notifier.OnAction -= this.AlertUser;
            ConsoleActivity.WaitInConsole();
        }

        /// <summary>
        /// Event handler that match with the method signature and alert the user via console.
        /// </summary>
        /// <param name="message">The message used to alert the users.</param>
        public void AlertUser(string message)
        {
            ConsoleActivity.PrintInConsole(message);
        }

        /// <summary>
        /// Notify the user with the message.
        /// </summary>
        public class Notifier
        {
            /// <summary>
            /// The delegate that notify the user with the message.
            /// </summary>
            /// <param name="message">The actual notification message.</param>
            public delegate void Notify(string message);

            /// <summary>
            /// Event.
            /// </summary>
            public event Notify? OnAction;

            /// <summary>
            /// Trigger the event safely.
            /// </summary>
            /// <param name="message">Message to be passed to subscribers.</param>
            public void PerformAction(string message)
            {
                if (this.OnAction != null)
                {
                    this.OnAction(message);
                }
            }
        }
    }
}
