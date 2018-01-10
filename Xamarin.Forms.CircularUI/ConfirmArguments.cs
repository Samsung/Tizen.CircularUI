using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xamarin.Forms.CircularUI
{
    public class ConfirmArguments
    {
        public ConfirmArguments(View view, string accept, string cancel)
        {
            View = view;
            Accept = accept;
            Cancel = cancel;
            Result = new TaskCompletionSource<bool>();
        }

        public View View { get; private set; }
        public string Accept { get; private set; }
        public string Cancel { get; private set; }

        public TaskCompletionSource<bool> Result { get; }
        public void SetResult(bool result)
        {
            Result.TrySetResult(result);
        }
    }
}
