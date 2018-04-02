using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CircularUI;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{

    public class RadioGroupViewModel
    {
        public IList<MyCustomData> SampleData { get; set; } = new ObservableCollection<MyCustomData>();

        public RadioGroupViewModel()
        {
            SampleData.Add(new MyCustomData() { Text = "No off", Value = "NoOff", GroupName = "timeout", IsSelected = false });
            SampleData.Add(new MyCustomData() { Text = "15 seconds", Value = "15s", GroupName = "timeout", IsSelected = false });
            SampleData.Add(new MyCustomData() { Text = "30 seconds", Value = "30s", GroupName = "timeout", IsSelected = true });
            SampleData.Add(new MyCustomData() { Text = "1 minute", Value = "1m", GroupName = "timeout", IsSelected = false });
            SampleData.Add(new MyCustomData() { Text = "5 minute", Value = "5m", GroupName = "timeout", IsSelected = false });
            SampleData.Add(new MyCustomData() { Text = "10 minute", Value = "10m", GroupName = "timeout", IsSelected = false });
            SampleData.Add(new MyCustomData() { Text = "15 minute", Value = "15m", GroupName = "timeout", IsSelected = false });
        }
    }

    public class MyCustomData
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public bool IsSelected { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCRadioGroup : CirclePage
    {
        public TCRadioGroup()
        {
            InitializeComponent();
        }

        public void OnSelected(object sender, SelectedEventArgs args)
        {
            //Console.WriteLine($"<<OnSelected>>  sender:{sender.GetHashCode()}, value:{args.Value}");
            Radio radio = sender as Radio;
            if(radio != null) Console.WriteLine($"<<OnSelected>>  Radio Value:{radio.Value}, GroupName:{radio.GroupName}, IsSelected:{radio.IsSelected}");
        }
    }
}