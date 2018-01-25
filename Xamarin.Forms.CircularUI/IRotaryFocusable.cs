namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The IRotaryFocusable is an interface to take a Rotary Event
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public interface IRotaryFocusable
    {
    }

    /// <summary>
    /// IRotaryEventReceiver is an interface to take Rotary Events
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public interface IRotaryEventReceiver : IRotaryFocusable
    {
        /// <summary>
        /// Rotate it by the RotaryEventArgs value.
        /// </summary>
        /// <param name="args">Rotated direction of Rotary device</param>
        /// <since_tizen> 4 </since_tizen>
        void Rotate(RotaryEventArgs args);
    }
}