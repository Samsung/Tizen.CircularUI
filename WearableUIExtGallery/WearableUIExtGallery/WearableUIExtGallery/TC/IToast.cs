using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace WearableUIExtGallery.TC
{
    public interface IToast
    {
        void LongToast(string message);
        void ShortToast(string message);
    }
}