using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace WearableUIGallery.Extensions
{
    public interface IPopup
    {
        View Content { get; set; }

        MenuItem BottomButton { get; set; }

        string Title { get; set; }

        string Text { get; set; }

        void Show();
    }
}