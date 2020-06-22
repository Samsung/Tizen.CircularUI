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
    class MainPageModel : INotifyPropertyChanged
    {
        Data _monthlyData;

        public string ValueFormat1 { get; set; }

        public string ValueFormat2 { get; set; }

        public Data DummyData { get; set; }

        public Data HRMDummyData { get; set; }

        public Data ColorDummyData { get; set; }

        public Data BarBGColorDummyData { get; set; }

        public Data WeeklyDummyData { get; set; }

        public Data MonthlyDummyData
        {
            get => _monthlyData;
            set
            {
                if (_monthlyData == value) return;
                _monthlyData = value;
                OnPropertyChanged();
            }
        }

        public Data GroupDummyData { get; set; }

        public Data GroupDummyData2 { get; set; }

        public Data LineDummyData { get; set; }

        public Data LineMonthlyDummyData1 { get; set; }

        public Data LineMonthlyDummyData2 { get; set; }

        public Data LineMultiDummyData { get; set; }

        public IList<IDataItem> ReferenceData1 { get; set; }

        public IList<IDataItem> ReferenceData2 { get; set; }

        public IList<IDataItem> MontlyReferenceData { get; set; }

        public AxisOption  MajorOnlyAxisOption { get; set; }

        public AxisOption ReferenceLineAxisOption { get; set; }

        public AxisOption MajorMinorAxisOption { get; set; }

        public AxisOption MontlyMajorMinorAxisOption { get; set; }

        public ICommand MontlyDataChangeCommand { get; private set; }

        public MainPageModel()
        {
            var SunTextItem = new TextItem { Text = "S", FontSize = 7, TextColor = Color.FromHex("#FF3A3D") };
            var MonTextItem = new TextItem { Text = "M", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") };
            var TueTextItem = new TextItem { Text = "T", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") };
            var WedTextItem = new TextItem { Text = "W", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") };
            var ThuTextItem = new TextItem { Text = "T", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") };
            var FriTextItem = new TextItem { Text = "F", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") };
            var SatTextItem = new TextItem { Text = "S", FontSize = 7, TextColor = Color.FromHex("#2176FF") };

            var dummyDataItems = new List<IDataItem>
            {
                new DataItem { Value = 5,ValueLabel = new TextItem { Text = "5", FontSize = 5, TextColor = Color.White } },
                new DataItem { Value = 8, ValueLabel = new TextItem { Text = "8", FontSize = 5, TextColor = Color.Red } },
                new DataItem { Value = 4, ValueLabel = new TextItem { Text = "4", FontSize = 5, TextColor = Color.Red } },
                null,
                new DataItem { Value = 10, ValueLabel = new TextItem { Text = "10", FontSize = 5 } },
            };

            var colorDummyDataItems = new List<IDataItem>
            {
                new DataItem {
                    Value = 5,
                    Color = Color.FromHex("#2176FF"),
                    Label = SunTextItem
                },
                new DataItem {
                    Value = 8,
                    Color = Color.FromHex("#00C7FF"),
                    Label = MonTextItem
                },
                new DataItem {
                    Value = 1,
                    Color = Color.FromHex("#00C7FF"),
                    Label = TueTextItem
                },
                new DataItem {
                    Value = 4,
                    Color = Color.FromHex("#A3B3BA"),
                    Label = WedTextItem,
                    ValueLabel = new TextItem { Text = "4", FontSize = 4, TextColor = Color.White }
                },
                new DataItem {
                    Value = 9,
                    Color = Color.FromHex("#4CBA2A"),
                    Label = ThuTextItem,
                    ValueLabel = new TextItem { Text = "9", FontSize = 4, TextColor = Color.White }
                },
                new DataItem {
                    Value = 0,
                    Color = Color.FromHex("#00C7FF"),
                    Label = FriTextItem
                },
                new DataItem {
                    Value = 8,
                    Color = Color.FromHex("#00C7FF"),
                    Label = SatTextItem,
                }
            };

            var HRMDataItems = new List<IDataItem>
            {
                new BarDataItem {
                    Value = 30,
                    Color = Color.DarkRed,
                    ValueLabel = new TextItem { Text = "30m", FontSize = 6, TextColor = Color.White },
                    ValueLabelPosition = ValueLabelPosition.Start
                },
                new BarDataItem {
                    Value = 90,
                    Color = Color.OrangeRed,
                    ValueLabel = new TextItem { Text = "1h 30m", FontSize = 6, TextColor = Color.White },
                    ValueLabelPosition = ValueLabelPosition.Start
                },
                new BarDataItem {
                    Value = 50,
                    Color = Color.Orange,
                    ValueLabel = new TextItem { Text = "50m", FontSize = 6, TextColor = Color.White },
                    ValueLabelPosition = ValueLabelPosition.Start
                }
            };

            var barBGDummyDataItems = new List<IDataItem>
            {
                new BarDataItem { 
                    Value = 5,
                    Color = Color.FromHex("#2176FF"),
                    BarBackgroundColor = Color.FromHex("#0F2752"),
                    Label = SunTextItem
                },
                new BarDataItem {
                    Value = 0,
                    Color = Color.FromHex("#2176FF"),
                    BarBackgroundColor = Color.FromHex("#0F2752"),
                    Label = MonTextItem
                },
                new BarDataItem {
                    Value = 8,
                    Color = Color.FromHex("#2176FF"),
                    BarBackgroundColor = Color.FromHex("#0F2752"),
                    Label = TueTextItem
                },
                new BarDataItem {
                    Value = 4,
                    Color = Color.FromHex("#FF4AC0"),
                    BarBackgroundColor = Color.FromHex("#401A34"),
                    Label = WedTextItem,
                    ValueLabel = new TextItem { Text = "4", FontSize = 5, TextColor = Color.White }
                },
                new BarDataItem {
                    Value = 9,
                    Color = Color.FromHex("#7EFA55"),
                    BarBackgroundColor = Color.FromHex("#152E00"),
                    Label = ThuTextItem,
                    ValueLabel = new TextItem { Text = "9", FontSize = 5, TextColor = Color.White }
                },
                new BarDataItem {
                    Value = 7,
                    Color = Color.FromHex("#2176FF"),
                    BarBackgroundColor = Color.FromHex("#0F2752"),
                    Label = FriTextItem,
                },
                new BarDataItem {
                    Value = 7,
                    Color = Color.FromHex("#2176FF"),
                    BarBackgroundColor = Color.FromHex("#0F2752"),
                    Label = SatTextItem,
                }
            };

            var WeeklyDataItems = new List<IDataItem>
            {
                new DataItem { Value = 3, Color = Color.Red, Label = SunTextItem },
                new DataItem { Value = 1, Color = Color.Orange, Label = MonTextItem },
                new DataItem { Value = 1, Color = Color.Yellow, Label = TueTextItem,
                    ValueLabel = new TextItem { Text = ""}}, //do not display value text
                new DataItem { Value = 9, Color = Color.Green, Label = WedTextItem },
                new DataItem { Value = 7, Color = Color.Blue, Label = ThuTextItem },
                new DataItem { Value = 8, Color = Color.Indigo, Label = FriTextItem },
                new DataItem { Value = 10, Color = Color.Purple, Label = SatTextItem,
                    ValueLabel = new TextItem { Text = "Max 10", FontSize = 5, TextColor = Color.LightGray } },
            };

            var monthlyDummyData = new List<IDataItem>
            {
                new DataItem(2340, new TextItem("JAN")),
                new DataItem(2650),
                new DataItem(2000),
                new DataItem(2810),
                new DataItem(2760),
                new DataItem(2100, new TextItem("JUN")),
                new DataItem(1850),
                new DataItem(1710),
                new DataItem(1300),
                new DataItem(1460),
                new DataItem(1300),
                new DataItem(2100, new TextItem("DEC") ),
            };

            var monthlyDummyData2 = new List<IDataItem>
            {
                new DataItem(1200, new TextItem("JAN")),
                new DataItem(1340),
                new DataItem(1490),
                new DataItem(1550),
                new DataItem(1630),
                new DataItem(1800, new TextItem("JUN")),
                new DataItem(2200),
                new DataItem(2370),
                new DataItem(2400),
                new DataItem(2450),
                new DataItem(2300),
                new DataItem(2650, new TextItem("DEC") ),
            };

            //var monthlyDummyData = new double[12] {2340, 2650, 2000, 2810, 2760, 2100, 1850, 1710, 1300, 1460, 1300, 2100};
            //var monthlyDummyData2 = new double[12] {1200, 1340, 1490, 1550, 1630, 1800, 2200, 2370, 2400, 2450, 2300, 2650};

            ReferenceData1 = new List<IDataItem>
            {
                new DataItem { Value = 0, ValueLabel = new TextItem{ Text = "0", FontSize = 5, TextColor = Color.White } },
                new DataItem { Value = 5, ValueLabel = new TextItem{ Text = "5", FontSize = 5, TextColor = Color.White } },
                new DataItem { Value = 10, ValueLabel = new TextItem{ Text = "10", FontSize = 5,  TextColor = Color.White } }
            };

            ReferenceData2 = new List<IDataItem>
            {
                new DataItem { Value = 5},
            };

            MontlyReferenceData = new List<IDataItem>
            {
                new DataItem { Value = 1500, ValueLabel = new TextItem{ Text = "1.5K", FontSize = 5, TextColor = Color.FromHex("#EF751D") } },
                new DataItem { Value = 2000, ValueLabel = new TextItem{ Text = "2.0K", FontSize = 5, TextColor = Color.FromHex("#EBF51D") } },
                new DataItem { Value = 2500, ValueLabel = new TextItem{ Text = "2.5K", FontSize = 5, TextColor = Color.FromHex("#72E61D") } },
                new DataItem { Value = 3000, ValueLabel = new TextItem{ Text = "3.0K", FontSize = 5,  TextColor = Color.FromHex("#41C7D1") } }
            };

            var groupDummyDataItems1 = new List<IDataItem>
            {
                new DataItem { Value = 6, Label = SunTextItem },
                new DataItem { Value = 8, Label = MonTextItem },
                new DataItem { Value = 7, Label = TueTextItem },
                new DataItem { Value = 8, Label = WedTextItem },
                new DataItem { Value = 9, Label = ThuTextItem },
                new DataItem { Value = 0, Label = FriTextItem },
                new DataItem { Value = 0, Label = SatTextItem },
            };

            var groupDummyDataItems2 = new List<IDataItem>
            {
                new DataItem { Value = 8 },
                new DataItem { Value = 7 },
                new DataItem { Value = 9 },
                new DataItem { Value = 0 },
                new DataItem { Value = 8 },
            };

            var groupDummyDataItems3 = new List<IDataItem>
            {
                new DataItem { Value = 4 },
                new DataItem { Value = 5 },
                new DataItem { Value = 0 },
                new DataItem { Value = 6 },
                new DataItem { Value = 0 },
                new DataItem { Value = 6 },
                new DataItem { Value = 7 },
            };

            var groupDummyDataItems4 = new List<IDataItem>
            {
                new DataItem { Value = 6 },
                new DataItem { Value = 2 },
                new DataItem { Value = 2 },
                new DataItem { Value = 3 },
                new DataItem { Value = 4 },
            };

            var groupDummyDataItems5 = new List<IDataItem>
            {
                new DataItem() { Value = 3 },
                new DataItem() { Value = 3 },
            };

            var groupDummyDataItems6 = new List<IDataItem>
            {
                new DataItem(3),
                new DataItem(6),
                new DataItem(2),
                new DataItem(3),
                new DataItem(5),
            };

            //Single Bar Chart dataset
            var dummyDataItemGroup = new DataItemGroup(dummyDataItems, "dummy data");
            dummyDataItemGroup.Color = Color.Yellow;
            var HRMDataItemGroup = new DataItemGroup(HRMDataItems, "dummy HRM data");
            var colorDataItemGroup = new DataItemGroup(colorDummyDataItems, "Weekly data with each entry color");
            colorDataItemGroup.Color = Color.Green;
            var barBGColorItemGroup = new DataItemGroup(barBGDummyDataItems, "Weekly data with entry color and bar background color");
            var montlyDataItemGroup = new DataItemGroup(monthlyDummyData, "Monthly data");
            var montlyDataItemGroup2 = new DataItemGroup(monthlyDummyData2, "Monthly data2");

            //Group-Bar Chart dataset
            var dataItemGroupList1 = new List<DataItemGroup>();
            var dataItemGroupList2 = new List<DataItemGroup>();
            var groupDataItemGroup1 = new BarDataItemGroup(groupDummyDataItems1, "Group data 1");
            groupDataItemGroup1.Color = Color.FromHex("#FF49BF");
            groupDataItemGroup1.BarBackgroundColor = Color.FromHex("#3F1933");
            dataItemGroupList1.Add(groupDataItemGroup1);
            dataItemGroupList2.Add(groupDataItemGroup1);

            var groupDataItemGroup2 = new BarDataItemGroup(groupDummyDataItems2, "Group data 2");
            groupDataItemGroup2.Color = Color.FromHex("#7EF954");
            groupDataItemGroup2.BarBackgroundColor = Color.FromHex("#142D00");
            dataItemGroupList1.Add(groupDataItemGroup2);
            dataItemGroupList2.Add(groupDataItemGroup2);

            var groupDataItemGroup3 = new BarDataItemGroup(groupDummyDataItems3, "Group data 3");
            groupDataItemGroup3.Color = Color.FromHex("#2176FF");
            groupDataItemGroup3.BarBackgroundColor = Color.FromHex("#0E2751");
            dataItemGroupList1.Add(groupDataItemGroup3);
            dataItemGroupList2.Add(groupDataItemGroup3);

            var groupDataItemGroup4 = new BarDataItemGroup(groupDummyDataItems4, "Group data 4");
            groupDataItemGroup4.Color = Color.Red;
            dataItemGroupList2.Add(groupDataItemGroup3);

            var groupDataItemGroup5 = new BarDataItemGroup(groupDummyDataItems5, "Group data 5");
            groupDataItemGroup5.Color = Color.Yellow;
            dataItemGroupList2.Add(groupDataItemGroup5);

            var groupDataItemGroup6 = new BarDataItemGroup(groupDummyDataItems6, "Group data 6");
            groupDataItemGroup6.Color = Color.Yellow;
            dataItemGroupList2.Add(groupDataItemGroup6);

            //Single Line Chart dataset
            var lineDataItemGroup = new LineDataItemGroup(colorDummyDataItems, "Weekly data with each color and fg color");
            lineDataItemGroup.ForegroundColor = Color.LightSkyBlue.MultiplyAlpha(0.4);

            var lineMonthlyDataItemGroup1 = new LineDataItemGroup(monthlyDummyData, "Montly data with each color and fg color");
            lineMonthlyDataItemGroup1.ForegroundColor = Color.LightPink.MultiplyAlpha(0.4);

            var lineMonthlyDataItemGroup2 = new LineDataItemGroup(monthlyDummyData2, "Montly data with each color and fg color");
            lineMonthlyDataItemGroup2.ForegroundColor = Color.LightYellow.MultiplyAlpha(0.4);

            DummyData = new Data(dummyDataItemGroup);
            HRMDummyData = new Data(HRMDataItemGroup);
            ColorDummyData = new Data(colorDataItemGroup);
            BarBGColorDummyData = new Data(barBGColorItemGroup);
            GroupDummyData = new Data(dataItemGroupList1);
            GroupDummyData2 = new Data(dataItemGroupList2);

            var MontlyData1 = new Data(montlyDataItemGroup);
            var MontlyData2 = new Data(montlyDataItemGroup2);
            MonthlyDummyData = MontlyData1;

            LineDummyData = new Data(lineDataItemGroup);
            LineMonthlyDummyData1 = new Data(lineMonthlyDataItemGroup1);
            LineMonthlyDummyData2 = new Data(lineMonthlyDataItemGroup2);

            LineMultiDummyData = new Data();
            LineMultiDummyData.DataItemGroups.Add(lineMonthlyDataItemGroup1);
            LineMultiDummyData.DataItemGroups.Add(lineMonthlyDataItemGroup2);

            //Pie dataset
            var weeklyDataItemGroup = new DataItemGroup(WeeklyDataItems, "weekly dataitems");
            WeeklyDummyData = new Data(weeklyDataItemGroup);

            var option1 = new AxisOption(true, false);
            option1.AxisLineColor = Color.Gray;
            MajorOnlyAxisOption = option1;


            option1.ReferenceDataItems = ReferenceData2;
            option1.IsVisibleOfReferenceLine = true;
            ReferenceLineAxisOption = option1;

            var option2 = new AxisOption(true, true, true, true);
            option2.ReferenceDataItems = ReferenceData1;
            MajorMinorAxisOption = option2;

            var option3 = new AxisOption(true, true, true, true);
            option3.ReferenceDataItems = MontlyReferenceData;
            MontlyMajorMinorAxisOption = option3;

            MontlyDataChangeCommand = new Command((obj) =>
            {
                Console.WriteLine("MontlyDataChangeCommand");
                if (MonthlyDummyData == MontlyData1)
                    MonthlyDummyData = MontlyData2;
                else
                    MonthlyDummyData = MontlyData1;
            });

            ValueFormat1 = "{0:0} $";
            ValueFormat2 = "{0:0.#} min";
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
