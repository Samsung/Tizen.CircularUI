using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace WearableUIGallery.TC
{
    public class MyImageData : INotifyPropertyChanged
    {
        bool _isSelected;

        public string Text { get; set; }

        public string Source { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TCIndexPageViewModel : INotifyPropertyChanged
    {

        public IList<MyImageData> SampleImageData { get; set; } = new ObservableCollection<MyImageData>();

        public TCIndexPageViewModel()
        {
            SampleImageData.Add(new MyImageData { Text = "Image 1", Source = "image/100_1.jpg"});
            SampleImageData.Add(new MyImageData { Text = "Image 2", Source = "image/100_2.jpg" });
            SampleImageData.Add(new MyImageData { Text = "Image 3", Source = "image/100_3.jpg" });
            SampleImageData.Add(new MyImageData { Text = "Image 4", Source = "image/100_4.jpg" });
            SampleImageData.Add(new MyImageData { Text = "Image 5", Source = "image/100_5.jpg" });
            SampleImageData.Add(new MyImageData { Text = "Image 6", Source = "image/100_6.jpg" });
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}