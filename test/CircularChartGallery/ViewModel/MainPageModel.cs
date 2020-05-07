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
using CLabel = Tizen.Wearable.CircularUI.Chart.Forms.Label;
using CEntry = Tizen.Wearable.CircularUI.Chart.Forms.Entry;
using System.Linq;

namespace CircularUIChartGallery.ViewModel
{
    class MainPageModel
    {
        public IList<DataSet> DummyData { get; set; }

        public IList<DataSet> HRMDummyData { get; set; }

        public IList<DataSet> ColorDummyData { get; set; }

        public IList<DataSet> BarBGColorDummyData { get; set; }

        public IList<DataSet> MonthlyDummyData { get; set; }

        public IList<DataSet> GroupDummyData { get; set; }

        public IList<CEntry> ReferenceData1 { get; set; }

        public IList<CEntry> ReferenceData2 { get; set; }

        public IList<CEntry> MontlyReferenceData { get; set; }

        public IList<CategoryLabel> WeeklyLabel { get; set; }

        public IList<CategoryLabel> MonthlyLabel { get; set; }

        public AxisOption  MajorOnlyAxisOption { get; set; }

        public AxisOption CategoryOnlyAxisOption { get; set; }

        public AxisOption CategoryReferenceLineAxisOption { get; set; }

        public AxisOption MajorMinorAxisOption { get; set; }

        public AxisOption MontlyMajorMinorAxisOption { get; set; }

        public MainPageModel()
        {
            var dummyEntries = new List<IEntry>
            {
                new CEntry { Key = 1, Value = 5, ValueLabel = new CLabel { Text = "5", FontSize = 5, TextColor = Color.White } },
                new CEntry { Key = 2, Value = 8, ValueLabel = new CLabel { Text = "8", FontSize = 5, TextColor = Color.White } },
                new CEntry { Key = 4, Value = 4, ValueLabel = new CLabel { Text = "4", FontSize = 5, TextColor = Color.White } },
                new CEntry { Key = 6, Value = 10, ValueLabel = new CLabel { Text = "10", FontSize = 5, TextColor = Color.White } },
            };

            var colorDummyEntries = new List<IEntry>
            {
                new CEntry { Value = 5, Color = Color.FromHex("#00C7FF") },
                new CEntry { Value = 8, Color = Color.FromHex("#00C7FF") },
                new CEntry { Value = 4, Color = Color.FromHex("#A3B3BA"), ValueLabel = new CLabel { Text = "4", FontSize = 4, TextColor = Color.White } },
                new CEntry { Value = 9, Color = Color.FromHex("#4CBA2A"), ValueLabel = new CLabel { Text = "9", FontSize = 4, TextColor = Color.White } },
                new CEntry() { Value = 7, Color = Color.FromHex("#00C7FF") },
                new CEntry() { Value = 8, Color = Color.FromHex("#00C7FF") }
            };

            var HRMEntries = new List<IEntry>
            {
                new CEntry { Value = 3, Color = Color.DarkRed, ValueLabel = new CLabel { Text = "12m", FontSize = 9, TextColor = Color.White } },
                new CEntry { Value = 9, Color = Color.OrangeRed, ValueLabel = new CLabel { Text = "1h 3m", FontSize = 9, TextColor = Color.White } },
                new CEntry { Value = 4, Color = Color.Orange, ValueLabel = new CLabel { Text = "17m", FontSize = 9, TextColor = Color.White } }
            };

            var barBGDummyEntries = new List<IEntry>
            {
                new BarEntry { Value = 5, Color = Color.FromHex("#2176FF"), BarBackgroundColor = Color.FromHex("#0F2752") },
                new BarEntry { Key = 3, Value = 8, Color = Color.FromHex("#2176FF"), BarBackgroundColor = Color.FromHex("#0F2752") },
                new BarEntry { Value = 4, Color = Color.FromHex("#FF4AC0"), BarBackgroundColor = Color.FromHex("#401A34"), ValueLabel = new CLabel { Text = "4", FontSize = 5, TextColor = Color.White } },
                new BarEntry { Value = 9, Color = Color.FromHex("#7EFA55"), BarBackgroundColor = Color.FromHex("#152E00"), ValueLabel = new CLabel { Text = "9", FontSize = 5, TextColor = Color.White } },
                new BarEntry { Value = 7, Color = Color.FromHex("#2176FF"), BarBackgroundColor = Color.FromHex("#0F2752") },
                new BarEntry { Key = 7, Value = 7, Color = Color.FromHex("#2176FF"), BarBackgroundColor = Color.FromHex("#0F2752") }
            };

            var monthlyDummyEntries = new List<IEntry>
            {
                new CEntry { Value = 2340 },
                new CEntry { Value = 2650 },
                new CEntry { Value = 2000 },
                new CEntry { Value = 2810 },
                new CEntry { Value = 2760 },
                new CEntry { Value = 2100 },
                new CEntry { Value = 1850 },
                new CEntry { Value = 1710 },
                new CEntry { Value = 1300 },
                new CEntry { Value = 1460 },
                new CEntry { Value = 1300 },
                new CEntry { Value = 2100 },
            };

            var monthlyDummyEntries2 = new List<IEntry>
            {
                new CEntry { Value = 1200 },
                new CEntry { Value = 1340 },
                new CEntry { Value = 1490 },
                new CEntry { Value = 1550 },
                new CEntry { Value = 1630 },
                new CEntry { Value = 1800 },
                new CEntry { Value = 2200 },
                new CEntry { Value = 2370 },
                new CEntry { Value = 2400 },
                new CEntry { Value = 2450 },
                new CEntry { Value = 2300 },
                new CEntry { Value = 2650 },
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
                new CategoryLabel{Key = 1,  Label = new CLabel { Text = "S", FontSize = 7, TextColor = Color.FromHex("#FF3A3D") } },
                new CategoryLabel{Key = 2,  Label = new CLabel { Text = "M", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{Key = 3,  Label = new CLabel { Text = "T", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{Key = 4,  Label = new CLabel { Text = "W", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{Key = 5,  Label = new CLabel { Text = "T", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{Key = 6,  Label = new CLabel { Text = "F", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } },
                new CategoryLabel{Key = 7,  Label = new CLabel { Text = "S", FontSize = 7, TextColor = Color.FromHex("#FCFCFC") } }
            };

            MonthlyLabel = new List<CategoryLabel>
            {
                new CategoryLabel{Key = 1,  Label = new CLabel { Text = "JAN", FontSize = 5 } },
                new CategoryLabel{Key = 6,  Label = new CLabel { Text = "JUN", FontSize = 5 } },
                new CategoryLabel{Key = 12,  Label = new CLabel { Text = "DEC", FontSize = 5, TextColor = Color.White } }
            };

            ReferenceData1 = new List<CEntry>
            {
                new CEntry { Value = 0, ValueLabel = new CLabel{ Text = "0", FontSize = 5, TextColor = Color.White } },
                new CEntry { Value = 5, ValueLabel = new CLabel{ Text = "5", FontSize = 5, TextColor = Color.White } },
                new CEntry { Value = 10, ValueLabel = new CLabel{ Text = "10", FontSize = 5,  TextColor = Color.White } }
            };

            ReferenceData2 = new List<CEntry>
            {
                new CEntry { Value = 5},
            };

            MontlyReferenceData = new List<CEntry>
            {
                new CEntry { Value = 1500, ValueLabel = new CLabel{ Text = "1.5K", FontSize = 5, TextColor = Color.FromHex("#EF751D") } },
                new CEntry { Value = 2000, ValueLabel = new CLabel{ Text = "2.0K", FontSize = 5, TextColor = Color.FromHex("#EBF51D") } },
                new CEntry { Value = 2500, ValueLabel = new CLabel{ Text = "2.5K", FontSize = 5, TextColor = Color.FromHex("#72E61D") } },
                new CEntry { Value = 3000, ValueLabel = new CLabel{ Text = "3.0K", FontSize = 5,  TextColor = Color.FromHex("#41C7D1") } }
            };

            var groupDummyEntries1 = new List<IEntry>
            {
                new CEntry { Value = 6, ValueLabel = new CLabel { Text = "6", FontSize = 4 } },
                new CEntry { Value = 8, ValueLabel = new CLabel { Text = "8", FontSize = 4 } },
                new CEntry { Value = 7, ValueLabel = new CLabel { Text = "7", FontSize = 4 } },
                new CEntry { Value = 8, ValueLabel = new CLabel { Text = "8", FontSize = 4 } },
                new CEntry { Value = 9, ValueLabel = new CLabel { Text = "9", FontSize = 4 } },
            };

            var groupDummyEntries2 = new List<IEntry>
            {
                new CEntry { Key = 1, Value = 8 },
                new CEntry { Key = 2, Value = 7 },
                new CEntry { Key = 3, Value = 9 },
                new CEntry { Key = 5, Value = 8 },
            };

            var groupDummyEntries3 = new List<IEntry>
            {
                new CEntry { Key = 1, Value = 4, ValueLabel = new CLabel { Text = "4", FontSize = 4 } },
                new CEntry { Key = 2, Value = 5 },
                new CEntry { Key = 4, Value = 6 },
                new CEntry { Key = 6, Value = 6 },
                new CEntry { Key = 7, Value = 7, ValueLabel = new CLabel { Text = "7", FontSize = 4 } },
            };


            DummyData = new List<DataSet>();
            HRMDummyData = new List<DataSet>();
            ColorDummyData = new List<DataSet>();
            BarBGColorDummyData = new List<DataSet>();
            MonthlyDummyData = new List<DataSet>();
            GroupDummyData = new List<DataSet>();

            //Single Bar Chart dataset
            var dataSet = new DataSet(dummyEntries, "Weekly data");
            dataSet.Color = Color.Yellow;
            DummyData.Add(dataSet);

            var HRMDataSet = new DataSet(HRMEntries, "HRM data");
            HRMDummyData.Add(HRMDataSet);

            var colorDataSet = new DataSet(colorDummyEntries, "Weekly data with each entry color");
            colorDataSet.Color = Color.Green;
            ColorDummyData.Add(colorDataSet);

            var barBGColorDataSet = new DataSet(barBGDummyEntries, "Weekly data with entry color and bar background color");
            BarBGColorDummyData.Add(barBGColorDataSet);

            var montlyDataSet = new DataSet(monthlyDummyEntries, "Monthly data");
            MonthlyDummyData.Add(montlyDataSet);

            //Group-Bar Chart dataset
            var groupDataSet1 = new BarDataSet(groupDummyEntries1, "Group data 1");
            groupDataSet1.Color = Color.FromHex("#FF49BF");
            groupDataSet1.BarBackgroundColor = Color.FromHex("#3F1933");
            GroupDummyData.Add(groupDataSet1);

            var groupDataSet2 = new BarDataSet(groupDummyEntries2, "Group data 2");
            groupDataSet2.Color = Color.FromHex("#7EF954");
            groupDataSet2.BarBackgroundColor = Color.FromHex("#142D00");
            GroupDummyData.Add(groupDataSet2);

            var groupDataSet3 = new BarDataSet(groupDummyEntries3, "Group data 3");
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
            option1.ReferenceDatas = ReferenceData2;
            option1.IsVisibleOfReferenceLine = true;
            CategoryReferenceLineAxisOption = option1;

            var option2 = new AxisOption(true, true, true, true, true);
            option2.CategoryLabels = WeeklyLabel;
            option2.ReferenceDatas = ReferenceData1;
            option2.AxisLineColor = Color.White;
            MajorMinorAxisOption = option2;

            var option3 = new AxisOption(true, true, true, true, true);
            option3.CategoryLabels = MonthlyLabel;
            option3.ReferenceDatas = MontlyReferenceData;
            option3.AxisLineColor = Color.White;
            MontlyMajorMinorAxisOption = option3;
        }
    }

    public static class EntryExtension
    {
        public static void SetMonthlyDummyColor(this IEntry entry)
        {
            if (entry.Value <= 1500)
            {
                (entry as CEntry).Color = Color.FromHex("#EF751D");
            }
            else if (entry.Value <= 2000)
            {
                (entry as CEntry).Color = Color.FromHex("#EBF51D");
            }
            else if (entry.Value <= 2500)
            {
                (entry as CEntry).Color = Color.FromHex("#72E61D");
            }
            else
            {
                (entry as CEntry).Color = Color.FromHex("#41C7D1");
            }
        }
    }
}
