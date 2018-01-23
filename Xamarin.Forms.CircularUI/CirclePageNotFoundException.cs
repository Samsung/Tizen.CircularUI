using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// Exception class when CircleSurface has no child
    /// </summary>
    public class CirclePageNotFoundException : Exception
    {
        /// <summary>
        /// Creates and initializes a new instance of the CirclePageNotFoundException class.
        /// </summary>
        public CirclePageNotFoundException() : base("Circle widget must be child of Circle Page.")
        {
        }
    }
}
