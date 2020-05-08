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
using System.Linq;

namespace CircularUIChartGallery.ViewModel
{
    class MainPageModel
    {
        public IList<DataItemGroup> DummyData { get; set; }

        public IList<DataItemGroup> HRMDummyData { get; set; }

        public IList<DataItemGroup> ColorDummyData { get; set; }

        public IList<DataItemGroup> BarBGColorDummyData { get; set; }

        public IList<DataItemGroup> MonthlyDummyData { get; set; }

        public IList<DataItemGroup> GroupDummyData { get; set; }

        public IList<DataItem> ReferenceData1 { get; set; }

        public IList<DataItem> ReferenceData2 { get; set; }

        public IList<DataItem> MontlyReferenceData { get; set; }

        public IList<CategoryLabel> WeeklyLabel { get; set; }

        public IList<CategoryLabel> MonthlyLabel { get; set; }

        public AxisOption  MajorOnlyAxisOption { get; set; }

        public AxisOption CategoryOnlyAxisOption { get; set; }

        public AxisOption CategoryReferenceLineAxisOption { get; set; }

        public AxisOption MajorMinorAxisOption { get; set; }

        public AxisOption MontlyMajorMinorAxisOption { get; set; }

        public MainPageModel()
        {
            var dummyEntries = new List<IDataItem>
            {
                new DataItem { Key = 1, Value = 5, ValueText = new TextItem { Text = "5", FontSize = 5, TextColor = Color.White } },
                new DataItem { Key = 2, Value = 8, ValueText = new TextItem { Text = "8", FontSize = 5, TextColor = Color.White } },
                new DataItem { Key = 4, Value = 4, ValueText = new TextItem { Text = "4", FontSize = 5, TextColor = Color.White } },
                new DataItem { Key = 6, Value = 10, ValueText = new TextItem { Text = "10", FontSize = 5, TextColor = Color.White } },
            };

            var colorDummyEntries = new List<IDataItem>
            {
                new DataItem { Value = 5, Color = Color.FromHex("#00C7FF") },
                new DataItem { Value = 8, Color = Color.FromHex("#00C7FF") },
                new DataItem { Value = 4, Color = Color.FromHex("#A3B3BA"), ValueText = new TextItem { Text = "4", FontSize = 4, TextColor = Color.White } },
                new DataItem { Value = 9, Color = Color.FromHex("#4CBA2A"), ValueText = new TextItem { Text = "9", FontSize = 4, TextColor = Color.White } },
                new DataItem() { Value = 7, Color = Color.FromHex("#00C7FF") },
                new DataItem() { Value = 8, Color = Color.FromHex("#00C7FF") }
            };

            var HRMEntries = new List<IDataItem>
            {
                new DataItem { Value = 3, Color = Color.DarkRed, ValueText = new TextItem { Text = "12m", FontSize = 9, TextColor = Color.White } },
                new DataItem { Value = 9, Color = Color.OrangeRed, ValueText = new TextItem { Text = "1h 3m", FontSize = 9, TextColor = Color.White } },
                new DataItem { Value = 4, Color = Color.Orange, ValueText = new TextItem { Text = "17m", FontSize = 9, TextColor = Color.White } }
            };

            var barBGDummyEntries = new List<IDataItem>
            {
                new BarDataItem { Value = 5, Color = Color.FromHex("#2176FF"), BarBackgroundColor = Color.FromHex("#0F2752") },
                new BarDataItem { Key = 3, Value = 8, Color = Color.FromHex("#2176FF"), BarBackgroundColor = Color.FromHex("#0F2752") },
                new BarDataItem { Value = 4, Color = Color.FromHex("#FF4AC0"), BarBackgroundColor = Color.FromHex("#401A34"), ValueText = new TextItem { Text = "4", FontSize = 5, TextColor = Color.White } },
                new BarDataItem { Value = 9, Color = Color.FromHex("#7EFA55"), BarBackgroundColor = Color.FromHex("#152E00"), ValueText = new TextItem { Text = "9", FontSize = 5, TextColor = Color.White } },
                new BarDataItem { Value = 7, Color = Color.FromHex("#2176FF"), BarBackgroundColor = Color.FromHex("#0F2752") },
                new BarDataItem { Key = 7, Value = 7, Color = Color.FromHex("#2176FF"), BarBackgroundColor = Color.FromHex("#0F2752") }
            };

            var monthlyDummyEntries = new List<IDataItem>
            {
                new DataItem { Value = 2340 },
                new DataItem { Value = 2650 },
                new DataItem { Value = 2000 },
                new DataItem { Value = 2810 },
                new DataItem { Value = 2760 },
                new DataItem { Value = 2100 },
                new DataItem { Value = 1850 },
                new DataItem { Value = 1710 },
                new DataItem { Value = 1300 },
                new DataItem { Value = 1460 },
                new DataItem { Value = 1300 },
                new DataItem { Value = 2100 },
            };

            var monthlyDummyEntries2 = new List<IDataItem>
            {
                new DataItem { Value = 1200 },
                new DataItem { Value = 1340 },
                new DataItem { Value = 1490 },
                new DataItem { Value = 1550 },
                new DataItem { Value = 1630 },
                new DataItem { Value = 1800 },
                new DataItem { Value = 2200 },
                new DataItem { Value = 2370 },
                new DataItem { Value = 2400 },
                new DataItem { Value = 2450 },
                new DataItem { Value = 2300 },
                new DataItem { Value = 2650 },
            };

            //set Color at dummy data
            var count = monthlyDummyEntries.Count();
            for (int i = 0; i < count; i++)
            {
                monthlyDummyEntries.ElementAt(i).SetMonthlyDummyColor();
            }

            count = monthlyDummyEntries2.Count();
            for (int i = 0; i < count; i++)
            {
                monthlyDummyEntries2.ElementAt(i).SetMonthlyDummyColor();
            }

            WeeklyLabel = new List<CategoryLabel>
            {
                new CategoryLabel{Key = 1,  Label = new TextItem { Text = "S", FontSize = 7, TextColor = Color.FromHex("#FF3A3D") } },
                new CategoryLabel{Key = 2,  Label = new TextItem { Text = "M", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{Key = 3,  Label = new TextItem { Text = "T", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{Key = 4,  Label = new TextItem { Text = "W", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{Key = 5,  Label = new TextItem { Text = "T", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{Key = 6,  Label = new TextItem { Text = "F", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{Key = 7,  Label = new TextItem { Text = "S", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } }
            };

            MonthlyLabel = new List<CategoryLabel>
            {
                new CategoryLabel{Key = 1,  Label = new TextItem { Text = "JAN", FontSize = 5 } },
                new CategoryLabel{Key = 6,  Label = new TextItem { Text = "JUN", FontSize = 5 } },
                new CategoryLabel{Key = 12,  Label = new TextItem { Text = "DEC", FontSize = 5, TextColor = Color.White } }
            };

            ReferenceData1 = new List<DataItem>
            {
                new DataItem { Value = 0, ValueText = new TextItem{ Text = "0", FontSize = 5, TextColor = Color.White } },
                new DataItem { Value = 5, ValueText = new TextItem{ Text = "5", FontSize = 5, TextColor = Color.White } },
                new DataItem { Value = 10, ValueText = new TextItem{ Text = "10", FontSize = 5,  TextColor = Color.White } }
            };

            ReferenceData2 = new List<DataItem>
            {
                new DataItem { Value = 5},
            };

            MontlyReferenceData = new List<DataItem>
            {
                new DataItem { Value = 1500, ValueText = new TextItem{ Text = "1.5K", FontSize = 5, TextColor = Color.FromHex("#EF751D") } },
                new DataItem { Value = 2000, ValueText = new TextItem{ Text = "2.0K", FontSize = 5, TextColor = Color.FromHex("#EBF51D") } },
                new DataItem { Value = 2500, ValueText = new TextItem{ Text = "2.5K", FontSize = 5, TextColor = Color.FromHex("#72E61D") } },
                new DataItem { Value = 3000, ValueText = new TextItem{ Text = "3.0K", FontSize = 5,  TextColor = Color.FromHex("#41C7D1") } }
            };

            var groupDummyEntries1 = new List<IDataItem>
            {
                new DataItem { Value = 6, ValueText = new TextItem { Text = "6", FontSize = 4 } },
                new DataItem { Value = 8, ValueText = new TextItem { Text = "8", FontSize = 4 } },
                new DataItem { Value = 7, ValueText = new TextItem { Text = "7", FontSize = 4 } },
                new DataItem { Value = 8, ValueText = new TextItem { Text = "8", FontSize = 4 } },
                new DataItem { Value = 9, ValueText = new TextItem { Text = "9", FontSize = 4 } },
            };

            var groupDummyEntries2 = new List<IDataItem>
            {
                new DataItem { Key = 1, Value = 8 },
                new DataItem { Key = 2, Value = 7 },
                new DataItem { Key = 3, Value = 9 },
                new DataItem { Key = 5, Value = 8 },
            };

            var groupDummyEntries3 = new List<IDataItem>
            {
                new DataItem { Key = 1, Value = 4, ValueText = new TextItem { Text = "4", FontSize = 4 } },
                new DataItem { Key = 2, Value = 5 },
                new DataItem { Key = 4, Value = 6 },
                new DataItem { Key = 6, Value = 6 },
                new DataItem { Key = 7, Value = 7, ValueText = new TextItem { Text = "7", FontSize = 4 } },
            };


            DummyData = new List<DataItemGroup>();
            HRMDummyData = new List<DataItemGroup>();
            ColorDummyData = new List<DataItemGroup>();
            BarBGColorDummyData = new List<DataItemGroup>();
            MonthlyDummyData = new List<DataItemGroup>();
            GroupDummyData = new List<DataItemGroup>();

            //Single Bar Chart dataset
            var dataSet = new DataItemGroup(dummyEntries, "Weekly data");
            dataSet.Color = Color.Yellow;
            DummyData.Add(dataSet);

            var HRMDataSet = new DataItemGroup(HRMEntries, "HRM data");
            HRMDummyData.Add(HRMDataSet);

            var colorDataSet = new DataItemGroup(colorDummyEntries, "Weekly data with each entry color");
            colorDataSet.Color = Color.Green;
            ColorDummyData.Add(colorDataSet);

            var barBGColorDataSet = new DataItemGroup(barBGDummyEntries, "Weekly data with entry color and bar background color");
            BarBGColorDummyData.Add(barBGColorDataSet);

            var montlyDataSet = new DataItemGroup(monthlyDummyEntries, "Monthly data");
            MonthlyDummyData.Add(montlyDataSet);

            //Group-Bar Chart dataset
            var groupDataSet1 = new BarDataItemGroup(groupDummyEntries1, "Group data 1");
            groupDataSet1.Color = Color.FromHex("#FF49BF");
            groupDataSet1.BarBackgroundColor = Color.FromHex("#3F1933");
            GroupDummyData.Add(groupDataSet1);

            var groupDataSet2 = new BarDataItemGroup(groupDummyEntries2, "Group data 2");
            groupDataSet2.Color = Color.FromHex("#7EF954");
            groupDataSet2.BarBackgroundColor = Color.FromHex("#142D00");
            GroupDummyData.Add(groupDataSet2);

            var groupDataSet3 = new BarDataItemGroup(groupDummyEntries3, "Group data 3");
            groupDataSet3.Color = Color.FromHex("#2176FF");
            groupDataSet3.BarBackgroundColor = Color.FromHex("#0E2751");
            GroupDummyData.Add(groupDataSet3);

            var option1 = new AxisOption(true, false);
            option1.CategoryLabels = WeeklyLabel;
            option1.IsVisibleOfCategoryLabel = true;
            option1.AxisLineColor = Color.White;
            MajorOnlyAxisOption = option1;

            option1.IsVisibleOfMajorAxisLine = false;
            CategoryOnlyAxisOption = option1;
            option1.ReferenceDataItems = ReferenceData2;
            option1.IsVisibleOfReferenceLine = true;
            CategoryReferenceLineAxisOption = option1;

            var option2 = new AxisOption(true, true, true, true, true);
            option2.CategoryLabels = WeeklyLabel;
            option2.ReferenceDataItems = ReferenceData1;
            option2.AxisLineColor = Color.White;
            MajorMinorAxisOption = option2;

            var option3 = new AxisOption(true, true, true, true, true);
            option3.CategoryLabels = MonthlyLabel;
            option3.ReferenceDataItems = MontlyReferenceData;
            option3.AxisLineColor = Color.White;
            MontlyMajorMinorAxisOption = option3;
        }
    }

    public static class EntryExtension
    {
        public static void SetMonthlyDummyColor(this IDataItem entry)
        {
            if (entry.Value <= 1500)
            {
                (entry as DataItem).Color = Color.FromHex("#EF751D");
            }
            else if (entry.Value <= 2000)
            {
                (entry as DataItem).Color = Color.FromHex("#EBF51D");
            }
            else if (entry.Value <= 2500)
            {
                (entry as DataItem).Color = Color.FromHex("#72E61D");
            }
            else
            {
                (entry as DataItem).Color = Color.FromHex("#41C7D1");
            }
        }
    }
}
