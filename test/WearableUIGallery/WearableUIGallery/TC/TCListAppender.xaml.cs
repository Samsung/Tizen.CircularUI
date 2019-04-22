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
using System.Collections.ObjectModel;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCListAppender : TwoButtonPage
    {
        public class MyData
        {
            public string Text { get; set; }
        }

        ObservableCollection<MyData> myDatas;

        public TCListAppender ()
        {
            myDatas = new ObservableCollection<MyData>();

            for (int i = 1; i <= 3; ++i)
            {
                myDatas.Add(new MyData{ Text = string.Format("TestItem{0}", i) });
            }

            InitializeComponent ();

            mylist.ItemTemplate = new DataTemplate(() =>
            {
                var cell = new TextCell();
                cell.SetBinding(TextCell.TextProperty, new Binding("Text"));
                cell.BindingContextChanged += (s, e) =>
                {
                    if (String.IsNullOrEmpty(cell.AutomationId))
                        cell.AutomationId = cell.Text;
                };
                return cell;
            });

            mylist.ItemsSource = myDatas;
        }

        void DoAdd(object sender, EventArgs e)
        {
            myDatas.Add(new MyData{ Text = string.Format("TestItem{0}", myDatas.Count + 1) });
        }

        void DoDel(object sender, EventArgs e)
        {
            if (myDatas.Count > 0)
                myDatas.RemoveAt(myDatas.Count - 1);
        }
    }
}