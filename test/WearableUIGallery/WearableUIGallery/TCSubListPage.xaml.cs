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
using System.Linq;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery
{
    [QueryProperty("TCName", "tc")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCSubListPage : CirclePage
    {
        string _tcName = "";

        public TCSubListPage()
        {
            InitializeComponent ();
        }

        public string TCName
        {
            set
            {
                _tcName = value;
                var desc = TCData.TCs.FirstOrDefault(tc => tc.Title == Uri.UnescapeDataString(value));
                if(desc != null)
                {
                    BindingContext = desc.Class;
                }
            }
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            if (args.Item == null) return;

            var desc = args.Item as TCDescribe;
            Shell.Current.GoToAsync(_tcName + "/" + desc.Title);
        }
    }

}