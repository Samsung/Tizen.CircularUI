using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The ConfirmArguments is a class that controls arguments of confirm popup
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class ConfirmArguments
    {
        /// <summary>
        /// Creates and initializes a new instance of the ConfirmArguments class
        /// </summary>
        /// <param name="view">View of Confirm popup</param>
        /// <param name="accept">Text of Accept button</param>
        /// <param name="cancel">Text of Cancel button</param>
        /// <since_tizen> 4 </since_tizen>
        public ConfirmArguments(View view, string accept, string cancel)
        {
            View = view;
            Accept = accept;
            Cancel = cancel;
            Result = new TaskCompletionSource<bool>();
        }

        /// <summary>
        /// Gets a view of Confirm popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public View View { get; private set; }
        /// <summary>
        /// Gets text of Accept button
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string Accept { get; private set; }
        /// <summary>
        /// Gets text of Cancel button
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string Cancel { get; private set; }

        /// <summary>
        /// Gets result of the button the user selected in the popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public TaskCompletionSource<bool> Result { get; }

        /// <summary>
        /// Sets result of the button the user selected in the popup
        /// </summary>
        /// <param name="result">Result of the button the user selected</param>
        /// <since_tizen> 4 </since_tizen>
        public void SetResult(bool result)
        {
            Result.TrySetResult(result);
        }
    }
}
