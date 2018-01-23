using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The class that controls arguments of confirm popup
    /// </summary>
    public class ConfirmArguments
    {
        /// <summary>
        /// Creates and initializes a new instance of the ConfirmArguments class
        /// </summary>
        /// <param name="view">view</param>
        /// <param name="accept">accept button text</param>
        /// <param name="cancel">cancel button text</param>
        public ConfirmArguments(View view, string accept, string cancel)
        {
            View = view;
            Accept = accept;
            Cancel = cancel;
            Result = new TaskCompletionSource<bool>();
        }

        /// <summary>
        /// View of Confirm popup
        /// </summary>
        public View View { get; private set; }
        /// <summary>
        /// Gets or sets text of accept button
        /// </summary>
        public string Accept { get; private set; }
        /// <summary>
        /// Gets or sets text of cancel button
        /// </summary>
        public string Cancel { get; private set; }

        /// <summary>
        /// The result of the button the user selected in the popup
        /// </summary>
        public TaskCompletionSource<bool> Result { get; }

        /// <summary>
        /// Set result of the button the user selected in the popup
        /// </summary>
        /// <param name="result"></param>
        public void SetResult(bool result)
        {
            Result.TrySetResult(result);
        }
    }
}
