using System;
using System.Windows.Input;
using Xamarin.Forms;
using CircularUI;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCContextPopup : ContentPage
    {
        bool _popupVisibility = false;
        ContextPopup _popup;

        public TCContextPopup()
        {
            InitializeComponent();

            _popup = new ContextPopup();
            var item1 = new ContextPopupItem("item 1");
            var item2 = new ContextPopupItem("item 2");

            _popup.Items.Add(item1);
            _popup.Items.Add(item2);

            _popup.ItemSelected += (s, e) =>
            {
                Console.WriteLine($"{_popup.SelectedItem?.Label} is selected");
                label1.Text = _popup.SelectedItem?.Label + " is selected!";
            };

            _popup.Dismissed += (s, e) =>
            {
                if (_popupVisibility)
                {
                    Console.WriteLine("Popup is dismissed");
                    label1.Text = "Popup is dismissed!";
                    _popupVisibility = false;
                }
            };
        }

        private void OnClicked(object sender, EventArgs e)
        {
            Console.WriteLine($"button1.Clicked _popupVisibility:{_popupVisibility}");
            if (!_popupVisibility)
            {
                _popup.Show(button1, button1.Width / 2, button1.Height);
                _popupVisibility = true;
                label1.Text = "Popup is shown!";
            }
        }
    }
}