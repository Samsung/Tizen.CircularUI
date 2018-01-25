using System;
using Xamarin.Forms;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The Toast class provides properties that show simple types of messages
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    /// <example>
    /// <code>
    /// Toast.DisplayText("Hello World", 3000)
    /// Toast.DisplayIconText("Hello World", new FileImageSource { File = "icon.jpg" }, 3000)
    /// </code>
    /// </example>
    public sealed class Toast
    {
        /// <summary>
        /// It shows the simplest form of the message.
        /// </summary>
        /// <param name="text">The body text of the toast.</param>
        /// <param name="duration">How long to display the text in milliseconds.</param>
        /// <since_tizen> 4 </since_tizen>
        public static void DisplayText(string text, int duration = 3000)
        {
            new ToastProxy
            {
                Text = text,
                Duration = duration,
            }.Show();
        }

        /// <summary>
        /// It shows simplest icon and text messege.
        /// </summary>
        /// <param name="text">The body text of the toast.</param>
        /// <param name="icon">The file path of the toast icon.</param>
        /// <param name="duration">How long to display the text in milliseconds.</param>
        /// <since_tizen> 4 </since_tizen>
        public static void DisplayIconText(string text, FileImageSource icon, int duration = 3000)
        {
            new ToastProxy
            {
                Text = text,
                Icon = icon,
                Duration = duration,
            }.Show();
        }
    }
}
