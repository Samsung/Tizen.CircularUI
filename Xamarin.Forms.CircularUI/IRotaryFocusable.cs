namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// Interface to take a Rotary Event
    /// </summary>
    public interface IRotaryFocusable
    {
    }

    /// <summary>
    /// Receiver interface to take Rotary Events
    /// </summary>
    public interface IRotaryEventReceiver : IRotaryFocusable
    {
        /// <summary>
        /// Rotate it by the RotaryEventArgs value.
        /// </summary>
        /// <param name="args"></param>
        void Rotate(RotaryEventArgs args);
    }
}