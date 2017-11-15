using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xamarin.Forms.CircularUI.Renderer
{
    public static class CircularUI
    {
        public static bool IsInitialized { get; private set; }
        public static void Init()
        {
            if (IsInitialized) return;
            IsInitialized = true;
        }
    }
}
