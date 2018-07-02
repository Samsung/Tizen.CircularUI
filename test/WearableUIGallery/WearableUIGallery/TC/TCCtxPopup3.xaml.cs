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
using System.Windows.Input;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace WearableUIGallery.TC
{
 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCCtxPopup3 : CirclePage
    {
        ContextPopupEffectBehavior _behavior;
        bool _visibility = false;

        public bool Visibility
        {
            get => _visibility;
            set
            {
                if (_visibility != value)
                {
                    Console.WriteLine($"Visibility: {_visibility}");
                    _visibility = value;
                    OnPropertyChanged();
                }
            }
        }


        public TCCtxPopup3()
        {
            CtxCheck1AcceptedCommand = new Command(
                execute: () =>
                {
                    Console.WriteLine("CtxCheck1AcceptedCommand  excute");
                });
            CtxCheck1CancelCommand = new Command(
                execute: () =>
                {
                    Console.WriteLine("CtxCheck1CancelCommand excute");
                });

            InitializeComponent();

            CtxCheck1.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == Check.IsToggledProperty.PropertyName)
                    System.Diagnostics.Debug.WriteLine($"IsToggled of CtxCheck1= {CtxCheck1.IsToggled}");
            };
        }

        void makeContextPopupBehavior()
        {
            if (_behavior == null)
            {
                _behavior = new ContextPopupEffectBehavior()
                {
                    AcceptCommand = CtxCheck1AcceptedCommand,
                    AcceptText = "Yes",
                    CancelCommand = CtxCheck1CancelCommand,
                    CancelText = "No",
                    PositionOption = PositionOption.BottomOfView,
                };

                _behavior.SetBinding(ContextPopupEffectBehavior.VisibilityProperty, "Visibility", BindingMode.TwoWay, new ValueConverter());
                _behavior.BindingContext = this;
                labelOfVisibilityValue.SetBinding(Label.TextProperty, "Visibility", BindingMode.Default, new ValueConverter());
                labelOfVisibilityValue.BindingContext = this;

                CtxCheck1.Behaviors.Add(_behavior);
            }
        }

        void OnClickAttach(object sender, EventArgs args)
        {
            makeContextPopupBehavior();
            labelOfStateValue.Text = "Attached";
        }

        void OnClickDetach(object sender, EventArgs args)
        {
            CtxCheck1.Behaviors.Clear();
            labelOfStateValue.Text = "Detached";
            _behavior = null;
        }
        void OnClickVisibility(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if(_behavior == null ) return;

            if (!Visibility)
            {
                Visibility = true;
            }
        }

        public ICommand CtxCheck1AcceptedCommand { get; private set; }
        public ICommand CtxCheck1CancelCommand { get; private set; }

        class ValueConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ((bool)value);

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
        }
    }
}