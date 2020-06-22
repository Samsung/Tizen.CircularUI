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
    class SimpleChartModel : INotifyPropertyChanged
    {
        public Data WeeklyData { get; set; }

        public Data GroupWeeklyData { get; set; }

        public AxisOption MajorOnlyAxisOption { get; set; }

        public AxisOption MajorMinorAxisOption { get; set; }

        public SimpleChartModel()
        {
            var sunTextItem = new TextItem { Text = "S", FontSize = 7, TextColor = Color.Red };
            var monTextItem = new TextItem { Text = "M", FontSize = 7 };
            var tueTextItem = new TextItem { Text = "T", FontSize = 7 };
            var wedTextItem = new TextItem { Text = "W", FontSize = 7 };
            var thuTextItem = new TextItem { Text = "T", FontSize = 7 };
            var friTextItem = new TextItem { Text = "F", FontSize = 7 };
            var satTextItem = new TextItem { Text = "S", FontSize = 7 };

            var dataItems = new List<IDataItem>
            {
                new DataItem {
                    Value = 5,
                    Label = sunTextItem,
                },
                new DataItem {
                    Value = 8,
                    Label = monTextItem
                },
                new DataItem {
                    Value = 2,
                    Label = tueTextItem,
                    Color = Color.FromHex("#FF4AC0"),
                    ValueLabel = new TextItem { Text = "2", FontSize = 4, TextColor = Color.White }
                },
                new DataItem {
                    Value = 4,
                    Label = wedTextItem,
                },
                new DataItem {
                    Value = 9,
                    Label = thuTextItem,
                    Color = Color.FromHex("#7EFA55"),
                    ValueLabel = new TextItem { Text = "9", FontSize = 4, TextColor = Color.White }
                },
                new DataItem {
                    Value = 0,
                    Label = friTextItem
                },
                new DataItem {
                    Value = 8,
                    Label = satTextItem,
                }
            };

            var groupDummyDataItems1 = new List<IDataItem>
            {
                new DataItem { Value = 6, Label = sunTextItem },
                new DataItem { Value = 8, Label = monTextItem },
                new DataItem { Value = 7, Label = tueTextItem },
                new DataItem { Value = 8, Label = wedTextItem },
                new DataItem { Value = 0, Label = thuTextItem },
                new DataItem { Value = 0, Label = friTextItem },
                new DataItem { Value = 0, Label = satTextItem },
            };

            var groupDummyDataItems2 = new List<IDataItem>
            {
                new DataItem { Value = 7 },
                new DataItem { Value = 10 },
                new DataItem { Value = 9 },
                new DataItem { Value = 3 },
            };

            var groupDummyDataItems3 = new List<IDataItem>
            {
                new DataItem { Value = 9 },
                new DataItem { Value = 4 },
                new DataItem { Value = 8 },
                new DataItem { Value = 5 },
            };

            //Single Bar Chart dataset
            var dataItemGroup = new DataItemGroup(dataItems, "test data set");
            dataItemGroup.Color = Color.FromHex("#2176FF");

            //Group-Bar Chart dataset
            var dataItemGroupList = new List<DataItemGroup>();
            var groupDataItemGroup1 = new DataItemGroup(groupDummyDataItems1, "Group data 1");
            var groupDataItemGroup2 = new DataItemGroup(groupDummyDataItems2, "Group data 2");
            var groupDataItemGroup3 = new DataItemGroup(groupDummyDataItems3, "Group data 3");
            groupDataItemGroup1.Color = Color.FromHex("#FF49BF");
            groupDataItemGroup2.Color = Color.FromHex("#7EF954");
            groupDataItemGroup3.Color = Color.FromHex("#2176FF");
            dataItemGroupList.Add(groupDataItemGroup1);
            dataItemGroupList.Add(groupDataItemGroup2);
            dataItemGroupList.Add(groupDataItemGroup3);

            WeeklyData = new Data(dataItemGroup);
            GroupWeeklyData = new Data(dataItemGroupList);

            var option1 = new AxisOption(true, false);
            MajorOnlyAxisOption = option1;

            var option2 = new AxisOption(true, true, true, true);
            option2.ReferenceDataItems = new List<IDataItem>
            {
                new DataItem { Value = 0, ValueLabel = new TextItem{ Text = "0", FontSize = 5, TextColor = Color.White } },
                new DataItem { Value = 5, ValueLabel = new TextItem{ Text = "5", FontSize = 5, TextColor = Color.White } },
                new DataItem { Value = 10, ValueLabel = new TextItem{ Text = "10", FontSize = 5,  TextColor = Color.White } }
            };

            MajorMinorAxisOption = option2;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
