using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    public class CircularShell : Shell
    {
        protected override bool OnBackButtonPressed()
        {
            if (FlyoutIsPresented)
            {
                FlyoutIsPresented = false;
                return true;
            }
            return base.OnBackButtonPressed();
        }
    }
}
