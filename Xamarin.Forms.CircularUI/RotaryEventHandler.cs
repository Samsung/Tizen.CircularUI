using System;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// Class that represents a triggering rotary event
    /// </summary>
    /// <param name="args"></param>
    public delegate void RotaryEventHandler(RotaryEventArgs args);

    /// <summary>
    /// RotaryEventArgs serve information for triggered rotary event.
    /// </summary>
    public class RotaryEventArgs : EventArgs
    {
        /// <summary>
        /// IsClockwise is true when Rotary device rotated clockwise direction or false on counter clockwise.
        /// </summary>
        public bool IsClockwise { get; set; }
    }
}