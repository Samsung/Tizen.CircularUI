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

using Tizen.Wearable.CircularUI.Chart.Forms;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CircularUIChartGallery.ViewModel
{
    class ChartDataTestModel : INotifyPropertyChanged
    {
        int _index;
        double[] value = new double[7] {5, 3, 6, 9, 8, 10, 2};
        Data _weeklyData;
        DataItemGroup _weeklyDataItemGroup;

        public Data WeeklyData
        {
            get => _weeklyData;
            set
            {
                if (_weeklyData == value) return;
                Console.WriteLine("WeeklyData set value");
                _weeklyData = value;
                OnPropertyChanged();
            }
        }

        DataItemGroup WeeklyDataItemGroup
        {
            get => _weeklyDataItemGroup;
            set
            {
                if (_weeklyDataItemGroup == value) return;
                Console.WriteLine("WeeklyDataItemGroup set value");
                _weeklyDataItemGroup = value;
                OnPropertyChanged();
            }
        }

        public IList<IDataItem> ReferenceData { get; set; }

        public IList<CategoryLabel> WeeklyLabel { get; set; }

        public AxisOption MajorMinorAxisOption { get; set; }

        public ICommand AddDataItemCommand { get; }

        public ICommand RemoveDataItemCommand { get; }

        public ChartDataTestModel()
        {
            _index = 0;
            WeeklyDataItemGroup = new DataItemGroup();
            WeeklyDataItemGroup.Color = Color.SkyBlue;
            WeeklyData = new Data();
            WeeklyData.DataItemGroups.Add(WeeklyDataItemGroup);

            WeeklyLabel = new List<CategoryLabel>
            {
                new CategoryLabel{ItemIndex = 1,  Label = new TextItem { Text = "S", FontSize = 7, TextColor = Color.FromHex("#FF3A3D") } },
                new CategoryLabel{ItemIndex = 2,  Label = new TextItem { Text = "M", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{ItemIndex = 3,  Label = new TextItem { Text = "T", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{ItemIndex = 4,  Label = new TextItem { Text = "W", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{ItemIndex = 5,  Label = new TextItem { Text = "T", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{ItemIndex = 6,  Label = new TextItem { Text = "F", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{ItemIndex = 7,  Label = new TextItem { Text = "S", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } }
            };

            ReferenceData = new List<IDataItem>
            {
                new DataItem { Value = 0, ValueLabel = new TextItem{ Text = "0", FontSize = 5, TextColor = Color.White } },
                new DataItem { Value = 5, ValueLabel = new TextItem{ Text = "5", FontSize = 5, TextColor = Color.White } },
                new DataItem { Value = 10, ValueLabel = new TextItem{ Text = "10", FontSize = 5,  TextColor = Color.White } }
            };

            var option = new AxisOption(true, true, true, true);
            option.CategoryLabels = WeeklyLabel;
            option.ReferenceDataItems = ReferenceData;
            option.AxisLineColor = Color.White;
            MajorMinorAxisOption = option;


            AddDataItemCommand = new Command(AddDataItem);
            RemoveDataItemCommand = new Command(RemoveDataItem);
        }

        void AddDataItem(object sender)
        {
            if (_index > 6) return;

            WeeklyDataItemGroup.DataItems.Add(new DataItem(value[_index]));
            Console.WriteLine($"AddDataItem index:{_index}, value:{value[_index]}");
            _index++;
            var chart = sender as ChartView;
            if (chart != null) chart.UpdateChart();
        }

        void RemoveDataItem(object sender)
        {
            if (_index <= 0) return;

            if (WeeklyDataItemGroup.DataItems.Count > 0)
            {
                _index--;
                WeeklyDataItemGroup.DataItems.RemoveAt(_index);
                Console.WriteLine($"RemoveDataItem index:{_index}");
                var chart = sender as ChartView;
                if (chart != null) chart.UpdateChart();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
