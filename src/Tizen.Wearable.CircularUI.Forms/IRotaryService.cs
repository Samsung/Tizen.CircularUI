using System;

namespace Tizen.Wearable.CircularUI.Forms
{
    interface IRotaryService
    {
        event EventHandler<RotaryEventArgs> Rotated;
    }
}
