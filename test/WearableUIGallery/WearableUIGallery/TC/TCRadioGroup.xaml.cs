/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tizen.Wearable.CircularUI.Forms;
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