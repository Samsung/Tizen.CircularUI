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
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using CircularUI;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCTwoButtonPage : TwoButtonPage
	{
        MenuItem tmp1, tmp2;
		public TCTwoButtonPage ()
		{
			InitializeComponent ();
		}

        public void OnRemove1(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if (btn.Text == "Remove 1")
            {
                tmp1 = FirstButton;
                FirstButton = null;
                btn.Text = "Add 1";
            }
            else
            {
                FirstButton = tmp1;
                btn.Text = "Remove 1";
            }
        }
        public void OnRemove2(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if (btn.Text == "Remove 2")
            {
                tmp2 = SecondButton;
                SecondButton = null;
                btn.Text = "Add 2";
            }
            else
            {
                SecondButton = tmp2;
                btn.Text = "Remove 2";
            }
        }
    }

    public class TCTwoButtonPageViewModel : INotifyPropertyChanged
    {
        string _text;
        string _title;
        ICommand _command1;
        ICommand _command2;

        const string _myContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque odio purus, vulputate a porttitor non, iaculis id nisi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Aenean feugiat ut justo ac porta. Nullam sit amet fermentum lectus. Vivamus id ante id felis fermentum finibus nec nec mi. Ut ac lectus id purus venenatis posuere a hendrerit sapien. Aliquam urna felis, aliquam vehicula egestas sed, commodo et risus. Sed suscipit sapien vel diam lacinia, quis vehicula eros egestas. Donec in ultricies nulla. Donec ullamcorper neque vitae neque ullamcorper pharetra. In non risus scelerisque, malesuada sapien ut, vulputate urna. Morbi sed justo eu lacus ornare viverra at id felis.";

        public TCTwoButtonPageViewModel()
        {
            _text = _myContent;
            _title = "MyTitle";
            _command1 = new Command(() => Title = string.IsNullOrEmpty(Title) ? "MyTitle" : "");
            _command2 = new Command(() => Text = string.IsNullOrEmpty(Text) ? _myContent : "");
        }

        public string Text
        {
            get => _text;
            set
            {
                if (_text == value) return;
                _text = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public ICommand Command1 => _command1;
        public ICommand Command2 => _command2;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}