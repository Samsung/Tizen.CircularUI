using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.CircularUI
{
    public class CirclePageNotFoundException : Exception
    {
        public CirclePageNotFoundException() : base("Circle widget must be child of Circle Page.")
        {
        }
    }
}
