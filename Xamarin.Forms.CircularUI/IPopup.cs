using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.CircularUI
{
    public interface IPopup
    {
        event EventHandler BackButtonPressed;

        View Content { get; set; }

        MenuItem FirstButton { get; set; }

        MenuItem SecondButton { get; set; }

        string Title { get; set; }

        string Text { get; set; }

        void Show();

        void Dismiss();
    }
}