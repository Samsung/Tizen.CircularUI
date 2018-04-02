using System;

namespace CircularUI
{
    /// <summary>
    /// Delegate for a triggering rotary event
    /// </summary>
    /// <param name="args">Rotated direction of Rotary device</param>
    /// <since_tizen> 4 </since_tizen>
    public delegate void RotaryEventHandler(RotaryEventArgs args);

    /// <summary>
    /// Event arguments for RotaryEvent.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class RotaryEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets rotated direction of Rotary device. IsClockwise is true when Rotary device rotated in the clockwise direction or false on counter clockwise.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsClockwise { get; set; }
    }
}