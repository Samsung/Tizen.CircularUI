namespace Xamarin.Forms.CircularUI
{
    public interface IRotaryFocusable
    {
    }

    public interface IRotaryEventReceiver : IRotaryFocusable
    {
        event RotaryEventHandler Rotated;
        void Rotate(RotaryEventArgs args);
    }
}