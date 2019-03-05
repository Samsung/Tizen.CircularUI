using System;

using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCIndexPageFocus : IndexPage
    {
        public TCIndexPageFocus()
        {
            InitializeComponent();
        }

        void OnFocused1(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnFocused1");
            firstPage.RotaryFocusObject = stepper1;
        }

        void OnFocused2(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnFocused2");
            firstPage.RotaryFocusObject = stepper2;
        }

        void OnFocused3(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnFocused3");
            thirdPage.RotaryFocusObject = stepper3;
        }

        void OnFocused4(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnFocused4");
            thirdPage.RotaryFocusObject = stepper4;
        }

        void OnEntryFocused1(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnEntryFocused1");
        }

        void OnEntryFocused2(object sender, ValueChangedEventArgs args)
        {
            Console.WriteLine("OnEntryFocused2");
        }
    }
}