namespace Xamarin.Forms.CircularUI
{
    public interface IRotaryFocusable
    {
    }

    public interface IRotaryEventReceiver : IRotaryFocusable
    {
        void Rotate(RotaryEventArgs args);
    }
}