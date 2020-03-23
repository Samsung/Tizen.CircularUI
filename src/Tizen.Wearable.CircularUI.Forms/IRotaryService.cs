using System;
using System.Collections.Generic;
using System.Text;

namespace Tizen.Wearable.CircularUI.Forms
{
    interface IRotaryService
    {
        event EventHandler<RotaryEventArgs> Rotated;
    }
}
