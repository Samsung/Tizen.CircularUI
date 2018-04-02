using System;
using System.Collections.Generic;
using System.Text;

namespace CircularUI
{
    /// <summary>
    /// The CirclePageNotFoundException is an Exception class that occurs when a CircleSurface has no child.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class CirclePageNotFoundException : Exception
    {
        /// <summary>
        /// Creates and initializes a new instance of the CirclePageNotFoundException class.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public CirclePageNotFoundException() : base("Circle widget must be child of Circle Page.")
        {
        }
    }
}
